using System.Linq;
using Library;

namespace HopDongThueNgoai.DanhGia
{
    public partial class FrmDanhGiaCongViecEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? HopDongId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        internal class DanhGiaView : Library.hdctnCongViec_DanhGia { }
        private System.Data.DataTable _dt;

        public FrmDanhGiaCongViecEdit()
        {
            InitializeComponent();
        }

        private void FrmDanhGiaCongViecEdit_Load(object sender, System.EventArgs e)
        {
            _dt = GetListCongViecByHopDongId().ConvertToDataTable();
            gc.DataSource = _dt;
        }

        private System.Collections.Generic.List<Library.hdctnCongViec_DanhGia> GetListCongViecByHopDongId()
        {
            return (from p in _db.hdctnChiTietHopDongThueNgoais
                join cv in _db.hdctnCongViecs on p.MaCongViec equals cv.RowID
                //join dg in _db.hdctnCongViec_DanhGias on p.RowID equals dg.ChiTietHopDongId into g
                //from dg in g.DefaultIfEmpty()
                where p.HopDongId == HopDongId //& 
                    //(dg == null || (dg.UserId != null & dg.UserId == Library.Common.User.MaNV))
                select new DanhGiaView
                {
                    ChiTietHopDongId = p.RowID, CongViecName = cv.TenCongViec, HopDongId = p.HopDongId,
                    HopDongNo = p.hdctnDanhSachHopDongThueNgoai.SoHopDong,
                    KhachHangId = int.Parse(p.hdctnDanhSachHopDongThueNgoai.NhaCungCap),
                    //DanhGiaId = dg != null ? dg.Id : 0, 
                    BuildingId = byte.Parse(p.hdctnDanhSachHopDongThueNgoai.MaToaNha), CongViecId = cv.RowID,
                    UserName = Library.Common.User.HoTenNV, UserId = Library.Common.User.MaNV,
                    DanhGia = _db.hdctnCongViec_DanhGias.FirstOrDefault(_ => _.ChiTietHopDongId == p.RowID & _.UserId == Library.Common.User.MaNV) != null ? _db.hdctnCongViec_DanhGias.First(_ => _.ChiTietHopDongId == p.RowID & _.UserId == Library.Common.User.MaNV).DanhGia : 0,
                    Id = _db.hdctnCongViec_DanhGias.FirstOrDefault(_ => _.ChiTietHopDongId == p.RowID & _.UserId == Library.Common.User.MaNV) != null ? _db.hdctnCongViec_DanhGias.First(_ => _.ChiTietHopDongId == p.RowID & _.UserId == Library.Common.User.MaNV).Id : 0
                }).Cast<Library.hdctnCongViec_DanhGia>().ToList();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                gv.FocusedRowHandle = -1;

                System.Collections.Generic.List<Library.hdctnCongViec_DanhGia> lCongViec = _dt.DataTableToList<Library.hdctnCongViec_DanhGia>();

                foreach (var item in lCongViec)
                {
                    Library.hdctnCongViec_DanhGia danhGia = GetDanhGiaById((int?)item.Id);
                    if (danhGia == null)
                    {
                        danhGia = new Library.hdctnCongViec_DanhGia();
                        _db.hdctnCongViec_DanhGias.InsertOnSubmit(danhGia);
                    }

                    danhGia.ChiTietHopDongId = item.ChiTietHopDongId;
                    if (item.DanhGia != null) danhGia.DanhGia = item.DanhGia;
                    danhGia.BuildingId = item.BuildingId;
                    danhGia.CongViecId = item.CongViecId;
                    danhGia.CongViecName = item.CongViecName;
                    danhGia.HopDongId = item.HopDongId;
                    danhGia.HopDongNo = item.HopDongNo;
                    danhGia.KhachHangId = item.KhachHangId;
                    danhGia.UserId = item.UserId;
                    danhGia.UserName = item.UserName;
                }

                _db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private Library.hdctnCongViec_DanhGia GetDanhGiaById(int? danhGiaId)
        {
            return _db.hdctnCongViec_DanhGias.FirstOrDefault(_ => _.Id == danhGiaId);
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void gv_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            var item = e.FocusedColumn.FieldName.Contains("DanhGia");
            if (item == true)
            {
                var value = gv.GetFocusedRowCellValue(e.FocusedColumn.FieldName);
            }
        }
    }
}