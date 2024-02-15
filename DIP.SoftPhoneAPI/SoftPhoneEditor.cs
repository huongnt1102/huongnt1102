using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;

using DIP.SoftPhoneSDK.Common.CallControl;
using DIP.SoftPhoneSDK.Sip;
using DIP.SoftPhoneSDK.Common;

namespace DIP.SoftPhoneAPI
{
    public partial class SoftPhoneEditor : DevExpress.XtraEditors.XtraForm
    {
        #region Private field
        private string CallID;
        private MediaPlayer Music;
        private HistoryEntity _History = new HistoryEntity();
        #endregion

        #region Properties
        // Get call manager instance
        CCallManager CallManager
        {
            get { return CCallManager.Instance; }
        }

        private PhoneConfig _config;
        internal PhoneConfig Config
        {
            get { return _config; }
        }

        private IStateMachine _call = null;

        #endregion

        public SoftPhoneEditor(string _Server, int _Port, string _LineName, string _Line, string _Password)
        {
            InitializeComponent();

            try
            {
                this.OffLine();

                var account = new AccountConfig();
                account.AccountName = _Line;
                account.DisplayName = _Line;
                account.Id = _Line;
                account.UserName = _Line;
                account.HostName = _Server;
                account.Password = _Password;
                _config = new PhoneConfig(account);
                _config.SIPPort = _Port;

                // register callbacks
                CallManager.CallStateRefresh += new DCallStateRefresh(CallManager_CallStateRefresh);
                pjsipRegistrar.Instance.AccountStateChanged += new DIP.SoftPhoneSDK.Common.DAccountStateChanged(Instance_AccountStateChanged);

                // Inject VoIP stack engine to CallManager
                CallManager.StackProxy = pjsipStackProxy.Instance;

                // Inject configuration settings SipekSdk
                CallManager.Config = Config;
                pjsipStackProxy.Instance.Config = Config;
                pjsipRegistrar.Instance.Config = Config;

                // Initialize
                CallManager.Initialize();
                // register accounts...
                pjsipRegistrar.Instance.registerAccounts();

                itemLineName.Caption = "Máy nhánh: " + _LineName;
                this.Music = new MediaPlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Disposed += new EventHandler(SoftPhoneEditor_Disposed);
        }

        #region Callbacks
        void Instance_AccountStateChanged(int accountId, int accState)
        {
            // MUST synchronize threads
            if (InvokeRequired)
                this.BeginInvoke(new DAccountStateChanged(OnRegistrationUpdate), new object[] { accountId, accState });
            else
                OnRegistrationUpdate(accountId, accState);
        }

        void CallManager_CallStateRefresh(int sessionId)
        {
            try
            {   // MUST synchronize threads
                if (InvokeRequired)
                    this.BeginInvoke(new DCallStateRefresh(OnStateUpdate), new object[] { sessionId });
                else
                    OnStateUpdate(sessionId);
            }
            catch { }
        }
        #endregion

        #region Synhronized callbacks
        private void OnRegistrationUpdate(int accountId, int accState)
        {
            if (accState.ToString() == "200")
            {
                this.OnLine();
            }
            else
            {
                this.OffLine();
            }
        }

        int endCall = 0;
        private void OnStateUpdate(int sessionId)
        {
            if (_call != null && _call.Session != sessionId)
            {
                return;
            }

            _call = CallManager.getCall(sessionId);

            

            switch (_call.StateId)
            {
                case EStateId.ALERTING: //Đỗ chuông gọi đi
                    endCall = 0;
                    lblStatus.Text = "Đang gọi..";

                    var thread = new System.Threading.Thread(RegiterBeginCallEvent);
                    thread.IsBackground = true;
                    thread.Start();

                    if (_History.ID > 0)
                    {
                        this.UpdateHistory();
                    }
                    _History = new HistoryEntity();
                    _History.Number = _call.CallingNumber;
                    _History.Type = 1;
                    _History.Date = DateTime.Now;
                    this.UpdateHistory();
                    break;
                case EStateId.INCOMING: //Đổ chuông gọi đến
                    tabSoftPhone.SelectedTabPageIndex = 0;

                    endCall = 0;
                    
                    lblStatus.Text = "Cuộc gọi đến..";
                    this.CallID = _call.CallingNumber;
                    txtNumber.Text = _call.CallingNumber;
                    Music.Play();

                    var th = new System.Threading.Thread(RegiterBeginCallEvent);
                    th.IsBackground = true;
                    th.Start();

                    btnAns.Enabled = true;
                    btnCall.Enabled = false;
                    btnGacMay.Enabled = true;
                    btnChuyen.Enabled = true;
                    btnGiuMay.Enabled = false;

                    if (_History.ID > 0)
                    {
                        this.UpdateHistory();
                    }

                    _History = new HistoryEntity();
                    _History.Number = _call.CallingNumber;
                    _History.Type = 2;
                    _History.Date = DateTime.Now;
                    this.UpdateHistory();

                    if (this.Visible == false)
                    {
                        this.ShowForm();
                    }

                    this.Activate();

                    break;
                case EStateId.ACTIVE: //Bắt đầu đàm thoại
                    endCall = 0;
                    timerCallLive.Start();
                    Music.Stop();

                    btnCall.Enabled = false;
                    btnAns.Enabled = false;
                    btnGacMay.Enabled = true;
                    btnGiuMay.Enabled = true;
                    btnChuyen.Enabled = true;

                    if (_History.Type == 2)
                    {
                        _History.Type = 3;
                        _History.Update();
                    }
                    break;
                case EStateId.TERMINATED: //Máy khách gác máy
                case EStateId.RELEASED: //Exten gác máy
                case EStateId.IDLE: //Exten chủ bận
                case EStateId.NULL: //Máy khách bận
                    if (endCall == 0)
                    {
                        var time = lblStatus.Text;

                        lblStatus.Text = "00:00:00";
                        Music.Stop();
                        timerCallLive.Stop();
                        btnSave.Enabled = true;
                        btnCall.Enabled = true;
                        btnDialDigit.Enabled = false;
                        btnGacMay.Enabled = false;
                        btnGiuMay.Enabled = false;
                        btnAns.Enabled = false;
                        btnChuyen.Enabled = false;

                        _History.Duration = time.IndexOf(':') > 0 ? time : "00:00:00";
                        UpdateHistory();

                        if (_History.Type == 2)
                        {
                            this.ResfeshMissed(_History.SetMissed());
                        }

                        endCall = 1;

                        if (_call.StateId == EStateId.NULL)
                            MessageBox.Show("Máy bận, vui lòng gọi lại sau..");
                    }
                    _call = null;
                    break;
            }
        }
        #endregion

        #region Properties Event
        public event BeginCallEventHandler BeginCall;
        public event CustomerAddNewButtonClickEventHandler CustomerAddNewButtonClick;
        public event CustomerEditButtonClickEventHandler CustomerEditButtonClick;
        public event CustomerSearchButtonClickEventHandler CustomerSearchButtonClick;
        public event ContactAddNewButtonClickEventHandler ContactAddNewButtonClick;
        public event ReferenceButtonClickEventHandler ReferenceButtonClick;
        public event SaveButtonClickEventHandler SaveButtonClick;
        public event HistoryPageViewEventHandler HistoryPageView;
        public event TransactionPageViewEventHandler TransactionPageView;
        public event ContactPageViewEventHandler ContactPageView;
        public event YeuCauPageViewEventHandler YeuCauPageView;
        public event CongNoPageViewEventHandler CongNoPageView;
        public event CallHistoryBeginEditEventHandler CallHistoryBeginEdit;
        public event ReferenceValueChangedEventHandler ReferenceValueChanged;
        #endregion

        #region Properties View
        public bool ShowTransactionPage { get { return pageTransaction.PageVisible; } set { pageTransaction.PageVisible = value; } }
        public bool ShowHistoryPage { get { return pageHistory.PageVisible; } set { pageHistory.PageVisible = value; } }
        public bool ShowContactPage { get { return pageContact.PageVisible; } set { pageContact.PageVisible = value; } }
        public bool EnabledStatusControl { get { return lkTrangThai.Enabled; } set { lkTrangThai.Enabled = value; } }
        public bool ShowContactButton { get { return btnContactAdd.Visible; } set { btnContactAdd.Visible = value; } }
        public string StatusLabel { get { return lblStatusDB.Text; } set { lblStatusDB.Text = value; colStatus.Caption = value; } }
        #endregion

        #region Private method
        delegate void UpdateControls();
        private string FormatPhone(string number)
        {
            var str = ".,; ()-_";
            for (var i = 0; i < str.Length; i++)
            {
                number = number.Replace(str.Substring(i, 1), "");
            }
            return number;
        }

        private void ShowForm()
        {
            try
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            catch { }

        }

        private void LoadCustomer(List<Customer> data)
        {
            try
            {
                lkKhachHang.Properties.DataSource = null;
                lkKhachHang.Properties.DataSource = data;
                if (data.Count > 0)
                    lkKhachHang.EditValue = data[0].ID;
                else
                    CustomerDetailReset();
            }
            catch { }
        }

        private void CustomerDetailReset()
        {
            try
            {
                lkKhachHang.EditValue = null;
            }
            catch { }
        }

        private void RegiterBeginCallEvent()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new UpdateControls(() => CustomerDetailReset()));
            }
            else
            {
                this.CustomerDetailReset();
            }

            if (this.BeginCall != null)
            {
                var args = new BeginCallEventArgs();
                args.Number = this.CallID;

                this.BeginCall(this, args);

                if (args.Customers != null)
                {
                    if (InvokeRequired)
                    {
                        this.BeginInvoke(new UpdateControls(() => LoadCustomer(args.Customers)));
                    }
                    else
                    {
                        LoadCustomer(args.Customers);
                    }
                }
            }
        }

        private void OffLine()
        {
            itemStatus.Caption = "Offline";
            this.Icon = Properties.Resources.phone_offline;
            notifyIcon1.Icon = Properties.Resources.phone_offline;
        }

        private void OnLine()
        {
            itemStatus.Caption = "Online";
            this.Icon = Properties.Resources.phone_online;
            notifyIcon1.Icon = Properties.Resources.phone_online;
        }
        #endregion

        #region Public Method
        public bool Call(string number)
        {
            if (number == null || number.Trim().Length == 0) return false;

            try
            {
                endCall = 0;
                lblStatus.Text = "Đang kết nối..";
                tabSoftPhone.SelectedTabPageIndex = 0;

                this.CallID = this.FormatPhone(number);
                txtNumber.Text = this.CallID;

                btnGacMay.Enabled = true;
                btnDialDigit.Enabled = true;
                btnCall.Enabled = false;
                btnAns.Enabled = false;
                btnGiuMay.Enabled = false;
                btnChuyen.Enabled = false;
                btnSave.Enabled = false;

                this.Update();

                CallManager.CreateSimpleOutboundCall(this.CallID);

                if (this.Visible == false)
                {
                    this.ShowForm();
                }

                this.Activate();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                MessageBox.Show(ex.Message);
            }
        }

        public bool Call(string number, int CustomerID)
        {
            var result = this.Call(number);

            lkKhachHang.EditValue = CustomerID;

            return result;
        }

        public bool Call(string number, int CustomerID, int LinkType, int LinkID, string LinkName)
        {
            var result = this.Call(number, CustomerID);

            btnThamChieu.Tag = string.Format("{0}:{1}", LinkType, LinkID);
            btnThamChieu.Text = LinkName;

            return result;
        }
        #endregion

        #region Function button event
        private void insertNumber(object sender, EventArgs e)
        {
            var button = (SimpleButton)sender;
            txtNumber.Text = txtNumber.Text + button.Tag.ToString();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            this.Call(txtNumber.Text);
        }

        private void btnDialDigit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new frmInputBox())
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        CallManager.OnUserDialDigit(_call.Session, frm.TextValue, EDtmfMode.DM_Outband);
                    }
                }
            }
            catch { }
        }

        private void btnGacMay_Click(object sender, EventArgs e)
        {
            try
            {
                CallManager.OnUserRelease(_call.Session);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAns_Click(object sender, EventArgs e)
        {
            try
            {
                CallManager.OnUserAnswer(_call.Session);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            try
            {
                CallManager.OnUserRelease(_call.Session);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGiuMay_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnGiuMay.Tag.ToString() == "1")
                {
                    btnGiuMay.Tag = 0;
                    btnGiuMay.Text = "Bỏ giữ máy";
                }
                else
                {
                    btnGiuMay.Tag = 1;
                    btnGiuMay.Text = "Giữ máy";
                }

                CallManager.OnUserHoldRetrieve(_call.Session);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmInputBox();
                frm.ShowDialog(this);
                if (frm.DialogResult != System.Windows.Forms.DialogResult.OK) return;

                CallManager.OnUserTransfer(_call.Session, frm.TextValue);

                MessageBox.Show("Cuộc gọi đã được chuyển tiếp sang số: " + frm.TextValue, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region lich su cuoc goi
        private void UpdateHistory()
        {
            try
            {
                if (lkKhachHang.EditValue != null)
                {
                    _History.CusID = (int)lkKhachHang.EditValue;
                    _History.CusName = lkKhachHang.Text;
                }
                if (btnThamChieu.Tag != null)
                {
                    var arrThamChieu = btnThamChieu.Tag.ToString().Split(':');
                    _History.RefType = int.Parse(arrThamChieu[0]);
                    _History.RefID = int.Parse(arrThamChieu[1]);
                    _History.RefName = btnThamChieu.Text;
                }
                if (lkTrangThai.EditValue != null)
                {
                    _History.StatusID = (int)lkTrangThai.EditValue;
                }
                _History.Note = txtDienGiai.Text;

                if (_History.ID == 0)
                    _History.Add();
                else
                    _History.Update();
            }
            catch { }
        }
        #endregion

        private void timerCallLive_Tick(object sender, EventArgs e)
        {
            timerCallLive.Stop();

            try
            {
                var arrTime = lblStatus.Text.Split(':');

                var giay = int.Parse(arrTime[2]);
                var phut = int.Parse(arrTime[1]);
                var gio = int.Parse(arrTime[0]);

                giay++;
                if (giay == 60)
                {
                    phut++;
                    giay = 0;
                }

                if (phut == 60)
                {
                    gio++;
                    phut = 0;
                    giay = 0;
                }

                lblStatus.Text = string.Format("{0:00}:{1:00}:{2:00}", gio, phut, giay);
            }
            catch
            {
                lblStatus.Text = "00:00:00";
            }

            timerCallLive.Start();
        }

        private void ReferenceChanged(int? type, int? id)
        {
            if (this.ReferenceValueChanged != null)
            {
                var args = new ReferenceValueChangedEventArgs();
                args.LinkType = type;
                args.LinkID = id;

                this.ReferenceValueChanged(this, args);

                lkTrangThai.Properties.DataSource = null;
                lkTrangThai.Properties.DataSource = args.StatusList;
            }
            else
            {
                lkTrangThai.Properties.DataSource = null;
            }

            lkTrangThai.EditValue = null;
        }

        private void lkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            if (lkKhachHang.EditValue != null)
            {
                txtTenLKH.EditValue = lkKhachHang.GetColumnValue("Type");
                //txtHoTen.EditValue = lkKhachHang.GetColumnValue("FullName");
                txtMaKH.EditValue = lkKhachHang.GetColumnValue("Code");
                txtMaSoMB.EditValue = lkKhachHang.GetColumnValue("MaSoMB");
                txtEmail.EditValue = lkKhachHang.GetColumnValue("Email");
                txtDiaChi.EditValue = lkKhachHang.GetColumnValue("Address");
                txtGhiChu.EditValue = lkKhachHang.GetColumnValue("Note");
                btnThamChieu.Tag = string.Format("0:{0}", lkKhachHang.EditValue);
                btnThamChieu.Text = "Khách hàng " + lkKhachHang.Text;

                this.ReferenceChanged(0, (int)lkKhachHang.EditValue);

                lkTrangThai.EditValue = lkKhachHang.GetColumnValue("StatusID");
            }
            else
            {
                txtTenLKH.Text = "";
                //txtHoTen.Text = "";
                txtMaSoMB.Text = "";
                txtMaKH.Text = "";
                txtEmail.Text = "";
                txtDiaChi.Text = "";
                txtGhiChu.Text = "";
                btnThamChieu.Tag = null;
                btnThamChieu.Text = "";

                lkTrangThai.Properties.DataSource = null;
                lkTrangThai.EditValue = null;
            }

            this.Update();
        }

        private void lkKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1:
                    if (this.CustomerAddNewButtonClick != null)
                    {
                        var args = new CustomerAddNewButtonClickEventArgs();
                        args.Number = txtNumber.Text;
                        this.CustomerAddNewButtonClick(this, args);
                        if (args.Customer != null)
                        {
                            var ltCustomer = new List<Customer>();
                            ltCustomer.Add(args.Customer);

                            LoadCustomer(ltCustomer);
                        }
                    }
                    break;
                case 2:
                    if (this.CustomerEditButtonClick != null)
                    {
                        var args = new CustomerEditButtonClickEventArgs();
                        args.ID = (int?)lkKhachHang.EditValue;
                        args.Number = txtNumber.Text;
                        this.CustomerEditButtonClick(this, args);
                        if (args.Customer != null)
                        {
                            var ltCustomer = new List<Customer>();
                            ltCustomer.Add(args.Customer);
                            LoadCustomer(ltCustomer);
                        }
                    }
                    break;
                case 3:
                    if (this.CustomerSearchButtonClick != null)
                    {
                        var args = new CustomerSearchButtonClickEventArgs();
                        this.CustomerSearchButtonClick(this, args);
                        if (args.Customer != null)
                        {
                            var ltCustomer = new List<Customer>();
                            ltCustomer.Add(args.Customer);
                            LoadCustomer(ltCustomer);
                            txtNumber.Text = args.Customer.PhoneNumber;
                        }
                    }
                    break;
            }
        }

        private void btnThamChieu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (lkKhachHang.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng", "Thông báo");
                return;
            }

            if (this.ReferenceButtonClick != null)
            {
                var args = new ReferenceButtonClickEventArgs();
                args.CusID = (int)lkKhachHang.EditValue;
                this.ReferenceButtonClick(this, args);
                if (args.LinkID != null)
                {
                    btnThamChieu.Tag = string.Format("{0}:{1}", args.LinkType, args.LinkID);
                    btnThamChieu.Text = args.LinkName;

                    this.ReferenceChanged(args.LinkType, args.LinkID);

                    lkTrangThai.EditValue = args.StatusID;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lkKhachHang.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng", "Thông báo");
                return;
            }

            if (txtDienGiai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập diễn giải", "Thông báo");
                return;
            }

            if (this.SaveButtonClick != null)
            {
                var args = new SaveButtonClickEventArgs();
                args.CallID = this.CallID;
                args.CusID = (int)lkKhachHang.EditValue;
                args.Note = txtDienGiai.Text.Trim();

                var arrThamChieu = btnThamChieu.Tag.ToString().Split(':');
                args.LinkType = int.Parse(arrThamChieu[0]);
                args.LinkID = int.Parse(arrThamChieu[1]);

                if (lkTrangThai.EditValue != null)
                    args.StatusID = Convert.ToInt32(lkTrangThai.EditValue);
                
                this.SaveButtonClick(this, args);
                if (args.Result)
                {
                    MessageBox.Show("Dữ liệu đã được lưu", "Thông báo");

                    _History.SaveDB = true;
                    _History.Update();

                    _History = new HistoryEntity();
                    
                    txtDienGiai.Text = "";                    
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, không thể lưu", "Thông báo");
                }
            }
        }

        private void btnCacel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void tabSoftPhone_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (e.Page.Name)
            {
                case "pageHistory":
                    if (lkKhachHang.EditValue == null)
                    {
                        gcHistory.DataSource = null;
                    }
                    else if (this.HistoryPageView != null)
                    {
                        var args = new HistoryPageViewEventArgs();
                        args.CustomerID = (int)lkKhachHang.EditValue;
                        this.HistoryPageView(this, args);
                        gcHistory.DataSource = args.Data;
                    }
                    break;
                case "pageTransaction":
                    if (lkKhachHang.EditValue == null)
                    {
                        gcTransaction.DataSource = null;
                    }
                    else if (this.TransactionPageView != null)
                    {
                        var args = new TransactionPageViewEventArgs();
                        args.CustomerID = (int)lkKhachHang.EditValue;
                        this.TransactionPageView(this, args);
                        gcTransaction.DataSource = args.Data;
                    }
                    break;
                case "pageContact":
                    if (lkKhachHang.EditValue == null)
                    {
                        gcContact.DataSource = null;
                    }
                    else if (this.ContactPageView != null)
                    {
                        var args = new ContactPageViewEventArgs();
                        args.CustomerID = (int)lkKhachHang.EditValue;
                        this.ContactPageView(this, args);
                        gcContact.DataSource = args.Data;
                    }
                    break;
                case "pageCallHistory":
                    gcCallHistory.DataSource = _History.GetData();
                    this.ResfeshMissed(_History.ResetMissed());
                    break;
                case "pageYeuCau":
                    if (lkKhachHang.EditValue == null)
                    {
                        gcYeuCau.DataSource = null;
                    }
                    else if (this.YeuCauPageView != null)
                    {
                        var args = new YeuCauPageViewEventArgs();
                        args.CustomerID = (int)lkKhachHang.EditValue;
                        this.YeuCauPageView(this, args);
                        gcYeuCau.DataSource = args.Data;
                    }
                    break;
                case "pageCongNo":
                    if (lkKhachHang.EditValue == null)
                    {
                        gcCongNo.DataSource = null;
                    }
                    else if (this.CongNoPageView != null)
                    {
                        var args = new CongNoPageViewEventArgs();
                        args.CustomerID = (int)lkKhachHang.EditValue;
                        this.CongNoPageView(this, args);
                        gcCongNo.DataSource = args.Data;
                    }
                    break;
            }
        }

        private void SoftPhone_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.Visible = false;
            }
            catch { }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.ShowForm();
        }

        private void SoftPhoneEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void SoftPhoneEditor_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (_call != null)
                {
                    CallManager.OnUserRelease(_call.Session);
                    _call = null;
                }

                CallManager.Shutdown();
            }
            catch { }

            try
            {
                notifyIcon1.Dispose();
                notifyIcon1 = null;
            }
            catch { }
        }

        private void ResfeshMissed(int missed)
        {
            if (missed > 0)
            {
                pageCallHistory.Text = string.Format("Cuộc gọi nhỡ ({0})", missed);
                pageCallHistory.Appearance.Header.Font = new Font(pageCallHistory.Appearance.Header.Font, FontStyle.Bold);
            }
            else
            {
                pageCallHistory.Text = "Lịch sử cuộc gọi";
                pageCallHistory.Appearance.Header.Font = new Font(pageCallHistory.Appearance.Header.Font, FontStyle.Regular);
            }
        }

        private void LoadLines()
        {
            try
            {
                gcMayNhanh.DataSource = HistoryCls.Lines;
            }
            catch { }
        }

        private void SoftPhoneEditor_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadLines();

                this.ResfeshMissed(_History.GetMissed());
            }
            catch { }
        }

        private void btnContactAdd_Click(object sender, EventArgs e)
        {
            if (this.ContactAddNewButtonClick != null)
            {
                var args = new ContactAddNewButtonClickEventArgs();
                args.CustomerID = (int?)lkKhachHang.EditValue;
                args.Number = txtNumber.Text;
                this.ContactAddNewButtonClick(this, args);
                if (args.Result == true)
                {
                    tabSoftPhone.SelectedTabPage = pageContact;
                }
            }
        }

        private void gvCallHistory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ppLichSuCuocGoi.ShowPopup(Cursor.Position);
            }
        }

        private void itemCallHistory_Call_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var number = gvCallHistory.GetFocusedRowCellValue("Number").ToString();
            this.Call(number);
        }

        private void itemCallHistory_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (int)gvCallHistory.GetFocusedRowCellValue("ID");

                _History = new HistoryEntity().Get(id);

                this.CallID = _History.Number;

                txtNumber.EditValue = _History.Number;
                lkKhachHang.EditValue = _History.CusID;
                btnThamChieu.Text = _History.RefName;
                btnThamChieu.Tag = string.Format("{0}:{1}", _History.RefType, _History.RefID);
                lkTrangThai.EditValue = _History.StatusID;
                txtDienGiai.EditValue = _History.Note;
                tabSoftPhone.SelectedTabPageIndex = 0;

                if (this.CallHistoryBeginEdit != null)
                {
                    var arg = new CallHistoryBeginEditEventArgs();
                    arg.Number = _History.Number;
                    arg.CusID = _History.CusID;

                    this.CallHistoryBeginEdit(this, arg);
                    if (arg.Customers != null)
                    {
                        LoadCustomer(arg.Customers);
                    }
                }

                this.ReferenceChanged(_History.RefType, _History.RefID);
                lkTrangThai.EditValue = _History.StatusID;
            }
            catch { }
        }

        private void itemCallHistory_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvCallHistory.GetSelectedRows();
                if (indexs.Length == 0) return;

                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;


                var arrID = new List<int>();
                foreach (var i in indexs)
                {
                    arrID.Add((int)gvCallHistory.GetRowCellValue(i, "ID"));
                }

                _History.Delete(arrID);

                gcCallHistory.DataSource = null;
                gcCallHistory.DataSource = _History.GetData();
            }
            catch { }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ppNotify.ShowPopup(Cursor.Position);
            }
        }

        private void itemOnline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                pjsipRegistrar.Instance.registerAccounts();
            }
            catch { }
        }

        private void itemOffline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                pjsipRegistrar.Instance.unregisterAccounts();

                this.OffLine();
            }
            catch { }
        }

        private void itemShowForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ShowForm();
        }

        private void itemHideForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void itemMayNhanh_Goi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var _Number = gvMayNhanh.GetFocusedRowCellValue("ID").ToString();
                this.Call(_Number);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemMayNhanh_Chuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var _Number = gvMayNhanh.GetFocusedRowCellValue("ID").ToString();
                CallManager.OnUserTransfer(_call.Session, _Number);
                MessageBox.Show(string.Format("Cuộc gọi đã được chuyển tiếp sang máy nhánh {0} của {1}", _Number, gvMayNhanh.GetFocusedRowCellValue("Name").ToString()),
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}