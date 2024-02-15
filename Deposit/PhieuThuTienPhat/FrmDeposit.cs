using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Deposit.PhieuThuTienPhat
{
    public partial class FrmDeposit : XtraForm
    {
        public int? HopDongDatCocId { get; set; }
        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();

        public FrmDeposit()
        {
            InitializeComponent();
        }

        private void FrmDeposit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            this.Text = Deposit.Class.Enum.FormName.PHIEU_THU_TIEN_PHAT;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);

            LoadData();
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao() {Index = index};
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var tuNgay = (DateTime) itemTuNgay.EditValue;
                var denNgay = (DateTime) itemDenNgay.EditValue;
                var maTn = (byte) itemToaNha.EditValue;

                switch (HopDongDatCocId)
                {
                    case null:
                        gc.DataSource = Deposit.Class.PhieuThu.GetPhieuThuTienPhat(maTn, tuNgay, denNgay);
                        break;
                    default:
                        gc.DataSource = Deposit.Class.PhieuThu.GetPhieuThuTienPhatByHopDong(HopDongDatCocId);
                        break;
                }
            }
            catch { }
        }

        private void LoadDetail()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gv.GetFocusedRowCellValue("ID");
                if(id ==null)
                {
                    gcChiTiet.DataSource = null; return;
                }
                gcChiTiet.DataSource = db.ptChiTietPhieuThus.Where(_ => _.MaPT == id).Select(_ => new { _.DienGiai, _.SoTien, _.KhauTru, _.ThuThua }).ToList();
            }
            catch { }
            finally { db.Dispose(); }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn phiếu");
                return;
            }

            using (var frm = new FrmDepositEdit())
            {
                frm.MaPt = id;
                frm.MaTn = (byte)itemToaNha.EditValue;
                frm.IsDepositFather = (bool?)gv.GetFocusedRowCellValue("IsDepositFather");
                frm.DepositFatherId = (int?) gv.GetFocusedRowCellValue("DepositFatherId");
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gv.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn phiếu thu");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            var db = new MasterDataContext();
            foreach (var i in indexs)
            {
                var pt = db.ptPhieuThus.FirstOrDefault(_ => _.ID == (int) gv.GetRowCellValue(i, "ID"));
                if (pt != null)
                {
                    if (pt.MaPCT != null)
                    {
                        DialogBox.Alert("Đây là phiếu thu bên chuyển tiền. Vui lòng qua nghiệp vụ chuyển tiền để xóa");
                        return;
                    }
                    #region Lưu lại phiếu thu đã xóa

                    db.ptPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(pt.LyDo, pt.MaKH, pt.MaNV, Library.Common.User.MaNV, pt.MaPL, pt.MaTKNH, pt.MaTN, pt.NguoiNop, System.DateTime.UtcNow.AddHours(7), pt.NgayThu, pt.SoPT, pt.SoTien, pt.ChungTuGoc, pt.DiaChiNN));

                    var queryChiTietPt = db.ptChiTietPhieuThus.Where(p => p.MaPT == pt.ID).ToList();
                    if (queryChiTietPt.Count > 0)
                    {
                        foreach (var qe in queryChiTietPt)
                        {
                            db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreateChiTietPhieuThuDaXoa(qe.LinkID, pt.SoPT, qe.SoTien, qe.TableName, qe.DienGiai));
                        }

                    }

                    #endregion

                    db.ptPhieuThus.DeleteOnSubmit(pt);
                    //Xóa Sổ quỹ thu chi
                    db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == (int?)gv.GetRowCellValue(i, "ID") && p.IsPhieuThu == true));
                }
            }
            try
            {
                db.SubmitChanges();

                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        private void Gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void Gv_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            LoadDetail();
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            Library.MasterDataContext db = new Library.MasterDataContext();
            try
            {
                var lReport = (from rp in db.rptReports
                    join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                    where tn.MaTN == (byte)itemToaNha.EditValue &
                          rp.GroupID == Deposit.Class.Enum.ReportGroupId.PHIEU_THU_ID
                    orderby rp.Rank
                    select new { rp.ID, rp.Name }).ToList();

                barPrint.ItemLinks.Clear();

                foreach (var report in lReport)
                {
                    DevExpress.XtraBars.BarButtonItem itemIn = new DevExpress.XtraBars.BarButtonItem(barManager1, report.Name);
                    itemIn.Tag = report.ID;
                    itemIn.ItemClick += itemIn_ItemClick;
                    barManager1.Items.Add(itemIn);
                    barPrint.ItemLinks.Add(itemIn);
                }
            }
            catch (System.Exception ex)
            {
                _lError.Add("ToaNhaEditValueChange: " + ex.Message);
            }

            finally
            {
                db.Dispose();
            }
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gv.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                return;
            }

            var db = new MasterDataContext();

            var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)e.Item.Tag);
            if (objForm != null)
            {
                var rtfText = BuildingDesignTemplate.Class.MergeField.PhieuThu(id.Value, objForm.Content);
                var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
                frm.ShowDialog(this);
            }
        }
    }
}