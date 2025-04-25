<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_CreateAgenda.aspx.cs" Inherits="CTMS.CTMS_CreateAgenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css">
    <script type="text/jscript">
        function pageLoad() {
            $('.txtTime').timepicker({
                timeFormat: 'HH:mm',
                interval: 1,
                minTime: '1',
                // maxTime: '6:00pm',
                //  defaultTime: '01',
                startTime: '01:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true
            });

            $('.txtTime').keypress(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
                    //let it happen, don't do anything
                    return true;
                }
                event.preventDefault();
                return false;
            });

            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),

                    // for date restriction
                    //  maxDate: new Date(),  
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
            $('.txtDate').keypress(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
                    //let it happen, don't do anything
                    return true;
                }
                event.preventDefault();
                return false;
            });





        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Upd_Pan_Sel_Dept" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Create Meeting</h3>
                </div>
                <!-- /.box-header -->
                <!-- text input -->
                <div class="box-body">
                    <div class="row">
                        <div class="lblError">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td class="label">
                                    Meeting Date:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:TextBox ID="txtAgendaDate" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                                </td>
                                <td class="style10">
                                </td>
                                <td class="label">
                                    Meeting Time
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:TextBox ID="txtAgendaTime" runat="server" MaxLength="5" CssClass="form-control txtTime required"></asp:TextBox>
                                </td>
                                <td class="style10">
                                </td>
                                <td class="label">
                                    Meeting venue
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:TextBox ID="txtVanue" runat="server" CssClass="form-control required"></asp:TextBox>
                                    <asp:TextBox ID="txtAgendaID" runat="server" CssClass="form-control required" Visible="false"></asp:TextBox>
                                </td>
                                <td class="style10">
                                </td>
                            </tr>
                        </table>
                        <table>
                            <caption>
                                <br />
                                <tr>
                                    <td class="label">
                                        Select Topics:
                                    </td>
                                    <td class="requiredSign">
                                        <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                                    </td>
                                    <td class="Control">
                                        <asp:ListBox ID="lstAgendaTopic" runat="server" CssClass="width300px select required"
                                            SelectionMode="Multiple" Width="409px"></asp:ListBox>
                                    </td>
                                    <td class="style10">
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 230px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Select Internal Attendies
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select User:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="drpInternalUser" runat="server" class="form-control drpControl">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnsubmit_Click" />
                                                            <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancel_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 230px;">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdInternalUser" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdAgendaTopic_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="User_ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="User_ID" runat="server" Text='<%# Bind("User_ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Emp Namee" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Emp_Name" runat="server" Text='<%# Bind("Emp_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%-- <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="EditVisit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="DeleteVisit" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 230px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">
                                            Select External Attendies
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select Site:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="drpExternalSite" runat="server" class="form-control drpControl"
                                                                AutoPostBack="True" OnSelectedIndexChanged="drpExternalSite_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select User:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="drpExternalUser" runat="server" class="form-control drpControl">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            &nbsp;
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:Button ID="btnExternalSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnExternalSubmit_Click" />
                                                            <asp:Button ID="btnExternalCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnExternalCancel_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 230px;">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">
                                            Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdExternalUser" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdExternalUser_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INVID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="User_ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="User_ID" runat="server" Text='<%# Bind("User_ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Emp Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Emp_Name" runat="server" Text='<%# Bind("Emp_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%--      <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="EditVisit" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                CommandName="DeleteVisit" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            <div class="txt_center">
                <asp:Button ID="btnSubmitAgenda" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btnSubmitAgenda_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitAgenda" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
