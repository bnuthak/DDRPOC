using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DDR.Entity;
using DDR.BusinessLogic;

namespace DDRPOC
{
    public partial class DBSchema : System.Web.UI.Page
    {
        SiteCode sitecode = new SiteCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            Control myControlMenu = Page.Master.FindControl("NavigationMenu");
            if ((DDRSessionEntity.Current.SiteCode == null) || (DDRSessionEntity.Current.SiteCode == "Not Set"))
            {
                myControlMenu.Visible = false;
            }
            if (Session["userid"] != null)//UserEntity.SystemId!=null)//Session["userid"] != null)
            {
                Label lbluserid = (Label)Page.Master.FindControl("lbluserid");
                lbluserid.Text = Session["userid"].ToString(); 
                //Label mylabelControl = (Label)Page.Master.FindControl("lblsitecode");
            }
            urllinkinschematable();
        }
        private void bindschema()
        {
            grdschema.DataSource = SchemaBindTableHeader();//mmreport.GetMMAuditingFinalData();
            grdschema.DataBind();
        }
        private void urllinkinschematable()
        {
            DataTable dt = SchemaBindTableHeader();

            GridView gv = new GridView();
            grdschema.DataSource = dt;
            grdschema.DataBind();

            foreach (GridViewRow gr in grdschema.Rows)
            {
                //HyperLink hp = new HyperLink();
                //hp.Text = gr.Cells[0].Text;
                //hp.NavigateUrl = "~/About.aspx?dsm=" + hp.Text;
                //gr.Cells[0].Controls.Add(hp);

                LinkButton lb = new LinkButton();
                lb.Text = gr.Cells[0].Text;
                lb.CommandName = gr.Cells[0].Text;
                lb.Command += LinkButton_Command;
                gr.Cells[0].Controls.Add(lb);
            }
        }
        protected DataTable SchemaBindTableHeader()
        {
            DataTable sctable = new DataTable();
            sctable.Columns.Add("Schema", typeof(string));
            sctable.Columns.Add("Description", typeof(string));


            //DataSet datasettotable = sitecode.GetSiteCodeFinalData();
            DataRow sbrow;
            sbrow = sctable.NewRow();
            sbrow["Schema"] = "SC3";
            sbrow["Description"] = "Odd Releases: R11, R13, etc.";
            sctable.Rows.Add(sbrow);
            sbrow = sctable.NewRow();
            sbrow["Schema"] = "SCM";
            sbrow["Description"] = "Even Releases: R12, R14, etc.";
            sctable.Rows.Add(sbrow);
            return sctable;
        }
        
        private void BindCountrySite()
        {
            DataSet dataset = sitecode.GetSiteCodeFinalData();            

            GridView gv = new GridView();
            //grdmmauditingreport.DataSource = dt;
            //grdmmauditingreport.DataBind();


            for (int row = 0; row < dataset.Tables[0].Rows.Count; row++)
            {                                
                if (dataset.Tables[0].Rows[row][1].ToString() == DDRSessionEntity.Current.SiteCode)
                {
                    DDRSessionEntity.Current.CountryCode = dataset.Tables[0].Rows[row][2].ToString();
                }                
            }            
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {


            if (DDRSessionEntity.Current.can_choose_country == "Y") 
            {
                if (e.CommandName.ToString() == "SCM" || e.CommandName.ToString() == "SC3")
                {
                    Response.Redirect("~/About.aspx?dsm=" + e.CommandName.ToString());
                }
            }
            else if (DDRSessionEntity.Current.can_choose_country == "N") 
            {
                //set default schema
                DDRSessionEntity.Current.table_schema = e.CommandName.ToString();
                BindCountrySite();
                var random = new Random();
                string security_string = "";
                for (int i = 0; i < 10; i++)
                {
                    security_string = String.Concat(security_string, random.Next(10).ToString());
                }
                DDRSessionEntity.Current.SecureSplit = security_string;
                Response.Redirect("~/Default.aspx?name=" + DDRSessionEntity.Current.SiteCode + DDRSessionEntity.Current.SecureSplit + DDRSessionEntity.Current.CountryCode);
            }
        }
    }
}