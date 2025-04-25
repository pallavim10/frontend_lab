<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_REVIEW.aspx.cs" Inherits="CTMS.DB_REVIEW" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/CKEditor/ckeditor.js"></script>
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <%--<script src="CommonFunctionsJs/Datatable1.js"></script>--%>
    <script type="text/javascript" src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/callChange_Review.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/showChild.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DB/PARENT_CHILD_LINKED.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }

        .Mandatory {
            border: solid 1px !important;
            border-color: Red !important;
        }

        .REQUIRED {
            background-color: yellow;
        }

        .highlight-border {
            border: 2px solid #3291e3; /* Example border style */
            /*background-color: #115ea1;*/ /* Example background color */
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }

        function OpenModule(element) {
            var MODULEID = $(element).closest('tr').find('td:eq(7)').find('span').text();
            var MODULENAME = $(element).closest('tr').find('td:eq(8)').find('span').text();

            if (MODULEID != "0") {
                var test = "DM_Mappings.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }
    </script>
    <%--<script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" runat="server" id="content">
        <div class="box-header">
            <h3 class="box-title">Module Reviews
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>Select System :</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="drpSystem" Style="width: 250px;" CssClass="form-control required1" AutoPostBack="true"
                        OnSelectedIndexChanged="drpSystem_SelectedIndexChanged" TabIndex="1">
                        <%--<asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="eSource" Value="eSource"></asp:ListItem>
                        <asp:ListItem Text="IWRS" Value="IWRS"></asp:ListItem>
                        <asp:ListItem Text="Pharmacovigilance" Value="Pharmacovigilance"></asp:ListItem>
                        <asp:ListItem Text="Data Management" Value="Data Management"></asp:ListItem>
                        <asp:ListItem Text="Protocol Deviation" Value="Protocol Deviation"></asp:ListItem>
                        <asp:ListItem Text="External/Independent" Value="External/Independent"></asp:ListItem>
                        <asp:ListItem Text="ePRO" Value="ePRO"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label>
                        Select Module:</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList Style="width: 250px;" ID="drpModule" runat="server" class="form-control drpControl required1 select"
                        AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged" TabIndex="2">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnShowReview" runat="server" Text="Show" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnShowReview_Click" TabIndex="3" />
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-danger">
        <div class="row" style="margin-top: 5px;">
            <div class="col-md-12">
                <div class="col-md-11">
                    <asp:Label runat="server" ID="lblReview" ForeColor="Blue" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body" style="margin-left: 2%; margin-right: 2%;">
            <div class="form-group">
                <asp:Label ID="lblModuleName" runat="server" Visible="false" Text="" Font-Size="12px" Font-Bold="true"
                    Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False" EmptyDataText="No Record Available."
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
                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                <div class="col-md-12" id="divcontrol" runat="server">
                                    <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                        OnClientClick="OpenModule(this);">
                                    </asp:LinkButton>
                                    <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                        Width="200px" TabIndex="4">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);" TabIndex="5"></asp:TextBox>
                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                    ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' TabIndex="6" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                    onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                    Text='<%# Bind("TEXT") %>' TabIndex="7" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                        runat="server"></asp:Label>
                                    <div id="divDisplayFeatures" runat="server" visible="false">
                                        <label style="color: deeppink;">Display Features: </label>
                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                    </div>
                                    <div id="divDataSignificance" runat="server" visible="false">
                                        <label style="color: deeppink;">Data Significance: </label>
                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                    </div>
                                    <div id="divDataLinkages" runat="server" visible="false">
                                        <label style="color: deeppink;">Data Linkages: </label>
                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                    </div>
                                    <div id="divMultipleDataEntry" runat="server" visible="false">
                                        <label style="color: deeppink;">Multiple Data Entry: </label>
                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick='<%# "return showAuditTrail_Review(\"DM_REVIEW_LOGS\", \"" + Eval("ID") + "\");" %>' ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
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
                                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                                                <div class="col-md-12" id="divcontrol" runat="server">
                                                                    <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                        OnClientClick="OpenModule(this);">
                                                                    </asp:LinkButton>
                                                                    <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                                        Width="200px">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);"></asp:TextBox>
                                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-4">
                                                                                <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                                                    ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' TabIndex="8" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-4">
                                                                                <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                    onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                    Text='<%# Bind("TEXT") %>' TabIndex="9" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                        runat="server"></asp:Label>
                                                                    <div id="divDisplayFeatures" runat="server" visible="false">
                                                                        <label style="color: deeppink;">Display Features: </label>
                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div id="divDataSignificance" runat="server" visible="false">
                                                                        <label style="color: deeppink;">Data Significance: </label>
                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div id="divDataLinkages" runat="server" visible="false">
                                                                        <label style="color: deeppink;">Data Linkages: </label>
                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div id="divMultipleDataEntry" runat="server" visible="false">
                                                                        <label style="color: deeppink;">Multiple Data Entry: </label>
                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
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
                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick='<%# "return showAuditTrail_Review(\"DM_REVIEW_LOGS\", \"" + Eval("ID") + "\");" %>' ToolTip="Audit Trail">
                                                                    <i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i>
                                                                </asp:LinkButton>
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
                                                                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>
                                                                                                &nbsp;&nbsp;
                                                                                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                                                                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                                                                                <div class="col-md-12" id="divcontrol" runat="server">
                                                                                                    <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                        OnClientClick="OpenModule(this);">
                                                                                                    </asp:LinkButton>
                                                                                                    <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                                                                        Width="200px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);"></asp:TextBox>
                                                                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="col-md-4">
                                                                                                                <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                                                                                    ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="col-md-4">
                                                                                                                <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                    onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                    Text='<%# Bind("TEXT") %>' TabIndex="10" />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </div>
                                                                                                <div class="col-md-12">
                                                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                                                        runat="server"></asp:Label>
                                                                                                    <div id="divDisplayFeatures" runat="server" visible="false">
                                                                                                        <label style="color: deeppink;">Display Features: </label>
                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div id="divDataSignificance" runat="server" visible="false">
                                                                                                        <label style="color: deeppink;">Data Significance: </label>
                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div id="divDataLinkages" runat="server" visible="false">
                                                                                                        <label style="color: deeppink;">Data Linkages: </label>
                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div id="divMultipleDataEntry" runat="server" visible="false">
                                                                                                        <label style="color: deeppink;">Multiple Data Entry: </label>
                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
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
                                                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick='<%# "return showAuditTrail_Review(\"DM_REVIEW_LOGS\", \"" + Eval("ID") + "\");" %>' ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
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
                                                                                                                                <asp:Label ID="lblSEQNO" Text='<%# Eval("SEQNO") + "." %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true"></asp:Label>
                                                                                                                                &nbsp;&nbsp;
                                                                                                                                <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Eval("Descrip") %>'
                                                                                                                                    Text='<%# Eval("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                <asp:Label ID="VARIABLENAME" Text='<%# " [" +  Eval("VARIABLENAME") + "]" %>' runat="server" Font-Size="Small" Style="text-align: LEFT" Font-Bold="true" ForeColor="DarkOrange"></asp:Label>
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
                                                                                                                                <div class="col-md-12" id="divcontrol" runat="server">
                                                                                                                                    <asp:Label ID="lblFieldName1" Text='<%# Bind("FIELDNAME") %>' Visible="false" runat="server"></asp:Label>
                                                                                                                                    <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                        OnClientClick="OpenModule(this);">
                                                                                                                                    </asp:LinkButton>
                                                                                                                                    <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild_Review(this);" Visible="false"
                                                                                                                                        Width="200px" TabIndex="11">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                                                                                                                    <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild_Review(this);" TabIndex="12"></asp:TextBox>
                                                                                                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild_Review(this);" CssClass="checkbox"
                                                                                                                                                    ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' TabIndex="13" />
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:Repeater>
                                                                                                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                    onchange="showChild_Review(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                                                    Text='<%# Bind("TEXT") %>' TabIndex="14" />
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:Repeater>
                                                                                                                                </div>
                                                                                                                                <div class="col-md-12">
                                                                                                                                    <asp:Label ID="lbltextType" Font-Size="14px" Font-Bold="true" Text='<%# "["+ Eval("CONTROLS") + "]" %>' ForeColor="Maroon"
                                                                                                                                        runat="server"></asp:Label>
                                                                                                                                    <div id="divDisplayFeatures" runat="server" visible="false">
                                                                                                                                        <label style="color: deeppink;">Display Features: </label>
                                                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDisplayFeature" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                    </div>
                                                                                                                                    <div id="divDataSignificance" runat="server" visible="false">
                                                                                                                                        <label style="color: deeppink;">Data Significance: </label>
                                                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataSignificance" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                    </div>
                                                                                                                                    <div id="divDataLinkages" runat="server" visible="false">
                                                                                                                                        <label style="color: deeppink;">Data Linkages: </label>
                                                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDataLinkages" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                    </div>
                                                                                                                                    <div id="divMultipleDataEntry" runat="server" visible="false">
                                                                                                                                        <label style="color: deeppink;">Multiple Data Entry: </label>
                                                                                                                                        &nbsp;&nbsp;
                                                                    <asp:Label ID="lblMultipleDataEntry" Font-Size="14px" Font-Bold="true" ForeColor="darkviolet" runat="server"></asp:Label>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
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

                                                                                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick='<%# "return showAuditTrail_Review(\"DM_REVIEW_LOGS\", \"" + Eval("ID") + "\");" %>' ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
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
            </div>
            <div align="center">
                <asp:Button ID="btnReview" runat="server" CssClass="btn btn-DARKORANGE btn-sm" Text="Review" OnClick="btnReview_Click" Visible="false" TabIndex="15" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBackToDesigner" runat="server" CssClass="btn btn-deepskyblue btn-sm" Text="Send Back To Designer" OnClick="btnBackToDesigner_Click" Visible="false" TabIndex="16" />
            </div>
            <br />
        </div>
    </div>
    <div class="box box-primary" id="divOnChangeCrit" runat="server" visible="false">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdid2');" id="_Folder2"><i id="imggrdid2" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="OnChange Criterias"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdid2" style="display: none">
            <div class="box">
                <div align="left" style="margin-left: 5px">
                    <div>
                        <div class="rows">
                            <div class="fixTableHead">
                                <asp:GridView ID="grdOnChangeCrit" runat="server" AutoGenerateColumns="false" HeaderStyle-ForeColor="Maroon" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered table-striped Datatable">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No." ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Criteria">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Derived">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsDerived" Text='<%# Bind("ISDERIVED") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Derived Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsDerivedValue" Text='<%# Bind("ISDERIVED_VALUE") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-info" id="divOnsubmitCrit" runat="server" visible="false">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdid1');" id="_Folder1"><i id="imggrdid1" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="OnSubmit Criterias"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdid1" style="display: none">
            <div class="box">
                <div align="left" style="margin-left: 5px">
                    <div>
                        <div class="rows">
                            <div class="fixTableHead">
                                <asp:GridView ID="grdOnSubmitCrit" runat="server" AutoGenerateColumns="False" HeaderStyle-ForeColor="Maroon" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered table-striped Datatable">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sequence No." ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit">
                                            <ItemTemplate>
                                                <asp:Label ID="VISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Label ID="ACTIONS" Text='<%# Bind("ACTIONS") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Criteria">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Error Message">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-danger" id="divModuleCrit" runat="server" visible="false">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdModuleCrit');" id="_Folder3"><i id="imggrdModuleCrit" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Module Visibility Criterias"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdModuleCrit" style="display: none">
            <div class="box">
                <div class="fixTableHead">
                    <asp:GridView ID="grdModuleCrits" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCritName" Text='<%# Bind("NAME") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visit">
                                <ItemTemplate>
                                    <asp:Label ID="VISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Criteria">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                            <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-success" id="divLabDefaults" runat="server" visible="false">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grddivLabDefaults');" id="_Folder4"><i id="imggrddivLabDefaults" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="Lab Default Mapping"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grddivLabDefaults" style="display: none">
            <div class="box">
                <div class="fixTableHead">
                    <asp:GridView ID="grdLabDefaults" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Visit" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="VISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Primary Date Visit">
                                <ItemTemplate>
                                    <asp:Label ID="P_VISIT" Text='<%# Bind("P_VISIT") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Primary Date Module">
                                <ItemTemplate>
                                    <asp:Label ID="P_MODULE" Text='<%# Bind("P_MODULE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Primary Date Field">
                                <ItemTemplate>
                                    <asp:Label ID="P_FIELD" Text='<%# Bind("P_FIELD") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Secondary Date Module">
                                <ItemTemplate>
                                    <asp:Label ID="S_MODULE" Text='<%# Bind("S_MODULE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Secondary Date Field">
                                <ItemTemplate>
                                    <asp:Label ID="S_FIELD" Text='<%# Bind("S_FIELD") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sex Date Module">
                                <ItemTemplate>
                                    <asp:Label ID="SEX_MODULE" Text='<%# Bind("SEX_MODULE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sex Date Field">
                                <ItemTemplate>
                                    <asp:Label ID="SEX_FIELD" Text='<%# Bind("SEX_FIELD") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Age Unit">
                                <ItemTemplate>
                                    <asp:Label ID="AGE_UNIT" Text='<%# Bind("AGE_UNIT") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lab Id Column">
                                <ItemTemplate>
                                    <asp:Label ID="LABID_COL" Text='<%# Bind("LABID_COL") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lower Limit Column">
                                <ItemTemplate>
                                    <asp:Label ID="LL_COL" Text='<%# Bind("LL_COL") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Upper Limit Column">
                                <ItemTemplate>
                                    <asp:Label ID="UL_COL" Text='<%# Bind("UL_COL") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Column">
                                <ItemTemplate>
                                    <asp:Label ID="UNIT_COL" Text='<%# Bind("UNIT_COL") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                            <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left" ItemStyle-Width="20%">
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-primary" id="divCoding" runat="server" visible="false">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdcode');" id="_Folder5"><i id="imggrdcode" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="Code Mapping"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdcode" style="display: none">
            <div class="row" id="divMeddraRecoard" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">MedDRA Records</h4>
                        </div>
                        <br />
                        <div align="left" style="margin-left: 5px">
                            <div>
                                <div class="rows">
                                    <div class="fixTableHead">
                                        <asp:GridView ID="grdMeddraData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="border-collapse: collapse; width: 99%;" OnPreRender="grdMeddraData_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="System Organ Class Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SOCNM" runat="server" Text='<%# Bind("SOCNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="System Organ Class code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SOCCD" runat="server" Text='<%# Bind("SOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="High Level Group Term Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="HLGTNM" runat="server" Text='<%# Bind("HLGTNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="High Level Group Term code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="HLGTCD" runat="server" Text='<%# Bind("HLGTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="High Level Term Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="HLTNM" runat="server" Text='<%# Bind("HLTNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="High Level Term code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="HLTCD" runat="server" Text='<%# Bind("HLTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Peferred Term Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PTNM" runat="server" Text='<%# Bind("PTNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Peferred Term code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PTCD" runat="server" Text='<%# Bind("PTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lowest Level Term Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LLTNM" runat="server" Text='<%# Bind("LLTNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lowest Level Term code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LLTCD" runat="server" Text='<%# Bind("LLTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dictionary Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DICNM" runat="server" Text='<%# Bind("DICNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dictionary Version" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DICNO" runat="server" Text='<%# Bind("DICNO") %>'></asp:Label>
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
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="divWhoddRecoard" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">WHODD Records</h4>
                        </div>
                        <br />
                        <div align="left" style="margin-left: 5px">
                            <div>
                                <div class="rows">
                                    <div class="fixTableHead">
                                        <asp:GridView ID="grdWhoddData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="border-collapse: collapse; width: 99%;" OnPreRender="grdWhoddData_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                    ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 1 Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC1C" runat="server" Text='<%# Bind("CMATC1C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 1 code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC1CD" runat="server" Text='<%# Bind("CMATC1CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 2 Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC2C" runat="server" Text='<%# Bind("CMATC2C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 2 code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC2CD" runat="server" Text='<%# Bind("CMATC2CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 3 Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC3C" runat="server" Text='<%# Bind("CMATC3C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 3 code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC3CD" runat="server" Text='<%# Bind("CMATC3CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 4 Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC4C" runat="server" Text='<%# Bind("CMATC4C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATC Level 4 code Column" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC4CD" runat="server" Text='<%# Bind("CMATC4CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Drug name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC5C" runat="server" Text='<%# Bind("CMATC5C") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Drug code" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CMATC5CD" runat="server" Text='<%# Bind("CMATC5CD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dictionary Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DICNM" runat="server" Text='<%# Bind("DICNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dictionary Version" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DICNO" runat="server" Text='<%# Bind("DICNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <HeaderTemplate>
                                                        <label>Entered By Details </label>
                                                        <br />
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
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-cyan" id="divReviewLogs" runat="server" visible="false">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                <a href="JavaScript:divexpandcollapse('grdid');" id="_Folder"><i id="imggrdid" class="ion-plus-circled" style="font-size: larger; color: #666666"></i></a>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblHeader" runat="server" ForeColor="Blue" Text="Review Logs"></asp:Label>
            </h3>
        </div>
        <div class="box-body" id="grdid" style="display: none">
            <div class="box">
                <div class="fixTableHead">
                    <asp:GridView ID="gridreviewlogs" HeaderStyle-CssClass="text_center" HeaderStyle-ForeColor="Maroon" runat="server" OnPreRender="gridsigninfo_PreRender" CssClass="table table-bordered table-striped Datatable" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlReview" TargetControlID="lnkReason"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlReview" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="lnkReason" Style="display: none" />
        <h5 class="heading">Review</h5>
        <div class="modal-body" runat="server" style="padding: 10px;">
            <div id="ModelPopup2">
                <div class="row">
                    <div style="width: 100%; height: auto; overflow: auto;">
                        <div>
                            <label id="lblReviewHeader" runat="server" style="color: blue; margin-left: 3%;"></label>
                        </div>
                        <asp:ListView ID="lstSystems" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblSystemName" Text='<%# Bind("SystemName") %>' ForeColor="Maroon"></asp:Label>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtReason" placeholder="Please enter comment to review module for above systems...." TextMode="MultiLine" MaxLength="500" CssClass="form-control required2" Style="width: 575px; height: 131px;"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="btnSubmitReview" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave2"
                            Text="Submit" OnClick="btnSubmitReview_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlBackToDesign" TargetControlID="Button1"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlBackToDesign" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="Button1" Style="display: none" />
        <h5 class="heading">Send Back To Designer</h5>
        <div class="modal-body" runat="server" style="padding: 10px;">
            <div id="ModelPopup3">
                <div class="row">
                    <div style="width: 100%; height: auto; overflow: auto;">
                        <div>
                            <label id="lblBackToDesigner" runat="server" style="color: blue; margin-left: 3%;"></label>
                        </div>
                        <asp:ListView ID="lstBackToDesigner" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblSystemName" Text='<%# Bind("SystemName") %>' ForeColor="Maroon"></asp:Label>
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <asp:TextBox runat="server" ID="txtBackToDesinerComment" placeholder="Please enter comment for send back to designer module for above systems...." TextMode="MultiLine"
                                CssClass="form-control required3" MaxLength="500" Style="width: 575px; height: 131px;">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="btnSubmitUnReview" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;"
                            CssClass="btn btn-DarkGreen btn-sm cls-btnSave3" Text="Submit" OnClick="btnSubmitUnReview_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelUnReview" runat="server" Text="Cancel"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelUnReview_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
