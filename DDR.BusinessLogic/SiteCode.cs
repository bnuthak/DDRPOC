using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class SiteCode
    {
       QMDataManager datamanager = new QMDataManager();
        public System.Data.DataSet GetSiteCodeFinalData()
        {
            try
            {
                string command = String.Format(@"
                select sapid, site_code, iso_code, description
                from gdd_owner.gdd_scm_sites
                order by site_code
               ");
                System.Data.DataSet dssitecode = datamanager.GetSiteCode(command);
                return dssitecode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
