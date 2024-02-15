using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Library;

namespace LandSoftBuilding.Receivables.DauKy
{
    public partial class FrmDauKy : DevExpress.XtraEditors.XtraForm
    {
        public FrmDauKy()
        {
            InitializeComponent();
        }

        private void FrmDauKy_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkBuilding.DataSource = Common.TowerList;

            itemBuilding.EditValue = Common.User.MaTN;

            itemYear.EditValue = DateTime.Now.Year;

            this.LoadData();
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte?) itemBuilding.EditValue;
                var year = Convert.ToInt32(itemYear.EditValue);
                var db = new MasterDataContext();
                gc.DataSource = (from dk in db.dvDauKies
                    join kh in db.tnKhachHangs on dk.MaKH equals kh.MaKH into khachhang
                    from kh in khachhang.DefaultIfEmpty()
                    join mb in db.mbMatBangs on dk.MaMB equals mb.MaMB into matBang
                    from mb in matBang.DefaultIfEmpty()
                    join dv in db.dvLoaiDichVus on dk.MaLDV equals dv.ID into dichVu
                    from dv in dichVu.DefaultIfEmpty()
                    where dk.MaTN == buildingId & dk.Nam == year //& (kh.IsNgungSuDung==null || kh.IsNgungSuDung == false)
                    select new
                    {
                        MaSoMB = mb!=null? mb.MaSoMB : "", TenKH = kh!=null?( kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen):"", TenLDV = dv!=null? dv.TenLDV:"",
                        dk.SoTien, dk.DienGiai, dk.Id,MaKH = kh!=null? kh.MaKH:0,MaMB = mb!=null? mb.MaMB : 0
                    }).ToList();
            }
            catch{}
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemCapNhatSoDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var db = new MasterDataContext();
                // tính số dư đầu kỳ
                var buildingId = (byte?)itemBuilding.EditValue;
                var year = Convert.ToInt32(itemYear.EditValue);
                var namTruoc = year - 1;
                var denNgay = new DateTime(year, 1, 1);

                var param = new Dapper.DynamicParameters();
                param = new Dapper.DynamicParameters();
                param.Add("@TowerId", buildingId, System.Data.DbType.Byte, null, null);
                param.Add("@DenNgay", denNgay, System.Data.DbType.DateTime, null, null);
                param.Add("@NamTruoc", namTruoc, System.Data.DbType.Int32, null, null);
                Library.Class.Connect.QueryConnect.Query<bool>("dbo.dvDauKy_All", param).ToList();

                DialogBox.Success("Cập nhật thành công.");

                LoadData();
            }
            catch
            {
                DialogBox.Error("Đã xãy ra lỗi, không cập nhật được");
            }
            
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new Import.FrmDauKy_Import())
                {
                    frm.MaTn = (byte)itemBuilding.EditValue;
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = gv.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn dòng cần xóa!");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                var db = new MasterDataContext();
                foreach (int i in indexs)
                {

                    var o = db.dvDauKies.FirstOrDefault(p => p.Id == (long)gv.GetRowCellValue(i, "Id"));
                    if (o != null)
                    {
                        db.dvDauKies.DeleteOnSubmit(o);
                    }

                }
                db.SubmitChanges();
                gv.DeleteSelectedRows();
                LoadData();
            }
            catch (Exception)
            {
                DialogBox.Alert("Không xóa được vì công nợ này đã được dùng ở nơi khác");
            }
        }

        private void itemChayLaiMotKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var khachHangId = (int?) gv.GetFocusedRowCellValue("MaKH");
                if (khachHangId == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn khách hàng");
                    return;
                }

                var db = new Library.MasterDataContext();
                var buildingId = (byte?)itemBuilding.EditValue;
                var year = Convert.ToInt32(itemYear.EditValue);
                var denNgay = new DateTime(year, 1, 1);
                //db.dvDauKies.DeleteAllOnSubmit(db.dvDauKies.Where(_ => _.MaKH == khachHangId));

                var khachHangs = (from kh in db.tnKhachHangs
                                  where kh.MaTN == buildingId & (kh.IsNgungSuDung == null || kh.IsNgungSuDung == false) & kh.MaKH == khachHangId
                                  select new
                                  {
                                      kh.MaKH,
                                      kh.KyHieu,
                                      kh.MaPhu,
                                      TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                      DienThoai = kh.DienThoaiKH,
                                      kh.EmailKH,
                                      DiaChi = kh.DCLL,
                                      MaMB = db.dvHoaDons.First(p =>
                                          p.MaKH == kh.MaKH & SqlMethods.DateDiffDay(p.NgayTT, denNgay) > 0 & p.IsDuyet == true &
                                          p.MaMB != null).MaMB,
                                  }).ToList();

                #region Số dư đầu năm trước
                var namTruoc = year - 1;
                var soDuDauNamTruoc = (from d in db.dvDauKies where d.MaTN == buildingId & d.Nam == namTruoc group new { d } by new { d.MaKH, d.MaLDV } into g select new { MaKH = g.Key.MaKH, SoTien = g.Sum(_ => _.d.SoTien).GetValueOrDefault(), MaLDV = g.Key.MaLDV }).ToList();

                var lDauKy_SoDuDauNamTruoc = (from kh in khachHangs join d in soDuDauNamTruoc on kh.MaKH equals d.MaKH into dk from d in dk.DefaultIfEmpty() select new { MaKH = kh.MaKH, SoTien = d == null ? 0 : d.SoTien, MaLDV = d == null ? 0 : d.MaLDV, MaMB = kh.MaMB }).ToList();

                #endregion

                var noDauKyHd1 = (from hd in db.dvHoaDons
                                  where SqlMethods.DateDiffDay(hd.NgayTT, denNgay) > 0 & hd.IsDuyet == true & hd.MaTN == buildingId & hd.MaKH == khachHangId & hd.NgayTT.Value.Year == namTruoc
                                  select new { hd.ID, hd.MaKH, hd.PhaiThu, hd.MaLDV }).ToList();

                var noDauKyHd_Ldv = (from hd in noDauKyHd1
                                     group hd by new { hd.MaKH, hd.MaLDV }
                                         into ndk
                                         select new { MaKH = ndk.Key.MaKH, SoTien = ndk.Sum(_ => _.PhaiThu), MaLDV = ndk.Key.MaLDV }).ToList();

                // errror???????????????????
                // phải tách ra thì họa may k bị timeout
                var noDauKySq1 = (from sq in db.SoQuy_ThuChis
                                  where SqlMethods.DateDiffDay(sq.NgayPhieu, denNgay) > 0 & sq.MaTN == buildingId &
                                        sq.IsPhieuThu == true & sq.MaLoaiPhieu != 24 & sq.LinkID != null &
                                        sq.TableName == "dvHoaDon" & sq.MaKH == khachHangId & sq.NgayPhieu.Value.Year == namTruoc
                                  select new
                                  {
                                      sq.MaKH,
                                      sq.LinkID,
                                      //SoTien = sq.DaThu.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault() - sq.ThuThua.GetValueOrDefault(),
                                      SoTien = sq.DaThu.GetValueOrDefault() + sq.KhauTru.GetValueOrDefault() - sq.ThuThua.GetValueOrDefault()
                                  }).ToList();
                var noDauKySq_Ldv = (from sq in noDauKySq1
                                     join hd in noDauKyHd1 on sq.LinkID equals hd.ID
                                     group new { sq, hd } by new { sq.MaKH, hd.MaLDV }
                                         into ndk
                                         select new { MaKH = ndk.Key.MaKH, SoTien = ndk.Sum(_ => _.sq.SoTien), MaLDV = ndk.Key.MaLDV })
                    .ToList();

                // list
                var lDauKy_Ldv_1 = (from kh in khachHangs
                                    join ndk in noDauKyHd_Ldv on kh.MaKH equals ndk.MaKH into nodk
                                    from ndk in nodk.DefaultIfEmpty()
                                    select new
                                    {
                                        MaKH = kh.MaKH,
                                        SoTien = ndk == null ? 0 : ndk.SoTien.GetValueOrDefault(),
                                        MaLDV = ndk == null ? 0 : ndk.MaLDV,
                                        MaMB = kh.MaMB
                                    }).ToList();
                var lDauKy_Ldv_2 = (from kh in khachHangs
                                    join sqdk in noDauKySq_Ldv on kh.MaKH equals sqdk.MaKH into soquydk
                                    from sqdk in soquydk.DefaultIfEmpty()
                                    select new
                                    {
                                        MaKH = kh.MaKH,
                                        SoTien = -(sqdk == null ? 0 : sqdk.SoTien),
                                        MaLDV = sqdk == null ? 0 : sqdk.MaLDV,
                                        kh.MaMB
                                    }).ToList();
                var lDauKy_Ldv = lDauKy_Ldv_1.Concat(lDauKy_Ldv_2).Concat(lDauKy_SoDuDauNamTruoc);
                var dl = (from i in lDauKy_Ldv
                          group new { i } by new { i.MaKH, i.MaLDV, i.MaMB }
                              into g
                              select new { g.Key.MaKH, g.Key.MaLDV, g.Key.MaMB, SoTien = g.Sum(_ => _.i.SoTien) }).ToList();

                foreach (var item in dl)
                {
                    
                    dvDauKy dk;
                    dk = db.dvDauKies.FirstOrDefault(_ => _.MaKH == item.MaKH & _.MaMB == item.MaMB & _.MaLDV == item.MaLDV & _.Nam == year & _.MaTN == buildingId);
                    if (dk != null)
                    {
                        dk.SoTien = item.SoTien;
                    }
                    else
                    {
                        if (item.SoTien == 0) continue;
                        dk = new dvDauKy();
                        dk.MaKH = item.MaKH;
                        dk.MaLDV = item.MaLDV;
                        dk.MaMB = item.MaMB;
                        dk.MaTN = buildingId;
                        dk.Nam = year;
                        dk.SoTien = item.SoTien;
                        dk.DienGiai = "";
                        db.dvDauKies.InsertOnSubmit(dk);
                    }
                }

                db.SubmitChanges();
                LoadData();
                db.Dispose();
            }
            catch{}
        }

        private void itemSuaTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (long?)gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn khách hàng");
                    return;
                }

                using (var frm = new FrmDauKyEditMonney() {DauKyId = id})
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch{}
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new FrmDauKyEdit() { BuildingId = (byte?)itemBuilding.EditValue, Nam = Convert.ToInt32(itemYear.EditValue) })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var id = (long?)gv.GetFocusedRowCellValue("Id");
                if (id == null)
                {
                    Library.DialogBox.Error("Vui lòng chọn khách hàng");
                    return;
                }

                using (var frm = new FrmDauKyEdit() { DauKyId = id, BuildingId = (byte?)itemBuilding.EditValue, Nam = Convert.ToInt32(itemYear.EditValue) })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
                }
            }
            catch{}
        }
    }
}