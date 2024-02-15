using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.PhanCong
{
    public partial class frmPhanCongEdit : XtraForm
    {
        public byte MaTn;
        public int Id;
        public int IsSua; // 0: Thêm, 1: Sửa
        public int MaNhomTaiSanId;
        private int _tt = 0;

        private tbl_PhanCong _pc;
        private MasterDataContext _db;
        private List<PhanCong_NhanVienChiTiet> _l;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmPhanCongEdit()
        {
            InitializeComponent();
        }

        private void frmCongViecEdit_Load(object sender, EventArgs e)
        {
            controls = Library.Class.HuongDan.ShowAuto.GetControlItems(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            _db = new MasterDataContext();
            _pc = new tbl_PhanCong();
            _l = new List<PhanCong_NhanVienChiTiet>();
            var objNhanVien = (from nv in _db.tnNhanViens
                               join pq in _db.tnToaNhaNguoiDungs on nv.MaNV equals pq.MaNV
                               where pq.MaTN == MaTn
                               select new {
                               nv.MaNV,
                               nv.MaSoNV,
                               nv.HoTenNV,
                               }).ToList();
            repGlkNhanVien.DataSource = objNhanVien.Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV }).Distinct().ToList();

            dateDenNgay.DateTime = Common.GetDateTimeSystem();

            if (IsSua == 0 & Id == 0)
            {
                dateTuNgay.EditValue = Common.GetDateTimeSystem();
            }
            else
            {
                _pc = _db.tbl_PhanCongs.FirstOrDefault(_ => _.ID == Id);
                if (_pc != null)
                {
                    dateTuNgay.EditValue = _pc.Ngay;
                    txtnoidung.Text = _pc.NoiDungCongViec;
                    txtMaSoPhanCong.Text = _pc.MaSoPC;
                    if (_pc.DenNgay != null) dateDenNgay.DateTime = (DateTime)_pc.DenNgay;
                    _l = (from ct in _db.tbl_PhanCong_NhanVienChiTiets
                        where ct.IDPhanCong == _pc.ID
                        group new {ct} by new {ct.MaNV}
                        into g
                        select new PhanCong_NhanVienChiTiet
                        {
                            MaNV=g.Key.MaNV
                        }).ToList();
                }
                else
                {
                    _pc = new tbl_PhanCong();
                }
            }

            gc.DataSource = _l.ConvertToDataTable();

            itemClearText.Click += ItemClearText_Click;
            itemHuongDan.Click += ItemHuongDan_Click;
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        public class PhanCong_NhanVienChiTiet
        {
            public int? ID { get; set; }
            public int? MaNV { get; set; }
        }

        private void simpleButtonLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //_db = new MasterDataContext();

                #region Kiểm tra

                if (dateTuNgay.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn ngày");
                    dateTuNgay.Focus();
                    return;
                }

                if (txtMaSoPhanCong.EditValue == null)
                {
                    DialogBox.Error("Vui lòng nhập mã số phân công");
                    txtMaSoPhanCong.Focus();
                    return;
                }

                var objPC = _db.tbl_PhanCongs.FirstOrDefault(o =>
                    o.MaSoPC == (string) txtMaSoPhanCong.EditValue && o.ID != Id);
                if (objPC != null)
                {
                    DialogBox.Error("Mã số phân công đã tồn tại!");
                    txtMaSoPhanCong.Focus();
                    return;
                }

                if (gv.RowCount <= 1)
                {
                    DialogBox.Error("Vui lòng chọn giá trị");
                    return;
                }

                #endregion

                if (IsSua == 0 & Id == 0)
                {
                    #region Thêm công việc

                    _pc.Ngay = (DateTime?) dateTuNgay.EditValue;
                    _pc.MaSoPC = txtMaSoPhanCong.EditValue as string;
                    _pc.NoiDungCongViec = txtnoidung.EditValue as string;
                    _pc.MaTN = MaTn;
                    _pc.NgayTao = DateTime.Now;
                    _pc.NguoiTao = Common.User.MaNV;
                    _pc.DenNgay = dateDenNgay.DateTime;
                    _db.tbl_PhanCongs.InsertOnSubmit(_pc);

                    #endregion
                }
                else
                {
                    #region Sửa công việc

                    _pc.Ngay = (DateTime?) dateTuNgay.EditValue;
                    _pc.MaSoPC = txtMaSoPhanCong.EditValue as string;
                    _pc.NoiDungCongViec = txtnoidung.EditValue as string;
                    _pc.NgaySua = DateTime.Now;
                    _pc.NguoiSua = Common.User.MaNV;
                    _pc.DenNgay = dateDenNgay.DateTime;

                    #endregion
                }

                //_db.SubmitChanges();
                _db.tbl_PhanCong_NhanVienChiTiets.DeleteAllOnSubmit(_pc.tbl_PhanCong_NhanVienChiTiets);

                for (var i = 0; i < gv.RowCount - 1; i++)
                {
                    var id_nv = int.Parse(gv.GetRowCellValue(i, "MaNV").ToString());
                    // tạo nhân viên chi tiết theo từng ngày
                    var tuNgay = dateTuNgay.DateTime.Date;
                    var denNgay = dateDenNgay.DateTime.Date;
                    while (tuNgay.Date <= denNgay.Date)
                    {
                        // kiểm tra sự tồn tại nhân viên trong cùng 1 ngày
                        var objNvn = _db.tbl_PhanCong_NhanVienChiTiets.Where(_ =>
                            _.MaNV == id_nv & _.Ngay.Value.Year == tuNgay.Year & _.Ngay.Value.Month == tuNgay.Month &
                            _.Ngay.Value.Day == tuNgay.Day & _.IDPhanCong != _pc.ID & _.MaTN == MaTn).ToList();
                        if (objNvn.Count > 0)
                        {
                            DialogBox.Error(
                                "Nhân viên đã được phân công trong ngày rồi. Không thể phân công được nữa");
                            gv.DeleteSelectedRows();
                            return;
                        }
                        var ct = new tbl_PhanCong_NhanVienChiTiet();
                        ct.MaTN = MaTn;
                        ct.Ngay = tuNgay;
                        ct.MaNV = id_nv;
                        ct.NguoiSua = Common.User.MaNV;
                        ct.NgaySua = DateTime.Now;

                        _pc.tbl_PhanCong_NhanVienChiTiets.Add(ct);

                        tuNgay = tuNgay.AddDays(1);
                    }
                }

                _db.SubmitChanges();
                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");
            }
            catch
            {
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }


        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtMaSoPhanCong_Leave(object sender, EventArgs e)
        {
            var objPC = _db.tbl_PhanCongs.FirstOrDefault(o => o.MaSoPC == txtMaSoPhanCong.EditValue && o.ID != Id);
            if (objPC != null)
            {
                DialogBox.Error("Mã số phân công đã tồn tại!");
                txtMaSoPhanCong.Focus();
                return;
            }
        }

        private void gv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gv.SetFocusedRowCellValue("ID", 0);
        }

        private void gv_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (!string.IsNullOrEmpty(gv.GetSelectedRows().ToString()))
                {
                    gv.DeleteSelectedRows();
                }
            }
        }

        private void repGlkNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn Nhân Viên");
                    return;
                }

                if (!IsDuplication("MaNV", gv.FocusedRowHandle, item.EditValue.ToString()))
                {
                    gv.SetFocusedRowCellValue("MaNV", item.EditValue.ToString());
                    gv.UpdateCurrentRow();
                    return;
                }
                DialogBox.Error("Trùng nhân viên, vui lòng chọn nhân viên khác.");
                gv.DeleteSelectedRows();
                gv.UpdateCurrentRow();
                gv.FocusedRowHandle = -1;
            }
            catch (Exception)
            {
                //throw;
            }
        }
        private bool IsDuplication(string fielName, int index, string value)
        {
            for (var i = 0; i < gv.RowCount - 1; i++)
            {
                if (i == index) continue;
                if (gv.GetRowCellValue(i, fielName) != null)
                {
                    var oldValue = gv.GetRowCellValue(i, fielName).ToString();
                    if (oldValue == value) return true;
                }
            }
            return false;
        }
    }
}