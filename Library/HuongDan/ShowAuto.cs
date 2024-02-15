using System.IO;
using System.Linq;

namespace Library.Class.HuongDan
{
    public static class ShowAuto
    {
        public static bool IsActive;

        private static DevExpress.ActiveDemos.ActiveDemo activeDemo;

        private static Library.MasterDataContext masterDataContext = new Library.MasterDataContext();

        public static string[] types = { "XtraTabControl", "LabelControl", "SimpleButton", "Button", "XtraTabPage", "VTLScrollBar", "HTLScrollBar", "DPILabel", "FakeFocusContainer", "BarDockControl", "PanelControl"  };
        public static string[] typesNoAdd = { "SplitContainerControl", "LayoutControl", "SplitGroupPanel","GroupControl", "GroupBox", "SimpleButton", "Button" };
        public static string[] typesClear = { "TextEdit", "MemoEdit", "TextBox" };

        public static void ActiveDemo(bool isAction, System.Collections.Generic.List<ControlItem> controls)
        {
            IsActive = true;
            if (isAction)
            {
                #region active tự động
                activeDemo = new DevExpress.ActiveDemos.ActiveDemo();
                var point = new System.Drawing.Point();
                if (controls == null) return;
                point = new System.Drawing.Point(10, controls[0].control.Height / 2);
                activeDemo.ShowMessage("Bắt đầu thêm thông tin.");
                foreach (var item in controls)
                {
                    if (IsActive == true)
                    {
                        point = new System.Drawing.Point(10, item.control.Height / 2);

                        activeDemo.Actions.MoveMousePointTo(item.control, point);
                        activeDemo.Actions.MouseClick(item.control, point);
                        activeDemo.ShowMessage(item.infor);
                        if (item == controls[0]) activeDemo.Actions.Dispose();
                        //activeDemo.Actions.Dispose();
                        DevExpress.ActiveDemos.ActiveActions.Delay(100);

                        switch (item.Loai.Name)
                        {
                            case "TextEdit": System.Windows.Forms.SendKeys.SendWait(item.text); break;
                            case "TextBox": System.Windows.Forms.SendKeys.SendWait(item.text); break;
                            case "MemoEdit": System.Windows.Forms.SendKeys.SendWait(item.text); break;
                            case "SpinEdit": System.Windows.Forms.SendKeys.SendWait("1"); break;
                            case "DateEdit": System.Windows.Forms.SendKeys.Send("{DOWN}"); break;
                            case "CheckedComboBoxEdit":
                                System.Windows.Forms.SendKeys.Send("{PGDN}");
                                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                                System.Windows.Forms.SendKeys.Send("{ENTER}"); 
                                break;
                            case "LookUpEdit":
                                System.Windows.Forms.SendKeys.Send("{DOWN}");
                                System.Windows.Forms.SendKeys.Send("{ENTER}");
                                break;
                            case "SearchLookUpEdit":
                                System.Windows.Forms.SendKeys.Send("{DOWN}");
                                System.Windows.Forms.SendKeys.Send("{ENTER}");
                                break;
                            case "GridLookUpEdit":
                                System.Windows.Forms.SendKeys.Send("{PGDN}");
                                System.Windows.Forms.SendKeys.Send("{ENTER}");
                                break;
                            case "GridControl":
                                System.Windows.Forms.SendKeys.Send("{PGDN}");
                                System.Windows.Forms.SendKeys.Send("{ENTER}");
                                break;
                            default:
                                break;
                        }

                        DevExpress.ActiveDemos.ActiveActions.Delay(50);
                    }
                    else return;
                }

                //point = new Point(10, txtMaKHCN.Height / 2);
                //activeDemo.ShowMessage("Bắt đầu thêm thông tin.");
                //activeDemo.Actions.MoveMousePointTo(txtMaKHCN, point);
                //activeDemo.Actions.MouseClick(txtMaKHCN, point);
                //activeDemo.ShowMessage("Nhập mã khách hàng.");
                //activeDemo.Actions.Dispose();
                //System.Windows.Forms.SendKeys.SendWait("KH01");
                //point = new Point(10, txtMaPhu.Height / 2);
                //activeDemo.Actions.MoveMousePointTo(txtMaPhu, point);
                //activeDemo.Actions.MouseClick(txtMaPhu, point);
                //activeDemo.ShowMessage("Nhập mã phụ.");
                //DevExpress.ActiveDemos.ActiveActions.Delay(1000);
                //System.Windows.Forms.SendKeys.SendWait("KH01");
                //DevExpress.ActiveDemos.ActiveActions.Delay(1000);

                //point = new Point(10, lookKhuVuc.Height / 2);
                //activeDemo.Actions.MoveMousePointTo(lookKhuVuc, point);
                //activeDemo.Actions.MouseClick(lookKhuVuc, point);
                //activeDemo.ShowMessage("Chọn khu vực.");
                //DevExpress.ActiveDemos.ActiveActions.Delay(1000);
                //System.Windows.Forms.SendKeys.Send("{DOWN}");
                //System.Windows.Forms.SendKeys.Send("{ENTER}");
                //DevExpress.ActiveDemos.ActiveActions.Delay(1000);
                //System.Windows.Forms.SendKeys.Send("{ESC}");
                #endregion
            }
        }

        #region Get items controls form
        public static System.Collections.Generic.List<ControlItem> GetControlItems(System.Windows.Forms.Control.ControlCollection control)
        {
            System.Collections.Generic.List<ControlItem> controls = new System.Collections.Generic.List<ControlItem>();

            var controls1 = control.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();

            controls = GetItemsControlForm(controls, controls1);

            return controls;
        }

        private static System.Collections.Generic.List<ControlItem> GetItemsControlForm(System.Collections.Generic.List<ControlItem> controls, System.Collections.Generic.List<System.Windows.Forms.Control> controls1)
        {
            foreach (System.Windows.Forms.Control c in controls1)
            {
                System.Type type = c.GetType();
                var index = c.TabIndex;
                var name = c.Name;
                if (!types.Contains(type.Name) & !typesNoAdd.Contains(type.Name)) controls.Add(new ControlItem { control = c, infor = c.Tag != null ? c.Tag.ToString() : "", text = c.Text, Loai = type });
                // Trường hợp riêng
                var controls3 = c.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                switch (type.Name)
                {
                    case "XtraTabControl":
                        controls = GetItemInXtraTabPage(controls, c);
                        break;

                    case "LayoutControl":
                        // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
                        controls = GetItemsControlForm(controls, controls3);
                        break;
                    case "SplitContainerControl":
                        controls = GetItemsControlForm(controls, controls3);
                        break;
                    case "SplitGroupPanel":
                        controls = GetItemsControlForm(controls, controls3);
                        break;
                    case "GroupControl":
                        controls = GetItemsControlForm(controls, controls3);
                        break;
                }
            }
            return controls;
        }

        private static System.Collections.Generic.List<ControlItem> GetItemInXtraTabPage(System.Collections.Generic.List<ControlItem> controls, System.Windows.Forms.Control control)
        {
            var controls2 = control.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
            // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
            var cc = controls2[0];
            if (cc.GetType().Name == "XtraTabPage")
            {
                controls.Add(new ControlItem { control = cc, infor = cc.Tag != null ? cc.Tag.ToString() : "", text = cc.Text, Loai = cc.GetType() });

                var controls3 = cc.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                foreach (var ccc in controls3)
                {
                    var typecc = ccc.GetType();
                    if (!types.Contains(ccc.GetType().Name)) controls.Add(new ControlItem { control = ccc, infor = ccc.Tag != null ? ccc.Tag.ToString() : "", text = ccc.Text, Loai = ccc.GetType() });
                }
            }
            return controls;
        }
        #endregion

        #region Get items controls form with tag auto from table
        public static System.Collections.Generic.List<ControlItem> GetControlItemsAutoTag(System.Windows.Forms.Control.ControlCollection control)
        {
            System.Collections.Generic.List<ControlItem> controls = new System.Collections.Generic.List<ControlItem>();

            var controls1 = control.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();

            controls = GetItemsControlFormAutoTag(controls, controls1).OrderBy(_ => _.index).ToList();

            return controls;
        }

        private static System.Collections.Generic.List<ControlItem> GetItemsControlFormAutoTag(System.Collections.Generic.List<ControlItem> controls, System.Collections.Generic.List<System.Windows.Forms.Control> controls1)
        {
            foreach (System.Windows.Forms.Control c in controls1)
            {
                System.Type type = c.GetType();
                var index = c.TabIndex;
                var name = c.Name;
                var getControl = masterDataContext.hd_ControlTags.FirstOrDefault(_ => _.ControlName.ToLower() == c.Name.ToLower());
                var tag = "";
                var text = "";
                if(getControl!=null)
                {
                    tag = getControl.Tag;
                    text = getControl.Text;
                }
                if (!types.Contains(type.Name) & !typesNoAdd.Contains(type.Name)) controls.Add(new ControlItem { control = c, infor = tag, text = text, Loai = type, index = c.TabIndex });
                // Trường hợp riêng
                var controls3 = c.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                switch (type.Name)
                {
                    case "XtraTabControl":
                        controls = GetItemInXtraTabPageAutoTag(controls, c);
                        break;

                    case "LayoutControl":
                        // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
                        controls = GetItemsControlFormAutoTag(controls, controls3);
                        break;
                    case "SplitContainerControl":
                        controls = GetItemsControlFormAutoTag(controls, controls3);
                        break;
                    case "SplitGroupPanel":
                        controls = GetItemsControlFormAutoTag(controls, controls3);
                        break;
                    case "GroupControl":
                        controls = GetItemsControlFormAutoTag(controls, controls3);
                        break;
                }
            }
            return controls;
        }

        private static System.Collections.Generic.List<ControlItem> GetItemInXtraTabPageAutoTag(System.Collections.Generic.List<ControlItem> controls, System.Windows.Forms.Control control)
        {
            var controls2 = control.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
            // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
            var cc = controls2[0];
            if (cc.GetType().Name == "XtraTabPage")
            {
                //controls.Add(new ControlItem { control = cc, infor = cc.Tag != null ? cc.Tag.ToString() : "", text = cc.Text, Loai = cc.GetType(), index = cc.TabIndex });

                var controls3 = cc.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                foreach (var ccc in controls3)
                {
                    var typecc = ccc.GetType();
                    var getControl = masterDataContext.hd_ControlTags.FirstOrDefault(_ => _.ControlName.ToLower() == ccc.Name.ToLower());
                    var tag = "";
                    var text = "";
                    if (getControl != null)
                    {
                        tag = getControl.Tag;
                        text = getControl.Text;
                    }
                    if (!types.Contains(ccc.GetType().Name)) controls.Add(new ControlItem { control = ccc, infor = tag, text = text, Loai = ccc.GetType(), index = ccc.TabIndex });
                }
            }
            return controls;
        }
        #endregion

        #region Auto save control to table
        public static System.Collections.Generic.List<ControlItem> GetControlItemsAutoSave(System.Windows.Forms.Control.ControlCollection control)
        {
            System.Collections.Generic.List<ControlItem> controls = new System.Collections.Generic.List<ControlItem>();

            var controls1 = control.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();

            controls = GetItemsControlFormAutoSave(controls, controls1);

            return controls;
        }

        private static System.Collections.Generic.List<ControlItem> GetItemsControlFormAutoSave(System.Collections.Generic.List<ControlItem> controls, System.Collections.Generic.List<System.Windows.Forms.Control> controls1)
        {
            foreach (System.Windows.Forms.Control c in controls1)
            {
                System.Type type = c.GetType();
                var index = c.TabIndex;
                var name = c.Name;
                var getControl = masterDataContext.hd_ControlTags.FirstOrDefault(_ => _.ControlName.ToLower() == c.Name.ToLower());
                var tag = c.Tag!=null?c.Tag.ToString():"";
                var text = c.Text;
                
                if(getControl!=null)
                {
                    tag = getControl.Tag;
                    text = getControl.Text;
                }
                
                if (!types.Contains(type.Name) & !typesNoAdd.Contains(type.Name))
                {
                    if (getControl == null)
                    {
                        getControl = new hd_ControlTag();
                        masterDataContext.hd_ControlTags.InsertOnSubmit(getControl);
                    }
                    getControl.Tag = tag;
                    getControl.Text = text;
                    getControl.ControlName = c.Name;

                    masterDataContext.SubmitChanges();
                }
                // Trường hợp riêng
                var controls3 = c.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                switch (type.Name)
                {
                    case "XtraTabControl":
                        controls = GetItemInXtraTabPageAutoSave(controls, c);
                        break;

                    case "LayoutControl":
                        // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
                        controls = GetItemsControlFormAutoSave(controls, controls3);
                        break;
                    case "SplitContainerControl":
                        controls = GetItemsControlFormAutoSave(controls, controls3);
                        break;
                    case "SplitGroupPanel":
                        controls = GetItemsControlFormAutoSave(controls, controls3);
                        break;
                    case "GroupControl":
                        controls = GetItemsControlFormAutoSave(controls, controls3);
                        break;
                    case "GroupBox":
                        controls = GetItemsControlFormAutoSave(controls, controls3);
                        break;
                }
            }
            return controls;
        }

        private static System.Collections.Generic.List<ControlItem> GetItemInXtraTabPageAutoSave(System.Collections.Generic.List<ControlItem> controls, System.Windows.Forms.Control control)
        {
            var controls2 = control.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
            // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
            var cc = controls2[0];
            if (cc.GetType().Name == "XtraTabPage")
            {
                controls.Add(new ControlItem { control = cc, infor = cc.Tag != null ? cc.Tag.ToString() : "", text = cc.Text, Loai = cc.GetType() });

                var controls3 = cc.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                foreach (var ccc in controls3)
                {
                    var typecc = ccc.GetType();
                    var getControl = masterDataContext.hd_ControlTags.FirstOrDefault(_ => _.ControlName.ToLower() == ccc.Name.ToLower());
                    var tag = ccc.Tag != null ? ccc.Tag.ToString() : "";
                    var text = ccc.Text;
                    if (getControl != null)
                    {
                        tag = getControl.Tag;
                        text = getControl.Text;
                    }
                    if (!types.Contains(ccc.GetType().Name))
                    {
                        if (getControl == null)
                        {
                            getControl = new hd_ControlTag();
                            masterDataContext.hd_ControlTags.InsertOnSubmit(getControl);
                        }
                        getControl.Tag = tag;
                        getControl.Text = text;
                        getControl.ControlName = ccc.Name;

                        masterDataContext.SubmitChanges();
                    }
                }
            }
            return controls;
        }
        #endregion

        #region Clear text items control form
        public static void ClearText(System.Windows.Forms.Control.ControlCollection control)
        {
            var controls1 = control.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
            ClearTextItemsControlForm(controls1);
        }

        private static void ClearTextItemsControlForm(System.Collections.Generic.List<System.Windows.Forms.Control> controls1)
        {
            foreach (System.Windows.Forms.Control c in controls1)
            {
                System.Type type = c.GetType();
                var index = c.TabIndex;
                var name = c.Name;
                if (typesClear.Contains(c.GetType().Name)) c.Text = "";
                // Trường hợp riêng
                var controls3 = c.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                switch (type.Name)
                {
                    case "XtraTabControl":
                        ClearTextItemInXtraTabPage(c);
                        break;

                    case "LayoutControl":
                        // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
                        ClearTextItemsControlForm(controls3);
                        break;
                    case "SplitContainerControl":
                        ClearTextItemsControlForm(controls3);
                        break;
                    case "SplitGroupPanel":
                        ClearTextItemsControlForm(controls3);
                        break;
                    case "GroupControl":
                        ClearTextItemsControlForm(controls3);
                        break;
                }
            }
        }

        private static void ClearTextItemInXtraTabPage(System.Windows.Forms.Control control)
        {
            var controls2 = control.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
            // Hiện tại chỉ click được cái đầu tiên, mấy cái sau cũng lấy được nhưng lại k click vào tab được. Làm sao để click vào tab?
            var cc = controls2[0];
            if (cc.GetType().Name == "XtraTabPage")
            {
                var controls3 = cc.Controls.Cast<System.Windows.Forms.Control>().Where(_ => !types.Contains(_.Name) & _.Visible == true).OrderBy(_ => _.TabIndex).ToList();
                foreach (var ccc in controls3)
                {
                    var typecc = ccc.GetType();
                    if (typesClear.Contains(ccc.GetType().Name)) ccc.Text = "";
                }
            }
        }
        #endregion

        public static System.Collections.Generic.List<System.Windows.Forms.Control> GetAllControl(System.Windows.Forms.Control control)
        {
            var ctrls = control.Controls.Cast<System.Windows.Forms.Control>();
            return ctrls.SelectMany(_ => GetAllControl(_)).Concat(ctrls).Where(t => !types.Contains(t.GetType().Name) & t.Visible == true).OrderBy(o => o.TabIndex).ToList();
        }

        public static System.Collections.Generic.IEnumerable<System.Windows.Forms.Control> GetAll(System.Windows.Forms.Control control, System.Collections.Generic.IEnumerable<System.Type> filteringTypes)
        {
            var ctrls = control.Controls.Cast<System.Windows.Forms.Control>();

            return ctrls.SelectMany(ctrl => GetAll(ctrl, filteringTypes))
                        .Concat(ctrls)
                        .Where(ctl => filteringTypes.Any(t => ctl.GetType() == t));
        }

        public class ControlItem
        {
            public System.Windows.Forms.Control control { get; set; }
            public string infor { get; set; }
            public string text { get; set; }
            public System.Type Loai { get; set; }
            public int index { get; set; }
        }
    }
}
