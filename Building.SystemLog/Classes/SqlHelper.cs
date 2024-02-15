using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using System.Transactions;
using Microsoft.VisualBasic;
using System.Globalization;
using DIPBMS.SystemLog.Classes;

namespace Building.SystemLog.Classes
{
    internal class MyConfig : AppSettingsReader
    {
        // Fields
        private string _cfgFile = (Application.ExecutablePath + ".Config");
        private XmlNode node;

        // Methods
        public string GetValue(string key)
        {
            return Convert.ToString(this.GetValue(key, typeof(string)));
        }

        public new object GetValue(string key, Type sType)
        {
            XmlDocument doc = new XmlDocument();
            object attribute = string.Empty;
            this.loadDoc(doc);
            string xpath = key.Substring(0, key.LastIndexOf("//"));
            try
            {
                this.node = doc.SelectSingleNode(xpath);
                if (this.node != null)
                {
                    XmlElement element = (XmlElement)this.node.SelectSingleNode(key);
                    if (element != null)
                    {
                        attribute = element.GetAttribute("value");
                    }
                }
                if (sType != typeof(string))
                {
                    if (sType == typeof(bool))
                    {
                        return ((attribute.Equals("True") || attribute.Equals("False")) && Convert.ToBoolean(attribute));
                    }
                    if (sType == typeof(int))
                    {
                        return Convert.ToInt32(attribute);
                    }
                    if (sType == typeof(double))
                    {
                        return Convert.ToDouble(attribute);
                    }
                    if (sType == typeof(DateTime))
                    {
                        return Convert.ToDateTime(attribute);
                    }
                }
                return Convert.ToString(attribute);
            }
            catch
            {
                return string.Empty;
            }
        }

        private void loadDoc(XmlDocument doc)
        {
            doc.Load(this._cfgFile);
        }

        public bool removeElement(string key)
        {
            XmlDocument doc = new XmlDocument();
            this.loadDoc(doc);
            try
            {
                string xpath = key.Substring(0, key.LastIndexOf("//"));
                this.node = doc.SelectSingleNode(xpath);
                if (this.node == null)
                {
                    return false;
                }
                XmlNode oldChild = this.node.SelectSingleNode(key);
                this.node.RemoveChild(oldChild);
                this.saveDoc(doc, this._cfgFile);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        private void saveDoc(XmlDocument doc, string docPath)
        {
            if (!this._cfgFile.Equals("web.config"))
            {
                try
                {
                    XmlTextWriter w = new XmlTextWriter(docPath, null);
                    w.Formatting = Formatting.Indented;
                    doc.WriteTo(w);
                    w.Flush();
                    w.Close();
                }
                catch
                {
                }
            }
        }

        public bool SetValue(string key, string val)
        {
            XmlDocument doc = new XmlDocument();
            this.loadDoc(doc);
            try
            {
                string xpath = key.Substring(0, key.LastIndexOf("//"));
                this.node = doc.SelectSingleNode(xpath);
                if (this.node == null)
                {
                    return false;
                }
                XmlElement element = (XmlElement)this.node.SelectSingleNode(key);
                if (element != null)
                {
                    element.SetAttribute("value", val);
                }
                else
                {
                    xpath = key.Substring(key.LastIndexOf("//") + 2);
                    XmlElement newChild = doc.CreateElement(xpath.Substring(0, xpath.IndexOf("[@")).Trim());
                    xpath = xpath.Substring(xpath.IndexOf("'") + 1);
                    newChild.SetAttribute("key", xpath.Substring(0, xpath.IndexOf("'")));
                    newChild.SetAttribute("value", val);
                    this.node.AppendChild(newChild);
                }
                this.saveDoc(doc, this._cfgFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Properties
        public string ConfigFile
        {
            get
            {
                return this._cfgFile;
            }
            set
            {
                this._cfgFile = Application.StartupPath + @"\" + value;
            }
        }
    }
    public class SqlAccess
    {
        // Fields
        private OleDbConnection _myConn;
        private OleDbTransaction _OleDbTrans;
        private string _strConn;

        // Methods
        [Obsolete]
        public SqlAccess()
        {
            if (ConfigurationSettings.AppSettings["connectionString"] == null)
            {
                throw new ArgumentNullException("Không thể đọc được chuỗi \"ConnectString\" trong tập tin .Config.");
            }
            this._strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            this._strConn = new Library.SqlSetting().Conn;
            this._myConn = new OleDbConnection();
        }

        public SqlAccess(string sDBName)
        {
            this._strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDBName + ";Persist Security Info=False;Jet OLEDB:Database Password=";
            this._myConn = new OleDbConnection();
        }

        public SqlAccess(string sDBName, string sPassword)
        {
            this._strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDBName + ";Persist Security Info=False;Jet OLEDB:Database Password=" + sPassword;
            this._myConn = new OleDbConnection();
        }

        public SqlAccess(string sLocation, string sDBName, string sPassword)
        {
            this._strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sLocation + sDBName + ";Persist Security Info=False;Jet OLEDB:Database Password=" + sPassword;
            this._myConn = new OleDbConnection();
        }

        public void Close()
        {
            if (this._myConn.State == ConnectionState.Open)
            {
                this._myConn.Close();
                this._myConn.Dispose();
                this._myConn = null;
            }
        }

        public string Commit()
        {
            string str2;
            try
            {
                this._OleDbTrans.Commit();
                str2 = "OK";
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return str2;
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            return this.ExecuteDataTable(commandText, null, null);
        }

        public DataTable ExecuteDataTable(OleDbTransaction myTransaction, string commandText)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OleDbCommand selectCommand = new OleDbCommand(commandText, myTransaction.Connection, myTransaction);
                new OleDbDataAdapter(selectCommand).Fill(dataTable);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return dataTable;
        }

        public DataTable ExecuteDataTable(string commandText, string[] myPara, object[] myValue)
        {
            DataTable dataTable = new DataTable();
            if (this.Open() == "OK")
            {
                try
                {
                    OleDbCommand selectCommand = new OleDbCommand();
                    selectCommand.CommandText = commandText;
                    selectCommand.Connection = this._myConn;
                    if (myPara != null)
                    {
                        selectCommand.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < myPara.Length; i++)
                        {
                            OleDbParameter parameter = new OleDbParameter(myPara[i], myValue[i]);
                            selectCommand.Parameters.Add(parameter);
                        }
                    }
                    new OleDbDataAdapter(selectCommand).Fill(dataTable);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    this.Close();
                }
            }
            return dataTable;
        }

        public string ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(commandText, null, null);
        }

        public string ExecuteNonQuery(OleDbTransaction myTransaction, string commandText)
        {
            return this.ExecuteNonQuery(myTransaction, commandText, null, null);
        }

        public string ExecuteNonQuery(string commandText, string[] myPara, object[] myValue)
        {
            string message = "";
            message = this.Open();
            if (message == "OK")
            {
                try
                {
                    OleDbCommand command = new OleDbCommand();
                    command.CommandText = commandText;
                    command.Connection = this._myConn;
                    if (myPara != null)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < myPara.Length; i++)
                        {
                            OleDbParameter parameter = new OleDbParameter(myPara[i], myValue[i]);
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                    message = "OK";
                }
                catch (Exception exception)
                {
                    message = exception.Message;
                    throw new Exception(message);
                }
                finally
                {
                    this.Close();
                }
            }
            return message;
        }

        public string ExecuteNonQuery(OleDbTransaction myTransaction, string commandText, string[] myPara, object[] myValue)
        {
            string message = "";
            try
            {
                OleDbCommand command = new OleDbCommand();
                command.CommandText = commandText;
                command.Connection = myTransaction.Connection;
                command.Transaction = myTransaction;
                if (myPara != null)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < myPara.Length; i++)
                    {
                        OleDbParameter parameter = new OleDbParameter(myPara[i], myValue[i]);
                        command.Parameters.Add(parameter);
                    }
                }
                command.ExecuteNonQuery();
                message = "OK";
            }
            catch (Exception exception)
            {
                message = exception.Message;
                this.Rollback();
                throw new Exception(message);
            }
            return message;
        }

        public OleDbDataReader ExecuteReader(string commandText)
        {
            OleDbDataReader reader = null;
            if (this.Open() == "OK")
            {
                reader = this.ExecuteReader(this._myConn, commandText);
            }
            return reader;
        }

        public OleDbDataReader ExecuteReader(OleDbConnection myConnection, string commandText)
        {
            OleDbDataReader reader = null;
            try
            {
                reader = new OleDbCommand(commandText, myConnection).ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
            return reader;
        }

        public OleDbDataReader ExecuteReader(OleDbTransaction myTransaction, string commandText)
        {
            OleDbDataReader reader = null;
            try
            {
                reader = new OleDbCommand(commandText, myTransaction.Connection, myTransaction).ExecuteReader();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
            return reader;
        }

        public object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(commandText, null, null);
        }

        public object ExecuteScalar(string commandText, string[] myPara, object[] myValue)
        {
            object obj2 = null;
            if (this.Open() == "OK")
            {
                try
                {
                    OleDbCommand command = new OleDbCommand();
                    command.CommandText = commandText;
                    command.Connection = this._myConn;
                    if (myPara != null)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < myPara.Length; i++)
                        {
                            OleDbParameter parameter = new OleDbParameter(myPara[i], myValue[i]);
                            command.Parameters.Add(parameter);
                        }
                    }
                    obj2 = command.ExecuteScalar();
                }
                catch (Exception exception)
                {
                    obj2 = null;
                    throw new Exception(exception.Message);
                }
                finally
                {
                    this.Close();
                }
            }
            return obj2;
        }

        ~SqlAccess()
        {
            this._myConn = null;
        }

        public string Open()
        {
            string str;
            if (this._myConn == null)
            {
                throw new Exception("Connect is null");
            }
            try
            {
                this._myConn.ConnectionString = this._strConn;
                if (this._myConn.State == ConnectionState.Closed)
                {
                    this._myConn.Open();
                }
                str = "OK";
            }
            catch (OleDbException exception)
            {
                string message = exception.Message;
                throw new Exception(exception.Message);
            }
            return str;
        }

        public string Rollback()
        {
            string message = "";
            try
            {
                this._OleDbTrans.Rollback();
                return "OK";
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }

        public string Start()
        {
            string str2;
            string str = this.Open();
            if (str != "OK")
            {
                return str;
            }
            try
            {
                this._OleDbTrans = this._myConn.BeginTransaction();
                str2 = "OK";
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return str2;
        }

        // Properties
        public OleDbConnection Connection
        {
            get
            {
                return this._myConn;
            }
        }

        public string ConnectString
        {
            get
            {
                return this._strConn;
            }
            set
            {
                this._strConn = value;
            }
        }

        public OleDbTransaction Transaction
        {
            get
            {
                return this._OleDbTrans;
            }
        }
    }
    public class SqlHelper
    {
        // Fields
        private bool _autoCommand;
        private bool _authentication;
        private string _commandText;
        private CommandType _commandType;
        private SqlConnection _connection;
        private string _connectionString;
        private static int _count = 0;
        private string _database;
        public static string _GlobalConnectString = "";
        private static bool _HideError = false;
        private bool _isextract;
        private bool _isFormatMessage;
        private bool _mustCloseConnection;
        private string _password;
        private string _result;
        private string _server;
        private static string _serverConnectionString = "";
        private SqlHelperException _sqlHelperException;
        private SqlTransaction _transaction;
        private string _userid;
        private int m_rowaffected;
        private bool m_useTransaction;

        // Events
        public event ConnectEventHander Connect;

        public event ConnectedEventHander Connected;

        public event ErrorEventHander Error;

        public event TransactionClosedEventHander TransactionClosed;

        public event TransactionCompletedEventHandler TransactionCompleted;

        public event TransactionEventHander TransactionStart;

        public event TransactionStartedEventHandler TransactionStarted;

        // Methods
        public SqlHelper()
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            this._connectionString = _GlobalConnectString;
            this._connection = new SqlConnection();
            Count++;
        }

        public SqlHelper(SqlConnection connection)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            if (connection == null)
            {
                throw new SqlHelperException("connection");
            }
            this._connection = connection;
            this._connectionString = this.Connection.ConnectionString;
            Count++;
        }

        public SqlHelper(string connectString)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(connectString))
            {
                throw new SqlHelperException("connectString");
            }
            this._connectionString = connectString;
            this._connection = new SqlConnection();
            Count++;
        }

        public SqlHelper(string serverName, int authencation)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            this._server = serverName;
            this._database = "";
            this._userid = "";
            this._password = "";
            this._authentication = true;
            this._connectionString = this.RebuildConnectionString(this._server);
            this._connection = new SqlConnection();
            Count++;
        }

        public SqlHelper(string serverName, string dataBaseName)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            this._server = serverName;
            this._database = dataBaseName;
            this._userid = "";
            this._password = "";
            this._authentication = true;
            this._connectionString = this.RebuildConnectionString(this._server, this._database);
            this._connection = new SqlConnection();
            Count++;
        }

        public SqlHelper(string connectstring, string password, bool IsCrypt)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            if (IsCrypt)
            {
                connectstring = MyEncryption.Decrypt(connectstring, password, true);
            }
            if (ConnectString == null)
            {
                throw new ArgumentNullException("ConnectString không được rỗng.");
            }
            if (string.IsNullOrEmpty(connectstring))
            {
                throw new SqlHelperException("connectstring");
            }
            this._connectionString = connectstring;
            this._connection = new SqlConnection();
            Count++;
        }

        public SqlHelper(string serverName, string userName, string passWord)
            : this(serverName, "", userName, passWord, false)
        {
        }

        public SqlHelper(string serverName, string dataBaseName, string userName, string passWord)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            this._server = serverName;
            this._database = dataBaseName;
            this._userid = userName;
            this._password = passWord;
            this._authentication = false;
            this._connectionString = this.RebuildConnectionString();
            this._connection = new SqlConnection();
            Count++;
        }

        public SqlHelper(string serverName, string dataBaseName, string userName, string passWord, bool authen)
        {
            this._autoCommand = true;
            this._authentication = true;
            this._connectionString = "";
            this._database = "";
            this._password = "";
            this._server = "";
            this._userid = "";
            this._result = "";
            this._commandText = "";
            this._isFormatMessage = true;
            this._commandType = CommandType.StoredProcedure;
            this._server = serverName;
            this._database = dataBaseName;
            this._userid = userName;
            this._password = passWord;
            this._authentication = authen;
            this._connectionString = this.RebuildConnectionString();
            this._connection = new SqlConnection();
            Count++;
        }

        public string AddUser(string userId, string password, string database)
        {
            this.CommandType = CommandType.Text;
            return this.ExecuteNonQuery("EXEC master.dbo.sp_addlogin @loginame = N'" + userId + "', @passwd = N'" + password + "', @defdb = N'" + database + "'");
        }

        private void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters != null) && (parameterValues != null))
            {
                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
                int index = 0;
                int length = commandParameters.Length;
                while (index < length)
                {
                    if (parameterValues[index] is IDbDataParameter)
                    {
                        IDbDataParameter parameter = (IDbDataParameter)parameterValues[index];
                        if (parameter.Value == null)
                        {
                            commandParameters[index].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[index].Value = parameter.Value;
                        }
                    }
                    else if (parameterValues[index] == null)
                    {
                        commandParameters[index].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[index].Value = parameterValues[index];
                    }
                    index++;
                }
            }
        }

        public string Attach(string mdfFile, string ldfFile, string database)
        {
            this.Result = this.Exists(database);
            if (this.Result == "OK")
            {
                string commandText = "EXEC sp_attach_db @dbname = N'" + database + "',@filename1 = N'" + mdfFile + "',@filename2 = N'" + ldfFile + "'";
                this.CommandType = CommandType.Text;
                this.Result = this.ExecuteNonQuery(commandText);
            }
            return this.Result;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (commandParameters != null)
            {
                foreach (SqlParameter parameter in commandParameters)
                {
                    if (parameter != null)
                    {
                        if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }
        }

        public void Close()
        {
            this.Close(this._connection);
        }

        public void Close(SqlConnection myConnection)
        {
            if (myConnection != null)
            {
                if (myConnection.State == ConnectionState.Open)
                {
                    try
                    {
                        myConnection.Close();
                    }
                    catch (SqlException)
                    {
                        this.Result = "Có lỗi xảy ra khi mở kết nối đến máy chủ.";
                    }
                    catch (Exception)
                    {
                    }
                }
                this._connection.Dispose();
                this._connection = null;
            }
        }

        public string Commit()
        {
            return this.Commit(this._transaction);
        }

        public string Commit(SqlTransaction myTransaction)
        {
            if (myTransaction == null)
            {
                throw new SqlHelperException("myTransaction");
            }
            this.Result = "";
            try
            {
                myTransaction.Commit();
                this.RaiseTransactionClosedEventHander();
                this.Close();
                this.Result = "OK";
                return this.Result;
            }
            catch (InvalidOperationException exception)
            {
                if (_HideError)
                {
                    this.Result = "Các giao dịch đã được hoàn thành hoặc được khôi phục lại.\n Ở kết nối bị huỹ.";
                }
                else
                {
                    this.Result = exception.Message;
                }
            }
            catch (SqlException exception2)
            {
                if (_HideError)
                {
                    this.Result = "Không thể kết thành giao dịch.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception2.Number));
            }
            catch (Exception exception3)
            {
                if (_HideError)
                {
                    this.Result = "Có lỗi xảy ra trong khi cố gắng hoàn thành giao dịch.";
                }
                else
                {
                    this.Result = exception3.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return this.Result;
        }

        public bool Check()
        {
            return this.Check(false);
        }

        public bool Check(bool isclose)
        {
            if (!(this.Open() == "OK"))
            {
                return false;
            }
            if (isclose)
            {
                this.Close();
            }
            return true;
        }

        public static string CheckId(string tableName, string columnName, string vKey)
        {
            string str = "";
            SqlHelper helper = new SqlHelper();
            helper.CommandType = CommandType.Text;
            object obj2 = helper.ExecuteScalar("select count([" + columnName + "]) from [" + tableName + "] where [" + columnName + "] like '" + vKey + "'");
            if ((obj2 != null) && (Convert.ToInt32(obj2) == 0))
            {
                str = "OK";
            }
            return str;
        }

        public string Deattach(string database)
        {
            this.Result = this.Open();
            if (this.Result == "OK")
            {
                string commandText = "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + database + "') BEGIN  EXEC sp_detach_db @dbname = N'" + database + "' END ";
                this.CommandType = CommandType.Text;
                this.Result = this.ExecuteNonQuery(commandText);
            }
            return this.Result;
        }

        public string Decrypt()
        {
            return this.Decrypt(this._connectionString, "!@#$%^&*()Pofd154$#@");
        }

        public string Decrypt(string connectstring, string password)
        {
            connectstring = MyEncryption.Decrypt(connectstring, password, true);
            this._connectionString = connectstring;
            if (this._connection == null)
            {
                this._connection = new SqlConnection();
            }
            return this._connectionString;
        }

        private string DispError(int number, string message, string function)
        {
            switch (number)
            {
                case 0x3a:
                    return "Không thể kết nối đến máy chủ,(máy chủ không tồn tại,\n phương thức kết nối không đúng,\n máy chủ không cho phép truy cập từ xa).";

                case 0xc9:
                    return ("Thủ tục hoặc chức năng " + function.ToUpper() + " đối số sử dụng không đúng.");

                case -2:
                    return "Thời gian giao dịch quá lâu, kết nối đến tự động ngắt.";

                case 2:
                    return "Không thể kết nối với máy chủ Sql Server.";

                case 0xe9:
                    return "Không thể thực hiện yêu cầu máy chủ mở giao dịch.";

                case 0xafc:
                    return ("Không tìm thấy thủ tục " + function.ToUpper() + ".\n Có thể cở sở dữ liệu này đã quá cũ vui lòng cập nhật mới.");

                case 0xfdc:
                    return "Không thể mở kết nối đến cở sở dữ liệu, Có thể cở sở dữ liệu này không tồn tại.";

                case 0x1fd2:
                    return ("Thủ tục " + function.ToUpper() + " không có đối số.");

                case 0x4818:
                    return "Không thể đăng nhập vào máy chủ Sql Server với tài khoản này.\n Có thể tài khoản hoặc mật khẩu không đúng.";
            }
            if (message.Length > 0)
            {
                int index = message.IndexOf('\n');
                if (index != -1)
                {
                    message = message.Substring(0, index);
                }
            }
            return message;
        }

        public string Encrypt()
        {
            return this.Encrypt(this._connectionString, "!@#$%^&*()Pofd154$#@");
        }

        public string Encrypt(string connectstring, string password)
        {
            this._connectionString = connectstring;
            if (this._connection == null)
            {
                this._connection = new SqlConnection();
            }
            return MyEncryption.Encrypt(connectstring, password, true);
        }

        public DataSet ExecuteDataSet(string commandText)
        {
            return this.ExecuteDataSet(commandText, null, null);
        }

        public DataSet ExecuteDataSet(SqlConnection myConnection, string commandText)
        {
            return this.ExecuteDataSet(myConnection, commandText, null, null);
        }

        public DataSet ExecuteDataSet(SqlTransaction myTransaction, string commandText)
        {
            return this.ExecuteDataSet(myTransaction, commandText, null, null);
        }

        public DataSet ExecuteDataSet(string commandText, string[] myParams, object[] myValues)
        {
            DataSet set = null;
            if (this.m_useTransaction)
            {
                this.Result = this.Start();
                if (this.Result != "OK")
                {
                    this.RollBack();
                    return set;
                }
                set = this.ExecuteDataSet(this._transaction, commandText, myParams, myValues);
                if (this.Result == "OK")
                {
                    this.Commit();
                }
                return set;
            }
            if (this.Open() == "OK")
            {
                set = this.ExecuteDataSet(this._connection, commandText, myParams, myValues);
            }
            this.Close();
            return set;
        }

        public DataSet ExecuteDataSet(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteDataSet(myConnection, this.CommandType, commandText, myParams, myValues);
        }

        public DataSet ExecuteDataSet(SqlTransaction myTransaction, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteDataSet(myTransaction, this.CommandType, commandText, myParams, myValues);
        }

        public DataSet ExecuteDataSet(SqlConnection myConnection, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myConnection == null)
            {
                throw new SqlHelperException("myConnection");
            }
            if (myConnection.State != ConnectionState.Open)
            {
                throw new SqlHelperException("Connnection is close");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new SqlHelperException("commandText");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            this.Result = "";
            DataSet dataSet = new DataSet();
            SqlCommand selectCommand = myConnection.CreateCommand();
            selectCommand.CommandText = commandText;
            selectCommand.Connection = myConnection;
            selectCommand.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    selectCommand.Parameters.Add(parameter);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            try
            {
                this.Rowaffected = adapter.Fill(dataSet);
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return dataSet;
            }
            catch (SqlException exception)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception.Message, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.Close(myConnection);
                this.Result = _HideError ? ("Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception2.Message) : exception2.Message;
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return dataSet;
        }

        public DataSet ExecuteDataSet(SqlTransaction myTransaction, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myTransaction == null)
            {
                throw new SqlHelperException("myTransaction");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new SqlHelperException("commandText");
            }
            if (myTransaction.Connection == null)
            {
                throw new SqlHelperException("The transaction was rollbacked or commited, please provide an open transaction.");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            this.Result = "";
            DataSet dataSet = new DataSet();
            SqlCommand selectCommand = myTransaction.Connection.CreateCommand();
            selectCommand.CommandText = commandText;
            selectCommand.Connection = myTransaction.Connection;
            selectCommand.Transaction = myTransaction;
            selectCommand.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    selectCommand.Parameters.Add(parameter);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            try
            {
                this.Rowaffected = adapter.Fill(dataSet);
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return dataSet;
            }
            catch (SqlException exception)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception.Message, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.RollBack(myTransaction);
                this.Result = _HideError ? ("Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception2.Message) : exception2.Message;
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return dataSet;
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            return this.ExecuteDataTable(commandText, null, null);
        }

        public DataTable ExecuteDataTable(SqlConnection myConnection, string commandText)
        {
            return this.ExecuteDataTable(myConnection, commandText, null, null);
        }

        public DataTable ExecuteDataTable(SqlTransaction myTransaction, string commandText)
        {
            return this.ExecuteDataTable(myTransaction, commandText, null, null);
        }

        public DataTable ExecuteDataTable(string commandText, string[] myParams, object[] myValues)
        {
            DataTable table = null;
            if (this.m_useTransaction)
            {
                this.Result = this.Start();
                if (this.Result != "OK")
                {
                    this.RollBack();
                    return table;
                }
                table = this.ExecuteDataTable(this._transaction, commandText, myParams, myValues);
                if (this.Result == "OK")
                {
                    this.Commit();
                }
                return table;
            }
            if (this.Open() == "OK")
            {
                table = this.ExecuteDataTable(this._connection, commandText, myParams, myValues);
            }
            this.Close();
            return table;
        }

        public DataTable ExecuteDataTable(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteDataTable(myConnection, this.CommandType, commandText, myParams, myValues);
        }

        public DataTable ExecuteDataTable(SqlTransaction myTransaction, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteDataTable(myTransaction, this.CommandType, commandText, myParams, myValues);
        }

        public DataTable ExecuteDataTable(SqlConnection myConnection, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myConnection == null)
            {
                throw new SqlHelperException("myConnection");
            }
            if (myConnection.State != ConnectionState.Open)
            {
                throw new SqlHelperException("Connnection is close");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new SqlHelperException("commandText");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            this.Result = "";
            DataTable dataTable = new DataTable();
            SqlCommand selectCommand = myConnection.CreateCommand();
            selectCommand.CommandText = commandText;
            selectCommand.Connection = myConnection;
            selectCommand.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    selectCommand.Parameters.Add(parameter);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            try
            {
                this.Rowaffected = adapter.Fill(dataTable);
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return dataTable;
            }
            catch (InvalidOperationException exception)
            {
                this.Close(myConnection);
                this.Result = _HideError ? ("Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception.Message) : exception.Message;
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            catch (SqlException exception2)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = this.DispError(exception2.Number, "Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception2.Message, commandText);
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception2.Number));
            }
            catch (Exception exception3)
            {
                this.Close(myConnection);
                this.Result = _HideError ? ("Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception3.Message) : exception3.Message;
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return dataTable;
        }

        public DataTable ExecuteDataTable(SqlTransaction myTransaction, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myTransaction == null)
            {
                throw new SqlHelperException("myTransaction");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText");
            }
            if ((myTransaction != null) && (myTransaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
            }
            this.Result = "";
            DataTable dataTable = new DataTable();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.CommandText = commandText;
            selectCommand.Connection = myTransaction.Connection;
            selectCommand.Transaction = myTransaction;
            selectCommand.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    selectCommand.Parameters.Add(parameter);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            try
            {
                this.Rowaffected = adapter.Fill(dataTable);
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return dataTable;
            }
            catch (InvalidOperationException exception)
            {
                this.RollBack(myTransaction);
                this.Result = _HideError ? ("Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception.Message) : exception.Message;
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            catch (SqlException exception2)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = this.DispError(exception2.Number, "Kh\x00f4ng thể được lấy dữ liệu.\nChi tiết:\n\t" + exception2.Message, commandText);
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception2.Number));
            }
            catch (Exception exception3)
            {
                this.RollBack(myTransaction);
                this.Result = _HideError ? ("Không thể được lấy dữ liệu.\nChi tiết:\n\t" + exception3.Message) : exception3.Message;
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return dataTable;
        }

        public string ExecuteNonQuery(StringCollection commandTexts)
        {
            return this.ExecuteNonQuery(commandTexts, false);
        }

        public string ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(commandText, null, null);
        }

        public string ExecuteNonQuery(string[] commandTexts)
        {
            this.Result = "Không thực hiện được";
            if (this.Open() == "OK")
            {
                this.Result = this.Start();
                if (this.Result != "OK")
                {
                    this.RollBack();
                    return this.Result;
                }
                for (int i = 0; i < commandTexts.Length; i++)
                {
                    this.Result = this.ExecuteNonQuery(this._transaction, commandTexts[i]);
                    if (this.Result != "OK")
                    {
                        this.RollBack();
                        return this.Result;
                    }
                }
                this.Commit();
            }
            return this.Result;
        }

        public string ExecuteNonQuery(StringCollection commandTexts, bool isTransaction)
        {
            this.Result = "Không thực hiện được";
            if (commandTexts == null)
            {
                throw new SqlHelperException("Null object", 0);
            }
            if (commandTexts.Count == 0)
            {
                return "OK";
            }
            if (this.Open() == "OK")
            {
                if (isTransaction)
                {
                    this.Result = this.Start();
                    if (this.Result != "OK")
                    {
                        this.RollBack();
                        return this.Result;
                    }
                    for (int i = 0; i < commandTexts.Count; i++)
                    {
                        this.Result = this.ExecuteNonQuery(this._transaction, commandTexts[i]);
                        if (this.Result != "OK")
                        {
                            this.RollBack();
                            return this.Result;
                        }
                    }
                    this.Commit();
                }
                else
                {
                    for (int j = 0; j < commandTexts.Count; j++)
                    {
                        this.Result = this.ExecuteNonQuery(this._connection, commandTexts[j]);
                        if (this.Result != "OK")
                        {
                            return this.Result;
                        }
                    }
                }
            }
            return this.Result;
        }

        public string ExecuteNonQuery(SqlConnection myConnection, string commandText)
        {
            return this.ExecuteNonQuery(myConnection, commandText, null, null);
        }

        public string ExecuteNonQuery(SqlTransaction myTransaction, string[] commandTexts)
        {
            string str = "Không thực hiện được";
            for (int i = 0; i < commandTexts.Length; i++)
            {
                str = this.ExecuteNonQuery(myTransaction, commandTexts[i]);
                if (str != "OK")
                {
                    this.RollBack(myTransaction);
                    return str;
                }
            }
            return str;
        }

        public string ExecuteNonQuery(SqlTransaction myTransaction, string commandText)
        {
            return this.ExecuteNonQuery(myTransaction, commandText, null, null);
        }

        public string ExecuteNonQuery(SqlTransaction myTransaction, CommandType commandType, StringCollection commandTexts)
        {
            string commandText = "OK";
            for (int i = 0; i < commandTexts.Count; i++)
            {
                this.CommandType = commandType;
                commandText = commandTexts[i];
                if (!(commandText == ""))
                {
                    commandText = this.ExecuteNonQuery(myTransaction, commandText);
                    if (commandText != "OK")
                    {
                        this.RollBack(myTransaction);
                        return commandText;
                    }
                }
            }
            return commandText;
        }

        public string ExecuteNonQuery(string commandText, string[] myParams, object[] myValues)
        {
            if (this.m_useTransaction)
            {
                this.Result = this.Start();
                if (this.Result != "OK")
                {
                    this.RollBack();
                    return this.Result;
                }
                this.Result = this.ExecuteNonQuery(this._transaction, commandText, myParams, myValues);
                if (this.Result == "OK")
                {
                    this.Result = this.Commit();
                }
            }
            else
            {
                this.Result = this.Open();
                if (this.Result == "OK")
                {
                    this.Result = this.ExecuteNonQuery(this._connection, commandText, myParams, myValues);
                }
                this.Close();
            }
            return this.Result;
        }

        public string ExecuteNonQuery(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteNonQuery(myConnection, this.CommandType, commandText, myParams, myValues);
        }

        public string ExecuteNonQuery(SqlTransaction myTransaction, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteNonQuery(myTransaction, this.CommandType, commandText, myParams, myValues);
        }

        public string ExecuteNonQuery(string connectionString, string commandText, string[] myParams, object[] myValues)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }
            this._connectionString = connectionString;
            return this.ExecuteNonQuery(commandText, myParams, myValues);
        }

        public string ExecuteNonQuery(SqlConnection myConnection, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            this.Result = "";
            if (myConnection == null)
            {
                throw new SqlHelperException("Connection is null");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new SqlHelperException("commandText");
            }
            if (myConnection.State != ConnectionState.Open)
            {
                throw new SqlHelperException("Connnection is close");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            SqlCommand command = myConnection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = myConnection;
            command.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                this.Rowaffected = command.ExecuteNonQuery();
                this.Result = "OK";
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return this.Result;
            }
            catch (SqlException exception)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Không thể thực hiện tác vụ này.\nChi tiết:\n\t" + exception.Message, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng thể thực hiện t\x00e1c vụ n\x00e0y.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return this.Result;
        }

        public string ExecuteNonQuery(SqlTransaction myTransaction, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myTransaction == null)
            {
                throw new SqlHelperException("Transaction is null");
            }
            if (commandText == null)
            {
                throw new SqlHelperException("commandText");
            }
            if ((myTransaction != null) && (myTransaction.Connection == null))
            {
                throw new SqlHelperException("The transaction was rollbacked or commited, please provide an open transaction.");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            SqlCommand command = myTransaction.Connection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = myTransaction.Connection;
            command.Transaction = myTransaction;
            command.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                this.Rowaffected = command.ExecuteNonQuery();
                this.Result = "OK";
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return this.Result;
            }
            catch (SqlException exception)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Không thể thực hiện tác vụ này.\nChi tiết:\n\t" + exception.Message, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = "Không thể thực hiện tác vụ này.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return this.Result;
        }

        public SqlDataReader ExecuteReader(string commandText)
        {
            return this.ExecuteReader(commandText, null, null);
        }

        public SqlDataReader ExecuteReader(SqlConnection myConnection, string commandText)
        {
            return this.ExecuteReader(myConnection, commandText, null, null);
        }

        public SqlDataReader ExecuteReader(SqlTransaction myTransaction, string commandText)
        {
            return this.ExecuteReader(myTransaction, commandText, null, null);
        }

        public SqlDataReader ExecuteReader(string commandText, string[] myParams, object[] myValues)
        {
            SqlDataReader reader = null;
            if (this.m_useTransaction)
            {
                this.Result = this.Start();
                if (this.Result != "OK")
                {
                    this.RollBack();
                    return reader;
                }
                reader = this.ExecuteReader(this._transaction, commandText, myParams, myValues);
                if (this.Result == "OK")
                {
                    this.Commit();
                }
                return reader;
            }
            if (this.Open() == "OK")
            {
                reader = this.ExecuteReader(this._connection, commandText, myParams, myValues);
            }
            return reader;
        }

        public SqlDataReader ExecuteReader(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteReader(myConnection, this.CommandType, commandText, myParams, myValues);
        }

        public SqlDataReader ExecuteReader(SqlTransaction myTransaction, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteReader(myTransaction, this.CommandType, commandText, myParams, myValues);
        }

        public SqlDataReader ExecuteReader(SqlConnection myConnection, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myConnection == null)
            {
                throw new SqlHelperException("myConnection");
            }
            if (myConnection.State != ConnectionState.Open)
            {
                throw new SqlHelperException("Connnection is close");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new SqlHelperException("commandText");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            SqlCommand command = myConnection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = myConnection;
            command.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return reader;
            }
            catch (SqlException exception)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Không lấy được dữ liệu.\nChi tiết:\n\t" + exception.Message, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = "Không lấy được dữ liệu.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return null;
        }

        public SqlDataReader ExecuteReader(SqlTransaction myTransaction, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myTransaction == null)
            {
                throw new SqlHelperException("myTransaction");
            }
            if (commandText == null)
            {
                throw new SqlHelperException("commandText");
            }
            if ((myTransaction != null) && (myTransaction.Connection == null))
            {
                throw new SqlHelperException("The transaction was rollbacked or commited, please provide an open transaction.");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
            }
            SqlCommand command = myTransaction.Connection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = myTransaction.Connection;
            command.Transaction = myTransaction;
            command.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return reader;
            }
            catch (SqlException exception)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Không lấy được dữ liệu.\nChi tiết:\n\t" + exception.Message, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng lấy được dữ liệu.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return null;
        }

        public object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(commandText, null, null);
        }

        public object ExecuteScalar(SqlConnection myConnection, string commandText)
        {
            return this.ExecuteScalar(myConnection, commandText, null, null);
        }

        public object ExecuteScalar(SqlTransaction myTransaction, string commandText)
        {
            return this.ExecuteScalar(myTransaction, commandText, null, null);
        }

        public decimal ExecuteScalar(string commandText, decimal defautValue)
        {
            object obj2 = this.ExecuteScalar(commandText);
            if (obj2 != null)
            {
                return Convert.ToDecimal(obj2);
            }
            return defautValue;
        }

        public double ExecuteScalar(string commandText, double defautValue)
        {
            object obj2 = this.ExecuteScalar(commandText);
            if (obj2 != null)
            {
                return Convert.ToDouble(obj2);
            }
            return defautValue;
        }

        public int ExecuteScalar(string commandText, int defautValue)
        {
            object obj2 = this.ExecuteScalar(commandText);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return defautValue;
        }

        public string ExecuteScalar(string commandText, string defautValue)
        {
            object obj2 = this.ExecuteScalar(commandText);
            if (obj2 != null)
            {
                return Convert.ToString(obj2);
            }
            return defautValue;
        }

        public double ExecuteScalar(SqlConnection myConnection, string commandText, double defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText);
            if (obj2 != null)
            {
                return Convert.ToDouble(obj2);
            }
            return defaultValue;
        }

        public int ExecuteScalar(SqlConnection myConnection, string commandText, int defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return defaultValue;
        }

        public string ExecuteScalar(SqlConnection myConnection, string commandText, string defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText);
            if (obj2 != null)
            {
                return Convert.ToString(obj2);
            }
            return defaultValue;
        }

        public double ExecuteScalar(SqlTransaction myTransaction, string commandText, double defautValue)
        {
            return this.ExecuteScalar(myTransaction, commandText, (string[])null, (object[])null, defautValue);
        }

        public int ExecuteScalar(SqlTransaction myTransaction, string commandText, int defautValue)
        {
            return this.ExecuteScalar(myTransaction, commandText, (string[])null, (object[])null, defautValue);
        }

        public string ExecuteScalar(SqlTransaction myTransaction, string commandText, string defautValue)
        {
            return this.ExecuteScalar(myTransaction, commandText, (string[])null, (object[])null, defautValue);
        }

        public object ExecuteScalar(string commandText, string[] myParams, object[] myValues)
        {
            object obj2 = null;
            if (this.m_useTransaction)
            {
                this.Result = this.Start();
                if (this.Result != "OK")
                {
                    this.RollBack();
                    return obj2;
                }
                obj2 = this.ExecuteScalar(this._transaction, commandText, myParams, myValues);
                if (this.Result == "OK")
                {
                    this.Commit();
                }
                return obj2;
            }
            if (this.Open() == "OK")
            {
                obj2 = this.ExecuteScalar(this._connection, commandText, myParams, myValues);
            }
            this.Close();
            return obj2;
        }

        public object ExecuteScalar(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteScalar(myConnection, this.CommandType, commandText, myParams, myValues);
        }

        public object ExecuteScalar(SqlTransaction myTransaction, string commandText, string[] myParams, object[] myValues)
        {
            return this.ExecuteScalar(myTransaction, this.CommandType, commandText, myParams, myValues);
        }

        public decimal ExecuteScalar(string commandText, string[] myParams, object[] myValues, decimal defaultValue)
        {
            object obj2 = this.ExecuteScalar(commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToDecimal(obj2);
            }
            return defaultValue;
        }

        public double ExecuteScalar(string commandText, string[] myParams, object[] myValues, double defaultValue)
        {
            object obj2 = this.ExecuteScalar(commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToDouble(obj2);
            }
            return defaultValue;
        }

        public int ExecuteScalar(string commandText, string[] myParams, object[] myValues, int defaultValue)
        {
            object obj2 = this.ExecuteScalar(commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return defaultValue;
        }

        public string ExecuteScalar(string commandText, string[] myParams, object[] myValues, string defaultValue)
        {
            object obj2 = this.ExecuteScalar(commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToString(obj2);
            }
            return defaultValue;
        }

        private object ExecuteScalar(SqlConnection myConnection, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myConnection == null)
            {
                throw new SqlHelperException("myConnection");
            }
            if (string.IsNullOrEmpty(commandText))
            {
                throw new SqlHelperException("commandText");
            }
            if (myConnection.State != ConnectionState.Open)
            {
                throw new SqlHelperException("Connnection is close");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new SqlHelperException("Parameter count does not match Parameter Value count.");
                }
            }
            object obj2 = null;
            this.Result = "";
            SqlCommand command = myConnection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = myConnection;
            command.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                obj2 = command.ExecuteScalar();
                if ((obj2 != null) && (obj2 == DBNull.Value))
                {
                    obj2 = null;
                }
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return obj2;
            }
            catch (SqlException exception)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = "Không lấy được dữ liệu.\nChi tiết:\n\t" + exception.Message;
                    this.Result = this.DispError(exception.Number, this.Result, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.Close(myConnection);
                if (_HideError)
                {
                    this.Result = "Không lấy được dữ liệu.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return obj2;
        }

        public decimal ExecuteScalar(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues, decimal defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToDecimal(obj2);
            }
            return defaultValue;
        }

        public double ExecuteScalar(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues, double defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToDouble(obj2);
            }
            return defaultValue;
        }

        public int ExecuteScalar(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues, int defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return defaultValue;
        }

        public string ExecuteScalar(SqlConnection myConnection, string commandText, string[] myParams, object[] myValues, string defaultValue)
        {
            object obj2 = this.ExecuteScalar(myConnection, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToString(obj2);
            }
            return defaultValue;
        }

        public object ExecuteScalar(SqlTransaction myTransaction, CommandType commandType, string commandText, string[] myParams, object[] myValues)
        {
            if (myTransaction == null)
            {
                throw new SqlHelperException("myTransaction");
            }
            if (commandText == null)
            {
                throw new SqlHelperException("commandText");
            }
            if ((myTransaction != null) && (myTransaction.Connection == null))
            {
                throw new SqlHelperException("The transaction was rollbacked or commited, please provide an open transaction.");
            }
            bool flag = false;
            if ((myParams != null) & (myValues != null))
            {
                flag = true;
                if (myParams.Length != myValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
            }
            this.Result = "";
            object obj2 = null;
            SqlCommand command = myTransaction.Connection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = myTransaction.Connection;
            command.Transaction = myTransaction;
            command.CommandType = commandType;
            if (flag)
            {
                for (int i = 0; i < myParams.Length; i++)
                {
                    SqlParameter parameter = new SqlParameter(myParams[i], myValues[i]);
                    command.Parameters.Add(parameter);
                }
            }
            try
            {
                obj2 = command.ExecuteScalar();
                if ((obj2 != null) && (obj2 == DBNull.Value))
                {
                    obj2 = null;
                }
                if (this.AutoCommand)
                {
                    this.CommandType = CommandType.StoredProcedure;
                }
                return obj2;
            }
            catch (SqlException exception)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng lấy được dữ liệu.\nChi tiết:\n\t" + exception.Message;
                    this.Result = this.DispError(exception.Number, this.Result, commandText);
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                this.RollBack(myTransaction);
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng lấy được dữ liệu.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return obj2;
        }

        public decimal ExecuteScalar(SqlTransaction myTraTransaction, string commandText, string[] myParams, object[] myValues, decimal defaultValue)
        {
            object obj2 = this.ExecuteScalar(myTraTransaction, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToDecimal(obj2);
            }
            return defaultValue;
        }

        public double ExecuteScalar(SqlTransaction myTraTransaction, string commandText, string[] myParams, object[] myValues, double defaultValue)
        {
            object obj2 = this.ExecuteScalar(myTraTransaction, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToDouble(obj2);
            }
            return defaultValue;
        }

        public int ExecuteScalar(SqlTransaction myTraTransaction, string commandText, string[] myParams, object[] myValues, int defaultValue)
        {
            object obj2 = this.ExecuteScalar(myTraTransaction, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return defaultValue;
        }

        public string ExecuteScalar(SqlTransaction myTraTransaction, string commandText, string[] myParams, object[] myValues, string defaultValue)
        {
            object obj2 = this.ExecuteScalar(myTraTransaction, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToString(obj2);
            }
            return defaultValue;
        }

        public long ExecuteScalar2(SqlTransaction myTransaction, string commandText, int defautValue)
        {
            return this.ExecuteScalar2(myTransaction, commandText, null, null, defautValue);
        }

        public long ExecuteScalar2(string commandText, string[] myParams, object[] myValues, int defaultValue)
        {
            object obj2 = this.ExecuteScalar(commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToInt64(obj2);
            }
            return (long)defaultValue;
        }

        public long ExecuteScalar2(SqlTransaction myTraTransaction, string commandText, string[] myParams, object[] myValues, int defaultValue)
        {
            object obj2 = this.ExecuteScalar(myTraTransaction, commandText, myParams, myValues);
            if (obj2 != null)
            {
                return Convert.ToInt64(obj2);
            }
            return (long)defaultValue;
        }

        public string Exists(string expression)
        {
            this.Result = "Cơ sở dữ liệu đã tồn tại.";
            string commandText = "DECLARE @return_value int if EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + expression + "') set @return_value =1 else set @return_value =0 select @return_value";
            this.CommandType = CommandType.Text;
            object obj2 = this.ExecuteScalar(commandText);
            if ((obj2 != Convert.DBNull) && (Convert.ToInt32(obj2) == 0))
            {
                this.Result = "OK";
            }
            return this.Result;
        }

        public void Extract()
        {
            this._authentication = IsAuthe(this._connectionString);
            if (!this._authentication)
            {
                this._server = GetItem("server=", this._connectionString);
                this._database = GetItem("database=", this._connectionString);
                this._userid = GetItem("user id=", this._connectionString);
                this._password = GetItem("password=", this._connectionString);
            }
            else
            {
                this._server = GetItem("data source=", this._connectionString);
                this._database = GetItem("initial catalog=", this._connectionString);
            }
        }

        ~SqlHelper()
        {
            Count--;
            this.Close();
            if ((this._transaction != null) && (this._transaction.Connection != null))
            {
                try
                {
                    if (this._transaction.Connection.State == ConnectionState.Open)
                    {
                        this._transaction.Connection.Close();
                    }
                    this._transaction.Rollback();
                    this._transaction = null;
                }
                catch (Exception)
                {
                }
            }
        }

        public static string GenCode(string fKey)
        {
            return GenCode("TRANS_REF", "RefID", fKey);
        }

        public static string GenCode(SqlHelper mySql, string fKey)
        {
            return GenCode(mySql, "TRANS_REF", "RefID", fKey);
        }

        public static string GenCode(SqlTransaction myTransaction, string fKey)
        {
            return GenCode(myTransaction, "TRANS_REF", "RefID", fKey);
        }

        public static string GenCode(string tableName, string columnName, string fKey)
        {
            SqlHelper helper = new SqlHelper();
            helper.CommandType = CommandType.Text;
            string expression = "";
            object obj2 = helper.ExecuteScalar("select max([" + columnName + "]) from [" + tableName + "] where [" + columnName + "] like N'" + fKey + "%'");
            expression = (obj2 == null) ? "0" : obj2.ToString();
            if (fKey.Length != 0)
            {
                expression = expression.Replace(fKey, "").Trim();
            }
            int num = 0;
            if (IsNumeric(expression))
            {
                num = (int)Conversion.Val(expression);
            }
            num++;
            string str2 = num.ToString();
            while (str2.Length < 6)
            {
                str2 = "0" + str2;
            }
            return (fKey + str2);
        }

        public static string GenCode(SqlHelper mySql, string tableName, string columnName, string fKey)
        {
            return GenCode(mySql.Transaction, tableName, columnName, fKey);
        }

        public static string GenCode(SqlTransaction myTransaction, string tableName, string columnName, string fKey)
        {
            if (myTransaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((myTransaction != null) && (myTransaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlHelper helper = new SqlHelper();
            helper.CommandType = CommandType.Text;
            object obj2 = helper.ExecuteScalar(myTransaction, "select max(" + columnName + ") from [" + tableName + "] where [" + columnName + "] like N'" + fKey + "%'");
            string expression = (obj2 == null) ? "0" : obj2.ToString();
            if (fKey.Length != 0)
            {
                expression = expression.Replace(fKey, "").Trim();
            }
            int num = 0;
            if (Information.IsNumeric(expression))
            {
                num = (int)Conversion.Val(expression);
            }
            num++;
            string str2 = num.ToString();
            while (str2.Length < 6)
            {
                str2 = "0" + str2;
            }
            return (fKey + str2);
        }

        private static string GetItem(string getStr, string data)
        {
            string message = "";
            if (string.IsNullOrEmpty(data))
            {
                message = _HideError ? "Chưa cấu h\x00ecnh chuỗi kết nối." : "Connecttion String is fail.";
                throw new SqlHelperException(message);
            }
            string str2 = data.ToLower();
            int index = str2.IndexOf(getStr);
            if (index == -1)
            {
                return string.Empty;
            }
            index += getStr.Length;
            int length = str2.IndexOf(';', index) - index;
            return data.Substring(index, length).Trim();
        }

        private static bool IsAuthe(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                string str;
                if (_HideError)
                {
                    str = "Chưa cấu hình chuỗi kết nối.";
                }
                else
                {
                    str = "Connecttion String is fail.";
                }
                throw new SqlHelperException(str);
            }
            return (data.ToLower().IndexOf("data source=") != -1);
        }

        public static bool IsNumeric(object expression)
        {
            double num;
            return double.TryParse(Convert.ToString(expression), NumberStyles.Any, (IFormatProvider)NumberFormatInfo.InvariantInfo, out num);
        }

        [Obsolete]
        public static void LoadConnectString()
        {
            if ((ConfigurationSettings.AppSettings != null) && (ConfigurationSettings.AppSettings["connectionString"] != null))
            {
                _GlobalConnectString = ConfigurationSettings.AppSettings["connectionString"].ToString();
            }
           
        }

        public string Open()
        {
            this.RaiseConnectEventHander();
            if (string.IsNullOrEmpty(this._connectionString))
            {
                throw new SqlHelperException("connectionString");
            }
            if (this._connection == null)
            {
                this._connection = new SqlConnection(this._connectionString);
            }
            return this.Open(this._connection);
        }

        public string Open(SqlConnection myConnection)
        {
            if (myConnection == null)
            {
                throw new SqlHelperException("myConnection");
            }
            this.RaiseConnectEventHander();
            if (myConnection.State != ConnectionState.Open)
            {
                try
                {
                    if (string.IsNullOrEmpty(this._connectionString))
                    {
                        throw new SqlHelperException("_connectionString");
                    }
                    myConnection.ConnectionString = this._connectionString;
                    myConnection.Open();
                    this.RaiseConnectedEventHander(myConnection);
                    this.Result = "OK";
                    return this.Result;
                }
                catch (SqlException exception)
                {
                    if (_HideError)
                    {
                        this.Result = "Không thể kết nối với máy chủ.\nChi tiết:\n\t" + exception.Message;
                        this.Result = this.DispError(exception.Number, this.Result, "");
                    }
                    else
                    {
                        this.Result = exception.Message;
                    }
                    this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
                }
                catch (Exception exception2)
                {
                    if (_HideError)
                    {
                        this.Result = "Không thể kết nối với máy chủ.\nChi tiết:\n\t" + exception2.Message;
                        this.Result = this.DispError(-1, this.Result, "");
                    }
                    else
                    {
                        this.Result = exception2.Message;
                    }
                    this.RaiseErrorEventHander(new SqlHelperException(this.Result));
                }
            }
            return this.Result;
        }

        private void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        private void RaiseConnectedEventHander(SqlConnection e)
        {
            if (this.Connected != null)
            {
                this.Connected(this, e);
            }
        }

        private void RaiseConnectEventHander()
        {
            if (this.Connect != null)
            {
                this.Connect(this);
            }
        }

        private void RaiseErrorEventHander(SqlHelperException e)
        {
            if (this.Error != null)
            {
                this.Error(this, e);
            }
        }

        private void RaiseTransactionClosedEventHander()
        {
            if (this.TransactionClosed != null)
            {
                this.TransactionClosed(this);
            }
        }

        private void RaiseTransactionCompletedEventHander(TransactionEventArgs e)
        {
            if (this.TransactionCompleted != null)
            {
                this.TransactionCompleted(this, e);
            }
        }

        private void RaiseTransactionEventHander()
        {
            if (this.TransactionStart != null)
            {
                this.TransactionStart(this);
            }
        }

        private void RaiseTransactionStartedEventHander(TransactionEventArgs e)
        {
            if (this.TransactionStarted != null)
            {
                this.TransactionStarted(this, e);
            }
        }

        public string RebuildConnectionString()
        {
            return this.RebuildConnectionString(this._server, this._database, this._userid, this._password, this._authentication);
        }

        public string RebuildConnectionString(string server)
        {
            return this.RebuildConnectionString(server, "", "", "", true);
        }

        public string RebuildConnectionString(string server, string database)
        {
            return this.RebuildConnectionString(server, database, "", "", true);
        }

        public string RebuildConnectionString(string server, string user, string password)
        {
            return this.RebuildConnectionString(server, "", user, password, false);
        }

        public string RebuildConnectionString(string server, string database, string user, string password, bool authentication)
        {
            if (!authentication)
            {
                if (database != "")
                {
                    return ("server=" + server + ";database=" + database + ";user id= " + user + " ;password=" + password + ";");
                }
                return ("server=" + server + ";database=master;user id= " + user + " ;password=" + password + ";");
            }
            if (database != "")
            {
                return ("Data Source=" + server + ";Initial Catalog=" + database + ";Integrated Security=True;");
            }
            return ("Data Source=" + server + ";Initial Catalog=master;Integrated Security=True;");
        }

        public string RollBack()
        {
            return this.RollBack(this._transaction);
        }

        public string RollBack(SqlTransaction myTransaction)
        {
            this.Result = "";
            if (this._transaction == null)
            {
                if (_HideError)
                {
                    this.Result = "Giao dịch chưa được khởi tạo.";
                }
                else
                {
                    this.Result = "Don't has Transaction created.";
                }
                this.Close();
                return this.Result;
            }
            try
            {
                if (myTransaction.Connection != null)
                {
                    myTransaction.Rollback();
                }
                myTransaction.Dispose();
                myTransaction = null;
                this.Result = "OK";
                return this.Result;
            }
            catch (SqlException exception)
            {
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng thể kh\x00f4i phục giao dịch.\nChi tiết:\n\t" + exception.Message;
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng thể kh\x00f4i phục giao dịch.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            finally
            {
                this.Close();
            }
            return this.Result;
        }

        public void SaveMyConfig()
        {
            string str;
            MyConfig config = new MyConfig();
            if (!this._authentication)
            {
                str = "server=" + this._server + ";database=" + this._database + ";user id= " + this._userid + " ;password=" + this._password + ";";
                config.SetValue("//appSettings//add[@key='connectionString']", str);
                str = "server=" + this._server + ";database=Master;user id= " + this._userid + " ;password=" + this._password + ";";
                config.SetValue("//appSettings//add[@key='ConnectionRestore']", str);
            }
            else
            {
                str = "Data Source=" + this._server + ";Initial Catalog=" + this._database + ";Integrated Security=True;";
                config.SetValue("//appSettings//add[@key='connectionString']", str);
                str = "Data Source=" + this._server + ";Initial Catalog=Master;Integrated Security=True;";
                config.SetValue("//appSettings//add[@key='ConnectionRestore']", str);
            }
        }

        public string Start()
        {
            this.Result = this.Open();
            if (this.Result != "OK")
            {
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
                return this.Result;
            }
            try
            {
                this.RaiseTransactionEventHander();
                this._transaction = this._connection.BeginTransaction();
                TransactionEventArgs e = new TransactionEventArgs();
                this.RaiseTransactionStartedEventHander(e);
                this.Result = "OK";
                return this.Result;
            }
            catch (SqlException exception)
            {
                if (_HideError)
                {
                    this.Result = this.DispError(exception.Number, "Kh\x00f4ng thể khởi tạo giao dịch.\nChi tiết:\n\t" + exception.Message, "");
                }
                else
                {
                    this.Result = exception.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result, exception.Number));
            }
            catch (Exception exception2)
            {
                if (_HideError)
                {
                    this.Result = "Kh\x00f4ng thể khởi tạo giao dịch.\nChi tiết:\n\t" + exception2.Message;
                }
                else
                {
                    this.Result = exception2.Message;
                }
                this.RaiseErrorEventHander(new SqlHelperException(this.Result));
            }
            return this.Result;
        }

        public override string ToString()
        {
            return this._connectionString;
        }

        // Properties
        public bool AutoCommand
        {
            get
            {
                return this._autoCommand;
            }
            set
            {
                this._autoCommand = value;
            }
        }

        public bool Authentication
        {
            get
            {
                return this._authentication;
            }
            set
            {
                this._authentication = value;
            }
        }

        public string CommandText
        {
            get
            {
                return this._commandText;
            }
            set
            {
                this._commandText = value;
            }
        }

        public CommandType CommandType
        {
            get
            {
                return this._commandType;
            }
            set
            {
                this._commandType = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        public static string ConnectString
        {
            get
            {
                return _GlobalConnectString;
            }
            set
            {
                _GlobalConnectString = value;
            }
        }

        public static int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }

        public string Database
        {
            get
            {
                return this._database;
            }
            set
            {
                this._database = value;
            }
        }

        public static bool HideError
        {
            get
            {
                return _HideError;
            }
            set
            {
                _HideError = value;
            }
        }

        public bool IsConnection
        {
            get
            {
                if (this._connection.State != ConnectionState.Open)
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsExtract
        {
            get
            {
                return this._isextract;
            }
            set
            {
                this._isextract = value;
                if (this._isextract)
                {
                    this.Extract();
                }
            }
        }

        public bool IsFormatMessage
        {
            get
            {
                return this._isFormatMessage;
            }
            set
            {
                this._isFormatMessage = value;
            }
        }

        public bool MustCloseConnection
        {
            get
            {
                return this._mustCloseConnection;
            }
            set
            {
                this._mustCloseConnection = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string Result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }

        public int Rowaffected
        {
            get
            {
                return this.m_rowaffected;
            }
            set
            {
                this.m_rowaffected = value;
            }
        }

        public string Server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
            }
        }

        public static string ServerConnectString
        {
            get
            {
                string item;
                if (!(_serverConnectionString == ""))
                {
                    return _serverConnectionString;
                }
                if (!IsAuthe(_GlobalConnectString))
                {
                    item = GetItem("server=", _GlobalConnectString);
                    string str2 = GetItem("user id=", _GlobalConnectString);
                    string str3 = GetItem("password=", _GlobalConnectString);
                    return ("server=" + item + ";user id= " + str2 + " ;password=" + str3 + ";");
                }
                item = GetItem("data source=", _GlobalConnectString);
                return ("Data Source=" + item + ";Initial Catalog=master;Integrated Security=True;");
            }
            set
            {
                _serverConnectionString = value;
            }
        }

        public SqlHelperException SqlHelperException
        {
            get
            {
                return this.SqlHelperException1;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return this._transaction;
            }
        }

        public string UserID
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public bool UseTransaction
        {
            get
            {
                return this.m_useTransaction;
            }
            set
            {
                this.m_useTransaction = value;
            }
        }

        public SqlHelperException SqlHelperException1 { get => _sqlHelperException; set => _sqlHelperException = value; }

        // Nested Types
        public delegate void ConnectedEventHander(object sender, SqlConnection e);

        public delegate void ConnectEventHander(object sender);

        public delegate void ErrorEventHander(object sender, SqlHelperException e);

        public delegate void Execute(object sender, int Percent);

        public delegate void Executed(object sender);

        public delegate void ExecuteStart(object sender);

        public delegate void TransactionClosedEventHander(object sender);

        public delegate void TransactionEventHander(object sender);
    }
    public class SqlHelperException : DbException
    {
        // Fields
        private int _lineError;
        private readonly string _message;
        private readonly int _numberError;

        // Methods
        public SqlHelperException(string message)
        {
            this._message = "";
            this._numberError = -1;
            this._lineError = -1;
            this._message = message;
            this._numberError = -1;
        }

        public SqlHelperException(string message, int number)
        {
            this._message = "";
            this._numberError = -1;
            this._lineError = -1;
            this._message = message;
            this._numberError = number;
        }

        public SqlHelperException(string message, int number, int lineError)
        {
            this._message = "";
            this._numberError = -1;
            this._lineError = -1;
            this._message = message;
            this._numberError = number;
            this._lineError = lineError;
        }

        // Properties
        public int LineError
        {
            get
            {
                return this._lineError;
            }
        }

        public override string Message
        {
            get
            {
                return this._message;
            }
        }

        public int Number
        {
            get
            {
                return this._numberError;
            }
        }
    }
}
