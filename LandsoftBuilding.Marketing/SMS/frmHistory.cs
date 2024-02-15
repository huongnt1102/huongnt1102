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
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Marketing.sms
{
    public partial class frmHistory : DevExpress.XtraEditors.XtraForm
    {
        public frmHistory()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();


            gcPhieuThu.DataSource = (from hs in db.smsHistories

                                    join nv in db.tnNhanViens on hs.StaffCreate equals nv.MaNV
                                    join pt in db.ptPhieuThus on hs.LinkID equals pt.ID into phieu
                                    from pt in phieu.DefaultIfEmpty()

                                    where hs.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, hs.DateCreate) >= 0 & SqlMethods.DateDiffDay(hs.DateCreate, denNgay) >= 0
                                    select new
                                    {
                                        hs.BrandName,
                                        hs.Contents,
                                        hs.DateCreate,
                                        hs.LinkID,
                                        nv.HoTenNV,
                                        hs.ToMobile,pt.SoPT,
                                        hs.ResultSend
                                    }).ToList();
        }

        void RefreshData()
        {
            LoadData();
        }

  

        void Details()
        {
         
        }

    

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuThu.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[7];
            SetDate(7);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void gvPhieuThu_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Details();
        }

        private void gvPhieuThu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Details();
        }
        
       
        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }
        
        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();

            e.QueryableSource = from hs in db.smsHistories
                           
                                join nv in db.tnNhanViens on hs.StaffCreate equals nv.MaNV


                                where hs.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, hs.DateCreate) >= 0 & SqlMethods.DateDiffDay(hs.DateCreate, denNgay) >= 0
                                select new
                                {
                                    hs.BrandName,
                                    hs.Contents,
                                    hs.DateCreate,hs.BrandID,
                                    hs.LinkID,
                                    nv.HoTenNV,hs.ToMobile
                                };
            e.Tag = db;
        }
        
        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DismissQueryable: " + ex.Message);
            }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
       
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == (byte)itemToaNha.EditValue & rp.GroupID == 5
                                orderby rp.Rank
                                select new { rp.ID, rp.Name }).ToList();

             
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                  
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                return;
            }

            var maTN = (byte)itemToaNha.EditValue;

           
        }

        private void btnHTTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                
        
            try
            {
                var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn dòng cần gửi lại SMS");
                    return;
                }
                    #region Gui SMS
                //var smsClient = null;// new ServiceReference1.APISoapClient("APISoap");
                //    var mobileList = null;//new LandSoftBuilding.Marketing.ServiceReference1.ArrayOfString();
                //    mobileList.Add(gvPhieuThu.GetFocusedRowCellValue("ToMobile").ToString());
                //    // smsClient.getBrandNames();
                //    MasterDataContext db = new MasterDataContext();
                 
                //    var result = smsClient.sendSMS("username", "matkhau", mobileList, gvPhieuThu.GetFocusedRowCellValue("BrandName").ToString(), false,
                //            DateTime.Now.ToString("hh:MM, dd/MM/yyyy"), gvPhieuThu.GetFocusedRowCellValue("Contents").ToString(), "", Common.User.HoTenNV);

                    string error = "";
                var result = 0;
                if (result < 0)
                {

                    switch (result)
                    {
                        case -1:
                            error = "Số dư tài khoản không đủ";
                            break;
                        case -2:
                            error = "Tin nhắn không có nội dung";
                            break;
                        case -3:
                            break;
                        case -4:
                            error = "Độ dài tin nhắn vượt quá giới hạn";
                            break;
                        case -5:
                            error = "Không thể gửi tới SĐT này. Kiểm tra lại số điện thoại";
                            break;
                        case -6:
                            error = "Nội dung không được ký tự đặc biệt hoặc viết có dấu";
                            break;
                    }
                    DialogBox.Error("Gửi không thành công. Lỗi: " + error);
                }

                #endregion
                
              
                    #region Save to database

                    using (var dbo = new MasterDataContext())
                    {
                        var objHisSMS = new smsHistory();
                        objHisSMS.DateCreate = DateTime.Now;
                        objHisSMS.StaffCreate = Common.User.MaNV;
                        objHisSMS.FormID =(int?) gvPhieuThu.GetFocusedRowCellValue("LinkID");
                        objHisSMS.LinkID = (int?)gvPhieuThu.GetFocusedRowCellValue("LinkID");
                        objHisSMS.MaTN =(int?) itemToaNha.EditValue;


                        objHisSMS.ResultSend = result < 0 ? error : "Gửi thành công";
                        objHisSMS.BrandID = (int?)gvPhieuThu.GetFocusedRowCellValue("BrandID");
                        objHisSMS.BrandName = gvPhieuThu.GetFocusedRowCellValue("BrandName").ToString();
                        objHisSMS.IsAds = false;
                        objHisSMS.ToMobile = gvPhieuThu.GetFocusedRowCellValue("ToMobile").ToString();
                        objHisSMS.Contents = gvPhieuThu.GetFocusedRowCellValue("Contents").ToString();
                        dbo.smsHistories.InsertOnSubmit(objHisSMS);
                        dbo.SubmitChanges();
                    }

                    #endregion
                
            

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
          
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        
        }
    }
}