using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BLL
{
    public class CustomerManager:BaseManager
    {

        public Message AddCustomer(Customers objCustomer)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter() { ParameterName = "@FName", Value = objCustomer.Fname });
            param.Add(new SqlParameter() { ParameterName = "@LName", Value = objCustomer.Lname });
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = objCustomer.Email });
            param.Add(new SqlParameter() { ParameterName = "@Password", Value = objCustomer.Password });
            param.Add(new SqlParameter() { ParameterName = "@Mobile", Value = objCustomer.Mobile });

            param.Add(new SqlParameter() { ParameterName = "@CreatedDate", Value = objCustomer.CreatedDate });
            param.Add(new SqlParameter() { ParameterName = "@TokenID", Value = objCustomer.TokenId });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = objCustomer.MessageCode, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDiscrition", Value = objCustomer.MessageDescription, Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.INSERT_CUSTOMER, param, out retParam);
            

            return new Message { MessageCode = (bool)retParam.ElementAtOrDefault(0).Value, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };
            //  return outputParameter;

        }



        public Tuple<string,string> VerifyCustomer(string Email,string tokenID)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter() { ParameterName = "@TokenId", Value = tokenID });
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = Email });
            param.Add(new SqlParameter() { ParameterName = "@LoginName", Value = null, Direction = ParameterDirection.Output, Size = 200 });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = null, Direction = ParameterDirection.Output, Size = 200 });
            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.VERIFYCUSTOMER, param, out retParam);
            string UserName = retParam.ElementAtOrDefault(1).Value.ToStringSafe();
            string message = retParam.ElementAtOrDefault(0).Value.ToStringSafe();


            return Tuple.Create(UserName,message);

        }

        public AuthenticCustomer CustomerLogin(string Email, string Password)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter() { ParameterName = "@Email", Value = Email });
            param.Add(new SqlParameter() { ParameterName = "@Password", Value = Password });

            List<AuthenticCustomer> AuthenticCustomerList = Select<AuthenticCustomer>(StoredProcedureName.CUSTOMERLOGIN, param);
            return AuthenticCustomerList.FirstOrDefault();
            
        }

        public Message ChangePassword(int customerId, string password)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = customerId });
            param.Add(new SqlParameter() { ParameterName = "@PassWord", Value = password });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = password, Direction = ParameterDirection.Output, Size = 5 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Value = password, Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.CHANGECUSTOMERPASSWORD, param, out retParam);

            bool isSuccess = retParam[0].Value.ToStringSafe() == "0" ? false : true;
            string message = retParam[1].Value.ToStringSafe() ;
            return new Message { MessageCode = isSuccess, MessageDescription = message }; 
        }

        public Tuple<string,string,bool> ForGotPassWord(string Email)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = Email });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = null, Direction = ParameterDirection.Output, Size = 200 });
            param.Add(new SqlParameter() { ParameterName = "@Password", Value = null, Direction = ParameterDirection.Output, Size = 100 });
            param.Add(new SqlParameter() { ParameterName = "@IsSuccess", Value = null, Direction = ParameterDirection.Output, Size = 5 });
            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.FORGOTPASSWORDCUSTOMER, param, out retParam);
            string password = retParam.ElementAtOrDefault(1).Value.ToStringSafe();
            string message = retParam.ElementAtOrDefault(0).Value.ToStringSafe();
            bool IsSuccess =  retParam[2].Value.ToStringSafe()=="0"?false:true;



            return Tuple.Create(password, message,IsSuccess);

        }

        public Message UpdateCustomer(Customers objCustomer)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = objCustomer.CustomerID });
            param.Add(new SqlParameter() { ParameterName = "@FName", Value = objCustomer.Fname });
            param.Add(new SqlParameter() { ParameterName = "@LName", Value = objCustomer.Lname });
            param.Add(new SqlParameter() { ParameterName = "@Mobile", Value = objCustomer.Mobile });

            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = objCustomer.MessageCode, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Value = objCustomer.MessageDescription, Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isUpdated = Update(param, StoredProcedureName.UPDATE_CUSTOMER_PROFILE, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, true))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };


        }

        public Customers GetCustomerDetailsById(int customerId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = customerId });

            List<Customers> customerList = Select<Customers>(StoredProcedureName.GET_CUSTOMER_DETAILS_BY_ID, param);
            return customerList.FirstOrDefault();
        }
        public List<TransactionHistory> GetTransactionHistory(int customerId,float? bookingInfoId = null)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = customerId });
            if (bookingInfoId != null) { param.Add(new SqlParameter() { ParameterName = "@BookingInfoId", Value = bookingInfoId }); }

            List<TransactionHistory> transactionHistory = Select<TransactionHistory>(StoredProcedureName.GET_CUSTOMER_TRANSACTION_HISTORY, param);
            return transactionHistory;
        }


    }

}
