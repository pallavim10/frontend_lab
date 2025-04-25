<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_ADD_NEW_ACTIVITY.aspx.cs" Inherits="CTMS.eTMF_ADD_NEW_ACTIVITY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Add New Sub-Artifact</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-1">
                    <label>
                        Zones :
                    </label>
                    &nbsp;
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblZones" runat="server" Style="width: auto;" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-1">
                    <label>
                        Sections :
                    </label>
                    &nbsp;
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblSections" runat="server" Style="width: auto;" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-1">
                    <label>
                        Artifacts :
                    </label>
                    &nbsp;
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblArtifacts" runat="server" Style="width: auto;" CssClass="form-control"></asp:Label>
                </div>
            </div>
        </div>
        <br />
    </div>
    <br />
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Enter below details to add new sub-artifact</h3>
        </div>
        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="col-md-4">
                    <label>
                        Enter Ref. No. :
                    </label>
                    &nbsp;
                    <asp:TextBox ID="txtRefNo" runat="server" Style="width: 90%;" MaxLength="2" CssClass="form-control required"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <label>
                        Enter Document Name :
                    </label>
                    &nbsp;
                    <asp:TextBox ID="txtDocName" runat="server" Style="width: 90%;" CssClass="form-control required"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    &nbsp;
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="col-md-4">
                    <label>
                        Select Replace Superseded Version? :
                    </label>
                    &nbsp;
                    <asp:DropDownList runat="server" ID="ddlAutoReplace" Width="90%" CssClass="form-control">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label>
                        Select Document Level :
                    </label>
                    &nbsp;
                    <asp:DropDownList runat="server" ID="ddlVerType" Width="90%" CssClass="form-control">
                        <asp:ListItem Value="None" Text="None"></asp:ListItem>
                        <asp:ListItem Value="Study" Text="Study"></asp:ListItem>
                        <asp:ListItem Value="Country" Text="Country"></asp:ListItem>
                        <asp:ListItem Value="Site" Text="Site"></asp:ListItem>
                        <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                        <asp:ListItem Value="Subject" Text="Subject"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="col-md-4">
                    <label>
                        Select Access Control :
                    </label>
                    &nbsp;
                    <asp:DropDownList runat="server" ID="ddlUnblind" Width="90%" CssClass="form-control ">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                        <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label>
                        Enable :
                    </label>
                    <br />
                    <asp:CheckBox runat="server" ID="chkVerDate" />&nbsp;
                    <label>
                         Ver. Date
                    </label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox runat="server" ID="chkVerSpec" />&nbsp;
                    <label>
                         Ver. Spec.
                    </label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div12" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="col-md-2">
                </div>
                <div class="col-md-3" align="right">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />
                </div>
                                    <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                        OnClick="btncancel_Click" />
                
            </div>
        </div>
        <br />
    </div>
</asp:Content>
