using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuilding.Lease.DatCoc.ThiCong
{
    public partial class frmDanhSachNghiemThu : DevExpress.XtraEditors.XtraForm
    {
        #region Class
        public class dkThiCong_LoadList
        {
            public Guid Id { get; set; }

            public int? MaTT { get; set; }

            public System.DateTime? From { get; set; }

            public System.DateTime? To { get; set; }

            public System.DateTime? TransferDate { get; set; }

            public System.DateTime? AcceptDate { get; set; }

            public bool IsUsedrill { get; set; }

            public bool IsMakeNoise { get; set; }

            public bool IsUseWelder { get; set; }

            public bool? IsHaveWasteForks { get; set; }

            public bool IsTransfer { get; set; }

            public decimal? Employees { get; set; }

            public decimal? TienDatCoc { get; set; }

            public decimal? TienPhat { get; set; }

            public decimal? TienHoanTra { get; set; }

            public string CustomerName { get; set; }

            public string ApartmentName { get; set; }

            public string DepartmentName { get; set; }

            public string Category { get; set; }

            public string Note { get; set; }

            public string TransferBy { get; set; }

            public string StatusName { get; set; }

            public string AcceptBy { get; set; }
            public int? TaiLieuId { get; set; }
            public decimal? Payment { get; set; }
        }

        public class Log_CapNhatTrangThai_DCTC_Load
        {
            public Guid? ID_dkThiCong { get; set; }

            public int ID { get; set; }

            public System.DateTime? NgayCN { get; set; }

            public string HoTenNV { get; set; }

            public string TrangThaiCu { get; set; }

            public string TrangThaiMoi { get; set; }

        }
        #endregion
        public frmDanhSachNghiemThu()
        {
            InitializeComponent();
        }

        private void frmDanhSachNghiemThu_Load(object sender, EventArgs e)
        {
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.TowerList.First().MaTN;
            LoadData();
        }

        private void itemDuyetNghiemThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = gvHopDong.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những danh sách nghiệm thu");
                return;
            }

            if (DialogBox.Question("Đồng ý duyệt?") == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (int i in indexs)
                {
                    try
                    {
                        var id_ = gvHopDong.GetRowCellValue(i, "Id");
                        if (id_ == null) continue;
                        var thiCong = db.dkThiCongs.Where(x => x.Id == (Guid?)gvHopDong.GetRowCellValue(i, "Id")).FirstOrDefault();
                        if (thiCong != null)
                        {
                            thiCong.MaTT = 9;
                            db.SubmitChanges();
                        }
                        
                    }
                    catch { }

                }

                LoadData();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        private void itemBoDuyetNghiemThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (Guid?)gvHopDong.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn dánh sách nghiệm thu");
                    return;
                }
                var db = new MasterDataContext();
                var hd = db.dkThiCongs.FirstOrDefault(_ => _.Id == id);
                if (hd == null)
                {
                    DialogBox.Error("Dữ liệu không chính xác");
                    return;
                }

                using (var frm = new Accept.frmAccept())
                {
                    frm.Text = "Hủy duyệt";
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        hd.MaTT = 10;
                        db.SubmitChanges();
                    }
                }

                LoadData();
            }
            catch
            {

                return;
            }
        }
        private void Detail()
        {
            try
            {
                var db = new MasterDataContext();
                var id = (Guid?)gvHopDong.GetFocusedRowCellValue("Id");
                if (id != null)
                {
                    if (id == null)
                    {
                        switch (tabMain.SelectedTabPageIndex)
                        {
                            case 0:
                                ctlTaiLieu1.TaiLieu_Load();
                                ctlTaiLieu1.MaNV = null;
                                ctlTaiLieu1.objNV = null;
                                break;
                            case 1:
                                gcLichSuCapNhat.DataSource = null;
                                break;
                            case 2:
                                gcPhieuThu.DataSource = null;
                                break;
                            case 3:
                                gcPhieuChi.DataSource = null;
                                break;
                            case 4:

                                break;
                        }
                        return;
                    }

                    switch (tabMain.SelectedTabPageIndex)
                    {
                        case 0:
                            ctlTaiLieu1.FormID = 3204;
                            ctlTaiLieu1.LinkID = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("TaiLieuId"));
                            ctlTaiLieu1.MaNV = Common.User.MaNV;
                            ctlTaiLieu1.objNV = Common.User;
                            ctlTaiLieu1.TaiLieu_Load();
                            break;
                        case 1:

                            break;
                        case 2:
                            var historys = Library.Class.Connect.QueryConnect.QueryData<Log_CapNhatTrangThai_DCTC_Load>("Log_CapNhatTrangThai_DCTC_Load",
                            new
                            {
                                ThiCongId = id
                            });
                            gcLichSuCapNhat.DataSource = historys;
                            //loadPhieuThu(id);
                            break;
                        case 3:
                            //loadPhieuChi(id);
                            break;
                        case 4:

                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                DialogBox.Error(ex.Message);
            }

        }
        private void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var sql = Library.Class.Connect.QueryConnect.QueryData<dkThiCong_LoadList>("dkThiCong_LoadList",
                    new
                    {
                        TowerId = (byte?)itemToaNha.EditValue,
                        Type = 2
                    });
                gcHopDong.DataSource = sql;
                gvHopDong.FocusedRowHandle = -1;
            }
        }
        void RefreshData()
        {
            LoadData();
        }
        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
        private void gvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }
        private void itemHoanTraCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            var thiCong = db.dkThiCongs.Where(x => x.Id == (Guid?)gvHopDong.GetFocusedRowCellValue("Id")).FirstOrDefault();
            if (thiCong == null)
            {
                DialogBox.Error("Dữ liệu không chính xác");
                return;
            }
            if (thiCong.MaTT != 9)
            {
                DialogBox.Error("Cần được duyệt trước khi hoàn trả cọc");
                return;
            }
            using (var frm = new LandSoftBuilding.Fund.Output.frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.MaKH = thiCong.MaKH;
                frm.PhanLoaiChi = 10;
                frm.ThiCongId = (Guid?)gvHopDong.GetFocusedRowCellValue("Id");
                frm.SoTien = Convert.ToDecimal(gvHopDong.GetFocusedRowCellValue("Payment"));
                frm.IsDatCocThiCong = true;

                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) RefreshData();
            }
        }
    }
}