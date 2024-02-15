using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using System.Linq;
using Library;

namespace LandSoftBuilding.Receivables
{
    public partial class ctlCongNoTheoKy : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public ctlCongNoTheoKy()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }
        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var matn = (byte?)itemToaNha.EditValue;

                if (itemTuNgay.EditValue == null) return;
                if (itemDenNgay.EditValue == null) return;

                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                //Số dư đầu kỳ

                var query = (from hd in db.dvHoaDons
                             join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                             join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                             where
                             SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) > 0 & hd.MaTN == matn & hd.IsDuyet == true
                             group hd by new { hd.MaLDV, ldv.TenLDV, tn.TenTN } into gr
                             select new
                             {
                                 gr.Key.TenTN,
                                 gr.Key.MaLDV,
                                 gr.Key.TenLDV,
                                 ConNo = gr.Sum(p => p.PhaiThu - (from ct in db.ptChiTietPhieuThus
                                                                  join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                                  where ct.TableName == "dvHoaDon" & ct.LinkID == p.ID & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) > 0
                                                                  select ct.SoTien).Sum().GetValueOrDefault()),

                             })
                                        .ToList();
                gcData.DataSource = query;
            }
            catch (Exception)
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
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

        private void ctlCongNoTheoKy_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;


            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = DateTime.Now.Month + 8;
            itemKyBaoCao.EditValue = objKBC.Source[index];
            SetDate(index);
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

        private void grvData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }


        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
      
    }
}
