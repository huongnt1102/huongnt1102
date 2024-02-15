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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace LandSoftBuilding.Receivables.HopDongThue
{
    public partial class frmManagerHDT : DevExpress.XtraEditors.XtraForm
    {
        //Liquidate_Config objConfig;
        MasterDataContext db;

        public frmManagerHDT()
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

        //void LoadData()
        //{
        //    gcHoaDon.DataSource = null;
        //    gcHoaDon.DataSource = linqInstantFeedbackSource1;
        //}

        void RefreshData()
        {
            //linqInstantFeedbackSource1.Refresh();
            LoadData();
        }

        void AddRecord()
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ tự động", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmAddAuto())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void AddNew()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmEditHDT())
            {
                var db = new MasterDataContext();
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void AddMulti()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhiều hóa đơn dịch vụ", "Thêm nhiều", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));

            using (var frm = new frmAddMulti())
            {
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        void Edit()
        {
            if (gvHoaDon.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn bản ghi, xin cảm ơn.");
                return;
            }

            if ((bool)gvHoaDon.GetFocusedRowCellValue("IsDuyet") == true)
            {
                DialogBox.Error("Hóa đơn đã duyệt, không thể sửa");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Sửa hóa đơn dịch vụ", "Sửa", "Dịch vụ: " + gvHoaDon.GetFocusedRowCellValue("TenLDV").ToString() + " của khách hàng:" + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            var f = new frmEditHDT();
            f.MaTN = (byte)itemToaNha.EditValue;
            f.ID = (long)gvHoaDon.GetFocusedRowCellValue("ID");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        void DeleteRecord()
        {
            var indexs = gvHoaDon.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            using (var frm = new frmLyDoXoa())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var i in indexs)
                    {
                        var hd = db.dvHoaDons.FirstOrDefault(p => p.ID == (long)gvHoaDon.GetRowCellValue(i, "ID") & p.IsDuyet.GetValueOrDefault() == false);
                        if (hd != null)
                        {
                            #region Luu lai LS xoa hoa don Phu Luc XuanMai
                            var HDDX = new dvHoaDonDaXoa();
                            HDDX.ConNo = hd.ConNo;
                            HDDX.DaThu = hd.DaThu;
                            HDDX.DenNgay = hd.DenNgay;
                            HDDX.DienGiai = hd.DienGiai;
                            HDDX.IsDuyet = hd.IsDuyet;
                            HDDX.KyTT = hd.KyTT;
                            HDDX.LinkID = hd.LinkID;
                            HDDX.MaDVTG = hd.MaDVTG;
                            HDDX.MaKH = hd.MaKH;
                            HDDX.MaMB = hd.MaMB;
                            HDDX.MaLDV = hd.MaLDV;
                            HDDX.MaNVN = Common.User.MaNV;
                            HDDX.NgayNhap = DateTime.Now;
                            HDDX.NgayTT = hd.NgayTT;
                            HDDX.MaTN = hd.MaTN;
                            HDDX.PhaiThu = hd.PhaiThu;
                            HDDX.TableName = hd.TableName;
                            HDDX.PhiDV = hd.PhiDV;
                            HDDX.TienCK = hd.TienCK;
                            HDDX.TienTT = hd.TienTT;
                            HDDX.TuNgay = hd.TuNgay;
                            HDDX.TyLeCK = hd.TyLeCK;
                            HDDX.LyDoXoa = frm.LyDo;
                            HDDX.IsHDThue = true;
                            db.dvHoaDonDaXoas.InsertOnSubmit(HDDX);
                            #endregion
                            db.dvHoaDons.DeleteOnSubmit(hd);
                            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xóa hóa đơn dịch vụ", "Xóa", "Dịch vụ:" + gvHoaDon.GetRowCellValue(i, "TenLDV").ToString() + " của khách hàng:" + gvHoaDon.GetRowCellValue(i, "TenKH").ToString() + " - Dự án: " + lkToaNha.DisplayMember);
                        }
                    }
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

        void Car(int LinkID, DateTime date)
        {
            try
            {
                //MasterDataContext dbo = new MasterDataContext();
                //var Tungay = (DateTime)dbo.GetSystemDate();
                ///var Xe = dbo.CauHinhs.FirstOrDefault(p => p.MaTN == (byte)itemToaNha.EditValue);
                //var NgayHetHan = dbo.dvgxTheXes.SingleOrDefault(p => p.ID == LinkID);
                //var Ngay = Tungay;
                //var NgayCuoiThang = DateTime.DaysInMonth(Ngay.Year, 12);
                //var ThangHetHan = new DateTime(Ngay.Year, 12, NgayCuoiThang);

                //var tam = Convert.ToDateTime(string.Format("{0}/{1}/{2} {3}", date.Year, date.Month, date.Day, "11:59:35 PM"));

                //NgayHetHan.NgayHH = tam;
                //dbo.SubmitChanges();

                //if (Xe != null & NgayHetHan.MaCapThe != null)
                //{
                //    try
                //    {
                //        string sql =
                //        string.Format(
                //            "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                //            Xe.Server, Xe.Database, Xe.UserID, Xe.Password);
                //        SqlConnection connection = new SqlConnection(sql);

                //        String query2 =
                //            "update tblRegisterMonthlyTicket set DateStop=@DateStop where TicketID=@ID";

                //        connection = new SqlConnection(sql);
                //        using (SqlCommand command = new SqlCommand(query2, connection))
                //        {
                //            command.Parameters.AddWithValue("@ID", NgayHetHan.MaCapThe);
                //            command.Parameters.AddWithValue("@DateStop", tam);

                //            connection.Open();
                //            int result = command.ExecuteNonQuery();

                //            // Check Error
                //            if (result < 0)
                //                Console.WriteLine("Error inserting data into Database!");
                //        }
                //        if (Xe != null)
                //        {
                //            string sql3 =
                //                string.Format(
                //                    "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                //                    Xe.Server, Xe.Database, Xe.UserID, Xe.Password);

                //            string query3 = "update tblCountVehicleDaily set LastTimeUpdateTicket = GETDATE()";
                //            SqlConnection connection3 = new SqlConnection(sql3);
                //            connection3.Open();
                //            SqlCommand command3 = new SqlCommand(query3, connection3);

                //            int result3 = command3.ExecuteNonQuery();
                //            string query4 = "update tblCountVehicleDaily set LastTimeUpdateMonthlyTicket = GetDate()";
                //            SqlConnection connection4 = new SqlConnection(sql3);
                //            connection4.Open();
                //            SqlCommand command4 = new SqlCommand(query4, connection4);

                //            int result4 = command4.ExecuteNonQuery();
                //        }
                //    }
                //    catch
                //    {

                //        DialogBox.Alert(NgayHetHan.MaCapThe.ToString());
                //    }

                //}
            }
            catch (Exception ec)
            {
                DialogBox.Error(ec.Message);
            }
        }
        void Payment()
        {
            var indexs = gvHoaDon.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thu tiền khách hàng", "Thu tiền", "Khách hàng:" + gvHoaDon.GetFocusedRowCellValue("TenKH").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));

            using (var frm = new frmPaymentHDT())
            {
                var db = new MasterDataContext();
                frm.MaKH = (int?)gvHoaDon.GetFocusedRowCellValue("MaKH");
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.RefreshData();
                }
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import hóa đơn dịch vụ", "Import", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        void Duyet(bool isDuyet)
        {
            var rows = gvHoaDon.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn [Hóa đơn]. Xin cám ơn!");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var i in rows)
                {
                    if ((bool?)gvHoaDon.GetRowCellValue(i, "IsDuyet") == isDuyet) continue;

                    var objHD = db.dvHoaDons.Single(p => p.ID == (long)gvHoaDon.GetRowCellValue(i, "ID"));
                    objHD.IsDuyet = isDuyet;
                    if (isDuyet)
                    {
                        DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Duyệt hóa đơn dịch vụ", "Duyệt", "Dịch vụ:" + gvHoaDon.GetRowCellValue(i, "TenLDV").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                    }
                    else
                    {
                        DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Không duyệt hóa đơn dịch vụ", "Không duyệt", "Dịch vụ:" + gvHoaDon.GetRowCellValue(i, "TenLDV").ToString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                    }
                }

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

        void DuyetAll(bool isDuyet)
        {
            if (DialogBox.Question("Bạn có chắc chắn thực hiện thao tác này không?") == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var _MaTN = (byte)itemToaNha.EditValue;

                var ltHoaDon = from hd in db.dvHoaDons
                               where hd.MaTN == _MaTN & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                               & hd.DaThu.GetValueOrDefault() == 0 & hd.IsDuyet.GetValueOrDefault() != isDuyet
                               select hd;

                foreach (var hd in ltHoaDon)
                    hd.IsDuyet = isDuyet;

                db.SubmitChanges();
                if (isDuyet)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Duyệt tất cả hóa đơn dịch vụ", "Duyệt", "Từ ngày: " + _TuNgay.ToShortDateString() + " đến ngày" + _DenNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                }
                else
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bỏ duyệt tất cả hóa đơn dịch vụ", "Bỏ Duyệt", "Từ ngày: " + _TuNgay.ToShortDateString() + " đến ngày" + _DenNgay.ToShortDateString() + "- Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                }
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

        void Load_PhieuThu()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }
                gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                                        join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                        where ct.TableName == "dvHoaDon" & ct.LinkID == id
                                        select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList();
                ;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            //TranslateLanguage.TranslateControl(this, barManager1);
            gvHoaDon.KeyDown += gvHoaDon_KeyDown;
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvHoaDon.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }

            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
            db = new MasterDataContext();
          
        }

        void gvHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                e.Handled = true;
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        void Load_PhieuKhauTru()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                gcKhauTru.DataSource = (from ct in db.ktttChiTiets
                                        join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                        where ct.LinkID == id
                                        select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();
                ;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        void Load_PhieuKhauTruTheBoi()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                //gcKTTB.DataSource = (from ct in db.ktttChiTietTheBois
                //                     join pt in db.ktttKhauTruThuTruocTheBois on ct.MaCT equals pt.ID
                //                     where ct.LinkID == id
                //                     select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();
                //;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        void Load_PhieuKhauTruGYM()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                //gcKTTG.DataSource = (from ct in db.ktttChiTietGYMs
                //                     join pt in db.ktttKhauTruThuTruocGYMs on ct.MaCT equals pt.ID
                //                     where ct.LinkID == id
                //                     select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();
                //;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        void Load_PhieuKhauTruBBQ()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                //gcKTBBQ.DataSource = (from ct in db.ktttChiTietBBQs
                //                      join pt in db.ktttKhauTruThuTruocBBQs on ct.MaCT equals pt.ID
                //                      where ct.LinkID == id
                //                      select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();
                //;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        void Load_PhieuKhauTruThiCong()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                //gcKTTC.DataSource = (from ct in db.ktttChiTietThiCongs
                //                     join pt in db.ktttKhauTruThuTruocThiCongs on ct.MaCT equals pt.ID
                //                     where ct.LinkID == id
                //                     select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();
                //;
                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void Load_PhieuKhauTruHDT()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (long?)gvHoaDon.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcKhauTru.DataSource = null;
                    return;
                }
                gcKTHDT.DataSource = (from ct in db.ktttChiTietHDTs
                                      join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                      where ct.LinkID == id
                                      select new { pt.NgayCT, pt.SoCT, ct.DienGiai, ct.SoTien }).ToList();

                //if ((int)itemToaNha.EditValue == 36)
                //{
                //    gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                //                            join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                            where ct.TableName == "dvHoaDon" & ct.LinkID == id
                //                            select new { pt.NgayThu, pt.SoPT, ct.DienGiai, ct.SoTien }).ToList().Take(1);
                //    ;
                //}

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        void LoadData()
        {
            using (var _db = new MasterDataContext())
            {
                _db.Dispose();
            }

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            gcHoaDon.DataSource = (from hd in db.dvHoaDons

                                   join ddh in db.dvDienDieuHoas on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDienDieuHoa", LinkID = (int?)ddh.ID } into dieuhoa
                                   from ddh in dieuhoa.DefaultIfEmpty()

                                   join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH into khach
                                   from kh in khach.DefaultIfEmpty()

                                   join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID

                                   join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                   from mb in tblMatBang.DefaultIfEmpty()

                                   join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                   from tl in tblTangLau.DefaultIfEmpty()

                                   join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                   from kn in tblKhoiNha.DefaultIfEmpty()

                                   join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                   from lmb in tblLoaiMatBang.DefaultIfEmpty()

                                   join nvn in db.tnNhanViens on hd.MaNVN equals nvn.MaNV into nvien
                                   from nvn in nvien.DefaultIfEmpty()

                                   join nvs in db.tnNhanViens on hd.MaNVS equals nvs.MaNV into nvsua
                                   from nvs in nvsua.DefaultIfEmpty()

                                   join tx in db.dvgxTheXes on new { hd.LinkID, hd.MaLDV } equals new { LinkID = (int?)tx.ID, MaLDV = (int?)6 } into xe
                                   from tx in xe.DefaultIfEmpty()

                                   //join txgym in db.dvTheGyms on new { hd.LinkID, hd.MaLDV } equals new { LinkID = (int?)txgym.ID, MaLDV = (int?)50 } into gym//16,52
                                   //from txgym in gym.DefaultIfEmpty()

                                   //join txboi in db.dvTheBois on new { hd.LinkID, hd.MaLDV } equals new { LinkID = (int?)txboi.ID, MaLDV = (int?)16 } into boi
                                   //from txboi in boi.DefaultIfEmpty()

                                   //join txboiNgay in db.dvTheBois on new { hd.LinkID, hd.MaLDV } equals new { LinkID = (int?)txboiNgay.ID, MaLDV = (int?)52 } into boingay
                                   //from txboiNgay in boingay.DefaultIfEmpty()

                                   join ltt in
                                       (from ct in db.ctLichThanhToans
                                        select new { ct.ID, ct.ctHopDong.SoHDCT, IsThanhLy = ct.ctHopDong.ctThanhLies.Any(), ct.NgayHHTT}) on hd.LinkID equals ltt.ID into lichthanhtoan
                                   from ltt in lichthanhtoan.DefaultIfEmpty()

                                   join nvthu in db.tnNhanViens on hd.MaNVN equals nvthu.MaNV into thu
                                   from nvthu in thu.DefaultIfEmpty()
                                   where hd.MaTN == matn & SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 &
                                   SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                                   & hd.IsHDThue.GetValueOrDefault()
                                   orderby mb.MaMB ascending
                                   select new
                                   {
                                       hd.ID,
                                       IsThanhLy = ltt.IsThanhLy,
                                       hd.NgayTT,
                                       hd.MaKH,
                                       SoHD = ltt.SoHDCT,
                                       //SoTheGym = txgym.SoThe,
                                       //SoTheBoi = txboi.SoThe,
                                       //SoTheBoiNgay = txboiNgay.SoThe,
                                       kh.KyHieu,
                                       kh.MaPhu,
                                       TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                       TenLDV = l.TenHienThi,
                                       hd.DienGiai,
                                       hd.PhiDV,
                                       hd.KyTT,
                                       hd.ThueGTGT,
                                       hd.TienThueGTGT,
                                       hd.TienTT,
                                       hd.TyLeCK,
                                       hd.TuNgay,
                                       hd.DenNgay,
                                       hd.TienCK,
                                       hd.PhaiThu,
                                       hd.NgayNhap,
                                       //hd.IsDaXuatHoaDon,
                                       NhanVienNhap = nvn.HoTenNV,
                                       hd.NgaySua,
                                       hd.LinkID,
                                       //hd.SoHoaDonDienTu,
                                       NhanVienSua = nvs.HoTenNV,
                                       DaThu = (from ct in db.ptChiTietPhieuThus
                                                join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                join hdct in db.dvHoaDons on ct.LinkID equals hdct.ID into tam
                                                from hdct in tam.DefaultIfEmpty()
                                                where ct.LinkID == hd.ID & ct.TableName == "dvHoaDon"
                                                select ct.SoTien).Sum().GetValueOrDefault()
                                               +
                                               (from ct in db.ktttChiTiets
                                                join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                                select ct.SoTien).Sum().GetValueOrDefault()
                                               +
                                               //(from ct in db.ktttChiTietTheBois
                                               // join pt in db.ktttKhauTruThuTruocTheBois on ct.MaCT equals pt.ID
                                               // where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                               // select ct.SoTien).Sum().GetValueOrDefault()
                                               //+
                                               //(from ct in db.ktttChiTietGYMs
                                               // join pt in db.ktttKhauTruThuTruocGYMs on ct.MaCT equals pt.ID
                                               // where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                               // select ct.SoTien).Sum().GetValueOrDefault()
                                               //+
                                               //(from ct in db.ktttChiTietBBQs
                                               // join pt in db.ktttKhauTruThuTruocBBQs on ct.MaCT equals pt.ID
                                               // where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                               // select ct.SoTien).Sum().GetValueOrDefault()

                                               //+
                                               (from ct in db.ktttChiTietHDTs
                                                join pt in db.ktttKhauTruThuTruocHDTs on ct.MaCT equals pt.ID
                                                where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                                select ct.SoTien).Sum().GetValueOrDefault(),

                                               //+
                                               //(from ct in db.ktttChiTietThiCongs
                                               // join pt in db.ktttKhauTruThuTruocThiCongs on ct.MaCT equals pt.ID
                                               // where ct.TableName == "dvHoaDon" & ct.LinkID == hd.ID
                                               // select ct.SoTien).Sum().GetValueOrDefault(),
                                       hd.IsDuyet,
                                       mb.MaSoMB,
                                       tl.TenTL,
                                       kn.TenKN,
                                       lmb.TenLMB,

                                       NguoiThu = nvthu.HoTenNV,
                                       //hd.NgayThu,
                                       //hd.LoaiThu,
                                       //CSH = (from khc in db.tnKhachHangs
                                       //       where khc.MaKH == hd.MaKH_CSH
                                       //       select khc.IsCaNhan == true ? (khc.HoKH + " " + khc.TenKH) : khc.CtyTen).FirstOrDefault(),
                                       HanTT = ltt.NgayHHTT,
                                       SoNgay = SqlMethods.DateDiffDay(DateTime.Now, ltt.NgayHHTT),
                                   }).Select(p => new
                                   {
                                       p.SoHD,
                                       //p.SoTheGym,
                                       //p.SoTheBoi,
                                       //p.SoTheBoiNgay,
                                       p.ID,
                                       p.NgayTT,
                                       p.MaKH,
                                       p.KyHieu,
                                       p.MaPhu,
                                       //p.NgayTinhPhi,
                                       p.TenKH,
                                       p.TenLDV,
                                       p.DienGiai,
                                       p.PhiDV,
                                       p.KyTT,
                                       //p.IsDaXuatHoaDon,
                                       p.ThueGTGT,
                                       p.TienThueGTGT,
                                       p.TienTT,
                                       p.TyLeCK,
                                       p.TuNgay,
                                       p.DenNgay,
                                       p.TienCK,
                                       p.PhaiThu,
                                       p.NgayNhap,
                                       p.NhanVienNhap,
                                       p.NgaySua,
                                       p.NhanVienSua,
                                       DaThu = p.DaThu,
                                       p.LinkID,
                                       //p.SoHoaDonDienTu,
                                       ConNo = p.PhaiThu - p.DaThu,
                                       p.IsDuyet,
                                       p.MaSoMB,
                                       p.TenTL,
                                       p.TenKN,
                                       p.TenLMB,
                                       //p.NgayThu,
                                       //p.LoaiThu,
                                       p.NguoiThu,
                                       //p.CSH,
                                       p.HanTT,
                                       p.SoNgay,
                                   }).ToList();

        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var matn = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from hd in db.dvHoaDons
                                join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID
                                join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                from mb in tblMatBang.DefaultIfEmpty()
                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                from tl in tblTangLau.DefaultIfEmpty()
                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                from kn in tblKhoiNha.DefaultIfEmpty()
                                join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                from lmb in tblLoaiMatBang.DefaultIfEmpty()
                                where hd.MaTN == matn & SqlMethods.DateDiffDay(tuNgay, hd.NgayTT) >= 0 & SqlMethods.DateDiffDay(hd.NgayTT, denNgay) >= 0
                                orderby kn.TenKN, tl.TenTL, mb.MaMB ascending //, hd.NgayTT descending
                                select new
                                {
                                    hd.ID,
                                    hd.NgayTT,
                                    hd.MaKH,
                                    kh.KyHieu,
                                    kh.MaPhu,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    TenLDV = l.TenHienThi,
                                    hd.DienGiai,
                                    hd.PhiDV,
                                    hd.KyTT,
                                    hd.TienTT,
                                    hd.TyLeCK,
                                    hd.TienCK,
                                    hd.PhaiThu,
                                    hd.DaThu,
                                    hd.ConNo,
                                    hd.IsDuyet,
                                    mb.MaSoMB,
                                    tl.TenTL,
                                    kn.TenKN,
                                    lmb.TenLMB
                                };
            e.Tag = db;
        }

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Payment();
            }
            catch { }
            
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.ImportRecord();
            }
            catch { }
            
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Duyet(true);
            }
            catch { }
            
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Duyet(false);
            }
            catch { }
            
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Edit();
            }
            catch { }
            
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.AddNew();
            }
            catch { }
            
        }

        private void itemDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();
                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Danh sách không có hóa đơn nào cả");
                    return;
                }

                this.DuyetAll(true);
            }
            catch { }
            
        }

        private void itemKhongDuyetAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();
                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Danh sách không có hóa đơn nào cả");
                    return;
                }

                this.DuyetAll(false);
            }
            catch { }
            
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Export excel hóa đơn dịch vụ", "Export", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
                Commoncls.ExportExcel(gcHoaDon);
            }
            catch { }
            
        }

        private void itemAddMulti_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.AddMulti();
            }
            catch { }
            
        }

        private void gvHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Load_PhieuThu();
            this.Load_PhieuKhauTru();
            Load_PhieuKhauTruTheBoi();
            this.Load_PhieuKhauTruGYM();
            this.Load_PhieuKhauTruBBQ();
            this.Load_PhieuKhauTruThiCong();
            this.Load_PhieuKhauTruHDT();
        }

        private void gvHoaDon_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Load_PhieuThu();
            this.Load_PhieuKhauTru();
            Load_PhieuKhauTruTheBoi();
            this.Load_PhieuKhauTruGYM();
            this.Load_PhieuKhauTruBBQ();
            this.Load_PhieuKhauTruThiCong();
            this.Load_PhieuKhauTruHDT();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var _MaTN = (byte?)itemToaNha.EditValue;
            //if (_MaTN == null)
            //{
            //    DialogBox.Alert("Vui lòng chọn Dự án");
            //    return;
            //}
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhiều hóa đơn dịch vụ nhiều dòng", "Thêm nhiều", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            //using (var frm = new frmAddMultiCheck())
            //{
            //    frm.MaTN = _MaTN;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //        LoadData();
            //}
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = gvHoaDon.GetSelectedRows();

                if (indexs.Length == 0)
                {
                    DialogBox.Alert("Vui lòng chọn mẫu tin ");
                    return;
                }

                //if (DialogBox.QuestionDelete() == DialogResult.No) return;

                var frm = new frmUpdateNgayHH();
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                foreach (var i in indexs)
                {
                    var linkid = (int?)gvHoaDon.GetRowCellValue(i, "LinkID");
                    if (linkid != null)
                    {
                        Car((int)linkid, frm.NgayHH);
                    }

                }
            }
            catch (Exception ex)
            {

                DialogBox.Error(ex.Message);
            }

        }

        private void itemHoaDonNguoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmEditMoi())
            {
                frm.MaTN = _MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnHoaDonXe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ tự động", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmAddAutoXe())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        private void itemPQL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ tự động", "Thêm", "Dự án: " + lkToaNha.GetDisplayText(itemToaNha.EditValue));
            using (var frm = new frmAddAutoPQL())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemDieuChinhHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmDieuChinh();
            f.MaTN = (byte)itemToaNha.EditValue;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void gvHoaDon_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            //try
            //{
            //    GridView view = sender as GridView;

            //    if (e.RowHandle < 0) return;


            //    var _SoNgay = (int)view.GetRowCellValue(e.RowHandle, "SoNgay");
            //    var _ConNo = (decimal) view.GetRowCellValue(e.RowHandle, "ConNo");
            //    if (_SoNgay > 0 && _ConNo <= 0) return;

            //    e.Appearance.BackColor = Color.FromArgb(objConfig.BackColor);
            //    e.Appearance.ForeColor = Color.FromArgb(objConfig.ForeColor);
            //}
            //catch
            //{
            //}
        }
    }
}