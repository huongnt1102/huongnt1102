using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
namespace DichVu.KhachHang
{
    public partial class frmTaiLieu : DevExpress.XtraEditors.XtraForm
    {
        public Library.tnNhanVien objnhanvien;
        public frmTaiLieu()
        {
            InitializeComponent();
            MasterDataContext db = new MasterDataContext();
            var objnhan = db.tnNhanViens.Single(p => p.MaNV == Common.User.MaNV);
            ctlTaiLieu1.FormID = 100;
            ctlTaiLieu1.LinkID = 100;
            ctlTaiLieu1.MaNV = objnhan.MaNV;
            ctlTaiLieu1.objNV = objnhan;
            ctlTaiLieu1.TaiLieu_Load();
        }
    }
}