<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_RULES_LISTS.aspx.cs" Inherits="CTMS.DM_RULES_LISTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable1").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });
        }

       function Check_All_Activate(element) {
            // Initialize the flag to track if any checkbox is checked
            let isAnyChecked = false;

            // Iterate through all checkboxes with id containing 'Chk_ActivateDeactive'
            $('input[type=checkbox][id*=Chk_ActivateDeactive]').each(function () {
                // Toggle the checkbox state based on the master checkbox
                if ($(element).prop('checked') === true) {
                    $(this).prop('checked', true);
                } else {
                    $(this).prop('checked', false);
                }

                // Update the flag if any checkbox is checked
                if ($(this).prop('checked')) {
                    isAnyChecked = true;
                }
            });

            // Check the final state after toggling
            if (!isAnyChecked) {
                console.log("No checkboxes are checked.");
                // Optional: Show an alert or update a message on the page
                // alert("Please select at least one checkbox.");
            } else {
                console.log("At least one checkbox is checked.");
            }
        }

        //function Check_All_Activate(element) {

        //    $('input[type=checkbox][id*=Chek_Activate]').each(function () {
        //        // >>this<< refers to specific checkbox

        //        if ($(element).prop('checked') == true) {
        //            $(this).prop('checked', true);
        //        }
        //        else {
        //            $(this).prop('checked', false);
        //        }

        //    });
        //}

        function Check_All_Deactivate(element) {

            $('input[type=checkbox][id*=Chek_Deactivate]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-info">
                <div class="box-header" runat="server" id="expandHeader">
                    <h3 class="box-title">Manage Rules</h3>
                </div>
                <asp:HiddenField ID="hdnRuleID" runat="server" />
                <asp:HiddenField ID="hdnConcatRuleID" runat="server" />
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label3" class="label" runat="server" Text="Select Visit:"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlVisit" class="form-control width300px required" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlVisit_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label1" class="label" runat="server" Text="Select Module:"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlModule" class="form-control width300px required" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label2" class="label" runat="server" Text="Select Field:"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlField" class="form-control width300px required" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlField_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <asp:Button ID="btnAddNewRule" runat="server" Text="Add New Rule" OnClick="btnAddNewRule_Click"
                                        CssClass="btn btn-primary btn-sm cls-btnSave" />
                                    <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex; margin-left: 5%;">
                                        <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                            style="color: #333333" title="Export"></a>
                                        <ul class="dropdown-menu dropdown-menu-sm">
                                            <li>
                                                <asp:LinkButton runat="server" ID="lbtnExportFunc" OnClick="lbtnExportFunc_Click" ToolTip="Export Rule Functions"
                                                    Text="Export Rule Functions" CssClass="dropdown-item" ForeColor="#8c0854" Font-Bold="true">
                                                </asp:LinkButton></li>
                                            <hr style="margin: 5px;" />
                                            <li>
                                                <asp:LinkButton runat="server" ID="lbtnExportRules" OnClick="lbtnExportRules_Click" ToolTip="Export Rules"
                                                    Text="Export Rules" CssClass="dropdown-item" ForeColor="#8c0854" Font-Bold="true">
                                                </asp:LinkButton></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                <div class="box-body expandBody fixTableHead" runat="server" id="divGV">
                    <asp:GridView ID="gvRules" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable1"
                        OnPreRender="grdUserDetails_PreRender" OnRowCommand="gvRules_RowCommand" OnRowDataBound="gvRules_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditRule"
                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Copy Rule" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCopy" CommandArgument='<%# Bind("ID") %>' CommandName="CopyRule"
                                        runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to copy this rule id : ", Eval("RULEID")) %>' ToolTip="Copy Rule"><i class="fa fa-copy"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteRule"
                                        runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this rule id : ", Eval("RULEID")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="btnStats" runat="server" CssClass="btn btn-primary btn-sm" 
                                        Text="Activate/Deactivate" OnClick="Active_Deactive_btnclk" /><br />
                                    <asp:CheckBox ID="ChekAll_chkbox" runat="server" onclick="Check_All_Activate(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Chk_ActivateDeactive" runat="server" />                     
                                    <asp:HiddenField ID="lblStatus" runat="server"/>
                                    <%--<asp:HiddenField ID="Activated" runat="server" Text='<%#Bind("ACTIVATED") %>'/>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                ControlStyle-Width="100%" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rule ID" ControlStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRULE_ID" Text='<%# Bind("RULEID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Copied From" ControlStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblcopiedfrom" Text='<%# Bind("COPY_RULE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Field Name" ControlStyle-Width="100%" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFIELDNAME" Text='<%# Bind("FIELDNAME") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rule Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" Text='<%# Bind("Description") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Query Text">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuery" Text='<%# Bind("QueryText") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Label ID="QUERY_ACTION" Text='<%# Bind("QUERY_ACTION") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Condition">
                                <ItemTemplate>
                                    <asp:Label ID="lblCondition" Text='<%# Bind("Condition") %>' CssClass="ruledescription" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Rule', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <HeaderTemplate>
                                    <label>Entered By Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <div>
                                            <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <HeaderTemplate>
                                    <label>Last Modified Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <div>
                                            <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="20%" ItemStyle-Width="20%" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">  <%--HeaderStyle-CssClass="align-left"--%>
                                <HeaderTemplate>
                                    <label>Activation Details</label>
                                    <br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Activated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="ACTIVEBYNAME" runat="server" Text='<%# Bind("ACTIVATEBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ACTIVE_CAL_DAT" runat="server" Text='<%# Bind("ACTIVATE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="ACTIVE_CAL_TZDAT" runat="server" Text='<%# Eval("ACTIVATE_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="20%" ItemStyle-Width="20%" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">  <%--HeaderStyle-CssClass="align-left"--%>
                                <HeaderTemplate>
                                    <label>Deactivation Details</label>
                                    <br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deactivated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="DEACTIVEBYNAME" runat="server" Text='<%# Bind("DEACTIVATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="DEACTIVE_CAL_DAT" runat="server" Text='<%# Bind("DEACTIVATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="DEACTIVE_CAL_TZDAT" runat="server" Text='<%# Eval("DEACTIVATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnExportFunc" />
            <asp:PostBackTrigger ControlID="lbtnExportRules" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
