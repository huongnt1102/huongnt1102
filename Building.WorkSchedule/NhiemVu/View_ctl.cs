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
using Library;

namespace Building.WorkSchedule.NhiemVu
{
    public partial class View_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public tnNhanVien objNV;
        bool KT = false, KT1 = false;
        bool IsAdd = false, IsEdit = false, IsDelete = false;
        int MaNVu = 0;
        DateTime dateStart;
        bool IsZoom = true;
        DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
        DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
        public View_ctl()
        {
            InitializeComponent();
        }

        int ThangDauCuaQuy(int Thang)
        {
            if (Thang <= 3)
                return 1;
            else if (Thang <= 6)
                return 4;
            else if (Thang <= 9)
                return 7;
            else
                return 10;
        }

        void SetToDate()
        {
            KT = false;
            KT1 = false;
            dateDenNgay.Enabled = false;
            dateTuNgay.Enabled = false;
            DateTime dateHachToan = DateTime.Now.Date;
            switch (cmbKyBC.SelectedIndex)
            {
                case 0: //Ngay nay
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
                case 1: //Tuan nay
                    dateDenNgay.DateTime = dateHachToan.AddDays(7 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 2: //Dau tuan den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan.AddDays(1 - (int)dateHachToan.DayOfWeek);

                    break;
                case 3: //Thang nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 4: //Dau thang den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1);

                    break;
                case 5: //Quy nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month) + 2, 1).AddMonths(1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 6: //Dau quy den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1);

                    break;
                case 7: //Nam nay
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 8: //Dau nam den hien tai
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 9: //Thang 1
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 10: //Thang 2
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 2, 1);

                    break;
                case 11: //Thang 3
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 3, 1);

                    break;
                case 12: //Thang 4
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 13: //Thang 5
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 5, 1);

                    break;
                case 14: //Thang 6
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 6, 1);

                    break;
                case 15: //Thang 7
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 16: //Thang 8
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 8, 1);

                    break;
                case 17: //Thang 9
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 9, 1);

                    break;
                case 18: //Thang 10
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 19: //Thang 11
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 11, 1);

                    break;
                case 20: //Thang 12
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 12, 1);

                    break;
                case 21: //Quy I
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 1, 1);

                    break;
                case 22: //Quy II
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);

                    break;
                case 23: //Quy III
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);

                    break;
                case 24: //Quy IV
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);

                    break;
                case 25: //Tuan truoc
                    dateDenNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(-(int)dateHachToan.DayOfWeek - 6);

                    break;
                case 26: //Thang truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(-1);

                    break;
                case 27: //Quy truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, ThangDauCuaQuy(dateHachToan.Month), 1).AddMonths(-3);

                    break;
                case 28: //Nam truoc
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year - 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year - 1, 1, 1);

                    break;
                case 29: //Tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(14 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 30: //Bon tuan sau
                    dateDenNgay.DateTime = dateHachToan.AddDays(35 - (int)dateHachToan.DayOfWeek);
                    dateTuNgay.DateTime = dateHachToan.AddDays(8 - (int)dateHachToan.DayOfWeek);

                    break;
                case 31: //Thang sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(2).AddDays(-1);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year, dateHachToan.Month, 1).AddMonths(1);

                    break;
                case 32: //Quy sau
                    switch (ThangDauCuaQuy(dateHachToan.Month))
                    {
                        case 10:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 4, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);
                            break;

                        case 1:
                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 4, 1);
                            break;
                        case 4:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1).AddDays(-1);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 7, 1);
                            break;
                        case 7:

                            dateDenNgay.DateTime = new DateTime(dateHachToan.Year, 12, 31);
                            dateTuNgay.DateTime = new DateTime(dateHachToan.Year, 10, 1);
                            break;
                    }
                    break;

                case 33: //Nam sau
                    dateDenNgay.DateTime = new DateTime(dateHachToan.Year + 1, 12, 31);
                    dateTuNgay.DateTime = new DateTime(dateHachToan.Year + 1, 1, 1);

                    break;
                case 34: //Tu chon
                    dateDenNgay.Enabled = true;
                    dateTuNgay.Enabled = true;
                    KT = true;
                    KT1 = true;
                    dateDenNgay.DateTime = dateHachToan;
                    dateTuNgay.DateTime = dateHachToan;

                    break;
            }
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
            o.NhanVien.MaNV = objNV.MaNV;
            //switch (Library.Commoncls.GetAccessData(Common.PerID, 66))
            //{
            //    case 1://Tat ca
            o.NgayBD = dateTuNgay.DateTime;
            o.NgayHH = dateDenNgay.DateTime;
            switch (itemCategory.EditValue.ToString())
            {
                case "Nhiệm vụ của tôi":
                    gridControl1.DataSource = o.SelectByStaff();
                    break;
                case "Nhiệm vụ được giao":
                    gridControl1.DataSource = o.SelectDuocGiao();
                    break;
                default:
                    gridControl1.DataSource = o.SelectAll();
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
            //gridView1.FocusedRowHandle = 1;
            //gridView1.FocusedRowHandle = 0;
            //try
            //{
            //    wait.Close();
            //}
            //catch { }
            //finally
            //{
            //    o = null;
            //    System.GC.Collect();
            //}
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhiemVu.AddNew_frm frm = new NhiemVu.AddNew_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void NhiemVu_ctl_Load(object sender, EventArgs e)
        {
            IsAdd = true;
            IsEdit = true;
            IsDelete = true;
            LoadDictionary();
            cmbKyBC.SelectedIndex = 3;
            LoadData();
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
                if (MessageBox.Show("Bạn có chắc chắn xóa nhiệm vụ này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Library.NhiemVuCls nv = new Library.NhiemVuCls();
                        nv.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                        nv.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        MessageBox.Show("Xóa không thành công vì: nhiệm vụ này đã được sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhiệm vụ cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                NhiemVu.AddNew_frm frm = new NhiemVu.AddNew_frm();
                frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhiệm vụ cần sửa.Xin cảm ơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            
        }

        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            //if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            //{
            //    NhiemVu.AddNew_frm frm = new NhiemVu.AddNew_frm();
            //    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
            //    frm.ShowDialog();
            //    if (frm.IsUpdate)
            //        LoadData();
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn nhiệm vụ cần sửa.Xin cảm ơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToDate();
        }

        private void btnFinish_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colTenTT).ToString() == "Hoàn thành")
                    DialogBox.Alert("<Nhiệm vụ> này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xác nhận hoàn thành nhiệm vụ này không?") == DialogResult.Yes)
                    {
                        Library.NhiemVuCls o = new Library.NhiemVuCls();
                        o.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                        o.UpdateFinish();

                        LoadData();
                    }
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn nhiệm vụ, xin cảm ơn.");
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

        private void btnGiaoViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaNVu) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colTenTT).ToString() == "Hoàn thành")
                    DialogBox.Alert("<Nhiệm vụ> này đã hoàn thành. Vui lòng kiểm tra lại, xin cảm ơn.");
                else
                {
                    SelectObject_frm frm = new SelectObject_frm();
                    frm.MaNVu = int.Parse(gridView1.GetFocusedRowCellValue(colMaNVu).ToString());
                    frm.ShowDialog();
                }
            }
            else
                DialogBox.Alert("Vui lòng chọn nhiềm vụ muốn giao việc, xin cảm ơn.");
        }

        private void itemZoom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CRM_frm frm = (CRM_frm)this.ParentForm;
            //frm.Zooms();
            //if (IsZoom)
            //{
            //    itemZoom.ImageIndex = 7;
            //    IsZoom = false;
            //    toolTipItem2.LeftIndent = 6;
            //    toolTipItem2.Text = "Thu nhỏ nhiệm vụ";
            //    superToolTip2.Items.Add(toolTipItem2);
            //}
            //else
            //{
            //    itemZoom.ImageIndex = 8;
            //    IsZoom = true;
            //    toolTipItem2.LeftIndent = 6;
            //    toolTipItem2.Text = "Phóng to nhiệm vụ";
            //    superToolTip2.Items.Add(toolTipItem2);
            //}
            //this.itemZoom.SuperTip = superToolTip2;
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
    }
}
