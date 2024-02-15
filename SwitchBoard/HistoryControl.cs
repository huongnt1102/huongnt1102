using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Xml.Linq;
using DIP.SoftPhoneAPI;

namespace DIP.SwitchBoard
{
    public partial class HistoryControl : DevExpress.XtraEditors.XtraUserControl
    {
        public int Type { get; set; }

        public HistoryControl()
        {
            InitializeComponent();
        }

        private void historyEditor1_CallHistoryRowClick(object sender, SoftPhoneAPI.CallHistoryRowClickEventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    e.Result = (from kh in db.tnKhachHangs
                               // join lh in db.tnN on kh.MaNLH equals lh.ID into tblLienHe
                              //  from lh in tblLienHe.DefaultIfEmpty()
                                where kh.MaKH == db.getCusIDByPhone(e.PhoneNumber)
                                select new DIP.SoftPhoneAPI.Customer()
                                {
                                    Code = kh.KyHieu,
                                    Name = kh.HoKH+" "+kh.TenKH,
                                    FullName ="",// lh.HoTen,
                                    Email = kh.EmailKH,
                                    Address = kh.DCTT,
                                    Note = ""
                                }).ToList();
                }
            }
            catch { }
        }

        private void historyEditorDB1_CallHistoryBinData(object sender, SoftPhoneAPI.CallHistoryBinDataEventArgs e)
        {
            try
            {
                e.Result = SwitchBoard.GetHistory(e.fromDate, e.toDate, this.Type);
            }
            catch
            {
            }
        }
    }
}
