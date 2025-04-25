<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_LABS_DATA.aspx.cs" Inherits="CTMS.DM_LABS_DATA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });

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
                    <h3 class="box-title" style="width: 100%;">
                        <asp:Label ID="lblHeader" Text="Define Lab Reference Range" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box box-primary" style="min-height: 264px;" id="divRefRange" runat="server">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">Add Lab Reference Range
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>
                                                                Select Site:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlSITE" Width="280px" runat="server" class="form-control drpControl required"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSITE_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>
                                                                Select Lab Name:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlLabName" Width="280px" runat="server" class="form-control drpControl required" AutoPostBack="true" OnSelectedIndexChanged="ddlLabName_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>
                                                                Select Test Name:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlTestName" Width="280px" runat="server" class="form-control drpControl required"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlTestName_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>
                                                                Select Gender:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlGender" Width="280px" runat="server" class="form-control drpControl required"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>
                                                                Age Lower Limit:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtAgeLower" Width="280px" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>
                                                                Age Upper Limit:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtAgeUpper" Width="280px" runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>
                                                                Reference Ranges Lower Limit:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtRefLower" Width="280px" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>
                                                                Reference Ranges Upper Limit:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtRefUpper" Width="280px" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2">
                                                            <label>
                                                                Effective From Date:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtfromdate" Width="280px" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label>
                                                                Units:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtUnits" Width="280px" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <div class="col-md-2">
                                                            <label>
                                                                Effective To Date:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtTodate" Width="280px" runat="server" CssClass="form-control txtDate"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-5">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnUpdate_Click" />
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                    OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-12" id="divRecords" runat="server" visible="false">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left">Lab Reference Range Records
                                        </h4>
                                        <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">
                                            <asp:LinkButton runat="server" ID="lbtnExport" OnClick="lbtnExport_Click" Visible="false"
                                                Text="Export Lab Reference Range" ForeColor="White" CssClass="btn btn-info btn-sm" Font-Bold="true"> Export Lab Reference Range&nbsp;<span class="glyphicon glyphicon-download 2x"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div class="fixTableHead">
                                                <div class="rows">
                                                    <asp:GridView ID="grdLabsData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                        Style="border-collapse: collapse; width: 97%;" OnRowCommand="grdLabsData_RowCommand" OnPreRender="grd_data_PreRender">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                ItemStyle-CssClass="disp-none">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupdateLab" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="EditLab" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Site Id" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lab Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LABNAME" runat="server" Text='<%# Bind("LABNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Test Name" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="testname" runat="server" Text='<%# Bind("LBTEST") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gender" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SEX" runat="server" Text='<%# Bind("SEX") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Age Lower Limit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AGELO" runat="server" Text='<%# Bind("AGELO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Age Upper Limit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="AGEHI" runat="server" Text='<%# Bind("AGEHI") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reference Ranges Lower Limit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LBORNRLO" runat="server" Text='<%# Bind("LBORNRLO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reference Ranges Upper Limit" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LBORNRHI" runat="server" Text='<%# Bind("LBORNRHI") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Units" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LBORRESU" runat="server" Text='<%# Bind("LBORRESU") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Effective from date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="EFFDATEFROM" runat="server" Text='<%# Bind("EFFDATEFROM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Effective to date" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="EFFDATETO" runat="server" Text='<%# Bind("EFFDATETO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_LAB_DEFAULTS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
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
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtndeleteLab" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DeleteLab" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this lab name : ", Eval("LABNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>&nbsp;&nbsp;
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
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
