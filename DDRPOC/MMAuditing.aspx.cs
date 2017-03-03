using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDR.BusinessLogic;
using DDR.Entity;
using System.Data;
using System.IO;

namespace DDRPOC
{
    public partial class MMAuditing : System.Web.UI.Page
    {        
        
        Common exportreport = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {         
               
            lblselectmessage.Visible = false;                        
            if (!IsPostBack)
            {
                
                bindwithsapinstance();
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
            foreach (ListItem item in chklstStates2.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                 
                }
            }
            foreach (ListItem item in chklstStates3.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);              
                }
            }
            foreach (ListItem item in chklstStates4.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                  
                }
            }
            foreach (ListItem item in chklstStates5.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates6.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates7.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates8.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates9.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates10.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates11.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates12.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates13.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates14.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                   
                }
            }
            foreach (ListItem item in chklstStates15.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                    
                }
            }
            foreach (ListItem item in chklstStates16.Items)
            {
                if (item.Selected)
                {
                    checklist.Add(item.Value, item.Text);                  
                }
            }
            //find the number of check box selected.
            DDRSessionEntity.Current.checkcount = checklist;
            //if (chklstStates.SelectedItem == null)
            if (checklist == null)
            {
                //Common.MyTrace("Please select one report from the list.");
                lblselectmessage.Text = "Please select one report from the list.";
                lblselectmessage.Visible = true;
            }
            else 
            {
                DDRSessionEntity.Current.reportType = "MMAuditing"; // Setting the report type for this session - used in Common.cs
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
        private void bindwithsapinstance()
        {
            ddmapinstance.DataSource = DDRSessionEntity.Current.userSAPList;
            ddmapinstance.DataBind();
        }
        protected void Excel_To_Export(List<DataTable> GetSelectedDataTable)
        {
            try
            {
               //var filePath = @"c:\Temp\MMAuditingReport.csv";
               // var filePath1 = Server.MapPath("~\\Temp\\MMAuditing.csv");
                DeleteFolder(DDRSessionEntity.Current.username);
                CreateFolder(DDRSessionEntity.Current.username);
                var filePath = Server.MapPath(string.Format("~/{0}/{1}/{2}","Temp", DDRSessionEntity.Current.username, "MMAuditing.xls"));
                
                for (int count = 0; count < GetSelectedDataTable.Count; count++)
                {
                    exportreport.CreateCSVFile(GetSelectedDataTable[count], filePath, GetSelectedDataTable[count].TableName);
                }

                Download_File( filePath);
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
            string directoryPath = Server.MapPath(string.Format("~/{0}/{1}","Temp", username));
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
                Directory.Delete(directoryPath,true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Directory does not exist.');", true);
            }
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


        protected void chkmainbox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates2.Items)
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

        protected void chkmainbox3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates3.Items)
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

        protected void chkmainbox4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates4.Items)
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

        protected void chkmainbox5_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates5.Items)
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

        protected void chkmainbox6_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates6.Items)
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

        protected void chkmainbox7_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates7.Items)
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

        protected void chkmainbox8_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates8.Items)
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

        protected void chkmainbox9_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates9.Items)
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

        protected void chkmainbox10_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates10.Items)
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

        protected void chkmainbox11_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates11.Items)
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

        protected void chkmainbox12_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates12.Items)
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

        protected void chkmainbox13_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates13.Items)
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

        protected void chkmainbox14_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates14.Items)
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

        protected void chkmainbox15_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates15.Items)
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

        protected void chkmainbox16_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem oItem in chklstStates16.Items)
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
