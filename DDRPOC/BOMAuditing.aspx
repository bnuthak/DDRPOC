<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="BOMAuditing.aspx.cs" Inherits="DDRPOC.BOMAuditing" %>

 <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


<table style="width: 100%;">
    <tr>
        
                    <td style="width: 10%;">
            <asp:Label ID="Label5" runat="server" Text="Output Format:" Font-Bold="True"></asp:Label>
        </td>
        <td style="width: 10%;">
            &nbsp;
            <asp:RadioButton ID="isbrowser" runat="server" Text="Browser" 
                GroupName = "report" Checked="True" oncheckedchanged="isbrowser_CheckedChanged"  
                    />
        </td>
        <td style="width: 10%;">
            &nbsp;
            <asp:RadioButton ID="isexcel" runat="server" Text="Excel" GroupName = "report" oncheckedchanged="isexcel_CheckedChanged"  
                  />
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
          <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
        </td>
    </tr>
    <tr>
        <td colspan=4>
            <asp:Label ID="lblrpttitle" runat="server" Text="BOM Auditing:" Font-Bold="True" 
                Font-Size="Large"></asp:Label><div id="line"></div>
        </td>
        
    </tr>
    <%--<tr>
        <td class="style4" colspan=4>
            &nbsp;</td>
        
    </tr>--%>
    
    <tr>
        <td  colspan="4">
            &nbsp;</td>
    </tr>


    <tr>
        <td width="10%">
            &nbsp;<label ><strong>Plant:</strong></label>
            </td>
          <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="lblselectmessage" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td  colspan="2">
            <asp:ListBox ID="lstplantcode" runat="server" Width="100" Height="150" SelectionMode="Multiple"></asp:ListBox>
        </td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>

    <tr>
        <td colspan="4" nowrap="nowrap">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">

            <asp:DropDownList ID="ddmapinstance" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
         
        
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>


            <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Basic Data View Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="updpnlmmautiding" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="LF - Bom Header UoM not Material's Base UoM" Value="1"></asp:ListItem>
                <asp:ListItem Text="LF - Components not setup in BOM's Plant" Value="2"></asp:ListItem>
                <asp:ListItem Text="BR - BOM Header Non-Matching Ztext Errors " Value="3"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Detail Recursive but flag not set" Value="4"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Detail Commas in Quantity(MENGE) " Value="5"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Detail Decimal Length" Value="6"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Header Decimal Length" Value="7"></asp:ListItem>
                <asp:ListItem Text="LF - Bom Component UoM not Base UoM and Not setup in Alt UoM View" Value="8"></asp:ListItem>
                <asp:ListItem Text="LF - Component UoM/Unit of Issue Errors " Value="9"></asp:ListItem>
                <asp:ListItem Text="LF - Bulk Component Checked with Storage Location" Value="10"></asp:ListItem>
                <asp:ListItem Text="LF - Bulk Component Checked with Supply Area" Value="11"></asp:ListItem>
                <asp:ListItem Text="LF - Costing BOM with Issue Storage Location" Value="12"></asp:ListItem>
                <asp:ListItem Text="LF - Costing BOM with Supply Area" Value="13"></asp:ListItem>
                <asp:ListItem Text="LF - Supply Area Without Storage Location" Value="14"></asp:ListItem>
                <asp:ListItem Text="FYI - BOM/Work Scheduling/Recipe Comparison" Value="15"></asp:ListItem>
                <asp:ListItem Text="BR - Sort String cannot contain C and E" Value="16"></asp:ListItem>
                <asp:ListItem Text="BR - Component Issue Storage Location does not exist in MARD" Value="17"></asp:ListItem>
                <asp:ListItem Text="LF - Component Issue Storage Location is not valid " Value="18"></asp:ListItem>
                <asp:ListItem Text="BR - BOM Header without BOM Details" Value="19"></asp:ListItem>

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

         <%--ACCOUNTING AND COSTING VIEW AUDITS--%>


    <tr>
         <td style="width:50%">
         <asp:CheckBox ID="chkmainbox2" runat="server" Text="Accounting And Costing View Audits" 
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
                <asp:ListItem Text="LF - BOM Component Material Status-Global" Value="20"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Component Material Status-Plant" Value="21"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Header Material Status-Global" Value="22"></asp:ListItem>
                <asp:ListItem Text="LF - BOM Header Material Status-Plant" Value="23"></asp:ListItem>

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




