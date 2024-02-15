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

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmPhieuThuDaXoa : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieuThuDaXoa()
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
            gcPhieuThu.DataSource = null;
            gcPhieuThu.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddRecord()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.MaPT = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void DeleteRecord()
        {
            var indexs = gvPhieuThu.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var pt = db.ptPhieuThuDaXoas.Single(p => p.ID == (int)gvPhieuThu.GetRowCellValue(i, "ID"));
                db.ptPhieuThuDaXoas.DeleteOnSubmit(pt);
            }

            try
            {
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

        void Details()
        {
            var db = new MasterDataContext();
            try
            {
                var id = gvPhieuThu.GetFocusedRowCellValue("SoPT").ToString();
                if (id == "")
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThuDaXoas
                                        where ct.MaPT == id
                                        select new { ct.DienGiai, ct.SoTien, ct.SAP_PT, ct.SAP_MSG })
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

        void ImportRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuThu.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
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

            e.QueryableSource = from p in db.ptPhieuThuDaXoas
                                join pl in db.ptPhanLoais on p.MaPL equals pl.ID into tblPhanLoai
                                from pl in tblPhanLoai.DefaultIfEmpty()
                                join k in db.tnKhachHangs on p.MaKH equals k.MaKH 
                                join nkh in db.khNhomKhachHangs on k.MaNKH equals nkh.ID 
                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                from nh in tblNganHang.DefaultIfEmpty()
                                where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayThu) >= 0 & SqlMethods.DateDiffDay(p.NgayThu, denNgay) >= 0
                                select new
                                {
                                    p.ID,
                                    p.SoPT,
                                    p.NgayThu,
                                    p.SoTien,
                                    k.KyHieu,
                                    TenKH = (bool)k.IsCaNhan ? String.Format("{0} {1}", k.HoKH, k.TenKH) : k.CtyTen,
                                    NguoiThu = nv.HoTenNV,
                                    p.NguoiNop,
                                    p.DiaChiNN,
                                    p.LyDo,
                                    pl.TenPL,
                                    p.ChungTuGoc,
                                    NguoiNhap = nvn.HoTenNV,
                                    p.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    p.NgaySua,
                                    PhuongThuc = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                    tk.SoTK,
                                    nh.TenNH,
                                    nkh.TenNKH
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
            int[] rows = gvPhieuThu.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần in");
                return;
            }

            int count = 1;
            var wait = DialogBox.WaitingForm();
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                foreach (var item in rows)
                {
                    var rpt = new rptPhieuThu((int)gvPhieuThu.GetRowCellValue(item, "ID"), maTN, 1);
                    for (int i = 0; i < 3; i++)
                    {
                        var rpt1 = new rptPhieuThu((int)gvPhieuThu.GetRowCellValue(item, "ID"), maTN, i);
                        rpt1.CreateDocument();
                        rpt.Pages.AddRange(rpt1.Pages);
                    }
                    rpt.PrintingSystem.ContinuousPageNumbering = true;
                    rpt.Print();

                    System.Threading.Thread.Sleep(50);

                    count++;
                    wait.SetCaption(string.Format("Đã in {0:n0}/{1:n0}", count, rows.Length));
                }
            }
            catch { }
            finally { wait.Close(); }
        }

        private void itemXemPhieuThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                return;
            }

            var maTN = (byte)itemToaNha.EditValue;
            var rpt = new rptPhieuThu(id.Value, maTN,1);
            for (int i = 1; i <= 3; i++)
            {
                var rpt1 = new rptPhieuThu(id.Value, maTN, i);
                rpt1.CreateDocument();
                rpt.Pages.AddRange(rpt1.Pages);
            }
            rpt.PrintingSystem.ContinuousPageNumbering = true;
            rpt.ShowPreviewDialog();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }
    }
}