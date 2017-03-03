using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class APOAuditingReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        public System.Data.DataTable SQL_APO_APO_vs_MM(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.locno, c.maktx, m.dismm mrp_type,
                decode(m.matnr,'','MM record in DDT does not exist',
                decode(m.dismm,'X0','','Z3','','Not Planned in MRP View')) Comments
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_products a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_marc m, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE locno in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND m.matnr(+) = a.matnr and m.werks(+) = a.locno"
                + " AND a.matnr = c.matnr(+) AND c.spras(+) = 'E'"
                + " AND a.locno in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND decode(m.matnr,'','MM record in DDT does not exist',"
                    + " decode(m.dismm,'X0','','Z3','','Not Planned in MRP View')) is not null"
                + " UNION "
                + " SELECT m.matnr, m.werks, c.maktx, m.dismm, decode(a.matnr,'','APO record does not exist','') Comments"
                + " FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_products a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_marc m, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE m.werks in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND m.matnr = a.matnr(+) and m.werks = a.locno(+) and m.dismm in ('X0','Z3')"
                + " AND m.matnr = c.matnr(+) AND c.spras(+) = 'E'"
                + " AND m.werks in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND a.matnr is null");
                System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
                mmdataset.TableName = title;
                return  mmdataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable SQL_APO_APO_SNP_STH_vs_PDT(string title)
        {
            try
            {
                string command = String.Format(@"
                SELECT a.matnr, a.locno, c.maktx, a.shiph, m.plifz, 'SNP STH is not > PDT in MM in DDT' Comments
                FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_apo_products a, " + DDRSessionEntity.Current.table_schema + "_owner.GDD_marc m,"
                    + DDRSessionEntity.Current.table_schema + "_owner.gdd_makt c"
                + " WHERE locno in (SELECT werks FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_werks_ref WHERE aland = '" + DDRSessionEntity.Current.SiteCode + "' " + ")"
                + " AND m.matnr = a.matnr and m.werks = a.locno"
                + " AND a.matnr = c.matnr(+) AND c.spras(+) = 'E'"
                + " AND a.locno in (" + DDRSessionEntity.Current.plantcode + ")"
                + " AND a.shiph < m.plifz");
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
