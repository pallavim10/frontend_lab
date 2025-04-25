<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_Request_List.aspx.cs" Inherits="CTMS.UMT_Request_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="CommonFunctionsJs/UMT/UMT_ConfirmMsg.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable1").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });

            $(".Datatable1").parent().parent().addClass('fixTableHead');
        }
        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_REQUESTS';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/showAuditTrail",
                data: '{TABLENAME: "' + TABLENAME + '",ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrail').html(data.d)
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            $("#popup_AuditTrail").dialog({
                title: "Audit Trail",
                width: 900,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Users Requests Details
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
            <h3 class="box-title">Users Requests Details
            </h3>
            <div id="Div3" class="pull-right" runat="server" style="margin-top: 5px;">
                <asp:LinkButton runat="server" ID="lbUsersRequestsExport" OnClick="lbUsersRequestsExport_Click" CssClass="btn btn-info" Style="color: white;">
			Export User Requests &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="rows">
                <div style="width: 100%; overflow: auto; height: auto">
                    <div>
                        <asp:GridView ID="gvUserRequests" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped Datatable1" OnPreRender="gvUserRequesr_PreRender" OnRowDataBound="gvUserRequests_RowDataBound" OnRowCommand="gvUserRequests_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                    HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%#  Eval("Fname") +" "+ Eval("Lname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Study Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequesttype" runat="server" Text='<%# Bind("REQ_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-left">
                                    <HeaderTemplate>
                                        <label>Request Details </label>
                                        <br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Requested By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server">
                                            <div>
                                                <asp:Label ID="REQ_BYNAME" runat="server" Font-Bold="true" Text='<%# Bind("REQ_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="REQ_CAL_DAT" runat="server" Text='<%# Bind("REQ_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="REQ_CAL_TZDAT" runat="server" Text='<%# Eval("REQ_CAL_TZDAT")+" , "+Eval("REQ_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="HiddenStatus" runat="server" Value='<%# Eval("REQ_STATUS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnReqforActi" runat="server" CommandName="ACTIVATION" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request  for Activation"><i class="fa fa-toggle-on" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReqForDactive" runat="server" CommandName="DEACTIVATION" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request for Deactivation"><i class="fa fa-toggle-off" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtUnlock" runat="server" CommandName="UNLOCK" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request for Unlock"><i class="fa fa-unlock" style='color: blue;font-size:20px'></i></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnGenReqPass" runat="server" CommandName="REQUEST_PASSWORD" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request reset Password"><i class="fa fa-envelope" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnGenReqSecQues" runat="server" CommandName="REQUEST_QUESTION" CommandArgument='<%# Bind("ID") %>'
                                            ToolTip="Request reset Security Question"><i class="fa fa-question-circle" style='color: blue;font-size:20px' ></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:20px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
