<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmINVDETAILS.aspx.cs" Inherits="PPT.frmINVDETAILS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

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

            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }
    </script>
    <style type="text/css">
        .label
        {
            margin-left: 10;
        }
    </style>
    <style>
        caption
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <div>
                    <h3 class="box-title">
                        Investigator Details
                    </h3></div>

                     <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="btnINVDetailsExport" OnClick="btnINVDetailsExport_Click" ToolTip="Export to Excel"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>


                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Site Name:
                                        </label>
                                        <div class="Control">
                                            <asp:ListBox ID="lstINST" AutoPostBack="true" runat="server" CssClass="width300px select"
                                                SelectionMode="Multiple" Width="870px"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Select Project:
                                        </label>
                                        <div class="Control">
                                            <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required width200px"
                                                AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <%--<div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Site Name:
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="drpInstitute" runat="server" class="form-control drpControl required width200px"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpInstitute_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Inv ID :
                                        </label>
                                        <div class="Control">
                                            <asp:TextBox ID="txtInvId" runat="server" class="form-control width200px required txt_center"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Name :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtInvName" runat="server" class="form-control width200px required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Qualification :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtInvQual" runat="server" class="form-control width200px required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Speciality :
                                        </label>
                                        <div class="Control">
                                            <asp:TextBox ID="txtInvSpec" runat="server" class="form-control width200px required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Mobile No :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtMobileNo" runat="server" class="form-control width200px required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Tel No :
                                        </label>
                                        <div class="Control">
                                            <asp:TextBox ID="txtTelNo" runat="server" class="form-control width200px required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Fax No :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtFaxNo" runat="server" class="form-control width200px required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Email Id :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtEmailId" runat="server" class="form-control width200px required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Country :
                                        </label>
                                        <div class="Control">
                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control required width200px"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        State :
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control required width200px"
                                            OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        City :
                                    </label>
                                    <div class="Control">
                                        <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" CssClass="form-control required width200px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 95px;">
                                            Contact Time :
                                        </label>
                                        <div class="Control">
                                            <asp:TextBox ID="txtCont" runat="server" class="form-control width200px required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label" style="width: 330px;">
                                        Start Date (Since, How long Investigator known to DLS) :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control txtDate required width300px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label" style="width: 95px;">
                                        End Date :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control txtDate width200px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 95px;">
                                        Personal Address :
                                    </label>
                                    <div class="Control">
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control required" Style="width: 870px;"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 88px;">
                                    </label>
                                    <div class="Control">
                                        <asp:Button ID="bntSave" runat="server" Text="Submit" OnClick="bntSave_Click" CssClass="btn btn-primary btn-sm cls-btnSave margin-left10" />
                                        <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave margin-left10"
                                            OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary margin-left10"
                                            OnClick="btncancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <%-- <div class="box">--%>
             <div class="box">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                <asp:GridView ID="grdInvAdded" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                    OnRowCommand="grdInvAdded_RowCommand" AllowSorting="True" OnPreRender="grdInvAdded_PreRender" OnRowDataBound="grdInvAdded_RowDataBound">
                    <Columns>
                        
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEDIT" runat="server" CommandArgument='<%# Bind("INVCOD") %>'
                                    CommandName="EDITINV"><i class="fa fa-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="INV Code" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="INVCOD" runat="server" Text='<%# Bind("INVCOD") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="INV ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="txtCenter">
                            <ItemTemplate>
                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="NAME">
                            <ItemTemplate>
                                <asp:Label ID="INVNAM" runat="server" Text='<%# Bind("INVNAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site Name">
                            <ItemTemplate>
                                <asp:Label ID="INSTNAM" runat="server" Text='<%# Bind("INSTNAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QUALIFICATION" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="INVQUAL" runat="server" Text='<%# Bind("INVQUAL") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="INVSPEC" runat="server" Text='<%# Bind("INVSPEC") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MOBILE NO" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="MOBNO" runat="server" Text='<%# Bind("MOBNO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FAXNO" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="FAXNO" runat="server" Text='<%# Bind("FAXNO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EMAILID" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="EMAILID" runat="server" Text='<%# Bind("EMAILID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COUNTRY" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="CNTRYID" runat="server" Text='<%# Bind("CNTRYID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="STATE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="State" runat="server" Text='<%# Bind("State") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CITY" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="City" runat="server" Text='<%# Bind("City") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COUNTRY" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="STATE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="StateName" runat="server" Text='<%# Bind("StateName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CITY" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="city_name" runat="server" Text='<%# Bind("city_name") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ADDRESS" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="address" runat="server" Text='<%# Bind("address") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="START DATE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="STARTDAT" runat="server" Text='<%# Bind("STARTDAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="END DATE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="ENDDATE" runat="server" Text='<%# Bind("ENDDATE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                     <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("INVCOD") %>'
                                                CommandName="Delete1"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div></div></div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnINVDetailsExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
