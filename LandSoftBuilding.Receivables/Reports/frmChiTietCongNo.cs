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

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class frmChiTietCongNo : Building.PrintControls.PrintFilterForm
    {
        public frmChiTietCongNo()
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

            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
        }

        private void frmChiTietCongNo_Load(object sender, EventArgs e)
        {
            //Toa nha
            lkToaNha.Properties.DataSource = Common.TowerList;
            lkToaNha.EditValue = Common.User.MaTN;
            //Ky bao cao
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);

            var index = DateTime.Now.Month + 8;
            cmbKyBaoCao.EditValue = objKBC.Source[index];
        }

        private void cmbKyBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetDate(cmbKyBaoCao.SelectedIndex);
        }

        private void lkToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                glkKhachHang.Properties.DataSource = from kh in db.tnKhachHangs
                                                     where kh.MaTN == (byte)lkToaNha.EditValue
                                                     orderby kh.KyHieu
                                                     select new
                                                     {
                                                         kh.MaKH,
                                                         kh.KyHieu,
                                                         TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen
                                                     };
                glkKhachHang.EditValue = null;
            }
            catch { }
            //finally {
            //    db.Dispose();
            //}
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            if (lkToaNha.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn Dự án");
                return;
            }
            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }
            if (dateTuNgay.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Từ ngày]");
                return;
            }
            if (dateDenNgay.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Đến ngày]");
                return;
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                var _MaTN = (byte)lkToaNha.EditValue;
                var _MaKH = (int)glkKhachHang.EditValue;
                var _TuNgay = (DateTime)dateTuNgay.EditValue;
                var _DenNgay = (DateTime)dateDenNgay.EditValue;

                this.PrintControl.Report = new rptChiTietCongNo(_MaTN, _MaKH, _TuNgay, _DenNgay);
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}