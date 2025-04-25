<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_ACTIVITY_AUDIT_TRAIL.aspx.cs" Inherits="CTMS.DB_ACTIVITY_AUDIT_TRAIL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJS/TabIndex.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }

        .Mandatory {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .REQUIRED {
            background-color: yellow;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" runat="server" id="content">
        <div class="box-header">
            <h3 class="box-title">Activity Audit Trail Report
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>
                        Select Activity:</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList Style="width: 250px;" ID="ddlActivity" runat="server" class="form-control drpControl"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" TabIndex="1">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="Master Library" Text="Master Library"></asp:ListItem>
                        <asp:ListItem Value="Master Library Options" Text="Master Library Options"></asp:ListItem>
                        <asp:ListItem Value="Modules" Text="Modules"></asp:ListItem>
                        <asp:ListItem Value="Fields" Text="Fields"></asp:ListItem>
                        <asp:ListItem Value="Field Options" Text="Field Options"></asp:ListItem>
                        <asp:ListItem Value="Conditional Visibility" Text="Conditional Visibility"></asp:ListItem>
                        <asp:ListItem Value="OnChange Criterias" Text="OnChange Criterias"></asp:ListItem>
                        <asp:ListItem Value="OnSubmit OR OnLoad Criterias" Text="OnSubmit OR OnLoad Criterias"></asp:ListItem>
                        <asp:ListItem Value="OnSubmit OR OnLoad Criterias (Variables)" Text="OnSubmit OR OnLoad Criterias (Variables)"></asp:ListItem>
                        <asp:ListItem Value="Code Mapping" Text="Code Mapping"></asp:ListItem>
                        <asp:ListItem Value="Lab Reference Range Mapping" Text="Lab Reference Range Mapping"></asp:ListItem>
                        <asp:ListItem Value="Listings" Text="Listings"></asp:ListItem>
                        <asp:ListItem Value="Listings Criterias" Text="Listings Criterias"></asp:ListItem>
                        <asp:ListItem Value="Rules" Text="Rules"></asp:ListItem>
                        <asp:ListItem Value="Labs" Text="Labs"></asp:ListItem>
                        <asp:ListItem Value="Lab Reference Range" Text="Lab Reference Range"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div id="divModules" runat="server" visible="false">
                    <div class="col-md-2">
                        <label>
                            Select Module:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="drpModule" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged" TabIndex="2">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divFields" runat="server" visible="false">
                    <div class="col-md-1 width100px">
                        <label>Select Field:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="ddlFields" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged" TabIndex="3">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divLab" runat="server" visible="false">
                    <div class="col-md-2">
                        <label>
                            Select Lab ID:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="drpLab" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpLab_SelectedIndexChanged" TabIndex="4">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divRules" runat="server" visible="false">
                    <div class="col-md-2">
                        <label>
                            Select Rule ID:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="drpRule" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpRule_SelectedIndexChanged" TabIndex="5">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divListing" runat="server" visible="false">
                    <div class="col-md-2">
                        <label>
                            Select Lisitng:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="drpListing" runat="server" class="form-control drpControl select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpListing_SelectedIndexChanged" TabIndex="6">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divListingCrit" runat="server" visible="false">
                    <div class="col-md-2">
                        <label>
                            Select Lisitng Criteria:</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList Style="width: 250px;" ID="ddlListingCrit" runat="server" class="form-control drpControl required1 select"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlListingCrit_SelectedIndexChanged" TabIndex="7">
                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Additional Fields" Value="Additional Fields"></asp:ListItem>
                            <asp:ListItem Text="Criterias" Value="Criterias"></asp:ListItem>
                            <asp:ListItem Text="Criteria Across Modules" Value="Criteria Across Modules"></asp:ListItem>
                            <asp:ListItem Text="Criteria Across Listings" Value="Criteria Across Listings"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShow" runat="server" Text="Show" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnShow_Click" TabIndex="8" />
                    <asp:LinkButton ID="btnExportExcel" Visible="false" runat="server" Text="Export Reviews Logs" OnClick="btnExportExcel_Click" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true" TabIndex="9"> Export To Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-primary" runat="server" id="divRecord">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Records
            </h4>
        </div>
        <div class="box-body">
            <div class="rows">
                <div class="fixTableHead">
                    <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped Datatable"
                        OnPreRender="grdField_PreRender" EmptyDataText="No Record Found.">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
