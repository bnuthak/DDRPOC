using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;


namespace DDR.BusinessLogic
{
    public class QMVerificationReport
    {
        QMDataManager datamanager = new QMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
        MMDataManager mmdatamanager = new MMDataManager();

        public System.Data.DataTable SQL_QM_QM_insp_plan(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.WERKS, A.PLNNR, A.PLNAL, A.VERWE, A.LTEXT, A.STATU, A.PLNME, 
                B.STEUS, B.ARBPL, B.LTXA1,
	            C.MERKNR, C.QPMK_WERKS, C.STICHPRVER 
	            from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp a,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_op b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_char c"
                + " WHERE a.werks IN (" + DDRSessionEntity.Current.plantcode + ") AND a.plnnr = b.plnnr AND a.plnal = b.plnal "
                + " AND b.plnnr = c.plnnr AND b.plnal = c.plnal AND b.arbpl = c.arbpl "
                + " Order by werks, plnnr, plnal, arbpl");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_insp_plan_alloc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr,plnnr, plnal, werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_insp_alloc a"
                + " WHERE werks IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " and exists (select 1 from  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r"
                + " where a.matnr = r.matnr and r.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " order by werks, matnr, plnnr, plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_BSS(string title)
        {
            try
            {
                string command = String.Format(@"
				select a.PTMAN,a.WERKS,a.auart,b.MATNR, c.CLASS, d.CHARA, d.Value,b.ZAEHL,
                b.CHASP,b.CHSPL, b.CHMDG, b.KZAME, b.CHMVS,b.CHVLL,b.CHVSK,b.SRTSQ
       	        FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_Batch_Search a, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_Batch_Sub_mat b,"
                   + DDRSessionEntity.Current.table_schema + "_procs.GDD_BATCH_MAT_CLASS c, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_BATCH_CLASS_CHAR d"
                + " WHERE a.werks IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND a.ptman = b.ptman AND a.werks = b.werks"
                + " AND b.ptman = c.ptman AND b.werks = c.werks AND b.matnr = c.matnr"
                + " AND c.ptman = d.ptman AND c.werks = d.werks AND c.matnr = d.matnr AND c.class = d.class"
                + " order by werks, ptman, matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        public System.Data.DataTable SQL_QM_extract_qm_insp(string title)
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
        public System.Data.DataTable SQL_QM_QM_Qinfo(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct q.MATNR, q.WERKS, 
                case 
                when l.KTOKK = 'Z001' then 'VN/MP'   
                when l.KTOKK = 'Z003' then 'VN'   
                when l.KTOKK||w.PARVW = 'Z004ZT' then 'MP/ZT'  
                when l.KTOKK = 'Z004' then 'MP'  
                when l.KTOKK||w.parvw = 'Z007ZT' then 'VN/MP/ZT'   
                when l.KTOKK = 'Z007' then 'VN/MP'  
                when nvl(l.KTOKK,'~~') = '~~' then ''
                else 'Other'
                end  vtype,  
                q.OLD_LIEFERANT, q.LIEFERANT, decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**') name1,
                    to_char(q.FREI_DAT, 'DD/MM/YYYY')FREI_DAT1,
                    q.STSMA, q.NOINSP, q.ZAEHL,LOEKZ, q.FREI_MGKZ,
                    q.QSSYSFAM,q.VARIABNAHM,q.VORLABN,q.SPERRFKT,q.SPERRGRUND
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qinf q, " + DDRSessionEntity.Current.mapinstance + ".lfa1@" + DDRSessionEntity.Current.mapinstance + " l, " + DDRSessionEntity.Current.mapinstance + ".wyt3@" + DDRSessionEntity.Current.mapinstance + " w"
                + " WHERE  q.lieferant = l.lifnr(+)"
                + " and  q.lieferant = w.lifnr(+) and w.parvw(+) = 'ZT'"
                + " and  q.werks IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " and exists (select 1 from  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r"
                    + " where q.matnr = r.matnr and r.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " order by decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**'), q.MATNR, q.WERKS, q.OLD_LIEFERANT,q.LIEFERANT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_QM_QM_Qinfo_sap(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct q.MATNR, q.WERK, 
                case 
                when l.KTOKK = 'Z001' then 'VN/MP'   
                when l.KTOKK = 'Z003' then 'VN'   
                when l.KTOKK||w.PARVW = 'Z004ZT' then 'MP/ZT'  
                when l.KTOKK = 'Z004' then 'MP'  
                when l.KTOKK||w.parvw = 'Z007ZT' then 'VN/MP/ZT'   
                when l.KTOKK = 'Z007' then 'VN/MP'  
                when nvl(l.KTOKK,'~~') = '~~' then ''
                else 'Other'
                end  vtype,  
                q.LIEFERANT, decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**') name1,
                    to_date(q.FREI_DAT, 'YYYYMMDD')FREI_DAT1,
                    q.STSMA, q.NOINSP, q.ZAEHL,LOEKZ, q.FREI_MGKZ,
                    q.QSSYSFAM,q.VARIABNAHM,q.VORLABN,q.SPERRFKT,q.SPERRGRUND
                FROM   z_gdd_qinf_v01@" + DDRSessionEntity.Current.mapinstance + " q, " + DDRSessionEntity.Current.mapinstance + ".lfa1@" + DDRSessionEntity.Current.mapinstance + " l, " + DDRSessionEntity.Current.mapinstance + ".wyt3@" + DDRSessionEntity.Current.mapinstance + " w"
                + " WHERE  q.lieferant = l.lifnr(+)"
                + " and  q.lieferant = w.lifnr(+) and w.parvw(+) = 'ZT'"
                + " and  q.werk IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " and exists (select 1 from  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r"
                    + " where q.matnr = r.matnr and r.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and to_date(q.erstelldat, 'YYYYMMDD')+45 > sysdate"
                + " order by decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**'), q.MATNR, q.WERK, q.LIEFERANT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable GetQMVerificationTotalReport()
        {
            try
            {
                /*
                string command = String.Format(@"
                SELECT distinct q.MATNR, q.WERK, 
                    case 
                      when l.KTOKK = 'Z001' then 'VN/MP'   
                      when l.KTOKK = 'Z003' then 'VN'   
                      when l.KTOKK = 'Z004' then 'MP'  
                      when l.KTOKK = 'Z007' then 'VN/MP'  
                      when nvl(l.KTOKK,'~~') = '~~' then ''
                      else 'Other'
                      end  vtype,  
                      q.LIEFERANT, decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**') name1,
                      to_date(q.FREI_DAT, 'YYYYMMDD')FREI_DAT1,
                      q.STSMA, q.NOINSP, q.ZAEHL,LOEKZ, q.FREI_MGKZ,
                      q.QSSYSFAM,q.VARIABNAHM,q.VORLABN,q.SPERRFKT,q.SPERRGRUND
                 FROM  z_gdd_qinf_v01@GDV q, gdd_procs.gdd_lfa1 l--,gdd_procs.wyt3 w
                 WHERE q.lieferant = l.lifnr(+)
                       and  q.werk IN ('0071') 
                       order by decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**'), q.MATNR, q.WERK, q.LIEFERANT
               ");
                */
                string SQL1 = String.Format(@"
                SELECT distinct q.MATNR, q.WERK, 
                      case 
                      when l.KTOKK = 'Z001' then 'VN/MP'   
                      when l.KTOKK = 'Z003' then 'VN'   
                      when l.KTOKK||w.PARVW = 'Z004ZT' then 'MP/ZT'  
                      when l.KTOKK = 'Z004' then 'MP'  
                      when l.KTOKK||w.parvw = 'Z007ZT' then 'VN/MP/ZT'   
                      when l.KTOKK = 'Z007' then 'VN/MP'  
                      when nvl(l.KTOKK,'~~') = '~~' then ''
                      else 'Other'
                      end  vtype,q.LIEFERANT, decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**') name1,
                      to_date(q.FREI_DAT, 'YYYYMMDD')FREI_DAT1,
                      q.STSMA, q.NOINSP, q.ZAEHL,LOEKZ, q.FREI_MGKZ,
                      q.QSSYSFAM,q.VARIABNAHM,q.VORLABN,q.SPERRFKT,q.SPERRGRUND
                 FROM ");
                string SQL2 = "z_gdd_qinf_v01@" + DDRSessionEntity.Current.mapinstance + " q, ";
                string SQL3 = DDRSessionEntity.Current.mapinstance + ".lfa1@" + DDRSessionEntity.Current.mapinstance + " l, "; //+ "gdd_procs.gdd_lfa1 l, ";
                string SQL4 = DDRSessionEntity.Current.mapinstance + ".wyt3@" + DDRSessionEntity.Current.mapinstance + " w ";
                string SQL5=string.Format(@"WHERE  q.lieferant = l.lifnr(+)
                        and  q.lieferant = w.lifnr(+) and w.parvw(+) = 'ZT'
                        and  q.werk IN ");
                string SQL6 = "(" + DDRSessionEntity.Current.plantcode + ")"; //"('"+UserEntity.plantcode+"')";
                string SQL7 = String.Format(@" and exists (select 1 from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r where q.matnr = r.matnr and r.aland = ");
                string SQL8 = "'" + DDRSessionEntity.Current.SiteCode + "')";
                string SQL9=string.Format(@"
                        and to_date(q.erstelldat, 'YYYYMMDD')+45 > sysdate
                        order by decode(l.lifnr,q.lieferant,l.name1,'**Does Not Exist in R/3**'), q.MATNR, q.WERK, q.LIEFERANT");

                string command = SQL1 + SQL2 + SQL3 + SQL4 + SQL5 + SQL6 + SQL7 + SQL8 + SQL9;
                //System.Data.DataSet qmdataset = datamanager.GetQMVerificationData(command);
                System.Data.DataTable qmdataset = TableHeader.QMVerificationtableheader(datamanager.GetQMVerificationData(command));

                return qmdataset;
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }
    }
}
