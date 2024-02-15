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
using DevExpress.XtraPivotGrid.Customization.ViewInfo;
using DevExpress.XtraPivotGrid;

namespace DichVu.GiuXe.Reports
{
    public partial class frmDaThuTheXe : DevExpress.XtraEditors.XtraForm
    {
        public frmDaThuTheXe()
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
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var ltToaNha = this.GetToaNha();

                var ltTheXe = from ct in db.ptChiTietPhieuThus
                              join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                              join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                              join tx in db.dvgxTheXes on new { hd.TableName, hd.LinkID } equals new { TableName = "dvgxTheXe", LinkID = (int?)tx.ID }
                              join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                              join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                              join mb in db.mbMatBangs on tx.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                              join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                              from lmb in tblLoaiMatBang.DefaultIfEmpty()
                              join tn in db.tnToaNhas on tx.MaTN equals tn.MaTN
                              where ltToaNha.Contains(tx.MaTN) & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu)>=0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay)>=0
                              select new
                              {
                                  tx.SoThe,
                                  tx.NgayDK,
                                  tx.ChuThe,
                                  tx.BienSo,
                                  tx.DoiXe,
                                  tx.MauXe,
                                  SoLuong = 1,
                                  DonGia = tx.GiaThang,
                                  lx.TenLX,
                                  kh.KyHieu,
                                  TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                  mb.MaSoMB,
                                  tl.TenTL,
                                  kn.TenKN,
                                  lmb.TenLMB,
                                  tn.TenTN,
                                  hd.NgayTT,
                                  ct.SoTien
                              };

                pvTheXe.DataSource = ltTheXe;
            }
            catch { }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        void Print()
        {
            var rpt = new rptDaThuTheXe(Common.User.MaTN.Value);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvTheXe.OptionsView.ShowColumnHeaders = false;
            pvTheXe.OptionsView.ShowDataHeaders = false;
            pvTheXe.OptionsView.ShowFilterHeaders = false;
            pvTheXe.SavePivotGridToStream(stream);
            pvTheXe.OptionsView.ShowColumnHeaders = true;
            pvTheXe.OptionsView.ShowDataHeaders = true;
            pvTheXe.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmDaThuTheXe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = DateTime.Now.Month + 8;
            itemKyBaoCao.EditValue = objKBC.Source[index];
            SetDate(index);

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(pvTheXe);
        }

        private void pvTheXe_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (!e.IsColumn)
            {
                List<PivotGridField> fields;
                if (e.Field == null)
                    fields = pvTheXe.GetFieldsByArea(PivotArea.RowArea).ToList();
                else
                    fields = pvTheXe.GetFieldsByArea(PivotArea.RowArea).Where(field => field.AreaIndex > e.Field.AreaIndex).ToList();
                if (e.ValueType == PivotGridValueType.GrandTotal
                    || e.ValueType == PivotGridValueType.Total
                    || e.IsCollapsed)
                    e.DisplayText = "Tổng số xe = " + e.CreateDrillDownDataSource().Cast<PivotDrillDownDataRow>().GroupBy(row => GetUniqueCombinations(row, fields)).Count();
            }
        }

        public string GetUniqueCombinations(PivotDrillDownDataRow row, List<PivotGridField> fields)
        {
            string key = string.Empty;
            try
            {
                foreach (PivotGridField field in fields)
                {
                    key += row[field].ToString() + "|";
                }
            }
            catch { }
            return key;
        }
    }
}