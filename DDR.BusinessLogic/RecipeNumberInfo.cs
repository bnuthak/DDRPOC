using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class RecipeNumberInfo
    {
       QMDataManager qmmanager = new QMDataManager();
       public List<string > GetRecipeNumberInfo()
       {
           try
           {
                //select distinct plnal from SCM_OWNER.gdd_recipegeneralview a, SCM_OWNER.gdd_werks_ref b where a.werks = b.werks and b.aland = 'SE' ORDER BY plnal
                string command = String.Format("select distinct plnal from " + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_recipegeneralview a, "
                     + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_werks_ref b where a.werks = b.werks and b.aland = "
                     + "'" + DDRSessionEntity.Current.SiteCode + "'" + " ORDER BY plnal");
                List<String> ListText = qmmanager.Getrecipenumbercode(command);
                return ListText;
            }
           catch (Exception ex)
           { throw ex; }
       }
    }
}
