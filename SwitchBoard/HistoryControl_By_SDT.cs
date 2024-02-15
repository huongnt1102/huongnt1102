using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DIPCRM.DataEntity;

namespace DIP.SwitchBoard
{
    public partial class HistoryControl_By_SDT : DevExpress.XtraEditors.XtraUserControl
    {
        public HistoryControl_By_SDT()
        {
            InitializeComponent();
        }

        private void historyEditor1_CallHistoryRowClick(object sender, SoftPhoneAPI.CallHistoryRowClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    e.Result = (from kh in db.KhachHangs
                                join lh in db.NguoiLienHes on kh.MaNLH equals lh.ID into tblLienHe
                                from lh in tblLienHe.DefaultIfEmpty()
                                where kh.MaKH == db.getCusIDByPhone(e.PhoneNumber)
                                select new DIP.SoftPhoneAPI.Customer()
                                {
                                    Code = kh.TenVietTat,
                                    Name = kh.TenCongTy,
                                    FullName = lh.HoTen,
                                    Email = kh.Email,
                                    Address = kh.DiaChiCT,
                                    Note = kh.GhiChu
                                }).ToList();
                }
            }
            catch { }
        }
    }
}
