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
using System.Data.Linq.SqlClient;

namespace DichVu.Dien.DieuHoaNgoaiGio
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
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

        void LoadData()
        {
            gcDieuHoa.DataSource = null;
            gcDieuHoa.DataSource = linqInstantFeedbackSource1;
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void AddRecord()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvDieuHoa.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.ID = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshData();
                    var db = new MasterDataContext();
                    var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == (int)gvDieuHoa.GetFocusedRowCellValue("ID") & p.MaLDV == 17);
                    var Nuoc = db.dvDienDieuHoas.FirstOrDefault(p => p.ID == (int)gvDieuHoa.GetFocusedRowCellValue("ID"));
                    if (KTIDHD != null)
                    {
                        if (DialogBox.Question("Bạn muốn cập nhật hoá đơn điện căn này sau khi được chỉnh sủa không") ==
                            DialogResult.No)
                        {
                            // return;
                        }
                        else
                        {
                            KTIDHD.PhiDV = Nuoc.ThanhTienQD;
                            KTIDHD.TienTT = Nuoc.ThanhTienQD;
                            KTIDHD.PhaiThu = Nuoc.ThanhTienQD;
                            KTIDHD.ConNo = Nuoc.ThanhTienQD - ((from ct in db.ptChiTietPhieuThus
                                                                join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                                where
                                                                    ct.TableName == "dvHoaDon" & ct.LinkID == KTIDHD.ID //&
                                                                //SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0
                                                                select ct.SoTien).Sum().GetValueOrDefault());
                            KTIDHD.MaNVS = Common.User.MaNV;
                            KTIDHD.NgaySua = DateTime.Now;
                            db.SubmitChanges();

                        }

                    }

                }
            }
        }

        void DeleteRecord()
        {
            var indexs = gvDieuHoa.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var dh = db.dvDienDieuHoas.Single(p => p.ID == (int)gvDieuHoa.GetRowCellValue(i, "ID"));
                var KTIDHD = db.dvHoaDons.FirstOrDefault(p => p.LinkID == (int)gvDieuHoa.GetRowCellValue(i, "ID") & p.MaLDV == 17);
                if (KTIDHD == null)
                {
                    db.dvDienDieuHoas.DeleteOnSubmit(dh);
                }
                else
                {
                    DialogBox.Error("Phí điện căn này đã có hoá đơn phát sinh vui lòng kiểm tra lại !");
                    var frm = new LandSoftBuilding.Receivables.frmManagerKiemTra();
                    frm.LinkID = (int)gvDieuHoa.GetRowCellValue(i, "ID");
                    frm.MaKH = (int)KTIDHD.MaKH;
                    frm.MaLDV = 17;
                    // return;
                    frm.ShowDialog();
                }
            }

            try
            {
                db.SubmitChanges();

                this.RefreshData();
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

        void ImportRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        void ExportRecord()
        {
            var db = new MasterDataContext();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime?)itemTuNgay.EditValue;
                var _DenNgay = (DateTime?)itemDenNgay.EditValue;
                var ltData = from n in db.dvDienDieuHoas
                             join b in db.mbMatBangs on n.MaMB equals b.MaMB
                             join t in db.LoaiTiens on n.MaLT equals t.ID
                             where n.MaTN == _MaTN
                             & SqlMethods.DateDiffMonth(_TuNgay, n.NgayCT) >= 0
                             & SqlMethods.DateDiffMonth(n.NgayCT, _DenNgay) >= 0
                             orderby b.MaSoMB
                             select new
                             {
                                 b.MaSoMB,
                                 n.NgayCT,
                                 n.SoFCU,
                                 n.SoGio,
                                 n.DonGia,
                                 n.ThanhTien,
                                 LoaiTien = t.KyHieuLT,
                                 n.DienGiai
                             };

                var tblData = SqlCommon.LINQToDataTable(ltData);
                ExportToExcel.exportDataToExcel(tblData);
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

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from dh in db.dvDienDieuHoas
                                join lt in db.LoaiTiens on dh.MaLT equals lt.ID
                                join kh in db.tnKhachHangs on dh.MaKH equals kh.MaKH
                                join mb in db.mbMatBangs on dh.MaMB equals mb.MaMB
                                join nvn in db.tnNhanViens on dh.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on dh.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                where dh.MaTN == matn & SqlMethods.DateDiffDay(tuNgay, dh.NgayCT) >= 0 & SqlMethods.DateDiffDay(dh.NgayCT, denNgay) >= 0
                                orderby dh.NgayCT descending
                                select new
                                {
                                    dh.ID,
                                    dh.NgayCT,
                                    kh.KyHieu,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    mb.MaSoMB,
                                    dh.SoFCU,
                                    dh.SoGio,
                                    dh.DonGia,
                                    dh.ThanhTien,
                                    lt.KyHieuLT,
                                    dh.ThanhTienQD,
                                    dh.DienGiai,
                                    NguoiNhap = nvn.HoTenNV,
                                    dh.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    dh.NgaySua,
                                    HeSo = dh.HeSo ?? 1
                                };
            e.Tag = db;
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExportRecord();
        }
    }
}