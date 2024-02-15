using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq.SqlClient;
using Library;
using System.Linq;
using LinqToExcel;

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmImportNguoiLienHe : DevExpress.XtraEditors.XtraForm
    {
        public frmImportNguoiLienHe()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        private void frmImportNguoiLienHe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();

                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
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

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<NguoiLienHe> list = Library.Import.ExcelAuto.ConvertDataTable<NguoiLienHe>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                gc.DataSource = list;

                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
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
                var lBoPhan = (from p in db.tnKhachHang_NguoiLienHe_BoPhans orderby p.TenBoPhan select new { p.ID, TenBoPhan = p.TenBoPhan.ToLower() }).ToList();
                var lNhomLienHe = (from p in db.tnKhachHang_NguoiLienHe_NhomLienHes orderby p.TenNhomLienHe select new { p.ID, TenNhomLienHe = p.TenNhomLienHe.ToLower() }).ToList();
                var lKhachHang = (from p in db.tnKhachHangs orderby p.KyHieu select new { p.MaKH, KyHieu = p.KyHieu.ToLower() }).ToList();

                var ltNguoiLienHe = (List<NguoiLienHe>)gc.DataSource;
                var ltError = new List<NguoiLienHe>();

                foreach(var n in ltNguoiLienHe)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var objBoPhan = lBoPhan.FirstOrDefault(_ => _.TenBoPhan == n.BoPhan.ToLower());
                        if(objBoPhan == null)
                        {
                            n.Error = "Bộ phận không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objKhachHang = lKhachHang.FirstOrDefault(_ => _.KyHieu == n.KhachHang.ToLower());
                        if(objKhachHang == null)
                        {
                            n.Error = "Khách hàng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objNhomLienHe = lNhomLienHe.FirstOrDefault(_ => _.TenNhomLienHe == n.NhomLienHe.ToLower());
                        if(objNhomLienHe == null)
                        {
                            n.Error = "Nhóm liên hệ không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objNguoiLienHe = db.tnKhachHang_NguoiLienHes.FirstOrDefault(_ => _.HoVaTen.ToLower() == n.HoVaTen.ToLower());
                        if(objNguoiLienHe == null)
                        {
                            objNguoiLienHe = new tnKhachHang_NguoiLienHe();
                            db.tnKhachHang_NguoiLienHes.InsertOnSubmit(objNguoiLienHe);
                        }

                        objNguoiLienHe.DiaChi = n.DiaChi;
                        objNguoiLienHe.DienThoai = n.DienThoai;
                        objNguoiLienHe.Email = n.Email;
                        objNguoiLienHe.GhiChu = n.GhiChu;
                        objNguoiLienHe.HoVaTen = n.HoVaTen;
                        objNguoiLienHe.MaBoPhan = objBoPhan.ID;
                        objNguoiLienHe.MaKH = objKhachHang.MaKH;
                        objNguoiLienHe.MaNhomLH = objNhomLienHe.ID;
                        objNguoiLienHe.MatKhau = n.MatKhau;
                        objNguoiLienHe.TaiKhoan = n.TaiKhoan;

                        db.SubmitChanges(); 
                    }
                    catch (Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                this.isSave = true;
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
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
                db.Dispose();
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

        public class NguoiLienHe
        {
            public string KhachHang { get; set; }
            public string HoVaTen { get; set; }
            public string Email { get; set; }
            public string DienThoai { get; set; }
            public string BoPhan { get; set; }
            public string DiaChi { get; set; }
            public string GhiChu { get; set; }
            public string NhomLienHe { get; set; }
            public string TaiKhoan { get; set; }
            public string MatKhau { get; set; }
            public string Error { get; set; }
        }
    }
}