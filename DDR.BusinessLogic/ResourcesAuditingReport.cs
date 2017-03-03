using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class ResourcesAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_Resources_audit_Missing_KOSTL(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, VERWE, KOSTL
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
	            + " WHERE KOSTL IS NULL"
                + " AND   WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY ARBPL, WERKS, VERWE");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_audit_Missing_FORTN(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, VERWE, FORTN 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
                + " WHERE FORTN IS NULL"
                + " AND   WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND   VERWE != '0017'"
                + " ORDER BY ARBPL, WERKS, VERWE");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_audit_Missing_ResourceDescription(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, STEXT FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE "
                + " WHERE ARBPL||WERKS||STEXT NOT IN "
                    + " (SELECT ARBPL||WERKS||STEXT FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCEDESCRIPTION)"
                    + " AND WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY ARBPL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_audit_KOSTL_wo_ActivityType_Linked(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT DISTINCT KOSTL FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE  WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
                   + " AND LARXX IS NULL"
                   + " AND LARXX1 IS NULL"
                   + " AND LARXX2 IS NULL"
                   + " AND LARXX3 IS NULL"
                   + " AND LARXX4 IS NULL"
                   + " AND LARXX5 IS NULL"
                + " ORDER BY KOSTL");
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
