using System;
using System.Windows.Forms;
using Library;
using System.Linq;


namespace DichVu.YeuCau
{
    public partial class frmGiaoViec : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public int MaYC;
        public int MaTN;
        public int TrangThai;
        public bool isHide { get; set; }
        public frmGiaoViec()
        {
            InitializeComponent();
        }

        private void Luu()
        {
             if (this.lkNhomCV.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn nhóm công việc.");
            }
            else
            {
                this.LuuLichSu();
                MasterDataContext masterDataContext = new MasterDataContext();
                tnycYeuCau tnycYeuCau = masterDataContext.tnycYeuCaus.Single((tnycYeuCau p) => p.ID == this.MaYC);
                if (tnycYeuCau != null)
                {
                    if(glkNhanVien.EditValue != null)
                    {
                        tnycYeuCau.MaNTN = new int?(int.Parse(this.glkNhanVien.EditValue.ToString()));
                    }
                    
                    tnycYeuCau.MaTT = new int?(2);
                    tnycYeuCau.MaNV = new int?(Common.User.MaNV);
                    tnycYeuCau.MaBP = (int?)glkPhongBan.EditValue;
                    tnycYeuCau.GroupProcessId = (int?)this.lkNhomCV.EditValue;
                    try
                    {
                        masterDataContext.SubmitChanges();

                        if((int?)lkLoaiThongBao.EditValue == 2)
                        {
                            var objConfig = db.web_Zalos.FirstOrDefault(o => o.MaTN == MaTN);
                            if (objConfig == null)
                            {
                                DialogBox.Error("Dự án này chưa được cấu hình sms zalo!");
                                return;
                            }
                            var send = Building.SMS.Class.DichVuYeuCau.SendSmsStafZalo(tnycYeuCau.TieuDe, tnycYeuCau.NoiDung, (byte)MaTN, 0 , objConfig.LinkToken, new int ? (int.Parse(this.glkNhanVien.EditValue.ToString())), memoNoiDung.Text);
                        }

                        DialogBox.Success("Đã lưu");
                        base.DialogResult = DialogResult.OK;
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void LuuLichSu()
        {
            MasterDataContext masterDataContext = new MasterDataContext();
            tnycLichSuCapNhat tnycLichSuCapNhat = new tnycLichSuCapNhat();
            tnycLichSuCapNhat.MaYC = new int?(this.MaYC);
            tnycLichSuCapNhat.MaNV = new int?(Common.User.MaNV);
            tnycLichSuCapNhat.NgayCN = this.db.GetLocalDate();
            tnycLichSuCapNhat.MaTT = new int?(2);
            tnycLichSuCapNhat.NoiDung = this.memoNoiDung.Text;
            tnycLichSuCapNhat.GroupProcessId = (int?)this.lkNhomCV.EditValue;
            try
            {
                masterDataContext.tnycLichSuCapNhats.InsertOnSubmit(tnycLichSuCapNhat);
                masterDataContext.SubmitChanges();
            }
            catch
            {
            }
        }
        private void frmGiaoViec_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, this.barManager1, null);
            this.glkPhongBan.Properties.DataSource = db.tnPhongBans.Where(p => p.MaTN == Convert.ToByte(this.MaTN)).ToList();
            
            this.lkNhomCV.Properties.DataSource = this.db.app_GroupProcesses;
            if (this.TrangThai == 1)
            {
                this.memoNoiDung.Text = "Giao việc";
            }
            if (this.TrangThai == 2)
            {
                this.memoNoiDung.Text = "Đổi nhân viên";
            }

            ShowHide();
        }

        public void ShowHide()
        {
            if(isHide == true)
            {
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void bbiHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Luu();
        }

        private void glkNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            if (TrangThai == 1)
            {
                memoNoiDung.Text = "Giao việc cho nhân viên " + glkNhanVien.Text;
            }
            if (TrangThai == 2)
            {
                memoNoiDung.Text = "Đổi nhân viên thực hiện:  " + glkNhanVien.Text;
            }
            lkLoaiThongBao.Properties.DataSource = db.tnycLoaiSms;

        }

        private void glkPhongBan_EditValueChanged(object sender, EventArgs e)
        {

            var obj = glkPhongBan.Properties.GetRowByKeyValue(glkPhongBan.EditValue);
             Type type = obj.GetType();
             var fieldValue = type.GetProperty("MaPB").GetValue(obj, null);
            this.glkNhanVien.Properties.DataSource = (from p in this.db.tnToaNhaNguoiDungs
                                                      where (int?)p.MaTN == (int?)this.MaTN && p.tnNhanVien.MaPB == (int?)fieldValue
                                                      select new
                                                      {
                                                          MaNV = p.MaNV,
                                                          HoTenNV = p.tnNhanVien.HoTenNV,
                                                          MaSoNV = p.tnNhanVien.MaSoNV,
                                                          DienThoai = p.tnNhanVien.DienThoai,
                                                          Email = p.tnNhanVien.Email
                                                      }).ToList();
        }

       
    }
}
