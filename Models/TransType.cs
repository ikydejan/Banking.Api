using System;
using Dapper.Contrib.Extensions;


namespace Banking.Api.Models
{
    [Table("tblMstTransType")]
    public class TransType 
    {
        [ExplicitKey]
        public int TransTypeID { get; set; }
        public string TransTypeName { get; set; }
        public string IO { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Write(false)]
        public string UserNameBy { get; set; }



    }
}