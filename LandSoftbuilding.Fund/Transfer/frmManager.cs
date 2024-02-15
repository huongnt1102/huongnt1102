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

namespace LandSoftBuilding.Fund.Transfer
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
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
            MasterDataContext db = new MasterDataContext();
            gcPhieuChuyenTien.DataSource = from ct in db.PhieuChuyenTiens
                                           join khc in db.tnKhachHangs on ct.MaKHCT equals khc.MaKH
                                           join khn in db.tnKhachHangs on ct.MaKHNhanTien equals khn.MaKH
                                           join nn in db.tnNhanViens on ct.NguoiNhap equals nn.MaNV
                                           join ns in db.tnNhanViens on ct.NguoiSua equals ns.MaNV into nsua
                                           from ns in nsua.DefaultIfEmpty()
                                           where SqlMethods.DateDiffDay((DateTime?)tuNgay, ct.NgayCT) >= (int?)0 && SqlMethods.DateDiffDay(ct.NgayCT, (DateTime?)denNgay) >= (int?)0 && ct.MaTN == maTN
                                           select new
                                           {
                                               ct.ID,
                                               ct.NgayCT,
                                               ct.SoCT,
                                               ct.SoTienChuyen,
                                               ct.NgayNhap,
                                               ct.NgaySua,
                                               KhachHangChuyen = khc.IsCaNhan == true ? khc.HoKH + " " + khc.TenKH : khc.CtyTen,
                                               KhachHangNhan = khn.IsCaNhan == true ? khn.HoKH + " " + khn.TenKH : khn.CtyTen,
                                               ct.DienGiai,
                                               NguoiNhap = nn.HoTenNV,
                                               NguoiSua = ns.HoTenNV
                                           };

            
        }

        void RefreshData()
        {
            LoadData();
        }

        void AddRecord()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.IsSave)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvPhieuChuyenTien.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.MaCK = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.IsSave)
                    this.RefreshData();
            }
        }

        void DeleteRecord()
        {
            var id =(int?) gvPhieuChuyenTien.GetFocusedRowCellValue("ID");

            if (id==null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu chuyển tiền muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            var objPCT = db.PhieuChuyenTiens.FirstOrDefault(p => p.ID == id);
            if (objPCT != null)
            {
                //Xóa phiếu thu
                foreach (var item in db.ptPhieuThus.Where(pt => pt.MaPCT == id))
                {
                    db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == item.ID));
                }
                db.ptPhieuThus.DeleteAllOnSubmit(db.ptPhieuThus.Where(pt => pt.MaPCT == id));
                db.PhieuChuyenTien_PhieuThus.DeleteAllOnSubmit(db.PhieuChuyenTien_PhieuThus.Where(p => p.PhieuChuyenTienID == id));
                db.PhieuChuyenTiens.DeleteOnSubmit(objPCT);
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
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuChuyenTien.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);

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
            e.QueryableSource = from ct in db.PhieuChuyenTiens
                                join khc in db.tnKhachHangs on ct.MaKHCT equals khc.MaKH
                                join khn in db.tnKhachHangs on ct.MaKHNhanTien equals khn.MaKH
                                join nn in db.tnNhanViens on ct.NguoiNhap equals nn.MaNV
                                join ns in db.tnNhanViens on ct.NguoiSua equals ns.MaNV into nsua
                                from ns in nsua.DefaultIfEmpty()
                                where SqlMethods.DateDiffDay((DateTime?)tuNgay, ct.NgayCT) >= (int?)0 && SqlMethods.DateDiffDay(ct.NgayCT, (DateTime?)denNgay) >= (int?)0 && ct.MaTN == maTN
                                select new { 
                                ct.NgayCT,
                                ct.SoCT,
                                ct.SoTienChuyen,
                                ct.NgayNhap,
                                ct.NgaySua,
                                HoTenNguoiChuyen=khc.IsCaNhan==true?khc.HoKH+" "+khc.TenKH:khc.CtyTen,
                                HoTenNguoiNhan = khn.IsCaNhan == true ? khn.HoKH + " " + khn.TenKH : khn.CtyTen,
                                ct.DienGiai,
                                NguoiNhap=nn.HoTenNV,
                                NguoiSua=ns.HoTenNV
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

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuChuyenTien);
        }

        private void gvPhieuChuyenTien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvPhieuChuyenTien.GetFocusedRowCellValue("ID") != null)
            {
                 var db = new MasterDataContext();
                 gcPhieuThu.DataSource = (from pct in db.PhieuChuyenTien_PhieuThus
                                          join pt in db.ptPhieuThus on pct.MaPT equals pt.ID
                                          join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                                          where pct.PhieuChuyenTienID == (int?)gvPhieuChuyenTien.GetFocusedRowCellValue("ID")
                                          select new {
                                          pt.NgayThu,
                                          pt.SoTien,
                                          SoPhieu=pt.SoPT,
                                          KhachHang = ((kh.IsCaNhan == (bool?)true) ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen),
                                          }).ToList();
            }
            else
            {
                gcPhieuThu.DataSource = null;
            }
        }


    }
}