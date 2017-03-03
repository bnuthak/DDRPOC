using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.BusinessLogic;
using System.Data;
using DDR.Entity;


namespace DDRPOC.QM
{
    public partial class QM_qinfo_sap : System.Web.UI.Page
    {
        QMVerificationReport report = new QMVerificationReport();
        Common exportreport = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (report.GetQMVerificationTotalReport().Rows.Count < 1)
            {
                lblauditreportmessage.Visible = true;
            }
            else
            {
                lblauditreportmessage.Visible = false;
            }
            //get the data in excel format
            string val=Request.QueryString.ToString();
            if (val == "2")
            {
                exportreport.ExportToExcel(report.GetQMVerificationTotalReport(), "QMVerification.xls");
            }
            else
            {
                lblrpthverification.Text = Session["reporttitle"].ToString();
                bindqmverificationreport();
            }
            if (!IsPostBack)
            {
                Label mylabelControl = (Label)Page.Master.FindControl("lbluserid");
                mylabelControl.Text = Session["userid"].ToString(); 
                Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
                lblsitecode.Text = DDRSessionEntity.Current.SiteCode;
            }
        }

        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void bindqmverificationreport()
        {
            try{
            qmveryficationreport.DataSource =  report.GetQMVerificationTotalReport();
            qmveryficationreport.DataBind();
            }
            catch(Exception ex)
            {
                lblauditreportmessage.Text = ex.Message;
            }
        }

        void DisplayCurrentPage()
        {
            //// Calculate the current page number.
            //int currentPage = qmveryficationreport.PageIndex + 1;

            //// Display the current page number. 
            //message. = "Page " + currentPage.ToString() + " of " +
            //  qmveryficationreport.PageCount.ToString() + ".";
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            qmveryficationreport.PageIndex = e.NewPageIndex;
            qmveryficationreport.DataBind();
        }

        protected void qmveryficationreport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            qmveryficationreport.PageIndex = e.NewPageIndex;
            qmveryficationreport.DataBind();
        }
        /// <summary>
        /// Change Table heading with proper naming convention
        /// </summary>
        /// <returns></returns>
        //protected DataTable QMVerificationtableheader()
        //{
        //    DataTable qmtable = new DataTable();
        //    qmtable.Columns.Add("Material", typeof(string));
        //    qmtable.Columns.Add("PLANT", typeof(string));
        //    qmtable.Columns.Add("Vendor Type", typeof(string));
        //    qmtable.Columns.Add("SAP Vendor No", typeof(string));
        //    qmtable.Columns.Add("Name", typeof(string));
        //    qmtable.Columns.Add("Date_Until_rel valid", typeof(string));
        //    qmtable.Columns.Add("Status Profile", typeof(string));
        //    qmtable.Columns.Add("Inspection Control", typeof(string));
        //    qmtable.Columns.Add("Internal counter", typeof(string));
        //    qmtable.Columns.Add("Deletion Flag", typeof(string));
        //    qmtable.Columns.Add("Rel Qnt is active", typeof(string));
        //    qmtable.Columns.Add("Vendor's QM System", typeof(string));
        //    qmtable.Columns.Add("Inspection Type", typeof(string));
        //    qmtable.Columns.Add("Lot Creation Lead Time", typeof(string));
        //    qmtable.Columns.Add("Blocked Function", typeof(string));
        //    qmtable.Columns.Add("Reason For Block", typeof(string));
        //    DataSet datasettotable = report.GetQMVerificationTotalReport();
        //    DataRow qmrow;
        //    for (int row = 0; row < datasettotable.Tables[0].Rows.Count ; row++)
        //    {
        //            qmrow = qmtable.NewRow();
        //            qmrow["Material"] = datasettotable.Tables[0].Rows[row][0];
        //            qmrow["PLANT"] = datasettotable.Tables[0].Rows[row][1];
        //            qmrow["Vendor Type"] = datasettotable.Tables[0].Rows[row][2];
        //            qmrow["SAP Vendor No"] = datasettotable.Tables[0].Rows[row][3];
        //            qmrow["Name"] = datasettotable.Tables[0].Rows[row][4];
        //            qmrow["Date_Until_rel valid"] = datasettotable.Tables[0].Rows[row][5];
        //            qmrow["Status Profile"] = datasettotable.Tables[0].Rows[row][6];
        //            qmrow["Inspection Control"] = datasettotable.Tables[0].Rows[row][7];
        //            qmrow["Internal Counter"] = datasettotable.Tables[0].Rows[row][8];
        //            qmrow["Deletion Flag"] = datasettotable.Tables[0].Rows[row][9];
        //            qmrow["Rel Qnt is active"] = datasettotable.Tables[0].Rows[row][10];
        //            qmrow["Vendor's QM System"] = datasettotable.Tables[0].Rows[row][11];
        //            qmrow["Inspection Type"] = datasettotable.Tables[0].Rows[row][12];
        //            qmrow["Lot Creation Lead Time"] = datasettotable.Tables[0].Rows[row][13];
        //            qmrow["Blocked Function"] = datasettotable.Tables[0].Rows[row][14];
        //            qmrow["Reason For Block"] = datasettotable.Tables[0].Rows[row][15];
                    
        //        qmtable.Rows.Add(qmrow);
        //    }
        //    return qmtable;
        //}

    }
}