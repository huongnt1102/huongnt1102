using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;

using DevExpress.XtraEditors;
using Library;

namespace DIPCRM.Need.Reports.ThongKeTinhTrang
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        public System.Windows.Forms.Form frm { get; set; }
        public ctlManager()
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

        private void itemCategory_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            itemNhanVien.Visibility = BarItemVisibility.Never;
            itemNhomKinhDoanh.Visibility = BarItemVisibility.Never;
            itemChiNhanh.Visibility = BarItemVisibility.Never;
            
            switch ((int?)itemLoaiBaoCao.EditValue)
            {
                case 1://Chi nhánh
                    var ctlCN = new ctlChiNhanh();
                    ctlCN.Dock = DockStyle.Fill;
                    ctlCN.tuNgay = (DateTime?)itemTuNgay.EditValue;
                    ctlCN.denNgay = (DateTime?)itemDenNgay.EditValue;
                    ctlCN.strTinhTrang = (itemTinhTrang.EditValue ?? "").ToString().Replace(" ", "");

                    this.panelControl1.Controls.Clear();
                    this.panelControl1.Controls.Add(ctlCN);
                    ctlCN.splitContainerControl1.SplitterPosition = this.Width;
                    break;
                case 2://Nhân viên
                    itemChiNhanh.Visibility = BarItemVisibility.Always;
                    itemNhanVien.Visibility = BarItemVisibility.Always;
                    var ctlNV = new ctlNhanVien();
                    ctlNV.Dock = DockStyle.Fill;
                    ctlNV.tuNgay = (DateTime?)itemTuNgay.EditValue;
                    ctlNV.denNgay = (DateTime?)itemDenNgay.EditValue;
                    ctlNV.strTinhTrang = (itemTinhTrang.EditValue ?? "").ToString().Replace(" ", "");
                    ctlNV.strNhanVien = (itemNhanVien.EditValue ?? "").ToString().Replace(" ", "");
                    ctlNV.MaChiNhanh = (short?)itemChiNhanh.EditValue;

                    this.panelControl1.Controls.Clear();
                    this.panelControl1.Controls.Add(ctlNV);
                    ctlNV.splitContainerControl1.SplitterPosition = this.Width;
                    break;
                case 3://Nhóm kinh doanh
                    itemNhomKinhDoanh.Visibility = BarItemVisibility.Always;
                    itemChiNhanh.Visibility = BarItemVisibility.Always;
                    var ctlNKD = new ctlNhomKinhDoanh();
                    //ctlCN.Width = this.Width;

                    ctlNKD.Dock = DockStyle.Fill;
                    ctlNKD.tuNgay = (DateTime?)itemTuNgay.EditValue;
                    ctlNKD.denNgay = (DateTime?)itemDenNgay.EditValue;
                    ctlNKD.strTinhTrang = (itemTinhTrang.EditValue ?? "").ToString().Replace(" ", "");
                    ctlNKD.strNhomKinhDoanh = (itemNhomKinhDoanh.EditValue ?? "").ToString().Replace(" ", "");
                    ctlNKD.MaChiNhanh = (short?)itemChiNhanh.EditValue;

                    this.panelControl1.Controls.Clear();
                    this.panelControl1.Controls.Add(ctlNKD);
                    ctlNKD.splitContainerControl1.SplitterPosition = this.Width;
                    break;
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
                var listLoaiBaoCao = new List<LoaiBaoCao>()
                {
                    new LoaiBaoCao(){Id = 1,Name = "Chi nhánh"},
                    new LoaiBaoCao(){Id = 2,Name = "Nhân viên"},
                    new LoaiBaoCao(){Id = 3,Name = "Nhóm kinh doanh"}
                };
                lkLoaiBaoCao.DataSource = listLoaiBaoCao;

                cmbTinhTrang.DataSource = db.ncTrangThais.Select(p => new {Id=p.MaTT,Name=p.TenTT}).ToList();

                lkChiNhanh.DataSource = db.tnToaNhas.Select(p => new {Id=p.MaTN,Name=p.TenTN});

                //cmbNhomKinhDoanh.DataSource = db.NhomKinhDoanhs.Select(p => new { Id = p.MaNKD, Name = p.TenNKD }).ToList();
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
            LoadDanhMuc();
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
            itemLoaiBaoCao.EditValue = 1;
        }
        private class LoaiBaoCao
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private void itemLoaiBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            itemNhanVien.Visibility = BarItemVisibility.Never;
            itemNhomKinhDoanh.Visibility = BarItemVisibility.Never;
            itemChiNhanh.Visibility = BarItemVisibility.Never;
            this.panelControl1.Controls.Clear();

            switch ((int?)itemLoaiBaoCao.EditValue)
            {
                case 2://Nhân viên
                    itemChiNhanh.Visibility = BarItemVisibility.Always;
                    itemNhanVien.Visibility = BarItemVisibility.Always;
                    break;
                case 3://Nhóm kinh doanh
                    itemNhomKinhDoanh.Visibility = BarItemVisibility.Always;
                    break;
            }
            LoadData();
        }

        private void itemTinhTrang_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemChiNhanh_EditValueChanged(object sender, EventArgs e)
        {
            //using (var db = new MasterDataContext())
            //{
            //    cmbNhanVien.DataSource = db.tnNhanViens.Where(p=>p.MaCT==(short?)itemChiNhanh.EditValue).Select(p => new { Id = p.MaNV, Name = p.HoTen }).ToList(); 
            //}
        }
    }
}
