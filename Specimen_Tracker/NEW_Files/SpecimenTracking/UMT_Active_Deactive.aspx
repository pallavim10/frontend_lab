<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Active_Deactive.aspx.cs" Inherits="SpecimenTracking.UMT_Active_Deactive" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="Style/Select2.css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
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

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_Site';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/showAuditTrail",
                data: JSON.stringify({ TABLENAME: TABLENAME, ID: ID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    } else {
                        $('#DivAuditTrail').html(response.d);
                        $('#modal-lg').modal('show'); // Show the modal after populating it
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching audit trail:', status, error);
                    alert("An error occurred. Please contact the administrator.");
                }
            });

            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Site Activation Deactivation</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Manage Sites</li>
                            <li class="breadcrumb-item active">Site Activation Deactivation</li>
                        </ol>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Site Details</h3>
                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto; height: auto">
                                        <div>
                                            <asp:GridView ID="grdSite" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender" OnRowDataBound="grdSite_RowDataBound" OnRowCommand="grdSite_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                        HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSiteId" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSiteName" runat="server" Text='<%# Bind("SiteName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Country">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblState" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCity" runat="server" Text='<%# Bind("CITYNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                            <asp:HiddenField ID="HiddenActive" runat="server" Value='<%# Eval("ACTIVE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label>Site  Activation </label>
                                                            <br />
                                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Activated By]</label><br />
                                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div runat="server">
                                                                <div>
                                                                    <asp:Label ID="ACTIVEBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("ACTIVEBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="ACTIVE_CAL_DAT" runat="server" Text='<%# Bind("ACTIVE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="ACTIVE_CAL_TZDAT" runat="server" Text='<%# Eval("ACTIVE_CAL_TZDAT")+" "+Eval("ACTIVE_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label>Site Deactivation </label>
                                                            <br />
                                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deactivated By]</label><br />
                                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div runat="server">
                                                                <div>
                                                                    <asp:Label ID="DEACTIVEBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("DEACTIVEBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="DEACTIVE_CAL_DAT" runat="server" Text='<%# Bind("DEACTIVE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="DEACTIVE_CAL_TZDAT" runat="server" Text='<%# Eval("DEACTIVE_CAL_TZDAT")+" "+Eval("DEACTIVE_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" CssClass="btn-info btn-sm" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Activation/Deactivation" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtActive" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DEACTIVATION" ToolTip="Deactivate"><i class="fa fa-toggle-on" style='color: #333;font-size:20px' ></i></asp:LinkButton>
                                                            <asp:Label ID="lblActive" runat="server"
                                                                ToolTip="This site can not be deactivated, as all site users are active."><i class="fa fa-toggle-on" style='color: red;font-size:20px' ></i></asp:Label>
                                                            <asp:LinkButton ID="lbtDeactive" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="ACTIVATION" ToolTip="Activate"><i class="fa fa-toggle-off" style='color: #333;font-size:20px' ></i></asp:LinkButton>
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
        </section>
    </div>

</asp:Content>

