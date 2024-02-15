using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace AnNinh
{
    public partial class frmShowKeHoach : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public List<AnNinhKeHoach> lstKeHoach;
        MasterDataContext db;

        public frmShowKeHoach()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmShowKeHoach_Load(object sender, EventArgs e)
        {
            txtHello.Caption = objnhanvien.HoTenNV;
            gcToDoList.DataSource = db.AnNinhKeHoachNoiDungs.Where(p=> lstKeHoach.Contains(p.AnNinhKeHoach))
                .Select(p => new
                {
                    p.MaKeHoach,
                    p.DienGiai,
                    DienGiaiRG = p.DienGiai.Length < 50 ? p.DienGiai.Substring(0, 20) : p.DienGiai.Substring(0, 50)
                });
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcToDoList.DataSource = db.AnNinhKeHoachNoiDungs.Where(p => lstKeHoach.Contains(p.AnNinhKeHoach))
                .Select(p => new
                {
                    p.MaKeHoach,
                    p.DienGiai,
                    DienGiaiRG = p.DienGiai.Length < 50 ? p.DienGiai.Substring(0, 20) : p.DienGiai.Substring(0, 50)
                });
        }

        private void btnNhanNV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime now = db.GetSystemDate();
            var ListNhiemVu = db.AnNinhKeHoachNoiDungs.Where(p => lstKeHoach.Contains(p.AnNinhKeHoach)).Select(p => p.MaNoiDung).ToList();
            var CheckAvail = db.AnNinhNhiemVuNhanViens
                .Where(p => ListNhiemVu.Contains(p.MaNoiDung.Value));
            if (CheckAvail != null)
            {
                var count = CheckAvail.Where(p => p.LastUpdated.Value.Day == now.Day & p.LastUpdated.Value.Month == now.Month & p.LastUpdated.Value.Year == now.Year).Count();
                if (count > 0)
                {
                    DialogBox.Alert("Bạn đã nhận nhiệm vụ hôm nay rồi");
                    return;
                }
            }
            

            if (XtraMessageBox.Show("Bạn có muốn nhận nhiệm vụ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                List<AnNinhNhiemVuNhanVien> ListNVAdd = new List<AnNinhNhiemVuNhanVien>();
                foreach (var item in ListNhiemVu)
                {
                    AnNinhNhiemVuNhanVien obj = new AnNinhNhiemVuNhanVien()
                    {
                        DaThucHien = false,
                        MaNoiDung = item,
                        MaNV = objnhanvien.MaNV,
                        LastUpdated = now
                    };

                    ListNVAdd.Add(obj);
                }

                try
                {
                    db.AnNinhNhiemVuNhanViens.InsertAllOnSubmit(ListNVAdd);
                    db.SubmitChanges();
                    DialogBox.Alert("Nhận nhiệm vụ thành công");
                    frmKeHoachCuaToi frm = new frmKeHoachCuaToi() { objnhanvien = objnhanvien };
                    frm.Show();
                    this.Close();
                }
                catch
                { }
            }
        }
    }
}