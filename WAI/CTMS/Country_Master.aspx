<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Country_Master.aspx.cs" Inherits="CTMS.Country_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
     

            <h3 class="box-title">
                Manage Masters
            </h3>

            <div id="Div4" class="dropdown" runat="server" style="display: inline-flex"><h3 class="box-title">
                 
                                    <asp:LinkButton runat="server" ID="btnMangConExport" OnClick="btnMangConExport_Click" ToolTip="Export to Excel"
                                        Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
						
                             </h3>
                        </div>

        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                            <h4 class="box-title" align="left">
                                                Add Country
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter Country Name:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtCountry" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="reqcategory" runat="server" ControlToValidate="txtCountry"
                                                                    ValidationGroup="COUNTRY" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter Country Code:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtCountryCode" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCountryCode"
                                                                    ValidationGroup="COUNTRY" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                        <div class="col-md-4">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnsubmitCountry" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="COUNTRY" OnClick="btnsubmitCountry_Click" />
                                                            <asp:Button ID="btnupdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnupdate_Click" />
                                                            <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancel_Click" />
                                                        </div>
                                                    </div>
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
                                        
                                            <div id="Div1" class="dropdown" runat="server" style="display: inline-flex"><h4 class="box-title">
                                                                <asp:LinkButton runat="server" ID="btnCountyExport" OnClick="btnCountyExport_Click" ToolTip="Export to Excel"
                                                                    Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
									                     </h4>
                                                    </div>

                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="grdCountry" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdCountry_RowCommand"
                                                                    OnRowDataBound="grdCountry_RowDataBound">
                                                                    <Columns>
                                                                         <asp:TemplateField HeaderText=" " ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                           <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("CNTRYID") %>'
                                                                                    CommandName="EditCOUNTRY" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                             </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CNTRYID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CNTRYID" runat="server" Text='<%# Bind("CNTRYID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="COUNTRY CODE" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="COUNTRYCOD" runat="server" Text='<%# Bind("COUNTRYCOD") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="COUNTRY" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                             
                                                                                <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("CNTRYID") %>'
                                                                                    CommandName="DeleteCOUNTRY" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Add State
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="rows">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4">
                                                                    <label>
                                                                        Select Country:</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                                                        OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-1">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcountry"
                                                                        InitialValue="0" ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-4">
                                                                <label>
                                                                    Enter State Name:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtState" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="reqsubcategory" runat="server" ControlToValidate="txtState"
                                                                    ValidationGroup="subvate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12" style="margin-top: 10px;">
                                                            <div class="col-md-4">
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Button ID="btnsubState" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="subvate" OnClick="btnsubState_Click" />
                                                                <asp:Button ID="btnupdateState" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    ValidationGroup="subvate" OnClick="btnupdateState_Click" />
                                                                <asp:Button ID="btncancelState" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                    OnClick="btncancelState_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
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
                                                

                                               <div id="Div2" class="dropdown" runat="server" style="display: inline-flex"><h4 class="box-title">
                                                                <asp:LinkButton runat="server" ID="btnstateExport" OnClick="btnstateExport_Click" ToolTip="Export to Excel"
                                                                    Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
									                     </h4>
                                                    </div>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="grdState" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdState_RowCommand"
                                                                    OnRowDataBound="grdState_RowDataBound">
                                                                    <Columns>
                                                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                              <asp:LinkButton ID="lbtnupdateCity" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="EditSTATE" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
																			 </ItemTemplate>
                                                                             </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Country" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="StateName" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="StateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                   <asp:LinkButton ID="lbtndeleteState" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                    CommandName="DeleteSTATE" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="box box-primary" style="min-height: 300px;">
                                        <div class="box-header with-border" style="float: left;">
                                            <h4 class="box-title" align="left">
                                                Add City
                                            </h4>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    Select State :</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                                                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlState"
                                                                    InitialValue="0" ValidationGroup="factor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="rows">
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    Enter City:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtCity" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity"
                                                                    ValidationGroup="factor" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="btnsubmitCity" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btnsubmitCity_Click" ValidationGroup="factor" />
                                                            <asp:Button ID="btnupdateCity" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                                ValidationGroup="factor" OnClick="btnupdateCity_Click" />
                                                            <asp:Button ID="btncancelCity" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                                OnClick="btncancelCity_Click" />
                                                        </div>
                                                    </div>
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
                                          

                                               <div id="Div3" class="dropdown" runat="server" style="display: inline-flex"><h4 class="box-title">
                                              
                                                                <asp:LinkButton runat="server" ID="btnCityExport" OnClick="btnCityExport_Click" ToolTip="Export to Excel"
                                                                    Text="" CssClass="dropdown-item dropdown-toggle glyphicon glyphicon-download-alt" Style="color: darkblue;"></asp:LinkButton>
									          
                                                         </h4>
                                                    </div>
                                        </div>
                                        <div class="box-body">
                                            <div align="left" style="margin-left: 5px">
                                                <div>
                                                    <div class="rows">
                                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                                            <div>
                                                                <asp:GridView ID="grdCity" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                    Style="width: 91%; border-collapse: collapse;" OnRowCommand="grdCity_RowCommand">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnupdateCity" runat="server" CommandArgument='<%# Bind("city_id") %>'
                                                                                    CommandName="EDITCITY" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CITYID" runat="server" Text='<%# Bind("city_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Country" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="StateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="City" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="CITY" runat="server" Text='<%# Bind("city_name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtndeleteCity" runat="server" CommandArgument='<%# Bind("city_id") %>'
                                                                                    CommandName="DeleteCITY" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCountyExport" />
                <asp:PostBackTrigger ControlID="btnstateExport" />
                <asp:PostBackTrigger ControlID="btnCityExport" />

            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
