using System;
using Dapper.Contrib.Extensions;


namespace Banking.Api.Models
{
    [Table("tblMstCustomer")]
    public class Customer 
    {
        [ExplicitKey]
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Write(false)]
        public string UserNameBy { get; set; }



    }
}