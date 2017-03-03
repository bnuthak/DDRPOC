using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.Entity;

namespace DDRPOC
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblsitecode.Text = "Not Set";
                        
            if ((Session["userid"] != null) ||(Session["userid"].ToString() != "Not Set"))
            {
                lbluserid.Text = Session["userid"].ToString();                
            }
            if ((DDRSessionEntity.Current.SiteCode != null) || (DDRSessionEntity.Current.SiteCode != "Not Set"))
            {
                lblsitecode.Text = DDRSessionEntity.Current.SiteCode;
            }

            if ((Session["userid"] == null) || (Session["userid"].ToString() == "Not Set"))
            {
                lnklogout.Visible = false;
            }
            else
            {
                lnklogout.Visible = true;
            }
        }
    }
}
