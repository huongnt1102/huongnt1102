using System;
using Library;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace LandSoftBuildingMain
{
    public partial class frmMainIntro : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        private delegate void DlgAddItemN();

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.pq_BieuDoMain_Nhom _nhomHienThi;

        private static int NhanVienAdminId = 6;

        private Thread _th;

        public frmMainIntro()
        {
            InitializeComponent();
        }

        private void frmMainIntro_Load(object sender, EventArgs e)
        {
            _nhomHienThi = GetNhom();
            if (_nhomHienThi != null)
                //await Task.Run(() => { 
                    LoadControl(); 
                //});
        }

        private Library.pq_BieuDoMain_Nhom GetNhom()
        {
            if (objnhanvien == null) objnhanvien = Library.Common.User;
            if (objnhanvien == null) objnhanvien = GetNhanVienById(NhanVienAdminId);
            if (objnhanvien == null) return null;

            Library.pqNhomNhanVien pqNhomNhanVien = GetNhomNhanVienByUserId(objnhanvien.MaNV);
            if (pqNhomNhanVien == null) return GetNhomHienThi();

            Library.pq_BieuDoMain_NhomNhanVien pqBieuDoMainNhomNhanVien = GetNhomNhanVienBieuDoByPqNhomNhanVien(pqNhomNhanVien.GroupID);
            if (pqBieuDoMainNhomNhanVien == null) return GetNhomHienThi();

            return GetNhomById(pqBieuDoMainNhomNhanVien.GroupId);
        }

        private Library.tnNhanVien GetNhanVienById(int? userId)
        {
            //return _db.tnNhanViens.FirstOrDefault(_ => _.MaNV == userId);
            Library.MasterDataContext db = new MasterDataContext();
            var nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == userId);
            if (nhanVien != null)
            {
                // Lấy mặc định tòa nhà của nhân viên theo phân quyền
                nhanVien.MaTN = Library.Common.TowerList.FirstOrDefault().MaTN;
                return nhanVien;
            }
            else nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.IsSuperAdmin == true); // nhân viên admin đầu tiên để phân quyền
            return nhanVien;
        }

        private Library.pq_BieuDoMain_Nhom GetNhomById(int? id)
        {
            return _db.pq_BieuDoMain_Nhoms.FirstOrDefault(_ => _.Id == id);
        }

        private Library.pq_BieuDoMain_Nhom GetNhomHienThi()
        {
            return _db.pq_BieuDoMain_Nhoms.FirstOrDefault(_ => _.IsHienThi == true);
        }

        private Library.pqNhomNhanVien GetNhomNhanVienByUserId(int? userId)
        {
            return _db.pqNhomNhanViens.FirstOrDefault(_ => _.MaNV == userId);
        }

        private Library.pq_BieuDoMain_NhomNhanVien GetNhomNhanVienBieuDoByPqNhomNhanVien(int? pqNhomNhanVienId)
        {
            return _db.pq_BieuDoMain_NhomNhanViens.FirstOrDefault(_ => _.GroupUserId == pqNhomNhanVienId & _.GroupId!=null);
        }

        private void LoadControl()
        {
            try
            {
                panelControl1.Controls.Clear();

                // biểu đồ từ 7 trở lên là biểu đồ thiết kế
                switch (_nhomHienThi.SoBieuDo)
                {
                    case 1: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel1 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId = _nhomHienThi.Id, IsView = true }); break;
                    case 2: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel2 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId = _nhomHienThi.Id, IsView = true }); break;
                    case 3: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel3 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId = _nhomHienThi.Id, IsView = true }); break;
                    case 4: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel4 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId = _nhomHienThi.Id, IsView = true }); break;
                    case 5: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel5 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId = _nhomHienThi.Id, IsView = true }); break;
                    case 6: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel6 { Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId = _nhomHienThi.Id, IsView = true }); break;
                    case 7: LoadControlSetting(new Building.PhanQuyenBieuDo.ControlSetting.CtlPanel7{Dock = System.Windows.Forms.DockStyle.Fill, SoBieuDo = _nhomHienThi.SoBieuDo, NhomCaiDatId=_nhomHienThi.Id, IsView = true});break;
                }
            }
            catch{}
            
        }

        private void LoadControlSetting(DevExpress.XtraEditors.XtraUserControl ctl)
        {
            if (panelControl1.InvokeRequired)
                //await System.Threading.Tasks.Task.Run(() => { 
                    BeginInvoke(new DlgAddItemN(LoadControl)); 
                //});
            else panelControl1.Controls.Add(ctl);
        }

        private void hyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            System.Diagnostics.Process.Start("http://dip.vn"); 
        }
    }
}