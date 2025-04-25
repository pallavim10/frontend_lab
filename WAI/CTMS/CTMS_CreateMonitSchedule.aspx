<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_CreateMonitSchedule.aspx.cs" Inherits="CTMS.CTMS_CreateMonitSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                New Monitoring Schedule
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row disp-none">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Project :&nbsp;
                            <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="Drp_Project" class="form-control drpControl required" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Visit Type :&nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpVisitType" runat="server" class="form-control drpControl required"
                                AutoPostBack="True" OnSelectedIndexChanged="drpVisitType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="label col-md-2">
                            Select Site: &nbsp;
                            <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drp_InvID" runat="server" class="form-control drpControl required1"
                                AutoPostBack="True" OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            First Visit date:&nbsp;
                            <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtStartdate" class="form-control txtDate required" runat="server"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            UnScheduled Visit &nbsp;
                            <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chkUnSchedualVisit" runat="server" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="margin-left: 19%;">
                        <asp:Button ID="Btn_Save" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave margin-left10"
                            Text="Save" OnClick="Btn_Save_Click" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <div class="box box-warning" runat="server" visible="false" id="divaddvisit">
        <div class="box-header">
            <h3 class="box-title">
                Add Visit Schedule</h3>
        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <table>
                <tr id="Tr1">
                    <td class="label">
                        Frequency(in Days):
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:TextBox ID="txtFrequency" class="form-control required1" runat="server" Text=""></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td class="style11">
                        <asp:Button ID="btnaddvisit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                            Text="Add" OnClick="btnaddvisit_Click" />
                    </td>
                    <td class="style8">
                        &nbsp;
                    </td>
                    <td class="style9">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:TemplateField HeaderText="Visit Type">
                    <ItemTemplate>
                        <asp:Label ID="VISIT_NAME" runat="server" Text='<%# Bind("VISIT_NAME") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Site Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Visit No" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="VISITNO" runat="server" Text='<%# Bind("VISITNO") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Visit Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="VSTDAT" runat="server" Text='<%# Bind("VSTDAT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <asp:Button ID="btnaddvisit" CommandName="AddVisit" runat="server" Text="ADD" CssClass="btn btn-primary" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnaddvisitdays" CommandName="insertnewvisit" CommandArgument='<%# Bind("VISITNO") %>' runat="server" CssClass="btn btn-link"><i class="fa fa-plus-square"></i></asp:LinkButton>
                        <asp:TextBox ID="txtvisitdays" runat="server" Visible="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
