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

namespace DIPCRM.PriceAlert
{
    public partial class frmHistory : DevExpress.XtraEditors.XtraForm
    {
        public int MaKH { get; set; }

        public frmHistory()
        {
            InitializeComponent();

            

            this.Load += new EventHandler(frmHistory_Load);
        }

        void frmHistory_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    //gridControl1.DataSource = (from bg in db.BaoGias
                    //                           join ct in db.bgSanPhams on bg.ID equals ct.MaBG
                    //                           join sp in db.SanPhams on ct.MaSP equals sp.ID
                    //                           join dvt in db.DonViTinhs on sp.MaDVT equals dvt.MaDVT into donvi
                    //                           from dvt in donvi.DefaultIfEmpty()
                    //                           join lt in db.LoaiTiens on bg.MaLT equals lt.MaLT
                    //                           where bg.MaKH == this.MaKH
                    //                           select new
                    //                           {
                    //                               bg.NgayBG,
                    //                               bg.SoBG,
                    //                               sp.MaSP,
                    //                               sp.TenSP,
                    //                               dvt.TenDVT,
                    //                               ct.SoLuong,
                    //                               ct.DonGia,
                    //                               ct.ThueGTGT,
                    //                               ct.ThanhTien,
                    //                               TenLT = lt.TenVT
                    //                           }).ToList();
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
    }
}