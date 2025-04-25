<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_NOT_SYNC_FROM_DDC.aspx.cs" Inherits="CTMS.DM_NOT_SYNC_FROM_DDC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
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

            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
            $(".Datatable").parent().parent().addClass('fixTableHead');
        }

        function showAuditTrail(element) {

            var PVID = $(element).closest('tr').find('td:eq(1)').find('span').html()
            var RECID = $(element).closest('tr').find('td:eq(4)').find('span').html()

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/showAuditTrail_NOT_SYNCED_FROM_DDC",
                data: '{PVID: "' + PVID + '",RECID:"' + RECID + '"}',
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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" id="lblHeader" runat="server">Data Not Synced from DDC
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300;"></asp:Label>
                <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpInvID" ForeColor="Blue" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="form-control ">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" ForeColor="Blue" runat="server" CssClass="form-control select"
                            AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Visit:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisit" ForeColor="Blue" runat="server" AutoPostBack="True"
                            CssClass="form-control select" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex; margin-bottom: -27px;">
                    <label class="label">
                    </label>
                    <div class="Control">
                        <asp:Button ID="Btn_Get_Data" runat="server" OnClick="Btn_Get_Data_Click" CssClass="btn btn-primary btn-sm cls-btnSave" Style="margin-bottom: -27px;"
                            Text="Get Data" />
                        <asp:LinkButton ID="btnExportExcel" Visible="false" runat="server" Text="Export Reviews Logs" OnClick="btnExportExcel_Click" CssClass="btn btn-info btn-sm" Style="margin-bottom: -27px;" ForeColor="White" Font-Bold="true"> Export To Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-primary" runat="server" id="divRecord">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Records
            </h4>
        </div>
        <div class="box-body">
            <div class="rows">
                <div class="fixTableHead">
                    <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable"
                        OnPreRender="grdField_PreRender" EmptyDataText="No Record Found.">
                        <Columns>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="PVID" runat="server" Text='<%# Bind("PVID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INVID" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="INVID" Font-Size="Small" Text='<%# Bind("INVID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject Id" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="SUBJID" Font-Size="Small" Text='<%# Bind("SUBJID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Record No." HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                    <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="VISIT" Font-Size="Small" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Module Name" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="MODULENAME" Font-Size="Small" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entry Status" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="EntryStatus" Font-Size="Small" Text='<%# Bind("EntryStatus") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>SDR Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SDR Status]</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[SDR By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="divSDR">
                                        <div>
                                            <asp:Label ID="SDR_STATUSNAME" runat="server" Text='<%# Bind("SDRSTATUS") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="SDRBYNAME" runat="server" Text='<%# Bind("SDRBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="SDR_CAL_DAT" runat="server" Text='<%# Bind("SDR_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="SDR_CAL_TZDAT" runat="server" Text='<%# Bind("SDR_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Signed Off Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Signed Off By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <div>
                                            <asp:Label ID="InvSignOffBYNAME" runat="server" Text='<%# Bind("InvSignOffBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="InvSignOff_CAL_DAT" runat="server" Text='<%# Bind("InvSignOff_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="InvSignOff_CAL_TZDAT" runat="server" Text='<%# Bind("InvSignOff_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
