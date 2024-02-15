using Library;
using System;
using System.Linq;

namespace LandSoftBuilding.Lease.DatCoc.ThiCong
{
    public partial class frmTrangThaiDatCoc : DevExpress.XtraEditors.XtraForm
    {
        public string Note { get; set; }

        private Guid ID ;
        public int? ID_TrangThai {get;set;}
        private MasterDataContext db = null;

        public frmTrangThaiDatCoc(Guid? _ID, int? _ID_TrangThai)
        {
            InitializeComponent();
            if (_ID != null)
            {
                this.ID = (Guid) _ID;
                this.ID_TrangThai = (int) _ID_TrangThai;
            }

        }

        private void frmTrangThaiDatCoc_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            try
            {
                // Load trang thai
                lkTrangThai.Properties.DataSource = db.dkTrangThais;//.Where(p=>p.ID == 1 || p.ID == 2);
                lkTrangThai.EditValue = ID_TrangThai;
            }
            catch{
            }
            
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var dbo = new MasterDataContext())
            {
                if (ID != null)
                {
                    var objTC = dbo.dkThiCongs.FirstOrDefault(p => p.Id == ID);
                    if (objTC != null)
                    {
                        objTC.MaTT = Convert.ToInt32(lkTrangThai.EditValue);

                        if (Convert.ToInt32(lkTrangThai.EditValue) == 9)
                        {
                            dbo.SubmitChanges();
                        }
                        else if (Convert.ToInt32(lkTrangThai.EditValue) == 5)
                        {
                            dbo.SubmitChanges();

                        }

                        if (Convert.ToInt32(lkTrangThai.EditValue) == 2)
                        {
                            // Cập nhật thông tin người duyệt mới nhất
                            objTC.NhanVienDuyet = Common.User.MaNV;
                            objTC.NgayDuyet = System.DateTime.UtcNow.AddHours(7);
                            objTC.LyDoKhongDuyet = memoGhiChu.Text;
                        }

                        // Ghi lại lịch sử quá trình duyệt
                        Log_CapNhatTrangThai_DCTC objLog = new Log_CapNhatTrangThai_DCTC();
                        objLog.NgayCN = db.GetSystemDate();
                        objLog.MaNV = Common.User.MaNV;
                        objLog.MaTrangThaiCu = ID_TrangThai;
                        objLog.MaTrangThaiMoi = Convert.ToInt32(lkTrangThai.EditValue);
                        objLog.ID_dkThiCong = ID;
                        objLog.GhiChu = memoGhiChu.Text;
                        dbo.Log_CapNhatTrangThai_DCTCs.InsertOnSubmit(objLog);
                        dbo.SubmitChanges();
                        DialogBox.Success();
                        this.Close();
                    }
                }
                Note = memoGhiChu.Text;
            }

            DialogBox.Success();
            Close();
        }
    }
}