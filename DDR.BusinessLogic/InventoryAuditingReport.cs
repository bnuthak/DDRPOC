using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

// Some reports had commented (--) code in the original ColdFusion files.
// Comments don't work in this string format, so they have been removed.

//Some ColdFusion reports were not used in Objects on this page because
//they were not referenced in the original code in the D or P DDR.

namespace DDR.BusinessLogic
{
    public class InventoryAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();

        public System.Data.DataTable SQL_Inventory_QUERY105(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, CHARG
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    TRANSLATE(upper(charg), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-', '^') <> '^'"
                + " AND    TRANSLATE(upper(charg), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-', '^') <> '^^'"
                + " ORDER BY MATNR, WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY64(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + " (SELECT MATNR,WERKS,CHARG "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                    + " WHERE  BWART = '561'"
                    + " INTERSECT"
                        + " SELECT MATNR,WERKS,CHARG "
                        + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                        + " WHERE  BWART in ('563','565')"
                        + " ORDER BY MATNR,WERKS,CHARG"
                    + " ) B"
                + " WHERE  B.MATNR = A.MATNR"
                + " AND    B.WERKS = A.WERKS"
                + " AND    B.CHARG = A.CHARG   "
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.BWART");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY16(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.CHARG, A.WERKS, B.BWART, A.BATCH_USE_RESTRICTION
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    A.WERKS = B.WERKS"
                + " AND    A.LGORT = B.LGORT"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.BATCH_USE_RESTRICTION,'XX') <> 'XX'"
                + " AND    B.BWART <> '561'"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, B.BWART, A.BATCH_USE_RESTRICTION");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY20(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, B.BWART, A.BATCH_USE_RESTRICTION
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    A.WERKS = B.WERKS"
                + " AND    A.LGORT = B.LGORT"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.BATCH_USE_RESTRICTION,'XX') = 'XX'"
                + " AND    B.BWART = '561'"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, B.BWART, A.BATCH_USE_RESTRICTION");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY217(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.batch_use_restriction, d.bwart  
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 c,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im d"
                + " WHERE  a.matnr = c.matnr (+)"
                + " AND  a.charg = c.charg (+)"
                + " AND  a.matnr = b.matnr"
                + " AND  a.matnr = d.matnr"
                + " AND  a.werks = d.werks"
                + " AND  a.lgort = d.lgort"
                + " AND  a.charg = d.charg"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(a.batch_use_restriction,'XX') = 'XX'"
                + " AND  d.bwart = '561'"
                + " AND  NVL(c.matnr,'XX') = 'XX'"
                + " AND  NVL(c.charg,'XX') = 'XX'"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY61(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.LGORT, B.MTART, B.XCHPF
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.CHARG,'XX') = 'XX'"
                + " AND    B.XCHPF = 'X'"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY62(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.LGORT, B.MTART, B.XCHPF
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.CHARG,'XX') <> 'XX'"
                + " AND    (NVL(B.XCHPF,'XX') = 'XX' OR B.XCHPF = ' ')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY63(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, B.MTART, B.XCHPF
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.CHARG,'XX') = 'XX'"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY24(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, charg, lgort, batch_use_restriction, 
                    qndat, lwedt, potency,
                    mfg_start_date, pkg_start_date,
                    ea_to_bundles, ea_to_cases, ea_to_pallets,
                    first_receipt_date, list_of_countries
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars"
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(charg,'XX') = 'XX'"
                + " AND    ((NVL(batch_use_restriction,'XX') <> 'XX') OR"
                    + " (potency > 0) OR "
                    + " (NVL(qndat,'XX') <> 'XX') OR "
                    + " (NVL(lwedt,'XX') <> 'XX') OR "
                    + " (NVL(mfg_start_date,'XX') <> 'XX') OR "
                    + " (NVL(pkg_start_date,'XX') <> 'XX') OR "
                    + " (NVL(ea_to_cases,'XX') <> 'XX') OR "
                    + " (NVL(ea_to_bundles,'XX') <> 'XX') OR "
                    + " (NVL(ea_to_pallets,'XX') <> 'XX') OR"
                    + " (NVL(first_receipt_date,'XX') <> 'XX') OR "
                    + " (NVL(list_of_countries,'XX') <> 'XX')"
                    + " )"
                + " ORDER BY matnr, werks, charg, lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY111(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.BATCH_USE_RESTRICTION, B.ATWRT, C.WERKS as CWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM P,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C,"
                    + " (SELECT X.MATNR,X.CHARG,Z.ATINN,Z.ATWRT "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 X,"
                        + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP Z"
                    + " WHERE  X.CUOBJ_BM = Z.OBJEK AND Z.ATINN = '876') B "
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    P.MATNR = A.MATNR"
                + " AND    P.CHARG = A.CHARG"
                + " AND    P.WERKS = A.WERKS"
                + " AND    P.LGORT = A.LGORT"
                + " AND    P.MATNR = C.MATNR (+)"
                + " AND    P.WERKS = C.WERKS (+)"
                + " AND    P.CHARG = C.CHARG (+)"
                + " AND    P.BWART = '561'"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    B.ATINN = '876'"
                + " AND    B.ATWRT is null"
                + " AND    A.BATCH_USE_RESTRICTION IS NULL"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY228(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, lgort, charg, original_mfg
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i"
                + " WHERE  ltrim(original_mfg,'0') NOT IN (SELECT ltrim(lifnr,'0') FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lfa1)"
                + " AND    werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    original_mfg is not null "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY83(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, B.MTART
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND (B.MTART <> 'FERT' AND B.MTART <> 'ZUNB')"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND A.MATNR NOT IN (SELECT MATNR FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_BOMDETAIL "
                    + " UNION"
                    + " SELECT IDNRK FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_BOMDETAIL)"
                + " Order by A.MATNR,A.WERKS,A.CHARG,B.MTART");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY01(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR,A.WERKS,A.CHARG,A.HSDAT,B.MHDHB
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND   A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS"
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(A.HSDAT,'XX') <> 'XX'"
                + " AND B.MHDHB = 0"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY02(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Distinct A.MATNR,A.WERKS,A.CHARG,A.HSDAT,B.MHDHB
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(A.HSDAT,'XX') = 'XX'"
                + " AND B.MHDHB > 0"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.HSDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY04(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.HSDAT, B.MHDHB, A.VFDAT, TO_CHAR(TO_DATE(A.HSDAT,'YYYYMMDD') + B.MHDHB, 'YYYYMMDD') AS CalcDate
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  A.HSDAT is not null and A.HSDAT != ' '"
                + " AND  B.MHDHB > 0"
                + " AND  TO_DATE(A.HSDAT,'YYYYMMDD') + B.MHDHB <> TO_DATE(A.VFDAT,'YYYYMMDD')"
                + " ORDER BY A.MATNR,A.WERKS,A.CHARG,A.VFDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY55(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.HSDAT
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                + " WHERE  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.HSDAT,'XX') <> 'XX'"
                + " AND    A.HSDAT > TO_CHAR(SYSDATE,'YYYYMMDD')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.HSDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY05(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.VFDAT, B.MHDRZ    
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.VFDAT,'XX') <> 'XX'"
                + " AND    B.MHDRZ = 0 "
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY06(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.VFDAT, B.MHDRZ    
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.VFDAT,'XX') = 'XX'"
                + " AND    B.MHDRZ = 1"
                + " ORDER BY a.MATNR, a.WERKS, a.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY08(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct A.MATNR, A.WERKS, A.CHARG, A.MFG_START_DATE, B.MTART 
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                 + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.MFG_START_DATE,'XX') = 'XX'"
                + " AND    B.MTART = 'HALB'"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY56(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, c.prfrq,
                    b.qndat, TO_CHAR(SYSDATE + c.prfrq,'YYYYMMDD') AS XXX
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marc c"
                + " WHERE (a.matnr = b.matnr and a.charg = b.charg)"
                + " AND  (a.matnr = c.matnr and a.werks = c.werks)	 "
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(a.qndat,'XX') = 'XX'"
                + " AND  c.prfrq > 0"
                + " AND  TO_CHAR(SYSDATE + c.prfrq,'YYYYMMDD') <> b.qndat"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY223(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, lgort, charg, mfg_start_date
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars"
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(mfg_start_date,'XX') <> 'XX'"
                + " AND    mfg_start_date > TO_CHAR(SYSDATE,'YYYYMMDD')"
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY57(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.VFDAT, 
                    TO_CHAR(SYSDATE + B.PRFRQ,'YYYYMMDD') AS XXX
                FROM (SELECT MATNR, WERKS, CHARG, VFDAT
                    FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS"
                    + " WHERE NVL(VFDAT,'XX') = 'XX'"
                    + " AND WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                        + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")      "
                    + " ) A,"
                    + " (SELECT MATNR, WERKS, PRFRQ"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC"
                    + " WHERE PRFRQ > 0"
                    + " ) B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    TO_CHAR(SYSDATE + B.PRFRQ,'YYYYMMDD') > A.VFDAT "
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY10(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct A.MATNR,A.WERKS, A.CHARG, A.QNDAT, B.PRFRQ
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    A.WERKS = B.WERKS"
                + " AND    NVL(A.QNDAT,'XX') <> 'XX'"
                + " AND    (B.PRFRQ IS null or B.PRFRQ = 0)"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.QNDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY11(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct A.MATNR, A.WERKS, A.CHARG, A.QNDAT, B.PRFRQ
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS"
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    A.WERKS = B.WERKS"
                + " AND    NVL(A.QNDAT,'XX') = 'XX'"
                + " AND    NVL(B.PRFRQ,0) > 0	"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.QNDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY58(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.WERKS, Z.CHARG, Z.VFDAT, Z.mch1VFDAT, Z.ATWRT, D.WERKS AS ZWERKS
                FROM (SELECT A.MATNR,A.WERKS,A.CHARG,A.VFDAT,B.VFDAT as mch1VFDAT,C.ATWRT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP C"
                + " WHERE (A.MATNR = B.MATNR AND A.CHARG = B.CHARG)"
                + " AND B.CUOBJ_BM = C.OBJEK"
                + " AND C.ATINN = '1035'"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND A.CHARG = B.CHARG"
                + " AND NVL(A.VFDAT,'XX') <> NVL(B.VFDAT,'XX')"
                + " ) Z,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA D"
                + " WHERE Z.MATNR = D.MATNR(+)"
                + " AND Z.CHARG = D.CHARG(+)"
                + " order by Z.MATNR,Z.WERKS,Z.CHARG,Z.VFDAT,Z.mch1VFDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY144(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct matnr, werks, charg, first_receipt_date
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars "
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    nvl(first_receipt_date,'xx') = 'xx'"
                + " AND  NVL(charg,'QQ') != 'QQ'"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY113(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, P.PRFRQ, A.QNDAT, B.QNDAT AS BQNDAT, C.WERKS as CWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC P,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C  "
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.MATNR = P.MATNR"
                + " AND  A.WERKS = P.WERKS"
                + " AND  A.MATNR = C.MATNR (+)"
                + " AND  A.CHARG = C.CHARG (+)"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  A.QNDAT is null "
                + " AND  B.QNDAT is null"
                + " AND  NVL(P.PRFRQ,0) > 0"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.QNDAT, B.QNDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY109(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.HSDAT, C.HSDAT as CHSDAT, B.MHDHB
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 C"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.MATNR = C.MATNR"
                + " AND  A.CHARG = C.CHARG"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(C.HSDAT,'XX') = 'XX'"
                + " AND  B.MHDHB > 0"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.HSDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY110(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.VFDAT, C.VFDAT as CVFDAT, B.MHDRZ
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 C"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.MATNR = C.MATNR"
                + " AND  A.CHARG = C.CHARG"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(C.VFDAT,'XX') = 'XX'"
                + " AND  B.MHDRZ = 1"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY112(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.MFG_START_DATE, B.ATWRT, C.WERKS AS CWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA P,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C,"
                    + " (SELECT X.MATNR, X.CHARG, Z.ATINN, Z.ATWRT "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 X,"
                        + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP Z"
                    + " WHERE  X.CUOBJ_BM = Z.OBJEK AND Z.ATINN = '873') B "
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.MATNR = C.MATNR(+)"
                + " AND  A.CHARG = C.CHARG(+)		"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.ATINN = '873'"
                + " AND  A.MATNR = P.MATNR"
                + " AND  NVL(P.MTART,'XX') = 'HALB'"
                + " AND  A.MFG_START_DATE is null"
                + " AND  B.ATWRT is null"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY118(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.hsdat, b.mhdhb
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr (+)"
                + " AND    a.charg = c.charg (+)"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(a.hsdat,'XX') = 'XX'"
                + " AND    b.mhdhb > 0"
                + " AND    NVL(c.matnr,'XX') = 'XX'"
                + " AND    NVL(c.charg,'XX') = 'XX'"
                + " ORDER BY a.matnr, a.werks, a.charg, a.hsdat");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY119(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.VFDAT, B.MHDRZ    
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 C"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.MATNR = C.MATNR (+)"
                + " AND    A.CHARG = C.CHARG (+)"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.VFDAT,'XX') = 'XX'"
                + " AND    B.MHDRZ = 1"
                + " AND    NVL(C.MATNR,'XX') = 'XX'"
                + " AND    NVL(C.CHARG,'XX') = 'XX'"
                + " ORDER BY A.MATNR, A.WERKS, A.VFDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY212(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.first_receipt_date  
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 C"
                + " WHERE  a.matnr = c.matnr (+)"
                + " AND  a.charg = c.charg (+)"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(a.first_receipt_date,'XX') = 'XX'"
                + " AND  NVL(c.matnr,'XX') = 'XX'"
                + " AND  NVL(c.charg,'XX') = 'XX'"
                + " AND  NVL(a.charg,'QQ') != 'QQ'"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY214(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.qndat, b.prfrq 
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marc B, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 C"
                + " WHERE  a.matnr = c.matnr (+)"
                + " AND  a.charg = c.charg (+)"
                + " AND  a.matnr = b.matnr"
                + " AND  a.werks = b.werks"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(a.qndat,'XX') = 'XX'"
                + " AND  NVL(c.matnr,'XX') = 'XX'"
                + " AND  NVL(c.charg,'XX') = 'XX'"
                + " AND  NVL(b.prfrq,0) > 0"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY215(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.mfg_start_date, b.mtart  
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 c"
                + " WHERE  a.matnr = c.matnr (+)"
                + " AND  a.charg = c.charg (+)"
                + " AND  a.matnr = b.matnr"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(a.mfg_start_date,'XX') = 'XX'"
                + " AND  b.mtart = 'HALB'"
                + " AND  NVL(c.matnr,'XX') = 'XX'"
                + " AND  NVL(c.charg,'XX') = 'XX'"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY247(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT  i.bwart, i.matnr, i.werks, i.lgort, i.charg,  m.vfdat , m.qndat, 
				    CASE WHEN to_date(m.vfdat,'yyyymmdd') <  SYSDATE Then 'Expired' END AS NOTE
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_MCH1 m"
                + " WHERE i.matnr=m.matnr and i.charg=m.charg "
                + " AND   (to_date(m.qndat,'yyyymmdd') between sysdate and sysdate + 30  "
                    + " OR  to_date(m.vfdat,'yyyymmdd') between sysdate and sysdate + 30"
                    + " OR  to_date(m.vfdat,'yyyymmdd') < sysdate)"
                + " AND   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY m.vfdat, m.qndat, i.matnr,i.werks,i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY257(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.QNDAT
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A"
                + " WHERE  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.QNDAT,'XX') <> 'XX'"
                + " AND    A.QNDAT < TO_CHAR(SYSDATE,'YYYYMMDD')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.QNDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY80(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT MATNR, WERKS, CHARG, SOBKZ, LIFNR
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  SOBKZ IN ('K','O')"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(LIFNR,'XX') = 'XX'"
                + " ORDER BY MATNR, WERKS, CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY151(string title)
        {
            try
            {
                //--Translate number to a space, if the length is null, then we know to zero pad value.
                string command = String.Format(@"
				SELECT MATNR, WERKS, CHARG, SOBKZ, LIFNR
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM i  "
                + " WHERE  decode(LENGTH(TRIM(TRANSLATE(LIFNR, '0123456789', ' '))), null, lpad(LIFNR,10,'0'), LIFNR) NOT IN (SELECT lifnr FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lfa1)"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    LIFNR IS NOT NULL "
                + " ORDER BY MATNR, WERKS, CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY81(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT MATNR, WERKS, CHARG, SOBKZ, KUNNR
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  SOBKZ IN ('W')"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(KUNNR,'XX') = NULL"
                + " ORDER BY MATNR, WERKS, CHARG, SOBKZ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY152(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, CHARG, SOBKZ, KUNNR
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  KUNNR NOT IN (SELECT lifnr FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_kna1)"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    KUNNR IS NOT NULL "
                + " ORDER BY MATNR, WERKS, CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY82(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct matnr, werks, lgort, charg, sobkz
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im"
                + " WHERE NVL(SOBKZ,'XX') <> 'XX'"
                + " AND NVL(LGORT,'XX') <> 'XX'"
                + " AND werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY98(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, LGORT, CHARG, BWART, SOBKZ
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  BWART != '561'"
                + " AND    SOBKZ in ('K','W')"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY MATNR, WERKS, LGORT, CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY220(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, LGORT, CHARG, BWART, SOBKZ
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  BWART not in ('561', '563')"
                + " AND    SOBKZ in ('O')"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY MATNR, WERKS, LGORT, CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY52(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT MATNR, WERKS, CHARG, VFDAT, BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE  VFDAT <= TO_CHAR(SYSDATE,'YYYYMMDD')"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND BWART <>'565'"
                + " ORDER BY MATNR, WERKS, CHARG, VFDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY99(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.ZUSTD
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    A.BWART = '561'"
                + " AND    B.ZUSTD = 'X'"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY114(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.ZUSTD
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    A.BWART = '563'"
                + " AND    B.ZUSTD = 'X'"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY115(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.ZUSTD
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    A.BWART = '565'"
                + " AND    B.ZUSTD = 'X'"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY209(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, lgort, charg, erfmg, erfme
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im "
                + " WHERE  erfmg is not null"
                + " AND    nvl(erfme,'XX') = 'XX'"
                + " AND    werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY29(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT bwart, matnr, werks, charg, lgort, SUM(erfmg) sumERFMG
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im "
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    lgort IN (SELECT DISTINCT a.lgort FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_t320 a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_warehouse_ref b"
                    + " WHERE a.lgnum = b.lgnum and b.site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    matnr||werks||charg||lgort not in (SELECT matnr||werks||charg||lgort"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm)"
                + " GROUP BY bwart, matnr, werks, charg, lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY30(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DECODE(bestq,'Q','563','S','565','561') bestq,
                    matnr, werks, charg, lgort, SUM(ANFME) sumANFME
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm"
                + " WHERE  WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    lgort IN (SELECT DISTINCT a.lgort FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_t320 a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_warehouse_ref b"
                    + " WHERE a.lgnum = b.lgnum and b.site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND matnr||werks||charg||lgort not in (SELECT matnr||werks||charg||lgort"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im)"
                + " GROUP BY bestq, matnr, werks, charg, lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY108(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT I.BWART, I.MATNR, I.WERKS, I.CHARG, I.LGORT, I.ERFME, W.LRMEI
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM I,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM W"
                + " WHERE  I.MATNR = W.MATNR"
                + " AND  I.CHARG = W.CHARG"
                + " AND  I.WERKS = W.WERKS"
                + " AND  I.LGORT = W.LGORT"
                + " AND  I.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(I.ERFME,'XX') <> NVL(W.LRMEI,'XX')"
                + " ORDER BY I.BWART, I.MATNR, I.WERKS, I.CHARG, I.LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY120(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT B.MATNR, B.CHARG, B.WERKS, B.LGORT, SUM(B.ANFME) AS WMSUM,
                    (SELECT SUM(A.ERFMG) FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A WHERE A.MATNR = B.MATNR AND A.WERKS = B.WERKS AND A.LGORT = B.LGORT AND A.CHARG = B.CHARG) AS IMSUM"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM B"
                + " WHERE  WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " HAVING SUM(B.ANFME) <> (SELECT SUM(A.ERFMG) FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A WHERE A.MATNR = B.MATNR AND A.WERKS = B.WERKS AND A.LGORT = B.LGORT AND A.CHARG = B.CHARG)"
                + " GROUP BY B.MATNR, B.CHARG, B.WERKS, B.LGORT   "
                + " ORDER BY B.MATNR, B.WERKS, B.LGORT, B.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY261(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT B.MATNR, B.CHARG, B.WERKS, B.LGORT, B.BESTQ, SUM(B.ANFME) AS WMSUM,
                    (SELECT SUM(A.ERFMG)  
                    FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                    + " WHERE A.MATNR = B.MATNR AND A.WERKS = B.WERKS AND A.LGORT = B.LGORT AND A.CHARG = B.CHARG "
                    + " and nvl(b.bestq,' ')=decode(a.bwart, '561', ' ', '565','S','563','Q','x')) AS IMSUM"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM B"
                + " WHERE  WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " HAVING SUM(B.ANFME) <> (SELECT SUM(A.ERFMG) "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                    + " WHERE A.MATNR = B.MATNR AND A.WERKS = B.WERKS AND A.LGORT = B.LGORT AND A.CHARG = B.CHARG and nvl(b.bestq,' ')=decode(a.bwart, '561', ' ', '565','S','563','Q','x'))"
                + " GROUP BY B.MATNR, B.CHARG, B.WERKS, B.LGORT, B.BESTQ   "
                + " ORDER BY B.MATNR, B.WERKS, B.LGORT, B.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY31(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.LGORT, A.BESTQ, B.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.WERKS = B.WERKS"
                + " AND  A.LGORT = B.LGORT"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(A.BESTQ,'XX') = 'XX'"
                + " AND  B.BWART <> '561'"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY32(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.LGORT, A.BESTQ, B.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.WERKS = B.WERKS"
                + " AND  A.LGORT = B.LGORT"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  A.BESTQ = 'S'"
                + " AND  B.BWART <> '565'"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY33(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.LGORT, A.BESTQ, B.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.WERKS = B.WERKS"
                + " AND  A.LGORT = B.LGORT"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  A.BESTQ = 'Q'"
                + " AND  B.BWART <> '563'"
                + " ORDER BY A.MATNR,A.WERKS,A.LGORT,A.CHARG,A.BESTQ,B.BWART");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY35(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT lgnum, matnr, werks, charg, bestq
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm "
                + " WHERE  NVL(bestq, 'Z') NOT IN ('Z', 'Q', 'S')"
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, bestq");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY34(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.LGORT, A.SOBKZ
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                + " WHERE  A.SOBKZ NOT IN ('K','W','O')"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.SOBKZ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY36(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                + " WHERE  A.BWART NOT IN ('561', '565', '563')"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.BWART");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY258(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT w.MATNR, w.WERKS,w.CHARG, w.LGORT, w.BESTQ, i.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM w,   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM i"
                + " WHERE  w.MATNR = i.MATNR(+)"
                + " AND  w.CHARG = i.CHARG(+)"
                + " AND  w.WERKS = i.WERKS(+)"
                + " AND  w.LGORT = i.LGORT(+)"
                + " AND  w.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(w.BESTQ,'XX') = 'XX'"
                + " AND  i.BWART(+) ='561'"
                + " AND i.BWART is null"
                + " ORDER BY w.MATNR, w.WERKS, w.LGORT, w.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY259(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT w.MATNR, w.WERKS,w.CHARG, w.LGORT, w.BESTQ, i.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM w,  " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM i"
                + " WHERE  w.MATNR = i.MATNR(+)"
                + " AND  w.CHARG = i.CHARG(+)"
                + " AND  w.WERKS = i.WERKS(+)"
                + " AND  w.LGORT = i.LGORT(+)"
                + " AND  w.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(w.BESTQ,'X') = 'S'"
                + " AND  i.BWART(+) ='565'"
                + " AND i.BWART is null"
                + " ORDER BY w.MATNR, w.WERKS, w.LGORT, w.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY260(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT w.MATNR, w.WERKS,w.CHARG, w.LGORT, w.BESTQ, i.BWART
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM w, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM i"
                + " WHERE  w.MATNR = i.MATNR(+)"
                + " AND  w.CHARG = i.CHARG(+)"
                + " AND  w.WERKS = i.WERKS(+)"
                + " AND  w.LGORT = i.LGORT(+)"
                + " AND  w.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(w.BESTQ,'X') = 'Q'"
                + " AND  i.BWART(+) ='563'"
                + " AND i.BWART is null"
                + " ORDER BY w.MATNR, w.WERKS, w.LGORT, w.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY13(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT matnr, werks, charg, lgort, potency
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars"
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    matnr NOT IN (SELECT matnr FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marm"
                    + " WHERE meinh in ('BKG', 'BGM'))"
                + " AND    potency is not null "
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY14(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.potency, b.umren, b.umrez, b.umren/b.umrez*100 as mmpotency
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marm b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  a.potency is null "
                + " AND  b.meinh in ('BKG', 'BGM')"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY216(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.potency, b.umren, b.umrez, b.umren/b.umrez*100 as mmpotency  
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marm b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 c"
                + " WHERE  a.matnr = c.matnr (+)"
                + " AND  a.charg = c.charg (+)"
                + " AND  a.matnr = b.matnr"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  a.potency is null"
                + " AND  b.meinh in ('BKG', 'BGM')"
                + " AND  NVL(c.matnr,'XX') = 'XX'"
                + " AND  NVL(c.charg,'XX') = 'XX'"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY15(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.potency
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a"
                + " WHERE  LENGTH(MOD(a.potency,1)) > 3"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg, a.potency");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY54(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.POTENCY, B.ATWRT
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                    + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                        + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                    + " WHERE x.CUOBJ_BM = z.OBJEK and z.ATINN = '870') B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.ATINN = '870'"
                + " AND  A.POTENCY <> B.ATWRT"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY72(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.POTENCY
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A"
                + " WHERE  A.POTENCY > 100"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.POTENCY");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY18(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.CHARG, A.WERKS, A.ERFMG
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                + " WHERE  A.ERFMG = 0"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR,A.WERKS,A.CHARG,A.ERFMG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY27(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.ERFME, A.ERFMG
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                + " WHERE  A.ERFME = 'EA' "
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  instr(A.ERFMG,'.') > 0"
                + " ORDER BY MATNR,WERKS,CHARG,ERFME");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY03(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.HSDAT, B.HSDAT AS BHSDAT, D.MHDHB, C.WERKS AS ZWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA D,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C"
                + " WHERE  A.MATNR = B.MATNR "
                + " AND    A.MATNR = D.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND    A.MATNR = C.MATNR(+)"
                + " AND    A.CHARG = C.CHARG(+) "
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS"
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(A.HSDAT,'XX') <> NVL(B.HSDAT,'XX')"
                + " ORDER BY A.MATNR,A.WERKS,A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY07(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.VFDAT, B.VFDAT as mch1VFDAT, 
                    D.MHDRZ, C.WERKS AS ZWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA D,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C"
                + " WHERE (A.MATNR = B.MATNR AND A.MATNR = D.MATNR AND A.CHARG = B.CHARG)"
                + " AND    A.MATNR = C.MATNR(+)"
                + " AND    A.CHARG = C.CHARG(+)"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(A.VFDAT,'XX') <> NVL(B.VFDAT,'XX')"
                + " ORDER BY A.MATNR,A.WERKS,A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY09(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.MFG_START_DATE, B.ATWRT, C.WERKS AS ZWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C,"
                    + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                        + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                    + " WHERE  x.CUOBJ_BM = z.OBJEK and z.ATINN = '873') B"
                + " WHERE  (A.MATNR = B.MATNR AND A.CHARG = B.CHARG)"
                + " AND    A.MATNR = C.MATNR(+)"
                + " AND    A.CHARG = C.CHARG(+)"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    B.ATINN = '873'"
                + " AND    NVL(A.MFG_START_DATE,'XX') <> NVL(B.ATWRT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY12(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.QNDAT, B.QNDAT AS BQNDAT, C.WERKS AS ZWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C "
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.MATNR = C.MATNR(+)"
                + " AND  A.CHARG = C.CHARG(+)"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(A.QNDAT,'XX') <> NVL(B.QNDAT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.QNDAT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY17(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.BATCH_USE_RESTRICTION, B.ATWRT, D.BWART, C.WERKS AS ZWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM D,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C,"
                    + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                        + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                    + " WHERE  x.CUOBJ_BM = z.OBJEK and z.ATINN = '876') B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  B.MATNR = D.MATNR "
                + " AND  A.CHARG = B.CHARG"
                + " AND  B.CHARG = D.CHARG"
                + " AND  A.MATNR = C.MATNR(+)"
                + " AND  A.CHARG = C.CHARG(+)"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.ATINN = '876'"
                + " AND  NVL(A.BATCH_USE_RESTRICTION,'XX') <> NVL(B.ATWRT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY145(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.FIRST_RECEIPT_DATE, B.ATWRT, C.WERKS AS CWERKS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM D,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C,"
                    + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                        + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                    + " WHERE x.CUOBJ_BM = z.OBJEK and z.ATINN = '1377') B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND  B.MATNR = D.MATNR "
                + " AND  A.CHARG = B.CHARG"
                + " AND  B.CHARG = D.CHARG"
                + " AND  A.MATNR = C.MATNR(+)"
                + " AND  A.CHARG = C.CHARG(+) "
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.ATINN = '1377'"
                + " AND  NVL(A.FIRST_RECEIPT_DATE,'XX') <> NVL(B.ATWRT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY245(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT i.matnr, i.werks, i.lgort, i.charg, i.erfmg, i.erfme, z.meins
                FROM  " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara z"
                + " WHERE i.matnr = z.matnr  and i.erfme<>decode(z.meins,'TH','TS',z.meins) "
                + " AND   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY251(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.lgort, i.charg, original_mfg, a.atwrt as SAP_VALUE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE a.objek=m.cuobj_bm  AND  i.matnr = m.matnr  AND  i.charg = m.charg "
                + " AND  a.atinn='1426'"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  ltrim(original_mfg,'0') <> ltrim(a.atwrt,'0')"
                + " AND  i. original_mfg is not null "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY252(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.lgort, i.charg, label_exp_date, a.atwrt as SAP_VALUE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE a.objek=m.cuobj_bm  AND  i.matnr = m.matnr  AND  i.charg = m.charg "
                + " AND  a.atinn='939'"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and  label_exp_date <> a.atwrt"
                + " AND  i.label_exp_date is not null  "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY253(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.lgort, i.charg, rs_discard_date, a.atwrt as SAP_VALUE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE a.objek=m.cuobj_bm  AND  i.matnr = m.matnr  AND  i.charg = m.charg "
                + " AND  a.atinn='1648'"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and  rs_discard_date <> a.atwrt"
                + " AND  i.rs_discard_date is not null  "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY254(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.lgort, i.charg, adj_exp_date, a.atwrt as SAP_VALUE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE a.objek=m.cuobj_bm  AND  i.matnr = m.matnr  AND  i.charg = m.charg "
                + " AND  a.atinn='1035'"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and  adj_exp_date <> a.atwrt"
                + " AND  i.adj_exp_date is not null  "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY255(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.lgort, i.charg, packaging_site, a.atwrt as SAP_VALUE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE a.objek=m.cuobj_bm  AND  i.matnr = m.matnr  AND  i.charg = m.charg "
                + " AND  a.atinn='1654'"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and  packaging_site <> a.atwrt"
                + " AND  i.packaging_site is not null  "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY256(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.lgort, i.charg, parent_batch, a.atwrt as SAP_VALUE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_ausp a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE a.objek=m.cuobj_bm  AND  i.matnr = m.matnr  AND  i.charg = m.charg "
                + " AND  a.atinn='1533'"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " and  parent_batch <> a.atwrt"
                + " AND  i.parent_batch is not null  "
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY53(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.MATNR, a.WERKS, b.MTART   
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA b"
                + " WHERE  a.MATNR = b.MATNR"
                + " AND    a.BWART = '563'"
                + " AND    a.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.MATNR||a.WERKS not in (SELECT matnr||werks FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_QMAT WHERE art = '05'"
                    + " and werks in (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                        + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + "))"
                + " ORDER BY a.MATNR, a.WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY100(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.LVORM
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND NVL(B.LVORM,'Z') = 'X'"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY101(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.LVORM
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS = B.WERKS"
                + " AND    NVL(B.LVORM,'Z') = 'X'"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY102(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.MMSTA
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS = B.WERKS"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(B.MMSTA,'XX') <> 'XX'"
                + " AND B.MMSTA in ('01', 'Z2', 'Z5', 'Z6', 'Z7')"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY103(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.MSTAE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(B.MSTAE,'XX') <> 'XX'"
                + " AND B.MSTAE in ('01', 'Z2', 'Z5', 'Z6', 'Z7')"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY21(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marc b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.werks = b.werks (+)"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    b.matnr is null"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY85(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, b.mtart
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mbew c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr (+)"
                + " AND    a.werks = c.bwkey (+)"
                + " AND    c.bwkey is null"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + " "
                    + " AND (werks not like 'Z%' AND WERKS not like 'N%' AND werks != '0424'))"
                + " AND    b.mtart not in ('ZUNB', 'ZGCM')"
                + " ORDER BY a.werks, a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY22(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, c.mtart, a.werks, a.lgort
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mbew b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.werks = b.bwkey"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND werks not like 'Z%')"
                + " AND (b.stprs is null or b.stprs = 0)"
                + " AND c.mtart not in ('ZUNB', 'ZCGM')"
                + " ORDER BY a.matnr, a.werks, a.lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY23(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mbew b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND werks not like 'Z%')"
                + " AND    a.werks = b.bwkey"
                + " AND    NVL(b.hkmat,'xx') = 'xx'"
                + " ORDER BY a.matnr, a.werks, a.lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY78(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mard b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.werks = b.werks (+)"
                + " AND    a.lgort = b.lgort (+)"
                + " AND    a.lgort is not null"
                + " AND    b.lgort is null"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY25(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgnum
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mlgn b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.lgnum = b.lgnum (+)"
                + " AND    b.lgnum is null"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY26(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.erfme, b.meins
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(a.erfme,'XX') <> NVL(b.meins,'XX')"
                + " AND    a.erfme NOT IN (SELECT meinh FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marm"
                    + " WHERE matnr = a.matnr) "
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY121(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.qndat qndat1, b.qndat qndat2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND   a.charg = b.charg"
                + " AND   a.qndat != b.qndat"
                + " AND   a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY122(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.vfdat vfdat1, b.vfdat vfdat2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.vfdat != b.vfdat"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY123(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.hsdat hsdat1, b.hsdat hsdat2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A , " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.hsdat != b.hsdat"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY124(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.lwedt lwedt1, b.lwedt lwedt2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.lwedt != b.lwedt"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY125(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.mfg_start_date mfg_date1, b.mfg_start_date mfg_date2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.mfg_start_date != b.mfg_start_date"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY126(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.ea_to_bundles ea_to_bundles1, b.ea_to_bundles ea_to_bundles2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.ea_to_bundles != b.ea_to_bundles"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY127(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.ea_to_cases ea_to_cases1, b.ea_to_cases ea_to_cases2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.ea_to_cases != b.ea_to_cases"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY128(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.ea_to_pallets ea_to_pallets1, b.ea_to_pallets ea_to_pallets2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.ea_to_pallets != b.ea_to_pallets"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY129(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.pkg_start_date pkg_date1, b.pkg_start_date pkg_date2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A , " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.pkg_start_date != b.pkg_start_date"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY130(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.batch_use_restriction batch_use_1, b.batch_use_restriction batch_use_2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.batch_use_restriction != b.batch_use_restriction"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY131(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.potency potency1, b.potency potency2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A , " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.potency != b.potency"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY149(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.FIRST_RECEIPT_DATE FIRST1, B.FIRST_RECEIPT_DATE FIRST2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  A.CHARG = B.CHARG"
                + " AND  A.FIRST_RECEIPT_DATE != B.FIRST_RECEIPT_DATE"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY244(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.rs_discard_date rsdat1, b.rs_discard_date rsdat2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND   a.charg = b.charg"
                + " AND   a.rs_discard_date != b.rs_discard_date"
                + " AND   a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY229(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.vfdat vfdat1, b.vfdat vfdat2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.vfdat != b.vfdat"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY230(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.hsdat hsdat1, b.hsdat hsdat2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.hsdat != b.hsdat"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY231(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.qndat qndat1, b.qndat qndat2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND   a.charg = b.charg"
                + " AND   a.qndat != b.qndat"
                + " AND   a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY232(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.lwedt lwedt1, b.lwedt lwedt2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.lwedt != b.lwedt"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY233(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.first_receipt_date first1, b.first_receipt_date first2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.first_receipt_date != b.first_receipt_date"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY234(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.mfg_start_date mfg_date1, b.mfg_start_date mfg_date2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars a ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.mfg_start_date != b.mfg_start_date"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY235(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.batch_use_restriction batch_use_1, b.batch_use_restriction batch_use_2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A ,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.batch_use_restriction != b.batch_use_restriction"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY236(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.potency potency1, b.potency potency2, b.werks werks2
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars A , " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_batch_chars B"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.charg = b.charg"
                + " AND  a.potency != b.potency"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks)"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY116(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT z.lgnum, z.matnr, z.werks, z.lgort, z.charg
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm z "
                + " WHERE  werks is null "
                + " OR     werks = ' '"
                + " ORDER BY z.matnr, z.werks, z.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY117(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT lgnum, matnr, werks, lgort, charg
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm"
                + " WHERE  (lgnum is null or lgnum = ' ' )"
                + " AND    werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY65(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT TO_CHAR(A.NLPLA) NLPLA, A.NLBER, A.NLTYP, A.LGNUM, A.WERKS, B.LGPLA
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_LAGP B"
                + " WHERE  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    A.LGNUM = B.LGNUM (+)"
                + " AND    A.NLTYP = B.LGTYP (+)"
                + " AND    A.NLBER = B.LGBER (+)"
                + " AND    A.NLPLA = B.LGPLA (+)"
                + " AND    B.LGPLA IS NULL"
                + " ORDER BY TO_CHAR(A.NLPLA), A.NLTYP, A.NLBER");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY218(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.werks, a.lgnum, a.nltyp, a.nlber, a.nlpla, b.skzue, b.spgru
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lagp b"
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.lgnum = b.lgnum"
                + " AND    a.nltyp = b.lgtyp"
                + " AND    a.nlpla = b.lgpla"
                + " AND    a.nlber = b.lgber"
                + " AND    b.skzue = 'X'"
                + " ORDER BY a.nlpla, a.nltyp, a.nlber");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY68(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT B.NLTYP, B.NLBER, B.NLPLA, B.MATNR, B.LGNUM, B.MKAPV
                FROM (SELECT DISTINCT X.NLTYP, X.NLBER, X.NLPLA, Y.MKAPV, X.MATNR, X.LGNUM
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM X, "
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD W"
                + " WHERE (X.MATNR = Y.MATNR AND X.LGNUM = Y.LGNUM)"
                + " AND (X.LGNUM = W.NLNUM AND X.NLTYP = W.NLTYP) "
                + " AND X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND W.CAPCHK > '0'"
                + " ) B"
                + " WHERE B.MKAPV IS NULL"
                + " ORDER BY B.NLTYP,B.NLBER,B.NLPLA");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY67(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DD.NLTYP, DD.NLBER, DD.NLPLA, DD.LKAPV, DD.MATNR, DD.LGNUM, DD.ANFME, DD.MKAPV, DD.SUMXXX
                FROM (SELECT D.NLTYP, D.NLBER, D.NLPLA, D.LKAPV,
                E.MATNR, E.LGNUM, E.ANFME, F.MKAPV, D.SUMXXX
                FROM (SELECT Z.NLTYP, Z.NLBER, Z.NLPLA, Z.SUMXXX, B.LKAPV
                FROM (SELECT A.NLTYP, A.NLBER, A.NLPLA, SUM(A.XXX) SUMXXX
                FROM (SELECT X.NLTYP, X.NLBER, X.NLPLA, X.ANFME,
                    Y.MKAPV, X.ANFME * Y.MKAPV XXX
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM X,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD W"
                + " WHERE (X.MATNR = Y.MATNR AND X.LGNUM = Y.LGNUM)"
                    + " AND (X.LGNUM = W.NLNUM AND X.NLTYP = W.NLTYP) "
                    + " AND X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                        + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND W.CAPCHK > '0'"
                + " ) A"
                + " WHERE A.XXX IS NOT NULL"
                + " GROUP BY A.NLTYP, A.NLBER, A.NLPLA"
                + " ) Z ,"
                + " (SELECT DISTINCT LGPLA,LGTYP,LGBER,LKAPV "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_LAGP) B"
                + " WHERE Z.NLTYP = B.LGTYP"
                + " AND Z.NLBER = B.LGBER"
                + " AND Z.NLPLA = B.LGPLA"
                + " AND B.LKAPV IS NOT NULL"
                + " AND Z.SUMXXX > B.LKAPV"
                + " ) D,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM E,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN F"
                + " WHERE (D.NLTYP = E.NLTYP"
                + " AND D.NLBER = E.NLBER"
                + " AND D.NLPLA = E.NLPLA)"
                + " AND (E.MATNR = F.MATNR"
                + " AND E.LGNUM = F.LGNUM)"
                + " )DD"
                + " ORDER BY DD.NLTYP, DD.NLBER, DD.NLPLA");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY69(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, CHARG, LGORT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM"
                + " WHERE UPPER(MATNR) IN ('ARCHINVE','GENERAL_USE','FURNITURE','COMPUTER')"
                + " AND WERKS <> '0385'"
                + " AND WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                        + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY MATNR, WERKS, CHARG, LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY87(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.LGNUM, A.LGTYP, A.LKAPV, B.CAPCHK
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_LAGP A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD B"
                + " WHERE A.LGNUM = B.NLNUM "
                + " AND A.LGTYP = B.NLTYP"
                + " AND A.LGNUM IN (SELECT LGNUM FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_WAREHOUSE_REF "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.CAPCHK > '0'"
                + " AND (A.LKAPV IS NULL OR A.LKAPV = 0)");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY71(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.NLTYP, A.NLBER, A.NLPLA, COUNT(*) AS COUNT
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD B"
                + " WHERE  A.LGNUM = B.NLNUM"
                + " AND  A.NLTYP = B.NLTYP"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.MIX = ' '"
                + " AND  B.ADDIT = ' '"
                + " GROUP BY A.NLTYP, A.NLBER, A.NLPLA"
                + " HAVING COUNT(*) > 1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY70(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.LGNUM, A.NLBER, A.NLTYP, A.NLPLA, COUNT(*) as COUNT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD B"
                + " WHERE A.LGNUM = B.NLNUM"
                + " AND   A.NLTYP = B.NLTYP"
                + " AND   A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.MIX = ' '"
                + " GROUP BY A.LGNUM, A.NLBER, A.NLTYP, A.NLPLA"
                + " HAVING COUNT(*) > 1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY88(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.WERKS, Z.LGNUM, Z.NLPLA
                FROM (SELECT DISTINCT A.MATNR,A.WERKS,A.LGNUM,A.NLPLA
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM A"
                + " WHERE A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND A.LGNUM not in (select B.LGNUM FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN B"
                + " WHERE b.matnr = a.matnr)"
                + " ) Z"
                + " ORDER by Z.MATNR,Z.WERKS,Z.LGNUM");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY225(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.lgnum, a.lgtyp, a.lgpla, a.lkapv, b.capchk, count(charg) as NUM_RECS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lagp a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm_config_std b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm c"
                + " WHERE  a.lgnum = b.nlnum"
                + " AND    a.lgnum = c.lgnum"
                + " AND    a.lgpla = c.nlpla"
                + " AND    a.lgtyp = c.nltyp"
                + " AND    a.lgtyp = b.nltyp"
                + " AND    a.lgnum IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_warehouse_ref "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    B.CAPCHK > '0'"
                + " HAVING count(charg) > a.lkapv"
                + " GROUP BY a.lgnum, a.lgtyp, a.lkapv, b.capchk, a.lgpla");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY76(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT X.MATNR, X.CHARG, X.LGNUM, X.NLTYP, X.NLBER, X.NLPLA, Y.LGBKZ, Y.LTKZE
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM X, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD Z"
                + " WHERE (X.MATNR = Y.MATNR AND X.LGNUM = Y.LGNUM)"
                + " AND (X.LGNUM = Z.NLNUM AND X.NLTYP = Z.NLTYP)"
                + " AND X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND SECTCHK = 'X'"
                + " AND X.NLBER NOT IN (SELECT SEC FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_SSS"
                    + " WHERE  NLNUM = X.LGNUM "
                    + " AND    NLTYP = X.NLTYP "
                    + " AND    NLBER = Y.LGBKZ)");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY86(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT X.MATNR, X.WERKS, X.CHARG, X.LGNUM, Y.LTKZE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM X,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y"
                + " WHERE  X.MATNR = Y.MATNR"
                + " AND    X.LGNUM = Y.LGNUM"
                + " AND    X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(Y.LTKZE,'XX') = 'XX'"
                + " ORDER BY X.MATNR, X.WERKS, X.LGNUM");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY59(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct b.matnr, b.werks, b.charg, b.lgort, 
                    b.nltyp, b.nlber, b.nlpla, b.letyp, a.lety1
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mlgn a"
                + " WHERE  a.matnr = b.matnr "
                + " AND  a.lgnum = b.lgnum         "
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")   "
                + " AND  NVL(decode(a.lety1, ' ', 'QQ', a.lety1),'QQ') <> NVL(b.letyp, 'QQ')        "
                + " AND  a.lety1 != ' '"
                + " ORDER BY b.matnr, b.werks, b.charg, a.lety1, b.letyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY94(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT lgnum, matnr, werks, lgort, charg, bwlvs
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM"
                + " WHERE  bwlvs <> '561' "
                + " AND    werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY104(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, CHARG, LGORT, NLTYP, NLBER, NLPLA
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM"
                + " WHERE WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND (NVL(NLTYP,'XX') = 'XX' OR "
                + "     NVL(NLBER,'XX') = 'XX' OR"
                + "     NVL(NLPLA,'XX') = 'XX')"
                + " ORDER BY MATNR, WERKS, CHARG, LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY28(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.charg, a.lgort, b.xchpf
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b"
                + " WHERE  a.matnr = b.matnr"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND ("
                + "    (b.xchpf = 'X' AND NVL(a.charg,'XX') = 'XX') OR"
                + " (b.xchpf <> 'X' AND NVL(a.charg,'XX') <> 'XX')"
                + "    )"
                + " ORDER BY a.matnr, a.werks, a.charg, a.lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY249(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, lgort, charg, nlenr, length(nlenr) as Length
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM "
                + " WHERE length(trim(nlenr)) <> 18 "
                + " AND   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY262(string title)  //SAP Connection Required
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, a.lgnum, a.nltyp
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM a"
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    lgnum||nltyp NOT IN (SELECT lgnum||lgtyp FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_t334E)"
                + " AND    lgnum||nltyp     IN (SELECT lgnum||lgtyp FROM " + DDRSessionEntity.Current.mapinstance + ".T331@" + DDRSessionEntity.Current.mapinstance + " WHERE LENVW='X')"
                + " ORDER BY a.matnr, a.werks, a.lgort, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_inv_wm_strategy_audits(string title)
        {
            try
            {
                string command = String.Format(@"
				select line_text FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_scm_reports"
                + " WHERE report_id = '999999'"
                + " AND username = '" + DDRSessionEntity.Current.username + "' "
                + " ORDER by query_number, line_number");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY175(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.umwrk
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC B"
                + " WHERE a.matnr = b.matnr (+)"
                + " AND   a.umwrk = b.werks (+)"
                + " AND   a.umwrk IS NOT NULL"
                + " AND   a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks  WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   b.matnr IS NULL"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY176(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr,a.werks,a.lgort, a.charg, a.umwrk,a.umlgo
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mard d"
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS  WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.matnr = d.matnr (+)   "
                + " AND    a.umwrk = d.werks (+) "
                + " AND    a.umlgo = d.lgort (+)"
                + " AND    a.umlgo IS NOT NULL"
                + " AND    d.matnr IS NULL"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY177(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, b.mtart, a.umwrk
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b"
                + " WHERE a.matnr = b.matnr"
                + " AND b.mtart != 'ZUNB'"
                + " AND a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND a.umwrk NOT IN (SELECT b.bwkey FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mbew b WHERE b.matnr = a.matnr)"
                + " AND NVL(a.umwrk, 'XX') <> 'XX'"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY178(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR,A.WERKS,A.LGORT, A.UMWRK, A.UMLGO, C.MTART
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_IM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MBEW B,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA C"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.MATNR = C.MATNR"
                + " AND A.UMWRK = B.BWKEY"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND (B.STPRS is null or b.stprs = 0)"
                + " AND NVL(A.UMWRK, 'XX') <> 'XX'"
                + " AND C.MTART not in ('ZUNB', 'ZPRM')"
                + " ORDER BY A.MATNR,A.WERKS,A.LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY179(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.umwrk
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mbew B"
                + " WHERE a.matnr = b.matnr"
                + " AND a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND a.umwrk = b.bwkey"
                + " AND DECODE(b.hkmat, ' ', 'Z', null, 'Z', b.hkmat) = 'Z'"
                + " AND NVL(a.umwrk, 'XX') <> 'XX'"
                + " ORDER BY a.matnr, a.werks, a.lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY207(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, a.umwrk, a.bwart, b.mmsta
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_marc b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.umwrk = b.werks"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(b.mmsta,'XX') <> 'XX'"
                + " AND    b.mmsta in ('01', 'Z2', 'Z5', 'Z6', 'Z7')"
                + " ORDER BY a.matnr, a.werks, a.lgort, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY180(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.charg, i.bwart, w.bwlvs
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_IM i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM w "
                + " WHERE  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    i.matnr = w.matnr "
                + " AND    i.umwrk = w.werks "
                + " AND    i.charg = w.charg"
                + " AND    i.umlgo = w.lgort"
                + " AND   ((i.bwart = '301' AND w.bwlvs <> '302')	 "
                    + " OR (i.bwart = 'Z01' AND w.bwlvs <> '312')"
                    + " OR (i.bwart = 'QC2' AND w.bwlvs <> '312'))"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY181(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.charg,i.lgort,i.umwrk,i.werks,w.werks as wm_werks, i.erfme, w.lrmei
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w "
                + " WHERE  i.WERKS In (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    i.matnr = w.matnr "
                + " AND    i.umwrk = w.werks"
                + " AND    DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg, Null, 'Null',w.charg)"
                + " AND    i.umlgo = w.lgort"
                + " AND    i.erfme <> w.lrmei"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY182(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT w.matnr, w.werks as wm_werks, w.lgort, w.charg, SUM(w.anfme) AS wmsum,
                    (SELECT SUM(i.erfmg) FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i "
                    + " WHERE i.matnr = w.matnr  "
                    + " AND  DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg, Null, 'Null',w.charg) "
                    + " AND i.umlgo = w.lgort "
                    + " AND i.umwrk = w.werks) AS imsum  "
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM w  "
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " HAVING SUM(w.anfme) <> (SELECT SUM(ii.ERFMG) FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im ii "
                    + " WHERE ii.matnr = w.matnr  "
                    + " AND  DECODE(ii.charg, Null, 'Null',ii.charg) = DECODE(w.charg, Null, 'Null',w.charg) "
                    + " AND ii.umlgo = w.lgort "
                    + " AND ii.umwrk = w.werks)"
                + " GROUP BY w.matnr, w.werks, w.charg, w.lgort   "
                + " ORDER BY w.matnr, w.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY183(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT i.bwart, i.matnr, i.werks, i.charg, i.lgort, i.status, 
                    w.bestq, i.umwrk, i.umlgo
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w"
                + " WHERE  i.matnr = w.matnr"
                + " AND  DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg, Null, 'Null',w.charg)"
                + " AND  i.umwrk = w.werks"
                + " AND  i.umlgo = w.lgort"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   NVL(i.status,'XX') <> NVL(w.bestq,'XX')"
                + " ORDER BY i.matnr,i.werks,i.lgort,i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY184(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.charg, i.werks, i.lgort, i.umwrk, i.umlgo, w.matnr as wm_matnr
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w "
                + " WHERE  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    i.matnr = w.matnr(+) "
                + " AND    i.umwrk = w.werks(+) "
                + " AND    i.umlgo = w.lgort(+)"
                + " AND    DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg(+), Null, 'Null',w.charg(+))"
                + " AND    i.umlgo IN (SELECT DISTINCT lgort FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm"
                    + " WHERE lgnum IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_warehouse_ref"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + "))"
                + " AND    i.umwrk IS NOT NULL"
                + " AND    w.matnr IS NULL"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY185(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT w.matnr, w.charg, w.lgort, w.werks, i.matnr as im_matnr
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w "
                + " WHERE  w.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    i.matnr(+) = w.matnr "
                + " AND    i.umwrk(+) = w.werks "
                + " AND    i.umlgo(+) = w.lgort "
                + " AND    DECODE(i.charg(+), Null, 'Null',i.charg(+)) = DECODE(w.charg, Null, 'Null',w.charg)"
                + " AND    i.matnr IS NULL		"
                + " ORDER BY w.matnr, w.werks, w.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY143(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, a.umwrk, a.umlgo
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mard d"
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks  WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  a.matnr = d.matnr (+)   "
                + " AND  a.werks = d.werks (+) "
                + " AND  a.umlgo = d.lgort (+)"
                + " AND  a.umlgo IS NOT NULL"
                + " AND  d.matnr is null"
                + " ORDER BY a.matnr, a.werks, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY156(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT  matnr, werks, charg, bwart, umwrk
                FROM  " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i  "
                + " WHERE   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND     umwrk is not null "
                + " AND     bwart IN ('325','311')"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY158(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.charg, i.lgort, i.werks, i.umwrk, w.werks as wm_werks, i.umlgo, w.lgort, i.erfme, w.lrmei
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w "
                + " WHERE  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  i.matnr = w.matnr"
                + " AND  i.werks = w.werks "
                + " AND  i.umlgo = w.lgort "
                + " AND  DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg, Null, 'Null',w.charg)"
                + " AND  i.erfme <> w.lrmei"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY161(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT w.matnr, w.werks as wm_werks, w.lgort, w.charg, SUM(w.anfme) AS wmsum,
                    (SELECT SUM(i.erfmg) FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i "
                    + " WHERE i.matnr = w.matnr  "
                    + " AND i.werks = w.werks "
                    + " AND  DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg, Null, 'Null',w.charg)  "
                    + " AND i.umlgo = w.lgort) AS imsum  "
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w  "
                + " WHERE  WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS  WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " HAVING SUM(w.anfme) <> (SELECT SUM(ii.erfmg) FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im ii "
                    + " WHERE ii.matnr = w.matnr  "
                    + " AND ii.werks = w.werks "
                    + " AND  DECODE(ii.charg, Null, 'Null',ii.charg) = DECODE(w.charg, Null, 'Null',w.charg) "
                    + " AND ii.umlgo = w.lgort)"
                + " GROUP BY w.matnr, w.werks, w.lgort, w.charg   "
                + " ORDER BY w.matnr, w.werks, w.lgort, w.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY164(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.charg, i.werks, i.lgort, i.umwrk, i.umlgo, w.matnr as wm_matnr
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w "
                + " WHERE  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  i.umlgo in (SELECT lgort FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_t320 WHERE werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + " ))"
                + " AND  i.matnr = w.matnr(+) "
                + " AND  i.umlgo = w.lgort(+)"
                + " AND  i.werks = w.werks(+) "
                + " AND  DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg(+), Null, 'Null',w.charg(+))"
                + " AND  w.matnr IS NULL"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY167(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT w.matnr, w.werks, w.lgort, w.charg, i.matnr as im_matnr
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w "
                + " WHERE  w.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")              "
                + " AND  i.matnr(+) = w.matnr "
                + " AND  i.umlgo(+) = w.lgort "
                + " AND  i.werks(+) = w.werks"
                + " AND  DECODE(i.charg(+), Null, 'Null',i.charg(+)) = DECODE(w.charg, Null, 'Null',w.charg)"
                + " AND  i.matnr IS NULL		"
                + " ORDER BY w.matnr, w.werks, w.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY165(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT i.matnr, i.werks, i.lgort, i.charg, i.bwart, i.status, w.bestq, i.umlgo
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm w"
                + " WHERE  i.matnr = w.matnr"
                + " AND  DECODE(i.charg, Null, 'Null',i.charg) = DECODE(w.charg, Null, 'Null',w.charg)"
                + " AND  i.werks = w.werks"
                + " AND  i.umlgo = w.lgort"
                + " AND  i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  decode(NVL(i.bwart,'XX'), '325', 'S', 'QC2', 'Q', 'XX') <> NVL(w.bestq,'XX')"
                + " ORDER BY i.matnr, i.werks, i.lgort, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY211(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.BWART, B.MMSTA
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_IM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARC B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS = B.WERKS"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(B.MMSTA,'XX') <> 'XX'"
                + " AND B.MMSTA in ('01', 'Z2', 'Z5', 'Z6', 'Z7')"
                + " ORDER BY A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY224(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT  matnr, werks, charg, lgort, umlgo, erfmg
                FROM  " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im"
                + " WHERE   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND     lgort = umlgo"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY210(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.lgort, a.charg, a.bwart, b.mstae
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(b.mstae,'XX') <> 'XX'"
                + " AND    b.mstae in ('01', 'Z2', 'Z5', 'Z6', 'Z7')"
                + " ORDER BY a.matnr, a.werks, a.lgort, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY213(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.bwart, a.matnr, a.werks, a.lgort, a.charg, a.umwrk, a.umlgo, 
                    a.erfme, a.erfmg, c.vfdat, b.mhdrz  
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 c"
                + " WHERE  a.matnr = b.matnr"
                + " AND  b.matnr = c.matnr"
                + " AND  a.charg = c.charg"
                + " AND  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  NVL(c.vfdat,'QQ') = 'QQ'"
                + " AND  b.mhdrz = 1"
                + " ORDER BY  a.matnr, a.werks, a.lgort, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY195(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg as im_charg, b.charg as sap_charg, a.erfmg
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mcha b "
                + " WHERE a.matnr = b.matnr(+)"
                + " AND   a.werks = b.werks (+)"
                + " AND   a.charg = b.charg (+)"
                + " AND   a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   b.charg IS NULL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY186(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.charg, i.erfme, a.meins
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara a "
                + " WHERE a.matnr = i.matnr"
                + " AND   i.erfme <> a.meins"
                + " AND   i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY19(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.CHARG, A.WERKS, A.ERFMG
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A"
                + " WHERE A.ERFMG <= .0009"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER by A.MATNR,A.WERKS,A.CHARG,A.ERFMG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY187(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT i.matnr, i.werks, i.charg, i.lrmei, a.meins
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm i, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara a "
                + " WHERE a.matnr = i.matnr"
                + " AND   i.lrmei <> a.meins"
                + " AND   i.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY i.matnr, i.werks, i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY188(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, lgort, umwrk, umlgo, charg, bwart, insp_lot  
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im"
                + " WHERE  werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    bwart = 'QC2' "
                + " AND    insp_lot IS NULL"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY206(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, a.umwrk, a.umlgo, a.insp_lot, substr(a.insp_lot,1,2) as lot_type
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_qmat b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.umwrk = b.werks (+)"
                + " AND    substr(a.insp_lot,1,2) = b.art (+)"
                + " AND    b.art is null"
                + " AND    a.insp_lot is not null"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.werks, a.matnr, a.lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY196(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT TO_CHAR(a.nlpla) nlpla, a.nlber, a.nltyp, a.lgnum, a.werks, b.lgpla
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lagp b"
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.lgnum = b.lgnum (+)"
                + " AND    a.nltyp = b.lgtyp (+)"
                + " AND    a.nlber = b.lgber (+)"
                + " AND    a.nlpla = b.lgpla (+)"
                + " AND    b.lgpla IS NULL"
                + " ORDER BY TO_CHAR(a.nlpla), a.nltyp, a.nlber");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY219(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.werks, a.lgnum, a.nltyp, a.nlber, a.nlpla, b.skzue, b.spgru
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lagp b"
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.lgnum = b.lgnum"
                + " AND    a.nltyp = b.lgtyp"
                + " AND    a.nlpla = b.lgpla"
                + " AND    a.nlber = b.lgber"
                + " AND    b.skzue = 'X'"
                + " ORDER BY a.nlpla, a.nltyp, a.nlber");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY208(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT lgnum, matnr, werks, charg, bestq
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm"
                + " WHERE  NVL(bestq, 'Z') NOT IN ('Z', 'Q', 'S')"
                + " AND    werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, bestq");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY197(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT b.matnr, b.werks, b.charg, b.lgort, 
                    b.nltyp, b.nlber, b.nlpla, b.letyp, a.lety1
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mlgn a"
                + " WHERE  a.matnr = b.matnr "
                + " AND  a.lgnum = b.lgnum         "
                + " AND  b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND  NVL(decode(a.lety1, ' ', 'QQ', a.lety1),'QQ') <> NVL(b.letyp, 'QQ')     "
                + " ORDER BY b.matnr, a.lety1, b.letyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY198(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DD.NLTYP, DD.NLBER, DD.NLPLA, DD.LKAPV, DD.MATNR, DD.LGNUM, DD.ANFME, DD.MKAPV, DD.SUMXXX
                FROM (SELECT D.NLTYP, D.NLBER, D.NLPLA, D.LKAPV, E.MATNR, E.LGNUM, E.ANFME, F.MKAPV, D.SUMXXX
                  FROM (SELECT Z.NLTYP, Z.NLBER, Z.NLPLA, Z.SUMXXX, B.LKAPV
                        FROM (SELECT A.NLTYP, A.NLBER, A.NLPLA, SUM(A.XXX) SUMXXX
                              FROM (SELECT X.NLTYP, X.NLBER, X.NLPLA, X.ANFME, Y.MKAPV, X.ANFME * Y.MKAPV XXX
                                    FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM X,"
                                         + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y,"
                                         + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD W"
                                    + " WHERE (X.MATNR = Y.MATNR AND X.LGNUM = Y.LGNUM)"
                                      + " AND (X.LGNUM = W.NLNUM AND X.NLTYP = W.NLTYP) "
                                      + " AND X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                                                      + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                                      + " AND W.CAPCHK > '0'"
                                   + " ) A"
                              + " WHERE A.XXX IS NOT NULL"
                              + " GROUP BY A.NLTYP, A.NLBER, A.NLPLA"
                             + " ) Z ,"
                             + " (SELECT DISTINCT LGPLA,LGTYP,LGBER,LKAPV "
                              + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_LAGP) B"
                              + " WHERE Z.NLTYP = B.LGTYP"
                                + " AND Z.NLBER = B.LGBER"
                                + " AND Z.NLPLA = B.LGPLA"
                                + " AND B.LKAPV IS NOT NULL"
                                + " AND Z.SUMXXX > B.LKAPV"
                       + " ) D,"
                       + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM E,"
                       + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN F"
                + " WHERE (D.NLTYP = E.NLTYP"
                + " AND D.NLBER = E.NLBER"
                + " AND D.NLPLA = E.NLPLA)"
                + " AND (E.MATNR = F.MATNR"
                + " AND E.LGNUM = F.LGNUM)"
                + " )DD"
                + " ORDER BY DD.NLTYP, DD.NLBER, DD.NLPLA");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY199(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT B.NLTYP, B.NLBER, B.NLPLA, B.MATNR, B.LGNUM, B.MKAPV
                FROM  (SELECT DISTINCT X.NLTYP, X.NLBER, X.NLPLA, Y.MKAPV, X.MATNR, X.LGNUM
                   FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM X, "
                          + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y,"
                          + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD W"
                   + " WHERE  (X.MATNR = Y.MATNR AND X.LGNUM = Y.LGNUM)"
                   + " AND    (X.LGNUM = W.NLNUM AND X.NLTYP = W.NLTYP) "
                   + " AND    X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                                      + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                   + " AND W.CAPCHK > '0'"
                + " ) B"
                + " WHERE B.MKAPV IS NULL"
                + " ORDER BY B.NLTYP,B.NLBER,B.NLPLA");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY200(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.NLTYP, A.NLBER, A.NLPLA, COUNT(*) AS COUNT
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD B"
                + " WHERE  A.LGNUM = B.NLNUM"
                + " AND  A.NLTYP = B.NLTYP"
                + " AND  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  B.MIX = ' '"
                + " AND  B.ADDIT = ' '"
                + " GROUP BY A.NLTYP, A.NLBER, A.NLPLA"
                + " HAVING COUNT(*) > 1 ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY201(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.LGNUM, A.NLBER, A.NLTYP, A.NLPLA, COUNT(*) as COUNT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD B"
                + " WHERE A.LGNUM = B.NLNUM"
                + " AND   A.NLTYP = B.NLTYP"
                + " AND   A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.MIX = ' '"
                + " GROUP BY A.LGNUM, A.NLBER, A.NLTYP, A.NLPLA"
                + " HAVING COUNT(*) > 1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY226(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.lgnum, a.lgtyp, a.lgpla, a.lkapv, b.capchk, count(charg) as NUM_RECS
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_lagp a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_wm_config_std b,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm c"
                + " WHERE  a.lgnum = b.nlnum"
                + " AND    a.lgnum = c.lgnum"
                + " AND    a.lgpla = c.nlpla"
                + " AND    a.lgtyp = c.nltyp"
                + " AND    a.lgtyp = b.nltyp"
                + " AND    a.lgnum IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_warehouse_ref "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    B.CAPCHK > '0'"
                + " HAVING count(charg) > a.lkapv"
                + " GROUP BY a.lgnum, a.lgtyp, a.lkapv, b.capchk, a.lgpla");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY202(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT X.MATNR, X.CHARG, X.LGNUM, X.NLTYP, X.NLBER, X.NLPLA, Y.LGBKZ, Y.LTKZE
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM X, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_STD Z"
                + " WHERE (X.MATNR = Y.MATNR AND X.LGNUM = Y.LGNUM)"
                + " AND (X.LGNUM = Z.NLNUM AND X.NLTYP = Z.NLTYP)"
                + " AND X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND SECTCHK = 'X'"
                + " AND X.NLBER NOT IN (SELECT SEC FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WM_CONFIG_SSS"
                    + " WHERE  NLNUM = X.LGNUM "
                    + " AND    NLTYP = X.NLTYP "
                    + " AND    NLBER = Y.LGBKZ)");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY203(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT X.MATNR, X.WERKS, X.CHARG, X.LGNUM, Y.LTKZE
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM X,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MLGN Y"
                + " WHERE  X.MATNR = Y.MATNR"
                + " AND    X.LGNUM = Y.LGNUM"
                + " AND    X.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    NVL(Y.LTKZE,'XX') = 'XX'"
                + " ORDER BY X.MATNR,X.WERKS, X.LGNUM");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY204(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT MATNR, WERKS, CHARG, LGORT, NLTYP, NLBER, NLPLA
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM"
                + " WHERE  WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND (NVL(NLTYP,'XX') = 'XX' OR "
                    + " NVL(NLBER,'XX') = 'XX' OR"
                    + " NVL(NLPLA,'XX') = 'XX')"
                + " ORDER BY MATNR, WERKS, CHARG, LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY205(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.LGORT, B.XCHPF
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM A, "
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND ("
                    + " (B.XCHPF = 'X' AND NVL(A.CHARG,'XX') = 'XX') OR"
                + " (B.XCHPF <> 'X' AND NVL(A.CHARG,'XX') <> 'XX')"
                    + " )"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG, A.LGORT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Inventory_QUERY189(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, a.status, d.clabs, d.cinsm, d.ceinm, d.cspem 
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb d "
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE Site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = d.matnr "
                + " AND    a.charg = d.charg "
                + " AND    a.lgort = d.lgort "
                + " AND    a.werks = d.werks "
                + " AND    a.status IS NULL "
                + " AND    d.clabs = 0"
                + " AND    d.ceinm = 0");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY190(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, a.status, d.clabs, d.cinsm, d.ceinm, d.cspem 
	            FROM  " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb d "
                + " WHERE   a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND     a.matnr = d.matnr "
                + " AND     a.charg = d.charg "
                + " AND     a.lgort = d.lgort "
                + " AND     a.werks = d.werks "
                + " AND     a.status = 'Q' "
                + " AND     d.cinsm = 0");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY191(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT A.MATNR, A.WERKS, A.LGORT, A.CHARG, A.STATUS, D.CLABS, D.CINSM, D.CEINM, D.CSPEM
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_IM A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHB D "
                + " WHERE  A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    A.MATNR = D.MATNR "
                + " AND    A.CHARG = D.CHARG "
                + " AND    A.LGORT = D.LGORT "
                + " AND    A.WERKS = D.WERKS "
                + " AND    A.STATUS = 'S' "
                + " AND    D.CSPEM = 0");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY192(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, b.meins, a.status, a.erfmg, a.erfme, d.clabs, d.cinsm, d.ceinm, d.cspem
	            FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb d,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b "
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = d.matnr "
                + " AND    a.matnr = b.matnr "
                + " AND    a.charg = d.charg "
                + " AND    a.lgort = d.lgort "
                + " AND    a.werks = d.werks "
                + " AND    a.status IS NULL"
                + " AND   (a.erfmg != d.clabs   "
                + " AND  a.erfmg != d.ceinm)");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY193(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, b.meins, a.status, a.erfmg, a.erfme, d.clabs, d.cinsm, d.ceinm, d.cspem
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, "
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb d,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b "
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = d.matnr "
                + " AND    a.matnr = b.matnr  "
                + " AND    a.charg = d.charg "
                + " AND    a.lgort = d.lgort "
                + " AND    a.werks = d.werks "
                + " AND    a.erfmg != d.cinsm "
                + " AND    a.status = 'Q' ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY194(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgort, a.charg, b.meins, a.status, a.erfmg, a.erfme, d.clabs, d.cinsm, d.ceinm, d.cspem
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb d,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b "
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = d.matnr "
                + " AND    a.matnr = b.matnr "
                + " AND    a.charg = d.charg "
                + " AND    a.lgort = d.lgort "
                + " AND    a.werks = d.werks "
                + " AND    a.erfmg != d.cspem "
                + " AND    a.status = 'S'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY240(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.lgnum
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_wm a,"
                    + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mlgn b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.lgnum = b.lgnum (+)"
                + " AND    b.lgnum is null"
                + " AND    a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY a.matnr, a.werks, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY241(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, charg, lgort, clabs, cinsm, ceinm, cspem, source
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb b"
                + " WHERE b.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND  b.lgort in (SELECT lgort FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_t320 WHERE werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + " ))"
                + " AND (clabs > 0 Or cinsm > 0 Or ceinm > 0 Or cspem > 0)"
                + " AND matnr||werks||charg||lgort NOT IN (Select matnr||werks||charg||lgort FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im)"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY242(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT a.matnr, a.werks, a.charg, a.lgort, b.meins, a.status, sum(a.erfmg) as erfmg, a.erfme, d.clabs, d.cinsm, d.ceinm, d.cspem, d.source
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im a, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mchb d, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara b "
                + " WHERE  a.werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_werks WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.matnr = d.matnr  AND a.matnr = b.matnr  AND a.charg = d.charg   "
                + " AND   a.lgort = d.lgort  AND a.werks = d.werks  AND a.status IS NULL"
                + " AND  ((SELECT sum(a.erfmg) FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im z "
                     + " WHERE z.matnr = a.matnr"
                    + " AND z.werks = a.werks "
                    + " AND z.lgort = z.lgort "
                    + " AND z.charg = a.charg) != d.clabs"
                + " AND (SELECT sum(a.erfmg) FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im z "
                    + " WHERE z.matnr = a.matnr "
                    + " AND  z.werks = a.werks "
                    + " AND  z.lgort = z.lgort "
                    + " AND  z.charg = a.charg) != d.ceinm)  "
                + " GROUP BY a.matnr, a.werks, a.lgort, a.charg, b.meins, a.status, a.erfme, d.clabs, d.cinsm, d.ceinm, d.cspem, d.source"
                + " ORDER BY a.matnr, a.werks, a.lgort, a.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY243(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT  i.bwart, i.matnr,i.werks,i.charg,i.lgort,i.erfme,i.erfmg, m.qndat
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mch1 m"
                + " WHERE i.matnr=m.matnr "
                + " AND i.charg=m.charg "
                + " AND to_date(m.qndat,'yyyymmdd') BETWEEN sysdate AND sysdate + 7"
                + " ORDER BY m.qndat");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY246(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT DISTINCT i.matnr, i.werks, i.lgort, i.charg, i.erfmg, i.erfme, z.meins
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_mara z"
                + " WHERE i.matnr = z.matnr  and i.erfme<>decode(z.meins,'TH','TS',z.meins) "
                + " AND   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, lgort, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY248(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT  i.bwart, i.matnr, i.werks, i.lgort, i.charg,  m.vfdat , m.qndat, 
				    CASE WHEN to_date(m.vfdat,'yyyymmdd') <  SYSDATE Then 'Expired' END AS NOTE
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_move_im i, " + DDRSessionEntity.Current.table_schema + "_procs.gdd_inv_MCH1 m"
                + " WHERE i.matnr=m.matnr and i.charg=m.charg "
                + " AND   (to_date(m.qndat,'yyyymmdd') between sysdate and sysdate + 30  "
                    + " OR  to_date(m.vfdat,'yyyymmdd') between sysdate and sysdate + 30"
                    + " OR  to_date(m.vfdat,'yyyymmdd') < sysdate)"
                + " AND   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY m.vfdat, m.qndat, i.matnr,i.werks,i.charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY250(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, lgort, charg, nlenr, length(nlenr) as Length
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MOVE_WM "
                + " WHERE length(trim(nlenr)) <> 18 "
                + " AND   werks IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY matnr, werks, charg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Inventory_QUERY37(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.WERKS, Z.CHARG, Z.PKG_START_DATE, Z.ATWRT, C.WERKS AS CWERKS
                FROM (SELECT DISTINCT A.MATNR,A.WERKS,A.CHARG,A.PKG_START_DATE,B.ATWRT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                + " WHERE x.CUOBJ_BM = z.OBJEK and z.ATINN = '963') B"
                + " WHERE (A.MATNR = B.MATNR AND A.CHARG = B.CHARG)"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.ATINN = '963'"
                + " AND NVL(A.PKG_START_DATE,'XX') <> NVL(B.ATWRT,'XX')"
                + " ) Z,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCHA C	   "
                + " WHERE Z.MATNR = C.MATNR(+)"
                + " AND Z.CHARG = C.CHARG(+)"
                + " ORDER BY Z.MATNR, Z.WERKS, Z.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY38(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_CASES
                FROM (SELECT DISTINCT MATNR, WERKS, CHARG, EA_TO_CASES
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS"
                + " WHERE MATNR NOT IN (SELECT MATNR FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARM)"
                + " AND WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND NVL(EA_TO_CASES,'XX') <> 'XX'"
                + " ) A");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY39(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_CASES, A.MEINH
                FROM (SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_CASES, B.MEINH 
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARM B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")               "
                + " AND NVL(A.EA_TO_CASES,'XX') = 'XX'"
                + " AND B.MEINH = 'ZC'"
                + " ) A");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY93(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.WERKS, Z.CHARG, Z.HSDAT, Z.VFDAT
                FROM (SELECT distinct A.MATNR, A.WERKS, A.CHARG, A.HSDAT, A.VFDAT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS B,"
                + " (SELECT MATNR, CHARG FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS "
                + " GROUP BY MATNR, CHARG HAVING COUNT(*) > 1 ) C"
                + " WHERE A.MATNR = B.MATNR AND A.CHARG = B.CHARG"
                + " AND A.MATNR = C.MATNR AND A.CHARG = C.CHARG"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND (A.HSDAT <> B.HSDAT OR A.VFDAT <> B.VFDAT)"
                + " ) Z"
                + " ORDER BY Z.MATNR, Z.WERKS, Z.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY96(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.WERKS, Z.CHARG
                FROM (SELECT A.MATNR, A.WERKS, A.CHARG
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B"
                + " WHERE A.MATNR = B.MATNR(+)"
                + " AND A.CHARG = B.CHARG(+)"
                + " AND NVL(B.MATNR,'XX') = 'XX'"
                + " AND NVL(B.CHARG,'XX') = 'XX'"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ) Z"
                + " ORDER BY Z.MATNR, Z.WERKS, Z.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Inventory_QUERY91(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.LWEDT as FILE_LWEDT, B.LWEDT as SAP_LWEDT
                FROM   " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                    + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 B"
                + " WHERE  A.MATNR = B.MATNR"
                + " AND    A.CHARG = B.CHARG"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                    + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(A.LWEDT,'XX') <> NVL(B.LWEDT,'XX')"
                + " ORDER BY A.MATNR,A.WERKS,A.CHARG,A.LWEDT,B.LWEDT");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY40(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_BUNDLES, B.ATWRT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                + " WHERE x.CUOBJ_BM = z.OBJEK and z.ATINN = '1300') B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.CHARG = B.CHARG"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.ATINN = '1300'"
                + " AND NVL(A.EA_TO_BUNDLES,'XX') <> NVL(B.ATWRT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY41(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_BUNDLES
                FROM (SELECT DISTINCT MATNR, WERKS, CHARG, EA_TO_BUNDLES
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS"
                + " WHERE MATNR NOT IN (SELECT MATNR FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARM)"
                + " AND WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(EA_TO_BUNDLES,'XX') <> 'XX'"
                + " ) A");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY42(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_BUNDLES, A.MEINH
                FROM (SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_BUNDLES, B.MEINH 
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARM B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(A.EA_TO_BUNDLES,'XX') = 'XX'"
                + " AND B.MEINH = 'ZB'"
                + " ) A");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY43(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_CASES, B.ATWRT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                + " WHERE x.CUOBJ_BM = z.OBJEK and z.ATINN = '1301') B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.CHARG = B.CHARG"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.ATINN = '1301'"
                + " AND NVL(A.EA_TO_CASES,'XX') <> NVL(B.ATWRT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY44(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_BUNDLES
                FROM (SELECT DISTINCT MATNR, WERKS, CHARG, EA_TO_BUNDLES
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS"
                + " WHERE MATNR NOT IN (SELECT MATNR FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARM)"
                + " AND WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " AND NVL(EA_TO_BUNDLES,'XX') <> 'XX'"
                + " ) A");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY45(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_PALLETS, A.MEINH
                FROM (SELECT DISTINCT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_PALLETS, B.MEINH 
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARM B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND NVL(A.EA_TO_PALLETS,'XX') = 'XX'"
                + " AND B.MEINH = 'ZP'"
                + " ) A");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY46(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.EA_TO_PALLETS, B.ATWRT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A,"
                + " (SELECT x.MATNR,x.CHARG,z.ATINN,z.ATWRT "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MCH1 x,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_AUSP z"
                + " WHERE x.CUOBJ_BM = z.OBJEK and z.ATINN = '1305') B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.CHARG = B.CHARG"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.ATINN = '1305'"
                + " AND NVL(A.EA_TO_Pallets,'XX') <> NVL(B.ATWRT,'XX')"
                + " ORDER BY A.MATNR, A.WERKS, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_Inventory_inv_financial_verification_audits(string title)
        {
            try
            {
                string command = String.Format(@"
                select line_text FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_scm_reports"
                + " WHERE report_id = '999998' "
                + " AND username = '" + DDRSessionEntity.Current.username + "'"
                + " ORDER by query_number, line_number");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY_FYI_01(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.BWART, A.MATNR, A.WERKS, A.LGORT, A.CHARG, to_char(A.SUMERFMG, 999999999.999) SUMERFMG_CHAR
                FROM (SELECT BWART, MATNR,WERKS,LGORT, CHARG, SUM(ERFMG) AS SUMERFMG
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS"
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " GROUP BY BWART, MATNR, WERKS, LGORT, CHARG) A"
                + " ORDER BY A.BWART, A.MATNR, A.WERKS, A.LGORT, A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY_FYI_02(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.MATNR, A.WERKS, A.CHARG, A.QNDAT
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_BATCH_CHARS A, "
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.CHARG = B.CHARG"
                + " AND A.WERKS = B.WERKS"
                + " AND A.LGORT = B.LGORT"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND B.BWART = '561'"
                + " AND NVL(A.QNDAT,'XX') <> 'XX'"
                + " AND TO_DATE(A.QNDAT,'YYYYMMDD') < SYSDATE"
                + " ORDER by A.MATNR,A.CHARG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY_FYI_08(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT A.gdd_plant, A.gdd_cat_location, A.gdd_license_number
                FROM gdd_final_inventory A"
                + " WHERE (NVL(A.gdd_cat_location,'XX') = 'XX' or"
                + " NVL(A.gdd_license_number,'XX') = 'XX'"
                + " )"
                + " AND A.gdd_plant IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ORDER BY A.gdd_plant, A.gdd_cat_location, A.gdd_License_number");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY_FYI_03(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.BWKEY, Z.BKLAS, Z.MTART
                FROM (SELECT DISTINCT b.MATNR,B.BWKEY,B.BKLAS,C.MTART    
	            FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                 + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MBEW B,"
                 + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MARA C"
                + " WHERE A.MATNR = B.MATNR AND A.MATNR = C.MATNR"
                + " AND A.WERKS = B.BWKEY"
                 + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                 + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")) Z"
                + " ORDER BY Z.MATNR, Z.BKLAS, Z.MTART");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY_FYI_07(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT 
                MATNR, 
                WERKS, 
                CHARG
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM"
                + " WHERE WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND (INSTR(CHARG,' ') > 0 OR "
                    + " INSTR(CHARG,'+') > 0 OR "
                    + " INSTR(CHARG,'/') > 0 OR "
                    + " INSTR(CHARG,'-') > 0 OR "
                    + " INSTR(CHARG,'*') > 0 OR "
                    + " INSTR(CHARG,'_') > 0 OR "
                    + " INSTR(CHARG,'/') > 0)"
                + " ORDER BY MATNR,WERKS");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Inventory_QUERY_FYI_04(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Z.MATNR, Z.BWKEY, Z.PEINH, Z.STPRS
                FROM (SELECT DISTINCT B.MATNR,B.BWKEY,B.PEINH,B.STPRS 
		        FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_IM A,"
                + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_MBEW B"
                + " WHERE A.MATNR = B.MATNR"
                + " AND A.WERKS = B.BWKEY"
                + " AND A.WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_INV_WERKS "
                + " WHERE SITE_CODE = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " ) Z"
                + " ORDER BY Z.MATNR, Z.PEINH, Z.STPRS   ");
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
