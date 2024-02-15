using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmXuatKho_Import : XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }
        public int LoaiXuat { get; set; }

        public frmXuatKho_Import()
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
                    System.Collections.Generic.List<XuatKho> list = Library.Import.ExcelAuto.ConvertDataTable<XuatKho>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;
                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new XuatKho
                    //{
                    //    MaPhieu = _["Mã phiếu"].ToString().Trim(),
                    //    LoaiXuat = _["Loại xuất"].ToString().Trim(),
                    //    DienGiai = _["Lý do xuất"].ToString().Trim(),
                    //    KhoXuat = _["Kho xuất"].ToString().Trim(),
                    //    NguoiNhan = _["Người xuất"].ToString().Trim(),
                    //    ChungTuGoc=_["Chứng từ gốc"].ToString().Trim(),
                    //    KyHieu = _["Mã số vật tư"].ToString().Trim(),
                    //    NgayPhieu = _["Ngày phiếu"].Cast<DateTime>(),
                    //    SoLuong = _["Số lượng"].Cast<decimal>(),
                    //    DonGia=_["Đơn giá"].Cast<decimal>(),
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
                var obj = (List<XuatKho>) gc.DataSource;
                var ltError = new List<XuatKho>();
                foreach (var n in obj)
                {
                    try
                    {
                        db = new MasterDataContext();
                        tbl_VatTu vatTu;

                        #region Kiểm tra

                        // kiểm tra loại nhập
                        var loaiXuat =
                            db.tbl_VatTu_XuatKho_LoaiXuats.FirstOrDefault(_ => _.Ten.ToLower() == n.LoaiXuat.ToLower());
                        if (loaiXuat == null)
                        {
                            n.Error = "Loại xuất: " + n.LoaiXuat + " không có trong hệ thống.";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra mã kho
                        var khoXuat = db.tbl_VatTu_Khos.FirstOrDefault(_ => _.MaKho.ToLower() == n.KhoXuat.ToLower());
                        if (khoXuat == null)
                        {
                            n.Error = "Mã kho " + n.KhoXuat + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra người mua có tồn tại
                        var nguoiXuat = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.NguoiNhan.ToLower());
                        if (nguoiXuat == null)
                        {
                            n.Error = "Người xuất " + n.NguoiNhan + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }


                        // kiểm tra vật tư có tồn tại, trường hợp không có phiếu đề xuất
                        vatTu = db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower());
                        if (vatTu == null)
                        {
                            n.Error = "Vật tư " + n.KyHieu + " không có trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }


                        #endregion

                        tbl_VatTu_XuatKho xk;
                        xk = db.tbl_VatTu_XuatKhos.FirstOrDefault(_ => _.MaPhieu.ToLower() == n.MaPhieu.ToLower());
                        if (xk != null)
                        {
                            xk.NguoiSua = Common.User.MaNV;
                            xk.NgaySua = DateTime.Now;
                        }
                        else
                        {
                            xk = new tbl_VatTu_XuatKho
                            {
                                NgayNhap = DateTime.Now, NguoiNhap = Common.User.MaNV, MaTN = MaTn
                            };
                            db.tbl_VatTu_XuatKhos.InsertOnSubmit(xk);
                        }

                        var ctXk = new tbl_VatTu_XuatKho_ChiTiet {SoLuong = n.SoLuong, DonGia=n.DonGia, ThanhTien=n.DonGia*n.SoLuong,VatTuID=vatTu.ID};
                        xk.tbl_VatTu_XuatKho_ChiTiets.Add(ctXk);

                        ctXk.SoLuongTonKho = db.tbl_VatTu_SoKhos
                            .Where(_ => _.MaTN == MaTn & _.KhoID == khoXuat.ID & _.VatTuID == vatTu.ID)
                            .Sum(_ => _.SoLuong).GetValueOrDefault();
                        ctXk.TenDVT = vatTu.tbl_VatTu_DVT.TenDVT;

                        xk.MaPhieu = n.MaPhieu;
                        xk.NgayPhieu = n.NgayPhieu;
                        xk.DienGiai = n.DienGiai;
                        xk.NguoiNhan = nguoiXuat.MaNV;
                        xk.KhoID = khoXuat.ID;
                        xk.ChungTuGoc = n.ChungTuGoc;

                        xk.LoaiXuatID = loaiXuat.ID;

                        db.SubmitChanges();

                        // cập nhật sổ kho
                        var dt = xk.tbl_VatTu_XuatKho_ChiTiets.ToList().ConvertToDataTable();
                        var l = db.tbl_VatTu_SoKhos;
                        if (dt != null)
                        {
                            var lCt = dt.AsEnumerable().Select(_ => new tbl_VatTu_SoKho
                            {
                                NgayPhieu = xk.NgayPhieu, SoPhieu = xk.MaPhieu, IDPhieu = xk.ID,
                                IDPhieuChiTiet = _.Field<long>("ID"), SoLuong = -_.Field<decimal>("SoLuong"),
                                DonGia = -_.Field<decimal>("DonGia"), ThanhTien = -_.Field<decimal>("ThanhTien"),
                                MaLoaiNhapXuat = 2, MaTN = xk.MaTN, NgayNhap = xk.NgayNhap, NguoiNhap = xk.NguoiNhap,
                                KhoID = xk.KhoID, VatTuID = _.Field<long>("VatTuID")
                            }).ToList();

                            var l1 = l.Where(_ => _.IDPhieu == xk.ID).ToList();

                            // vòng for 1, kiểm tra lCt không có trong l1, for l1, nếu kiểm tra thấy không có trong lCt, delete l1
                            foreach (var i in l1)
                            {
                                var o = lCt.FirstOrDefault(_ =>
                                    _.IDPhieu == i.IDPhieu & _.IDPhieuChiTiet == i.IDPhieuChiTiet);
                                if (o == null) l.DeleteOnSubmit(i);
                            }

                            foreach (var i in lCt)
                            {
                                var o = l1.FirstOrDefault(_ =>
                                    _.IDPhieu == i.IDPhieu & _.IDPhieuChiTiet == i.IDPhieuChiTiet);
                                if (o == null) l.InsertOnSubmit(i);
                                else
                                {
                                    // update
                                    o.NgayPhieu = i.NgayPhieu;
                                    o.SoPhieu = i.SoPhieu;
                                    o.SoLuong = i.SoLuong;
                                    o.DonGia = i.DonGia;
                                    o.ThanhTien = i.ThanhTien;
                                    o.KhoID = i.KhoID;
                                    o.VatTuID = i.VatTuID;
                                    o.NguoiSua = i.NguoiSua;
                                    o.NgaySua = i.NgaySua;
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class XuatKho
    {
        public string MaPhieu { get; set; }
        public string LoaiXuat { get; set; }
        public string KhoXuat { get; set; }
        public string NguoiNhan { get; set; }
        public string DienGiai { get; set; }
        public string ChungTuGoc { get; set; }
        public string KyHieu { get; set; }
        public string Error { get; set; }

        public DateTime? NgayPhieu { get; set; }

        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
    }
}