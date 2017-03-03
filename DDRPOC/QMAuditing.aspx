<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="QMAuditing.aspx.cs" Inherits="DDRPOC.QMAuditing" %>

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
            <asp:Label ID="lblrpttitle" runat="server" Text="QM Auditing:" Font-Bold="True" 
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
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Audit Reports" 
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
                    <asp:ListItem Value="1">Exceptional data Listing for Group Counter </asp:ListItem>
                    <asp:ListItem Value="2">No QM View for Insp Type </asp:ListItem>
                    <asp:ListItem Value="3">QM View without Insp_Type </asp:ListItem>
                    <asp:ListItem Value="4">Inspection Type/Inspection Plan Exception List    </asp:ListItem>
                    <asp:ListItem Value="5">Material Classification View</asp:ListItem>
                    <asp:ListItem Value="6">Exceptions List for Inspection Intervals</asp:ListItem>
                    <asp:ListItem Value="7">Materials mfd by your site expires and are not re-evaluated.</asp:ListItem>
                    <asp:ListItem Value="8">Materials not mfd by your site expires and are not re-evaluated.</asp:ListItem>
                    <asp:ListItem Value="9">Materials re-evaluated and does not expire.</asp:ListItem>
                    <asp:ListItem Value="10">Materials not re-evaluated and does not expire </asp:ListItem>
                    <asp:ListItem Value="11">Materials re-evaluated and expire</asp:ListItem>
                    <asp:ListItem Value="12">Material Expires,  09 Insp Type Missing (does not use plant box)</asp:ListItem>
                    <asp:ListItem Value="13">Material has Inspection Interval,  09 Insp Type Missing (does not use plant box)</asp:ListItem>
                </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
         
         
    </tr>

        <tr>
         <td style="width:50%">
         <asp:CheckBox ID="chkmainbox2" runat="server" Text="Extracts for Procurement Types" 
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
                    <asp:ListItem Value="14">In House Production Final Inspection</asp:ListItem>
                    <asp:ListItem Value="15">External Procurement receiving Inspection</asp:ListItem>
                    <asp:ListItem Value="16">Procurement & Production Inspection</asp:ListItem>
                    <asp:ListItem Value="17">04 Type Missing - Procurement E, Control Key 0000</asp:ListItem>
                    <asp:ListItem Value="18">01 Type Missing - Procurement F, Control Key 0001</asp:ListItem>
                    <asp:ListItem Value="19">04 Type Missing - Procurement X</asp:ListItem>
                    <asp:ListItem Value="20">01 Type Missing - Procurement X</asp:ListItem>
                    <asp:ListItem Value="21">Procurement E Should not have Control Key 0001</asp:ListItem>
                    <asp:ListItem Value="22">Procurement F Should not have Control Key 0000</asp:ListItem>
                </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
         
         
    </tr>       
        
    <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox3" runat="server" Text="Extracts for Inspection Type & Cont Insp.Lot:" 
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
                    <asp:ListItem Value="23">Insp.type = 01 & Cont.Insp.Lot Not Blank</asp:ListItem>
                    <asp:ListItem Value="24">Insp.type = 08 & Cont.Insp.Lot Not Blank</asp:ListItem>
                    <asp:ListItem Value="25">Insp.type = 04 & Cont.Insp.Lot Not Eq. 'X'</asp:ListItem>
                    <asp:ListItem Value="26">Insp.type = 04 & Cont.Insp.Lot Not Eq. 'Y'</asp:ListItem>
                    <asp:ListItem Value="27">Insp.type = 05 & Cont.Insp.Lot Eq. '2'</asp:ListItem>
                    <asp:ListItem Value="28">Insp.type = 05 & Cont.Insp.Lot Eq. Blank</asp:ListItem>
                    <asp:ListItem Value="29">Insp.type = 05 & Cont.Insp.Lot Not Eq '2' or Blank</asp:ListItem>
                    <asp:ListItem Value="30">Insp Type 01 - Control Insp Lot cannot be empty</asp:ListItem>
                    <asp:ListItem Value="31">Insp Type 04 - Control Insp Lot cannot be empty</asp:ListItem>
                    <asp:ListItem Value="32">Insp Type 0105 - Control Insp Lot cannot be empty</asp:ListItem>
                    <asp:ListItem Value="33">Control Insp Lot should be empty (except 01, 04, 0105)</asp:ListItem>
                </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
         
         
    </tr>       

       <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox4" runat="server" Text="Extracts for Components having BOM sort string A/C/E" 
                 Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox4_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label15" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates4" runat="server" AutoPostBack = "true">
                    <asp:ListItem Value="34">Component List having BOM string A/C/E</asp:ListItem>
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




