<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="MMVerificationPlantData.aspx.cs" Inherits="DDRPOC.MMVerificationPlantData" %>

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
            <asp:Label ID="lblrpttitle" runat="server" Text="MM Verification Plant Data:" Font-Bold="True" 
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
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Plant View Extracts" 
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
                <asp:ListItem Value="1">Accounting 1 </asp:ListItem>
                <asp:ListItem Value="2">Costing 1 </asp:ListItem>
                <asp:ListItem Value="3">Foreign Trade Import </asp:ListItem>
                <asp:ListItem Value="4">Foreign Trade Export </asp:ListItem>
                <asp:ListItem Value="5">MRP 1 </asp:ListItem>
                <asp:ListItem Value="6">MRP 2 </asp:ListItem>
                <asp:ListItem Value="7">MRP 3 </asp:ListItem>
                <asp:ListItem Value="8">MRP 4 </asp:ListItem>
                <asp:ListItem Value="9">MRP 1-4 Combined </asp:ListItem>
                <asp:ListItem Value="10">MRP Area </asp:ListItem>
                <asp:ListItem Value="11">Purchasing </asp:ListItem>
                <asp:ListItem Value="12">QM </asp:ListItem>
                <asp:ListItem Value="13">QM fields across MMS </asp:ListItem>
                <asp:ListItem Value="14">Storage Locations </asp:ListItem>
                <asp:ListItem Value="15">Storage Plant Data </asp:ListItem>
                <asp:ListItem Value="16">Storage Fields (Location, Haz, Issue) </asp:ListItem>
                <asp:ListItem Value="17">Work Scheduling </asp:ListItem>
                <asp:ListItem Value="18">Custom Tarrif Prefs </asp:ListItem>
                <asp:ListItem Value="19">Legal Control </asp:ListItem>
                <asp:ListItem Value="20">Zcountry List </asp:ListItem>
                </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>

    </tr>
    <tr>
         <td style="width:50%">
         <asp:CheckBox ID="chkmainbox2" runat="server" Text="Additional Options" 
                 Font-Bold="True" 
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
                <asp:ListItem Value="21">Basic Data by Plant </asp:ListItem>
                <asp:ListItem Value="22">English Descriptions by Plant </asp:ListItem>
                <asp:ListItem Value="23">Alternate UoM by Plant </asp:ListItem>
                <asp:ListItem Value="24">Profit Center by Plant </asp:ListItem>
                <asp:ListItem Value="25">Storage Global Fields by Plant </asp:ListItem>
                <asp:ListItem Value="26">Transportation Group by Plant </asp:ListItem>
                <asp:ListItem Value="27">Classification by Plant </asp:ListItem>
                <asp:ListItem Value="28">AIU and Profit Center </asp:ListItem>
                <asp:ListItem Value="29">Accounting and Costing </asp:ListItem>
            </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>

    </tr>
    <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox3" runat="server" Text="Classification" 
                 Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox3_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates3" runat="server" AutoPostBack = "true">        
                <asp:ListItem Value="30">Expire and Mfg Date Formats by Plant </asp:ListItem>
                <asp:ListItem Value="31">US_CHARS by Plant </asp:ListItem>
                <asp:ListItem Value="32">Z_GLOBAL by Plant </asp:ListItem>
                <asp:ListItem Value="33">Z_GLOBAL by Plant - Financial </asp:ListItem>
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




