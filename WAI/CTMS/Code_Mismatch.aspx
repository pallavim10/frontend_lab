<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Code_Mismatch.aspx.cs" Inherits="CTMS.Code_Mismatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/CD/Coding_AuditTrails.js"></script>
    <script src="CommonFunctionsJs/CD/Coding_Grid_Queries.js"></script>
    <script language="javascript" type="text/javascript">

        function showCodeMismatches(element) {

            var PVID = $(element).closest('tr').find('td').eq(2).text().trim();
            var RECID = $(element).closest('tr').find('td').eq(3).text().trim();
            var MODULEID = $('#MainContent_drpForm').val();

            $.ajax({
                type: "POST",
                url: "Code_Mismatch.aspx/showCodeMismatches",
                data: '{PVID: "' + PVID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivCodeMismatches').html(data.d)
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

            $("#popup_CodeMismatches").dialog({
                title: "Other matching terms",
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
                "bSort": true, "ordering": true,
                "bDestroy": true, stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="content">
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Code Mismatches
                </h3>
            </div>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="row">
                        <div class="col-md-12">
                            <br />
                            <div class="col-md-2">
                                <label>
                                    Select Form:</label>
                            </div>
                            <div class="col-md-10">
                                <asp:DropDownList Style="width: 30%;" ID="drpForm" runat="server" class="form-control drpControl required"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpForm_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div class="box-body">
                    <div class="box box-success" runat="server" id="SITESUBJ" visible="false">
                        <br />
                        <div class="form-group">
                            <div runat="server" id="divSIte" style="display: inline-flex" visible="false">
                                <div style="display: inline-flex">
                                    <label class="label width70px">
                                        Site ID:
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="divSubject" style="display: inline-flex" visible="false">
                                <div style="display: inline-flex">
                                    <label class="label width70px">
                                        Subject ID:
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="ddlSUBJID" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btngetdata_Click" />&nbsp&nbsp&nbsp&nbsp
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="GridView1_PreRender" OnRowCommand="gridData_RowCommand" CssClass="table table-bordered Datatable table-striped" OnRowDataBound="gridData_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Change Code">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" ToolTip="Change Code" CommandName="ChangeCode"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Show other matching terms">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnShow" runat="server" OnClientClick="return showCodeMismatches(this);" ToolTip="Show other matching terms"><i class="fa fa-search"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="PVID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PVID" runat="server" Text='<%# Bind("PVID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="RECID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RECID" runat="server" Text='<%# Bind("RECID") %>'></asp:Label>
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
    <div id="popup_CodeMismatches" title="Other matching terms" class="disp-none">
        <div id="DivCodeMismatches" style="font-size: small;">
        </div>
    </div>
</asp:Content>
