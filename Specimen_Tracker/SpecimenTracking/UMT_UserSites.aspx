<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_UserSites.aspx.cs" Inherits="SpecimenTracking.UMT_UserSites" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
        .txt_center {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });

            $('.select').select2();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Assign Sites</li>
                        </ol>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Assign Sites</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" Font-Size="14px" CssClass="btn btn-default" ForeColor="Black" ID="lbtnExport" ToolTip="Export Assigned Sites" OnClick="lbtnExport_Click"> Export Assigned Sites &nbsp;<span class="fas fa-download btn-xs"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>

                    <div class="row">
                        <div class="col-md-12" style="display:flex">
                            <div class="col-md-5">
                                <div class="card card-info" style="min-height: 264px;">
                                    <div class="card-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="card-title" align="left">Assign Site To User
                                        </h4>
                                        <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <asp:label runat="server" ID="lblSelectUser" Text="Select User:" CssClass="font-weight-bold"></asp:label>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="DrpUser" class="form-control drpControl required width200px" runat="server" AutoPostBack="True" Style="margin-bottom: 1px" OnSelectedIndexChanged="DrpUser_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="rows">
                                                    <div style="width: 100%; height: 300px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 97%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="d-none"
                                                                        ItemStyle-CssClass="d-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteName" runat="server" Text='<%# Bind("SiteName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="ChkSITEID" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <br />
                                <br />
                                <br />
                                <br />
                                <div class="card-body">
                                    <div style="min-height: 300px; padding:0.5%">
                                        <div class="row txtCenter justify-content-center">
                                            <asp:LinkButton ID="lbtnAddFunctionName" ForeColor="White" Text="Add" runat="server"
                                                CssClass="btn btn-primary btn-sm cls-btnSave " OnClick="lbtnAddFunctionName_Click" />
                                        </div>
                                        <div class="row txtCenter">
                                            &nbsp;
                                        </div>
                                        <div class="row txtCenter justify-content-center">
                                            <asp:LinkButton ID="lbtnRemoveFunctionName" ForeColor="White" Text="Remove" runat="server"
                                                CssClass="btn btn-primary btn-sm cls-btnSave justify-content-center" OnClick="lbtnRemoveFunctionName_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card card-info" style="min-height: 300px;">
                                    <div class="card-header with-border" style="float: left;">
                                        <h4 class="card-title" align="left">Records
                                        </h4>
                                        <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdAddedUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="d-none"
                                                                        ItemStyle-CssClass="d-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteName" runat="server" Text='<%# Bind("SiteName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAddedSiteID" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>

