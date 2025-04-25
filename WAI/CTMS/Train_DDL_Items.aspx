<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Train_DDL_Items.aspx.cs"
    Inherits="CTMS.Train_DDL_Items" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<title>Diagonsearch</title>
<script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
<link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
<link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
<link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<link rel="icon" href="img/favicon.ico" type="image/x-icon">
<link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
<%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
<script src="Scripts/bootstrap.js" type="text/javascript"></script>
<script src="Scripts/ClientValidation.js" type="text/javascript"></script>
<!-- for pikaday datepicker//-->
<link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
<script src="Scripts/moment.js" type="text/javascript"></script>
<script src="Scripts/pikaday.js" type="text/javascript"></script>
<script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
<!-- for pikaday datepicker//-->
<link href="Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
<link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Select2.js" type="text/javascript"></script>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Add Listitems</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width120px label">
                        Item Name</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:TextBox Style="width: 200px;" ID="txtItem" runat="server" CssClass="form-control required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txt" runat="server" ControlToValidate="txtItem" ValidationGroup="add"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group txt_center">
                    <asp:Button ID="btnAddPlan" Text="Submit" ValidationGroup="add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave "
                        OnClick="btnAddPlan_Click" />
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" 
                    onrowcommand="gvItems_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="Items">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtndeleteRole" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
