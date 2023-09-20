using Newtonsoft.Json;

namespace Banking.Api.Models
{
    
    public class Log4netSet
    {
        public static void SetLogNet<T>(T obj, string UserNameBy)
        {
            log4net.LogicalThreadContext.Properties["Appversion"] = "1.0.0";
            log4net.LogicalThreadContext.Properties["NewValue"] = JsonConvert.SerializeObject(obj);
            log4net.LogicalThreadContext.Properties["User"] = UserNameBy;
            log4net.LogicalThreadContext.Properties["company"] = "";
        }
    }
}