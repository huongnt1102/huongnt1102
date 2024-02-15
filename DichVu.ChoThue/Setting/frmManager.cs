using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace DichVu.ChoThue.Setting
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        bool first = true;
        public frmManager()
        {
            InitializeComponent();

            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.DataSource = list;
                if (list.Count > 0)
                    itemToaNha.EditValue = list[0].MaTN;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objnhanvien.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.DataSource = list2;
                if (list2.Count > 0)
                    itemToaNha.EditValue = list2[0].MaTN;
            }

            LoadData();

            first = false;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                byte maTN = (byte?)itemToaNha.EditValue ?? 0;
                gcDieuHoa.DataSource = (from dh in db.thueHopDongCaiDats
                                        join nv in db.tnNhanViens on dh.MaNV equals nv.MaNV
                                        join nvcn in db.tnNhanViens on dh.MaNVCN equals nvcn.MaNV into up
                                        from up2 in up.DefaultIfEmpty()
                                        where dh.MaTN == maTN
                                        select new
                                        {
                                            dh.ID,
                                            dh.NgayCN,
                                            dh.SoNgay,
                                            dh.DienGiai,
                                            NguoiTao = nv.HoTenNV,
                                            NguoiCN = up2.HoTenNV,
                                            dh.NgayTao
                                        }).AsEnumerable().Select((p, index) => new
                                        {
                                            STT = index + 1,
                                            p.ID,
                                            p.NgayCN,
                                            p.SoNgay,
                                            p.DienGiai,
                                            p.NguoiTao,
                                            p.NguoiCN,
                                            p.NgayTao
                                        }).ToList();
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void grvDinhMuc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            grvDieuHoa.UpdateCurrentRow();

            db.SubmitChanges();
           
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void barEditItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            var f = new frmEdit();
            f.MaTN = (byte)maTN;
            f.ToaNha = lookUpToaNha.GetDisplayText(f.MaTN);
            f.objNV = objnhanvien;
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            if (grvDieuHoa.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Định mức], xin cảm ơn.");
                return;
            }
            var f = new frmEdit() { objNV = objnhanvien };
            f.MaTN = (byte)maTN;
            f.ToaNha = lookUpToaNha.GetDisplayText(f.MaTN);
            f.ID = (int)grvDieuHoa.GetFocusedRowCellValue("ID");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDieuHoa.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Định mức], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == System.Windows.Forms.DialogResult.No) return;

            try
            {
                var obj = db.thueHopDongCaiDats.Single(p => p.ID == (int)grvDieuHoa.GetFocusedRowCellValue("ID"));
                db.thueHopDongCaiDats.DeleteOnSubmit(obj);
                db.SubmitChanges();

                grvDieuHoa.DeleteRow(grvDieuHoa.FocusedRowHandle);
            }
            catch { }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}