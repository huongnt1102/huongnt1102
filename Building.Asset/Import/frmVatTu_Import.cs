using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Building.Asset.Import
{
    public partial class frmVatTu_Import : XtraForm
    {
        public short MaTn { get; set; }
        public bool IsSave { get; set; }

        public frmVatTu_Import()
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
                    System.Collections.Generic.List<VatTu> list = Library.Import.ExcelAuto.ConvertDataTable<VatTu>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

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
                var objCaTruc = (List<VatTu>) gc.DataSource;
                var ltError = new List<VatTu>();
                foreach (var n in objCaTruc)
                {
                    try
                    {
                        db = new MasterDataContext();

                        #region Kiểm tra

                        var objDvt = db.tbl_VatTu_DVTs.FirstOrDefault(_ => _.TenDVT.ToLower() == n.TenDVT.ToLower() & _.NgungSuDung==false);
                        if (objDvt == null)
                        {
                            n.Error = "Đơn vị tính không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objKn = db.mbKhoiNhas.FirstOrDefault(_ =>
                            _.TenKN.ToLower() == n.TenKN.ToLower() & _.MaTN == MaTn);
                        if (objKn == null)
                        {
                            n.Error = "Khối nhà không tồn tại";
                            ltError.Add(n);
                            continue;
                        }
                        #endregion

                        var kt = db.tbl_VatTus.FirstOrDefault(_ => _.KyHieu.ToLower() == n.KyHieu.ToLower()&_.MaTN==MaTn);
                        if (kt != null)
                        {
                            kt.Ten = n.Ten;
                            kt.ThongSoKyThuat = n.ThongSoKyThuat;
                            kt.MoTa = n.MoTa;
                            kt.NguyenGia = n.NguyenGia;
                            kt.GiaNhap = n.GiaNhap;
                            kt.GiaXuat = n.GiaXuat;
                            kt.BlockID = objKn.MaKN;
                            kt.ViTri = n.ViTri;
                            kt.DVTID = objDvt.ID;
                            kt.NgungSuDung = n.NgungSuDung;
                            kt.NgaySua = DateTime.Now;
                            kt.NguoiSua = Common.User.MaNV;
                        }
                        else
                        {
                            var objCt = new tbl_VatTu();
                            objCt.Ten = n.Ten;
                            objCt.ThongSoKyThuat = n.ThongSoKyThuat;
                            objCt.MoTa = n.MoTa;
                            objCt.NguyenGia = n.NguyenGia;
                            objCt.GiaNhap = n.GiaNhap;
                            objCt.GiaXuat = n.GiaXuat;
                            objCt.BlockID = objKn.MaKN;
                            objCt.ViTri = n.ViTri;
                            objCt.DVTID = objDvt.ID;
                            objCt.MaTN = (byte)MaTn;
                            objCt.KyHieu = n.KyHieu;
                            objCt.NgungSuDung = n.NgungSuDung;
                            objCt.NguoiNhap = Common.User.MaNV;
                            objCt.NgayNhap = DateTime.Now;
                            db.tbl_VatTus.InsertOnSubmit(objCt);
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

    public class VatTu
    {
        public string KyHieu { get; set; }
        public string Ten { get; set; }
        public string TenDVT { get; set; }
        public string ThongSoKyThuat { get; set; }
        public string MoTa { get; set; }
        public string TenKN { get; set; }
        public string ViTri { get; set; }

        public decimal? NguyenGia { get; set; }
        public decimal? GiaNhap { get; set; }
        public decimal? GiaXuat { get; set; }

        public string Error { get; set; }

        public bool? NgungSuDung { get; set; }
    }
}