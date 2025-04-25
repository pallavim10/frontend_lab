<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_REVIEW_HISTORY.aspx.cs" Inherits="CTMS.NCTMS_REVIEW_HISTORY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script>
        function REVIEW_HISTORY(element) {

            var SVID = $(element).closest('tr').find('td:eq(3)').find('span').text();

            var test = "NCTMS_REVIEW_HISTORY_LIST.aspx?SVID=" + SVID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%; display: inline-flex; font-size: medium;">
                Visit Matrix
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div id="Div1" runat="server" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Site Id:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSiteId" runat="server" ForeColor="Blue" CssClass="form-control required"
                                OnSelectedIndexChanged="ddlSiteId_SelectedIndexChanged" AutoPostBack="True" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Visit Type:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisitType" runat="server" ForeColor="Blue" AutoPostBack="True"
                            CssClass="form-control required" Style="width: 100%" OnSelectedIndexChanged="drpVisitType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Visit Id:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisitID" ForeColor="Blue" runat="server" AutoPostBack="True"
                            CssClass="form-control required" OnSelectedIndexChanged="drpVisitID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="box">
                    <asp:GridView runat="server" ID="grdVisits" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                        Width="100%" OnRowDataBound="grdVisits_RowDataBound" OnPreRender="gvEmp_PreRender">
                        <Columns>
                            <asp:TemplateField HeaderText="Site Id" HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit Type">
                                <ItemTemplate>
                                    <asp:Label ID="VISIT_NAME" runat="server" Text='<%# Bind("VISIT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="VISITID" runat="server" Text='<%# Bind("VISITID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit Id" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="SVID" runat="server" Text='<%# Bind("SVID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="User_Name" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Status">
                                <ItemTemplate>
                                    <asp:Label ID="SUB_STATUS" runat="server" Text='<%# Bind("SUB_STATUS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="STARTDT" runat="server" Text='<%# Bind("STARTDT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="ENDDT" runat="server" Text='<%# Bind("ENDDT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnReviewHistory" runat="server" ToolTip="Review History" OnClientClick="return REVIEW_HISTORY(this);"
                                        CommandArgument='<%# Eval("SVID") %>'>
                                        <i id="iconhistory" runat="server" class="fa fa-history" style="color: #333333;"
                                            aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
