using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.Need.Reports
{
    public partial class ctlBaoCaoDoanhThuDuKien : DevExpress.XtraEditors.XtraUserControl
    {
        public System.Windows.Forms.Form frm { get; set; }
        public ctlBaoCaoDoanhThuDuKien()
        {
            InitializeComponent();
            
        }

        private void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
            using (var db = new MasterDataContext())
            {
                //string strNhanVien = (itemNhanVien.EditValue ?? "").ToString().Replace(" ", "");
                //var arrNhanVien = strNhanVien.Split(',');
                //var chiNhanh = (short?)itemChiNhanh.EditValue;
                //var tuNgay = (DateTime?)itemTuNgay.EditValue;
                //var denNgay = (DateTime?)itemDenNgay.EditValue;

                //var list = (from p in db.ncNhuCaus
                //            join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH into kh_p
                //            from kh in kh_p.DefaultIfEmpty()
                //            join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV into nv_p
                //            from nv in nv_p.DefaultIfEmpty()
                //            where 
                //            (SqlMethods.DateDiffDay(tuNgay, p.NgayDuKien_MuaHang) >= 0 & SqlMethods.DateDiffDay(p.NgayDuKien_MuaHang, denNgay) >= 0) &
                //            nv.MaCT == chiNhanh &
                //            (arrNhanVien.Contains(nv.MaNV.ToString()) == true | strNhanVien == "")
                //            select new
                //            {
                //                CoHoi = p.SoNC + " - Nhu cầu ( " + p.DienGiai + " ) - Khách hàng( " + kh.TenCongTy + " )",
                //                SoTien = (from sp in db.ncSanPhams
                //                          where sp.MaNC==p.MaNC
                //                          select new
                //                          {
                //                              ThanhTien = sp.ThanhTien??0
                //                          }).ToList().Sum(i => (decimal?)i.ThanhTien)??0,
                //                NhanVien = nv.HoTen,
                //                NgayDuKien=p.NgayDuKien_MuaHang
                //            }).AsEnumerable()
                //            .Select((p, index) => new
                //            {
                //                STT = index + 1,
                //                p.CoHoi,
                //                p.SoTien,
                //                p.NhanVien,
                //                p.NgayDuKien

                //            }).ToList();
                //gc.DataSource = list;
            }
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void LoadDanhMuc()
        {
            using (var db = new MasterDataContext())
            {
                cmbTinhTrang.DataSource = db.ncTrangThais.Select(p => new { Id = p.MaTT, Name = p.TenTT }).ToList();
                //lkChiNhanh.DataSource = db.CongTies.Select(p => new { Id = p.ID, Name = p.TenCT });
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            LoadDanhMuc();
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(6);
            LoadData();
        }

        private void itemChiNhanh_EditValueChanged(object sender, EventArgs e)
        {
            //using (var db = new MasterDataContext())
            //{
            //    cmbNhanVien.DataSource = db.tnNhanViens.Where(p => p.MaCT == (short?)itemChiNhanh.EditValue).Select(p => new { Id = p.MaNV, Name = p.HoTen }).ToList();
            //}
            LoadData();
        }
    }
}
