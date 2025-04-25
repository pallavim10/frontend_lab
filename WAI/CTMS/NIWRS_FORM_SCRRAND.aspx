<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_FORM_SCRRAND.aspx.cs" Inherits="CTMS.NIWRS_FORM_SCRRAND" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/DataChange.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/ShowChild.js"></script>
    <script src="CommonFunctionsJs/IWRS/CallChange.js"></script>
    <script src="CommonFunctionsJs/RadioCheck.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <link href="CommonStyles/EntryGrid.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/DisableButton.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" onkeydown="tab(event);">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </h3>
        </div>
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px; margin-left: 7px"></asp:Label>
        <div class="box-body">
            <div style="display: inline-flex;">
                <div runat="server" id="divDDLS">
                    <div class="form-group" runat="server" id="divSite" style="display: inline-flex">
                        <label class="label">
                            Select Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpSite" runat="server" onchange="setSiteStrata(); setScreeningID();"
                                CssClass="form-control required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="form-group" runat="server" id="divScreeningID" style="display: inline-flex">
                        <label class="label">
                            Enter &nbsp;<asp:Label runat="server" ID="SUBJECTTEXT"></asp:Label>:
                        </label>
                        <div class="Control">
                            <asp:TextBox ID="txtScreeningID" MaxLength="6" runat="server" CssClass="form-control required"
                                AutoPostBack="true" OnTextChanged="txtScreeningID_TextChanged" onkeydown="return validateSubjectID(event);"></asp:TextBox>
                            
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="form-group">
                <asp:HiddenField runat="server" ID="hfFormCode" />
                <asp:HiddenField runat="server" ID="hfMODULEID" />
                <asp:HiddenField runat="server" ID="hfTablename" />
                <asp:HiddenField runat="server" ID="hfRANDID" />
                <asp:HiddenField runat="server" ID="hfRANDTREATGRP" />
                <asp:HiddenField runat="server" ID="hfSUBJID" />
                <asp:HiddenField runat="server" ID="hfSITEID" />
                <asp:HiddenField runat="server" ID="hfSUBSITEID" />
                <asp:HiddenField runat="server" ID="hfDM_SYNC" />
                <asp:HiddenField runat="server" ID="hfDM_Tablename" />
                <asp:HiddenField runat="server" ID="hfDM_MODULEID" />
                <asp:HiddenField runat="server" ID="hfApplVisit" />
                <asp:HiddenField runat="server" ID="hfSCR_NA" />
                <asp:HiddenField runat="server" ID="hfCOUNTRY" />
                <div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                ShowHeader="False" CssClass="table table-bordered table-striped ShowChild1" OnRowDataBound="grd_Data_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                        ControlStyle-CssClass="label" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this); DATA_Changed(this);" CssClass="RunChecks " Visible="false" Width="200px">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                    CssClass="RunChecks" onchange="showChild(this); DATA_Changed(this);"></asp:TextBox>

                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this); DATA_Changed(this);"
                                                                CssClass="checkbox RunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                onchange=" showChild(this); DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                CssClass="radio RunChecks" Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
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
                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                    CssClass="RunChecks" Visible="false" Width="200px">
                                                                                </asp:DropDownList>
                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                                                    CssClass="RunChecks" onchange=" showChild(this); DATA_Changed(this);"
                                                                                    onfocus="myFocus(this)"></asp:TextBox>

                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                CssClass="checkboxRunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                onchange=" showChild(this);  DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                                                CssClass="radio RunChecks" Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
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
                                                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                    CssClass="RunChecks" Visible="false" Width="200px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                                                                                    CssClass="RunChecks" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                    onfocus="myFocus(this)"></asp:TextBox>

                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                CssClass=" checkboxRunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                onchange=" showChild(this);  DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                                                                                CssClass="radioRunChecks" Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                                                                                                                                            <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
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
                                                                                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                                    CssClass="RunChecks" Visible="false" Width="200px">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                                                                                                                    CssClass="RunChecks" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                                    onfocus="myFocus(this)"></asp:TextBox>

                                                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange=" showChild(this); DATA_Changed(this);"
                                                                                                                                                                CssClass="checkboxRunChecks" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                onchange=" showChild(this);   DATA_Changed(this);" onfocus="myFocus(this)" onclick="return RadioCheck(this);"
                                                                                                                                                                CssClass="radioRunChecks" Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_OLD_VALUE" />
                                                                                                                                                <asp:Button runat="server" ID="btnDATA_Changed" CssClass="disp-none" OnClick="DATA_Changed"></asp:Button>
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="STRATA" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblSTRATA" Text='<%# Bind("STRATA") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="IWRS_DM_SYNC" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="IWRS_DM_SYNC" Text='<%# Bind("IWRS_DM_SYNC") %>' runat="server"></asp:Label>
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
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-8 txt_center">
                            <asp:Button ID="btnsubmit" Text="Submit" runat="server"
                                CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btncancel_Click" />
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
