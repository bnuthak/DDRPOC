using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Common;
using DDR.BusinessLogic;
using DDR.Entity;
using System.Data;

namespace DDRPOC
{
    public partial class Home : System.Web.UI.Page
    {
        string user = string.Empty;
        string Password = string.Empty;
        string defaultOracleInstance = string.Empty;
        Common commmanager = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {                        

            user = ((TextBox)LoginUser.FindControl("UserName")).Text;
            Password = ((TextBox)LoginUser.FindControl("Password")).Text;

            if ((HttpContext.Current.Request.Url.AbsoluteUri.Contains("local")) || // Dev or Local (via GUI)
                (HttpContext.Current.Request.Url.AbsoluteUri.Contains("ddr-d")))
            {                
                DDRSessionEntity.Current.defaultOracleInstance = "dev_49";
                DDRSessionEntity.Current.OraclePort = "credit.d51.lilly.com";
                DDRSessionEntity.Current.OracleServer = "1525";
            }
            else // Qual or Prod
            {               
                DDRSessionEntity.Current.defaultOracleInstance = "prd_49";
                DDRSessionEntity.Current.OraclePort = "refund.am.lilly.com";
                DDRSessionEntity.Current.OracleServer = "1526";
            }

            Control myControlMenu = Page.Master.FindControl("NavigationMenu");
            Control sitemasterupdpnl = Page.Master.FindControl("MainContentPanel");
            //(Panel)sitemasterupdpnl.s
            if (myControlMenu != null)
            {
                myControlMenu.Visible = false;
            }
            ((Label)LoginUser.FindControl("lblnopassworduser")).Visible = false;

            if (!IsPostBack)
            {
                Label mylabelControl = (Label)Page.Master.FindControl("lbluserid");
                mylabelControl.Text = "Not Set";
                Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
                lblsitecode.Text = "Not Set";
                
                Session["userid"] = "Not Set";
                DDRSessionEntity.Current.SiteCode = "Not Set";
            }
             
        }

        SiteCode sitecode = new SiteCode();
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
        protected void LoginButton_Click1(object sender, EventArgs e)
        {
            defaultOracleInstance = DDRSessionEntity.Current.defaultOracleInstance;
            try
            {
                if (commmanager.ValidateUserAccess(user, Password, defaultOracleInstance))
                {
                  
                    Get_Active_Schema_And_Site(user.ToUpper());

                    Session["userid"] = user.ToUpper().ToString();

                    // Randomly generates a 10-digit secure code and assigns it to the session, so the customer
                    // cannot change their Site or Country in the URL as they're passed from Oracle.
                    var random = new Random();
                    string security_string = "";
                    for (int i = 0; i < 10; i++)
                    {
                        security_string = String.Concat(security_string, random.Next(10).ToString());
                    }
                    DDRSessionEntity.Current.SecureSplit = security_string;                   

                    if ((DDRSessionEntity.Current.can_choose_country == "Y") && (DDRSessionEntity.Current.can_choose_schema == "Y"))
                    {
                        Control myControlMenu = Page.Master.FindControl("NavigationMenu");

                        if (myControlMenu != null)
                        {
                            myControlMenu.Visible = true;
                        }
                        //Response.Redirect("About.aspx");
                        Response.Redirect("DBSchema.aspx");
                    }
                    else if ((DDRSessionEntity.Current.can_choose_country == "Y") && (DDRSessionEntity.Current.can_choose_schema == "N"))
                    {
                        Response.Redirect("About.aspx?dsm=" + DDRSessionEntity.Current.code_schema);
                    }
                    else if ((DDRSessionEntity.Current.can_choose_country == "N") && (DDRSessionEntity.Current.can_choose_schema == "Y"))
                    {
                        //UserEntity.username = user.ToUpper().ToString();
                        Response.Redirect("DBSchema.aspx");
                    }
                    else  // Cannot Choose Country AND Cannot Choose Schema
                    {
                        BindCountrySite();                        
                        Response.Redirect("~/Default.aspx?name=" + DDRSessionEntity.Current.SiteCode + DDRSessionEntity.Current.SecureSplit + DDRSessionEntity.Current.CountryCode);
                    }
                }
                else
                {
                    ((Label)LoginUser.FindControl("lblnopassworduser")).Visible = true;
                    ((Label)LoginUser.FindControl("lblnopassworduser")).Text = "Access denied...Please check your database access.";
                }
            }
            catch (Exception ex)
            {
                ((Label)LoginUser.FindControl("lblnopassworduser")).Visible = true;
                ((Label)LoginUser.FindControl("lblnopassworduser")).Text = ex.Message.ToString();
              //  throw ex; 
            }
        }        
        public void Get_Active_Schema_And_Site(string userid)//
        {
            List<string> tempSAPList = new List<string>() { "GDV", "GQA", "GPR", "GBP", "STJ", "STG" };
            try
            {
                  // If any of these break, they'll need to be put in Try statements just like the DS/DM below.
                System.Data.DataSet userschemadata = commmanager.Active_Schema_And_Site(userid);
                var rows_can_choose_country = userschemadata.Tables[0].Select("PROP_NAME='CAN_CHOOSE_COUNTRY'");
                DDRSessionEntity.Current.can_choose_country = rows_can_choose_country[0].ItemArray[3].ToString();

                var rows_can_choose_schema = userschemadata.Tables[0].Select("PROP_NAME = 'CAN_CHOOSE_SCHEMA'");
                DDRSessionEntity.Current.can_choose_schema = rows_can_choose_schema[0].ItemArray[3].ToString();

                var default_site_code = userschemadata.Tables[0].Select("PROP_NAME = 'USER_SITE'");
                DDRSessionEntity.Current.SiteCode = default_site_code[0].ItemArray[3].ToString(); // Gets overridden if user selects a different site.  (Ex: SE, W5)                          

                DDRSessionEntity.Current.table_schema = rows_can_choose_schema[0].ItemArray[1].ToString();

                var rows_code_schema = userschemadata.Tables[0].Select("PROP_NAME = 'CODE_SCHEMA'");
                DDRSessionEntity.Current.code_schema = rows_code_schema[0].ItemArray[3].ToString().Split('_')[0]; // Gets overridden if user selects a different schema. (SCM/SC3)

                // Set Default SAP Instance
                try
                {
                    DDRSessionEntity.Current.defaultSAPInstance = userschemadata.Tables[0].Select("PROP_NAME = 'USER_SAPINSTANCE'")[0].ItemArray[3].ToString();
                    tempSAPList.Remove(DDRSessionEntity.Current.defaultSAPInstance); // Removes the user's default SAP instc from wherever it used to be in the list.
                    tempSAPList.Insert(0, DDRSessionEntity.Current.defaultSAPInstance); // Places the user's default SAP instc at the beginning of the list.
                    DDRSessionEntity.Current.userSAPList = tempSAPList;
                }
                catch (Exception) // If user doesn't have an assigned default SAP instance
                {
                    DDRSessionEntity.Current.userSAPList = tempSAPList; // ["GDV","GQA","GPR","GBP","STJ","STG"] 
                }

                //////////////////////////  Is User an Admin ////////////////////////////////

                string isDSUser = "N";
                string isDMUser = "N";

                // Must be inside Try statement because not all users have the Property set for DS or DM.
                try
                {
                    isDSUser = userschemadata.Tables[0].Select("PROP_NAME='IS_DS_USER'")[0].ItemArray[3].ToString();
                }
                catch (Exception) { }

                try
                {
                    isDMUser = userschemadata.Tables[0].Select("PROP_NAME='IS_DM_USER'")[0].ItemArray[3].ToString();
                }
                catch (Exception) { }

                
                if ((isDSUser == "Y") ||
                    (isDMUser == "Y"))
                {
                    DDRSessionEntity.Current.isUserAnAdmin = true;
                }
                else
                {
                    DDRSessionEntity.Current.isUserAnAdmin = false;
                }

                /////////////////////////////////////////////////////////////////////////////


                // Is the User in Local Environment, Dev DDR, Q DDR, or Production DDR? (Based on URL)
                if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("localhost"))
                {
                    DDRSessionEntity.Current.ddrInstance = "L"; // User is testing in Local environment
                }
                else if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("ddr-d"))
                {
                    DDRSessionEntity.Current.ddrInstance = "D"; // User is on the D server                    
                }
                else if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("ddr-q"))
                {
                    DDRSessionEntity.Current.ddrInstance = "Q"; // User is on the Q server                    
                }
                else
                {
                    DDRSessionEntity.Current.ddrInstance = "P"; // User is on the P server                    
                }
                                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}