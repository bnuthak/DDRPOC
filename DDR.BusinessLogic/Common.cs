using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using DDR.DataAccess;
using DDR.Entity;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace DDR.BusinessLogic
{
    public class Common
    {
        QMDataManager qmmanager = new QMDataManager();
        CommonManager commmanager = new CommonManager();

        MMAuditingReport mmauditingreport = new MMAuditingReport();
        MMVerificationGlobalDataReport mmverificationglobaldatareport = new MMVerificationGlobalDataReport();
        MMVerificationPlantDataReport mmverificationplantdatareport = new MMVerificationPlantDataReport();
        MMVerificationSalesDataReport mmverificationsalesdatareport = new MMVerificationSalesDataReport();
        MMVerificationWarehouseDataReport mmverificationwarehousedatareport = new MMVerificationWarehouseDataReport();

        BOMAuditingReport bomauditingreport = new BOMAuditingReport();
        BOMVerificationReport bomverificationreport = new BOMVerificationReport();

        ResourcesAuditingReport resourcesauditingreport = new ResourcesAuditingReport();
        ResourcesVerificationReport resourcesverificationreport = new ResourcesVerificationReport();

        RecipesAuditingReport recipesauditingreport = new RecipesAuditingReport();
        RecipesVerificationReport recipesverificationreport = new RecipesVerificationReport();

        QMAuditingReport qmauditingreport = new QMAuditingReport();
        QMVerificationReport qmverificationreport = new QMVerificationReport();

        APOAuditingReport apoauditingreport = new APOAuditingReport();
        APOVerificationReport apoverificationreport = new APOVerificationReport();

        InventoryAuditingReport inventoryauditingreport = new InventoryAuditingReport();
        InventoryVerificationReport inventoryverificationreport = new InventoryVerificationReport();

        ConsistencyChecksMMConsistencyReport consistencychecksmmconsistencyreport = new ConsistencyChecksMMConsistencyReport();
        ConsistencyChecksMMClashesReport consistencychecksmmclashesreport = new ConsistencyChecksMMClashesReport();
        ConsistencyChecksBOMConsistencyReport consistencychecksbomconsistencyreport = new ConsistencyChecksBOMConsistencyReport();
        ConsistencyChecksResourceConsistencyReport consistencychecksresourceconsistencyreport = new ConsistencyChecksResourceConsistencyReport();
        ConsistencyChecksRecipeConsistencyReport consistencychecksrecipeconsistencyreport = new ConsistencyChecksRecipeConsistencyReport();

        OtherAdminReport otheradminreport = new OtherAdminReport();

        public  void ExportToExcel(DataTable dt, string reportname)
        {
            GridView grdvreportdata = new GridView();
            try
            {
                grdvreportdata.AutoGenerateColumns = true;
                grdvreportdata.DataSource = dt;// qmsapdata.GetQMVerificationTotalReport();
                grdvreportdata.DataBind();
                grdvreportdata.AllowPaging = false;
                grdvreportdata.AllowSorting = false;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + "" + reportname + "");
                //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=QMVerificationReport.xls");
                HttpContext.Current.Response.Charset = "";
               // HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
               
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

                foreach (GridViewRow r in grdvreportdata.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                        {
                            r.Cells[columnIndex].Attributes.Add("class", "text");
                        }
                    }
                }
                grdvreportdata.RenderControl(htmlWrite);
                string style = @"<style> .text { mso-number-format:\@; } </style> ";
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Write(stringWrite.ToString());
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConvertToExcel(DataSet ds)
        {
            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                ExcelApp.Workbooks.Add(Type.Missing);
                string reportname = string.Empty;

                int ind = 0;
                foreach (DataTable dtab in ds.Tables)
                {
                    ind++;
                    Microsoft.Office.Interop.Excel.Worksheet Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[ind];

                    for (int i = 0; i < dtab.Columns.Count; i++)
                    {
                        Sheet1.Cells[1, i + 1] = dtab.Columns[i].ColumnName;
                    }

                    for (int i = 0; i < dtab.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtab.Columns.Count; j++)
                        {
                            Sheet1.Cells[i + 2, j + 1] = dtab.Rows[i][j].ToString();
                        }
                    }
                    //  int idxsheet = ind - 1;
                    bool hasValue = DDRSessionEntity.Current.checkcount.TryGetValue((ind).ToString(), out reportname);
                    string mmrptname = reportname.Replace("-", " ");
                    mmrptname = (mmrptname.Length > 30 ? mmrptname.Substring(0, 30) : mmrptname);
                    Sheet1.Name = mmrptname;
                }
                ExcelApp.ActiveWorkbook.SaveCopyAs("TextData.xls");
                ExcelApp.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateCSVFile(DataTable dtDataTable, string strFilePath, string tableName)
        {
            try
            {

                StreamWriter sw = new StreamWriter(strFilePath, true);

                //StreamWriter sw = new StreamWriter("~/GridDataMINH.csv", false);
                sw.WriteLine(tableName);
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    sw.Write(dtDataTable.Columns[i]);
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.WriteLine("");
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataTable> DataTableList = new List<DataTable>();

        public List<DataTable> GetSelectedDataTable()
        {
            //List<DataTable> DataTableList = new List<DataTable>();
            switch (DDRSessionEntity.Current.reportType) // What is the session's current report type?
            {
                case "MMAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Makt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Makt_E(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Basic_Eancat(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Ferts_Expire(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sales_Ntgew(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mara_Miss_Data(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_fert_01M_ea(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_halb_01M_ts(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_V1M_ts(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_halb_001_ea(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_MATKL_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_matkl_roh(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_matkl_halb(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_matkl_fert(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "15":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_PRDHA_MTART_required(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "16":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_PRDHA_MTART_mismatch(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "17":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_PRDHA_InvalidValue(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "18":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_MTART_MTPOS_NORM(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "19":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_MTART_MTPOS_ZVPT(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "20":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_BAS_MEINS_InvalidValue(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "21":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Profit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "22":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Stdpricezero(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(mmauditingreport.SQL_MM_errors_zunb_acct(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "24":
                                DataTableList.Add(mmauditingreport.SQL_MM_errors_zunb_cost(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "25":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Costingflagset(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "26":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sobsk30_No_Bom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "27":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sobsk30_No_F_X(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "28":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Procurtyp_Costnglot(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "29":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Costing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "30":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Cost_lot_size_not_less_price_unit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "31":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Size_Price(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "32":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Lotsize_To_Basmg(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "33":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Dislsfx_No_Bstfe(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "34":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Valclass_Stprs(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "35":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mat_Type_Hawa(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "36":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mat_Type_Verp(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "37":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mat_Type_Fert(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "38":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mat_Type_Halb(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "39":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mat_Type_Roh(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "40":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_fert_procF_nospt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "41":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_AC_FC_FERT_HAWA_ValClass_SPT_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "42":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_AC_FC_FERT_ValClass_SPT_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "43":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_halb_procF_nospt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "44":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_AC_FC_HALB_SPT_ValClass_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "45":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_AC_FC_HALB_ValClass_SPT_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "46":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_roh_procF_spt30(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "47":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_roh_profit_1799(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "48":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_roh_profit_3199(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "49":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_roh_profit_notbe_1799(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "50":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_verp_profit_1810(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "51":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_verp_profit_3510(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "52":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_procE_bklas(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "53":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_procF_bklas(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "54":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_nospt_bklas(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "55":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_spt_bklas(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "56":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_spt30_bklas(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "57":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_sptnot30_orign_not1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "58":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_spt30_orign_1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "59":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_prctrNotSameAcrossPlants(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "60":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_prctrNotSameAcrossMaterialRoot(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "61":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_prctrNotSameAcrossMaterialRootAll(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "62":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_AC_PRCTR_Invalid_ZTMM_ITEM_PROFIT(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "63":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_AC_NCOST_required(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "64":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Profit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "65":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mpr_Hb(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "66":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mpr_Vb(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "67":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Mpr_Z1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "68":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_ReorderPoint_Populated(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "69":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_mpr_type_planning(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "70":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Dismm_Othersbad(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "71":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Dispo_Othersbad(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "72":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Beskz_Othersbad(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "73":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_Missing_Required_Fields(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "74":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Procrument_Nt_E_F_X(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "75":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Dislsfx_No_Bstfe(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "76":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sobsl30_Sobsk30(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "77":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sobsl30_Beskz(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "78":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sobsl30_No_Bom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "79":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Beskzf_Bad_Losgr(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "80":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Beskz_F_No_Lgfsb(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "81":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Beskz_E_No_Lgpro(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "82":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Proc_E_No_Bom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "83":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_BESKZ_E_AvailCheck_Not_Z1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "84":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_BESKZ_E_WZEIT_Required(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "85":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_BESKZ_E_PLIFZ_WEBAZ_Required(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "86":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_BESKZ_F_STPC_30_AvailCheck_Not_Z1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "87":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Lgpro_No_Mard(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "88":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Lgfsb_No_Mard(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "89":
                                DataTableList.Add(mmauditingreport.SQL_MM_check_mrp_lt_siz(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "90":
                                DataTableList.Add(mmauditingreport.SQL_MM_check_mrp_lt_siz2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "91":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_DISLS_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "92":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_MinMax_LotSize(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "93":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_MaxLotSize_LessThan_RoundingValue(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "94":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_SHFLG_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "95":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_SHFLG_missing_SHZET(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "96":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_MAABC_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "97":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_BSTRF_LotSize_FX(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "98":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRP_SOBSL_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "99":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRPAREA_SOBSL_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "100":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MRPAREA_MRPPP_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "101":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Purch_Valkey_Req(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "102":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Qmpur_No_Ssqss(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "103":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Qmat_No_Qmview(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "104":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_no_ssqss(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "105":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_no_kzdkz(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "106":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_no_qmata(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "107":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_FT_STAWN_CommodityCode_NotInSAP(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "108":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_FT_STAWN_CommodityCode_MissingMARM(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "109":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_FT_IncorrectData_US_ExportingPlants(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "110":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_FT_STAWN_NotValid_ZTCOMCOD(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "111":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_FT_STAWN_NotSameAcrossMaterialRoot(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "112":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_FT_Legal_EMBGR_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "113":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sales_Notax(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "114":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_SALES_Missing_Tax(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "115":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Sales_Missing_Required(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "116":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Profitmarm(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "117":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Vrkme_No_Marm(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "118":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Tax_Nosales(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "119":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Salesuom_Matches_Base(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "120":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_SALES_Tax_US_Missing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "121":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_SALES_GEWEI_Missing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "122":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_SALES_LADGR_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "123":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_SALES_MTVFP_invalid_missing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "124":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Ausp(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "125":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Class911(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "126":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Class912(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "127":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Class952(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "128":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Class954(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "129":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_prfrq_no_953(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "130":
                                DataTableList.Add(mmauditingreport.SQL_MM_errors_no_class914(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "131":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Bad_Itm_Fam(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "132":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Bad_Itm_Num(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "133":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Bad_Pack_Code(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "134":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Bad_Label_Code(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "135":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_bad_subsell(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "136":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_no_class898(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "137":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_no_class909(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "138":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_no_class913(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "139":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_no_class1152(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "140":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_no_class1152_R05(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "141":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Class_Mult_Chars(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "142":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_RecycleInd245(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "143":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_CLASS_Z_GLOBAL_value_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "144":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Profit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "145":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Has_Batch(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "146":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_No_Batch(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "147":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_No_Batch_Expires(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "148":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_No_Batch_Tsl(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "149":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_No_Batch_Prfrq(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "150":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_No_Wm_Sloc100(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "151":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Ausme_No_Marm(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "152":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Unit_Issue_Eq_Base_Uom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "153":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_r10_expire(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "154":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_r10_batchmgt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "155":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_no_prfrq(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "156":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Etifo_Etiar(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "157":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_StoreTempNotSameAcrossPlants(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "158":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_StoreTemp_Required_MTART(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "159":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_StoreTemp_Invalid_Combo(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "160":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_StoreTemp_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "161":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_LGPRO_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "162":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_LGFSB_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "163":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_LGORT_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "164":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_STORE_ABCIN_Invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "165":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Wm_No_Uom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "166":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Wm_No_Mard(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "167":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Missing_Storage(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "168":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Uoi_Wmunit_Proppuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "169":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_uom_base_wm_or_issue(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "170":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM_LTKZA_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "171":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM_LTKZE_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "172":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM_LVSME_same_as_BaseUoM(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "173":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM_LGBKZ_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "174":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM_LETYx_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "175":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM_LHMG1_required(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "176":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WM2_LGPLA_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "177":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_ea_mia(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "178":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_aiuuom_mia(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "179":
                                DataTableList.Add(mmauditingreport.SQL_MM_Audit_Multiple_Active_Units(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "180":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_st_mia(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "181":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_meins_not_ea_no_altuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "182":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_planned_no_st_altuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "183":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_planned_sold_no_st_altuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "184":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_planned_sold_meins_not_ea_no_altuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "185":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MARM_LabelClaim_Missing_ZTMMLBLCLAIM_UOM(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "186":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Additional_Eancat(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "187":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_MARM_MEINH_InvalidValue(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "188":
                                DataTableList.Add(mmauditingreport.SQL_MM_error_ccycle1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "189":
                                DataTableList.Add(mmauditingreport.SQL_MM_error_ccycle2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "190":
                                DataTableList.Add(mmauditingreport.SQL_MM_error_ccycle3(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "191":
                                DataTableList.Add(mmauditingreport.SQL_MM_error_ccycle_missing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "192":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_CCycle_PRVBE_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "193":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_CCycle_LGPLA_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "194":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_CCycle_LGPLA_LGTYP_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "195":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WS_UEETO_UEETK_OverDeliveryLimit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "196":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WS_ProdSchedProfile_Missing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "197":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WS_ProdSchedProfile_populated(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "198":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_WS_FRTME_populated(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "199":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_zcountry1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "200":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_zcountry3(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "201":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_zcountry2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "202":
                                DataTableList.Add(mmauditingreport.SQL_MM_Errors_Inmarc_Nt_Matnr_Ref(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "203":
                                DataTableList.Add(mmauditingreport.SQL_MM_Config_Check_MRP_Controller(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "204":
                                //DataTableList.Add(mmauditingreport.SQL_MM_audit_matnr_flaged_delete(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "205":
                                DataTableList.Add(mmauditingreport.SQL_MM_audit_DDT_spt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes MMAuditing Internal Switch Case
                    } // Closes For loop for MMAuditing
                    break;

                case "MMVerificationGlobalData":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_basicdata(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_descriptions(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_descrip_english(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_storageglobal(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_storageglobal_nomard(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_altuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_altean(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_proportional(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_jdnet(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_basictext(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_inspecttext(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_internaltext(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_purchtext(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_presentation(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "15":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_prodnum(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "16":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_pkg_size(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "17":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_label(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "18":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_subselling(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "19":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_matuse(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "20":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_galenic(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "21":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_submole(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "22":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_molecule(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_brand(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "24":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_dosage_strength(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "25":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_dosage_stren_uom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "26":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_strength_comp(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "27":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_dose_form(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "28":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_pack_format(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "29":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_pack_type(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "30":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_var_potency(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "31":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_manuf_date(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "32":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_expire_date(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "34":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_sub_inspect(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "35":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_animal(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "36":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_bse_free(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "37":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_bulk_recycle(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "38":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_pharma_code(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "39":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_printed(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "40":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_fill_qty(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "41":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_fill_qtyuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "42":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_cas(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "43":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_serial_num(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "44":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_special_security(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "45":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_stren_mole(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "46":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_init_retest_trigger(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "47":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_msds_title(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "48":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_retention_sample(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "49":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_FINISHED_PACKAGE_QUANTITY(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "50":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_FINISHED_PACKAGE_UOM(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "51":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_PACK_ATTRRIBUTE_1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "52":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_PACK_ATTRRIBUTE_2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "53":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_CONTRACT_MFG_ORDER_TYPE(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "54":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_STO_TMP_CONDITION_REGISTERED(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "55":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_RES_SAMP_DISC_RULE(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "56":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_EXP_DATE_POTENCY(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "57":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_BATCH_RELEASE_LIMIT(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "58":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_Z_ACTIVITY_FOR_NU(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "59":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_global_all(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "60":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_us_oldmatnr(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "61":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_us_controlled(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "62":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_us_overtol(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "63":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_us_undertol(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "64":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_us_chars(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "65":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_acquisition(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "66":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_multikit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "67":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_contents(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "68":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_stren1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "69":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_strenuom1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "70":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_form1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "71":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_stren2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "72":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_strenuom2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "73":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_form2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "74":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_stren3(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "75":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_strenuom3(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "76":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_comp_form3(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "77":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_total_stren(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "78":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_total_strenuom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "79":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_z_total_form(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "80":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_waste_presentation(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "81":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_waste_prodnum(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "82":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_waste_genarea(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "83":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_waste_dispcode(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "84":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_family(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "85":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_reorder(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "86":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_location(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "87":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_regulatory(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "88":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_shopping(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "89":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_territory(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "90":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_priceunit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "91":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_promo_packsize(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "92":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_class2_expire_date(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "93":
                                DataTableList.Add(mmverificationglobaldatareport.SQL_MM_extract_class2_mfg_date(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes MMVerificationGlobalData Internal Switch Case
                    } // Closes For loop for MMVerificationGlobalData
                    break;

                case "MMVerificationPlantData":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_accounting1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_costing1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_foreigntradeImport(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_foreigntradeExport(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_mrp1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_mrp2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_mrp3(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_mrp4(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_mrp_all4(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_mrparea(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_purchasing(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_qm(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_qmverify(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_storageloc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "15":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_storageplant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "16":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_storageverify(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "17":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_worksched(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "18":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_tarrif(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "19":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_legalcontrol(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "20":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_zcountry(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "21":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_basic_by_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "22":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_descrip_eng_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_altuom_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "24":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_prctr(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "25":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_storageglobal_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "26":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_tragr_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "27":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_class_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "28":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_aiU_profit(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "29":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_acct_cost(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "30":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_format_ids_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "31":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_us_chars_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "32":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_z_global_plant(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "33":
                                DataTableList.Add(mmverificationplantdatareport.SQL_MM_extract_z_global_plant_financial(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes MMVerificationPlantData Internal Switch Case
                    } // Closes For loop for MMVerificationPlantData
                    break;

                case "MMVerificationSalesData":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(mmverificationsalesdatareport.SQL_MM_extract_salesgen(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(mmverificationsalesdatareport.SQL_MM_extract_salesorg1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(mmverificationsalesdatareport.SQL_MM_extract_salesorg2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(mmverificationsalesdatareport.SQL_MM_extract_taxes(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(mmverificationsalesdatareport.SQL_MM_extract_salestext(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes MMVerificationSalesData Internal Switch Case
                    } // Closes For loop for MMVerificationSalesData
                    break;

                case "MMVerificationWarehouseData":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(mmverificationwarehousedatareport.SQL_MM_extract_wms_Warehouse1(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(mmverificationwarehousedatareport.SQL_MM_extract_wms_Warehouse2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(mmverificationwarehousedatareport.SQL_MM_extract_wms_control_cycles(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes MMVerificationWarehouseData Internal Switch Case
                    } // Closes For loop for MMVerificationWarehouseData
                    break;

                case "APOAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(apoauditingreport.SQL_APO_APO_vs_MM(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(apoauditingreport.SQL_APO_APO_SNP_STH_vs_PDT(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes APOAuditing Internal Switch Case
                    } // Closes For loop for APOAuditing
                    break;

                case "APOVerification":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(apoverificationreport.SQL_APO_verfication_products(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(apoverificationreport.SQL_APO_verfication_Setup_matrix(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(apoverificationreport.SQL_APO_verfication_Transitions(DDRSessionEntity.Current.checkcount[i]));
                                break;                            
                            default:
                                break;
                        } // Closes APOVerification Internal Switch Case
                    } // Closes For loop for APOVerification
                    break;

                case "BOMAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_Header_UoM_not_Base(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_idnrk_not_exist(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(bomauditingreport.SQL_BOM_aa_audit_BOMHeaderNonMatchingZtext(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BOMDetail_Recursive(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(bomauditingreport.SQL_BOM_aa_error_BOMDetail_Comma_In_Quantity(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BOMDetail_DecimalLength(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BOMHeader_DecimalLength(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_UnitOfMeasure(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_idnrkausme(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BulkComponentWithStorLoc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BulkComponentWithSupplyArea(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_CostingBOMwithIssueStorLoc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_CostingBOMwithSupplyArea(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_SupplyAreaWithoutStorLoc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "15":
                                DataTableList.Add(bomauditingreport.SQL_BOM_audit_size_compare(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "16":
                                DataTableList.Add(bomauditingreport.SQL_BOM_audit_sortf_ce(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "17":
                                DataTableList.Add(bomauditingreport.SQL_BOM_audit_BOM_Comp_LGORT_missing_MARD(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "18":
                                DataTableList.Add(bomauditingreport.SQL_BOM_audit_BOM_Comp_LGORT_invalid(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "19":
                                DataTableList.Add(bomauditingreport.SQL_BOM_audit_BOM_Head_missing_Details(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "20":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BomComponentMaterialStatGbl(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "21":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BomComponentMaterialStatPlt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "22":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BomHeaderMaterialStatGbl(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(bomauditingreport.SQL_BOM_errors_BomHeaderMaterialStatPlt(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes BOMAuditing Internal Switch Case
                    } // Closes For loop for BOMAuditing
                    break;

                case "BOMVerification":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(bomverificationreport.SQL_BOM_extract_header(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(bomverificationreport.SQL_BOM_extract_details(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(bomverificationreport.SQL_BOM_extract_subdetails(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(bomverificationreport.SQL_BOM_extract_hd(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes BOMVerification Internal Switch Case
                    } // Closes For loop for BOMVerification
                    break;

                case "ConsistencyChecksBOMConsistency":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(consistencychecksbomconsistencyreport.SQL_Consistency_bom_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(consistencychecksbomconsistencyreport.SQL_Consistency_extract_hd(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(consistencychecksbomconsistencyreport.SQL_Consistency_R10_bom_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes ConsistencyChecksBOMConsistency Internal Switch Case
                    } // Closes For loop for ConsistencyChecksBOMConsistency
                    break;

                case "ConsistencyChecksMMClashes":
                    // Some of these reports are identical with just one minor variable change.  In the old ColdFusion, they were all
                    // written out separately.  It took significantly less code to write them with variables instead.  These cases
                    // are where multiple reports are pulled for a single "case", i.e. Case "2".
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_mara_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_marm_clash(DDRSessionEntity.Current.checkcount[i] + " UMREN", "umren"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_marm_clash(DDRSessionEntity.Current.checkcount[i] + " UMREZ", "umrez"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_marm_clash(DDRSessionEntity.Current.checkcount[i] + " LAENG", "laeng"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_marm_clash(DDRSessionEntity.Current.checkcount[i] + " BREIT", "breit"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_marm_clash(DDRSessionEntity.Current.checkcount[i] + " HOEHE", "hoehe"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_marm_clash(DDRSessionEntity.Current.checkcount[i] + " MEABM", "meabm"));
                                break;
                            case "3":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_makt_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_mean_clash(DDRSessionEntity.Current.checkcount[i] + " EANTP", "eantp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_mean_clash(DDRSessionEntity.Current.checkcount[i] + " HPEAN", "eantp"));
                                break;
                            case "5":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_mlan_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_prounit_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_class_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_mrsl_tsl_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_format_id_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_Acct_Cost_clash_a(DDRSessionEntity.Current.checkcount[i] + " Accounting 1: Valueation Type (BWTAR)", "bwtar"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_Acct_Cost_clash_a(DDRSessionEntity.Current.checkcount[i] + " Accounting 1: Price Unit (PEINH)", "peinh"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_Acct_Cost_clash_a(DDRSessionEntity.Current.checkcount[i] + " Accounting 1: Standard Price (STPRS)", "stprs"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_Acct_Cost_clash_a(DDRSessionEntity.Current.checkcount[i] + " Accounting 1: Valuation Class (BKLAS)", "bklas"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_Acct_Cost_clash_losgr(DDRSessionEntity.Current.checkcount[i] + " Costing 1: Costing Lot Size (LOSGR)", "losgr"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_Acct_Cost_clash_sobsk(DDRSessionEntity.Current.checkcount[i] + " Costing 1: SpecProc TypeFor Costing (SOBSK)", "sobsk"));
                                break;
                            case "11":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_AltUoM_clash(DDRSessionEntity.Current.checkcount[i] + " Alt Unit of Measure: X (UMREN)", "umren"));                                 
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_AltUoM_clash(DDRSessionEntity.Current.checkcount[i] + " Alt Unit of Measure: Y (UMREZ)", "umrez"));
                                break;
                            case "12":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Plant Specific Material Status (MMSTA)", "mmsta"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 2: Valid From (MMSTD)", "mmstd"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: MRP Group (DISGR)", "disgr"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: MRP Type (DISMM)", "dismm"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: ReOrder Point (MINBE)", "minbe"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Planning Time Fence (FXHOR)", "fxhor"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: MRP Controller (DISPO)", "dispo"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Lot Size (DISLS)", "disls"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Fixed Lot Size(BSTFE)", "bstfe"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Max Stock Level (MABST)", "mabst"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Assembly Scrap Percent (AUSSS)", "ausss"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Min Lot Size (BSTMI)", "bstmi"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Max Lot Size (BSTMA)", "bstma"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Rounding Profile (RDPRF)", "rdprf"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Rounding Value (BSTRF)", "bstrf"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Procurement Type (BESKZ)", "beskz"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Batch Entry (KZECH)", "kzech"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Special Procurement Type (SOBSL)", "sobsl"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Issue Storage Location(LGPRO)", "lgpro"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Storage Loc fo EP (LGFSB)", "lgfsb"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: JIT Sched Indc (FABKZ)", "fabkz"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Bulk Material (SCHGT)", "schgt"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Co-Product (KZKUP)", "kzkup"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: In-House Production Time (DZEIT)", "dzeit"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Planned Delivery Time (PLIFZ)", "plifz"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: GR Processing Time (WEBAZ)", "webaz"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Planning Calendar (MRPPP)", "mrppp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: SchedMargin Key (FHORI)", "fhori"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Safety Stock (EISBE)", "eisbe"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Coverage Profile (RWPRO)", "rwpro"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Safety Time Ind (SHFLG)", "shflg"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Safety Time (SHZET)", "shzet"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Strategy Group (STRGR)", "strgr"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Availability Check (MTVFP)", "mtvfp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Total Repl Lead Time (WZEIT)", "wzeit"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_MRP_clash(DDRSessionEntity.Current.checkcount[i] + " MRP 1: Selection Method (ALTSL)", "altsl"));
                                break;
                            case "13":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_PURCH_clash_a(DDRSessionEntity.Current.checkcount[i] + " Purchasing:  Purchasing Value Key (ekwsl)", "ekwsl"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_PURCH_clash_b(DDRSessionEntity.Current.checkcount[i] + " Purchasing:  Purchasing Group (ekgrp)", "ekgrp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_PURCH_clash_b(DDRSessionEntity.Current.checkcount[i] + " Purchasing:  Source List (kordb)", "kordb"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_PURCH_clash_b(DDRSessionEntity.Current.checkcount[i] + " Purchasing:  Autom. PO (kautb)", "kautb"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_PURCH_clash_b(DDRSessionEntity.Current.checkcount[i] + " Purchasing:  Critical Part (kzkri)", "kzkri"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_PURCH_clash_b(DDRSessionEntity.Current.checkcount[i] + " Purchasing:  JIT sched. Indicator (fabkz)", "fabkz"));
                                break;
                            case "14":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_prfrq(DDRSessionEntity.Current.checkcount[i] + " QM:  Inspection Interval (prfrq)", "prfrq"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_qmata(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Material Auth (qmata)", "qmata"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_kzdkz(DDRSessionEntity.Current.checkcount[i] + " QM:  Documentation Required (kzdkz)", "kzdkz"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_mmsta(DDRSessionEntity.Current.checkcount[i] + " QM:  Plant Specific Material Status (mmsta)", "mmsta"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_mmstd(DDRSessionEntity.Current.checkcount[i] + " QM:  Valid From (mmstd)", "mmstd"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_qmpur(DDRSessionEntity.Current.checkcount[i] + " QM:  QM in Proc Active (qmpur)", "qmpur"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_clash_ssqss(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Control Key (ssqss)", "ssqss"));
                                break;
                            //case "15":                               
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Inspection Interval (prfrq)", "prfrq"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Material Auth (qmata)", "qmata"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Documentation Required (kzdkz)", "kzdkz"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Plant Specific Material Status (mmsta)", "mmsta"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Valid From (mmstd)", "mmstd"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM in Proc Active (qmpur)", "qmpur"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z131_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Control Key (ssqss)", "ssqss"));
                            //    break;
                            //case "16":                                
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Inspection Interval (prfrq)", "prfrq"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Material Auth (qmata)", "qmata"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Documentation Required (kzdkz)", "kzdkz"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Plant Specific Material Status (mmsta)", "mmsta"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Valid From (mmstd)", "mmstd"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM in Proc Active (qmpur)", "qmpur"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z413_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Control Key (ssqss)", "ssqss"));
                            //    break;
                            //case "17":                                
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Inspection Interval (prfrq)", "prfrq"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Material Auth (qmata)", "qmata"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Documentation Required (kzdkz)", "kzdkz"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Plant Specific Material Status (mmsta)", "mmsta"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Valid From (mmstd)", "mmstd"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM in Proc Active (qmpur)", "qmpur"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z314_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Control Key (ssqss)", "ssqss"));
                            //    break;
                            //case "18":                                
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Inspection Interval (prfrq)", "prfrq"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Material Auth (qmata)", "qmata"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Documentation Required (kzdkz)", "kzdkz"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Plant Specific Material Status (mmsta)", "mmsta"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  Valid From (mmstd)", "mmstd"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM in Proc Active (qmpur)", "qmpur"));
                            //    DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_QM_Z004_clash(DDRSessionEntity.Current.checkcount[i] + " QM:  QM Control Key (ssqss)", "ssqss"));
                            //    break;
                            case "19":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_mhdhb(DDRSessionEntity.Current.checkcount[i] + " Storage:  Total Shelf Life (mhdhb)", "mhdhb"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_mhdrz(DDRSessionEntity.Current.checkcount[i] + " Storage:  Min Remaining Shelf Life (mhdrz)", "mhdrz"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_ausme(DDRSessionEntity.Current.checkcount[i] + " Storage:  Unit of Issue (ausme)", "ausme"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_etifo(DDRSessionEntity.Current.checkcount[i] + " Storage:  Label Form (etifo)", "etifo"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_etiar(DDRSessionEntity.Current.checkcount[i] + " Storage:  Label Type (etiar)", "etiar"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_abcin(DDRSessionEntity.Current.checkcount[i] + " Storage:  CC Phys. Inv. Ind (abcin)", "abcin"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_ccfix(DDRSessionEntity.Current.checkcount[i] + " Storage:  CC Fixed (ccfix)", "ccfix"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_xchpf(DDRSessionEntity.Current.checkcount[i] + " Storage:  Batch Mgt (xchpf)", "xchpf"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_mhdlp(DDRSessionEntity.Current.checkcount[i] + " Storage:  Storage Percentage (mhdlp)", "mhdlp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_xmcng(DDRSessionEntity.Current.checkcount[i] + " Storage:  Neg. Stocks in Plant (xmcng)", "xmcng"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_loggr(DDRSessionEntity.Current.checkcount[i] + " Storage:  Logistics Handling Group (loggr)", "loggr"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_sernp(DDRSessionEntity.Current.checkcount[i] + " Storage:  Serial Num Profile (sernp)", "sernp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_tempb2(DDRSessionEntity.Current.checkcount[i] + " Storage:  Temp Conditions Ind (tempb2)", "tempb2"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_STOR_clash_raube2(DDRSessionEntity.Current.checkcount[i] + " Storage:  Storage Conditions (raube2)", "raube2"));
                                break;
                            case "20":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_TAX_clash(DDRSessionEntity.Current.checkcount[i] + " Sales Org 1: Tax Classification1 (taxm1)", "taxm1"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_TAX_clash(DDRSessionEntity.Current.checkcount[i] + " Sales Org 1: Tax Classification2 (taxm2)", "taxm2"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_TAX_clash(DDRSessionEntity.Current.checkcount[i] + " Sales Org 1: Tax Classification3 (taxm3)", "taxm3"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_TAX_clash(DDRSessionEntity.Current.checkcount[i] + " Sales Org 1: Tax Classification4 (taxm4)", "taxm4"));
                                break;
                            case "21":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Production Unit (frtme)", "frtme"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Production Scheduler (fevor)", "fevor"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Production Schedule Profile (sfcpf)", "sfcpf"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Material Pricing Group (matgr)", "matgr"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Batch Entry (kzech)", "kzech"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Underdelivery Tolerance (uneto)", "uneto"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Overdelivery Tolerance (ueeto)", "ueeto"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Underdelivery Allowed (ueetk)", "ueetk"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mm_WKSCH_clash(DDRSessionEntity.Current.checkcount[i] + " Work Scheduling:  Base Qty (basmg)", "basmg"));
                                break;
                            case "22":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_mara_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_makt_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "24":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash(DDRSessionEntity.Current.checkcount[i] + " X(Denominator)(UMREN)", "umren"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash(DDRSessionEntity.Current.checkcount[i] + " Y(Numerator)(UMREZ)", "umrez"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash(DDRSessionEntity.Current.checkcount[i] + " Length(LAENG)", "laeng"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash(DDRSessionEntity.Current.checkcount[i] + " Width(BREIT)", "breit"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash(DDRSessionEntity.Current.checkcount[i] + " Height(HOEHE)", "hoehe"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash_meabm(DDRSessionEntity.Current.checkcount[i] + " Unit of dimension for length/width/height(MEABM)", "meamb"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash_sapid(DDRSessionEntity.Current.checkcount[i] + " Found in GPR, but sapid not X in DDT"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash_sapid1(DDRSessionEntity.Current.checkcount[i] + " SAPID in DDT is X, but record not found in GPR"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash_atwrt(DDRSessionEntity.Current.checkcount[i] + " Potency - DDT not match GPR"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash_poten(DDRSessionEntity.Current.checkcount[i] + " POTENCY - in GPR, not found in DDT"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_marm_clash_poten1(DDRSessionEntity.Current.checkcount[i] + " POTENCY - Found in DDT with sapid X, not found in GPR"));                                
                                break;
                            case "25":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_mean_clash(DDRSessionEntity.Current.checkcount[i] + " EAN Category (EANTP)", "eantp"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_mean_clash(DDRSessionEntity.Current.checkcount[i] + " Main EAN Ind (HPEAN)", "hpean"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_mean_clash_sapidx(DDRSessionEntity.Current.checkcount[i] + " In DDT with sapid X, not found in GPR"));
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_mean_clash_gpr(DDRSessionEntity.Current.checkcount[i]+ " In GPR, not found in DDT"));
                                break;
                            case "26":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_all_ausp_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "27":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_format_id_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "28":
                                DataTableList.Add(consistencychecksmmclashesreport.SQL_Consistency_mrsl_tsl_clash(DDRSessionEntity.Current.checkcount[i]));
                                break;

                            default:
                                break;
                        } // Closes ConsistencyChecksMMClashes Internal Switch Case
                    } // Closes For loop for ConsistencyChecksMMClashes
                    break;

                case "ConsistencyChecksMMConsistency":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_gen_mara(DDRSessionEntity.Current.checkcount[i] + " Error:  Material number contains non-alphanumeric characters"));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_mrp_chk(DDRSessionEntity.Current.checkcount[i] + " Warning:  MRP not fully defined (dismm)"));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_qm_defined(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_account_ck(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_no_mbew(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_cost_ck(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_gen_type(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_chk_mbew(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_ausme_prop(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_vrkme_prop(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_no_etiar(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_etiar_etifo(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_roh_matkl(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_halb_alt(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_halb_matkl(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_fert_nosales(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_fert_mhdrz(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_fert_sales(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_fert_st(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_work_XO(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_mat_sold(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_purch_plan(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_wm_no0100(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_nowm_0100(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_purch(DDRSessionEntity.Current.checkcount[i] + ""));
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_mm_check_manuf(DDRSessionEntity.Current.checkcount[i] + ""));
                                break;
                            case "2":
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_qmat_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                //DataTableList.Add(consistencychecksmmconsistencyreport.SQL_Consistency_R10_class_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes ConsistencyChecksMMConsistency Internal Switch Case
                    } // Closes For loop for ConsistencyChecksMMConsistency
                    break;

                case "ConsistencyChecksRecipeConsistency":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(consistencychecksrecipeconsistencyreport.SQL_Consistency_recipe_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(consistencychecksrecipeconsistencyreport.SQL_Consistency_R10_recipe_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes ConsistencyChecksRecipeConsistency Internal Switch Case
                    } // Closes For loop for ConsistencyChecksRecipeConsistency
                    break;

                case "ConsistencyChecksResourceConsistency":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(consistencychecksresourceconsistencyreport.SQL_Consistency_resource_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(consistencychecksresourceconsistencyreport.SQL_Consistency_R10_resource_check(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes ConsistencyChecksResourceConsistency Internal Switch Case
                    } // Closes For loop for ConsistencyChecksResourceConsistency
                    break;

                case "InventoryAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY105(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY64(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY16(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY20(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY217(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY61(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY62(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY63(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY24(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY111(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY228(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY83(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY01(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY02(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "15":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY04(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "16":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY55(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "17":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY05(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "18":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY06(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "19":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY08(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "20":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY223(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "21":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY57(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "22":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY10(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY11(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "24":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY144(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "25":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY113(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "26":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY109(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "27":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY110(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "28":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY112(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "29":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY118(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "30":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY119(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "31":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY212(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "32":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY214(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "33":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY215(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "34":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY247(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "35":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY257(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "36":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY80(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "37":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY151(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "38":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY81(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "39":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY152(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "40":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY82(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "41":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY98(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "42":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY220(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "43":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY52(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "44":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY99(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "45":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY114(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "46":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY115(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "47":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY209(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "48":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY29(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "49":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY30(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "50":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY108(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "51":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY120(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "52":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY261(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "53":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY31(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "54":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY32(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "55":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY33(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "56":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY35(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "57":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY34(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "58":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY36(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "59":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY258(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "60":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY259(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "61":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY260(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "62":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY13(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "63":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY14(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "64":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY216(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "65":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY15(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "66":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY54(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "67":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY72(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "68":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY18(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "69":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY27(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "70":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY03(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "71":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY07(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "72":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY09(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "73":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY12(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "74":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY17(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "75":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY145(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "76":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY245(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "77":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY251(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "78":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY252(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "79":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY253(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "80":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY254(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "81":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY255(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "82":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY256(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "83":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY53(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "84":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY100(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "85":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY101(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "86":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY102(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "87":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY103(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "88":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY21(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "89":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY85(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "90":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY22(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "91":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY23(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "92":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY78(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "93":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY25(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "94":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY26(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "95":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY121(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "96":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY122(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "97":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY123(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "98":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY124(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "99":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY125(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "100":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY126(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "101":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY127(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "102":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY128(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "103":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY130(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "104":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY131(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "105":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY149(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "106":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY244(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "107":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY229(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "108":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY230(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "109":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY231(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "110":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY232(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "111":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY233(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "112":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY234(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "113":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY235(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "114":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY236(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "115":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY116(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "116":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY117(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "117":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY65(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "118":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY218(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "119":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY68(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "120":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY67(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "121":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY87(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "122":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY71(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "123":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY70(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "124":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY225(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "125":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY76(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "126":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY86(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "127":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY59(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "128":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY94(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "129":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY104(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "130":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY28(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "131":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY249(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "132":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY262(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "133":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_inv_wm_strategy_audits(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "134":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY175(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "135":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY176(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "136":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY177(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "137":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY178(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "138":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY179(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "139":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY207(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "140":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY180(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "141":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY181(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "142":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY182(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "143":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY183(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "144":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY184(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "145":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY185(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "146":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY143(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "147":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY156(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "148":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY158(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "149":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY161(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "150":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY164(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "151":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY167(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "152":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY165(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "153":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY211(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "154":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY224(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "155":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY210(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "156":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY213(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "157":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY195(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "158":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY186(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "159":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY187(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "160":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY188(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "161":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY206(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "162":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY196(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "163":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY219(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "164":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY208(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "165":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY197(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "166":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY198(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "167":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY199(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "168":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY200(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "169":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY201(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "170":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY226(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "171":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY202(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "172":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY203(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "173":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY204(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "174":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY205(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "175":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY87(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "176":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY189(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "177":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY190(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "178":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY191(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "179":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY192(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "180":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY193(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "181":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY194(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "182":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY240(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "183":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY241(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "184":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY242(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "185":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY243(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "186":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY246(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "187":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY248(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "188":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY250(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "189":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY03(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "190":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY07(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "191":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY09(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "192":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY12(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "193":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY37(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "194":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY17(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "195":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY96(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "196":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY54(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "197":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY91(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "198":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY40(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "199":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY43(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "200":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY46(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "201":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY145(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "202":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_inv_financial_verification_audits(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "203":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY_FYI_01(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "204":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY_FYI_02(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "205":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY_FYI_08(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "206":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY_FYI_03(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "207":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY_FYI_07(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "208":
                                DataTableList.Add(inventoryauditingreport.SQL_Inventory_QUERY_FYI_04(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes InventoryAuditing Internal Switch Case
                    } // Closes For loop for InventoryAuditing
                    break;

                case "InventoryVerification":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                //Cold Fusion File Missing     //DataTableList.Add(inventoryverificationreport.SQL_Inventory_integrated_z603(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes InventoryVerification Internal Switch Case
                    } // Closes For loop for InventoryVerification
                    break;

                case "QMAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_Group_counter(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_NO_QMView(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_QMView_without_Insp_Type(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_INSP(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_AUSP(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_INSP_INTERVAL(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(qmauditingreport.SQL_QM_QMSce_01(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(qmauditingreport.SQL_QM_QMSce_02(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(qmauditingreport.SQL_QM_QMSce_03(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(qmauditingreport.SQL_QM_QMSce_04(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(qmauditingreport.SQL_QM_QMSce_05(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_expires_no_09(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_prfrq_no_09(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_INHO_PROC(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "15":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_EXTN_PROC(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "16":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_BOTH_PROC(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "17":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_ProcE_no_04(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "18":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_ProcF_no_01(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "19":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_ProcX_no_04(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "20":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_ProcX_no_01(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "21":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_ProcE_not_0000(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "22":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_ProcF_not_0001(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "23":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_01_insplot_NB(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "24":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_08_insplot_NB(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "25":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_04_insplot_NX(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "26":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_04_insplot_NY(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "27":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_05_insplot_E2(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "28":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_05_insplot_EB(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "29":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_05_insplot_N2B(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "30":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_01_insplot_blank(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "31":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_04_insplot_blank(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "32":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_0105_insplot_blank(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "33":
                                DataTableList.Add(qmauditingreport.SQL_QM_QM_insptype_insplot_blank(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "34":
                                DataTableList.Add(qmauditingreport.SQL_QM_BOM_COMP_SORT_ACE(DDRSessionEntity.Current.checkcount[i]));
                                break;

                            default:
                                break;
                        } // Closes QMAuditing Internal Switch Case
                    } // Closes For loop for QMAuditing
                    break;

                case "QMVerification":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(qmverificationreport.SQL_QM_QM_insp_plan(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(qmverificationreport.SQL_QM_QM_insp_plan_alloc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(qmverificationreport.SQL_QM_QM_BSS(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(qmverificationreport.SQL_QM_extract_qm_insp(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(qmverificationreport.SQL_QM_QM_Qinfo(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(qmverificationreport.SQL_QM_QM_Qinfo_sap(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes QMVerification Internal Switch Case
                    } // Closes For loop for QMVerification
                    break;

                case "RecipesAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_rec_recipebadresource(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_no_version(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_version_no_bom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_version_no_worksched(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_version_no_alloc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_version_no_recipe(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_alloc_no_version(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_bomdetail_no_alloc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "9":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_no_allocations(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "10":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_relationship_bad(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "11":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_no_vge0x(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "12":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_relationship_uom(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "13":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_error_uom_not_base(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "14":
                                DataTableList.Add(recipesauditingreport.SQL_Recipes_audit_size_compare(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes RecipesAuditing Internal Switch Case
                    } // Closes For loop for RecipesAuditing
                    break;

                case "RecipesVerification":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_header(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_operations(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_secresource(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_oprelations(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_userfld(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_matalloc(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(recipesverificationreport.SQL_Recipes_extract_rec_version(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes RecipesVerification Internal Switch Case
                    } // Closes For loop for RecipesVerification
                    break;

                case "ResourcesAuditing":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(resourcesauditingreport.SQL_Resources_audit_Missing_KOSTL(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(resourcesauditingreport.SQL_Resources_audit_Missing_FORTN(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(resourcesauditingreport.SQL_Resources_audit_Missing_ResourceDescription(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(resourcesauditingreport.SQL_Resources_audit_KOSTL_wo_ActivityType_Linked(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes ResourcesAuditing Internal Switch Case
                    } // Closes For loop for ResourcesAuditing
                    break;

                case "ResourcesVerification":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_BasicData(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "2":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_DefaultValues(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "3":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_Capacities(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "4":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_CapacityHeader(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "5":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_Scheduling(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "6":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_CostCenters(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "7":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_ShortText(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            case "8":
                                DataTableList.Add(resourcesverificationreport.SQL_Resources_extract_DistinctCostCenters(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes ResourcesVerification Internal Switch Case
                    } // Closes For loop for ResourcesVerification
                    break;

                case "OtherAdminReport":
                    foreach (string i in DDRSessionEntity.Current.checkcount.Keys) // For each selected report (Stored in the session)
                    {
                        switch (i)
                        {
                            case "1":
                                DataTableList.Add(otheradminreport.SQL_Other_dbinfo(DDRSessionEntity.Current.checkcount[i]));
                                break;
                            default:
                                break;
                        } // Closes OtherAdminReport Internal Switch Case
                    } // Closes For loop for OtherAdminReport
                    break;

                default:
                    break;

            } // Closes Outer Switch Case

           

            // This function dynamically sets the Column Headers to a more user-friendly set of titles.
            // If there is no value specified for a header, then the program will default to the column header in the Oracle table.
            // I kept this as a set of if-statements instead of a switch case so that the program doesn't
            // break if there is an unexpected column found.

            // There can be NO repeats of original or new headers.  If the "User Friendly" header already exists for another 
            // original header, then simply add a space to make it different.  (No 2 columns in the same table can have the same identical header.)
            foreach (DataTable report in DataTableList)                
            {
                for (int column = 0; column < report.Columns.Count; column++)
                {
                    if (report.Columns[column].ColumnName == "abcin") report.Columns[column].ColumnName = "CC Phys. Inv Ind ";
                    if (report.Columns[column].ColumnName == "abwkz") report.Columns[column].ColumnName = "Devaluation Indicator ";
                    if (report.Columns[column].ColumnName == "ahdis ") report.Columns[column].ColumnName = "MRP dep. requirements ";
                    if (report.Columns[column].ColumnName == "ALNAL") report.Columns[column].ColumnName = "Recipe Number";
                    if (report.Columns[column].ColumnName == "altsl") report.Columns[column].ColumnName = "Selection Method";
                    if (report.Columns[column].ColumnName == "aplal") report.Columns[column].ColumnName = "Group Counter";
                    if (report.Columns[column].ColumnName == "atpkz") report.Columns[column].ColumnName = "Replacement Part ";
                    if (report.Columns[column].ColumnName == "ATWRT") report.Columns[column].ColumnName = "Promo";
                    if (report.Columns[column].ColumnName == "auftl ") report.Columns[column].ColumnName = "Splitting Indicator";
                    if (report.Columns[column].ColumnName == "aumng") report.Columns[column].ColumnName = "Min Order Qty ";
                    if (report.Columns[column].ColumnName == "AUSCH") report.Columns[column].ColumnName = "Component Scrap %";
                    if (report.Columns[column].ColumnName == "ausdt ") report.Columns[column].ColumnName = "Eff. -Out ";
                    if (report.Columns[column].ColumnName == "ausme") report.Columns[column].ColumnName = "Unit of Issue ";
                    if (report.Columns[column].ColumnName == "ausss ") report.Columns[column].ColumnName = "Assembly Scrap Percentage ";
                    if (report.Columns[column].ColumnName == "awsls") report.Columns[column].ColumnName = "Variance Key";
                    if (report.Columns[column].ColumnName == "basmg") report.Columns[column].ColumnName = "Base Quantity  ";
                    if (report.Columns[column].ColumnName == "bearz") report.Columns[column].ColumnName = "Processing Time";
                    if (report.Columns[column].ColumnName == "begru") report.Columns[column].ColumnName = "Authorization Group";
                    if (report.Columns[column].ColumnName == "behvo") report.Columns[column].ColumnName = "Container Requirements ";
                    if (report.Columns[column].ColumnName == "beskz ") report.Columns[column].ColumnName = "Procurement Type ";
                    if (report.Columns[column].ColumnName == "bismt") report.Columns[column].ColumnName = "Old Material Number";
                    if (report.Columns[column].ColumnName == "bklas") report.Columns[column].ColumnName = "Valuation Class ";
                    if (report.Columns[column].ColumnName == "BMENG") report.Columns[column].ColumnName = "Base Quantity";
                    if (report.Columns[column].ColumnName == "bonus") report.Columns[column].ColumnName = "Volume Rebate Group ";
                    if (report.Columns[column].ColumnName == "BREIT") report.Columns[column].ColumnName = "Width";
                    if (report.Columns[column].ColumnName == "brgew") report.Columns[column].ColumnName = "Gross Weight ";
                    if (report.Columns[column].ColumnName == "bstfe ") report.Columns[column].ColumnName = "Fixed Lot Size";
                    if (report.Columns[column].ColumnName == "BSTMA") report.Columns[column].ColumnName = "To Lot Size ";
                    if (report.Columns[column].ColumnName == "bstma ") report.Columns[column].ColumnName = "Maximum Lot Size";
                    if (report.Columns[column].ColumnName == "bstme ") report.Columns[column].ColumnName = "Order Unit ";
                    if (report.Columns[column].ColumnName == "BSTMI") report.Columns[column].ColumnName = "From Lot Size ";
                    if (report.Columns[column].ColumnName == "bstrf ") report.Columns[column].ColumnName = "Rounding Value";
                    if (report.Columns[column].ColumnName == "bwkey") report.Columns[column].ColumnName = "Plant";
                    if (report.Columns[column].ColumnName == "bwpei") report.Columns[column].ColumnName = "Price Unit";
                    if (report.Columns[column].ColumnName == "bwph1") report.Columns[column].ColumnName = "Commercial Price 2 ";
                    if (report.Columns[column].ColumnName == "bwprh") report.Columns[column].ColumnName = "Commercial Price 1 ";
                    if (report.Columns[column].ColumnName == "bwprs") report.Columns[column].ColumnName = "Tax Price 1";
                    if (report.Columns[column].ColumnName == "bwps1") report.Columns[column].ColumnName = "Tax Price 2";
                    if (report.Columns[column].ColumnName == "bwtar") report.Columns[column].ColumnName = "Valuation Type";
                    if (report.Columns[column].ColumnName == "c1032") report.Columns[column].ColumnName = "Pack (Type)";
                    if (report.Columns[column].ColumnName == "c1033") report.Columns[column].ColumnName = "Pack Format (Code)";
                    if (report.Columns[column].ColumnName == "c1110") report.Columns[column].ColumnName = "BSE Free Country Source";
                    if (report.Columns[column].ColumnName == "c1134") report.Columns[column].ColumnName = "Initial Manufactured Form";
                    if (report.Columns[column].ColumnName == "c1141") report.Columns[column].ColumnName = "Bulk and/or Recycle Indicator";
                    if (report.Columns[column].ColumnName == "c1142") report.Columns[column].ColumnName = "Dose Form";
                    if (report.Columns[column].ColumnName == "c1143") report.Columns[column].ColumnName = "Pharma Packing Code Identifier";
                    if (report.Columns[column].ColumnName == "c1148") report.Columns[column].ColumnName = "Material Use";
                    if (report.Columns[column].ColumnName == "c1152") report.Columns[column].ColumnName = "Strength Active Component";
                    if (report.Columns[column].ColumnName == "c1155") report.Columns[column].ColumnName = "Labeled Fill or Recon Volume";
                    if (report.Columns[column].ColumnName == "c1156") report.Columns[column].ColumnName = "Fill Qty UOM";
                    if (report.Columns[column].ColumnName == "c1166") report.Columns[column].ColumnName = "Chemical Abstract Number";
                    if (report.Columns[column].ColumnName == "c1167") report.Columns[column].ColumnName = "Lilly Serial Number";
                    if (report.Columns[column].ColumnName == "c1224") report.Columns[column].ColumnName = "LLY Special Security Substance";
                    if (report.Columns[column].ColumnName == "c1329") report.Columns[column].ColumnName = "Molecular Weight Ratio";
                    if (report.Columns[column].ColumnName == "c1372") report.Columns[column].ColumnName = "Lilly Number";
                    if (report.Columns[column].ColumnName == "c1486") report.Columns[column].ColumnName = "MSDS Title";
                    if (report.Columns[column].ColumnName == "c1570") report.Columns[column].ColumnName = "Initial Retest Calc Method";
                    if (report.Columns[column].ColumnName == "c1647") report.Columns[column].ColumnName = "Reserve Sample Storage Time";
                    if (report.Columns[column].ColumnName == "c1836") report.Columns[column].ColumnName = "Acquired Company";
                    if (report.Columns[column].ColumnName == "c1837") report.Columns[column].ColumnName = "Acquired Old Material Number";
                    if (report.Columns[column].ColumnName == "c1845") report.Columns[column].ColumnName = "Acquired Old Description 1";
                    if (report.Columns[column].ColumnName == "c1846") report.Columns[column].ColumnName = "Acquired Old Description 2";
                    if (report.Columns[column].ColumnName == "c1847") report.Columns[column].ColumnName = "Acquired Old Description 3";
                    if (report.Columns[column].ColumnName == "c1848") report.Columns[column].ColumnName = "Acquired Old Description 4";
                    if (report.Columns[column].ColumnName == "c1849") report.Columns[column].ColumnName = "Acquired Old Description 5";
                    if (report.Columns[column].ColumnName == "c1857") report.Columns[column].ColumnName = "Contract Mfg Order Type";
                    if (report.Columns[column].ColumnName == "c1928") report.Columns[column].ColumnName = "Cal Exp Date From Potency Test";
                    if (report.Columns[column].ColumnName == "c1944") report.Columns[column].ColumnName = "Reserve Sample Discard Rule";
                    if (report.Columns[column].ColumnName == "c2136") report.Columns[column].ColumnName = "Sto/Tmp Conditions Registered";
                    if (report.Columns[column].ColumnName == "c2812") report.Columns[column].ColumnName = "Activity for Computing NU";
                    if (report.Columns[column].ColumnName == "c898") report.Columns[column].ColumnName = "Brand Name";
                    if (report.Columns[column].ColumnName == "c900") report.Columns[column].ColumnName = "Dosage Strength Quantity";
                    if (report.Columns[column].ColumnName == "c901") report.Columns[column].ColumnName = "Dosage Strength Quantity UOM";
                    if (report.Columns[column].ColumnName == "c904") report.Columns[column].ColumnName = "Pack Total Count";
                    if (report.Columns[column].ColumnName == "c905") report.Columns[column].ColumnName = "Pack Total Count UOM";
                    if (report.Columns[column].ColumnName == "c908") report.Columns[column].ColumnName = "Label Code";
                    if (report.Columns[column].ColumnName == "c909") report.Columns[column].ColumnName = "Common Name (Molecule)";
                    if (report.Columns[column].ColumnName == "c910") report.Columns[column].ColumnName = "Pack Code (Package size)";
                    if (report.Columns[column].ColumnName == "c911") report.Columns[column].ColumnName = "Item Family (Presentation)";
                    if (report.Columns[column].ColumnName == "c912") report.Columns[column].ColumnName = "Item Number (Product Number)";
                    if (report.Columns[column].ColumnName == "c913") report.Columns[column].ColumnName = "Active Pharmaceutical Ingredient (Sub-molecule)";
                    if (report.Columns[column].ColumnName == "c914") report.Columns[column].ColumnName = "Sub-selling Market";
                    if (report.Columns[column].ColumnName == "c951") report.Columns[column].ColumnName = "Variable Potency";
                    if (report.Columns[column].ColumnName == "c952") report.Columns[column].ColumnName = "Expiration Date Format";
                    if (report.Columns[column].ColumnName == "c953") report.Columns[column].ColumnName = "Subsequent Inspection Interval";
                    if (report.Columns[column].ColumnName == "c954") report.Columns[column].ColumnName = "Manufacture Date Format";
                    if (report.Columns[column].ColumnName == "c955") report.Columns[column].ColumnName = "Animal Sourced Product";
                    if (report.Columns[column].ColumnName == "c957") report.Columns[column].ColumnName = "Printed/Non-printed (y/n)";
                    if (report.Columns[column].ColumnName == "casnr ") report.Columns[column].ColumnName = "CAS Number";
                    if (report.Columns[column].ColumnName == "cBLM") report.Columns[column].ColumnName = "Batch Release Overdue Limit";
                    if (report.Columns[column].ColumnName == "ccfix") report.Columns[column].ColumnName = "CC Fixed";
                    if (report.Columns[column].ColumnName == "cPack1") report.Columns[column].ColumnName = "Pack Attribute 1";
                    if (report.Columns[column].ColumnName == "cPack2") report.Columns[column].ColumnName = "Pack Attribute 2";
                    if (report.Columns[column].ColumnName == "disgr ") report.Columns[column].ColumnName = "MRP Group";
                    if (report.Columns[column].ColumnName == "disls ") report.Columns[column].ColumnName = "Lot Size";
                    if (report.Columns[column].ColumnName == "dismm ") report.Columns[column].ColumnName = "MRP Type";
                    if (report.Columns[column].ColumnName == "dispo ") report.Columns[column].ColumnName = "MRP Controller";
                    if (report.Columns[column].ColumnName == "dispr ") report.Columns[column].ColumnName = "MRP Profile";
                    if (report.Columns[column].ColumnName == "disst") report.Columns[column].ColumnName = "Low-Level Code";
                    if (report.Columns[column].ColumnName == "dplfs ") report.Columns[column].ColumnName = "Fair Share Rule ";
                    if (report.Columns[column].ColumnName == "dplho ") report.Columns[column].ColumnName = "Deployment Horizon ";
                    if (report.Columns[column].ColumnName == "dplpu ") report.Columns[column].ColumnName = "Push Distribution";
                    if (report.Columns[column].ColumnName == "dwerk") report.Columns[column].ColumnName = "Delivering Plant ";
                    if (report.Columns[column].ColumnName == "dzeit ") report.Columns[column].ColumnName = "In-house Production Time";
                    if (report.Columns[column].ColumnName == "EAN11") report.Columns[column].ColumnName = "EAN/UPC";
                    if (report.Columns[column].ColumnName == "eisbe ") report.Columns[column].ColumnName = "Safety Stock";
                    if (report.Columns[column].ColumnName == "eislo ") report.Columns[column].ColumnName = "Min Safety StockNot used and not available in db";
                    if (report.Columns[column].ColumnName == "ekalr") report.Columns[column].ColumnName = "With Quality Structure";
                    if (report.Columns[column].ColumnName == "ekgrp ") report.Columns[column].ColumnName = "Purchasing Group ";
                    if (report.Columns[column].ColumnName == "eklas") report.Columns[column].ColumnName = "Sales Order Stock VC";
                    if (report.Columns[column].ColumnName == "ekwsl ") report.Columns[column].ColumnName = "Purchasing Value Key";
                    if (report.Columns[column].ColumnName == "eprio") report.Columns[column].ColumnName = "Stock Determ. Group";
                    if (report.Columns[column].ColumnName == "eprio ") report.Columns[column].ColumnName = "Stock Determ Group";
                    if (report.Columns[column].ColumnName == "ergei") report.Columns[column].ColumnName = "Unit of Weight (Allowed Pkg Wgt)";
                    if (report.Columns[column].ColumnName == "ergew") report.Columns[column].ColumnName = "Allowed Package Weight ";
                    if (report.Columns[column].ColumnName == "ervoe") report.Columns[column].ColumnName = "Unit of Volume";
                    if (report.Columns[column].ColumnName == "ervol") report.Columns[column].ColumnName = "Allowed Package Volume ";
                    if (report.Columns[column].ColumnName == "ETIAR") report.Columns[column].ColumnName = "Label Type";
                    if (report.Columns[column].ColumnName == "ETIFO") report.Columns[column].ColumnName = "Label Form";
                    if (report.Columns[column].ColumnName == "EXPME") report.Columns[column].ColumnName = "Unit of measure for commodity code";
                    if (report.Columns[column].ColumnName == "extwg") report.Columns[column].ColumnName = "External Material Group ";
                    if (report.Columns[column].ColumnName == "fabkz ") report.Columns[column].ColumnName = "JIT Schedule Indicator ";
                    if (report.Columns[column].ColumnName == "fevor") report.Columns[column].ColumnName = "Production Scheduler";
                    if (report.Columns[column].ColumnName == "fhori ") report.Columns[column].ColumnName = "SchedMargin Key";
                    if (report.Columns[column].ColumnName == "FORKN") report.Columns[column].ColumnName = "Other Formula";
                    if (report.Columns[column].ColumnName == "fprfm") report.Columns[column].ColumnName = "Distr. Profile ";
                    if (report.Columns[column].ColumnName == "frtme") report.Columns[column].ColumnName = "Production Unit";
                    if (report.Columns[column].ColumnName == "fuelg") report.Columns[column].ColumnName = "Maximum Level ";
                    if (report.Columns[column].ColumnName == "fxhor ") report.Columns[column].ColumnName = "Planning Time Fence ";
                    if (report.Columns[column].ColumnName == "fxpru") report.Columns[column].ColumnName = "Fxd Price (Co-Product)";
                    if (report.Columns[column].ColumnName == "gewei") report.Columns[column].ColumnName = "Unit of Weight";
                    if (report.Columns[column].ColumnName == "gewto") report.Columns[column].ColumnName = "Excess Weight Tolerance";
                    if (report.Columns[column].ColumnName == "gpnum ") report.Columns[column].ColumnName = "PRODCOM No. ";
                    if (report.Columns[column].ColumnName == "groes") report.Columns[column].ColumnName = "Size/Dimensions";
                    if (report.Columns[column].ColumnName == "GZOLX") report.Columns[column].ColumnName = "Pref.Zone";
                    if (report.Columns[column].ColumnName == "herkl ") report.Columns[column].ColumnName = "Contry of Origin";
                    if (report.Columns[column].ColumnName == "herkr ") report.Columns[column].ColumnName = "Region of Origin";
                    if (report.Columns[column].ColumnName == "hkmat") report.Columns[column].ColumnName = "Material Origin";
                    if (report.Columns[column].ColumnName == "HOEHE") report.Columns[column].ColumnName = "Height";
                    if (report.Columns[column].ColumnName == "hrkft") report.Columns[column].ColumnName = "Origin Group";
                    if (report.Columns[column].ColumnName == "indus ") report.Columns[column].ColumnName = "Mat CFOP Category ";
                    if (report.Columns[column].ColumnName == "insmk ") report.Columns[column].ColumnName = "Quality Inspection Indicator ";
                    if (report.Columns[column].ColumnName == "IPRKZ") report.Columns[column].ColumnName = "Period Indicator for Shelf Life Expiration Date";
                    if (report.Columns[column].ColumnName == "itark ") report.Columns[column].ColumnName = "Military Good";
                    if (report.Columns[column].ColumnName == "JDNET_BIO_DIV") report.Columns[column].ColumnName = "Biologically Division";
                    if (report.Columns[column].ColumnName == "JDNET_MED_CODE") report.Columns[column].ColumnName = "Medical Code";
                    if (report.Columns[column].ColumnName == "JDNET_MED_DIV") report.Columns[column].ColumnName = "Medical Product Division";
                    if (report.Columns[column].ColumnName == "JDNET_PACK_H") report.Columns[column].ColumnName = "Packaging Height";
                    if (report.Columns[column].ColumnName == "JDNET_PACK_L") report.Columns[column].ColumnName = "Packaging Length";
                    if (report.Columns[column].ColumnName == "JDNET_PACK_UNIT") report.Columns[column].ColumnName = "Packaging Unit";
                    if (report.Columns[column].ColumnName == "JDNET_PACK_VOL") report.Columns[column].ColumnName = "Packaging Volume";
                    if (report.Columns[column].ColumnName == "JDNET_PACK_W") report.Columns[column].ColumnName = "Packaging Width";
                    if (report.Columns[column].ColumnName == "JDNET_PACK_WT") report.Columns[column].ColumnName = "Packaging Weight";
                    if (report.Columns[column].ColumnName == "JDNET_PRODUCT") report.Columns[column].ColumnName = "Product Name";
                    if (report.Columns[column].ColumnName == "JDNET_PUR_UNIT") report.Columns[column].ColumnName = "Purchase Request Unit";
                    if (report.Columns[column].ColumnName == "JDNET_REFG_DIV") report.Columns[column].ColumnName = "Refrigerated Product Division";
                    if (report.Columns[column].ColumnName == "JDNET_STOR_COND") report.Columns[column].ColumnName = "Storage Condtion Flag";
                    if (report.Columns[column].ColumnName == "JDNET_UNI_MATNR") report.Columns[column].ColumnName = "Universal Product";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_H") report.Columns[column].ColumnName = "Unit Packaging Height";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_L") report.Columns[column].ColumnName = "Unit Packaging Length";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_LEN1") report.Columns[column].ColumnName = "Unit Length 1";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_LEN2") report.Columns[column].ColumnName = "Unit Length 2";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_LEN3") report.Columns[column].ColumnName = "Unit Length 3";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_LEN4") report.Columns[column].ColumnName = "Unit Length 4";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_LEN5") report.Columns[column].ColumnName = "Unit Length 5";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_LEN6") report.Columns[column].ColumnName = "Unit Length 6";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_VOL") report.Columns[column].ColumnName = "Unit Packaging Volume";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_VOL1") report.Columns[column].ColumnName = "Unit Volume 1";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_VOL2") report.Columns[column].ColumnName = "Unit Volume 2";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_W") report.Columns[column].ColumnName = "Unit Packaging Width";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_WT") report.Columns[column].ColumnName = "Unit Packaging Weight";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_WT1") report.Columns[column].ColumnName = "Unit Weight 1";
                    if (report.Columns[column].ColumnName == "JDNET_UNIT_WT2") report.Columns[column].ColumnName = "Unit Weight 2";
                    if (report.Columns[column].ColumnName == "JDNET_VAL_DATE") report.Columns[column].ColumnName = "Validation Period";
                    if (report.Columns[column].ColumnName == "JDNET_VAL_FORM") report.Columns[column].ColumnName = "Valid Division Format";
                    if (report.Columns[column].ColumnName == "KAPART") report.Columns[column].ColumnName = "Capacity Category";
                    if (report.Columns[column].ColumnName == "KAPNAMEP") report.Columns[column].ColumnName = "Pooled Capacities";
                    if (report.Columns[column].ColumnName == "kausf ") report.Columns[column].ColumnName = "Component Scrap Percentage";
                    if (report.Columns[column].ColumnName == "kautb ") report.Columns[column].ColumnName = "Autom. PO";
                    if (report.Columns[column].ColumnName == "KOBER") report.Columns[column].ColumnName = "Picking Area ";
                    if (report.Columns[column].ColumnName == "kondm") report.Columns[column].ColumnName = "Material Pricing Group  ";
                    if (report.Columns[column].ColumnName == "kordb ") report.Columns[column].ColumnName = "Source List";
                    if (report.Columns[column].ColumnName == "KOSCH") report.Columns[column].ColumnName = "Product Allocation";
                    if (report.Columns[column].ColumnName == "ktgrm") report.Columns[column].ColumnName = "Account Assignment Group";
                    if (report.Columns[column].ColumnName == "KTSCH") report.Columns[column].ColumnName = "Standard Text Key";
                    if (report.Columns[column].ColumnName == "kzaus ") report.Columns[column].ColumnName = "Discontinued Indicator";
                    if (report.Columns[column].ColumnName == "kzbed ") report.Columns[column].ColumnName = "Requirements Group ";
                    if (report.Columns[column].ColumnName == "kzdkz") report.Columns[column].ColumnName = "Documentation Required";
                    if (report.Columns[column].ColumnName == "kzech ") report.Columns[column].ColumnName = "Batch Entry";
                    if (report.Columns[column].ColumnName == "kzgvh") report.Columns[column].ColumnName = "Packaging Material is Closed Packaging";
                    if (report.Columns[column].ColumnName == "kzkri") report.Columns[column].ColumnName = "Critical Part ";
                    if (report.Columns[column].ColumnName == "kzkri ") report.Columns[column].ColumnName = "Critical Part Indicator";
                    if (report.Columns[column].ColumnName == "kzkup") report.Columns[column].ColumnName = "Co-Product";
                    if (report.Columns[column].ColumnName == "kzkup ") report.Columns[column].ColumnName = "Co-Product ";
                    if (report.Columns[column].ColumnName == "kzpsp ") report.Columns[column].ColumnName = "Cross-Project";
                    if (report.Columns[column].ColumnName == "kzsel ") report.Columns[column].ColumnName = "Selector ";
                    if (report.Columns[column].ColumnName == "kzsel") report.Columns[column].ColumnName = "Selector";
                    if (report.Columns[column].ColumnName == "KZWSM") report.Columns[column].ColumnName = "Units measure usage";
                    if (report.Columns[column].ColumnName == "labor") report.Columns[column].ColumnName = "Laboratory/Design Office";
                    if (report.Columns[column].ColumnName == "ladgr") report.Columns[column].ColumnName = "Loading Group ";
                    if (report.Columns[column].ColumnName == "LAENG") report.Columns[column].ColumnName = "Length";
                    if (report.Columns[column].ColumnName == "lagpr ") report.Columns[column].ColumnName = "Storage Costs Indicator";
                    if (report.Columns[column].ColumnName == "LFGJA") report.Columns[column].ColumnName = "Fiscal Year of Current Period";
                    if (report.Columns[column].ColumnName == "lfmng") report.Columns[column].ColumnName = "Min. Delivery Qty";
                    if (report.Columns[column].ColumnName == "LFMON") report.Columns[column].ColumnName = "Current period (posting period)";
                    if (report.Columns[column].ColumnName == "lfrhy ") report.Columns[column].ColumnName = "Planning Cycle";
                    if (report.Columns[column].ColumnName == "lgfsb ") report.Columns[column].ColumnName = "Storage Location for EP";
                    if (report.Columns[column].ColumnName == "LGNUM") report.Columns[column].ColumnName = "Warehouse No";
                    if (report.Columns[column].ColumnName == "lgort") report.Columns[column].ColumnName = "Storage Location";
                    if (report.Columns[column].ColumnName == "lgpbe") report.Columns[column].ColumnName = "Storage Bin ";
                    if (report.Columns[column].ColumnName == "LGPLA") report.Columns[column].ColumnName = "Storage Bin";
                    if (report.Columns[column].ColumnName == "lgpro") report.Columns[column].ColumnName = "Issue Storage Location";
                    if (report.Columns[column].ColumnName == "lgpro ") report.Columns[column].ColumnName = "Issue Storage Location ";
                    if (report.Columns[column].ColumnName == "lgrad ") report.Columns[column].ColumnName = "Service Level (%)";
                    if (report.Columns[column].ColumnName == "LGTYP") report.Columns[column].ColumnName = "Storage Type";
                    if (report.Columns[column].ColumnName == "LINENO") report.Columns[column].ColumnName = "Line Number";
                    if (report.Columns[column].ColumnName == "loggr") report.Columns[column].ColumnName = "Logistics Handling Group";
                    if (report.Columns[column].ColumnName == "LOSBS") report.Columns[column].ColumnName = "To Lot Size";
                    if (report.Columns[column].ColumnName == "losfx ") report.Columns[column].ColumnName = "Ordering Costs";
                    if (report.Columns[column].ColumnName == "losgr") report.Columns[column].ColumnName = "Costing Lot Size";
                    if (report.Columns[column].ColumnName == "LOSVN") report.Columns[column].ColumnName = "From lot Size";
                    if (report.Columns[column].ColumnName == "LTKZA") report.Columns[column].ColumnName = "Stock Removal";
                    if (report.Columns[column].ColumnName == "LTKZE") report.Columns[column].ColumnName = "Stock Placement";
                    if (report.Columns[column].ColumnName == "LVSME") report.Columns[column].ColumnName = "WM Unit";
                    if (report.Columns[column].ColumnName == "lwmkb") report.Columns[column].ColumnName = "Picking Area";
                    if (report.Columns[column].ColumnName == "lzeih") report.Columns[column].ColumnName = "Time Unit";
                    if (report.Columns[column].ColumnName == "maabc ") report.Columns[column].ColumnName = "ABC Indicator ";
                    if (report.Columns[column].ColumnName == "mabst ") report.Columns[column].ColumnName = "Maximum Stock Level ";
                    if (report.Columns[column].ColumnName == "magrv") report.Columns[column].ColumnName = "Material Group Packing Materials ";
                    if (report.Columns[column].ColumnName == "MAKTX") report.Columns[column].ColumnName = "Description";
                    if (report.Columns[column].ColumnName == "matgr") report.Columns[column].ColumnName = "Material Pricing Group ";
                    if (report.Columns[column].ColumnName == "MATKL") report.Columns[column].ColumnName = "Material Group ";
                    if (report.Columns[column].ColumnName == "matkl ") report.Columns[column].ColumnName = "Material Group";
                    if (report.Columns[column].ColumnName == "MATNR") report.Columns[column].ColumnName = "Material No";
                    if (report.Columns[column].ColumnName == "maxlz") report.Columns[column].ColumnName = "Maximum Storage Period ";
                    if (report.Columns[column].ColumnName == "MBRSH") report.Columns[column].ColumnName = "Industry Sector";
                    if (report.Columns[column].ColumnName == "mdach ") report.Columns[column].ColumnName = "Action Control";
                    if (report.Columns[column].ColumnName == "MEABM") report.Columns[column].ColumnName = "Unit of dimension for length/width/height";
                    if (report.Columns[column].ColumnName == "megru ") report.Columns[column].ColumnName = "Unit of Measure Group ";
                    if (report.Columns[column].ColumnName == "megru") report.Columns[column].ColumnName = "Unit of Measure Group";
                    if (report.Columns[column].ColumnName == "MEINH") report.Columns[column].ColumnName = "UoM";
                    if (report.Columns[column].ColumnName == "MEINS") report.Columns[column].ColumnName = "Base UoM";
                    if (report.Columns[column].ColumnName == "mfrgr ") report.Columns[column].ColumnName = "Material Freight Group  ";
                    if (report.Columns[column].ColumnName == "mfrgr") report.Columns[column].ColumnName = "Material Freight Group ";
                    if (report.Columns[column].ColumnName == "mfrnr ") report.Columns[column].ColumnName = "Manufacturer";
                    if (report.Columns[column].ColumnName == "mfrpn ") report.Columns[column].ColumnName = "Manufacturer Part Number";
                    if (report.Columns[column].ColumnName == "MHDHB") report.Columns[column].ColumnName = "Total Shelf Life";
                    if (report.Columns[column].ColumnName == "MHDLP") report.Columns[column].ColumnName = "Storage Percentage";
                    if (report.Columns[column].ColumnName == "MHDRZ") report.Columns[column].ColumnName = "Min Shelf Life";
                    if (report.Columns[column].ColumnName == "minbe ") report.Columns[column].ColumnName = "ReOrder Point ";
                    if (report.Columns[column].ColumnName == "miskz ") report.Columns[column].ColumnName = "Mixed MRP ";
                    if (report.Columns[column].ColumnName == "mmsta") report.Columns[column].ColumnName = "Plant-Specific Material Status";
                    if (report.Columns[column].ColumnName == "mmsta ") report.Columns[column].ColumnName = "Plant Specific Material Status";
                    if (report.Columns[column].ColumnName == "mmstd") report.Columns[column].ColumnName = "Valid From";
                    if (report.Columns[column].ColumnName == "mmstd ") report.Columns[column].ColumnName = "Valid From ";
                    if (report.Columns[column].ColumnName == "mogru ") report.Columns[column].ColumnName = "CAP Prod. Group";
                    if (report.Columns[column].ColumnName == "mownr ") report.Columns[column].ColumnName = "EU Mkt Prod. List No ";
                    if (report.Columns[column].ColumnName == "mprof ") report.Columns[column].ColumnName = "Manufacturer Part Profile ";
                    if (report.Columns[column].ColumnName == "mrppp ") report.Columns[column].ColumnName = "Planning Calendar";
                    if (report.Columns[column].ColumnName == "MSTAE") report.Columns[column].ColumnName = "X-Plant Material Status";
                    if (report.Columns[column].ColumnName == "mstav") report.Columns[column].ColumnName = "X-distr. Chain Status";
                    if (report.Columns[column].ColumnName == "MSTDE") report.Columns[column].ColumnName = "X-Plant Valid From";
                    if (report.Columns[column].ColumnName == "mstdv") report.Columns[column].ColumnName = "X-distr. Chain Status Valid From ";
                    if (report.Columns[column].ColumnName == "MTART") report.Columns[column].ColumnName = "Material Type";
                    if (report.Columns[column].ColumnName == "mtorg") report.Columns[column].ColumnName = "Material Origin (CFOP)";
                    if (report.Columns[column].ColumnName == "mtpos") report.Columns[column].ColumnName = "Item Category Group ";
                    if (report.Columns[column].ColumnName == "MTPOS_MARA") report.Columns[column].ColumnName = "Gen Item Category Group";
                    if (report.Columns[column].ColumnName == "mtpos_mara ") report.Columns[column].ColumnName = "GeneralItemCatGroup";
                    if (report.Columns[column].ColumnName == "mtuse") report.Columns[column].ColumnName = "Material Usage (CFOP) ";
                    if (report.Columns[column].ColumnName == "mtver ") report.Columns[column].ColumnName = "Export/Import Group";
                    if (report.Columns[column].ColumnName == "mtvfp ") report.Columns[column].ColumnName = "Availability Check ";
                    if (report.Columns[column].ColumnName == "mtvfp") report.Columns[column].ColumnName = "Availability Check";
                    if (report.Columns[column].ColumnName == "mvgr1") report.Columns[column].ColumnName = "Material Group 1 (Local Business Unit)";
                    if (report.Columns[column].ColumnName == "mvgr2") report.Columns[column].ColumnName = "Material Group 2 (Disease State)";
                    if (report.Columns[column].ColumnName == "mvgr3") report.Columns[column].ColumnName = "Material Group 3 (Clinical Therapy Class)";
                    if (report.Columns[column].ColumnName == "mvgr4") report.Columns[column].ColumnName = "Material Group 4 (Pharma Class";
                    if (report.Columns[column].ColumnName == "mvgr5") report.Columns[column].ColumnName = "Material Group 5";
                    if (report.Columns[column].ColumnName == "mypol") report.Columns[column].ColumnName = "LIFO Pool ";
                    if (report.Columns[column].ColumnName == "ncost") report.Columns[column].ColumnName = "No Costing / Do Not Cost";
                    if (report.Columns[column].ColumnName == "nfmat ") report.Columns[column].ColumnName = "Repetitive Manufacturing ";
                    if (report.Columns[column].ColumnName == "nrfhg ") report.Columns[column].ColumnName = "Discount in Kind ";
                    if (report.Columns[column].ColumnName == "NTGEW") report.Columns[column].ColumnName = "Net Weight";
                    if (report.Columns[column].ColumnName == "NUMTP") report.Columns[column].ColumnName = "EAN Category";
                    if (report.Columns[column].ColumnName == "ocmpf") report.Columns[column].ColumnName = "Overall Profile";
                    if (report.Columns[column].ColumnName == "ownpr") report.Columns[column].ColumnName = "Produced in House (CFOP) ";
                    if (report.Columns[column].ColumnName == "peinh") report.Columns[column].ColumnName = "Price Unit ";
                    if (report.Columns[column].ColumnName == "periv ") report.Columns[column].ColumnName = "Fiscal Year Variant";
                    if (report.Columns[column].ColumnName == "perkz ") report.Columns[column].ColumnName = "Period Indicator";
                    if (report.Columns[column].ColumnName == "plifz ") report.Columns[column].ColumnName = "Planned Delivery Time";
                    if (report.Columns[column].ColumnName == "PLNAL") report.Columns[column].ColumnName = "Recipe";
                    if (report.Columns[column].ColumnName == "plnnr") report.Columns[column].ColumnName = "Group";
                    if (report.Columns[column].ColumnName == "plnty") report.Columns[column].ColumnName = "Task List Type ";
                    if (report.Columns[column].ColumnName == "pmatn") report.Columns[column].ColumnName = "Pricing Reference Material";
                    if (report.Columns[column].ColumnName == "prat1") report.Columns[column].ColumnName = "Product Attribute 1 (Narcotic)";
                    if (report.Columns[column].ColumnName == "prat2") report.Columns[column].ColumnName = "Product Attribute 2 (Essential Chemical) ";
                    if (report.Columns[column].ColumnName == "prat3") report.Columns[column].ColumnName = "Product Attribute 3 (Branded)";
                    if (report.Columns[column].ColumnName == "prat4") report.Columns[column].ColumnName = "Product Attribute 4 (Prescription) ";
                    if (report.Columns[column].ColumnName == "prat5") report.Columns[column].ColumnName = "Product Attribute 5 ";
                    if (report.Columns[column].ColumnName == "prat6") report.Columns[column].ColumnName = "Product Attribute 6 ";
                    if (report.Columns[column].ColumnName == "prat7") report.Columns[column].ColumnName = "Product Attribute 7 ";
                    if (report.Columns[column].ColumnName == "prat8") report.Columns[column].ColumnName = "Product Attribute 8 ";
                    if (report.Columns[column].ColumnName == "prat9") report.Columns[column].ColumnName = "Product Attribute 9 ";
                    if (report.Columns[column].ColumnName == "prata") report.Columns[column].ColumnName = "Product Attribute 10";
                    if (report.Columns[column].ColumnName == "prctr") report.Columns[column].ColumnName = "Profit Center ";
                    if (report.Columns[column].ColumnName == "PRDHA") report.Columns[column].ColumnName = "Prod Hierarchy";
                    if (report.Columns[column].ColumnName == "PREDA") report.Columns[column].ColumnName = "PrefDet.";
                    if (report.Columns[column].ColumnName == "PREFE") report.Columns[column].ColumnName = "Preference";
                    if (report.Columns[column].ColumnName == "prefe ") report.Columns[column].ColumnName = "Preference Status ";
                    if (report.Columns[column].ColumnName == "prenc ") report.Columns[column].ColumnName = "Exemption Certificate";
                    if (report.Columns[column].ColumnName == "prend ") report.Columns[column].ColumnName = "Iss. Date of Ex. Cert.";
                    if (report.Columns[column].ColumnName == "PRENE") report.Columns[column].ColumnName = "Vdrdecl.";
                    if (report.Columns[column].ColumnName == "prene ") report.Columns[column].ColumnName = "Vendor Decl. Status";
                    if (report.Columns[column].ColumnName == "PRENG") report.Columns[column].ColumnName = "VrDeclDate";
                    if (report.Columns[column].ColumnName == "preno ") report.Columns[column].ColumnName = "Exemption Cert. No";
                    if (report.Columns[column].ColumnName == "prfrq") report.Columns[column].ColumnName = "Inspection Interval";
                    if (report.Columns[column].ColumnName == "prodh") report.Columns[column].ColumnName = "Product Hierarchy";
                    if (report.Columns[column].ColumnName == "provg") report.Columns[column].ColumnName = "Commission Group ";
                    if (report.Columns[column].ColumnName == "PRVBE") report.Columns[column].ColumnName = "Default supply area";
                    if (report.Columns[column].ColumnName == "qklas") report.Columns[column].ColumnName = "Project Stock VC";
                    if (report.Columns[column].ColumnName == "qmata") report.Columns[column].ColumnName = "QM Material Auth.";
                    if (report.Columns[column].ColumnName == "qmpur") report.Columns[column].ColumnName = "QM in Procurement Active ";
                    if (report.Columns[column].ColumnName == "qssys") report.Columns[column].ColumnName = "QM System Requirements ";
                    if (report.Columns[column].ColumnName == "qzgtp") report.Columns[column].ColumnName = "Certificate Type";
                    if (report.Columns[column].ColumnName == "raube2") report.Columns[column].ColumnName = "Storage Conditions";
                    if (report.Columns[column].ColumnName == "rbnrm") report.Columns[column].ColumnName = "Catalog Profile ";
                    if (report.Columns[column].ColumnName == "rdmhd") report.Columns[column].ColumnName = "Rounding Rule SLED";
                    if (report.Columns[column].ColumnName == "rdprf ") report.Columns[column].ColumnName = "Rounding Profile  ";
                    if (report.Columns[column].ColumnName == "rdprf") report.Columns[column].ColumnName = "Rounding Profile ";
                    if (report.Columns[column].ColumnName == "rgekz ") report.Columns[column].ColumnName = "Backflush";
                    if (report.Columns[column].ColumnName == "ruezt") report.Columns[column].ColumnName = "Setup Time ";
                    if (report.Columns[column].ColumnName == "rwpro ") report.Columns[column].ColumnName = "Coverage Profile ";
                    if (report.Columns[column].ColumnName == "SANKA") report.Columns[column].ColumnName = "CostingRelevncy; in SAP, BOM Detail - Status/lng text tab(it's not a checkbox)";
                    if (report.Columns[column].ColumnName == "SAPID") report.Columns[column].ColumnName = "SAPID";
                    if (report.Columns[column].ColumnName == "sauft ") report.Columns[column].ColumnName = "Flag";
                    if (report.Columns[column].ColumnName == "sbdkz ") report.Columns[column].ColumnName = "Individual/Coll.";
                    if (report.Columns[column].ColumnName == "schgt ") report.Columns[column].ColumnName = "Bulk Material ";
                    if (report.Columns[column].ColumnName == "schme") report.Columns[column].ColumnName = "Delivery Unit UoM";
                    if (report.Columns[column].ColumnName == "scmng") report.Columns[column].ColumnName = "Delivery Unit ";
                    if (report.Columns[column].ColumnName == "sernp") report.Columns[column].ColumnName = "SerialNo Profile ";
                    if (report.Columns[column].ColumnName == "sfcpf") report.Columns[column].ColumnName = "Prodcution Schedule Profile";
                    if (report.Columns[column].ColumnName == "sfepr ") report.Columns[column].ColumnName = "REM Profile";
                    if (report.Columns[column].ColumnName == "shflg ") report.Columns[column].ColumnName = "Safety Time Ind";
                    if (report.Columns[column].ColumnName == "shpro ") report.Columns[column].ColumnName = "Stime Period Profile";
                    if (report.Columns[column].ColumnName == "shzet ") report.Columns[column].ColumnName = "Safety Time";
                    if (report.Columns[column].ColumnName == "sktof") report.Columns[column].ColumnName = "Cash Discount Indicator";
                    if (report.Columns[column].ColumnName == "SLWID") report.Columns[column].ColumnName = "User Defined - Field Key";
                    if (report.Columns[column].ColumnName == "sobsk") report.Columns[column].ColumnName = "SpecProc TypeFor Costing (SPT for Costing)";
                    if (report.Columns[column].ColumnName == "sobsl ") report.Columns[column].ColumnName = "Special Procurement Type (SPT)";
                    if (report.Columns[column].ColumnName == "SPART") report.Columns[column].ColumnName = "Division";
                    if (report.Columns[column].ColumnName == "SPRAS") report.Columns[column].ColumnName = "Language";
                    if (report.Columns[column].ColumnName == "ssqss") report.Columns[column].ColumnName = "QM Control Key";
                    if (report.Columns[column].ColumnName == "stawn ") report.Columns[column].ColumnName = "Comm./Imp. Code No";
                    if (report.Columns[column].ColumnName == "stdpd ") report.Columns[column].ColumnName = "Configurable Material ";
                    if (report.Columns[column].ColumnName == "steuc ") report.Columns[column].ColumnName = "Control Code";
                    if (report.Columns[column].ColumnName == "stfak") report.Columns[column].ColumnName = "Stackability Factor ";
                    if (report.Columns[column].ColumnName == "stlal") report.Columns[column].ColumnName = "Alternative BOM";
                    if (report.Columns[column].ColumnName == "stlan") report.Columns[column].ColumnName = "BOM Usage";
                    if (report.Columns[column].ColumnName == "STOFF") report.Columns[column].ColumnName = "Hazardour Material Num";
                    if (report.Columns[column].ColumnName == "stprs") report.Columns[column].ColumnName = "Standard Price";
                    if (report.Columns[column].ColumnName == "strgr ") report.Columns[column].ColumnName = "Planning strategy group";
                    if (report.Columns[column].ColumnName == "stype") report.Columns[column].ColumnName = "line";
                    if (report.Columns[column].ColumnName == "stype ") report.Columns[column].ColumnName = "Line  ";
                    if (report.Columns[column].ColumnName == "takzt ") report.Columns[column].ColumnName = "Takt Time";
                    if (report.Columns[column].ColumnName == "taxim") report.Columns[column].ColumnName = "Tax 10";
                    if (report.Columns[column].ColumnName == "taxm1") report.Columns[column].ColumnName = "Tax 1";
                    if (report.Columns[column].ColumnName == "taxm2") report.Columns[column].ColumnName = "Tax 2";
                    if (report.Columns[column].ColumnName == "taxm3") report.Columns[column].ColumnName = "Tax 3";
                    if (report.Columns[column].ColumnName == "taxm4") report.Columns[column].ColumnName = "Tax 4";
                    if (report.Columns[column].ColumnName == "taxm5") report.Columns[column].ColumnName = "Tax 5";
                    if (report.Columns[column].ColumnName == "taxm6") report.Columns[column].ColumnName = "Tax 6";
                    if (report.Columns[column].ColumnName == "taxm7") report.Columns[column].ColumnName = "Tax 7";
                    if (report.Columns[column].ColumnName == "taxm8") report.Columns[column].ColumnName = "Tax 8";
                    if (report.Columns[column].ColumnName == "taxm9") report.Columns[column].ColumnName = "Tax 9";
                    if (report.Columns[column].ColumnName == "tempb2") report.Columns[column].ColumnName = "Temperature Conditions Indicator";
                    if (report.Columns[column].ColumnName == "tragr") report.Columns[column].ColumnName = "Transportation Group";
                    if (report.Columns[column].ColumnName == "tranz") report.Columns[column].ColumnName = "Interoperation Time ";
                    if (report.Columns[column].ColumnName == "ueetk") report.Columns[column].ColumnName = "Unlimited Overdelivery Tolerance";
                    if (report.Columns[column].ColumnName == "ueeto") report.Columns[column].ColumnName = "Overdelivery Tolerance Percentage";
                    if (report.Columns[column].ColumnName == "UMREN") report.Columns[column].ColumnName = "X";
                    if (report.Columns[column].ColumnName == "UMREZ") report.Columns[column].ColumnName = "Y";
                    if (report.Columns[column].ColumnName == "uneto") report.Columns[column].ColumnName = "Underdelivery Tolerance Percentage ";
                    if (report.Columns[column].ColumnName == "usequ ") report.Columns[column].ColumnName = "Quota Arr. Usage ";
                    if (report.Columns[column].ColumnName == "vabme ") report.Columns[column].ColumnName = "Var. Oun";
                    if (report.Columns[column].ColumnName == "vavme") report.Columns[column].ColumnName = "Sales Unit Not Var";
                    if (report.Columns[column].ColumnName == "vbamg") report.Columns[column].ColumnName = "Base Quantity ";
                    if (report.Columns[column].ColumnName == "vbeaz") report.Columns[column].ColumnName = "Shipping Processing Time";
                    if (report.Columns[column].ColumnName == "VERID") report.Columns[column].ColumnName = "Production Version";
                    if (report.Columns[column].ColumnName == "versg") report.Columns[column].ColumnName = "Material Statistics Group ";
                    if (report.Columns[column].ColumnName == "VERWE") report.Columns[column].ColumnName = "Usage";

                }
            }

            return DataTableList;
        }

        public DataTable ConvertListToDataTable(List<string> list)
        {
            // New table.
            DataTable table = new DataTable();
            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }
            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }
            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }
            return table;
        }

        public void MyTrace(string msg)
        {
            HttpContext.Current.Response.Write("<script>alert('" + msg + "','Some content')</script>");
        }

        public void MyTrace1(string msg, Page pg, Type tp)
        {
            ClientScriptManager CSM = pg.ClientScript;
            HttpContext.Current.Response.Write("<script>alert('" + msg + "','Some content')</script>");
            string display = "Pop-up!";
            CSM.RegisterStartupScript(tp, "yourMessage", "alert('" + display + "');", true);
        }

        public bool ValidateUserAccess(string systemid,string password, string defaultOracleInstance)
        {
            try
            {
                bool checkconn = commmanager.CheckSchemaValidation(systemid, password, defaultOracleInstance);
                return checkconn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Message(String msg, Type tp, Page pg)
        {
            ClientScriptManager CSM = pg.ClientScript;
            string script = "window.onload = function(){ alert('";
            script += msg;
            script += "');";
            script += "window.location = '";
            script += "'; }";
            CSM.RegisterStartupScript(tp, "Redirect", script, true);
        }

        public DataSet Active_Schema_And_Site(string userid)//
        {
            try
            {
                string SQL1 = String.Format(@"
                select * from GDD_OWNER.GDD_USER_PROPERTIES where user_name=");
                string SQL2 = "'" + userid + "'";
                string command = SQL1 + SQL2;

                System.Data.DataSet userschemadata = commmanager.GetUserSchemaAccessDetails(command);

                return userschemadata;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}