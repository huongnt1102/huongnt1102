using System;
using System.Data.Linq.Mapping;
using System.Reflection;
namespace ServiceTTCNew
{
    partial class DataClasses1DataContext
    {
        [Function(Name = "GetDate", IsComposable = true)]
        public DateTime GetSystemDate()
        {
            MethodInfo mi = MethodBase.GetCurrentMethod() as MethodInfo;
            return (DateTime)this.ExecuteMethodCall(this, mi, new object[] { }).ReturnValue;
        }
    }
}
