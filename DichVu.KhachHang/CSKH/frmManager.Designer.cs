using DevExpress.Data;
using DevExpress.Data.Linq;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DIP.SwitchBoard;
//using DIPCRM.Customer.Contact;
//using DIPCRM.Customer.Reports;
//using KyThuat.KhachHang.NguoiLienHe;
using Library;
//using Library.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace DichVu.KhachHang.CSKH
{
    partial class frmManager
    {
        private IContainer components = null;

        private BarManager barManager1;

        private Bar bar1;

        private BarDockControl barDockControlTop;

        private BarDockControl barDockControlBottom;

        private BarDockControl barDockControlLeft;

        private BarDockControl barDockControlRight;

        private RepositoryItemSpinEdit repositoryItemSpinEditNumberPage;

        private RepositoryItemSpinEdit repositoryItemSpinEditTotal;

        private RepositoryItemTextEdit repositoryItemTextEdit1;

        private BarButtonItem itemRefresh;

        private BarButtonItem itemProcess;

        private GridControl gcCustomers;

        private GridView grvKH;

        private GridColumn gridColumn1;

        private GridColumn gridColumn3;

        private GridColumn gridColumn4;

        private GridColumn gridColumn5;

        private GridColumn gridColumn12;

        private BarButtonItem itemSearch;

        private GridColumn gridColumn14;

        private BarButtonItem itemDelete;

        private GridColumn gridColumn15;

        private RepositoryItemLookUpEdit lookUpTinh;

        private BarButtonItem itemExport;

        private BarSubItem subExport;

        private BarButtonItem itemExportList;

        private BarButtonItem itemEdit;

        private SplitContainerControl splitContainerControl1;

        private LabelControl lblDiDong;

        private LabelControl lblNguoiDaiDien;

        private LabelControl lblMaSoThue;

        private LabelControl lblTenCongTy;

        private LabelControl lblEmail;

        private LabelControl lblNgayCN;

        private LabelControl lblWebsite;

        private LabelControl lblDienThoai;

        private BarButtonItem itemSendMail;

        private BarButtonItem itemSendSMS;

        private XtraTabControl xtraTabControl1;

        private XtraTabPage xtraTabPage1;

        private XtraTabPage xtraTabPage2;

        private BarButtonItem itemImport;

        private GridColumn gridColumn6;

        private GridColumn gridColumn10;

        private GridColumn gridColumn16;

        private XtraTabPage xtraTabPage4;

        private BarButtonItem itemAdd;

        private XtraTabPage xtraTabPage6;

        private GridColumn gridColumn27;

        private NguoiLienHe.ctlContact ctlContact1 ;

        private Library.Controls.NhuCau.ctlNhuCau ctlNhuCau1;

        private XtraTabPage xtraTabPage3;

        private XtraTabPage xtraTabPage7;

        private XtraTabPage xtraTabPage8;

        private GridColumn gridColumn2;

        private GridColumn gridColumn18;

        private GridColumn gridColumn20;

        private GridColumn gridColumn21;

        private GridColumn gridColumn19;

        private RepositoryItemButtonEdit btnCallPhone;

        private RepositoryItemComboBox cmbKyBaoCao;

        private RepositoryItemDateEdit dateTuNgay;

        private RepositoryItemDateEdit dateDenNgay;

        private GridColumn gridColumn7;

        private GridColumn gridColumn13;

        private RepositoryItemLookUpEdit lkXaEdit;

        private RepositoryItemLookUpEdit lkHuyenEdit;

        private BarEditItem itemCongTy;

        private RepositoryItemCheckedComboBoxEdit cmbCongTy;

        private GridColumn gridColumn22;

        private RepositoryItemLookUpEdit lkNhanVien;

        private GridColumn gridColumn23;

        private XtraTabPage xtraTabPage9;

        private XtraTabPage xtraTabPage10;

        private RepositoryItemHyperLinkEdit hlpEmail;

        //private ctlMailHistory ctlMailHistory1;

        private GridColumn gridColumn8;

        //private ctlContractSale ctlContractSale1;

        private BarButtonItem itemXem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemCongTy = new DevExpress.XtraBars.BarEditItem();
            this.cmbCongTy = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemXem = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemImport = new DevExpress.XtraBars.BarButtonItem();
            this.itemExport = new DevExpress.XtraBars.BarButtonItem();
            this.itemProcess = new DevExpress.XtraBars.BarButtonItem();
            this.itemSendMail = new DevExpress.XtraBars.BarButtonItem();
            this.itemSendSMS = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.itemChange = new DevExpress.XtraBars.BarButtonItem();
            this.ItemChuyenDangChamSoc = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.itemConvert = new DevExpress.XtraBars.BarButtonItem();
            this.itemRemoveConvert = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemSearch = new DevExpress.XtraBars.BarButtonItem();
            this.subExport = new DevExpress.XtraBars.BarSubItem();
            this.itemExportList = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.itemDSKHCN = new DevExpress.XtraBars.BarButtonItem();
            this.itemDSKHDN = new DevExpress.XtraBars.BarButtonItem();
            this.itemThemLichHen = new DevExpress.XtraBars.BarButtonItem();
            this.itemSuaLichHen = new DevExpress.XtraBars.BarButtonItem();
            this.itemXoaLichHen = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemSpinEditNumberPage = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemSpinEditTotal = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.ctlLichHen1 = new Library.Controls.LichHen.ctlLichHen();
            this.gcCustomers = new DevExpress.XtraGrid.GridControl();
            this.grvKH = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbNgheNghiep = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hlpEmail = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCallPhone = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn34 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkNhanVien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IsChinhThuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookUpTinh = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lkXaEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lkHuyenEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lblDiDong = new DevExpress.XtraEditors.LabelControl();
            this.lblNguoiDaiDien = new DevExpress.XtraEditors.LabelControl();
            this.lblDienThoai = new DevExpress.XtraEditors.LabelControl();
            this.lblMaSoThue = new DevExpress.XtraEditors.LabelControl();
            this.lblNgayCN = new DevExpress.XtraEditors.LabelControl();
            this.lblWebsite = new DevExpress.XtraEditors.LabelControl();
            this.lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.lblTenCongTy = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.ctlContact1 = new NguoiLienHe.ctlContact();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.ctlNhuCau1 = new Library.Controls.NhuCau.ctlNhuCau();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            //this.ctlMailHistory1 = new Library.Other.ctlMailHistory();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            //this.ctlNhiemVu1 = new Building.WorkSchedule.NhiemVu.ctlNhiemVu();
            this.xtraTabPage8 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage9 = new DevExpress.XtraTab.XtraTabPage();
            //this.ctlContractSale1 = new Library.Other.ctlContractSale();
            this.xtraTabPage10 = new DevExpress.XtraTab.XtraTabPage();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCongTy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumberPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNgheNghiep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlpEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCallPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkXaEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkHuyenEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage6.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.xtraTabPage7.SuspendLayout();
            this.xtraTabPage9.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.HideBarsWhenMerging = false;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemRefresh,
            this.itemProcess,
            this.itemSearch,
            this.itemDelete,
            this.itemExport,
            this.subExport,
            this.itemExportList,
            this.itemEdit,
            this.itemSendMail,
            this.itemSendSMS,
            this.itemImport,
            this.itemAdd,
            this.itemCongTy,
            this.itemXem,
            this.barButtonItem1,
            this.barSubItem1,
            this.itemDSKHCN,
            this.itemDSKHDN,
            this.itemChange,
            this.itemThemLichHen,
            this.itemSuaLichHen,
            this.itemXoaLichHen,
            this.barSubItem2,
            this.ItemChuyenDangChamSoc,
            this.itemConvert,
            this.barSubItem3,
            this.itemRemoveConvert});
            this.barManager1.MaxItemId = 49;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemSpinEditNumberPage,
            this.repositoryItemSpinEditTotal,
            this.cmbKyBaoCao,
            this.dateTuNgay,
            this.dateDenNgay,
            this.cmbCongTy});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemCongTy, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemXem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemProcess, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSendMail, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSendSMS, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemCongTy
            // 
            this.itemCongTy.Caption = "Chi nhánh";
            this.itemCongTy.Edit = this.cmbCongTy;
            this.itemCongTy.EditWidth = 120;
            this.itemCongTy.Id = 25;
            this.itemCongTy.Name = "itemCongTy";
            // 
            // cmbCongTy
            // 
            this.cmbCongTy.AutoHeight = false;
            this.cmbCongTy.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCongTy.DisplayMember = "TenCT";
            this.cmbCongTy.Name = "cmbCongTy";
            this.cmbCongTy.SelectAllItemCaption = "Chọn tất cả";
            this.cmbCongTy.ValueMember = "ID";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 5;
            this.itemRefresh.ImageOptions.ImageIndex = 3;
            this.itemRefresh.Name = "itemRefresh";
            this.itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRefresh_ItemClick);
            // 
            // itemXem
            // 
            this.itemXem.Caption = "Xem";
            this.itemXem.Id = 27;
            this.itemXem.ImageOptions.ImageIndex = 9;
            this.itemXem.Name = "itemXem";
            this.itemXem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemXem_ItemClick);
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm";
            this.itemAdd.Id = 21;
            this.itemAdd.ImageOptions.ImageIndex = 0;
            this.itemAdd.Name = "itemAdd";
            this.itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemAdd_ItemClick);
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 17;
            this.itemEdit.ImageOptions.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemEdit_ItemClick);
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 13;
            this.itemDelete.ImageOptions.ImageIndex = 1;
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemDelete_ItemClick);
            // 
            // itemImport
            // 
            this.itemImport.Caption = "Import";
            this.itemImport.Id = 20;
            this.itemImport.ImageOptions.ImageIndex = 4;
            this.itemImport.Name = "itemImport";
            this.itemImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemImport_ItemClick);
            // 
            // itemExport
            // 
            this.itemExport.Caption = "Export";
            this.itemExport.Id = 14;
            this.itemExport.ImageOptions.ImageIndex = 7;
            this.itemExport.Name = "itemExport";
            this.itemExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExport_ItemClick);
            // 
            // itemProcess
            // 
            this.itemProcess.Caption = "Xử lý";
            this.itemProcess.Id = 8;
            this.itemProcess.ImageOptions.ImageIndex = 17;
            this.itemProcess.Name = "itemProcess";
            this.itemProcess.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemProcess_ItemClick);
            // 
            // itemSendMail
            // 
            this.itemSendMail.Caption = "Gửi mail";
            this.itemSendMail.Id = 18;
            this.itemSendMail.ImageOptions.ImageIndex = 14;
            this.itemSendMail.Name = "itemSendMail";
            this.itemSendMail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSendMail_ItemClick);
            // 
            // itemSendSMS
            // 
            this.itemSendSMS.Caption = "Gửi SMS";
            this.itemSendSMS.Id = 19;
            this.itemSendSMS.ImageOptions.ImageIndex = 15;
            this.itemSendSMS.Name = "itemSendSMS";
            this.itemSendSMS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSendSMS_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Chuyển đổi";
            this.barSubItem2.Id = 43;
            this.barSubItem2.ImageOptions.ImageIndex = 18;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.ItemChuyenDangChamSoc)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // itemChange
            // 
            this.itemChange.Caption = "Chính thức";
            this.itemChange.Id = 34;
            this.itemChange.ImageOptions.ImageIndex = 20;
            this.itemChange.Name = "itemChange";
            this.itemChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemChange_ItemClick);
            // 
            // ItemChuyenDangChamSoc
            // 
            this.ItemChuyenDangChamSoc.Caption = "Bỏ chính thức";
            this.ItemChuyenDangChamSoc.Id = 45;
            this.ItemChuyenDangChamSoc.ImageOptions.ImageIndex = 19;
            this.ItemChuyenDangChamSoc.Name = "ItemChuyenDangChamSoc";
            this.ItemChuyenDangChamSoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemChuyenDangChamSoc_ItemClick);
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "Lấy dữ liệu";
            this.barSubItem3.Id = 47;
            this.barSubItem3.ImageOptions.ImageIndex = 12;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemConvert, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemRemoveConvert)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // itemConvert
            // 
            this.itemConvert.Caption = "Lấy dữ liệu từ khách hàng chính thức";
            this.itemConvert.Id = 46;
            this.itemConvert.ImageOptions.ImageIndex = 10;
            this.itemConvert.Name = "itemConvert";
            this.itemConvert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemConvert_ItemClick);
            // 
            // itemRemoveConvert
            // 
            this.itemRemoveConvert.Caption = "Bỏ dữ liệu";
            this.itemRemoveConvert.Id = 48;
            this.itemRemoveConvert.ImageOptions.ImageIndex = 11;
            this.itemRemoveConvert.Name = "itemRemoveConvert";
            this.itemRemoveConvert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemRemoveConvert_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1335, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 622);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1335, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 591);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1335, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 591);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add_outline.png");
            this.imageCollection1.Images.SetKeyName(1, "edit-delete.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            this.imageCollection1.Images.SetKeyName(3, "108.gif");
            this.imageCollection1.Images.SetKeyName(4, "table-import.png");
            this.imageCollection1.Images.SetKeyName(5, "preview-file.png");
            this.imageCollection1.Images.SetKeyName(6, "1336097347_agt_print.png");
            this.imageCollection1.Images.SetKeyName(7, "table-export.png");
            this.imageCollection1.Images.SetKeyName(8, "invoice.png");
            this.imageCollection1.Images.SetKeyName(9, "search.png");
            this.imageCollection1.Images.SetKeyName(10, "add.png");
            this.imageCollection1.Images.SetKeyName(11, "remove.png");
            this.imageCollection1.Images.SetKeyName(12, "icon.png");
            this.imageCollection1.Images.SetKeyName(13, "email-message-by-mobile-phone.png");
            this.imageCollection1.Images.SetKeyName(14, "gmail.png");
            this.imageCollection1.Images.SetKeyName(15, "chat.png");
            this.imageCollection1.Images.SetKeyName(16, "document.png");
            this.imageCollection1.Images.SetKeyName(17, "edit.png");
            this.imageCollection1.Images.SetKeyName(18, "transfer (1).png");
            this.imageCollection1.Images.SetKeyName(19, "cancel.png");
            this.imageCollection1.Images.SetKeyName(20, "check.png");
            // 
            // itemSearch
            // 
            this.itemSearch.Caption = "Tìm kiếm";
            this.itemSearch.Id = 11;
            this.itemSearch.ImageOptions.ImageIndex = 8;
            this.itemSearch.Name = "itemSearch";
            // 
            // subExport
            // 
            this.subExport.Caption = "Export";
            this.subExport.Id = 15;
            this.subExport.ImageOptions.ImageIndex = 10;
            this.subExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemExportList)});
            this.subExport.Name = "subExport";
            // 
            // itemExportList
            // 
            this.itemExportList.Caption = "Danh sách tìm được";
            this.itemExportList.Id = 16;
            this.itemExportList.Name = "itemExportList";
            this.itemExportList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemExportList_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 29;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Chuyển sang danh sách khách hàng chính thức";
            this.barSubItem1.Id = 31;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDSKHCN),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDSKHDN)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // itemDSKHCN
            // 
            this.itemDSKHCN.Id = 35;
            this.itemDSKHCN.Name = "itemDSKHCN";
            // 
            // itemDSKHDN
            // 
            this.itemDSKHDN.Id = 36;
            this.itemDSKHDN.Name = "itemDSKHDN";
            // 
            // itemThemLichHen
            // 
            this.itemThemLichHen.Id = 40;
            this.itemThemLichHen.Name = "itemThemLichHen";
            // 
            // itemSuaLichHen
            // 
            this.itemSuaLichHen.Id = 41;
            this.itemSuaLichHen.Name = "itemSuaLichHen";
            // 
            // itemXoaLichHen
            // 
            this.itemXoaLichHen.Id = 42;
            this.itemXoaLichHen.Name = "itemXoaLichHen";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemSpinEditNumberPage
            // 
            this.repositoryItemSpinEditNumberPage.AutoHeight = false;
            this.repositoryItemSpinEditNumberPage.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditNumberPage.Name = "repositoryItemSpinEditNumberPage";
            // 
            // repositoryItemSpinEditTotal
            // 
            this.repositoryItemSpinEditTotal.AutoHeight = false;
            this.repositoryItemSpinEditTotal.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditTotal.DisplayFormat.FormatString = "{0:n0}";
            this.repositoryItemSpinEditTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEditTotal.EditFormat.FormatString = "{0:n0}";
            this.repositoryItemSpinEditTotal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEditTotal.Name = "repositoryItemSpinEditTotal";
            this.repositoryItemSpinEditTotal.ReadOnly = true;
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.NullText = "Kỳ báo cáo";
            this.cmbKyBaoCao.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "Từ ngày";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.AutoHeight = false;
            this.dateDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.NullText = "Đến ngày";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.ctlLichHen1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1329, 272);
            this.xtraTabPage3.Text = "Lịch hẹn";
            // 
            // ctlLichHen1
            // 
            this.ctlLichHen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlLichHen1.frm = null;
            this.ctlLichHen1.Location = new System.Drawing.Point(0, 0);
            this.ctlLichHen1.Name = "ctlLichHen1";
            this.ctlLichHen1.Size = new System.Drawing.Size(1329, 272);
            this.ctlLichHen1.TabIndex = 0;
            // 
            // gcCustomers
            // 
            this.gcCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCustomers.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcCustomers.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcCustomers.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcCustomers.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcCustomers.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcCustomers.EmbeddedNavigator.TextStringFormat = "{0} / {1}";
            this.gcCustomers.Location = new System.Drawing.Point(0, 0);
            this.gcCustomers.MainView = this.grvKH;
            this.gcCustomers.MenuManager = this.barManager1;
            this.gcCustomers.Name = "gcCustomers";
            this.gcCustomers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookUpTinh,
            this.btnCallPhone,
            this.lkXaEdit,
            this.lkHuyenEdit,
            this.lkNhanVien,
            this.hlpEmail,
            this.repositoryItemCheckEdit1,
            this.cmbNgheNghiep});
            this.gcCustomers.Size = new System.Drawing.Size(1335, 286);
            this.gcCustomers.TabIndex = 4;
            this.gcCustomers.UseEmbeddedNavigator = true;
            this.gcCustomers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvKH});
            // 
            // grvKH
            // 
            this.grvKH.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn27,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn13,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn4,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn9,
            this.gridColumn38,
            this.gridColumn10,
            this.gridColumn37,
            this.gridColumn25,
            this.gridColumn6,
            this.gridColumn26,
            this.gridColumn28,
            this.gridColumn29,
            this.gridColumn14,
            this.gridColumn31,
            this.gridColumn32,
            this.gridColumn33,
            this.gridColumn34,
            this.gridColumn35,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn36,
            this.gridColumn17,
            this.gridColumn24,
            this.gridColumn39,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn30,
            this.IsChinhThuc,
            this.gridColumn45,
            this.gridColumn40,
            this.gridColumn41,
            this.gridColumn42,
            this.gridColumn43,
            this.gridColumn44});
            this.grvKH.GridControl = this.gcCustomers;
            this.grvKH.IndicatorWidth = 30;
            this.grvKH.Name = "grvKH";
            this.grvKH.OptionsFind.AlwaysVisible = true;
            this.grvKH.OptionsSelection.MultiSelect = true;
            this.grvKH.OptionsView.ColumnAutoWidth = false;
            this.grvKH.OptionsView.ShowAutoFilterRow = true;
            this.grvKH.OptionsView.ShowGroupPanel = false;
            this.grvKH.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn7, DevExpress.Data.ColumnSortOrder.Descending)});
            this.grvKH.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.grvKH_CustomDrawRowIndicator);
            this.grvKH.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvKH_FocusedRowChanged);
            this.grvKH.FocusedRowLoaded += new DevExpress.XtraGrid.Views.Base.RowEventHandler(this.grvKH_FocusedRowLoaded);
            this.grvKH.DoubleClick += new System.EventHandler(this.grvKH_DoubleClick);
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "Mã hiệu";
            this.gridColumn27.FieldName = "TenVietTat";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.OptionsColumn.AllowEdit = false;
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 0;
            this.gridColumn27.Width = 119;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên KH, NCC";
            this.gridColumn1.FieldName = "TenCongTy";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 177;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Địa chỉ";
            this.gridColumn3.FieldName = "DiaChi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 10;
            this.gridColumn3.Width = 116;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Tỉnh(TP)";
            this.gridColumn15.FieldName = "TenTinh";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 20;
            this.gridColumn15.Width = 89;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Quận/Huyện";
            this.gridColumn16.FieldName = "TenHuyen";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 19;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Xã/Phường";
            this.gridColumn13.FieldName = "TenXa";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 18;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Loại";
            this.gridColumn20.FieldName = "TenLoaiKH";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 17;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Nhóm";
            this.gridColumn21.FieldName = "TenNKH";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 16;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ngành nghề";
            this.gridColumn4.ColumnEdit = this.cmbNgheNghiep;
            this.gridColumn4.FieldName = "idNgheNghiep";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 15;
            this.gridColumn4.Width = 147;
            // 
            // cmbNgheNghiep
            // 
            this.cmbNgheNghiep.AutoHeight = false;
            this.cmbNgheNghiep.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNgheNghiep.Name = "cmbNgheNghiep";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Loại hình kinh doanh";
            this.gridColumn11.FieldName = "TenLoaiHinhKD";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 28;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Mã số thuế";
            this.gridColumn12.FieldName = "MaSoThue";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 22;
            this.gridColumn12.Width = 77;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Nguồn đến";
            this.gridColumn9.FieldName = "TenNguon";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 27;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "Ngày cấp MST";
            this.gridColumn38.FieldName = "NgayCapMST";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 42;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.Caption = "Email";
            this.gridColumn10.ColumnEdit = this.hlpEmail;
            this.gridColumn10.FieldName = "Email";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 100;
            // 
            // hlpEmail
            // 
            this.hlpEmail.AutoHeight = false;
            this.hlpEmail.Name = "hlpEmail";
            this.hlpEmail.ReadOnly = true;
            this.hlpEmail.Click += new System.EventHandler(this.hlpEmail_Click);
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "Nơi cấp MST";
            this.gridColumn37.FieldName = "NoiCapMST";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 41;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "Số tài khoản";
            this.gridColumn25.FieldName = "SoTKNH";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 31;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Điện thoại";
            this.gridColumn6.ColumnEdit = this.btnCallPhone;
            this.gridColumn6.FieldName = "DienThoai";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 91;
            // 
            // btnCallPhone
            // 
            this.btnCallPhone.AllowFocused = false;
            this.btnCallPhone.AutoHeight = false;
            this.btnCallPhone.Name = "btnCallPhone";
            this.btnCallPhone.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCallPhone.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCallPhone_ButtonClick);
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "Tên ngân hàng";
            this.gridColumn26.FieldName = "TenNH";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 32;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "Website";
            this.gridColumn28.FieldName = "Website";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 33;
            // 
            // gridColumn29
            // 
            this.gridColumn29.Caption = "Số nợ tối đa";
            this.gridColumn29.FieldName = "NoToiDa";
            this.gridColumn29.Name = "gridColumn29";
            this.gridColumn29.Visible = true;
            this.gridColumn29.VisibleIndex = 34;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Ghi chú";
            this.gridColumn14.FieldName = "GhiChu";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 23;
            this.gridColumn14.Width = 120;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "Fax";
            this.gridColumn31.FieldName = "Fax";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 35;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "Quy mô";
            this.gridColumn32.FieldName = "TenQM";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 36;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "Vốn điều lệ";
            this.gridColumn33.FieldName = "VonDieuLe";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 37;
            // 
            // gridColumn34
            // 
            this.gridColumn34.Caption = "Số giấy phép kinh doanh";
            this.gridColumn34.FieldName = "SoGPKD";
            this.gridColumn34.Name = "gridColumn34";
            this.gridColumn34.Visible = true;
            this.gridColumn34.VisibleIndex = 38;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "Ngày thành lập";
            this.gridColumn35.FieldName = "NgayDKKD";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 39;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Di động";
            this.gridColumn18.ColumnEdit = this.btnCallPhone;
            this.gridColumn18.FieldName = "DiDong";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 8;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Số CMND";
            this.gridColumn19.FieldName = "SoCMND";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 21;
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "Ngày Sinh";
            this.gridColumn36.FieldName = "NgaySinh";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.Visible = true;
            this.gridColumn36.VisibleIndex = 40;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Ngày Cấp";
            this.gridColumn17.FieldName = "NgayCap";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 29;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Nơi Cấp";
            this.gridColumn24.FieldName = "NoiCap";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 30;
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "Số hộ chiếu";
            this.gridColumn39.FieldName = "HoChieu";
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 43;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Người liên hệ";
            this.gridColumn5.FieldName = "TenNLH";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 12;
            this.gridColumn5.Width = 123;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Di động NLH";
            this.gridColumn2.ColumnEdit = this.btnCallPhone;
            this.gridColumn2.FieldName = "DiDongNLH";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 14;
            this.gridColumn2.Width = 84;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Người nhập";
            this.gridColumn22.ColumnEdit = this.lkNhanVien;
            this.gridColumn22.FieldName = "MaNV";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 25;
            this.gridColumn22.Width = 130;
            // 
            // lkNhanVien
            // 
            this.lkNhanVien.AutoHeight = false;
            this.lkNhanVien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkNhanVien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTen", "Name1")});
            this.lkNhanVien.DisplayMember = "HoTen";
            this.lkNhanVien.Name = "lkNhanVien";
            this.lkNhanVien.NullText = "";
            this.lkNhanVien.ShowHeader = false;
            this.lkNhanVien.ShowLines = false;
            this.lkNhanVien.ValueMember = "MaNV";
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Ngày nhập";
            this.gridColumn23.DisplayFormat.FormatString = "{0:dd/MM/yyyy | HH:mm}";
            this.gridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn23.FieldName = "NgayDangKy";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 26;
            this.gridColumn23.Width = 81;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ngày CN";
            this.gridColumn7.FieldName = "NgayCN";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Nhân viên quản lý";
            this.gridColumn8.FieldName = "NhanVienQuanLy";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 13;
            this.gridColumn8.Width = 112;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Phân loại";
            this.gridColumn30.FieldName = "PhanLoai";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 3;
            this.gridColumn30.Width = 143;
            // 
            // IsChinhThuc
            // 
            this.IsChinhThuc.Caption = "Chính thức";
            this.IsChinhThuc.ColumnEdit = this.repositoryItemCheckEdit1;
            this.IsChinhThuc.FieldName = "IsChinhThuc";
            this.IsChinhThuc.Name = "IsChinhThuc";
            this.IsChinhThuc.Visible = true;
            this.IsChinhThuc.VisibleIndex = 2;
            this.IsChinhThuc.Width = 85;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "gridColumn40";
            this.gridColumn40.FieldName = "MaKH";
            this.gridColumn40.Name = "gridColumn40";
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "CSKH";
            this.gridColumn41.FieldName = "IsCSKH";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.OptionsColumn.AllowEdit = false;
            this.gridColumn41.Visible = true;
            this.gridColumn41.VisibleIndex = 4;
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "Trạng thái";
            this.gridColumn42.FieldName = "TrangThaiXL";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.Visible = true;
            this.gridColumn42.VisibleIndex = 6;
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "Ngày xử lý";
            this.gridColumn43.DisplayFormat.FormatString = "d";
            this.gridColumn43.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn43.FieldName = "XuLy_NgayXuLy";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.Visible = true;
            this.gridColumn43.VisibleIndex = 5;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "Nguồn đến";
            this.gridColumn44.FieldName = "TenNguon";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 11;
            this.gridColumn44.Width = 167;
            // 
            // lookUpTinh
            // 
            this.lookUpTinh.AutoHeight = false;
            this.lookUpTinh.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpTinh.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTinh", "Name1")});
            this.lookUpTinh.DisplayMember = "TenTinh";
            this.lookUpTinh.Name = "lookUpTinh";
            this.lookUpTinh.NullText = "";
            this.lookUpTinh.ShowHeader = false;
            this.lookUpTinh.ValueMember = "MaTinh";
            // 
            // lkXaEdit
            // 
            this.lkXaEdit.AutoHeight = false;
            this.lkXaEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkXaEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenXa", "TenXa")});
            this.lkXaEdit.DisplayMember = "TenXa";
            this.lkXaEdit.Name = "lkXaEdit";
            this.lkXaEdit.NullText = "";
            this.lkXaEdit.ShowHeader = false;
            this.lkXaEdit.ShowLines = false;
            this.lkXaEdit.ValueMember = "MaXa";
            // 
            // lkHuyenEdit
            // 
            this.lkHuyenEdit.AutoHeight = false;
            this.lkHuyenEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkHuyenEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHuyen", "TenHuyen")});
            this.lkHuyenEdit.DisplayMember = "TenHuyen";
            this.lkHuyenEdit.Name = "lkHuyenEdit";
            this.lkHuyenEdit.NullText = "";
            this.lkHuyenEdit.ShowHeader = false;
            this.lkHuyenEdit.ShowLines = false;
            this.lkHuyenEdit.ValueMember = "MaHuyen";
            // 
            // lblDiDong
            // 
            this.lblDiDong.AllowHtmlString = true;
            this.lblDiDong.Location = new System.Drawing.Point(496, 59);
            this.lblDiDong.Name = "lblDiDong";
            this.lblDiDong.Size = new System.Drawing.Size(113, 13);
            this.lblDiDong.TabIndex = 0;
            this.lblDiDong.Text = "Di động: <b>0987654321</b>";
            // 
            // lblNguoiDaiDien
            // 
            this.lblNguoiDaiDien.AllowHtmlString = true;
            this.lblNguoiDaiDien.Location = new System.Drawing.Point(496, 39);
            this.lblNguoiDaiDien.Name = "lblNguoiDaiDien";
            this.lblNguoiDaiDien.Size = new System.Drawing.Size(142, 13);
            this.lblNguoiDaiDien.TabIndex = 0;
            this.lblNguoiDaiDien.Text = "Người đại diện: <b>DIPVietnam</b>";
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AllowHtmlString = true;
            this.lblDienThoai.Location = new System.Drawing.Point(18, 58);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(142, 13);
            this.lblDienThoai.TabIndex = 0;
            this.lblDienThoai.Text = "Điện thoại: <b>(08) 3 5424234</b>";
            // 
            // lblMaSoThue
            // 
            this.lblMaSoThue.AllowHtmlString = true;
            this.lblMaSoThue.Location = new System.Drawing.Point(18, 38);
            this.lblMaSoThue.Name = "lblMaSoThue";
            this.lblMaSoThue.Size = new System.Drawing.Size(137, 13);
            this.lblMaSoThue.TabIndex = 0;
            this.lblMaSoThue.Text = "Mã số thuế: <b>03100056725</b>";
            // 
            // lblNgayCN
            // 
            this.lblNgayCN.AllowHtmlString = true;
            this.lblNgayCN.Location = new System.Drawing.Point(496, 79);
            this.lblNgayCN.Name = "lblNgayCN";
            this.lblNgayCN.Size = new System.Drawing.Size(145, 13);
            this.lblNgayCN.TabIndex = 0;
            this.lblNgayCN.Text = "Ngày cập nhật: <b>01/01/2012</b>";
            // 
            // lblWebsite
            // 
            this.lblWebsite.AllowHtmlString = true;
            this.lblWebsite.Location = new System.Drawing.Point(496, 18);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(119, 13);
            this.lblWebsite.TabIndex = 0;
            this.lblWebsite.Text = "Website: <b>http://dip.vn</b>";
            // 
            // lblEmail
            // 
            this.lblEmail.AllowHtmlString = true;
            this.lblEmail.Location = new System.Drawing.Point(18, 79);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(96, 13);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email: <b>info@dip.vn</b>";
            // 
            // lblTenCongTy
            // 
            this.lblTenCongTy.AllowHtmlString = true;
            this.lblTenCongTy.Location = new System.Drawing.Point(18, 18);
            this.lblTenCongTy.Name = "lblTenCongTy";
            this.lblTenCongTy.Size = new System.Drawing.Size(130, 13);
            this.lblTenCongTy.TabIndex = 0;
            this.lblTenCongTy.Text = "Khách hàng: <b>DIPVietnam</b>";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcCustomers);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1335, 591);
            this.splitContainerControl1.SplitterPosition = 300;
            this.splitContainerControl1.TabIndex = 14;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1335, 300);
            this.xtraTabControl1.TabIndex = 10;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage6,
            this.xtraTabPage2,
            this.xtraTabPage4,
            this.xtraTabPage3,
            this.xtraTabPage7,
            this.xtraTabPage8,
            this.xtraTabPage9,
            this.xtraTabPage10});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.lblDiDong);
            this.xtraTabPage1.Controls.Add(this.lblTenCongTy);
            this.xtraTabPage1.Controls.Add(this.lblNguoiDaiDien);
            this.xtraTabPage1.Controls.Add(this.lblEmail);
            this.xtraTabPage1.Controls.Add(this.lblDienThoai);
            this.xtraTabPage1.Controls.Add(this.lblWebsite);
            this.xtraTabPage1.Controls.Add(this.lblMaSoThue);
            this.xtraTabPage1.Controls.Add(this.lblNgayCN);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Padding = new System.Windows.Forms.Padding(1);
            this.xtraTabPage1.Size = new System.Drawing.Size(1329, 272);
            this.xtraTabPage1.Text = "Thông tin chi tiết";
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.ctlContact1);
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(1329, 272);
            this.xtraTabPage6.Text = "Người liên hệ";
            // 
            // ctlContact1
            // 
            this.ctlContact1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlContact1.frm = null;
            this.ctlContact1.Location = new System.Drawing.Point(0, 0);
            this.ctlContact1.MaKH = null;
            this.ctlContact1.MaTN = null;
            this.ctlContact1.Name = "ctlContact1";
            this.ctlContact1.Size = new System.Drawing.Size(1329, 272);
            this.ctlContact1.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ctlNhuCau1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Padding = new System.Windows.Forms.Padding(1);
            this.xtraTabPage2.Size = new System.Drawing.Size(1329, 272);
            this.xtraTabPage2.Text = "Cơ hội bán hàng";
            // 
            // ctlNhuCau1
            // 
            this.ctlNhuCau1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlNhuCau1.frm = null;
            this.ctlNhuCau1.Location = new System.Drawing.Point(1, 1);
            this.ctlNhuCau1.Name = "ctlNhuCau1";
            this.ctlNhuCau1.Size = new System.Drawing.Size(1327, 270);
            this.ctlNhuCau1.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            //this.xtraTabPage4.Controls.Add(this.ctlMailHistory1);
            //this.xtraTabPage4.Name = "xtraTabPage4";
            //this.xtraTabPage4.Size = new System.Drawing.Size(1329, 272);
            //this.xtraTabPage4.Text = "Mail";
            //// 
            //// ctlMailHistory1
            //// 
            //this.ctlMailHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ctlMailHistory1.FormID = null;
            //this.ctlMailHistory1.frm = null;
            //this.ctlMailHistory1.LinkID = null;
            //this.ctlMailHistory1.Location = new System.Drawing.Point(0, 0);
            //this.ctlMailHistory1.MaKH = null;
            //this.ctlMailHistory1.Name = "ctlMailHistory1";
            //this.ctlMailHistory1.Size = new System.Drawing.Size(1329, 272);
            //this.ctlMailHistory1.TabIndex = 0;
            //// 
            //// xtraTabPage7
            //// 
            //this.xtraTabPage7.Controls.Add(this.ctlNhiemVu1);
            //this.xtraTabPage7.Name = "xtraTabPage7";
            //this.xtraTabPage7.Size = new System.Drawing.Size(1329, 272);
            //this.xtraTabPage7.Text = "Nhiệm vụ";
            //// 
            //// ctlNhiemVu1
            //// 
            //this.ctlNhiemVu1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ctlNhiemVu1.frm = null;
            //this.ctlNhiemVu1.Location = new System.Drawing.Point(0, 0);
            //this.ctlNhiemVu1.MaCD = null;
            //this.ctlNhiemVu1.MaKH = null;
            //this.ctlNhiemVu1.MaLSP = null;
            //this.ctlNhiemVu1.MaNV = null;
            //this.ctlNhiemVu1.MaSP = null;
            //this.ctlNhiemVu1.Name = "ctlNhiemVu1";
            //this.ctlNhiemVu1.Size = new System.Drawing.Size(1329, 272);
            //this.ctlNhiemVu1.TabIndex = 0;
            // 
            // xtraTabPage8
            // 
            this.xtraTabPage8.Name = "xtraTabPage8";
            this.xtraTabPage8.PageVisible = false;
            this.xtraTabPage8.Size = new System.Drawing.Size(1329, 272);
            this.xtraTabPage8.Text = "Chiến dịch";
            //// 
            //// xtraTabPage9
            //// 
            //this.xtraTabPage9.Controls.Add(this.ctlContractSale1);
            //this.xtraTabPage9.Name = "xtraTabPage9";
            //this.xtraTabPage9.Size = new System.Drawing.Size(1329, 272);
            //this.xtraTabPage9.Text = "Hợp đồng";
            //// 
            //// ctlContractSale1
            //// 
            //this.ctlContractSale1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ctlContractSale1.frm = null;
            //this.ctlContractSale1.Location = new System.Drawing.Point(0, 0);
            //this.ctlContractSale1.MaDA = null;
            //this.ctlContractSale1.MaKH = null;
            //this.ctlContractSale1.Name = "ctlContractSale1";
            //this.ctlContractSale1.Size = new System.Drawing.Size(1329, 272);
            //this.ctlContractSale1.TabIndex = 0;
            // 
            // xtraTabPage10
            // 
            this.xtraTabPage10.Name = "xtraTabPage10";
            this.xtraTabPage10.PageVisible = false;
            this.xtraTabPage10.Size = new System.Drawing.Size(1329, 272);
            this.xtraTabPage10.Text = "Báo giá";
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "Nội dung xử lý";
            this.gridColumn45.FieldName = "NoiDung";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 24;
            this.gridColumn45.Width = 99;
            // 
            // frmManager
            // 
            this.ClientSize = new System.Drawing.Size(1335, 622);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManager";
            this.Text = "Danh sách khách hàng";
            this.Load += new System.EventHandler(this.ctlManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCongTy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumberPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvKH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNgheNghiep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlpEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCallPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpTinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkXaEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkHuyenEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage6.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
            this.xtraTabPage9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private GridColumn gridColumn11;
        private GridColumn gridColumn9;
        private GridColumn gridColumn17;
        private GridColumn gridColumn24;
        private GridColumn gridColumn38;
        private GridColumn gridColumn37;
        private GridColumn gridColumn25;
        private GridColumn gridColumn26;
        private GridColumn gridColumn28;
        private GridColumn gridColumn29;
        private GridColumn gridColumn31;
        private GridColumn gridColumn32;
        private GridColumn gridColumn33;
        private GridColumn gridColumn34;
        private GridColumn gridColumn35;
        private GridColumn gridColumn36;
        private GridColumn gridColumn39;
        private BarButtonItem barButtonItem1;
        private GridColumn gridColumn30;
        private BarSubItem barSubItem1;
        private BarButtonItem itemDSKHCN;
        private BarButtonItem itemDSKHDN;
        private BarButtonItem itemChange;
        private GridColumn IsChinhThuc;
        private GridColumn gridColumn40;
        private BarButtonItem itemThemLichHen;
        private BarButtonItem itemSuaLichHen;
        private BarButtonItem itemXoaLichHen;
        private GridColumn gridColumn41;
        private RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private RepositoryItemCheckedComboBoxEdit cmbNgheNghiep;
        private BarSubItem barSubItem2;
        private BarButtonItem ItemChuyenDangChamSoc;
        private BarButtonItem itemConvert;
        private BarSubItem barSubItem3;
        private BarButtonItem itemRemoveConvert;
        private ImageCollection imageCollection1;
        private GridColumn gridColumn42;
        private GridColumn gridColumn43;
        private Library.Controls.LichHen.ctlLichHen ctlLichHen1;
        private GridColumn gridColumn44;
        private GridColumn gridColumn45;
    }
}