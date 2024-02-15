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

namespace DichVu
{
    public partial class frmBaoCaoThuChiTienDieuHoaNG : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCaoThuChiTienDieuHoaNG()
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
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            try
            {


                pvHoaDon.DataSource = (from dn in db.dvDienDieuHoas
                                       from hd in db.dvHoaDons.Where(hd => hd.TableName == "dvDienDieuHoa" & dn.ID == hd.LinkID).DefaultIfEmpty()
                                       join kh in db.tnKhachHangs on dn.MaKH equals kh.MaKH
                                       join mb in db.mbMatBangs on dn.MaMB equals mb.MaMB
                                       join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                       join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                       join lt in db.LoaiTiens on dn.MaLT equals lt.ID

                                       where //hd.MaLDV: 15 (Điều hòa ngoài giờ)
                                            kn.MaTN == matn
                                            & SqlMethods.DateDiffDay(tuNgay, dn.NgayNhap) >= 0
                                            & SqlMethods.DateDiffDay(dn.NgayNhap, denNgay) >= 0
                                       select new
                                       {
                                           kn.TenKN,
                                           tl.TenTL,
                                           mb.MaSoMB,
                                           //
                                           kh.MaPhu,
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           //TenLDV = l.TenHienThi,
                                           //
                                           dn.SoGio,
                                           dn.SoFCU,
                                           dn.DonGia,
                                           dn.ThanhTien,
                                           lt.KyHieuLT,
                                           //
                                           nam = dn.NgayNhap.Value.Year, //hd.NgayTT.Value.Year,
                                           thang = dn.NgayNhap.Value.Month, //hd.NgayTT.Value.Month,
                                           ngay = dn.NgayNhap.Value.Day, //hd.NgayTT.Value.Day,
                                           //
                                           PhaiThu = dn.ThanhTienQD, //hd.PhaiThu,
                                           DaThu = hd.DaThu,
                                           ConNo = hd.ConNo

                                       }).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
        Boolean first = true;
        private void frmBaoCaoThuChiTienDieuHoaNG_Load(object sender, EventArgs e)
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
            if (e.DataField.Name == pivotGridField14_DonGia.Name)
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