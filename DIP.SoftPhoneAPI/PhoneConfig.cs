using System;
using System.Collections.Generic;
using System.Text;
using DIP.SoftPhoneSDK.Common;
using DIP.SoftPhoneSDK;

namespace DIP.SoftPhoneAPI
{
  internal class PhoneConfig : IConfiguratorInterface
  {

    List<IAccount> _acclist = new List<IAccount>();

    internal PhoneConfig(AccountConfig acc)
    {
      _acclist.Add(acc);
    }

    #region IConfiguratorInterface Members

    public bool AAFlag
    {
      get
      {
        return false;
      }
      set {}
    }

    public List<IAccount> Accounts
    {
      get { return _acclist; }
    }

    public bool CFBFlag
    {
      get
      {
        return false;
      }
      set {}
    }

    public string CFBNumber
    {
      get
      {
        return "";
      }
      set{}
    }

    public bool CFNRFlag
    {
      get
      {
        return false;
      }
      set {}
    }

    public string CFNRNumber
    {
      get
      {
        return "";
      }
      set {}
    }

    public bool CFUFlag
    {
      get
      {
        return false;
      }
      set {}
    }

    public string CFUNumber
    {
      get
      {
        return "";
      }
      set{}
    }

    public List<string> CodecList
    {
      get
      {
        List<String> cl = new List<string>();
        cl.Add("PCMA");
        return cl;
      }
      set {}
    }

    public bool DNDFlag
    {
      get
      {
        return false;
      }
      set{}
    }

    public int DefaultAccountIndex
    {
      get { return 0; }
    }

    public bool IsNull
    {
      get { return false; }
    }

    public bool PublishEnabled
    {
      get
      {
        return false;
      }
      set {}
    }

    private int _SIPPort = 5060;
    public int SIPPort
    {
      get
      {
        return _SIPPort;
      }
      set{
          _SIPPort = value;
      }
    }

    public void Save()
    {
      //TODO;
    }

    #endregion
  }
   
  class AccountConfig : IAccount
  {
    #region IAccount Members

      private bool _Enabled = true;
      public bool Enabled
      {
          get { return _Enabled; }
          set { _Enabled = value; }
      }

    private string _AccountName;
    public string AccountName
    {
      get
      {
          return _AccountName;
      }
      set
      {
          _AccountName = value;
      }
    }

    private string _DisplayName;
    public string DisplayName
    {
      get
      {
          return _DisplayName;
      }
      set {
          _DisplayName = value;
      }
    }

    public string DomainName
    {
      get
      {
          return "*";
      }
      set{}
    }

    private string _HostName;
    public string HostName
    {
      get
      {
        return _HostName;
      }
      set{
          _HostName = value;
      }
    }

    private string _Id;
    public string Id
    {
      get
      {
          return _Id;
      }
      set {
          _Id = value;
      }
    }

    public int Index
    {
      get
      {
        return 0;
      }
      set{}
    }

    private string _Password;
    public string Password
    {
      get
      {
        return _Password;
      }
      set {
          _Password = value;
      }
    }

    public string ProxyAddress
    {
      get
      {
          return "";
      }
      set{}
    }

    public int RegState
    {
      get
      {
        return 0;
      }
      set{}
    }

    private ETransportMode _TransportMode = ETransportMode.TM_UDP;
    public ETransportMode TransportMode
    {
      get
      {
        return _TransportMode;
      }
      set{
          _TransportMode = value;
      }
    }

    private string _UserName;
    public string UserName
    {
      get
      {
        return _UserName;
      }
      set{
          _UserName = value;
      }
    }
    #endregion
  }
}
