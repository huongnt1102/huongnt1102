using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmNhapKho_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmNhapKho_Import()
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
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());
                    System.Collections.Generic.List<NhapKho> list = Library.Import.ExcelAuto.ConvertDataTable<NhapKho>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

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
                var obj = (List<NhapKho>) gc.DataSource;
                var ltError = new List<NhapKho>();
                foreach (var n in obj)
                {
                    try
                    {
                        db = new MasterDataContext();
                        tbl_VatTu_MuaHang muaHang=null;
                        tbl_VatTu vatTu = null;
                        tbl_VatTu_MuaHang_ChiTiet muaHangChiTiet = null;

                        #region Kiểm tra
                        // kiểm tra loại nhập
                        var loaiNhap =
                            db.tbl_VatTu_NhapKho_LoaiNhaps.FirstOrDefault(_ => _.Ten.ToLower() == n.LoaiNhap.ToLower());
                        if (loaiNhap==null)
                        {
                            n.Error = "Loại nhập: " + n.LoaiNhap + " không có trong hệ thống.";
                            ltError.Add(n);
                            continue;
                        }
                        // kiểm tra mã kho
                        var maKho = db.tbl_VatTu_Khos.FirstOrDefault(_ => _.MaKho.ToLower() == n.MaKho.ToLower());
                        if (maKho == null)
                        {
                            n.Error = "Mã kho " + n.MaKho + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }
                        // kiểm tra người mua có tồn tại
                        var nguoiNhap = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.NguoiNhap.ToLower());
                        if (nguoiNhap == null)
                        {
                            n.Error = "NguoiNhap " + n.NguoiNhap + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra nhà cung cấp

                        // kiểm tra phiếu đề xuất, trường hợp có phiếu đề xuất
                        if (n.MaPhieuMuaHang != "")
                        {
                            // kiểm tra có phiếu mua hàng hay không
                            muaHang = db.tbl_VatTu_MuaHangs.FirstOrDefault(_ =>
                                _.MaPhieu.ToLower() == n.MaPhieuMuaHang.ToLower());
                            if (muaHang == null)
                            {
                                n.Error = "Phiếu mua hàng " + n.MaPhieuMuaHang + " không tồn tại.";
                                ltError.Add(n);
                                continue;
                            }
                            else
                            {
                                // trường hợp có tồn tại, kiểm tra mã hàng có tồn tại hay không
                                vatTu = db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower());
                                if (vatTu == null)
                                {
                                    n.Error = "Vật tư " + n.KyHieu + " không có trong hệ thống.";
                                    ltError.Add(n);
                                    continue;
                                }
                                else
                                {
                                    // ngược lại nếu có, kiểm tra chi tiết mua hàng xem hàng này có đúng trong phiếu mua hay không
                                    muaHangChiTiet = db.tbl_VatTu_MuaHang_ChiTiets.FirstOrDefault(_ =>
                                        _.MuaHangID == muaHang.ID & _.VatTuID == vatTu.ID);
                                    if (muaHangChiTiet == null)
                                    {
                                        n.Error = "Phiếu mua hàng " + n.MaPhieuMuaHang + " không có vật tư " + n.KyHieu;
                                        ltError.Add(n);
                                        continue;
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            // kiểm tra vật tư có tồn tại, trường hợp không có phiếu đề xuất
                            vatTu = db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower());
                            if (vatTu == null)
                            {
                                n.Error = "Vật tư " + n.KyHieu + " không có trong hệ thống";
                                ltError.Add(n);
                                continue;
                            }
                            else
                            {
                                //vatTu = ktVatTu;
                            }
                        }

                        #endregion

                        tbl_VatTu_MuaHang mh;
                        tbl_VatTu_NhapKho nk;

                        // kiểm tra đã tồn tại phiếu nhập kho chưa
                        nk = db.tbl_VatTu_NhapKhos.FirstOrDefault(_ =>
                            _.SoCT.ToLower() == n.MaPhieu.ToLower() & _.MaTN == MaTn);
                        if (nk != null)
                        {
                            // tồn tại phiếu nhập kho
                            nk.NgaySua = DateTime.Now;
                            nk.NguoiSua = Common.User.MaNV;
                        }
                        else
                        {
                            nk = new tbl_VatTu_NhapKho {NgayNhap = DateTime.Now, NguoiNhap = Common.User.MaNV};
                            db.tbl_VatTu_NhapKhos.InsertOnSubmit(nk);
                        }

                        nk.NgayNhapKho = n.NgayPhieu;
                        nk.SoCT = n.MaPhieu;
                        nk.MaTN = MaTn;
                        nk.LoaiNhapID = loaiNhap.ID;
                        nk.KhoID = maKho.ID;
                        nk.NguoiNhapKho = nguoiNhap.MaNV;
                        nk.LyDoNhap = n.DienGiai;
                        nk.ChungTuGoc = n.ChungTuGoc;

                        var nkCt = new tbl_VatTu_NhapKho_ChiTiet
                        {
                            VatTuID = vatTu.ID, SoLuong = n.SoLuong, DonGia = n.DonGia
                        };
                        nkCt.ThanhTien = nkCt.SoLuong * nkCt.DonGia;
                        nk.tbl_VatTu_NhapKho_ChiTiets.Add(nkCt);

                        if (muaHang != null)
                        {
                            nk.MuaHangID = muaHang.ID;
                            nk.DeXuatID = muaHang.DeXuatID;
                            nkCt.MuaHangChiTietID = muaHangChiTiet.ID;
                            nkCt.DeXuatChiTietID = muaHangChiTiet.DeXuatChiTietID;
                            nkCt.SoLuongMuaHang = muaHangChiTiet.SoLuong;
                            nkCt.TenDVT = vatTu.tbl_VatTu_DVT.TenDVT;

                            // cập nhật lại số lượng nhập kho trong phiếu mua hàng
                            var soLuongNhap = db.tbl_VatTu_NhapKho_ChiTiets
                                .Where(_ => _.MuaHangChiTietID == muaHangChiTiet.ID).Sum(_ => _.SoLuong)
                                .GetValueOrDefault();
                            muaHangChiTiet.SoLuongNhapKho = soLuongNhap + nkCt.SoLuong;
                            if (muaHangChiTiet.DeXuatChiTietID != null)
                            {
                                var deXuatChiTiet =
                                    db.tbl_VatTu_DeXuat_ChiTiets.First(_ => _.ID == muaHangChiTiet.DeXuatChiTietID);
                                deXuatChiTiet.SoLuongNhapKho = muaHangChiTiet.SoLuongNhapKho;
                            }

                            // cập nhật lại trạng thái phiếu mua hàng
                            var kt2 = muaHang.tbl_VatTu_MuaHang_ChiTiets.Where(_ =>
                                _.SoLuongNhapKho.GetValueOrDefault() != 0).ToList();
                            if (kt2.Count != 0)
                            {
                                var kt = muaHang.tbl_VatTu_MuaHang_ChiTiets.Where(_ => _.SoLuong > _.SoLuongNhapKho)
                                    .ToList();
                                muaHang.TrangThaiNhapKhoID = kt.Count > 0 ? 2 : 3;
                            }
                            else
                                muaHang.TrangThaiNhapKhoID = 1;
                        }

                        db.SubmitChanges();

                        // cập nhật sổ kho
                        var dt = nk.tbl_VatTu_NhapKho_ChiTiets.ToList().ConvertToDataTable();
                        if (dt != null)
                        {
                            var lCt = dt.AsEnumerable().Select(_ => new tbl_VatTu_SoKho
                            {
                                NgayPhieu = nk.NgayNhapKho,
                                SoPhieu = nk.SoCT,
                                IDPhieu = nk.ID,
                                IDPhieuChiTiet = _.Field<long>("ID"),
                                SoLuong = _.Field<decimal>("SoLuong"),
                                DonGia = _.Field<decimal>("DonGia"),
                                ThanhTien = _.Field<decimal>("ThanhTien"),
                                MaLoaiNhapXuat = 1,
                                MaTN = MaTn,
                                NgayNhap = nk.NgayNhap,
                                NguoiNhap = nk.NguoiNhap,
                                KhoID = nk.KhoID,
                                VatTuID = _.Field<long>("VatTuID")
                            }).ToList();

                            var l = db.tbl_VatTu_SoKhos;
                            var l1 = l.Where(_ => _.IDPhieu == nk.ID).ToList();

                            // vòng for 1, kiểm tra lCt không có trong l1, nếu kt thấy không có trong lCt, delete l1
                            foreach (var i in l1)
                            {
                                var o1 = lCt.FirstOrDefault(_ =>
                                    _.IDPhieu == i.IDPhieu & _.IDPhieuChiTiet == i.IDPhieuChiTiet);
                                if (o1 == null)
                                {
                                    l.DeleteOnSubmit(i);
                                }
                            }

                            foreach (var i in lCt)
                            {
                                var o2 = l1.FirstOrDefault(_ =>
                                    _.IDPhieu == i.IDPhieu & _.IDPhieuChiTiet == i.IDPhieuChiTiet);
                                if (o2 == null)
                                    l.InsertOnSubmit(i);
                                else
                                {
                                    // update sổ kho
                                    o2.NgayNhap = i.NgayPhieu;
                                    o2.SoPhieu = i.SoPhieu;
                                    o2.SoLuong = i.SoLuong;
                                    o2.DonGia = i.DonGia;
                                    o2.ThanhTien = i.ThanhTien;
                                    o2.KhoID = i.KhoID;
                                    o2.VatTuID = i.VatTuID;
                                    o2.NguoiSua = Common.User.MaNV;
                                    o2.NgaySua = DateTime.Now;
                                }
                            }
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

                gc.DataSource = ltError.Count > 0 ? ltError : null;
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

    public class NhapKho
    {
        public string MaPhieu { get; set; }
        public string LoaiNhap { get; set; }
        public string MaKho { get; set; }
        public string NguoiNhap { get; set; }
        public string DienGiai { get; set; }
        public string ChungTuGoc { get; set; }
        public string KyHieu { get; set; }
        public string Error { get; set; }
        public string MaPhieuMuaHang { get; set; }

        public DateTime? NgayPhieu { get; set; }

        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
    }
}