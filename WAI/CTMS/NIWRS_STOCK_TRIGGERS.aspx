<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_STOCK_TRIGGERS.aspx.cs" Inherits="CTMS.NIWRS_STOCK_TRIGGERS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
    <script>
        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
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
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.noSpace').keypress(function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" id="div2" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Back-Up Kit Request Limits</h3>
                            </div>
                            <div class="rows">
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-9">
                                            <label>
                                                Number of Back-Up Kits are Allowed Without Sponsor Approval. :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox Style="width: 60px;" MaxLength="3" ID="txtReqBakWithoutApp" runat="server"
                                                CssClass="form-control numeric required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-9">
                                            <label>
                                                Number of Back-Up Kits are Allowed With Sponsor Approval. :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox Style="width: 60px;" MaxLength="3" ID="txtReqBakWithApp" runat="server"
                                                CssClass="form-control numeric required"></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitQues" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnSubmitQues_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelQues" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnCancelQues_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" id="div19" runat="server">
                            <div class="box-header">
                                <h3 class="box-title">Define Reasons for Back-Up Kit</h3>
                            </div>
                            <div class="rows">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Sequence No. :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Style="width: 60px;" MaxLength="3" ID="txtReasonSeqNo" runat="server"
                                                CssClass="form-control numeric required1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Reason :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control required1 width200px"></asp:TextBox>
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
                                            <asp:Button ID="btnSubmitReason" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                OnClick="btnSubmitReason_Click" />
                                            <asp:Button ID="btnUpdateReason" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                OnClick="btnUpdateReason_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelReason" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnCancelReason_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gvReasons" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvReasons_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="EditReason" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%# Bind("TEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_OPTIONS_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteReason"
                                                            OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this reason :  ", Eval("TEXT")) %>'
                                                            ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
