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

namespace LandSoftBuilding.Lease.Liquidate
{
    public partial class frmImportLichThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public frmImportLichThanhToan()
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

                System.Collections.Generic.List<LichThanhToan_Import> list = Library.Import.ExcelAuto.ConvertDataTable<LichThanhToan_Import>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

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

                var ltLichThanhToan = (List<LichThanhToan_Import>)gc.DataSource;
                var ltError = new List<LichThanhToan_Import>();

                foreach(var n in ltLichThanhToan)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var objHopDong = lHopDong.FirstOrDefault(_ => _.SoHDCT.ToLower() == n.HopDong.ToLower());
                        if(objHopDong == null)
                        {
                            n.Error = "Hợp đồng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objMatBang = lMatBang.FirstOrDefault(_ => _.MaSoMB.ToLower() == n.MatBang.ToLower());
                        if (objMatBang == null)
                        {
                            n.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        var objLoaiTien = lLoaiTien.FirstOrDefault(_ => _.KyHieuLT.ToLower() == n.LoaiTien.ToLower());
                        if(objLoaiTien == null)
                        {
                            n.Error = "Loại tiền không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        if(n.Dot == 0)
                        {
                            n.Error = "Đợt thanh toán không hợp lý";
                            ltError.Add(n);
                            continue;
                        }

                        var model_ltt = new { HopDong = objHopDong.ID, MatBang = objMatBang.MaMB, Dot = n.Dot, DienGiai = n.DienGiai, TuNgay = n.TuNgay, DenNgay = n.DenNgay, SoThang = n.SoThang, DienTich = n.DienTich, DonGia = n.DonGia, TienTruocThue = n.TienTruocThue, TienSauThue = n.TienSauThue, LoaiTien = objLoaiTien.ID, TienQuyDoi = n.TienQuyDoi };
                        var param_ltt = new Dapper.DynamicParameters();
                        param_ltt.AddDynamicParams(model_ltt);
                        var result = Library.Class.Connect.QueryConnect.Query<bool>("ltt_LichThanhToan_Insert", param_ltt);

                        if(result.First() == false)
                        {
                            n.Error = "Đã tồn tại hóa đơn";
                            ltError.Add(n);
                            continue;
                        }
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

        public class LichThanhToan_Import
        {
            public string HopDong { get; set; }
            public string MatBang { get; set; }
            public int? Dot { get; set; }
            public string DienGiai { get; set; }
            public DateTime? TuNgay { get; set; }
            public DateTime? DenNgay { get; set; }
            public decimal? SoThang { get; set; }
            public decimal? DienTich { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? TienTruocThue { get; set; }
            public decimal? TienSauThue { get; set; }
            public string LoaiTien { get; set; }
            public decimal? TienQuyDoi { get; set; }
            public string Error { get; set; }
        }
    }
}