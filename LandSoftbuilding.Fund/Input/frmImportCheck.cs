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

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmImportCheck : DevExpress.XtraEditors.XtraForm
    {
        public frmImportCheck()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        List<int> GetMaLDVs(string _MaLDVs)
        {
            var ltMaLDV = new List<int>();

            try
            {
                var arrMaLDV = _MaLDVs.Split(',');
                if (arrMaLDV[0].Trim() != "")
                {
                    foreach (var i in arrMaLDV)
                    {
                        ltMaLDV.Add(int.Parse(i));
                    }
                }
            }
            catch { }

            return ltMaLDV;
        }

        private void frmImport_Load(object sender, EventArgs e)
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

                System.Collections.Generic.List<PhieuThuItem> list = Library.Import.ExcelAuto.ConvertDataTable<PhieuThuItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                //gcImport.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new PhieuThuItem
                //{
                //    NgayCT = p["Ngày chứng từ"].Cast<DateTime>(),
                //    SoCT = p["Số chứng từ"].ToString().Trim(),
                //    MaSoKH = p["Mã khách hàng"].ToString().Trim(),
                //    NguoiNop = p["Người nộp"].ToString().Trim(),
                //    DiaChi = p["Địa chỉ"].ToString().Trim(),
                //    DienGiai = p["Diễn giải"].ToString().Trim(),
                //    SoTien = p["Số tiền"].Cast<decimal>(),
                //    SoTKNH = p["Số TKNH"].ToString().Trim(),
                //    ChungTuGoc = p["Chứng từ gốc"].ToString().Trim(),
                //    TenPL = p["Phân loại"].ToString().Trim(),
                //    MaLDVs = p["MaLDV"].ToString().Trim(),
                //}).ToList();

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
            gvImport.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcImport.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                #region Nap tu dien 
                var ltPhieuThu = (from pt in db.ptPhieuThus
                                  where pt.MaTN == this.MaTN & pt.MaPL == 12
                                  select new { pt.ID, SoCT = pt.SoPT.ToLower(), pt.SoTien })
                                   .ToList();
                
                #endregion

                var ltSource = (List<PhieuThuItem>)gcImport.DataSource;
                var ltError = new List<PhieuThuItem>();
                //decimal _SoTien = 0;
                //decimal _TienCT = 0;

                foreach(var i in ltSource)
                {
                    try
                    {
                        var objPT= ltPhieuThu.FirstOrDefault(p=>p.SoCT == i.SoCT.ToLower() & p.SoTien == i.SoTien);
                        if (objPT == null)
                        {
                            ltError.Add(i);
                        }
                        else
                        {
                            
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                this.isSave = true;

                //DialogBox.Success(string.Format("{0:n0}", _TienCT));

                if (ltError.Count > 0)
                {
                    DialogBox.Alert(string.Format("Có {0:n0} dòng xảy ra lỗi", ltError.Count));
                     gcImport.DataSource = ltError;
                }
                else
                {
                    DialogBox.Success();
                    gcImport.DataSource = null;
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

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcImport);
        }
    }
}