using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class MMVerificationGlobalDataReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();

        public System.Data.DataTable SQL_MM_extract_basicdata(string title) //1
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.mbrsh, a.meins, a.matkl, a.spart,
                a.kosch, a.mstae, a.mstde, a.mtpos_mara, a.ean11, a.numtp, 
                a.brgew, a.ntgew, a.gewei, a.PRDHA, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; //"Extract Basic Data";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_descriptions(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.maktx, c.spras, c.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr, c.spras");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; //"Extract Description";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_descrip_english(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.maktx, c.spras, c.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   c.spras = 'E'"
                + " ORDER BY c.matnr, c.spras");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; //"Extract English Description";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_storageglobal(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, a.stoff, a.etiar, a.etifo, a.xchpf, 
                   a.mhdrz, a.mhdhb, a.mhdlp, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
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
        public System.Data.DataTable SQL_MM_extract_storageglobal_nomard(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, a.stoff, a.etiar, a.etifo, a.xchpf, 
                   a.mhdrz, a.mhdhb, a.mhdlp, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
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
        public System.Data.DataTable SQL_MM_extract_altuom(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.umren, c.meinh, c.umrez, c.LAENG, c.BREIT, 
                    c.HOEHE, c.MEABM, a.meins, c.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_altean(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.meinh, c.hpean, c.ean11, c.eantp, c.sapid, a.mtart
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mean c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_proportional(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.atnam, c.atwrt, c.wsmei, a.kzwsm, c.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2 c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr, c.atnam");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_jdnet(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.MATNR, b.maktx,
                   JDNET_UNI_MATNR, JDNET_PRODUCT, JDNET_PACK_UNIT, JDNET_PUR_UNIT, JDNET_MED_CODE, JDNET_BIO_DIV, JDNET_MED_DIV, JDNET_REFG_DIV, 
                   JDNET_STOR_COND, JDNET_PACK_WT, JDNET_PACK_W, JDNET_PACK_L, JDNET_PACK_H, JDNET_PACK_VOL, JDNET_UNIT_WT, JDNET_UNIT_L, JDNET_UNIT_W,   
                   JDNET_UNIT_H, JDNET_UNIT_VOL, JDNET_UNIT_WT1, JDNET_UNIT_WT2, JDNET_UNIT_LEN1, JDNET_UNIT_LEN2, JDNET_UNIT_LEN3, JDNET_UNIT_LEN4,
                   JDNET_UNIT_LEN5, JDNET_UNIT_LEN6, JDNET_UNIT_VOL1, JDNET_UNIT_VOL2, JDNET_VAL_DATE, JDNET_VAL_FORM
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_JDNET a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b  "
                + " WHERE a.matnr = c.matnr"
                + " AND   a.matnr = b.matnr"
                + " AND   b.spras = 'E'"
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
        public System.Data.DataTable SQL_MM_extract_basictext(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.spras, c.lineno, c.ltext, c.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_othertext c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.tdid = 'GRUN'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr, c.spras");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_inspecttext(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.spras, c.lineno, c.ltext, c.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_othertext c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.tdid = 'PRUE'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr, c.spras");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_internaltext(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.spras, c.lineno, c.ltext, c.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_othertext c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.tdid = 'IVER'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr, c.spras");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_purchtext(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.spras, c.lineno, c.ltext, c.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_othertext c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.tdid = 'BEST'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY c.matnr, c.spras");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_presentation(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PRESENTATION'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_prodnum(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PRODUCT_NUMBER'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_pkg_size(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PACKAGE_SIZE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_label(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_LABEL_CODE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_subselling(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_SUBSELLING_MARKET'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_matuse(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_MATERIAL_USE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; //"Extract Material Use";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_galenic(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_GALENIC_FORM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_submole(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_SUBMOLECULE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_molecule(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_MOLECULE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_brand(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_BRAND_NAME'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_dosage_strength(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_DOSAGE_STRENGTH_QUANTITY'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_dosage_stren_uom(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_DOSAGE_STRENGTH_UOM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_strength_comp(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_STRENGTH_COMPONENT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_dose_form(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_DOSE_FORM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_pack_format(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PACK_FORMAT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_pack_type(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PACK_TYPE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_var_potency(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_VARIABLE_POTENCY'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_manuf_date(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_MANUF_DATE_FORMAT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_expire_date(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b," + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_EXPIRE_DATE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    //    public System.Data.DataTable SQL_MM_extract_z_lilly_num(string title)
    //    {
    //        try
    //        {
    //            string command = String.Format(@"
				//");
    //            System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
    //            mmdataset.TableName = title; 
    //            return mmdataset;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
        public System.Data.DataTable SQL_MM_extract_z_sub_inspect(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_SUBSEQ_INSPECT_INTERVAL'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_animal(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_ANIMAL_SOURCE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_bse_free(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_BSE_FREE_COUNTRY_SOURCE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_bulk_recycle(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_BULK_RECYCLE_INDICATOR'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_pharma_code(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PHARMA_CODE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_printed(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PRINTED'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_fill_qty(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c , " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_FILL_QTY_OR_COUNT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_fill_qtyuom(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_FILL_QTY_OR_COUNT_UOM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_cas(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_CAS'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_serial_num(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_SERIAL_NUMBER'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_special_security(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_LLY_SPECIAL_SECURITY_SUBSTNC'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_stren_mole(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_STRENGTH_MOLECULE_CONVERSION'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_init_retest_trigger(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_INIT_RETEST_TRIGGER'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_msds_title(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " 
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_MSDS_TITLE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_retention_sample(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " 
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_RETENTION_SAMPLE_DAYS'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_FINISHED_PACKAGE_QUANTITY(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_FINISHED_PACKAGE_QUANTITY'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_FINISHED_PACKAGE_UOM(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_FINISHED_PACKAGE_UOM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_PACK_ATTRRIBUTE_1(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PACK_ATTRRIBUTE_1'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_PACK_ATTRRIBUTE_2(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PACK_ATTRRIBUTE_2'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_CONTRACT_MFG_ORDER_TYPE(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_CONTRACT_MFG_ORDER_TYPE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_STO_TMP_CONDITION_REGISTERED(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_STO_TMP_CONDITION_REGISTERED'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_RES_SAMP_DISC_RULE(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_RES_SAMP_DISC_RULE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_EXP_DATE_POTENCY(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_EXP_DATE_POTENCY'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_BATCH_RELEASE_LIMIT(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_BATCH_RELEASE_LIMIT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_Z_ACTIVITY_FOR_NU(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_ACTIVITY_FOR_NU'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_global_all(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT ma.matnr OBJEK, mt.maktx, ma.mtart, ma.spart,
                    c911, c912 , c910 , c908 , c914 , c1148, c1134, c913 , c909 , c898 , c900 , c901 , c1152,
                    c1142, c1033, c1032, c951 , c954 , c952 , c953 , c955 , c1110, c1141, c1143, c957 , c1155, 
                    c1156, c1166, c1167, c1329, c1224, c904, c1570, c1486,  c1647, c1372, c1944, 
                    c905, cPack1, cPack2, c1857, c2136, c1928, cBLM, c2812, ma.sapid

                FROM 
                (SELECT objek, ATWRT AS c911  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 911  AND atzhl = '1') a ,  "
                + " (SELECT objek, ATWRT AS c912  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 912  AND atzhl = '1') b , "
                + " (SELECT objek, ATWRT AS c910  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 910  AND atzhl = '1') c , "
                + " (SELECT objek, ATWRT AS c908  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 908  AND atzhl = '1') d , "
                + " (SELECT objek, ATWRT AS c914  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 914  AND atzhl = '1') e , "
                + " (SELECT objek, ATWRT AS c1148 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1148 AND atzhl = '1') f , "
                + " (SELECT objek, ATWRT AS c1134 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1134 AND atzhl = '1') g , "
                + " (SELECT objek, ATWRT AS c913  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 913  AND atzhl = '1') h , "
                + " (SELECT objek, ATWRT AS c909  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 909  AND atzhl = '1') i , "
                + " (SELECT objek, ATWRT AS c898  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 898  AND atzhl = '1') j , "
                + " (SELECT objek, ATWRT AS c900  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 900  AND atzhl = '1') k , "
                + " (SELECT objek, ATWRT AS c901  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 901  AND atzhl = '1') l , "
                + " (SELECT objek, ATWRT AS c1152 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1152 AND atzhl = '1') m , "
                + " (SELECT objek, ATWRT AS c1142 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1142 AND atzhl = '1') n , "
                + " (SELECT objek, ATWRT AS c1033 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1033 AND atzhl = '1') o , "
                + " (SELECT objek, ATWRT AS c1032 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1032 AND atzhl = '1') p , "
                + " (SELECT objek, ATWRT AS c951  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 951  AND atzhl = '1') q , "
                + " (SELECT objek, ATWRT AS c954  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 954  AND atzhl = '1') r , "
                + " (SELECT objek, ATWRT AS c952  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 952  AND atzhl = '1') s , "
                + " (SELECT objek, ATWRT AS c953  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 953  AND atzhl = '1') t , "
                + " (SELECT objek, ATWRT AS c955  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 955  AND atzhl = '1') u , "
                + " (SELECT objek, ATWRT AS c1110 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1110 AND atzhl = '1') v , "
                + " (SELECT objek, ATWRT AS c1141 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1141 AND atzhl = '1') w , "
                + " (SELECT objek, ATWRT AS c1143 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1143 AND atzhl = '1') x , "
                + " (SELECT objek, ATWRT AS c957  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 957  AND atzhl = '1') y , "
                + " (SELECT objek, ATWRT AS c1155 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1155 AND atzhl = '1') z , "
                + " (SELECT objek, ATWRT AS c1156 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1156 AND atzhl = '1') aa , "
                + " (SELECT objek, ATWRT AS c1166 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1166 AND atzhl = '1') ab , "
                + " (SELECT objek, ATWRT AS c1167 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1167 AND atzhl = '1') ac, "
                + " (SELECT objek, ATWRT AS c1329 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1329 AND atzhl = '1') ad, "
                + " (SELECT objek, ATWRT as c1224 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1224 AND atzhl = '1') ae, "
                + " (SELECT objek, ATWRT as c904  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 904  AND atzhl = '1') af, "
                + " (SELECT objek, ATWRT as c1570 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1570 AND atzhl = '1') ag, "
                + " (SELECT objek, ATWRT as c1486 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1486 AND atzhl = '1') ah, "
                + " (SELECT objek, ATWRT as c1647 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1647 AND atzhl = '1') ai, "
                + " (SELECT objek, ATWRT as c1372 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1372 AND atzhl = '1') aj, "
                + " (SELECT objek, ATWRT as c1944 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1944 AND atzhl = '1') ak, "
                + " (SELECT objek, ATWRT as c905   FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 905 AND atzhl = '1') al, "
                + " (SELECT objek, ATWRT as cPack1 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = (Select atinn From " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn where atnam='Z_PACK_ATTRRIBUTE_1')) am, "
                + " (SELECT objek, ATWRT as cPack2 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = (Select atinn From " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn where atnam='Z_PACK_ATTRRIBUTE_2')) an, "
                + " (SELECT objek, ATWRT as c1857  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1857 AND atzhl = '1') ao, "
                + " (SELECT objek, ATWRT as c2136  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 2136 AND atzhl = '1') ap, "
                + " (SELECT objek, ATWRT as c1928  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1928 AND atzhl = '1') aq, "
                + " (SELECT objek, ATWRT as cBLM   FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = (Select atinn From " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn where atnam='Z_BATCH_RELEASE_LIMIT')) ar, "
                + " (SELECT objek, ATWRT as c2812  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 2812 AND atzhl = '1') ass, "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref mr, "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt mt,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara ma"

                + " WHERE"
                + " ma.matnr =  a.objek(+) AND "
                + " ma.matnr =  b.objek(+) AND "
                + " ma.matnr =  c.objek(+) AND "
                + " ma.matnr =  d.objek(+) AND "
                + " ma.matnr =  e.objek(+) AND "
                + " ma.matnr =  f.objek(+) AND "
                + " ma.matnr =  g.objek(+) AND "
                + " ma.matnr =  h.objek(+) AND "
                + " ma.matnr =  i.objek(+) AND "
                + " ma.matnr =  j.objek(+) AND "
                + " ma.matnr =  k.objek(+) AND "
                + " ma.matnr =  l.objek(+) AND "
                + " ma.matnr =  m.objek(+) AND "
                + " ma.matnr =  n.objek(+) AND "
                + " ma.matnr =  o.objek(+) AND "
                + " ma.matnr =  p.objek(+) AND "
                + " ma.matnr =  q.objek(+) AND "
                + " ma.matnr =  r.objek(+) AND "
                + " ma.matnr =  s.objek(+) AND "
                + " ma.matnr =  t.objek(+) AND "
                + " ma.matnr =  u.objek(+) AND "
                + " ma.matnr =  v.objek(+) AND "
                + " ma.matnr =  w.objek(+) AND "
                + " ma.matnr =  x.objek(+) AND "
                + " ma.matnr =  y.objek(+) AND "
                + " ma.matnr =  z.objek(+) AND "
                + " ma.matnr = aa.objek(+) AND "
                + " ma.matnr = ab.objek(+) AND "
                + " ma.matnr = ac.objek(+) AND "
                + " ma.matnr = ad.objek(+) AND "
                + " ma.matnr = ae.objek(+) AND "
                + " ma.matnr = af.objek(+) AND "
                + " ma.matnr = ag.objek(+) AND "
                + " ma.matnr = ah.objek(+) AND "
                + " ma.matnr = ai.objek(+) AND "
                + " ma.matnr = aj.objek(+) AND "
                + " ma.matnr = ak.objek(+) AND "
                + " ma.matnr = al.objek(+) AND "
                + " ma.matnr = am.objek(+) AND "
                + " ma.matnr = an.objek(+) AND "
                + " ma.matnr = ao.objek(+) AND "
                + " ma.matnr = ap.objek(+) AND "
                + " ma.matnr = aq.objek(+) AND "
                + " ma.matnr = ar.objek(+) AND "
                + " ma.matnr = ass.objek(+) AND "
                + " ma.matnr = mr.matnr  AND "
                + " ma.matnr = mt.matnr  AND "
                + " mt.spras = 'E'AND "
                + " mr.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_us_oldmatnr(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek "
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_US_OLD_MATERIAL_NUMBER'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_us_controlled(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_US_CONTROLLED_SUBSTANCE_TYPE' "
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_us_overtol(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_US_PP_OVERDELIVERY_TOLERANCE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_us_undertol(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_US_PP_UNDRDELIVERY_TOLERANCE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_us_chars(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, d.atnam,b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam like 'Z_US_%'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_acquisition(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.maktx, a.mtart,
                c1836, c1837, c1845, c1846, c1847, c1848, c1849, a.sapid
                FROM 
                    (SELECT objek, ATWRT AS c1836  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1836  AND atzhl = '1') p ,"
                    + " (SELECT objek, ATWRT AS c1837  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1837  AND atzhl = '1') q ,"
                    + " (SELECT objek, ATWRT AS c1845  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1845  AND atzhl = '1') r ,"
                    + " (SELECT objek, ATWRT AS c1846  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1846  AND atzhl = '1') s ,"
                    + " (SELECT objek, ATWRT AS c1847  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1847  AND atzhl = '1') t ,"
                    + " (SELECT objek, ATWRT AS c1848  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1848  AND atzhl = '1') u ,"
                    + " (SELECT objek, ATWRT AS c1849  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1849  AND atzhl = '1') v ,"
                    + " " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + " " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b,"
                    + " " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = p.objek (+)"
                + " AND   a.matnr = q.objek (+)"
                + " AND   a.matnr = r.objek (+)"
                + " AND   a.matnr = s.objek (+)"
                + " AND   a.matnr = t.objek (+)"
                + " AND   a.matnr = u.objek (+)"
                + " AND   a.matnr = v.objek (+)"
                + " AND   a.matnr = c.matnr "
                + " AND   a.matnr = b.matnr"
                + " AND   b.spras = 'E'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
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
        public System.Data.DataTable SQL_MM_extract_z_multikit(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr"
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_MULTI_KIT_TYPE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_contents(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMPONENT_CONTENTS'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_stren1(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_STRENGTH_1'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_strenuom1(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_STRENGTH_UOM_1'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_form1(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_FORM_1'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_stren2(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_STRENGTH_2'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_strenuom2(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr"
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_STRENGTH_UOM_2'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_form2(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_FORM_2'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_stren3(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_STRENGTH_3'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_strenuom3(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_STRENGTH_UOM_3'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_comp_form3(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "                 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_COMP_FORM_3'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_total_stren(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_TOTAL_DOSAGE_STRENGTH'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_total_strenuom(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_TOTAL_DOSAGE_STRENGTH_UOM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_z_total_form(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_TOTAL_COMP_FORM'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_waste_presentation(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = 1284"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_waste_prodnum(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = 1283"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_waste_genarea(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_GEN_AREA'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_waste_dispcode(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_DISP_CODE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_family(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_FAMILY'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_reorder(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_REORDER_POINT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_location(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_LOCATION'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_regulatory(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_REGULATORY_GROUP'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_shopping(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_SHOPPING_LIST'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_territory(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_TERRITORY_LIMIT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_priceunit(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_PRICE_UNIT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_promo_packsize(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, " + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_PROMO_PACK_SIZE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_class2_expire_date(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp2 b," + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_EXPIRE_DATE'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; 
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_class2_mfg_date(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT b.objek, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " 
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp2 b," + DDRSessionEntity.Current.table_schema 
                + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND   d.atnam = 'Z_MANUF_DATE_FORMAT'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY b.objek");
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
