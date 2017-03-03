using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class MMVerificationPlantDataReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
        public System.Data.DataTable SQL_MM_extract_accounting1(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT aa.matnr, aa.mtart, aa.meins, a.bwkey, a.bklas, a.peinh, a.stprs, a.hkmat, a.MTUSE, a.MTORG, a.OWNPR
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara aa, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE aa.matnr = a.matnr"
                + " AND   a.matnr = b.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.bwkey in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY aa.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_costing1(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.awsls, a.prctr, a.ncost, a.sobsk, a.losgr, b.hkmat, b.hrkft, a.mmsta
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.matnr = c.matnr"
                + " AND   a.werks = b.bwkey"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_foreigntradeImport(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT c.matnr, c.werks, c.stawn, c.mtver, c.gpnum, c.steuc, c.herkl, c.herkr, c.sobsl
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE c.matnr = a.matnr"
                + " AND   (c.stawn is not null OR c.mtver is not null "
                    + " or c.herkl is not null or c.herkr is not null)"
                + " AND   c.matnr IN (SELECT b.matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b WHERE b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND   c.ekgrp IS NOT NULL"
                    + " AND   a.ekwsl = '1'	"
                + " ORDER BY c.matnr,c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_foreigntradeExport(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT Distinct c.matnr, c.werks, c.stawn, c.mtver, c.gpnum, c.steuc, c.herkl, c.herkr, c.sobsl
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke e"
                + " WHERE (c.stawn is not null  OR  c.mtver is not null  OR  c.herkl is not null  OR  c.herkr is not null)"
                + " AND   c.matnr IN (SELECT b.matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b WHERE b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   e.vkorg IN (SELECT vkorg FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                  + " AND   c.matnr = e.matnr"
                + " AND   c.matnr = a.matnr "
                + " ORDER BY c.matnr,c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_mrp1(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.disgr, a.maabc, a.dismm, a.minbe, a.fxhor, a.dispo, a.disls,
                    a.bstmi, a.bstma, a.bstfe, a.mabst, a.bstrf, a.sobsl
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   (a.dismm is not null or a.dispo is not null or a.disls is not null)"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_mrp2(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.beskz, a.kzech, a.sobsl, a.lgpro, a.lgfsb, 
                    a.fabkz, a.schgt, a.kzkup, a.dzeit, a.plifz, a.webaz, 
                    a.mrppp, a.fhori, a.eisbe, a.rwpro, a.shflg, a.shzet, a.vspvb 
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND  (a.beskz is not null or a.mrppp is not null or a.fhori is not null)"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_mrp3(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.strgr, a.mtvfp, a.wzeit, a.vrmod, a.vint1, a.vint2, a.sobsl
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    (a.mtvfp is not null or a.strgr is not null or a.wzeit is not null)"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND    a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_mrp4(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.altsl, a.ahdis, a.sobsl
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   (a.dismm is not null or a.disls is not null or a.dispo is not null)"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_mrp_all4(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.disgr, a.maabc, a.dismm, a.minbe, a.fxhor, a.dispo, a.disls,
                    a.bstmi, a.bstma, a.bstfe, a.mabst, a.bstrf, 
                    a.beskz, a.sobsl, a.lgpro, a.lgfsb, a.plifz, a.webaz, a.mrppp, a.fhori, a.eisbe, a.shflg, a.shzet, 
                    a.strgr, a.mtvfp, a.wzeit, a.vrmod, a.vint1, a.vint2,
                    a.altsl, a.ahdis
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   (a.dismm is not null or a.dispo is not null or a.disls is not null"
                    + " or a.beskz is not null or a.mrppp is not null or a.fhori is not null)"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr,a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_mrparea(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT d.MATNR, d.WERKS, d.BERID, d.DISMM, d.minbe, d.FXHOR, d.DISPO, d.DISLS, d.BSTMI,d.BSTMA,d.BSTFE, d.MABST,d.BSTRF, d.sobsl,
	                d.lgpro, d.lgfsb, d.plifz, d.mrppp, d.eisbe, d.SHFLG, d.SHZET, d.RWPRO
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.GDD_MDMA d"
                    + " WHERE b.matnr = d.matnr"
                    + " AND   d.WERKS In (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " Order By d.MATNR,d.WERKS,d.BERID");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_purchasing(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.werks, a.ekwsl, b.ekgrp, b.fabkz, b.kautb, b.kordb, b.usequ, a.mtart, a.sapid, b.sobsl
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND NVL(a.ekwsl,'XX') <> 'XX'"
                + " AND NVL(b.ekgrp,'XX') <> 'XX'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_qm(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.werks, a.qmpur, b.qmata, b.kzdkz, b.prfrq, 
                    b.ssqss, b.mmsta, b.mmstd, a.mtart, a.sapid, b.sobsl, b.webaz
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.ssqss is not null"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND    b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr,b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_qmverify(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.werks, b.qmata, b.kzdkz, b.prfrq, b.mmsta, b.mmstd, a.qmpur,
                    b.ssqss, a.xchpf, a.mhdrz, a.mhdhb, a.sapid, a.mtart,b.sobsl
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND    b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_storageloc(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.matnr, b.werks, b.lgort, x.sobsl
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard b, "
                  + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MARC x"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " And   x.MATNR = b.MATNR"
                + " And   x.WERKS = b.WERKS	"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY b.matnr, b.werks, b.lgort");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_storageplant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, b.werks, b.abcin, b.ccfix, b.ausme, b.loggr, b.prctr, b.tempb2, b.raube2, b.zzaddlab
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d"
                + " WHERE a.matnr = b.matnr"
                + " AND   b.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.werks"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_storageverify(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.werks, c.lgort, b.ausme, a.stoff, a.etiar, a.etifo,b.sobsl
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d"
                + " WHERE a.matnr = b.matnr"
                + " AND   b.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.werks"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_worksched(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.matnr, b.werks, b.fevor, b.sfcpf, b.matgr,
                    b.kzech, b.uneto, b.ueeto, b.ueetk, b.basmg, b.frtme, a.mtart, b.sobsl
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    b.sfcpf is not null"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND    b.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_tarrif(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT c.matnr, c.werks, e.gzolx, e.prefe, e.preda, e.prene, e.preng
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mape e"
                + " WHERE c.matnr IN (SELECT b.matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b WHERE b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND   c.werks = e.werks"
                + " AND   a.matnr = e.matnr"
                + " AND   a.matnr = c.matnr "
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
        public System.Data.DataTable SQL_MM_extract_legalcontrol(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.aland, a.gegru, a.alnum, a.embgr, a.secgk, a.pmast
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_maex a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.matnr = b.matnr"
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
        public System.Data.DataTable SQL_MM_extract_zcountry(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.MATNR, b.maktx, a.WERKS, a.LAND1, a.VALID_FROM, a.VALID_FR_REASON, a.VALID_TO, a.VALID_TO_REASON, a.ADDITIONAL_INFO, a.FLAG, a.sap, a.site
                FROM " + DDRSessionEntity.Current.table_schema + "_procs.gdd_zcountry_list a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b "
                + " WHERE a.matnr = c.matnr"
                + " AND   a.matnr = b.matnr"
                + " AND   b.spras = 'E'"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   a.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, a.werks, a.land1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_basic_by_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.werks, a.mtart, a.mbrsh, a.meins, a.matkl, a.spart,
                    a.kosch, a.mstae, a.mstde, a.mtpos_mara, a.ean11, a.numtp, 
                    a.brgew, a.ntgew, a.gewei, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c "
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.matnr = c.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_descrip_eng_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT c.matnr, d.werks, c.maktx, c.spras, c.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   c.spras = 'E'"
                + " AND   d.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY c.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_altuom_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT c.matnr, d.werks, a.mtart, c.umren, c.meinh, c.umrez, c.LAENG, c.BREIT, c.HOEHE, c.MEABM,  a.meins, c.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
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
        public System.Data.DataTable SQL_MM_extract_prctr(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, a.werks, a.prctr
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
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
        public System.Data.DataTable SQL_MM_extract_storageglobal_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT distinct a.matnr, c.werks, a.stoff, a.etiar, a.etifo, a.xchpf, 
                    a.mhdrz, a.mhdhb, a.mhdlp, a.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.matnr = d.matnr"
                + " AND   c.werks = d.werks"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_tragr_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, c.werks, a.mtart, a.tragr, a.meins, a.matkl, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c "
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.matnr = c.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_class_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.objek, a.atinn, d.atnam, a.atwrt, a.sapid, c.werks
		        FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d"
                + " WHERE a.objek = b.matnr"
                + " AND b.matnr = c.matnr"
                + " AND a.atinn = d.atinn"
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " order by a.objek,a.atinn");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_aiU_profit(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, b.maktx, a.mtart, a.matkl, c.werks, d.umren, d.meinh, d.umrez, a.meins, c.prctr
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr (+)"
                + " AND   a.matnr = d.matnr (+)"
                + " AND  (d.meinh in ('BEA', 'LCC', 'LCG', 'LCK', 'LCM', 'MU') OR d.meinh is null)"
                + " AND   b.spras = 'E'"
                + " AND   a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND   c.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " ORDER BY a.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_extract_acct_cost(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT a.matnr, t.maktx, a.mtart, c.werks, b.bklas, b.stprs, b.peinh, c.beskz, c.sobsk, c.prctr, c.mmsta, c.ncost, b.hkmat, b.hrkft, c.losgr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt t"
                + " WHERE  c.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND    a.matnr = c.matnr "
                + " AND    a.matnr = t.matnr "
                + " AND    c.matnr = b.matnr "
                + " AND    c.werks = b.bwkey "
                + " AND    t.spras ='E' "
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
        public System.Data.DataTable SQL_MM_extract_format_ids_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.objek, e.werks, d.atnam, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc e"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = e.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND  (d.atnam = 'Z_EXPIRE_DATE' "
                    + " OR d.atnam = 'Z_MANUF_DATE_FORMAT')"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   e.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_us_chars_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT b.objek, e.werks, d.atnam, b.atwrt, b.sapid, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc e"
                + " WHERE a.matnr = b.objek"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = e.matnr "
                + " AND   b.atinn = d.atinn"
                + " AND  (d.atnam = 'Z_US_CONTROLLED_SUBSTANCE_TYPE' "
                    + " OR d.atnam = 'Z_US_OLD_MATERIAL_NUMBER'"
                    + " OR d.atnam = 'Z_US_PP_OVERDELIVERY_TOLERANCE' "
                    + " OR d.atnam = 'Z_US_PP_UNDRDELIVERY_TOLERANCE')"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND   e.werks in (" + DDRSessionEntity.Current.plantcode + ")"
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
        public System.Data.DataTable SQL_MM_extract_z_global_plant(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT ma.matnr OBJEK, mt.maktx, ma.mtart, ma.spart,
                c911, c912 , c910 , c908 , c914 , c1148, c1134, c913 , c909 , c898 , c900 , c901 , c1152,
                c1142, c1033, c1032, c951 , c954 , c952 , c953 , c955 , c1110, c1141, c1143, c957 , c1155, 
                c1156, c1166, c1167, c1329, c1224, c904, c1570, c1486,  c1647, c1372, c1944, 
                c905, cPack1, cPack2, c1857, c2136, c1928, cBLM, c2812, mc.werks, ma.sapid
                FROM 
                (SELECT objek, ATWRT AS c911  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 911  AND atzhl = '1') a , "
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
                + " (SELECT objek, ATWRT AS c1167 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1167 AND atzhl = '1') ac,"
                + " (SELECT objek, ATWRT AS c1329 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1329 AND atzhl = '1') ad,"
                + " (SELECT objek, ATWRT as c1224 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1224 AND atzhl = '1') ae, "
                + " (SELECT objek, ATWRT as c904  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 904  AND atzhl = '1') af,  "
                + " (SELECT objek, ATWRT as c1570 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1570 AND atzhl = '1') ag,  "
                + " (SELECT objek, ATWRT as c1486 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1486 AND atzhl = '1') ah,  "
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
                + " (SELECT objek, ATWRT as c2812  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 2812 AND atzhl = '1') ass,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref mr, "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt mt,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara ma,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc mc"
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
                + " ma.matnr = aa.objek(+) AND"
                + " ma.matnr = ab.objek(+) AND"
                + " ma.matnr = ac.objek(+) AND"
                + " ma.matnr = ad.objek(+) AND"
                + " ma.matnr = ae.objek(+) AND"
                + " ma.matnr = af.objek(+) AND "
                + " ma.matnr = ag.objek(+) AND"
                + " ma.matnr = ah.objek(+) AND"
                + " ma.matnr = ai.objek(+) AND"
                + " ma.matnr = aj.objek(+) AND"
                + " ma.matnr = ak.objek(+) AND"
                + " ma.matnr = al.objek(+) AND"
                + " ma.matnr = am.objek(+) AND"
                + " ma.matnr = an.objek(+) AND"
                + " ma.matnr = ao.objek(+) AND"
                + " ma.matnr = ap.objek(+) AND"
                + " ma.matnr = aq.objek(+) AND"
                + " ma.matnr = ar.objek(+) AND"
                + " ma.matnr = ass.objek(+) AND"
                + " ma.matnr = mr.matnr  AND "
                + " ma.matnr = mt.matnr  AND "
                + " ma.matnr = mc.matnr AND"
                + " mc.werks in (" + DDRSessionEntity.Current.plantcode + ") AND"
                + " mt.spras = 'E'AND "
                + " mr.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
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
        public System.Data.DataTable SQL_MM_extract_z_global_plant_financial(string title)
        {
            try
            {
                string command = String.Format(@"
				SELECT mc.matnr OBJEK, mt.maktx, ma.mtart, ma.spart,
                    c911 , c912 , c910 , c908 , c914 ,c951 , c1329,
                    mc.werks, ma.sapid
                FROM 
                (SELECT objek, ATWRT AS c911  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 911 ) a , "
                + " (SELECT objek, ATWRT AS c912  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 912 ) b , "
                + " (SELECT objek, ATWRT AS c910  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 910 ) c , "
                + " (SELECT objek, ATWRT AS c908  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 908 ) d , "
                + " (SELECT objek, ATWRT AS c914  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 914 ) e , "
                + " (SELECT objek, ATWRT AS c951  FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 951 ) q , "
                + " (SELECT objek, ATWRT AS c1329 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 1329) ad,  "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref mr, "
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt mt,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara ma,"
                + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc mc"
                + " WHERE ma.matnr =  a.objek(+) "
                + " AND   ma.matnr =  b.objek(+) "
                + " AND   ma.matnr =  c.objek(+) "
                + " AND   ma.matnr =  d.objek(+) "
                + " AND   ma.matnr =  e.objek(+) "
                + " AND   ma.matnr =  q.objek(+) "
                + " AND   ma.matnr = ad.objek(+) "
                + " AND   ma.matnr = mr.matnr "
                + " AND   ma.matnr = mt.matnr  "
                + " AND   ma.matnr = mc.matnr "
                + " AND   ma.matnr = a.objek(+)"
                + " AND   mc.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND   mt.spras = 'E'"
                + " AND   mr.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
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
    }
}
