using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.NhanKhau
{
    public partial class ctlCuDan : DevExpress.XtraEditors.XtraUserControl
    {
        public int? MaKH { get; set; }

        public int? MaMB { get; set; }

        public byte? MaTN { get; set; }

        public async void CuDanLoadData()
        {
            if (this.MaKH == null && this.MaMB == null)
            {
                gcNhanKhau.DataSource = null;
                return;
            }

            var db = new MasterDataContext();
            try
            {
                System.Collections.Generic.List<DataNhanKhau> dataNhanKhaus = new List<DataNhanKhau>();
                await System.Threading.Tasks.Task.Run(() => { dataNhanKhaus = GetDataNhanKhaus(); });

                gcNhanKhau.DataSource = dataNhanKhaus;
            }
            catch
            {
                gcNhanKhau.DataSource = null;
            }
            finally
            {
                db.Dispose();
            }
        }

        #region LoadData

        public class DataNhanKhau { public System.Int32 ID { get; set; } public System.String HoTenNK { get; set; } public System.String GioiTinh { get; set; } public System.String NgaySinh { get; set; } public System.String CMND { get; set; } public System.String NgayCap { get; set; } public System.String NoiCap { get; set; } public System.String DienThoai { get; set; } public System.String Email { get; set; } public System.String DCTT { get; set; } public System.Boolean? DaDKTT { get; set; } public System.String TenQuanHe { get; set; } public System.String TenTrangThai { get; set; } public System.String HoTenNV { get; set; } public System.String NgayDK { get; set; } }

        private System.Collections.Generic.List<DataNhanKhau> GetDataNhanKhaus()
        {
            using(Library.MasterDataContext db = new MasterDataContext())
            {
                return (from p in db.tnNhanKhaus
                        join tt in db.tnNhanKhauTrangThais on p.MaTT equals tt.MaTT into ttrang
                        from tt in ttrang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                        where (p.MaKH == this.MaKH | this.MaKH == null)
                        & (p.MaMB == this.MaMB | this.MaMB == null)
                        select new DataNhanKhau
                        {
                            ID= p.ID,
                            HoTenNK= p.HoTenNK,
                            GioiTinh = p.GioiTinh.Value ? "Nam" : "Nữ",
                            NgaySinh= p.NgaySinh,
                            CMND= p.CMND,
                            NgayCap= p.NgayCap,
                            NoiCap= p.NoiCap,
                            DienThoai= p.DienThoai,
                            Email= p.Email,
                            DCTT= p.DCTT,
                            DaDKTT= p.DaDKTT,
                            TenQuanHe = p.tnQuanHe.Name,
                            TenTrangThai= tt.TenTrangThai,
                            HoTenNV= nv.HoTenNV,
                            NgayDK= p.NgayDK
                        }).ToList();
            }
        }
        #endregion

        public ctlCuDan()
        {
            InitializeComponent();

            barManager1.SetPopupContextMenu(gcNhanKhau, popupMenu1);

            //this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvNhanKhau.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Cư dân]. Xin cám ơn!");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    var objNK = db.tnNhanKhaus.Single(p => p.ID == (int)grvNhanKhau.GetRowCellValue(i, "ID"));
                    db.tnNhanKhaus.DeleteOnSubmit(objNK);
                }

                db.SubmitChanges();
                CuDanLoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvNhanKhau.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn [Cư dân]. Xin cám ơn!");
                return;
            }

            var frm = new DichVu.NhanKhau.frmEdit();
            frm.ID = id;
            frm.MaTN = MaTN;
            frm.MaKH = this.MaKH;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                CuDanLoadData();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new DichVu.NhanKhau.frmEdit();
            frm.MaKH = this.MaKH;
            frm.MaMB = this.MaMB;
            frm.MaTN = MaTN;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                CuDanLoadData();
        }
    }
}
