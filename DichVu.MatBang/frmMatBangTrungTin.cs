using System;
using System.Data;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.Windows.Forms;

namespace MatBang
{
    public partial class frmMatBangTrungTin : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DataTable tblMB;
        DateTime now;
        int? IDMatBang;
        int? MaTT;

        public frmMatBangTrungTin()
        {
            InitializeComponent();

            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            var strKhoiNha = (itemKhoiNha.EditValue ?? "").ToString().Replace(" ", "");
            var arrKhoiNha = strKhoiNha.Split(',');
            string strKN="";
            foreach (var i in arrKhoiNha)
            {
                strKN += "-" + i + "-";
            }
            if (itemKhoiNha.EditValue == null)
            {
                gcMatBang.DataSource = null;
                return;
            }

            //int MaKN = (int)itemKhoiNha.EditValue;

            DataTable tblTL = SqlCommon.getData(
                  "select mb.MaTL, mb.SoNha, tl.TenTL, count(MaSoMB) as SL,kn.TenKN from mbMatBang mb "
                + "inner join mbTangLau tl on tl.MaTL=mb.MaTL "
                + "inner join mbKhoiNha kn on kn.MaKN=tl.MaKN "
                + "where '" + strKN + "' LIKE '%-'+CAST(tl.MaKN AS NVARCHAR(max))+'-%'"
                + " group by mb.MaTL, mb.SoNha, tl.TenTL, tl.STT,kn.TenKN order by tl.STT");

            if (tblTL.Rows.Count <= 0)
            {
                gcMatBang.DataSource = null;
                return;
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                tblMB = SqlCommon.getData("select SoNha, MaMB, MaSoMB, mb.MaTT, mb.DienTich, mb.GiaThue as DonGia, mb.DienTich * mb.GiaThue as ThanhTien, lt.KyHieuLT as TenTG, mb.MaTL, " +
                    "TenTL, TenTT,kn.TenKN, tt.MauNen from mbMatBang mb "
                    + "inner join mbTrangThai tt on tt.MaTT=mb.MaTT "
                    + "inner join mbTangLau tl on tl.MaTL=mb.MaTL "
                    + "inner join mbKhoiNha kn on kn.MaKN=tl.MaKN "
                    + "inner join LoaiTien lt on lt.ID=mb.MaLT "
                    + "where '" + strKN + "'LIKE '%-'+CAST(tl.MaKN AS NVARCHAR(max))+'-%'");

                    DataTable tbl = new DataTable();
                grvMatBang.Columns.Clear();

                var coltl = tblTL.AsEnumerable()
                    .Select(p => new
                    {
                        TenTL = p.Field<string>("TenTL") + "-" + p.Field<string>("TenKN"),
                        MaTL = p.Field<int>("MaTL"),
                        //TenKN = p.Field<string>("TenKN")
                    })
                    .Distinct();

                foreach (var item in coltl)
                {
                    tbl.Columns.Add(item.MaTL.ToString());

                    DevExpress.XtraGrid.Columns.GridColumn colMaSo = new DevExpress.XtraGrid.Columns.GridColumn();
                    colMaSo.Caption = item.TenTL;
                    colMaSo.FieldName = item.MaTL.ToString();
                    colMaSo.Visible = true;
                    grvMatBang.Columns.Add(colMaSo);
                }

                for (int i = 0; i < tblTL.Rows.Count; i++)
                {
                    DataRow[] rMB = tblMB.Select("MaTL=" + tblTL.Rows[i]["MaTL"].ToString());
                    for (int j = 0; j < rMB.Length; j++)
                    {
                        if (tbl.Rows.Count < j + 1)
                        {
                            DataRow rNew = tbl.NewRow();
                            tbl.Rows.Add(rNew);
                        }
                        var index = coltl.FirstOrDefault(p => p.MaTL.ToString() == tblTL.Rows[i]["MaTL"].ToString()).MaTL.ToString();
                        tbl.Rows[j][index] = rMB[j]["MaSoMB"].ToString();
                    }
                }

                gcMatBang.DataSource = tbl;
                grvMatBang.BestFitColumns();
            }
            catch { gcMatBang.DataSource = null; }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void LoadDetail()
        {
            switch (tcdetail.SelectedTabPageIndex)
            {
                case 0:
                    #region Thong tin mat bang
                    var obj = db.mbMatBangs.Where(p => p.MaMB == IDMatBang & p.mbTangLau.mbKhoiNha.MaKN == (int)itemKhoiNha.EditValue)
                        .Select(p => new
                        {
                            p.MaMB,
                            p.MaSoMB,
                            db.mbLoaiMatBangs.SingleOrDefault(p1 => p1.MaLMB == p.MaLMB).TenLMB,
                            p.DienTich,
                            DonGia = p.GiaThue,
                            ThanhTien = p.DienTich * p.GiaThue,
                            p.mbTangLau.TenTL,
                            p.mbTrangThai.TenTT,
                            p.DienGiai,
                            p.SoNha
                        }).ToList();

                    vgcMatBang.DataSource = obj;
                    txtDienGiai.Text = obj.FirstOrDefault().DienGiai;
                    #endregion
                    break;
                case 1:
                    #region Cu dan
                    ctlCuDan1.MaMB = IDMatBang;
                    ctlCuDan1.CuDanLoadData();
                    #endregion
                    break;
                case 2:
                    #region Hop dong cho thue
                    gcHD.DataSource = (from ct in db.ctChiTiets
                                       join p in db.ctHopDongs on ct.MaHDCT equals p.ID
                                       join kh in db.tnKhachHangs on p.MaKH equals kh.MaKH
                                       join lt in db.LoaiTiens on p.MaLT equals lt.ID
                                       where ct.MaMB == IDMatBang
                                       orderby p.NgayKy descending
                                       select new
                                       {
                                           ct.DienTich,
                                           ct.DonGia,
                                           ct.PhiDichVu,
                                           ct.TongGiaThue,
                                           ct.TyLeVAT,
                                           ct.TienVAT,
                                           ct.TyLeCK,
                                           ct.TienCK,
                                           ct.PhiSuaChua,
                                           TenLT = lt.KyHieuLT,
                                           p.TyGia,
                                           TongTien = (from ltt in db.ctLichThanhToans where ltt.MaHD == ct.MaHDCT & ltt.MaMB == ct.MaMB select ltt.SoTienQD).Sum(),
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           p.SoHDCT,
                                           p.NgayKy,
                                           p.ThoiHan,
                                           p.NgayHH,
                                           p.NgayBG,
                                           ct.NgungSuDung
                                       }).ToList();
                    #endregion
                    break;
                case 3:
                    #region The xe
                    gcTheXe.DataSource = (from tx in db.dvgxTheXes
                                          join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                                          join gx in db.dvgxGiuXes on tx.MaGX equals gx.ID into tblGiuXe
                                          from gx in tblGiuXe.DefaultIfEmpty()
                                          join kh in db.tnKhachHangs on tx.MaKH equals kh.MaKH
                                          join nvn in db.tnNhanViens on tx.MaNVN equals nvn.MaNV
                                          join nvs in db.tnNhanViens on tx.MaNVS equals nvs.MaNV into tblNguoiSua
                                          from nvs in tblNguoiSua.DefaultIfEmpty()
                                          where tx.MaMB == IDMatBang
                                          select new
                                          {
                                              tx.ID,
                                              tx.MaGX,
                                              TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                              tx.NgayDK,
                                              tx.SoThe,
                                              tx.ChuThe,
                                              lx.TenLX,
                                              tx.BienSo,
                                              tx.MauXe,
                                              tx.DoiXe,
                                              tx.GiaNgay,
                                              tx.GiaThang,
                                              tx.PhiLamThe,
                                              tx.DienGiai,
                                              gx.SoDK,
                                              NguoiNhap = nvn.HoTenNV,
                                              gx.NgayNhap,
                                              NguoiSua = nvs.HoTenNV,
                                              gx.NgaySua,
                                              tx.NgungSuDung
                                          }).ToList();
                    #endregion
                    break;
            }
        }

        private void frmMatBangTrungTin_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            now = db.GetSystemDate();

            lookToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemKhoiNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        string getRowInfo(string id)
        {
            string infoText =string.Format(" Mã số: <b>{0}</b>", id);

            DataRow r = tblMB.Select("MaSoMB='" + id + "'")[0];

            infoText += string.Format("\r\n Số nhà: <b>{0}</b>", r["SoNha"]);
            infoText += string.Format("\r\n Tổng: <b>{0}</b>", r["TenTL"]);
            infoText += string.Format("\r\n Diện tích: <b>{0:#,0.##} m2</b>", r["DienTich"]);
            infoText += string.Format("\r\n Ðơn giá: <b>{0:#,0.##} {1}/m2</b>", r["DonGia"], r["TenTG"]);
            infoText += string.Format("\r\n Giá bán/cho thuê: <b><color=255, 0, 0>{0:#,0.##} {1}</color></b>", r["ThanhTien"], r["TenTG"]);
            infoText += string.Format("\r\n Trạng thái: <b>{0}</b>", r["TenTT"]);

            return infoText;
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gcMatBang) return;

            ToolTipControlInfo info = null;

            try
            {
                GridHitInfo hi = grvMatBang.CalcHitInfo(e.ControlMousePosition);
                if (hi.RowHandle < 0) return;

                if (hi.InRowCell)
                {
                    string id = grvMatBang.GetRowCellValue(hi.RowHandle, hi.Column).ToString();
                    if (id == "") return;
                    info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), getRowInfo(id));
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void grvMatBang_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                string maSoMB = grvMatBang.GetRowCellValue(e.RowHandle, e.Column).ToString();

                if (maSoMB != "")
                {
                    DataRow[] r = tblMB.Select("MaSoMB = '" + maSoMB + "'");
                    int MauNen = r.Length > 0 ? (int)r[0]["MauNen"] : -1;
                    e.Appearance.BackColor = Color.FromArgb(MauNen);
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(-1);
                }
            }
            catch { }
        }        

        private void grvMatBang_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 | e.CellValue == null) return;
            var wait = Library.DialogBox.WaitingForm();
            string id = e.CellValue.ToString();            

            try
            {
                var objMB = db.mbMatBangs.Where(p => p.MaSoMB == id).FirstOrDefault();
                IDMatBang = objMB.MaMB;
                MaTT = objMB.MaTT;

                LoadDetail();
            }
            catch
            {
                IDMatBang = null;
                MaTT = null;
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }
        
        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (itemToaNha.EditValue == null) return;

            var INTMaTN = Convert.ToInt32(itemToaNha.EditValue);
            itemKhoiNha.EditValue = null;
            lookKhoiNha.DataSource = db.mbKhoiNhas.Where(p => p.MaTN == INTMaTN);
            cmbKhoiNha.DataSource = db.mbKhoiNhas.Where(p => p.MaTN == INTMaTN);
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DichVu.MatBang.frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0 || IDMatBang == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            using (var frm = new DichVu.MatBang.frmEdit())
            {
                frm.MaMB = IDMatBang;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (grvMatBang.FocusedRowHandle < 0 || IDMatBang == null)
                {
                    DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                    return;
                }

                mbMatBang objMB = db.mbMatBangs.Single(p => p.MaMB == IDMatBang);
                db.mbMatBangs.DeleteOnSubmit(objMB);
                db.SubmitChanges();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
                return;
            }
        }

        private void btnChuyenQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvMatBang.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            frmChuyenKhachHang frm = new frmChuyenKhachHang();
            frm.objnhanvien = objnhanvien;
            var objmb = db.mbMatBangs.Single(p => p.MaMB == IDMatBang);
            frm.objmb = objmb;
            if (IDMatBang != null)
            {
                frm.objkhachHangSource = db.tnKhachHangs.Single(p => p.MaKH == objmb.MaKH);
            }
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                LoadData();
                grvMatBang_RowCellClick(null, null);
            }
        }

        private void tcdetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }
    }
}