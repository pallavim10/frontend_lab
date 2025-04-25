<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_INDIC_TRIGGER.aspx.cs"
    Inherits="CTMS.RM_INDIC_TRIGGER" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diagonsearch</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {

            $('.txtDecimal').inputmask(
            "decimal", {
                rightAlign: 0
            }
            );

        });

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

        // Read a page's GET URL variables and return them as an associative array.
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        function AssignRiskIndic() {
            var TileID = getUrlVars()["TILEID"];
            var test = "RM_Assign_NewRiskIndic.aspx?TILEID=" + TileID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=820px,width=1900px";
            window.open(test, '_blank', strWinProperty);
            window.top.close();
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Risk Indicator Trigger
                </h3>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Tile Name :
                                    </label>
                                </div>
                                <div class="col-lg-6" style="padding-left: 0px">
                                    <asp:Label runat="server" ID="lblTileName" CssClass="form-control" Style="min-width: 150px;
                                        width: auto;"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        &nbsp;</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:Label runat="server" ID="lbl" Width="100px"> 
                                    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label runat="server" ID="Label1" Width="50px" CssClass="label" Text="Scores"> 
                                    </asp:Label>
                                </div>
                                <div class="col-lg-2 txtCenter" style="display: inline-flex; padding-left: 0px">
                                    <asp:Label runat="server" ID="Label5" Width="200px" CssClass="label" Text="Post To"> 
                                    </asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <label>
                                        &nbsp;</label>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Level 1 Trigger :</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:TextBox runat="server" ID="txtL1Trig" Width="100px" CssClass="form-control txtDecimal txtCenter"> 
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlLV0Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="X" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Z" Value="15"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="display: inline-flex; padding-left: 0px">
                                    <asp:DropDownList runat="server" ID="ddlLPost1" CssClass="form-control txtCenter width200px">
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                        <asp:ListItem Text="Issue" Value="Issue"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtLAct1" CssClass="form-control"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        &nbsp;</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:Label runat="server" ID="Label2" Width="100px"> 
                                    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlLV1Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="A" Value="-5"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="-10"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="-15"></asp:ListItem>
                                        <asp:ListItem Text="D" Value="-20"></asp:ListItem>
                                        <asp:ListItem Text="E" Value="-25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Level 2 Trigger :</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:TextBox runat="server" ID="txtL2Trig" Width="100px" CssClass="form-control txtDecimal txtCenter"> 
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlLV2Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="A" Value="-5"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="-10"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="-15"></asp:ListItem>
                                        <asp:ListItem Text="D" Value="-20"></asp:ListItem>
                                        <asp:ListItem Text="E" Value="-25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="display: inline-flex; padding-left: 0px">
                                    <asp:DropDownList runat="server" ID="ddlLPost2" CssClass="form-control txtCenter width200px">
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                        <asp:ListItem Text="Issue" Value="Issue"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtLAct2" CssClass="form-control "> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Country Level 1 Trigger :</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:TextBox runat="server" ID="txtCL1Trig" Width="100px" CssClass="form-control txtCenter txtDecimal"> 
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlCLV0Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="X" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Z" Value="15"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="display: inline-flex; padding-left: 0px">
                                    <asp:DropDownList runat="server" ID="ddlCLPost1" CssClass="form-control txtCenter width200px">
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                        <asp:ListItem Text="Issue" Value="Issue"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtCLAct1" CssClass="form-control"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        &nbsp;</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:Label runat="server" ID="Label3" Width="100px"> 
                                    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlCLV1Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="A" Value="-5"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="-10"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="-15"></asp:ListItem>
                                        <asp:ListItem Text="D" Value="-20"></asp:ListItem>
                                        <asp:ListItem Text="E" Value="-25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Country Level 2 Trigger :</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:TextBox runat="server" ID="txtCL2Trig" Width="100px" CssClass="form-control txtCenter txtDecimal"> 
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlCLV2Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="A" Value="-5"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="-10"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="-15"></asp:ListItem>
                                        <asp:ListItem Text="D" Value="-20"></asp:ListItem>
                                        <asp:ListItem Text="E" Value="-25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="display: inline-flex; padding-left: 0px">
                                    <asp:DropDownList runat="server" ID="ddlCLPost2" CssClass="form-control txtCenter width200px">
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                        <asp:ListItem Text="Issue" Value="Issue"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtCLAct2" CssClass="form-control "> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Inv. Level 1 Trigger :</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:TextBox runat="server" ID="txtInvL1Trig" Width="100px" CssClass="form-control txtCenter txtDecimal"> 
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlInvLV0Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="X" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Y" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Z" Value="15"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="display: inline-flex; padding-left: 0px">
                                    <asp:DropDownList runat="server" ID="ddlInvLPost1" CssClass="form-control txtCenter width200px">
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                        <asp:ListItem Text="Issue" Value="Issue"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtInvLAct1" CssClass="form-control "> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        &nbsp;</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:Label runat="server" ID="Label4" Width="100px"> 
                                    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlInvLV1Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="A" Value="-5"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="-10"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="-15"></asp:ListItem>
                                        <asp:ListItem Text="D" Value="-20"></asp:ListItem>
                                        <asp:ListItem Text="E" Value="-25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                    <label>
                                        Inv. Level 2 Trigger :</label>
                                </div>
                                <div class="col-lg-6" style="display: inline-flex; padding-left: 0px">
                                    <asp:TextBox runat="server" ID="txtInvL2Trig" Width="100px" CssClass="form-control txtCenter txtDecimal"> 
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="ddlInvLV2Trig" CssClass="form-control txtCenter width60px">
                                        <asp:ListItem Text="A" Value="-5"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="-10"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="-15"></asp:ListItem>
                                        <asp:ListItem Text="D" Value="-20"></asp:ListItem>
                                        <asp:ListItem Text="E" Value="-25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="display: inline-flex; padding-left: 0px">
                                    <asp:DropDownList runat="server" ID="ddlInvLPost2" CssClass="form-control txtCenter width200px">
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Risk" Value="Risk"></asp:ListItem>
                                        <asp:ListItem Text="Issue" Value="Issue"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox runat="server" ID="txtInvLAct2" CssClass="form-control"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="rows">
                            <div class="col-lg-12" style="display: inline-flex">
                                <div class="col-lg-2 width250pximp">
                                </div>
                                <div class="col-lg-6" style="padding-left: 0px">
                                    <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="bntSave_Click" />
                                </div>
                                <div class="col-lg-2 " style="display: inline-flex;">
                                    &nbsp;
                                </div>
                                <div class="col-lg-2" style="display: inline-flex;">
                                    &nbsp;
                                    <div class="pull-right label" runat="server" id="divSetRiskIndic" style="margin-right: 20px;">
                                        <asp:Label runat="server" ID="lblAddAnticip" CssClass="requiredSign" Text="Risk Indicator is Not Assigned"></asp:Label>
                                        <asp:LinkButton ID="lbtnAddAnticip" runat="server" OnClientClick="return AssignRiskIndic();"
                                            Text="click here to assign Risk Indicator..."></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
