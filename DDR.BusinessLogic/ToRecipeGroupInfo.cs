using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class ToRecipeGroupInfo
    {
       QMDataManager qmmanager = new QMDataManager();
       public List<string > GetToRecipeGroupInfo()
       {
           try
           {
                //select distinct plnnr from SCM_OWNER.gdd_recipegeneralview a, SCM_OWNER.gdd_werks_ref b where a.werks = b.werks and b.aland = 'SE' ORDER BY plnnr
                string command = String.Format("select distinct plnnr from " + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_recipegeneralview a, "
                     + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_werks_ref b where a.werks = b.werks and b.aland = "
                     + "'" + DDRSessionEntity.Current.SiteCode + "'" + " ORDER BY plnnr");
                List<String> ListText = qmmanager.Gettorecipegroupcode(command);
                return ListText;
           }
           catch (Exception ex)
           { throw ex; }
       }
    }
}
