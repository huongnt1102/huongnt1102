using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq;
using System.Linq;

namespace DIP.SwitchBoard
{
    public partial class ctlNhatKyXuLy : DevExpress.XtraEditors.XtraUserControl
    {
        public string CallID = "";
        public string CallID2 = "";
        public bool isNhanVien = false;

        public ctlNhatKyXuLy()
        {
            InitializeComponent();
        }

        public void NhatKyLoad()
        {
            CallID = CallID.Trim().Replace(" ", "");
            CallID2 = CallID2.Trim().Replace(" ", "");
            try
            {
                if (CallID == ""&&CallID2=="")
                {
                    gcNhatKy.DataSource = null;
                    return;
                }

                using(var db = new DIPCRM.DataEntity.MasterDataContext())
                {
                    gcNhatKy.DataSource = (from c in db.cNhatKies
                                           join t in db.cTrangThaiNKs on c.MaTT equals t.ID into t_c
                                           from t in t_c.DefaultIfEmpty()
                                           join v in db.NhanViens on c.MaNVN equals v.MaNV into tblNhanVien
                                           from v in tblNhanVien.DefaultIfEmpty()
                                           orderby c.NgayXL descending
                                           where c.CallID == CallID
                                           select new
                                           {
                                               c.ID,
                                               NgayXL = c.NgayXL,
                                               DienGiai = c.DienGiai,
                                               Formality = CallID,
                                               TrangThai = t.TenTT,
                                               NhanVien = v.HoTen,
                                               CallID = c.CallID == null ? "" : c.CallID
                                           }).Union(from c in db.cNhatKies
                                                    join t in db.cTrangThaiNKs on c.MaTT equals t.ID into t_c
                                                    from t in t_c.DefaultIfEmpty()
                                                    join v in db.NhanViens on c.MaNVN equals v.MaNV into tblNhanVien
                                                    from v in tblNhanVien.DefaultIfEmpty()
                                                    orderby c.NgayXL descending
                                                    where c.CallID == (isNhanVien==false?CallID2:"-1")
                                                    select new
                                                    {
                                                        c.ID,
                                                        NgayXL = c.NgayXL,
                                                        DienGiai = c.DienGiai,
                                                        Formality = CallID,
                                                        TrangThai = t.TenTT,
                                                        NhanVien = v.HoTen,
                                                        CallID = c.CallID == null ? "" : c.CallID
                                                    }).OrderByDescending(p=>p.NgayXL).ToList();
                }
                
            }
            catch
            {
                
            }
        }

        private void repListen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string CallID = gvNhatKy.GetFocusedRowCellValue("CallID") != null ? gvNhatKy.GetFocusedRowCellValue("CallID").ToString() : "";
            string NgayXL = gvNhatKy.GetFocusedRowCellValue("NgayXL") != null ? gvNhatKy.GetFocusedRowCellValue("NgayXL").ToString() : "";
            DateTime dt = Convert.ToDateTime(NgayXL);
            DateTime dt2 = new DateTime(dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second);
            dt2 = dt2.AddSeconds(2);
            //MessageBox.Show("Số ĐT : " + CallID + " - Ngày : " + dt2.AddMilliseconds(10));
            DIP.SwitchBoard.SwitchBoard.SoftPhone.ListenAgain(CallID, dt2);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvNhatKy.GetFocusedRowCellValue("ID") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn cuộc gọi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idNK = int.Parse( gvNhatKy.GetFocusedRowCellValue("ID").ToString() );
            var frm = new DIPCRM.Customer.frmNhatKyEdit(idNK);
            if (frm.ShowDialog() == DialogResult.OK)
                NhatKyLoad();             
        }
    }
}
