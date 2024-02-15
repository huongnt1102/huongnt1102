using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Library;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace TaiSan.DanhGiaLai
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNhanVien { get; set; }
        public int?  MaDGL{ get; set; }
        MasterDataContext db;
        string SoPhieu = "";
        tsDanhGiaLai objDGL;
        public int[] ListTS { get; set; }
       // ntchNghiemThuCanHo objNT;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            lookLoaiCT.EditValueChanged += new EventHandler(lookLoaiCT_EditValueChanged);
            btnSoChungTu.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnSoChungTu_ButtonClick);
        }

        void SeachChungTu()
        {
            frmSelectChungTu frm = new frmSelectChungTu();
            frm.MaLCT = Convert.ToInt32(gvChungTu.GetFocusedRowCellValue("LoaiCT"));
            frm.objnhanvien = objNhanVien;
            frm.ShowDialog();
            gvChungTu.SetFocusedRowCellValue("SoCT", frm.SoCT);
            gvChungTu.SetFocusedRowCellValue("MaCT", frm.MaCT);
        }

        void btnSoChungTu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvChungTu.GetFocusedRowCellValue("LoaiCT") == null)
                return;
            SeachChungTu();
        }

        void lookLoaiCT_EditValueChanged(object sender, EventArgs e)
        {
            var LookLCT = (LookUpEdit)sender;
            int MaLCT = (int)LookLCT.EditValue;
            gvChungTu.SetFocusedRowCellValue("LoaiCT", MaLCT);
            if (gvChungTu.GetFocusedRowCellValue("MaCT") != null)
            {
                gvChungTu.SetFocusedRowCellValue("SoCT", null);
                gvChungTu.SetFocusedRowCellValue("MaCT", null);
            }
        }

        void AddNew()
        {
            db.DanhGiaLai_TaoSoPhieu(ref SoPhieu);
            txtSoDG.Text = SoPhieu;
            txtSoQD.Text = "";
            dateNgayDGL.EditValue = DateTime.Now;
            dateNgayKyQD.EditValue = DateTime.Now;
            lookLyDo.EditValue = null;
            Enable(true);
        }

        void Enable(bool bl)
        {
            xtraTabControl2.Enabled = bl;
            groupBox1.Enabled = bl;
            itemDelete.Enabled = bl;
            itemNew.Enabled = !bl;
            itemSave.Enabled = bl;
            itemEdit.Enabled = bl;
            itemStandBy.Enabled = bl;
        }

        void LoadData()
        {
            lookLyDo.Properties.DataSource = db.tsDanhGiaLaiLyDos;
            var wait = DialogBox.WaitingForm();
            try
            {
                lookTaiSan.DataSource = db.tsTaiSans.Select(p => new { p.MaTS, p.TenTS, p.ID });
                lookTaiKhoanCo.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
                lookTaiKhoanNo.DataSource = db.TaiKhoans.Select(p => new { p.MaTK, p.TenTK });
                lookLoaiCT.DataSource = db.tsLoaiChungTus;
                lookNhanVien.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, p.HoTenNV });
                ///Load 1
                if (MaDGL == null)
                {
                    AddNew();
                    objDGL = new tsDanhGiaLai();
                    db.tsDanhGiaLais.InsertOnSubmit(objDGL);
                }
                else
                {
                    objDGL = db.tsDanhGiaLais.SingleOrDefault(p => p.ID == MaDGL);
                    lookLyDo.EditValue = objDGL.MaLyDo;
                    txtSoDG.Text = objDGL.SoDGL;
                    dateNgayKyQD.EditValue = objDGL.NgayKyQD;
                    dateNgayDGL.EditValue = objDGL.NgayDGL;
                    txtSoQD.Text = objDGL.SoQuyetDinh;
                }
                if (ListTS != null)
                {
                    tsTaiSan obj;
                    decimal TGSuDung;
                    for (int i = 0; i < ListTS.Length; i++)
                    {
                        tsDanhGiaLaiChiTiet item = new tsDanhGiaLaiChiTiet();
                        obj = db.tsTaiSans.Single(p => p.ID == ListTS[i]);
                        TGSuDung = DateTime.Now.Subtract(obj.NgayBDSD.Value).Days / 30;
                        item.GiaTriKHConLai = obj.GiaTriTinhKH - obj.TyLeKHThang.Value * TGSuDung;
                        if ((bool)obj.DVTTGSD)
                            item.ThoiGianSD = obj.ThoiGianSD * 12 - TGSuDung;
                        else
                            item.ThoiGianSD = obj.ThoiGianSD -TGSuDung;
                        item.MaTS = ListTS[i];
                        objDGL.tsDanhGiaLaiChiTiets.Add(item);
                    }

                }
                gcDanhGiaCT.DataSource = objDGL.tsDanhGiaLaiChiTiets;
                gcThanhVien.DataSource = objDGL.tsDanhGiaLaiThanhViens;
                gcChungTu.DataSource = objDGL.tsdglChungTuThamChieus;
              

            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        void SaveData()
        {
            if (dateNgayDGL.EditValue == null)
            {
                DialogBox.Warning("Vui lòng nhập ngày đânhs giá lại.Xin cảm ơn!");
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaDGL == null)
                {
                    objDGL.MaNV = objNhanVien.MaNV;
                    objDGL.NgayTao = DateTime.Now;
                }
                else
                {
                    objDGL = db.tsDanhGiaLais.SingleOrDefault(p => p.ID == MaDGL);
                    objDGL.MaNVCN = objNhanVien.MaNV;
                    objDGL.NgayCN = DateTime.Now;
                }
                objDGL.SoDGL = txtSoDG.Text.Trim();
                objDGL.NgayDGL = (DateTime?)dateNgayDGL.EditValue;
                objDGL.NgayKyQD = (DateTime?)dateNgayKyQD.EditValue;
                objDGL.MaLyDo = (short?)lookLyDo.EditValue;
                objDGL.SoQuyetDinh = txtSoQD.Text.Trim();
                gvDanhGiaCT.RefreshData();
                gvDanhGiaCT.FocusedColumn = colTaiSan;
                gvDanhGiaCT.FocusedRowHandle = 0;
                List<tsTaiSan> ListTS = new List<tsTaiSan>();
                for (int i = 0; i < gvDanhGiaCT.RowCount -1 ; i++)
                {
                    var obj = db.tsTaiSans.SingleOrDefault(p=>p.ID==(int)gvDanhGiaCT.GetRowCellValue(i,"MaTS"));
                    ListTS.Add(obj);
                }
                for (int i = 0; i < ListTS.Count; i++)
                {
                    ListTS[i].NgayBDSD = (DateTime)dateNgayDGL.EditValue;
                    ListTS[i].GiaTriTinhKH = (decimal?)gvDanhGiaCT.GetRowCellValue(i, "GiaTriKHSauDC");
                    if ((bool)ListTS[i].DVTTGSD)
                        ListTS[i].ThoiGianSD = (decimal?)gvDanhGiaCT.GetRowCellValue(i, "ThoiGianSDSauDC") / 12;
                    else
                        ListTS[i].ThoiGianSD = (decimal?)gvDanhGiaCT.GetRowCellValue(i, "ThoiGianSDSauDC");
                }
                db.SubmitChanges();

            }
            catch { }
            finally
            {
                wait.Close();
            }
            Enable(false);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNew();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();    
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemStandBy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Enable(false);
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Enable(true);
        }

        private void lookTaiSan_EditValueChanged(object sender, EventArgs e)
        {
            var lookTS = (LookUpEdit)sender;
            if (lookTS.EditValue == null)
                return;
           // int TGSuDung;
            var objTS = db.tsTaiSans.Single(p => p.ID == (int)lookTS.EditValue);
            //if(objTS.NgayBDSD!=null)
             var TGSuDung = DateTime.Now.Subtract(objTS.NgayBDSD.Value).Days / 30;
            gvDanhGiaCT.SetFocusedRowCellValue("GiaTriKHConLai", objTS.GiaTriTinhKH - objTS.TyLeKHThang.Value * TGSuDung);
            gvDanhGiaCT.SetFocusedRowCellValue("GiaTriKHSauDC", objTS.GiaTriTinhKH - objTS.TyLeKHThang.Value * TGSuDung);
            if ((bool)objTS.DVTTGSD)
            {
                gvDanhGiaCT.SetFocusedRowCellValue("ThoiGianSD", objTS.ThoiGianSD * 12- TGSuDung);
                gvDanhGiaCT.SetFocusedRowCellValue("ThoiGianSDSauDC", (objTS.NgayBDSD.Value.AddMonths(Convert.ToInt32((objTS.ThoiGianSD * 12))) - (DateTime)dateNgayDGL.EditValue).Days / 30);

            }
            else
            {
                gvDanhGiaCT.SetFocusedRowCellValue("ThoiGianSD", objTS.ThoiGianSD - TGSuDung);
                gvDanhGiaCT.SetFocusedRowCellValue("ThoiGianSDSauDC", (objTS.NgayBDSD.Value.AddMonths(Convert.ToInt32((objTS.ThoiGianSD))) - (DateTime)dateNgayDGL.EditValue).Days / 30);
            }
           
            
        }

        private void spinGTKHSauDC_EditValueChanged(object sender, EventArgs e)
        {
            var sl = ((SpinEdit)sender).Value;
            gvDanhGiaCT.SetFocusedRowCellValue("GiaTriKHSauDC", sl);
            gvDanhGiaCT.SetFocusedRowCellValue("ChenhLech", (decimal?)gvDanhGiaCT.GetFocusedRowCellValue("GiaTriKHConLai") - (decimal?)gvDanhGiaCT.GetFocusedRowCellValue("GiaTriKHSauDC"));
        }

        private void gvChungTu_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
          //  int r = e.RowHandle;
          //  GridView gv = sender as GridView;
          //  if (gvChungTu.GetFocusedRowCellValue("SoCT")==null)
          //  {
          //      DialogBox.Alert("Vui lòng nhập số chứng từ ở dòng này.Xin cảm ơn!"); 
          //  }
        }

    }

    public class ItemDGLCT
    {
        public int? MaTS { get; set; }
        public decimal? GiaTriKHConLai { get; set; }
        public decimal? GiaTriKHSauDC { get; set; }
        public decimal? ChenhLech { get; set; }
        public decimal? ThoiGianSD { get; set; }
        public decimal? ThoiGianSDSauDC { get; set; }
        public decimal? GiaTriKHThangSauDC { get; set; }
        public string TKNo { get; set; }
        public string TKCo { get; set; }
    }
}
