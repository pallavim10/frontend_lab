<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Emp_Master.aspx.cs" Inherits="CTMS.Emp_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upl" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Employee Details
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

            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">
                        Personal Details
                    </h3>
                </div>

                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    First Name : &nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Last Name :&nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Personal Email Address : &nbsp;
                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtpersonalEmailid" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Mobile Phone :&nbsp;
                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtMobilePhone" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Home Phone : &nbsp;
                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtHomePhone" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Address :&nbsp;
                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control required"
                                        Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Country : &nbsp;
                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <%--<asp:TextBox runat="server" ID="txtCountry" CssClass="form-control required"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control required width200px"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    State :&nbsp;
                                    <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <%--<asp:TextBox runat="server" ID="txtState" CssClass="form-control required"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control required width200px"
                                        OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    City : &nbsp;
                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <%--<asp:TextBox runat="server" ID="txtCity" CssClass="form-control required"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" CssClass="form-control required width200px">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Zip/Postal Code :&nbsp;
                                    <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtPostal" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">
                        Company Details
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Company : &nbsp;
                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtCompany" CssClass="form-control required width200px"
                                        Width="100%"></asp:TextBox>
                                </div>
                                 <div class="label col-md-2">
                                    Company Email Address : &nbsp;
                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Job Title :&nbsp;
                                    <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtJobTitle" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Employee Code :&nbsp;
                                    <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtEmpCode" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Business Phone : &nbsp;
                                    <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtBusPhone" CssClass="form-control required width200px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Fax Number :&nbsp;
                                    <%--<asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>--%>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtFax" CssClass="form-control width200px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">
                        Other Details
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Notes : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" CssClass="form-control"
                                        Style="width: 250px;"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Attachments :&nbsp;
                                </div>
                                <div class="col-md-4">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box">
                <div class="box-body">
                    <div class="form-group">
                        <br />
                        <div class="row txt_center">
                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="lbtnSubmit_Click"></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                                OnClick="lbtnCancel_Click"></asp:LinkButton>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
