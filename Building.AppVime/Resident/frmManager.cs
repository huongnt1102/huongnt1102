using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;

namespace Building.AppVime.Resident
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public byte TowerId { get; set; }
        public List<ResidentChoiceModel> ListResident;
        private int BlockId { get; set; }
        private int AmountResident { get; set; }
        List<ResidentModel> ListInternal;

        public frmManager()
        {
            InitializeComponent();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = TowerId;

            lookUpEditStatus.DataSource = new ResidentStatusBindingModel().GetData();

            using (var db = new MasterDataContext())
            {
                var blocks = db.mbKhoiNhas.Where(p => p.MaTN == Convert.ToByte(itemToaNha.EditValue))
                    .Select(p => new
                    {
                        Id = p.MaKN,
                        Name = p.TenKN
                    }).ToList();
                blocks.Insert(0, new { Id = 0, Name = "Tất cả" });

                lookUpBlock.DataSource = blocks;
                itemBlock.EditValue = 0;
            }

            LoadData();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                gcResident.DataSource = null;
                using (var db = new MasterDataContext())
                {
                    List<ApartmentModel> listApartment = new List<ApartmentModel>();

                    if (BlockId > 0)
                    {
                        //Theo khu/ block
                        listApartment = (from mb in db.mbMatBangs
                                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                         join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                         where mb.MaTN == TowerId && tl.MaKN == BlockId
                                         select new ApartmentModel
                                         {
                                             Code = mb.MaSoMB,
                                             Id = mb.MaMB,
                                             Phone = kh.DienThoaiKH,
                                             Floor = tl.TenTL
                                         }).ToList();
                    }
                    else
                    {
                        //Theo tòa nhà
                        listApartment = (from mb in db.mbMatBangs
                                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                         join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                         where mb.MaTN == TowerId
                                         select new ApartmentModel
                                         {
                                             Code = mb.MaSoMB,
                                             Id = mb.MaMB,
                                             Phone = kh.DienThoaiKH,
                                             Floor = tl.TenTL
                                         }).ToList();
                    }


                    var listData = (from r in db.app_Residents
                                    join re in db.app_ResidentTowers on r.Id equals re.ResidentId
                                    join nvn in db.tnNhanViens on re.EmployeeId equals nvn.MaNV
                                    from nvs in db.tnNhanViens.Where(nvs => nvs.MaNV == re.EmployeeIdUpdate).DefaultIfEmpty()
                                    where re.TowerId == TowerId && r.IsResident.GetValueOrDefault()
                                    orderby r.DateOfCreate
                                    select new ResidentModel
                                    {
                                        IsCheck = false,
                                        Id = re.Id,
                                        AmountNewsUnread = r.AmountNewsUnread ?? 0,
                                        AmountNotifyUnread = r.AmountNotifyUnread ?? 0,
                                        DateOfJoin = re.DateOfJoin,
                                        DateOfProcess = re.DateOfProcess,
                                        DescriptionProcess = re.DescriptionProcess,
                                        Employeer = nvn.HoTenNV,
                                        FullName = r.FullName,
                                        StatusId = re.TypeId ?? 0,
                                        TypeId = re.TypeId,
                                        Phone = r.Phone,
                                        EmployeerProcess = re.EmployeeIdUpdate != null ? nvs.HoTenNV : "",
                                        ResidentId = re.ResidentId
                                    }).ToList();
                    ListInternal = new List<ResidentModel>();
                    foreach (var item in listData)
                    {
                        var obj = listApartment.FirstOrDefault(p => p.Phone == item.Phone);
                        if (obj != null)
                        {
                            item.Code = obj.Code;
                            item.Floor = obj.Floor;
                            ListInternal.Add(item);
                        }
                    }

                    gcResident.DataSource = ListInternal;

                    AmountResident = ListInternal.Count;
                }
            }
            catch (Exception ex)
            {
                wait.Close();
                wait.Dispose();
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void itemChoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListResident = new List<ResidentChoiceModel>();

            gvResident.RefreshData();
            gvResident.FocusedColumn = colFullname;
            gvResident.FocusedRowHandle = gvResident.FocusedRowHandle - 1;

            if (chkAll.Checked)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn chọn [Tất cả] dòng không?") == DialogResult.No) return;
            }
            
            for (int i = 0; i < gvResident.RowCount; i++)
            {
                if (chkAll.Checked)
                {
                    var obj = new ResidentChoiceModel();
                    obj.Id = (decimal)gvResident.GetRowCellValue(i, "ResidentId");
                    obj.FullName = gvResident.GetRowCellValue(i, "FullName").ToString();
                    obj.Phone = gvResident.GetRowCellValue(i, "Phone").ToString();

                    ListResident.Add(obj);
                }
                else
                {
                    if ((bool)gvResident.GetRowCellValue(i, "IsCheck"))
                    {
                        var obj = new ResidentChoiceModel();
                        obj.Id = (decimal)gvResident.GetRowCellValue(i, "ResidentId");
                        obj.FullName = gvResident.GetRowCellValue(i, "FullName").ToString();
                        obj.Phone = gvResident.GetRowCellValue(i, "Phone").ToString();

                        ListResident.Add(obj);
                    }
                }
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        int CountRowSelected()
        {
            int amount = 0;
            for (int i = 0; i < gvResident.RowCount; i++)
            {
                if ((bool)gvResident.GetRowCellValue(i, "IsCheck"))
                {
                    amount++;
                }
            }

            return amount;
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvResident_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //var isCheck = (bool)gvResident.GetFocusedRowCellValue("IsCheck");
            //gvResident.SetFocusedRowCellValue("IsCheck", !isCheck);

            //gvResident.RefreshData();
        }

        private void itemBlock_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlockId = Convert.ToInt32(itemBlock.EditValue);

                LoadData();
            }
            catch { BlockId = 0; }
        }

        void LoadDataUpdate(bool val)
        {
            foreach (var item in ListInternal)
            {
                item.IsCheck = val;
            }

            gcResident.DataSource = ListInternal;

            gvResident.RefreshData();
            gvResident.FocusedColumn = colFullname;
            gvResident.FocusedRowHandle = gvResident.FocusedRowHandle - 1;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //LoadDataUpdate(chkAll.Checked);
        }
    }
}
