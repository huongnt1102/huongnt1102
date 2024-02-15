using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.Linq;

namespace Building.SystemLog
{
    public partial class ctlSysLog : DevExpress.XtraEditors.XtraForm
    {
        Library.MasterDataContext db = new Library.MasterDataContext();
        public ctlSysLog()
        {
            InitializeComponent();
            this.Init();
            this.bm.SetPopupContextMenu(this.gcList, this.pm);
            this.Fromdate.DateTime = DateTime.Now;
            this.Todate.DateTime = DateTime.Now;
            this.LoadLog();
        }
        private void bbiClear_ItemClick(object sender, ItemClickEventArgs e)
        {

            DIPBMS.SystemLog.Classes.SYS_LOG sysLog = new DIPBMS.SystemLog.Classes.SYS_LOG();
            for (int i = 0; i < gbList.RowCount; i++)
            {
                sysLog.Delete(Convert.ToInt64(gbList.GetRowCellValue(i,"SYS_ID").ToString()));
            }
            this.LoadLog();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (base.ParentForm != null)
            {
                base.ParentForm.Close();
            }
        }

        private void bbiDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            object arg;
            if (this.gbList.SelectedRowsCount > 0)
            {
                int[] selectrow = this.gbList.GetSelectedRows();
                for (int i = selectrow.Length; i > 0; i--)
                {
                    arg = this.gbList.GetRowCellValue(selectrow[i - 1], "SYS_ID");
                    if (arg != null)
                    {
                        DIPBMS.SystemLog.Classes.SYS_LOG sysLog = new DIPBMS.SystemLog.Classes.SYS_LOG();
                        sysLog.Delete(Convert.ToInt64(arg));
                    }
                }
            }
            else
            {
                arg = this.gbList.GetFocusedRowCellValue("SYS_ID");
                if (arg == null)
                {
                    return;
                }
                new DIPBMS.SystemLog.Classes.SYS_LOG().Delete(Convert.ToInt64(arg));
            }
            this.LoadLog();
        }

        private void bbiExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.btnExport_Click(sender, e);
        }

        private void bbiOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog initLocal1 = new OpenFileDialog();
            initLocal1.Filter = "Xml File(*.xml)|*.xml|All File(*.*)|*.*";
            OpenFileDialog fileDialog = initLocal1;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                //if (fileInfo.Exists)
                //{
                //    this.dsSysLog.SYS_LOG.ReadXml(fileInfo.FullName);
                //}
            }
        }

        private void bbiOtherView_Popup(object sender, EventArgs e)
        {
        }

        private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.btnView_Click(sender, e);
        }

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog initLocal0 = new SaveFileDialog();
            initLocal0.Filter = "Xml File(*.xml)|*.xml|All File(*.*)|*.*";
            SaveFileDialog fileDialog = initLocal0;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //this.dsSysLog.SYS_LOG.WriteXml(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi:\n\t" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.pcc.HidePopup();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (base.ParentForm != null)
            {
                base.ParentForm.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.bbiDelete_ItemClick(this, null);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //if (MyRule.IsExport("bbiInventory"))
            //{
            //    base.ExportView = this.gbList;
            //    base.Export();
            //}
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
          //  this.bbiExport_ItemClick(this, null);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
          //  this.bbiOpen_ItemClick(this, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           // this.bbiSave_ItemClick(this, null);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.LoadLog();
            //if (this.dsSysLog.SYS_LOG.Rows.Count <= 0)
            //{
            //    XtraMessageBox.Show("Kh\x00f4ng c\x00f3 dữ liệu n\x00e0o trong khoảng thời gian n\x00e0y", "Th\x00f4ng B\x00e1o", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //}
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.btnStart_Click(sender, e);
        }

        private void btnView_Click_1(object sender, EventArgs e)
        {
            this.btnStart_Click(sender, e);
        }
        private void Init()
        {
            //SYS_LOGTableAdapter adapter = this.sYS_LOGTableAdapter;
            //adapter.Connection.ConnectionString = SqlHelper.ConnectString;
            //adapter.Fill(this.dsSysLog.SYS_LOG);
            DIPBMS.SystemLog.Classes.SYS_LOG sysLog=new DIPBMS.SystemLog.Classes.SYS_LOG();
            gcList.DataSource = sysLog.GetList(this.Fromdate.DateTime, this.Todate.DateTime);

            }

        private void LoadLog()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG sysLog = new DIPBMS.SystemLog.Classes.SYS_LOG();
            gcList.DataSource = sysLog.GetList(this.Fromdate.DateTime, this.Todate.DateTime);
            //SysLog sysLog = new SysLog();
            //this.dsSysLog.SYS_LOG.Rows.Clear();
            //this.dsSysLog.SYS_LOG.Merge(sysLog.GetList(this.Fromdate.DateTime, this.Todate.DateTime));
        }
    }
}
