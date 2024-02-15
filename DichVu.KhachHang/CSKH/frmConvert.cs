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

namespace DichVu.KhachHang.CSKH
{
    public partial class frmConvert : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmConvert()
        {
            InitializeComponent();
        }

        private void frmConvert_Load(object sender, EventArgs e)
        {
            glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  orderby kh.KyHieu descending
                                                  where !kh.IsCSKH.GetValueOrDefault()
                                                  & (kh.IsRoot.GetValueOrDefault() | kh.IsChinhThuc.GetValueOrDefault())
                                                  select new
                                                  {
                                                      MaKH = kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                      kh.DiaChi
                                                  }).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            var objKH = db.tnKhachHangs.Single(o => o.MaKH == (int?)glkKhachHang.EditValue);
            objKH.IsCSKH = true;
            db.SubmitChanges();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}