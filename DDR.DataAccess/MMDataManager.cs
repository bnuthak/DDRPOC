using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace DDR.DataAccess
{
    public class MMDataManager
    {
        CommonManager commanager = new CommonManager();
        public System.Data.DataSet GetMMAuditingData(string command)//, List<OracleParameter> oracleParameters)
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
                    dataadapter.Fill(dsQMVerification, "MMAuditing");
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //catch (OracleException oraex)
            //{
            //    throw;
            //}
            finally
            {
                _connectionString.Close();
            }
            return dsQMVerification;
        } //end plan code
    }
}
