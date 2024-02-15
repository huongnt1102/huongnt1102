using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UI;
using Library;

namespace Building.PrintControls
{
    public delegate void ReportIDChangedEventHandler(object sender, ReportIDChangedEventArgs e);
    public class ReportIDChangedEventArgs : EventArgs
    {
        public ReportIDChangedEventArgs(int? reportID)
        {
            _ReportID = reportID;
        }

        private int? _ReportID;
        public int? ReportID { get { return _ReportID; } }
    }

    public delegate void PrintviewEventHandler(object sender, PrintviewEventArgs e);
    public class PrintviewEventArgs : EventArgs
    {
        public PrintviewEventArgs(int? id, string name)
        {
            _ReportID = id;
            _ReportName = name;
        }

        private int? _ReportID;
        public int? ReportID { get { return _ReportID; } }

        private string _ReportName;
        public string ReportName { get { return _ReportName; } }
    }

    public delegate void EditButtonClickEventHandler(object sender, EditButtonClickEventArgs e);
    public class EditButtonClickEventArgs : EventArgs
    {
        public EditButtonClickEventArgs(int? reportID)
        {
            _ReportID = reportID;
        }

        private int? _ReportID;
        public int? ReportID { get { return _ReportID; } }
        public XtraReport Report { get; set; }
    }

    public delegate void SavingTemplateEventHandler(object sender, SavingTemplateEventArgs e);
    public class SavingTemplateEventArgs : EventArgs
    {
        public SavingTemplateEventArgs(int? reportID, System.Data.Linq.Binary template)
        {
            _ReportID = reportID;
            _Template = template;
        }

        private int? _ReportID;
        public int? ReportID { get { return _ReportID; } }

        private System.Data.Linq.Binary _Template;
        public System.Data.Linq.Binary Template { get { return _Template; } }

        public bool Saved { get; set; }
    }

    public class SaveCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
    {
        event SavingTemplateEventHandler savingTemplate;
        XRDesignPanel panel;
        int? reportID;

        public SaveCommandHandler(XRDesignPanel _panel, int? _reportID, SavingTemplateEventHandler _savingTemplate)
        {
            this.panel = _panel;
            this.reportID = _reportID;
            this.savingTemplate = _savingTemplate;
        }

        public void HandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command,
    object[] args)
        {
            // Save the report. 
            Save(command);
        }
        public bool CanHandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command,
        ref bool useNextHandler)
        {
            useNextHandler = !(command == ReportCommand.SaveFile ||
                command == ReportCommand.SaveFileAs);
            return !useNextHandler;
            //if (!CanHandleCommand(command)) return;

            //handled = true;

            //if (panel.ReportState == ReportState.Changed)
            //{
            //    if (command == ReportCommand.Closing && DialogBox.Question("Do you want to save changes?") == System.Windows.Forms.DialogResult.No)
            //    {
            //        return;
            //    }
            //    try
            //    {
            //        System.Data.Linq.Binary layout;
            //        using (var stream = new System.IO.MemoryStream())
            //        {
            //            panel.Report.SaveLayout(stream);
            //            layout = new System.Data.Linq.Binary(stream.ToArray());
            //        }
            //        SavingTemplateEventArgs objSave = new SavingTemplateEventArgs(this.reportID, layout);
            //        this.savingTemplate(null, objSave);
            //        if (objSave.Saved)
            //        {
            //            panel.ReportState = ReportState.Saved;
            //        }
            //    }
            //    catch { }
            //}
        }
        void Save(ReportCommand command)
        {
            // Write your custom saving here. 
            // ... 

            // For instance: 
            //panel.Report.SaveLayout("c:\\report1.repx");

            if (panel.ReportState == ReportState.Changed)
            {
                if (command == ReportCommand.SaveFile && DialogBox.Question("Bạn có muốn lưu không?") == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        System.Data.Linq.Binary layout;
                        using (var stream = new System.IO.MemoryStream())
                        {
                            panel.Report.SaveLayout(stream);
                            layout = new System.Data.Linq.Binary(stream.ToArray());
                        }
                        SavingTemplateEventArgs objSave = new SavingTemplateEventArgs(this.reportID, layout);
                        this.savingTemplate(null, objSave);
                        if (objSave.Saved)
                        {
                            panel.ReportState = ReportState.Saved;
                        }

                        DialogBox.Success("Lưu thành công");
                    }
                    catch { }
                }
            }

            // Prevent the "Report has been changed" dialog from being shown. 
            panel.ReportState = ReportState.Saved;
        }

        //public bool CanHandleCommand(ReportCommand command)
        //{
        //    return command == ReportCommand.SaveFile || command == ReportCommand.Closing;
        //}

        //public void HandleCommand(ReportCommand command, object[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
    
    public enum ReportItem
    {
        PrintView = 1,
        EditItem = 2
    }

    public delegate void ToaNhaEditValueChangedEventHandler(object sender, ToaNhaEditValueChangedEventArgs e);
    public class ToaNhaEditValueChangedEventArgs : EventArgs
    {
        public List<SourceItem> DataSource { get; set; }
    }

    public class SourceItem
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int? GroupID { get; set; }
        public string GroupName { get; set; }
    }
}
