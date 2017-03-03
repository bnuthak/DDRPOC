using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class APOVerificationReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_APO_verfication_products(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.MATNR, a.LOCNO, c.MAKTX,a.PLANNER_PPS,a.PLANNER_DEMAND,
                    a.PLANNER_SNP,a.RQMKY,a.LSZKY,a.SHIPF,a.PRODH,a.SHIPH,a.PRIO,
                    a.RRP_TYPE,a.HEUR_ID,a.SAFTYC,a.MSDP_SB_METHOD,a.SVTTY,a.VRMOD,
                    a.VINT1,a.VINT2,a.PEG_FUTURE_MAX,a.PEG_PAST_MAX,a.PEG_FUTURE_ALERT,
                    a.PEG_PAST_ALERT,a.PEG_NO_DYN,a.GET_ALERTS,a.CONVH,a.CTX_ATT04,
                    a.CTX_ATT05,a.LGRAD,a.UEETO
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_products a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.locno in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND a.locno = b.werks(+) AND a.matnr = b.matnr(+)"
                + " AND a.matnr = c.matnr(+) AND c.spras(+) = 'E'"
                + " UNION"
                + " SELECT a.MATNR, a.LOCNO, c.MAKTX,a.PLANNER_PPS,a.PLANNER_DEMAND,"
                    + " a.PLANNER_SNP,a.RQMKY,a.LSZKY,a.SHIPF,a.PRODH,a.SHIPH,a.PRIO,"
                    + " a.RRP_TYPE,a.HEUR_ID,a.SAFTYC,a.MSDP_SB_METHOD,a.SVTTY,a.VRMOD,"
                    + " a.VINT1,a.VINT2,a.PEG_FUTURE_MAX,a.PEG_PAST_MAX,a.PEG_FUTURE_ALERT,"
                    + " a.PEG_PAST_ALERT,a.PEG_NO_DYN,a.GET_ALERTS,a.CONVH,a.CTX_ATT04,"
                    + " a.CTX_ATT05,a.LGRAD,a.UEETO"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_products a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.locno in (SELECT berid FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mdma b WHERE b.werks in (" + DDRSessionEntity.Current.plantcode + "))"
                + " AND a.locno = b.werks(+) AND a.matnr = b.matnr(+)"
                + " AND a.matnr = c.matnr(+) AND c.spras(+) = 'E'"
                + " ORDER BY matnr, locno");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_APO_verfication_Setup_matrix(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT MATRIX_NAME, LOCNO, MATRIX_TXT
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_setup_matrix "
                + " WHERE  locno in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY Locno, Matrix_name");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_APO_verfication_Transitions(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT MATRIX_NAME,LOCNO, SETUP_ID_FROM, SETUP_ID_TO,
                DURATION, DURATION_UNIT, COST
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_setup_transition "
                + " WHERE  locno in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY locno, Matrix_name");
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
