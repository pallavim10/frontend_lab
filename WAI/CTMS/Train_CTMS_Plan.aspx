<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Train_CTMS_Plan.aspx.cs" Inherits="CTMS.Train_CTMS_Plan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function Items(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('input').val();
            var test = "Train_DDL_Items.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=275,width=450";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }
    </script>
    <script type="text/javascript">
        $('#MainContent_chkDateType').change(function () {
            if ($(this).is(":checked")) {
                $('#MainContent_txtANS').addClass('txtDate');
            } else {
                $('#MainContent_txtANS').removeClass('txtDate');
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Manage Training Plan
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-5">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Manage Topics
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Section:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSec"
                                                    ValidationGroup="Sec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlSec" runat="server" AutoPostBack="true"
                                                    class="form-control drpControl" ValidationGroup="Sec" OnSelectedIndexChanged="ddlSec_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvNewActs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Section">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubSectionID" runat="server" Visible="false" Text='<%# Bind("SubSection_ID") %>'></asp:Label>
                                                                <asp:Label ID="lblSubSection" runat="server" Text='<%# Bind("SubSection") %>'></asp:Label>
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
                    <div class="col-md-1">
                        <div class="box-body">
                            <div style="min-height: 300px;">
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnAddToPlan" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                        ValidationGroup="Plan" OnClick="lbtnAddToPlan_Click"></asp:LinkButton>
                                </div>
                                <div class="row txtCenter">
                                    &nbsp;
                                </div>
                                <div class="row txtCenter">
                                    <asp:LinkButton ID="lbtnRemoveFromPlan" ForeColor="White" Text="Remove" runat="server"
                                        ValidationGroup="Plan" CssClass="btn btn-primary btn-sm" OnClick="lbtnRemoveFromPlan_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Manage Plan
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Plan Name:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox Style="width: 200px;" ID="txtGrp" ValidationGroup="AddGrp" runat="server"
                                                    CssClass="form-control required"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnAddPlan" Text="Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave "
                                                    ValidationGroup="AddPlan" OnClick="btnAddPlan_Click" />
                                                <asp:Button ID="btnUpdatePlan" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    ValidationGroup="AddPlan" OnClick="btnUpdatePlan_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select Plan:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlPlan" runat="server" AutoPostBack="true"
                                                    ValidationGroup="Plan" class="form-control drpControl" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_Addto" runat="server" ControlToValidate="ddlPlan"
                                                    ValidationGroup="Plan" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:LinkButton ID="lbtnUpdatePlan" ValidationGroup="Plan" runat="server" ToolTip="Edit"
                                                    OnClick="lbtnUpdatePlan_Click"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnDeletePlan" ValidationGroup="Plan" runat="server" ToolTip="Delete"
                                                    OnClick="lbtnDeletePlan_Click"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvAddedActs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Section">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubSectionID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblSubSection" runat="server" Text='<%# Bind("SubSection") %>'></asp:Label>
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
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 300px;">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Add Questions
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="form-group">
                                        <table>
                                            <tr>
                                                <td class="label" style="width: 150px;">
                                                    Select Plan:
                                                </td>
                                                <td class="requiredSign">
                                                    <asp:Label ID="Label4" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfv_QuePlan" InitialValue="0" ControlToValidate="ddlQuePlan"
                                                        ValidationGroup="addMore" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Control">
                                                    <asp:DropDownList ID="ddlQuePlan" runat="server" AutoPostBack="true" class="form-control drpControl"
                                                        ValidationGroup="addMore" Width="250px" OnSelectedIndexChanged="ddlQuePlan_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="width: 150px;">
                                                    Select Sub-Section:
                                                </td>
                                                <td class="requiredSign">
                                                    <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" ControlToValidate="ddlSubSec"
                                                        ValidationGroup="addMore" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Control">
                                                    <asp:DropDownList ID="ddlSubSec" runat="server" AutoPostBack="true" class="form-control drpControl"
                                                        ValidationGroup="addMore" Width="250px" OnSelectedIndexChanged="ddlSubSec_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="width: 150px;">
                                                    Enter Question:
                                                </td>
                                                <td class="requiredSign">
                                                    <asp:Label ID="Label6" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtQue"
                                                        ValidationGroup="addMore" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Control">
                                                    <asp:TextBox TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"
                                                        ValidationGroup="addMore" ID="txtQue" Width="250px"></asp:TextBox>
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="width: 150px;">
                                                    Select Control Type:
                                                </td>
                                                <td class="requiredSign">
                                                    <asp:Label ID="Label7" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" ControlToValidate="ddlChkList"
                                                        ValidationGroup="addMore" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Control">
                                                    <asp:DropDownList ID="ddlChkList" runat="server" Width="250px" class="form-control drpControl"
                                                        AutoPostBack="true" ValidationGroup="addMore" OnSelectedIndexChanged="ddlChkList_SelectedIndexChanged">
                                                        <asp:ListItem Text="-Select-" Value="0">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="CHECKBOX" Value="CHECKBOX">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="DROPDOWN" Value="DROPDOWN">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="TEXTBOX" Value="TEXTBOX">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="RADIOBUTTON" Value="RADIOBUTTON">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false" id="tr_datetype">
                                                <td class="label" style="width: 150px;">
                                                    Is this Date Control?
                                                </td>
                                                <td class="requiredSign">
                                                </td>
                                                <td class="Control">
                                                    <asp:CheckBox ToolTip="Select if 'YES'" runat="server" ID="chkDateType" />
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="width: 150px;">
                                                    Enter Answer:
                                                </td>
                                                <td class="requiredSign">
                                                    <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtANS"
                                                        ValidationGroup="addMore" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Control">
                                                    <asp:TextBox runat="server" CssClass="form-control" ValidationGroup="addMore" ID="txtANS"
                                                        Width="250px"></asp:TextBox>
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="width: 150px;">
                                                    Enter Sequence Number:
                                                </td>
                                                <td class="requiredSign">
                                                    <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtChkSeq"
                                                        ValidationGroup="addMore" runat="server"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Control">
                                                    <asp:TextBox runat="server" CssClass="form-control" ValidationGroup="addMore" ID="txtChkSeq"
                                                        MaxLength="3" Width="45px"></asp:TextBox>
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="text-align: center;">
                                                <td class="label">
                                                </td>
                                                <td class="requiredSign">
                                                </td>
                                                <td class="Control">
                                                    <asp:Button runat="server" ID="btnSubmitQue" CssClass="btn btn-primary btn-sm" ValidationGroup="addMore"
                                                        Text="Submit" OnClick="btnSubmitQue_Click" />
                                                    <asp:Button runat="server" ID="btnUpdateQue" CssClass="btn btn-primary btn-sm" ValidationGroup="addMore"
                                                        Text="Update" OnClick="btnUpdateQue_Click" />
                                                    <asp:Button runat="server" ID="btnCancelQue" CssClass="btn btn-primary btn-sm" Text="Cancel"
                                                        OnClick="btnCancelQue_Click" />
                                                </td>
                                                <td class="style10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">
                                    Records
                                </h4>
                            </div>
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div>
                                        <div class="rows">
                                            <div style="width: 100%; height: 264px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="gvQues" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvQues_RowCommand"
                                                        OnRowDataBound="gvQues_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hfv" runat="server" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChkSEQ" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Question" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsubcategory" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdateQues" runat="server" CommandArgument='<%# Bind("id") %>'
                                                                        CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtndeleteQues" runat="server" CommandArgument='<%# Bind("id") %>'
                                                                        CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnItems" Visible="false" runat="server" CommandArgument='<%# Bind("id") %>'
                                                                        CommandName="Items" OnClientClick="return Items(this);" ToolTip="Dropdownlist Items"><i class="fa fa-list"></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
