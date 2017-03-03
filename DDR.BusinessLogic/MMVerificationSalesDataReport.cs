using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class MMVerificationSalesDataReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_MM_extract_salesgen(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, b.werks, a.brgew, a.gewei, a.ntgew, a.tragr, b.sernp, b.ladgr, b.prctr
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b "
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X')"
                + " AND   a.matnr = b.matnr"
                + " AND   b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND (nvl(a.brgew,'0') <> '0' OR  "
                    + " nvl(a.gewei,' ') <> ' ' OR "
                    + " nvl(a.ntgew,'0') <> '0' OR "
                    + " nvl(a.tragr,' ') <> ' ' OR "
                    + " nvl(b.ladgr,' ') <> ' ') "
                + " ORDER by a.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_MM_extract_salesorg1(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, c.vkorg, c.vtweg, c.vrkme, c.vmsta, c.dwerk, c.aumng, c.lfmng, c.scmng, c.schme, c.rdprf, c.sktof  
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d"
                + " WHERE a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' " 
                + " AND   c.vkorg in (" + DDRSessionEntity.Current.salesorgcode + ")"
                + " ORDER BY a.matnr, c.vkorg, c.vtweg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_MM_extract_salesorg2(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr,  a.mtart, c.vkorg, c.vtweg, c.versg, c.kondm, c.bonus, c.ktgrm, c.mtpos, c.mvgr1, c.mvgr2, c.mvgr3, c.mvgr4, c.mvgr5,
                    c.prat1, c.prat2, c.prat3, c.prat4, c.prat5
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d"
                + " WHERE a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' " 
                + " AND   c.vkorg in (" + DDRSessionEntity.Current.salesorgcode + ")"
                + " ORDER BY a.matnr, c.vkorg, c.vtweg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_MM_extract_taxes(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.aland, a.taxm1, a.taxm2, a.taxm3, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke d,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_scm_tax_ref e"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = d.matnr "
                + " AND   d.vkorg in (" + DDRSessionEntity.Current.salesorgcode + ")"
                + " AND   b.aland in '" + DDRSessionEntity.Current.SiteCode + "' " 
                + " AND   d.vkorg = e.vkorg"
                + " AND   d.vtweg = e.vtweg"
                + " AND   a.aland = e.tax_aland"
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

        public System.Data.DataTable SQL_MM_extract_salestext(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.vkorg, c.vtweg, c.spras, c.lineno,
                    c.ltext, c.stdtxt, c.tdformat, c.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_salestext c"
                + " WHERE a.matnr = c.matnr"
                + " AND   c.vkorg in (" + DDRSessionEntity.Current.salesorgcode + ")"
                + " ORDER BY c.matnr, c.vkorg, c.spras, c.lineno");
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
