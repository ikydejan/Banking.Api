using System;
using Dapper.Contrib.Extensions;


namespace Banking.Api.Models
{
    [Table("tblMstAccount")]
    public class Account 
    {
        [ExplicitKey]
        public long AccountNo { get; set; }
        public long CustomerID { get; set; }
        public bool InActive { get; set; }
        public string CardNo { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Write(false)]
        public string UserNameBy { get; set; }



    }
}