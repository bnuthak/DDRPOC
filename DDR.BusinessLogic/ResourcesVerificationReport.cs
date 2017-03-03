using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class ResourcesVerificationReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_Resources_extract_BasicData(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, VERWE, STEXT, VERAN, SUBSYS, PRVBE, PLANV, RESGR ,VGWTS 
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
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
        public System.Data.DataTable SQL_Resources_extract_DefaultValues(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL,WERKS,KTSCH,KTSCH_REF,RASCH,RASCH_REF,
	            VGEXX,VGEXX1,VGEXX2,VGEXX3,VGEXX4,VGEXX5
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY ARBPL, WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_extract_Capacities(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, KAPART, KAPNAMEP, FORKN
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCECAPACITY"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY ARBPL, WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_extract_CapacityHeader(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, KAPART, KTEXT, PLANR, MOSID, KALID, VERSA, MEINS,
                    BEGZT, ENDZT, PAUSE, NGRAD, AZNOR, KAPEH, KAPTER, KAPAVO, KAPLPL
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_ResourceCapacity"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY ARBPL,WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_extract_Scheduling(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, KAPART, KAPNAME, FORTN
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY ARBPL, WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_extract_CostCenters(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL,WERKS,BEGDA,ENDDA,KOKRS,KOSTL,LARXX,FLG_REF,FORXX,
		        LARXX1,FLG_REF1,FORXX1,LARXX2,FLG_REF2,FORXX2,LARXX3,FLG_REF3,
                    FORXX3,LARXX4,FLG_REF4,FORXX4,LARXX5,FLG_REF5,FORXX5
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
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
        public System.Data.DataTable SQL_Resources_extract_ShortText(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ARBPL, WERKS, SPRAS, STEXT
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCEDESCRIPTION "
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY ARBPL, WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Resources_extract_DistinctCostCenters(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT DISTINCT KOSTL
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RESOURCE"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ") "
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
