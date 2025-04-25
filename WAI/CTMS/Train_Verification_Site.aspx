<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Train_Verification_Site.aspx.cs" Inherits="CTMS.Train_Verification_Site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvSections.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
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
                Training Verification
            </h3>
        </div>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="pull-left" style="margin-left: 30px">
                    <asp:Label runat="server" Style="font-size: initial;" Text="Training Plan:" ID="Label1"></asp:Label>&nbsp;<asp:Label
                        runat="server" ID="lblPlan" Style="font-size: initial; font-weight: bold;"></asp:Label>
                </div>
                <div class="pull-right" style="margin-right: 30px">
                    <asp:Label runat="server" Style="font-size: initial;" Text="Trainee Name:" ID="Label2"></asp:Label>&nbsp;<asp:Label
                        runat="server" ID="lblTrainee" Style="font-size: initial; font-weight: bold;"></asp:Label>
                </div>
                <br />
                <br />
                <asp:GridView ID="gvSections" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-striped Datatable1" ShowHeader="false" OnRowDataBound="gvSections_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="width100px disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblID" Text='<%# Bind("ID") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="width100px disp-none"
                            ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ANS" HeaderStyle-CssClass="width100px disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="lblANS" Text='<%# Bind("ANS") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question No." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_QNO" Width="100%" ToolTip='<%# Bind("SEQNO") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question" ItemStyle-Width="50%">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Question" Width="100%" ToolTip='<%# Bind("FIELDNAME") %>' CssClass="label"
                                    runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Controls" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Repeater runat="server" ID="repeat_chk" Visible="false">
                                    <ItemTemplate>
                                        <div style="display: inline-flex">
                                            <asp:CheckBox ID="chk" runat="server" CssClass="checkbox" Text='<%# Bind("Item") %>'>
                                            </asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="repeat_rbtn" Visible="false">
                                    <ItemTemplate>
                                        <div style="display: inline-flex">
                                            <asp:RadioButton ID="rbtn" onclick="return RadioCheck(this);" runat="server" CssClass="radio"
                                                Name="grp" Text='<%# Bind("Item") %>' />&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:DropDownList ID="ddl" runat="server" Width="100px" Visible="false">
                                </asp:DropDownList>
                                <asp:TextBox ID="txt" runat="server" Visible="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <div class="txt_center">
                    <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                        OnClick="btnsubmit_Click" />
                </div>
                <br />
                <br />
            </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
