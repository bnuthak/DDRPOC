using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class PlantInfo
    {
       QMDataManager qmmanager = new QMDataManager();
       public List<string > GetPlantInfo()
       {
           try
           {
               string command = String.Format("select werks from " + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_werks_ref where aland =");
               string SQL1 = "'" + DDRSessionEntity.Current.SiteCode + "'";
               List<String> ListText = qmmanager.Getplantcode(command + SQL1);

               return ListText;
           }
           catch (Exception ex)
           { throw ex; }
       }
    }
}
