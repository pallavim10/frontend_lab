<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmEthicsCommity.aspx.cs" Inherits="CTMS.frmEthicsCommity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        caption
        {
            font-weight: bold;
        }
    </style>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script>
        function pageLoad() {
            $(".Datatable1").dataTable({ "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });

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
                        Ethics Committee Details
                    </h3></div>

                      <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="lbECdetailExport" OnClick="lbECdetailExport_Click" ToolTip="Export to Excel Ethics Committee Details"
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
                                    <label class="label " style="width: 140px;">
                                        Institute:
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:DropDownList ID="drpInstitute" runat="server" class="form-control drpControl required"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpInstitute_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        Name :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtEthicName" runat="server" class="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        Qualification :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtEthicQual" runat="server" class="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 140px;">
                                            Specification :
                                        </label>
                                        <div class="Control" style="width: 160px;">
                                            <asp:TextBox ID="txtEthicSpec" runat="server" class="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        Mobile No :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtMobileNo" runat="server" class="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 140px;">
                                            Tel No :
                                        </label>
                                        <div class="Control" style="width: 160px;">
                                            <asp:TextBox ID="txtTelNo" runat="server" class="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        Fax No :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtFaxNo" runat="server" class="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        Email Id :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtEmailId" runat="server" class="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <div class="" style="display: inline-flex">
                                        <label class="label " style="width: 140px;">
                                            Country :
                                        </label>
                                        <div class="Control" style="width: 160px;">
                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control required"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        State :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" CssClass="form-control required"
                                            OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        City :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" CssClass="form-control required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label" style="width: 140px;">
                                        Start Date :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control txtDate required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label" style="width: 140px;">
                                        End Date :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 140px;">
                                        Address :
                                    </label>
                                    <div class="Control" style="width: 160px;">
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control required" Style="width: 490px;"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 130px;">
                                    </label>
                                    <div class="Control" style="width: 160px;">
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
            <div class="box">
                 <div style="width: 100%; overflow: auto;">
                            <div>
                <asp:GridView ID="grdEthicsComm" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                    AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowCommand="grdEthicsComm_RowCommand"
                    OnRowDataBound="grdEthicsComm_RowDataBound" AllowSorting="True" OnPreRender="grdEthicsComm_PreRender">
                    <Columns>
                        
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEDIT" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="EDITETHICS"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site Name">
                            <ItemTemplate>
                                <asp:Label ID="INSTNAM" runat="server" Text='<%# Bind("INSTNAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NAME">
                            <ItemTemplate>
                                <asp:Label ID="EthicsNAM" runat="server" Text='<%# Bind("EthicsNAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QUALIFICATION"  HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="EthicsQUAL" runat="server" Text='<%# Bind("EthicsQUAL") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SPECIFICATION">
                            <ItemTemplate>
                                <asp:Label ID="EthicsSPEC" runat="server" Text='<%# Bind("EthicsSPEC") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MOBILE NO" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="MOBNO" runat="server" Text='<%# Bind("MOBNO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FAXNO" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="FAXNO" runat="server" Text='<%# Bind("FAXNO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EMAILID">
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
                        <asp:TemplateField HeaderText="ADDRESS">
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
                                <asp:LinkButton ID="lbtndeleteEmp" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
           </div>
          </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbECdetailExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
