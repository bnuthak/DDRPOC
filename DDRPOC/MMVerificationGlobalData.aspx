<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MMVerificationGlobalData.aspx.cs" Inherits="DDRPOC.MMVerificationGlobalData" %>

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
            <asp:Label ID="Label6" runat="server" Text="MM Verification Global Data:" Font-Bold="True" 
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



         <%--Global View Extracts--%>



        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox" runat="server" Text="Global View Extracts" 
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
                <asp:ListItem Text="Basic Data" Value="1"></asp:ListItem>
                <asp:ListItem Text="Material Descriptions" Value="2"></asp:ListItem>
                <asp:ListItem Text="Material Descriptions**English Only" Value="3"></asp:ListItem>
                <asp:ListItem Text="Storage Global Fields (only when sloc exists)" Value="4"></asp:ListItem>
                <asp:ListItem Text="Storage Global Fields 2 (does not factor sloc)" Value="5"></asp:ListItem>
                <asp:ListItem Text="Alternate Unit of Measure" Value="6"></asp:ListItem>
                <asp:ListItem Text="Alternate EAN/UPC" Value="7"></asp:ListItem>
                <asp:ListItem Text="Proportional Units" Value="8"></asp:ListItem>
                <asp:ListItem Text="JD Net" Value="9"></asp:ListItem>
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

         <%--Texts--%>


         <tr>
         <td style="width:50%">
         <asp:CheckBox ID="chkmainbox2" runat="server" Text="Texts" 
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
                <asp:ListItem Text="Basic Data Text" Value="10"></asp:ListItem>
                <asp:ListItem Text="Inspection Text" Value="11"></asp:ListItem>
                <asp:ListItem Text="Internal Text" Value="12"></asp:ListItem>
                <asp:ListItem Text="Purchasing Text" Value="13"></asp:ListItem>
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



         <%--Z_Global Classification--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox3" runat="server" Text="Z_Global Classification" 
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
                <asp:ListItem Text="Item Family (Z_PRESENTATION)" Value="14"></asp:ListItem>
                <asp:ListItem Text="Item Number (Z_PRODUCT_NUMBER)" Value="15"></asp:ListItem>
                <asp:ListItem Text="Pack Code (Code) (Z_PACKAGE_SIZE)" Value="16"></asp:ListItem>
                <asp:ListItem Text="Label Code (Z_LABEL_CODE)" Value="17"></asp:ListItem>
                <asp:ListItem Text="Subselling Market (Z_SUBSELLING_MARKET)" Value="18"></asp:ListItem>
                <asp:ListItem Text="Material Use (Z_MATERIAL_USE)" Value="19"></asp:ListItem>
                <asp:ListItem Text="Initial Manufactured Form (Z_GALENIC_FORM)" Value="20"></asp:ListItem>
                <asp:ListItem Text="Active Pharma. Ingredient(API) (Z_SUBMOLECULE)" Value="21"></asp:ListItem>
                <asp:ListItem Text="Common Name (Z_MOLECULE)" Value="22"></asp:ListItem>
                <asp:ListItem Text="Product Name (Z_BRAND_NAME)" Value="23"></asp:ListItem>
                <asp:ListItem Text="Dosage Strength (Z_DOSAGE_STRENGTH_QUANTITY)" Value="24"></asp:ListItem>
                <asp:ListItem Text="Dosage Strength UOM (Z_DOSAGE_STRENGTH_UOM)" Value="25"></asp:ListItem>
                <asp:ListItem Text="Strength Active Component (Z_STRENGTH_COMPONENT)" Value="26"></asp:ListItem>
                <asp:ListItem Text="Dose Form (Z_DOSE_FORM)" Value="27"></asp:ListItem>
                <asp:ListItem Text="Pack Format (Code) (Z_PACK_FORMAT)" Value="28"></asp:ListItem>
                <asp:ListItem Text="Pack (Type) (Z_PACK_TYPE)" Value="29"></asp:ListItem>
                <asp:ListItem Text="Report/Plan in Active Units (Z_VARIABLE_POTENCY)" Value="30"></asp:ListItem>
                <asp:ListItem Text="Manufacture Date Format (Z_MANUF_DATE_FORMAT)" Value="31"></asp:ListItem>
                <asp:ListItem Text="Expiration Date Format (Z_EXPIRE_DATE)" Value="32"></asp:ListItem>
                <%--<asp:ListItem Text="Lilly Number (Z_LILLY_NUMBER)" Value="33"></asp:ListItem>--%>
                <asp:ListItem Text="Subsequent Inspection Interval (Z_SUBSEQ_INSPECT_INTERVAL)" Value="34"></asp:ListItem>
                <asp:ListItem Text="Animal Source Material (Z_ANIMAL_SOURCE)" Value="35"></asp:ListItem>
                <asp:ListItem Text="BSE Free Country Source (Z_BSE_FREE_COUNTRY_SOURCE)" Value="36"></asp:ListItem>
                <asp:ListItem Text="Bulk and/or Recycle Indicator (Z_BULK_RECYCLE_INDICATOR)" Value="37"></asp:ListItem>
                <asp:ListItem Text="Pharma Packing Code Identifier (Z_PHARMA_CODE)" Value="38"></asp:ListItem>
                <asp:ListItem Text="Packaging: Printed (y/n) (Z_PRINTED)" Value="39"></asp:ListItem>
                <asp:ListItem Text="Labeled Fill or Recon Volume (Z_FILL_QTY_OR_COUNT)" Value="40"></asp:ListItem>
                <asp:ListItem Text="Fill Qty UOM (Z_FILL_QTY_OR_COUNT_UOM)" Value="41"></asp:ListItem>
                <asp:ListItem Text="Chemical Abstract Number (Z_CAS)" Value="42"></asp:ListItem>
                <asp:ListItem Text="Lilly Serial Number(LSN) (Z_SERIAL_NUMBER)" Value="43"></asp:ListItem>
                <asp:ListItem Text="Lly Special Security Substance (Z_LLY_SPECIAL_SECURITY_SUBSTNC)" Value="44"></asp:ListItem>
                <asp:ListItem Text="Molecular Weight Ratio (Z_STRENGTH_MOLECULE_CONVERSION)" Value="45"></asp:ListItem>
                <asp:ListItem Text="Initial Retest Calc Method (Z_INIT_RETEST_TRIGGER)" Value="46"></asp:ListItem>
                <asp:ListItem Text="MSDS Title (Z_MSDS_TITLE)" Value="47"></asp:ListItem>
                <asp:ListItem Text="Reserve Sample Storage Time (Z_RETENTION_SAMPLE_DAYS)" Value="48"></asp:ListItem>
                <asp:ListItem Text="Pack Total Count (Z_FINISHED_PACKAGE_QUANTITY)" Value="49"></asp:ListItem>
                <asp:ListItem Text="Pack Total Count UOM (Z_FINISHED_PACKAGE_UOM)" Value="50"></asp:ListItem>
                <asp:ListItem Text="Business Packaging Attribute 1 (Z_PACK_ATTRRIBUTE_1)" Value="51"></asp:ListItem>
                <asp:ListItem Text="Business Packaging Attribute 2 (Z_PACK_ATTRRIBUTE_2)" Value="52"></asp:ListItem>
                <asp:ListItem Text="Contract Mfg Order Type (Z_CONTRACT_MFG_ORDER_TYPE)" Value="53"></asp:ListItem>
                <asp:ListItem Text="Sto/Tmp Conditions Registered (Z_STO_TMP_CONDITION_REGISTERED)" Value="54"></asp:ListItem>
                <asp:ListItem Text="Reserve Sample Discard Rule (Z_RES_SAMP_DISC_RULE)" Value="55"></asp:ListItem>
                <asp:ListItem Text="Cal Exp Date From Potency Test (Z_EXP_DATE_POTENCY)" Value="56"></asp:ListItem>
                <asp:ListItem Text="Batch Release Overdue Limit (Z_BATCH_RELEASE_LIMIT)" Value="57"></asp:ListItem>
                <asp:ListItem Text="Activity for Computing NU (Z_ACTIVITY_FOR_NU)" Value="58"></asp:ListItem>
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

         <%--Z_Global Classification All Chars--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox4" runat="server" Text="Z_Global Classification All Chars" 
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
                <asp:ListItem Text="Z_GLOBAL Combined" Value="59"></asp:ListItem>
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

         <%--Z_US_CHARS Classification--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox5" runat="server" Text="Z_US_CHARS Classification" 
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
                <asp:ListItem Text="US Chars Old Material Number (Z_US_OLD_MATERIAL_NUMBER)" Value="60"></asp:ListItem>
                <asp:ListItem Text="US Chars Controlled Substance Type (Z_US_CONTROLLED_SUBSTANCE_TYPE)" Value="61"></asp:ListItem>
                <asp:ListItem Text="US Chars Overdelivery Tolerance (Z_US_PP_OVERDELIVERY_TOLERANCE)" Value="62"></asp:ListItem>
                <asp:ListItem Text="US Chars Underdelivery Tolerance (Z_US_PP_UNDRDELIVERY_TOLERANCE)" Value="63"></asp:ListItem>
                <asp:ListItem Text="US Chars (All)" Value="64"></asp:ListItem>
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

         <%--Z_ACQUISITION Classification--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox6" runat="server" Text="Z_ACQUISITION Classification" 
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
                <asp:ListItem Text="Acquisition Chars (All)" Value="65"></asp:ListItem>
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

         <%--Z_MULTI_COMPONENT Classification--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox7" runat="server" Text="Z_MULTI_COMPONENT Classification" 
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
                <asp:ListItem Text="Type of Multi-Component Kit" Value="66"></asp:ListItem>
                <asp:ListItem Text="Contents of Kit, Pack, Etc." Value="67"></asp:ListItem>
                <asp:ListItem Text="Component 1 Dosage Strength" Value="68"></asp:ListItem>
                <asp:ListItem Text="Component 1 Dosage Strength UOM" Value="69"></asp:ListItem>
                <asp:ListItem Text="Component 1 Form" Value="70"></asp:ListItem>
                <asp:ListItem Text="Component 2 Dosage Strength" Value="71"></asp:ListItem>
                <asp:ListItem Text="Component 2 Dosage Strength UOM" Value="72"></asp:ListItem>
                <asp:ListItem Text="Component 2 Form" Value="73"></asp:ListItem>
                <asp:ListItem Text="Component 3 Dosage Strength" Value="74"></asp:ListItem>
                <asp:ListItem Text="Component 3 Dosage Strength UOM" Value="75"></asp:ListItem>
                <asp:ListItem Text="Component 3 Form" Value="76"></asp:ListItem>
                <asp:ListItem Text="Total of All Dose Strengths" Value="77"></asp:ListItem>
                <asp:ListItem Text="Unit of Measr for Tot Strength" Value="78"></asp:ListItem>
                <asp:ListItem Text="Comprehensive descrip-all comp" Value="79"></asp:ListItem>
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

         <%--Z_WASTE Classification--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox8" runat="server" Text="Z_WASTE Classification" 
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
                <asp:ListItem Text="Item Family (Presentation)" Value="80"></asp:ListItem>
                <asp:ListItem Text="Item Number (Product Number)" Value="81"></asp:ListItem>
                <asp:ListItem Text="Generator Area" Value="82"></asp:ListItem>
                <asp:ListItem Text="Disposition Code" Value="83"></asp:ListItem>
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

         <%--Z_PROMO Classification--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox9" runat="server" Text="Z_PROMO Classification" 
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
                <asp:ListItem Text="Promo Family" Value="84"></asp:ListItem>
                <asp:ListItem Text="Promo Reorder Point" Value="85"></asp:ListItem>
                <asp:ListItem Text="Promo Location" Value="86"></asp:ListItem>
                <asp:ListItem Text="Promo Regulatory Group" Value="87"></asp:ListItem>
                <asp:ListItem Text="Promo Shopping List" Value="88"></asp:ListItem>
                <asp:ListItem Text="Promo Territory Limit" Value="89"></asp:ListItem>
                <asp:ListItem Text="Promo Price Unit" Value="90"></asp:ListItem>
                <asp:ListItem Text="Promo Pack Size" Value="91"></asp:ListItem>
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

         <%--Classification 2 (delta loads)--%>

        <tr>
        <td style="width:50%">
        <asp:CheckBox ID="chkmainbox10" runat="server" Text="Classification 2 (delta loads)" 
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
                <asp:ListItem Text="Class2 Expiration Date Format" Value="92"></asp:ListItem>
                <asp:ListItem Text="Class2 Manufacture Date Format" Value="93"></asp:ListItem>
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




