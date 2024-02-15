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

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class frmBaoCaoTuoiNo2_Layout2 : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCaoTuoiNo2_Layout2()
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
                //var ltToaNha = this.GetToaNha();
                //var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                if (itemToaNha.EditValue == null) return;
                //if (itemLoaiDichVu.EditValue == null) return;
                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                //var strLoaiDichVu = (itemLoaiDichVu.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var model = new
                {
                    tn_arr = strToaNha,
                    DateTo = _DenNgay
                };
                //var param = new DynamicParameters();
                //param.AddDynamicParams(model);
                //var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.tbl_phieuvanhanh_tong_checklist", param).ToList();
                //gridControl1.DataSource = await Task.Run(() => Library.Class.Connect.QueryConnect.Query<cn_bao_cao_tuoi_no>("dbo.cn_bao_cao_tuoi_no", param));

                pvData.DataSource = Library.Class.Connect.QueryConnect.QueryData<cn_bao_cao_tuoi_no>("dbo.cn_bao_cao_tuoi_no2_Layout2", model);

                //Nap vào pivot
                //pvData.DataSource = (from pt in db.ptPhieuThus
                //                     join lpt in db.ptPhanLoais on pt.MaPL equals lpt.ID
                //                     join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                //                     join nkh in db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhom
                //                     from nkh in nhom.DefaultIfEmpty()
                //                     join tn in db.tnToaNhas on kh.MaTN equals tn.MaTN
                //                     join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV
                //                     where ltToaNha.Contains(pt.MaTN) & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                //                     orderby pt.NgayThu descending
                //                     select new
                //                     {
                //                         tn.TenTN,
                //                         kh.KyHieu,
                //                         TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                //                         nkh.TenNKH,
                //                         pt.SoPT,
                //                         pt.NgayThu,
                //                         pt.SoTien,
                //                         lpt.TenPL,
                //                         nv.HoTenNV,
                //                         TenHT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
                //                         pt.LyDo,
                //                     }).ToList();
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
            //var rpt = new rptPhieuThu();
            //var stream = new System.IO.MemoryStream();
            //var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            //var _TuNgay = (DateTime)itemTuNgay.EditValue;
            //var _DenNgay = (DateTime)itemDenNgay.EditValue;

            //pvData.OptionsView.ShowColumnHeaders = false;
            //pvData.OptionsView.ShowDataHeaders = false;
            //pvData.OptionsView.ShowFilterHeaders = false;
            //pvData.SavePivotGridToStream(stream);
            //pvData.OptionsView.ShowColumnHeaders = true;
            //pvData.OptionsView.ShowDataHeaders = true;
            //pvData.OptionsView.ShowFilterHeaders = true;

            //rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            //rpt.ShowPreviewDialog();
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
            //this.Print();
            Commoncls.ExportExcel(pvData);
        }

        private void pvData_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = "Tổng cộng";
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }

        public class cn_bao_cao_tuoi_no
        {
            public string TenTN { get; set; }
            public string TenLDV { get; set; }
            public string TenPL { get; set; }
            public string TieuDe { get; set; }
            public string STT { get; set; }
            public decimal? CongNo { get; set; }
            public int? No { get; set; }
            public string KyHieu { get; set; }
            public string TenKH { get; set; }
            public string MaSoMB { get; set; }
            public string SoNha { get; set; }
        }
    }
}