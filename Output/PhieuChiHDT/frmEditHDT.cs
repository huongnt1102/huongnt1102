using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Fund.Output.PhieuChi
{
    public partial class frmEditHDT : DevExpress.XtraEditors.XtraForm
    {
        public frmEditHDT()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }
        public int? MaKH { get; set; }
        public int? MaThiCong { get; set; }
        public int? MaHD { get; set; }
        public decimal? SoTienCoc { get; set; }
        public string TableName { get; set; }
        public bool isThanhLy = false;
        public List<ChiTietPhieuChiItem> ChiTiets { get; set; }

        pcPhieuChi objPC;
        MasterDataContext db = new MasterDataContext();

        void TinhSoTien()
        {
            spSoTien.EditValue = objPC.pcChiTiets.Sum(o => o.SoTien);
        }

        void SavePhieuChi(bool _IsPrint)
        {
            gvChiTiet.UpdateCurrentRow();

            #region Rang buoc
            if (lkHTTT.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phương thức thanh toán");
                lkHTTT.Focus();
                return;
            }
            if (glDoiTuong.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if((int?)lkLoaiChi.EditValue==null)
            {
                DialogBox.Error("Vui lòng chọn loại chi");
                return;
            }
            if ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan") == true && lkTaiKhoanNganHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                return;
            }

            if (txtSoPC.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPC.Focus();
                return;
            }
            else
            {
                var count = db.pcPhieuChis.Where(p => p.MaTN == objPC.MaTN & p.SoPC == txtSoPC.Text & p.ID != objPC.ID).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng số phiếu thu. Vui lòng kiểm tra lại");
                    txtSoPC.Focus();
                    return;
                }
            }

            if (dateNgayChi.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thu");
                dateNgayChi.Focus();
                return;
            }

            if (spSoTien.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền");
                spSoTien.Focus();
                return;
            }

            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }

            var dbo = new MasterDataContext();
            var id = (int?)lkLoaiChi.EditValue;

            if (id != null )
            {
                var _maHD = (int?)glkHopDong.EditValue;

                if (_maHD == null)
                {
                    DialogBox.Error("Vui lòng chọn hợp đồng");
                    return;
                }

                if (id == 7)
                {
                    var ConLai = (from pt in dbo.ptPhieuThus
                                  where pt.MaKH == (int)glDoiTuong.EditValue & pt.MaPL == 25
                                  //& pt.idctHopDong == _maHD
                                  select pt.SoTien).Sum().GetValueOrDefault()

                                 - (from pt in dbo.ktttKhauTruThuTruocHDTs
                                    where pt.MaKH == (int)glDoiTuong.EditValue
                                    & pt.idctHopDong == _maHD
                                    & pt.MaPL == 25
                                    select pt.SoTien).Sum().GetValueOrDefault()

                                 - (from pt in db.pcPhieuChis
                                    where pt.MaNCC == (int)glDoiTuong.EditValue & pt.LoaiChi == id
                                    & pt.ID != this.ID.GetValueOrDefault()
                                    //& pt.idctHopDong == _maHD
                                    select pt.SoTien).Sum().GetValueOrDefault();

                    if (ConLai < spSoTien.Value)
                    {
                        var ThongBao = string.Format("Không thể chi vượt quá số tiền còn lại({0:n0})", ConLai);
                        DialogBox.Error(ThongBao);
                        spSoTien.Focus();
                        return;
                    }
                }

                if (id == 8)
                {
                    var ConLai = (from pt in dbo.ptPhieuThus
                                  where pt.MaKH == (int)glDoiTuong.EditValue & pt.MaPL == 22
                                  //& pt.idctHopDong == _maHD
                                  select pt.SoTien).Sum().GetValueOrDefault()

                                  - (from pt in dbo.ktttKhauTruThuTruocHDTs
                                     where pt.MaKH == (int)glDoiTuong.EditValue
                                     & pt.idctHopDong == _maHD
                                     & pt.MaPL == 22
                                     select pt.SoTien).Sum().GetValueOrDefault()

                                 - (from pt in db.pcPhieuChis
                                    where pt.MaNCC == (int)glDoiTuong.EditValue & pt.LoaiChi == id
                                    & pt.ID != this.ID.GetValueOrDefault()
                                    //& pt.idctHopDong == _maHD
                                    select pt.SoTien).Sum().GetValueOrDefault();

                    if (ConLai < spSoTien.Value)
                    {
                        var ThongBao = string.Format("Không thể chi vượt quá số tiền còn lại({0:n0})", ConLai);
                        DialogBox.Error(ThongBao);
                        spSoTien.Focus();
                        return;
                    }
                }

            }

            #endregion

            #region "   Set thong tin"
            //thông tin chứng từ
            objPC.SoPC = txtSoPC.Text;
            objPC.NgayChi = (DateTime)dateNgayChi.EditValue;
            objPC.SoTien = (decimal)spSoTien.EditValue;
            objPC.MaNV = (int)lkNhanVien.EditValue;
            objPC.LoaiChi = (int?)lkLoaiChi.EditValue;
            //thong tin chung
            objPC.MaNCC = (int)glDoiTuong.EditValue;
            objPC.NguoiNhan = txtNguoiNhan.EditValue + "";
            objPC.DiaChiNN = txtDiaChiNN.EditValue + "";
            if (lkTaiKhoanNganHang.EditValue != null)
                objPC.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
            else
                objPC.MaTKNH = null;
            objPC.ChungTuGoc = txtChungTuGoc.Text;
            objPC.LyDo = txtDienGiai.EditValue + "";
            objPC.IsHDThue = true;
            //objPC.idctHopDong = (int?)glkHopDong.EditValue;
            #endregion

            try
            {
                db.SubmitChanges();

                if (_IsPrint)
                {
                    using (var rpt = new rptPhieuChi(objPC.ID, this.MaTN.Value))
                    {
                        rpt.ShowPreviewDialog();
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {

            List<int> ltIDChi = new List<int>() { 8 };

            //if (this.isThanhLy)
                ltIDChi = new List<int>() { 7, 8 };

            TranslateLanguage.TranslateControl(this);
            gvChiTiet.InvalidRowException += Common.InvalidRowException;

            #region " Show  LookupItem"
            lkTaiKhoanNganHang.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                        where tk.MaTN == this.MaTN
                                                        select new
                                                        {
                                                            tk.ID,
                                                            tk.SoTK,
                                                            tk.ChuTK,
                                                            nh.TenNH,
                                                            nh.DiaChi
                                                        }).ToList();
            lkLoaiChi.Properties.DataSource = db.pcPhanLoais.Where(o => ltIDChi.Contains(o.ID));

            lkNhanVien.Properties.DataSource = (from n in db.tnNhanViens where n.MaTN == this.MaTN select new { n.MaNV, n.MaSoNV, n.HoTenNV }).ToList();
            glDoiTuong.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                     DiaChi = kh.DCLL,
                                                 }).ToList();
            //lkHTTT.Properties.DataSource = db.ptHinhThucThanhToans;
            lkHTTT.EditValue = 1;
            
            #endregion

            #region "   Show Thong tin"
            if (this.ID != null)
            {
                objPC = db.pcPhieuChis.Single(pt => pt.ID == ID);
                //thông tin chứng từ
                txtSoPC.EditValue = objPC.SoPC;
                dateNgayChi.EditValue = objPC.NgayChi;
                spSoTien.EditValue = objPC.SoTien;
                lkNhanVien.EditValue = Common.User.MaNV;
                lkLoaiChi.EditValue = objPC.LoaiChi;
                //thong tin chung
                glDoiTuong.EditValue = objPC.MaNCC;
                txtNguoiNhan.EditValue = objPC.NguoiNhan;
                txtDiaChiNN.EditValue = objPC.DiaChiNN;
                lkTaiKhoanNganHang.EditValue = objPC.MaTKNH;
                txtDienGiai.EditValue = objPC.LyDo;
                txtSoPC.EditValue = objPC.MaTKNH == null ? 0 : 1;
                txtChungTuGoc.Text = objPC.ChungTuGoc;
                lkNhanVien.EditValue = objPC.MaNV;

                objPC.MaNVS = Library.Common.User.MaNV;
                objPC.NgaySua = db.GetSystemDate();
            }
            else
            {
                objPC = new pcPhieuChi();
                objPC.MaTN = this.MaTN;

                //if(this.MaThiCong > 0)
                //    objPC.MaThiCong = this.MaThiCong;

                objPC.TableName = this.TableName;
                objPC.MaNVN = Library.Common.User.MaNV;
                objPC.NgayNhap = db.GetSystemDate();
                db.pcPhieuChis.InsertOnSubmit(objPC);

                lkHTTT.EditValue = 1;
                txtSoPC.EditValue = db.CreateSoChungTu(12, this.MaTN);

                dateNgayChi.EditValue = DateTime.Now;
                lkNhanVien.EditValue = Library.Common.User.MaNV;
                glDoiTuong.EditValue = this.MaKH;
                glkHopDong.EditValue = this.MaHD;

                if (this.ChiTiets != null)
                {
                    foreach (var i in this.ChiTiets)
                    {
                        var ct = new pcChiTiet();
                        //ct.TableName = i.TableName;
                        ct.LinkID = i.LinkID;
                        ct.DienGiai = i.DienGiai;
                        ct.SoTien = i.SoTien;
                        objPC.pcChiTiets.Add(ct);
                    }

                    this.TinhSoTien();
                }
            }

            gcChiTiet.DataSource = objPC.pcChiTiets;
            #endregion
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            db.Dispose();
            this.Close();
        }

        private void lkTaiKhoanNganHang_EditValueChanged(object sender, EventArgs e)
        {
            if (lkTaiKhoanNganHang.EditValue != null)
                txtTenNH.EditValue = db.nhTaiKhoans.Single(k => k.ID == (int)lkTaiKhoanNganHang.EditValue).nhNganHang.TenNH;
            else
                txtTenNH.EditValue = null;
        }

        private void gvChiTiet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if ((gvChiTiet.GetRowCellValue(e.RowHandle, "DienGiai") ?? "").ToString() == "")
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập diễn giải!";
                return;
            }

            var value = (decimal?)gvChiTiet.GetFocusedRowCellValue("SoTien") ?? 0;
            if (value <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập số tiền!";
                return;
            }
        }

        private void gcChiTiet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    gvChiTiet.DeleteSelectedRows();
                    this.TinhSoTien();
                }
            }
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (db != null)
                db.Dispose();
        }

        private void gvChiTiet_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                this.TinhSoTien();

                var strDienGiai = "";
                foreach (var i in objPC.pcChiTiets)
                {
                    strDienGiai += "; " + i.DienGiai;
                }

                strDienGiai = strDienGiai.Trim().Trim(';');
                txtDienGiai.Text = strDienGiai;
            }
            catch { }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            this.SavePhieuChi(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.SavePhieuChi(false);
        }

        private void glDoiTuong_EditValueChanged(object sender, EventArgs e)
        {
            HopDongLoad();
        }

        void HopDongLoad()
        {
            try
            {
                var _maPL = (int?)lkLoaiChi.EditValue;

                if (_maPL == null)
                {
                    glkHopDong.Properties.DataSource = null;
                    return;
                }

                var LoaiThu = _maPL == 7 ? 25 : 22;

                var r = glDoiTuong.Properties.GetRowByKeyValue(glDoiTuong.EditValue);

                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNhan.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChiNN.Text = (from mb in db.mbMatBangs
                                        join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                        where mb.MaKHF1 == (int)glDoiTuong.EditValue
                                        select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();

                    var ltTienThuTruoc = (from hd in db.ctHopDongs
                                          where hd.MaKH == (int)glDoiTuong.EditValue
                                          select new
                                          {
                                              MaHD = hd.ID,
                                              SoHD = hd.SoHDCT,
                                              NgayKy = hd.NgayKy,
                                              //DaThu = db.ptPhieuThus.Where(o => o.MaPL == LoaiThu & o.idctHopDong == hd.ID).Sum(o => o.SoTien).GetValueOrDefault(),
                                              DaKhauTru = db.ktttKhauTruThuTruocHDTs.Where(o => o.idctHopDong == hd.ID & o.MaPL == LoaiThu).Sum(o => o.SoTien).GetValueOrDefault(),
                                              //DaChi = db.pcPhieuChis.Where(o => o.LoaiChi == _maPL & o.idctHopDong == hd.ID).Sum(o => o.SoTien).GetValueOrDefault(),
                                              ConLai = 0,
                                          }).AsEnumerable()
                                         .Select(o => new
                                         {
                                             o.MaHD,
                                             //HienThi = string.Format("{0} - {1:n0}", o.SoHD, o.DaThu - o.DaKhauTru - o.DaChi),
                                             HienThi = string.Format("{0} - {1:n0}", o.SoHD, o.DaKhauTru),
                                             o.SoHD,
                                             o.NgayKy,
                                             //o.DaThu,
                                             o.DaKhauTru,
                                             //o.DaChi,
                                             //ConLai = o.DaThu - o.DaKhauTru - o.DaChi
                                         });
                    glkHopDong.Properties.DataSource = ltTienThuTruoc.ToList();
                }
            }
            catch { }
        }

        private void lkLoaiChi_EditValueChanged(object sender, EventArgs e)
        {
            HopDongLoad();
        }

        private void lkHTTT_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                lkTaiKhoanNganHang.Enabled = ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan")).GetValueOrDefault();
                MasterDataContext db = new MasterDataContext();

                switch ((int?)lkHTTT.EditValue)
                {
                    // CHUYỂN KHOẢN
                    case 2:
                            txtSoPC.EditValue = db.CreateSoChungTu(13, MaTN);
                        break;
                    default:
                            txtSoPC.EditValue = db.CreateSoChungTu(12, MaTN);
                        break;
                }
            }
            catch { }
        }
    }

    public class ChiTietPhieuChiItemHDT
    {
        public long? LinkID { get; set; }
        public string DienGiai { get; set; }
        public decimal? SoTien { get; set; }
    }
}