using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using LinqToExcel;

namespace LandSoftBuilding.Lease.TaiSanBanGiao
{
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        public frmImport()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        private void frmImportLichThanhToan_Load(object sender, EventArgs e)
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

                System.Collections.Generic.List<HopDong_TaiSanBanGiao> list = Library.Import.ExcelAuto.ConvertDataTable<HopDong_TaiSanBanGiao>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

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
                var lHopDong = (from p in db.ctHopDongs orderby p.SoHDCT where p.MaTN == MaTN select new { p.ID, p.SoHDCT }).ToList();
                var lMatBang = (from p in db.mbMatBangs orderby p.MaSoMB where p.MaTN == MaTN select new { p.MaMB, p.MaSoMB }).ToList();
                var lLoaiTien = (from p in db.LoaiTiens orderby p.KyHieuLT select new { p.ID, p.KyHieuLT }).ToList();

                var lTaiSanBanGiao = (List<HopDong_TaiSanBanGiao>)gc.DataSource;
                var ltError = new List<HopDong_TaiSanBanGiao>();

                foreach(var n in lTaiSanBanGiao)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var objHopDong = lHopDong.FirstOrDefault(_ => _.SoHDCT.ToLower() == n.SoHDCT.ToLower());
                        if(objHopDong == null)
                        {
                            n.Error = "Hợp đồng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }


                        var model_ltt = new { BenGiao = n.BenGiao, BenNhan = n.BenNhan, ChucVu_Giao = n.ChucVu_Giao, ChucVu_Nhan = n.ChucVu_Nhan, DiaChi_Giao = n.DiaChi_Giao, DiaChi_Nhan = n.DiaChi_Nhan, GhiChu = n.GhiChu, HangMuc = n.HangMuc, HopDong = objHopDong.ID, MaBanGiao = n.MaBanGiao, NgayGiao = n.NgayGiao, NgayNhan = n.NgayNhan, NguoiDaiDien_Giao = n.NguoiDaiDien_Giao, NguoiDaiDien_Nhan = n.NguoiDaiDien_Nhan, NguoiNhap = Library.Common.User.MaNV, NhanHieu = n.NhanHieu, SoLuong = n.SoLuong, TrangThai = n.TrangThai };
                        var param_ltt = new Dapper.DynamicParameters();
                        param_ltt.AddDynamicParams(model_ltt);
                        var result = Library.Class.Connect.QueryConnect.Query<bool>("ct_TaiSanBanGiao_Insert", param_ltt);
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

        public class HopDong_TaiSanBanGiao
        {
            public string SoHDCT { get; set; }
            public string MaBanGiao { get; set; }
            public string BenGiao { get; set; }
            public string DiaChi_Giao { get; set; }
            public string NguoiDaiDien_Giao { get; set; }
            public string ChucVu_Giao { get; set; }
            public string BenNhan { get; set; }
            public string DiaChi_Nhan { get; set; }
            public string NguoiDaiDien_Nhan { get; set; }
            public string ChucVu_Nhan { get; set; }
            public DateTime? NgayGiao { get; set; }
            public DateTime? NgayNhan { get; set; }
            public string HangMuc { get; set; }
            public decimal? SoLuong { get; set; }
            public string NhanHieu { get; set; }
            public string TrangThai { get; set; }
            public string GhiChu { get; set; }
            public string Error { get; set; }
        }
    }
}