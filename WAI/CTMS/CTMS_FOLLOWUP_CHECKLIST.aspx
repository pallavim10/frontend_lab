<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_FOLLOWUP_CHECKLIST.aspx.cs" Inherits="CTMS.CTMS_FOLLOWUP_CHECKLIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            if ($(location).attr('href').indexOf('NIWRS_FORM') != -1) {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050],
                        maxDate: new Date()
                    });
                });

            }
            else {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050]
                    });
                });

            }

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });

            $('.txtDateMask').each(function (index, element) {
                $(element).inputmask("dd/mm/yyyy", { placeholder: "dd/mm/yyyy" });
            });

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
                    "hh:mm", {
                        placeholder: "HH:MM",
                        insertMode: false,
                        showMaskOnHover: false,
                        hourFormat: "24"
                    });
            });


            $('.txtuppercase').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });

            $('.txtuppercase').keydown(function (e) {

                var key = e.keyCode;
                if (key === 189 && e.shiftKey === true) {
                    return true;
                }
                else if ((key == 189) || (key == 109)) {
                    return true;
                }
                else if (e.ctrlKey || e.altKey) {
                    e.preventDefault();
                }
                else {
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                Follow-Up Tracking Data
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div id="Div1" runat="server" class="form-group" style="display: inline-flex">
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Site Id:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="ddlSiteId" runat="server" ForeColor="Blue" CssClass="form-control "
                            OnSelectedIndexChanged="ddlSiteId_SelectedIndexChanged" AutoPostBack="True" Style="width: 100%">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Select Visit Type:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpVisitType" runat="server" ForeColor="Blue" AutoPostBack="True"
                        CssClass="form-control " Style="width: 100%" OnSelectedIndexChanged="drpVisitType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex">
                <label class="label" style="color: Maroon;">
                    Select Visit Id:
                </label>
                <div class="Control">
                    <asp:DropDownList ID="drpVisitID" ForeColor="Blue" runat="server" AutoPostBack="True"
                        CssClass="form-control " OnSelectedIndexChanged="drpVisitID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="box">
                <asp:GridView ID="grdChecklistComments" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped Datatable" Caption="" OnRowDataBound="grdChecklistComments_RowDataBound"
                    OnPreRender="gvEmp_PreRender" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="ID" runat="server" CommandArgument='<%#Eval("ID") %>' Text='<%# Eval("ID") %>'
                                    CommandName="INVID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROJECT ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="Project_ID" runat="server" CommandArgument='<%#Eval("Project_ID") %>'
                                    Text='<%# Eval("Project_ID") %>' CommandName="INVID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="SECTIONID" runat="server" Text='<%# Eval("SECTIONID") %>' CommandArgument='<%#Eval("SECTIONID") %>'
                                    CommandName="LBTEST"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Section" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="SUBSECTIONID" runat="server" Text='<%# Eval("SUBSECTIONID") %>' CommandArgument='<%#Eval("SECTIONID") %>'
                                    CommandName="LBTEST"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblInvID" runat="server" CommandArgument='<%#Eval("INVID") %>' Text='<%# Eval("INVID") %>'
                                    CommandName="INVID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject ID" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblSUBJID" runat="server" CommandArgument='<%#Eval("SUBJID") %>' Text='<%# Eval("SUBJID") %>'
                                    CommandName="SUBJID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monitoring Visit Id" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="ChecklistID" runat="server" Text='<%# Eval("ChecklistID") %>' CommandArgument='<%#Eval("ChecklistID") %>'
                                    CommandName="SUBJID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:Label ID="Comments" runat="server" CommandName="Comments" Text='<%# Eval("Comments") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_Issue" Enabled="false" runat="server" CssClass="txt_center"
                                    ToolTip="Issue" font-family="Arial"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="For Internal Use" HeaderStyle-CssClass="txt_center"
                            ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_Internal" Enabled="false" runat="server" CssClass="txt_center"
                                    ToolTip="Internal" font-family="Arial"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Report" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_REPORT" Enabled="false" runat="server" CssClass="txt_center"
                                    ToolTip="Issue" font-family="Arial"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Followup" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_Followup" Enabled="false" runat="server" CssClass="txt_center"
                                    ToolTip="Followup" font-family="Arial"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observation" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:CheckBox ID="CHK_Observation" Enabled="false" runat="server" CssClass="txt_center"
                                    ToolTip="Observation" font-family="Arial"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User">
                            <ItemTemplate>
                                <asp:Label ID="ENTEREDBY" runat="server" Text='<%# Eval("ENTEREDBY") %>' CommandName="ENTEREDBY"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
