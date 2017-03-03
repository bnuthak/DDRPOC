using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.BusinessLogic;
using DDR.Entity;

namespace DDRPOC
{
    public partial class Unused : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                Label mylabelControl = (Label)Page.Master.FindControl("lbluserid");
                mylabelControl.Text = Session["userid"].ToString(); 
                Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
                lblsitecode.Text = DDRSessionEntity.Current.SiteCode;
        }
    }
}