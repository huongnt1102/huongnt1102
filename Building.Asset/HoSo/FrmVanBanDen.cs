using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;
using ftp=FTP;

namespace Building.Asset.HoSo
{
    public partial class FrmVanBanDen : XtraForm
    {
        public FrmVanBanDen()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            beiTuNgay.EditValue = objKbc.DateFrom;
            beiDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    var db = new MasterDataContext();
                    //gc.DataSource = db.tbl_HoSos.Where(_ => _.MaTN == (byte) beiToaNha.EditValue & _.PhanLoaiId == 1 &
                    //                                        SqlMethods.DateDiffDay((DateTime) beiTuNgay.EditValue,
                    //                                            _.NgayNhap) >= 0 &
                    //                                        SqlMethods.DateDiffDay(_.NgayNhap,
                    //                                            (DateTime) beiDenNgay.EditValue) >= 0);
                    gc.DataSource = (from _ in db.tbl_HoSos join cl in db.tbl_HoSo_PhanLoaiDiDens on _.PhanLoaiId equals cl.Id into phanLoai from cl in phanLoai.DefaultIfEmpty() where _.MaTN == (byte)beiToaNha.EditValue & _.PhanLoaiId == 1 & SqlMethods.DateDiffDay((DateTime)beiTuNgay.EditValue, _.NgayNhap) >= 0 & SqlMethods.DateDiffDay(_.NgayNhap, (DateTime)beiDenNgay.EditValue) >= 0 select new {_.ID, _.PhanLoaiName,_.KyHieu, _.NgayBanHanh, _.NoiBanHanhName, _.TenLoaiVanBan, _.NgayDen, _.SoTrang, _.NguoiNhan, _.NguoiKy, _.NguoiDuyet, _.NoiDung, _.DienGiai, cl.Color }).ToList();

                }
            }
            catch
            {
                // ignored
            }

            LoadDetail();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();

            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }

            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            var db = new MasterDataContext();
            //lkNhanVien.DataSource = db.tnNhanViens.Select(o => new {o.MaNV, o.HoTenNV}).ToList();
            repNhanVien.DataSource = db.tnNhanViens.Select(_ => new {_.MaNV, _.HoTenNV}).ToList();
            //repNhomHoSo.DataSource = db.tbl_HoSo_NhomHoSos;
            //repDayKe.DataSource = db.tbl_HoSo_DayKes;
            //repDoKhanCap.DataSource = db.tbl_HoSo_DoKhanCaps;
            //repKhoGiay.DataSource = db.tbl_HoSo_KhoGiays;
            //repLoaiVanBan.DataSource = db.tbl_HoSo_LoaiVanBans;
            //repMatBang.DataSource = db.mbMatBangs.Select(_ => new {_.MaMB, _.MaSoMB}).ToList();
            //repMucDoBaoMat.DataSource = db.tbl_HoSo_MucDoBaoMats;
            LoadData();
        }

        private void LoadDetail()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var id = (int?)gv.GetFocusedRowCellValue("ID");
                    if (id == null)
                    {
                        return;
                    }
                    switch (xtraTabDetail.SelectedTabPage.Name)
                    {
                        case "tabFileDinhKem":
                            ctlTaiLieu1.FormID = 40;
                            ctlTaiLieu1.LinkID = id;
                            ctlTaiLieu1.MaNV = Common.User.MaNV;
                            ctlTaiLieu1.objNV = Common.User;
                            ctlTaiLieu1.TaiLieu_Load();
                            break;
                        case "tabLichSu":
                            gcLichSu.DataSource = db.tbl_HoSo_LichSus.Where(_ => _.HoSoID == id);
                            break;
                        
                    }
                }
                
            }
            catch (Exception)
            {
                //
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void bbiXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                        return;
                    }
                    if (DialogBox.QuestionDelete() == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var o = db.tbl_HoSos.FirstOrDefault(_ =>
                            _.ID == int.Parse(gv.GetRowCellValue(r, "ID").ToString()));
                        if (o != null)
                        {
                            db.tbl_HoSo_LichSus.DeleteAllOnSubmit(o.tbl_HoSo_LichSus);
                            db.tbl_HoSos.DeleteOnSubmit(o);
                        }

                    }
                    db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng thiết bị này nên không xóa được");
                return;
            }
        }

        private void gvDanhSachYeuCau_RowClick(object sender, RowClickEventArgs e)
        {
            LoadDetail();
        }

        private void xtraTabDetail_Click(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void itemDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn bản ghi, xin cảm ơn.");
                return;

            }
            if (gv.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new ftp.frmDownloadFile();
            frm.FileName = gv.GetFocusedRowCellValue("DuongDan").ToString();
            if (frm.SaveAs())
                frm.ShowDialog();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmHoSo_Edit { MaTN = (byte)beiToaNha.EditValue })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("ID") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn.");
                    return;
                }

                using (var frm = new frmHoSo_Edit { MaTN = (byte)beiToaNha.EditValue, ID = (int?)gv.GetFocusedRowCellValue("ID") })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        private void gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.FieldName == "PhanLoaiName")
                    e.Appearance.BackColor = Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
            }
            catch { }
        }
    }
}