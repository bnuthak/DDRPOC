using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class QMAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();

        public System.Data.DataTable SQL_QM_QM_Group_counter(string title)//comments removed
        {
            try
            {
                string command = String.Format(@"
				select a.matnr, a.werks, a.plnnr, a.plnal, 
                case 
                    when plnal = '01' then 'G'   
                    when plnal = '02' then 'L'   
                    when plnal = '03' then 'M'   
                    when plnal = '04' then 'EA'   
                    when plnal = '05' then 'TS' 
                    when plnal = '06' then 'M2' 
		            when plnal = '07' then 'FT2'
                    else plnal
                end uom_on_alloc, meins uom_on_mm,
                case
                    when meins in ('KG','MG','G') then  '01'
                    when meins in ('ML','KL','L')  then  '02'
                    when meins = 'M'  then  '03'
                    when meins = 'EA' then  '04' 
                    when meins = 'TS' then  '05'
                    when meins = 'M2' then  '06'
	                when meins = 'FT2' then  '07'
                    else meins
	                end PLNAL_for_MM    
                from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara m,"
                + " ("
                    + " select distinct matnr, "
                + " case "
                    + " when plnal = '01' then 'G'   "
                    + " when plnal = '02' then 'L'   "
                    + " when plnal = '03' then 'M'   "
                    + " when plnal = '04' then 'EA'   "
                    + " when plnal = '05' then 'TS'"
                    + " when plnal = '06' then 'M2'"
                        + " when plnal = '07' then 'FT2'"
                    + " else plnal"
                + " end  uom"
                + " from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc a where werks in (" + DDRSessionEntity.Current.plantcode + ") "
                + " and exists (select 1 from  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r where a.matnr = r.matnr and r.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " minus"
                + " select matnr, decode(meins,'KG','G', 'MG', 'G', 'ML', 'L', 'KL', 'L','FT2','M2','M3','L', meins) from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara"
                + " minus  "
                + " select matnr, decode(meinh,'KG','G', 'MG', 'G', 'ML', 'L', 'KL', 'L','FT2','M2','M3','L', meinh) from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm"
                + " ) u"
                + " where a.matnr = u.matnr(+)"
                + " and  case "
                    + " when plnal = '01' then 'G'   "
                    + " when plnal = '02' then 'L'   "
                    + " when plnal = '03' then 'M'   "
                    + " when plnal = '04' then 'EA'   "
                    + " when plnal = '05' then 'TS'"
                    + " when plnal = '06' then 'M2'"
                    + " when plnal = '07' then 'FT2'"
                    + " else plnal "
                + " end = u.uom(+)"
                + " and a.matnr = m.matnr"
                + " and werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " and exists (select 1 from  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r  where a.matnr = r.matnr and r.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and  decode(uom,'','Good','Bad') = 'Bad'"
                + " order by 6, 1, 2, 3, 4");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_NO_QMView(string title)
        {
            try
            {
                string command = String.Format(@"
				select matnr, werks, 'No QM View' NQMV 
                from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat "
                + " where werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " minus"
                + " select matnr, werks, 'No QM View' NQMV  "
                + " from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc "
                + " where ssqss is not null and werks in (" + DDRSessionEntity.Current.plantcode + ")");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_QMView_without_Insp_Type(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.werks, a.xchpf, a.mhdrz, a.mhdhb, c.prfrq, 'QM View without Insp Type' QMVNIT  
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE a.matnr IN              (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + "AND load='X')"
                + " AND   c.matnr||c.werks NOT IN (SELECT distinct matnr||werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat) "
                + " AND   a.matnr=c.matnr"
                + " AND   c.werks IN (" + DDRSessionEntity.Current.plantcode + ")");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_INSP(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.werks, a.art, b.plnnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc b "
                + " WHERE a.matnr = b.matnr AND a.werks = b.werks"
                + " AND a.werks IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND SUBSTR(a.art, 1, 2) NOT IN (select concat('0',substr(c.plnnr,6,1)) from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc c where a.matnr = c.matnr and a.werks = c.werks )"
                + " MINUS"
                + " SELECT distinct a.matnr, a.werks, a.art, b.plnnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc b "
                + " WHERE a.matnr = b.matnr AND a.werks = b.werks"
                + " AND a.werks IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND SUBSTR(a.art, 1, 2) = '89' AND '00' IN (select concat('0',substr(c.plnnr,6,1)) from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc c where a.matnr = c.matnr and a.werks = c.werks )"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_AUSP(string title)
        {
            try
            {
                string command = String.Format(@"
				select matnr,mtart, atinn,atwrt from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b"
                + " where a.matnr = b.objek and atinn in ('0000000952','0000000954') and atwrt is null");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_INSP_INTERVAL(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr,a.mtart, b.prfrq, c.atinn,c.atwrt
	            FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.mtart IN ('ROH','HALB','VERP')"
                + " AND    b.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    b.prfrq IS NULL"
                + " AND    a.matnr = c.objek"
                + " AND    c.atinn = '0000000953'"
                + " AND    c.ATWRT IS NOT NULL"
                + " UNION"
                + " SELECT a.matnr,a.mtart, b.prfrq, c.atinn,c.atwrt"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    b.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    b.prfrq IS NOT NULL"
                + " AND    a.matnr = c.objek"
                + " AND    c.atinn = '0000000953'"
                + " AND    (( a.mtart = 'HALB' AND c.ATWRT != '60' ) OR"
                    + " ( a.mtart = 'ROH'  AND c.ATWRT != '180' ) OR"
                    + " ( a.mtart = 'VERP' AND c.ATWRT IS NOT NULL ))"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0]; 
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QMSce_01(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks, a.matnr, a.prfrq, b.mhdrz, b.mhdhb
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE a.matnr = b.matnr"
                + " AND a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND ( a.prfrq is NULL OR a.prfrq = 0 )"
                + " AND b.mhdrz is NOT NULL AND b.mhdrz <> 0"
                + " AND b.mhdhb is NOT NULL AND b.mhdhb <> 0 order by werks, matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QMSce_02(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks, a.matnr, a.prfrq, b.mhdrz, b.mhdhb
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE a.matnr = b.matnr"
                + " AND a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND ( a.prfrq is NULL OR a.prfrq = 0 ) "
                + " AND b.mhdrz is NOT NULL AND b.mhdrz <> 0"
                + " AND  ( b.mhdhb is NULL OR  b.mhdhb = 0 ) order by werks, matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QMSce_03(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks, a.matnr, a.prfrq, b.mhdrz, b.mhdhb
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE a.matnr = b.matnr"
                + " AND a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND a.prfrq is NOT NULL AND a.prfrq <> 0"
                + " AND ( b.mhdrz is NULL OR b.mhdrz = 0 )"
                + " AND ( b.mhdhb is NULL OR b.mhdhb = 0 ) order by werks,matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QMSce_04(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks, a.matnr, a.prfrq, b.mhdrz, b.mhdhb
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE a.matnr = b.matnr"
                + " AND a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND ( a.prfrq is NULL OR a.prfrq = 0 )"
                + " AND ( b.mhdrz is NULL OR b.mhdrz = 0 )"
                + " AND ( b.mhdhb is NULL OR b.mhdhb = 0 ) order by werks, matnr, prfrq, mhdrz, mhdhb");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QMSce_05(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks, a.matnr, a.prfrq, b.mhdrz, b.mhdhb
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE a.matnr = b.matnr"
                + " AND a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND a.prfrq is NOT NULL AND a.prfrq <> 0 "
                + " AND b.mhdrz is NOT NULL AND b.mhdrz <> 0 order by werks , matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_expires_no_09(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.mtart, a.mhdrz
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    nvl(a.mhdrz,0) = 1"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND    c.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.matnr||c.werks not in (SELECT matnr||werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat WHERE art = '09')"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_prfrq_no_09(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.mtart, b.werks, b.prfrq
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    nvl(b.prfrq,0) > 0"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND    b.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.matnr||b.werks not in (SELECT matnr||werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat WHERE art = '09')"
                + " ORDER BY a.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_INHO_PROC(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks,a.matnr, a.beskz, a.ssqss, b.art
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'E'"
                + " AND    a.ssqss = '0000' "
                + " AND    a.matnr || a.werks NOT IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where ART = '04' AND werks IN (" + DDRSessionEntity.Current.plantcode + "))"
                + " UNION"
                + " SELECT a.werks,a.matnr, a.beskz, a.ssqss, b.art"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'E'"
                + " AND    a.ssqss <> '0000' "
                + " UNION"
                + " SELECT a.werks,a.matnr, a.beskz, a.ssqss, b.art"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'E'"
                + " AND    a.ssqss = '0000' "
                + " AND    b.ART = '04'"
                + " AND    a.matnr || a.werks IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where werks IN (" + DDRSessionEntity.Current.plantcode + ") AND ART = '01' )"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0]; 
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_EXTN_PROC(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks,a.matnr, a.beskz, a.ssqss, b.art
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = b.werks"
                + " AND   a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND   a.beskz = 'F'"
                + " AND   a.ssqss = '0001' "
                + " AND   a.matnr  || a.werks NOT IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where ART = '01' AND werks IN (" + DDRSessionEntity.Current.plantcode + "))"
                + " UNION"
                + " SELECT a.werks,a.matnr, a.beskz, a.ssqss, b.art"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'F'"
                + " AND    a.ssqss <> '0001' "
                + " UNION"
                + " SELECT a.werks,a.matnr, a.beskz, a.ssqss, b.art"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'E'"
                + " AND    a.ssqss = '0000' "
                + " AND    b.ART = '04'"
                + " AND    a.matnr || a.werks IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where werks IN (" + DDRSessionEntity.Current.plantcode + ") AND ART = '01' )"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_BOTH_PROC(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.werks, a.matnr, a.beskz, a.ssqss, b.art
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'X'"
                + " AND    a.ssqss = '0001'"
                + " AND    b.art = '04'"
                + " AND    a.matnr || a.werks IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where werks IN (" + DDRSessionEntity.Current.plantcode + ") AND ART = '01')"
                + " UNION"
                + " SELECT a.werks, a.matnr, a.beskz, a.ssqss, b.art"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'X'"
                + " AND    a.ssqss <> '0001'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_ProcE_no_04(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.werks, a.beskz, a.ssqss
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'E'"
                + " AND    a.ssqss = '0000' "
                + " AND    a.matnr || a.werks NOT IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where ART = '04' )");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_ProcF_no_01(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.werks, a.beskz, a.ssqss
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'F'"
                + " AND    a.ssqss = '0001' "
                + " AND    a.matnr || a.werks NOT IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where ART = '01' )");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_ProcX_no_04(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.werks, a.beskz, a.ssqss
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'X'"
                + " AND    a.matnr || a.werks NOT IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where ART = '04' )");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_ProcX_no_01(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.werks, a.beskz, a.ssqss
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'X'"
                + " AND    a.matnr || a.werks NOT IN ( SELECT matnr || werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat where ART = '01' )");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_ProcE_not_0000(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.werks, a.beskz, a.ssqss
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'E'"
                + " AND    a.ssqss = '0001'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_ProcF_not_0001(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.werks, a.beskz, a.ssqss
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.beskz = 'F'"
                + " AND    a.ssqss = '0000'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_01_insplot_NB(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '01' AND CHG IS NOT NULL "
                + " order by werks, matnr, art"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_08_insplot_NB(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '08' AND CHG IS NOT NULL "
                + " order by werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_04_insplot_NX(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '04' AND ( CHG <> 'X' or chg is null )"
                + " order by werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_04_insplot_NY(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '04' AND ( chg <> 'Y' OR chg is NULL )"
                + " order by werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_05_insplot_E2(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '05' AND CHG = '2' "
                + " order by werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_05_insplot_EB(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '05' AND CHG IS NULL "
                + " order by werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_05_insplot_N2B(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT art, matnr, werks, chg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND art = '05' AND ( chg is not null and chg <> '2')"
                + " order by werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_01_insplot_blank(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, art, chg
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE  werks IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    art = '01' "
                + " AND    chg IS NULL "
                + " ORDER BY werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_04_insplot_blank(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, art, chg
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE  werks IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    art = '04' "
                + " AND    chg IS NULL "
                + " ORDER BY werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_0105_insplot_blank(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, art, chg
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE  werks IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    art = '0105' "
                + " AND    chg IS NULL "
                + " ORDER BY werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insptype_insplot_blank(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, art, chg
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat"
                + " WHERE  werks IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    art not in ('01', '04', '0105') "
                + " AND    chg IS not NULL "
                + " ORDER BY werks, matnr, art");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_BOM_COMP_SORT_ACE(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.werks,a.matnr, a.sfcpf, a.kzech, b.sortf, c.mtart 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail b,  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara c "
                + " WHERE a.matnr = c.matnr AND a.matnr = b.matnr AND a.werks = b.werks AND a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND ( b.sortf like '%A%' OR b.sortf like '%E%' OR b.sortf like '%C%' )"
                + " order by werks, matnr");
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
