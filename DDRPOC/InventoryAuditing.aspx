<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryAuditing.aspx.cs" Inherits="DDRPOC.InventoryAuditing" %>

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
            <asp:Label ID="Label6" runat="server" Text="Inventory Auditing:" Font-Bold="True" 
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
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Batch Audits" 
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
                <asp:ListItem Text="Invalid Batch Numbers" Value="1"></asp:ListItem>
                <asp:ListItem Text="Split Batch" Value="2"></asp:ListItem>
                <asp:ListItem Text="Batch Use Restriction should not be populated" Value="3"></asp:ListItem>
                <asp:ListItem Text="Batch Use Restriction should be populated" Value="4"></asp:ListItem>
                <asp:ListItem Text="Batch Use Restriction should be populated, Batch NOT already in SAP" Value="5"></asp:ListItem>
                <asp:ListItem Text="Batch Number Should Be Populated" Value="6"></asp:ListItem>
                <asp:ListItem Text="Batch Number Should Not Be Populated" Value="7"></asp:ListItem>
                <asp:ListItem Text="Blank Batch Number" Value="8"></asp:ListItem>
                <asp:ListItem Text="Blank Batch Number w/ Batch Characteristics" Value="9"></asp:ListItem>
                <asp:ListItem Text="Unrestricted Stock - Batch Use Restriction Empty in File and SAP" Value="10"></asp:ListItem>
                <asp:ListItem Text="Invalid Original Manufacturer" Value="11"></asp:ListItem>
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
         <asp:CheckBox ID="chkmainbox2" runat="server" Text="BOM Audits" 
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
                <asp:ListItem Text="FYI - Inventory Not In a BOM" Value="12"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox3" runat="server" Text="Date Audits" 
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
                <asp:ListItem Text="Production Date Must Not Be Populated" Value="13"></asp:ListItem>
                <asp:ListItem Text="Production Date Must Be Populated" Value="14"></asp:ListItem>
                <asp:ListItem Text="PRD Date + TSL = EXP Date Mismatch" Value="15"></asp:ListItem>
                <asp:ListItem Text="Production Date > Today" Value="16"></asp:ListItem>
                <asp:ListItem Text="Expiration Date Must Not Be Populated" Value="17"></asp:ListItem>
                <asp:ListItem Text="Expiration Date Must Be Populated" Value="18"></asp:ListItem>
                <asp:ListItem Text="Manufacturing Start Date Must be Populated for HALB Materials" Value="19"></asp:ListItem>
                <asp:ListItem Text="Manufacturing Start Date > Today" Value="20"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Date AutoCreate > Expiration Date" Value="21"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Date Should Not be Populated" Value="22"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Date Should be Populated" Value="23"></asp:ListItem>
                <asp:ListItem Text="SAP First Goods Receipt Date Must Be Populated" Value="24"></asp:ListItem>
                <asp:ListItem Text="Batch in File and SAP, but Next Inspection Date not Populated and is required" Value="25"></asp:ListItem>
                <asp:ListItem Text="Batch in File and SAP, TSL Populated but Prod Date not Populated in SAP" Value="26"></asp:ListItem>
                <asp:ListItem Text="Batch in File and SAP, MRSL Populated but Expiration Date not Populated in SAP" Value="27"></asp:ListItem>
                <asp:ListItem Text="HALB material, Mfg Start Date empty in both file and SAP" Value="28"></asp:ListItem>
                <asp:ListItem Text="Production Date required, Batch NOT already in SAP" Value="29"></asp:ListItem>
                <asp:ListItem Text="Expiration Date required, Batch NOT already in SAP" Value="30"></asp:ListItem>
                <asp:ListItem Text="SAP First Goods Receipt Date required, Batch NOT already in SAP" Value="31"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Date Should be Populated, Batch NOT already in SAP" Value="32"></asp:ListItem>
                <asp:ListItem Text="Manufacturing Start Date Must be Populated, HALB, Batch NOT already in SAP" Value="33"></asp:ListItem>
                <asp:ListItem Text="FYI: Batch is expired, or batch expires or re-evaluates within 30 days" Value="34"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Date is in the past" Value="35"></asp:ListItem>
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

         <%--MRP AREA AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox4" runat="server" Text="IM Audits" 
                Value="100" Font-Bold="True" 
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
                <asp:ListItem Text="Missing Vendor Code" Value="36"></asp:ListItem>
                <asp:ListItem Text="Invalid Vendor Code" Value="37"></asp:ListItem>
                <asp:ListItem Text="Missing Customer Code" Value="38"></asp:ListItem>
                <asp:ListItem Text="Invalid Customer Code" Value="39"></asp:ListItem>
                <asp:ListItem Text="Storage Location Must Not be Populated for Special Stock" Value="40"></asp:ListItem>
                <asp:ListItem Text="Special Stock K(vendor) or W(customer) Must be Unrestricted (Movement 561)" Value="41"></asp:ListItem>
                <asp:ListItem Text="Special Stock O (Customer) - Movement Types 561, 563 Only" Value="42"></asp:ListItem>
                <asp:ListItem Text="Expired Stock" Value="43"></asp:ListItem>
                <asp:ListItem Text="Movement Type 561 But Restricted in  SAP" Value="44"></asp:ListItem>
                <asp:ListItem Text="Movement Type 563 But Restricted in  SAP" Value="45"></asp:ListItem>
                <asp:ListItem Text="Movement Type 565 But Restricted in  SAP" Value="46"></asp:ListItem>
                <asp:ListItem Text="UoM Must be Populated when Quantity Populated" Value="47"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox5" runat="server" Text="IM/WM Records Missing Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox5_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label14" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates5" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="IM Records Missing from WM" Value="48"></asp:ListItem>
                <asp:ListItem Text="WM Records Missing from IM" Value="49"></asp:ListItem>
                <asp:ListItem Text="IM / WM UoM Mismatch" Value="50"></asp:ListItem>
                <asp:ListItem Text="IM / WM Quantity Mismatch" Value="51"></asp:ListItem>
                <asp:ListItem Text="IM / WM Quantity Mismatch: Group by Status" Value="52"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox6" runat="server" Text="Incorrect Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox6_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label13" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates6" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="WM status not match IM status for Unrestricted Stock" Value="53"></asp:ListItem>
                <asp:ListItem Text="WM status not match IM status for Blocked Stock" Value="54"></asp:ListItem>
                <asp:ListItem Text="WM status not match IM status for QI stock" Value="55"></asp:ListItem>
                <asp:ListItem Text="WM - Incorrect Stock Category(Status) Code" Value="56"></asp:ListItem>
                <asp:ListItem Text="IM - Incorrect Special Stock Indicator" Value="57"></asp:ListItem>
                <asp:ListItem Text="IM - Incorrect Movement Type" Value="58"></asp:ListItem>
                <asp:ListItem Text="WM status is Unrestricted - IM status not match: Group by Status" Value="59"></asp:ListItem>
                <asp:ListItem Text="WM status is Block - IM status not match: Group by Status" Value="60"></asp:ListItem>
                <asp:ListItem Text="WM status is QI - IM status not match: Group by Status" Value="61"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox7" runat="server" Text="Potency & Quantity Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox7_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label12" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates7" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Potency Should not be Populated" Value="62"></asp:ListItem>
                <asp:ListItem Text="Potency Should be Populated" Value="63"></asp:ListItem>
                <asp:ListItem Text="Potency Should be Populated, Batch not already in SAP" Value="64"></asp:ListItem>
                <asp:ListItem Text="Potency Format" Value="65"></asp:ListItem>
                <asp:ListItem Text="Potency Mismatch in SAP" Value="66"></asp:ListItem>
                <asp:ListItem Text="Potency Greater Than 100" Value="67"></asp:ListItem>
                <asp:ListItem Text="Records with 0 Quantity" Value="68"></asp:ListItem>
                <asp:ListItem Text="No Decimal Places for Each Unit of Measure" Value="69"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox8" runat="server" Text="SAP Mismatch Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox8_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label11" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates8" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Production Date Mismatch in SAP" Value="70"></asp:ListItem>
                <asp:ListItem Text="Expiration Date Mismatch in SAP" Value="71"></asp:ListItem>
                <asp:ListItem Text="Manufacturing Start Date Mismatch in SAP" Value="72"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Date Mismatch in SAP" Value="73"></asp:ListItem>
                <asp:ListItem Text="Batch Use Restriction Mismatch in SAP" Value="74"></asp:ListItem>
                <asp:ListItem Text="SAP First Goods Receipt Date Mismatch in SAP" Value="75"></asp:ListItem>
                <asp:ListItem Text="IM UoM and Base UoM mismatch" Value="76"></asp:ListItem>
                <asp:ListItem Text="Original Manufacturer Mismatch in SAP" Value="77"></asp:ListItem>
                <asp:ListItem Text="Formatted Label Expiry Date Mismatch in SAP" Value="78"></asp:ListItem>
                <asp:ListItem Text="Reserve Sample Discard Date Mismatch in SAP" Value="79"></asp:ListItem>
                <asp:ListItem Text="Adjusted Expiration Date Mismatch in SAP" Value="80"></asp:ListItem>
                <asp:ListItem Text="Packaging Site Mismatch in SAP" Value="81"></asp:ListItem>
                <asp:ListItem Text="Parent Batch Mismatch in SAP" Value="82"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox9" runat="server" Text="Special Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox9_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label10" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates9" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Missing Inpection Types for 563 Material" Value="83"></asp:ListItem>
                <asp:ListItem Text="Material Flagged for Deletion Globally" Value="84"></asp:ListItem>
                <asp:ListItem Text="Material Flagged for Deletion at Plant" Value="85"></asp:ListItem>
                <asp:ListItem Text="Material Blocked From Inventory Movement - Plant" Value="86"></asp:ListItem>
                <asp:ListItem Text="Material Blocked From Inventory Movement - Global" Value="87"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox10" runat="server" Text="View Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox10_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label9" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates10" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Material-Plant Combination not in SAP" Value="88"></asp:ListItem>
                <asp:ListItem Text="Accounting/Costing View Does Not Exist" Value="89"></asp:ListItem>
                <asp:ListItem Text="Standard Price Does Not Exist" Value="90"></asp:ListItem>
                <asp:ListItem Text="Material Origin Does Not Exist (Costing view not fully defined)" Value="91"></asp:ListItem>
                <asp:ListItem Text="Storage View Does Not Exist" Value="92"></asp:ListItem>
                <asp:ListItem Text="Warehouse View Does Not Exist" Value="93"></asp:ListItem>
                <asp:ListItem Text="UOM Conversion Does Not Exist" Value="94"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox11" runat="server" Text="Batch Chars Audits - Multiple records for batch exist in file" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox11_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label8" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates11" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Multiple Records for Batch - Next Inspection Dates differ" Value="95"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - Expiration Dates differ" Value="96"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - Production Dates differ" Value="97"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - Last Goods Receipt Dates differ" Value="98"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - MFG Start Dates differ" Value="99"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - EA To Bundle Values differ" Value="100"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - EA To Case Values differ" Value="101"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - EA To Pallet Values differ" Value="102"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - Batch Use Restriction values differ" Value="103"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - Potency values differ" Value="104"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - SAP First Goods Receipt Dates differ" Value="105"></asp:ListItem>
                <asp:ListItem Text="Multiple Records for Batch - RS Discard Dates differ" Value="106"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox12" runat="server" Text="Batch Chars Audits - Clash Between Sites in Release" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox12_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label7" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates12" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Expiration Dates differ across sites" Value="107"></asp:ListItem>
                <asp:ListItem Text="Production Dates differ across sites" Value="108"></asp:ListItem>
                <asp:ListItem Text="Next Inspection Dates differ across sites" Value="109"></asp:ListItem>
                <asp:ListItem Text="Last Goods Receipt Dates differ across sites" Value="110"></asp:ListItem>
                <asp:ListItem Text="SAP First Goods Receipt Dates differ across sites" Value="111"></asp:ListItem>
                <asp:ListItem Text="MFG Start Dates differ across sites" Value="112"></asp:ListItem>
                <asp:ListItem Text="Batch Use Restriction differs across sites" Value="113"></asp:ListItem>
                <asp:ListItem Text="Potency differs across sites" Value="114"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox13" runat="server" Text="WM Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox13_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label4" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates13" runat="server" AutoPostBack = "true">
                <asp:ListItem Text="Plant Cannot be empty" Value="115"></asp:ListItem>
                <asp:ListItem Text="Warehouse Cannot be empty" Value="116"></asp:ListItem>
                <asp:ListItem Text="Bin Missing in SAP" Value="117"></asp:ListItem>
                <asp:ListItem Text="Bin Blocked for Putaway" Value="118"></asp:ListItem>
                <asp:ListItem Text="Capacity Multiplier Not Set Up In MM" Value="119"></asp:ListItem>
                <asp:ListItem Text="Capacity Mismatch in SAP" Value="120"></asp:ListItem>
                <asp:ListItem Text="Capacity Check in Config" Value="121"></asp:ListItem>
                <asp:ListItem Text="Additional Stock Not Allowed In Non-Mix Bin" Value="122"></asp:ListItem>
                <asp:ListItem Text="Double Stock In Non-Mix Bins" Value="123"></asp:ListItem>
                <asp:ListItem Text="Number records for Bin Exceed Bin's Capacity" Value="124"></asp:ListItem>
                <asp:ListItem Text="Strategy: Incorrect Storage Section Strategy" Value="125"></asp:ListItem>
                <asp:ListItem Text="Storage Type Empty on MM Warehouse 1 View" Value="126"></asp:ListItem>
                <asp:ListItem Text="Storage Unit Mismatch with MM Warehouse" Value="127"></asp:ListItem>
                <asp:ListItem Text="Invalid Movement Type in WM (561 only)" Value="128"></asp:ListItem>
                <asp:ListItem Text="Destination(putaway) Fields Emtpy in File" Value="129"></asp:ListItem>
                <asp:ListItem Text="Batch Management Indicator and WM Batch Number Mismatch" Value="130"></asp:ListItem>
                <asp:ListItem Text="Storage Number must be 18 characters long" Value="131"></asp:ListItem>
                <asp:ListItem Text="Storage Type is not valid" Value="132"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox14" runat="server" Text="WM Strategy" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox14_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label3" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates14" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="WM Strategy Audits (Must run package first! - gdd_wm_startegy_pkg OR gdd_wm_move_strategy_pkg)" Value="133"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox15" runat="server" Text="Plant MOVE Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox15_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates15" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="Plant Move - Material not maintained in the 'Move To Plant'" Value="134"></asp:ListItem>
				<asp:ListItem Text="Plant Move - Material not maintained in the 'Move To Storage Location'" Value="135"></asp:ListItem>
				<asp:ListItem Text="Plant Move - Accounting/Costing View Does Not Exist for 'To Plant'" Value="136"></asp:ListItem>
				<asp:ListItem Text="Plant Move - Standard Price for 'Move To Plant' is Zero" Value="137"></asp:ListItem>
				<asp:ListItem Text="Plant Move - Material Origin Not Exist for 'Move To Plant' (Costing view not exist)" Value="138"></asp:ListItem>
				<asp:ListItem Text="Plant Move - Material Blocked at Move To Plant" Value="139"></asp:ListItem>
				<asp:ListItem Text="Plant Move - Incorrect WM Movement Type" Value="140"></asp:ListItem>
				<asp:ListItem Text="Plant Move - IM/WM UoM Mismatch" Value="141"></asp:ListItem>
				<asp:ListItem Text="Plant Move - IM/WM Quantity Mismatch" Value="142"></asp:ListItem>
				<asp:ListItem Text="Plant Move - IM/WM not match on Status" Value="143"></asp:ListItem>
				<asp:ListItem Text="Plant Move - IM batches without WM record" Value="144"></asp:ListItem>
				<asp:ListItem Text="Plant Move - WM batches without IM record" Value="145"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox16" runat="server" Text="SLOC MOVE Audits" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox16_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label16" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates16" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="Stor Loc Move - Material not maintained in the 'To SLOC'" Value="146"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - 'To Plant' must be empty" Value="147"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - IM/WM UoM Mismatch" Value="148"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - IM/WM Quantity Mismatch" Value="149"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - IM batches without WM record" Value="150"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - WM batches without IM record" Value="151"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - IM/WM not match on Status" Value="152"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - Material Blocked at Plant Level" Value="153"></asp:ListItem>
				<asp:ListItem Text="Stor Loc Move - Move To Sloc Cannot Match From Sloc" Value="154"></asp:ListItem>
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
        <asp:CheckBox ID="CheckBox1" runat="server" Text="MOVE Audits - both kind" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox17_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label17" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

       <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates17" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="Material Blocked at Global level" Value="155"></asp:ListItem>
				<asp:ListItem Text="MOVE:  MRSL now 1, batch missing Expiration Date in SAP (WM Fails)" Value="156"></asp:ListItem>
				<asp:ListItem Text="Batch not exist in SAP" Value="157"></asp:ListItem>
				<asp:ListItem Text="IM / Base UoM Mismatch" Value="158"></asp:ListItem>
				<asp:ListItem Text="WM / Base UoM Mismatch" Value="159"></asp:ListItem>
				<asp:ListItem Text="Q Stock - Inspection Lot Missing " Value="160"></asp:ListItem>
				<asp:ListItem Text="Move To Plant not have Insp Type matching Insp Lot" Value="161"></asp:ListItem>
				<asp:ListItem Text="Bin Missing in SAP" Value="162"></asp:ListItem>
				<asp:ListItem Text="Bin Blocked for Putaway" Value="163"></asp:ListItem>
				<asp:ListItem Text="WM - Incorrect Stock Category(Status) Code; Valid values empty, Q, S" Value="164"></asp:ListItem>
				<asp:ListItem Text="Storage Unit Mismatch with MM Warehouse" Value="165"></asp:ListItem>
				<asp:ListItem Text="Capacity Mismatch in SAP" Value="166"></asp:ListItem>
				<asp:ListItem Text="Capacity Multiplier Not Set Up In MM" Value="167"></asp:ListItem>
				<asp:ListItem Text="Additional Stock Not Allowed In Non-Mix Bin" Value="168"></asp:ListItem>
				<asp:ListItem Text="Double Stock In Non-Mix Bins" Value="169"></asp:ListItem>
				<asp:ListItem Text="Number records for Bin Exceed Bin's Capacity" Value="170"></asp:ListItem>
				<asp:ListItem Text="Strategy: Incorrect Storage Section Strategy" Value="171"></asp:ListItem>
				<asp:ListItem Text="Storage Type Empty on MM Warehouse 1 View" Value="172"></asp:ListItem>
				<asp:ListItem Text="Destination(putaway) Fields Emtpy in File" Value="173"></asp:ListItem>
				<asp:ListItem Text="Batch Management Indicator and WM Batch Number Mismatch" Value="174"></asp:ListItem>
				<asp:ListItem Text="Capacity Check in Config" Value="175"></asp:ListItem>
				<asp:ListItem Text="File and SAP Status Mismatch - Unrestricted Stock" Value="176"></asp:ListItem>
				<asp:ListItem Text="File and SAP Status Mismatch - Q Stock" Value="177"></asp:ListItem>
				<asp:ListItem Text="File and SAP Status Mismatch - Blocked Stock" Value="178"></asp:ListItem>
				<asp:ListItem Text="File and SAP Quantity Mismatch - Unrestricted Stock" Value="179"></asp:ListItem>
				<asp:ListItem Text="File and SAP Quantity Mismatch - Q Stock" Value="180"></asp:ListItem>
				<asp:ListItem Text="File and SAP Quantity Mismatch - Blocked Stock" Value="181"></asp:ListItem>
				<asp:ListItem Text="Material not maintained in Warehouse" Value="182"></asp:ListItem>
				<asp:ListItem Text="Batch in SAP, but not in the DDT" Value="183"></asp:ListItem>
				<asp:ListItem Text="Quantity mismatch between SAP and the DDT" Value="184"></asp:ListItem>
				<asp:ListItem Text="Batches that re-evaluate within 7 days" Value="185"></asp:ListItem>
				<asp:ListItem Text="IM UoM and Base UoM mismatch" Value="186"></asp:ListItem>
				<asp:ListItem Text="FYI: Batch is expired, or batch expires or re-evaluates within 30 days" Value="187"></asp:ListItem>
				<asp:ListItem Text="Storage Number must be 18 characters long" Value="188"></asp:ListItem>
            </asp:CheckBoxList>
               </ContentTemplate>
             </asp:UpdatePanel>
        </td>
    </tr>


             <tr>
        <td colspan="4">
            <asp:Label ID="Label18" runat="server" Text="POST SAP LOAD ONLY:" Font-Bold="True" 
                Font-Size="Medium"></asp:Label>
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
        <asp:CheckBox ID="CheckBox3" runat="server" Text="Cutover Verification Audits 1" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox18_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label19" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

         <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates18" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="Production Date Mismatch in SAP" Value="189"></asp:ListItem>
				<asp:ListItem Text="Expiration Date Mismatch in SAP" Value="190"></asp:ListItem>
				<asp:ListItem Text="Manufacturing Start Date Mismatch in SAP" Value="191"></asp:ListItem>
				<asp:ListItem Text="Next Inspection Date Mismatch in SAP" Value="192"></asp:ListItem>
				<asp:ListItem Text="Packaging Start Date Mismatch in SAP" Value="193"></asp:ListItem>
				<asp:ListItem Text="Batch Use Restriction Mismatch in SAP" Value="194"></asp:ListItem>
				<asp:ListItem Text="Batch Chars Not In SAP Batch Header" Value="195"></asp:ListItem>
				<asp:ListItem Text="Potency Mismatch in SAP" Value="196"></asp:ListItem>
				<asp:ListItem Text="Last Good Receipt Date Mismatch in SAP" Value="197"></asp:ListItem>
				<asp:ListItem Text="Eaches To Bundles Mismatch in SAP" Value="198"></asp:ListItem>
				<asp:ListItem Text="Eaches To Cases Mismatch in SAP" Value="199"></asp:ListItem>
				<asp:ListItem Text="Eaches To Pallets Mismatch in SAP" Value="200"></asp:ListItem>
				<asp:ListItem Text="SAP First Goods Receipt Date Mismatch in SAP" Value="201"></asp:ListItem>
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
        <asp:CheckBox ID="CheckBox2" runat="server" Text="Financial Verification" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox19_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label20" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

         <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates19" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="Financial Verification (Must run package first!)" Value="202"></asp:ListItem>
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
        <asp:CheckBox ID="CheckBox4" runat="server" Text="FYI Queries" 
                Value="100" Font-Bold="True" 
                Font-Size="12pt" oncheckedchanged="chkmainbox20_CheckedChanged" ValidationGroup="main" AutoPostBack = "true"/>
            &nbsp;</td>
            <td align="left" colspan="3"> <asp:Label Font-Bold="true" ID="Label21" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

         <tr>
           
        <td  colspan="4">

            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
          
                 &nbsp;

                <asp:CheckBoxList ID="chklstStates20" runat="server" AutoPostBack = "true">
				<asp:ListItem Text="Check the Inventory Summation Process" Value="203"></asp:ListItem>
				<asp:ListItem Text="Create ZS050 List" Value="204"></asp:ListItem>
				<asp:ListItem Text="Final Batch Audits Catalyst" Value="205"></asp:ListItem>
				<asp:ListItem Text="Financial Accounts to Be Open" Value="206"></asp:ListItem>
				<asp:ListItem Text="Individual Batch Numbers" Value="207"></asp:ListItem>
				<asp:ListItem Text="Standard Costs for Comparision" Value="208"></asp:ListItem>
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




