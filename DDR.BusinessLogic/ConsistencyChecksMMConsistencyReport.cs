using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.DataAccess;
using DDR.Entity;

namespace DDR.BusinessLogic
{
    public class ConsistencyChecksMMConsistencyReport
    {
        MMDataManager mmdatamanager = new MMDataManager();
        ReportTableHeader TableHeader = new ReportTableHeader();
     
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_gen_mara(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        SELECT distinct a.matnr, a.mtart, a.meins, a.matkl, a.mbrsh, a.spart,  " &
        //         "       replace(translate(a.matnr, '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX'),'X') check_mat  " &
        //         "FROM #session.table_schema#.gdd_mara a, #session.table_schema#.gdd_marc b  " &
        //         "WHERE a.matnr = b.matnr AND b.werks in (#werks_param#) and nvl(a.mtart, 'XXX')  not in ('DIEN', 'PROD')  " &                
        //         "AND a.matnr not like 'HUM%' AND a.matnr not like 'ZQM%'  #matnr_clause#   ORDER BY a.matnr");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return  mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_mrp_chk(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        SELECT distinct a.matnr, b.werks, nvl(b.dismm, 'Z') dismm, nvl(b.dispo, 'Z') dispo, nvl(b.disls, 'Z') disls, nvl(b.beskz, 'Z') beskz,  " &
        //         "       nvl(b.mrppp, 'Z') mrppp, nvl(b.fhori, 'Z') fhori, nvl(b.mtvfp, 'Z') mtvfp   " &
        //         "FROM #session.table_schema#.gdd_mara a, #session.table_schema#.gdd_marc b   " &
        //         "WHERE a.matnr = b.matnr AND b.werks in (#werks_param#)  AND a.matnr not like 'HUM%' AND a.matnr not like 'ZQM%' AND a.mtart not in ('ZUNB', 'DIEN', 'PROD') AND b.werks not like 'S%' AND b.werks in (SELECT werks from #session.table_schema#.gdd_werks_ref WHERE planning is null AND aland = '#session.site_code#')  #matnr_clause#  ORDER BY a.matnr");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_qm_defined(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_account_ck(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_no_mbew(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_cost_ck(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_gen_type(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_chk_mbew(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_ausme_prop(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_vrkme_prop(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_no_etiar(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_etiar_etifo(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_roh_matkl(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_halb_alt(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_halb_matkl(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_fert_nosales(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_fert_mhdrz(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_fert_sales(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_fert_st(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_work_XO(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_mat_sold(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_purch_plan(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_wm_no0100(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_nowm_0100(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_purch(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_mm_check_manuf(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        ");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_qmat_check(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        SELECT distinct a.matnr, b.werks 
        //        FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_qmat b "
        //        + " WHERE a.matnr = b.matnr AND werks in (" + DDRSessionEntity.Current.plantcode + ") AND nvl(a.mtart, 'XXXX') not in ('DIEN', 'PROD', 'ZUNB') AND a.matnr not like 'HUM%' AND a.matnr not like 'ZQM%'  "
        //        + " AND b.art = '04' AND nvl(b.chg, 'W') not in ('X', 'Y')  #matnr_clause#   ORDER BY a.matnr");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public System.Data.DataTable SQL_Consistency_R10_class_check(string title)
        //{
        //    try
        //    {
        //        string command = String.Format(@"
        //        SELECT distinct a.matnr FROM " + DDRSessionEntity.Current.table_schema + "_owner.gdd_mara a, " + DDRSessionEntity.Current.table_schema + "_owner.gdd_marc b WHERE a.matnr = b.matnr AND werks in (" + DDRSessionEntity.Current.plantcode + ") AND nvl(a.mtart, 'XXXX') not in ('DIEN', 'PROD') AND a.matnr not like 'HUM%' AND a.matnr not like 'ZQM%' AND a.matnr not in (select distinct objek from " + DDRSessionEntity.Current.table_schema + "_owner.gdd_ausp) #matnr_clause#   ORDER BY a.matnr");
        //        System.Data.DataTable mmdataset = mmdatamanager.GetMMAuditingData(command).Tables[0];
        //        mmdataset.TableName = title;
        //        return mmdataset;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



    }
}
