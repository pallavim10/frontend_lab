<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_ClassifiedDocs.aspx.cs" Inherits="CTMS.eTMF_ClassifiedDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .label
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Classify Documents</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hfVerDATE" />
                <asp:HiddenField runat="server" ID="hfVerTYPE" />
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row" id="divmapto" runat="server" style="margin-top: 10px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Classify Using : &nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAction" runat="server" class="form-control drpControl required width250px"
                                OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" AutoPostBack="true">
                                <%--<asp:ListItem Value="0" Text="-Select-"></asp:ListItem>--%>
                                <asp:ListItem Value="2" Text="eTMF"></asp:ListItem>
                                <%--<asp:ListItem Value="3" Text="Tasks"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="divforeTMF" runat="server" visible="false">
                    <div class="row" id="div1" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Structure :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drpDocType" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="drpDocType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select Zones :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlZones_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div3" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Sections :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSections" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlSections_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select Artifacts :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlArtifacts" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlArtifacts_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divfortask" runat="server" visible="false">
                    <div class="row" id="divdepartment" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Department :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddldepartment" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Select Task :
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlTask" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlTask_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divsubtask" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Sub Task :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlSubTask" runat="server" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="ddlSubTask_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="div5" runat="server" style="padding-top: 30px;">
                    <div class="col-md-12">
                        <div id="divdocument" runat="server" visible="false">
                            <div class="label col-md-2">
                                Select Documents :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpDocument" CssClass="form-control width250px required"
                                    OnSelectedIndexChanged="drpDocument_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divstatus" runat="server" visible="false">
                            <div class="label col-md-2">
                                Select Status : &nbsp;
                                <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpAction" AutoPostBack="true" CssClass="form-control required width250px"
                                    OnSelectedIndexChanged="drpAction_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Draft" Value="Draft"></asp:ListItem>
                                    <asp:ListItem Text="Final" Value="Final"></asp:ListItem>
                                    <asp:ListItem Text="Obsolete" Value="Obsolete"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="divCountry" runat="server" visible="false">
                            <div class="label col-md-2">
                                Select Country : &nbsp;
                                <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpCountry" AutoPostBack="true" CssClass="form-control width250px"
                                    OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divINVID" runat="server" visible="false">
                            <div class="label col-md-2">
                                Site ID : &nbsp;
                                <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control width250px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 15px;" id="divIndividual" runat="server">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Individual : &nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlIndividual" CssClass="form-control required width250px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="divDefaultView" runat="server" visible="false">
                    <div class="row" id="div7" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div id="divStatus2" visible="false" runat="server">
                                <div class="label col-md-2">
                                    Select Sub-Status :&nbsp;
                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlFinalStatus" CssClass="form-control width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFinalStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                        <asp:ListItem Text="Collaborate" Value="Collaborate"></asp:ListItem>
                                        <asp:ListItem Text="Review" Value="Review"></asp:ListItem>
                                        <asp:ListItem Text="QC" Value="QC"></asp:ListItem>
                                        <asp:ListItem Text="QA Review" Value="QA Review"></asp:ListItem>
                                        <asp:ListItem Text="Internal Approval" Value="Internal Approval"></asp:ListItem>
                                        <asp:ListItem Text="Sponsor Approval" Value="Sponsor Approval"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divStatus3" visible="false" runat="server">
                                <div class="label col-md-2">
                                    Select Action : &nbsp;
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlStatus3" CssClass="form-control width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlStatus3_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                                        <asp:ListItem Text="Replace" Value="Replace"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divFileReplace" visible="false" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select File : &nbsp;
                                <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:GridView ID="gvFiles" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped table-striped1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="UploadFileName" Text='<%# Eval("UploadFileName") %>' ToolTip='<%# Eval("UploadFileName") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Version No.">
                                            <ItemTemplate>
                                                <asp:Label ID="DOC_VERSIONNO" Text='<%# Eval("DOC_VERSIONNO") %>' ToolTip='<%# Eval("DOC_VERSIONNO") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Date">
                                            <ItemTemplate>
                                                <asp:Label ID="DOC_DATETIME" Text='<%# Eval("DOC_DATETIME") %>' ToolTip='<%# Eval("DOC_DATETIME") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Note">
                                            <ItemTemplate>
                                                <asp:Label ID="NOTE" Text='<%# Eval("NOTE") %>' ToolTip='<%# Eval("NOTE") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divDeadline" visible="false" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Enter Deadline Date (if Applicable) : &nbsp;
                                <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDeadline" CssClass="form-control txtDate"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divUsers" visible="false" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select Users : &nbsp;
                                <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:GridView ID="grd_Users" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped table-striped1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="User_ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="User_ID" Text='<%# Eval("User_ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMAILID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="EMAILID" Text='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="User_Name" Text='<%# Eval("User_Name") %>' ToolTip='<%# Eval("User_Name") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div8" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Document Version Number: &nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDovVersionNo" CssClass="form-control width250px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Document Date : &nbsp;
                                <asp:Label ID="lblRequiredDocDate" runat="server" Font-Size="Small" ForeColor="#FF3300"
                                    Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtDocDateTime" CssClass="form-control txtDateNoFuture width250px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div13" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Note : &nbsp;
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" CssClass="form-control"
                                    Height="50" Width="721"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divFunction" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Function : &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlFunction" CssClass="form-control required width250px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div11" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Enter Expiry Date (if Applicable) : &nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtExpiryDate" CssClass="form-control txtDate width250px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                File Name:
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblfilename" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div12" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-3">
                                <asp:Button ID="btnBack" runat="server" Text="Back to Modify / Delete Documents"
                                    CssClass="btn btn-DARKORANGE btn-sm" OnClick="btnBack_Click" />
                            </div>
                            &nbsp&nbsp
                            <div class="col-md-3" align="right">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" ValidationGroup="filePDF"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpload_Click" />
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
