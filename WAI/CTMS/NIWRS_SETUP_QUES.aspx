<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_SETUP_QUES.aspx.cs" Inherits="CTMS.NIWRS_SETUP_QUES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Question</h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" runat="server">
                    <br />
                    <div class="rows">
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label>
                                            Participant id must be shown as :</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtSubjectID" runat="server" CssClass="form-control required8 width200px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label>
                                            Upload User Manual :</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:FileUpload ID="UserManualFile" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                &nbsp;
                            </div>
                            <div class="col-md-7">
                                <asp:Button ID="btnSubmitQues" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmitQues_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancelQues" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancelQues_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
        </div>
    </div>
        </div>
</asp:Content>

