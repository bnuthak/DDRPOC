<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsistencyChecksMMClashes.aspx.cs" Inherits="DDRPOC.ConsistencyChecksMMClashes" %>

 <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <table style="width: 100%;">
    <tr>
       <td style="width: 10%;">
            <asp:Label ID="Label5" runat="server" Text="Output Format:" Font-Bold="True"></asp:Label>
        </td>
        <td style="width: 10%;">
            &nbsp;
            <asp:RadioButton ID="isbrowser" runat="server" Text="Browser" 
                GroupName = "report" Checked="True" oncheckedchanged="isbrowser_CheckedChanged"/>  
        </td>
        <td style="width: 10%;">
            &nbsp;
            <asp:RadioButton ID="isexcel" runat="server" Text="Excel" GroupName = "report" oncheckedchanged="isexcel_CheckedChanged"/>  
        </td>
        <td style="width: 70%;">
            <asp:Button ID="Button1" runat="server" style="margin-left: 0px" 
                Text="Get Report" onclick="btngetReport_Click" class="submitButton"/>
                    </td>
    </tr>
    <tr>
        <td >
            &nbsp;
        </td>
        <td >
            &nbsp;
        </td>
        <td >
            &nbsp;
        </td>
        <td>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label456" runat="server" Text="Clash Checks" Font-Bold="True" 
                Font-Size="Large"></asp:Label>
        </td>
    </tr>
         <tr>
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" Text="Note: Refresh Clash Tables Prior to Running These Reports" Font-Bold="True" 
                Font-Size="Medium"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>

    <tr>
        <td colspan="4">
            &nbsp;<label ><strong style="font-size: small" >Some Reports Require Selecting an SAP Instance.  Please Do So Here:</strong></label>
            <asp:DropDownList ID="ddmapinstance" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
   <%-- <tr>
     <td colspan=4 align="left">
      <hr width="100%" /> 
      </td>
    </tr>--%>

         <tr>
             <td>
             </td>
         </tr>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox" runat="server" Text="MM Clash Checks" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="lblselectmessage" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="updpnlmmautiding" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="MARA Clash Check (Global data)" Value="1"></asp:ListItem>
                <asp:ListItem Text="MARM Clash Check (Alt Unit Of Measure)" Value="2"></asp:ListItem>
                <asp:ListItem Text="MAKT Clash Check (Description)" Value="3"></asp:ListItem>
                <asp:ListItem Text="MEAN Clash Check (Additional EAN Num)" Value="4"></asp:ListItem>
                <asp:ListItem Text="MLAN Clash Check (Taxes)" Value="5"></asp:ListItem>
                <asp:ListItem Text="Proportional Unit Clash Check" Value="6"></asp:ListItem>
                <asp:ListItem Text="Classification Clash Check" Value="7"></asp:ListItem>
                <asp:ListItem Text="MRSL, TSL, Batch Mgt Clash Check w/Bom" Value="8"></asp:ListItem>
                <asp:ListItem Text="Format ID Clash Check w/Bom" Value="9"></asp:ListItem>
            </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
    </tr>


    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>

    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>




         <tr>
         <td style="width:50%">
         <asp:CheckBox ID="chkmainbox2" runat="server" Text="MM Clash Checks by View" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox2_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="lblselectmessage2" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates2" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Account & Costing Clash Check" Value="10"></asp:ListItem>
                <asp:ListItem Text="Alt Unit of Measure Clash Check" Value="11"></asp:ListItem>
                <asp:ListItem Text="MRP Clash Check" Value="12"></asp:ListItem>
                <asp:ListItem Text="Purchasing Clash Check" Value="13"></asp:ListItem>
                <asp:ListItem Text="QM Clash Check" Value="14"></asp:ListItem>
                <%--<asp:ListItem Text="QM Clash Check - IBO only, 0131 vs. Z131" Value="15"></asp:ListItem>
                <asp:ListItem Text="QM Clash Check - DDAO only, 0413 vs. Z413" Value="16"></asp:ListItem>
                <asp:ListItem Text="QM Clash Check - Indy Parenteral only, 0314 vs. Z314" Value="17"></asp:ListItem>
                <asp:ListItem Text="QM Clash Check - Indy Dry only, 0004 vs. Z004" Value="18"></asp:ListItem>--%>
                <asp:ListItem Text="Storage Clash Check" Value="19"></asp:ListItem>
                <asp:ListItem Text="Tax Clash Check" Value="20"></asp:ListItem>
                <asp:ListItem Text="Work Scheduling Clash Check" Value="21"></asp:ListItem>
            </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
    </tr>

    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>

    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>




        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox3" runat="server" Text="MM Clash Checks - Full DDT Scan" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox3_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates3" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="MARA - Global" Value="22"></asp:ListItem>
                <asp:ListItem Text="MAKT - Descriptions" Value="23"></asp:ListItem>
                <asp:ListItem Text="MARM - Alt UoM" Value="24"></asp:ListItem>
                <asp:ListItem Text="MEAN - Additional EAN" Value="25"></asp:ListItem>
                <asp:ListItem Text="AUSP - Classification" Value="26"></asp:ListItem>
                <asp:ListItem Text="Format ID Clash" Value="27"></asp:ListItem>
                <asp:ListItem Text="MRSL, TSL, Batch Mgt" Value="28"></asp:ListItem>
            </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
    </tr>


    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>


    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>
    
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .style4
        {
            width: 176px;
        }
        </style>
</asp:Content>




