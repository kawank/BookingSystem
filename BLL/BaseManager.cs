using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Utility;
using System.Data.SqlClient;
using System.Reflection;
using BE;
using System.Data;

namespace BLL
{
    public abstract class BaseManager
    {

        private DatabaseManager _dbHelper;
        public virtual DatabaseManager dbHelper
        {
            get { return (_dbHelper != null) ? _dbHelper : new DatabaseManager(); }
            set { _dbHelper = value; }
        }
        public BaseManager()
        {
            _dbHelper = new DatabaseManager();
        }
       
        public List<R> Select<R>(string spName, object parameter)
            where R : class,new()
        {
            List<R> result = null;
            DataSet dsResult = dbHelper.Select(ConvertObjectToParameter(parameter), spName);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0] != null)
            {
                result = dsResult.Tables[0].ConvertToList<R>();
            }
            return result;
        }
        public List<R> Select<R>(string spName, List<SqlParameter> parameterList = null)
            where R : class,new()
        {
            List<R> result = null;
            DataSet dsResult = dbHelper.Select(parameterList, spName);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0] != null)
            {
                result = dsResult.Tables[0].ConvertToList<R>();
            }
            return result;
        }
        public List<R> Select<R>(string spName, List<SqlParameter> parameterList, out List<SqlParameter> returnParameters)
            where R : class,new()
        {
            List<R> result = null;
            DataSet dsResult = dbHelper.Select(parameterList, spName, out returnParameters);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0] != null)
            {
                result = dsResult.Tables[0].ConvertToList<R>();
            }
            return result;
        }
       
        public DataSet Select(string spName, object parameter)
        {
            return dbHelper.Select(ConvertObjectToParameter(parameter), spName);
        }
        public DataSet Select(string spName, List<SqlParameter> parameterList = null)
        {
            return dbHelper.Select(parameterList, spName);
        }
        public DataSet Select(string spName, List<SqlParameter> parameterList,out List<SqlParameter> returnParameters)
        {
            return dbHelper.Select(parameterList, spName, out returnParameters);
        }

       

        public object SelectScalar<P>(string spName, object parameter)
        {
            return dbHelper.SelectScalar(ConvertObjectToParameter(parameter), spName);
        }

        public object SelectScalar(string spName, List<SqlParameter> parameterList = null)
        {
            return dbHelper.SelectScalar(parameterList, spName);
        }

       
        public bool Insert(string spName, object parameter)
        {
            return dbHelper.Insert(ConvertObjectToParameter(parameter), spName);
        }

        public bool Insert(string spName, List<SqlParameter> parameterList)
        {
            return dbHelper.Insert(parameterList, spName);
        }

        public int Insert(string spName, List<SqlParameter> parameterList, out List<SqlParameter> returnParameter)
        {
            return dbHelper.Update(parameterList, spName, out returnParameter);
        }

       
        public bool Update(string spName, object parameter)
        {
            return dbHelper.Update(ConvertObjectToParameter(parameter), spName);
        }
        public bool Update(string spName, List<SqlParameter> parameterList)
        {
            return dbHelper.Update(parameterList, spName);
        }

        public int Update(List<SqlParameter> parameterList, string spName, out List<SqlParameter> returnParameter)
        {
            return dbHelper.Update(parameterList, spName, out returnParameter);
        }

        
        public bool Delete(string spName, object parameter)
        {
            return dbHelper.Delete(ConvertObjectToParameter(parameter), spName);
        }

        public bool Delete( string spName,List<SqlParameter> parameterList)
        {
            return dbHelper.Delete(parameterList, spName);
        }

        public int Delete(List<SqlParameter> parameterList, string spName, out List<SqlParameter> returnParameter)
        {
            return dbHelper.Delete(parameterList, spName, out returnParameter);
        }
        public List<SqlParameter> ConvertEntitytoParameter<E>(E entity)
            where E : class
        {
            try
            {
                List<SqlParameter> sqlParameters = null;
                if (entity != null)
                {
                    PropertyInfo[] propertiesInfoList = entity.GetType().GetProperties();
                    sqlParameters = new List<SqlParameter>();
                    for (int propertyCounter = 0; propertyCounter < propertiesInfoList.Count(); propertyCounter++)
                    {
                        SqlParameter sqlParameter = new SqlParameter("@" + propertiesInfoList[propertyCounter].Name, propertiesInfoList[propertyCounter].GetType());
                        sqlParameter.Value = propertiesInfoList[propertyCounter].GetValue(entity);
                        sqlParameters.Add(sqlParameter);
                    }
                }
                return sqlParameters;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SqlParameter> ConvertObjectToParameter(object param)
        {
            try
            {
                List<SqlParameter> sqlParameters = null;
                if (param != null)
                {
                    
                    PropertyInfo[] propertiesInfoList = param.GetType().GetProperties();
                    sqlParameters = new List<SqlParameter>();
                    for (int propertyCounter = 0; propertyCounter < propertiesInfoList.Count(); propertyCounter++)
                    {
                        SqlParameter sqlParameter = new SqlParameter("@" + propertiesInfoList[propertyCounter].Name, propertiesInfoList[propertyCounter].GetType());
                        sqlParameter.Value = propertiesInfoList[propertyCounter].GetValue(param);
                        sqlParameters.Add(sqlParameter);
                    }
                }
                return sqlParameters;
            }
            catch (Exception)
            {

                throw;
            }
        }
       

    }
}
