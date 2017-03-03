using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DDR.BusinessLogic
{
  public class ReportTableHeader
    {
      public DataTable MMAuditingbindtableheader(DataSet mmmulreptable)
      {
          DataTable mmtable = new DataTable();
          mmtable.Columns.Add("Material Num", typeof(string));
          mmtable.Columns.Add("Material Type", typeof(string));
          mmtable.Columns.Add("SAPID", typeof(string));

          DataSet datasettotable = mmmulreptable;//mmreport.SQL_MM_Errors_No_Makt();
          DataRow mmrow;
          for (int row = 0; row < datasettotable.Tables[0].Rows.Count; row++)
          {
              mmrow = mmtable.NewRow();
              mmrow["Material Num"] = datasettotable.Tables[0].Rows[row][0];
              mmrow["Material Type"] = datasettotable.Tables[0].Rows[row][1];
              mmrow["SAPID"] = datasettotable.Tables[0].Rows[row][2];
              mmtable.Rows.Add(mmrow);
          }
          return mmtable;
      }
      /// <summary>
      /// Change Table heading with proper naming convention
      /// </summary>
      /// <returns></returns>
      public DataTable QMVerificationtableheader(DataSet qmveryficationdataset)
      {
          DataTable qmtable = new DataTable();
          qmtable.Columns.Add("Material", typeof(string));
          qmtable.Columns.Add("PLANT", typeof(string));
          qmtable.Columns.Add("Vendor Type", typeof(string));
          qmtable.Columns.Add("SAP Vendor No", typeof(string));
          qmtable.Columns.Add("Name", typeof(string));
          qmtable.Columns.Add("Date_Until_rel valid", typeof(string));
          qmtable.Columns.Add("Status Profile", typeof(string));
          qmtable.Columns.Add("Inspection Control", typeof(string));
          qmtable.Columns.Add("Internal counter", typeof(string));
          qmtable.Columns.Add("Deletion Flag", typeof(string));
          qmtable.Columns.Add("Rel Qnt is active", typeof(string));
          qmtable.Columns.Add("Vendor's QM System", typeof(string));
          qmtable.Columns.Add("Inspection Type", typeof(string));
          qmtable.Columns.Add("Lot Creation Lead Time", typeof(string));
          qmtable.Columns.Add("Blocked Function", typeof(string));
          qmtable.Columns.Add("Reason For Block", typeof(string));
          DataSet datasettotable = qmveryficationdataset;//report.GetQMVerificationTotalReport();
          DataRow qmrow;
          for (int row = 0; row < datasettotable.Tables[0].Rows.Count; row++)
          {
              qmrow = qmtable.NewRow();
              qmrow["Material"] = datasettotable.Tables[0].Rows[row][0];
              qmrow["PLANT"] = datasettotable.Tables[0].Rows[row][1];
              qmrow["Vendor Type"] = datasettotable.Tables[0].Rows[row][2];
              qmrow["SAP Vendor No"] = datasettotable.Tables[0].Rows[row][3];
              qmrow["Name"] = datasettotable.Tables[0].Rows[row][4];
              qmrow["Date_Until_rel valid"] = datasettotable.Tables[0].Rows[row][5];
              qmrow["Status Profile"] = datasettotable.Tables[0].Rows[row][6];
              qmrow["Inspection Control"] = datasettotable.Tables[0].Rows[row][7];
              qmrow["Internal Counter"] = datasettotable.Tables[0].Rows[row][8];
              qmrow["Deletion Flag"] = datasettotable.Tables[0].Rows[row][9];
              qmrow["Rel Qnt is active"] = datasettotable.Tables[0].Rows[row][10];
              qmrow["Vendor's QM System"] = datasettotable.Tables[0].Rows[row][11];
              qmrow["Inspection Type"] = datasettotable.Tables[0].Rows[row][12];
              qmrow["Lot Creation Lead Time"] = datasettotable.Tables[0].Rows[row][13];
              qmrow["Blocked Function"] = datasettotable.Tables[0].Rows[row][14];
              qmrow["Reason For Block"] = datasettotable.Tables[0].Rows[row][15];

              qmtable.Rows.Add(qmrow);
          }
          return qmtable;
      }
    }
}
