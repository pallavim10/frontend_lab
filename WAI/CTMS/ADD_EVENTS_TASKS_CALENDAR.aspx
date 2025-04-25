<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ADD_EVENTS_TASKS_CALENDAR.aspx.cs" Inherits="CTMS.ADD_EVENTS_TASKS_CALENDAR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function pageload() {
            $('.txtTime').inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );
        }

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
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hfOLDVARIABLENAME" />
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 264px;">
                            <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                <h4 class="box-title" align="left">
                                    Add Events/Tasks
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    <label>
                                                        Enter Event/Task Name :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Style="width: 200px;" ID="txttaskname" runat="server" CssClass="form-control required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="Div1" class="row" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    <label>
                                                        Event Start Date :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Style="width: 200px;" ID="txtstartdate" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="Div3" class="row" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    <label>
                                                        Event Start Time :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Style="width: 200px;" ID="txtstarttime" runat="server" CssClass="form-control txtTime required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="Div2" class="row" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    <label>
                                                        Event End Date :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Style="width: 200px;" ID="txtenddate" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="Div4" class="row" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    <label>
                                                        Event End Time :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Style="width: 200px;" ID="txtendtime" runat="server" CssClass="form-control txtTime"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btncancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 284px;">
                            <div class="box-header with-border">
                                <h4 class="box-title" align="left">
                                    Records
                                </h4>
                                <div class="pull-right" style="margin: 10px;">
                                    <a id="A1" href="HomePage.aspx" runat="server">Back to HomePage</a>
                                </div>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 284px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdEvnetData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdEvnetData_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKNAME" runat="server" Text='<%# Bind("TASKNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task Start Datetime" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKSTARTDATETIME" runat="server" Text='<%# Bind("TASKSTARTDATETIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task End Datetime" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKENDDATETIME" runat="server" Text='<%# Bind("TASKENDDATETIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task Start Date" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKSTARTDATE" runat="server" Text='<%# Bind("TASKSTARTDATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task End Date" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKENDDATE" runat="server" Text='<%# Bind("TASKENDDATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task Start Time" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKSTARTTIME" runat="server" Text='<%# Bind("TASKSTARTTIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Task End Time" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="TASKENDTIME" runat="server" Text='<%# Bind("TASKENDTIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="EditField" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp&nbsp&nbsp
                                                                    <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DeleteField" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
</asp:Content>
