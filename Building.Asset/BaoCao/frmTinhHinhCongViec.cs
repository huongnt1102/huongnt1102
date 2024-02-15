using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Linq.SqlClient;
using DevExpress.Utils;

namespace DichVu.YeuCau
{
    public partial class frmTinhHinhCongViec : XtraForm
    {
        private MasterDataContext _db;
        private DateTime tuNgay;
        private DateTime denNgay;

        public frmTinhHinhCongViec()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            cmbToaNha.DataSource = Common.TowerList;

            var objKbc = new KyBaoCao();
            _db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            LoadData();
            gv.BestFitColumns();
        }

        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                if (beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    tuNgay = (DateTime)beiTuNgay.EditValue;
                    denNgay = (DateTime) beiDenNgay.EditValue;
                    var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                    var ltToaNha = strToaNha.Split(',');

                    _db = new MasterDataContext();

                    var listToaNha = (from p in _db.tnycYeuCaus
                        where SqlMethods.DateDiffDay(tuNgay, p.NgayYC) >= 0
                              & SqlMethods.DateDiffDay(p.NgayYC, denNgay) >= 0
                              & ltToaNha.Contains(p.mbMatBang.mbTangLau.mbKhoiNha.MaTN.ToString())
                        select new
                        {
                            p.tnToaNha.TenTN,
                            MaTT = p.MaTT ?? 1,
                            NgayBatDau = p.NgayBatDauTinh ?? DateTime.Now,
                            NgayXL=p.tnycLichSuCapNhats.Where(_=>_.MaTT==3).OrderByDescending(_=>_.NgayCN).FirstOrDefault().NgayCN,
                            NgayHanCuoiHoanThanh=p.NgayHanCuoiHoanThanh??DateTime.Now,
                            KtTre = SqlMethods.DateDiffHour(p.NgayHanCuoiHoanThanh ?? DateTime.Now, p.tnycLichSuCapNhats.Where(_ => _.MaTT == 3).OrderByDescending(_ => _.NgayCN).FirstOrDefault().NgayCN??DateTime.Now) > 0 ? 1 : 0,p.MaTN,
                        }).ToList();

                    gc.DataSource = (from p in listToaNha
                        group new {p} by new {p.TenTN,p.MaTN}
                        into g
                        select new
                        {
                            g.Key.TenTN, YeuCauMoi = g.Count(_ => _.p.MaTT == 1),
                            DangXuLy = g.Count(_ => _.p.MaTT == 2), DaHoanThanh = g.Count(_ => _.p.MaTT == 3),
                            DaDanhGia = g.Count(_ => _.p.MaTT == 5), Tong = g.Count(),
                            Tre=g.Sum(_=>_.p.KtTre),g.Key.MaTN,
                            YeuCauMoiMaTT=1,DangXuLyMaTT=2,DaHoanThanhMaTT=3,DaDanhGiaMaTT=5,TongMaTT=99,TreMaTT=98
                        }).ToList();
                }
            }
            catch
            {
                // ignored
            }
            gv.BestFitColumns();
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

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
            gv.BestFitColumns();
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            var ea = e as DXMouseEventArgs;
            var view = sender as GridView;
            var info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                if (info.Column.FieldName == "TenTN") return;
                var maTt = gv.GetRowCellValue(info.RowHandle, info.Column.FieldName + "MaTT").ToString();
                var maTn = gv.GetFocusedRowCellValue("MaTN");
                using (var frm = new frmTinhHinhCongViec_ViewItem
                    {MaTn = (byte?) maTn, TuNgay = tuNgay, DenNgay = denNgay, MaTt = int.Parse(maTt)})
                {
                    frm.ShowDialog();
                }
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }
    }
}