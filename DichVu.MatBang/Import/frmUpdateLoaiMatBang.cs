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

namespace DichVu.MatBang.Import
{
    public partial class frmUpdateLoaiMatBang : DevExpress.XtraEditors.XtraForm
    {
        public frmUpdateLoaiMatBang()
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

            using (var db = new MasterDataContext())
            {
                lkLMB.DataSource = db.mbLoaiMatBangs.Where(p=>p.MaTN == this.MaTN);
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
                System.Collections.Generic.List<MatBangItem> list = Library.Import.ExcelAuto.ConvertDataTable<MatBangItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvImport, itemSheet));

                gcImport.DataSource = list;

                //gcImport.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new MatBangItem
                //{
                //    MaSoMB = p["Mã mặt bằng"].ToString().Trim(),
                //    MaSoKH = p["Mã khách hàng"].ToString().Trim(),
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
                var ltSource = (List<MatBangItem>)gcImport.DataSource;
                var ltError = new List<MatBangItem>();

                foreach(var i in ltSource)
                {
                    
                    try
                    {
                        var objKH = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == i.MaSoKH & p.MaTN == this.MaTN);
                        var objMB = db.mbMatBangs.FirstOrDefault(p => p.MaSoMB == i.MaSoMB & p.MaTN == this.MaTN);

                        //Update loai mat bang
                        objMB.MaLMB = (int)itemLoaiMatBang.EditValue;

                        //update dich vu co ban
                        foreach(var dv in db.dvDichVuKhacs.Where(p=>p.MaTN == this.MaTN & p.MaMB == objMB.MaMB & p.MaLDV == 13))
                        {
                            dv.MaKH = objKH.MaKH;
                        }

                        //Update hoa don
                        foreach (var hd in db.dvHoaDons.Where(p => p.MaTN == this.MaTN & p.MaMB == objMB.MaMB & p.MaLDV == 13))
                        {
                            hd.MaKH = objKH.MaKH;
                        }
                    }
                    catch (Exception ex)
                    {
                        i.Error = ex.Message;
                        ltError.Add(i);
                    }
                }

                db.SubmitChanges();

                this.isSave = true;

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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcImport);
        }
    }


}