using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmTenTaiSan_Import : XtraForm
    {
        public byte? MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmTenTaiSan_Import()
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
                    System.Collections.Generic.List<TenTaiSans> list = Library.Import.ExcelAuto.ConvertDataTable<TenTaiSans>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new TenTaiSans
                    //{
                    //    TenVietTat = _["Mã tên tài sản"].ToString().Trim(),
                    //    TenTaiSan = _["Tên tài sản"].ToString().Trim(),
                    //    DienGiai = _["Diễn giải"].ToString().Trim(),
                    //    NgungSuDung = _["Ngưng sử dụng"].Cast<bool>(),
                    //    TenHeThong = _["Tên hệ thống"].ToString().Trim(),
                    //    LoaiTaiSan = _["Tên loại tài sản"].ToString().Trim(),
                    //    ThongSoKyThuat = _["Thông số kỹ thuật"].ToString().Trim(),
                    //    SoLuong = _["Số lượng"].Cast<float>(),
                    //    TinhTrangTaiSan = _["Tình trạng tài sản"].ToString().Trim(),
                    //    NhaCungCap = _["Nhà cung cấp"].ToString().Trim(),
                    //    Block = _["Khối nhà"].ToString().Trim(),
                    //    ViTri = _["Vị trí"].ToString().Trim(),
                    //    QuanTrong = _["Quan trọng"].Cast<bool>(),
                    //    TonKho = _["Tồn kho"].Cast<float>(),
                    //    NgayMua = _["Ngày mua"] == null ? (DateTime?)null : _["Ngày mua"].Cast<DateTime>(),
                    //    NgayDuaVaoSuDung = _["Ngày đưa vào sử dụng"] == null ? (DateTime?)null : _["Ngày đưa vào sử dụng"].Cast<DateTime>(),
                    //    ThoiGianBaoHanh = _["Thời gian bảo hành"] == null ? (int?)null : _["Thời gian bảo hành"].Cast<int>(),
                    //    NgayHetHanBaoHanh = _["Ngày hết hạn bảo hành"] == null ? (DateTime?)null : _["Ngày hết hạn bảo hành"].Cast<DateTime>(),
                    //    NguyenGia = _["Nguyên giá"].Cast<decimal>(),
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
                var objTenTaiSan = (List<TenTaiSans>) gc.DataSource;
                var ltError = new List<TenTaiSans>();
                foreach (var n in objTenTaiSan)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra dữ liệu

                        var objNhomTaiSan = db.tbl_NhomTaiSans.FirstOrDefault(_ =>
                            _.MaTN == MaTn &&( _.TenVietTat.ToUpper().Equals(n.TenHeThong.ToUpper()) || (_.TenNhomTaiSan.ToUpper().Equals(n.TenHeThong.ToUpper()))));
                        if (objNhomTaiSan == null)
                        {
                            n.Error = "Hệ thống này không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objLoaiTaiSan =
                            db.tbl_LoaiTaiSans.FirstOrDefault(_ => _.NhomTaiSanID == (int?) objNhomTaiSan.ID && (_.TenVietTat.ToUpper().Equals(n.LoaiTaiSan.ToUpper()) || _.TenLoaiTaiSan.ToUpper().Equals(n.LoaiTaiSan.ToUpper())));
                        if (objLoaiTaiSan == null)
                        {
                            n.Error = "Loại tài sản này không tồn tại";
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

                        var kt = db.tbl_TenTaiSans.FirstOrDefault(_ => _.TenVietTat.ToUpper().Equals(n.TenVietTat.ToLower()) && _.LoaiTaiSanID==objLoaiTaiSan.ID);
                        if (kt != null)
                        {
                            n.Error = "Mã tài sản này đã tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        else
                        {
                            var objLts = new tbl_TenTaiSan();
                            objLts.TenVietTat = n.TenVietTat;
                            objLts.TenTaiSan = n.TenTaiSan;
                            objLts.DienGiai = n.DienGiai;
                            objLts.ThongSoKyThuat = n.ThongSoKyThuat;
                            objLts.SoLuong = n.SoLuong;
                            objLts.LoaiTaiSanID = objLoaiTaiSan.ID;
                            objLts.TinhTrangTaiSanID = objTinhTrangTaiSan.ID;
                            objLts.NhaCungCapID = objNhaCungCap.ID;
                            objLts.BlockID = objKhoiNha.MaKN;
                            objLts.ViTri = n.ViTri;
                            objLts.QuanTrong = n.QuanTrong;
                            objLts.TonKho = n.TonKho;
                            objLts.NgayMua = n.NgayMua == DateTime.MinValue ? (DateTime?)null : n.NgayMua;
                            objLts.NgayDuaVaoSuDung = n.NgayDuaVaoSuDung == DateTime.MinValue ? (DateTime?)null : n.NgayDuaVaoSuDung;
                            objLts.ThoiGianBaoHanh = n.ThoiGianBaoHanh;
                            objLts.NgayHetHanBaoHanh = n.NgayHetHanBaoHanh == DateTime.MinValue ? (DateTime?)null : n.NgayHetHanBaoHanh;
                            objLts.NguyenGia = n.NguyenGia;
                            objLts.NgungSuDung = n.NgungSuDung;
                            objLts.NguoiNhap = Common.User.MaNV;
                            objLts.NgayNhap = DateTime.Now;
                            objLts.LoaiTaiSanID = objLoaiTaiSan.ID;
                            objLts.MaTN = MaTn;
                            db.tbl_TenTaiSans.InsertOnSubmit(objLts);
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }
    }

    public class TenTaiSans
    {
        public string TenVietTat { get; set; }
        public string TenTaiSan { get; set; }
        public string DienGiai { get; set; }
        public string TenHeThong { get; set; }
        public string LoaiTaiSan { get; set; }
        public string ThongSoKyThuat { get; set; }
        public string TinhTrangTaiSan { get; set; }
        public string NhaCungCap { get; set; }
        public string Block { get; set; }
        public string ViTri { get; set; }
        public float? SoLuong { get; set; }
        public float? TonKho { get; set; }
        public decimal? NguyenGia { get; set; }
        public bool? QuanTrong { get; set; }
        public DateTime? NgayMua { get; set; }
        public DateTime? NgayDuaVaoSuDung { get; set; }
        public DateTime? NgayHetHanBaoHanh { get; set; }
        public int? ThoiGianBaoHanh { get; set; }
        public bool? NgungSuDung { get; set; }
        public string Error { get; set; }
        
    }
}