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

namespace Building.Asset.VatTu
{
    public partial class frmTonKho_Manager : XtraForm
    {
        private MasterDataContext _db;

        public frmTonKho_Manager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lueToaNha.DataSource = Common.TowerList;
            beiToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            _db = new MasterDataContext();
            foreach (var v in objKbc.Source)
            {
                cbxKBC.Items.Add(v);
            }
            beiKBC.EditValue = objKbc.Source[7];
            SetDate(7);

            lkNhanVien.DataSource = _db.tnNhanViens.Select(o => new { o.MaNV, o.HoTenNV }).ToList();

            LoadData();
            gv.BestFitColumns();
        }

        private void LoadData()
        {
            try
            {
                gc.DataSource = null;
                if (beiToaNha.EditValue != null && beiTuNgay.EditValue != null && beiDenNgay.EditValue != null)
                {
                    _db = new MasterDataContext();

                    glkKho.DataSource = _db.tbl_VatTu_Khos;//.Where(_=>_.MaTN==(byte)beiToaNha.EditValue);

                    gc.DataSource = (from p in _db.tbl_VatTu_SoKhos
                        join vt in _db.tbl_VatTus on p.VatTuID equals vt.ID
                        where p.MaTN == (byte) beiToaNha.EditValue
                        group new {p} by new {vt.ID, vt.KyHieu, vt.Ten, vt.tbl_VatTu_DVT.TenDVT, p.KhoID}
                        into g
                        select new
                        {
                            g.Key.ID, g.Key.KyHieu, g.Key.Ten, g.Key.TenDVT, g.Key.KhoID,
                            SoLuong = g.Sum(_ => _.p.SoLuong).GetValueOrDefault(),
                            ThanhTien = g.Sum(_ => _.p.ThanhTien).GetValueOrDefault()
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

        private void beiToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
            gv.BestFitColumns();
        }
    }
}