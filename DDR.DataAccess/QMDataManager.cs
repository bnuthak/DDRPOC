using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Configuration;
using Oracle.DataAccess.Client;
using DDR.Entity;

namespace DDR.DataAccess
{
    public class QMDataManager
    {
        CommonManager commanager = new CommonManager();
        //private const string CONNECTION_NAME = "DDRConnectionString";
        //private static string _connectionString;
        //private static DbProviderFactory _factory;
        /// <summary>
        /// database connection string.
        /// </summary>
        //internal static string ConnectionString
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(_connectionString))
        //            _connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_NAME].ConnectionString;
        //        return _connectionString;
        //    }
        //}

        //internal static DbProviderFactory Factory
        //{
        //    get { return _factory ?? (_factory = DbProviderFactories.GetFactory("Oracle.DataAccess.Client")); }
        //}
        /// <summary>
        /// Check if user is exist in the database.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int ExecuteCommand(string command)//, List<OracleParameter> oracleParameters)
        {
            //bool bl = CheckSchemaValidation(ConnectionString);
            int ExistUser = 0;//string.Empty;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
                    try
                    {
                            _connectionString.Open();
                            var cmd = new OracleCommand();
                            if (cmd != null)
                            {
                                cmd.Connection = _connectionString;
                                cmd.CommandText = command;
                                var IsRecord = cmd.ExecuteScalar();
                                ExistUser = Convert.ToInt16(IsRecord);
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
            return ExistUser;
        }

        //get the plan code :
        /// <summary>
        /// Get the list for plan code as per the site code
        /// </summary>
        /// <param name="command">country code</param>
        /// <returns></returns>
        public List<string> Getplantcode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader=cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                           DDRSessionEntity.Current.plantcode = datareader.GetString(0);
                           ListText.Add(DDRSessionEntity.Current.plantcode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        } //end plan code

        public List<string> Gettomatnrcode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.tomatnrcode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.tomatnrcode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Getfrommatnrcode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.frommatnrcode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.frommatnrcode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Gettorecipegroupcode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.torecipegroupcode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.torecipegroupcode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Getfromrecipegroupcode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.fromrecipegroupcode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.fromrecipegroupcode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Getrecipenumbercode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.recipenumbercode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.recipenumbercode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Gettoresourcecode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.toresourcecode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.toresourcecode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Getfromresourcecode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.fromresourcecode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.fromresourcecode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        public List<string> Getsalesorgcode(string command)//, List<OracleParameter> oracleParameters)
        {
            List<String> ListText = new List<string>();
            OracleDataReader datareader;
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    cmd.Connection = _connectionString;
                    cmd.CommandText = command;
                    datareader = cmd.ExecuteReader();
                    if (cmd.ExecuteReader().HasRows)
                    {
                        while (datareader.Read())
                        {
                            DDRSessionEntity.Current.salesorgcode = datareader.GetString(0);
                            ListText.Add(DDRSessionEntity.Current.salesorgcode);
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connectionString.Close();
            }
            return ListText;
        }

        /// <summary>
        /// Get the QM verificaion recordset.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public System.Data.DataSet GetQMVerificationData(string command)//, List<OracleParameter> oracleParameters)
        {
            OracleDataAdapter dataadapter = new OracleDataAdapter();
            System.Data.DataSet dsQMVerification = new System.Data.DataSet();
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    dataadapter.SelectCommand = new OracleCommand(command, _connectionString);
                    dataadapter.Fill(dsQMVerification, "QMVerification");
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
            return dsQMVerification;
        } //end plan code
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public System.Data.DataSet GetSiteCode(string command)
        {
            OracleDataAdapter dataadapter = new OracleDataAdapter();
            System.Data.DataSet dssitecode = new System.Data.DataSet();
            OracleConnection _connectionString = new OracleConnection(commanager.ConnectionString);
            try
            {
                _connectionString.Open();
                var cmd = new OracleCommand();
                if (cmd != null)
                {
                    dataadapter.SelectCommand = new OracleCommand(command, _connectionString);
                    dataadapter.Fill(dssitecode, "SiteCode");
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
            return dssitecode;
        } //end plan code
    }
}