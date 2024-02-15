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
using System.Data.Linq.SqlClient;
//using ReportMisc.DichVu;

namespace DichVu.Quy
{
    public partial class frmDSPhieuThuHuy : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmDSPhieuThuHuy()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        private void frmPhieuThu_Load(object sender, EventArgs e)
        {
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cmbKyBC.Items.Add(str);
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    var tuNgay = (DateTime)itemTuNgay.EditValue;
                    var denNgay = (DateTime)itemDenNgay.EditValue;

                    gcPhieuThu.DataSource = db.PhieuThuHuys
                        .Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayThu.Value) >= 0
                            & SqlMethods.DateDiffDay(p.NgayThu.Value, denNgay) >= 0)
                        .Select(p => new
                        {
                            p.NgayThu,
                            p.MaPhieu,
                            p.DiaChi,
                            p.DichVu,
                            p.MaHopDong,
                            p.DotThanhToan,
                            p.SoTienThanhToan,
                            p.NguoiNop,
                            p.DienGiai,
                            p.tnNhanVien.HoTenNV,
                            p.SoPhieu,
                            p.KeToanDaDuyet,
                            p.mbMatBang.MaSoMB,
                            p.mbMatBang.MaKH,
                            p.mbMatBang.tnKhachHang.KyHieu,
                            p.mbMatBang.tnKhachHang.MaPhu,
                            p.MaMB,
                            // LoaiDichVu = LoaiDichVu(p.DichVu.Value, p.MaHopDong),
                            p.SoThangThuPhiQuanLy,
                            p.SoTienChietKhauPhiQL,
                            p.NgayCN,
                            // HoTenNVCN = p.tnNhanVien1 == null ? "" : p.tnNhanVien1.HoTenNV,
                            p.MaNVCN,
                            ChuyenKhoan = p.ChuyenKhoan.GetValueOrDefault(),
                            IsCancel=p.IsCancel.GetValueOrDefault(),
                            p.LyDoHuy
                        })
                        .OrderByDescending(p => p.NgayThu);
                }
            }
            catch { }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

    }

}