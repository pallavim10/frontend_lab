<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmINSTITUTEDetails.aspx.cs" Inherits="CTMS.frmINSTITUTEDetails" %>

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
                "bDestroy": true, stateSave: true
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
                Site Details</h3>
            </div>
                   <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbSiteExport" OnClick="lbSiteExport_Click" ToolTip="Export to Excel"
                 Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
		      </h3>
            </div>

        </div>

        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="lblError">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="row" runat="server" visible="false">
                                <div class="col-md-12">
                                    <div class="label col-md-2" style="width: 140px;">
                                        Select Project :&nbsp;
                                        <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <%--<asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required"
                                            AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
                                        <asp:ListBox ID="lstProjects" AutoPostBack="true" runat="server" CssClass="width300px select required"
                                            SelectionMode="Multiple" Width="660px" OnSelectedIndexChanged="lstProjects_SelectedIndexChanged">
                                        </asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2" style="width: 140px;">
                                        Institute Name :&nbsp;
                                        <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtInstitute" runat="server" class="form-control required  width250px"></asp:TextBox>
                                    </div>
                                    <div class="label col-md-2" style="width: 140px;">
                                        Country : &nbsp;
                                        <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <%--<asp:TextBox runat="server" ID="txtCountry" CssClass="form-control required"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control required  width250px"
                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2" style="width: 140px;">
                                        State :&nbsp;
                                        <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <%--<asp:TextBox runat="server" ID="txtState" CssClass="form-control required"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control required  width250px"
                                            OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="label col-md-2" style="width: 140px;">
                                        City : &nbsp;
                                        <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <%--<asp:TextBox runat="server" ID="txtCity" CssClass="form-control required"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" CssClass="form-control required  width250px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2" style="width: 140px;">
                                        WebSite :&nbsp;
                                        <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtwebsites" CssClass="form-control required width250px"></asp:TextBox>
                                    </div>
                                    <div class="label col-md-2" style="width: 140px;">
                                        Address :&nbsp;
                                        <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control required"
                                            Style="width: 280px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="margin-left: 15%;">
                                    <asp:Button ID="bntSave" runat="server" Text="Submit" OnClick="bntSave_Click" CssClass="btn btn-primary btn-sm cls-btnSave" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                        CssClass="btn btn-primary btn-sm" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <asp:GridView ID="grdInstitute" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                                OnRowCommand="grdInstitute_RowCommand" OnRowDataBound="grdInstitute_RowDataBound"
                                AllowSorting="True" OnPreRender="grdInstitute_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnedit" runat="server" CommandArgument='<%# Bind("INSTID") %>'
                                                CommandName="EDITINST"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="INSTID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                        ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:TextBox ID="INSTID" runat="server" Style="text-align: left" font-family="Arial"
                                                Font-Size="X-Small" Text='<%# Bind("INSTID") %>' Width="70px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Institute NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="INSTNAM" runat="server" Text='<%# Bind("INSTNAM")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbprojname" runat="server" Text='<%# Bind("PROJNAME")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Website" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="WEBSITE" runat="server" Text='<%# Bind("WEBSITE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COUNTRY" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%# Bind("StateName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CITY" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="CITY" runat="server" Text='<%# Bind("City_name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArea" runat="server" Text='<%# Bind("Area") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                    
                                            <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("INSTID") %>'
                                                CommandName="DELETEINST"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:GridView ID="InstituteMaster" Caption="Select Institute" runat="server" AutoGenerateColumns="False"
                                CssClass="table
                    table-bordered table-striped txt_center" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="INSTID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                        ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:TextBox ID="INSTID" runat="server" Style="text-align: left" font-family="Arial"
                                                Font-Size="X-Small" Text='<%# Bind("INSTID")
                    %>' Width="70px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="INST
                    NAME" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="INSTNAM" runat="server" Text='<%# Bind("INSTNAM") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CITY" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="CITY" runat="server" Text='<%# Bind("CITY") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COUNTRY" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY")
                    %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select Institute">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="bntSave" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
