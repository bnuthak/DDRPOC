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

namespace DDRPOC
{
    public partial class ConsistencyChecksRecipeConsistency : System.Web.UI.Page
    {
        Common exportreport = new Common();
        PlantInfo plantcode = new PlantInfo();
        ToRecipeGroupInfo torecipegroupcode = new ToRecipeGroupInfo();
        FromRecipeGroupInfo fromrecipegroupcode = new FromRecipeGroupInfo();
        RecipeNumberInfo recipenumbercode = new RecipeNumberInfo();
        protected void Page_Load(object sender, EventArgs e)
        {

            // Only Admins (DS or DM is "Y") can view this page.
            if (!DDRSessionEntity.Current.isUserAnAdmin) Response.Redirect("~\\Blocked.aspx", true);

            //  ClientScript.RegisterStartupScript(typeof(Page), "MessagePopUp", "window.location.href = 'Home.aspx';", true);
            if (!IsPostBack)
            {
                bindwithplantcode();
                bindtorecipegroupcode();
                bindfromrecipegroupcode();
                bindrecipenumbercode();
                bindwithsapinstance();
            }
            lblselectmessage.Visible = false;
            if (!IsPostBack)
            {
                Label mylabelControl = (Label)Page.Master.FindControl("lbluserid");
                mylabelControl.Text = Session["userid"].ToString();
                Label lblsitecode = (Label)Page.Master.FindControl("lblsitecode");
                lblsitecode.Text = DDRSessionEntity.Current.SiteCode;
            }
        }
        protected void isbrowser_CheckedChanged(object sender, EventArgs e)
        {
            isexcel.Checked = false;
        }
        protected void isexcel_CheckedChanged(object sender, EventArgs e)
        {
            isbrowser.Checked = false;
        }

        protected void btngetReport_Click(object sender, EventArgs e)
        {
            DDRSessionEntity.Current.mapinstance = ddmapinstance.SelectedItem.ToString();
            Dictionary<string, string> checklist = new Dictionary<string, string>();

            foreach (ListItem item in chklstStates.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);
                }
            }
            //find the number of check box selected.
            DDRSessionEntity.Current.checkcount = checklist;
            //if (chklstStates.SelectedItem == null)
            if ((lstplantcode.SelectedItem == null) ||
                (checklist == null) ||
                (lstfromrecipegroupcode.SelectedItem == null) ||
                (lsttorecipegroupcode.SelectedItem == null) ||
                (lstrecipenumbercode.SelectedItem == null))
            {
                //MyTrace("Please select Plant and Report....");
                lblselectmessage.Visible = true;
                lblselectmessage.Text = "Please select plant, from recipe group, to recipe group, recipe number, and report from the below list.";
            }
            else
            {
                DDRSessionEntity.Current.reportType = "ConsistencyChecksRecipeConsistency"; // Setting the report type for this session - used in Common.cs        
                Session["plantcode"] = lstplantcode.SelectedItem.Value.ToString();
                Session["torecipegroupcode"] = lsttorecipegroupcode.SelectedItem.Value.ToString();
                Session["fromrecipegroupcode"] = lstfromrecipegroupcode.SelectedItem.Value.ToString();
                Session["recipenumbercode"] = lstfromrecipegroupcode.SelectedItem.Value.ToString();

                //Concate the selecte multiple plancode from the list.
                List<string> SelectedPlantCode = new List<string>();
                foreach (ListItem lst in lstplantcode.Items)
                {
                    if (lst.Selected == true)
                    {
                        SelectedPlantCode.Add(lst.Value);
                    }
                }

                List<string> SelectedTorecipegroupCode = new List<string>();
                foreach (ListItem lst in lsttorecipegroupcode.Items)
                {
                    if (lst.Selected == true)
                    {
                        SelectedTorecipegroupCode.Add(lst.Value);
                    }
                }

                List<string> SelectedFromrecipegroupCode = new List<string>();
                foreach (ListItem lst in lstfromrecipegroupcode.Items)
                {
                    if (lst.Selected == true)
                    {
                        SelectedFromrecipegroupCode.Add(lst.Value);
                    }
                }
                List<string> SelectedrecipenumberCode = new List<string>();
                foreach (ListItem lst in lstrecipenumbercode.Items)
                {
                    if (lst.Selected == true)
                    {
                        SelectedrecipenumberCode.Add(lst.Value);
                    }
                }
                DDRSessionEntity.Current.plantcode = string.Join(",", SelectedPlantCode.Select(x => string.Format("'{0}'", x)));
                DDRSessionEntity.Current.torecipegroupcode = string.Join(",", SelectedTorecipegroupCode.Select(x => string.Format("'{0}'", x)));
                DDRSessionEntity.Current.fromrecipegroupcode = string.Join(",", SelectedFromrecipegroupCode.Select(x => string.Format("'{0}'", x)));
                DDRSessionEntity.Current.recipenumbercode = string.Join(",", SelectedrecipenumberCode.Select(x => string.Format("'{0}'", x)));
                // UserEntity.plantcode = lstplantcode.SelectedItem.Value.ToString();
                if (isexcel.Checked == true)
                {
                    Excel_To_Export(exportreport.GetSelectedDataTable());
                }
                else
                {
                    Response.Redirect("~\\Report_Output.aspx", true);
                }
            }
        }
        protected void MyTrace(string msg)
        {
            Response.Write("<script>alert('" + msg + "')</script>");
        }
        

        private void bindwithplantcode()
        {
            lstplantcode.DataSource = plantcode.GetPlantInfo();
            lstplantcode.DataBind();
        }
        private void bindtorecipegroupcode()
        {
            lsttorecipegroupcode.DataSource = torecipegroupcode.GetToRecipeGroupInfo();
            lsttorecipegroupcode.DataBind();
        }

        private void bindfromrecipegroupcode()
        {
            lstfromrecipegroupcode.DataSource = fromrecipegroupcode.GetFromRecipeGroupInfo();
            lstfromrecipegroupcode.DataBind();
        }
        private void bindrecipenumbercode()
        {
            lstrecipenumbercode.DataSource = recipenumbercode.GetRecipeNumberInfo();
            lstrecipenumbercode.DataBind();
        }
        protected void Excel_To_Export(List<DataTable> GetSelectedDataTable)
        {
            try
            {
                //var filePath = @"c:\Temp\MMAuditingReport.csv";
                // var filePath1 = Server.MapPath("~\\Temp\\MMAuditing.csv");
                DeleteFolder(DDRSessionEntity.Current.username);
                CreateFolder(DDRSessionEntity.Current.username);
                var filePath = Server.MapPath(string.Format("~/{0}/{1}/{2}", "Temp", DDRSessionEntity.Current.username, "RecipeConsistency.xls"));

                for (int count = 0; count < GetSelectedDataTable.Count; count++)
                {
                    exportreport.CreateCSVFile(GetSelectedDataTable[count], filePath, GetSelectedDataTable[count].TableName);
                }

                Download_File(filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Download the the file in client machine
        private void Download_File(string FilePath)
        {
            try
            {
                HttpContext.Current.Response.ContentType = ContentType;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
                HttpContext.Current.Response.WriteFile(FilePath);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Delete the user specific report file 
        protected void DeleteFile(string FilePath)
        {
            if (Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                var filestream = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                filestream.Close();
                File.Delete(FilePath);
            }
        }
        //Create the user specific report folder
        protected void CreateFolder(string username)
        {
            string directoryPath = Server.MapPath(string.Format("~/{0}/{1}", "Temp", username));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Folder already exists.');", true);
            }
        }
        //Delete the user specific report folder 
        protected void DeleteFolder(string username)
        {
            string directoryPath = Server.MapPath(string.Format("~/{0}/{1}", "Temp", username));
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Directory does not exist.');", true);
            }
        }
        private void bindwithsapinstance()
        {
            ddmapinstance.DataSource = DDRSessionEntity.Current.userSAPList;
            ddmapinstance.DataBind();
        }
        protected void chkmainbox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates.Items)
            {
                if (oItem.Selected == false)
                {
                    oItem.Selected = true;
                }
                else
                {
                    oItem.Selected = false;
                }
            }
        }

    }
}
            
