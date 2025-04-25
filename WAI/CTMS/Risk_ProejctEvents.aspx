<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Risk_ProejctEvents.aspx.cs"
    Inherits="CTMS.Risk_ProejctEvents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">

        function DecodeUrl(url) {

            var dec = decodeURI(url);

            window.location.href = dec;
        }



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


        // js prototype
        if (typeof (Number.prototype.isBetween) === "undefined") {
            Number.prototype.isBetween = function (min, max, notBoundaries) {
                var between = false;
                if (notBoundaries) {
                    if ((this < max) && (this > min)) between = true;
                } else {
                    if ((this <= max) && (this >= min)) between = true;
                }
                return between;
            }
        }


        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });


            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

        }


        function AddMitigation(TYPE) {
            var EventID = $('#txtRiskID').val();
            var test = "RM_AddRiskMitigation.aspx?EventID=" + EventID + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=350px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Mitigation(element, TYPE) {
            var EventID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var test = "RM_AddRiskMitigation.aspx?EventID=" + EventID + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=350px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddCause(TYPE) {
            var EventID = $('#txtRiskID').val();
            var test = "RM_AddRiskCause.aspx?EventID=" + EventID + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=350px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Cause(element, TYPE) {
            var EventID = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var test = "RM_AddRiskCause.aspx?EventID=" + EventID + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=350px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        $(document).ready(function () {
            $('.blink').blink(); // default is 500ms blink interval.
            //$('.blink').blink({delay:100}); // causes a 100ms blink interval.
        });

        function Print() {
            var ProjectId = '<%= Session["PROJECTID"] %>'
            var RISKID = $('#txtRiskID').val();
            var test = "ReportRisk_ProejctEvents.aspx?RISKID=" + RISKID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function OPENDETAILS(element) {
            var SOURCE = $("#txtSource").val()

            if (SOURCE = 'Protocol Deviation') {
                var PROTVOIL_ID = $("#lbtnRefrence").text()
                var test = "ProtDev.aspx?PROTVOIL_ID=" + PROTVOIL_ID;
            }
            else {
                var IssueID = $("#lbtnRefrence").text()
                var test = "IssueDetails.aspx?IssueID=" + IssueID;
            }

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=750";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function RELATEDRISK(element) {
            var RiskId = $(element).prev().attr('commandargument');
            var test = "Risk_ProejctEvents.aspx?RiskId=" + RiskId + "&TYPE=UPDATE";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=650,width=750";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Event Detail
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 1400px;">
                <label class="col-lg-2 width100px label">
                    ID</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtRiskID" Enabled="false" Width="150px" CssClass="form-control txtCenter"> 
                    </asp:TextBox>
                </div>
                <label class="col-lg-1 label">
                    Date Identified</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtDateIdentify" Width="150px" CssClass="form-control txtDate txtCenter"> 
                    </asp:TextBox>
                </div>
                <div style="float: right" class="col-lg-3 align-right">
                    <label class="col-lg-3 width100px label">
                        Risk Status
                    </label>
                    <div class="col-lg-3">
                        <asp:DropDownList runat="server" ID="DrpRiskStatus" Width="300px" CssClass="form-control required ">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 1400px;">
                <label class="col-lg-2 width100px label">
                    Identified By</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtIndetifyBy" Width="150px" CssClass="form-control txtCenter"> 
                    </asp:TextBox>
                </div>
                <label class="col-lg-1  label">
                    SITE ID</label>
                <div class="col-lg-2">
                    <asp:TextBox runat="server" ID="txtSiteID" ReadOnly="true" Width="150px" CssClass="form-control txtCenter"> 
                    </asp:TextBox>
                </div>
                <div style="float: right" class="col-lg-3 align-right">
                    <label class="col-lg-3 width100px label">
                        Risk Owner
                    </label>
                    <div class="col-lg-4">
                        <asp:DropDownList runat="server" ID="drpRiskOwner" Width="300px" CssClass="form-control required">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 1200px;">
                <label class="col-lg-3 width100px label">
                    Category
                </label>
                <div class="col-lg-3">
                    <asp:Label ID="lblCategory" runat="server" CssClass="form-control" Style="width: auto;
                        min-width: 200px;"></asp:Label>
                </div>
                <label class="col-lg-1 label">
                    Sub Category
                </label>
                <div class="col-lg-3 ">
                    <asp:Label ID="lblSubCategory" runat="server" CssClass="form-control" Style="width: auto;
                        min-width: 250px;"></asp:Label>
                </div>
                <div class="label txtCenter">
                    Factors
                </div>
                <div class="col-lg-3 ">
                    <asp:Label ID="lblFactor" runat="server" CssClass="form-control" Style="width: auto"></asp:Label>
                </div>
            </div>
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 1200px;">
                <label class="col-lg-3 width100px label">
                    Source
                </label>
                <div class="col-lg-3 txtCenter">
                    <asp:TextBox ID="txtSource" runat="server" Width="270px" CssClass="form-control txtCenter required"></asp:TextBox>
                </div>
                <label class="col-lg-1 label">
                    Reference</label>
                <div class="col-lg-3">
                    <asp:LinkButton ID="lbtnRefrence" runat="server" Width="150px" OnClientClick="return OPENDETAILS(this)"
                        CssClass="form-control txtCenter"></asp:LinkButton>
                </div>
                <label class="col-lg-1 width100px label">
                    Count
                </label>
                <div class="col-lg-3 ">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtCount" ForeColor="Red" CssClass="form-control txtCenter"
                                ReadOnly="true" Style="width: 50px;"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Description</label>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="txtRiskDisc" Width="700px" Height="50px" TextMode="MultiLine"
                        CssClass="form-control required"> 
                    </asp:TextBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Impacts</label>
                <div class="col-lg-9">
                    <asp:ListBox ID="lstRiskImpact" runat="server" CssClass="width300px select" SelectionMode="Multiple"
                        Width="700px"></asp:ListBox>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 800px;">
                <label class="col-lg-3 width100px label">
                    Risk Type
                </label>
                <div class="col-lg-9">
                    <asp:ListBox ID="lstRiskType" runat="server" CssClass="select" Width="700px" Height="50px"
                        SelectionMode="Multiple"></asp:ListBox>
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 800px;">
                <label class="col-lg-3 width100px label">
                    Comment
                </label>
                <div class="col-lg-9">
                    <asp:TextBox ID="txtcomments" runat="server" Width="700px" CssClass="form-control"
                        Height="50px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 800px;">
            </div>
            <br />
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 1200px;">
                <label class="col-lg-2 width100px label">
                    Category</label>
                <div class="col-lg-3">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlcategory" AutoPostBack="true" Width="270px"
                                CssClass="form-control" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="col-lg-1 label">
                    Sub Category</label>
                <div class="col-lg-3 ">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlsubcategory" AutoPostBack="true" Width="270px"
                                CssClass="form-control" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="label txtCenter">
                    Factors</label>
                <div class="col-lg-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="ddlfactor" Width="270px" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlfactor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 800px;">
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px; width: 1200px;">
                <div class="pull-right label" runat="server" id="divAddAnticip" style="margin-right: 20px;">
                    <asp:Label runat="server" ID="lblAddAnticip" CssClass="requiredSign" Text="This combination of Category, Sub-Category and Factor is not added in Anticipated Risks"></asp:Label>
                    <asp:LinkButton ID="lbtnAddAnticip" runat="server" Text="click here to add in Anticipated..."
                        OnClick="lbtnAddAnticip_Click"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <div class="inline">
                <h3 class="box-title">
                    <asp:Button ID="bntSave" runat="server" Text="Save" Style="margin-left: 10px" CssClass="btn
    btn-primary btn-sm cls-btnSave" OnClick="bntSave_Click" />
                </h3>
            </div>
        </div>
    </div>
    <div class="col-md-10">
        <div id="tabscontainer" class="nav-tabs-custom" runat="server">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab-1" data-toggle="tab">Related Risk</a></li>
                <li><a href="#tab-2" data-toggle="tab">Root Causes</a></li>
                <li><a href="#tab-3" data-toggle="tab">Risk Mitigations</a></li>
                <li><a href="#tab-4" data-toggle="tab">Mitigations OutCome</a></li>
                <li><a href="#tab-5" data-toggle="tab">Review History</a></li>
                <li><a href="#tab-6" data-toggle="tab">Comments</a></li>
            </ul>
            <div class="tab">
                <div id="tab-1" class="tab-content current">
                    <asp:GridView ID="grdRelatedRisk" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center width20px">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' CommandArgument='<%# Eval("ID") %>'
                                        CssClass="disp-noneimp" />
                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("ID") %>' CommandArgument='<%# Eval("ID") %>'
                                        OnClientClick="return RELATEDRISK(this);"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="txt_center width20px" HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Summary" ItemStyle-CssClass="width20px">
                                <ItemTemplate>
                                    <asp:Label ID="Summary" runat="server" Text='<%# Bind("REvent_description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-2" class="tab-content current">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <asp:Button ID="Button1" runat="server" Text="Add New" Style="margin-left: 10px"
                                    CssClass="btn btn-primary btn-sm cls-btnSave1" OnClientClick="return AddCause('NEW');" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnRefreshCause" runat="server" ToolTip="Refresh" Style="margin-left: 20px;"
                                    OnClick="lbtnRefreshCause_Click"> <i class="fa fa-2x fa-refresh" style="color: #333333;" title="Refresh"></i> </asp:LinkButton>
                            </div>
                            <br />
                            <div class="row">
                                <asp:GridView runat="server" ID="gvCause" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                    OnRowCommand="gvCause_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                            HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>' CommandArgument='<%#Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cause">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCause" runat="server" Text='<%# Bind("Cause") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubCause" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubCause" runat="server" Text='<%# Bind("SubCause") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
                                                    <asp:LinkButton ID="lbtnupdateCause" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="Edit1" OnClientClick="return Cause(this, 'UPDATE');" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtndeleteCause" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="tab-3" class="tab-content">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <asp:Button ID="btnAddNew" runat="server" Text="Add New" Style="margin-left: 10px"
                                    CssClass="btn btn-primary btn-sm cls-btnSave1" OnClientClick="return AddMitigation('NEW');" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnSync" runat="server" ToolTip="Refresh" OnClick="btnSync_Click"
                                    Style="margin-left: 20px;"> <i class="fa fa-2x fa-refresh" style="color: #333333;" title="Refresh"></i> </asp:LinkButton>
                            </div>
                            <br />
                            <div class="row">
                                <asp:GridView runat="server" ID="gvMitigation" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                    OnRowCommand="gvMitigation_RowCommand" OnRowDataBound="gvMitigation_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                            HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>' CommandArgument='<%#Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cause" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCause" runat="server" Text='<%# Bind("Cause") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mitigation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMitigation" runat="server" Text='<%# Bind("Mitigation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Primary Responsible Person" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrimary_RES" runat="server" Text='<%# Bind("Primary_RES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Secondary Responsible Person" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSecondary_RES" runat="server" Text='<%# Bind("Secondary_RES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date By" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Complete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateComplete" Width="100%" runat="server" Text='<%# Bind("Date_Complete") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div style="display: inline-flex;">
                                                    <asp:LinkButton ID="lbtncompleteMit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="Complete" ToolTip="Complete"><i class="fa fa-check"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtnupdateMit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="Edit1" OnClientClick="return Mitigation(this, 'UPDATE');" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lbtndeleteMit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                        CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="tab-4" class="tab-content">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="row">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="tab-5" class="tab-content current">
                    <asp:GridView ID="grdReviewHistory" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable txtCenter" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnPreRender="GridView_PreRender">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="50%" ItemStyle-Width="50%">
                                <ItemTemplate>
                                    <asp:Label ID="lblusername" runat="server" Text='<%# Bind("USERDISNAME") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DateTime" ItemStyle-CssClass="txt_center" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldatetime" runat="server" Text='<%# Bind("LOGDATETIME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-6" class="tab-content">
                    <asp:GridView ID="grdCmts" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdCmts_RowDataBound">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Comment" runat="server" Text='<%# Bind("Comment") %>' CssClass="form-control"
                                        TextMode="MultiLine" Width="100%" Height="40px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EnteredDate" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="DTENTERED" Text='<%# Bind("DTENTERED") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EnteredBy" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="ENTEREDBY" Text='<%# Bind("ENTEREDBY") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_cmt" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_cmt" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_cmt") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="bntCommentAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntCommentAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
