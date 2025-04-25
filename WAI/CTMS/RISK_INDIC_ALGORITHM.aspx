<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RISK_INDIC_ALGORITHM.aspx.cs"
    Inherits="CTMS.RISK_INDIC_ALGORITHM" %>

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
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- fullCalendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
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
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <script>

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
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

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 5px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">
                    Manage Algorithms</h3>
            </div>
            <div class="rows">
                <asp:GridView ID="grdAlgorithm" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                    OnRowCommand="grdAlgorithm_RowCommand" OnRowDataBound="grdAlgorithm_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Active" ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lblActive" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="DeactiveAlgo"><i class="fa fa-check-square-o" style="color:Black;" ></i></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lblDeActive" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                    CommandName="ActiveAlgo"><i class="fa fa-square-o" style="color:Black;"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SEQ No." ItemStyle-CssClass="txt_center">
                            <ItemTemplate>
                                <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actionable">
                            <ItemTemplate>
                                <asp:Label ID="lblActionable" Text='<%# Bind("Actionable") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Condition">
                            <ItemTemplate>
                                <asp:Label ID="lblCrit" Text='<%# Bind("Condition") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Options" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditAlgo"
                                    runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteAlgo"
                                    runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" style="min-height: 210px;">
                    <div class="box-header" style="float: left; top: 0px; left: 0px;">
                        <h4 class="box-title" align="left">
                            Create Algorithm
                        </h4>
                    </div>
                    <br />
                    <br />
                    <div class="box-body">
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Enter Actionable :</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtActionable" Height="70px" TextMode="MultiLine"
                                            Width="80%" CssClass="form-control required"> 
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>
                                            Enter Sequence No.:</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtSEQNO" runat="server" Width="10%" CssClass="form-control numeric required"></asp:TextBox>
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
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat1" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField1" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition1" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue1" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr1" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat2" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField2" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition2" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue2" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr2" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat3" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField3" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition3" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue3" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr3" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat4" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField4" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition4" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue4" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr4" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat5" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField5" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition5" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue5" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr5" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat6" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField6" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition6" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue6" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr6" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat7" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField7" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition7" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue7" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr7" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat8" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField8" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition8" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue8" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr8" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat9" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField9" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition9" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue9" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpAndOr9" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                    <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                    <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCat10" runat="server" CssClass="form-control" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpField10" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Score" Value="Score"></asp:ListItem>
                                                    <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                                    <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                                    <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Z" Value="Z"></asp:ListItem>
                                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="drpCondition10" runat="server" CssClass="form-control required"
                                                    Width="100%">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                    <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                    <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                    <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" CssClass="numeric form-control" ID="txtValue10" Width="100%"> </asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
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
                                        <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnsubmit_Click" />
                                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
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
    </form>
</body>
</html>
