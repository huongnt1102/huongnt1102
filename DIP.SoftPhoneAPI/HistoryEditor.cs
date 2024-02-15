using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace DIP.SoftPhoneAPI
{
    public partial class HistoryEditor : DevExpress.XtraEditors.XtraUserControl
    {
        public HistoryEditor()
        {
            InitializeComponent();
        }

        public string ip = "";
        public string URLWS = "";
        public string wsuser = "";
        public string wssecret = "";
        public List<CallLine> ltLine = null;
        public event CallHistoryRowClickEventHandler CallHistoryRowClick;

        string getLine(string src)
        {
            var line = ltLine.SingleOrDefault(p => p.ID == src);
            if (line != null)
            {
                return line.Name;
            }
            else
            {
                return src;
            }
        }

        void loadData()
        {
            try
            {
                cdrgw.OneLotusGW robo = new cdrgw.OneLotusGW();

                cdrgw.cdrRequest apprequested = new cdrgw.cdrRequest();

                apprequested.userlogin = wsuser;
                apprequested.secret = wssecret;
                apprequested.uniqueid = "";
                apprequested.fromdate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", itemTuNgay.EditValue);
                apprequested.todate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", itemDenNgay.EditValue);

                var inforet = robo.cdr_gw(apprequested).Select(p => new
                {
                    p.uniqueid,
                    p.userfield,
                    TypeName = p.userfield != "" ? (p.monitor_file != "" ? "Cuộc gọi đến" : "Cuộc gọi nhỡ") : "Cuộc gọi đi",
                    p.calldate,
                    answer = p.userfield == "" ? (p.disposition == "ANSWERED" ? p.answer : null) : (p.monitor_file != "" ? p.answer : null),
                    p.end,
                    p.duration,
                    src = getLine(p.src),
                    p.dst,
                    p.disposition,
                    p.monitor_file,
                    Status = p.userfield == "" ? (p.disposition == "ANSWERED" ? "Thành công" : "Không thành công") : (p.monitor_file != "" ? "Thành công" : "Không thành công")
                }).ToList();


                gcCallHistory.DataSource = inforet;

                gvCallHistory.IndicatorWidth = 15 * inforet.Count.ToString().Length;
            }
            catch { }
        }

        void click()
        {
            try
            {
                var status = gvCallHistory.GetFocusedRowCellValue("Status") as string;
                itemXem.Enabled = status == "Thành công";
                
                var type = gvCallHistory.GetFocusedRowCellValue("TypeName") as string;
                if (type == null) return;

                var phone = "";
                if (type == "Cuộc gọi đến" | type == "Cuộc gọi nhỡ")
                {
                    phone = gvCallHistory.GetFocusedRowCellValue("src").ToString();
                }
                else
                {
                    phone = gvCallHistory.GetFocusedRowCellValue("dst").ToString();
                }

                if (this.CallHistoryRowClick != null)
                {
                    var args = new CallHistoryRowClickEventArgs();
                    args.PhoneNumber = phone;
                    this.CallHistoryRowClick(this, args);
                    vgCustomer.DataSource = args.Result;
                }
            }
            catch
            {
                vgCustomer.DataSource = null;
            }
        }

        string getTime(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return null;

            var time = (endDate.Value - startDate.Value);

            return string.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
        }

        void SetDate(int index)
        {
            KyBaoCaoCls objKBC = new KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            try
            {
                //Properties.Settings.Default.DIP_SoftPhoneAPI_cdrgw_OneLotusGW = this.URLWS;

                //var ltStatus = new List<CallStatus>();
                //ltStatus.Add(new CallStatus() { ID = "BUSY", Name = "Máy bận" });
                //ltStatus.Add(new CallStatus() { ID = "FAILED", Name = "Không liên lạc được" });
                //ltStatus.Add(new CallStatus() { ID = "NO ANSWER", Name = "Không trả lời" });
                //ltStatus.Add(new CallStatus() { ID = "ANSWERED", Name = "Gọi thành công" });
                //lkStatus.DataSource = ltStatus;

                KyBaoCaoCls objKBC = new KyBaoCaoCls();
                objKBC.Initialize(cmbKyBC);
                SetDate(1);

                itemTuNgay.EditValue = DateTime.Now.AddDays(-1);
                itemDenNgay.EditValue = DateTime.Now;

                this.loadData();
            }
            catch { }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.loadData();
        }

        private void txtXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCallHistory.FocusedRowHandle >= 0)
            {
                using (var frm = new MediaEditor())
                {
                    frm.Url = "http://" + ip + "/monitor/" + gvCallHistory.GetFocusedRowCellValue("monitor_file").ToString();
                    frm.ShowDialog(this);
                }
            }
        }

        private void txtTaiVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCallHistory.GetFocusedRowCellValue("monitor_file") == null) return;

            using (WebBrowser web = new WebBrowser())
            {
                web.Url = new Uri("http://" + ip + "/monitor/" + gvCallHistory.GetFocusedRowCellValue("monitor_file").ToString());
                web.ShowSaveAsDialog();
            }
        }

        private void gvCallHistory_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (gvCallHistory.FocusedRowHandle >= 0)
                {
                    if (e.Column.Caption == "File")
                    {
                        var filePath = "http://" + ip + "/monitor/" + gvCallHistory.GetFocusedRowCellValue("monitor_file").ToString();
                        System.Diagnostics.Process.Start(filePath);
                    }
                }
            }
            catch { }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new System.Windows.Forms.SaveFileDialog())
            {
                frm.Filter = "Excel|*.xls";
                frm.ShowDialog();
                if (frm.FileName != "")
                {
                    gcCallHistory.ExportToXls(frm.FileName);
                    if (MessageBox.Show("Đã xử lý xong, bạn có muốn xem lại không?", "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        System.Diagnostics.Process.Start(frm.FileName);
                }
            }
        }

        private void gvCallHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.click();
        }

        private void gvCallHistory_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gvCallHistory_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column.FieldName == "TypeName")
            {
                var typeName = e.CellValue as string;
                switch (typeName)
                {
                    case "Cuộc gọi đến": e.Appearance.BackColor = Color.Blue; break;
                    case "Cuộc gọi nhỡ": e.Appearance.BackColor = Color.Red; break;
                    case "Cuộc gọi đi": e.Appearance.BackColor = Color.Green; break;
                }
            }
            else if (e.Column.FieldName == "Status")
            {
                var status = e.CellValue as string;
                switch (status)
                {
                    case "Thành công": e.Appearance.BackColor = Color.Yellow; break;
                    default: e.Appearance.BackColor = Color.Red; break;
                }
            }
        }
    }
}
