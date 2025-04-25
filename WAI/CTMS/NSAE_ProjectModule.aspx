<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_ProjectModule.aspx.cs" Inherits="CTMS.NSAE_ProjectModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome-ie7.min.css" rel="stylesheet" />
    <script type="text/jscript">
        function AddDrpDownData(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(1)').find('span').html();
            var test = "DM_AddDrpDownData.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=330,width=1000";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null || value == "Select") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });
        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == null || value == "Select") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });


    </script>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });
            $(".Datatable").parent().parent().addClass('fixTableHead');
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
                    <h3 class="box-title">Manage Project Modules
                    </h3>
                </div>
            </div>
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            <div class="box box-primary">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left">All Modules
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div>
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gvAllModules" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 98%; border-collapse: collapse; margin-left: 20px;" OnPreRender="gvAddedModules_PreRender" OnRowDataBound="gvAllModules_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SEQNO" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSEQNO" runat="server" MaxLength="3" CssClass="form-control width100px txt_center numeric"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medical Review" ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMedicalReview" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="txtCenter" HeaderText="Select To Add">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                         <asp:Label ID="lblStatus" runat="server" ForeColor="Blue"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleID" runat="server" Visible="false" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                                        <asp:Label ID="lblModule" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
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
            <br />
            <div class="box-body">
                <div>
                    <div class="row txtCenter">
                        <asp:LinkButton ID="lbtnAddToGrp" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                            ValidationGroup="Grp" OnClick="lbtnAddToGrp_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                <asp:LinkButton ID="lbtnRemoveFromGrp" ForeColor="White" Text="Remove" runat="server"
                                    ValidationGroup="Grp" CssClass="btn btn-danger btn-sm" OnClick="lbtnRemoveFromGrp_Click" />
                    </div>
                </div>
            </div>
            <br />
            <div class="box box-info">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left">Added Modules
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div>
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gvAddedModules" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 98%; border-collapse: collapse;" OnPreRender="gvAddedModules_PreRender" OnRowDataBound="gvAddedModules_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SEQNO" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" Text='<%# Bind("SAE_SEQNO") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleID" runat="server" Visible="false" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                                        <asp:Label ID="lblModule" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medical Review" ItemStyle-CssClass="txtCenter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="chkMedicalReview" CommandArgument='<%# Eval("SAE_MM") %>' runat="server" Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSAE_MM" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
                                                    <HeaderTemplate>
                                                        <label>Added By Details</label><br />
                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
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
                                                <asp:TemplateField ItemStyle-CssClass="width30px txtCenter" HeaderText="Select To Remove">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
