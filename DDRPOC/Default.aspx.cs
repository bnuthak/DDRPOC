using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.Entity;

namespace DDRPOC
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {                                                              
            try
            {
                DDRSessionEntity.Current.SiteCode = Request.QueryString["name"].Replace(DDRSessionEntity.Current.SecureSplit, "-").Split('-')[0];
                DDRSessionEntity.Current.ISOCode = Request.QueryString["name"].Replace(DDRSessionEntity.Current.SecureSplit, "-").Split('-')[1];
                //  Quickly refreshes the page without Site, Country, or Secure Code in the URL so the customers don't know that they can change them.
                //  \/\/\/  Uncomment this portion if you want the Site, Country, or Secure Code to be hidden.  \/\/\/                                    
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception)
            {
                try
                {
                    string previousPage = Request.UrlReferrer.ToString(); // Hits Exception if you're trying to edit the URL on the /Default.aspx page.

                    if (previousPage == "/Default.aspx") // Hits if you backed up away from the Default.aspx page and tried to manually change your Schema
                    {
                        Response.Redirect("http://giphy.com/gifs/5ftsmLIqktHQA/html5");
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("http://giphy.com/gifs/5ftsmLIqktHQA/html5");
                }

            }

            Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
            lblsitecode.Text = DDRSessionEntity.Current.SiteCode;

            if (Session["userid"] != null)
            {
                Label lbluserid = (Label)Page.Master.FindControl("lbluserid");
                lbluserid.Text = Session["userid"].ToString(); 
            }
        }
    }
}
