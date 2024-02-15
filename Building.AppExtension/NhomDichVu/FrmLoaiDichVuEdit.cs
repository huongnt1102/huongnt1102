using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Building.AppExtension.NhomDichVu
{
    public partial class FrmLoaiDichVuEdit : DevExpress.XtraEditors.XtraForm
    {
        #region Param
        public byte? MaTN { get; set; }
        public Guid? Id { get; set; }
        public Guid? IdParent { get; set; }
        public class app_loaidichvu_edit_get
        {
            public Guid Id { get; set; }

            public Guid? IdNhom { get; set; }

            public int? NguoiNgungSuDung { get; set; }

            public int? NguoiTao { get; set; }

            public System.DateTime? NgayNgungSuDung { get; set; }

            public bool? IsNgungSuDung { get; set; }

            public string Ten { get; set; }

        }
        #endregion

        public FrmLoaiDichVuEdit()
        {
            InitializeComponent();
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (Id != null)
                {
                    var model = new { id = Id };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model);
                    var result = Library.Class.Connect.QueryConnect.Query<app_loaidichvu_edit_get>("app_loaidichvu_edit_get", param);
                    if (result.Count() > 0)
                    {
                        var item = result.First();
                        txtTenNhom.Text = item.Ten;
                        chkIsNgungSuDung.EditValue = item.IsNgungSuDung;
                    }
                    else Id = null;
                }
            }
            catch(System.Exception ex) { }

        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //string id = "";
                //if (Id != null) id = Id.ToString();
                var model = new { matn = MaTN, id = Id, ten = txtTenNhom.Text, isngung = chkIsNgungSuDung.Checked, user = Library.Common.User.MaNV, idnhom = IdParent };
                var param = new Dapper.DynamicParameters();
                param.AddDynamicParams(model);
                var result = Library.Class.Connect.QueryConnect.Query<string>("app_loaidichvu_edit_save", param);
                if(result.Count() > 0)
                {
                    var item = result.First();
                    if (item == "1")
                    {
                        DialogBox.Success();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        DialogBox.Error("Lưu không thành công");
                    }
                }
                else
                {
                    DialogBox.Error("Lưu không thành công" );
                }
                
            }
            catch(System.Exception ex)
            {
                DialogBox.Error("Lưu không thành công, Lỗi: "+ex.Message);
            }
            
        }
    }
}