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

namespace LandSoftBuilding.Lease.TOS
{
    public partial class frmImportSales : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        public frmImportSales()
        {
            InitializeComponent();
        }

        bool IsSoNguyen(decimal val)
        {
            try
            {
                Convert.ToInt32(val);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog(); 
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
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
            { file.Dispose(); }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gcHD.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(this.Tag.ToString());

                    System.Collections.Generic.List<HopDongThueItem> list = Library.Import.ExcelAuto.ConvertDataTable<HopDongThueItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvHD, itemSheet));

                    gcHD.DataSource = list;

                    excel = null;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
        }

        private int ConvertInt(string val)
        {
            try
            {
                var Int = Convert.ToInt32(val);
                return Int;
            }
            catch
            {
                return 0;
            }
        }
        private decimal Convertdecimal(string val)
        {
            try
            {
                var Int = Convert.ToDecimal(val);
                return Int;
            }
            catch
            {
                return 0;
            }
        }
        private DateTime ConvertDate(string value)
        {
            try
            {
                //return value.Cast<DateTime>();
                // return DateTime.FromOADate(Convert.ToInt64(value));
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            grvHD.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcHD.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltHopDong = (List<HopDongThueItem>)gcHD.DataSource;
                var ltError = new List<HopDongThueItem>();
                foreach (var n in ltHopDong)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var hopDong = db.ctHopDongs.FirstOrDefault(_ => _.SoHDCT.ToLower() == n.HopDong.ToLower());
                        if(hopDong == null)
                        {
                            n.Error = "Hợp đồng không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var matBang = db.mbMatBangs.FirstOrDefault(_ => _.MaSoMB.ToLower() == n.MatBang.ToLower());
                        if(matBang == null)
                        {
                            n.Error = "Mặt bằng không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var loaiTien = db.LoaiTiens.FirstOrDefault(_ => _.KyHieuLT.ToLower() == n.LoaiTien.ToLower());
                        if(loaiTien == null)
                        {
                            n.Error = "Loại tiền không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var model = new { MaHD = hopDong.ID, MaMB = matBang.MaMB, DoanhThuThucTe = n.Sales, TuNgay = n.TuNgay, DenNgay = n.DenNgay, NgayTT = n.NgayTT, DienGiai = n.DienGiai, LoaiTien = n.LoaiTien, TyGia = n.TyGia };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        Library.Class.Connect.QueryConnect.Query<bool>("lease_frmImport_ctChiTiet_UpdateDoanhSo", param);

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
                    gcHD.DataSource = ltError;
                }
                else
                {
                    gcHD.DataSource = null;
                }
            }
            catch
            {
                wait.Close();
                DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                this.Close();
            }
            finally { wait.Dispose(); db.Dispose(); }
        }

        private void frmImport_Load(object sender, EventArgs e)
        {

        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcHD);
        }

        public class HopDongThueItem
        {
            public string LoaiTien { get; set; }
            public string HopDong { get; set; }
            public string MatBang { get; set; }
            public decimal? Sales { get; set; }
            public decimal? TyGia { get; set; }
            public DateTime? TuNgay { get; set; }
            public DateTime? DenNgay { get; set; }
            public DateTime? NgayTT { get; set; }
            public string DienGiai { get; set; }
            public string Error { get; set; }
        }
    }


}