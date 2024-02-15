using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DIPCRM.DataEntity;
using DIPCRM.Library;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace DIP.SwitchBoard
{
    public partial class frmChungTu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public int? LinID { get; set; }
        public int? LinkType { get; set; }
        public int? MaKH { get; set; }
        public int? MaNVQL { get; set; }
        public string LinkName { get; set; }
        public int? MaTT { get; set; }

        public frmChungTu()
        {
            InitializeComponent();
            cmbKyBaoCao.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
           
            this.Load += new EventHandler(frmChungTu_Load);
            itemGetDaTa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemGetDaTa_ItemClick);
            lookLoaiChungTu.EditValueChanged += new EventHandler(lookLoaiChungTu_EditValueChanged);
        }

        void lookLoaiChungTu_EditValueChanged(object sender, EventArgs e)
        {
            var lkLoaiChungTu = (LookUpEdit)sender;
            this.LinkName = lkLoaiChungTu.GetColumnValue("TenLCT") as string;
        }

        void ChungTu()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue;
            var denNgay = (DateTime?)itemDenNgay.EditValue;
            this.LinkType = (int?)itemChungTu.EditValue;
            switch (this.LinkType)
            {
                case 74://Nhu cau
                    gcChungTu.DataSource = (from nc in db.ncNhuCaus
                                            join kh in db.KhachHangs on nc.MaKH equals kh.MaKH
                                            join nv in db.NhanViens on nc.MaNVQL equals nv.MaNV
                                            where SqlMethods.DateDiffDay(tuNgay, nc.NgayNhap) >= 0 & SqlMethods.DateDiffDay(nc.NgayNhap, denNgay) >= 0
                                            & nc.MaKH == this.MaKH
                                            select new { ID = nc.MaNC, NgayCT = nc.NgayNhap, SoCT = nc.SoNC, nc.MaNVQL, NhanVien = nv.HoTen, DienGiai = nc.DienGiai, MaTT = (int?)nc.MaTT }).ToList();
                    break;
                case 88://Hop dong
                    gcChungTu.DataSource = (from bg in db.cContracts
                                            join kh in db.KhachHangs on bg.CusID equals kh.MaKH
                                            join nv in db.NhanViens on bg.SaleID equals nv.MaNV
                                            where SqlMethods.DateDiffDay(tuNgay, bg.SigningDate) >= 0 & SqlMethods.DateDiffDay(bg.SigningDate, denNgay) >= 0
                                            & bg.CusID == this.MaKH
                                            select new { ID = bg.ContractID, SoCT = bg.ContractNo, NgayCT = bg.SigningDate, MaNVQL = bg.SaleID, NhanVien = nv.HoTen, DienGiai = bg.Description, MaTT = bg.StatusID }).ToList();
                    break;
            }
        }

        void itemGetDaTa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChungTu();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex);
        }

        void frmChungTu_Load(object sender, EventArgs e)
        {
            lookLoaiChungTu.DataSource = db.Forms.Where(p => p.FormID == 74 | p.FormID == 88).Select(p => new { ID = p.FormID, TenLCT = p.FormName }).ToList();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();
            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ItemLuu_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(gvChungTu.GetFocusedRowCellValue("ID") ?? 0);
            if (id == 0)
            {
                DIPCRM.DialogBox.Error("Vui lòng chọn chứng từ");
                return;
            }

            this.LinID = id;
            this.MaNVQL = (int?)gvChungTu.GetFocusedRowCellValue("MaNVQL");
            this.MaTT = (int?)gvChungTu.GetFocusedRowCellValue("MaTT");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            ChungTu();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            ChungTu();
        }

        private void btnCoHoiAdd_Click(object sender, EventArgs e)
        {
            var frm = new DIPCRM.NhuCau.frmEdit();
            frm.MaKH = this.MaKH;
            frm.ShowDialog();
            if (frm.IsSave)
                ChungTu();
        }

        private void btnHopDongAdd_Click(object sender, EventArgs e)
        {
            var frm = new DIPCRM.Contracts.frmEdit();
            frm.MaKH = this.MaKH;
            frm.ShowDialog();
            if (frm.IsSave)
                ChungTu();
        }
    }
}