<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MM_PD_LIST.aspx.cs" Inherits="CTMS.MM_PD_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script language="javascript" type="text/javascript">

        function ShowAuditTrail_PD(ID) {

            $.ajax({
                type: "POST",
                url: "MM_PD_LIST.aspx/showAuditTrail",
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
                title: "Audit trail",
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
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="lblHeader" runat="server" Text="Protocol Deviation Log"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div runat="server" id="Div2" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width65px">
                            Country:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl"
                                AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div runat="server" id="DivINV" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width65px">
                            Site ID :
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control"
                                OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <label class="label width65px">
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btngetdata_Click" />
                &nbsp&nbsp&nbsp&nbsp
                <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                        style="color: #333333" title="Export"></a>
                    <ul class="dropdown-menu dropdown-menu-sm">
                        <li>
                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                            </asp:LinkButton>
                        </li>
                        <hr style="margin: 5px;" />
                    </ul>
                </div>
                <br />
                <br />
                <div class="box-body">
                    <div class="rows">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdPROTVIOL" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                    OnRowDataBound="grdPROTVIOL_RowDataBound" OnRowCommand="grdPROTVIOL_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="View">
                                                        <i title="View" class="fa fa-search" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Show Audittrail" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtnAudittrail" OnClientClick='<%# string.Format("return ShowAuditTrail_PD("+ Eval("ID")+" );") %>'>
                                                        <i class="fa fa-clock-o" style="font-size:14px" aria-hidden="true"></i>
                                                </asp:LinkButton>
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
</asp:Content>
