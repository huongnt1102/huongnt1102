using System;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace Building.Asset.BaoCao
{
    public partial class frmTinhHinhKiemTraDinhKy : XtraForm
    {
        private MasterDataContext _db = new MasterDataContext();
        public frmTinhHinhKiemTraDinhKy()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var obj = (from _ in _db.tbl_NhomTaiSans
                    //join lts in _db.tbl_LoaiTaiSans on _.ID equals lts.NhomTaiSanID
                    //join tts in _db.tbl_TenTaiSans on lts.ID equals tts.LoaiTaiSanID
                    //join kh in _db.tbl_PhieuVanHanh_ChiTiet_TaiSans on tts.ID equals kh.MaTaiSanChiTietID
                    where _.MaTN == (byte?) itemToaNha.EditValue
                    select new
                    {
                        ID = "NTS" + _.ID,
                        IDName = _.ID,
                        Name = _.TenNhomTaiSan,
                        ParentID = "0"
                    }).Union(from _ in _db.tbl_NhomTaiSans
                    join lts in _db.tbl_LoaiTaiSans on _.ID equals lts.NhomTaiSanID
                    //join tts in _db.tbl_TenTaiSans on lts.ID equals tts.LoaiTaiSanID
                    //join kh in _db.tbl_PhieuVanHanh_ChiTiet_TaiSans on tts.ID equals kh.MaTaiSanChiTietID
                    where _.MaTN == (byte?) itemToaNha.EditValue
                    select new
                    {
                        ID = "LTS" + lts.ID,
                        IDName =  lts.ID,
                        Name = lts.TenLoaiTaiSan,
                        ParentID = "NTS" + lts.NhomTaiSanID
                    }).Union(from _ in _db.tbl_NhomTaiSans
                    join lts in _db.tbl_LoaiTaiSans on _.ID equals lts.NhomTaiSanID
                    join tts in _db.tbl_TenTaiSans on lts.ID equals tts.LoaiTaiSanID
                    //join kh in _db.tbl_PhieuVanHanh_ChiTiet_TaiSans on tts.ID equals kh.MaTaiSanChiTietID
                    where _.MaTN == (byte?) itemToaNha.EditValue
                    select new
                    {
                        ID = "TTS" + tts.ID,
                        IDName = tts.ID,
                        Name = tts.TenTaiSan,
                        ParentID = "LTS" + tts.LoaiTaiSanID
                    })
                .Union(from _ in _db.tbl_NhomTaiSans
                    join lts in _db.tbl_LoaiTaiSans on _.ID equals lts.NhomTaiSanID
                    join tts in _db.tbl_TenTaiSans on lts.ID equals tts.LoaiTaiSanID
                    //join kh in _db.tbl_PhieuVanHanh_ChiTiet_TaiSans on tts.ID equals kh.MaTaiSanChiTietID
                    join kh in _db.tbl_PhieuVanHanhs on tts.ID equals kh.TenTaiSanID
                    where _.MaTN == (byte?) itemToaNha.EditValue & kh.IsTenTaiSan==true
                    select new
                    {
                        ID = kh.ID.ToString(),
                        IDName = kh.ID,
                        Name = kh.tbl_KeHoachVanHanh.tbl_TanSuat.TenTanSuat,
                        ParentID = "TTS" + tts.ID
                    }).ToList();

             obj.Add(new {ID = "0", IDName =0, Name = "ROOT", ParentID = ""});

            try
            {
                var listPhatSinh = (from _ in _db.tbl_NhomTaiSans
                    join lts in _db.tbl_LoaiTaiSans on _.ID equals lts.NhomTaiSanID
                    join tts in _db.tbl_TenTaiSans on lts.ID equals tts.LoaiTaiSanID
                    //join kh in _db.tbl_PhieuVanHanh_ChiTiet_TaiSans on tts.ID equals kh.MaTaiSanChiTietID
                    join kh in _db.tbl_PhieuVanHanhs on tts.ID equals kh.TenTaiSanID
                    where _.MaTN == (byte?) itemToaNha.EditValue & kh.IsTenTaiSan==true
                    select new
                    {
                        ID = kh.ID.ToString(),
                        IDName = kh.ID,
                        Name = kh.tbl_KeHoachVanHanh.tbl_TanSuat.TenTanSuat,
                        ParentID = "TTS" + tts.ID,
                        ThoiGianThucHienTheoKh=kh.tbl_KeHoachVanHanh.TuNgay,
                        TrangThai=kh.TrangThaiPhieu,
                        kh.NgayThucHien,
                        kh.tbl_KeHoachVanHanh.ChiPhiTheoKh,kh.tbl_KeHoachVanHanh.ChiPhiThucHien
                    }).ToList();

                var listTong = (from p in obj
                    join bb in listPhatSinh on p.ID equals bb.ID into go
                    from bb in go.DefaultIfEmpty()
                    select new TinhHinhThucHienClass
                    {
                        ID = p.ID,
                        IDName = p.IDName,
                        Name = p.Name,
                        ParentID = p.ParentID,
                        ThoiGianThucHienTheoKh = bb != null ? bb.ThoiGianThucHienTheoKh : new DateTime?(),
                        TrangThai = bb != null ? bb.TrangThai : new int?(),
                        NgayThucHien=bb!=null?bb.NgayThucHien:new DateTime?(),
                        ChiPhiTheoKh=bb!=null?bb.ChiPhiTheoKh:new decimal?(),
                        ChiPhiThucHien=bb!=null?bb.ChiPhiThucHien:new decimal?()
                    }).ToList();

                treeList1.DataSource = listTong;
                treeList1.ExpandAll();
            }
            catch(Exception ex)
            {
                //var a = ex;
            }
            
                     
        }
        private void frmGanProfileChoHeThong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            //rspToaNha.DataSource = _db.tnToaNhas;
            rspToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            repTrangThai.DataSource = _db.tbl_PhieuVanHanh_TrangThais;
            LoadData();
           
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        } 
        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private class TinhHinhThucHienClass
        {
            public string  ID {set;get;}
            public string Name { set; get; }
            public int? IDName { set; get; }
            public string ParentID { set; get; }
            public DateTime? ThoiGianThucHienTheoKh { get; set; }
            public int? TrangThai { get; set; }
            public DateTime? NgayThucHien { get; set; }
            public decimal? ChiPhiTheoKh { get; set; }
            public decimal? ChiPhiThucHien { get; set; }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog _save = new SaveFileDialog();
            _save.InitialDirectory = "";
            _save.Title = "Tình hình kiểm tra định kỳ";
            _save.DefaultExt = "xlsx";
            _save.Filter = "Excel Files|*.xls;*.xlsx";
            _save.FilterIndex = 2;
            _save.RestoreDirectory = true;
            if (_save.ShowDialog() == DialogResult.OK)
            {
                XlsxExportOptions xport = new XlsxExportOptions();
                xport.TextExportMode = TextExportMode.Text;
                treeList1.ExportToXlsx(_save.FileName, xport);
            }
        }
    }
}