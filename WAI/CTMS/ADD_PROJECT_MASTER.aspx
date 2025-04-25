<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ADD_PROJECT_MASTER.aspx.cs" Inherits="CTMS.ADD_PROJECT_MASTER" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });


            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });
        }

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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

        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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
    </script>
    <style type="text/css">
        .label
        {
            margin-left: 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" style="border: none;">
        <div id="Div1" runat="server" class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active" id="liSubDet" runat="server"><a href="ADD_PROJECT_MASTER.aspx"
                    title="ADD PROJECT">ADD PROJECT</a> </li>
                <li class="#" id="liSAE" visible="false" runat="server"><a href="ViewProjects.aspx"
                    title="View Project">View Project</a> </li>
            </ul>
            <div class="tab-content" style="display: block;">
                <div class="tab-pane active" style="display: block;">
                    <div class="box box-warning">
                        <div class="box-header">
                            <h3 class="box-title">
                                Project Details
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="form-group has-warning">
                                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Sponsor : &nbsp;
                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:TextBox runat="server" ID="txtSponsor" CssClass="form-control required width200px" Style="width: 244px;"></asp:TextBox>--%>
                                            <asp:ListBox ID="lstSponsor" runat="server" CssClass="width300px select" SelectionMode="Multiple"
                                                Width="800px"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title">
                                    </h3>
                                </div>
                                <div class="box-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Study Award Date :
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox runat="server" ID="txtStudyAwardDate" CssClass="form-control txtDate width200px"></asp:TextBox>
                                                </div>
                                                <div class="label col-md-1">
                                                    Study ID :
                                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox runat="server" ID="txtStudyID" ValidationGroup="secondOBJ" CssClass="form-control required width200px"></asp:TextBox>
                                                </div>
                                                <div class="label col-md-1">
                                                    Phase :&nbsp;
                                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox runat="server" ID="txtPhase" CssClass="form-control required width150px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Study Title :&nbsp;
                                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox runat="server" ID="txtTitle" TextMode="MultiLine" Style="width: 650px;"
                                                        CssClass="form-control required width200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Product Name :&nbsp;
                                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox runat="server" ID="txtProductName" CssClass="form-control required width200px"></asp:TextBox>
                                                </div>
                                                <div class="label col-md-2">
                                                    Comparator Name :&nbsp;
                                                    <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox runat="server" ID="txtComparator" CssClass="form-control required width200px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Therapeutic Class : &nbsp;
                                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <%--<asp:DropDownList runat="server" AutoPostBack="true" ID="" CssClass="form-control required width200px"
                                        OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                                    <asp:ListBox ID="lstClass" AutoPostBack="true" runat="server" CssClass="width300px select"
                                                        SelectionMode="Multiple" Width="650px" OnSelectedIndexChanged="lstClass_SelectedIndexChanged">
                                                    </asp:ListBox>
                                                </div>
                                                <div class="label col-md-2">
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Therapeutic Sub-Class : &nbsp;
                                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <%--<asp:DropDownList runat="server" ID="ddlSubClass" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubClass_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                                    <asp:ListBox ID="lstSubClass" AutoPostBack="true" runat="server" CssClass="width300px select"
                                                        SelectionMode="Multiple" Width="650px" OnSelectedIndexChanged="lstSubClass_SelectedIndexChanged">
                                                    </asp:ListBox>
                                                </div>
                                                <div class="label col-md-2">
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Indication : &nbsp;
                                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <%--<asp:DropDownList runat="server" ID="ddlIndic" CssClass="form-control required width200px">
                                    </asp:DropDownList>--%>
                                                    <asp:ListBox ID="lstIndic" runat="server" CssClass="width300px select" SelectionMode="Multiple"
                                                        Width="650px"></asp:ListBox>
                                                </div>
                                                <div class="label col-md-2">
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="label col-md-2">
                                                    Primary Objective : &nbsp;
                                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-4" style="display: inline-flex;">
                                                    <asp:TextBox runat="server" ID="txtPrimaryOBJ" TextMode="MultiLine" CssClass="form-control required2"
                                                        Style="width: 250px;"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:LinkButton runat="server" ID="lbtnSavePrimaryOBJ" ToolTip="Add Primary Objective"
                                                        CssClass="cls-btnSave2" OnClick="lbtnSavePrimaryOBJ_Click"><i class="fa fa-check fa-2x"></i></asp:LinkButton>
                                                </div>
                                                <div class="label col-md-2">
                                                    Secondary Objectives :&nbsp;
                                                    <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                                <div class="col-md-4" style="display: inline-flex;">
                                                    <asp:TextBox runat="server" ID="txtSecOBJ" CssClass="form-control required1" TextMode="MultiLine"
                                                        Style="width: 250px;"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton runat="server" ID="lbtnSaveSecObj" ToolTip="Add Secondary Objective"
                                                        CssClass="cls-btnSave1" OnClick="lbtnSaveSecObj_Click"><i class="fa fa-check fa-2x"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    <div class="fixTableHead">
                                                        <asp:GridView ID="gvPrimaryOBJ" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 88%; border-collapse: collapse;" OnRowCommand="gvPrimaryOBJ_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Primary Objective">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOBJ" runat="server" Text='<%# Bind("DATA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Bind("ID") %>' runat="server"
                                                                            CommandName="EditPrimaryObj" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtndeleteOBJ" CommandArgument='<%# Bind("ID") %>' runat="server"
                                                                            CommandName="DeletePrimaryObj" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:HiddenField ID="hdnPrimaryID" runat="server" />
                                                    <asp:HiddenField ID="hdnSecObjID" runat="server" />
                                                    <div class="fixTableHead">
                                                        <asp:GridView ID="gvSecondOBJ" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 88%; border-collapse: collapse;" OnRowCommand="gvSecondOBJ_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Secondary Objective">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOBJ" runat="server" Text='<%# Bind("DATA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Bind("ID") %>' runat="server"
                                                                            CommandName="EditSecObj" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtndeleteOBJ" CommandArgument='<%# Bind("ID") %>' runat="server"
                                                                            CommandName="DeleteSecObj" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lbtnSaveSecObj" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Study Start Date : &nbsp;
                                            <asp:Label ID="Label24" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtStudySTDAT" CssClass="form-control txtDate required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Study End Date :&nbsp;
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtStudyENDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Actual Study Start Date : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtStudyACTSTDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Actual Study End Date :&nbsp;
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtStudyACTENDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            No. of Patients to be Screened : &nbsp;
                                            <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtScreened" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-3">
                                            No. of Patients to be Randomized : &nbsp;
                                            <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtRandomized" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            No. of Patients to be Evaluable : &nbsp;
                                            <asp:Label ID="Label15" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEvaluable" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-3">
                                            No. of Sites : &nbsp;
                                            <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtSites" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            No. of Patients (Dropout) : &nbsp;
                                            <asp:Label ID="Label17" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtPatientsPSPM" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-3">
                                            Enrollment Rate : &nbsp;
                                            <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEnrollRate" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            Enrollment Start Date : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEnrolSTDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-3">
                                            Enrollment End Date : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEnrolENDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            Actual Enrollment Start Date : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEnrollACSTDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-3">
                                            Actual Enrollment End Date : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEnrollACENDAT" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            Enrollment Duration(Months) : &nbsp;
                                            <asp:Label ID="Label23" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtEnrollDur" CssClass="form-control required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-3" style="margin-right: -48px;">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                                Source Options
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            <asp:CheckBox ID="chkIWRS" runat="server" />
                                            &nbsp IWRS
                                        </div>
                                        <div class="label col-md-3">
                                            <asp:CheckBox ID="chkDM" runat="server" />
                                            &nbsp Data Management
                                        </div>
                                        <div class="label col-md-3">
                                            <asp:CheckBox ID="chkLD" runat="server" />
                                            &nbsp Laboratory Data
                                        </div>
                                        <div class="label col-md-3">
                                            <asp:CheckBox ID="chkCTMS" runat="server" />
                                            &nbsp CTMS
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-3">
                                            <asp:CheckBox ID="chkSiteMgmt" runat="server" />
                                            &nbsp Site Management
                                        </div>
                                        <div class="label col-md-3">
                                            <asp:CheckBox ID="chkSafety" runat="server" />
                                            &nbsp Pharmacovigilance
                                        </div>
                                         <div class="label col-md-3">
                                            <asp:CheckBox ID="ChkeSource" runat="server" />
                                            &nbsp eSource
                                        </div>
                                          <div class="label col-md-3">
                                            <asp:CheckBox ID="ChkeTMF" runat="server" />
                                            &nbsp eTMF
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="box">
                            <div class="box-body">
                                <div class="form-group">
                                    <br />
                                    <div class="row txt_center">
                                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                                            OnClick="lbtnCancel_Click"></asp:LinkButton>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
