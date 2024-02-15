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
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Fund.Output.PhieuChi
{
    public partial class frmManagerHDT : DevExpress.XtraEditors.XtraForm
    {
        public frmManagerHDT()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        void LoadData()
        {

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            gcPhieuChi.DataSource = null;
            gcPhieuChi.DataSource = from p in db.pcPhieuChis
                                        //join pl in db.PhanLoaiChis on p.LoaiChi equals pl.ID into tblPhanLoai
                                        //from pl in tblPhanLoai.DefaultIfEmpty()
                                    join k in db.tnKhachHangs on p.MaNCC equals k.MaKH
                                    into tblKhachHang
                                    from k in tblKhachHang.DefaultIfEmpty()
                                    join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                    join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                    join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                    from nvs in tblNguoiSua.DefaultIfEmpty()
                                    join nvd in db.tnNhanViens on p.MaNVN equals nvd.MaNV into tblNguoiDuyet
                                    from nvd in tblNguoiDuyet.DefaultIfEmpty()
                                    join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                    from tk in tblTaiKhoan.DefaultIfEmpty()
                                    join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                    from nh in tblNganHang.DefaultIfEmpty()
                                        //join hd in db.ctHopDongs on p.idctHopDong equals hd.ID into hopdong
                                        //from hd in hopdong.DefaultIfEmpty()

                                    where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0
                                    //  & p.IsHDThue.GetValueOrDefault()
                                    select new
                                    {
                                        p.ID,
                                        //  SoHD = hd.SoHDCT,
                                        p.SoPC,
                                        NVDuyet = nvd.HoTenNV,
                                        p.NgayChi,
                                        p.SoTien,
                                        TenKH = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                                        HTTT = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                        NguoiChi = nv.HoTenNV,
                                        p.NguoiNhan,
                                        p.DiaChiNN,
                                        k.KyHieu,
                                       p.LyDo,
                                        //   pl.TenLoaiChi,
                                        p.ChungTuGoc,
                                        //  NguoiNhap = nvn.HoTenNV,
                                        p.NgayNhap,
                                        NguoiSua = nvs.HoTenNV,
                                        p.NgaySua,
                                        tk.SoTK,
                                        nh.TenNH,
                                    };
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddRecord()
        {
            using (var frm = new frmEditHDT())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");

            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEditHDT())
            {
                frm.ID = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void DeleteRecord()
        {
            var indexs = gvPhieuChi.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
                var objTL_U = (from tl in db.ctThanhLies
                               join hd in db.ctHopDongs on tl.MaHD equals hd.ID
                               join pc in db.pcPhieuChis on hd.MaKH equals pc.MaNCC
                               where pc.ID == id
                               select tl).FirstOrDefault();
                var objPT_D = (from tl in db.ctThanhLies
                               join hd in db.ctHopDongs on tl.MaHD equals hd.ID
                               join pc in db.pcPhieuChis on hd.MaKH equals pc.MaNCC
                               where pc.ID == id
                               select pc).FirstOrDefault();
                objTL_U.PhaiTra = objTL_U.PhaiTra + objPT_D.SoTien;
                objTL_U.DaTra = objTL_U.DaTra - objPT_D.SoTien;
                db.SubmitChanges();


                var pt = db.pcPhieuChis.Single(p => p.ID == (int)gvPhieuChi.GetRowCellValue(i, "ID"));
                db.pcPhieuChis.DeleteOnSubmit(pt);
            }

            try
            {
                db.SubmitChanges();

                this.LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void Details()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from ct in db.pcChiTiets
                                        where ct.MaPC == id
                                        select new { ct.DienGiai, ct.SoTien })
                                       .ToList();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuChi.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            gvChiTiet.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void gvPhieuThu_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Details();
        }

        private void gvPhieuThu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Details();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from p in db.pcPhieuChis
                                    //join pl in db.PhanLoaiChis on p.LoaiChi equals pl.ID into tblPhanLoai
                                    //from pl in tblPhanLoai.DefaultIfEmpty()
                                join k in db.tnKhachHangs on p.MaNCC equals k.MaKH
                                into tblKhachHang
                                from k in tblKhachHang.DefaultIfEmpty()
                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join nvd in db.tnNhanViens on p.MaNVN equals nvd.MaNV into tblNguoiDuyet
                                from nvd in tblNguoiDuyet.DefaultIfEmpty()
                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                from nh in tblNganHang.DefaultIfEmpty()

                                    //join hd in db.ctHopDongs on p.idctHopDong equals hd.ID into hopdong
                                    //from hd in hopdong.DefaultIfEmpty()

                                where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayChi) >= 0 & SqlMethods.DateDiffDay(p.NgayChi, denNgay) >= 0
                                & p.IsHDThue.GetValueOrDefault()
                                select new
                                {
                                    p.ID,
                                    //SoHD = hd.SoHDCT,
                                    //p.SoPC,p.IsDuyet,p.NgayDuyet,NVDuyet=nvd.HoTenNV,
                                    p.NgayChi,
                                    p.SoTien,
                                    TenKH = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                                    HTTT = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                    NguoiChi = nv.HoTenNV,
                                    p.NguoiNhan,
                                    p.DiaChiNN,
                                    k.KyHieu,
                                    p.LyDo,
                                    //pl.TenLoaiChi,
                                    p.ChungTuGoc,
                                    NguoiNhap = nvn.HoTenNV,
                                    p.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    p.NgaySua,
                                    tk.SoTK,
                                    nh.TenNH,
                                };
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DismissQueryable: " + ex.Message);
            }
        }

        private void itemInPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] rows = gvPhieuChi.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu chi] cần in");
                return;
            }

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                foreach (var item in rows)
                {
                    var rpt = new rptPhieuChi((int)gvPhieuChi.GetRowCellValue(item, "ID"), maTN);
                    rpt.Print();

                    System.Threading.Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0}", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); }
        }

        private void itemXemPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu chi] cần xem");
                return;
            }

            var maTN = (byte)itemToaNha.EditValue;
            var rpt = new rptPhieuChi(id.Value, maTN);
            rpt.ShowPreviewDialog();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuChi);
        }
        void Duyet(bool isDuyet, DateTime? ngay)
        {
            var rows = gvPhieuChi.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Phiếu chi]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    if ((bool?)gvPhieuChi.GetRowCellValue(i, "IsDuyet") == isDuyet) continue;

                    var objHD = db.pcPhieuChis.Single(p => p.ID == (int)gvPhieuChi.GetRowCellValue(i, "ID"));
                    //objHD.IsDuyet = isDuyet;
                    //if (isDuyet == true)
                    //{
                    //    objHD.NgayDuyet = ngay;
                    //    objHD.MaNVDuyet = Common.User.MaNV;
                    //}
                    //else
                    //{
                    //    objHD.NgayDuyet = null;
                    //    objHD.MaNVDuyet = null;
                    //}
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var frm = new frmNgayDuyet();
            //frm.ShowDialog();
            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    Duyet(true,frm.ngayduyet);
            //}
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(false, null);
        }
    }
}