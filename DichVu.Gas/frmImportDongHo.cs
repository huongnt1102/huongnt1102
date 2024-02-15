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

namespace DichVu.Gas
{
    public partial class frmImportDongHo : DevExpress.XtraEditors.XtraForm
    {
        public frmImportDongHo()
        {
            InitializeComponent();
        }

        public byte MaTN { get; set; }
        public bool isSave { get; set; }

        private void frmImportDongHo_Load(object sender, EventArgs e)
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

                System.Collections.Generic.List<DongHoItem> list = Library.Import.ExcelAuto.ConvertDataTable<DongHoItem>(Library.Import.ExcelAuto.GetDataExcel(excel, gvDongHo, itemSheet));

                gcDongHo.DataSource = list;

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
            gvDongHo.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gcDongHo.DataSource == null)
            {
                DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {

                var ltMatBang = (from mb in db.mbMatBangs
                                 where mb.MaTN == this.MaTN
                                 orderby mb.MaSoMB
                                 select new { mb.MaMB, MaSoMB = mb.MaSoMB.ToLower(), mb.MaKH }).ToList();

                var ltDongHo = (from dh in db.dvGasDongHos
                                where dh.MaTN == this.MaTN
                                orderby dh.SoDH
                                select dh.SoDH.ToLower()).ToList();


                var list = (List<DongHoItem>)gcDongHo.DataSource;
                var ltError = new List<DongHoItem>();

                foreach(var n in list)
                {
                    db = new MasterDataContext();
                    try
                    {
                        var objMB = ltMatBang.FirstOrDefault(p => p.MaSoMB == n.MaSoMB.ToLower());
                        if (objMB == null)
                        {
                            n.Error = "Mặt bằng không tồn tại trong hệ thống";
                            ltError.Add(n);
                            continue;
                        }

                        if (string.IsNullOrEmpty(n.SoDH))
                        {
                            n.Error = "Vui lòng nhập số đồng hồ";
                            ltError.Add(n);
                            continue;
                        }
                        else if(ltDongHo.Contains(n.SoDH.ToLower()))
                        {
                            n.Error = "Số đồng hồ đã tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        var objDH = new dvGasDongHo();
                        objDH.MaTN = this.MaTN;
                        objDH.MaMB = objMB.MaMB;
                        objDH.SoDH = n.SoDH;
                        objDH.HeSo = n.HeSo <= 0 ? 1 : n.HeSo;
                        objDH.DienGiai = n.DienGiai;

                        db.dvGasDongHos.InsertOnSubmit(objDH);
                        db.SubmitChanges();

                        ltDongHo.Add(n.SoDH);
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
                    gcDongHo.DataSource = ltError;
                }
                else
                {
                    gcDongHo.DataSource = null;
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
            Library.Commoncls.ExportExcel(gcDongHo);
        }
    }

    public class DongHoItem
    {
        public string MaSoMB { get; set; }
        public string SoDH { get; set; }
        public int? HeSo { get; set; }
        public string DienGiai { get; set; }
        public string Error { get; set; }
    }
}