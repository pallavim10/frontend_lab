<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_STOP_CLAUSE.aspx.cs" Inherits="CTMS.NIWRS_STOP_CLAUSE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
  <%--  <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <script type="text/javascript" src="CommonFunctionsJs/IWRS/IWRS_Datatable.js"></script>
    <script type="text/javascript">

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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

        function bindOptionValues() {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue1') {
                    avaTag = $('#MainContent_hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue2') {
                    avaTag = $('#MainContent_hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue3') {
                    avaTag = $('#MainContent_hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue4') {
                    avaTag = $('#MainContent_hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue5') {
                    avaTag = $('#MainContent_hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        }

        $(function () {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue1') {
                    avaTag = $('#MainContent_hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue2') {
                    avaTag = $('#MainContent_hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue3') {
                    avaTag = $('#MainContent_hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue4') {
                    avaTag = $('#MainContent_hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'MainContent_txtLISTValue5') {
                    avaTag = $('#MainContent_hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });


    </script>
    <script type='text/javascript'>
        $('#Count').css('display', 'none');
        var maxLimit = 100;
        $(document).ready(function () {
            $('#<%= txtMsg.ClientID %>').keyup(function () {
                var lengthCount = this.value.length;
                if (lengthCount > maxLimit) {
                    this.value = this.value.substring(0, maxLimit);
                    var charactersLeft = maxLimit - lengthCount + 1;
                }
                else {
                    var charactersLeft = maxLimit - lengthCount;
                }
                $('#Count').css('display', 'block');
                $('#Count').text(charactersLeft + ' Characters left');
            });
        });
        
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }
    </script>
    <style type="text/css">
        .btn-info {
            background-repeat: repeat-x;
            border-color: #28a4c9;
            /*background-image: linear-gradient(to bottom, #5bc0de 0%, #2aabd2 100%);*/
        }
        
        .prevent-refresh-button {
            display: inline-block;
            padding: 5px 5px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            width: 100pt;
            height: 20pt;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-6">
            <div class="box box-primary" style="min-height: 333px;">
                <div class="box-header">
                    <h3 class="box-title">Define Variables</h3>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 0px">
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <asp:Label ID="Label5" class="label" runat="server" Text="Variable Name :"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:TextBox runat="server" ID="txtRuleVariableName" CssClass="form-control width300px required"> </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label9" class="label" runat="server" Text="Module :"></asp:Label>
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
                                    <div class="col-md-4">
                                        <asp:Label ID="Label10" class="label" runat="server" Text="Field :"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="control">
                                            <asp:DropDownList ID="ddlField" class="form-control width300px required" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtnSubmitVariableDec" Text="Submit" runat="server" Style="color: white;"
                                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmitVariableDec_Click" />
                                    <asp:LinkButton ID="lbtnUpdateVariableDec" Text="Update" runat="server" Style="color: white;"
                                        Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnUpdateVariableDec_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbtnCancelVariableDec" Text="Cancel" runat="server" Style="color: white;"
                                        CssClass="btn btn-primary btn-sm" OnClick="lbtnCancelVariableDec_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="box box-primary" style="min-height: 300px;">
                <div class="box-header with-border">
                    <h4 class="box-title" align="left">Records
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div class="rows">
                            <div style="width: 100%; height: 300px; overflow: auto;">
                                <asp:GridView ID="gvVariableDeclare" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                    OnRowCommand="gvVariableDeclare_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                            HeaderText="ID" ControlStyle-Width="100%" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EDITVAR"
                                                    runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variable Name" ControlStyle-Width="100%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVariableName" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module Name" ControlStyle-Width="100%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Name" ControlStyle-Width="100%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SC_VARIABLES', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DELETEVAR"
                                                    runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this variable : ", Eval("VARIABLENAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Defined Stop Clauses</h3>
            <div class="pull-right">
                        <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Stop Clauses&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" 
                                    Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                                </asp:LinkButton></li>
                            <hr style="margin: 5px;" />
                            <li>
                                <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                                    ToolTip="PDF" Text="PDF" Style="color: #333333;">
                                </asp:LinkButton>
                            </li>
                        </ul>
                        &nbsp;&nbsp;
                    </div>
        </div>
        <div class="rows">
            <asp:GridView ID="grdStepCrits" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                OnRowCommand="grdStepCrits_RowCommand" OnPreRender="gvVariableDeclare_PreRender">
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="ID" Text='<%# Bind("ID") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Site Id">
                        <ItemTemplate>
                            <asp:Label ID="lblSITEID" Text='<%# Bind("SITEID") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Form">
                        <ItemTemplate>
                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Limit" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblLimit" Text='<%# Bind("Limit") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Message Box" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lblMSGBOX" Text='<%# Bind("MSGBOX") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Criteria">
                        <ItemTemplate>
                            <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' ToolTip='<%# Bind("CritCode") %>'
                                runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_SC_CRITs', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit"
                                runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this module : ", Eval("MODULENAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary" style="min-height: 210px;">
                <div class="box-header" style="float: left; top: 0px; left: 0px;">
                    <h4 class="box-title" align="left">Define Stop Clause
                    </h4>
                </div>
                <br />
                <br />
                <div class="box-body">
                    <div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Site Id :</label>
                                </div>
                                <div class="row col-md-10">
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlSITEID" CssClass="form-control width200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Name :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtName" runat="server" Width="500" CssClass="form-control required1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Form :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="control">
                                            <asp:DropDownList ID="ddlSCModule" Width="500" class="form-control required1" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Limit (No. of Subjects) :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtLimit" runat="server" Width="500" CssClass="form-control required1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Action :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="col-md-2">
                                            <asp:CheckBox ID="chkBefore" runat="server" />&nbsp&nbsp Before Activity
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox ID="chkAfter" runat="server" />&nbsp&nbsp After Activity
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Message Box :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:Label ID="Count"></asp:Label>
                                    <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Height="70" Width="500" MaxLength="100"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Navigate to :</label>
                                </div>
                                <div class="row col-md-10">
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlNavType" CssClass="form-control width200px"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlNavType_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Listing" Value="Listing"></asp:ListItem>
                                            <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlNavTo" CssClass="form-control width200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Set Criteria :</label>
                                </div>
                                <div class="col-md-10">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required1 width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required1"
                                                Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField runat="server" ID="hfValue2" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue2"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField runat="server" ID="hfValue3" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue3"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField runat="server" ID="hfValue4" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue4"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField5_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField runat="server" ID="hfValue5" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue5"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                        OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                        OnClick="btnUpdate_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btncancel_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
