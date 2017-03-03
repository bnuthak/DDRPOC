using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class ConsistencyChecksMMClashesReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();

        public System.Data.DataTable SQL_Consistency_mm_mara_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.aland, a.mtart, a.mtart as ddt_mtart, b.mtart as gpr_mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr AND a.matnr = c.matnr AND nvl(a.mtart,'XX') != b.mtart AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        public System.Data.DataTable SQL_Consistency_mm_marm_clash(string title, string report_Type)
        {
            string x = "";
            if (report_Type == "meabm")
            {
                x = " ";           
            }
            else
            {
                x = "00";
            }
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.aland,a.meinh, a." + report_Type + " as ddt_" + report_Type + ", b." + report_Type + " as gpr_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c "
                + " WHERE a.matnr = b.matnr AND a.matnr = c.matnr AND nvl(a." + report_Type + ",'" + x + "') != b." + report_Type + " AND a.meinh = b.meinh AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_makt_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.aland,a.spras, a.maktx as ddt_maktx, b.maktx as gpr_maktx
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_makt_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr AND a.matnr = c.matnr AND nvl(a.maktx,'XX') != b.maktx  AND a.spras = b.spras AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_mean_clash(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.aland,a.ean11, a.meinh, a." + report_Type + " as ddt_" + report_Type + ", b." + report_Type + " as gpr_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mean a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mean_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c "
                + " WHERE a.matnr = b.matnr AND a.matnr = c.matnr AND a.ean11 = b.ean11 AND a.meinh = b.meinh "
                + " AND nvl(a." + report_Type + ",'XX') != b." + report_Type + " AND a.meinh = b.meinh AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_mm_mlan_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.aland, a.taxm1 as ddt_taxm1, b.taxm1 as gpr_taxm1
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mlan_clash b"
                + " WHERE a.matnr = b.matnr  AND nvl(a.taxm1,' ') != b.taxm1"
                + " AND   a.aland IN (SELECT iso_code FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_site_taxes WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref  WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X')"
                + " AND   a.aland = b.aland"
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_prounit_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr,a.sapid,a.wsmei, a.atwrt as ddt_atwrt, (b.umren/b.umrez)*100 as gpr_atwrt
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2 a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE a.matnr = b.matnr  AND nvl(a.atwrt,'00') != (b.umren/b.umrez)*100  AND a.wsmei = b.meinh"
                + " AND   a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref  WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X')"
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_class_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.sapid, a.objek, a.atinn,d.atnam, a.atwrt as ddt_atwrt, b.atwrt as gpr_atwrt
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_ausp_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ksml l, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_klah h"
                + " WHERE a.objek = b.objek AND a.objek = c.matnr AND a.atinn  = b.atinn AND a.atinn = d.atinn"
                + " AND d.atinn = l.imerk AND l.clint = h.clint AND h.class = 'Z_GLOBAL'"
                + " AND nvl(a.atwrt,'XX') != nvl(b.atwrt,'XX') AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ORDER by a.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_mrsl_tsl_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.aland, a.mtart, a.MHDRZ as ddt_MHDRZ, b.MHDRZ as gpr_MHDRZ
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr AND a.matnr = c.matnr AND nvl(a.MHDRZ,'0') != nvl(b.MHDRZ,'0') AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_format_id_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.sapid, a.objek, e.mtart, a.atinn, d.atnam, a.atwrt as ddt_atwrt, b.atwrt as gpr_atwrt
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_ausp_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ksml l, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_klah h, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara e"
                + " WHERE a.objek = b.objek AND a.objek = c.matnr AND a.objek = e.matnr AND a.atinn  = b.atinn AND a.atinn = d.atinn"
                + " AND d.atinn = l.imerk AND l.clint = h.clint AND h.class = 'Z_GLOBAL'"
                + " AND nvl(a.atwrt,'XX') != nvl(b.atwrt,'XX') AND a.atinn in (952, 954) AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ORDER by a.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_Acct_Cost_clash_a(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct b.matnr, b.bwkey as DDT_Plant, h.bwkey as SAP_Plant, b." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew b, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mbew_clash h  "
                + " WHERE b.matnr = h.matnr AND b.bwkey = h.bwkey AND nvl(b." + report_Type + ",' ') != h." + report_Type + "  "
                + " AND   b.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
                + " AND   b.bwkey in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by b.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_mm_Acct_Cost_clash_losgr(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks AND nvl(c." + report_Type + ",0) != h." + report_Type + "  "
                + " AND   c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
                + " AND   c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_mm_Acct_Cost_clash_sobsk(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks AND nvl(c." + report_Type + ",' ') != h." + report_Type + "  "
                + " AND   c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
                + " AND   c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_mm_AltUoM_clash(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct m.matnr,m.meinh, m." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + ""
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm m, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash h "
                + " WHERE m.matnr = h.matnr and m.meinh  = h.meinh "
                + " AND nvl(m." + report_Type + ",'0') != h." + report_Type + "  "
                + " AND m.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " ORDER by m.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public System.Data.DataTable SQL_Consistency_mm_MRP_clash(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + ""
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks AND nvl(c." + report_Type + ",'0') != h." + report_Type + "" 
                + " AND   c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
                + " AND   c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_PURCH_clash_a(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + ""
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h "
                + " WHERE a.matnr = h.matnr  AND nvl(a." + report_Type + ",' ') != h." + report_Type + " "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_mm_PURCH_clash_b(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c." + report_Type + ",' ') != h." + report_Type + "  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Consistency_mm_QM_clash_prfrq(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + "  "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
                + " WHERE c.matnr = h.matnr "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " )"
                + " AND c.werks = h.werks AND nvl(c." + report_Type + ",0) <> h." + report_Type + " "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_QM_clash_qmata(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.qmata as ddt_qmata, h.qmata as sap_qmata   
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "  
                + " WHERE c.matnr = h.matnr"
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')"
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " )"
                + " AND c.werks = h.werks AND nvl(c.qmata,' ') <> h.qmata "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_QM_clash_kzdkz(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.kzdkz as ddt_kzdkz, h.kzdkz as sap_kzdkz   
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND c.werks = h.werks AND nvl(c.kzdkz,' ') <> h.kzdkz "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_QM_clash_mmsta(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.mmsta as ddt_mmsta, h.mmsta as sap_mmsta   
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND c.werks = h.werks AND nvl(c.mmsta,' ') <> h.mmsta "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_QM_clash_mmstd(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.mmstd as ddt_mmstd, h.mmstd as sap_mmstd   
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND c.werks = h.werks AND nvl(c.mmstd,'00000000') <> h.mmstd "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_QM_clash_qmpur(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, c.werks as DDT_Plant, a.qmpur as ddt_qmpur, h.qmpur as sap_qmpur   
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h "
                + " WHERE c.matnr = h.matnr AND a.matnr = c.matnr "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
                + " AND nvl(a.qmpur,' ') <> h.qmpur "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_QM_clash_ssqss(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.ssqss as ddt_ssqss, h.ssqss as sap_ssqss   
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND c.werks = h.werks AND nvl(c.ssqss, ' ') <> h.ssqss "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //    public System.Data.DataTable SQL_Consistency_mm_QM_Z131_clash(string title, string report_Type)
        //    {
        //        try
        //        {
        //            string command = String.Format(@"
        //SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + "  "
        //            + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
        //            + " WHERE c.matnr = h.matnr "
        //            + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
        //            + " AND c.werks = '0131' and h.werks = 'Z131' AND nvl(c." + report_Type + ",0) <> h." + report_Type + " "
        //            + " ORDER by c.matnr");
        //            System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //            mmdataset.TableName = title;
        //            return mmdataset;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    public System.Data.DataTable SQL_Consistency_mm_QM_Z413_clash(string title, string report_Type)
        //    {
        //        try
        //        {
        //            string command = String.Format(@"
        //SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + "  "
        //            + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
        //            + " WHERE c.matnr = h.matnr "
        //            + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X') "
        //            + " AND c.werks = '0413' and h.werks = 'Z413' AND nvl(c." + report_Type + ",0) <> h." + report_Type + " "
        //            + " ORDER by c.matnr");
        //            System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //            mmdataset.TableName = title;
        //            return mmdataset;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    public System.Data.DataTable SQL_Consistency_mm_QM_Z314_clash(string title, string report_Type)
        //    {
        //        try
        //        {
        //            string command = String.Format(@"
        //SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + "  "
        //            + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
        //            + " WHERE c.matnr = h.matnr "
        //            + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
        //            + " AND c.werks = '0314' and h.werks = 'Z314' AND nvl(c." + report_Type + ",0) <> h." + report_Type + " "
        //            + " ORDER by c.matnr");
        //            System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //            mmdataset.TableName = title;
        //            return mmdataset;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    public System.Data.DataTable SQL_Consistency_mm_QM_Z004_clash(string title, string report_Type)
        //    {
        //        try
        //        {
        //            string command = String.Format(@"
        //SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + "  "
        //            + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h  "
        //            + " WHERE c.matnr = h.matnr "
        //            + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')  "
        //            + " AND c.werks = '0004' and h.werks = 'Z004' AND nvl(c." + report_Type + ",0) <> h." + report_Type + " "
        //            + " ORDER by c.matnr");
        //            System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //            mmdataset.TableName = title;
        //            return mmdataset;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_mhdhb(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.mhdhb as ddt_mhdhb, h.mhdhb as sap_mhdhb 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.mhdhb,'0') != h.mhdhb  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_mhdrz(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.mhdrz as ddt_mhdrz, h.mhdrz as sap_mhdrz 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.mhdrz,0) != h.mhdrz  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_ausme(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.ausme as ddt_ausme, h.ausme as sap_ausme 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c.ausme,' ') != h.ausme  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                    + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_etifo(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.etifo as ddt_etifo, h.etifo as sap_etifo 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.etifo,' ') != h.etifo  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_etiar(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.etiar as ddt_etiar, h.etiar as sap_etiar 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.etiar,' ') != h.etiar  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_abcin(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.abcin as ddt_abcin, h.abcin as sap_abcin 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c.abcin,' ') != h.abcin  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_ccfix(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.ccfix as ddt_ccfix, h.ccfix as sap_ccfix 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c.ccfix,' ') != h.ccfix  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_xchpf(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.xchpf as ddt_xchpf, h.xchpf as sap_xchpf 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.xchpf,' ') != h.xchpf  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_mhdlp(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.mhdlp as ddt_mhdlp, h.mhdlp as sap_mhdlp 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.mhdlp,'0') != h.mhdlp  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_xmcng(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.xmcng as ddt_xmcng, h.xmcng as sap_xmcng 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c.xmcng,' ') != h.xmcng  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_loggr(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.loggr as ddt_loggr, h.loggr as sap_loggr 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c.loggr,' ') != h.loggr  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_sernp(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c.sernp as ddt_sernp, h.sernp as sap_sernp 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h "
                + " WHERE c.matnr = h.matnr AND c.werks = h.werks  "
                + " AND nvl(c.sernp,' ') != h.sernp  "
                + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " ORDER by c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_tempb2(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.tempb2 as ddt_tempb2, h.tempb2 as sap_tempb2 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.tempb2,' ') != h.tempb2  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_STOR_clash_raube2(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.mtart as ddt_mtart, a.raube2 as ddt_raube2, h.raube2 as sap_raube2 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash h  "
                + " WHERE a.matnr = h.matnr  AND nvl(a.raube2,' ') != h.raube2  "
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_TAX_clash(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct m.matnr,h.aland, m." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + ""
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan m, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mlan_clash h"
                + " WHERE m.matnr = h.matnr AND nvl(m." + report_Type + ",' ') != h." + report_Type + " "
                + " AND   m.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')"
                + " AND   h.aland in (select iso_code from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_site_taxes where site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + " )"
                + " AND   m.aland = h.aland"
                + " ORDER by m.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mm_WKSCH_clash(string title, string report_Type)
        {
            if ((report_Type == "uneto") ||
                (report_Type == "ueeto") ||
                (report_Type == "basmg"))
            {
                try
                {
                    string command = String.Format(@"
				    SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + ""
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h"
                    + " WHERE c.matnr = h.matnr AND c.werks = h.werks "
                    + " AND nvl(c." + report_Type + ",'0') != h." + report_Type + " "
                    + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')"
                    + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " )"
                    + " ORDER by c.matnr");
                    System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                    mmdataset.TableName = title;
                    return mmdataset;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    string command = String.Format(@"
				    SELECT distinct c.matnr, c.werks as DDT_Plant, h.werks as SAP_Plant, c." + report_Type + " as ddt_" + report_Type + ", h." + report_Type + " as sap_" + report_Type + ""
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marc_clash h"
                    + " WHERE c.matnr = h.matnr AND c.werks = h.werks "
                    + " AND nvl(c." + report_Type + ",' ') != h." + report_Type + " "
                    + " AND c.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load ='X')"
                    + " AND c.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " )"
                    + " ORDER by c.matnr");
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
        public System.Data.DataTable SQL_Consistency_all_mara_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.mtart, a.mtart as ddt_mtart, b.mtart as gpr_mtart 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash b "
                + " WHERE a.matnr = b.matnr AND nvl(a.mtart,'XX') != b.mtart ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_makt_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.spras, a.maktx as ddt_maktx, b.maktx as gpr_maktx
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_makt_clash b"
                + " WHERE a.matnr = b.matnr AND nvl(a.maktx,'XX') != b.maktx  AND a.spras = b.spras ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.meinh, a." + report_Type + " as ddt_" + report_Type + ", b." + report_Type + " as gpr_" + report_Type + ""
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE a.matnr = b.matnr AND nvl(a." + report_Type + ",'00') != b." + report_Type + " AND a.meinh = b.meinh ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash_meabm(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.meinh, a.MEABM as ddt_MEABM, b.MEABM as gpr_MEABM 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b "
                + " WHERE a.matnr = b.matnr AND nvl(a.MEABM,' ') != b.MEABM AND a.meinh = b.meinh  ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash_sapid(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.meinh, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE a.matnr = b.matnr AND a.meinh = b.meinh  AND nvl(a.sapid,'1') != 'X' ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash_sapid1(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.meinh, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE a.matnr = b.matnr (+) AND a.meinh = b.meinh (+)  AND nvl(a.sapid,'1') = 'X' AND b.matnr is null ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash_atwrt(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.wsmei, a.atwrt as ddt_poten, b.umren/b.umrez*100 as gpr_poten
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2 a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE a.matnr = b.matnr AND a.wsmei = b.meinh AND b.umren/b.umrez*100 != a.atwrt ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash_poten(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.matnr, b.meinh, a.atwrt, b.umren/b.umrez*100 as gpr_poten
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2 a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE b.matnr = a.matnr (+) AND b.meinh = a.wsmei (+) AND b.meinh in ('BKG', 'BGM') AND a.matnr is null ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_marm_clash_poten1(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.wsmei, a.atwrt, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2 a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_marm_clash b"
                + " WHERE a.matnr = b.matnr (+) AND a.wsmei = b.meinh (+) AND nvl(a.sapid,'Z') = 'X' AND b.meinh is null ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_mean_clash(string title, string report_Type)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.meinh, a." + report_Type + " as ddt_" + report_Type + ", b." + report_Type + " as gpr_" + report_Type + " "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mean a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mean_clash b"
                + " WHERE a.matnr = b.matnr AND a.meinh = b.meinh  AND a.ean11 = b.ean11 AND nvl(a." + report_Type + ",'XX') != b." + report_Type + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_mean_clash_sapidx(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.meinh, a.ean11, a.sapid 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mean a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mean_clash b "
                + " WHERE a.matnr = b.matnr (+) AND a.meinh = b.meinh (+) AND a.ean11 = b.ean11 (+) AND NVL(a.sapid,'Z') = 'X' AND b.meinh is null ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_mean_clash_gpr(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.matnr, b.meinh, b.ean11 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mean a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mean_clash b "
                + " WHERE a.matnr (+) = b.matnr AND a.meinh (+) = b.meinh and a.ean11 (+) = b.ean11 AND a.meinh is null ORDER by b.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_all_ausp_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.sapid, a.objek, a.atinn, d.atnam, a.atwrt as ddt_atwrt, b.atwrt as gpr_atwrt 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_ausp_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ksml l, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_klah h "
                + " WHERE a.objek = b.objek AND a.atinn  = b.atinn AND a.atinn = d.atinn "
                + " AND d.atinn = l.imerk AND l.clint = h.clint AND h.class = 'Z_GLOBAL' "
                + " AND nvl(a.atwrt,'XX') != nvl(b.atwrt,'XX') AND a.atinn not in (952, 954) ORDER by a.atinn, objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_format_id_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.sapid, a.objek, c.mtart, a.atinn, d.atnam, a.atwrt as ddt_atwrt, b.atwrt as gpr_atwrt
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_ausp_clash b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ksml l, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_klah h,  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara c"
                + " WHERE a.objek = b.objek AND a.objek = c.matnr AND a.atinn  = b.atinn AND a.atinn = d.atinn"
                + " AND d.atinn = l.imerk AND l.clint = h.clint AND h.class = 'Z_GLOBAL'"
                + " AND nvl(a.atwrt,'XX') != nvl(b.atwrt,'XX') AND a.atinn in (952, 954) ORDER by a.atinn, objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Consistency_mrsl_tsl_clash(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.mtart, a.MHDRZ as ddt_MHDRZ, b.MHDRZ as gpr_MHDRZ 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_mara_clash b "
                + " WHERE a.matnr = b.matnr AND nvl(a.MHDRZ,'0') != nvl(b.MHDRZ,'0') ORDER by a.matnr");
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
