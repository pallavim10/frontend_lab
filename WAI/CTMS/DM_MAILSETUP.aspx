<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_MAILSETUP.aspx.cs" Inherits="CTMS.DM_MAILSETUP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
           ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
           ['Styles', 'Format', 'Font', 'FontSize']
           ];

        CKEDITOR.config.height = 250;

        function CallCkedit() {

            CKEDITOR.replace("MainContent_txtEmailBody");

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Mail Configuration</h3>
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
                <div class="box box-primary" id="div17" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            Freezed Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-7">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdFreezed" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-5">
                                    <div class="rows">
                                        <div class="row">
                                            <label>
                                                Email Subject :</label>
                                            <asp:TextBox runat="server" ID="txtSubjectFreezed" Height="50px" Width="99%" TextMode="MultiLine"
                                                CssClass="form-control required"> 
                                            </asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <label>
                                                Email Body :</label>
                                            <asp:TextBox runat="server" ID="txtBodyFreezed" CssClass="ckeditor  required" Height="50%"
                                                TextMode="MultiLine" Width="99%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitFreezed" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitFreezed_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelFreezed" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelFreezed_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" id="div1" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            UnFreez Request Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdUnFreezRequest" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUnFreezRequest" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitUnFreezRequest_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelUnFreezRequest" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelUnFreezRequest_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" id="div4" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            UnFreezed Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-7">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdUnFreezed" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-5">
                                    <div class="rows">
                                        <div class="row">
                                            <label>
                                                Email Subject :</label>
                                            <asp:TextBox runat="server" ID="txtSubjectUnfreezed" Height="50px" Width="99%" TextMode="MultiLine"
                                                CssClass="form-control  required"> 
                                            </asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <label>
                                                Email Body :</label>
                                            <asp:TextBox runat="server" ID="txtBodyUnfreezed" CssClass="ckeditor  required" Height="50%"
                                                TextMode="MultiLine" Width="99%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUnFreezed" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitUnFreezed_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelUnFreezed" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelUnFreezed_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" id="div2" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            Locked Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-7">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdLocked" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-5">
                                    <div class="rows">
                                        <div class="row">
                                            <label>
                                                Email Subject :</label>
                                            <asp:TextBox runat="server" ID="txtSubjectLocked" Height="50px" Width="99%" TextMode="MultiLine"
                                                CssClass="form-control required"> 
                                            </asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <label>
                                                Email Body :</label>
                                            <asp:TextBox runat="server" ID="txtBodyLocked" CssClass="ckeditor required" Height="50%"
                                                TextMode="MultiLine" Width="99%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitLocked" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitLocked_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelLocked" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelLocked_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" id="div5" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            UnLock Request Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdUnlockRequest" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUnlockRequest" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitUnlockRequest_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelUnlockRequest" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelUnlockRequest_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" id="div3" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            UnLocked Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-7">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdUnLocked" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-5">
                                    <div class="rows">
                                        <div class="row">
                                            <label>
                                                Email Subject :</label>
                                            <asp:TextBox runat="server" ID="txtSubjectUnLocked" Height="50px" Width="99%" TextMode="MultiLine"
                                                CssClass="form-control required"> 
                                            </asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <label>
                                                Email Body :</label>
                                            <asp:TextBox runat="server" ID="txtBodyUnlocked" CssClass="ckeditor required" Height="50%"
                                                TextMode="MultiLine" Width="99%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitUnLocked" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitUnLocked_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelUnLocked" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCancelUnLocked_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary" id="div6" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            Investigator Sign Off Email IDs</h3>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-7">
                                    <label>
                                        Email IDs :</label>
                                    <asp:GridView runat="server" ID="grdINVSignOff" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                        Style="width: 100%; border-collapse: collapse;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                        runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-5">
                                    <div class="rows">
                                        <div class="row">
                                            <label>
                                                Email Subject :</label>
                                            <asp:TextBox runat="server" ID="txtSubjectINVSignOff" Height="50px" Width="99%" TextMode="MultiLine"
                                                CssClass="form-control  required"> 
                                            </asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <label>
                                                Email Body :</label>
                                            <asp:TextBox runat="server" ID="txtBodyINVSignOff" CssClass="ckeditor  required"
                                                Height="50%" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitINVSIGNOFF" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubmitINVSIGNOFF_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCANCELINVSIGNOFF" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnCANCELINVSIGNOFF_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
