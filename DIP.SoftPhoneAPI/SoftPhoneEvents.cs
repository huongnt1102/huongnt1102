using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIP.SoftPhoneAPI
{
    public delegate void BeginCallEventHandler(object sender, BeginCallEventArgs e);
    public class BeginCallEventArgs : EventArgs
    {
        public string Number { get; set; }
        public List<Customer> Customers { get; set; }
    }

    public delegate void ContactAddNewButtonClickEventHandler(object sender, ContactAddNewButtonClickEventArgs e);
    public class ContactAddNewButtonClickEventArgs : EventArgs
    {
        public string Number { get; set; }
        public int? CustomerID { get; set; }
        public bool Result { get; set; }
    }

    public delegate void CustomerAddNewButtonClickEventHandler(object sender, CustomerAddNewButtonClickEventArgs e);
    public class CustomerAddNewButtonClickEventArgs : EventArgs
    {
        public string Number { get; set; }
        public Customer Customer { get; set; }
    }

    public delegate void CustomerEditButtonClickEventHandler(object sender, CustomerEditButtonClickEventArgs e);
    public class CustomerEditButtonClickEventArgs : EventArgs
    {
        public int? ID { get; set; }
        public string Number { get; set; }
        public Customer Customer { get; set; }
    }

    public delegate void CustomerSearchButtonClickEventHandler(object sender, CustomerSearchButtonClickEventArgs e);
    public class CustomerSearchButtonClickEventArgs : EventArgs
    {
        public Customer Customer { get; set; }
    }

    public delegate void ReferenceButtonClickEventHandler(object sender, ReferenceButtonClickEventArgs e);
    public class ReferenceButtonClickEventArgs : EventArgs
    {
        public int CusID { get; set; }
        public int? LinkType { get; set; }
        public int? LinkID { get; set; }        
        public string LinkName { get; set; }
        public int? StatusID { get; set; }
    }

    public delegate void ReferenceValueChangedEventHandler(object sender, ReferenceValueChangedEventArgs e);
    public class ReferenceValueChangedEventArgs : EventArgs
    {
        public int? LinkType { get; set; }
        public int? LinkID { get; set; }
        public List<HistoryStatus> StatusList { get; set; }
    }

    public delegate void SaveButtonClickEventHandler(object sender, SaveButtonClickEventArgs e);
    public class SaveButtonClickEventArgs : EventArgs
    {
        public string CallID { get; set; }
        public int CusID { get; set; }
        public int? LinkType { get; set; }
        public int? LinkID { get; set; }
        public int? StatusID { get; set; }
        public string Note { get; set; }
        public bool Result { get; set; }
    }

    public delegate void HistoryPageViewEventHandler(object sender, HistoryPageViewEventArgs e);
    public class HistoryPageViewEventArgs : EventArgs
    {
        public int CustomerID { get; set; }
        public List<History> Data { get; set; }
    }

    public delegate void TransactionPageViewEventHandler(object sender, TransactionPageViewEventArgs e);
    public class TransactionPageViewEventArgs : EventArgs
    {
        public int CustomerID { get; set; }
        public List<Transaction> Data { get; set; }
    }

    public delegate void ContactPageViewEventHandler(object sender, ContactPageViewEventArgs e);
    public class ContactPageViewEventArgs : EventArgs
    {
        public int CustomerID { get; set; }
        public List<Contact> Data { get; set; }
    }

    public delegate void YeuCauPageViewEventHandler(object sender, YeuCauPageViewEventArgs e);
    public class YeuCauPageViewEventArgs : EventArgs
    {
        public int CustomerID { get; set; }
        public List<YeuCau> Data { get; set; }
    }

    public delegate void CongNoPageViewEventHandler(object sender, CongNoPageViewEventArgs e);
    public class CongNoPageViewEventArgs : EventArgs
    {
        public int CustomerID { get; set; }
        public List<CongNo> Data { get; set; }
    }

    public delegate void CallHistoryBeginEditEventHandler(object sender, CallHistoryBeginEditEventArgs e);
    public class CallHistoryBeginEditEventArgs : EventArgs
    {
        public string Number { get; set; }
        public int? CusID { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
