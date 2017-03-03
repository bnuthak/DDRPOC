<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MMAuditing.aspx.cs" Inherits="DDRPOC.MMAuditing" %>

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
                Text="Get Report" onclick="btngetReport_Click"/>
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
            <asp:Label ID="Label6" runat="server" Text="MM Auditing:" Font-Bold="True" 
                Font-Size="Large"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td  colspan="4">
            &nbsp;<label ><strong style="font-size: medium" >Error Indicators: LF - Load Failure/Critical; BR - Business Requirement:</strong></label>
            <div id="line"></div>
            </td>
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



         <%--BASIC DATA VIEW AUDITS--%>



        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Basic Data View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - No Description Exists" Value="1"></asp:ListItem>
                <asp:ListItem Text="LF - No English Description Exists " Value="2"></asp:ListItem>
                <asp:ListItem Text="LF - Basic Data: Ean Missing Ean Category " Value="3"></asp:ListItem>
                <asp:ListItem Text="BR - Ferts That Do Not Expire " Value="4"></asp:ListItem>
                <asp:ListItem Text="LF - Net Weight Is Greater Than Gross Weight " Value="5"></asp:ListItem>
                <asp:ListItem Text="LF - Missing Values On MARA,MTART,MEINS,MBRSH,MATKL " Value="6"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - Material Type FERT, HAWA, ZPRM w/ Pack Code 001 or 01M, Base UoM MUST be EA " Value="7"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - Material Type HALB with Pack Code 01M, Base UoM MUST be TS " Value="8"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - For Pack Code V1M, Base UoM MUST be TS " Value="9"></asp:ListItem>
                <asp:ListItem Text="BR - Material Type HALB, Pack Code 001 or V01, Base UoM MUST be EA " Value="10"></asp:ListItem>
                <asp:ListItem Text="LF - Material Group is not valid (SAP ACCESS REQUIRED) " Value="11"></asp:ListItem>
                <asp:ListItem Text="Global Error - Material Type ROH, Material Group Should be F02, L20, M25, R15, R20, R40 or R45 " Value="12"></asp:ListItem>
                <asp:ListItem Text="Global Error - Material Type HALB, Material Group Should be R05, R15, R20, R25, R40, or R45" Value="13"></asp:ListItem>
                <asp:ListItem Text="Global Error - Material Type FERT, Material Group Should be R05 or R10 " Value="14"></asp:ListItem>
                <asp:ListItem Text="LF - Product Hierarchy required for FERT, HALB, HAWA, ROH, VERP, ZGCM, ZMAB" Value="15"></asp:ListItem>
                <asp:ListItem Text="LF - Product Hierarchy does not match Material Type" Value="16"></asp:ListItem>
                <asp:ListItem Text="LF - Product Hierarchy value not setup in SAP (SAP ACCESS REQUIRED)" Value="17"></asp:ListItem>
                <asp:ListItem Text="BR - Gen Item Category should be NORM for all material types except for ZMAB" Value="18"></asp:ListItem>
                <asp:ListItem Text="BR - Gen Item Category should be ZVPT for Semifin Dimensionless materials (ZMAB) " Value="19"></asp:ListItem>
                <asp:ListItem Text="LF - Base Unit of Measure value not setup in SAP (SAP ACCESS REQUIRED)" Value="20"></asp:ListItem>
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
                <asp:ListItem Text="LF - Materials Missing Profit Center" Value="21"></asp:ListItem>
                <asp:ListItem Text="FYI - Standard Price (Stprs) Is Set To 0" Value="22"></asp:ListItem>
                <asp:ListItem Text="LF - ZUNB has Accounting Data" Value="23"></asp:ListItem>
                <asp:ListItem Text="LF - ZUNB has Costing Data" Value="24"></asp:ListItem>
                <asp:ListItem Text="FYI - Costing Flag (Ncost) Set" Value="25"></asp:ListItem>
                <asp:ListItem Text="BR - Costing SP Procurement Type 30 And No Bom" Value="26"></asp:ListItem>
                <asp:ListItem Text="BR - Costing SP Procurement Type 30 And Procurement Not F Or X" Value="27"></asp:ListItem>
                <asp:ListItem Text="BR - Special Procurement Type F, Indicator <> 30, Costing Lot Size Conflict (<> 1 Or 1000)" Value="28"></asp:ListItem>
                <asp:ListItem Text="LF - Costing View Incomplete/Missing (Material Origin Empty)" Value="29"></asp:ListItem>
                <asp:ListItem Text="LF - Costing Lot Size Cannot be Less than Price Unit" Value="30"></asp:ListItem>
                <asp:ListItem Text="FYI - Costing Lot Size, Price Unit And Standard Price" Value="31"></asp:ListItem>
                <asp:ListItem Text="FYI - Costing Lot Size, Fixed Lot Size, And Work Sched Base Qty" Value="32"></asp:ListItem>
                <asp:ListItem Text="LF - Lot Size(Disls) Is Fixed But Fixed Lot Size(Bstfe) Empty" Value="33"></asp:ListItem>
                <asp:ListItem Text="LF - Valuation Class Is Empty When Stprs Or Peinh Has A Value" Value="34"></asp:ListItem>
                <asp:ListItem Text="LF - Material Type HAWA With Invalid Valuation Class" Value="35"></asp:ListItem>
                <asp:ListItem Text="LF - Material Type VERP With Invalid Valuation Class" Value="36"></asp:ListItem>
                <asp:ListItem Text="LF - Material Type FERT With Invalid Valuation Class" Value="37"></asp:ListItem>
                <asp:ListItem Text="LF - Material Type HALB With Invalid Valuation Class" Value="38"></asp:ListItem>
                <asp:ListItem Text="LF - Material Type ROH With Invalid Valuation Class" Value="39"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - FERT/HAWA, Proc Type F/X, Costing SPT empty, Val Class MUST be 3100, 3103 or 3104" Value="40"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - FERT/HAWA, Proc Type F, Val Class 7930/7936, Costing SPT must not be 30/31" Value="41"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - FERT, Proc Type F/X, Val Class 7955/7956, Costing SPT must be 30/31" Value="42"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - HALB, Proc Type F/X, Costing SPT empty, Val Class MUST be 3101, 3102, 7903, 7904, 7952, 7953 or 7954" Value="43"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - HALB, Proc Type F/X, Costing SPT populated, Val Class 3101/3102/7904 invalid" Value="44"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - HALB, Proc Type F/X, Val Class 7952/7953/7954, Costing SPT must be 30/31" Value="45"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - ROH, Proc Type F/X, Costing SPT 30, Val Class MUST be 7951" Value="46"></asp:ListItem>
                <asp:ListItem Text="BR - ROH, Material Group R20 or R45, Division 01, Profit Center SHOULD be 1799" Value="47"></asp:ListItem>
                <asp:ListItem Text="BR - ROH, Material Group R20 or R45, Division 02, Profit Center SHOULD be 3199" Value="48"></asp:ListItem>
                <asp:ListItem Text="BR - ROH, Material Group R15 or R40, Profit center SHOULD NOT be 1799 or 3199" Value="49"></asp:ListItem>
                <asp:ListItem Text="BR - VERP, Division 01, Profit Center SHOULD be 1810" Value="50"></asp:ListItem>
                <asp:ListItem Text="BR - VERP, Division 02, Profit Center SHOULD be 3510" Value="51"></asp:ListItem>
                <asp:ListItem Text="BR - For Mfg Materials, Val Class Should be 7900, 7901, 7902, 7920 or 7935" Value="52"></asp:ListItem>
                <asp:ListItem Text="BR - For Purchased Materials, Val Class Should be 3000, 3050, 3100, 3101, 3102, 3104, 7903, 7930, 7936, 7951, 7952, 7953, 7954, 7955, or 7956" Value="53"></asp:ListItem>
                <asp:ListItem Text="BR - Costing SPT Should be Empty When Val Class is 7900, 7901, 7902, 7920, 7935, 7911, 7912, 7940, 3100, 3101, 3102, or 3104" Value="54"></asp:ListItem>
                <asp:ListItem Text="BR - Costing SPT Should not be Empty, 10 or 30 When Val Class is 7930, 7936, or 7903" Value="55"></asp:ListItem>
                <asp:ListItem Text="BR - Costing SPT Should be 30 When Val Class is 7951, 7952, 7953, 7954, 7955, or 7956" Value="56"></asp:ListItem>
                <asp:ListItem Text="BR - Costing SPT populated <> 30, Origin Group must be I - Financial Critical" Value="57"></asp:ListItem>
                <asp:ListItem Text="BR - Origin Group is I, Costing SPT cannot = 30 - Financial Critical" Value="58"></asp:ListItem>
                <asp:ListItem Text="BR - Profit Center not equal across all plants for FERT, HAWA, ZPRM" Value="59"></asp:ListItem>
                <asp:ListItem Text="BR - Profit Center not equal across Material 6-digit root, Site Only Plants" Value="60"></asp:ListItem>
                <asp:ListItem Text="BR - Profit Center not equal across Material 6-digit root, All Plants" Value="61"></asp:ListItem>
                <asp:ListItem Text="LF - Profit Center is not correct for this material (ZTMM_ITEM_PROFIT) (SAP Access Required)" Value="62"></asp:ListItem>
                <asp:ListItem Text="BR - Do Not Cost Flag must be checked if Status is H9, L9, M9, Z2, Z4, or Z9" Value="63"></asp:ListItem>
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



         <%--MRP VIEW AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox3" runat="server" Text="MRP View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Materials Missing Profit Center " Value="64"></asp:ListItem>
                <asp:ListItem Text="LF - MRP Type Is HB And Max Stock Level Is Not Populated " Value="65"></asp:ListItem>
                <asp:ListItem Text="LF - MRP Type Is VB And Reorder Point Is Not Populated " Value="66"></asp:ListItem>
                <asp:ListItem Text="LF - MRP Type Is Z1 And Reorder Point Is Not Populated " Value="67"></asp:ListItem>
                <asp:ListItem Text="LF - Reorder Point should not be populated when MRP type is X0 " Value="68"></asp:ListItem>
                <asp:ListItem Text="LF - MRP Type is P1 and Planning Time Fence is empty " Value="69"></asp:ListItem>
                <asp:ListItem Text="LF - MRP Type Populated, Other Required MRP Fields Missing " Value="70"></asp:ListItem>
                <asp:ListItem Text="LF - MRP Controller Populated, Other Required MRP Fields Missing " Value="71"></asp:ListItem>
                <asp:ListItem Text="LF - Procurement Type Populated, Other Required MRP Fields Missing " Value="72"></asp:ListItem>
                <asp:ListItem Text="LF - MRP missing required fields " Value="73"></asp:ListItem>
                <asp:ListItem Text="LF - Procurement Type Populated, But It Is Not E,F Or X " Value="74"></asp:ListItem>
                <asp:ListItem Text="LF - Lot Size(Disls) Is Fixed But Fixed Lot Size(Bstfe) Empty " Value="75"></asp:ListItem>
                <asp:ListItem Text="BR - MRP Special Proc = 30, Costing 1 SP. Procurement Type Not = 30 " Value="76"></asp:ListItem>
                <asp:ListItem Text="BR - MRP Special Proc = 30, Procurement Type Not F Or X " Value="77"></asp:ListItem>
                <asp:ListItem Text="BR - MRP Special Proc = 30, Bom Missing " Value="78"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = F, Costing Lot Size Must Be 1 Or 1000 " Value="79"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = F, Storage Loc For EP Missing " Value="80"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = E, Issue Storage Location Missing " Value="81"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = E, Without BOMS " Value="82"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = E, Availability Check must be Z1 " Value="83"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = E, Replenishment Lead Time required " Value="84"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = E and Avail Check = Z1, Planned Delivery Time and GR Processing Time required " Value="85"></asp:ListItem>
                <asp:ListItem Text="BR - Procurement Type = F with STPC 30, Availability Check must be Z1 " Value="86"></asp:ListItem>
                <asp:ListItem Text="BR - Issue Storage Location(Lgpro) Without Storage (Lgort) " Value="87"></asp:ListItem>
                <asp:ListItem Text="BR - Storage Loc For EP(Lgfsb) Without Storage (Lgort) " Value="88"></asp:ListItem>
                <asp:ListItem Text="BR - Lot Size FX, Min Lot Size and Max Lot Size should be empty (SAP ACCESS REQUIRED)" Value="89"></asp:ListItem>
                <asp:ListItem Text="BR - Lot Size EX, Fixed Lot Size should be empty " Value="90"></asp:ListItem>
                <asp:ListItem Text="LF - Lot Size is not valid " Value="91"></asp:ListItem>
                <asp:ListItem Text="LF - Minimum Lot Size may not be greater than Maximum Lot Size " Value="92"></asp:ListItem>
                <asp:ListItem Text="LF - Maximum lot size may not be smaller than Rounding Value " Value="93"></asp:ListItem>
                <asp:ListItem Text="LF - Safety Time Indicator is not valid " Value="94"></asp:ListItem>
                <asp:ListItem Text="LF - Safety time indicator populated, Safety Time missing " Value="95"></asp:ListItem>
                <asp:ListItem Text="LF - ABC Indicator is not valid " Value="96"></asp:ListItem>
                <asp:ListItem Text="BR - Rounding Value should not be populated when Lot Size is FX (SAP ACCESS REQUIRED)" Value="97"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - Special Proc Type must be Blank, 30, 31, 80, or Z1 (SAP ACCESS REQUIRED)" Value="98"></asp:ListItem>
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
        <asp:CheckBox ID="chkmainbox4" runat="server" Text="MRP Area Audits" 
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
                <asp:ListItem Text="LF - Special Procurement Key is not valid " Value="99"></asp:ListItem>
                <asp:ListItem Text="LF - Planning Calandar is not valid " ></asp:ListItem>
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

         <%--PURCHASING VIEW AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox5" runat="server" Text="Purchasing View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Purchasing Value Key Required When Purchasing Group Populated " Value="101"></asp:ListItem>
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

         <%--QM VIEW AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox6" runat="server" Text="QM View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - QM In Proc Active But Control Key Empty " Value="102"></asp:ListItem>
                <asp:ListItem Text="LF - Inspection Type Records Without MM QM View " Value="103"></asp:ListItem>
                <asp:ListItem Text="QC Error - QM Control Key must be populated " Value="104"></asp:ListItem>
                <asp:ListItem Text="QC Error - Documentation Required must be populated (excludes ZUNB) (SAP Access Required)" Value="105"></asp:ListItem>
                <asp:ListItem Text="QC Error - QM Material Auth must be populated (SAP Access Required)" Value="106"></asp:ListItem>
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

         <%--FOREIGN TRADE VIEW AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox7" runat="server" Text="Foreign Trade View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Commodity Code not setup in SAP(T604) " Value="107"></asp:ListItem>
                <asp:ListItem Text="LF - Commodity Code missing Alt UoM record (SAP Access Required)" Value="108"></asp:ListItem>
                <asp:ListItem Text="BR - Foreign Trade fields should be populated for US exporting plants. Sales Orgs 1100, 1101, 1102, 1184, 1706 " Value="109"></asp:ListItem>
                <asp:ListItem Text="LF - Commodity Code not setup in SAP(ZTCOMCOD) for 6-digit material (SAP ACCESS REQUIRED)" Value="110"></asp:ListItem>
                <asp:ListItem Text="BR - Commodity Code not equal across Material 6-digit root " Value="111"></asp:ListItem>
                <asp:ListItem Text="LF - Grouping for Legal Control is not valid " Value="112"></asp:ListItem>
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

         <%--SALES AND TAX DATA VIEW AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox8" runat="server" Text="Sales and Tax Data View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Sales Records Without Matching Tax Records " Value="113"></asp:ListItem>
                <asp:ListItem Text="LF - Tax record missing " Value="114"></asp:ListItem>
                <asp:ListItem Text="LF - Sales Records With Missing Required Fields " Value="115"></asp:ListItem>
                <asp:ListItem Text="BR - Sales History - Profit Center W/O Sales Alt Uom (Lc*, Mu, Cm) " Value="116"></asp:ListItem>
                <asp:ListItem Text="LF - Sales Unit (Vrkme) Without Alt Uom Conversion " Value="117"></asp:ListItem>
                <asp:ListItem Text="LF - Tax Records Without Matching Sales Record " Value="118"></asp:ListItem>
                <asp:ListItem Text="LF - Sales Unit Cannot match Base UoM " Value="119"></asp:ListItem>
                <asp:ListItem Text="LF - US Tax needs both TAXM1 and TAXM2 populated" Value="120"></asp:ListItem>
                <asp:ListItem Text="LF - Weight Unit must be populated if there is a Sales record" Value="121"></asp:ListItem>
                <asp:ListItem Text="LF - Loading Group is not valid (SAP Connection Required)" Value="122"></asp:ListItem>
                <asp:ListItem Text="LF - Availability Check is missing or is not valid (SAP Connection Required)" Value="123"></asp:ListItem>
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

         <%--CLASSIFICATION DATA AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox9" runat="server" Text="Classification Data Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Classification Does Not Exist " Value="124"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Item Family is required " Value="125"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Item Number is required " Value="126"></asp:ListItem>
                <asp:ListItem Text="QC Critical - Expiration Format Missing - Required for all except ZUNB " Value="127"></asp:ListItem>
                <asp:ListItem Text="QC Critical - Manufacture Date Format Missing - Required for all except ZUNB " Value="128"></asp:ListItem>
                <asp:ListItem Text="QC Critical - Subsequent Insp Interval must be populated when Insp Interval Populated (ROH,HALB,ZGCM) " Value="129"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Subselling Market required for FERT, HALB, ROH length > 11 " Value="130"></asp:ListItem>
                <asp:ListItem Text="LF - Item Family does not match first two positions of Material Num " Value="131"></asp:ListItem>
                <asp:ListItem Text="LF - Item Number does not match positions 3-6 of Material Num " Value="132"></asp:ListItem>
                <asp:ListItem Text="LF - Pack Code does not match positions 7-9 of Material Num " Value="133"></asp:ListItem>
                <asp:ListItem Text="LF - Label Code does not match positions 10-11 of Material Num " Value="134"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Subselling Market must match positions 10-13 of Material " Value="135"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Brand Name Required for FERTs (family not UC) " Value="136"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Common Name Required for Material Groups R10 and R25 " Value="137"></asp:ListItem>
                <asp:ListItem Text="Global Error - API Required for Material Groups R10 and R25 " Value="138"></asp:ListItem>
                <asp:ListItem Text="Global Critical - Strength Active Component Required for Material Groups R10 and R15 " Value="139"></asp:ListItem>
                <asp:ListItem Text="Global Error - Strength Active Component Should be Blank for Material Group R05 " Value="140"></asp:ListItem>
                <asp:ListItem Text="LF - Multiple Values Exist For Same Characteristic " Value="141"></asp:ListItem>
                <asp:ListItem Text="FYI - Bulk Managed Materials - Recycle indicator = 1, 3, or 4. " Value="142"></asp:ListItem>
                <asp:ListItem Text="LF - Z_GLOBAL Classification value not valid " Value="143"></asp:ListItem> 
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

         <%--STORAGE VIEW AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox10" runat="server" Text="Storage View Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Materials Missing Profit Center " Value="144"></asp:ListItem>
                <asp:ListItem Text="FYI - Batch Management " Value="145"></asp:ListItem>
                <asp:ListItem Text="FYI - Batch Management Not Checked " Value="146"></asp:ListItem>
                <asp:ListItem Text="BR - Batch Management Not Checked, but Material Expires " Value="147"></asp:ListItem>
                <asp:ListItem Text="BR - Batch Management Not Checked, but Material has TSL " Value="148"></asp:ListItem>
                <asp:ListItem Text="BR - Batch Management Not Checked, but Material ReEvaluates " Value="149"></asp:ListItem>
                <asp:ListItem Text="BR - Sloc 0100 Without WM Data " Value="150"></asp:ListItem>
                <asp:ListItem Text="LF - Unit Of Issue (Ausme) Without Alt Uom Conversion " Value="151"></asp:ListItem>
                <asp:ListItem Text="LF - Unit Of Issue Equal Base Uom " Value="152"></asp:ListItem>
                <asp:ListItem Text="QC Critical - MRSL must be 1 for FERTs with Material Group R10 " Value="153"></asp:ListItem>
                <asp:ListItem Text="QC Critical - Batch Mgt must be checked for Material Group R10 " Value="154"></asp:ListItem>
                <asp:ListItem Text="QC Critical - Inspection Interval must be populated when MRSL = 0 (ROH,HALB,ZGCM) " Value="155"></asp:ListItem>
                <asp:ListItem Text="LF - Materials Missing Required Label Type " Value="156"></asp:ListItem>
                <asp:ListItem Text="BR - Storage and Temperature Conditions not same across plants " Value="157"></asp:ListItem>
                <asp:ListItem Text="LF - Storage and Temperature Conditions required for HALB, ROH, and VERP " Value="158"></asp:ListItem>
                <asp:ListItem Text="LF - Storage and Temperature Condition combination not valid (SAP Connection Required)" Value="159"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Or Temperature Condition invalid (SAP Connection Required)" Value="160"></asp:ListItem>
                <asp:ListItem Text="LF - Issue Storage Location is not valid (SAP Connection Required)" Value="161"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Location for EP is not valid (SAP Connection Required)" Value="162"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Location (MARD) is not valid (SAP Connection Required)" Value="163"></asp:ListItem>
                <asp:ListItem Text="LF - Physical Inventory Ind for Cycle Counting is not valid (SAP Connection Required)" Value="164"></asp:ListItem>
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

         <%--WM DATA AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox11" runat="server" Text="WM Data Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Capacity UoM exists but Alt UoM missing " Value="165"></asp:ListItem>
                <asp:ListItem Text="BR - WM Data Exists But Sloc 0100 Missing " Value="166"></asp:ListItem>
                <asp:ListItem Text="FYI - Storage Strategies - at least one of three is empty " Value="167"></asp:ListItem>
                <asp:ListItem Text="FYI - Unit Of Issue,Wm Unit,Proposal Uom " Value="168"></asp:ListItem>
                <asp:ListItem Text="LF - LE Qty UoM must be Base unit, WM unit, or Unit of Issue " Value="169"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Type Indicator for Stock Removal (LTKZA) is not valid (SAP Connection Required)" Value="170"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Type Indicator for Stock Placement (LTKZE) is not valid (SAP Connection Required)" Value="171"></asp:ListItem>
                <asp:ListItem Text="LF - WM UoM should not be the same as the Base UoM " Value="172"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Section Indicator is not valid " Value="173"></asp:ListItem>
                <asp:ListItem Text="LF – Storage Unit Type (LETY1, LETY2, or LETY3) is not valid (SAP Connection Required)" Value="174"></asp:ListItem>
                <asp:ListItem Text="LF – Loading Qty required when Qty UoM or SUT are populated " Value="175"></asp:ListItem>
                <asp:ListItem Text="LF - WM2 Storage Bin is not valid (SAP Connection Required)" Value="176"></asp:ListItem>
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

         <%--ALTERNATE UOM AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox12" runat="server" Text="Alternate UoM Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="Financial Critical - Alt UoM EA required for Material Groups R05, R10, R15, R25 " Value="177"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - Alt UoM LCC,LCG,LCK,LCM, MU, or TEU must exist when Planned w/ Material Groups R05, R10, R15, R25 " Value="178"></asp:ListItem>
                <asp:ListItem Text="Financial Critical - Materials Cannot Have Multiple Active Units (MU, LCC, LCG, TEU etc) " Value="179"></asp:ListItem>
                <asp:ListItem Text="Financial Warning - Alt UoM ST should exist for FERTs " Value="180"></asp:ListItem>
                <asp:ListItem Text="BR - Planned Material - Base UoM is not EA and EA not exist as Alt UoM " Value="181"></asp:ListItem>
                <asp:ListItem Text="BR - Planned Material - ST Alt UoM does not exist " Value="182"></asp:ListItem>
                <asp:ListItem Text="BR - Planned Material and sold - ST Alt UoM does not exist " Value="183"></asp:ListItem>
                <asp:ListItem Text="BR - Planned Material and sold - Base UoM is not EA and EA not exist as Alt UoM " Value="184"></asp:ListItem>
                <asp:ListItem Text="BR - Material missing Label Claim Alt UoM record (ZTMMLBLCLAIM_UOM) (SAP Connection Required)" Value="185"></asp:ListItem>
                <asp:ListItem Text="LF - Additional Data: Alt Ean Missing Ean Category " Value="186"></asp:ListItem>
                <asp:ListItem Text="LF - Unit of Measure value not setup in SAP (SAP Connection Required)" Value="187"></asp:ListItem> 
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

         <%--CONTROL CYCLE AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox13" runat="server" Text="Control Cycle Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Ccycles staging ind = 3,dynamic storage bin must be empty and storage bin must be populated " Value="188"></asp:ListItem>
                <asp:ListItem Text="LF - Ccycles staging ind = 1,dynamic storage bin must be checked and storage bin must be empty " Value="189"></asp:ListItem>
                <asp:ListItem Text="LF - Ccycles staging ind = 0,dynamic storage bin and storage bin must be empty " Value="190"></asp:ListItem>
                <asp:ListItem Text="BR - Ccycle record missing, Material has a BOM and WM record " Value="191"></asp:ListItem>
                <asp:ListItem Text="LF - Supply Area is not valid (SAP Connection Required)" Value="192"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Bin is not valid (SAP Connection Required)" Value="193"></asp:ListItem>
                <asp:ListItem Text="LF - Storage Bin & Storage Type combo is not valid (SAP Connecion Required)" Value="194"></asp:ListItem> 
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

         <%--WORK SCHEDULING--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox14" runat="server" Text="Work Scheduling" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Both Overdelivery Tolerance and Unlimited Overdelivery flag are populuated. Only populate one " Value="195"></asp:ListItem>
                <asp:ListItem Text="LF - Production Scheduling Profile must be populated if any Work Scheduling fields are populated " Value="196"></asp:ListItem>
                <asp:ListItem Text="LF - Production Scheduling Profile should not be populated for HAWA and ZGCM materials " Value="197"></asp:ListItem>
                <asp:ListItem Text="LF - Production Unit should only be populated for ZMAB materials " Value="198"></asp:ListItem>
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

         <%--ZCOUNTRY AUDITS--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox15" runat="server" Text="Zcountry Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="LF - Zcountry record without MARC or MATNR_REF record " Value="199"></asp:ListItem>
                <asp:ListItem Text="LF - Zcountry record with invalid country (SAP Connection Required)" Value="200"></asp:ListItem>
                <asp:ListItem Text="FYI - FERT & HALB materials without a Zcountry record " Value="201"></asp:ListItem>
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

         <%--DATA MANAGEMENT AUDITS--%>


        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox16" runat="server" Text="Data Management Audits" 
                 Font-Bold="True" 
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
                <asp:ListItem Text="List Of Materials Not In Matnr Ref Table " Value="202"></asp:ListItem>
                <asp:ListItem Text="MRP Controller Does Not Exist In Set SAP Instance. (SAP Connection Required)" Value="203"></asp:ListItem>
                <%--<asp:ListItem Text="Material flagged for deletion at global level " Value="204"></asp:ListItem>--%>
                <asp:ListItem Text="DDT - SPT Plant not found in GPR nor DDT " Value="205"></asp:ListItem>
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




