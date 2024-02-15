using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Building.Asset.VanHanh
{
    public partial class frmKeHoachVanhanh_ChiPhi: XtraForm
    {
        public List<int> ListId { get; set; }
        public int LoaiChiPhi { get; set; }

        private MasterDataContext _db;

        public frmKeHoachVanhanh_ChiPhi()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();

            spinChiPhi.Value = 0;
        }

        private void itemHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
 
        private void itemLuu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                foreach (var i in ListId)
                {
                    var o = _db.tbl_KeHoachVanHanhs.FirstOrDefault(_ => _.ID == i);
                    if (o != null)
                    {
                        if (LoaiChiPhi == 0)
                        {
                            o.ChiPhiTheoKh = spinChiPhi.Value;
                        }
                        else
                            o.ChiPhiThucHien = spinChiPhi.Value;
                        o.NgaySua = DateTime.Now;
                        o.NguoiSua = Common.User.MaNV;
                    }
                }

                _db.SubmitChanges();

                DialogResult = DialogResult.OK;
                DialogBox.Alert("Đã lưu thành công");

            }
            catch (Exception)
            {
                DialogResult = DialogResult.Cancel;
                DialogBox.Error("Không lưu được, vui lòng kiểm tra lại");
            }
        }
    }
}