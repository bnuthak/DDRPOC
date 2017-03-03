<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryVerification.aspx.cs" Inherits="DDRPOC.InventoryVerification" %>

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
            <asp:Label ID="Label6" runat="server" Text="Verification:" Font-Bold="True" 
                Font-Size="Large"></asp:Label>
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
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Verification:" 
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
                <asp:ListItem Text="(DOESN'T EXIST) Integrated Z603 EE Group/Subgroup" Value="1"></asp:ListItem>
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




