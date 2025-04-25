<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_SET_VISIT_CRITERIA.aspx.cs"
    Inherits="CTMS.DM_SET_VISIT_CRITERIA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">

        function bindOptionValues() {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'txtLISTValue1') {
                    avaTag = $('#hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue2') {
                    avaTag = $('#hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue3') {
                    avaTag = $('#hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue4') {
                    avaTag = $('#hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue5') {
                    avaTag = $('#hfValue5').val().split(',');
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
                if ($(colorFields[a]).attr('id') == 'txtLISTValue1') {
                    avaTag = $('#hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue2') {
                    avaTag = $('#hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue3') {
                    avaTag = $('#hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue4') {
                    avaTag = $('#hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue5') {
                    avaTag = $('#hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-md-12">
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Manage Visit Criterias</h3>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div class="fixTableHead">
                                            <asp:GridView ID="grdStepCrits" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                OnRowCommand="grdStepCrits_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                                                runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="VISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NAME" Text='<%# Bind("NAME") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Criteria">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_VISIT_CRITs', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none">
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
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none">
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
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit"
                                                                runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this criteria :  ", Eval("Criteria")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                        <div id="DivAuditTrail" style="font-size: small;">
                        </div>
                    </div>
                    <div class="box box-primary" style="min-height: 210px;">
                        <div class="box-header" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left"></h4>
                            Add Visit Criterias for : &nbsp;<asp:Label ID="lblList" runat="server"></asp:Label>
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Enter Name :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control width200px required"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisit1" runat="server" CssClass="form-control width200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModule1" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required"
                                                Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                        <div class="col-md-2">
                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisit2" runat="server" CssClass="form-control width200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModule2" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                        <div class="col-md-2">
                                            <asp:HiddenField runat="server" ID="hfValue2" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue2"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisit3" runat="server" CssClass="form-control width200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModule3" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                        <div class="col-md-2">
                                            <asp:HiddenField runat="server" ID="hfValue3" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue3"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisit4" runat="server" CssClass="form-control width200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModule4" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                        <div class="col-md-2">
                                            <asp:HiddenField runat="server" ID="hfValue4" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue4"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlVisit5" runat="server" CssClass="form-control width200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlModule5" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule5_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control width200px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField5_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                        <div class="col-md-2">
                                            <asp:HiddenField runat="server" ID="hfValue5" />
                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue5"
                                                Width="100%"> </asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            &nbsp;
                                        </div>
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
                                        <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnsubmit_Click" />
                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
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
    </form>
</body>
</html>
