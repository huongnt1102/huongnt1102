using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;


namespace Library.Controls
{
    public class ctlNhanVienEdit: LookEdit
    {
        public ctlNhanVienEdit()
        {
            this.ButtonClick += new ButtonPressedEventHandler(ctlNhanVienEdit_ButtonClick);

            this.Properties.DisplayMember = "MaSo";
            this.Properties.ValueMember = "MaNV";
            this.Properties.ShowLines = false;
        }

        void ctlNhanVienEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //switch (e.Button.Index)
            //{
            //    case 1:
            //        if (Library.Common.getAction(15).Contains(1) == false)
            //        {
            //            DialogBox.Error("Bạn không có quyền thêm");
            //            return;
            //        }
            //        //Kiểm tra số lượng User đăng ký
            //        LicenseCountUserOfDIP.LicenseCountUserCls dip = new LicenseCountUserOfDIP.LicenseCountUserCls();
            //        var sqlInfo = new DataEntity.SqlSetting();
            //        string sConectString = it.EncDec.Decrypt(sqlInfo.Conn);
            //        int SoLuongUserDK = dip.GetCountUser(sConectString);
            //        //Get số lượng nhân viên hiện tại của phần mềm
            //        var db = new MasterDataContext();
            //        int SoLuongNV = db.tnNhanViens.Where(p => p.IsDelete == false).Count();
            //        if (SoLuongUserDK > 0)
            //        {
            //            if (SoLuongNV >= SoLuongUserDK)
            //            {
            //               DialogBox.Error("Số lượng User vượt quá số lượng cho phép. Vui lòng liên hệ nhà cung cấp phần mềm");
            //                return;
            //            }
            //        }
            //        using (var frm = new Staff.frmEdit())
            //        {
            //            frm.MaNV = (int?)this.EditValue;
            //            frm.ShowDialog();
            //            if (frm.IsSave)
            //            {
            //                this.LoadData();
            //                this.EditValue = frm.MaNV;
            //            }
            //        }
            //        break;
            //}
        }

        public void CreateColumns()
        {
            this.Properties.Columns.Clear();
            this.Properties.Columns.Add(new LookUpColumnInfo("MaSo", 30, "Mã số"));
            this.Properties.Columns.Add(new LookUpColumnInfo("HoTen", 70, "Họ tên"));
        }

        public void LoadData()
        {
            this.CreateColumns();

            using (var db = new MasterDataContext())
            {
                this.Properties.DataSource = null;
                this.Properties.DataSource = db.tnNhanViens.Where(p=>p.IsLocked == false).Select(p => new { p.MaNV, MaSo = p.MaSoNV, HoTen = p.HoTenNV }).OrderBy(p => p.MaSo).ToList();
                    //Select(p => new { p.MaNV, MaSo = p.MaSoNV, HoTen = p.HoTenNV }).OrderBy(p => p.MaSo).ToList();
            }
        }
    }
}
