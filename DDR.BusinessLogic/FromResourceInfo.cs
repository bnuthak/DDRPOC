using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class FromResourceInfo
    {
       QMDataManager qmmanager = new QMDataManager();
       public List<string > GetFromResourceInfo()
       {
           try
           {
                //SELECT distinct arbpl FROM SCM_OWNER.gdd_resource where werks in (SELECT werks from SCM_OWNER.gdd_werks_ref WHERE aland = 'SE') ORDER BY arbpl
                string command = String.Format("select distinct arbpl from " + DDRSessionEntity.Current.table_schema
                    + "_OWNER.gdd_resource where werks in (SELECT werks from " + DDRSessionEntity.Current.table_schema
                    + "_OWNER.gdd_werks_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "')" + " ORDER BY arbpl");
                List<String> ListText = qmmanager.Getfromresourcecode(command);
                return ListText;
            }
           catch (Exception ex)
           { throw ex; }
       }
    }
}
