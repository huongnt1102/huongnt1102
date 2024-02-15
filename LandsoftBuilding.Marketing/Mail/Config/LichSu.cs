using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandSoftBuilding.Marketing.Mail.Config
{
    public partial class LichSu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public LichSu()
        {
            InitializeComponent();
        }
        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }
        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                gcMailHistory.DataSource = (from m in db.mailHistories
                                            join c in db.mailConfigs on m.MailID equals c.ID
                                            join n in db.tnNhanViens on m.StaffCreate equals n.MaNV
                                            join s in db.tnNhanViens on m.StaffModify equals s.MaNV into nv
                                            from s in nv.DefaultIfEmpty()
                                            join mb in db.mbMatBangs on m.CusID equals mb.MaKH
                                            orderby m.DateCreate descending
                                            where SqlMethods.DateDiffDay(tuNgay, m.DateCreate) >= 0 & SqlMethods.DateDiffDay(m.DateCreate, denNgay) >= 0
                                            select new
                                            {
                                                m.ID,
                                                m.Subject,
                                                mb.MaSoMB,
                                                FromMail = c.Email,
                                                m.ToMail,
                                                m.CcMail,
                                                m.BccMail,
                                                m.Contents,
                                                Status = m.Status == 2 ? "Thất bại" : "Thành công",
                                                NameCreate = n.HoTenNV,
                                                m.DateCreate,
                                                NameModify = s.HoTenNV,
                                                m.DateModify
                                            }).ToList();
            }
            catch
            {
                gcMailHistory.DataSource = null;
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);
            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.MaTN = (byte?)itemToaNha.EditValue ?? Common.User.MaTN;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gvConfig.FocusedRowHandle < 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn dòng cần sửa. Xin cám ơn!");
            //    return;
            //}

            //var frm = new frmEdit();
            //frm.MailID = (int?)gvConfig.GetFocusedRowCellValue("ID");
            //frm.ShowDialog();
            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    this.LoadData();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var rows = gvConfig.GetSelectedRows();
            //if (rows.Length <= 0)
            //{
            //    DialogBox.Alert("Vui lòng chọn dòng cần xóa. Xin cám ơn!");
            //    return;
            //}

            //if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            //db = new MasterDataContext();
            //try
            //{
            //    foreach (var i in rows)
            //    {
            //        var obj = db.mailConfigs.Single(p => p.ID == (int?)gvConfig.GetRowCellValue(i, "ID"));
            //        db.mailConfigs.DeleteOnSubmit(obj);
            //    }

            //    db.SubmitChanges();
            //    gvConfig.DeleteSelectedRows();
            //}
            //catch (Exception ex)
            //{
            //    DialogBox.Error(ex.Message);
            //}
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcMailHistory);
        }
    }
}