using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.BusinessLogic;
using System.Data;
using DDR.Entity;

namespace DDRPOC
{
    public partial class About : System.Web.UI.Page
    {
        SiteCode sitecode = new SiteCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            //bindSiteCode();
            //gettable(sitecodebindtableheader());                       
            
            if (DDRSessionEntity.Current.can_choose_schema == "Y")
            {
                DDRSessionEntity.Current.table_schema = Request.QueryString["dsm"];
            }            

            Control myControlMenu = Page.Master.FindControl("NavigationMenu");
            if ((DDRSessionEntity.Current.SiteCode == null) || (DDRSessionEntity.Current.SiteCode == "Not Set"))
            {
                myControlMenu.Visible = false;
            }
            if (Session["userid"] != null)
            {
                Label lbluserid = (Label)Page.Master.FindControl("lbluserid");
                lbluserid.Text = Session["userid"].ToString();
                //Label mylabelControl = (Label)Page.Master.FindControl("lblsitecode");
            }
            if (DDRSessionEntity.Current.SiteCode != null)
            {
                Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
                lblsitecode.Text = DDRSessionEntity.Current.SiteCode;
            }

            BindCountrySite();
        }
        private void bindSiteCode()
        {
            grdmmauditingreport.DataSource = sitecodebindtableheader();//mmreport.GetMMAuditingFinalData();
            grdmmauditingreport.DataBind();
        }

        protected DataTable sitecodebindtableheader()
        {
            DataTable sctable = new DataTable();
            sctable.Columns.Add("SAPID", typeof(string));
            sctable.Columns.Add("Site Code", typeof(string));
            sctable.Columns.Add("ISO Code", typeof(string));
            sctable.Columns.Add("Description", typeof(string));

            DataSet datasettotable = sitecode.GetSiteCodeFinalData();
            DataRow scrow;
            for (int row = 0; row < datasettotable.Tables[0].Rows.Count; row++)
            {
                scrow = sctable.NewRow();
                scrow["SAPID"] = datasettotable.Tables[0].Rows[row][0];
                scrow["Site Code"] = datasettotable.Tables[0].Rows[row][1];
                scrow["ISO Code"] = datasettotable.Tables[0].Rows[row][2];
                scrow["Description"] = datasettotable.Tables[0].Rows[row][3];
                sctable.Rows.Add(scrow);
            }
            return sctable;
        }
        private void BindCountrySite()
        {
            DataTable dt = sitecodebindtableheader();

            GridView gv = new GridView();
            grdmmauditingreport.DataSource = dt;
            grdmmauditingreport.DataBind();            

            foreach (GridViewRow gr in grdmmauditingreport.Rows)
            {
                HyperLink hp = new HyperLink();
                hp.Text = gr.Cells[1].Text;                
                hp.NavigateUrl = "~/Default.aspx?name=" + hp.Text + DDRSessionEntity.Current.SecureSplit + gr.Cells[2].Text;
                gr.Cells[1].Controls.Add(hp);
            }
        }

        protected void grdmmauditingreport_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UserEntity.SiteCode = grdmmauditingreport.SelectedRow.Cells[1].Text;
        }

    }
}
