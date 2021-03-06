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
    public class AdminManager : BaseManager
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public AuthenticUser ValidateUser(String Email, String Password)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = Email });
            param.Add(new SqlParameter() { ParameterName = "@Password", Value = Password });
            List<AuthenticUser> AuthenticUserList = Select<AuthenticUser>(StoredProcedureName.GET_ADMIN_BY_USERNAME_AND_PASSWORD, param);
            return AuthenticUserList.FirstOrDefault();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AdminUserDetails> GetUserList()
        {
            List<AdminUserDetails> userList = Select<AdminUserDetails>(StoredProcedureName.GET_ADMIN_USER_LIST);
            return userList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RoleKeyValuePair> GetRoleKeyValueList()
        {
            List<RoleKeyValuePair> roleList = Select<RoleKeyValuePair>(StoredProcedureName.GET_ROLE_LIST);
            return roleList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        public Message UpdateUserDetails(EditUserDetails userDetails)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = userDetails.UserId });
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = userDetails.Email });
            param.Add(new SqlParameter() { ParameterName = "@Fname", Value = userDetails.FName });
            param.Add(new SqlParameter() { ParameterName = "@LName", Value = userDetails.LName });
            param.Add(new SqlParameter() { ParameterName = "@Mobile", Value = userDetails.Mobile });
            param.Add(new SqlParameter() { ParameterName = "@RoleId", Value = userDetails.RoleId });
            param.Add(new SqlParameter() { ParameterName = "@ModifiedBy", Value = userDetails.ModifiedBy });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = userDetails.MessageCode, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Value = userDetails.MessageDescription, Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isSuccessful = Update(param, StoredProcedureName.UPDATE_ADMIN_USER_DETAILS, out retParam);
            return new Message { MessageCode = (bool)retParam.ElementAtOrDefault(0).Value, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public EditUserDetails GetUserDetailsByUserId(int userId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = userId });

            List<EditUserDetails> userList = Select<EditUserDetails>(StoredProcedureName.GET_ADMIN_USER_DETAILS_BY_USER_ID, param);
            return userList.FirstOrDefault();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Message DeleteUser(int userId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = userId });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isDeleted = Delete(param, StoredProcedureName.DELETE_ADMIN_USER, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, "1"))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cuser"></param>
        /// <returns></returns>
        public bool AddCinemaUser(CinemaUser Cuser)
        {
            bool dbResult = false;
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "@Email", Value = Cuser.Email });
                param.Add(new SqlParameter() { ParameterName = "@Password", Value = Cuser.Password });
                param.Add(new SqlParameter() { ParameterName = "@ConfirmPassword", Value = Cuser.ConfirmPassword });
                param.Add(new SqlParameter() { ParameterName = "@FName", Value = Cuser.Fname });
                param.Add(new SqlParameter() { ParameterName = "@LName", Value = Cuser.Lname });
                param.Add(new SqlParameter() { ParameterName = "@Mobile", Value = Cuser.Mobile });
                param.Add(new SqlParameter() { ParameterName = "@CreatedBy", Value = Cuser.CreatedBy });
                param.Add(new SqlParameter() { ParameterName = "@RoleId", Value = Cuser.RoleId });
                param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = "", Direction = ParameterDirection.Output, Size = 10 });
                param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Value = "", Direction = ParameterDirection.Output, Size = 1000 });
                List<SqlParameter> retParam = new List<SqlParameter>();
                dbResult = Insert(StoredProcedureName.INSERT_USER, param);
                }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("Username is already Exists") == true)
                {
                   dbResult=false;
                }

            }
            return dbResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<CinemaUser> GetAllCinemaUsers(Int16 RoleId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@RoleId", Value = RoleId });
            List<CinemaUser> lstCinemaUser = Select<CinemaUser>(StoredProcedureName.GET_USERS_BY_ROLE, param);
            return lstCinemaUser;
        }

        /// <summary>
        /// Get Password by email 
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public UserPassword GetPasswordByEmail(String Email)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = Email });
            List<UserPassword> objuserPassword = Select<UserPassword>(StoredProcedureName.GET_USER_PASSWORD_BY_USERNAME, param);
            return objuserPassword.FirstOrDefault();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Message UpdatePassword(int userId, string password)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = userId });
            param.Add(new SqlParameter() { ParameterName = "@Password", Value = password });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isDeleted = Delete(param, StoredProcedureName.UPDATE_USER_PASSWORD, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, "1"))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };

        }

        public string IsUserAlreadyExists(string userName)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@UserName", Value = userName });
            string resultName = SelectScalar(StoredProcedureName.IS_USER_EXISTS_BY_USER_NAME, param).ToStringSafe();
            return resultName;
        }
    }
}
