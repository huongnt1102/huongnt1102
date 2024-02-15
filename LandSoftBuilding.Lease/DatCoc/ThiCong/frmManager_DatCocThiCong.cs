using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;
using LandSoftBuilding.Lease.GanHetHan;
using LandSoftBuilding.Fund.Input;
using DevExpress.XtraReports.UI;
using LandSoftBuilding.Lease.Class;

namespace LandSoftBuilding.Lease.DatCoc.ThiCong
{
    public partial class frmManager_DatCocThiCong : DevExpress.XtraEditors.XtraForm
    {
        #region Class
        public class dkThiCong_LoadList
        {
            public Guid Id { get; set; }

            public int? MaTT { get; set; }

            public int TaiLieuId { get; set; }

            public int? MaKH { get; set; }

            public System.DateTime? From { get; set; }

            public System.DateTime? To { get; set; }

            public System.DateTime? TransferDate { get; set; }

            public System.DateTime? AcceptDate { get; set; }

            public System.DateTime? NgayTao { get; set; }

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

        public frmManager_DatCocThiCong()
        {
            InitializeComponent();
        }

        private void frmHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.TowerList.First().MaTN;
            gvHopDong.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            loadMatBang();
            LoadData();
        }

        private void loadMatBang()
        {
            using (var db = new MasterDataContext())
            {
                //Load mat bang
                glMatBang.DataSource = (from mb in db.mbMatBangs
                                        join l in db.mbTangLaus on mb.MaTL equals l.MaTL
                                        join k in db.mbKhoiNhas on l.MaKN equals k.MaKN
                                        where k.MaTN == Convert.ToInt32(itemToaNha.EditValue)
                                        orderby mb.MaSoMB
                                        select new { mb.MaMB, mb.MaSoMB, l.TenTL, k.TenKN, mb.DienTich }).ToList();
            }
        }

        private void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var sql = Library.Class.Connect.QueryConnect.QueryData<dkThiCong_LoadList>("dkThiCong_LoadList",
                    new
                    {
                        TowerId = (byte?)itemToaNha.EditValue
                    });
                gcHopDong.DataSource = sql;
                gvHopDong.FocusedRowHandle = -1;
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
            catch(System.Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new LandSoftBuilding.Lease.DatCoc.ThiCong.frmEdit {
                TowerId = (byte?)itemToaNha.EditValue 
            })
            {
                frm.ShowDialog();
                LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = gvHopDong.GetFocusedRowCellValue("Id");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [phiếu đặt cọc], xin cảm ơn.");
                    return;
                }

                using (var db = new MasterDataContext())
                {
                    var objcheck = db.dkThiCongs.FirstOrDefault(p => Convert.ToString(p.Id) == Convert.ToString(id));
                    if (objcheck != null)
                    {
                        if (objcheck.MaTT != 1)
                        {
                            DialogBox.Alert("Phiếu đặt cọc này đã duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                            return;
                        }
                    }
                }

                using (var frm = new frmEdit { TowerId = (byte?)itemToaNha.EditValue, Id = id })
                {
                    frm.ShowDialog();
                    LoadData();
                }
            }
            catch
            {

            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var ID = gvHopDong.GetFocusedRowCellValue("Id");
            if (ID == null) return;

            try
            {
                using (var db = new MasterDataContext())
                {
                    var objcheck = db.dkThiCongs.FirstOrDefault(p => Convert.ToString(p.Id) == Convert.ToString(ID));
                    if (objcheck != null)
                    {
                        if (objcheck.MaTT != 1)
                        {
                            DialogBox.Alert("Phiếu đặt cọc này đã duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                            return;
                        }
                    }

                    db.dkThiCong_ChiTiets.DeleteAllOnSubmit(objcheck.dkThiCong_ChiTiets);

                    db.Log_CapNhatTrangThai_DCTCs.DeleteAllOnSubmit(db.Log_CapNhatTrangThai_DCTCs.Where(_=>_.ID_dkThiCong == objcheck.Id));

                        db.dkThiCongs.DeleteOnSubmit(objcheck);
                        db.SubmitChanges();

                    LoadData();
                }

                
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi khi xóa dữ liệu: "+ ex.Message);
            }
        }

        private void gvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }

        private void loadPhieuThu(int? ID)
        {

        }

        private void loadPhieuChi(int? ID)
        {

        }

        private void loadLichSuCapNhat(int? ID)
        {

        }

        private void itemCapNhatTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemTaoPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = gvHopDong.GetFocusedRowCellValue("Id");
                if (id == null) return;

                using (var db = new MasterDataContext())
                {
                    var objcheck = db.dkThiCongs.FirstOrDefault(p => Convert.ToString(p.Id) == Convert.ToString(id));
                    if (objcheck != null)
                    {
                        if (objcheck.MaTT == 1)
                        {
                            DialogBox.Alert("Phiếu đặt cọc này chưa được duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                            return;
                        }

                        if (objcheck.MaPT.GetValueOrDefault() > 0)
                        {
                            DialogBox.Alert("Phiếu đặt cọc này đã phát sinh phiếu thu. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                            return;
                        }
                    }
                }


                using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
                {
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.IsDatCocThiCong = true;
                    frm.ThiCongId = id;
                    frm.MaKH = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaKH"));
                    frm.SoTien = Convert.ToDecimal(gvHopDong.GetFocusedRowCellValue("TienDatCoc"));
                    frm.PhanLoaiId = 50; // Phân loại thu đặt cọc thi công
                    frm.ShowDialog();
                    LoadData();
                }
            }
            catch
            {
            }
            
        }

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            Detail();
        }

        private void itemTaoPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gvHopDong_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void itemKhongDuyetPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemLapHopDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        // Duyệt phiếu
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var selectedRows = gvHopDong.GetSelectedRows();

                if (selectedRows.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn phiếu. Xin cám ơn!");
                    return;
                }

                int StatusId = 2;

                if (selectedRows.Count() <= 1)
                {
                    foreach (int rowHandle in selectedRows)
                    {
                        var id = gvHopDong.GetRowCellValue(rowHandle, "Id");
                        var id_TrangThai = Convert.ToInt32(gvHopDong.GetRowCellValue(rowHandle, "MaTT"));

                        if (id_TrangThai != 1)
                        {
                            DialogBox.Alert("Hợp đồng chưa ở trạng thái chờ duyệt");
                            return;
                        }
                    }
                }

                using (var frm = new frmTrangThaiDatCoc(null, StatusId) {
                    ID_TrangThai = StatusId
                })
                {
                    frm.ShowDialog();
                    var note = frm.Note;

                    foreach (int rowHandle in selectedRows)
                    {
                        var id = gvHopDong.GetRowCellValue(rowHandle, "Id");
                        var id_TrangThai = Convert.ToInt32(gvHopDong.GetRowCellValue(rowHandle, "MaTT"));

                        if (id_TrangThai != 1)
                        {
                            continue;
                        }

                        using (var dbo = new MasterDataContext())
                        {
                            var objTC = dbo.dkThiCongs.FirstOrDefault(p => Convert.ToString(p.Id) == Convert.ToString(id));
                            if (objTC != null)
                            {
                                objTC.NhanVienDuyet = Common.User.MaNV;
                                objTC.NgayDuyet = System.DateTime.UtcNow.AddHours(7);
                                objTC.LyDoKhongDuyet = note;

                                // Ghi lại lịch sử quá trình duyệt
                                Log_CapNhatTrangThai_DCTC objLog = new Log_CapNhatTrangThai_DCTC();
                                objLog.NgayCN = dbo.GetSystemDate();
                                objLog.MaNV = Common.User.MaNV;
                                objLog.MaTrangThaiCu =objTC.MaTT;
                                objLog.MaTrangThaiMoi = StatusId;
                                objLog.ID_dkThiCong = objTC.Id;
                                objLog.GhiChu = note;
                                dbo.Log_CapNhatTrangThai_DCTCs.InsertOnSubmit(objLog);

                                objTC.MaTT = StatusId;

                                dbo.SubmitChanges();
                            }

                            #region Gửi notify đến khách hàng

                            List<Class.dk_token_kh> tokens = new List<Class.dk_token_kh>();
                            var list_token = Library.Class.Connect.QueryConnect.QueryData<Class.dk_token_kh>("dk_thicong_get_token_kh", new
                            {
                                id = objTC.Id
                            });

                            if (list_token.Count() > 0)
                            {
                                tokens = list_token.ToList();

                                var listToken = tokens.Select(_ => _.Token).ToList();
                                var itemFirst = tokens.First();

                                var model_notification = new NotificationSendBindingModel
                                {
                                    notification = new NotificationModel
                                    {
                                        body = itemFirst.DienGiai,
                                        title = itemFirst.TieuDe,
                                        tag = objTC.Id.ToString()
                                    },
                                    data = new DataModel
                                    {
                                        actionId = 22,
                                        body = itemFirst.DienGiai,
                                        title = itemFirst.TieuDe,
                                        item = new ItemModel
                                        {
                                            residentId = itemFirst.ResidentId,
                                            typeId = 22,
                                            dateCreate = System.DateTime.Now,
                                            id = objTC.Id.ToString(),
                                            imageUrl = null,
                                            isImportant = false,
                                            isRead = false,
                                            shortDescription = itemFirst.DienGiai,
                                            title = itemFirst.TieuDe,
                                            towerId = (byte)itemFirst.MaTN,
                                            towerName = itemFirst.TenVT
                                        }
                                    },
                                    registration_ids = listToken
                                };

                                //var post = Building.AppVime.VimeService.PostH(model_notification, $"/Notification/SendNotifyBasic");
                                var post = Building.AppVime.VimeService.PostH(model_notification, "/Notification/SendNotifyBasic");
                                if (post == "\"OK\"")
                                {
                                    foreach (var item in tokens)
                                    {
                                        var save_notify_model = new
                                        {
                                            id = objTC.Id.ToString(),
                                            typeid = 22,
                                            residentid = item.ResidentId,
                                            diengiai = item.DienGiai,
                                            tieude = item.TieuDe,
                                            towerid = item.MaTN
                                        };
                                        var kq_notify = Library.Class.Connect.QueryConnect.QueryData<bool>("dbo.app_notification_create_customer", save_notify_model);
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                }

                //DialogBox.Success();

                LoadData();
            }
            catch (System.Exception ex)
            {
            }
        }

        // Bỏ duyệt phiếu
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var selectedRows = gvHopDong.GetSelectedRows();

                if (selectedRows.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn phiếu. Xin cám ơn!");
                    return;
                }

                int StatusId = 1;

                if (selectedRows.Count() <= 1)
                {
                    foreach (int rowHandle in selectedRows)
                    {
                        var id = gvHopDong.GetRowCellValue(rowHandle, "Id");
                        var id_TrangThai = Convert.ToInt32(gvHopDong.GetRowCellValue(rowHandle, "MaTT"));
                        var tienDatCoc = gvHopDong.GetRowCellValue(rowHandle, "TienDatCoc");

                        if (id_TrangThai != 2)
                        {
                            DialogBox.Alert("Hợp đồng chưa ở trạng thái đã duyệt");
                            return;
                        }

                        else if (tienDatCoc != null)
                        {
                            DialogBox.Alert("Hợp đồng đã có tiền đặt cọc");
                            return;
                        }
                    }
                }

                using (var frm = new frmTrangThaiDatCoc(null, StatusId)
                {
                    ID_TrangThai = StatusId
                })
                {
                    frm.ShowDialog();
                    var note = frm.Note;

                    foreach (int rowHandle in selectedRows)
                    {
                        var id = gvHopDong.GetRowCellValue(rowHandle, "Id");
                        var id_TrangThai = Convert.ToInt32(gvHopDong.GetRowCellValue(rowHandle, "MaTT"));
                        var tienDatCoc = gvHopDong.GetRowCellValue(rowHandle, "TienDatCoc");

                        if (id_TrangThai != 2)
                        {
                            continue;
                        }

                        else if (tienDatCoc != null)
                        {
                            continue;
                        }

                        using (var dbo = new MasterDataContext())
                        {
                            var objTC = dbo.dkThiCongs.FirstOrDefault(p => Convert.ToString(p.Id) == Convert.ToString(id));
                            if (objTC != null)
                            {
                                objTC.NhanVienDuyet = Common.User.MaNV;
                                objTC.NgayDuyet = System.DateTime.UtcNow.AddHours(7);
                                objTC.LyDoKhongDuyet = note;

                                // Ghi lại lịch sử quá trình duyệt
                                Log_CapNhatTrangThai_DCTC objLog = new Log_CapNhatTrangThai_DCTC();
                                objLog.NgayCN = dbo.GetSystemDate();
                                objLog.MaNV = Common.User.MaNV;
                                objLog.MaTrangThaiCu = objTC.MaTT;
                                objLog.MaTrangThaiMoi = StatusId;
                                objLog.ID_dkThiCong = objTC.Id;
                                objLog.GhiChu = note;
                                dbo.Log_CapNhatTrangThai_DCTCs.InsertOnSubmit(objLog);

                                objTC.MaTT = StatusId;

                                dbo.SubmitChanges();
                            }
                        }
                    }
                }

                //DialogBox.Success();

                LoadData();
            }
            catch (System.Exception ex)
            {
            }
        }

        // Thanh lý lên hợp đồng
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        // Thanh lý hoàn trả cọc
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var objHD = (Guid)gvHopDong.GetFocusedRowCellValue("Id");
            if (objHD == null) return;

            using (var dbo = new MasterDataContext())
            {
                var objcheck = dbo.dkThiCongs.FirstOrDefault(p => p.Id == objHD);
                if (objcheck != null)
                {
                    if (objcheck.MaTT != 2)
                    {
                        DialogBox.Alert("Phiếu đặt cọc này chưa được duyệt. Vui lòng chọn phiếu đặt cọc khác. Xin cảm ơn!");
                        return;
                    }
                }
            }

            using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.IsDatCocThiCong = true;
                frm.ThiCongId = objHD;
                frm.MaKH = Convert.ToInt32(gvHopDong.GetFocusedRowCellValue("MaKH"));
                frm.PhanLoaiId = 51;
                //frm.SoTien = Convert.ToDecimal(gvHopDong.GetFocusedRowCellValue("TienPhat"));
                frm.ShowDialog();
                LoadData();
            }
        }

        private void itemYeuCauNghiemThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = new MasterDataContext();
            var datCocThiCongId = (Guid?)gvHopDong.GetFocusedRowCellValue("Id");
            if (datCocThiCongId  == null)
            {
                DialogBox.Error("Vui lòng chọn dòng đặt cọc thi công");
                return;
            }
            var dkThiCong = db.dkThiCongs.Where(x => x.Id == datCocThiCongId).FirstOrDefault();
            if (dkThiCong != null)
            {
                if (dkThiCong.MaTT != 2)
                {
                    DialogBox.Error("Chỉ được nghiệm thu các dòng đã được duyệt và đóng tiền cọc");
                    return;
                }
                var tienCoc = dkThiCong.TienDatCoc > 0 ? dkThiCong.TienDatCoc.Value : 0;
                var tienPhat = dkThiCong.TienPhat > 0 ? dkThiCong.TienPhat.Value : 0 ;
                var tienHoanTra = tienCoc - tienPhat;
                using (frmYeuCauNghiemThu frm = new frmYeuCauNghiemThu { DatCocThiCongId = datCocThiCongId.Value, TienDatCoc = tienCoc, TienPhat = tienPhat, TienHoanTra = tienHoanTra })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.LoadData();
                    }
                }
            }
            
        }
    }
}