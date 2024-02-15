using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace TaiSan.KhoHang
{
    public partial class frmChuyenTSSuDung : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public Kho objkho;
        MasterDataContext db;

        public frmChuyenTSSuDung()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }
        string KyHieu;

        private string getNewKH()
        {
            db.tsDangDung_getNewMaTSDD(ref KyHieu);
            return db.DinhDang(16, int.Parse(KyHieu));
        }
        private void frmChuyenTSSuDung_Load(object sender, EventArgs e)
        {
            slookMatBang.Properties.DataSource = db.mbMatBangs.
                Select(p => new { p.MaMB,p.MaSoMB, p.mbLoaiMatBang.TenLMB, p.NgayBanGiao, p.mbTangLau.TenTL ,
                    p.mbTrangThai.TenTT,  p.mbTangLau.mbKhoiNha.TenKN});
            lookHSX.Properties.DataSource = db.tsHangSanXuats;
            lookNCC.Properties.DataSource = db.tnNhaCungCaps;
            lookXX.Properties.DataSource = db.tsXuatXus;
            lookTrangThai.Properties.DataSource = db.tsTrangThais;
            dateNgaySX.DateTime = DateTime.Now;
            dateNgayHH.DateTime = DateTime.Now;
            spinGiaTri.Value = objkho.DonGia ?? 0;
            txtMaSuDung.Text = getNewKH();
            txtLoaiTaiSan.Text = objkho.tsLoaiTaiSan.TenLTS;
            lookTrangThai.EditValue = objkho.MaTT;
            lookNCC.EditValue = objkho.MaNCC;
            spinThoiHan.EditValue = objkho.HanSuDung;
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateNgayHH.DateTime <= dateNgaySX.DateTime)
            {
                DialogBox.Alert("Ngày hết hạn không thể nhỏ hơn ngày sản xuất");
                return;
            }

            if (lookTrangThai.EditValue == null | lookNCC.EditValue == null | lookHSX.EditValue == null | lookXX.EditValue == null)
            {
                DialogBox.Alert("Các trường dữ liệu không được để trống");
                return;
            }

            if (spinSL.Value <= 0)
            {
                DialogBox.Alert("Vui lòng chọn số lượng tài sản");
                return;
            }
            int retry = 0;

            var wait = DialogBox.WaitingForm();
            luu:
            try
            {
                if (retry >= 5)
                {
                    DialogBox.Error("Không chuyển tài sản được, vui lòng thử lại sau");
                    return;
                }

                objkho = db.Khos.Single(p => p.ID == objkho.ID);
                if (objkho.SoLuong < spinSL.Value)
                {
                    DialogBox.Alert("Số lượng trong kho không đủ");
                    return;
                }
                else
                {
                    objkho.SoLuong = objkho.SoLuong - (int)spinSL.Value;
                    for (int i = 0; i < spinSL.Value; i++)
                    {
                        tsTaiSanSuDung objtssd = new tsTaiSanSuDung()
                        {
                            KyHieu = getNewKH(),
                            MaHSX = (int)lookHSX.EditValue,
                            MaTN = objnhanvien.MaTN,
                            MaTT = (int)lookTrangThai.EditValue,
                            MaXX = (int)lookXX.EditValue,
                            NgayHH = dateNgayHH.DateTime,
                            NgaySX = dateNgaySX.DateTime,
                            ThoiHan = (int)spinThoiHan.Value,
                            MaLTS = objkho.tsLoaiTaiSan.MaLTS,
                            MaNCC = (int)lookNCC.EditValue,
                            GiaTriTaiSan = spinGiaTri.Value,
                            MaMB =(int?)slookMatBang.EditValue,
                            NgayBDSD =(DateTime?)dateNgayBDSD.EditValue
                        };
                        db.tsTaiSanSuDungs.InsertOnSubmit(objtssd);
                        db.SubmitChanges();
                        wait.SetCaption("Đang chuyển hàng... ");
                    }
                }

                db.SubmitChanges();
                DialogBox.Alert("Đã chuyển xong");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch
            {
                txtMaSuDung.Text = getNewKH();
                retry++;
                goto luu;
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void spinSL_EditValueChanged(object sender, EventArgs e)
        {
            spinGiaTri.Value = (objkho.DonGia ?? 0) * spinSL.Value;
        }

        private void slookMatBang_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}