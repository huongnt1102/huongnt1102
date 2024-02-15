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
namespace LandSoftBuilding.Lease
{
    public partial class frmCapNhatTrangThai : DevExpress.XtraEditors.XtraForm
    {
        private int ID = 0;
        private int ID_TrangThai = 0;
        private MasterDataContext db = null;

        public frmCapNhatTrangThai(int _ID, int _ID_TrangThai)
        {
            InitializeComponent();
            this.ID = _ID;
            this.ID_TrangThai = _ID_TrangThai;
        }

        private void frmCapNhatTrangThai_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            // Load trang thai
            lkTrangThai.Properties.DataSource = db.PhieuDatCoc_GiuCho_TrangThais;//.Where(p=>p.ID == 1 || p.ID == 2);
            lkTrangThai.EditValue = ID_TrangThai;
        }

        private void itemdong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var dbo = new MasterDataContext())
            {
                var objPDC = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p=>p.ID == ID);
                if(objPDC != null)
                {
                    objPDC.MaTT = Convert.ToInt32(lkTrangThai.EditValue);
                    
                    if(Convert.ToInt32(lkTrangThai.EditValue) == 2)
                    {
                        foreach(var ob in objPDC.PhieuDatCoc_GiuCho_ChiTiets)
                        {
                            using (var dbe = new MasterDataContext())
                            {
                                var objMB = dbe.mbMatBangs.FirstOrDefault(p=>p.MaMB == ob.MaMB);
                                if(objMB != null)
                                {
                                    objMB.MaTT = 85;
                                }
                                dbe.SubmitChanges();
                            }
                        }
                    }else if (Convert.ToInt32(lkTrangThai.EditValue) == 1)
                    {
                        foreach (var ob in objPDC.PhieuDatCoc_GiuCho_ChiTiets)
                        {
                            using (var dbe = new MasterDataContext())
                            {
                                var objMB = dbe.mbMatBangs.FirstOrDefault(p => p.MaMB == ob.MaMB);
                                if (objMB != null)
                                {
                                    objMB.MaTT = 84;
                                }
                                dbe.SubmitChanges();
                            }
                        }
                    }
                    //else if (Convert.ToInt32(lkTrangThai.EditValue) == 3)
                    //{
                    //    foreach (var ob in objPDC.PhieuDatCoc_GiuCho_ChiTiets)
                    //    {
                    //        using (var dbe = new MasterDataContext())
                    //        {
                    //            var objMB = dbe.mbMatBangs.FirstOrDefault(p => p.MaMB == ob.MaMB);
                    //            if (objMB != null)
                    //            {
                    //                objMB.MaTT = 80;
                    //            }
                    //            dbe.SubmitChanges();
                    //        }
                    //    }
                    //}

                    Log_CapNhatTrangThai_PDC objLog = new Log_CapNhatTrangThai_PDC();
                    objLog.NgayCN = db.GetSystemDate();
                    objLog.MaNV = Common.User.MaNV;
                    objLog.MaTrangThaiCu = ID_TrangThai;
                    objLog.MaTrangThaiMoi = Convert.ToInt32(lkTrangThai.EditValue);
                    objLog.ID_PhieuDatCoc_GiuCho = ID;
                    objLog.GhiChu = memoGhiChu.Text;
                    dbo.Log_CapNhatTrangThai_PDCs.InsertOnSubmit(objLog);
                    dbo.SubmitChanges();
                    DialogBox.Success();
                    this.Close();
                }
            }
        }  
    }
}