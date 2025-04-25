<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Doc_Documents.aspx.cs" Inherits="CTMS.Doc_Documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ShowTrack(element) {
            var SecID = $(element).closest('input:eq(0)').context.nextElementSibling.value;
            var DocID = $("#<%=drpPlan.ClientID%>").val();
            var test = "Doc_TrackChanges.aspx?SecID=" + SecID + "&DocID=" + DocID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComment(element) {
            var SecID = $(element).closest('input:eq(0)').context.previousElementSibling.value;
            var DocID = $("#<%=drpPlan.ClientID%>").val();
            var test = "Doc_Comments.aspx?SecID=" + SecID + "&DocID=" + DocID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=850";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Documents
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>
                                Select Document:</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList Style="width: 500px;" ID="drpPlan" runat="server" class="form-control drpControl required"
                                AutoPostBack="True" OnSelectedIndexChanged="drpPlan_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div runat="server" id="divDoc" visible="false" class="box-body">
            <div class="form-group">
                <br />
                <div style="margin-left: 100px; margin-right: 100px;">
                    <div class="col-md-12" style="border: double;">
                        <div class="txt_center">
                            <h3 style="font-weight: bold">
                                <asp:Label runat="server" ID="lblHeader"></asp:Label>
                            </h3>
                        </div>
                        <asp:Repeater runat="server" ID="repeat" OnItemDataBound="repeat_ItemDataBound">
                            <ItemTemplate>
                                <br />
                                <div>
                                    <div>
                                        <div class="box-header" style="position: inherit;">
                                            <h4 style="font-weight: bold" class="box-title">
                                                <%# Eval("SectionName")%>
                                            </h4>
                                            <div class="pull-right" style="margin-top: 5px; margin-right: 5%; display: inline-flex;">
                                                <asp:ImageButton ID="imgTrack" runat="server" ToolTip="Click here to see tracked changes."
                                                    ImageUrl="Images/trackChanges.png" Style="margin-top: 1%; height: 15px;" OnClientClick="return ShowTrack(this);">
                                                </asp:ImageButton>
                                                <asp:HiddenField runat="server" ID="hfSecID" Value='<%# Eval("SecID")%>' />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnAddComment" ToolTip="Comments" Style="margin-top: 1%; height: 15px;"
                                                    runat="server" OnClientClick="return ShowComment(this);">
                        <i class="fa fa-comment" style="color:#333333;" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <div class="col-md-12">
                                            <asp:Literal runat="server" ID="lit" Text='<%# Eval("DATA") %>'></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
