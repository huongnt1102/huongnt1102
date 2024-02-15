using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace Building.WorkSchedule.NhiemVu
{
    public partial class List_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public System.Windows.Forms.Form frm { get; set; }
        bool KT = false, KT1 = false;
        bool IsAdd = false, IsEdit = false, IsDelete = false;
        int MaNVu = 0;
        DateTime dateStart;
        public tnNhanVien objNV;
        public long? MaDMCV { get; set; }
        public long? MaCVNV { get; set; }
        MasterDataContext db = new MasterDataContext();
        public List_ctl()
        {
            InitializeComponent();
            
        }

        void SetDate(int index)
        {
            Library.KyBaoCao objKBC = new Library.KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateTuNgay.EditValueChanged -= new EventHandler(dateTuNgay_EditValueChanged);
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate(cmbKyBC.SelectedIndex);
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadDictionary()
        {
            Library.NhiemVuCls o = new Library.NhiemVuCls();
            lookUpStatus.Properties.DataSource = o.TinhTrang.SelectAll();
            lookUpStatus.ItemIndex = 0;
            lookUpMucDo.Properties.DataSource = o.MucDo.SelectAll();
            lookUpMucDo.ItemIndex = 0;
            lookUpLoaiNVu.Properties.DataSource = o.LoaiNV.SelectAll();
            lookUpLoaiNVu.ItemIndex = 0;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            Library.NhiemVuCls o = new Library.NhiemVuCls();
            o.LoaiNV.TenLNV = lookUpLoaiNVu.Text == "<Tất cả>" ? "%%" : lookUpLoaiNVu.EditValue.ToString();
            o.MucDo.TenMD = lookUpMucDo.Text == "<Tất cả>" ? "%%" : lookUpMucDo.EditValue.ToString();
            o.TinhTrang.TenTT = lookUpStatus.Text == "<Tất cả>" ? "%%" : lookUpStatus.EditValue.ToString();
            //o.NhanVien.MaNV = objNV.MaNV;
            o.IsNhac = itemOption.EditValue.ToString() == "Ngày cập nhật" ? true : false;
            //switch (Library.Commoncls.GetAccessData(Common.PerID, 66))
            //{
            //    case 1://Tat ca
            o.NgayBD = dateTuNgay.DateTime;
            o.NgayHH = dateDenNgay.DateTime;
            switch (itemCategory.EditValue.ToString())
            {
                case "Nhiệm vụ của tôi":
                    gridControl1.DataSource = (from nv in db.NhiemVus
                                               join lnv in db.NhiemVu_Loais on nv.MaLNV equals lnv.MaLNV into loai
                                               from lnv in loai.DefaultIfEmpty()
                                               join md in db.NhiemVu_MucDos on nv.MaMD equals md.MaMD into mucdo
                                               from md in mucdo.DefaultIfEmpty()
                                               join tt in db.NhiemVu_TinhTrangs on nv.MaTT equals tt.MaTT into dstt
                                               from tt in dstt.DefaultIfEmpty()
                                               join nvien in db.tnNhanViens on nv.MaNV equals nvien.MaNV into dsnv
                                               from nvien in dsnv.DefaultIfEmpty()
                                               join kh in db.tnKhachHangs on nv.MaKH equals kh.MaKH into dskh
                                               from kh in dskh.DefaultIfEmpty()
                                               join nv1 in db.tnNhanViens on nv.NguoiCapNhat equals nv1.MaNV into dsnv1
                                               from nv1 in dsnv1.DefaultIfEmpty()
                                               where SqlMethods.DateDiffDay(dateTuNgay.DateTime,nv.NgayBD)>=0 & SqlMethods.DateDiffDay(nv.NgayBD, dateDenNgay.DateTime)>=0 & nv.MaNV == Common.User.MaNV
                                               & nvien.MaTN == Common.User.MaTN & ((int)lookUpLoaiNVu.EditValue == 0 || nv.MaLNV == (int?)lookUpLoaiNVu.EditValue)
                                               & ((int)lookUpStatus.EditValue == 0 || nv.MaTT == (int?)lookUpStatus.EditValue) & ((int)lookUpMucDo.EditValue == 0 || nv.MaTT == (int?)lookUpMucDo.EditValue) 
                                               select new
                                               {
                                                   nv.TieuDe,
                                                   nv.NgayBD,
                                                   nv.NgayHH,
                                                   nv.MaNVu,
                                                   nv.NgayNhac,
                                                   TongSoNgay = SqlMethods.DateDiffDay(DateTime.Now, nv.NgayHH),
                                                   lnv.TenLNV,
                                                   md.TenMD,
                                                   tt.TenTT,
                                                   tt.MaTT,
                                                   nv.NgayHT,
                                                   NguoiCapNhat = nv1.HoTenNV,
                                                   NguoiTao = nvien.HoTenNV,
                                                   nv.MaNV,
                                                   nv.TienDo,
                                                   HoTenKH = kh.HoKH + " " + kh.TenKH,
                                               }).AsEnumerable()
                                                   .Select((p, index) => new
                                                   {
                                                       STT = index +1,
                                                        p.TieuDe,
                                                        p.NgayBD,
                                                        p.NgayHH,
                                                        p.MaNVu,
                                                        p.NgayNhac,
                                                        p.TongSoNgay,
                                                        p.TenLNV,
                                                        p.TenMD,
                                                        p.TenTT,
                                                        p.NgayHT,
                                                        p.NguoiCapNhat,
                                                        p.NguoiTao,
                                                        p.MaNV,
                                                        p.TienDo,
                                                        p.HoTenKH,
                                                   }).ToList();
                    break;
                case "Nhiệm vụ được giao":
                    gridControl1.DataSource = (from nv in db.NhiemVus
                                               join lnv in db.NhiemVu_Loais on nv.MaLNV equals lnv.MaLNV into loai
                                               from lnv in loai.DefaultIfEmpty()
                                               join md in db.NhiemVu_MucDos on nv.MaMD equals md.MaMD into mucdo
                                               from md in mucdo.DefaultIfEmpty()
                                               join tt in db.NhiemVu_TinhTrangs on nv.MaTT equals tt.MaTT into dstt
                                               from tt in dstt.DefaultIfEmpty()
                                               join nvien in db.tnNhanViens on nv.MaNV equals nvien.MaNV into dsnv
                                               from nvien in dsnv.DefaultIfEmpty()
                                               join kh in db.tnKhachHangs on nv.MaKH equals kh.MaKH into dskh
                                               from kh in dskh.DefaultIfEmpty()
                                               join nvnv in db.NhiemVu_tnNhanViens on nv.MaNVu equals nvnv.MaNVu into dsnvnv
                                               from nvnv in dsnvnv.DefaultIfEmpty()
                                               join nv1 in db.tnNhanViens on nv.NguoiCapNhat equals nv1.MaNV into dsnv1
                                               from nv1 in dsnv1.DefaultIfEmpty()
                                               where SqlMethods.DateDiffDay(dateTuNgay.DateTime, nv.NgayBD) >= 0 & SqlMethods.DateDiffDay(nv.NgayBD, dateDenNgay.DateTime) >= 0 & nvnv.MaNV == Common.User.MaNV
                                               & nvien.MaTN == Common.User.MaTN & ((int)lookUpLoaiNVu.EditValue == 0 || nv.MaLNV == (int?)lookUpLoaiNVu.EditValue)
                                               & ((int)lookUpStatus.EditValue == 0 || nv.MaTT == (int?)lookUpStatus.EditValue) & ((int)lookUpMucDo.EditValue == 0 || nv.MaTT == (int?)lookUpMucDo.EditValue)
                                               select new
                                               {
                                                   nv.TieuDe,
                                                   nv.NgayBD,
                                                   nv.NgayHH,
                                                   nv.MaNVu,
                                                   nv.NgayNhac,
                                                   TongSoNgay = SqlMethods.DateDiffDay(DateTime.Now, nv.NgayHH),
                                                   lnv.TenLNV,
                                                   md.TenMD,
                                                   tt.TenTT,
                                                   tt.MaTT,
                                                   nv.NgayHT,
                                                   NguoiCapNhat = nv1.HoTenNV,
                                                   NguoiTao = nvien.HoTenNV,
                                                   nv.MaNV,
                                                   nv.TienDo,
                                                   HoTenKH = kh.HoKH + " " + kh.TenKH,
                                               }).AsEnumerable()
                                                   .Select((p, index) => new
                                                   {
                                                       STT = index + 1,
                                                       p.TieuDe,
                                                       p.NgayBD,
                                                       p.NgayHH,
                                                       p.MaNVu,
                                                       p.NgayNhac,
                                                       p.TongSoNgay,
                                                       p.TenLNV,
                                                       p.TenMD,
                                                       p.TenTT,
                                                       p.NgayHT,
                                                       p.NguoiCapNhat,
                                                       p.NguoiTao,
                                                       p.MaNV,
                                                       p.TienDo,
                                                       p.HoTenKH,
                                                   }).ToList();
                    break;
                default:
                    gridControl1.DataSource = (from nv in db.NhiemVus
                                               join lnv in db.NhiemVu_Loais on nv.MaLNV equals lnv.MaLNV into loai
                                               from lnv in loai.DefaultIfEmpty()
                                               join md in db.NhiemVu_MucDos on nv.MaMD equals md.MaMD into mucdo
                                               from md in mucdo.DefaultIfEmpty()
                                               join tt in db.NhiemVu_TinhTrangs on nv.MaTT equals tt.MaTT into dstt
                                               from tt in dstt.DefaultIfEmpty()
                                               join nvien in db.tnNhanViens on nv.MaNV equals nvien.MaNV into dsnv
                                               from nvien in dsnv.DefaultIfEmpty()
                                               join kh in db.tnKhachHangs on nv.MaKH equals kh.MaKH into dskh
                                               from kh in dskh.DefaultIfEmpty()
                                               join nv1 in db.tnNhanViens on nv.NguoiCapNhat equals nv1.MaNV into dsnv1
                                               from nv1 in dsnv1.DefaultIfEmpty()
                                               where SqlMethods.DateDiffDay(dateTuNgay.DateTime, nv.NgayBD) >= 0 & SqlMethods.DateDiffDay(nv.NgayBD, dateDenNgay.DateTime) >= 0 & nv.MaNV != Common.User.MaNV
                                               & nvien.MaTN == Common.User.MaTN & ((int)lookUpLoaiNVu.EditValue == 0 || nv.MaLNV == (int?)lookUpLoaiNVu.EditValue)
                                               & ((int)lookUpStatus.EditValue == 0 || nv.MaTT == (int?)lookUpStatus.EditValue) & ((int)lookUpMucDo.EditValue == 0 || nv.MaTT == (int?)lookUpMucDo.EditValue)
                                               select new
                                               {
                                                   nv.TieuDe,
                                                   nv.NgayBD,
                                                   nv.NgayHH,
                                                   nv.MaNVu,
                                                   nv.NgayNhac,
                                                   TongSoNgay = SqlMethods.DateDiffDay(DateTime.Now, nv.NgayHH),
                                                   lnv.TenLNV,
                                                   md.TenMD,
                                                   tt.TenTT,
                                                   tt.MaTT,
                                                   nv.NgayHT,
                                                   NguoiCapNhat = nv1.HoTenNV,
                                                   NguoiTao = nvien.HoTenNV,
                                                   nv.MaNV,
                                                   nv.TienDo,
                                                   HoTenKH = kh.HoKH + " " + kh.TenKH,
                                               }).AsEnumerable()
                                                   .Select((p, index) => new
                                                   {
                                                       STT = index + 1,
                                                       p.TieuDe,
                                                       p.NgayBD,
                                                       p.NgayHH,
                                                       p.MaNVu,
                                                       p.NgayNhac,
                                                       p.TongSoNgay,
                                                       p.TenLNV,
                                                       p.TenMD,
                                                       p.TenTT,
                                                       p.NguoiCapNhat,
                                                       p.NgayHT,
                                                       p.NguoiTao,
                                                       p.MaNV,
                                                       p.TienDo,
                                                       p.HoTenKH,
                                                   }).ToList();
                    break;
            }
            //        break;
            //    case 2://Theo phong ban
            //        o.NgayBD = dateTuNgay.DateTime;
            //        o.NgayHH = dateDenNgay.DateTime;
            //        o.NhanVien.PhongBan.MaPB = Common.MaPB;                    
            //        switch (itemCategory.EditValue.ToString())
            //        {
            //            case "Nhiệm vụ của tôi":
            //                gridControl1.DataSource = o.SelectByStaff();
            //                break;
            //            case "Nhiệm vụ được giao":
            //                gridControl1.DataSource = o.SelectDuocGiao();
            //                break;
            //            default:
            //                gridControl1.DataSource = o.SelectByDepartment();
            //                break;
            //        }
            //        break;
            //    case 3://Theo nhom
            //        o.NgayBD = dateTuNgay.DateTime;
            //        o.NgayHH = dateDenNgay.DateTime;
            //        o.NhanVien.NKD.MaNKD = Common.MaNKD;
            //        switch (itemCategory.EditValue.ToString())
            //        {
            //            case "Nhiệm vụ của tôi":
            //                gridControl1.DataSource = o.SelectByStaff();
            //                break;
            //            case "Nhiệm vụ được giao":
            //                gridControl1.DataSource = o.SelectDuocGiao();
            //                break;
            //            default:
            //                gridControl1.DataSource = o.SelectByGroup();
            //                break;
            //        }
            //        break;                
            //    case 4://Theo nhan vien
            //        o.NgayBD = dateTuNgay.DateTime;
            //        o.NgayHH = dateDenNgay.DateTime;             
            //        switch (itemCategory.EditValue.ToString())
            //        {
            //            case "Nhiệm vụ được giao":
            //                gridControl1.DataSource = o.SelectDuocGiao();
            //                break;
            //            default:
            //                gridControl1.DataSource = o.SelectByStaff();
            //                break;
            //        }
            //        break;
            //    default:
            //        gridControl1.DataSource = null;
            //        break;
            //}
            gridView1.FocusedRowHandle = 1;
            gridView1.FocusedRowHandle = 0;
            try
            {
                wait.Close();
            }
            catch { }
            finally
            {
                o = null;
                System.GC.Collect();
            }
        }

        void LoadNhanVien()
        {
            //Library.NhiemVu_NhanVienCls o = new Library.NhiemVu_NhanVienCls();
            //o.MaNVu = MaNVu;
            //o.MaNV = objNV.MaNV;
            gridControl3.DataSource = (from nv in db.NhiemVu_tnNhanViens
                                       join nvn in db.tnNhanViens on nv.NguoiGiao equals nvn.MaNV
                                       join nvgv in db.tnNhanViens on nv.MaNV equals nvgv.MaNV
                                       where nv.MaNVu == MaNVu & nv.NguoiGiao == Common.User.MaNV & nv.MaNV != nv.NguoiGiao
                                       select new
                                       {
                                           nv.NgayGiao,
                                           NguoiGiao = nvn.HoTenNV,
                                           HoTen = nvgv.HoTenNV,
                                       }).AsEnumerable()
                                       .Select((p, index) => new
                                       {
                                           STT = index +1,
                                           p.NgayGiao,
                                           p.NguoiGiao,
                                           p.HoTen,
                                       }).ToList();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhiemVu.AddNew_frm frm = new NhiemVu.AddNew_frm();
            frm.objNV = objNV;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void NhiemVu_ctl_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(frm, Common.User, barManager1);
            //LoadPermission();
            //LoadPermissionScheduler();
            IsAdd = true;
            IsEdit = true;
            IsDelete = true;
            LoadDictionary();

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Properties.Items.Add(str);
            cmbKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            timer1.Start();
        }

        private void btnRefech_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btndelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn xóa [Nhiệm vụ] này không") == DialogResult.Yes)
                {
                    try
                    {
                        if (int.Parse(gridView1.GetFocusedRowCellValue("MaNV").ToString()) == Common.User.MaNV | Library.Common.User.IsSuperAdmin == true)
                        {
                            Library.NhiemVuCls nv = new Library.NhiemVuCls();
                            nv.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                            nv.Delete();
                            gridView1.DeleteSelectedRows();
                        }
                        else
                            DialogBox.Alert("Bạn không có quyền xóa [Nhiệm vụ] này.");
                    }
                    catch
                    {
                        DialogBox.Alert("Xóa không thành công vì: [Nhiệm vụ] này đã được sử dụng.");
                    }
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn [Nhiệm vụ] cần xóa, xin cảm ơn.");
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gridView1_RowStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, colTenTT).ToString() == "Hoàn thành")
                    gridView1.SetRowCellValue(i, colThoiGian, "Hoàn thành");
                else
                {
                    if (int.Parse(gridView1.GetRowCellValue(i, colTime).ToString()) > 0)
                    {
                        gridView1.SetRowCellValue(i, colThoiGian, Library.ConvertDateTimeCls.StringDateTime(int.Parse(gridView1.GetRowCellValue(i, colTime).ToString())));
                        gridView1.SetRowCellValue(i, colTime, int.Parse(gridView1.GetRowCellValue(i, colTime).ToString()) - 1);
                    }
                    else
                        gridView1.SetRowCellValue(i, colThoiGian, "Hết hạn");
                }
            }
            timer1.Start();
        }

        private void gridView1_RowStyle_2(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                try
                {
                    byte isNew = (byte)gridView1.GetRowCellValue(e.RowHandle, "IsNew");
                    if (isNew == 1)
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }
                catch { }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                dateStart = Convert.ToDateTime(gridView1.GetFocusedRowCellValue(colNgayBD));
            }
            else
            {
                MaNVu = 0;
                dateStart = dateTuNgay.DateTime;
            }

            LoadGeneral();
        }

        void LoadHistory()
        {
            gcHistory.DataSource = (from xl in db.NhiemVu_XuLies
                                    join tt in db.NhiemVu_TinhTrangs on xl.MaTT equals tt.MaTT into dstt
                                    from tt in dstt.DefaultIfEmpty()
                                    join nv in db.tnNhanViens on xl.MaNV equals nv.MaNV into dsnv
                                    from nv in dsnv.DefaultIfEmpty()
                                    where xl.MaNVu == MaNVu
                                    select new
                                    {
                                        xl.MaNVu,
                                        xl.DienGiai,
                                        xl.NgayXL,
                                        xl.TienDo,
                                        nv.HoTenNV,
                                        tt.TenTT,
                                    }).AsEnumerable()
                                       .Select((p, index) => new
                                       {
                                           STT = index +1,
                                           p.MaNVu,
                                           p.DienGiai,
                                           p.NgayXL,
                                           p.TienDo,
                                           p.HoTenNV,
                                           p.TenTT,
                                       }).ToList();
        }

        void LoadGeneral()
        {
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    try
                    {
                        var db = new MasterDataContext();
                        txtDienGiai.Text = db.NhiemVus.SingleOrDefault(p => p.MaNVu == MaNVu).DienGiai;
                    }
                    catch { txtDienGiai.Text = ""; }
                    break;
                case 1:
                    LoadHistory();
                    break;
                case 2:
                    LoadLichHen();
                    break;
                case 3:
                    LoadLichHenScheduler();
                    break;
                case 4:
                    LoadNhanVien();
                    break;
                case 5:
                    ctlTaiLieu1.objNV = objNV;
                    ctlTaiLieu1.FormID = 66;
                    ctlTaiLieu1.LinkID = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                    ctlTaiLieu1.MaNV = (int?)gridView1.GetFocusedRowCellValue("MaNV");
                    ctlTaiLieu1.objNV = Common.User;
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
                case 6:
                    ctlNoteHistory1.objNV = objNV;
                    ctlNoteHistory1.FormID = 66;
                    ctlNoteHistory1.LinkID = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                    ctlNoteHistory1.MaNV = (int?)gridView1.GetFocusedRowCellValue("MaNV");
                    ctlNoteHistory1.NoteHistory_Load();
                    ctlNoteHistory1.objNV = Common.User;
                    break;
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (itemCategory.EditValue.ToString() == "Nhiệm vụ của tôi")
                {
                    NhiemVu.AddNew_frm frm = new NhiemVu.AddNew_frm();
                    frm.objNV = objNV;
                    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                    frm.ShowDialog();
                    if (frm.IsUpdate)
                        LoadData();
                }
                else
                    DialogBox.Alert("Bạn không phải là người tạo [Nhiệm vụ] này nên không thể sửa.");
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn [Nhiệm vụ] cần sửa, xin cảm ơn.");
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            
        }

        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (itemCategory.EditValue.ToString() == "Nhiệm vụ của tôi")
                {
                    NhiemVu.AddNew_frm frm = new NhiemVu.AddNew_frm();
                    frm.objNV = objNV;
                    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                    frm.ShowDialog();
                    if (frm.IsUpdate)
                        LoadData();
                }
                else
                    DialogBox.Alert("Bạn không phải là người tạo [Nhiệm vụ] này nên không thể sửa.");
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn [Nhiệm vụ] cần sửa, xin cảm ơn.");
            }
        }

        void LoadLichHen()
        {
            //Library.LichHenCls o = new Library.LichHenCls();
            //o.NhiemVu.MaNVu = MaNVu;
            gcScheduler.DataSource = (from lh in db.LichHens
                                      join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH into dskh
                                      from kh in dskh.DefaultIfEmpty()
                                      join td in db.LichHen_ThoiDiems on lh.MaTD equals td.MaTD into dstd
                                      from td in dstd.DefaultIfEmpty()
                                      join cd in db.LichHen_ChuDes on lh.MaCD equals cd.MaCD into dscd
                                      from cd in dscd.DefaultIfEmpty()
                                      join nv in db.tnNhanViens on lh.MaNV equals nv.MaNV
                                      where lh.MaNVu == MaNVu & Common.User.IsSuperAdmin == true || lh.MaNV == Common.User.MaNV
                                      select new
                                              {
                                                  lh.TieuDe,
                                                  HoTenKH = kh.IsCaNhan == true ? kh.HoKH + kh.TenKH : kh.CtyTen,
                                                  td.TenTD,
                                                  nv.HoTenNV,
                                                  lh.NgayBD,
                                                  lh.NgayKT,
                                                  lh.DienGiai,
                                                  lh.DiaDiem,
                                              }).AsEnumerable()
                                                   .Select((p, index) => new
                                                   {
                                                       STT = index + 1,
                                                       p.TieuDe,
                                                       p.HoTenKH,
                                                       p.TenTD,
                                                       p.HoTenNV,
                                                       p.NgayBD,
                                                       p.NgayKT,
                                                       p.DienGiai,
                                                       p.DiaDiem,
                                                   }).ToList();
        }

        void LoadLichHenScheduler()
        {
            var wait = DialogBox.WaitingForm();

            schedulerControl1.Start = dateStart;
            Library.LichHenCls o = new Library.LichHenCls();
            o.NhiemVu.MaNVu = MaNVu;
            this.schedulerStorage1.Appointments.DataSource = (from lh in db.LichHens
                                                              join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH into dskh
                                                              from kh in dskh.DefaultIfEmpty()
                                                              join td in db.LichHen_ThoiDiems on lh.MaTD equals td.MaTD into dstd
                                                              from td in dstd.DefaultIfEmpty()
                                                              join cd in db.LichHen_ChuDes on lh.MaCD equals cd.MaCD into dscd
                                                              from cd in dscd.DefaultIfEmpty()
                                                              join nv in db.tnNhanViens on lh.MaNV equals nv.MaNV
                                                              where lh.MaNVu == MaNVu & Common.User.IsSuperAdmin == true || lh.MaNV == Common.User.MaNV
                                                              select new
                                                              {
                                                                  lh.TieuDe,
                                                                  HoTenKH = kh.IsCaNhan == true ? kh.HoKH + kh.TenKH : kh.CtyTen,
                                                                  td.TenTD,
                                                                  nv.HoTenNV,
                                                                  lh.NgayBD,
                                                                  lh.NgayKT,
                                                                  lh.DienGiai,
                                                                  lh.DiaDiem,
                                                              }).AsEnumerable()
                                                                   .Select((p, index) => new
                                                                   {
                                                                       STT = index + 1,
                                                                       p.TieuDe,
                                                                       p.HoTenKH,
                                                                       p.TenTD,
                                                                       p.HoTenNV,
                                                                       p.NgayBD,
                                                                       p.NgayKT,
                                                                       p.DienGiai,
                                                                       p.DiaDiem,
                                                                   }).ToList();

            schedulerStorage1.Appointments.Statuses.Clear();
            DataTable tblTable1 = Library.Commoncls.Table("Select * from LichHen_ThoiDiem order by STT");
            foreach (DataRow r1 in tblTable1.Rows)
                schedulerStorage1.Appointments.Statuses.Add(Color.FromArgb((int)r1["MaTD"]), r1["TenTD"].ToString());

            schedulerStorage1.Appointments.Labels.Clear();
            DataTable tblTable = Library.Commoncls.Table("select * from LichHen_ChuDe order by STT");
            foreach (DataRow r in tblTable.Rows)
                schedulerStorage1.Appointments.Labels.Add(Color.FromArgb((int)r["MaCD"]), r["TenCD"].ToString());

            this.schedulerStorage1.Appointments.Mappings.Start = "NgayBD";
            this.schedulerStorage1.Appointments.Mappings.End = "NgayKT";
            this.schedulerStorage1.Appointments.Mappings.Subject = "TieuDe";
            this.schedulerStorage1.Appointments.Mappings.Description = "DienGiai";
            this.schedulerStorage1.Appointments.Mappings.Label = "LabelId";
            this.schedulerStorage1.Appointments.Mappings.Location = "HoTenKH";
            this.schedulerStorage1.Appointments.Mappings.Status = "StatusId";
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("StatusId", "StatusId"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("LabelId", "LabelId"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaLH", "MaLH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaKH", "MaKH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("HoTenKH", "HoTenKH"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("TenNVu", "TenNVu"));
            this.schedulerStorage1.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("MaNVu", "MaNVu"));

            schedulerStorage1.EnableReminders = true;

            wait.Close();
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;

            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);
            int? maKh = 0;
            try { maKh = (int?)gridView1.GetFocusedRowCellValue("MaKH"); }
            catch { }
            var maNVu = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
            if (maKh == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng, xin cảm ơn.");
                return;
            }
            string hotenKH = gridView1.GetFocusedRowCellValue("HoTenKH").ToString();
            string tieuDe = gridView1.GetFocusedRowCellValue("TieuDe").ToString();
            LichHen.AddNew_frm f = new LichHen.AddNew_frm((SchedulerControl)sender, apt, openRecurrenceForm, objNV, MaDMCV, MaCVNV);
            f.objNV = objNV;
            f.NhiemVu = tieuDe;
            f.MaKH = maKh ?? 0;
            f.MaNVu = maNVu ?? 0;
            f.KhachHang = hotenKH;
            f.IsEdit = IsEdit;
            f.IsAdd = IsAdd;
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            e.DialogResult = f.ShowDialog();
            e.Handled = true;

            if (f.IsUpdate)
                LoadLichHenScheduler();
        }

        private void schedulerControl1_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            //if (XtraMessageBox.Show("Dữ liệu có thay đổi bạn có muốn lưu lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                Library.LichHenCls o = new Library.LichHenCls();
                o.NgayBD = _New.Start;
                o.NgayKT = _New.End;
                o.MaLH = int.Parse(_New.CustomFields["MaLH"].ToString());
                o.UpdateTime(); e.ToString();
                // LoadData();
            }
            catch { }
            //}
        }

        private void schedulerControl1_AppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            try
            {
                Appointment _New = (Appointment)e.EditedAppointment;
                Library.LichHenCls o = new Library.LichHenCls();
                o.NgayBD = _New.Start;
                o.NgayKT = _New.End;
                o.MaLH = int.Parse(_New.CustomFields["MaLH"].ToString());
                o.UpdateTime();
            }
            catch { }
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e)
        {
            //if (XtraMessageBox.Show("Dữ liệu có thay đổi bạn có muốn lưu lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            try
            {
                Appointment app = (Appointment)e.Objects[0];
                Library.LichHenCls o = new Library.LichHenCls();
                o.TieuDe = app.Subject;
                o.MaLH = int.Parse(app.CustomFields["MaLH"].ToString());
                o.ThoiDiem.STT = (byte)app.StatusId;
                o.ChuDe.STT = (byte)app.LabelId;
                o.UpdateSubject();
                //LoadData();
            }
            catch { }
            //}
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
            }
            else
                MaNVu = 0;

            LoadGeneral();
        }

        private void btnFinish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue("TienDo").ToString() == "100")
                    DialogBox.Alert("<Nhiệm vụ> này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xác nhận hoàn thành nhiệm vụ này không?") == DialogResult.Yes)
                    {
                        using (var db = new MasterDataContext())
                        {
                            // Update NhiemVu
                            int Manvu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                            var objNVu = db.NhiemVus.Single(p => p.MaNVu == Manvu);
                            objNVu.TienDo = 100;
                            objNVu.NgayHT = DateTime.Now;
                            // Lưu NhiemVu_XyLy
                            NhiemVu_XuLy objxl = new NhiemVu_XuLy();
                            objxl.MaNVu = Manvu;
                            objxl.NgayXL = DateTime.Now;
                            objxl.MaNV = Common.User.MaNV;
                            objxl.TienDo = 100;
                            objxl.DienGiai = "Hoàn thành";
                            db.NhiemVu_XuLies.InsertOnSubmit(objxl);
                            db.SubmitChanges();
                            LoadData();
                        }
                    }
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn nhiệm vụ, xin cảm ơn.");
        }

        private void btnLoadScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadLichHenScheduler();
        }

        private void btnAddScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void btnEditScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormShow(new AppointmentFormEventArgs(this.schedulerControl1.SelectedAppointments(apt)));
        }

        private void btnDeleteScheduler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            switch (e.Menu.Id)
            {
                case SchedulerMenuItemId.DefaultMenu:
                    SchedulerMenuItem add = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment);
                    if (add != null)
                    {
                        add.Caption = "Thêm lịch hẹn";
                        //add.Image
                    }
                    if (!IsAdd)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAppointment);

                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoThisDay);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleEnable);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleVisible);
                    //e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu);
                    SchedulerPopupMenu switchs = e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu);
                    if (switchs != null)
                    {
                        switchs.Caption = "Kiểu lịch";
                        switchs.Items[0].Caption = "Lịch ngày";
                        switchs.Items[1].Caption = "Lịch tuần làm việc";
                        switchs.Items[2].Caption = "Lịch tuần";
                        switchs.Items[3].Caption = "Lịch tháng";
                        switchs.Items[4].Caption = "Lịch dòng thời gian";
                    }
                    break;
                case SchedulerMenuItemId.AppointmentMenu:
                    // Find the "Label As" item of the appointment popup menu and corresponding submenu.        
                    SchedulerPopupMenu label = e.Menu.GetPopupMenuById(SchedulerMenuItemId.LabelSubMenu);
                    if (label != null)
                    {
                        // Rename the item of the appointment popup menu.             
                        label.Caption = "Loại lịch hẹn";
                        // Rename the first item of the submenu.            
                        //submenu.Items[0].Caption = "Label 1";      
                    }

                    // Find the "Status As" item of the appointment popup menu and corresponding submenu.        
                    SchedulerPopupMenu status = e.Menu.GetPopupMenuById(SchedulerMenuItemId.StatusSubMenu);
                    if (status != null)
                        status.Caption = "Thời điểm liên hệ";

                    SchedulerMenuItem open = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment);
                    if (open != null)
                        open.Caption = "Xem thông tin";

                    SchedulerMenuItem delete = e.Menu.GetMenuItemById(SchedulerMenuItemId.DeleteAppointment);
                    if (delete != null)
                        delete.Caption = "Xóa lịch hẹn";
                    if (!IsDelete)
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.DeleteAppointment);

                    if (!IsEdit)
                    {
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
                        e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
                    }
                    break;
                case SchedulerMenuItemId.AppointmentDragMenu:
                    //SchedulerMenuItem cancel = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragCancel);
                    //if (cancel != null)
                    //    cancel.Caption = "Bỏ qua";

                    //SchedulerMenuItem copy = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragCopy);
                    //if (copy != null)
                    //    copy.Caption = "Sao chép";

                    //SchedulerMenuItem move = e.Menu.GetMenuItemById(SchedulerMenuItemId.AppointmentDragMove);
                    //if (move != null)
                    //    move.Caption = "Di chuyển";
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCancel);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragCopy);
                    e.Menu.RemoveMenuItem(SchedulerMenuItemId.AppointmentDragMove);
                    break;
            }
        }

        private void schedulerControl1_PreparePopupMenu(object sender, PreparePopupMenuEventArgs e)
        {

            Point p = schedulerControl1.PointToClient(Form.MousePosition);

            SchedulerHitInfo hitInfo = schedulerControl1.ActiveView.ViewInfo.CalcHitInfo(p, true);

            if (hitInfo.HitTest == SchedulerHitTest.ResourceHeader)
            {

                MessageBox.Show(hitInfo.ViewInfo.Resource.Caption, "Resource Header clicked", MessageBoxButtons.OK, MessageBoxIcon.Information);

                e.Menu.Items.Clear();

            }
        }

        private void btnGiaoViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colTenTT).ToString() == "Hoàn thành")
                    DialogBox.Alert("<Nhiệm vụ> này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    SelectObject_frm frm = new SelectObject_frm();
                    frm.objNV = objNV;
                    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                    frm.ShowDialog();
                    LoadNhanVien();
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn nhiềm vụ muốn giao việc, xin cảm ơn.");
        }

        private void schedulerStorage1_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
        {
            if (!IsDelete)
                e.Cancel = true;
            else
            {
                if (DialogBox.QuestionDelete() == DialogResult.Yes)
                {
                    try
                    {
                        Appointment app = (Appointment)e.Object;
                        Library.LichHenCls o = new Library.LichHenCls();
                        o.MaLH = int.Parse(app.CustomFields["MaLH"].ToString());
                        o.Delete();
                        LoadData();
                    }
                    catch
                    {
                        DialogBox.Alert("Xóa không thành công vì lịch hẹn này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                    }
                }
                else
                    e.Cancel = true;
            }
        }

        private void schedulerStorage1_AppointmentChanging(object sender, PersistentObjectCancelEventArgs e)
        {
            if (!IsEdit)
                e.Cancel = true;            
        }

        private void btnXoaNTH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (gridView3.GetFocusedRowCellValue(colMaNV) != null)
            //{
            //    if (DialogBox.Question() == DialogResult.Yes)
            //    {
            //        Library.NhiemVu_NhanVienCls o = new Library.NhiemVu_NhanVienCls();
            //        o.MaNV = int.Parse(gridView3.GetFocusedRowCellValue(colMaNV).ToString());
            //        o.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
            //        try
            //        {
            //            o.Delete();
            //        }
            //        catch
            //        {
            //            DialogBox.Alert("Xóa không thành công vì: <Người thực hiện> đã có phát sinh <Lịch hẹn>\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            //        }
            //    }
            //}
            //else
            //    DialogBox.Alert("Vui lòng chọn <Người thực hiện> muốn xóa. Xin cảm ơn.");
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                var f = new frmDuyet();
                f.objNV = objNV;
                f.MaNVu = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colMaNVu));
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                    LoadData();
            }
            else
                DialogBox.Alert("Vui lòng chọn nhiềm vụ muốn giao việc, xin cảm ơn.");            
        }

        private void itemAddSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn <Nhiệm vụ>, xin cảm ơn.");
                return;
            }
            try
            {
                int? maKh = 0;
                try { maKh = (int?)gridView1.GetFocusedRowCellValue("MaKH"); }
                catch { }
                var maNVu = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                string hotenKH = gridView1.GetFocusedRowCellValue("HoTenKH").ToString();

                var frm = new LichHen.AddNew_frm(null, maNVu, maKh ?? 0, 0, 0, hotenKH);
                frm.objNV = objNV;
                frm.NhiemVu = gridView1.GetFocusedRowCellValue("TieuDe").ToString();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemEditSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvScheduler.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn <Lịch hẹn>, xin cảm ơn.");
                return;
            }
            try
            {
                int? maKh = 0;
                try { maKh = (int?)gridView1.GetFocusedRowCellValue("MaKH"); }
                catch { }
                var maNVu = (int?)gridView1.GetFocusedRowCellValue("MaNVu");
                var maLH = (int?)gvScheduler.GetFocusedRowCellValue("MaLH");
                string hotenKH = gridView1.GetFocusedRowCellValue("HoTenKH").ToString();

                var frm = new LichHen.AddNew_frm(maLH, maNVu, maKh ?? 0, 0, 0, hotenKH);
                frm.NhiemVu = gridView1.GetFocusedRowCellValue("TieuDe").ToString();
                frm.objNV = objNV;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemDeleteSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvScheduler.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn <Lịch hẹn>, xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.Yes)
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objLH = db.LichHens.Single(p => p.MaLH == (int?)gvScheduler.GetFocusedRowCellValue("MaLH"));
                    try
                    {
                        db.LichHens.DeleteOnSubmit(objLH);
                        db.SubmitChanges();

                        gvScheduler.DeleteSelectedRows();
                    }
                    catch { }
                }
            }
        }

        private void itemCategory_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                try
                {
                    byte isNew = (byte)gridView1.GetRowCellValue(e.RowHandle, "IsNew");
                    if (isNew == 1)
                    {
                        using (var db = new MasterDataContext())
                        {
                            var o = new nvNhanVien();
                            o.MaNVu = (int)gridView1.GetRowCellValue(e.RowHandle, "MaNVu");
                            o.MaNV = objNV.MaNV;
                            db.nvNhanViens.InsertOnSubmit(o);
                            db.SubmitChanges();

                            gridView1.SetRowCellValue(e.RowHandle, "IsNew", 0);
                            gridView1.Appearance.Row.Font = new Font(gridView1.Appearance.Row.Font, FontStyle.Regular);
                            gridView1.LayoutChanged();
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void itemNotRead_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                try
                {
                    byte isNew = (byte)gridView1.GetFocusedRowCellValue("IsNew");
                    if (isNew == 0)
                    {
                        using (var db = new MasterDataContext())
                        {
                            var objNew = db.nvNhanViens.Single(p => p.MaNV == objNV.MaNV && p.MaNVu == (int?)gridView1.GetFocusedRowCellValue("MaNVu"));
                            db.nvNhanViens.DeleteOnSubmit(objNew);
                            db.SubmitChanges();

                            gridView1.SetFocusedRowCellValue("IsNew", 1);
                            gridView1.Appearance.Row.Font = new Font(gridView1.Appearance.Row.Font, FontStyle.Regular);
                            gridView1.LayoutChanged();
                        }
                    }
                }
                catch
                { }
            }
        }
    }
}
