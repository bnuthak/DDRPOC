using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class BOMAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();

        public System.Data.DataTable SQL_BOM_errors_Header_UoM_not_Base(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.stlal, a.bmein, b.meins
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.bmein != b.meins"
                + " AND    a.werks in (" + DDRSessionEntity.Current.plantcode + ")");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_idnrk_not_exist(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.matnr, b.werks, b.stlal, b.posnr, b.idnrk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail b"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = b.werks"
                + " AND   a.stlal = b.stlal "
                + " AND   b.idnrk not in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                    + " where werks = b.werks)"
                + " AND b.werks in (" + DDRSessionEntity.Current.plantcode + ")");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_aa_audit_BOMHeaderNonMatchingZtext(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.ZTEXT
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader a LEFT OUTER JOIN " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader b"
                + " ON a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " WHERE a.ZTEXT != b.ZTEXT"
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " GROUP BY a.MATNR, a.WERKS, a.STLAL, a.ZTEXT"
                + " ORDER BY MATNR, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BOMDetail_Recursive(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.POSNR,a.IDNRK,a.REKRS
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDetail a"
                + " WHERE   a.MATNR = a.IDNRK"
                + " AND     a.REKRS Is Null"
                + " AND     a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY a.MATNR, a.STLAL, a.POSNR");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_aa_error_BOMDetail_Comma_In_Quantity(string title)
        {
            try
            {
                string command = String.Format(@"
				Select a.MATNR, a.WERKS, a.STLAL, a.POSNR,a.IDNRK,a.MENGE, a.MEINS
			    From " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDetail a"
                + " Where INSTR(MENGE, ',') > 0	"
                + " And a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " Order By a.MATNR, a.STLAL, a.POSNR");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BOMDetail_DecimalLength(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.POSNR, a.IDNRK, a.MENGE, a.AUSCH
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL a"
                + " WHERE  a.WERKS IN (" + DDRSessionEntity.Current.plantcode + ")  "
                  + " AND (a.MENGE LIKE '%.____%' OR a.AUSCH LIKE '%.___%')"
                + " ORDER BY a.MATNR, a.STLAL, a.POSNR");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BOMHeader_DecimalLength(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.BMENG
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a"
                + " WHERE  a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND (a.BMENG LIKE '%.____%' OR  a.BMENG = '0' OR a.BMENG = '-0')"
                + " ORDER BY a.MATNR, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_UnitOfMeasure(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.POSNR, a.IDNRK, a.MEINS as BOM_MEINS, b.MEINS
			    FROM  " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MARA b"
                + " Where   a.IDNRK = b.MATNR"
                + " And a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " And a.MEINS <> b.MEINS	"
                + " And   not (a.MEINS = 'G'   And b.MEINS = 'KG')"
                + " And   not (a.MEINS = 'KG'  And b.MEINS = 'G')"
                + " And   not (a.MEINS = 'L'   And b.MEINS = 'ML')"
                + " And   not (a.MEINS = 'ML'  And b.MEINS = 'L')"
                + " And a.IDNRK not IN (Select c.MATNR From " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MARM c "
                                + " Where c.MATNR = a.IDNRK"
                                + " And   c.MEINH = a.MEINS)"
                + " And a.IDNRK not IN (Select d.MATNR From " + DDRSessionEntity.Current.table_schema + "_owner.GDD_PROPORTIONALUNITS2 d "
                                + " Where d.MATNR = a.IDNRK"
                                + " And   d.WSMEI = a.MEINS)"
                + " Order By a.MATNR,a.WERKS,a.STLAL,a.POSNR");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_idnrkausme(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.werks, a.stlal, a.posnr, a.idnrk, a.meins as bom_meins, b.meins, c.ausme
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE a.idnrk = b.matnr"
                + " AND   a.idnrk = c.matnr"
                + " AND   b.matnr = c.matnr"
                + " AND   a.werks = c.werks"
                + " AND   a.werks IN (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND   a.meins <> b.meins"
                + " AND   not (a.meins = 'G'   AND b.meins = 'KG')"
                + " AND   not (a.meins = 'KG'  AND b.meins = 'G')"
                + " AND   not (a.meins = 'L'   AND b.meins = 'ML')"
                + " AND   not (a.meins = 'ML'  AND b.meins = 'L')"
                + " AND   c.ausme is null"
                + " ORDER BY a.idnrk,a.werks,a.stlal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BulkComponentWithStorLoc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.STLAN, b.POSNR, b.IDNRK, b.SCHGT, b.LGORT
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " AND a.STLAL = b.STLAL"
                + " AND b.LGORT IS NOT NULL"
                + " AND b.SCHGT='X'"
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " Order By a.MATNR,a.WERKS,a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BulkComponentWithSupplyArea(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.STLAN, b.POSNR, b.IDNRK, b.SCHGT, b.PRVBE
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " AND a.STLAL = b.STLAL"
                + " AND b.PRVBE IS NOT NULL"
                + " AND b.SCHGT='X'"
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " Order By a.MATNR,a.WERKS,a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_CostingBOMwithIssueStorLoc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.STLAN, b.POSNR, b.IDNRK, b.LGORT
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " AND a.STLAL = b.STLAL"
                + " AND a.STLAN = '6'"
                + " AND b.LGORT IS NOT NULL"
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " Order By a.MATNR,a.WERKS,a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_CostingBOMwithSupplyArea(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.STLAN, b.POSNR, b.IDNRK, b.PRVBE
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " AND a.STLAL = b.STLAL"
                + " AND a.STLAN = '6'"
                + " AND b.PRVBE IS NOT NULL"
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " Order By a.MATNR,a.WERKS,a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_SupplyAreaWithoutStorLoc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, b.POSNR, b.IDNRK, b.PRVBE, b.LGORT
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " AND a.STLAL = b.STLAL"
                + " AND b.PRVBE IS NOT NULL"
                + " AND b.LGORT IS NULL"
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " Order By a.MATNR,a.WERKS,a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_audit_size_compare(string title)
        {
            try
            {
                string command = String.Format(@"
				Select a.MATNR, a.WERKS, a.STLAL, a.BMENG, a.LOSVN, a.LOSBS,c.PLNNR, c.PLNAL, 
                   b.BSTMI, b.BSTMA, c.BMSCH, c.MEINH, c.LOSVN, c.LOSBS, b.VERID
                From " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MKAL b, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_RECIPEGENERALVIEW c, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF d"
                + " Where a.STLAL = b.STLAL "
                + " And a.WERKS = b.WERKS"
                + " And a.MATNR = b.MATNR "
                + " And b.ALNAL = c.PLNAL"
                + " And b.PLNNR = c.PLNNR"
                + " And a.MATNR = d.MATNR"
                + " And d.ALAND = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " Order By a.MATNR, a.WERKS, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_audit_sortf_ce(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, stlal, posnr, idnrk, sortf
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL "
                + " WHERE  WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND    instr(sortf, 'C') > 0"
                + " AND    instr(sortf, 'E') > 0"
                + " ORDER BY matnr, werks, stlal, posnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_audit_BOM_Comp_LGORT_missing_MARD(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.POSNR, a.IDNRK, a.lgort
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDetail a"
                + " WHERE  a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND    NVL(a.LGORT, ' ') <> ' '"
                + " AND    a.IDNRK||a.WERKS||a.LGORT NOT IN (SELECT MATNR||WERKS||LGORT FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MARD)"
                + " ORDER BY a.MATNR, a.STLAL, a.POSNR");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_audit_BOM_Comp_LGORT_invalid(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.POSNR, a.IDNRK, a.lgort, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDetail a"
                + " WHERE  a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND    NVL(a.LGORT, ' ') <> ' '"
                + " AND    a.WERKS||a.LGORT NOT IN  (SELECT WERKS||LGORT FROM " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100')"
                + " ORDER BY a.MATNR, a.STLAL, a.POSNR");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_audit_BOM_Head_missing_Details(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHeader a"
                + " WHERE  a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND    a.MATNR||a.WERKS||a.STLAL NOT IN (SELECT MATNR||WERKS||STLAL  FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDetail)"
                + " ORDER BY a.MATNR, a.WERKS, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BomComponentMaterialStatGbl(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, a.IDNRK, a.POSNR, b.MSTAE
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL a, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_MARA_CLASH b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.IDNRK = b.MATNR"
                + " AND b.MSTAE IN ('Z2', 'Z5') "
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY a.MATNR, a.WERKS, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BomComponentMaterialStatPlt(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL,  a.IDNRK, a.POSNR, b.MMSTA
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL a, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_MARC_CLASH b"
                + " WHERE a.WERKS = b.WERKS"
                + " AND a.IDNRK = b.MATNR"
                + " AND b.MMSTA IN ('Z2', 'Z5') "
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY a.MATNR, a.WERKS, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BomHeaderMaterialStatGbl(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, b.MSTAE
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL a, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_MARA_CLASH b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND b.MSTAE IN ('Z2', 'Z5') "
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY a.MATNR, a.WERKS, a.STLAL");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_errors_BomHeaderMaterialStatPlt(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, a.WERKS, a.STLAL, b.MMSTA
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMDETAIL a, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_MARC_CLASH b"
                + " WHERE a.MATNR = b.MATNR"
                + " AND a.WERKS = b.WERKS"
                + " AND b.MMSTA IN ('Z2', 'Z5') "
                + " AND a.WERKS In (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY a.MATNR, a.WERKS, a.STLAL");
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
