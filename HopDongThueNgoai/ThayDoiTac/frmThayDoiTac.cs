using System.Linq;

namespace HopDongThueNgoai.ThayDoiTac
{
    public partial class FrmThayDoiTac : DevExpress.XtraEditors.XtraForm
    {
        public int? HopDongId { get; set; }
        public int? KhachHangId { get; set; }
        public string DiaChi { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.hdctnDanhSachHopDongThueNgoai _hd { get; set; }

        private System.Collections.Generic.List<string> _lError = new System.Collections.Generic.List<string>();

        public FrmThayDoiTac()
        {
            InitializeComponent();
        }

        private void frmThayDoiTac_Load(object sender, System.EventArgs e)
        {
            _hd = GetDanhSachHopDongThueNgoai(HopDongId);
            TaoKhachHang(0, "");

            if (_hd == null) return;

            glkNhaCungCap.Properties.DataSource = (from p in _db.tnKhachHangs
                                                   where p.MaTN.ToString() == _hd.MaToaNha
                                                   select new
                                                   {
                                                       TenVT = p.HoKH + " " + p.TenKH,
                                                       TenNCC = p.KyHieu,
                                                       MaNCC = p.MaKH
                                                   }).ToList();
            glkNhaCungCap.EditValue = int.Parse(_hd.NhaCungCap);
        }

        private Library.hdctnDanhSachHopDongThueNgoai GetDanhSachHopDongThueNgoai(int? hopDongId)
        {
            return _db.hdctnDanhSachHopDongThueNgoais.FirstOrDefault(_ => _.RowID == hopDongId);
        }

        private void TaoKhachHang(int? khachHangId, string diaChi)
        {
            KhachHangId = khachHangId;
            DiaChi = diaChi;
        }

        private void glkNhaCungCap_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                Library.tnKhachHang khachHang = GetKhachHangById((int?)item.EditValue);
                if (khachHang == null) return;
                txtDiaChi.Text = khachHang.DCLL;
            }
            catch (System.Exception ex)
            {
                _lError.Add(e.GetType() + ex.Message);
            }
        }

        private Library.tnKhachHang GetKhachHangById(int? khachHangId)
        {
            return _db.tnKhachHangs.FirstOrDefault(_ => _.MaKH == khachHangId);
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region Kiểm tra

            if (glkNhaCungCap.EditValue == null)
            {
                Library.DialogBox.Error("Vui lòng chọn Nhà Cung Cấp. Xin cảm ơn!");
                return;
            }

            #endregion

            try
            {
                // thay đổi khách hàng của hợp đồng này ở tất cả các bảng khác, và cả sổ quỹ lẫn sổ quỹ thu chi
                // cập nhật khách hàng ở hợp đồng
                _hd.NhaCungCap = glkNhaCungCap.EditValue.ToString();
                _hd.DiaChi = txtDiaChi.Text;

                // không đổi phiếu thu phiếu chi, thằng nào chi tiền thì vẫn là của nó, chứ phiếu người ta in ra rồi mà mình thay đổi thì không hay lắm
                // chỉ đổi công nợ để biết tổng công nợ còn bao nhiêu. Còn trong quá trình đó có nhiều thằng khác thì vẫn là nó đóng tiền. Trừ khi kế toán vào đó chỉnh hoặc họ có yêu cầu mình đổi hết cả trong phiếu thu phiếu chi thì đổi. Chứ theo đúng thì không được đổi nhé.

                var congNoNhaCungCap = _db.hdctnCongNoNhaCungCaps.Where(_ => _.HopDongId == _hd.RowID);

                foreach (var item in congNoNhaCungCap) item.KhachHangId = (int?)glkNhaCungCap.EditValue;

                var danhGia = _db.hdctnCongViec_DanhGias.Where(_ => _.HopDongId == _hd.RowID);
                foreach (var item in danhGia) item.KhachHangId = (int?)glkNhaCungCap.EditValue;

                var lichSuTienTrinh = _db.hdctnLichSuTienTrinhs.Where(_ => _.HopDongId == _hd.RowID);
                foreach (var item in lichSuTienTrinh) item.KhachHangId = (int?)glkNhaCungCap.EditValue;

                var lichThanhToan = _db.hdctnLichThanhToans.Where(_ => _.HopDongId == _hd.RowID);
                foreach (var item in lichThanhToan) item.KhachHangId = (int?)glkNhaCungCap.EditValue;

                var thanhLy = _db.hdctnThanhLies.Where(_ => _.HopDongId == _hd.RowID);
                foreach (var item in thanhLy) item.NhaCungCap = glkNhaCungCap.EditValue.ToString();

                _db.SubmitChanges();

                TaoKhachHang((int?)glkNhaCungCap.EditValue, txtDiaChi.Text);

                Library.DialogBox.Success("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                _lError.Add(e.GetType() + ex.Message);
                Library.DialogBox.Error("Không lưu được hợp đồng, lỗi: " + ex.Message);
            }

            finally
            {
                _db.Dispose();
                this.Close();
            }
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}