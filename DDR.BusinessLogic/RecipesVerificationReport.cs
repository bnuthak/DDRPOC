using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class RecipesVerificationReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_Recipes_extract_rec_header(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT plnnr, plnal, ktext, werks, statu,
                    verwe, losvn, losbs, plnme, bmsch, meinh
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview"
                + " WHERE werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_Recipes_extract_rec_operations(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.plnnr, b.plnal, b.vornr, b.phflg, b.pvznr, b.phseq, b.arbpl, b.steus, b.ktsch, b.ltax1,
                    b.bmsch, b.meinh, b.rfgrp, b.vgw01, b.vge01, b.lar01,
                    b.vgw02, b.vge02, b.lar02, b.vgw03, b.vge03, b.lar03,
                    b.vgw04, b.vge04, b.lar04, b.vgw05, b.vge05, b.lar05,
                    b.infnr, b.ekorg, b.sakto, b.frdlb
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview b"
                + " WHERE  a.plnnr = b.plnnr"
                + " AND    a.plnal = b.plnal "
                + " AND    a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY  b.plnnr, b.plnal, b.vornr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_extract_rec_secresource(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT c.plnnr, c.plnal, c.vornr, c.uvorn, c.arbpl, c.steus, c.ltax1,
                c.bmsch, c.meinh, c.vgw01, c.vge01, c.lar01,
                c.vgw02, c.vge02, c.lar02, c.vgw03, c.vge03, c.lar03,
                c.vgw04, c.vge04, c.lar04, c.vgw05, c.vge05, c.lar05
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipesecresource c"
                + " WHERE a.plnnr = b.plnnr"
                + " AND   a.plnal = b.plnal"
                + " AND   b.plnnr = c.plnnr"
                + " AND   b.plnal = c.plnal"
                + " AND   b.vornr = c.vornr "
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY c.plnnr, c.plnal, c.vornr, c.uvorn");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_extract_rec_oprelations(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT c.plnnr, c.plnal, c.vornr, c.vornr1, c.dauer, c.dauermax, c.zeinh, c.aobar, c.kalid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_reciperelationships c"
                + " WHERE a.plnnr = b.plnnr"
                + " AND   a.plnal = b.plnal"
                + " AND   b.plnnr = c.plnnr"
                + " AND   b.plnal = c.plnal"
                + " AND   b.vornr = c.vornr"
                + " AND a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY c.plnnr, c.plnal, c.vornr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_extract_rec_userfld(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.plnnr, b.plnal, b.vornr, b.slwid, b.text1, b.text3, b.quan1, b.quan_uom1, b.indc1, b.indc2
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipegeneralview a, "
                        + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipeoperationoverview b"
                + " WHERE a.plnnr = b.plnnr"
                + " AND   a.plnal = b.plnal "
                + " AND   b.vornr = '1000'"
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY b.plnnr, b.plnal, b.vornr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_extract_rec_matalloc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.plnnr, a.plnal, c.verid, a.matnr, a.werks, a.stlal, a.vornr, a.posnr, b.idnrk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_recipematlallocation a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.werks = b.werks"
                + " AND   a.werks = c.werks"
                + " AND   a.stlal = b.stlal"
                + " AND   a.stlal = c.stlal"
                + " AND   a.posnr = b.posnr"
                + " AND   a.plnnr = c.plnnr"
                + " AND   a.plnal = c.alnal"
                + " AND  a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.plnnr, a.plnal, a.matnr, c.verid, a.posnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_Recipes_extract_rec_version(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT werks, matnr, verid, text1, bstmi, bstma, plnty, plnnr,
                alnal, stlal, stlan, adatu, bdatu, alort
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mkal"
                + " WHERE werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY plnnr, alnal");
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
