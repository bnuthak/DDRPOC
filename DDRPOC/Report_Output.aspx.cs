using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.BusinessLogic;
using System.Data;
using DDR.Entity;
using System.IO;
using System.Diagnostics;


namespace DDRPOC.MM
{
    public partial class Report_Output : System.Web.UI.Page
    {        
        Common exportreport = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {

            string val = Request.QueryString.ToString();
            if (val == "2")
            {
                //exportreport.ExportToExcel(mmreport.SQL_MM_Errors_No_Makt(), "MMAuditing.xls");
            }
            else
            {
                bindmmAuditingReport1();
            }
            if (!IsPostBack)
            {
                Label mylabelControl = (Label)Page.Master.FindControl("lbluserid");
                mylabelControl.Text = Session["userid"].ToString();
                Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
                lblsitecode.Text = DDRSessionEntity.Current.SiteCode;
            }
        }

        private void bindmmAuditingReport1()
        {
            List<DataTable> SelectedReports = exportreport.GetSelectedDataTable(); // Runs all of the selected reports (selections stored in Session variable).
            for (int i = 0; i < exportreport.DataTableList.Count; i++)
            {
                string reportname = string.Empty;
                Label lblsetreporttitle = new Label();
                lblsetreporttitle.Font.Bold = true;
                lblsetreporttitle.Font.Size = 12;
                System.Web.UI.HtmlControls.HtmlTableRow tbrow = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tbcol = new System.Web.UI.HtmlControls.HtmlTableCell();
                //bool hasValue = DDRSessionEntity.Current.checkcount.TryGetValue((i + 1).ToString(), out reportname);
                //tbcol.InnerText = reportname;
                //lblsetreporttitle.Text = reportname;
                lblsetreporttitle.Text = SelectedReports.ElementAt(i).TableName;
                tbcol.Controls.Add(lblsetreporttitle);
                tbrow.Cells.Add(tbcol);
                rpttable.Rows.Add(tbrow);
                //Create blank row
                System.Web.UI.HtmlControls.HtmlTableRow tbblankrow = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tbblankcol = new System.Web.UI.HtmlControls.HtmlTableCell();
                tbblankcol.Height = "15";
                tbblankrow.Cells.Add(tbblankcol);
                rpttable.Rows.Add(tbblankrow);                
                //Initialize the grid with data.
                GridView reportgrdi = new GridView();
                reportgrdi = GridStyle();
                reportgrdi.DataSource = SelectedReports.ElementAt(i);//MMAuditingbindtableheader();
                reportgrdi.DataBind();
                if (SelectedReports.ElementAt(i).Rows.Count > 0)
                    reportgrdi.Rows[0].Cells[0].Width = 250;
                System.Web.UI.HtmlControls.HtmlTableRow tbrowtable = new System.Web.UI.HtmlControls.HtmlTableRow();
                System.Web.UI.HtmlControls.HtmlTableCell tbcoltable = new System.Web.UI.HtmlControls.HtmlTableCell();
                tbcoltable.Controls.Add(reportgrdi);
                tbrowtable.Cells.Add(tbcoltable);
                rpttable.Rows.Add(tbrowtable);                
            }
        }

        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected GridView GridStyle()
        {
            GridView reportgrdv = new GridView();
            //drd = (GridView)this.FindControl("grdmmmultiplereport");
            System.Drawing.Color backclr = System.Drawing.ColorTranslator.FromHtml("#DEBA84");
            reportgrdv.BackColor = backclr;
            System.Drawing.Color borderclr = System.Drawing.ColorTranslator.FromHtml("#DEBA84");
            reportgrdv.BorderColor = borderclr;

            System.Drawing.Color rowstyleBackcolor = System.Drawing.ColorTranslator.FromHtml("#FFF7E7");
            System.Drawing.Color rowstyleForcolor = System.Drawing.ColorTranslator.FromHtml("#8C4510");
            reportgrdv.RowStyle.BackColor = rowstyleBackcolor;
            reportgrdv.RowStyle.ForeColor = rowstyleForcolor;
            //reportgrdv.RowStyle.BorderStyle = BorderStyle.Solid;
            //reportgrdv.BorderWidth = 1;
            //reportgrdv.CellPadding = 2;
            //reportgrdv.CellSpacing = 1;
            reportgrdv.ApplyStyle(grdmmmultiplereport.HeaderStyle);
            //reportgrdv.ApplyStyle(grdmmmultiplereport.FooterStyle);
            reportgrdv.ApplyStyle(grdmmmultiplereport.RowStyle);
            reportgrdv.ApplyStyle(grdmmmultiplereport.SelectedRowStyle);
            reportgrdv.ApplyStyle(grdmmmultiplereport.SortedAscendingCellStyle);
            reportgrdv.ApplyStyle(grdmmmultiplereport.SortedAscendingHeaderStyle);
            reportgrdv.ApplyStyle(grdmmmultiplereport.SortedDescendingCellStyle);
            reportgrdv.ApplyStyle(grdmmmultiplereport.SortedDescendingHeaderStyle);

            //ThisGridView.ApplyStyle(MasterGridView.ControlStyle)
            // reportgrdv.ApplyStyle(grdmmmultiplereport.ControlStyle);
            return reportgrdv;
        }

        protected void grdmmmultiplereport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Attributes["width"] = "250px";
        }
    }
}