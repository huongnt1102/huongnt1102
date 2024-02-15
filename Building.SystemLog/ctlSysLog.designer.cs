using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using System.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using System;
namespace Building.SystemLog
{
    partial class ctlSysLog
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlSysLog));
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gbList = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSYS_ID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUserName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMChine = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAccountWin = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCreated = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colModule = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAction = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAction_Name = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReference = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIPLan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIPWan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMac = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDeviceName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDescription = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colActive = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rpType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rpGroup = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rpVal = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.rpDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.rpgProduct = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pcc = new DevExpress.XtraBars.PopupControlContainer();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.bm = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection();
            this.bbiPrint = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiHelp = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.bbiOtherView = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClear = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.lblFrom = new DevExpress.XtraEditors.LabelControl();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.pm = new DevExpress.XtraBars.PopupMenu();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lb02 = new DevExpress.XtraEditors.LabelControl();
            this.lb01 = new DevExpress.XtraEditors.LabelControl();
            this.Todate = new DevExpress.XtraEditors.DateEdit();
            this.Fromdate = new DevExpress.XtraEditors.DateEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpgProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcc)).BeginInit();
            this.pcc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Todate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Todate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fromdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fromdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(2, 2);
            this.gcList.MainView = this.gbList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpType,
            this.rpGroup,
            this.rpVal,
            this.rpDate,
            this.rpgProduct});
            this.gcList.Size = new System.Drawing.Size(1005, 475);
            this.gcList.TabIndex = 15;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gbList});
            // 
            // gbList
            // 
            this.gbList.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8.75F, System.Drawing.FontStyle.Bold);
            this.gbList.Appearance.GroupRow.ForeColor = System.Drawing.Color.Red;
            this.gbList.Appearance.GroupRow.Options.UseFont = true;
            this.gbList.Appearance.GroupRow.Options.UseForeColor = true;
            this.gbList.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.gbList.AppearancePrint.FilterPanel.Options.UseTextOptions = true;
            this.gbList.AppearancePrint.FilterPanel.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
            this.gbList.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.gbList.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSYS_ID,
            this.colMChine,
            this.colCreated,
            this.colModule,
            this.colAction,
            this.colAction_Name,
            this.colReference,
            this.colDescription,
            this.colActive,
            this.colUserName,
            this.colAccountWin,
            this.colIPLan,
            this.colIPWan,
            this.colMac,
            this.colDeviceName});
            this.gbList.CustomizationFormBounds = new System.Drawing.Rectangle(1048, 353, 223, 236);
            this.gbList.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gbList.GridControl = this.gcList;
            this.gbList.GroupCount = 2;
            this.gbList.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", null, "")});
            this.gbList.Name = "gbList";
            this.gbList.OptionsBehavior.AutoExpandAllGroups = true;
            this.gbList.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gbList.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            this.gbList.OptionsFilter.UseNewCustomFilterDialog = true;
            this.gbList.OptionsPrint.ExpandAllDetails = true;
            this.gbList.OptionsPrint.PrintDetails = true;
            this.gbList.OptionsPrint.PrintFilterInfo = true;
            this.gbList.OptionsSelection.InvertSelection = true;
            this.gbList.OptionsSelection.MultiSelect = true;
            this.gbList.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateFocusedItem;
            this.gbList.OptionsView.ShowAutoFilterRow = true;
            this.gbList.OptionsView.ShowBands = false;
            this.gbList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gbList.OptionsView.ShowDetailButtons = false;
            this.gbList.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMChine, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colUserName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gbList.ViewCaption = "Danh sách Nhân viên";
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colSYS_ID);
            this.gridBand1.Columns.Add(this.colUserName);
            this.gridBand1.Columns.Add(this.colMChine);
            this.gridBand1.Columns.Add(this.colAccountWin);
            this.gridBand1.Columns.Add(this.colCreated);
            this.gridBand1.Columns.Add(this.colModule);
            this.gridBand1.Columns.Add(this.colAction);
            this.gridBand1.Columns.Add(this.colAction_Name);
            this.gridBand1.Columns.Add(this.colReference);
            this.gridBand1.Columns.Add(this.colIPLan);
            this.gridBand1.Columns.Add(this.colIPWan);
            this.gridBand1.Columns.Add(this.colMac);
            this.gridBand1.Columns.Add(this.colDeviceName);
            this.gridBand1.Columns.Add(this.colDescription);
            this.gridBand1.Columns.Add(this.colActive);
            this.gridBand1.MinWidth = 20;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.OptionsBand.ShowCaption = false;
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 1350;
            // 
            // colSYS_ID
            // 
            this.colSYS_ID.Caption = "ID";
            this.colSYS_ID.FieldName = "SYS_ID";
            this.colSYS_ID.Name = "colSYS_ID";
            this.colSYS_ID.OptionsColumn.ReadOnly = true;
            this.colSYS_ID.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colSYS_ID.Width = 80;
            // 
            // colUserName
            // 
            this.colUserName.Caption = "Người Dùng";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colUserName.Visible = true;
            this.colUserName.Width = 103;
            // 
            // colMChine
            // 
            this.colMChine.Caption = "Máy Tính";
            this.colMChine.FieldName = "MChine";
            this.colMChine.Name = "colMChine";
            this.colMChine.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colMChine.Visible = true;
            this.colMChine.Width = 113;
            // 
            // colAccountWin
            // 
            this.colAccountWin.Caption = "Tài Khoản";
            this.colAccountWin.FieldName = "AccountWin";
            this.colAccountWin.Name = "colAccountWin";
            this.colAccountWin.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAccountWin.Visible = true;
            this.colAccountWin.Width = 112;
            // 
            // colCreated
            // 
            this.colCreated.Caption = "Thời Gian";
            this.colCreated.DisplayFormat.FormatString = "{0:dd/MM/yyyy hh:mm:ss}";
            this.colCreated.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colCreated.FieldName = "Created";
            this.colCreated.Name = "colCreated";
            this.colCreated.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colCreated.Visible = true;
            this.colCreated.Width = 150;
            // 
            // colModule
            // 
            this.colModule.Caption = "Chức Năng";
            this.colModule.FieldName = "Module";
            this.colModule.Name = "colModule";
            this.colModule.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colModule.Visible = true;
            this.colModule.Width = 100;
            // 
            // colAction
            // 
            this.colAction.Caption = "Tác Vụ";
            this.colAction.FieldName = "Action";
            this.colAction.Name = "colAction";
            this.colAction.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAction.Width = 80;
            // 
            // colAction_Name
            // 
            this.colAction_Name.Caption = "Hành Động";
            this.colAction_Name.FieldName = "Action_Name";
            this.colAction_Name.Name = "colAction_Name";
            this.colAction_Name.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAction_Name.Visible = true;
            this.colAction_Name.Width = 80;
            // 
            // colReference
            // 
            this.colReference.AutoFillDown = true;
            this.colReference.Caption = "Đối Tượng";
            this.colReference.FieldName = "Reference";
            this.colReference.Name = "colReference";
            this.colReference.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colReference.ToolTip = "Đối Tượng Chịu Tác Động Tron Hành Động Này";
            this.colReference.Visible = true;
            this.colReference.Width = 152;
            // 
            // colIPLan
            // 
            this.colIPLan.Caption = "IP LAN";
            this.colIPLan.FieldName = "IPLan";
            this.colIPLan.Name = "colIPLan";
            this.colIPLan.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colIPLan.Visible = true;
            this.colIPLan.Width = 90;
            // 
            // colIPWan
            // 
            this.colIPWan.Caption = "IP WAN";
            this.colIPWan.FieldName = "IPWan";
            this.colIPWan.Name = "colIPWan";
            this.colIPWan.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colIPWan.Width = 90;
            // 
            // colMac
            // 
            this.colMac.Caption = "MAC";
            this.colMac.FieldName = "Mac";
            this.colMac.Name = "colMac";
            this.colMac.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colMac.Visible = true;
            this.colMac.Width = 150;
            // 
            // colDeviceName
            // 
            this.colDeviceName.FieldName = "DeviceName";
            this.colDeviceName.Name = "colDeviceName";
            this.colDeviceName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDeviceName.Visible = true;
            this.colDeviceName.Width = 300;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Ghi Chú";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colActive
            // 
            this.colActive.Caption = "Kích Hoạt";
            this.colActive.FieldName = "Active";
            this.colActive.Name = "colActive";
            this.colActive.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rpType
            // 
            this.rpType.AutoHeight = false;
            this.rpType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpType.Name = "rpType";
            // 
            // rpGroup
            // 
            this.rpGroup.AutoHeight = false;
            this.rpGroup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpGroup.DisplayMember = "ProductGroup_ID";
            this.rpGroup.Name = "rpGroup";
            this.rpGroup.PopupView = this.repositoryItemGridLookUpEdit1View;
            this.rpGroup.ValueMember = "ProductGroup_ID";
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // rpVal
            // 
            this.rpVal.AutoHeight = false;
            this.rpVal.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpVal.DisplayFormat.FormatString = "{0:##,##0.###}";
            this.rpVal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.rpVal.EditFormat.FormatString = "{0:##,##0.###}";
            this.rpVal.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.rpVal.Name = "rpVal";
            // 
            // rpDate
            // 
            this.rpDate.AutoHeight = false;
            this.rpDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rpDate.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.rpDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.rpDate.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.rpDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.rpDate.Name = "rpDate";
            // 
            // rpgProduct
            // 
            this.rpgProduct.AutoComplete = false;
            this.rpgProduct.AutoHeight = false;
            this.rpgProduct.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpgProduct.DisplayMember = "Product_Name";
            this.rpgProduct.Name = "rpgProduct";
            this.rpgProduct.NullText = "(Chọn)";
            this.rpgProduct.PopupView = this.gridView3;
            this.rpgProduct.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rpgProduct.ValueMember = "Product_ID";
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // pcc
            // 
            this.pcc.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.pcc.Appearance.Options.UseBackColor = true;
            this.pcc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcc.Controls.Add(this.btnCancel);
            this.pcc.Controls.Add(this.dtFrom);
            this.pcc.Controls.Add(this.btnStart);
            this.pcc.Controls.Add(this.lblFrom);
            this.pcc.Controls.Add(this.dtTo);
            this.pcc.Controls.Add(this.lblTo);
            this.pcc.Location = new System.Drawing.Point(103, 163);
            this.pcc.Manager = this.bm;
            this.pcc.Name = "pcc";
            this.pcc.Padding = new System.Windows.Forms.Padding(4);
            this.pcc.Size = new System.Drawing.Size(190, 95);
            this.pcc.TabIndex = 17;
            this.pcc.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(102, 63);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 241;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = new System.DateTime(2009, 7, 25, 22, 57, 46, 531);
            this.dtFrom.Location = new System.Drawing.Point(59, 7);
            this.dtFrom.MenuManager = this.bm;
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtFrom.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dtFrom.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dtFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtFrom.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dtFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtFrom.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.dtFrom.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.dtFrom.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtFrom.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dtFrom.Size = new System.Drawing.Size(123, 20);
            this.dtFrom.TabIndex = 238;
            // 
            // bm
            // 
            this.bm.AutoSaveInRegistry = true;
            this.bm.DockControls.Add(this.barDockControlTop);
            this.bm.DockControls.Add(this.barDockControlBottom);
            this.bm.DockControls.Add(this.barDockControlLeft);
            this.bm.DockControls.Add(this.barDockControlRight);
            this.bm.Form = this;
            this.bm.Images = this.imageCollection2;
            this.bm.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiPrint,
            this.bbiRefresh,
            this.bbiExport,
            this.bbiHelp,
            this.bbiClose,
            this.bbiOtherView,
            this.bbiDelete,
            this.bbiClear,
            this.bbiSave,
            this.bbiOpen});
            this.bm.MaxItemId = 29;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.bm;
            this.barDockControlTop.Size = new System.Drawing.Size(1009, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 510);
            this.barDockControlBottom.Manager = this.bm;
            this.barDockControlBottom.Size = new System.Drawing.Size(1009, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.bm;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 510);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1009, 0);
            this.barDockControlRight.Manager = this.bm;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 510);
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.Images.SetKeyName(0, "icons8_view.png");
            this.imageCollection2.Images.SetKeyName(1, "icons8_delete1.png");
            this.imageCollection2.Images.SetKeyName(2, "icons8_save.png");
            this.imageCollection2.Images.SetKeyName(3, "icons8_thuhoi.png");
            this.imageCollection2.Images.SetKeyName(4, "icons8_export.png");
            this.imageCollection2.Images.SetKeyName(5, "icons8_cancel1.png");
            // 
            // bbiPrint
            // 
            this.bbiPrint.Caption = "In";
            this.bbiPrint.Id = 3;
            this.bbiPrint.ImageOptions.ImageIndex = 9;
            this.bbiPrint.Name = "bbiPrint";
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "Xem";
            this.bbiRefresh.Id = 4;
            this.bbiRefresh.ImageOptions.ImageIndex = 35;
            this.bbiRefresh.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bbiRefresh.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // bbiExport
            // 
            this.bbiExport.Caption = "Xuất";
            this.bbiExport.Id = 5;
            this.bbiExport.ImageOptions.ImageIndex = 37;
            this.bbiExport.ImageOptions.LargeImageIndex = 19;
            this.bbiExport.Name = "bbiExport";
            this.bbiExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExport_ItemClick);
            // 
            // bbiHelp
            // 
            this.bbiHelp.Caption = "Trợ Giúp";
            this.bbiHelp.Id = 6;
            this.bbiHelp.ImageOptions.ImageIndex = 16;
            this.bbiHelp.Name = "bbiHelp";
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "Đóng";
            this.bbiClose.Id = 10;
            this.bbiClose.ImageOptions.ImageIndex = 13;
            this.bbiClose.ImageOptions.LargeImageIndex = 1;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // bbiOtherView
            // 
            this.bbiOtherView.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.bbiOtherView.Caption = "Xem Khác...";
            this.bbiOtherView.DropDownControl = this.pcc;
            this.bbiOtherView.Id = 24;
            this.bbiOtherView.Name = "bbiOtherView";
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "Xoá";
            this.bbiDelete.Id = 25;
            this.bbiDelete.ImageOptions.ImageIndex = 4;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // bbiClear
            // 
            this.bbiClear.Caption = "Dọn Sạch";
            this.bbiClear.Id = 26;
            this.bbiClear.ImageOptions.ImageIndex = 38;
            this.bbiClear.Name = "bbiClear";
            this.bbiClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClear_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "Lưu Nhật Ký";
            this.bbiSave.Id = 27;
            this.bbiSave.ImageOptions.ImageIndex = 1;
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiOpen
            // 
            this.bbiOpen.Caption = "Nạp Nhật Ký Từ Tập Tin...";
            this.bbiOpen.Id = 28;
            this.bbiOpen.ImageOptions.ImageIndex = 2;
            this.bbiOpen.Name = "bbiOpen";
            this.bbiOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiOpen_ItemClick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(16, 63);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 25);
            this.btnStart.TabIndex = 240;
            this.btnStart.Text = "Thực Hiện";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(4, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(40, 13);
            this.lblFrom.TabIndex = 236;
            this.lblFrom.Text = "Từ ngày";
            // 
            // dtTo
            // 
            this.dtTo.EditValue = new System.DateTime(2009, 7, 25, 22, 57, 46, 531);
            this.dtTo.Location = new System.Drawing.Point(59, 33);
            this.dtTo.MenuManager = this.bm;
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtTo.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dtTo.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dtTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtTo.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dtTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtTo.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.dtTo.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.dtTo.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtTo.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dtTo.Size = new System.Drawing.Size(123, 20);
            this.dtTo.TabIndex = 239;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(5, 37);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(48, 13);
            this.lblTo.TabIndex = 237;
            this.lblTo.Text = "Đến Ngày";
            // 
            // pm
            // 
            this.pm.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiOtherView),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClear),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiExport, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiHelp, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClose, true)});
            this.pm.Manager = this.bm;
            this.pm.Name = "pm";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lb02);
            this.panelControl1.Controls.Add(this.lb01);
            this.panelControl1.Controls.Add(this.Todate);
            this.panelControl1.Controls.Add(this.Fromdate);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.btnOpen);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnView);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1009, 31);
            this.panelControl1.TabIndex = 14;
            // 
            // lb02
            // 
            this.lb02.Location = new System.Drawing.Point(159, 9);
            this.lb02.Name = "lb02";
            this.lb02.Size = new System.Drawing.Size(48, 13);
            this.lb02.TabIndex = 10;
            this.lb02.Text = "Đến Ngày";
            // 
            // lb01
            // 
            this.lb01.Location = new System.Drawing.Point(6, 9);
            this.lb01.Name = "lb01";
            this.lb01.Size = new System.Drawing.Size(41, 13);
            this.lb01.TabIndex = 9;
            this.lb01.Text = "Từ Ngày";
            // 
            // Todate
            // 
            this.Todate.EditValue = null;
            this.Todate.Location = new System.Drawing.Point(213, 6);
            this.Todate.MenuManager = this.bm;
            this.Todate.Name = "Todate";
            this.Todate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Todate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Todate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.Todate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Todate.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.Todate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Todate.Size = new System.Drawing.Size(100, 20);
            this.Todate.TabIndex = 8;
            // 
            // Fromdate
            // 
            this.Fromdate.EditValue = null;
            this.Fromdate.Location = new System.Drawing.Point(53, 6);
            this.Fromdate.MenuManager = this.bm;
            this.Fromdate.Name = "Fromdate";
            this.Fromdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Fromdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Fromdate.Size = new System.Drawing.Size(100, 20);
            this.Fromdate.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.ImageIndex = 5;
            this.btnClose.ImageOptions.ImageList = this.imageCollection2;
            this.btnClose.Location = new System.Drawing.Point(779, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.ImageOptions.ImageIndex = 4;
            this.btnExport.ImageOptions.ImageList = this.imageCollection2;
            this.btnExport.Location = new System.Drawing.Point(700, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Xuất";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click_1);
            // 
            // btnOpen
            // 
            this.btnOpen.ImageOptions.ImageIndex = 3;
            this.btnOpen.ImageOptions.ImageList = this.imageCollection2;
            this.btnOpen.Location = new System.Drawing.Point(587, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(109, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Đọc Từ Tập Tin";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageIndex = 2;
            this.btnSave.ImageOptions.ImageList = this.imageCollection2;
            this.btnSave.Location = new System.Drawing.Point(480, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu vào tập tin";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.ImageIndex = 1;
            this.btnDelete.ImageOptions.ImageList = this.imageCollection2;
            this.btnDelete.Location = new System.Drawing.Point(401, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnView
            // 
            this.btnView.ImageOptions.ImageIndex = 0;
            this.btnView.ImageOptions.ImageList = this.imageCollection2;
            this.btnView.Location = new System.Drawing.Point(322, 4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "Xem";
            this.btnView.Click += new System.EventHandler(this.btnView_Click_1);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.gcList);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 31);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1009, 479);
            this.panelControl4.TabIndex = 18;
            // 
            // ctlSysLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 510);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.pcc);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlSysLog";
            this.Text = "Nhật ký hệ thống";
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpgProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcc)).EndInit();
            this.pcc.ResumeLayout(false);
            this.pcc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Todate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Todate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fromdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fromdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarButtonItem bbiClear;
        private BarButtonItem bbiClose;
        private BarButtonItem bbiDelete;
        private BarButtonItem bbiExport;
        private BarButtonItem bbiHelp;
        private BarButtonItem bbiHistory;
        private BarButtonItem bbiImport;
        private BarButtonItem bbiOpen;
        private BarButtonItem bbiOtherView;
        private BarButtonItem bbiPrint;
        private BarSubItem bbiProduct;
        private BarButtonItem bbiProductDetail;
        private BarButtonItem bbiRefresh;
        private BarButtonItem bbiSave;
        private BarManager bm;
        private SimpleButton btnCancel;
        private SimpleButton btnClose;
        private SimpleButton btnDelete;
        private SimpleButton btnExport;
        private SimpleButton btnOpen;
        private SimpleButton btnSave;
        private SimpleButton btnStart;
        private SimpleButton btnView;
        private BandedGridColumn colAccountWin;
        private BandedGridColumn colAction;
        private BandedGridColumn colAction_Name;
        private BandedGridColumn colActive;
        private BandedGridColumn colCreated;
        private BandedGridColumn colDescription;
        private BandedGridColumn colDeviceName;
        private BandedGridColumn colIPLan;
        private BandedGridColumn colIPWan;
        private BandedGridColumn colMac;
        private BandedGridColumn colMChine;
        private BandedGridColumn colModule;
        private BandedGridColumn colReference;
        private BandedGridColumn colSYS_ID;
        private BandedGridColumn colUserName;
       // private dsSysLog dsSysLog;
        protected DateEdit dtFrom;
        protected DateEdit dtTo;
        private DateEdit Fromdate;
        protected AdvBandedGridView gbList;
        protected GridControl gcList;
        protected GridBand gridBand1;
        private GridView gridView3;
        private ImageCollection imageCollection2;
        private LabelControl lb01;
        private LabelControl lb02;
        protected LabelControl lblFrom;
        protected LabelControl lblTo;
        private PanelControl panelControl1;
        private PanelControl panelControl4;
        private PopupControlContainer pcc;
        private PopupMenu pm;
        private GridView repositoryItemGridLookUpEdit1View;
        private RepositoryItemDateEdit rpDate;
        private RepositoryItemGridLookUpEdit rpgProduct;
        private RepositoryItemGridLookUpEdit rpGroup;
        private RepositoryItemComboBox rpType;
        private RepositoryItemCalcEdit rpVal;
       // private SYS_LOGTableAdapter sYS_LOGTableAdapter;
        private BindingSource sYSLOGBindingSource;
        private DateEdit Todate;

        public BarButtonItem BbiHistory { get => BbiHistory1; set => BbiHistory1 = value; }
        public BarButtonItem BbiHistory1 { get => bbiHistory; set => bbiHistory = value; }
        public BarButtonItem BbiImport { get => bbiImport; set => bbiImport = value; }
        public BarSubItem BbiProduct { get => bbiProduct; set => bbiProduct = value; }
        public BarButtonItem BbiProductDetail { get => bbiProductDetail; set => bbiProductDetail = value; }
        public BindingSource SYSLOGBindingSource { get => sYSLOGBindingSource; set => sYSLOGBindingSource = value; }
        #endregion
    }
}
