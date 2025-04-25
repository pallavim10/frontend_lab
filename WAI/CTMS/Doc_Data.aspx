<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Doc_Data.aspx.cs" Inherits="CTMS.Doc_Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        CKEDITOR.config.toolbar = [
        //   ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        //   ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor'],
        //   '/',
        //   ['Styles', 'Format', 'Font', 'FontSize']
        //   ];

        CKEDITOR.config.height = 500;
    </script>
    <script type="text/javascript">
        function ShowTrack(element) {

            if ($("#<%=drpSection.ClientID%>").val() == "0") {
                $("#<%=drpSection.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=drpPlan.ClientID%>").val() == "0") {
                $("#<%=drpPlan.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var SecID = $("#<%=drpSection.ClientID%>").val();
            var DocID = $("#<%=drpPlan.ClientID%>").val();
            var test = "Doc_TrackChanges.aspx?SecID=" + SecID + "&DocID=" + DocID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ShowComment(element) {

            if ($("#<%=drpSection.ClientID%>").val() == "0") {
                $("#<%=drpSection.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=drpPlan.ClientID%>").val() == "0") {
                $("#<%=drpPlan.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var SecID = $("#<%=drpSection.ClientID%>").val();
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
                Documentation
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField runat="server" ID="hfOldData" />
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>
                                Select Document:</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList Style="width: 250px;" ID="drpPlan" runat="server" class="form-control drpControl required"
                                AutoPostBack="True" OnSelectedIndexChanged="drpPlan_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>
                                Select Section:</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList Style="width: 250px;" ID="drpSection" runat="server" class="form-control drpControl required"
                                AutoPostBack="True" OnSelectedIndexChanged="drpSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:ImageButton ID="imgTrack" runat="server" ToolTip="Click here to see tracked changes."
                                ImageUrl="Images/trackChanges.png" Style="margin-top: 1%; height: 15px;" OnClientClick="return ShowTrack(this);">
                            </asp:ImageButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnAddComment" ToolTip="Comments" Style="margin-top: 1%; height: 15px;"
                                runat="server" OnClientClick="return ShowComment(this);">
                        <i class="fa fa-comment" style="color:#333333;" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="txtData" CssClass="ckeditor" Height="100%" TextMode="MultiLine"
                            Width="100%"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="margin-left: 25%;">
                        <asp:LinkButton runat="server" ID="lbtnSave" Text="Save" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="lbtnSave_Click"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"
                            OnClick="lbtnCancel_Click"></asp:LinkButton>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
