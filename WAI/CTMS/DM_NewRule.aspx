<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_NewRule.aspx.cs" Inherits="CTMS.DM_NewRule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript">
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        jQuery(document).ready(function ($) {

            $("#expandHeader").click(function () {

                $("#divGV").slideToggle("slow");

            });

        });
    </script>
    <script type="text/jscript">
        function OpenCalcDiv(element) {
            var ddlName = $(element).attr('id');

            if (ddlName == "MainContent_ddlSymbol1" && $(element).val() != "-1") {

                if ($("#divCalc2").hasClass("disp-none")) {
                    $("#divCalc2").removeClass("disp-none");
                }

                if ($("#divCalc3").hasClass("disp-none")) {
                    $("#divCalc3").addClass("disp-none");
                }

                if ($("#divCalc4").hasClass("disp-none")) {
                }
                else {
                    $("#divCalc4").addClass("disp-none");
                }

                if ($("#divCalc5").hasClass("disp-none")) {
                }
                else {
                    $("#divCalc5").addClass("disp-none");
                }

            }
            else if (ddlName == "MainContent_ddlSymbol2" && $(element).val() != "-1") {

                if ($("#divCalc3").hasClass("disp-none")) {
                    $("#divCalc3").removeClass("disp-none");
                }

                if ($("#divCalc4").hasClass("disp-none")) {
                }
                else {
                    $("#divCalc4").addClass("disp-none");
                }

                if ($("#divCalc5").hasClass("disp-none")) {
                }
                else {
                    $("#divCalc5").addClass("disp-none");
                }

            }
            else if (ddlName == "MainContent_ddlSymbol3" && $(element).val() != "-1") {

                if ($("#divCalc4").hasClass("disp-none")) {
                    $("#divCalc4").removeClass("disp-none");
                }

                if ($("#divCalc5").hasClass("disp-none")) {
                }
                else {
                    $("#divCalc5").addClass("disp-none");
                }

            }
            else if (ddlName == "MainContent_ddlSymbol4" && $(element).val() != "-1") {

                if ($("#divCalc5").hasClass("disp-none")) {
                    $("#divCalc5").removeClass("disp-none");
                }

            }
            else {

                if (ddlName == "MainContent_ddlSymbol1" && $(element).val() == "-1") {

                    if ($("#divCalc2").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc2").addClass("disp-none");
                    }

                    if ($("#divCalc3").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc3").addClass("disp-none");
                    }

                    if ($("#divCalc4").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc4").addClass("disp-none");
                    }

                    if ($("#divCalc5").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc5").addClass("disp-none");
                    }

                }
                else if (ddlName == "MainContent_ddlSymbol2" && $(element).val() == "-1") {

                    if ($("#divCalc3").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc3").addClass("disp-none");
                    }

                    if ($("#divCalc4").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc4").addClass("disp-none");
                    }

                    if ($("#divCalc5").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc5").addClass("disp-none");
                    }

                }
                else if (ddlName == "MainContent_ddlSymbol3" && $(element).val() == "-1") {

                    if ($("#divCalc4").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc4").addClass("disp-none");
                    }

                    if ($("#divCalc5").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc5").addClass("disp-none");
                    }

                }
                else if (ddlName == "MainContent_ddlSymbol4" && $(element).val() == "-1") {

                    if ($("#divCalc5").hasClass("disp-none")) {
                    }
                    else {
                        $("#divCalc5").addClass("disp-none");
                    }
                }
            }
        };
    </script>
    <script language="javascript" type="text/javascript">

        function callOpenCalcDiv() {

            var DDLs = ["MainContent_ddlSymbol1", "MainContent_ddlSymbol2", "MainContent_ddlSymbol3", "MainContent_ddlSymbol4"];
            for (a = 0; a < DDLs.length; ++a) {
                OpenCalcDiv(document.getElementById(DDLs[a]));
            }
        }

        function View_Reference() {
            if ($("#<%=ddlIndication.ClientID%>").val() == "-1") {
                $("#<%=ddlIndication.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var IndicationID = $("#<%= ddlIndication.ClientID %>").val();
            if (IndicationID != "-1") {
                var test = "DM_ReferenceMappings.aspx?IndicationID=" + IndicationID;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }

        }

    </script>
    <style type="text/css">
        .divFormula {
            border-radius: 20px;
            background-color: floralwhite;
            border: outset;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updatepanel2" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Rules</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label7" class="label" runat="server" Text="Indication :"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="control">
                                            <asp:DropDownList ID="ddlIndication" class="form-control width300px required1" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlIndication_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label3" class="label" runat="server" Text="Visit :"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="control">
                                            <asp:DropDownList ID="ddlVisit" class="form-control width300px required1" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlVisit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label1" class="label" runat="server" Text="Module :"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="control">
                                            <asp:DropDownList ID="ddlModule" class="form-control width300px required1" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label2" class="label" runat="server" Text="Field :"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="control">
                                            <asp:DropDownList ID="ddlField" class="form-control width300px required1" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlField_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-info">
                <div class="box-header" runat="server" id="expandHeader">
                    <h3 class="box-title">Added Rules</h3>
                </div>
                <div class="box-body expandBody" runat="server" id="divGV" style="overflow: auto; max-height: 160px;">
                    <asp:GridView ID="gvRules" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                        OnRowCommand="gvRules_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                HeaderText="ID" ControlStyle-Width="100%" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No." ControlStyle-Width="100%" ItemStyle-Width="1%"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rule ID" ControlStyle-Width="100%" ControlStyle-CssClass="txt_center"
                                ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRULE_ID" Text='<%# Bind("RULE_ID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Field Name" ControlStyle-Width="100%" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFIELDNAME" Text='<%# Bind("FIELDNAME") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rule Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" Text='<%# Bind("Description") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Query Text">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuery" Text='<%# Bind("QueryText") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Options" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditRule"
                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteRule"
                                        runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="box box-info">
                <div class="box-header">
                    <h3 class="box-title">Add New Rule</h3>
                    <div class="pull-right" style="margin-right: 1%; margin-top: 1%;">
                        <asp:LinkButton runat="server" ID="lbtnViewReference" Text="View Reference" OnClientClick="View_Reference();"></asp:LinkButton>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label34" class="label" runat="server" Text="Enter Rule ID :"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtRuleID" ReadOnly="true" Width="140px" CssClass="form-control required1"> 
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2" style="width: 25%; display: inline-flex;">
                                    <asp:Label ID="Label35" class="label" runat="server" Text="Select Action :"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlAction" class="form-control required1" runat="server">
                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Generate Query" Value="Generate Query"></asp:ListItem>
                                        <asp:ListItem Text="Set Field Value" Value="Set Field Value"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3" style="display: inline-flex; width: 15%;">
                                    <asp:Label ID="Label43" class="label" runat="server" Text="Enter SEQNO :"></asp:Label>&nbsp;&nbsp;
                                    <asp:TextBox runat="server" ID="txtSEQNO" Width="40px" CssClass="form-control required1"> 
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2" style="width: 11%;">
                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInformational" />&nbsp;&nbsp;
                                    <label>
                                        Informational</label>
                                </div>
                                <div class="col-md-1" style="text-align: left;">
                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAll" />&nbsp;&nbsp;
                                    <label>
                                        All</label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label29" class="label" runat="server" Text="Enter Rule Description :"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtDescription" Width="96%" Height="30px" TextMode="MultiLine"
                                        CssClass="form-control required1"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label6" class="label" runat="server" Text="Enter Query Text :"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtQuery" Width="96%" Height="30px" TextMode="MultiLine"
                                        CssClass="form-control required1"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label4" class="label" runat="server" Text="Select Condition :"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlCondition" class="form-control required1" runat="server">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Is Blank" Value="Is Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Blank" Value="Is Not Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="Is Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="Is Not Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value="Is Greater Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="Is Greater Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="Is Lesser Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="Is Lesser Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="Does Not Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="Contains"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="Does Not Contains"></asp:ListItem>
                                            <asp:ListItem Text="Is Between" Value="Is Between"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Between" Value="Is Not Between"></asp:ListItem>
                                            <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label42" class="label" runat="server" Text="Select Nature :"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlNature" class="form-control required1" runat="server">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Missing Data" Value="Missing Data"></asp:ListItem>
                                            <asp:ListItem Text="Incomplete Data" Value="Incomplete Data"></asp:ListItem>
                                            <asp:ListItem Text="Inconsistent Data" Value="Inconsistent Data"></asp:ListItem>
                                            <asp:ListItem Text="Protocol Deviation" Value="Protocol Deviation"></asp:ListItem>
                                            <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1" style="width: 13%;">
                                    <asp:Label ID="Label5" class="label" runat="server" Text="Enter Value :"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <div class="control">
                                        <asp:TextBox runat="server" ID="txtValue" CssClass="form-control"></asp:TextBox>
                                        <%--<div style="width: 400%;">
                                                [ <b>Note :</b> Enter <b>"Current Date"</b> to compare with <b>Date of Data Entry</b>.
                                                ]
                                            </div>--%>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <asp:HiddenField runat="server" ID="hfFormula" />
                                    <asp:LinkButton ID="lbtnFormula" runat="server" CssClass="btn btn-info" Text="Formula"
                                        Style="color: white;" OnClick="lbtnFormula_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div runat="server" id="divFormula" visible="false" class="divFormula">
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2" style="width: 13%;">
                                        <asp:Label ID="Label36" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtFormula" Width="96%" Height="60px" TextMode="MultiLine"
                                            CssClass="form-control "> 
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2" style="width: 13%;">
                                        <asp:LinkButton ID="lbtnAddFormula" Text="Add" runat="server" Style="color: white;"
                                            CssClass="btn btn-primary btn-sm" OnClick="lbtnAddFormula_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlFunctionFormula" class="form-control" runat="server" Style="width: 100%;">
                                            <asp:ListItem Text="-Select Function-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="DATEADD" Value="DATEADD(<day/month/year>, <number>, <field>)"></asp:ListItem>
                                            <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/month/year>, <field>, <field>)"></asp:ListItem>
                                            <asp:ListItem Text="Today" Value="Today"></asp:ListItem>
                                            <asp:ListItem Text="Now" Value="Now"></asp:ListItem>
                                            <asp:ListItem Text="MAX" Value="MAX(<field>)"></asp:ListItem>
                                            <asp:ListItem Text="MIN" Value="MIN(<field>)"></asp:ListItem>
                                            <asp:ListItem Text="ROUND" Value="ROUND(<field>, <decimals>)"></asp:ListItem>
                                            <asp:ListItem Text="SUM" Value="SUM(<field>)"></asp:ListItem>
                                            <asp:ListItem Text="AVG" Value="AVG(<field>)"></asp:ListItem>
                                            <asp:ListItem Text="CONCAT" Value="CONCAT(<field>,<field>)"></asp:ListItem>
                                            <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                            <asp:ListItem Text="COUNT" Value="COUNT(<field>)"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 txt_center">
                                        OR
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlVisitFormula" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlVisitFormula_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlModuleFormula" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlModuleFormula_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlFieldFormula" class="form-control" runat="server" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="box box-group" style="background-color: lightcyan;">
                        <%--<div style="margin: 1%;">
                            [ <b>Note :</b> Value of <b>Date</b> Control should be in <b>"DD-MMM-YYYY"</b> format
                            only. For e.g. <b>"01-Jan-2019"</b> ]
                        </div>--%>
                        <br />
                        <div id="divCalc1">
                            <div class="row">
                                <div class="col-md-12 txt_center">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:DropDownList ID="ddlSymbol1" class="form-control" onchange="OpenCalcDiv(this);"
                                            runat="server" Style="width: 69px;">
                                            <asp:ListItem Selected="True" Text="N/A" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="AND" Value="AND"></asp:ListItem>
                                            <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label9" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                        <asp:DropDownList ID="ddlVisit1" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlVisit1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label10" class="label" runat="server" Text="Select Module :"></asp:Label>
                                        <asp:DropDownList ID="ddlModule1" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlModule1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label11" class="label" runat="server" Text="Select Field :"></asp:Label>
                                        <asp:DropDownList ID="ddlField1" class="form-control" runat="server" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label28" runat="server" Text="Condition" class="label"></asp:Label>
                                        <asp:DropDownList ID="ddlCondition1" class="form-control" runat="server" Style="width: 100%;">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Is Blank" Value="Is Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Blank" Value="Is Not Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="Is Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="Is Not Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value="Is Greater Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="Is Greater Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="Is Lesser Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="Is Lesser Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="Does Not Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="Contains"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="Does Not Contains"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label8" class="label" runat="server" Text="Enter Value :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtValue1" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:HiddenField runat="server" ID="hfFormula1" />
                                        <br />
                                        <asp:LinkButton ID="lbtnFormula1" runat="server" CssClass="btn btn-info" Text="Formula"
                                            Style="color: white;" OnClick="lbtnFormula1_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divFormula1" visible="false" class="divFormula">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:Label ID="Label37" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="txtFormula1" Width="96%" Height="60px" TextMode="MultiLine"
                                                CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:LinkButton ID="lbtnAddFormula1" Text="Add" runat="server" Style="color: white;"
                                                CssClass="btn btn-primary btn-sm" OnClick="lbtnAddFormula1_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFunctionFormula1" class="form-control" runat="server" Style="width: 100%;">
                                                <asp:ListItem Text="-Select Function-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DATEADD" Value="DATEADD(<day/month/year>, <number>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/month/year>, <field>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="Today" Value="Today"></asp:ListItem>
                                                <asp:ListItem Text="Now" Value="Now"></asp:ListItem>
                                                <asp:ListItem Text="Max" Value="MAX(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="MIN" Value="MIN(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="ROUND" Value="ROUND(<field>, <decimals>)"></asp:ListItem>
                                                <asp:ListItem Text="SUM" Value="SUM(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="AVG" Value="AVG(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="CONCAT" Value="CONCAT(<field>,<field>)"></asp:ListItem>
                                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                                <asp:ListItem Text="COUNT" Value="COUNT(<field>)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 txt_center">
                                            OR
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisitFormula1" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlVisitFormula1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModuleFormula1" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlModuleFormula1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFieldFormula1" class="form-control" runat="server" Style="width: 100%;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <br />
                        </div>
                        <div class="disp-none" id="divCalc2">
                            <div class="row">
                                <div class="col-md-12 txt_center">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:DropDownList ID="ddlSymbol2" class="form-control" onchange="OpenCalcDiv(this);"
                                            runat="server" Style="width: 69px;">
                                            <asp:ListItem Selected="True" Text="N/A" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="AND" Value="AND"></asp:ListItem>
                                            <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label13" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                        <asp:DropDownList ID="ddlVisit2" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlVisit2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label14" class="label" runat="server" Text="Select Module :"></asp:Label>
                                        <asp:DropDownList ID="ddlModule2" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlModule2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label15" class="label" runat="server" Text="Select Field :"></asp:Label>
                                        <asp:DropDownList ID="ddlField2" class="form-control" runat="server" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label30" runat="server" Text="Condition" class="label"></asp:Label>
                                        <asp:DropDownList ID="ddlCondition2" class="form-control" runat="server" Style="width: 100%;">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Is Blank" Value="Is Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Blank" Value="Is Not Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="Is Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="Is Not Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value="Is Greater Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="Is Greater Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="Is Lesser Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="Is Lesser Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="Does Not Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="Contains"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="Does Not Contains"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label12" class="label" runat="server" Text="Enter Value :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtValue2" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:HiddenField runat="server" ID="hfFormula2" />
                                        <br />
                                        <asp:LinkButton ID="lbtnFormula2" runat="server" CssClass="btn btn-info" Text="Formula"
                                            Style="color: white;" OnClick="lbtnFormula2_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divFormula2" visible="false" class="divFormula">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:Label ID="Label38" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="txtFormula2" Width="96%" Height="60px" TextMode="MultiLine"
                                                CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:LinkButton ID="lbtnAddFormula2" Text="Add" runat="server" Style="color: white;"
                                                CssClass="btn btn-primary btn-sm" OnClick="lbtnAddFormula2_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFunctionFormula2" class="form-control" runat="server" Style="width: 100%;">
                                                <asp:ListItem Text="-Select Function-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DATEADD" Value="DATEADD(<day/month/year>, <number>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/month/year>, <field>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="Today" Value="Today"></asp:ListItem>
                                                <asp:ListItem Text="Now" Value="Now"></asp:ListItem>
                                                <asp:ListItem Text="Max" Value="MAX(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="MIN" Value="MIN(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="ROUND" Value="ROUND(<field>, <decimals>)"></asp:ListItem>
                                                <asp:ListItem Text="SUM" Value="SUM(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="AVG" Value="AVG(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="CONCAT" Value="CONCAT(<field>,<field>)"></asp:ListItem>
                                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                                <asp:ListItem Text="COUNT" Value="COUNT(<field>)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 txt_center">
                                            OR
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisitFormula2" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlVisitFormula2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModuleFormula2" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlModuleFormula2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFieldFormula2" class="form-control" runat="server" Style="width: 100%;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <br />
                        </div>
                        <div class="disp-none" id="divCalc3">
                            <div class="row">
                                <div class="col-md-12 txt_center">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:DropDownList ID="ddlSymbol3" class="form-control" onchange="OpenCalcDiv(this);"
                                            runat="server" Style="width: 69px;">
                                            <asp:ListItem Selected="True" Text="N/A" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="AND" Value="AND"></asp:ListItem>
                                            <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label17" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                        <asp:DropDownList ID="ddlVisit3" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlVisit3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label18" class="label" runat="server" Text="Select Module :"></asp:Label>
                                        <asp:DropDownList ID="ddlModule3" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlModule3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label19" class="label" runat="server" Text="Select Field :"></asp:Label>
                                        <asp:DropDownList ID="ddlField3" class="form-control" runat="server" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label31" runat="server" Text="Condition" class="label"></asp:Label>
                                        <asp:DropDownList ID="ddlCondition3" class="form-control" runat="server" Style="width: 100%;">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Is Blank" Value="Is Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Blank" Value="Is Not Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="Is Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="Is Not Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value="Is Greater Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="Is Greater Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="Is Lesser Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="Is Lesser Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="Does Not Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="Contains"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="Does Not Contains"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label16" class="label" runat="server" Text="Enter Value :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtValue3" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:HiddenField runat="server" ID="hfFormula3" />
                                        <br />
                                        <asp:LinkButton ID="lbtnFormula3" runat="server" CssClass="btn btn-info" Text="Formula"
                                            Style="color: white;" OnClick="lbtnFormula3_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divFormula3" visible="false" class="divFormula">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:Label ID="Label39" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="txtFormula3" Width="96%" Height="60px" TextMode="MultiLine"
                                                CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:LinkButton ID="lbtnAddFormula3" Text="Add" runat="server" Style="color: white;"
                                                CssClass="btn btn-primary btn-sm" OnClick="lbtnAddFormula3_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFunctionFormula3" class="form-control" runat="server" Style="width: 100%;">
                                                <asp:ListItem Text="-Select Function-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DATEADD" Value="DATEADD(<day/month/year>, <number>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/month/year>, <field>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="Today" Value="Today"></asp:ListItem>
                                                <asp:ListItem Text="Now" Value="Now"></asp:ListItem>
                                                <asp:ListItem Text="Max" Value="MAX(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="MIN" Value="MIN(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="ROUND" Value="ROUND(<field>, <decimals>)"></asp:ListItem>
                                                <asp:ListItem Text="SUM" Value="SUM(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="AVG" Value="AVG(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="CONCAT" Value="CONCAT(<field>,<field>)"></asp:ListItem>
                                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                                <asp:ListItem Text="COUNT" Value="COUNT(<field>)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 txt_center">
                                            OR
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisitFormula3" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlVisitFormula3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModuleFormula3" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlModuleFormula3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFieldFormula3" class="form-control" runat="server" Style="width: 100%;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <br />
                        </div>
                        <div class="disp-none" id="divCalc4">
                            <div class="row">
                                <div class="col-md-12 txt_center">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:DropDownList ID="ddlSymbol4" class="form-control" onchange="OpenCalcDiv(this);"
                                            runat="server" Style="width: 69px;">
                                            <asp:ListItem Selected="True" Text="N/A" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="AND" Value="AND"></asp:ListItem>
                                            <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label21" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                        <asp:DropDownList ID="ddlVisit4" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlVisit4_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label22" class="label" runat="server" Text="Select Module :"></asp:Label>
                                        <asp:DropDownList ID="ddlModule4" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlModule4_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label23" class="label" runat="server" Text="Select Field :"></asp:Label>
                                        <asp:DropDownList ID="ddlField4" class="form-control" runat="server" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label32" runat="server" Text="Condition" class="label"></asp:Label>
                                        <asp:DropDownList ID="ddlCondition4" class="form-control" runat="server" Style="width: 100%;">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Is Blank" Value="Is Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Blank" Value="Is Not Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="Is Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="Is Not Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value="Is Greater Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="Is Greater Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="Is Lesser Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="Is Lesser Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="Does Not Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="Contains"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="Does Not Contains"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label20" class="label" runat="server" Text="Enter Value :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtValue4" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:HiddenField runat="server" ID="hfFormula4" />
                                        <br />
                                        <asp:LinkButton ID="lbtnFormula4" runat="server" CssClass="btn btn-info" Text="Formula"
                                            Style="color: white;" OnClick="lbtnFormula4_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divFormula4" visible="false" class="divFormula">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:Label ID="Label40" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="txtFormula4" Width="96%" Height="60px" TextMode="MultiLine"
                                                CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:LinkButton ID="lbtnAddFormula4" Text="Add" runat="server" Style="color: white;"
                                                CssClass="btn btn-primary btn-sm" OnClick="lbtnAddFormula4_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFunctionFormula4" class="form-control" runat="server" Style="width: 100%;">
                                                <asp:ListItem Text="-Select Function-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DATEADD" Value="DATEADD(<day/month/year>, <number>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/month/year>, <field>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="Today" Value="Today"></asp:ListItem>
                                                <asp:ListItem Text="Now" Value="Now"></asp:ListItem>
                                                <asp:ListItem Text="Max" Value="MAX(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="MIN" Value="MIN(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="ROUND" Value="ROUND(<field>, <decimals>)"></asp:ListItem>
                                                <asp:ListItem Text="SUM" Value="SUM(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="AVG" Value="AVG(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="CONCAT" Value="CONCAT(<field>,<field>)"></asp:ListItem>
                                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                                <asp:ListItem Text="COUNT" Value="COUNT(<field>)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 txt_center">
                                            OR
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisitFormula4" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlVisitFormula4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModuleFormula4" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlModuleFormula4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFieldFormula4" class="form-control" runat="server" Style="width: 100%;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <br />
                        </div>
                        <div class="disp-none" id="divCalc5">
                            <div class="row">
                                <div class="col-md-12 txt_center">
                                    <div class="col-md-1">
                                        <br />
                                        <asp:DropDownList ID="ddlSymbol5" class="form-control" onchange="OpenCalcDiv(this);"
                                            runat="server" Style="width: 69px;">
                                            <asp:ListItem Selected="True" Text="N/A" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="AND" Value="AND"></asp:ListItem>
                                            <asp:ListItem Text="OR" Value="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label25" class="label" runat="server" Text="Select Visit :"></asp:Label>
                                        <asp:DropDownList ID="ddlVisit5" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlVisit5_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label26" class="label" runat="server" Text="Select Module :"></asp:Label>
                                        <asp:DropDownList ID="ddlModule5" class="form-control" runat="server" AutoPostBack="True"
                                            Style="width: 100%;" OnSelectedIndexChanged="ddlModule5_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label27" class="label" runat="server" Text="Select Field :"></asp:Label>
                                        <asp:DropDownList ID="ddlField5" class="form-control" runat="server" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label33" runat="server" Text="Condition" class="label"></asp:Label>
                                        <asp:DropDownList ID="ddlCondition5" class="form-control" runat="server" Style="width: 100%;">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Is Blank" Value="Is Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Blank" Value="Is Not Blank"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="Is Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="Is Not Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than" Value="Is Greater Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Greater Than OR Equals To" Value="Is Greater Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than" Value="Is Lesser Than"></asp:ListItem>
                                            <asp:ListItem Text="Is Lesser Than OR Equals To" Value="Is Lesser Than OR Equals To"></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="Does Not Begins With"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="Contains"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="Does Not Contains"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label24" class="label" runat="server" Text="Enter Value :"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtValue5" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:HiddenField runat="server" ID="hfFormula5" />
                                        <br />
                                        <asp:LinkButton ID="lbtnFormula5" runat="server" CssClass="btn btn-info" Text="Formula"
                                            Style="color: white;" OnClick="lbtnFormula5_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divFormula5" visible="false" class="divFormula">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:Label ID="Label41" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="txtFormula5" Width="96%" Height="60px" TextMode="MultiLine"
                                                CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="width: 13%;">
                                            <asp:LinkButton ID="lbtnAddFormula5" Text="Add" runat="server" Style="color: white;"
                                                CssClass="btn btn-primary btn-sm" OnClick="lbtnAddFormula5_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFunctionFormula5" class="form-control" runat="server" Style="width: 100%;">
                                                <asp:ListItem Text="-Select Function-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="DATEADD" Value="DATEADD(<day/month/year>, <number>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="DATEDIFF" Value="DATEDIFF(<day/month/year>, <field>, <field>)"></asp:ListItem>
                                                <asp:ListItem Text="Today" Value="Today"></asp:ListItem>
                                                <asp:ListItem Text="Now" Value="Now"></asp:ListItem>
                                                <asp:ListItem Text="Max" Value="MAX(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="MIN" Value="MIN(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="ROUND" Value="ROUND(<field>, <decimals>)"></asp:ListItem>
                                                <asp:ListItem Text="SUM" Value="SUM(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="AVG" Value="AVG(<field>)"></asp:ListItem>
                                                <asp:ListItem Text="CONCAT" Value="CONCAT(<field>,<field>)"></asp:ListItem>
                                                <asp:ListItem Text="String" Value="String"></asp:ListItem>
                                                <asp:ListItem Text="COUNT" Value="COUNT(<field>)"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 txt_center">
                                            OR
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisitFormula5" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlVisitFormula5_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModuleFormula5" class="form-control" runat="server" AutoPostBack="True"
                                                Style="width: 100%;" OnSelectedIndexChanged="ddlModuleFormula5_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFieldFormula5" class="form-control" runat="server" Style="width: 100%;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row txt_center">
                        <asp:LinkButton ID="lbtnsubmit" Text="Submit" runat="server" Style="color: white;"
                            CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="lbtnsubmit_Click" />
                        <asp:LinkButton ID="lbtnUpdate" Text="Update" runat="server" Style="color: white;"
                            Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="lbtnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnCancel" Text="Cancel" runat="server" Style="color: white;"
                            CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click" />
                    </div>
                </div>
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
