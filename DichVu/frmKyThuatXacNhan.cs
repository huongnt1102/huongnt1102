﻿using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using DevExpress.XtraGrid;
using System.Data.Linq.SqlClient;
using Building.AppVime;
using Building.AppVime.Class;
using Newtonsoft.Json;
using Library.Class.Connect;
using static Library.Properties.Settings;
using static Building.AppExtension.Model.ExtensionModel;
using System;

namespace DichVu
{
    public partial class frmKyThuatXacNhan : XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? IDToaNha { get; set; }
        public int? ID { get; set; }
        DataTable dt1 = null;

        void LoadDetail()
        {
            var obj = db.tnSuaChuaVatTu_ChiTiets.Where(p => p.IDSuaChua == ID).ToList();
            if (obj.Count > 0)
            {
                foreach (var item in obj)
                {
                    gvChiTiet.AddNewRow();
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "ID", item.ID);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "VatPham", item.VatPham);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "SoLuong", item.SoLuongThucTe);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "DonGia", item.DonGia);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "ThanhTien", item.ThanhTien);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "TyleVAT", item.TyleVAT);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "TienVAT", item.TienVAT);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "TienCK", item.TienCK);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "TyLeCK", item.TyLeCK);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "TongTien", item.TongTien);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "KTGhiChu", item.KTGhiChu);
                    gvChiTiet.SetRowCellValue(GridControl.NewItemRowHandle, "STT", item.STT);
                    gvChiTiet.FocusedRowHandle = -1;
                }
            }
        }
        private void initTable()
        {
            dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("ID", typeof(int)));
            dt1.Columns.Add(new DataColumn("VatPham", typeof(string)));
            dt1.Columns.Add(new DataColumn("SoLuong", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("DonGia", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("ThanhTien", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("TyleVAT", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("TienVAT", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("TienCK", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("TyLeCK", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("TongTien", typeof(decimal)));
            dt1.Columns.Add(new DataColumn("KTGhiChu", typeof(string)));
            dt1.Columns.Add(new DataColumn("STT", typeof(string)));
            gcChiTiet.DataSource = dt1;
        }
        public frmKyThuatXacNhan()
        {
            InitializeComponent();
            initTable();
        }

        private void frmKyThuatXacNhan_Load(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int a = 0; a < gvChiTiet.RowCount; a++)
            {
                try
                {
                    if (gvChiTiet.GetRowCellValue(a, "TongTien").ToString() == "")
                    {
                        DialogBox.Error("Chưa điền đầy đủ thông tin, vui lòng kiểm tra lại");
                        return;
                    }
                }
                catch
                { }
            }
            try
            {
                for (int i = 0; i < gvChiTiet.RowCount - 1; i++)
                {
                    var IDC = (int?)gvChiTiet.GetRowCellValue(i, "ID");
                    var SL = (decimal)gvChiTiet.GetRowCellValue(i, "SoLuong");
                    var TT = (decimal)gvChiTiet.GetRowCellValue(i, "ThanhTien");
                    var TLVAT = (decimal)gvChiTiet.GetRowCellValue(i, "TyleVAT");
                    var VAT = (decimal)gvChiTiet.GetRowCellValue(i, "TienVAT");
                    var TLCK = (decimal)gvChiTiet.GetRowCellValue(i, "TyLeCK");
                    var CK = (decimal)gvChiTiet.GetRowCellValue(i, "TienCK");
                    var TongT = (decimal)gvChiTiet.GetRowCellValue(i, "TongTien");
                    var GhiChu = gvChiTiet.GetRowCellValue(i, "KTGhiChu");
                    if (IDC != null)
                    {
                        var ct = db.tnSuaChuaVatTu_ChiTiets.SingleOrDefault(p => p.ID == IDC);
                        if (ct != null)
                        {
                            ct.SoLuongThucTe = SL;
                            ct.ThanhTien = TT;
                            ct.TyleVAT = TLVAT;
                            ct.TienVAT = VAT;
                            ct.TyLeCK = TLCK;
                            ct.TienCK = CK;
                            ct.TongTien = TongT;
                            ct.KTGhiChu = GhiChu.ToString();
                            db.SubmitChanges();
                        }
                    }
                }
                var check = db.tnSuaChuaVatTus.SingleOrDefault(p => p.ID == ID);
                if (check != null)
                {
                    var a = db.tnSuaChuaVatTu_ChiTiets.Where(p => p.IDSuaChua == ID).Sum(o => o.TongTien);
                    check.PhaiTra = a > 0 ? a : 0;
                    check.KTTraLoi = "Sửa chữa hoàn tất!";
                    check.KTDuyet = true;
                    check.NVKT = Common.User.MaNV;
                    db.SubmitChanges();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            using (var db = new MasterDataContext())
            {
                try
                {
                    CommonVime.GetConfig();
                    var toa_nha = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == IDToaNha);
                    string building_code = toa_nha.DisplayName;
                    int building_matn = toa_nha.Id;

                    tbl_building_get_id model_param = new tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_param);

                    var connectString = Default.Building_dbConnectionString;
                    //var idNew = QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", connectString, param).FirstOrDefault();
                    var idNew = 17;
                    var body = new BaoGiaSCModel();
                    body.bqlTraLoi = "Sửa chữa hoàn tất!";
                    body.nvqlTraLoi = Common.User.MaNV;
                    var post = VimeService.PostH(body, $"/YeuCauSuaChua/APIBaoGiaChoPM?idNew={idNew}&idYeuCau={ID}");
                    this.Close();
                }
                catch (Exception ex)
                {
                    DialogBox.Error("Error: " + ex);
                }
            }
        }

        void TinhThanhTien()
        {
            try
            {
                var _DonGia = (gvChiTiet.GetFocusedRowCellValue("DonGia") as decimal?) ?? 0;
                var _SL = (gvChiTiet.GetFocusedRowCellValue("SoLuong") as decimal?) ?? 0;
                var _TyLeVAT = (gvChiTiet.GetFocusedRowCellValue("TyleVAT") as decimal?) ?? 0;
                var _TienVAT = (gvChiTiet.GetFocusedRowCellValue("TienVAT") as decimal?) ?? 0;
                var _TyLeCK = (gvChiTiet.GetFocusedRowCellValue("TyLeCK") as decimal?) ?? 0;
                var _TienCK = (gvChiTiet.GetFocusedRowCellValue("TienCK") as decimal?) ?? 0;
                var _ThanhTien = (gvChiTiet.GetFocusedRowCellValue("ThanhTien") as decimal?) ?? 0;
                var _TongTien = (decimal?)0;
                if (_DonGia > 0)
                {
                    _ThanhTien = _DonGia * _SL > 0 ? _DonGia * _SL : 0;

                    if (_TyLeVAT > 0)
                        _TienVAT = (_TyLeVAT / 100) * _ThanhTien;
                    else
                        _TyLeVAT = _TienVAT / _ThanhTien * 100;

                    if (_TyLeCK > 0)
                        _TienCK = (_TyLeCK / 100) * (_ThanhTien + _TienVAT);
                    else
                        _TyLeCK = _TienCK / (_ThanhTien + _TienVAT) * 100;

                    _TongTien = _ThanhTien + _TienVAT - _TienCK;

                    gvChiTiet.SetFocusedRowCellValue("DonGia", _DonGia);
                    gvChiTiet.SetFocusedRowCellValue("ThanhTien", _ThanhTien);
                    gvChiTiet.SetFocusedRowCellValue("TienVAT", _TienVAT);
                    gvChiTiet.SetFocusedRowCellValue("TyleVAT", _TyLeVAT);
                    gvChiTiet.SetFocusedRowCellValue("TyLeCK", _TyLeCK);
                    gvChiTiet.SetFocusedRowCellValue("TienCK", _TienCK);
                    gvChiTiet.SetFocusedRowCellValue("TongTien", _TongTien);
                }
                else
                {
                    gvChiTiet.SetFocusedRowCellValue("DonGia", (decimal)0);
                    gvChiTiet.SetFocusedRowCellValue("ThanhTien", (decimal)0);
                    gvChiTiet.SetFocusedRowCellValue("TienVAT", (decimal)0);
                    gvChiTiet.SetFocusedRowCellValue("TyleVAT", (decimal)0);
                    gvChiTiet.SetFocusedRowCellValue("TyLeCK", (decimal)0);
                    gvChiTiet.SetFocusedRowCellValue("TienCK", (decimal)0);
                    gvChiTiet.SetFocusedRowCellValue("TongTien", (decimal)0);
                }


            }
            catch { }
        }

        private void spinSL_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("SoLuong", ((SpinEdit)sender).Value);
            TinhThanhTien();
        }

        private void txtGhiChu_EditValueChanged(object sender, EventArgs e)
        {
            gvChiTiet.SetFocusedRowCellValue("KTGhiChu", ((TextEdit)sender).Text);
        }
    }
}