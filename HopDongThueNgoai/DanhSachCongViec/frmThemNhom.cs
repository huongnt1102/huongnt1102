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

namespace HopDongThueNgoai
{
    public partial class frmThemNhom : DevExpress.XtraEditors.XtraForm
    {
        public int? NhomCongViecId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        hdctnNhomCongViec _objNhomCongViec;

        public frmThemNhom()
        {
            InitializeComponent();
        }

        

        private string CreateNo()
        {
            //string buildingNo;
            //if (buildingNo == null) return "";
            var db = new MasterDataContext();

            string temp = "NCV.";
            string stt = "";

            var obj = (from _ in db.hdctnNhomCongViecs
                orderby _.MaNhomCongViec.Substring(_.MaNhomCongViec.IndexOf('.') + 4) descending
                select new
                {
                    Stt = _.MaNhomCongViec.Substring(_.MaNhomCongViec.IndexOf('.') + 4)
                }).FirstOrDefault();
            if (obj == null || (obj != null & obj.Stt == null))
            {
                stt = "0001";
            }
            else stt = (int.Parse(obj.Stt) + 1).ToString().PadLeft(4, '0');

            temp = temp + stt;
            return temp;
        }

        private void LuuMacDinh()
        {
            switch (NhomCongViecId!=null)
            {
                case true:
                    _objNhomCongViec.NgaySua = System.DateTime.UtcNow.AddHours(7);
                    _objNhomCongViec.UserUpdateId = Library.Common.User.MaNV;
                    _objNhomCongViec.UserUpdateName = Library.Common.User.HoTenNV;
                    break;
                case false:
                    _objNhomCongViec.UserCreateId = Library.Common.User.MaNV;
                    _objNhomCongViec.UserCreateName = Library.Common.User.HoTenNV;
                    _objNhomCongViec.MaNhomCongViec = CreateNo();

                    _db.hdctnNhomCongViecs.InsertOnSubmit(_objNhomCongViec);
                    break;
            }
        }

        private bool KiemTra()
        {
            #region Ràng buộc dữ liệu
            if (txtNhomCongViec.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập Nhóm Công Việc");
                return true;
            }
            if (txtNhomCongViec.EditValue.ToString() == "")
            {
                DialogBox.Alert("Vui lòng nhập Nhóm Công Việc");
                return true;
            }
            if (deNgayThem.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập ngày");
                return true;
            }
            #endregion

            return false;
        }

        private void sbThem_Click(object sender, EventArgs e)
        {
            if (KiemTra()) return;

            LuuMacDinh();

            _objNhomCongViec.NgayThem = deNgayThem.DateTime;
            _objNhomCongViec.TenNhomCongViec = txtNhomCongViec.Text;
            if(glkNoiDungNhomCongViec.EditValue!=null) _objNhomCongViec.NhomCongViecContentId = (int?) glkNoiDungNhomCongViec.EditValue;

            _objNhomCongViec.hdctnCongViecs.All(_ =>
            {
                _.MaNhomCongViec = _objNhomCongViec.MaNhomCongViec;
                _.NgayThem = _objNhomCongViec.NgayThem;
                _.NgaySua = _objNhomCongViec.NgaySua;
                _.UserCreateId = _objNhomCongViec.UserCreateId;
                _.UserCreateName = _objNhomCongViec.UserCreateName;
                return true;
            });

            try
            {
                _db.SubmitChanges();
                DialogBox.Success("Đã lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }

            finally
            {
                _db.Dispose();
                this.Close();
            }
        }

        private void frmThemNhom_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            
            _objNhomCongViec = GetNhomCongViec();

            GetDanhMuc();

            deNgayThem.EditValue = NhomCongViecId!=null? _objNhomCongViec.NgayThem: System.DateTime.UtcNow.AddHours(7);
            txtNhomCongViec.EditValue = _objNhomCongViec.TenNhomCongViec;
            
            gc.DataSource = _objNhomCongViec.hdctnCongViecs;
            if(_objNhomCongViec.NhomCongViecContentId!=null) glkNoiDungNhomCongViec.EditValue = _objNhomCongViec.NhomCongViecContentId;
        }

        private Library.hdctnNhomCongViec GetNhomCongViec()
        {
            return NhomCongViecId != null ? _db.hdctnNhomCongViecs.First(_ => _.RowID == NhomCongViecId) : new Library.hdctnNhomCongViec();
        }

        private void GetDanhMuc()
        {
            glkNoiDungNhomCongViec.Properties.DataSource = _db.hdctnNhomCongViecContents;
            glkNoiDungCongViec.DataSource = _db.hdctnCongViecContents;
        }

        private void sbHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmThemNhom_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _db.Dispose();
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
        }

        private void gvDanhSachCongViecCon_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                var size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                var width = Convert.ToInt32(size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(width, gv); }));
            }
        }
        bool cal(Int32 _width,GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _width ? _width : _View.IndicatorWidth;
            return true;
        }

        private void GlkNoiDungNhomCongViec_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                txtNhomCongViec.Text = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch(System.Exception ex){}
        }

        private void GlkNoiDungCongViec_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (item == null) return;
                if (item.EditValue == null) return;

                gv.SetFocusedRowCellValue("TenCongViec",item.Properties.View.GetFocusedRowCellValue("Name").ToString());
            }
            catch{}
        }
    }
}