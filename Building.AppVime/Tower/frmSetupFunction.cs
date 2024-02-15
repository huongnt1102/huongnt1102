using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.Tower
{
    public partial class frmSetupFunction : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        List<TowerModel> ListChoice;
        List<TowerModel> ListSource;
        List<short> ListId;
        public byte TowerId { get; set; }

        public frmSetupFunction()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var list = db.app_TowerSettingFunctions.Where(p => p.TowerId == TowerId).ToList();
            db.app_TowerSettingFunctions.DeleteAllOnSubmit(list);

            var listChoice = new List<app_TowerSettingFunction>();
            for (int i = 0; i < gvFunctionChoice.RowCount; i++)
            {
                listChoice.Add(new app_TowerSettingFunction()
                {
                    FunctionId = (short)gvFunctionChoice.GetRowCellValue(i, "FunctionId"),
                    Id = Guid.NewGuid(),
                    IndexNumber = (byte)gvFunctionChoice.GetRowCellValue(i, "IndexNumber"),
                    TowerId = TowerId
                });
            }

            db.app_TowerSettingFunctions.InsertAllOnSubmit(listChoice);
            try
            {
                db.SubmitChanges();

                DialogResult = DialogResult.OK;
            }
            catch { }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetupFunction_Load(object sender, EventArgs e)
        {
            try
            {
                ListChoice = (from p in db.app_TowerSettingFunctions
                              join f in db.app_Functions on p.FunctionId equals f.Id
                              where p.TowerId == TowerId
                              orderby p.IndexNumber
                              select new TowerModel()
                              {
                                  FunctionId = p.FunctionId ?? 0,
                                  TowerId = p.TowerId ?? 0,
                                  IndexNumber = p.IndexNumber ?? 0,
                                  Name = f.Name
                              }).ToList();

                ListId = ListChoice.Select(p => p.FunctionId).ToList();

                gcFunctionChoice.DataSource = ListChoice;

                ListSource = (from f in db.app_Functions
                              where !ListId.Contains(f.Id)
                              select new TowerModel()
                              {
                                  FunctionId = f.Id,
                                  TowerId = TowerId,
                                  IndexNumber = 0,
                                  Name = f.Name
                              }).ToList();
                gcFunction.DataSource = ListSource;
            }
            catch
            {
                this.Close();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int[] indexs = gvFunctionChoice.GetSelectedRows();

            ListId = new List<short>();
            foreach (var i in indexs)
            {
                try
                {
                    short id = (short)gvFunctionChoice.GetRowCellValue(i, "FunctionId");
                    ListId.Add(id);
                }
                catch (Exception ex) { }
            }

            var list = ListChoice.Where(p => ListId.Contains(p.FunctionId)).ToList();
            ListSource.AddRange(list);
            gcFunction.RefreshDataSource();

            foreach (var id in ListId)
            {
                var obj = ListSource.FirstOrDefault(p => p.FunctionId == id);
                ListChoice.Remove(obj);
            }
            gcFunctionChoice.RefreshDataSource();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int[] indexs = gvFunction.GetSelectedRows();
            ListId = new List<short>();

            foreach (var i in indexs)
            {
                try
                {
                    short id = (short)gvFunction.GetRowCellValue(i, "FunctionId");
                    ListId.Add(id);
                }
                catch (Exception ex) { }
            }

            var list = ListSource.Where(p => ListId.Contains(p.FunctionId)).ToList();
            ListChoice.AddRange(list);
            gcFunctionChoice.RefreshDataSource();

            foreach (var id in ListId)
            {
                var obj = ListChoice.FirstOrDefault(p => p.FunctionId == id);
                ListSource.Remove(obj);
            }
            gcFunction.RefreshDataSource();
        }
    }
}
