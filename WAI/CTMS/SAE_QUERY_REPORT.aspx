<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SAE_QUERY_REPORT.aspx.cs" Inherits="CTMS.SAE_QUERY_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }

        function SAE_showQueryComment(element) {

            var ID = $(element).closest('tr').find('td:eq(7)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_SAE.aspx/SAE_ShowQueryComment",
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
                        $('#divShowQryComment').html(data.d)
                        $("#Popup_ShowQryComment").dialog({
                            title: "Comment",
                            width: 900,
                            height: 500,
                            modal: true,
                            buttons: {
                                "Close": function () { $(this).dialog("close"); }
                            }
                        });
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
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">SAE Query Report</h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="divSIte" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divSubject" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Subject ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlSUBJID" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSUBJID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divSAEID" style="display: inline-flex">
                            <label class="label">
                                SAE ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div runat="server" id="divgetdata" style="display: inline-flex">
                            <asp:Button ID="btngetdata" Text="Get Data" runat="server" OnClick="btngetdata_Click" CssClass="btn btn-primary btn-sm" />
                        </div>
                        &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                        Text="Export to Excel" ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="false" Width="98%" CssClass="table table-bordered table-striped Datatable"
                                            OnPreRender="gridData_PreRender" OnRowDataBound="gridData_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Query Comment" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblComment" ToolTip="Comments" runat="server" Visible="false" OnClientClick="return SAE_showQueryComment(this);">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;" ></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Site ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SiteID" runat="server" Text='<%# Eval("Site ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SubjectID" runat="server" Text='<%# Bind("[Subject ID]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RecordNo" Font-Size="Small" Text='<%# Eval("[RecordNo]") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SAEID" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SAEID" runat="server" Text='<%# Bind("SAEID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ModuleName" runat="server" Text='<%# Bind("[Module Name]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FieldName" runat="server" Text='<%# Bind("[Field Name]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query ID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="QueryID" runat="server" Text='<%# Bind("[Query ID]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query Text" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="QueryText" runat="server" Text='<%# Bind("[Query Text]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query Status" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="QueryStatus" runat="server" Text='<%# Bind("[Query Status]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query Type" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Querytype" runat="server" Text='<%# Bind("[Query Type]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                    <HeaderTemplate>
                                                        <label>Generated Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div runat="server" id="divGenerated">
                                                            <div>
                                                                <asp:Label ID="QRYGENBYNAME" runat="server" Text='<%# Bind("QRYGENBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="QRYGEN_CAL_DAT" runat="server" Text='<%# Bind("QRYGEN_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="QRYGEN_CAL_TZDAT" runat="server" Text='<%# Bind("QRYGEN_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                    <HeaderTemplate>
                                                        <label>Resolved Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Resolved By]</label><br />
                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div runat="server" id="divResolved">
                                                            <div>
                                                                <asp:Label ID="QRYRESBYNAME" runat="server" Text='<%# Bind("QRYRESBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="QRYRES_CAL_DAT" runat="server" Text='<%# Bind("QRYRES_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="QRYRES_CAL_TZDAT" runat="server" Text='<%# Bind("QRYRES_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Answered Text" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LastAnsweredText" runat="server" Text='<%# Bind("[Last Answered Text]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Closed Text" HeaderStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LastClosedText" runat="server" Text='<%# Bind("[Last Closed Text]") %>' />
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
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
