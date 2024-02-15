using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace Library
{
    public partial class FrmCapNhatPql : DevExpress.XtraEditors.XtraForm
    {
        public FrmCapNhatPql(List<int> _ids)
        {
            InitializeComponent();

            this.ids = _ids;
        }
        public int? MaLDV { get; set; }
        Thread thread;
        delegate void UpdateControls();
        List<int> ids { get; set; }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            SetCheck();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            var _TuNgay = (DateTime?)dateTuNgay.EditValue;

            var _DenNgay = (DateTime?)dateDenNgay.EditValue;

            if (spPQL.Value == 0)
            {
                DialogBox.Error("Vui lòng nhập đơn giá");
                return;
            }

            if (ckbUpdateHoaDon.Checked)
            {
                if (_TuNgay == null | _DenNgay == null)
                {
                    DialogBox.Error("Vui lòng nhập đầy đủ từ ngày - đến ngày");
                    return;
                }

                if (SqlMethods.DateDiffDay(_TuNgay, _DenNgay) < 0)
                {
                    DialogBox.Error("[Từ ngày] - [Đến ngày] không hợp lệ");
                    return;
                }
                
            }
            if (DialogBox.Question("Bạn có chắc chắn không?") == System.Windows.Forms.DialogResult.No) return;
           
                thread = new Thread(this.process);
                thread.IsBackground = true;
                thread.Start();
        }

        void process()
        {
            var _TuNgay = (DateTime?)dateTuNgay.EditValue;

            var _DenNgay = (DateTime?)dateDenNgay.EditValue;


            lblTongSo.BeginInvoke(new UpdateControls(() => updateTongSo(ids.Count)));
            progressBarControl1.BeginInvoke(new UpdateControls(() => updateStatus(ids.Count)));
            if (MaLDV == 13)
            {
                using (var db = new MasterDataContext())
                {
                    var DichVuKhacs = db.dvDichVuKhacs.Where(o => ids.Contains(o.ID));
                    //foreach (var i in ids)
                    //{
                        //var dvk = db.dvDichVuKhacs.FirstOrDefault(o => o.ID == i);

                        //db.SubmitChanges();
                    foreach (var dvk in DichVuKhacs)
                    {
                        #region Xử lý phần dịch vụ khác
                        dvk.DonGia = spPQL.Value;
                        dvk.ThanhTien = dvk.DonGia * dvk.SoLuong;
                        dvk.TienTT = dvk.ThanhTien * dvk.KyTT;
                        dvk.TienTTQD = dvk.ThanhTien * dvk.KyTT;
                        #endregion

                        #region Xử lý hóa đơn nếu có check
                        if (ckbUpdateHoaDon.Checked)
                        {
                            var HoaDons = db.dvHoaDons.Where(p => p.MaLDV == dvk.MaLDV
                                                                & p.LinkID == dvk.ID
                                                                & ckbUpdateHoaDon.Checked
                                                                & !db.ptChiTietPhieuThus.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                                                & !db.ktttChiTiets.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                                                & SqlMethods.DateDiffDay(_TuNgay, p.NgayTT) >= 0
                                                                & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0);
                            foreach (var hd in HoaDons)
                            {
                                hd.PhiDV = dvk.ThanhTien;
                                hd.KyTT = dvk.KyTT;
                                hd.TienTT = dvk.ThanhTien;
                                hd.PhaiThu = dvk.TienTT;
                                hd.ConNo = hd.PhaiThu - hd.DaThu;
                            }
                        }

                        if (checkDaThu.Checked)
                        {
                            var HoaDons = db.dvHoaDons.Where(p => p.MaLDV == dvk.MaLDV
                                                                & p.LinkID == dvk.ID
                                                                & ckbUpdateHoaDon.Checked
                                //& !db.ptChiTietPhieuThus.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                //& !db.ktttChiTiets.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                                                & SqlMethods.DateDiffDay(_TuNgay, p.NgayTT) >= 0
                                                                & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0);
                            foreach (var hd in HoaDons)
                            {
                                hd.PhiDV = dvk.ThanhTien;
                                hd.KyTT = dvk.KyTT;
                                hd.TienTT = dvk.ThanhTien;
                                hd.PhaiThu = dvk.TienTT;
                                hd.ConNo = hd.PhaiThu - hd.DaThu;
                            }
                        }

                        #endregion
                        progressBarControl1.BeginInvoke(new UpdateControls(updateStatus));
                    }
                    db.SubmitChanges();
                
                }
            }
            if (MaLDV == 6)
            {
                using (var db = new MasterDataContext())
                {
                    lblTongSo.BeginInvoke(new UpdateControls(() => updateTongSo(ids.Count)));
                    progressBarControl1.BeginInvoke(new UpdateControls(() => updateStatus(ids.Count)));

                    var TheXe = db.dvgxTheXes.Where(o => ids.Contains(o.ID));
                    //var tx = db.dvgxTheXes.FirstOrDefault(o => o.ID == i);
                    //db.SubmitChanges();
                    foreach (var tx in TheXe)
                    {
                        #region Xử lý phần thẻ xe
                        tx.GiaThang = spPQL.Value;
                        tx.TienTT = tx.GiaThang;
                        tx.TienTruocThue = tx.GiaThang * tx.KyTT;

                        #endregion

                        #region Xử lý hóa đơn nếu có check
                        if (ckbUpdateHoaDon.Checked)
                        {
                            var HoaDons = db.dvHoaDons.Where(p => p.MaLDV == MaLDV
                                                                & p.LinkID == tx.ID
                                                                & ckbUpdateHoaDon.Checked
                                                                & !db.ptChiTietPhieuThus.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                                                & !db.ktttChiTiets.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                                                & SqlMethods.DateDiffDay(_TuNgay, p.NgayTT) >= 0
                                                                & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0);
                            foreach (var hd in HoaDons)
                            {
                                hd.PhiDV = tx.TienTT;
                                hd.KyTT = tx.KyTT;
                                hd.TienTT = tx.TienTT;
                                hd.PhaiThu = tx.TienTT;
                                hd.ConNo = hd.PhaiThu - hd.DaThu;
                            }
                        }

                        if (checkDaThu.Checked)
                        {
                            var HoaDons = db.dvHoaDons.Where(p => p.MaLDV == MaLDV
                                                               & p.LinkID == tx.ID
                                                               & checkDaThu.Checked
                                //& !db.ptChiTietPhieuThus.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                //& !db.ktttChiTiets.Any(o => o.TableName == "dvHoaDon" & o.LinkID == p.ID)
                                                               & SqlMethods.DateDiffDay(_TuNgay, p.NgayTT) >= 0
                                                               & SqlMethods.DateDiffDay(p.NgayTT, _DenNgay) >= 0);
                            foreach (var hd in HoaDons)
                            {
                                hd.PhiDV = tx.TienTT;
                                hd.KyTT = tx.KyTT;
                                hd.TienTT = tx.TienTT;
                                hd.PhaiThu = tx.TienTT;
                                hd.ConNo = hd.PhaiThu - hd.DaThu;
                            }
                        }

                        #endregion
                        progressBarControl1.BeginInvoke(new UpdateControls(updateStatus));
                    }

                    db.SubmitChanges();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void ckbUpdateHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            checkDaThu.Checked = false;
            SetCheck();
        }

        void SetCheck()
        {
            dateTuNgay.Enabled = ckbUpdateHoaDon.Checked | checkDaThu.Checked;
            dateDenNgay.Enabled = ckbUpdateHoaDon.Checked | checkDaThu.Checked;

            if (ckbUpdateHoaDon.Checked  | checkDaThu.Checked)
            {
                dateTuNgay.EditValue = Common.GetFirstDayOfMonth(DateTime.Now);
                dateDenNgay.EditValue = Common.GetLastDayOfMonth(DateTime.Now);
            }
            else
            {
                dateTuNgay.EditValue = null;
                dateDenNgay.EditValue = null;
            }
        }

        void updateTongSo(int count)
        {
            lblTongSo.Text = "Tổng số: " + count;
        }

        void updateStatus(int max)
        {
            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.PercentView = true;
            progressBarControl1.Properties.Maximum = max;
            progressBarControl1.Properties.Minimum = 0;
        }

        void updateStatus()
        {
            progressBarControl1.PerformStep();
        }

        private void checkDaThu_CheckedChanged(object sender, EventArgs e)
        {
            ckbUpdateHoaDon.Checked = false;
            SetCheck();
        }

    }
}