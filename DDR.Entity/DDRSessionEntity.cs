using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;


namespace DDR.Entity
{
    public class DDRSessionEntity
    {
        private DDRSessionEntity()
        {
            username = string.Empty;
            password = string.Empty;
            SiteCode = string.Empty;
        }
        /// <summary>
        /// Property to create a session variable.
        /// </summary>
        public static DDRSessionEntity Current
        {
            get
            {
                DDRSessionEntity ddrsession =
                  (DDRSessionEntity)HttpContext.Current.Session["MySession"];
                if (ddrsession == null)
                {
                    ddrsession = new DDRSessionEntity();
                    HttpContext.Current.Session["MySession"] = ddrsession;
                }
                return ddrsession;
            }
        }

       /**********add your session properties here***********/
        

        public string password { get; set; }
        public string username { get; set; }
        public string ddrInstance { get; set; } // Is the user in Local, D, Q, or P
        public string defaultSAPInstance { get; set; } // Based on USER_SAPINSTANCE
        public List<string> userSAPList { get; set; }
        public string defaultOracleInstance { get; set; } // If ddrInstance!=P, User can select DEV_49 or PRD_49
        public string OracleServer { get; set; } // Dev or Prd server
        public string OraclePort { get; set; } // Dev or Prd port number
        public bool isUserAnAdmin { get; set; }
        public string SiteCode { get; set; }
        public string CountryCode { get; set; }
        public string SecureSplit { get; set; }  // Used for security purposes so that the user cannot physically input their schema/country in the URL
        public string ISOCode { get; set; }
        public string plantcode { get; set; }
        public string salesorgcode { get; set; }
        public string tomatnrcode { get; set; }
        public string frommatnrcode { get; set; }
        public string torecipegroupcode { get; set; }
        public string fromrecipegroupcode { get; set; }
        public string recipenumbercode { get; set; }
        public string toresourcecode { get; set; }
        public string fromresourcecode { get; set; }
        public string reportType { get; set; }
        public string mapinstance { get; set; }
        public string table_schema { get; set; }
        public string code_schema { get; set; }
        public int IsUserCount { get; set; }
        public Dictionary<string, string> checkcount { get; set; }
        public string can_choose_country { get; set; }
        public string can_choose_schema { get; set; }        
    }
}
