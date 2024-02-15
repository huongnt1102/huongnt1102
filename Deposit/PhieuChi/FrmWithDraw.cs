using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace Deposit.PhieuChi
{
    public partial class FrmWithDraw : DevExpress.XtraEditors.XtraForm
    {
        public int? HopDongDatCocId { get; set; }
        public string FormName { get; set; }

        private Library.MasterDataContext _db = new MasterDataContext();
        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();

        public FrmWithDraw()
        {
            InitializeComponent();
        }

        private void FrmWithDraw_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            this.Text = FormName;

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
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var maTn = (byte)itemToaNha.EditValue;

                switch (HopDongDatCocId)
                {
                    case null:
                        gc.DataSource = Deposit.Class.PhieuChi.GetPhieuChi(maTn, tuNgay, denNgay);
                        break;
                    default:
                        gc.DataSource = Deposit.Class.PhieuChi.GetPhieuChiByHopDong(HopDongDatCocId);
                        break;
                };
                
            }
            catch{}
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

            var hopDongDatCocId = (int?) gv.GetFocusedRowCellValue("HopDongDatCocId");
            if (hopDongDatCocId == null)
            {
                Library.DialogBox.Alert("Phiếu không có hợp đồng đặt cọc");
                return;
            }

            using (var frm = new FrmWithDrawEdit())
            {
                frm.MaPc = id;
                frm.MaPt = (int?)gv.GetFocusedRowCellValue("MaPT");
                frm.HopDongDatCocId = (int?) gv.GetFocusedRowCellValue("HopDongDatCocId");
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
                Library.DialogBox.Alert("Vui lòng chọn phiếu thu");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            _db = new MasterDataContext();
            foreach (var i in indexs)
            {
                var pc = _db.pcPhieuChi_TraLaiKhachHangs.FirstOrDefault(_ => _.ID == (int)gv.GetRowCellValue(i, "ID"));
                if (pc != null)
                {
                    #region Lưu lại phiếu thu đã xóa
                    // xóa hết những phiếu thu tự tạo
                    var ptChild = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == pc.PtPhatId);
                    if (ptChild != null)
                    {
                        _db.ptPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreatePhieuThuDaXoa(ptChild.LyDo, ptChild.MaKH, ptChild.MaNV, Library.Common.User.MaNV, ptChild.MaPL, ptChild.MaTKNH, ptChild.MaTN, ptChild.NguoiNop, System.DateTime.UtcNow.AddHours(7), ptChild.NgayThu, ptChild.SoPT, ptChild.SoTien, ptChild.ChungTuGoc, ptChild.DiaChiNN));
                        var queryChiTietPt = _db.ptChiTietPhieuThus.Where(p => p.MaPT == pc.ID).ToList();
                        if (queryChiTietPt.Count > 0)
                        {
                            foreach (var qe in queryChiTietPt)
                            {
                                _db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(Deposit.Class.PhieuThu.CreateChiTietPhieuThuDaXoa(qe.LinkID,ptChild.SoPT,qe.SoTien,qe.TableName,qe.DienGiai));
                            }
                        }
                        _db.ptChiTietPhieuThus.DeleteAllOnSubmit(ptChild.ptChiTietPhieuThus);
                        _db.ptPhieuThus.DeleteOnSubmit(ptChild);
                        //Xóa Sổ quỹ thu chi
                        _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(p => p.IDPhieu == ptChild.ID && p.IsPhieuThu == true));
                    }

                    #endregion
                    _db.SoQuy_ThuChis.DeleteAllOnSubmit(_db.SoQuy_ThuChis.Where(_=>_.IDPhieu == pc.ID && _.IsKhauTru == false));
                    _db.pcPhieuChi_TraLaiKhachHangs.DeleteOnSubmit(pc);
                    _db.SubmitChanges();

                    #region cập nhật tiền phiếu thu tổng
                    var pt = _db.ptPhieuThus.FirstOrDefault(_ => _.ID == pc.MaPT);
                    if (pt != null)
                    {
                        pt = UpdatePhieuThuTong(pt, pc.MaPT);

                        #region Update hợp đồng

                        var hopDong = GetHopDongById(pt.HopDongDatCocId);
                        if (hopDong != null) hopDong = UpdateHopDong(hopDong);
                        #endregion
                    }
                    
                    #endregion

                    

                    _db.SubmitChanges();
                }
            }
            try
            {
                

                LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                _db.Dispose();
            }
        }

        private Library.ptPhieuThu UpdatePhieuThuTong(Library.ptPhieuThu phieuThu, int? maPt)
        {
            decimal totalReceil = 0, totalPay = 0;
            var listPhieuChi = _db.pcPhieuChi_TraLaiKhachHangs.Where(p => p.MaPT == maPt).ToList();
            foreach (var phieuChi in listPhieuChi)
            {
                totalReceil = totalReceil + phieuChi.SoTienPhat.GetValueOrDefault();
                totalPay = totalPay + phieuChi.SoTienChi.GetValueOrDefault();
            }

            phieuThu.TotalPay = totalPay;
            phieuThu.TotalReceipts = totalReceil;
            return phieuThu;
        }

        private Library.Dep_HopDong GetHopDongById(int? id)
        {
            return _db.Dep_HopDongs.FirstOrDefault(_ => _.Id == id);
        }

        private Library.Dep_HopDong UpdateHopDong(Library.Dep_HopDong hopDong)
        {
            var listPhieuChi = _db.ptPhieuThus.Where(p => p.HopDongDatCocId == hopDong.Id).ToList();
            hopDong.ThuPhat = listPhieuChi.Sum(_ => _.TotalReceipts).GetValueOrDefault();
            hopDong.TienTra = listPhieuChi.Sum(_ => _.TotalPay).GetValueOrDefault();

            return hopDong;
        }

        private void CbxKbc_EditValueChanged(object sender, EventArgs e)
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
                          rp.GroupID == Deposit.Class.Enum.ReportGroupId.PHIEU_CHI_DAT_COC_ID
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
                DialogBox.Error("Vui lòng chọn [Phiếu chi] cần xem");
                return;
            }

            var db = new MasterDataContext();

            var objForm = db.template_Forms.FirstOrDefault(_ => _.ReportId == (int)e.Item.Tag);
            if (objForm != null)
            {
                var rtfText = Deposit.Class.InPhieuChiDatCoc.PhieuChi(id.Value, objForm.Content);
                var frm = new BuildingDesignTemplate.FrmShow { RtfText = rtfText };
                frm.ShowDialog(this);
            }
        }
    }
}