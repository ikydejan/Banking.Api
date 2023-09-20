using System.Collections.Generic;
using Banking.Api.Models;
using System;
namespace Banking.Api.Dtos
{

    public class AccountMutationDto : Account
    {
        public AccountMutationDto()
        {
            lst_dtl = new List<AccountMutation>(); 
        }
        public List<AccountMutation> lst_dtl { get; set; }  
        public Customer customer { get; set; }  
        
    }

    public class AccountMutationParamDto  
    {
        public long? AccountNo { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        private QueryHelper _qH;
        
        public string query
        {
            get
            {

                string cond=""; 
                _qH = new QueryHelper(); 
 
                if (AccountNo !=0 && AccountNo != null) {
                      cond = _qH.SetConditionAND(cond,string.Format(@"A.AccountNo = '{0}' ",AccountNo));
                }
                if (StartDate != null && EndDate !=null) {
                    cond = _qH.SetConditionAND(cond,String.Format(@"(CONVERT(Date,B.TransDate) BETWEEN '{0}' AND '{1}')", StartDate?.ToString("yyyy-MM-dd"), EndDate?.ToString("yyyy-MM-dd")));
                }
 
                var sql = string.Format(@"SELECT A.*,  
                                            B.DetailID as ID_Mutation,B.*,
                                            C.CustomerID as ID_Customer,C.* 
                                            FROM tblMstAccount A 
                                            INNER JOIN tblTrnAccountMutation B ON A.AccountNo = B.AccountNo
                                            LEFT JOIN tblMstCustomer C ON A.CustomerID = C.CustomerID 
                                        {0}
                                        ORDER BY B.TransDate,B.DocumentNo,B.DetailID  ",
                                        cond ==""? "" : " WHERE " + cond);


                    return sql;
                }
        } 
     
    }
}