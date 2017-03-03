using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class BOMVerificationReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_BOM_extract_header(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, stlal, stlan, stlst, ztext, stktx, bmeng, bmein,
                    losvn, losbs, stlst, aennr
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader d"
                + " WHERE werks in (" + DDRSessionEntity.Current.plantcode + ") "
                + " ORDER BY matnr, werks, stlal");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable SQL_BOM_extract_details(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, stlal, posnr, idnrk, menge, meins,
                    postp, sortf, ausch, fmeng, rekrs, nlfzv, nlfmv, 
                    sanka, schgt, alpgr, ewahr, lgort, prvbe , kzkup
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomdetail "
                + " WHERE werks in (" + DDRSessionEntity.Current.plantcode + ") "
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
        public System.Data.DataTable SQL_BOM_extract_subdetails(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT MATNR, WERKS, STLAL, POSNR, EBORT, UPMNG, UPOSZ, UPTXT
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomsubdetail "
                + " WHERE werks in (" + DDRSessionEntity.Current.plantcode + ") "
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
        public System.Data.DataTable SQL_BOM_extract_hd(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT matnr, werks, stlal, stlst FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_bomheader "
                    + " WHERE werks IN (" + DDRSessionEntity.Current.plantcode + ") "
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



    }
}
