<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Add_User_Profile.aspx.cs" Inherits="PPT.Add_User_Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'ion-minus-round';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'ion-plus-circled';
            }
        }


    </script>
    <script src="Scripts/Select2.js"></script>

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

            $(".Datatable1").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }
    </script>


    <style type="text/css">
        .label {
            margin-left: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header">
                    <div>
                        <h3 class="box-title">Add User Profile</h3>
                    </div>
                    <div id="Div3" class="dropdown" runat="server" style="display: inline-flex">
                        <h3 class="box-title">
                            <asp:LinkButton runat="server" ID="lbUserDetailsExport" OnClick="lbUserDetailsExport_Click" ToolTip="Export to Excel"
                                Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
                        </h3>
                    </div>

                </div>
                <div class="box-header">
                    <div class="lblError">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select User Group : &nbsp;
                                <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <%--<asp:DropDownList ID="Drp_User_Group" runat="server" AutoPostBack="true" class="form-control drpControl required width200px"
                                    OnSelectedIndexChanged="Drp_User_Group_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="lstUser_Group" AutoPostBack="true" runat="server" CssClass="width300px select"
                                    SelectionMode="Multiple" Width="668px" OnSelectedIndexChanged="lstUser_Group_SelectedIndexChanged"></asp:ListBox>
                            </div>
                            <div class="label col-md-2" id="proj" runat="server" visible="false">
                                Select Project: &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4" id="projname" runat="server" visible="false">
                                <asp:DropDownList ID="Drp_Project_Name" runat="server" AutoPostBack="True" class="form-control required drpControl width200px"
                                    OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divProject" runat="server" style="margin-top: 10px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Project : &nbsp;
                                <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:ListBox ID="lstProjects" AutoPostBack="true" runat="server" CssClass="width300px select"
                                    SelectionMode="Multiple" Width="668px" OnSelectedIndexChanged="lstProjects_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Country: &nbsp;
                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drpCountry" runat="server" class="form-control drpControl required width200px">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select User Type: &nbsp;
                                <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="drpUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUserType_SelectedIndexChanged"
                                    class="form-control drpControl required width200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2" id="divinternalemp" runat="server">
                                Select Internal Employee :&nbsp;
                                <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3" id="divddlemp" runat="server">
                                <asp:DropDownList ID="drpEmployee" runat="server" AutoPostBack="true" class="form-control drpControl width200px"
                                    OnSelectedIndexChanged="drpEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2" id="div1" runat="server" visible="false">
                                Investigator Team Members :&nbsp;
                            </div>
                            <div class="col-md-3" id="div2" runat="server" visible="false">
                                <asp:DropDownList ID="ddlInvestigatorTeamMem" runat="server" AutoPostBack="true"
                                    class="form-control drpControl width200px" OnSelectedIndexChanged="ddlInvestigatorTeamMem_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <%-- <div class="label col-md-2" id="divexternaluser" runat="server" visible="false">
                                            Enter External User Name :&nbsp;
                                            <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3" id="divtxtextrernaluser" runat="server" visible="false">
                                            <asp:TextBox ID="txt_User_Name" runat="server" class="form-control drpControl required"></asp:TextBox>
                                        </div>--%>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                User Display Name :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txt_User_Dis_Name" ReadOnly="true" runat="server" class="form-control drpControl required width200px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                User Email ID :&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_EmailID" runat="server" ReadOnly="true" ValidationGroup="addUser" class="form-control drpControl width200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Req_EmailID" runat="server" ControlToValidate="txt_EmailID"
                                    ErrorMessage="Required" Font-Size="X-Small" ForeColor="Red" ValidationGroup="addUser"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="Reg_Exp_Email_ID" runat="server" ControlToValidate="txt_EmailID"
                                    ErrorMessage="Invalid Email ID" Font-Size="X-Small" ForeColor="#CC3300" Style="font-weight: 700"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="addUser"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                TimeZone :&nbsp;
                                <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlTimeZone" runat="server" class="form-control drpControl width200px required">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Unblind :&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" ID="ddlUnblind" CssClass="form-control txt_center width200px required">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                    <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Medical Authority :&nbsp;
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkForm" />
                                &nbsp;&nbsp;
                                <label>
                                    Form
                                </label>
                            </div>
                            <div class="label col-md-2">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkfield" />
                                &nbsp;&nbsp;
                                <label>
                                    Field
                                </label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                SignOff Authority :&nbsp;
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eSource" />
                                &nbsp;&nbsp;
                                <label>
                                    eSource
                                </label>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_Safety" />
                                &nbsp;&nbsp;
                                <label>
                                    Pharmacovigilance
                                </label>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eCRF" />
                                &nbsp;&nbsp;
                                <label>
                                    eCRF
                                </label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                eSource ReadOnly :&nbsp;
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eSourceReadOnly" />
                                &nbsp;&nbsp;                                
                            </div>                           
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="Btn_Add" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="Btn_Add_Click" ValidationGroup="addUser" Text="Submit" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divsitetable" runat="server" visible="false">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Site ID :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:ListBox ID="lstINVID" AutoPostBack="true" runat="server" CssClass="width300px select"
                                    SelectionMode="Multiple" Width="668px"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="rows">
                    <div style="width: 100%; overflow: auto;">
                        <div>
                            <asp:GridView ID="grdUserDetails" runat="server" AllowSorting="True" AutoGenerateColumns="true"
                                CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbUserDetailsExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
