<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_DOC_Review.aspx.cs" Inherits="CTMS.eTMF_DOC_Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Review Documents
            </h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div id="divforeTMF" runat="server" visible="false">
            <div class="row" id="div1" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Structure :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblStructure" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="div2" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Zones :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblZone" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="div3" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Sections :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblSection" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="div4" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Artifacts :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblArtifacts" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div id="divfortask" runat="server" visible="false">
            <div class="row" id="divdepartment" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Department :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblDepartment" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="divtask" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Task :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblTask" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="divsubtask" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Sub Task :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblSubTask" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div id="divdocument" runat="server" visible="false">
            <div class="row" id="div5" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Documents :
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblDocument" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div id="divDefaultView" runat="server" visible="false" style="padding-top: 15px;">
            <div class="row" id="div7" runat="server">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Action/Mode : &nbsp;
                        <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblAction" runat="server" CssClass="form-control width300px label"></asp:Label>
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
                        <asp:Label ID="lblDeadlineDate" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="div9" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Country : &nbsp;
                        <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblCountry" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="divINVID" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Site ID : &nbsp;
                        <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblSiteId" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="div11" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Enter Expiry Date (if Applicable) : &nbsp;
                        <asp:Label ID="Label15" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblExpireDate" runat="server" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row" id="div6" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        File Name:
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblfilename" runat="server" Font-Bold="true" ForeColor="Maroon" CssClass="form-control width300px label"></asp:Label>
                    </div>
                </div>
            </div>
            <br />
            <div class="row" id="div12" runat="server">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary btn-sm"
                            OnClick="btnBack_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnUpload" runat="server" Text="Reviwed" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnUpload_Click" />
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
