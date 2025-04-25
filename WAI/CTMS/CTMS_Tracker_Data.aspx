<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CTMS_Tracker_Data.aspx.cs" Inherits="CTMS.CTMS_Tracker_Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Site Tracker</h3>
        </div>
        <!-- /.box-header -->
        <!-- text input -->
        <div class="box-body">
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <table>
                <tr>
                    <td class="label">
                        Select Project:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Lbl_Dept" runat="server" Text="*"></asp:Label>
                    </td>
                    <td class="Control">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="Drp_Project" class="form-control " runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="Drp_Project_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Select Tracker:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drpTracker" runat="server" class="form-control required" 
                                    AutoPostBack="True" onselectedindexchanged="drpTracker_SelectedIndexChanged"
                                  >
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="label">
                        Select Site Id:
                    </td>
                    <td class="requiredSign">
                        <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="drp_INVID" runat="server" class="form-control required" AutoPostBack="True"
                                    OnSelectedIndexChanged="drp_INVID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                  <tr>
                    <td class="label">
                      
                    </td>
                    <td class="requiredSign">
                      
                    </td>
                    <td class="Control">
                     
                    </td>
                    <td class="label">
                     
                    </td>
                    <td class="requiredSign">
                        
                    </td>
                    <td>
                      
                    </td>                  
                </tr>         
            </table>
        </div>
    </div>
    <br />
    <div id="popup_ManualQuery" title="Raise Checklist Comments" class="disp-none">
        <div class="disp-none">
            <div class="formControl">
                <asp:Label ID="Label10" runat="server" CssClass="wrapperLable" Text="ProjectID"></asp:Label>
                <asp:TextBox ID="txtProjectID" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="formControl">
                <asp:Label ID="Label8" runat="server" CssClass="wrapperLable" Text="Visit Type"></asp:Label>
                <asp:TextBox ID="txtSectionID" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="formControl">
                <asp:Label ID="Label9" runat="server" CssClass="wrapperLable" Text="Sub Section "></asp:Label>
                <asp:TextBox ID="txtSubsectionID" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="formControl">
                <asp:Label ID="Label11" runat="server" CssClass="wrapperLable" Text="ChecklistID"></asp:Label>
                <asp:TextBox ID="txtChecklistID" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
            </div>
        </div>
        <div id="grdoldManualQuery">
        </div>
        <div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label17" runat="server" CssClass="wrapperLable label" Text="Visit Type"></asp:Label>
                <asp:TextBox ID="txtSectionText" Width="300px" CssClass="form-control" Enabled="false"
                    runat="server"></asp:TextBox>
            </div>
            <br />
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label18" runat="server" CssClass="wrapperLable label" Text="Sub Section "></asp:Label>
                <asp:TextBox ID="txtSubSectionText" Width="300px" CssClass="form-control" Enabled="false"
                    runat="server"></asp:TextBox>
            </div>
            <br />
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label6" runat="server" CssClass="wrapperLable label" Text="Site Id"></asp:Label>
                <asp:TextBox ID="txtINVID" Width="300px" CssClass="form-control" Enabled="false"
                    runat="server"></asp:TextBox>
            </div>
            <br />
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label7" runat="server" CssClass="wrapperLable label" Text=" Visit Id"></asp:Label>
                <asp:TextBox ID="txtMVID" Width="300px" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <br />
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label22" runat="server" CssClass="wrapperLable label" Text="Query Text"></asp:Label>
                <asp:TextBox ID="txt_QueryText" CssClass="width245px form-control" TextMode="MultiLine"
                    runat="server"></asp:TextBox>
            </div>
        </div>
        <div style="margin-top: 10px">
            <asp:Button ID="btnMQSave" runat="server" Style="margin-left: 107px;" class="btn btn-primary btn-sm"  OnClientClick="AddQuery()"
                Text="Save" />
        </div>
    </div>
    <div id="popup_FieldComments" title="Comments" class="disp-none">

     
                <div id="grdQueryDetails" class="">
                </div>
                <div class="disp-none">
                    <asp:Label ID="Label12" runat="server" Text="Table Name"></asp:Label>
                    <asp:TextBox ID="txtFieldTableName" runat="server"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text="Cont Id"></asp:Label>
                    <asp:TextBox ID="txtFieldContID" runat="server"></asp:TextBox>
                    <asp:Label ID="txtFieldName" runat="server" Text="Variable Name"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:Label ID="Label21" runat="server" Text="Field Name"></asp:Label>
                    <asp:TextBox ID="txtcolumnName" runat="server"></asp:TextBox>
                </div>
                <div>
                 <asp:Label ID="Label15" runat="server" style="margin-top: 4px" CssClass="wrapperLable" Text="Add Comment"></asp:Label>
                 </div>
                <div id="Div4" style="display:inline-flex">                   
                    <asp:TextBox ID="txtFieldComments"  CssClass="form-control" Width="480px" TextMode="MultiLine" runat="server"></asp:TextBox>
                     
                </div>
                <div style="margin-top: 5px">
                   <asp:Button ID="btnSaveFieldComments" runat="server"  class="btn btn-primary btn-sm txt_center"  Style="vertical-align: bottom;"
                        Text="Save" OnClientClick="SaveQueryComments();" />
                </div>
            </div>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnAddComments" runat="server" Text="Add New Comment" Visible="false"
                OnClientClick="return AddComments()" CssClass="btn btn-info btn-sm" />
            <asp:Button ID="btnViewComments" runat="server" Text="View All Comments" Visible="false"
                OnClientClick="return ViewComments()" CssClass="btn btn-info btn-sm" />
            
            <div class="box">
                <asp:GridView ID="DSVISDAT" runat="server" AutoGenerateColumns="False" Name="DSVISDAT"
                    OnRowDataBound="DSVISDAT_RowDataBound" CssClass="table table-bordered table-striped" 
                    AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onfocus="myFocus(this)"
                                    onchange="myFunction(this)"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CheckListID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblChecklistID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="width100px disp-none"
                            ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblVARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="width100px disp-none"
                            ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RECRUITMENT" HeaderStyle-CssClass="" ItemStyle-CssClass="align-left">
                            <HeaderTemplate>
                                <asp:Label ID="lblSubSection" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="width100px disp-none"
                            ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="width100px disp-none"
                            ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="width100px align-center">
                            <ItemTemplate>
                                <asp:DropDownList ID="DRP_FIELD" runat="server" Width="100px" ToolTip="Initials"
                                    Visible="false">
                                </asp:DropDownList>
                                <asp:TextBox ID="TXT_FIELD" runat="server" ToolTip="ContID" Visible="false"></asp:TextBox>
                                <asp:CheckBox ID="CHK_FIELD" runat="server" ToolTip="Vasectomised partner" Visible="false"
                                    CssClass="checkbox"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                          
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSubSection" runat="server" CommandArgument='<%# Bind("VARIABLENAME") %>'
                                    Visible="false" class="fa fa-edit" OnClientClick="return AddSubSectionRecords(this)"
                                    CommandName="EditRow" ToolTip="Edit"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                    </Columns>
                </asp:GridView>

                   
                
    
            </div>
            
          
            <asp:Button ID="bntSave" runat="server" Text="Save" OnClick="bntSaveComplete_Click"
                CssClass="btn btn-primary btn-sm cls-btnSave" />


                <br />
                <div style="margin-top:10px">
                <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="true" 
                 CssClass="table table-bordered table-striped" stylr="margin-top:10px"
                    AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" onrowdatabound="grdData_RowDataBound" 
                    >
                    <Columns>
                      
                     
                    </Columns>
                </asp:GridView>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="bntSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
