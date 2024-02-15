﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17929.
// 
#pragma warning disable 1591

namespace DIP.SwitchBoard.cdrgw {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OneLotusGWBinding", Namespace="urn:OneLotusGW")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(cdr))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(monitor))]
    public partial class OneLotusGW : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback monitor_gwOperationCompleted;
        
        private System.Threading.SendOrPostCallback cdr_gwOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public OneLotusGW() {
            this.Url = global::DIP.SwitchBoard.Properties.Settings.Default.DIP_SwitchBoard_cdrgw_OneLotusGW;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event monitor_gwCompletedEventHandler monitor_gwCompleted;
        
        /// <remarks/>
        public event cdr_gwCompletedEventHandler cdr_gwCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://118.69.195.253/onelotus/pbxws/oneagentws~.php/monitor_gw", RequestNamespace="", ResponseNamespace="")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public monitor[] monitor_gw(monitorRequest request) {
            object[] results = this.Invoke("monitor_gw", new object[] {
                        request});
            return ((monitor[])(results[0]));
        }
        
        /// <remarks/>
        public void monitor_gwAsync(monitorRequest request) {
            this.monitor_gwAsync(request, null);
        }
        
        /// <remarks/>
        public void monitor_gwAsync(monitorRequest request, object userState) {
            if ((this.monitor_gwOperationCompleted == null)) {
                this.monitor_gwOperationCompleted = new System.Threading.SendOrPostCallback(this.Onmonitor_gwOperationCompleted);
            }
            this.InvokeAsync("monitor_gw", new object[] {
                        request}, this.monitor_gwOperationCompleted, userState);
        }
        
        private void Onmonitor_gwOperationCompleted(object arg) {
            if ((this.monitor_gwCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.monitor_gwCompleted(this, new monitor_gwCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://118.69.195.253/onelotus/pbxws/oneagentws~.php/cdr_gw", RequestNamespace="", ResponseNamespace="")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public cdr[] cdr_gw(cdrRequest request) {
            object[] results = this.Invoke("cdr_gw", new object[] {
                        request});
            return ((cdr[])(results[0]));
        }
        
        /// <remarks/>
        public void cdr_gwAsync(cdrRequest request) {
            this.cdr_gwAsync(request, null);
        }
        
        /// <remarks/>
        public void cdr_gwAsync(cdrRequest request, object userState) {
            if ((this.cdr_gwOperationCompleted == null)) {
                this.cdr_gwOperationCompleted = new System.Threading.SendOrPostCallback(this.Oncdr_gwOperationCompleted);
            }
            this.InvokeAsync("cdr_gw", new object[] {
                        request}, this.cdr_gwOperationCompleted, userState);
        }
        
        private void Oncdr_gwOperationCompleted(object arg) {
            if ((this.cdr_gwCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.cdr_gwCompleted(this, new cdr_gwCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:OneLotusGW")]
    public partial class monitorRequest {
        
        private string uniqueidField;
        
        private string userfieldField;
        
        private string srcField;
        
        private string dstField;
        
        private string userloginField;
        
        private string secretField;
        
        private string fromdateField;
        
        private string todateField;
        
        /// <remarks/>
        public string uniqueid {
            get {
                return this.uniqueidField;
            }
            set {
                this.uniqueidField = value;
            }
        }
        
        /// <remarks/>
        public string userfield {
            get {
                return this.userfieldField;
            }
            set {
                this.userfieldField = value;
            }
        }
        
        /// <remarks/>
        public string src {
            get {
                return this.srcField;
            }
            set {
                this.srcField = value;
            }
        }
        
        /// <remarks/>
        public string dst {
            get {
                return this.dstField;
            }
            set {
                this.dstField = value;
            }
        }
        
        /// <remarks/>
        public string userlogin {
            get {
                return this.userloginField;
            }
            set {
                this.userloginField = value;
            }
        }
        
        /// <remarks/>
        public string secret {
            get {
                return this.secretField;
            }
            set {
                this.secretField = value;
            }
        }
        
        /// <remarks/>
        public string fromdate {
            get {
                return this.fromdateField;
            }
            set {
                this.fromdateField = value;
            }
        }
        
        /// <remarks/>
        public string todate {
            get {
                return this.todateField;
            }
            set {
                this.todateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:OneLotusGW")]
    public partial class cdr {
        
        private string uniqueidField;
        
        private string userfieldField;
        
        private string accountcodeField;
        
        private string srcField;
        
        private string dstField;
        
        private string clidField;
        
        private string channelField;
        
        private string dstchannelField;
        
        private string calldateField;
        
        private string answerField;
        
        private string endField;
        
        private string durationField;
        
        private string billsecField;
        
        private string monitor_fileField;
        
        private string dispositionField;
        
        /// <remarks/>
        public string uniqueid {
            get {
                return this.uniqueidField;
            }
            set {
                this.uniqueidField = value;
            }
        }
        
        /// <remarks/>
        public string userfield {
            get {
                return this.userfieldField;
            }
            set {
                this.userfieldField = value;
            }
        }
        
        /// <remarks/>
        public string accountcode {
            get {
                return this.accountcodeField;
            }
            set {
                this.accountcodeField = value;
            }
        }
        
        /// <remarks/>
        public string src {
            get {
                return this.srcField;
            }
            set {
                this.srcField = value;
            }
        }
        
        /// <remarks/>
        public string dst {
            get {
                return this.dstField;
            }
            set {
                this.dstField = value;
            }
        }
        
        /// <remarks/>
        public string clid {
            get {
                return this.clidField;
            }
            set {
                this.clidField = value;
            }
        }
        
        /// <remarks/>
        public string channel {
            get {
                return this.channelField;
            }
            set {
                this.channelField = value;
            }
        }
        
        /// <remarks/>
        public string dstchannel {
            get {
                return this.dstchannelField;
            }
            set {
                this.dstchannelField = value;
            }
        }
        
        /// <remarks/>
        public string calldate {
            get {
                return this.calldateField;
            }
            set {
                this.calldateField = value;
            }
        }
        
        /// <remarks/>
        public string answer {
            get {
                return this.answerField;
            }
            set {
                this.answerField = value;
            }
        }
        
        /// <remarks/>
        public string end {
            get {
                return this.endField;
            }
            set {
                this.endField = value;
            }
        }
        
        /// <remarks/>
        public string duration {
            get {
                return this.durationField;
            }
            set {
                this.durationField = value;
            }
        }
        
        /// <remarks/>
        public string billsec {
            get {
                return this.billsecField;
            }
            set {
                this.billsecField = value;
            }
        }
        
        /// <remarks/>
        public string monitor_file {
            get {
                return this.monitor_fileField;
            }
            set {
                this.monitor_fileField = value;
            }
        }
        
        /// <remarks/>
        public string disposition {
            get {
                return this.dispositionField;
            }
            set {
                this.dispositionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:OneLotusGW")]
    public partial class cdrRequest {
        
        private string uniqueidField;
        
        private string userfieldField;
        
        private string accountcodeField;
        
        private string srcField;
        
        private string dstField;
        
        private string durationField;
        
        private string dispositionField;
        
        private string userloginField;
        
        private string secretField;
        
        private string fromdateField;
        
        private string todateField;
        
        /// <remarks/>
        public string uniqueid {
            get {
                return this.uniqueidField;
            }
            set {
                this.uniqueidField = value;
            }
        }
        
        /// <remarks/>
        public string userfield {
            get {
                return this.userfieldField;
            }
            set {
                this.userfieldField = value;
            }
        }
        
        /// <remarks/>
        public string accountcode {
            get {
                return this.accountcodeField;
            }
            set {
                this.accountcodeField = value;
            }
        }
        
        /// <remarks/>
        public string src {
            get {
                return this.srcField;
            }
            set {
                this.srcField = value;
            }
        }
        
        /// <remarks/>
        public string dst {
            get {
                return this.dstField;
            }
            set {
                this.dstField = value;
            }
        }
        
        /// <remarks/>
        public string duration {
            get {
                return this.durationField;
            }
            set {
                this.durationField = value;
            }
        }
        
        /// <remarks/>
        public string disposition {
            get {
                return this.dispositionField;
            }
            set {
                this.dispositionField = value;
            }
        }
        
        /// <remarks/>
        public string userlogin {
            get {
                return this.userloginField;
            }
            set {
                this.userloginField = value;
            }
        }
        
        /// <remarks/>
        public string secret {
            get {
                return this.secretField;
            }
            set {
                this.secretField = value;
            }
        }
        
        /// <remarks/>
        public string fromdate {
            get {
                return this.fromdateField;
            }
            set {
                this.fromdateField = value;
            }
        }
        
        /// <remarks/>
        public string todate {
            get {
                return this.todateField;
            }
            set {
                this.todateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:OneLotusGW")]
    public partial class monitor {
        
        private string uniqueidField;
        
        private string userfieldField;
        
        private string accountcodeField;
        
        private string srcField;
        
        private string dstField;
        
        private string agentField;
        
        private string clidField;
        
        private string channelField;
        
        private string dstchannelField;
        
        private string calldateField;
        
        private string answerField;
        
        private string endField;
        
        private string durationField;
        
        private string billsecField;
        
        private string monitor_dateField;
        
        private string monitor_fileField;
        
        private string dispositionField;
        
        /// <remarks/>
        public string uniqueid {
            get {
                return this.uniqueidField;
            }
            set {
                this.uniqueidField = value;
            }
        }
        
        /// <remarks/>
        public string userfield {
            get {
                return this.userfieldField;
            }
            set {
                this.userfieldField = value;
            }
        }
        
        /// <remarks/>
        public string accountcode {
            get {
                return this.accountcodeField;
            }
            set {
                this.accountcodeField = value;
            }
        }
        
        /// <remarks/>
        public string src {
            get {
                return this.srcField;
            }
            set {
                this.srcField = value;
            }
        }
        
        /// <remarks/>
        public string dst {
            get {
                return this.dstField;
            }
            set {
                this.dstField = value;
            }
        }
        
        /// <remarks/>
        public string agent {
            get {
                return this.agentField;
            }
            set {
                this.agentField = value;
            }
        }
        
        /// <remarks/>
        public string clid {
            get {
                return this.clidField;
            }
            set {
                this.clidField = value;
            }
        }
        
        /// <remarks/>
        public string channel {
            get {
                return this.channelField;
            }
            set {
                this.channelField = value;
            }
        }
        
        /// <remarks/>
        public string dstchannel {
            get {
                return this.dstchannelField;
            }
            set {
                this.dstchannelField = value;
            }
        }
        
        /// <remarks/>
        public string calldate {
            get {
                return this.calldateField;
            }
            set {
                this.calldateField = value;
            }
        }
        
        /// <remarks/>
        public string answer {
            get {
                return this.answerField;
            }
            set {
                this.answerField = value;
            }
        }
        
        /// <remarks/>
        public string end {
            get {
                return this.endField;
            }
            set {
                this.endField = value;
            }
        }
        
        /// <remarks/>
        public string duration {
            get {
                return this.durationField;
            }
            set {
                this.durationField = value;
            }
        }
        
        /// <remarks/>
        public string billsec {
            get {
                return this.billsecField;
            }
            set {
                this.billsecField = value;
            }
        }
        
        /// <remarks/>
        public string monitor_date {
            get {
                return this.monitor_dateField;
            }
            set {
                this.monitor_dateField = value;
            }
        }
        
        /// <remarks/>
        public string monitor_file {
            get {
                return this.monitor_fileField;
            }
            set {
                this.monitor_fileField = value;
            }
        }
        
        /// <remarks/>
        public string disposition {
            get {
                return this.dispositionField;
            }
            set {
                this.dispositionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void monitor_gwCompletedEventHandler(object sender, monitor_gwCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class monitor_gwCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal monitor_gwCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public monitor[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((monitor[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void cdr_gwCompletedEventHandler(object sender, cdr_gwCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class cdr_gwCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal cdr_gwCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public cdr[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((cdr[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591