<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="RecipesVerification.aspx.cs" Inherits="DDRPOC.RecipesVerification" %>

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
            <asp:Label ID="lblrpttitle" runat="server" Text="Recipe Verification:" Font-Bold="True" 
                Font-Size="Large"></asp:Label><div id="line"></div>
        </td>
    </tr>
    <%--<tr>
        <td class="style4" colspan=4>
            &nbsp;</td>
        
    </tr>--%>
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
        <td  colspan="4">
            &nbsp;</td>
    </tr>

    <tr>
        <td  colspan="4">
            &nbsp;</td>
    </tr>

    <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Recipe Extracts" 
                 Font-Bold="True" 
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
                    <asp:ListItem Value="1">Extract Recipe Header by Plant</asp:ListItem>
                    <asp:ListItem Value="2">Extract Recipe Operations by Plant</asp:ListItem>
                    <asp:ListItem Value="3">Extract Recipe Secondary Resource by Plant</asp:ListItem>
                    <asp:ListItem Value="4">Extract Recipe Operation Relationships by Plant</asp:ListItem>
                    <asp:ListItem Value="5">Extract Recipe User Fields by Plant</asp:ListItem>
                    <asp:ListItem Value="6">Extract Recipe Material Allocations by Plant</asp:ListItem>
                    <asp:ListItem Value="7">Extract Recipe Production Versions by Plant</asp:ListItem>
                </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
         
         
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




