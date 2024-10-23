<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Feilds.aspx.cs" Inherits="SpecimenTracking.Manage_Feilds" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="Scripts/btnSave_Required.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });


            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {

                    return true;
                }
                else {
                    event.preventDefault();
                }
            });
        });

        $(document).ready(function () {
            var dropdown = $('#<%= drpControlType.ClientID %>');
            
            dropdown.change(function () {
                var selectedValue = $(this).val();
                var divMaxlength = $('#divMaxlength');
               

                if (selectedValue === 'Textbox' || selectedValue === 'Multiline Textbox') {
                    divMaxlength.removeClass('d-none');
                    
                } else {
                    divMaxlength.addClass('d-none');
                }
            });
        });


        function pageLoad() {

            $('.select').select2();

            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });


            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {

                    return true;
                }
                else {
                    event.preventDefault();
                }
            });

        }


        function DATA_Changed(element) {

            $(element).closest('td').find("input[id*='btnDATA_Changed']").click();

        }

        function showAuditTrail(element) {
            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'FIELD_MASTER';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
                data: JSON.stringify({ TABLENAME: TABLENAME, ID: ID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    } else {
                        $('#DivAuditTrail').html(response.d);
                        $('#modal-lg').modal('show'); // Show the modal after populating it
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching audit trail:', status, error);
                    alert("An error occurred. Please contact the administrator.");
                }
            });

            return false;
        }
        
        function AddOptions(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var test = "MANAGE_ADD_OPTION.aspx?ID=" + ID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1200";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }
        function ManageFields(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var test = "MANAGE_FEILDS_DATA.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=700";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
    <style>
        .uppercase {
            text-transform: uppercase;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Fields</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active"><a href="SETUPDashboard.aspx">Setup</a></li>
                            <li class="breadcrumb-item active">Manage Fields</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Manage Default Fields</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div style="width: 100%; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grdMngOptFields" runat="server" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-striped " OnRowDataBound="grdMngOptFields_RowDataBound" OnRowCommand="grdMngOptFields_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                                HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="HdnISACTIVE" runat="server" Value='<%# Eval("ISACTIVE") %>' />
                                                                    <asp:HiddenField ID="HdnRepeat" runat="server" Value='<%# Eval("REPEAT") %>' />
                                                                    <asp:HiddenField ID="HdnLebvel1" runat="server" Value='<%# Eval("FIRSTENTRY") %>' />
                                                                    <asp:HiddenField ID="HdnLebvel2" runat="server" Value='<%# Eval("SECONDENTRY") %>' />
                                                                    <asp:HiddenField ID="HdnControlType" runat="server" Value='<%# Eval("CONTROLTYPE") %>' />
                                                                    <asp:HiddenField ID="HdnVariableName" runat="server" Value='<%# Eval("VARIABLENAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFieldName" runat="server" onChange="DATA_Changed(this);" Text='<%# Bind("FIELDNAME") %>' CssClass="form-control w-75"></asp:TextBox>
                                                                    <asp:Button runat="server" ID="btnDATA_Changed" CssClass="d-none" OnClick="DATA_Changed"></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Enable/Disable" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtEnable" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="DISABLE" ToolTip="Disable"><i class="fa fa-toggle-on" style='color: blue;font-size: 25px' ></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtDisable" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="ENABLE" ToolTip="Enable"><i class="fa fa-toggle-off" style='color: green;font-size: 25px' ></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Repeat" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtRepeatYes" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="REPEATNO" ToolTip="NO"><i class="fa fa-toggle-on" style='color: blue;font-size: 25px' ></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtRepeatNo" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                        CommandName="REPEATYES" ToolTip="Yes"><i class="fa fa-toggle-off" style='color: green;font-size: 25px' ></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Applicable for" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drplevel" runat="server" CssClass="form-control required" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drplevel_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Level 1" Value="Level 1"></asp:ListItem>
                                                                        <asp:ListItem Text="Level 2" Value="Level 2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Variable Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVariable" runat="server" Text='<%# Bind("VARIABLENAME") %>' CssClass="form-control w-100"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Manage" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtAddOption" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>' OnClientClick="return AddOptions(this)"
                                                                        ToolTip="Add Option"><i class="fa fa-tasks" ></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtManageFields" runat="server" class="btn-info btn-sm" Visible="false" CommandArgument='<%# Bind("ID") %>' OnClientClick="return ManageFields(this)"
                                                                        ToolTip="Manage"><i class="fa fa-cog" ></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-5">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Create Fields</h3>
                                        <div class="pull-right">
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Field Name: &nbsp;</label>
                                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtFieldsName" runat="server" CssClass="form-control required w-75"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Variable Name : &nbsp;</label>
                                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtVariableName" runat="server" OnTextChanged="txtVariableName_TextChanged" AutoPostBack="true" CssClass="form-control required w-75 uppercase noSpace"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Sequence Number : &nbsp;</label>
                                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtSeqNo" runat="server" CssClass="form-control required numeric w-75"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Select Control Type : &nbsp;</label>
                                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="drpControlType" runat="server" AutoPostBack="false" class="form-control drpControl w-75 required select" SelectionMode="Single">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="Readonly" Text="Readonly"></asp:ListItem>
                                                                <asp:ListItem Value="Textbox" Text="Textbox"></asp:ListItem>
                                                                <asp:ListItem Value="Multiline Textbox" Text="Multiline Textbox"></asp:ListItem>
                                                                <asp:ListItem Value="Date" Text="Date"></asp:ListItem>
                                                                <asp:ListItem Value="Time" Text="Time"></asp:ListItem>
                                                                <asp:ListItem Value="Dropdown" Text="Dropdown"></asp:ListItem>
                                                                <asp:ListItem Value="Radio Button" Text="Radio Button"></asp:ListItem>
                                                                <asp:ListItem Value="Checkbox" Text="Checkbox"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group d-none" id="divMaxlength">
                                                            <label>Enter Max Length : &nbsp;</label>
                                                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtmaxlength" runat="server" CssClass="form-control numeric w-75"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="row form-group">

                                                            <label>Applicable for : &nbsp;</label>
                                                            <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <div class="col-md-12 d-inline-flex">
                                                                <div class="col-md-3">
                                                                    <asp:RadioButton ID="rbtnFirst" runat="server" GroupName="Type" />&nbsp;&nbsp;
                                                                            <label>Level 1</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:RadioButton ID="rbtnSecond" runat="server" GroupName="Type" />&nbsp;&nbsp;
                                                                        <label>Level 2</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:RadioButton ID="rbtnAliquotPre" runat="server" GroupName="Type" />&nbsp;&nbsp;
                                                                        <label>Aliquot Preparation</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enable/Disable Properties : &nbsp;</label>
                                                            <br />
                                                            <div class="col-md-12 d-inline-flex">
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="chkRepeat" runat="server" />&nbsp;&nbsp;
                                                                            <label>Repeat</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="chkRequired" runat="server" />&nbsp;&nbsp;
                                                                            <label>Required</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <center>
                                                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="lbnUpdate_Click"></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                                    </center>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-7">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Records</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Fields &nbsp;<span class="fas fa-download btn-sm"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div style="width: 100%; height: 607px; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="GrdMngFields" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                            CssClass="table table-bordered table-striped Datatable1" OnRowCommand="GrdMngFields_RowCommand" OnRowDataBound="GrdMngFields_RowDataBound" OnPreRender="grdMngOptFields_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        <asp:HiddenField ID="HdnControl" runat="server" Value='<%# Eval("CONTROLTYPE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtEdit" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                            CommandName="EDITED" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Seq No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSquenceNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Variable Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVariableName" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Field Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfieldName" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Control Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblControltype" runat="server" Text='<%# Bind("CONTROLTYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Applicable For">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApplicablefor" runat="server" Text='<%# Eval("Applicable For") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Repeat">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRepeat" runat="server" CommandArgument='<%# Eval("REPEAT") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconREPEAT" runat="server" class="fa fa-check"></i></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Required">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRequired" runat="server" CommandArgument='<%# Eval("REQUIRED") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconREQUIRED" runat="server" class="fa fa-check"></i></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add Option" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtAddOption" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>' OnClientClick="return AddOptions(this)"
                                                                            ToolTip="Add Option"><i class="fa fa-tasks"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtdelete" runat="server" class="btn-danger btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                            CommandName="DELETED" OnClientClick="return confirm(event);" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });
    </script>
    <script>

        function confirm(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to delete this Record?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                if (isConfirm) {
                    var linkButton = event.target;
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }
                    linkButton.onclick = null;
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });
            return false;
        }

    </script>
   
</asp:Content>
