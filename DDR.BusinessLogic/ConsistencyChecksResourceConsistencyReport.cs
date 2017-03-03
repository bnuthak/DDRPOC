using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class ConsistencyChecksResourceConsistencyReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_Consistency_resource_check(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr AND a.matnr not in (select distinct matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt) AND b.aland =" + "'" + DDRSessionEntity.Current.SiteCode + "'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_R10_resource_check(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr AND a.matnr not in (select distinct matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt) AND b.aland =" + "'" + DDRSessionEntity.Current.SiteCode + "'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
