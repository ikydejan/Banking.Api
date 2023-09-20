using System;
using Dapper.Contrib.Extensions;


namespace Banking.Api.Models
{
    [Table("tblMstUser")]
    public class User 
    {
        [ExplicitKey]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordKey { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}