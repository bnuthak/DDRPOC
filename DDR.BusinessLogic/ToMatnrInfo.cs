using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;

namespace DDR.BusinessLogic
{
   public class ToMatnrInfo
    {
       QMDataManager qmmanager = new QMDataManager();

        public List<string> GetToMatnrInfo()
        {
            try
            {
                //select matnr from SCM_OWNER.gdd_matnr_ref where aland = 'SE' order by matnr
                string command = String.Format("select matnr from " + DDRSessionEntity.Current.table_schema + "_OWNER.gdd_matnr_ref where aland =");
                string SQL1 = "'" + DDRSessionEntity.Current.SiteCode + "'" + " order by matnr";
                List<String> ListText = qmmanager.Gettomatnrcode(command + SQL1);

                return ListText;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
