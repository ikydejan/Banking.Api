using System;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace Banking.Api.Models
{
    [Table("tblUtlAPILogs")]
    public class Log
    {
        [Key]
        public long LogID { get; set; }
        public string LogLevel { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string NewValue { get; set; }
        public string OldValue { get; set; }
        public string Exception { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LogDate { get; set; } 
        public string CompanyID { get; set; }
   
    }
}