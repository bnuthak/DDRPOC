using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class RecipesAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_Recipes_error_rec_recipebadresource(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.plnnr, a.plnal, a.vornr, a.arbpl, b.werks
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE  a.plnnr = b.plnnr"
                + " AND    a.plnal = b.plnal "
                + " AND    a.arbpl || b.werks  NOT IN (SELECT arbpl || werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_resource)"
                + " AND    b.werks in (" + DDRSessionEntity.Current.plantcode + ")");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_no_version(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT plnnr, plnal, werks, ktext
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview"
                + " WHERE  werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    plnnr||plnal not in (SELECT plnnr||alnal"
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal"
                    + " WHERE  werks in (" + DDRSessionEntity.Current.plantcode + "))"
                + " ORDER BY plnnr, plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_version_no_bom(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT matnr, werks, verid, plnnr, alnal, stlal, stlan
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal"
                + " WHERE  matnr||werks||stlal not in (SELECT matnr||werks||stlal"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader)"
                + " AND   werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_version_no_worksched(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.plnnr, a.alnal, a.verid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    b.sfcpf is null"
                + " AND    a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, a.werks, a.plnnr, a.alnal, a.verid");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_version_no_alloc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.verid, a.plnnr, a.alnal, a.stlal, b.plnnr as alloc_plnnr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipematlallocation b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.werks = b.werks (+)"
                + " AND    a.plnnr = b.plnnr (+)"
                + " AND    a.alnal = b.plnal (+)"
                + " AND    a.stlal = b.stlal (+)"
                + " AND    b.plnnr is null"
                + " AND    a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, a.werks, a.verid, a.plnnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_version_no_recipe(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.plnnr, a.alnal, a.werks, a.verid, a.stlal, a.stlan, a.text1, b.plnal
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND a.plnnr = b.plnnr (+)"
                + " AND a.alnal = b.plnal (+)"
                + " AND a.werks = b.werks (+)"
                + " AND b.plnal IS NULL"
                + " ORDER BY b.plnnr, b.plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_alloc_no_version(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.werks, a.plnnr, a.plnal, a.stlal, b.verid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipematlallocation a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.werks = b.werks (+)"
                + " AND    a.plnnr = b.plnnr (+)"
                + " AND    a.plnal = b.alnal (+)"
                + " AND    a.stlal = b.stlal (+)"
                + " AND    b.verid is null "
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, a.werks, a.plnnr, a.plnal, a.stlal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_bomdetail_no_alloc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, a.werks, a.stlal, a.posnr, a.idnrk, b.posnr as REC_POSNR
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipematlallocation b"
                + " WHERE  a.matnr = b.matnr (+)"
                + " AND    a.werks = b.werks (+)"
                + " AND    a.stlal = b.stlal (+)"
                + " AND    a.posnr = b.posnr (+)"
                + " AND    b.posnr is null "
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, a.werks, a.stlal, a.posnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_no_allocations(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.plnnr, a.plnal, a.werks, a.ktext
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipematlallocation b"
                + " WHERE  a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.plnnr = b.plnnr (+)"
                + " AND    a.plnal = b.plnal (+)"
                + " AND    a.werks = b.werks (+)"
                + " AND    b.plnal is null"
                + " ORDER BY a.plnnr, a.plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_relationship_bad(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.plnnr, b.plnal, b.vornr, b.vornr1
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_reciperelationships b"
                + " WHERE  werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.plnnr = b.plnnr"
                + " AND    a.plnal = b.plnal"
                + " AND    b.plnnr||b.plnal||b.vornr1 not in (SELECT plnnr||plnal||vornr"
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview c"
                    + " WHERE  b.plnnr = c.plnnr"
                    + " AND    b.plnal = c.plnal)"
                + " ORDER BY plnnr, plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_no_vge0x(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.PLNNR, a.PLNAL, VORNR, ARBPL, LTAX1, VGW01, VGE01, LAR01, '1' position 
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE a.plnnr = b.plnnr AND a.plnal = b.plnal"
                + " AND werks in (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND vgw01 IS NOT NULL AND vgw01 <> 0 AND vge01 IS NULL"
                + " UNION"
                + " SELECT a.PLNNR, a.PLNAL, VORNR, ARBPL, LTAX1, VGW02, VGE02, LAR02, '2' Activity  "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE a.plnnr = b.plnnr AND a.plnal = b.plnal"
                + " AND werks in (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND vgw02 IS NOT NULL AND vgw02 <> 0 AND vge02 IS NULL"
                + " UNION"
                + " SELECT a.PLNNR, a.PLNAL, VORNR, ARBPL, LTAX1, VGW03, VGE03, LAR03, '3' Activity  "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE a.plnnr = b.plnnr AND a.plnal = b.plnal"
                + " AND werks in (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND vgw03 IS NOT NULL AND vgw03 <> 0 AND vge03 IS NULL"
                + " UNION"
                + " SELECT a.PLNNR, a.PLNAL, VORNR, ARBPL, LTAX1, VGW04, VGE04, LAR04, '4' Activity  "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE a.plnnr = b.plnnr AND a.plnal = b.plnal"
                + " AND werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND vgw04 IS NOT NULL AND vgw04 <> 0 AND vge04 IS NULL"
                + " UNION"
                + " SELECT a.PLNNR, a.PLNAL, VORNR, ARBPL, LTAX1, VGW05, VGE05, LAR05, '5' Activity "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b"
                + " WHERE a.plnnr = b.plnnr AND a.plnal = b.plnal"
                + " AND werks in (" + DDRSessionEntity.Current.plantcode + ") "
                + " AND vgw05 IS NOT NULL AND vgw05 <> 0 AND vge05 IS NULL"); 
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_relationship_uom(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.plnnr, b.plnal, b.vornr, b.vornr1, b.dauer, b.dauermax, b.zeinh
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_reciperelationships b"
                + " WHERE  werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    a.plnnr = b.plnnr"
                + " AND    a.plnal = b.plnal"
                + " AND    nvl(b.dauer, '0') = 0"
                + " AND    nvl(b.dauermax, '0') = 0"
                + " AND    b.zeinh is not null"
                + " ORDER BY b.plnnr, b.plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_error_uom_not_base(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.werks, b.plnnr, b.plnal, a.meins, b.plnme, b.meinh
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal c"
                + " WHERE  a.matnr = c.matnr"
                + " AND    b.plnnr = c.plnnr"
                + " AND    b.plnal = c.alnal"
                + " AND    b.werks = c.werks"
                + " AND    b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND    (a.meins != b.plnme OR a.meins != b.meinh)"
                + " ORDER BY a.matnr, b.werks, b.plnnr, b.plnal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_audit_size_compare(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.stlal, a.bmeng, a.losvn, a.losbs,c.plnnr, c.plnal, 
                    b.bstmi, b.bstma, c.bmsch, c.meinh, c.losvn, c.losbs, b.verid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_BOMHEADER a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.GDD_MKAL b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.GDD_RECIPEGENERALVIEW c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF d"
                + " WHERE a.stlal = b.stlal "
                + " AND a.werks = b.werks"
                + " AND a.matnr = b.matnr "
                + " AND b.alnal = c.plnal"
                + " AND b.plnnr = c.plnnr"
                + " AND a.matnr = d.matnr"
                + " AND d.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.werks, a.matnr, a.stlal");
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
