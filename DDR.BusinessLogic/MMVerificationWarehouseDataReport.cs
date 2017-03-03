using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class MMVerificationWarehouseDataReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_MM_extract_wms_Warehouse1(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT  a.matnr, a.lgnum, a.lvsme, a.vomem, a.plkpt, a.mkapv, a.bezme, 
                    a.ltkza, a.ltkze, a.lgbkz, a.block, a.bsskz, a.kzmbf, a.l2skr, 
                    a.kzzul, a.lhmg1, a.lhmg2, a.lhmg3, a.lhme1, a.lhme2, a.lhme3, 
                    a.lety1, a.lety2, a.lety3
                FROM    " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgn a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref c"
                + " WHERE   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " AND     a.matnr = b.matnr"
                + " AND     a.lgnum = c.lgnum"
                + " AND     b.aland = c.site_code"
                + " ORDER BY a.lgnum, a.matnr");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_MM_extract_wms_Warehouse2(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT  a.matnr, a.lgnum, a.lgtyp, a.lgpla, a.lpmax, a.lpmin, a.rdmng, 
                    a.kober, a.mamng, a.nsmng
                FROM  " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mlgt a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_warehouse_ref c"
                + " WHERE a.matnr = b.matnr"
                + " AND   a.lgnum = c.lgnum"
                + " AND   b.aland = c.site_code"
                + " AND   b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
                + " ORDER BY a.lgnum, a.matnr, a.lgtyp");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_MM_extract_wms_control_cycles(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, t.maktx, a.werks, a.prvbe, a.behaz, a.sigaz, a.behmg, a.lgnum, a.berkz, a.lgtyp, a.lgpla, a.nkdyn, a.ablad
	            FROM   " + DDRSessionEntity.Current.table_schema + "_owner.gdd_pkhd a, "
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_matnr_ref b,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref c,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt t"
                + " WHERE  a.matnr = b.matnr"
                + " AND    a.werks = c.werks"
                + " AND    b.aland = c.aland"
                + " AND    a.matnr = t.matnr"
                + " AND    t.spras = 'E'"
                + " AND    b.aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ""
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



    }
}
