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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace DichVu.YeuCau
{
    public partial class frmCongViecNhanVien : DevExpress.XtraEditors.XtraForm
    {
        public int? MaYC { get; set; }

        MasterDataContext db;
        bool first = true;

        public frmCongViecNhanVien()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        public void ClickYC()
        {
            //
            var maYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
            if (maYC == null)
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        txtYeuCau.Text = "";
                        break;
                    case 1:
                        gcNKXL.DataSource = null;
                        break;
                    case 2:
                        gcGiaoViec.DataSource = null;
                        break;
                }
                gcGiaoViecDetail.DataSource = null;
                return;
            }

            txtYeuCau.Text = grvYeuCau.GetFocusedRowCellValue("NoiDung").ToString();

            gcNKXL.DataSource = (from ct in db.tnycLichSuCapNhats
                                 join nv in db.tnNhanViens on ct.MaNV equals nv.MaNV into vns
                                 from nv in vns.DefaultIfEmpty()
                                 join kh in db.tnKhachHangs on ct.MaKH equals kh.MaKH into khs
                                 from kh in khs.DefaultIfEmpty()
                                 orderby ct.NgayCN descending
                                 where ct.MaYC == maYC
                                 select new
                                 {
                                     ct.ID,
                                     ct.tnycTrangThai.TenTT,
                                     ct.NgayCN,
                                     ct.NoiDung,
                                     HoTenNV = ct.MaKH != null ? kh.HoKH + " " + kh.TenKH : nv.HoTenNV,
                                     isKH = ct.MaKH != null ? "KH" : "NV",
                                     ct.MaNV,
                                     ct.tnycTrangThai.MauNen
                                 }).ToList();
        }

        #region LoadDanhSach
        public class DanhSachYeuCauCaNhan
        {
            public int? ID { get; set; }
            public int? MauNen { get; set; }
            public string MaYC { get; set; }
            public string TieuDe { get; set; }
            public string NoiDung { get; set; }
            public string SoDienThoai { get; set; }
            public string TenTT { get; set; }
            public string TenPB { get; set; }
            public string TenKH { get; set; }
            public string HoTenNTN { get; set; }
            public string HoTenNV { get; set; }
            public string TenDoUuTien { get; set; }
            public string MaSoMB { get; set; }
            public string TenNguonDen { get; set; }
            public System.DateTime? NgayYC { get; set; }
            
        }

        private System.Collections.Generic.List<DanhSachYeuCauCaNhan> GetDanhSachYeuCauCaNhans(System.DateTime tuNgay, System.DateTime denNgay, int? matn)
        {
            var list = (from p in db.tnycYeuCaus
                        join nv in db.tnNhanViens on p.MaNV equals nv.MaNV into nvs
                        from nv in nvs.DefaultIfEmpty()
                        join ntn in db.tnNhanViens on p.MaNTN equals ntn.MaNV
                        where p.MaNTN == Library.Common.User.MaNV & (MaYC !=null ?(p.ID == MaYC) :(SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0 & p.MaTN == matn)) &
                            p.MaMB!=null
                        orderby p.NgayYC descending
                        select new DanhSachYeuCauCaNhan
                        {
                            ID = p.ID,
                            MaYC = p.MaYC,
                            NgayYC = p.NgayYC,
                            TieuDe = p.TieuDe,
                            NoiDung = p.NoiDung,
                            SoDienThoai = p.SoDienThoai,
                            TenTT = p.tnycTrangThai.TenTT,
                            TenPB = p.tnPhongBan.TenPB,
                            TenKH = p.NguoiGui,
                            HoTenNTN = ntn.HoTenNV,
                            HoTenNV = nv.HoTenNV,
                            MauNen = p.tnycTrangThai.MauNen,
                            TenDoUuTien = p.tnycDoUuTien.TenDoUuTien,
                            MaSoMB = p.mbMatBang.MaSoMB,
                            TenNguonDen = p.tnycNguonDen.TenNguonDen
                        }).ToList();
            return list;
        }
        #endregion

        private async void LoadData()
        {
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                bsiXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                //| (SqlMethods.DateDiffDay(tuNgay, p.NgayYC.Value) >= 0 & SqlMethods.DateDiffDay(p.NgayYC.Value, denNgay) >= 0 & p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == maTN)
                db = new MasterDataContext();
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

                System.Collections.Generic.List<DanhSachYeuCauCaNhan> danhSachYeuCauCaNhans = new List<DanhSachYeuCauCaNhan>();

                await System.Threading.Tasks.Task.Run(() => { danhSachYeuCauCaNhans = GetDanhSachYeuCauCaNhans(tuNgay, denNgay, maTN); });

                gcYeuCau.DataSource = danhSachYeuCauCaNhans;

                itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                bsiXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                gcYeuCau.DataSource = null;
            }

            //grvYeuCau.BestFitColumns();
            //grvNKXL.BestFitColumns();
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }
        private void GiaoViec()
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu");
                return;
            }
            if(grvYeuCau.GetFocusedRowCellValue("HoTenNTN").ToString()!="")
            {
                DialogBox.Error("Công việc này đã có nhân viên thực hiện");
                return;
            }
            XuLyGiaoViec();
            
        }

        private void XuLyGiaoViec()
        {
            using (frmGiaoViec frm = new frmGiaoViec())
            {
                frm.MaYC = int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString());
                frm.MaTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                frm.TrangThai = 1;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void DoiNhanVien()
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu");
                return;
            }
            using (frmGiaoViec frm = new frmGiaoViec())
            {
                frm.MaYC = int.Parse(grvYeuCau.GetFocusedRowCellValue("ID").ToString());
                frm.MaTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                frm.TrangThai = 2;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            
            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
            first = false;
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemXuLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region
            //if (grvYeuCau.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn yêu cầu");
            //    return;
            //}
            //var frm = new ToaNha.NhacViec_ThongBao.frmNhacViecEdit();
            //frm.objnhanvien = objnhanvien;
            //frm.MaYC = (int)grvYeuCau.GetFocusedRowCellValue("ID");
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //     ClickYC();
            #endregion
        }

        private void grvYeuCau_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClickYC();
        }
      
        void LoadDetail()
        {
            var id = (int?)grvGiaoViec.GetFocusedRowCellValue("MaNhacViec");
            if (id == null) gcGiaoViecDetail.DataSource = null;
            gcGiaoViecDetail.DataSource = db.tnNhacViec_Details.Select(p => new { p.tnNhanVien.HoTenNV, p.DaDoc }).ToList();
        }

        private void grvGiaoViec_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        string getNewMaBT()
        {
            string MaBT = "";
            db.btDauMucCongViec_getNewMaBT(ref MaBT);
            return db.DinhDang(32, int.Parse(MaBT));
        }

        private void btnGuiYC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn yêu cầu để gửi cho bộ phận kĩ thuật. Xin cảm ơn!");
                return;
            }
            var objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == (int)grvYeuCau.GetFocusedRowCellValue("ID"));
            if (objYC.MaTT > 1)
            {
                DialogBox.Alert("Yêu cầu này đã gửi hoàn thành gửi cho kỹ thuật. Vui lòng chọn yêu cầu khác!");
                return;
            }
            var wait = DialogBox.WaitingForm();
            try
            {
                var objDMCV = new btDauMucCongViec();
                db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                
                db.btDauMucCongViecs.InsertOnSubmit(objDMCV);
                objDMCV.MaSoCV = getNewMaBT();
                objDMCV.MoTa = objYC.NoiDung;
                objDMCV.MaTN = objYC.MaTN;
                objDMCV.MaKN = objYC.mbMatBang.mbTangLau.MaKN;
                objDMCV.MaTL = objYC.mbMatBang.MaTL;
                objDMCV.MaMB = objYC.MaMB;
                objDMCV.NguonCV = 0;
                objDMCV.MaNguonCV = objYC.ID;
                objDMCV.TrangThaiCV = 1;
                objDMCV.ThoiGianGhiNhan = db.GetSystemDate();
                objYC.MaTT = 2;

                var objLS = new tnycLichSuCapNhat();
                objYC.tnycLichSuCapNhats.Add(objLS);
                objLS.MaNV = Common.User.MaNV;
                objLS.NgayCN = db.GetSystemDate();
                objLS.MaTT = 2;
                db.SubmitChanges();
                DialogBox.Alert("Yêu cầu đã được gửi cho bộ phận kĩ thuật. Xin cảm ơn!");
                LoadData();
                LoadDetail();
            }
            catch
            {
                DialogBox.Alert("Có lỗi phát sinh. Không thể gửi yêu cầu này cho kĩ thuật!");
            }
            finally
            {
                wait.Close();
            }
            
        }

        private void btnXacNhanChoKT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn yêu cầu để phản hồi cho bộ phận kĩ thuật. Xin cảm ơn!");
                return;
            }
            try
            {
                frmTimeConfirm frm = new frmTimeConfirm();
                frm.objNV = Common.User;
                frm.MaNguonCV = (int)grvYeuCau.GetFocusedRowCellValue("ID");
                frm.ShowDialog();
                LoadData();
                LoadDetail();
            }
            catch
            {
              //  DialogBox.Alert("Có lỗi phát sinh. Không thể gửi yêu cầu này cho kĩ thuật!");
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            ClickYC();
        }

        private void btnInPhieuXN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(grvYeuCau.FocusedRowHandle<0)
            {
                DialogBox.Alert("Vui lòng chọn [Yêu cầu] để in phiếu xin xác nhận. Xin cảm ơn!");
                return;
            }
            int? MaYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
            var obj = db.tnycYeuCaus.SingleOrDefault(p => p.ID == MaYC);
            DichVu.YeuCau.rptPhieuYeuCauXN rpt = new rptPhieuYeuCauXN(MaYC);
            rpt.ShowPreviewDialog();
            var objLS = new tnycLichSuCapNhat();
            objLS.MaNV = Common.User.MaNV;
            objLS.NgayCN = db.GetSystemDate();
            objLS.MaYC = MaYC;
            objLS.MaTT = obj.MaTT;
            obj.tnycLichSuCapNhats.Add(objLS);
            try
            {
                db.SubmitChanges();
            }
            catch
            {
                DialogBox.Alert("Phiếu yêu cầu xác nhận của khách hàng không thể tao!");
            }
        }

        private void btnXacNhanYCKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmXacNhanCVKT frm = new frmXacNhanCVKT();
            frm.objNV = Common.User;
            frm.ShowDialog();
        }

        private void grvYeuCau_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    if (e.Column.FieldName == "TenTT")
                    {
                        e.Appearance.BackColor = Color.FromArgb((int)grvYeuCau.GetRowCellValue(e.RowHandle, "MauNen"));
                    }
                }
            }
            catch { }
            //GridView view = sender as GridView;
            //if (e.Column.FieldName == "TenTT")
            //{
            //    string tentt = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TenTT"]);
            //    if (tentt == "Yêu cầu mới") // 1
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightSalmon;
            //    }
            //    if (tentt == "Đã tiếp nhận thông tin") // 2
            //    {
            //        //e.Appearance.BackColor = Color.DeepSkyBlue;
            //        //e.Appearance.BackColor2 = Color.LightCyan;
            //        e.Appearance.BackColor = Color.DeepPink;
            //        e.Appearance.BackColor2 = Color.LightPink;
            //    }
            //    if (tentt == "Đã xử lý xong") //3
            //    {
            //        e.Appearance.BackColor = Color.DeepSkyBlue;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //    if (tentt == "Báo cáo hoàn thành") //5
            //    {
            //        e.Appearance.BackColor = Color.LimeGreen;
            //        e.Appearance.BackColor2 = Color.LightGreen;
            //    }
            //    if (tentt == "Hủy phiếu") //6
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.Appearance.BackColor2 = Color.GhostWhite;
            //    }
            //    if (tentt == "Đã gửi nhà thầu") //7
            //    {
            //        e.Appearance.BackColor = Color.Aquamarine;
            //        e.Appearance.BackColor2 = Color.Azure;
            //    }
            //    if (tentt == "Xử lý lại") //8
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //        e.Appearance.BackColor2 = Color.LightYellow;
            //    }
            //    if (tentt == "Giao nhân viên")//9
            //    {
            //        e.Appearance.BackColor = Color.Orange;
            //        e.Appearance.BackColor2 = Color.LightGoldenrodYellow;
            //    }
            //    if (tentt == "Không hoàn thành")//10
            //    {
            //        e.Appearance.BackColor = Color.DarkMagenta;
            //        e.Appearance.BackColor2 = Color.Plum;
            //    }
            //}
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcYeuCau);
        }

        private void grvNKXL_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if(e.RowHandle>=0)
            {
                string isKH = view.GetRowCellDisplayText(e.RowHandle, view.Columns["isKH"]);
                if(isKH=="KH")
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void grvYeuCau_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            grvYeuCau.BestFitColumns();
            grvNKXL.BestFitColumns();
        }

        private void bbiDoiTrangThai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu");
                return;
            }

            using (frmXuLyYC frm = new frmXuLyYC())
            {
                int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
                frm.MaYC = (int?)grvYeuCau.GetFocusedRowCellValue("ID");
                frm.TrangThai = 1;
                frm.MaTN = maTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void grvNKXL_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle && e.RowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    if (e.Column.FieldName == "TenTT")
                    {
                        e.Appearance.BackColor = Color.FromArgb((int)grvNKXL.GetRowCellValue(e.RowHandle, "MauNen"));
                    }
                }
            }
            catch { }

            //GridView view = sender as GridView;
            //if (e.Column.FieldName == "TenTT")
            //{
            //    string tentt = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TenTT"]);
            //    if (tentt == "Đã tiếp nhận thông tin") // 2
            //    {
            //        //e.Appearance.BackColor = Color.DeepSkyBlue;
            //        //e.Appearance.BackColor2 = Color.LightCyan;
            //        e.Appearance.BackColor = Color.DeepPink;
            //        e.Appearance.BackColor2 = Color.LightPink;
            //    }
            //    if (tentt == "Đã xử lý xong") //3
            //    {
            //        e.Appearance.BackColor = Color.DeepSkyBlue;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //    if (tentt == "Báo cáo hoàn thành") //5
            //    {
            //        e.Appearance.BackColor = Color.LimeGreen;
            //        e.Appearance.BackColor2 = Color.LightGreen;
            //    }
            //    if (tentt == "Hủy phiếu") //6
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.Appearance.BackColor2 = Color.GhostWhite;
            //    }
            //    if (tentt == "Đã gửi nhà thầu") //7
            //    {
            //        e.Appearance.BackColor = Color.Aquamarine;
            //        e.Appearance.BackColor2 = Color.Azure;
            //    }
            //    if (tentt == "Xử lý lại") //8
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //        e.Appearance.BackColor2 = Color.LightYellow;
            //    }
            //    if (tentt == "Giao nhân viên")//9
            //    {
            //        e.Appearance.BackColor = Color.Orange;
            //        e.Appearance.BackColor2 = Color.LightGoldenrodYellow;
            //    }
            //    if (tentt == "Không hoàn thành")//10
            //    {
            //        e.Appearance.BackColor = Color.DarkMagenta;
            //        e.Appearance.BackColor2 = Color.Plum;
            //    }
            //}
        }

        private void bbiSuaLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvYeuCau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn yêu cầu");
                return;
            }
            if(grvNKXL.FocusedRowHandle<0)
            {
                DialogBox.Error("Vui lòng chọn lịch sử");
                return;
            }
            if (int.Parse(grvNKXL.GetFocusedRowCellValue("MaNV").ToString()) != Common.User.MaNV)
            {
                DialogBox.Error("Bạn không thể sửa dòng lịch sử của người khác! Thân!");
                return;
            }
            using (frmXuLyYC frm = new frmXuLyYC())
            {
                frm.MaYC = (int?)grvNKXL.GetFocusedRowCellValue("ID");
                frm.TrangThai = 3;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}