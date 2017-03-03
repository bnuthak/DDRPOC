using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDR.Entity;
using DDR.DataAccess;


namespace DDR.BusinessLogic
{
    public class UserAccount
    {
        QMDataManager qmmanager = new QMDataManager();
        public void GetNumberUser(string systemId)
        {
            try
            {
                string command =
                String.Format("select count(user_name) from gdd_user_properties where user_name='{0}'", systemId);
                DDRSessionEntity.Current.IsUserCount = qmmanager.ExecuteCommand(command);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
