<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_QueryText_Change.aspx.cs" Inherits="CTMS.MM_QueryText_Change" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript">

        function pageLoad() {
            //    $(".Datatable").dataTable();
            $(".Datatable1").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: true
            });

            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }

        function SHOWDETAILS(element) {

            var RECID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var PVID = $(element).closest('tr').find('td:eq(1)').text().trim();
            var LISTID = $(element).closest('tr').find('td:eq(2)').text().trim();

            var url = "MM_QUERY_DATA.aspx?RECID=" + RECID + "&PVID=" + PVID + "&LISTID=" + LISTID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1300,resizable=no";
            window.open(url, '_blank', strWinProperty);
        }

        // when query override then popup will open and with comments it update status 
        function OpenDeleteCommentBox(lnk) {

            var id = $(lnk).closest('tr').find('td').eq(0).text().trim();
            $('#lblMMQueryID').val(id);
            $('#txtDeleteComment').val("");

            $("#MM_popup_DeleteComments").dialog({
                title: "Delete Reason",
                width: 700,
                height: 450,
                modal: true
            });

            return false;
        }

        function SaveDeletedCommnet() {

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/MM_ADD_DELETED_QUERY_COMMENT",
                data: '{ID: "' + $("#lblMMQueryID")[0].value + '",Comment: "' + $("#txtDeleteComment")[0].value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert(data.d);
                        $(location).attr('href', window.location.href);
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
        }

        function ShowQueryComments(element) {
            var ID = "";
            ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/GET_MM_ALL_COMMENT",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#MM_div_Comments').html(data.d)
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
            $("#MM_popup_Comments").dialog({
                title: "Comment Details",
                width: 700,
                height: 400,
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
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="Updatepanel1">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Query Reports</h3>
                </div>
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
                </div>
                <div class="">
                    <br />
                    <div runat="server" id="Div1" class="" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblSiteId" runat="server" CssClass="wrapperLable" Text="Select Site Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpSite" CssClass="form-control required" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div2" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblPatientId" runat="server" CssClass="wrapperLable" Text="Select Subject Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpPatient" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="drpPatient_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div3" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblVisitId" runat="server" CssClass="wrapperLable" Text="Select Visit:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div5" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblModule" runat="server" CssClass="wrapperLable" Text="Select Module:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpModule" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div7" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblQueryStatus" runat="server" CssClass="wrapperLable" Text="Query Status:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpQueryStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                <asp:ListItem Text="Posted" Value="Posted"></asp:ListItem>
                                <asp:ListItem Text="Deleted" Value="Deleted"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div8" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Query Type:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpQueryType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Auto" Value="Auto"></asp:ListItem>
                                <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                        OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <div id="Div6" class="dropdown" runat="server" style="display: inline-flex">
                        <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                            style="color: #333333" title="Export"></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                    Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                </asp:LinkButton></li>
                            <hr style="margin: 5px;" />
                        </ul>
                    </div>
                </div>
            </div>
            <div class="box-group">
                <div class="form-group">
                    <div style="overflow: auto;">
                        <asp:GridView ID="grdMMQueryDetailReports" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped Datatable1" OnPreRender="grd_data_PreRender"
                            CellPadding="4" CellSpacing="2" OnRowCommand="grdMMQueryDetailReports_RowCommand"
                            OnRowDataBound="grdMMQueryDetailReports_RowDataBound" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="RECID" runat="server" Text='<%# Bind("RECID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="PVID" runat="server" Text='<%# Bind("PVID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LISTING_ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="LISTING_ID" runat="server" Text='<%# Bind("LISTING_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VISITID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="VISITID" runat="server" Text='<%# Bind("VISITID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site Id">
                                    <ItemTemplate>
                                        <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject Id">
                                    <ItemTemplate>
                                        <asp:Label ID="Subject" runat="server" Text='<%# Bind("Subject") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MULTIPLEYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MODULEID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reference">
                                    <ItemTemplate>
                                        <asp:Label ID="Txt_REFERENCE" runat="server" Text='<%# Bind("REFERENCE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Text">
                                    <ItemTemplate>
                                        <asp:Label ID="Txt_QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visit">
                                    <ItemTemplate>
                                        <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module Name">
                                    <ItemTemplate>
                                        <asp:Label ID="Txt_MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Type">
                                    <ItemTemplate>
                                        <asp:Label ID="Txt_QUERYTYPE" runat="server" Text='<%# Bind("QUERYTYPE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="RESOLVE_TYPE" runat="server" Text='<%# Bind("RESOLVE_TYPE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="VISITCOUNT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="VISITCOUNT" runat="server" Text='<%# Bind("VISITCOUNT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="STATUS" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="STATUS" runat="server" Text='<%# Bind("STATUS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Opened">
                                    <ItemTemplate>
                                        <asp:Label ID="OPENBY" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reviewed">
                                    <ItemTemplate>
                                        <asp:Label ID="Reviewed" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Closed">
                                    <ItemTemplate>
                                        <asp:Label ID="RESOLVEBY" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deleted">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDELETEBY" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deleted Comment">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDELETECOMMNET" runat="server" Text='<%# Bind("DELETED_COMMENTS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <asp:Button ID="btnReview" runat="server" Text="Review" CssClass="btn btn-primary" OnClick="btnReview_Click" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkreview" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnChangeQueryText" runat="server" ToolTip="Change Query Text" CommandName="ChangeQueryText" CommandArgument='<%# Bind("ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbldeletequery" runat="server" ToolTip="Delete Query" OnClientClick="return OpenDeleteCommentBox(this)" CommandArgument='<%# Bind("ID") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnComments" OnClientClick="return ShowQueryComments(this);" runat="server">
                                            <i title="Comments" id="iconComments" runat="server" class="fa fa-comment" style="color: #333333;"
                                                aria-hidden="true"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gridData_Tran" Visible="false" HeaderStyle-CssClass="txt_center"
                            runat="server" AutoGenerateColumns="true" OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped">
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
