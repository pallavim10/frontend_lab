<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Budget_MngProjectBudget.aspx.cs" Inherits="CTMS.Budget_MngProjectBudget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Manage Project Budget
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Select Department:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:DropDownList ID="ddl_Dept" runat="server" Width="250px" class="form-control drpControl required"
                                        ValidationGroup="Mng" AutoPostBack="true" 
                                        onselectedindexchanged="ddl_Dept_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Select Task:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="lbl_Section" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:DropDownList ID="ddl_Tasks" runat="server" Width="250px" class="form-control drpControl required"
                                        ValidationGroup="Mng" AutoPostBack="true" OnSelectedIndexChanged="ddl_Tasks_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Select Sub-Task:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:DropDownList ID="ddl_SubTask" runat="server" Width="250px" class="form-control drpControl required"
                                        ValidationGroup="Mng" AutoPostBack="true" OnSelectedIndexChanged="ddl_SubTask_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Select Role:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:DropDownList ID="ddlRole" runat="server" Width="250px" class="form-control drpControl required"
                                        ValidationGroup="Mng" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlRole" ValidationGroup="Mng" ID="RFVddlRole"
                                        runat="server" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Rate:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="Label5" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtRate" CssClass="form-control required"
                                        ValidationGroup="Mng"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Enter Hours:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtHrs" CssClass="form-control required"
                                        ValidationGroup="Mng" OnTextChanged="txtHrs_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    Amount:
                                </div>
                                <div class="col-md-3 requiredSign">
                                    <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:TextBox runat="server" ID="txtAmt" CssClass="form-control required" ReadOnly="true"
                                        ValidationGroup="Mng"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3 label" style="width: 15%;">
                                    &nbsp;
                                </div>
                                <div class="col-md-3 requiredSign">
                                    &nbsp;
                                </div>
                                <div class="col-md-3 Control">
                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        ForeColor="White" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        ForeColor="White" OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                        ForeColor="White" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvTasks" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped Datatable1" OnPreRender="gvTasks_PreRender"
                    OnRowCommand="gvTasks_RowCommand">
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("Task_Id") %>'></asp:Label>
                                <asp:Label ID="lbl_SubTaskId" runat="server" Text='<%# Bind("Sub_Task_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Roles" ItemStyle-Width="60%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Role" Width="100%" ToolTip='<%# Bind("Role") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate (per Hour)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Rate" Width="100%" ToolTip='<%# Bind("Rate") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("Rate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Hours" Width="100%" ToolTip='<%# Bind("Hrs") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("Hrs") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Amt" Width="100%" ToolTip='<%# Bind("Amt") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("Amt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Options" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnupdateResource" runat="server" CommandArgument='<%# Bind("Role_ID") %>'
                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtndeleteResource" runat="server" CommandArgument='<%# Bind("Role_ID") %>'
                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
