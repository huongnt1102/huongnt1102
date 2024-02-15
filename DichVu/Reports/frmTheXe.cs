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

namespace DichVu.Reports
{
    public partial class frmTheXe : DevExpress.XtraEditors.XtraForm
    {
        public frmTheXe()
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

            itenTuNgay.EditValue = objKBC.DateFrom;
            itenDenNgay.EditValue = objKBC.DateTo;
        }

        Boolean first = true;

        void LoadData()
        {
            var tuNgay = (DateTime)itenTuNgay.EditValue;
            var denNgay = (DateTime)itenDenNgay.EditValue;
            var _MaTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            try
            {
                pvData.DataSource = from tx in db.dvgxTheXes
                                      join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID
                                      join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                      join mb in db.mbMatBangs on gx.MaMB equals mb.MaMB
                                      join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                      join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                      join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                      where gx.MaTN == _MaTN && tx.NgungSuDung == false
                                         & SqlMethods.DateDiffMonth(tuNgay, tx.NgayDK) >= 0
                                         & SqlMethods.DateDiffMonth(tx.NgayDK, denNgay) >= 0
                                      select new
                                      {
                                          tx.ID,
                                          tx.NgayDK,
                                          tx.SoThe,
                                          tx.ChuThe,
                                          gx.NgayTT,
                                          tx.BienSo,
                                          tx.MauXe,
                                          tx.DoiXe,
                                          lx.TenLX,
                                          SoLuong = 1,
                                          tx.GiaThang,
                                          gx.NgungSuDung,
                                          mb.MaSoMB,
                                          tl.TenTL,
                                          kn.TenKN,
                                          kh.MaPhu,
                                          TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen
                                      };
                
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

        void Print()
        {
            var _MaTN = (byte)itemToaNha.EditValue;
            var rpt = new rptTheXe(_MaTN);
            var stream = new System.IO.MemoryStream();
           
            pvData.OptionsView.ShowColumnHeaders = false;
            pvData.OptionsView.ShowDataHeaders = false;
            pvData.OptionsView.ShowFilterHeaders = false;
            pvData.SavePivotGridToStream(stream);
            pvData.OptionsView.ShowColumnHeaders = true;
            pvData.OptionsView.ShowDataHeaders = true;
            pvData.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData( stream);
            rpt.CreateDocument();
            rpt.PrintingSystem.Document.AutoFitToPagesWidth = 1;       
            rpt.ShowPreviewDialog();
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
            itemKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
            first = false;
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first)
                this.LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
    }
}