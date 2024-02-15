using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmListTaiSan_Import : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmListTaiSan_Import()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    repositoryItemDateEdit_NgayDuaVaoSD.NullDate = DateTime.MinValue;
                    repositoryItemDateEdit_NgayDuaVaoSD.NullText = String.Empty;
                    repositoryItemDateEdit_NgayHetHan.NullDate = DateTime.MinValue;
                    repositoryItemDateEdit_NgayHetHan.NullText = String.Empty;
                    repositoryItemDateEdit_NgayMua.NullDate = DateTime.MinValue;
                    repositoryItemDateEdit_NgayMua.NullText = String.Empty;
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());
                    System.Collections.Generic.List<TaiSanChiTiet> list = Library.Import.ExcelAuto.ConvertDataTable<TaiSanChiTiet>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new TaiSanChiTiet
                    //{
                    //    TenHeThong = _["Tên nhóm tài sản (*)"].ToString().Trim(),
                    //    TenLoaiTaiSan = _["Tên loại tài sản (*)"].ToString().Trim(),
                    //    TenTaiSan = _["Tên tài sản (*)"].ToString().Trim(),
                    //    MaChiTietTaiSan = _["Mã tài sản chi tiết (*)"].ToString().Trim(),
                    //    TenChiTietTaiSan = _["Tên tài sản chi tiết (*)"].ToString().Trim(),
                    //    ThongSoKyThuat = _["Thông số kỹ thuật"].ToString().Trim(),
                    //    MoTa = _["Mô tả"].ToString().Trim(),
                    //    SoLuong = _["Số lượng"].Cast<float>(),
                    //    TinhTrangTaiSan = _["Tình trạng tài sản (*)"].ToString().Trim(),
                    //    NhaCungCap = _["Nhà cung cấp (*)"].ToString().Trim(),
                    //    Block = _["Khối nhà"].ToString().Trim(),
                    //    ViTri = _["Vị trí"].ToString().Trim(),
                    //    QuanTrong = _["Quan trọng"].Cast<byte>(),
                    //    TonKho = _["Tồn kho"].Cast<float>(),
                    //    NgayMua = _["Ngày mua"] == null ? (DateTime?)null : _["Ngày mua"].Cast<DateTime>(),
                    //    NgayDuaVaoSuDung = _["Ngày đưa vào sử dụng"] == null ? (DateTime?)null : _["Ngày đưa vào sử dụng"].Cast<DateTime>(),
                    //    ThoiGianBaoHanh = _["Thời gian bảo hành"] == null ? (int?)null : _["Thời gian bảo hành"].Cast<int>(),
                    //    NgayHetHanBaoHanh = _["Ngày hết hạn bảo hành"] == null ? (DateTime?)null : _["Ngày hết hạn bảo hành"].Cast<DateTime>(),
                    //    NguyenGia = _["Nguyên giá"].Cast<decimal>(),
                    //    NgungSuDung = _["Ngưng sử dụng"].Cast<bool>()
                    //}).ToList();

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var objTaiSan = (List<TaiSanChiTiet>) gc.DataSource;
                var ltError = new List<TaiSanChiTiet>();
                foreach (var n in objTaiSan)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra dữ liệu
                        var objNhomTaiSan = db.tbl_NhomTaiSans.FirstOrDefault(_ =>
                            _.MaTN == MaTn && (_.TenVietTat.ToUpper().Equals(n.TenHeThong.ToUpper()) || (_.TenNhomTaiSan.ToUpper().Equals(n.TenHeThong.ToUpper()))));
                        if (objNhomTaiSan == null)
                        {
                            n.Error = "Hệ thống này không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objLoaiTaiSan =
                            db.tbl_LoaiTaiSans.FirstOrDefault(_ => _.NhomTaiSanID == (int?)objNhomTaiSan.ID && (_.TenVietTat.ToUpper().Equals(n.TenLoaiTaiSan.ToUpper()) || _.TenLoaiTaiSan.ToUpper().Equals(n.TenLoaiTaiSan.ToUpper())));
                        if (objLoaiTaiSan == null)
                        {
                            n.Error = "Loại tài sản này không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        var objTenTaiSan =
                            db.tbl_TenTaiSans.FirstOrDefault(_ => _.LoaiTaiSanID == objLoaiTaiSan.ID && (_.TenVietTat.ToUpper().Equals(n.TenTaiSan.ToUpper()) || _.TenTaiSan.ToUpper().Equals(n.TenTaiSan.ToUpper())));
                        if (objTenTaiSan == null)
                        {
                            n.Error = "Tên tài sản không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objTinhTrangTaiSan = db.tbl_TinhTrangTaiSans.FirstOrDefault(_ =>
                            _.TenTinhTrang.ToLower().Equals(n.TinhTrangTaiSan.ToLower()));
                        if (objTinhTrangTaiSan == null)
                        {
                            n.Error = "Tình trạng tài sản không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objNhaCungCap = db.tbl_NhaCungCapTaiSans.FirstOrDefault(_ =>
                            _.TenNhaCungCap.ToLower().Equals(n.NhaCungCap.ToLower()));
                        if (objNhaCungCap == null)
                        {
                            n.Error = "Nhà cung cấp không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objKhoiNha = db.mbKhoiNhas.FirstOrDefault(_ =>
                            _.MaTN == MaTn & _.TenKN.ToLower().Equals(n.Block.ToLower()));
                        if (objKhoiNha == null)
                        {
                            n.Error = "Khối nhà không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        
                        #endregion

                        var kt = db.tbl_ChiTietTaiSans.FirstOrDefault(_ => _.MaChiTietTaiSan.ToLower() == n.MaChiTietTaiSan.ToLower()& _.MaTN==MaTn & _.TenTaiSanID==objTenTaiSan.ID);
                        if (kt != null)
                        {
                            n.Error = "Mã chi tiết tài sản tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        else
                        {
                            var objLts = new tbl_ChiTietTaiSan();
                            objLts.NgungSuDung = n.NgungSuDung;
                            objLts.NguoiNhap = Common.User.MaNV;
                            objLts.NgayNhap = DateTime.Now;
                            objLts.TenChiTietTaiSan = n.TenChiTietTaiSan;
                            objLts.ThongSoKyThuat = n.ThongSoKyThuat;
                            objLts.MoTa = n.MoTa;
                            objLts.SoLuong = n.SoLuong;
                            objLts.TenTaiSanID = objTenTaiSan.ID;
                            objLts.TinhTrangTaiSanID = objTinhTrangTaiSan.ID;
                            objLts.NhaCungCapID = objNhaCungCap.ID;
                            objLts.BlockID = objKhoiNha.MaKN;
                            objLts.ViTri = n.ViTri;
                            objLts.QuanTrong = n.QuanTrong;
                            objLts.TonKho = n.TonKho;
                            objLts.NgayMua = n.NgayMua == DateTime.MinValue ? (DateTime?)null : n.NgayMua;
                            objLts.NgayDuaVaoSuDung = n.NgayDuaVaoSuDung==DateTime.MinValue?(DateTime?)null:n.NgayDuaVaoSuDung;
                            objLts.ThoiGianBaoHanh = n.ThoiGianBaoHanh;
                            objLts.NgayHetHanBaoHanh = n.NgayHetHanBaoHanh == DateTime.MinValue ? (DateTime?)null : n.NgayHetHanBaoHanh;
                            objLts.NguyenGia = n.NguyenGia;
                            objLts.MaTN = (byte?) MaTn;
                            objLts.MaChiTietTaiSan = n.MaChiTietTaiSan;
                            db.tbl_ChiTietTaiSans.InsertOnSubmit(objLts);
                        }

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gc.DataSource = ltError;
                }
                else
                {
                    gc.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }
    }

    public class TaiSanChiTiet
    {
        public string MaChiTietTaiSan { get; set; }
        public string TenChiTietTaiSan { get; set; }
        public string ThongSoKyThuat { get; set; }
        public string MoTa { get; set; }
        public string TenHeThong { get; set; }
        public string TenLoaiTaiSan { get; set; }
        public string TenTaiSan { get; set; }
        public string TinhTrangTaiSan { get; set; }
        public string NhaCungCap { get; set; }
        public string Block { get; set; }
        public string ViTri { get; set; }

        public float? SoLuong { get; set; }
        public float? TonKho { get; set; }

        public decimal? NguyenGia { get; set; }

        public byte? QuanTrong { get; set; }

        public DateTime? NgayMua { get; set; }
        public DateTime? NgayDuaVaoSuDung { get; set; }
        public DateTime? NgayHetHanBaoHanh { get; set; }

        public int? ThoiGianBaoHanh { get; set; }

        public string Error { get; set; }

        public bool? NgungSuDung { get; set; }
    }
}