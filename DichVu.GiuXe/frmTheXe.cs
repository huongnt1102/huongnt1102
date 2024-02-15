using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using Library;

namespace DichVu.GiuXe
{
    public partial class frmTheXe : DevExpress.XtraEditors.XtraForm
    {
        IEnumerable<Library.APITheXe.LichSuTheXe> ltLichSu;
        MasterDataContext db = new MasterDataContext();
        public frmTheXe()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        void Add_TheXe()
        {
            using (var frm = new frmTheXeEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        void Edit_TheXe()
        {
            var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;
            }

            using (var frm = new frmTheXeEdit())
            {
                frm.ID = _ID;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        void Delete_TheXe()
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in indexs)
                {
                    var id = (int?)grvTheXe.GetRowCellValue(i, "ID");

                    var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == id & p.MaLDV == 6);

                    if (KTIDHD == null)
                    {
                        var tx = db.dvgxTheXes.Single(p => p.ID == id);
                        #region Luu lai LS xoa the xe Phu Luc XuanMai
                        byte? TN = (byte?)itemToaNha.EditValue;

                        var thexe = new dvgxTheXeDaXoa();
                        thexe.BienSo = tx.BienSo;
                        thexe.ChuThe = tx.ChuThe;
                        thexe.DienGiai = tx.DienGiai;
                        thexe.DoiXe = tx.DoiXe;
                        thexe.KyTT = tx.KyTT;
                        thexe.GiaNgay = tx.GiaNgay;
                        thexe.GiaThang = tx.GiaThang;
                        thexe.MaKH = tx.MaKH;
                        thexe.MaMB = tx.MaMB;
                        thexe.KyTT = tx.KyTT;
                        thexe.MaDM = tx.MaDM;
                        thexe.NgayXoa = DateTime.Now;
                        thexe.MaNVXoa = Common.User.MaNV;
                        thexe.NgayTT = tx.NgayTT;
                        thexe.PhiLamThe = tx.PhiLamThe;
                        thexe.NgayDK = tx.NgayDK;
                        thexe.TienTT = tx.TienTT;
                        thexe.MaTN = tx.MaTN;
                        thexe.SoThe = tx.SoThe;
                        thexe.MaMB = tx.MaMB;
                        thexe.MaKH = tx.MaKH;
                        thexe.MaLX = tx.MaLX;
                        thexe.MaNK = tx.MaNK;
                        db.dvgxTheXeDaXoas.InsertOnSubmit(thexe);
                        #endregion


                        if (!string.IsNullOrEmpty(tx.MaTheChip) && db.dvgxTheXes.Count(o => o.MaTheChip == tx.MaTheChip) == 1)
                        {
                            int? MaMB = (int?)grvTheXe.GetRowCellValue(i, "MaMB");
                            string sBlockCode = GetBlockCode(MaMB.GetValueOrDefault());
                            APITheXe.XoaTheXe(tx.MaTheChip, tx.MaTN.Value, sBlockCode, tx.RowID);
                            db.dvgxTheXes.DeleteOnSubmit(tx);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(tx.MaTheChip))
                            {
                                db.dvgxTheXes.DeleteOnSubmit(tx);
                            }
                        }
                    }
                    else
                    {
                        DialogBox.Error("Thẻ xe này đã có hoá đơn phát sinh vui lòng kiểm tra lại !");
                    }
                }

                db.SubmitChanges();

                LoadData();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        string GetBlockCode(int MaMB)
        {
            string sblockcode = "";
            if (MaMB == null)
                return sblockcode;
            var obj = (from mb in db.mbMatBangs
                       join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                       join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                       where mb.MaMB == MaMB
                       select new { kn.BlockCode }).FirstOrDefault();
            if (obj != null)
                sblockcode = obj.BlockCode;
            return sblockcode;
        }
        void Import_TheXe()
        {
            try
            {
                using (var frm = new frmImport())
                {
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.isSave)
                        LoadData();
                }
            }
            catch { }
        }

        private void frmTheXe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            grvTheXe.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[8];
            SetDate(8);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }
        void LoadData()
        {
            try
            {
                var maTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                gcTheXe.DataSource = from tx in db.dvgxTheXes
                                     join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                     join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID into tblGiuXe
                                     from gx in tblGiuXe.DefaultIfEmpty()
                                     join mb in db.mbMatBangs on tx.MaMB equals mb.MaMB
                                     join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                                     join nvn in db.tnNhanViens on tx.MaNVN equals nvn.MaNV
                                     join nvs in db.tnNhanViens on tx.MaNVS equals nvs.MaNV into tblNguoiSua
                                     from nvs in tblNguoiSua.DefaultIfEmpty()
                                     where tx.MaTN == maTN && tx.IsThangMay.GetValueOrDefault() == false
                                     select new
                                     {
                                         tx.ID,
                                         tx.MaGX,
                                         tx.MaMB,
                                         mb.MaSoMB,
                                         kh.MaPhu,
                                         TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                         tx.NgayDK,
                                         tx.SoThe,
                                         tx.ChuThe,
                                         tx.NgayNgungSD,
                                         lx.TenLX,
                                         lx.MaLX,
                                         tx.BienSo,
                                         tx.MauXe,
                                         tx.DoiXe,
                                         tx.GiaThang,
                                         tx.NgayTT,
                                         tx.KyTT,
                                         tx.TienTT,
                                         tx.DienGiai,
                                         //tx.SoDK,
                                         NguoiNhap = nvn.HoTenNV,
                                         tx.NgayNhap,
                                         NguoiSua = nvs.HoTenNV,
                                         gx.NgaySua,
                                         tx.NgungSuDung,
                                         tx.PhiLamThe,
                                         tx.TienTruocThue,
                                         tx.ThueGTGT,
                                         tx.TienThueGTGT,
                                         tx.MaTheChip,
                                         tx.TuNgay,
                                         tx.DenNgay,
                                         TenKN = mb.mbTangLau.mbKhoiNha.TenKN,
                                         tx.NgayKichHoatThe
                                         , tx.SoTienCoc
                                     };
            }
            catch
            {
                gcTheXe.DataSource = null;
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            // APITheXe.GetLichSu(DateTime.Now.AddDays(-7), DateTime.Now, "", (byte)itemToaNha.EditValue);
            this.LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Add_TheXe();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit_TheXe();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete_TheXe();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Import_TheXe();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcTheXe);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void DetailGiuXe()
        {
            var db = new MasterDataContext();
            try
            {
                var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");
                var MaMB = (int?)grvTheXe.GetFocusedRowCellValue("MaMB");
                if (_ID == null)
                {
                    gridControl1.DataSource = null;
                    //gcLTT.DataSource = null;
                }

                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        gridControl1.DataSource = (from ls in db.dvgxTheXe_Backups
                                                   join nv in db.tnNhanViens on ls.MaNVN equals nv.MaNV
                                                   join lx in db.dvgxLoaiXes on ls.MaLX equals lx.MaLX
                                                   //join gx in db.dvgxGiuXes on ls.MaGX equals gx.ID into tblGiuXe
                                                   //from gx in tblGiuXe.DefaultIfEmpty()
                                                   join mb in db.mbMatBangs on ls.MaMB equals mb.MaMB
                                                   join kh in db.tnKhachHangs on ls.MaKH equals kh.MaKH


                                                   where ls.MaThe == _ID
                                                   select new
                                                   {
                                                       ls.ID,
                                                       ls.MaGX,
                                                       mb.MaSoMB,
                                                       mb.MaMB,
                                                       TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                       ls.NgayDK,
                                                       ls.SoThe,
                                                       ls.ChuThe,
                                                       ls.NgayNgungSD,
                                                       lx.TenLX,
                                                       lx.MaLX,
                                                       ls.BienSo,
                                                       ls.MauXe,
                                                       ls.DoiXe,
                                                       ls.GiaThang,
                                                       ls.NgayTT,
                                                       ls.KyTT,
                                                       ls.TienTT,
                                                       ls.DienGiai,
                                                       //gx.SoDK,
                                                       NguoiNhap = nv.HoTenNV,
                                                       ls.NgayNhap,

                                                       ls.NgungSuDung,
                                                       ls.PhiLamThe,

                                                   }).ToList();

                        break;
                    //case 1:
                    //    var MaTheChip = grvTheXe.GetFocusedRowCellValue("MaTheChip") as string;
                    //    var TuNgay = DateTime.Now;
                    //    var DenNgay = TuNgay.AddDays(-7);
                    //    gcLichSuRaVao.DataSource = ltLichSu.Where( o=>o.ID == MaTheChip);
                    //    break;
                    case 2:
                        gcLichSuVeThang.DataSource = null;
                        var MaTheChip = grvTheXe.GetFocusedRowCellValue("MaTheChip");
                        if (MaTheChip != null)
                        {
                            gcLichSuVeThang.DataSource = APITheXe.GetLichSuVeThang((byte)itemToaNha.EditValue, MaTheChip.ToString(), GetBlockCode(MaMB.GetValueOrDefault()));
                        }
                        break;
                }

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void grvTheXe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DetailGiuXe();
        }

        private void grvTheXe_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var frm = new frmEditMuti();
            frm.MaTN = (byte?)itemToaNha.EditValue;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemCapNhatDGXe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Tòa nhà không có thẻ xe");
                return;

            }

            TheXe_SetDonGia cls = new TheXe_SetDonGia();
            if (cls.SetDonGia((byte)itemToaNha.EditValue) == false)
            {
                DialogBox.Error("Cập nhật bị lỗi! Vui lòng thử lại");
            }
            else
            {
                DialogBox.Alert("Cập nhật dữ liệu thành công!");
            }
            LoadData();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)//Ngưng
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Tòa nhà không có thẻ xe");
                return;

            }

            var frm = new frmAddAuto();
            frm.Duyet = true;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                DialogBox.Alert("Cập nhật thành công!");
                LoadData();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Tòa nhà không có thẻ xe");
                return;

            }

            var frm = new frmAddAuto();
            frm.Duyet = false;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                DialogBox.Alert("Cập nhật thành công!");
                LoadData();
            }
        }

        private async void itemNgungSD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }
            if (DialogBox.Question("Bạn thật sự muốn ngưng sử dụng thẻ xe đang chọn") == DialogResult.No)
                return;

            foreach (var i in indexs)
            {
                var _ID = (int?)grvTheXe.GetRowCellValue(i, "ID");
                if (_ID != null)
                {
                    using (var db = new MasterDataContext())
                    {
                        var tx = db.dvgxTheXes.Single(o => o.ID == _ID);

                        if (tx.NgungSuDung.GetValueOrDefault()) return;
                        
                        tx.NgungSuDung = true;
                        tx.NgayNgungSD = Common.GetDateTimeSystem();
                        if (!string.IsNullOrEmpty(tx.MaTheChip))
                        {
                            if (tx.MaTheChip.Length > 0)
                            {
                                var getNgungSuDung = false;
                                await System.Threading.Tasks.Task.Run(() => { getNgungSuDung = APITheXe.NgungSuDung(tx.MaTheChip, tx.MaTN.Value, GetBlockCode(tx.MaMB.GetValueOrDefault())); });
                                if (getNgungSuDung)
                                {
                                    
                                }
                                else
                                {
                                    DialogBox.Alert("Có lỗi xảy ra trong quá trình đồng bộ. Vui lòng kiểm tra lại!");
                                }
                            }
                            else
                            {
                                db.SubmitChanges();
                                LoadData();
                            }

                        }
                        else
                        {
                            db.SubmitChanges();
                            LoadData();
                        }


                    }
                }
            }

            //db.SubmitChanges();
            DialogBox.Alert("Ngưng sử dụng thành công!");
            LoadData();

        }

        private void itemKichHoatThe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }

            if (DialogBox.Question("Bạn thật sự muốn kích hoạt lại thẻ xe đang chọn") == DialogResult.No)
                return;

            
            foreach (var i in indexs)
            {
                var _ID = (int?)grvTheXe.GetRowCellValue(i, "ID");
                if (_ID != null)
                {
                    using (var db = new MasterDataContext())
                    {
                        var tx = db.dvgxTheXes.Single(o => o.ID == _ID);
                        if (!tx.NgungSuDung.GetValueOrDefault()) return;
                        
                        tx.NgungSuDung = false;
                        tx.NgayNgungSD = null;

                        if (!string.IsNullOrEmpty(tx.MaTheChip))
                        {
                            var objDB = APITheXe.KichHoatThe(tx.MaTheChip, tx.MaTN.Value, GetBlockCode(tx.MaMB.GetValueOrDefault())).FirstOrDefault();
                            if (objDB != null)
                            {
                                // var objSearch = APITheXe.SearchVeThang(tx.MaTN.Value, GetBlockCode(tx.MaMB), tx.MaTheChip);
                                tx.RowID = objDB.rowid;
                                
                            }
                            else
                            {
                                DialogBox.Alert("Có lỗi xảy ra trong quá trình đồng bộ. Vui lòng kiểm tra lại");
                            }
                        }
                        else
                        {
                            //DialogBox.Alert("Đã kích hoạt thẻ thành công!");
                            db.SubmitChanges();
                            LoadData();
                        }
                    }
                }
            }
            DialogBox.Alert("Đã kích hoạt thẻ thành công!");
            //db.SubmitChanges();
            LoadData();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            DetailGiuXe();
        }

        private void gvLichSuRaVao_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                // Đóng tạm
                //Dictionary<string, string> ltImage = new Dictionary<string, string>();
                //Library.APITheXe.LichSuTheXe obj = (Library.APITheXe.LichSuTheXe)gvLichSuRaVao.GetFocusedRow();
                //ltImage.Add("Ảnh 1", obj.ImagesURL1);
                //ltImage.Add("Ảnh 2", obj.ImagesURL2);
                //ltImage.Add("Ảnh 3", obj.ImagesURL3);
                //ltImage.Add("Ảnh 4", obj.ImagesURL4);

                //gcImage.DataSource = ltImage.Where(o => o.Value != null);
            }
            catch
            {
            }
        }

        private void gvImage_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                // Đóng tạm
                //var Url = gvImage.GetFocusedRowCellValue("Value").ToString();
                //pic.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(Url)));
            }
            catch
            {
            }
        }

        private void itemGiaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }

            int? MaMB = 0;
            List<int> ltIDTheXe = new List<int>();
            foreach (var i in indexs)
            {
                var id = (int?)grvTheXe.GetRowCellValue(i, "ID");
                if (id != null)
                {
                    ltIDTheXe.Add(id.Value);
                    MaMB = (int?)grvTheXe.GetRowCellValue(i, "MaMB");
                }
            }
            if (APITheXe.GiaHanVeThang((byte)itemToaNha.EditValue, ltIDTheXe, GetBlockCode(MaMB.GetValueOrDefault())))
            {
                DialogBox.Alert("Đã gia hạn thẻ thành công!");
                LoadData();
            }
            else
            {
                DialogBox.Alert("Có lỗi xảy ra trong quá trình gia hạn. Vui lòng kiểm tra lại!");
            }
        }

        private void itemDongBoTheChup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Tòa nhà không có thẻ xe");
                return;

            }

            process.Maximum = 0;
            itemProcess.EditValue = 0;
            itemNap.Enabled = false;
            itemProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            itemDongBoTheChup.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var ltTheDangSD = APITheXe.Get_VeThangDangSuDung((byte)itemToaNha.EditValue);
                var TongSo = ltTheDangSD.Count();
                this.BeginInvoke(new Action(() =>
                {
                    process.Step = 1;
                    process.PercentView = true;
                    process.Maximum = TongSo;
                    process.Minimum = 0;
                }));

                int i = 0;
                foreach (var item in ltTheDangSD)
                {
                    var BienSo = item.Digit.Replace("-", "").Replace(".", "").Replace(" ", "").ToUpper();
                    //var objTheXe=db.dvgxTheXes.Where(o => o.BienSo.Replace("-", "").Replace(".", "").Replace(" ", "").ToUpper().Equals(BienSo));

                    var objTheXe = db.dvgxTheXes.FirstOrDefault(o => o.BienSo.Replace("-", "").Replace(".", "").Replace(" ", "").ToUpper().Equals(BienSo) & o.MaMB!=null & (db.mbMatBangs.FirstOrDefault(mb=>mb.MaMB==o.MaMB).mbTangLau.mbKhoiNha.BlockCode.Equals(item.blockCode)) & o.MaTN == (byte)itemToaNha.EditValue);

                    if (objTheXe != null)
                    {
                        objTheXe.MaTheChip = item.id;
                        objTheXe.RowID = item.rowid;
                        objTheXe.TuNgay = item.DateStart;
                        objTheXe.DenNgay = item.DateEnd;
                        objTheXe.NgayKichHoatThe = item.DayUnLimit;
                    }
                    this.BeginInvoke(new Action(() =>
                    {
                        itemProcess.Caption = string.Format("Đang đồng bộ thẻ chip({0:n0}/{1:n0})", i, TongSo);
                        itemProcess.EditValue = i;
                    }));
                    i++;

                    db.SubmitChanges();
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                itemProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemNap.Enabled = true;
                itemDongBoTheChup.Enabled = true;
            }));
        }

        private void itemKhoaThe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }

            var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");

            if (_ID == null) return;
            if (DialogBox.Question("Bạn thật sự muốn khóa vĩnh viễn thẻ xe đang chọn") == DialogResult.No)
                return;
            using (var db = new MasterDataContext())
            {
                var tx = db.dvgxTheXes.Single(o => o.ID == _ID);
                tx.NgungSuDung = true;
                tx.NgayNgungSD = Common.GetDateTimeSystem();
                tx.IsKhoaThe = true;
                tx.NgayKhoaThe = Common.GetDateTimeSystem();
                if (tx.MaTheChip.Length > 0)
                {
                    if (APITheXe.KhoaThe(tx.MaTheChip, tx.MaTN.Value, GetBlockCode(tx.MaMB.GetValueOrDefault())))
                    {
                        tx.MaTheChip = "";
                        tx.TuNgay = (DateTime?)null;
                        tx.DenNgay = (DateTime?)null;
                        tx.SoThe = "";
                        tx.NgayKichHoatThe = (DateTime?)null;
                        DialogBox.Alert("Đã khóa thẻ thành công!");
                        db.SubmitChanges();
                        LoadData();
                    }
                    else
                    {
                        DialogBox.Alert("Có lỗi xảy ra trong quá trình đồng bộ. Vui lòng kiểm tra lại!");
                    }
                }
                else
                {
                    DialogBox.Alert("Thẻ xe này không có để đồng bộ");
                }
            }
        }

        private void itemBlackList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmBlackList())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
            }
        }

        //private void itemHuyKhoaThe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");
        //    var MaMB = (int?)grvTheXe.GetFocusedRowCellValue("MaMB");
        //    if (_ID == null) return;

        //    using (var db = new MasterDataContext())
        //    {
        //        var tx = db.dvgxTheXes.Single(o => o.ID == _ID);

        //        if (tx.MaTheChip != null)
        //        {
        //            if (APITheXe.HuyKhoaThe(tx.MaTheChip.ToString(), tx.RowID.ToString(), tx.MaTN.Value, GetBlockCode(MaMB)))
        //            {
        //                tx.IsKhoaThe = false;
        //                tx.NgayKhoaThe = (DateTime?)null;
        //                tx.RowID = (int?)null;
        //                tx.MaTheChip = "";
        //                tx.SoThe = "";
        //                tx.TuNgay = (DateTime?)null;
        //                tx.DenNgay = (DateTime?)null;
        //                db.SubmitChanges();
        //                DialogBox.Alert("Đã hủy khóa thẻ thành công!");
        //            }
        //        }
        //        else
        //        {
        //            tx.SoThe = "";
        //            tx.IsKhoaThe = false;
        //            tx.NgayKhoaThe = (DateTime?)null;
        //            tx.RowID = (int?)null;
        //            tx.MaTheChip = "";
        //            tx.TuNgay = (DateTime?)null;
        //            tx.DenNgay = (DateTime?)null;
        //            db.SubmitChanges();
        //        }

        //        LoadData();
        //    }
        //}

        private void itemNgungTheXeVaThuHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }

            if (DialogBox.Question("Bạn thật sự muốn ngưng và thu hồi các thẻ xe đang chọn") == DialogResult.No)
                return;

            //var _ID = (int?)grvTheXe.GetFocusedRowCellValue("ID");

            //if (_ID == null) return;

            foreach (var i in indexs)
            {
                var _ID = (int?)grvTheXe.GetRowCellValue(i, "ID");
                if (_ID != null)
                {
                    using (var db = new MasterDataContext())
                    {

                        var tx = db.dvgxTheXes.Single(o => o.ID == _ID);
                        tx.NgungSuDung = true;
                        tx.NgayNgungSD = Common.GetDateTimeSystem();

                        if (!string.IsNullOrEmpty(tx.MaTheChip))
                        {
                            if (APITheXe.XoaTheXe(tx.MaTheChip, tx.MaTN.Value, GetBlockCode(tx.MaMB.GetValueOrDefault()), tx.RowID))
                            {
                                tx.TuNgay = (DateTime?)null;
                                tx.DenNgay = (DateTime?)null;
                                tx.NgayKichHoatThe = (DateTime?)null;
                                tx.SoThe = "";
                                tx.RowID = (int?)null;
                                tx.MaTheChip = "";
                                db.SubmitChanges();
                                
                                LoadData();
                            }
                            else
                            {
                                DialogBox.Alert("Có lỗi trong quá trình thu hồi thẻ. Vui lòng kiểm tra lại!");
                            }

                        }
                        else
                        {
                            db.SubmitChanges();
                            LoadData();
                        }
                    }
                }
            }
            DialogBox.Alert("Đã hủy thẻ và thu hồi thành công!");

        }

        private void itemThongKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThongKeTheXe frm = new frmThongKeTheXe();
            frm.MaTN = Convert.ToByte(itemToaNha.EditValue);
            frm.TuNgay = Convert.ToDateTime(itemTuNgay.EditValue);
            frm.DenNgay = Convert.ToDateTime(itemDenNgay.EditValue);
            frm.ShowDialog();

        }

        private void itemDongBoTheXeChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn thẻ xe");
                return;

            }

            var db = new MasterDataContext();
            try
            {
                var ltTheDangSD = APITheXe.Get_VeThangDangSuDung((byte)itemToaNha.EditValue);
                foreach (var i in indexs)
                {
                    var BienSo = grvTheXe.GetRowCellValue(i, "BienSo");
                    var _id = (int?)grvTheXe.GetRowCellValue(i, "ID");
                    var _blockCode = GetBlockCode(Convert.ToInt32(grvTheXe.GetRowCellValue(i, "MaMB")));
                    if (BienSo != null)
                    {
                        BienSo = BienSo.ToString().Replace("-", "").Replace(".", "").Replace(" ", "").ToLower();
                        var objKT = ltTheDangSD.FirstOrDefault(p => p.Digit.Replace("-", "").Replace(".", "").Replace(" ", "").ToLower().Equals(BienSo) && p.blockCode.Equals(_blockCode));
                        if (objKT != null)
                        {
                            var tx = db.dvgxTheXes.FirstOrDefault(p => p.ID == _id);
                            tx.MaTheChip = objKT.id;
                            tx.RowID = objKT.rowid;
                            tx.TuNgay = objKT.DateStart;
                            tx.DenNgay = objKT.DateEnd;
                            tx.NgayKichHoatThe = objKT.DayUnLimit;
                            tx.SoThe = objKT.stt.ToString();
                            db.SubmitChanges();
                        }
                        else
                        {
                            var tx = db.dvgxTheXes.FirstOrDefault(p => p.ID == _id);
                            tx.MaTheChip ="";
                            tx.RowID = (int?)null;
                            tx.TuNgay =(DateTime?)null;
                            tx.DenNgay = (DateTime?)null;
                            tx.NgayKichHoatThe = (DateTime?)null;
                            tx.SoThe = "";
                            db.SubmitChanges();
                        }
                    }
                }
                DialogBox.Alert("Đã đồng bộ xong!");
                LoadData();
            }
            catch
            {
            }
        }

        private void itemCapNhatDonGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvTheXe.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Tòa nhà không có thẻ xe");
                return;

            }

            List<int> ids = new List<int>();

            List<int?> MaLX = new List<int?>();

            foreach (var i in indexs)
            {
                var id = (int?)grvTheXe.GetRowCellValue(i, "ID");

                var _MaLX = (int?)grvTheXe.GetRowCellValue(i, "MaLX");

                if (id == null)
                    continue;

                if (MaLX.Contains(_MaLX) == false && MaLX.Count() == 0)
                    MaLX.Add(_MaLX);

                if (MaLX.Contains(_MaLX) == false)
                {
                    DialogBox.Error("Có 2 loại xe khác nhau vui lòng chọn lại");
                    return;
                }
                else
                {
                    ids.Add(id.Value);
                }

            }

            if (ids.Count() == 0)
            {
                DialogBox.Error("Vui lòng chọn dữ liệu xử lý");
                return;
            }

            using (var frm = new Library.FrmCapNhatPql(ids))
            {
                frm.MaLDV = 6;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK) LoadData();
            }
        }


        private async void itemTaoLaiCongNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No)
                return;

            await System.Threading.Tasks.Task.Run(() => { TaoLaiCongNo(); });

            Library.DialogBox.Success();
            LoadData();
        }

        private void TaoLaiCongNo()
        {
            var indexs = grvTheXe.GetSelectedRows();
            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    var _ID = (int?)grvTheXe.GetRowCellValue(i, "ID");

                    var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == _ID);
                    if (tx == null) continue;

                    try
                    {
                        db.dvHoaDon_InsertAll_LoaiDichVu(tx.MaTN, tx.NgayTT.Value.Month, tx.NgayTT.Value.Year, Library.Common.User.MaNV, tx.ID, 6);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void itemImportDanhSachNgungSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new frmImportXeNgungSuDung())
                {
                    frm.MaTN = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.isSave)
                        LoadData();
                }
            }
            catch { }
        }
    }
}