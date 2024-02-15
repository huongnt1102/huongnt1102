using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using LandSoftBuilding.Receivables.Reports;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace LandSoftBuilding.Receivables.DuTinh
{
    public partial class frmDuTinhDoanhThuXe : DevExpress.XtraEditors.XtraForm
    {
        public frmDuTinhDoanhThuXe()
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
        public class TKCongNoCls
        {
            public string KyBC { set; get; }
            public byte? MaTN { set; get; }
            public int? MaKH { set; get; }
            public int? MaMB { set; get; }
            public int? MaLDV { set; get; }
            public decimal? SoTien { set; get; }
            public decimal? SoTienDuTinh{ set; get; }
        }
        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                db.CommandTimeout = 100000;
                var ltToaNha = this.GetToaNha();
                var thang = Convert.ToUInt32(itemThang.EditValue);
                var nam = Convert.ToUInt32(itemNam.EditValue);
                if (thang <= 0 || thang >= 13)
                {
                    DialogBox.Error("Tháng không hợp lệ");
                    return;
                }
                if (nam <= 0)
                {
                    DialogBox.Error("Năm không hợp lệ");
                    return;
                }

                #region code mới
                var objHienTai = (from hd in db.dvHoaDons
                               where ltToaNha.Contains(hd.MaTN)
                               & hd.NgayTT.Value.Month == thang
                               & hd.NgayTT.Value.Year == nam
                               & hd.MaLDV == 6
                               & hd.IsDuyet == true  //& hd.MaKH == 3099
                               group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV, hd.ID } into gr
                               select new
                               {
                                   gr.Key.MaTN,
                                   gr.Key.MaKH,
                                   gr.Key.MaMB,
                                   gr.Key.MaLDV,
                                   SoTien = gr.Sum(p => p.PhaiThu)
                               }).Select(p => new TKCongNoCls
                               {
                                   MaTN = p.MaTN,
                                   MaKH = p.MaKH,
                                   MaMB = p.MaMB,
                                   MaLDV = p.MaLDV,
                                   SoTien = p.SoTien
                               }).ToList();

                var objDuTinh = (from hd in db.dvHoaDons
                                 join tx in db.dvgxTheXes on new { TableName = hd.TableName, LinkID = hd.LinkID } equals new { TableName = "dvgxTheXe", LinkID = (int?)tx.ID }
                                 where ltToaNha.Contains(hd.MaTN)
                                  & !tx.NgungSuDung.GetValueOrDefault()
                                  & hd.NgayTT.Value.Month == thang
                                  & hd.NgayTT.Value.Year == nam
                                  & hd.MaLDV == 6
                                  & hd.IsDuyet == true  //& hd.MaKH == 3099
                                  group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV, hd.ID } into gr
                                  select new
                                  {
                                      gr.Key.MaTN,
                                      gr.Key.MaKH,
                                      gr.Key.MaMB,
                                      gr.Key.MaLDV,
                                      SoTien = gr.Sum(p => p.PhaiThu)
                                  }).Select(p => new TKCongNoCls
                                  {
                                      MaTN = p.MaTN,
                                      MaKH = p.MaKH,
                                      MaMB = p.MaMB,
                                      MaLDV = p.MaLDV,
                                      SoTienDuTinh = p.SoTien
                                  }).ToList();
                var data = objHienTai.Concat(objDuTinh).ToList();
                var objKH = (from kh in db.tnKhachHangs
                             where ltToaNha.Contains(kh.MaTN)
                             select new
                             {
                                 kh.MaKH,
                                 kh.KyHieu,
                                 kh.MaPhu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                             }).ToList();
                var objMB = (from mb in db.mbMatBangs
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                             from tl in tblTangLau.DefaultIfEmpty()
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                             from kn in tblKhoiNha.DefaultIfEmpty()
                             join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                             join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                             from lmb in tblLoaiMatBang.DefaultIfEmpty()
                             where ltToaNha.Contains(mb.MaTN)
                             select new
                             {
                                 mb.MaMB,
                                 lmb.TenLMB,
                                 tn.TenTN,
                                 kn.TenKN,
                                 tl.TenTL,
                                 mb.MaSoMB,
                             }).ToList();
                //Nap vào pivot
                var listTong = (from kh in objKH
                                join hd in data on kh.MaKH equals hd.MaKH
                                join mb in objMB on hd.MaMB equals mb.MaMB
                                select new
                                {
                                    hd.KyBC,
                                    mb.TenLMB,
                                    mb.TenTN,
                                    mb.TenKN,
                                    mb.TenTL,
                                    mb.MaSoMB,
                                    kh.KyHieu,
                                    kh.MaPhu,
                                    TenKH = kh.TenKH,
                                    SoTien = hd.SoTien,
                                    SoTienDuTinh = hd.SoTienDuTinh
                                }).ToList();
                pvHoaDon.DataSource = listTong;
                #endregion

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
            var rpt = new rptCongNo(Common.User.MaTN.Value);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvHoaDon.OptionsView.ShowColumnHeaders = false;
            pvHoaDon.OptionsView.ShowDataHeaders = false;
            pvHoaDon.OptionsView.ShowFilterHeaders = false;
            pvHoaDon.SavePivotGridToStream(stream);
            pvHoaDon.OptionsView.ShowColumnHeaders = true;
            pvHoaDon.OptionsView.ShowDataHeaders = true;
            pvHoaDon.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmCongNo_Load(object sender, EventArgs e)
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
            var date = DateTime.Now;
            itemThang.EditValue = date.Month;
            itemNam.EditValue = date.Year;
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
            this.Print();
        }

        private void pvHoaDon_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
    }
}