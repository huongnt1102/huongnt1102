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
//using ReportMisc.DichVu;

namespace DichVu
{
    public partial class frmEditTTNNDV : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        //public EnumLoaiDichVu LoaiDichVu;
        public dvdnDien objdien;
        public dvdnNuoc objnuoc;
        public dvtmThanhToanThangMay objtm;
        public dvkDichVuThanhToan objdvk;
        public hthtCongNo objht;
        public thueCongNo objthue;
        
        public frmEditTTNNDV()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmEditTTNNDV_Load(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                looktrangthai.Properties.DataSource = db.dvTrangThaiNhacNos;
                looktrangthai.Properties.DisplayMember = "TenTT";
                looktrangthai.Properties.ValueMember = "MaTT";

                //switch (LoaiDichVu)
                //{
                //    case EnumLoaiDichVu.DichVuDien:
                //        looktrangthai.EditValue = objdien.MaTTNhacNo;
                //        break;
                //    case EnumLoaiDichVu.DichVuNuoc:
                //        looktrangthai.EditValue = objnuoc.MaTTNhacNo;
                //        break;
                //    case EnumLoaiDichVu.DichVuKhac:
                //        looktrangthai.EditValue = objdvk.MaTTNhacNo;
                //        break;
                //    case EnumLoaiDichVu.DichVuThueNgoai:
                //        break;
                //    case EnumLoaiDichVu.HopDongThue:
                //        looktrangthai.EditValue = objthue.MaTTNhacNo;
                //        break;
                //    case EnumLoaiDichVu.DichVuThangMay:
                //        looktrangthai.EditValue = objtm.MaTTNhacNo;
                //        break;
                //    case EnumLoaiDichVu.DichVuHoptac:
                //        looktrangthai.EditValue = objht.MaTTNhacNo;
                //        break;
                //    default:
                //        break;
                //}
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                //switch (LoaiDichVu)
                //{
                //    case EnumLoaiDichVu.DichVuDien:
                //        var dien = db.dvdnDiens.Single(p => p.ID == objdien.ID);
                //        dien.MaTTNhacNo = (int)looktrangthai.EditValue;
                //        break;
                //    case EnumLoaiDichVu.DichVuNuoc:
                //        var nuoc = db.dvdnNuocs.Single(p => p.ID == objnuoc.ID);
                //        nuoc.MaTTNhacNo = (int)looktrangthai.EditValue;
                //        break;
                //    case EnumLoaiDichVu.DichVuKhac:
                //        var dvk = db.dvkDichVuThanhToans.Single(p => p.ThanhToanID == objdvk.ThanhToanID);
                //        dvk.MaTTNhacNo = (int)looktrangthai.EditValue;
                //        break;
                //    case EnumLoaiDichVu.DichVuThueNgoai:
                //        break;
                //    case EnumLoaiDichVu.HopDongThue:
                //        var thue = db.thueCongNos.Single(p => p.MaCN == objthue.MaCN);
                //        thue.MaTTNhacNo = (int)looktrangthai.EditValue;
                //        break;
                //    case EnumLoaiDichVu.DichVuThangMay:
                //        var tm = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == objtm.ThanhToanID);
                //        tm.MaTTNhacNo = (int)looktrangthai.EditValue;
                //        break;
                //    case EnumLoaiDichVu.DichVuHoptac:
                //        var ht = db.hthtCongNos.Single(p => p.MaCongNo == objht.MaCongNo);
                //        ht.MaTTNhacNo = (int)looktrangthai.EditValue;
                //        break;
                //    default:
                //        break;
                //}

                try
                {
                    db.SubmitChanges();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }
    }
}