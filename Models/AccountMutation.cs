using System;
using Dapper.Contrib.Extensions;


namespace Banking.Api.Models
{
    [Table("tblTrnAccountMutation")]
    public class AccountMutation 
    {
        [ExplicitKey]
        public long DetailID { get; set; }
        public string DocumentNo { get; set; }
        public DateTime TransDate { get; set; }
        public int TransTypeID { get; set; }
        public long AccountNo { get; set; }
        public decimal Amount { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Write(false)]
        public string UserNameBy { get; set; }



    }
}