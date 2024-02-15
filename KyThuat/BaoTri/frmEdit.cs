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
using DevExpress.XtraTreeList.Nodes;

namespace KyThuat.BaoTri
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public btBaoTri objBT;
        public tnNhanVien objnhanvien;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        string getNewMaBT()
        {
            string MaBT = "";
            db.btBaoTri_getNewMaBT(ref MaBT);
            return db.DinhDang(17, int.Parse(MaBT));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (objnhanvien.IsSuperAdmin.Value)
            {
                treeTaiSan.DataSource = db.tsTaiSanSuDungs
                    .Select(p => new { p.MaTS, p.MaTSCHA, p.KyHieu, p.tsLoaiTaiSan.TenLTS, p.MaTT });
                lookNhanVien.Properties.DataSource = db.tnNhanViens;
                lookDoiTac.Properties.DataSource = db.tnNhaCungCaps
                    .Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
            }
            else
            {
                treeTaiSan.DataSource = db.tsTaiSanSuDungs
                    .Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new { p.MaTS, p.MaTSCHA, p.KyHieu, p.tsLoaiTaiSan.TenLTS, p.MaTT });
                lookNhanVien.Properties.DataSource = db.tnNhanViens
                    .Where(p => p.MaTN == objnhanvien.MaTN);
                lookDoiTac.Properties.DataSource = db.tnNhaCungCaps
                    //.Where(p => p.MaTN == objnhanvien.MaTN)
                    .Select(p => new { p.MaNCC, p.TenVT, p.TenNCC });
            }
            treeTaiSan.ExpandAll();
            
            lookTrangThai.Properties.DataSource = db.tsTrangThais;
            lookHinhThuc.Properties.DataSource = db.btHinhThucs;
            lookNhanSu.DataSource = lookNhanVien.Properties.DataSource;
            
            colMaTB.ColumnEdit = new RepositoryItemPopupContainerEditLoaiTaiSan(objnhanvien);
            if (this.objBT != null)
            {
                objBT = db.btBaoTris.Single(p => p.MaBT == objBT.MaBT);
                txtMaSoBT.Text = objBT.MaSoBT;
                dateNgayBT.DateTime = (DateTime)objBT.NgayBT;
                lookHinhThuc.EditValue = objBT.MaHT;
                popupTaiSan.EditValue = objBT.MaTS;
                lookTrangThai.EditValue = objBT.MaTT;
                lookDoiTac.EditValue = objBT.MaDT;
                spinPhiBT.EditValue = objBT.PhiBT;
                ckbDaTT.Checked = (bool)objBT.DaTT;
                lookNhanVien.EditValue = objBT.MaNV;
                txtDienGiai.Text = objBT.DienGiai;
                objBT.btHinhThuc.MaHT = (byte)objBT.MaHT;
            }
            else
            {
                objBT = new btBaoTri();
                db.btBaoTris.InsertOnSubmit(objBT);
                objBT.MaTT = 1;

                txtMaSoBT.Text = getNewMaBT();
                dateNgayBT.DateTime = DateTime.Now;
                lookNhanVien.EditValue = UserInfo.MaNV;
                lookHinhThuc.ItemIndex = 0;
            }
            gcThietBi.DataSource = objBT.btThietBis;
            gcNhanSu.DataSource = objBT.btNhanViens;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (popupTaiSan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn tài sản");
                return;
            }

            objBT.MaSoBT = txtMaSoBT.Text;
            objBT.NgayBT = dateNgayBT.DateTime;
            objBT.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)lookNhanVien.EditValue);
            objBT.MaNV = lookNhanVien.EditValue == null ? 0 : (int)lookNhanVien.EditValue;
            objBT.MaTS = popupTaiSan.EditValue == null ? 0 : (int)popupTaiSan.EditValue;
            objBT.MaTT = lookTrangThai.EditValue == null ? 0 : (int)lookTrangThai.EditValue;
            objBT.MaHT = lookHinhThuc.EditValue == null ? (byte)0 : (byte)lookHinhThuc.EditValue;
            objBT.MaDT = lookDoiTac.EditValue == null ? 0 : (int)lookDoiTac.EditValue;
            objBT.PhiBT = spinPhiBT.Value;
            objBT.DaTT = ckbDaTT.Checked;
            objBT.DienGiai = txtDienGiai.Text.Trim();
            objBT.MaTN = objnhanvien.MaTN;

            #region Tai san chi tiet
            var lts = db.tsTaiSanSuDungs.Single(p=>p.MaTS == (int)popupTaiSan.EditValue).MaLTS;
            var counttsct = db.tsTaiSanChiTiets.Where(p => p.MaTS == (int)popupTaiSan.EditValue).Count();
            var chitiet = db.ChiTietTaiSans.Where(p => p.MaTS == lts);
            if (counttsct == 0 & chitiet.Count() > 0)
            {
                List<tsTaiSanChiTiet> lsttsct = new List<tsTaiSanChiTiet>();
                foreach (var item in chitiet)
                {
                    tsTaiSanChiTiet objtsct = new tsTaiSanChiTiet()
                    {
                        MaTS = (int)popupTaiSan.EditValue,
                        NgayNhap = DateTime.Now,
                        MaChiTiet = item.MaChiTiet
                    };
                    lsttsct.Add(objtsct);
                }
                db.tsTaiSanChiTiets.InsertAllOnSubmit(lsttsct);
            }
            #endregion

            #region Lưu lịch sử bảo trì
            if (ckbDaTT.Checked)
            {
                tsLichSuBaoTriSuaChua objls = new tsLichSuBaoTriSuaChua()
                {
                    DienGiai = txtDienGiai.Text.Trim(),
                    MaNV = objnhanvien.MaNV,
                    MaTSSD = (int)popupTaiSan.EditValue,
                    MaTT = (int)lookTrangThai.EditValue,
                    NgayBaoTriSuaChua = db.GetSystemDate()
                };

                db.tsLichSuBaoTriSuaChuas.InsertOnSubmit(objls);
            }
            #endregion
            int retry = 0; 

        Save:
            try
            {
                if (retry >=10)
                {
                    DialogBox.Alert("Lưu không thành công. Vui lòng thử lại.");
                    return;
                }
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công");
            }
            catch
            {
                retry = retry + 1;
                objBT.MaSoBT = getNewMaBT();
                goto Save;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void grvThietBi_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void grvNhanSu_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
        }

        private void popupTaiSan_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            e.Value = treeTaiSan.FocusedNode.GetValue("MaTS");
            lookTrangThai.EditValue = treeTaiSan.FocusedNode.GetValue("MaTT");
        }

        private void popupTaiSan_QueryDisplayText(object sender, DevExpress.XtraEditors.Controls.QueryDisplayTextEventArgs e)
        {
            if (e.EditValue != null)
            {
                e.DisplayText = treeTaiSan.FindNodeByKeyID(e.EditValue).GetValue("KyHieu").ToString();
            }
        }

        private void popupTaiSan_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (popupTaiSan.EditValue != null)
            {
                treeTaiSan.FocusedNode = treeTaiSan.FindNodeByKeyID(popupTaiSan.EditValue);
            }
        }

        private void treeTaiSan_DoubleClick(object sender, EventArgs e)
        {
            popupTaiSan.ClosePopup();                
        }

        private void grvThietBi_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaLTS")
            {
                btThietBi objTB = (btThietBi)grvThietBi.GetRow(e.RowHandle);
                objTB.tsLoaiTaiSan = db.tsLoaiTaiSans.Single(p => p.MaLTS == (int)e.Value);
            }
        }

        private void grvNhanSu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.FieldName == "MaNV")
            {
                btNhanVien objNV = (btNhanVien)grvNhanSu.GetRow(e.RowHandle);
                objNV.tnNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)e.Value);
            }
        }

        private void grvThietBi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvThietBi.DeleteSelectedRows();
        }

        private void grvNhanSu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                grvNhanSu.DeleteSelectedRows();
        }

        private void popupTaiSan_QueryDisplayText_1(object sender, DevExpress.XtraEditors.Controls.QueryDisplayTextEventArgs e)
        {
            TreeListNode node = treeTaiSan.FindNodeByFieldValue("MaTS", e.EditValue);
            if (node != null)
            {
                //e.DisplayText = node.GetDisplayText(treeTaiSan.Columns[0]);
                e.DisplayText = this.treeTaiSan.FindNodeByFieldValue("MaTS", e.EditValue).GetValue("TenLTS").ToString();
            }
        }

        private void popupTaiSan_QueryPopUp_1(object sender, CancelEventArgs e)
        {
            PopupContainerEdit p = (PopupContainerEdit)sender;
            this.treeTaiSan.FocusedNode = this.treeTaiSan.FindNodeByFieldValue("MaTS", p.EditValue);
        }

        private void popupTaiSan_QueryResultValue_1(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            e.Value = this.treeTaiSan.FocusedNode.GetValue("MaTS");
        }

    }
}