<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmINVASSIGNPROJECT.aspx.cs" Inherits="CTMS.frmINVASSIGNPROJECT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                        Assign Project Investigator
                    </h3>
                        </div>

                    
                     <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
               <asp:LinkButton runat="server" ID="btnAssignINVExport" OnClick="btnAssignINVExport_Click" ToolTip="Export to Excel Assign Project Investigator"
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Select Project : &nbsp;
                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required width200px"
                                                AutoPostBack="True" OnSelectedIndexChanged="Drp_Project_Name_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            Site Name : &nbsp;
                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="drpInstitute" runat="server" class="form-control drpControl required width200px"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpInstitute_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Investigator Name : &nbsp;
                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlINVNAME" runat="server" class="form-control drpControl required width200px">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtINVNAME" runat="server" CssClass="form-control required width200px"
                                                Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Investigator ID : &nbsp;
                                            <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtInvId" runat="server" class="form-control width200px required"></asp:TextBox>
                                            <%--  <asp:TextBox ID="txtInvId" runat="server" class="form-control width200px required txt_center"></asp:TextBox> --%>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Start Date :&nbsp;
                                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control txtDate required width200px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            End Date :&nbsp;
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control txtDate width200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            IP Supply Delivery Address :
                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtAddress" runat="server" class="form-control required" Style="width: 680px;"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="" style="display: inline-flex">
                                    <label class="label " style="width: 180px;">
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
            <div class="box">
                   <div style="width: 100%; overflow: auto;">
                            <div>
                <asp:GridView ID="grdInvAdded" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                    AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowCommand="grdInvAdded_RowCommand"
                    AllowSorting="True" OnPreRender="grdInvAdded_PreRender">
                    <Columns>
                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="width30px txt_center"
                            ItemStyle-CssClass="width30px txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEDIT" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="EDITINV"><i class="fa fa-edit"></i></asp:LinkButton>
                       
                                </ItemTemplate>
                             </asp:TemplateField>
                        <asp:TemplateField HeaderText="INV Code" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="INVCOD" runat="server" Text='<%# Bind("INVCOD") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROJECT NAME" HeaderStyle-CssClass="width120px" ItemStyle-CssClass="width120px">
                            <ItemTemplate>
                                <asp:Label ID="PROJNAME" runat="server" Text='<%# Bind("PROJNAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site Name" HeaderStyle-CssClass="width200px" ItemStyle-CssClass="width200px">
                            <ItemTemplate>
                                <asp:Label ID="INSTNAM" runat="server" Text='<%# Bind("INSTNAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="INV ID" HeaderStyle-CssClass="width40px" ItemStyle-CssClass="width40px">
                            <ItemTemplate>
                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="INV NAME">
                            <ItemTemplate>
                                <asp:Label ID="INVNAM" runat="server" Text='<%# Bind("INVNAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="INV PROJECT ADDRESS" HeaderStyle-CssClass="width200px"
                            ItemStyle-CssClass="width200px">
                            <ItemTemplate>
                                <asp:Label ID="INVPROJECTADDRESS" runat="server" Text='<%# Bind("INVPROJECTADDRESS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="START DATE" HeaderStyle-CssClass="width120px" ItemStyle-CssClass="width120px">
                            <ItemTemplate>
                                <asp:Label ID="STARTDAT" runat="server" Text='<%# Bind("STARTDAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="END DATE" HeaderStyle-CssClass="width120px" ItemStyle-CssClass="width120px">
                            <ItemTemplate>
                                <asp:Label ID="ENDDATE" runat="server" Text='<%# Bind("ENDDATE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="width30px txt_center"
                            ItemStyle-CssClass="width30px txt_center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="DELETEINV"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div> </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAssignINVExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
