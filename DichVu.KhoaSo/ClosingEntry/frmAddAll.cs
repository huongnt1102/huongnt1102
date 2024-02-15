using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace DichVu.KhoaSo.ClosingEntry
{
    public partial class frmAddAll : DevExpress.XtraEditors.XtraForm
    {
        public int? Id { get; set; }
        public frmAddAll()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            var db = new Library.MasterDataContext();
            chkSerive.Properties.DataSource = db.bcServices;
            lkTower.Properties.DataSource = Library.Common.TowerList;
            lkTower.EditValue = Library.Common.User.MaTN;

            var objKbc = new KyBaoCao();

            foreach (var v in objKbc.Source)
            {
                cbxPeriod.Properties.Items.Add(v);
            }

            cbxPeriod.EditValue = objKbc.Source[3];
            SetDate(3);

            // Sửa phiếu
            if(Id != null)
            {
                var objClosing = db.bcBookClosings.FirstOrDefault(_ => _.Id == Id);
                if(objClosing != null)
                {
                    var objService = db.bcServices.FirstOrDefault(_ => _.Id == objClosing.ServiceId);
                    if(objService != null)
                    {
                        int positionService = chkSerive.FindString(objService.TableName);
                        if (positionService >= 0)
                        {
                            chkSerive.Properties.Items[positionService - 1].CheckState = CheckState.Checked;
                        }

                        chkSerive.EditValue = objClosing.ServiceId;
                    }
                    
                    lkTower.EditValue = objClosing.TowerId;

                    cbxPeriod.EditValue = objKbc.Source[objClosing.PeriodId??0];

                    dateDateFrom.EditValue = objClosing.DateFrom;
                    dateDateTo.EditValue = objClosing.DateTo;

                    Lock(true);
                }
            }
        }

        public void Lock(bool isLock)
        {
            lkTower.ReadOnly = isLock;
            chkSerive.ReadOnly = isLock;
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var towerId = (byte?)lkTower.EditValue;
            if (towerId == null)
            {
                Library.DialogBox.Alert("Vui lòng chọn Dự án");
            }

            var strService = (chkSerive.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var lsSerice = strService.Split(',');

            if (strService == "")
            {
                Library.DialogBox.Alert("Vui lòng chọn dịch vụ");
                return;
            }

            foreach (var item in lsSerice)
            {
                var model = new
                {
                    Id = Id,
                    TowerId = towerId,
                    DateFrom = dateDateFrom.DateTime,
                    DateTo = dateDateTo.DateTime,
                    ServiceId = int.Parse(item),
                    UserId = Common.User.MaNV,
                    PeriodId = cbxPeriod.SelectedIndex
                };
                Library.Class.Connect.QueryConnect.QueryData<bool>("bcClosingEdit", model);
            }

            Library.DialogBox.Success("Lưu dữ liệu thành công.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void itemHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbxPeriod_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao()
            {
                Index = index
            };
            objKbc.SetToDate();
            dateDateFrom.EditValue = objKbc.DateFrom;
            dateDateTo.EditValue = objKbc.DateTo;
        }
    }
}