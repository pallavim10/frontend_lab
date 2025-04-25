<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_NEW_LOG.aspx.cs" Inherits="CTMS.NSAE_NEW_LOG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script src="js/CKEditor/ckeditor.js"></script>
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <script src="CommonFunctionsJs/ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/Datatable1.js"></script>
    <script src="CommonFunctionsJs/DisableButton.js"></script>
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_CallChange.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_DataChange.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_AuditTrail.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_ShowChild.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_OnChangeCrit.js"></script>
    <script language="javascript">
        $(document).ready(function () {

            if ($('#MainContent_hdn_PAGESTATUS').val() != '0') {

                $(".Comments").removeClass("disp-none");

            }

            FillAuditDetails();
            FillCommentsDetails();
        });

        //onfocus of any control this function will call    
        function myFocus() {

            if ($("#MainContent_hdn_PAGESTATUS").val() == '1') {

                $('#MainContent_bntSaveComplete').prop('disabled', true);

            }

            if ($("#MainContent_hdn_PAGESTATUS").val() == '1' && $("#MainContent_hdnRECID").val() != "-1") {

                $('#MainContent_bntSaveComplete').prop('disabled', true);

            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">Log New SAE &nbsp;||&nbsp;
                <asp:Label runat="server" ID="lblSiteId" />&nbsp;||&nbsp;<asp:Label runat="server"
                    ID="lblSubjectId" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblSAE" />&nbsp;||&nbsp;<asp:Label
                        runat="server" ID="lblSAEID" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblStatus" />&nbsp;
            </h3>
            <asp:GridView ID="SAE_grdOnPageSpecs" runat="server" AutoGenerateColumns="False" CellPadding="4"
                font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
                Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="CritCode">
                        <ItemTemplate>
                            <asp:TextBox ID="CritCode" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("CritCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ERR_MSG">
                        <ItemTemplate>
                            <asp:TextBox ID="ERR_MSG" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("ERR_MSG") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Restricted">
                        <ItemTemplate>
                            <asp:TextBox ID="Restricted" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("Restricted") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ISDERIVED">
                        <ItemTemplate>
                            <asp:TextBox ID="ISDERIVED" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("ISDERIVED") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ISDERIVED_VALUE">
                        <ItemTemplate>
                            <asp:TextBox ID="ISDERIVED_VALUE" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("ISDERIVED_VALUE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SETFIELDID">
                        <ItemTemplate>
                            <asp:TextBox ID="SETFIELDID" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("SETFIELDID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SETVALUEDATA">
                        <ItemTemplate>
                            <asp:TextBox ID="SETVALUEDATA" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("SETVALUEDATA") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VARIABLENAME">
                        <ItemTemplate>
                            <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("VARIABLENAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="SAE_grdAUDITTRAILDETAILS" runat="server" AutoGenerateColumns="False"
                CellPadding="4" font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
                Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="VARIABLE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REASON">
                        <ItemTemplate>
                            <asp:TextBox ID="REASON" runat="server" font-family="Arial" Font-Size="X-Small" Text='<%# Bind("REASON") %>'
                                Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="SAE_grdComments" runat="server" AutoGenerateColumns="False" CellPadding="4"
                font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
                Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="VARIABLE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="navbar-custom-menu navbar-right" id="divhelp" runat="server">
                <ul class="nav navbar-nav">
                    <li class="dropdown notifications-menu"><a href="#" class="dropdown-toggle" data-toggle="dropdown"
                        style="padding-top: 0px; padding-bottom: 0px;" aria-expanded="false">
                        <asp:Label ID="Label1" runat="server" ToolTip="Help"><i class="fa fa-info-circle"
                                style="font-size: 20px;"></i></asp:Label></a>
                        <ul class="dropdown-menu" style="width: 500px;">
                            <li class="box-header">
                                <h3 class="box-title" style="width: 100%;">
                                    <label>
                                        CRF Filling Instructions</label></h3>
                            </li>
                            <li>
                                <!-- inner menu: contains the messages -->
                                <div style="width: 100%; height: 400px; overflow: auto;">
                                    <ul class="menu" style="overflow: hidden; width: 100%; height: auto;">
                                        <div class="box-body" style="text-align: left; padding-left: 10px; padding-right: 10px;">
                                            <asp:Literal ID="LiteralText" runat="server"></asp:Literal>
                                        </div>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:HiddenField runat="server" ID="hfTablename" />
                <asp:HiddenField runat="server" ID="hdnSAEID" />
                <asp:HiddenField runat="server" ID="hdnSAE" />
                <asp:HiddenField runat="server" ID="hdnRECID" Value="0" />
                <asp:HiddenField runat="server" ID="hdnnextvalue" />
                <asp:HiddenField runat="server" ID="hdnPrevValue" />
                <asp:HiddenField runat="server" ID="hdnNextClick" />
                <asp:HiddenField runat="server" ID="hdn_PAGESTATUS" />
                <asp:HiddenField runat="server" ID="hdnIsComplete" />
                <asp:HiddenField runat="server" ID="hdnActions" Value="Log New SAE" />
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300;"></asp:Label>
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        Module:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpModule" runat="server" ForeColor="Blue"
                            CssClass="form-control" Enabled="false" Style="width: 100%">
                        </asp:DropDownList>
                    </div>
                </div>
                <table class="style1">
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                ShowHeader="False" CssClass="table table-bordered table-striped ShowChild1" OnRowDataBound="grd_Data_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <div class="col-md-12">
                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>

                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server" onchange="showChild(this); checkOnChangeCrit(this);" Visible="false">
                                                </asp:DropDownList>

                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>

                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="rightClick" onmousedown="myFocus();"
                                                    onchange="showChild(this); checkOnChangeCrit(this);"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />

                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox rightClick" onmousedown="myFocus();"
                                                                onchange="showChild(this); checkOnChangeCrit(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                onclick="RadioCheck(this);  checkOnChangeCrit(this);"
                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                        <asp:Button runat="server" ID="btnRightClick_Changed" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnRightClick_Changed" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                    <div style="float: right; font-size: 13px;">
                                                    </div>
                                                    <div>
                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                            <asp:GridView ID="grd_Data1" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                ShowHeader="False" CssClass="table table-bordered table-striped table-striped1 ShowChild2"
                                                                OnRowDataBound="grd_Data1_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-12">

                                                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>

                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server"
                                                                                    onchange="showChild(this);  checkOnChangeCrit(this);" Visible="false">
                                                                                </asp:DropDownList>

                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass=" rightClick" onmousedown="myFocus();"
                                                                                    onchange="showChild(this); checkOnChangeCrit(this);"></asp:TextBox>

                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />

                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox rightClick" onmousedown="myFocus();"
                                                                                                onchange="showChild(this); checkOnChangeCrit(this);"
                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval ("color").ToString()) %>'
                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                onclick="RadioCheck(this);  checkOnChangeCrit(this);"
                                                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                    <ContentTemplate>
                                                                                        <asp:Button runat="server" ID="btnDATA_Changed1" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                                                        <asp:Button runat="server" ID="btnRightClick_Changed1" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed1" EventName="Click" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="btnRightClick_Changed1" EventName="Click" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                    <div style="float: right; font-size: 13px;">
                                                                                    </div>
                                                                                    <div>
                                                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                                                            <asp:GridView ID="grd_Data2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                ShowHeader="False" CssClass="table table-bordered table-striped table-striped2 ShowChild3"
                                                                                                OnRowDataBound="grd_Data2_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="col-md-12">
                                                                                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server" onchange="showChild(this); checkOnChangeCrit(this);" Visible="false">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass=" rightClick" onmousedown="myFocus();"
                                                                                                                    onchange="showChild(this); checkOnChangeCrit(this);"></asp:TextBox>

                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />

                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                CssClass="checkbox rightClick" onmousedown="myFocus();" onchange="showChild(this); checkOnChangeCrit(this);"
                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                onclick="RadioCheck(this);checkOnChangeCrit(this);"
                                                                                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:Button runat="server" ID="btnDATA_Changed2" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                                                                                        <asp:Button runat="server" ID="btnRightClick_Changed2" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed2" EventName="Click" />
                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnRightClick_Changed2" EventName="Click" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                                    <div style="float: right; font-size: 13px;">
                                                                                                                    </div>
                                                                                                                    <div>
                                                                                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                                                                                            <asp:GridView ID="grd_Data3" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                                                ShowHeader="False" CssClass="table table-bordered table-striped table-striped3 ShowChild4"
                                                                                                                                OnRowDataBound="grd_Data3_RowDataBound">
                                                                                                                                <Columns>
                                                                                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                                                runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                                                                                Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                                                                <asp:Label ID="lblFieldNameFrench" Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" onmousedown="myFocus();" runat="server" onchange="showChild(this); checkOnChangeCrit(this);" Visible="false">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="rightClick" onmousedown="myFocus();"
                                                                                                                                                    onchange="showChild(this); checkOnChangeCrit(this);"></asp:TextBox>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />

                                                                                                                                                <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server"
                                                                                                                                                                CssClass="checkbox rightClick" onmousedown="myFocus();" onchange="showChild(this); checkOnChangeCrit(this);"
                                                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" onchange="showChild(this);"
                                                                                                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                onclick="RadioCheck(this);  checkOnChangeCrit(this);"
                                                                                                                                                                CssClass="radio rightClick" onmousedown="myFocus();" Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                                                                                    <ContentTemplate>
                                                                                                                                                        <asp:Button runat="server" ID="btnDATA_Changed3" CssClass="disp-none btnDATA_Changed" OnClick="DATA_Changed"></asp:Button>
                                                                                                                                                        <asp:Button runat="server" ID="btnRightClick_Changed3" CssClass="disp-none btnRightClick_Changed" OnClick="btnRightClick"></asp:Button>
                                                                                                                                                    </ContentTemplate>
                                                                                                                                                    <Triggers>
                                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnDATA_Changed3" EventName="Click" />
                                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnRightClick_Changed3" EventName="Click" />
                                                                                                                                                    </Triggers>
                                                                                                                                                </asp:UpdatePanel>
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AQ" ToolTip="Open Query" OnClientClick="return SAE_ShowOpenQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:maroon"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AWSQ" ToolTip="Answered Open Query" OnClientClick="return SAE_ShowAnsQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:17px;color:blue"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CQ" ToolTip="Closed Query" OnClientClick="return SAE_ShowClosedQuery(this);" class="disp-none" runat="server">
                                                <i class="fa fa-question-circle" style="font-size:18px;color:darkgreen"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="CM" ToolTip="Field Comments" OnClientClick="return SAE_ShowComments(this);" runat="server" class="disp-none Comments">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:LinkButton ID="AD" ToolTip="Audit trail" OnClientClick="return SAE_showAuditTrail(this);" class="disp-none" runat="server">
                                                <i class="fa fa-history" style="font-size:17px"></i>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="READYN" Text='<%# Bind("READYN") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="SYNC_COUNT" Text='<%# Bind("SYNC_COUNT") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                                            <asp:Label ID="AutoNum" Text='<%# Bind("AutoNum") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                </Columns>
                                                                                                                            </asp:GridView>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="pull-left">
                            <asp:Button ID="btnBacktoHomePage" runat="server" Text="Back to Main Page" CssClass="btn btn-sm btn-deepskyblue"
                                OnClick="btnBacktoHomePage_Click"></asp:Button>
                            <asp:Button ID="btnPrev" runat="server" Text="Previous" CssClass="btn btn-primary btn-danger"
                                OnClick="btnPrev_Click" />
                        </td>
                        <td class="pull-right">
                            <asp:Button ID="bntSaveComplete" runat="server" Text="Save Complete" OnClick="bntSaveComplete_Click"
                                CssClass="btn btn-DarkGreen btn-sm cls-btnSave" />
                            <asp:Button ID="btnNext" runat="server" Text="Next" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnNext_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button_Popup"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup1" Style="display: none">
        <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
        <asp:UpdatePanel ID="updPnlIDDetail" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h5 class="heading">Reason for Change</h5>
                <div class="disp-none">
                    <asp:Label ID="txt_TableName" runat="server"></asp:Label>
                    <asp:Label ID="txt_VariableName" runat="server"></asp:Label>
                </div>
                <div class="modal-body" runat="server">
                    <div id="ModelPopup">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Module Name" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="txt_ModuleName" CssClass="form-control-model" ValidationGroup="Update_AuditTrail" runat="server" Style="min-width: 250px;"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label8" runat="server" CssClass="wrapperLable" Text="Field Name" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="txt_FieldName" CssClass="form-control-model" ValidationGroup="Update_AuditTrail" runat="server" Style="min-width: 250px;"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label9" runat="server" CssClass="wrapperLable" Text="Old value" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="txt_OldValue" Enabled="false" CssClass="form-control-model" ValidationGroup="Update_AuditTrail" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="Label10" runat="server" CssClass="wrapperLable" Text="New value" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:Label ID="txt_NewValue" CssClass="form-control-model" ValidationGroup="Update_AuditTrail" runat="server" Style="overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label44" runat="server" CssClass="wrapperLable" Text="Reason" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="drp_Reason" CssClass="form-control-model wrapperLable" ValidationGroup="Update_AuditTrail" runat="server" Style="width: 250px;">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="Data entry error">Data entry error</asp:ListItem>
                                <asp:ListItem Value="Updated data available">Updated data available</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txt_Comments" CssClass="form-control-model" ValidationGroup="Update_AuditTrail" TextMode="MultiLine"
                                runat="server" Style="width: 250px;"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-9">
                            <asp:Button ID="btn_Update" runat="server" Style="margin-left: 37px; height: 34px; font-size: 14px;" ValidationGroup="Update_AuditTrail" CssClass="btn btn-DarkGreen"
                                OnClientClick="return Check_ReasonEntered();" Text="Update Data" OnClick="btn_Update_Click" />
                            &nbsp;
                            &nbsp;
                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" ValidationGroup="Update_AuditTrail"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
