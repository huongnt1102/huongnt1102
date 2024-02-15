using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIP.SoftPhoneAPI
{
    public delegate void CallHistoryRowClickEventHandler(object sender, CallHistoryRowClickEventArgs e);
    public class CallHistoryRowClickEventArgs : EventArgs
    {
        public string PhoneNumber { get; set; }
        public List<Customer> Result { get; set; }
    }

    public delegate void CallHistoryBinDataEventHandler(object sender, CallHistoryBinDataEventArgs e);
    public class CallHistoryBinDataEventArgs : EventArgs
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public List<CallRecord> Result { get; set; }
    }

    public delegate void CallHistoryListenEventHandler(object sender, CallHistoryListenEventArgs e);
    public class CallHistoryListenEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }
}