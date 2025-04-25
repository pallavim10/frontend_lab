<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_Refrence_Doc.aspx.cs" Inherits="CTMS.eTMF_Refrence_Doc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>

        function ConfirmMsg() {
            var newLine = "\r\n"

            var error_msg = "This File is already exists";

            error_msg += newLine;
            error_msg += newLine;

            error_msg += "Note : Press OK to Proceed.";

            if (confirm(error_msg)) {

                $("#MainContent_btnUploadAgainDoc").click();

                return true;

            } else {

                return false;
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Reference Documents</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnZoneId" />
                <asp:HiddenField runat="server" ID="hdnSectionId" />
                <asp:HiddenField runat="server" ID="hfVerDATE" />
                <asp:HiddenField runat="server" ID="hfVerTYPE" />
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div id="divforeTMF" runat="server">
                    <div class="row" id="div1" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Zones :
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblZones" runat="server" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div3" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Sections :
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblSections" runat="server" CssClass="form-control"></asp:Label>
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
                <div class="row" id="div5" runat="server" style="padding-top: 30px;">
                    <div class="col-md-12">
                        <div id="divdocument" runat="server">
                            <div class="label col-md-2">
                                Select Documents :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drpDocument" CssClass="form-control width250px required"
                                    OnSelectedIndexChanged="drpDocument_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divstatus" runat="server">
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
                                <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control width250px">
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
                <div id="divDefaultView" runat="server">
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
                        </div>
                    </div>
                    <div class="row" id="div2" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Select PDF File : &nbsp;
                                <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="width250px" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="filePDF"
                                    ErrorMessage="Only PDF files are allowed!" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$"
                                    ControlToValidate="FileUpload1" CssClass="text-red"></asp:RegularExpressionValidator>
                            </div>
                            <div class="label col-md-2">
                                Select Other Type File : &nbsp;
                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="width250px" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="filePDF"
                                    ErrorMessage="Only Word,Excel,Image and Power-Point files are allowed!" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.doc|.docx|.png|.jpg|.txt|.xls|.xlsx|.ppt|.pptx)$"
                                    ControlToValidate="FileUpload2" CssClass="text-red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="div12" runat="server" style="padding-top: 15px;">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                            </div>
                            <div class="col-md-3" align="right">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" ValidationGroup="filePDF"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpload_Click" />
                                <asp:Button ID="btnUploadAgainDoc" runat="server" CssClass="disp-none" OnClick="btnUploadAgainDoc_Click">
                                </asp:Button>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
