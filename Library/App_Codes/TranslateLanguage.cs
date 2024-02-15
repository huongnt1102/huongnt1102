using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace Library
{
    public class TranslateLanguage
    {
        private static List<System.Windows.Forms.Control> ListControl;

        public static void TranslateControl(DevExpress.XtraEditors.XtraForm form, DevExpress.XtraBars.BarManager InputBarManager = null, DevExpress.XtraBars.Ribbon.RibbonControl ribbon = null)
        {
            try
            {
                var NgonNgu = global::Library.Properties.Settings.Default.NgonNgu;

                using (MasterDataContext db = new MasterDataContext())
                {
                    var lData = db.Translates.ToList();
                    switch (NgonNgu)
                    {
                        case "EN":
                            try
                            {
                                if (form.Name.ToUpper() != "frmMain".ToUpper())
                                { 
                                    var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == form.Text.ToUpper()); 
                                    if (ngon_ngu != null) form.Text = ngon_ngu.TiengAnh; 
                                }
                            }
                            catch { }

                            ListControl = new List<System.Windows.Forms.Control>();

                            foreach (var item in form.Controls)
                            {
                                GetAllControls(item);
                            }

                            foreach (var item in ListControl)
                            {
                                ChangeControls(item, lData);
                            }

                            #region BarManager
                            if (InputBarManager != null)
                            {
                                //BarButtonItem
                                foreach (var item in InputBarManager.Items)
                                {
                                    if (item is DevExpress.XtraBars.BarButtonItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarButtonItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }

                                    //barsubitem
                                    if (item is DevExpress.XtraBars.BarSubItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarSubItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }
                                }
                            }
                            #endregion

                            #region Ribbon
                            if (ribbon != null)
                            {
                                //BarButtonItem
                                foreach (var item in (ribbon as DevExpress.XtraBars.Ribbon.RibbonControl).Items)
                                {
                                    if (item is DevExpress.XtraBars.BarButtonItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarButtonItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }

                                    //barsubitem
                                    if (item is DevExpress.XtraBars.BarSubItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarSubItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }
                                }
                                //page
                                foreach (var page in ribbon.Pages)
                                {
                                    if (page is DevExpress.XtraBars.Ribbon.RibbonPage)
                                    {
                                        var name = page as DevExpress.XtraBars.Ribbon.RibbonPage;
                                        try
                                        {
                                            name.Text = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper()).TiengAnh.ToUpper();
                                        }
                                        catch { }
                                    }
                                }

                                //PageGroup
                                foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbon.Pages)
                                {
                                    foreach (var pageGroup in page.Groups)
                                    {
                                        if (pageGroup is DevExpress.XtraBars.Ribbon.RibbonPageGroup)
                                        {
                                            var name = pageGroup as DevExpress.XtraBars.Ribbon.RibbonPageGroup;
                                            try
                                            {
                                                name.Text = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper()).TiengAnh;
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                            #endregion

                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            {
                //DialogBox.Error("Không kết nối được. Vui lòng thiết lập chuỗi kết nối");  
                return;
            }
        }

        public static void TranslateUserControl(DevExpress.XtraEditors.XtraUserControl form, DevExpress.XtraBars.BarManager InputBarManager = null, DevExpress.XtraBars.Ribbon.RibbonControl ribbon = null)
        {
            try
            {
                var NgonNgu = global::Library.Properties.Settings.Default.NgonNgu;

                using (MasterDataContext db = new MasterDataContext())
                {
                    var lData = db.Translates.ToList();
                    switch (NgonNgu)
                    {
                        case "EN":
                            try
                            {
                                if (form.Name.ToUpper() != "frmMain".ToUpper())
                                    form.Text = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == form.Text.ToUpper()).TiengAnh;
                            }
                            catch { }

                            ListControl = new List<System.Windows.Forms.Control>();

                            foreach (var item in form.Controls)
                            {
                                GetAllControls(item);
                            }

                            foreach (var item in ListControl)
                            {
                                ChangeControls(item, lData);
                            }

                            #region BarManager
                            if (InputBarManager != null)
                            {
                                //BarButtonItem
                                foreach (var item in InputBarManager.Items)
                                {
                                    if (item is DevExpress.XtraBars.BarButtonItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarButtonItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }

                                    //barsubitem
                                    if (item is DevExpress.XtraBars.BarSubItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarSubItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }
                                }
                            }
                            #endregion

                            #region Ribbon
                            if (ribbon != null)
                            {
                                //BarButtonItem
                                foreach (var item in (ribbon as DevExpress.XtraBars.Ribbon.RibbonControl).Items)
                                {
                                    if (item is DevExpress.XtraBars.BarButtonItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarButtonItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }

                                    //barsubitem
                                    if (item is DevExpress.XtraBars.BarSubItem)
                                    {
                                        var name = item as DevExpress.XtraBars.BarSubItem;
                                        try
                                        {
                                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                                        }
                                        catch { }
                                    }
                                }
                                //page
                                foreach (var page in ribbon.Pages)
                                {
                                    if (page is DevExpress.XtraBars.Ribbon.RibbonPage)
                                    {
                                        var name = page as DevExpress.XtraBars.Ribbon.RibbonPage;
                                        try
                                        {
                                            name.Text = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper()).TiengAnh.ToUpper();
                                        }
                                        catch { }
                                    }
                                }

                                //PageGroup
                                foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbon.Pages)
                                {
                                    foreach (var pageGroup in page.Groups)
                                    {
                                        if (pageGroup is DevExpress.XtraBars.Ribbon.RibbonPageGroup)
                                        {
                                            var name = pageGroup as DevExpress.XtraBars.Ribbon.RibbonPageGroup;
                                            try
                                            {
                                                name.Text = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper()).TiengAnh;
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                            #endregion

                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            {
                //DialogBox.Error("Không kết nối được. Vui lòng thiết lập chuỗi kết nối");  
                return;
            }
        }

        private static void ChangeControls(object item, List<Translate> lData)
        {
            #region Common Control
            //label
            if (item is DevExpress.XtraEditors.LabelControl)
            {
                var name = item as DevExpress.XtraEditors.LabelControl;
                try
                {
                    int length = name.Text.Trim().Length;
                    if (name.Text.Trim().Substring(length - 1, 1) == ":")
                    {
                        name.Text = name.Text.Trim().Substring(0, length - 1);
                        name.Text = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper()).TiengAnh + ":";
                    }
                    else
                    {
                        var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper());
                        if (ngon_ngu != null) name.Text = ngon_ngu.TiengAnh;
                    }
                }
                catch {  }
            }

            //button
            if (item is DevExpress.XtraEditors.SimpleButton)
            {
                var name = item as DevExpress.XtraEditors.SimpleButton;
                try
                {
                    var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper());
                    if (ngon_ngu != null) name.Text = ngon_ngu.TiengAnh;
                }
                catch { }
            }

            //checkEdit
            if (item is DevExpress.XtraEditors.CheckEdit)
            {
                var name = item as DevExpress.XtraEditors.CheckEdit;
                try
                {
                    var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper());
                    if (ngon_ngu != null) name.Text = ngon_ngu.TiengAnh;
                }
                catch { }
            }

            //tab
            if (item is DevExpress.XtraTab.XtraTabPage)
            {
                var name = item as DevExpress.XtraTab.XtraTabPage;
                try
                {
                    var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper());
                    if (ngon_ngu != null) name.Text = ngon_ngu.TiengAnh;
                }
                catch { }
            }

            //groupcontrol
            if (item is DevExpress.XtraEditors.GroupControl)
            {
                var name = item as DevExpress.XtraEditors.GroupControl;
                try
                {
                    var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper());
                    if (ngon_ngu != null) name.Text = ngon_ngu.TiengAnh;
                }
                catch { }
            }

            //groupbox
            if (item is System.Windows.Forms.GroupBox)
            {
                var name = item as System.Windows.Forms.GroupBox;
                try
                {
                    var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Text.Trim().ToUpper());
                    if (ngon_ngu != null) name.Text = ngon_ngu.TiengAnh;
                }
                catch { }
            }

            //radio
            if (item is DevExpress.XtraEditors.RadioGroup)
            {
                var rdb = item as DevExpress.XtraEditors.RadioGroup;
                foreach (var radio in rdb.Properties.Items)
                {
                    var name = radio as DevExpress.XtraEditors.Controls.RadioGroupItem;

                    try
                    {
                        var ngon_ngu = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Description.Trim().ToUpper());
                        if (ngon_ngu != null) name.Description = ngon_ngu.TiengAnh;
                    }
                    catch { }
                }
            }

            //GridControl
            if (item is DevExpress.XtraGrid.GridControl)
            {
                var grid = item as DevExpress.XtraGrid.GridControl;
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((DevExpress.XtraGrid.Views.Base.ColumnView)grid.Views[0]).Columns)
                {
                    try
                    {
                        col.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == col.Caption.Trim().ToUpper()).TiengAnh;
                    }
                    catch
                    { }
                }
            }

            //VGridControl
            if (item is DevExpress.XtraVerticalGrid.VGridControl)
            {
                var grid = item as DevExpress.XtraVerticalGrid.VGridControl;
                foreach (DevExpress.XtraVerticalGrid.Rows.EditorRow col in ((DevExpress.XtraVerticalGrid.Rows.VGridRows)grid.Rows))
                {

                    try
                    {
                        col.Properties.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == col.Properties.Caption.Trim().ToUpper()).TiengAnh;
                    }
                    catch
                    { }
                }
            }

            //nav
            if (item is DevExpress.XtraNavBar.NavBarControl)
            {
                var navControl = item as DevExpress.XtraNavBar.NavBarControl;
                foreach (var nav in navControl.Groups)
                {
                    if (nav is DevExpress.XtraNavBar.NavBarGroup)
                    {
                        var name = nav as DevExpress.XtraNavBar.NavBarGroup;
                        try
                        {
                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TiengAnh;
                        }
                        catch { }
                    }
                }

                foreach (var nav in navControl.Items)
                {
                    if (nav is DevExpress.XtraNavBar.NavBarItem)
                    {
                        var name = nav as DevExpress.XtraNavBar.NavBarItem;
                        try
                        {
                            name.Caption = lData.FirstOrDefault(p => p.TiengViet.Trim().ToLower() == name.Caption.Trim().ToLower()).TiengAnh;
                        }
                        catch { }
                    }
                }
            }
            
            
            #endregion
        }

        private static void GetAllControls(object control)
        {
            ListControl.Add((System.Windows.Forms.Control)control);
            foreach (System.Windows.Forms.Control ctl in ((System.Windows.Forms.Control)control).Controls)
            {
                ListControl.Add(ctl);
                GetAllControls(ctl);
            }
        }
    }
}
