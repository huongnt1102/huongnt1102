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

namespace DichVu.Dien.Report
{
    public partial class frmBaoCaoThuChiTienDien : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCaoThuChiTienDien()
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
        Boolean first = true;
        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            try
            {
                pvHoaDon.DataSource = (from dn in db.dvDiens
                                       from hd in db.dvHoaDons.Where(hd => hd.TableName == "dvDien" & hd.LinkID == dn.ID).DefaultIfEmpty()
                                       join kh in db.tnKhachHangs on dn.MaKH equals kh.MaKH
                                       join mb in db.mbMatBangs on dn.MaMB equals mb.MaMB 
                                           join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL 
                                           join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                       where 
                                            kn.MaTN == matn
                                            & SqlMethods.DateDiffDay(tuNgay, dn.NgayTB) >= 0
                                            & SqlMethods.DateDiffDay(dn.NgayTB, denNgay) >= 0
                                       select new
                                       {
                                           kn.TenKN,
                                           tl.TenTL,
                                           mb.MaSoMB,
                                           //
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           //TenLDV = l.TenHienThi,
                                           //
                                           dn.ChiSoCu,
                                           dn.ChiSoMoi,
                                           dn.SoTieuThu,
                                           DonGia = dn.SoTieuThu > 0 ? Decimal.Round((decimal)(dn.ThanhTien / dn.SoTieuThu), 0) : 0,
                                           //
                                           dn.NgayTB,
                                           //
                                           PhaiThu = dn.TienTT, //hd.PhaiThu,
                                           DaThu = hd.DaThu,
                                           ConNo = hd.ConNo
                                           
                                       }).ToList();
                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void frmBaoCaoThuChiTienDien_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
            first = false;
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(pvHoaDon);
        }

        private void pvHoaDon_CustomCellDisplayText(object sender, DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs e)
        {
            if (e.DataField.Name == pivotGridField2_DauKy.Name || e.DataField.Name == pivotGridField6_CuoiKy.Name || e.DataField.Name == pivotGridField14_DonGia.Name)
            {
                if (e.ColumnValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value || e.RowValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value)
                {
                    e.DisplayText = "";
                }
            }
        }
        
        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
                this.LoadData();
        }
    }
}