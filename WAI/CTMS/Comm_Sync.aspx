<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_Sync.aspx.cs" Inherits="CTMS.Comm_Sync" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );
            });
        }
    </script>
    <script language="javascript" type="text/javascript">

        $(function () {
            $('.txtTime').inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Import from Mailbox
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body" style="font-weight: bold;">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1 width120px">
                            From Email ID :
                        </div>
                        <div class="col-md-4" style="display: inline-flex;">
                            <asp:TextBox ID="txtEMAILID" CssClass="form-control" runat="server" autocomplete="off"
                                Width="400px"></asp:TextBox>
                        </div>
                        <div class="col-md-1  width130px">
                            Subject Contains:
                        </div>
                        <div class="col-md-4" style="display: inline-flex;">
                            <asp:TextBox ID="txtSubject" CssClass="form-control" runat="server" autocomplete="off"
                                Width="400px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1  width120px">
                            Body Contains:
                        </div>
                        <div class="col-md-10" style="display: inline-flex;">
                            <asp:TextBox ID="txtBody" CssClass="form-control" runat="server" autocomplete="off"
                                Width="839px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1 width120px">
                            From Date
                        </div>
                        <div class="col-md-3" style="display: inline-flex;">
                            <asp:TextBox ID="txtDateFrom" CssClass="form-control required txtDate" runat="server"
                                autocomplete="off" Width="110px"></asp:TextBox>
                            <asp:TextBox ID="txtTimeFrom" CssClass="form-control required txtTime" runat="server"
                                autocomplete="off" Text="00:00" Width="90px"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            To Date
                        </div>
                        <div class="col-md-4" style="display: inline-flex;">
                            <asp:TextBox ID="txtDateTo" CssClass="form-control required txtDate" runat="server"
                                autocomplete="off" Width="110px"></asp:TextBox>
                            <asp:TextBox ID="txtTimeTo" CssClass="form-control required txtTime" runat="server"
                                autocomplete="off" Text="11:59" Width="90px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1 width120px">
                            &nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnGet" runat="server" Text="Get Mails" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnGet_Click" />
                        </div>
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <asp:GridView ID="Grd_Mails" runat="server" AutoGenerateColumns="False" CellPadding="3"
            Style="text-align: center" CellSpacing="2" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                    <ItemTemplate>
                        <asp:Label ID="txtID" runat="server" Style="border: none; text-align: center" Text='<%# Bind("ID") %>'
                            Width="59px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From">
                    <ItemTemplate>
                        <asp:Label ID="txtFrom" runat="server" Text='<%# Bind("From") %>' Width="100%"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="txtSubject" runat="server" Text='<%# Bind("Subject") %>' Width="100%"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DateTime">
                    <ItemTemplate>
                        <asp:Label ID="txtDateTime" runat="server" Text='<%# Bind("DateTime") %>' Width="100%"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Import" ItemStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <asp:Button ID="Btn_Update" runat="server" OnClick="Btn_Sync_Click" CssClass="btn btn-primary btn-sm "
                            Text="Sync" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Chk_Sync" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Update"></asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
