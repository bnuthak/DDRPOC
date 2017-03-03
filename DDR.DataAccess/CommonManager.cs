using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Oracle.DataAccess.Client;
using DDR.Entity;

namespace DDR.DataAccess
{
   public class CommonManager
    {
       //UserEntity SetUserEntity = new UserEntity();
        private const string CONNECTION_NAME = "DDRConnectionString";
        private static string _connectionString;
        //private static DbProviderFactory _factory;
        /// database connection string.
        /// </summary>
        internal string ConnectionString
        {
            get
            {
                if (String.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_NAME].ConnectionString;
                }
                string replaceusername = _connectionString.Replace("@", DDRSessionEntity.Current.username);
                string replacepassword = replaceusername.Replace("#", DDRSessionEntity.Current.password );
                string replaceOraclePort = replacepassword.Replace("%", DDRSessionEntity.Current.OraclePort);
                string replaceOracleServer = replaceOraclePort.Replace("^", DDRSessionEntity.Current.OracleServer);
                string replaceoracleinstance = replaceOracleServer.Replace("*", DDRSessionEntity.Current.defaultOracleInstance);

                return replaceoracleinstance;
            }
        }

        public bool CheckSchemaValidation(string username, string password, string defaultOracleInstance)
        {
           
            //SetUserEntity.username = username;
            //SetUserEntity.Password = password;
            DDRSessionEntity.Current.username = username;
            DDRSessionEntity.Current.password = password;
            DDRSessionEntity.Current.defaultOracleInstance = defaultOracleInstance;
            try
            {
                string replaceusername = ConnectionString.Replace("@", username);
                string replacepassword = replaceusername.Replace("#", password);
                string replaceOraclePort = replacepassword.Replace("%", DDRSessionEntity.Current.OraclePort);
                string replaceOracleServer = replaceOraclePort.Replace("^", DDRSessionEntity.Current.OracleServer);
                string replaceoracleinstance = replaceOracleServer.Replace("*", defaultOracleInstance);

                using (var connection = new OracleConnection(replaceoracleinstance))
                {
                    connection.Open();
                    return true;
                }
            }
            catch  
            {
                return false;
            }
        }

        public System.Data.DataSet GetUserSchemaAccessDetails(string command)//, List<OracleParameter> oracleParameters)
        {
            OracleDataAdapter dataadapter = new OracleDataAdapter();
            System.Data.DataSet dsuseraccess = new System.Data.DataSet();
            OracleConnection _connectionString = new OracleConnection(ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    dataadapter.SelectCommand = new OracleCommand(command, _connectionString);
                    dataadapter.Fill(dsuseraccess, "userschema");
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connectionString.Close();
            }
            return dsuseraccess;
        } //end plan code
    }
}
