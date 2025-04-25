<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="USERLOGIN_AUTHENTICATION.aspx.cs" Inherits="CTMS.USERLOGIN_AUTHENTICATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>

    <script type="text/javascript">
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

        function pageLoad() {
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });
        }  
     
    </script>
    <style type="text/css">
        .label
        {
            margin-left: 0;
        }
    </style>
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <div>
            <h3 class="box-title">
                Assign User Login Date</h3></div>

             <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbAssignLogDtExport" OnClick="lbAssignLogDtExport_Click" ToolTip="Export to Excel Assign User Login Date"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>

        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select User: &nbsp;
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="Drp_User" class="form-control drpControl required" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="Drp_User_SelectedIndexChanged" Style="margin-bottom: 1px">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2" id="proj" runat="server">
                                    Select Project: &nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4" id="projname" runat="server">
                                    <asp:DropDownList ID="Drp_Project_Name" runat="server" AutoPostBack="True" class="form-control required drpControl"
                                        OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Start Date: &nbsp;
                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Start Time: &nbsp;
                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtstarttime" runat="server" CssClass="form-control txtTime required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    End Date: &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    End Time: &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtendtime" runat="server" CssClass="form-control txtTime"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        Text="Submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        Text="Update" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel"
                                        OnClick="btncancel_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="box">
                            <div class="box-body">
                                   <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdUserAssignData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                                    OnRowCommand="grdUserAssignData_RowCommand" AllowSorting="True" OnPreRender="grdUserAssignData_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditData" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="txtCenter disp-none" ItemStyle-CssClass="txtCenter disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="USERID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="USER NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusername" runat="server" Text='<%# Eval("User_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROJNAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPROJNAME" runat="server" Text='<%# Eval("PROJNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STARTDATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstartdate" runat="server" Text='<%# Eval("Startdatetime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ENDDATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblenddate" runat="server" Text='<%# Eval("EndDatetime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div></div></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
