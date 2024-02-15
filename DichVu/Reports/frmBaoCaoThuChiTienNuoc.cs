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
    public partial class frmBaoCaoThuChiTienNuoc : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCaoThuChiTienNuoc()
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
                pvHoaDon.DataSource = (from nc in db.dvNuocs
                                       from hd in db.dvHoaDons.Where(hd => nc.ID == hd.LinkID && hd.TableName == "dvNuoc").DefaultIfEmpty()
                                       join kh in db.tnKhachHangs on nc.MaKH equals kh.MaKH
                                       join mb in db.mbMatBangs on nc.MaMB equals mb.MaMB
                                       join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                       join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                       join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                       where
                                            kn.MaTN == matn
                                            & SqlMethods.DateDiffMonth(tuNgay, nc.NgayTB) >= 0
                                            & SqlMethods.DateDiffMonth(nc.NgayTB, denNgay) >= 0
                                       select new
                                       {
                                           nc.NgayTB,
                                           nc.TuNgay,
                                           nc.DenNgay,
                                           kn.TenKN,
                                           tl.TenTL,
                                           lmb.TenLMB,
                                           mb.MaSoMB,
                                           kh.MaPhu,
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           nc.ChiSoCu,
                                           nc.ChiSoMoi,
                                           nc.SoTieuThu,
                                           PhaiThu = nc.TienTT,
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
        Boolean first = true;
        private void frmBaoCaoThuChiTienNuoc_Load(object sender, EventArgs e)
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

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
                this.LoadData();
        }
    }
}