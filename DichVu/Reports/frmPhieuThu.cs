using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Data.PivotGrid;

namespace DichVu.Reports
{
    public partial class frmPhieuThu : DevExpress.XtraEditors.XtraForm
    {
        public frmPhieuThu()
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

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltToaNha = this.GetToaNha();
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                //Nap vào pivot
                pvData.DataSource = (from ct in db.ptChiTietPhieuThus
                                     join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                     join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                     join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                     join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                     join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                     from mb in tblMatBang.DefaultIfEmpty()
                                     join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                     from tl in tblTangLau.DefaultIfEmpty()
                                     join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                     from kn in tblKhoiNha.DefaultIfEmpty()
                                     join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                     join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                     from lmb in tblLoaiMatBang.DefaultIfEmpty()
                                     join lx in db.dvgxLoaiXes on hd.MaLX equals lx.MaLX into LoaiXe from lx in LoaiXe.DefaultIfEmpty()
                                     join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV into nhanVien from nv in nhanVien.DefaultIfEmpty()
                                     where ltToaNha.Contains(pt.MaTN) & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                   //  orderby hd.NgayTT.Value.Year, hd.NgayTT.Value.Month
                                     select new
                                     {
                                         lmb.TenLMB,
                                         tn.TenTN,
                                         kn.TenKN,
                                         tl.TenTL,
                                         mb.MaSoMB,
                                         kh.KyHieu,
                                         kh.MaPhu,
                                         TenKH = kh.IsCaNhan == true ? (kh.TenKH) : kh.CtyTen,
                                         TenLDV = l.TenHienThi,
                                         pt.SoPT,
                                         pt.NgayThu,
                                         NgayTT = hd.NgayTT.Value.Month.ToString() + "." + hd.NgayTT.Value.Year.ToString(),
                                         ct.SoTien,
                                      
                                         ct.DienGiai,
                                         HoTenNV = nv!= null ? nv.HoTenNV: "",
                                         TenHT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
                                         TenLX = lx!= null? lx.TenLX : "",
                                         NgayPhatSinh = (hd.MaLDV == 8 || hd.MaLDV == 9) 
                                         ? (hd.NgayTT.GetValueOrDefault().AddMonths(-1).Month.ToString() + "." + hd.NgayTT.GetValueOrDefault().AddMonths(-1).Year.ToString())
                                         : (hd.NgayTT.Value.Month.ToString() + "." + hd.NgayTT.Value.Year.ToString())
                                     })
                                     .ToList();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        void Print()
        {
            var _MaTN = Common.User.MaTN.Value;
            var rpt = new rptPhieuThu(_MaTN);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;            

            pvData.OptionsView.ShowColumnHeaders = false;
            pvData.OptionsView.ShowDataHeaders = false;
            pvData.OptionsView.ShowFilterHeaders = false;
            pvData.SavePivotGridToStream(stream);
            pvData.OptionsView.ShowColumnHeaders = true;
            pvData.OptionsView.ShowDataHeaders = true;
            pvData.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData( _KyBC, _TuNgay, _DenNgay, stream);
            rpt.CreateDocument();
            rpt.PrintingSystem.Document.AutoFitToPagesWidth = 1;       
            rpt.ShowPreviewDialog();
        }

        private void frmPhieuThu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            itemKyBaoCao.EditValue = objKBC.Source[0];
            SetDate(0);

            LoadData();

            pvData.OptionsView.ShowRowTotals = !DK;
            DK = pvData.OptionsView.ShowRowTotals;
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void pvData_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");  
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
        private bool DK = true;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pvData.OptionsView.ShowRowTotals = !DK;
            DK = pvData.OptionsView.ShowRowTotals;
        }
    }
}