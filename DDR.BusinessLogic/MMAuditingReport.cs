using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;
namespace DDR.BusinessLogic
{
    public class MMAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();

        public System.Data.DataTable SQL_MM_Errors_No_Makt(string title) //1
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr AND a.matnr not in (select distinct matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt) AND b.aland =" + "'" + DDRSessionEntity.Current.SiteCode + "'");
                //System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_MM_Errors_No_Makt_E(string title) //2
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr AND a.matnr not in (select matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt where spras = 'E') AND b.aland =" + "'" + DDRSessionEntity.Current.SiteCode + "'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Basic_Eancat(string title) //3
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.ean11, a.numtp, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.numtp is null "
                + " AND   a.ean11 is not null "
                + " AND   b.aland =" + "'" + DDRSessionEntity.Current.SiteCode + "'" + " ORDER BY a.matnr ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Basic Data: Ean Missing Ean Category";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Ferts_Expire(string title) //4
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, a.mhdrz, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c "
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr "
                + " AND    a.mtart = 'FERT' "
                + " AND    NVL(a.mhdrz,'0') = '0' "
                + " AND    b.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "') "
                + " AND    c.aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " ORDER BY a.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Ferts That Do Not Expire";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sales_Ntgew(string title) //5
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, ntgew, brgew
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b "
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.ntgew > a.brgew "
                + " AND    b.aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + "ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Net Weight Is Greater Than Gross Weight";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mara_Miss_Data(string title) //6
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, mtart, meins, mbrsh, spart, sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara "
                + " WHERE matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_Ref WHERE aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "' ) "
                + " AND (mtart is null or meins is null or mbrsh is null or spart is null or matkl is null) "
                + " ORDER BY matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Missing Values On MARA,MTART,MEINS,MBRSH,MATKL";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_fert_01M_ea(string title) //7
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, substr(a.matnr,7,3) as PACK, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr "
                + " AND    b.aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart in ('FERT', 'HAWA', 'ZPRM') "
                + " AND    nvl(a.meins, 'XX') != 'EA'"
                + " AND    substr(a.matnr,7,3) in ('01M', '001') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - Material Type FERT, HAWA, ZPRM w/ Pack Code 001 or 01M, Base UoM MUST be EA";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_halb_01M_ts(string title) //8
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, substr(a.matnr,7,3) as PACK, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr "
                + " AND    b.aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart in ('HALB') "
                + " AND    nvl(a.meins, 'XX') != 'TS'"
                + " AND    substr(a.matnr,7,3) = '01M'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - Material Type HALB with Pack Code 01M, Base UoM MUST be TS";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_V1M_ts(string title) //9
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, substr(a.matnr,7,3) as PACK, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr "
                + " AND    b.aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    nvl(a.meins, 'XX') != 'TS'"
                + " AND    substr(a.matnr,7,3) = 'V1M'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - For Pack Code V1M, Base UoM MUST be TS";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_halb_001_ea(string title) //10
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, substr(a.matnr,7,3) as PACK, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr "
                + " AND    b.aland ="
                + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart in ('HALB') "
                + " AND    nvl(a.meins, 'XX') != 'EA'"
                + " AND    substr(a.matnr,7,3) in ('001', 'V01') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Material Type HALB, Pack Code 001 or V01, Base UoM MUST be EA";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_MATKL_Invalid(string title) //11
        {
            try
            {
                ///  SAP ACCESS REQUIRED ///
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.MATKL, " + "'" + DDRSessionEntity.Current.mapinstance + "'" + " SAP FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a "
                + "WHERE  a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland =" + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load ='X') "
                + "AND    a.MATKL NOT IN (SELECT MATKL FROM  " + DDRSessionEntity.Current.mapinstance + ".T023t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt = '100' AND spras = 'E') "
                + "AND    NVL(MATKL, ' ') <> ' ' "
                + "ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Material Group is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_matkl_roh(string title) //12
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, a.matkl, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    b.aland = " + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'ROH'"
                + " AND    nvl(a.matkl, 'XX') not in ('L20', 'M25', 'R20', 'R40', 'R45', 'F02', 'R15') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Error - Material Type ROH, Material Group Should be F02, L20, M25, R15, R20, R40 or R45";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_matkl_halb(string title) //13
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, a.matkl, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    b.aland = " + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'HALB'"
                + " AND    nvl(a.matkl, 'XX') not in ('R05', 'R15', 'R20', 'R25', 'R40', 'R45') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Error - Material Type HALB, Material Group Should be R05, R15, R20, R25, R40, or R45";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_matkl_fert(string title) //14
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, a.matkl, a.sapid
			    FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    b.aland = " + "'" + DDRSessionEntity.Current.SiteCode + "'"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'FERT'"
                + " AND    nvl(a.matkl, 'XX') not in ('R05', 'R10') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Error - Material Type FERT, Material Group Should be R05 or R10";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_PRDHA_MTART_required(string title) //15
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.prdha
			    FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.MTART in ('FERT', 'HALB', 'VERP', 'HAWA', 'ZGCM', 'ROH', 'ZMAB') "
                + " AND nvl(prdha, ' ') = ' '"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Product Hierarchy required for FERT, HALB, HAWA, ROH, VERP, ZGCM, ZMAB";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_PRDHA_MTART_mismatch(string title) //16
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.prdha
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.mtart != 'ZUNB'"
                + " AND    substr(prdha, 1, 3) <> substr(mtart, 1, 3) "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Product Hierarchy does not match Material Type";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_PRDHA_InvalidValue(string title) //17
        {
            try
            {
                ///  SAP ACCESS REQUIRED ///
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.prdha, '" + DDRSessionEntity.Current.mapinstance + "' as SAP "
                + " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    prdha not in (select prodh from " + DDRSessionEntity.Current.mapinstance + ".T179t@" + DDRSessionEntity.Current.mapinstance + " where mandt = '100' and spras = 'E') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Product Hierarchy value not setup in SAP";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_MTART_MTPOS_NORM(string title) //18
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.mtpos_mara
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.mtart <> 'ZMAB'"
                + " AND    a.mtpos_mara <> 'NORM'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Gen Item Category should be NORM for all material types except for ZMAB";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_MTART_MTPOS_ZVPT(string title) //19
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.mtpos_mara
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.mtart = 'ZMAB'"
                + " AND    a.mtpos_mara <> 'ZVPT'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Gen Item Category should be ZVPT for Semifin Dimensionless materials (ZMAB) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_BAS_MEINS_InvalidValue(string title) //20
        {
            try
            {
                ///  SAP ACCESS REQUIRED ///
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.meins, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    meins NOT IN(SELECT mseh3 FROM " + DDRSessionEntity.Current.mapinstance + ".T006A@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt = '100' AND spras = 'E') "
                + " ORDER BY a.matnr");
                // The code below is the same query as above, but the SAP instance is hard-coded in.
                //string command = String.Format(@"
                //SELECT a.matnr, a.sapid, b.maktx, a.mtart, a.meins, 'GQA' as SAP"
                //+ " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                //+ " WHERE  a.matnr  IN(SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = " + "'" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                //+ " AND    a.matnr = b.matnr"
                //+ " AND    b.spras = 'E'"
                //+ " AND    meins NOT IN(SELECT mseh3 FROM " + "GQA" + ".T006A@" + "GQA" + " WHERE mandt = '100' AND spras = 'E') "
                //+ " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Base Unit of Measure value not setup in SAP";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Profit(string title) //21, 64, 144
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, b.werks, b.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                    + " WHERE a.matnr = b.matnr "
                    + " AND   a.matnr = c.matnr "
                    + " AND   nvl(b.prctr, ' ') = ' ' "
                    + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                    + " AND   b.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Materials Missing Profit Center";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Stdpricezero(string title) //22
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, bwkey, stprs, peinh
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew"
                + " WHERE  stprs = 0"
                + " AND    bwkey IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref "
                    + " WHERE  aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " ORDER BY matnr, bwkey");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Standard Price (Stprs) Is Set To 0";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_errors_zunb_acct(string title) //23
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, b.bwkey, b.bklas, b.stprs, b.peinh, b.hkmat
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c "
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart = 'ZUNB'"
                + " AND   b.bwkey in (SELECT werks "
                              + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                              + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - ZUNB has Accounting Data";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_errors_zunb_cost(string title) //24
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, b.werks, b.losgr, b.ncost, b.sobsk, b.awsls
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c "
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart = 'ZUNB'"
                + " AND  (b.losgr is not null OR b.awsls is not null OR b.ncost is not null OR b.sobsk is not null) "
                + " AND   b.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - ZUNB has Costing Data";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Costingflagset(string title) //25
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.ncost
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc A, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref B"
                + " WHERE a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.matnr = b.matnr"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   ncost = 'X'");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Costing Flag (Ncost) Set";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sobsk30_No_Bom(string title) //26
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, a.sobsl, a.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.sobsk = '30'"
                + " AND    a.matnr||a.werks not IN (SELECT matnr||werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks  "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Costing SP Procurement Type 30 And No Bom";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sobsk30_No_F_X(string title) //27
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, a.sobsl, a.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.sobsk = '30'"
                + " AND    NVL(a.beskz, 'Q') not in ('F', 'X') "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Costing SP Procurement Type 30 And Procurement Not F Or X";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Procurtyp_Costnglot(string title) //28
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, beskz, losgr
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc "
                + " WHERE beskz = 'F'"
                + " AND   losgr != '1'"
                + " AND   losgr != '1000'"
                + " AND   (sobsk <> '30' OR sobsl is NULL) "
                + " AND   werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Special Procurement Type F, Indicator <> 30, Costing Lot Size Conflict (<> 1 Or 1000) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Costing(string title) //29
        {
            try
            {
                string command = String.Format(@"
                SELECT b.matnr, b.werks, b.losgr, b.prctr, c.hkmat
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref e"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.aland = e.aland"
                + " AND   b.werks = e.werks"
                + " AND   c.hkmat is null"
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Costing View Incomplete/Missing (Material Origin Empty) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Cost_lot_size_not_less_price_unit(string title) //30
        {
            try
            {
                string command = String.Format(@"
                SELECT A.matnr, B.werks, A.meins, C.stprs, B.losgr, C.peinh
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara A, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc B, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew C, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref D"
                + " WHERE  A.matnr = B.matnr"
                + " AND    A.matnr = D.matnr"
                + " AND    B.matnr = C.matnr"
                + " AND    B.werks = C.bwkey"
                + " AND    B.losgr < C.peinh"
                + " AND    D.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    B.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY B.werks, A.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Costing Lot Size Cannot be Less than Price Unit";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Size_Price(string title) //31
        {
            try
            {
                string command = String.Format(@"
                SELECT b.matnr, b.werks, b.losgr, c.peinh, c.stprs
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   b.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   b.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref "
                    + " WHERE  aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Costing Lot Size, Price Unit And Standard Price";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Lotsize_To_Basmg(string title) //32
        {
            try
            {
                string command = String.Format(@"
                SELECT b.matnr, b.werks, b.losgr, b.bstfe, b.basmg, a.mtart
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b"
                + " WHERE a.matnr = b.matnr"
                + " AND   b.losgr is not null"
                + " AND   b.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   b.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref "
                    + " WHERE  aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Costing Lot Size, Fixed Lot Size, And Work Sched Base Qty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Dislsfx_No_Bstfe(string title) //33, 75
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.disls, a.bstfe
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = c.matnr"
                + " AND   b.aland = c.aland"
                + " AND   a.werks = b.werks"
                + " AND   a.disls = 'FX'"
                + " AND   a.bstfe is null "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Lot Size(Disls) Is Fixed But Fixed Lot Size(Bstfe) Empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Valclass_Stprs(string title) //34
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.bwkey, a.bklas, a.stprs, a.peinh
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.bklas is null"
                + " AND    (a.stprs is not null"
                    + " OR a.peinh is not null) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.bwkey in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.bwkey");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Valuation Class Is Empty When Stprs Or Peinh Has A Value";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mat_Type_Hawa(string title) //35
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.bwkey, b.mtart, a.bklas, c.aland
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b,"
                + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.matnr = c.matnr   "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.bwkey in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.GDD_WERKS_REF WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    mtart = 'HAWA'"
                + " AND    bklas NOT IN ('3100','3103', '7930', '7936') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Material Type HAWA With Invalid Valuation Class";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mat_Type_Verp(string title) //36
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.bwkey, b.mtart, a.bklas, c.aland
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b,"
                + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.matnr = c.matnr"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.bwkey in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.GDD_WERKS_REF WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.mtart = 'VERP'"
                + " AND    a.bklas NOT IN ('3050')  "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Material Type VERP With Invalid Valuation Class";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mat_Type_Fert(string title) //37
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.bwkey, b.mtart, a.bklas, c.aland
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b,"
                + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF c	"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.matnr = c.matnr  "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.bwkey in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.GDD_WERKS_REF WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.mtart = 'FERT'"
                + " AND    a.bklas NOT IN ('3103','3104','7930','7936','7920','7935','7955','7956','7940')   "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Material Type FERT With Invalid Valuation Class";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mat_Type_Halb(string title) //38
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.bwkey, b.mtart, a.bklas, c.aland
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b,"
                + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.matnr = c.matnr "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.bwkey in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.GDD_WERKS_REF WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.mtart = 'HALB'"
                + " AND    a.bklas NOT IN ('3101','3102','7903','7960','7900','7901','7902','7952','7953','7954','7911','7912', '3103', '7904') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Material Type HALB With Invalid Valuation Class";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mat_Type_Roh(string title) //39
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.bwkey, b.mtart, a.bklas, c.aland
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b,"
                + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.matnr = c.matnr "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.bwkey in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.GDD_WERKS_REF WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.mtart = 'ROH'"
                + " AND    a.BKLAS NOT IN ('3000','7951')   "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Material Type ROH With Invalid Valuation Class";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_fert_procF_nospt(string title) //40
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.meins, d.beskz, d.sobsk, e.bklas
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart in ('FERT', 'HAWA') "
                + " AND    d.beskz in ('F', 'X') "
                + " AND    d.sobsk is null"
                + " AND    e.bklas not in ('3100', '3103', '3104') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - FERT/HAWA, Proc Type F/X, Costing SPT empty, Val Class MUST be 3100, 3103 or 3104";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_AC_FC_FERT_HAWA_ValClass_SPT_invalid(string title) //41
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, d.werks, d.beskz, e.bklas, d.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart IN ('FERT', 'HAWA') "
                + " AND    e.bklas IN ('7930', '7936') "
                + " AND    d.beskz IN ('F') "
                + " AND    nvl(d.sobsk,' ') IN ('30','31') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - FERT/HAWA, Proc Type F, Val Class 7930/7936, Costing SPT must not be 30/31";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_AC_FC_FERT_ValClass_SPT_invalid(string title) //42
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, d.werks, d.beskz, e.bklas, d.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'FERT'"
                + " AND    e.bklas IN ('7955', '7956') "
                + " AND    d.beskz IN ('F', 'X') "
                + " AND    NVL(d.sobsk,' ') NOT IN ('30','31') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - FERT, Proc Type F/X, Val Class 7955/7956, Costing SPT must be 30/31";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_halb_procF_nospt(string title) //43
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.meins, d.beskz, d.sobsk, e.bklas
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'HALB'"
                + " AND    d.beskz in ('F', 'X') "
                + " AND    d.sobsk is null"
                + " AND    e.bklas not in ('3101', '3102', '7904', '7903', '7952', '7953', '7954') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - HALB, Proc Type F/X, Costing SPT empty, Val Class MUST be 3101, 3102, 7903, 7904, 7952, 7953 or 7954";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_AC_FC_HALB_SPT_ValClass_invalid(string title) //44
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.meins, d.beskz, d.sobsk, e.bklas
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'HALB'"
                + " AND    d.beskz in ('F', 'X') "
                + " AND    nvl(d.sobsk, ' ') <> ' '"
                + " AND    e.bklas in ('3101', '3102', '7904') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - HALB, Proc Type F/X, Costing SPT populated, Val Class 3101/3102/7904 invalid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_AC_FC_HALB_ValClass_SPT_invalid(string title) //45
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.meins, d.werks, d.beskz, e.bklas, d.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'HALB'"
                + " AND    e.bklas IN ('7952', '7953', '7954') "
                + " AND    d.beskz IN ('F', 'X') "
                + " AND    nvl(d.sobsk,' ') NOT IN ('30','31') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - HALB, Proc Type F/X, Val Class 7952/7953/7954, Costing SPT must be 30/31";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_roh_procF_spt30(string title) //46
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.meins, d.beskz, d.sobsk, e.bklas
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew e"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    d.werks = e.bwkey"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'ROH'"
                + " AND    d.beskz in ('F', 'X') "
                + " AND    decode(e.bklas,' ', 'Q', null, 'Q', e.bklas) = '30'"
                + " AND    e.bklas not in ('7951') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - ROH, Proc Type F/X, Costing SPT 30, Val Class MUST be 7951";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_roh_profit_1799(string title) //47
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.spart, a.matkl, d.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'ROH'"
                + " AND    a.spart = '01'"
                + " AND    a.matkl in ('R20', 'R45') "
                + " AND    d.prctr not like '%1799'"
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - ROH, Material Group R20 or R45, Division 01, Profit Center SHOULD be 1799";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_roh_profit_3199(string title) //48
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.spart, a.matkl, d.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'ROH'"
                + " AND    a.spart = '02'"
                + " AND    a.matkl in ('R20', 'R45') "
                + " AND    d.prctr not like '%3199'"
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - ROH, Material Group R20 or R45, Division 02, Profit Center SHOULD be 3199";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_roh_profit_notbe_1799(string title) //49
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.spart, a.matkl, d.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'ROH'"
                + " AND    a.matkl in ('R15', 'R40') "
                + " AND    (d.prctr like '%3199' OR d.prctr like '%1799') "
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - ROH, Material Group R15 or R40, Profit center SHOULD NOT be 1799 or 3199";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_verp_profit_1810(string title) //50
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.spart, a.matkl, d.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'VERP'"
                + " AND    a.spart = '01'"
                + " AND    a.matkl not in ('B10', 'B35', 'M45', 'M50', 'M52', 'R05', 'M05', 'M10', 'M15', 'M20', 'M25', 'M30', 'M35', 'M40', 'M55') "
                + " AND    d.prctr not like '%1810'"
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - VERP, Division 01, Profit Center SHOULD be 1810";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_verp_profit_3510(string title) //51
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.mtart, a.spart, a.matkl, d.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = d.matnr"
                + " AND    d.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.spras = 'E'"
                + " AND    a.mtart = 'VERP'"
                + " AND    a.spart = '02'"
                + " AND    a.matkl not in ('B10', 'B35', 'M45', 'M50', 'M52', 'R05', 'M05', 'M10', 'M15', 'M20', 'M25', 'M30', 'M35', 'M40', 'M55') "
                + " AND    d.prctr not like '%3510'"
                + " ORDER BY a.matnr, d.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - VERP, Division 02, Profit Center SHOULD be 3510";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_procE_bklas(string title) //52
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, b.beskz, c.bklas, b.sobsk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref e"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.aland = e.aland"
                + " AND   b.werks = e.werks"
                + " AND   NVL(b.beskz, 'Q') = 'E'"
                + " AND   c.bklas not in (7900, 7901, 7902, 7920, 7935) "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - For Mfg Materials, Val Class Should be 7900, 7901, 7902, 7920 or 7935";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_procF_bklas(string title) //53
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, b.beskz, c.bklas, b.sobsk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref e"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.aland = e.aland"
                + " AND   b.werks = e.werks"
                + " AND   NVL(b.beskz, 'Q') = 'F'"
                + " AND   c.bklas not in (3000, 3050, 3100, 3101, 3102, 3104, 7903, 7930, 7936, 7951, 7952, 7953, 7954, 7955, 7956) "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - For Purchased Materials, Val Class Should be 3000, 3050, 3100, 3101, 3102, 3104, 7903, 7930, 7936, 7951, 7952, 7953, 7954, 7955, or 7956";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_nospt_bklas(string title) //54
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, b.beskz, c.bklas, b.sobsk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref e"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.aland = e.aland"
                + " AND   b.werks = e.werks"
                + " AND   NVL(b.sobsk, 'Q') != 'Q'"
                + " AND   c.bklas in (7900, 7901, 7902, 7920, 7935, 7911, 7912, 7940, 3100, 3101, 3102, 3104) "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Costing SPT Should be Empty When Val Class is 7900, 7901, 7902, 7920, 7935, 7911, 7912, 7940, 3100, 3101, 3102, or 3104";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_spt_bklas(string title) //55
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, b.beskz, c.bklas, b.sobsk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref e"
                + " WHERE a.matnr = b.matnr  "
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.aland = e.aland"
                + " AND   b.werks = e.werks"
                + " AND   NVL(b.sobsk, 'Q') in ('Q', '10', '30') "
                + " AND   c.bklas in (7930, 7936, 7903) "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Costing SPT Should not be Empty, 10 or 30 When Val Class is 7930, 7936, or 7903";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_spt30_bklas(string title) //56
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, b.beskz, c.bklas, b.sobsk
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref e"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   b.werks = c.bwkey"
                + " AND   d.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.aland = e.aland"
                + " AND   b.werks = e.werks"
                + " AND   NVL(b.sobsk, 'Q') != '30'"
                + " AND   c.bklas in (7951, 7952, 7953, 7954, 7955, 7956) "
                + " ORDER BY b.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Costing SPT Should be 30 When Val Class is 7951, 7952, 7953, 7954, 7955, or 7956";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_sptnot30_orign_not1(string title) //57
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.sobsk, b.hrkft
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew b"
                + " WHERE  a.sobsk <> '30'"
                + " AND    (b.hrkft <> 'I' or b.hrkft is null) "
                + " AND    a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = b.matnr"
                + " AND    a.werks = b.bwkey"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Costing SPT populated <> 30, Origin Group must be I - Financial Critical";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_spt30_orign_1(string title) //58
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.sobsk, b.hrkft
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mbew b"
                + " WHERE  a.sobsk = '30'"
                + " AND    b.hrkft = 'I'"
                + " AND    a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = b.matnr"
                + " AND    a.werks = b.bwkey");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Origin Group is I, Costing SPT cannot = 30 - Financial Critical";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_prctrNotSameAcrossPlants(string title) //59
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, LISTAGG (c.werks||'/'||to_number(nvl(c.prctr,'0')), '; ') WITHIN GROUP (ORDER BY c.matnr) ""LSTAGG""
				FROM (SELECT DISTINCT c1.matnr, c1.werks,c1.prctr  
        		    FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c1, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c2, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a "
                        + " WHERE a.matnr=c1.matnr "
                        + " AND a.mtart in ('FERT', 'HAWA', 'ZPRM') "
                        + " AND c1.matnr=c2.matnr "
                        + " AND c1.werks<>c2.werks "
                        + " AND to_number(nvl(c1.prctr,'0'))<>to_number(nvl(c2.prctr,'0'))) c "
                + " WHERE nvl(prctr,'0') <> ' '"
                + " AND    c.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                //+ " --AND    c.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " GROUP BY c.matnr"
                + " ORDER BY c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Profit Center not equal across all plants for FERT, HAWA, ZPRM";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_prctrNotSameAcrossMaterialRoot(string title) //60
        {
            try
            {
                string command = String.Format(@"
                SELECT c.shortmatnr, 
       			LISTAGG (c.matnr||': '||c.werks||'/'||to_number(nvl(c.prctr,'0')), '; ') WITHIN GROUP (ORDER BY c.werks) ""LSTAGG""
				    FROM (SELECT DISTINCT c1.matnr, substr(c1.matnr,1,6) as shortmatnr, c1.werks, c1.prctr  
      				    FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c1, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c2"
                          + " WHERE substr(c1.matnr,1,6)=substr(c2.matnr,1,6) "
                          + " AND c1.werks<>c2.werks "
                          + " AND to_number(nvl(c1.prctr,'0'))<>to_number(nvl(c2.prctr,'0')) "
                          + " AND c1.prctr is not null "
                          + " AND c2.prctr is not null"
                          + " AND c1.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                        + " AND c1.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                          + " AND c2.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                            + " AND c2.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                          + " ) c"
                    + " WHERE c.prctr is not null"
                    + " AND    c.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                    + " AND    c.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                    + " GROUP BY c.shortmatnr"
                    + " ORDER BY c.shortmatnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Profit Center not equal across Material 6-digit root, Site Only Plants";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_prctrNotSameAcrossMaterialRootAll(string title) //61
        {
            try
            {
                string command = String.Format(@"
                SELECT c.shortmatnr, 
       			LISTAGG (c.matnr||': '||c.werks||'/'||to_number(nvl(c.prctr,'0')), '; ') WITHIN GROUP (ORDER BY c.werks) ""LSTAGG""
				    FROM (SELECT DISTINCT c1.matnr, substr(c1.matnr,1,6) as shortmatnr, c1.werks, c1.prctr  
      				    FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c1, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c2"
                          + " WHERE substr(c1.matnr,1,6)=substr(c2.matnr,1,6) "
                          + " AND c1.werks<>c2.werks "
                          + " AND to_number(nvl(c1.prctr,'0'))<>to_number(nvl(c2.prctr,'0')) "
                          + " AND c1.prctr is not null "
                          + " AND c2.prctr is not null "
                          + " ) c "
                    + " WHERE c.prctr is not null"
                    + " GROUP BY c.shortmatnr "
                    + " ORDER BY c.shortmatnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Profit Center not equal across Material 6-digit root, All Plants";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_AC_PRCTR_Invalid_ZTMM_ITEM_PROFIT(string title) //62
                                                                                                  /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.mtart, c.werks, c.prctr, to_number(z.prctr) as SAPprctr, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c," + DDRSessionEntity.Current.mapinstance + ".ZTMM_ITEM_PROFIT@" + DDRSessionEntity.Current.mapinstance + " z"
                + " WHERE  a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ")  "
                + " AND    RPAD(a.matnr,6)=z.item"
                + " AND    a.matnr=c.matnr"
                + " AND    a.matnr=b.matnr"
                + " AND    b.spras='E'"
                + " AND    z.mandt='100'"
                + " AND    nvl(c.prctr,'0') <> to_number(z.prctr) "
                + " AND    c.prctr not in (select to_number(prctr) from " + DDRSessionEntity.Current.mapinstance + ".ZTMM_ITEM_PC_EXP@" + DDRSessionEntity.Current.mapinstance + " where mandt='100' and prctr<>' ') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Profit Center is not correct for this material (ZTMM_ITEM_PROFIT) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_AC_NCOST_required(string title) //63
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.werks, c.mmsta, c.ncost
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE c.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.mmsta IN ('H9', 'L9', 'M9', 'Z2', 'Z4','Z9') "
                + " AND   NVL(c.ncost, ' ') <> 'X'"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Do Not Cost Flag must be checked if Status is H9, L9, M9, Z2, Z4, or Z9";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mpr_Hb(string title) //65
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, werks, dismm, mabst
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.dismm = 'HB'"
                + " AND    a.mabst is null"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP Type Is HB And Max Stock Level Is Not Populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mpr_Vb(string title) //66
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, werks, dismm, minbe
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.dismm = 'VB'"
                + " AND    a.minbe is null"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP Type Is VB And Reorder Point Is Not Populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Mpr_Z1(string title) //67
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, werks, dismm, minbe
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.dismm = 'Z1'"
                + " AND    a.minbe is null"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP Type Is Z1 And Reorder Point Is Not Populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_ReorderPoint_Populated(string title) //68
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, werks, dismm, minbe
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.dismm = 'X0'"
                + " AND    nvl(MINBE,'0') >0"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Reorder Point should not be populated when MRP type is X0";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_mpr_type_planning(string title) //69
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, dismm, fxhor
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc "
                + " WHERE  MATNR IN (SELECT MATNR FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_MATNR_REF"
                    + " WHERE ALAND = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    WERKS IN (SELECT WERKS FROM " + DDRSessionEntity.Current.table_schema + "_owner.GDD_WERKS_REF"
                    + " WHERE ALAND = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    DISMM = 'P1'"
                + " AND    (FXHOR IS NULL or FXHOR = 0) "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP Type is P1 and Planning Time Fence is empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Dismm_Othersbad(string title) //70
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.dismm, a.dispo, a.disls, a.beskz, a.mrppp, a.fhori, a.mtvfp
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b," + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.werks = b.werks"
                + " AND    b.aland = c.aland"
                + " AND    dismm is not null"
                + " AND    (dispo is null or disls is null or beskz is null"
                    + " or mrppp is null or fhori is null or mtvfp is null) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP Type Populated, Other Required MRP Fields Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Dispo_Othersbad(string title) //71
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.dismm, a.dispo, a.disls, a.beskz, a.mrppp, a.fhori, a.mtvfp
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = c.matnr"
                + " AND    b.aland = c.aland"
                + " AND    dispo is not null"
                + " AND    (dismm is null or disls is null or beskz is null"
                    + " or mrppp is null or fhori is null or mtvfp is null) "
                + " AND    a.werks = b.werks"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP Controller Populated, Other Required MRP Fields Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Beskz_Othersbad(string title) //72
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.dismm, a.dispo, a.disls, a.beskz, a.mrppp, a.fhori, a.mtvfp
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.beskz is not null"
                + " AND    (a.dismm is null or a.dispo is null or a.disls is null"
                    + " or a.mrppp is null or a.fhori is null or a.mtvfp is null) "
                + " AND    a.werks = b.werks"
                + " AND    a.matnr = c.matnr"
                + " AND    b.aland = c.aland"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Procurement Type Populated, Other Required MRP Fields Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_Missing_Required_Fields(string title) //73
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.dismm, a.dispo, a.disls, a.beskz, a.mrppp, a.fhori, a.mtvfp
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = c.matnr "
                + " AND    b.aland = c.aland"
                    + " AND  (dismm IS NOT NULL OR dispo IS NOT NULL OR disls IS NOT NULL  OR beskz IS NOT NULL OR mrppp IS NOT NULL OR fhori IS NOT NULL) "
                    + " AND  (dismm IS NULL OR dispo IS NULL OR disls IS NULL  OR beskz IS NULL OR mrppp IS  NULL OR fhori IS NULL OR mtvfp IS NULL) "
                + " AND    a.werks = b.werks"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - MRP missing required fields";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Procrument_Nt_E_F_X(string title) //74
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.beskz <> ' '"
                + " AND    a.beskz not in ('E', 'F', 'X') "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Procurement Type Populated, But It Is Not E,F Or X";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sobsl30_Sobsk30(string title) //76
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, a.sobsl, a.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.sobsl = '30'"
                + " AND    NVL(a.sobsk, 'XX') != '30'"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - MRP Special Proc = 30, Costing 1 SP. Procurement Type Not = 30";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sobsl30_Beskz(string title) //77
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, a.sobsl, a.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.sobsl = '30'"
                + " AND    NVL(a.beskz, 'Q') not in ('F', 'X') "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - MRP Special Proc = 30, Procurement Type Not F Or X";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sobsl30_No_Bom(string title) //78
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, a.sobsl, a.sobsk
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.sobsl = '30'"
                + " AND    a.matnr not IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader"
                    + " WHERE werks = a.werks) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - MRP Special Proc = 30, Bom Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Beskzf_Bad_Losgr(string title) //79
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, c.werks, c.beskz, c.losgr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  c.matnr = a.matnr"
                + " AND    c.matnr = b.matnr"
                + " AND    a.mtart NOT IN ('ZUNB', 'ZGCM', 'ZPRM') "
                + " AND    c.beskz = 'F'"
                + " AND    nvl(c.losgr, 3) not in (1, 1000) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    c.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = F, Costing Lot Size Must Be 1 Or 1000";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Beskz_F_No_Lgfsb(string title) //80
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, lgfsb
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  beskz = 'F'"
                + " AND    lgfsb is null"
                + " AND    a.matnr = b.matnr"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = F, Storage Loc For EP Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Beskz_E_No_Lgpro(string title) //81
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, lgpro
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  beskz = 'E'"
                + " AND    lgpro is null"
                + " AND    a.werks = b.werks"
                + " AND    a.matnr = c.matnr"
                + " AND    b.aland = c.aland"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = E, Issue Storage Location Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Proc_E_No_Bom(string title) //82
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.beskz, a.mmsta
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.beskz = 'E' "
                    + " AND    a.matnr NOT IN (SELECT matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader WHERE werks = a.werks) "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    NVL(a.MMSTA,' ') <>'M9'"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = E, Without BOMS";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_BESKZ_E_AvailCheck_Not_Z1(string title) //83
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, beskz, mtvfp
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE  matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    beskz = 'E' "
                + " AND    mtvfp <> 'Z1' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = E, Availability Check must be Z1";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_BESKZ_E_WZEIT_Required(string title) //84
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, beskz, wzeit
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE  matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    beskz = 'E' "
                + " AND NVL(wzeit,'0') = '0' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = E, Replenishment Lead Time required";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_BESKZ_E_PLIFZ_WEBAZ_Required(string title) //85
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, beskz, plifz, webaz
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE  matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    beskz = 'E' "
                + " AND    mtvfp = 'Z1'"
                + " AND   (NVL(plifz,'0')='0' OR  NVL( webaz,'0')='0') "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = E  and Avail Check = Z1, Planned Delivery Time and GR Processing Time required";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_BESKZ_F_STPC_30_AvailCheck_Not_Z1(string title) //86
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, beskz, sobsk, mtvfp
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE  matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    beskz = 'F'"
                + " AND    sobsk = '30' "
                + " AND    mtvfp <> 'Z1' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Procurement Type = F with STPC 30, Availability Check must be Z1";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Lgpro_No_Mard(string title) //87
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.lgpro
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr    "
                + " AND    a.lgpro is not null"
                + " AND    a.matnr||a.werks||a.lgpro not in (select matnr||werks||lgort"
                    + " from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard) "
                + " AND    werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Issue Storage Location(Lgpro) Without Storage (Lgort) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Lgfsb_No_Mard(string title) //88
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.lgfsb
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.lgfsb is not null"
                + " AND    a.matnr||a.werks||a.lgfsb not in (select matnr||werks||lgort"
                    + " from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard) "
                + " AND    a.werks in (SELECT werks"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Storage Loc For EP(Lgfsb) Without Storage (Lgort) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_check_mrp_lt_siz(string title) //89
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.disls, a.bstmi, a.bstma, a.bstfe
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c"
                + " WHERE a.matnr = b.matnr"
                    + " AND a.werks = c.werks"
                    + " AND b.aland = c.aland"
                    + " AND a.disls = 'FX'"
                    + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                    + " AND (NVL(a.bstmi, 0) != 0"
                    + " OR NVL(a.bstma, 0) != 0 ) "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Lot Size FX, Min Lot Size and Max Lot Size should be empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_check_mrp_lt_siz2(string title) //90
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.disls, a.bstfe
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.disls = 'EX'"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr = b.matnr"
                + " AND    a.bstfe <> 0"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Lot Size EX, Fixed Lot Size should be empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public System.Data.DataTable SQL_MM_audit_MRP_DISLS_invalid(string title) //91
                                                                                  /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.disls, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   nvl(a.disls,' ')<>' '"
                + " AND   disls NOT IN (SELECT disls FROM " + DDRSessionEntity.Current.mapinstance + ".T439t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Lot Size is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_MinMax_LotSize(string title) //92
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.disls, a.bstmi, a.bstma, a.bstfe
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = c.werks"
                + " AND   b.aland = c.aland"
                + " AND   nvl(a.BSTMI,'0') > nvl(a.BSTMA,'0') "
                + " AND   nvl(a.BSTMA,'0')<>'0' "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Minimum Lot Size may not be greater than Maximum Lot Size";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_MaxLotSize_LessThan_RoundingValue(string title) //93
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.disls, a.bstmi, a.bstma, a.bstrf
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = c.werks"
                + " AND   b.aland = c.aland"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   nvl(a.BSTRF,'0') > nvl(a.BSTMA,'0') "
                + " AND   nvl(a.BSTMA,'0') <>'0'"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Maximum lot size may not be smaller than Rounding Value";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_SHFLG_Invalid(string title) //94
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.SHFLG
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = c.werks"
                + " AND   b.aland = c.aland"
                + " AND   a.SHFLG NOT IN (' ', '1','2') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Safety Time Indicator is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_SHFLG_missing_SHZET(string title) //95
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, shflg, shzet
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE  matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    shflg IS NOT NULL "
                + " AND    shzet IS NULL"
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Safety time indicator populated, Safety Time missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_MAABC_Invalid(string title) //96
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.MAABC
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = c.werks"
                + " AND   b.aland = c.aland"
                + " AND   nvl(a.MAABC, ' ') NOT IN (' ', 'A', 'B','C') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - ABC Indicator is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_BSTRF_LotSize_FX(string title) //97
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.DISLS, a.BSTRF 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = c.werks"
                + " AND   b.aland = c.aland"
                + " AND   nvl(a.bstrf,'0')<>'0' "
                + " AND   a.DISLS='FX'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Rounding Value should not be populated when Lot Size is FX";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRP_SOBSL_invalid(string title) //98
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, sobsl
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE  matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    NVL(sobsl, ' ' ) NOT IN (' ', '30', '31', '80', 'Z1') "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - Special Proc Type must be Blank, 30, 31, 80, or Z1";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRPAREA_SOBSL_Invalid(string title) //99
                                                                                      /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.berid, a.werks, a.sobsl, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mdma a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   nvl(a.sobsl,' ')<>' '"
                + " AND   a.werks||a.sobsl NOT IN (SELECT werks||sobsl  FROM " + DDRSessionEntity.Current.mapinstance + ".T460A@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " ORDER BY a.matnr, a.berid");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Special Procurement Key is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MRPAREA_MRPPP_Invalid(string title) //100
                                                                                      /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.berid, a.werks, a.MRPPP, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mdma a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   nvl(a.MRPPP,' ')<>' '"
                + " AND   a.werks||a.MRPPP NOT IN (SELECT werks||MRPPP  FROM " + DDRSessionEntity.Current.mapinstance + ".T439H@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " ORDER BY a.matnr, a.berid");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Planning Calandar is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Purch_Valkey_Req(string title) //101
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.mtart, b.beskz, a.ekwsl, b.ekgrp
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND a.matnr = c.matnr"
                    + " AND b.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                    + " AND c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                    + " AND b.ekgrp is not null"
                    + " AND nvl(a.ekwsl, 'P') != '1'"
                    + " ORDER by a.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Purchasing Value Key Required When Purchasing Group Populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Qmpur_No_Ssqss(string title) //102
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.qmpur, b.ssqss, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref d"
                + " WHERE a.matnr = b.matnr"
                + " AND   b.werks = c.werks"
                + " AND   a.matnr = d.matnr"
                + " AND   c.aland = d.aland"
                + " AND   a.qmpur = 'X'"
                + " AND   b.ssqss is null"
                + " AND   c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, b.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - QM In Proc Active But Control Key Empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Qmat_No_Qmview(string title) //103
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.werks
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.werks = b.werks"
                + " AND   b.matnr = c.matnr"
                + " AND   (ssqss is null or qmpur is null) "
                + " AND   a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Inspection Type Records Without MM QM View";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_no_ssqss(string title) //104
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, a.mtart, c.maktx, a.qmpur, d.ssqss, d.qmata, d.kzdkz, d.beskz
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,   "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   d.ssqss is null"
                + " AND   a.matnr not like 'HUM%'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Error - QM Control Key must be populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_no_kzdkz(string title) //105
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, a.mtart, c.maktx, d.ssqss, d.kzdkz
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   d.ssqss is not null"
                + " AND   nvl(d.kzdkz, 'Q') != 'X'"
                + " AND   a.mtart != 'ZUNB'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Error - Documentation Required must be populated (excludes ZUNB) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_no_qmata(string title) //106
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, a.mtart, c.maktx, d.ssqss, d.qmata
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   d.ssqss is not null"
                + " AND   nvl(d.qmata, 'Q') = 'Q'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Error - QM Material Auth must be populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_FT_STAWN_CommodityCode_NotInSAP(string title) //107
                                                                                                ///SAP Access Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.stawn, b.land1, a.werks, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.mapinstance + ".t001w@" + DDRSessionEntity.Current.mapinstance + " b"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.werks = b.werks"
                + " AND   a.stawn is not null"
                + " AND   a.stawn||b.land1 NOT IN (SELECT stawn||land1 FROM " + DDRSessionEntity.Current.mapinstance + ".t604@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " ORDER BY a.stawn, b.land1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Commodity Code not setup in SAP(T604) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_FT_STAWN_CommodityCode_MissingMARM(string title) //108
                                                                                                   /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct  a.matnr, a.werks, a.stawn, b.land1, c.bemeh, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.mapinstance + ".t001w@" + DDRSessionEntity.Current.mapinstance + " b, " + DDRSessionEntity.Current.mapinstance + ".t604@" + DDRSessionEntity.Current.mapinstance + " c"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.werks = b.werks "
                + " AND   a.stawn = c.stawn "
                + " AND   b.land1 = c.land1 "
                + " AND   b.mandt = c.mandt"
                + " AND   a.stawn is not null"
                + " AND   c.bemeh<>' '"
                + " AND   a.matnr||c.bemeh NOT IN (SELECT matnr||meinh FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm) "
                + " AND   b.mandt='100'"
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Commodity Code missing Alt UoM record";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_FT_IncorrectData_US_ExportingPlants(string title) //109
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.werks, v.vkorg, v.vtweg, c.stawn, c.HERKL, c.HERKR 
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke v"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    v.vkorg  IN (SELECT vkorg FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.matnr=v.matnr"
                + " AND    v.vkorg||v.vtweg IN ('110002','110102','110202', '170602','170603','118402','118403') "
                + " AND    nvl(c.STAWN,' ')=' '"
                + " ORDER BY c.matnr, c.werks, v.vkorg, v.vtweg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Foreign Trade fields should be populated for US exporting plants. Sales Orgs 1100, 1101, 1102, 1184, 1706";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_FT_STAWN_NotValid_ZTCOMCOD(string title) //110
                                                                                           /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                select distinct c.matnr, RPAD(c.matnr,6) as matnr6, c.werks, c.stawn, z.ZZSTAWN, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".ZTCOMCOD@" + DDRSessionEntity.Current.mapinstance + " z, " + DDRSessionEntity.Current.mapinstance + ".t001w@" + DDRSessionEntity.Current.mapinstance + " tz"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    RPAD(c.matnr,6)=z.zzmatnr(+) "
                + " AND    c.stawn is not null"
                + " AND    c.STAWN <> z.ZZSTAWN"
                + " AND    c.werks = tz.werks and tz.land1 in ('US','PR') "
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Commodity Code not setup in SAP(ZTCOMCOD) for 6-digit material";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_FT_STAWN_NotSameAcrossMaterialRoot(string title)  //111
        {
            try
            {
                string command = String.Format(@"
                SELECT c.shortmatnr, c.werks, LISTAGG (c.matnr||': '||nvl(c.stawn,' '), ';  ') WITHIN GROUP (ORDER BY c.werks) ""LSTAGG""
                FROM (SELECT DISTINCT c1.matnr, substr(c1.matnr,1,6) as shortmatnr, c1.werks, c1.stawn "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c1, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c2"
                    + " WHERE c1.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                    + " AND   c1.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                    + " AND substr(c1.matnr,1,6)=substr(c2.matnr,1,6) "
                    + " AND c1.werks=c2.werks "
                    + " AND nvl(c1.stawn,' ')<> nvl(c2.stawn,' ') "
                    + " AND NVL(c1.stawn,' ')<> ' '"
                    + " AND NVL(c2.stawn,' ')<> ' '"
                    + " ) c"
                + " WHERE c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.stawn is not null"
                + " GROUP BY c.shortmatnr, c.werks"
                + " ORDER BY c.shortmatnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Commodity Code not equal across Material 6-digit root";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_FT_Legal_EMBGR_invalid(string title) //112
                                                                                       /// SAP ACCESS REQUIRED ///
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, aland, gegru, alnum, embgr, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_maex"
                + " WHERE matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   embgr NOT IN (SELECT embgr FROM " + DDRSessionEntity.Current.mapinstance + ".T606I@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " ORDER BY matnr,aland, gegru");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Grouping for Legal Control is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sales_Notax(string title) //113
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct matnr, vkorg, vtweg
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke"
                + " WHERE vkorg in (select vkorg from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales where werks in ('" + DDRSessionEntity.Current.plantcode + "')) "
                + " AND   matnr not in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan  where  aland = '" + DDRSessionEntity.Current.ISOCode + "') "
                + " ORDER BY matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Sales Records Without Matching Tax Records";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_SALES_Missing_Tax(string title) //114
        {
            try
            {
                string command = String.Format(@"
                SELECT v.matnr, v.vkorg, v.vtweg, r.tax_aland 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke v, " + DDRSessionEntity.Current.table_schema + "_procs.GDD_SCM_TAX_REF r "
                + " WHERE v.vkorg IN (SELECT vkorg FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND   v.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref   WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   v.vkorg=r.vkorg "
                + " AND   v.vtweg=r.vtweg"
                + " minus"
                + " SELECT v.matnr,v.vkorg,v.vtweg,t.aland "
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke v, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan t"
                + " WHERE t.matnr=v.matnr"
                + " ORDER BY 1,2,3,4");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Tax record missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Sales_Missing_Required(string title) //115
        {
            try
            {
                string command = String.Format(@"
                SELECT a.MATNR, a.MTART, a.TRAGR, a.GEWEI, v.VKORG, v.VTWEG, v.DWERK, v.VERSG, v.KTGRM, v.VMSTA, v.SKTOF
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke v"
                + " WHERE  v.matnr = a.matnr "
                + " AND    a.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref   WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    v.vkorg in (SELECT vkorg FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND   (NVL(v.DWERK,'XX') = 'XX' OR"
                    + " NVL(v.VERSG,'XX') = 'XX' OR"
                    + " NVL(v.KTGRM,'XX') = 'XX' OR"
                    + " NVL(v.VMSTA,'XX') = 'XX' OR"
                    + " NVL(v.SKTOF,'XX') = 'XX' OR"
                    + " NVL(a.TRAGR,'XX') = 'XX' OR"
                    + " NVL(a.GEWEI,'XX') = 'XX') "
                + " ORDER BY a.matnr, v.VKORG, v.VTWEG");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Sales Records With Missing Required Fields";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Profitmarm(string title) //116
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.prctr
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales d,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara e"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    a.matnr = e.matnr"
                + " AND    a.werks = d.werks"
                + " AND    c.vkorg = d.vkorg"
                + " AND    b.aland = d.aland"
                + " AND    e.mtart != 'DIEN'"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.prctr not in ('0315', '315', '0570', '570', '1511',"
                    + " '3179', '3150', '0799', '799', '3199',"
                    + " '0599', '599', '1799', '1855', '1840', "
                    + " '1871', '1869', '1850', '1910', '1870', "
                    + " '1920', '1900', '1865', '1939', '1930') "
                + " AND    a.matnr not in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm "
                    + " where meinh in ('MU', 'CM', 'LCC', 'LCG', 'LCK', 'LCM') "
                    + " ) "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Sales History - Profit Center W/O Sales Alt Uom (Lc*, Mu, Cm) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Vrkme_No_Marm(string title) //117
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, vkorg, vtweg, vrkme
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke"
                + " WHERE vrkme is not null"
                + " AND matnr||vrkme not in (SELECT matnr||meinh"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm) "
                + " AND matnr||vrkme not in (SELECT matnr||wsmei"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2) "
                + " AND vkorg in (SELECT vkorg"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY matnr, vkorg, vtweg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Sales Unit (Vrkme) Without Alt Uom Conversion";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Tax_Nosales(string title) //118
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.aland, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.aland = '" + DDRSessionEntity.Current.ISOCode + "'"
                + " AND   b.aland in '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.matnr NOT in (SELECT matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke"
                    + " WHERE  vkorg in (select vkorg from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales"
                        + " where werks in (SELECT werks"
                            + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                                + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                            + " ) "
                        + " ) "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Tax Records Without Matching Sales Record";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Salesuom_Matches_Base(string title) //119
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.vkorg, a.vtweg, a.vrkme, c.meins
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.matnr = c.matnr"
                + " AND    a.vrkme = c.meins"
                + " AND    a.vkorg in (select vkorg from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.vkorg, a.vtweg");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Sales Unit Cannot match Base UoM";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_SALES_Tax_US_Missing(string title) //120
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, aland, sapid, taxm1, taxm2 
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlan "
                + " WHERE  matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " and load='X') "
                + " AND    matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke) "
                + " AND    aland='US' "
                + " AND (nvl(taxm1,' ')=' ' or nvl(taxm2, ' ')=' ') "
                + " ORDER BY matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - US Tax needs both TAXM1 and TAXM2 populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_SALES_GEWEI_Missing(string title) //121
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.meins, v.vkorg, v.vtweg, a.tragr, a.brgew, a.ntgew, a.gewei 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke v, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   v.matnr(+)=a.matnr "
                + " AND   ((nvl(a.gewei, ' ') = ' ' AND v.vkorg IS NOT NULL) "
                    + " OR  (nvl(a.gewei, ' ') = ' ' AND nvl(a.brgew,'0')<>'0') "
                    + " OR  (nvl(a.gewei, ' ') = ' ' AND nvl(a.ntgew,'0')<>'0') "
                    + " OR  (nvl(a.gewei, ' ') = ' ' AND a.tragr IS NOT NULL)) "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Weight Unit must be populated if there is a Sales record";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_SALES_LADGR_invalid(string title) //122
                                                                                    /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, ladgr, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   nvl(ladgr,' ')<>' '"
                + " AND   ladgr NOT IN (SELECT ladgr FROM " + DDRSessionEntity.Current.mapinstance + ".TLGRT@" + DDRSessionEntity.Current.mapinstance + " WHERE spras='E' AND mandt='100') "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Loading Group is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_SALES_MTVFP_invalid_missing(string title) //123
                                                                                            /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, mtvfp, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc"
                + " WHERE matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND  (nvl(mtvfp, ' ') = ' '"
                    + " OR mtvfp NOT IN (SELECT mtvfp FROM " + DDRSessionEntity.Current.mapinstance + ".TMVFT@" + DDRSessionEntity.Current.mapinstance + " WHERE spras='E' AND mandt='100')) "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Availability Check is missing or is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Ausp(string title) //124
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr not in (select distinct objek from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart != 'DIEN'"
                + " AND   a.matnr not like 'ZQM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Classification Does Not Exist";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Class911(string title) //125
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.matnr not in (select distinct objek from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " where atinn = 911) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart != 'DIEN'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical -  Item Family is required";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Class912(string title) //126
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.matnr not in (select distinct objek from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " where atinn = 912) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart != 'DIEN'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical - Item Number is required";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Class952(string title) //127
        {
            try
            {
                string command = String.Format(@"   
                SELECT a.matnr, c.maktx, a.mtart, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr"
                + " AND    c.spras = 'E'"
                + " AND    a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " WHERE atinn = 952) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.mtart not in ('DIEN', 'ZUNB') "
                + " AND    a.matnr not like 'ZQM%'"
                + " AND    a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Critical - Expiration Format Missing - Required for all except ZUNB";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Class954(string title) //128
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE  a.matnr = b.matnr "
                + " AND    a.matnr = c.matnr"
                + " AND    c.spras = 'E'"
                + " AND    a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " WHERE atinn = 954) "
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.mtart not in ('DIEN', 'ZUNB') "
                + " AND    a.matnr not like 'ZQM%'"
                + " AND    a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Critical - Manufacture Date Format Missing - Required for all except ZUNB";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_prfrq_no_953(string title) //129
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, a.mtart, a.matkl, c.maktx, d.ssqss, d.prfrq
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   nvl(d.prfrq, 0) > 0"
                + " AND   a.mtart in ('ROH', 'HALB', 'ZGCM') "
                + " AND   (a.matnr not in (SELECT objek from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 953) "
                    + " OR "
                    + " a.matnr in (SELECT objek from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp where atinn = 953 and atwrt = '0') )  "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Critical - Subsequent Insp Interval must be populated when Insp Interval Populated (ROH,HALB,ZGCM) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_errors_no_class914(string title) //130
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " WHERE atinn = 914) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart in ('FERT', 'HALB', 'ROH') "
                + " AND   length(a.matnr) > 11"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical - Subselling Market required for FERT, HALB, ROH length > 11";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Bad_Itm_Fam(string title) //131
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, substr(a.matnr,1,2) AS FIRST_TWO, b.atwrt, b.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn c"
                + " WHERE a.matnr = b.objek"
                + " AND   b.atinn = c.atinn"
                + " AND   nvl(a.mtart, 'XXXX') not in ('DIEN', 'PROD', 'ZUNB') "
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   c.atnam = 'Z_PRESENTATION'"
                + " AND   b.atwrt != substr(a.matnr,1,2) "
                + " AND   a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Item Family does not match first two positions of Material Num";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Bad_Itm_Num(string title) //132
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, substr(a.matnr,3,4) AS FOUR_POS, b.atwrt, b.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn c"
                + " WHERE a.matnr = b.objek"
                + " AND   b.atinn = c.atinn"
                + " AND   nvl(a.mtart, 'XXXX') not in ('DIEN', 'PROD', 'ZUNB') "
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   c.atnam = 'Z_PRODUCT_NUMBER'"
                + " AND   b.atwrt != substr(a.matnr,3,4) "
                + " AND   a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Item Number does not match positions 3-6 of Material Num";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Bad_Pack_Code(string title) //133
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, substr(a.matnr,7,3) AS PACK_CODE, b.atwrt, b.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn c"
                + " WHERE a.matnr = b.objek"
                + " AND   b.atinn = c.atinn"
                + " AND   nvl(a.mtart, 'XXXX') not in ('DIEN', 'PROD', 'ZUNB') "
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   c.atnam = 'Z_PACKAGE_SIZE'"
                + " AND   b.atwrt != substr(a.matnr,7,3) "
                + " AND   a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Pack Code does not match positions 7-9 of Material Num";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Bad_Label_Code(string title) //134
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, substr(a.matnr,10,2) AS LABEL_CODE, b.atwrt, b.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn c"
                + " WHERE a.matnr = b.objek"
                + " AND   b.atinn = c.atinn"
                + " AND   nvl(a.mtart, 'XXXX') not in ('DIEN', 'PROD', 'ZUNB') "
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   c.atnam = 'Z_LABEL_CODE'"
                + " AND   b.atwrt != substr(a.matnr,10,2) "
                + " AND   a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Label Code does not match positions 10-11 of Material Num";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_bad_subsell(string title) //135
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.maktx, substr(a.matnr,10,4) AS SUBSELL, b.atwrt, b.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt d"
                + " WHERE a.matnr = b.objek "
                + " AND   b.atinn = c.atinn"
                + " AND   a.matnr = d.matnr"
                + " AND   d.spras = 'E'"
                + " AND   nvl(a.mtart, 'XXXX') in ('FERT', 'HALB', 'ROH') "
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   c.atnam = 'Z_SUBSELLING_MARKET'"
                + " AND   b.atwrt != substr(a.matnr,10,4) "
                + " AND   length(a.matnr) > 11"
                + " AND   a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical - Subselling Market must match positions 10-13 of Material";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_no_class898(string title) //136
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " WHERE atinn = 898) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.mtart in ('FERT') "
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'UC%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical - Brand Name Required for FERTs (family not UC) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_no_class909(string title) //137
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.matkl, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " WHERE atinn = 909) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.matkl in ('R10','R25') "
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " AND   a.matnr not like 'Q%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical - Common Name Required for Material Groups R10 and R25";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_no_class913(string title) //138
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.matkl, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = 913) "
                + " AND   a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp WHERE atinn = '1152' and atwrt='202768') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.matkl in ('R10','R25', 'R15') "
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Error - API Required for Material Groups R10 and R25";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_no_class1152(string title) //139
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.mtart, a.matkl, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr not in (SELECT distinct objek FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp"
                    + " WHERE atinn = 1152) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   a.matkl in ('R10','R15') "
                + " AND   c.spras = 'E'"
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Critical - Strength Active Component Required for Material Groups R10 and R15";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_no_class1152_R05(string title) //140
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.maktx, a.mtart, a.matkl, a.sapid, c.atwrt, c.sapid as ausp_sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt d"
                + " WHERE a.matnr = b.matnr "
                + " AND   a.matnr = d.matnr"
                + " AND   a.matnr = c.objek"
                + " AND   c.atinn = 1152"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   d.spras = 'E'  "
                + " AND   a.matkl in ('R05') "
                + " AND   a.matnr not like 'ZQM%'"
                + " AND   a.matnr not like 'HUM%'"
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Global Error - Strength Active Component Should be Blank for Material Group R05";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Class_Mult_Chars(string title) //141
        {
            try
            {
                string command = String.Format(@"
                SELECT a.objek, a.atinn,d.atnam,count(*) as Count
			    FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn d, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ksml l, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_klah h "
                + " WHERE  a.atinn = d.atinn "
                + " AND d.atein = 'X'"
                + " AND d.atinn = l.imerk"
                + " AND l.clint = h.clint "
                + " AND h.class = 'Z_GLOBAL'"
                + " GROUP BY a.objek, a.atinn,d.atnam HAVING count(*) > 1 ORDER BY count(*) desc, a.objek");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Multiple Values Exist For Same Characteristic";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_RecycleInd245(string title) //142
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.sapid, b.atwrt, b.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp b"
                + " WHERE a.matnr in (SELECT distinct matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND a.matnr = b.objek"
                + " AND b.atinn = '1141'"
                + " AND b.atwrt in ('01', '03', '04') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Bulk Managed Materials - Recycle indicator = 1, 3, or 4.";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_CLASS_Z_GLOBAL_value_invalid(string title) //143
        {
            try
            {
                string command = String.Format(@"
                        SELECT 'Z_PRESENTATION' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + ", LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_PRESENTATION') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_PRESENTATION' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"

                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_PACKAGE_SIZE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_PACKAGE_SIZE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_PACKAGE_SIZE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_LABEL_CODE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_LABEL_CODE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_LABEL_CODE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_SUBSELLING_MARKET' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_SUBSELLING_MARKET') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_SUBSELLING_MARKET' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_MOLECULE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_MOLECULE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_MOLECULE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_SUBMOLECULE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_SUBMOLECULE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_SUBMOLECULE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_BRAND_NAME' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_BRAND_NAME') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_BRAND_NAME' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_STRENGTH_COMPONENT' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_STRENGTH_COMPONENT') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_STRENGTH_COMPONENT' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_DOSE_FORM' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_DOSE_FORM') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_DOSE_FORM' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_GALENIC_FORM' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_GALENIC_FORM') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_GALENIC_FORM' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_VARIABLE_POTENCY' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_VARIABLE_POTENCY') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_VARIABLE_POTENCY' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_INIT_RETEST_TRIGGER' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_INIT_RETEST_TRIGGER') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_INIT_RETEST_TRIGGER' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_ANIMAL_SOURCE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_ANIMAL_SOURCE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_ANIMAL_SOURCE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_BSE_FREE_COUNTRY_SOURCE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_BSE_FREE_COUNTRY_SOURCE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_BSE_FREE_COUNTRY_SOURCE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_PACK_TYPE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_PACK_TYPE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_PACK_TYPE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_PACK_FORMAT' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_PACK_FORMAT') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                             + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_PACK_FORMAT' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_BULK_RECYCLE_INDICATOR' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_BULK_RECYCLE_INDICATOR') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_BULK_RECYCLE_INDICATOR' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_PRINTED' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_PRINTED') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_PRINTED' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_MATERIAL_USE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_MATERIAL_USE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_MATERIAL_USE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_LLY_SPECIAL_SECURITY_SUBSTNC' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_LLY_SPECIAL_SECURITY_SUBSTNC') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_LLY_SPECIAL_SECURITY_SUBSTNC' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_CONTRACT_MFG_ORDER_TYPE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_CONTRACT_MFG_ORDER_TYPE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_CONTRACT_MFG_ORDER_TYPE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_STO_TMP_CONDITION_REGISTERED' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_STO_TMP_CONDITION_REGISTERED') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_STO_TMP_CONDITION_REGISTERED' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt"
                        + " UNION"
                        + " SELECT 'Z_EXP_DATE_POTENCY' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_EXP_DATE_POTENCY') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_EXP_DATE_POTENCY' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt "
                        + " UNION"
                        + " SELECT 'Z_ACTIVITY_FOR_NU' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_ACTIVITY_FOR_NU') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_ACTIVITY_FOR_NU' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt          "
                        + " UNION"
                        + " SELECT 'Z_RES_SAMP_DISC_RULE' as chars, a.atinn, a.atwrt, '" + DDRSessionEntity.Current.mapinstance + "' as SAP" + " , LISTAGG(a.objek, '; ') WITHIN GROUP (ORDER BY a.atwrt) as Materials"
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp a"
                        + " WHERE a.atinn IN     (SELECT atinn FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_cabn WHERE atnam='Z_RES_SAMP_DISC_RULE') "
                        + " AND   a.atwrt NOT IN (SELECT atwrt FROM " + DDRSessionEntity.Current.mapinstance + ".cawn@" + DDRSessionEntity.Current.mapinstance + " z "
                                              + " WHERE z.atinn IN (SELECT atinn FROM " + DDRSessionEntity.Current.mapinstance + ".cabn@" + DDRSessionEntity.Current.mapinstance + " WHERE atnam='Z_RES_SAMP_DISC_RULE' AND mandt='100')) "
                        + " AND   a.objek IN     (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load= 'X') "
                        + " AND   a.sapid <> 'X'"
                        + " GROUP BY a.atinn,a.atwrt   "
                        + " order by 1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Z_GLOBAL Classification value not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Has_Batch(string title) //145
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.xchpf = 'X'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Batch Management";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_No_Batch(string title) //146
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   nvl(a.xchpf, 'Z') != 'X'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Batch Management Not Checked";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_No_Batch_Expires(string title) //147
        {
            try
            {
                string command = String.Format(@"SELECT a.matnr, c.maktx, a.xchpf, a.mhdrz
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   nvl(a.xchpf, 'Z') != 'X'"
                + " AND   nvl(a.mhdrz, 0) = 1"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Batch Management Not Checked, but Material Expires";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_No_Batch_Tsl(string title) //148
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.maktx, a.xchpf, a.mhdhb
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   nvl(a.xchpf, 'Z') != 'X'"
                + " AND   nvl(a.mhdhb, 0) > 0"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Batch Management Not Checked, but Material has TSL";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_No_Batch_Prfrq(string title)  //149
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, c.maktx, a.xchpf, d.prfrq
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   nvl(a.xchpf, 'Z') != 'X'"
                + " AND   nvl(d.prfrq, 0) > 0"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Batch Management Not Checked, but Material ReEvaluates";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_No_Wm_Sloc100(string title) //150
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.lgort
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = b.werks"
                + " AND    a.matnr = c.matnr"
                + " AND    a.lgort = '0100'"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.matnr not in (SELECT d.matnr"
                    + " from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn d, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc e"
                    + " WHERE d.matnr = e.matnr"
                    + " AND   d.lgnum IN (SELECT lgnum from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref"
                        + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                    + " AND   e.werks in (SELECT werks "
                        + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                        + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                        + " ) "
                + " AND    b.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Sloc 0100 Without WM Data";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Ausme_No_Marm(string title) //151
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.ausme, b.meins
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.ausme is not null   "
                + " AND    a.matnr||a.ausme not in (SELECT matnr||meinh"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm) "
                + " AND    a.matnr||a.ausme not in (SELECT matnr||wsmei"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_proportionalunits2) "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Unit Of Issue (Ausme) Without Alt Uom Conversion";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Unit_Issue_Eq_Base_Uom(string title) //152
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.meins, b.ausme
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = b.matnr"
                + " and    b.matnr = c.matnr"
                + " AND    c.aland  = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    b.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    NVL(a.meins,'XX') = NVL(b.ausme,'XX') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Unit Of Issue Equal Base Uom";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_r10_expire(string title)  //153
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.matkl, a.mhdrz, b.maktx, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c  "
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.matkl = 'R10'"
                + " AND    a.mtart = 'FERT' "
                + " AND    NVL(a.mhdrz, 0) = 0 "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Critical - MRSL must be 1 for FERTs with Material Group R10";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_r10_batchmgt(string title) //154
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.matkl, a.xchpf, b.maktx, a.sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b,    "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c  "
                + " WHERE  a.matnr = c.matnr"
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.mtart != 'ZUNB'"
                + " AND    a.matkl = 'R10'  "
                + " AND    nvl(a.xchpf, 'Q') != 'X'"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Critical - Batch Mgt must be checked for Material Group R10";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_no_prfrq(string title) //155
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, d.werks, a.mtart, c.maktx, a.mhdrz, d.prfrq
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   nvl(a.mhdrz, 0) = 0"
                + " AND   nvl(d.prfrq, 0) = 0"
                + " AND   a.mtart in ('ROH', 'HALB', 'ZGCM') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "QC Critical - Inspection Interval must be populated when MRSL = 0 (ROH,HALB,ZGCM) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Etifo_Etiar(string title) //156
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, mtart, etifo, etiar
	            FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara "
                + " WHERE matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_Ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND mtart <> 'DIEN'"
                + " AND NVL(ETIFO,'XX') <> 'XX'"
                + " AND NVL(ETIAR,'XX') <> 'M7'"
                + " ORDER BY matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Materials Missing Required Label Type";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_StoreTempNotSameAcrossPlants(string title) //157
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, d.maktx, LISTAGG (c.werks||'='||nvl(c.RAUBE2 ,'-')||'/'||nvl(c.TEMPB2 ,'-')||'/'||nvl(c.ZZADDLAB ,'-'), '; ') 
                    WITHIN GROUP (ORDER BY c.matnr) ""LSTAGG""
                FROM (SELECT DISTINCT c1.matnr, c1.werks,c1.RAUBE2, c1.TEMPB2, c1.ZZADDLAB  
                    FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c1, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c2"
                    + " WHERE c1.matnr=c2.matnr and c1.werks<>c2.werks "
                    + " AND (nvl(c1.RAUBE2,' ')<>nvl(c2.RAUBE2 ,' ') OR nvl(c1.TEMPB2 ,' ')<>nvl(c2.TEMPB2 ,' ') OR nvl(c1.ZZADDLAB ,' ')<>nvl(c2.ZZADDLAB ,' ')) "
                    + " ) c,"
                    + " " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt d"
                + " WHERE c.matnr = d.matnr  and d.spras = 'E' "
                + " GROUP BY c.matnr, d.maktx "
                + " ORDER BY c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Storage and Temperature Conditions not same across plants";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_StoreTemp_Required_MTART(string title)  //158
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, c.werks, c.RAUBE2, c.TEMPB2, c.ZZADDLAB
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " and  mtart in ('HALB', 'ROH', 'VERP') "
                + " and a.matnr=c.matnr"
                + " and (nvl(c.RAUBE2, ' ') = ' ' OR  nvl(c.TEMPB2,' ') = ' ') "
                + " ORDER BY a.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage and Temperature Conditions required for HALB, ROH, and VERP";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_StoreTemp_Invalid_Combo(string title) //159
                                                                                              /// SAP Connection Needed ///
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, a.mtart, c.WERKS, z.ZZRBTXT, c.RAUBE2, c.TEMPB2, LISTAGG(z.ZZTEMPB, '; ') 
                    WITHIN GROUP (ORDER BY c.matnr, c.werks) as ""CorrectTempValues"", '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".ZT142T@" + DDRSessionEntity.Current.mapinstance + " z"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr=c.matnr "
                + " AND    a.mtart IN ('ROH','HALB','VERP') "
                + " AND    nvl(c.TEMPB2,' ') NOT IN (SELECT ZZTEMPB FROM  " + DDRSessionEntity.Current.mapinstance + ".ZT142T@" + DDRSessionEntity.Current.mapinstance + " z2 WHERE  z2.ZZRAUBE = c.RAUBE2 and z2.spras='E') "
                + " AND    z.spras(+)='E' "
                + " AND    c.RAUBE2=z.zzraube(+) "
                + " AND    (nvl(c.RAUBE2,' ') <> ' '  OR  nvl(c.TEMPB2,' ') <>' ') "
                + " GROUP BY c.matnr,a.mtart, c.WERKS, z.ZZRBTXT, c.RAUBE2, c.TEMPB2"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage and Temperature Condition combination not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_StoreTemp_Invalid(string title) //160
                                                                                        /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, a.mtart, c.WERKS, c.RAUBE2, c.TEMPB2, 'Invalid Temp Cond' AS INVALIDCOND, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".ZT143T@" + DDRSessionEntity.Current.mapinstance + " zt"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr=c.matnr"
                + " AND    c.TEMPB2= zt.TEMPB2(+) "
                + " AND    zt.spras(+)='E' "
                + " AND    nvl(c.TEMPB2,' ')  <>' '"
                + " AND    zt.tbtxt is null"
                + " UNION"
                + " SELECT c.matnr, a.mtart, c.WERKS, c.RAUBE2, c.TEMPB2, 'Invalid Store Cond' AS INVALIDCOND, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".ZT141T@" + DDRSessionEntity.Current.mapinstance + " zr"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr=c.matnr"
                + " AND    c.RAUBE2= zr.RAUBE2(+) "
                + " AND    zr.spras(+)='E' "
                + " AND    nvl(c.RAUBE2,' ')  <>' '"
                + " AND    zr.rbtxt is null"
                + " ORDER BY 1,2,3");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Or Temperature Condition invalid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_LGPRO_Invalid(string title) //161
                                                                                    /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.werks, c.lgpro, LISTAGG(z.lgort, '; ') WITHIN GROUP (ORDER BY c.matnr, c.werks) ""ValidSlocs"", '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " z"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.werks= z.werks   AND z.mandt='100'"
                + " AND c.werks||c.lgpro NOT IN (SELECT werks||lgort FROM " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND NVL(lgpro, ' ') <> ' '"
                + " GROUP BY c.matnr, c.werks, c.lgpro"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Issue Storage Location is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_LGFSB_Invalid(string title) //162
                                                                                    /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.werks, c.lgfsb, LISTAGG(z.lgort, '; ') WITHIN GROUP (ORDER BY c.matnr, c.werks) ""ValidSlocs"", '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " z"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.werks= z.werks   AND z.mandt='100'"
                + " AND c.werks||c.lgfsb NOT IN (SELECT werks||lgort FROM " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND NVL(lgfsb, ' ') <> ' ' "
                + " GROUP BY c.matnr, c.werks, c.lgpro, c.lgfsb"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Location for EP is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_LGORT_Invalid(string title) //163
                                                                                    /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.werks, c.lgort, LISTAGG(z.lgort, '; ') WITHIN GROUP (ORDER BY c.matnr, c.werks) ""ValidSlocs"", '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard c, " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " z"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.werks= z.werks   AND z.mandt='100'"
                + " AND c.werks||c.lgort NOT IN (SELECT werks||lgort FROM " + DDRSessionEntity.Current.mapinstance + ".T001L@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " GROUP BY c.matnr, c.werks, c.lgort"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Location (MARD) is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_STORE_ABCIN_Invalid(string title)  //164
                                                                                     /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, c.werks, c.ABCIN, LISTAGG(z.ABCIN, '; ') WITHIN GROUP (ORDER BY c.matnr, c.werks) ""Indicator"", '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.mapinstance + ".T159C@" + DDRSessionEntity.Current.mapinstance + " z"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.werks= z.werks   AND z.mandt='100'"
                + " AND c.werks||c.ABCIN NOT IN (SELECT werks||ABCIN FROM " + DDRSessionEntity.Current.mapinstance + ".T159C@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND NVL(c.ABCIN, ' ') <> ' '"
                + " GROUP BY c.matnr, c.werks, c.ABCIN"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Physical Inventory Ind for Cycle Counting is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Wm_No_Uom(string title)  //165
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.bezme
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE  a.matnr = b.matnr"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.lgnum IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    NVL(A.bezme,'XX') <> 'XX' "
                + " AND    a.matnr||a.bezme NOT IN (Select matnr||meinh FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm) "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Capacity UoM exists but Alt UoM missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Wm_No_Mard(string title) //166
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.werks, a.lgnum
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.lgnum IN (SELECT lgnum"
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref"
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    b.werks IN (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    b.matnr||b.werks not in (SELECT matnr||werks"
                    + " FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mard"
                    + " WHERE  lgort = '0100') "
                + " AND    b.werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - WM Data Exists But Sloc 0100 Missing";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Missing_Storage(string title) //167
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.lgnum, b.lgbkz, b.ltkze, b.ltkza
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c "
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.matnr = c.matnr"
                + " AND    (b.lgbkz is null or b.ltkze is null or b.ltkza is null)  "
                + " AND    c.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND    a.werks in (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Storage Strategies - at least one of three is empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Uoi_Wmunit_Proppuom(string title) //168
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, c.werks, a.mtart, a.meins, b.lgnum, c.ausme, b.lvsme, b.vomem
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE  a.matnr = b.matnr (+) "
                + " AND    a.matnr = c.matnr"
                + " AND    c.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    a.matnr in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    (b.lgnum is null or  b.lgnum in "
                    + " (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ")) "
                + " AND     b.vomem is not null"
                + " ORDER by a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - Unit Of Issue,Wm Unit,Proposal Uom";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_uom_base_wm_or_issue(string title)  //169
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr,c.werks,b.lgnum,a.meins,b.lhme1,b.lvsme,c.ausme 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_Mara a, "
                  + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn b,"
                   + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE b.matnr IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   b.lgnum IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                  + " AND   (b.lhme1 <> a.meins"
                + " OR     b.lhme1 <> b.lvsme "
                + " OR     b.lhme1 <> c.ausme) "
                + " AND    b.matnr = a.matnr"
                + " AND    b.matnr = c.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - LE Qty UoM must be Base unit, WM unit, or Unit of Issue";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM_LTKZA_invalid(string title)  //170
                                                                                  /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.LTKZA, a.LTKZE, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.lgnum||a.LTKZA NOT IN (SELECT lgnum||LGTKZ  FROM " + DDRSessionEntity.Current.mapinstance + ".T305t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras='E') "
                + " AND   nvl(a.LTKZA, ' ') <> ' '"
                + " ORDER BY a.matnr, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Type Indicator for Stock Removal (LTKZA) is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM_LTKZE_invalid(string title) //171
                                                                                 /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.LTKZA, a.LTKZE, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.lgnum||a.LTKZE NOT IN (SELECT lgnum||LGTKZ  FROM " + DDRSessionEntity.Current.mapinstance + ".T305t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras='E') "
                + " AND   nvl(a.LTKZE, ' ') <> ' '"
                + " ORDER BY a.matnr, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Type Indicator for Stock Placement (LTKZE) is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM_LVSME_same_as_BaseUoM(string title)  //172
        {
            try
            {
                string command = String.Format(@"
                SELECT m.matnr, m.lgnum, a.meins, m.LVSME
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn m, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a"
                + " WHERE m.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   m.lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   nvl(m.LVSME, ' ') <> ' '"
                + " AND   m.matnr=a.matnr "
                + " AND   m.LVSME=a.meins"
                + " ORDER BY m.matnr, m.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - WM UoM should not be the same as the Base UoM";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM_LGBKZ_invalid(string title)  //173
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.LGBKZ, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.lgnum||a.LGBKZ NOT IN (SELECT lgnum||LGBKZ  FROM " + DDRSessionEntity.Current.mapinstance + ".T304t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras='E') "
                + " AND   nvl(a.LGBKZ, ' ') <> ' '"
                + " ORDER BY a.matnr, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Section Indicator is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM_LETYx_invalid(string title) //174
                                                                                 /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, lety1, lety2, lety3, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref     WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND (LETY1 Is Not Null AND lgnum||lety1 NOT IN (SELECT lgnum||letyp FROM " + DDRSessionEntity.Current.mapinstance + ".T307t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras='E')) "
                + " OR (LETY2 Is Not Null AND lgnum||lety2 NOT IN (SELECT lgnum||letyp FROM " + DDRSessionEntity.Current.mapinstance + ".T307t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras='E')) "
                + " OR (LETY3 Is Not Null AND lgnum||lety3 NOT IN (SELECT lgnum||letyp FROM " + DDRSessionEntity.Current.mapinstance + ".T307t@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras='E')) "
                + " ORDER BY a.matnr, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF – Storage Unit Type (LETY1, LETY2, or LETY3) is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM_LHMG1_required(string title)  //175
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, lhme1, lety1, LHMG1
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a"
                + " WHERE matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   (NVL(lhme1, ' ') <> ' '  OR  NVL(lety1, ' ') <> ' ') "
                + " AND   NVL(LHMG1, '0') = '0'"
                + " ORDER BY a.matnr, a.lgnum");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF – Loading Qty required when Qty UoM or SUT are populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WM2_LGPLA_invalid(string title)  //176
                                                                                   /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.lgtyp, a.lgpla, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgt a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.lgnum  IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.lgnum||a.lgtyp||a.lgpla NOT IN (SELECT lgnum||lgtyp||lgpla  FROM " + DDRSessionEntity.Current.mapinstance + ".LAGP@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND   nvl(a.lgpla, ' ') <> ' '"
                + " ORDER BY a.matnr, a.lgnum,a.lgtyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - WM2 Storage Bin is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_ea_mia(string title) //177
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.matkl, a.meins, c.maktx, a.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.matkl in ('R05', 'R10', 'R15', 'R25') "
                + " AND   a.meins != 'EA'"
                + " AND   a.matnr NOT IN (SELECT matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh = 'EA') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - Alt UoM EA required for Material Groups R05, R10, R15, R25";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_aiuuom_mia(string title) //178
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.matkl, a.meins, c.maktx, a.sapid, d.werks, d.dismm
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   d.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   c.spras = 'E'"
                + " AND   a.matkl in ('R05', 'R10', 'R15', 'R25') "
                + " AND   nvl(d.dismm, 'ND') != 'ND'"
                + " AND   a.matnr NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh IN ('LCC', 'LCG', 'LCK', 'LCM', 'MU','TEU')) "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - Alt UoM LCC,LCG,LCK,LCM, MU, or TEU must exist when Planned w/ Material Groups R05, R10, R15, R25";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Audit_Multiple_Active_Units(string title) //179
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, c.maktx, a.meinh, a.umren, a.umrez, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND EXISTS (SELECT 1 FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm m "
                    + " WHERE a.matnr = m.matnr AND meinh in ('LCC', 'LCG', 'LCK', 'LCM', 'MU','TEU') "
                    + " GROUP BY matnr HAVING count(meinh) > 1) "
                + " AND   a.meinh IN ('BEA', 'LCC', 'LCG', 'LCK', 'LCM', 'MU','TEU') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Critical - Materials Cannot Have Multiple Active Units (MU, LCC, LCG, TEU etc) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_st_mia(string title) //180
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.mtart, a.matkl, a.meins, c.maktx, a.sapid
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   c.spras = 'E'"
                + " AND   a.mtart = 'FERT'"
                + " AND   a.matnr NOT IN (SELECT matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh = 'ST') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "Financial Warning - Alt UoM ST should exist for FERTs";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_meins_not_ea_no_altuom(string title) //181
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, a.meins
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.meins != 'EA'"
                + " AND   nvl(c.dismm, 'QQ') IN ('X0', 'Z1', 'Z3') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.matnr NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh = 'EA') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Planned Material - Base UoM is not EA and EA not exist as Alt UoM";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_planned_no_st_altuom(string title) //182
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, a.meins
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   nvl(c.dismm, 'QQ') IN ('X0', 'Z3') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.matnr NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh = 'ST') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Planned Material - ST Alt UoM does not exist";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_planned_sold_no_st_altuom(string title) //183
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, a.meins
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   nvl(c.dismm, 'QQ') IN ('X0', 'Z3') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   d.vkorg in (select vkorg from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales where werks = c.werks ) "
                + " AND   a.matnr NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh = 'ST') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Planned Material and sold - ST Alt UoM does not exist";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_planned_sold_meins_not_ea_no_altuom(string title) //184
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct a.matnr, a.mtart, a.meins
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mvke d"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.matnr = c.matnr"
                + " AND   a.matnr = d.matnr"
                + " AND   a.meins != 'EA'"
                + " AND   nvl(c.dismm, 'QQ') IN ('X0', 'Z3') "
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND   c.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   d.vkorg in (select vkorg from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_sales where werks = c.werks ) "
                + " AND   a.matnr NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm WHERE meinh = 'EA') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Planned Material and sold - Base UoM is not EA and EA not exist as Alt UoM";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MARM_LabelClaim_Missing_ZTMMLBLCLAIM_UOM(string title) //185
                                                                                                         /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT r.matnr, b.maktx, z.aun, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.mapinstance + ".ZTMMLBLCLAIM_UOM@" + DDRSessionEntity.Current.mapinstance + " z, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref r, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE r.matnr = b.matnr "
                + " AND b.spras='E'"
                + " AND r.aland='" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND r.load='X'"
                + " AND r.matnr like z.itempack||'%' "
                + " AND z.aun<>' '"
                + " AND r.matnr||aun NOT IN (SELECT matnr||meinh FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm) "
                + " ORDER BY r.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Material missing Label Claim Alt UoM record (ZTMMLBLCLAIM_UOM) ";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Additional_Eancat(string title) //186
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.meinh, a.ean11, a.eantp, a.hpean, a.sapid
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mean a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.eantp is null"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " ORDER BY a.matnr, a.meinh");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Additional Data:  Alt Ean Missing Ean Category";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_MARM_MEINH_InvalidValue(string title) //187
                                                                                        /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.sapid, b.maktx, a.meinh, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marm a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b"
                + " WHERE  a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    a.matnr = b.matnr"
                + " AND    b.spras = 'E'"
                + " AND    a.meinh NOT IN (SELECT mseh3 FROM " + DDRSessionEntity.Current.mapinstance + ".T006A@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100' AND spras ='E') "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Unit of Measure value not setup in SAP";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_error_ccycle1(string title) //188
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.prvbe, a.berkz, a.lgpla, a.lgtyp, a.nkdyn
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref B"
                + " WHERE a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " and a.matnr = b.matnr "
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND a.berkz = 3"
                + " AND a.lgpla is null"
                + " AND a.lgtyp is null");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Ccycles staging ind = 3,dynamic storage bin must be empty and storage bin must be populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_error_ccycle2(string title) //189
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.prvbe, a.berkz, a.lgpla, a.nkdyn
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref B"
                + " WHERE a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " and a.matnr = b.matnr"
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND a.berkz = 1"
                + " AND a.nkdyn is null"
                + " AND a.lgpla is null");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Ccycles staging ind = 1,dynamic storage bin must be checked and storage bin must be empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_error_ccycle3(string title) //190
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.berkz, a.lgpla, a.nkdyn, a.lgtyp
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref B"
                + " WHERE a.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " and a.matnr = b.matnr                  "
                + " AND b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND a.berkz = 0"
                + " AND (a.lgpla is not null"
                + " OR  a.nkdyn is not null"
                + " OR  a.lgtyp is not null) ");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Ccycles staging ind = 0,dynamic storage bin and storage bin must be empty";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_error_ccycle_missing(string title) //191
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct b.idnrk, b.werks, m.lgnum 
			    FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail b, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn m"
                + " WHERE  b.idnrk IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref   "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " AND load = 'X') "
                + " AND    b.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref "
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    m.lgnum IN (SELECT lgnum FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref "
                    + " WHERE site_code = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    b.idnrk NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd) "
                + " AND    b.idnrk = m.matnr"
                + " ORDER BY idnrk, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "BR - Ccycle record missing, Material has a BOM and WM record";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_CCycle_PRVBE_invalid(string title) //192
                                                                                     /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, lgnum, a.werks, a.prvbe, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.werks||a.prvbe NOT IN (SELECT werks||prvbe FROM " + DDRSessionEntity.Current.mapinstance + ".PVBE@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " ORDER BY a.matnr, a.lgnum,a.lgtyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Supply Area is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_CCycle_LGPLA_invalid(string title) //193
                                                                                     /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.lgtyp, a.lgpla, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.lgnum||a.lgtyp||a.lgpla NOT IN (SELECT lgnum||lgtyp||lgpla  FROM " + DDRSessionEntity.Current.mapinstance + ".LAGP@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND   nvl(a.lgpla, ' ') <> ' '"
                + " ORDER BY a.matnr, a.lgnum,a.lgtyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Bin is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_CCycle_LGPLA_LGTYP_invalid(string title) //194
                                                                                           /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.lgnum, a.lgtyp, a.lgpla, '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a"
                + " WHERE a.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND   a.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref WHERE site_code='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND   a.lgnum||a.lgtyp||a.lgpla NOT IN (SELECT lgnum||lgtyp||lgpla  FROM " + DDRSessionEntity.Current.mapinstance + ".LAGP@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND   nvl(a.lgpla, ' ') <> ' ' "
                + " AND   nvl(a.lgtyp, ' ') <> ' '"
                + " ORDER BY a.matnr, a.lgnum,a.lgtyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Storage Bin & Storage Type combo is not valid";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WS_UEETO_UEETK_OverDeliveryLimit(string title) //195
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, UEETO, UEETK 
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    nvl(UEETK, ' ') <> ' '  "
                + " AND    nvl(UEETO,'0') >'0'"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Both Overdelivery Tolerance and Unlimited Overdelivery flag are populuated. Only populate one";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WS_ProdSchedProfile_Missing(string title) //196
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, sfcpf, fevor, matgr, kzech, uneto, ueeto, ueetk, basmg, frtme, sobsl
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND  nvl(sfcpf,' ') =' '"
                + " AND (nvl(FEVOR,' ') <> ' '"
                + " OR nvl(UNETO,'0') <> 0"
                + " OR nvl(UEETK,' ') <> ' ' "
                + " OR nvl(BASMG,'0') <> 0 "
                + " OR nvl(MATGR,' ') <> ' '"
                + " OR nvl(BEARZ,'0') <> 0"
                + " OR nvl(TRANZ,'0') <> 0) "
                + " ORDER BY matnr, werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Production Scheduling Profile must be populated if any Work Scheduling fields are populated";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WS_ProdSchedProfile_populated(string title) //197
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, a.mtart, c.werks, c.sfcpf
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    nvl(c.sfcpf,' ') <> ' '"
                + " AND    a.matnr=c.matnr"
                + " AND    a.mtart IN ('HAWA','ZGCM') "
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Production Scheduling Profile should not be populated for HAWA and ZGCM materials";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_WS_FRTME_populated(string title)  //198
        {
            try
            {
                string command = String.Format(@"
                SELECT c.matnr, a.mtart, c.werks, c.sfcpf, c.FRTME
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a"
                + " WHERE  c.matnr  IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND    c.werks  IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    nvl(c.sfcpf,' ') <> ' '"
                + " and    nvl(c.FRTME,' ') <> ' '"
                + " AND    a.matnr=c.matnr"
                + " AND    a.mtart <>'ZMAB'"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Production Unit should only be populated for ZMAB materials";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_zcountry1(string title) //199
        {
            try
            {
                string command = String.Format(@"
                SELECT DISTINCT a.matnr, a.werks
                FROM  " + DDRSessionEntity.Current.table_schema + "_procs.gdd_zcountry_list a"
                + " WHERE a.site = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " AND (a.matnr||a.werks not in (SELECT matnr||werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc "
                    + " WHERE a.werks IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")) "
                + " OR  a.matnr not in (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref  WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")) "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Zcountry record without MARC or MATNR_REF record";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_zcountry3(string title)  //200
                                                                           /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.werks, a.land1,  '" + DDRSessionEntity.Current.mapinstance + "' as SAP"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_procs.GDD_ZCOUNTRY_LIST a"
                + " WHERE a.land1 NOT IN (SELECT land1 FROM " + DDRSessionEntity.Current.mapinstance + ".T005@" + DDRSessionEntity.Current.mapinstance + " WHERE mandt='100') "
                + " AND a.matnr       IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + " AND load='X') "
                + " AND a.werks       IN (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland='" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " ORDER BY a.matnr, a.werks, a.land1");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "LF - Zcountry record with invalid country";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_audit_zcountry2(string title)  //201
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, b.maktx, a.sapid, a.mtart, c.werks 
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt b, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c"
                + " WHERE  a.matnr in (select matnr from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " and load='X') "
                + " AND a.matnr=b.matnr"
                + " AND a.matnr=c.matnr"
                + " AND b.spras='E'"
                + " AND a.mtart in ('FERT','HALB') "
                + " AND c.werks in  (select werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref where aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND c.matnr||c.werks not in (select matnr||werks from " + DDRSessionEntity.Current.table_schema + "_procs.gdd_zcountry_list) "
                + " ORDER BY a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "FYI - FERT & HALB materials without a Zcountry record";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Errors_Inmarc_Nt_Matnr_Ref(string title)  //202
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr,werks 
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc "
                + " WHERE  werks in (SELECT werks "
                    + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref"
                    + " WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    matnr NOT IN (SELECT matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + " ) "
                + " AND    matnr not like 'ZQM%'"
                + " ORDER BY matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "List Of Materials Not In Matnr Ref Table";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_MM_Config_Check_MRP_Controller(string title)  //203
                                                                                       /// SAP Connection Required ///
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct A.MATNR, A.WERKS, DISPO
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref c"
                + " WHERE  beskz = 'E'"
                + " AND    a.werks = b.werks"
                + " AND    a.matnr = c.matnr"
                + " AND    b.aland = c.aland"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' "
                + " and    a.dispo || a.werks not in (select dispo || werks from " + DDRSessionEntity.Current.mapinstance + ".T024D@" + DDRSessionEntity.Current.mapinstance + ") "
                + " ORDER BY a.matnr, a.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "MRP Controller Does Not Exist In Set SAP Instance.";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //    public System.Data.DataTable SQL_MM_audit_matnr_flaged_delete(string title)  //204
        //    {
        //        try
        //        {
        //            string command = String.Format(@"
        //");
        //            System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //            mmdataset.TableName = title ; // "";
        //            return mmdataset;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        public System.Data.DataTable SQL_MM_audit_DDT_spt(string title)  //205
        {
            try
            {
                string command = String.Format(@"
                SELECT distinct c.matnr, c.sobsk, c.werks, s.wrk02, sapid
                FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara b, " + DDRSessionEntity.Current.mapinstance + ".T460A@" + DDRSessionEntity.Current.mapinstance + " s"
                + " WHERE  c.sobsk = s.sobsl and s.wrk02 <> ' '"
                + " AND    c.matnr||s.wrk02 not in (SELECT z.matnr||z.werks FROM " + DDRSessionEntity.Current.mapinstance + ".marc@" + DDRSessionEntity.Current.mapinstance + " z WHERE z.matnr = c.matnr) "
                + " AND    c.matnr||s.wrk02 not in (SELECT c2.matnr||c2.werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc c2 WHERE c2.matnr = c.matnr) "
                + " AND    c.werks in (SELECT werks from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ") "
                + " AND    c.matnr = b.matnr"
                + " ORDER BY c.matnr, c.werks");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title; // "DDT - SPT Plant not found in GPR nor DDT";
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}