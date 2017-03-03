using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class SalesOrgInfo
    {
       QMDataManager qmmanager = new QMDataManager();
       public List<string > GetSalesOrgInfo()
       {
           try
           {
               string command = String.Format("select distinct vkorg from " + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_werks_sales where aland =");
               string SQL1 = "'" + DDRSessionEntity.Current.SiteCode + "'";
               List<String> ListText = qmmanager.Getsalesorgcode(command + SQL1);

               return ListText;
           }
           catch (Exception ex)
           { throw ex; }
       }
    }
}
