<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doc_Comments.aspx.cs" Inherits="CTMS.Doc_Comments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false
            });
        }
        
    </script>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }
    </script>
    <script src="js/htmldiff.js" type="text/javascript"></script>
    <style type="text/css">
        ins
        {
            text-decoration: none;
            background-color: #d4fcbc;
        }
        
        del
        {
            text-decoration: line-through;
            background-color: #fbb6c2;
            color: #555;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function setDiffvalue(element) {
            var oldHTML;
            var newHTML;
            var diffHTML;


            newHTML = $(element).closest('td').find('div:eq(5)').html().trim();
            oldHTML = $(element).closest('td').find('div:eq(6)').html().trim();

            // Diff HTML strings
            diffHTML = htmldiff(oldHTML, newHTML);

            // Show HTML diff output as HTML (crazy right?)!
            $(element).html(diffHTML);
        }

    </script>
    <script language="javascript" type="text/javascript">
        $(function diffChange() {

            var a;

            var divDiff = $("div[id*='divDiff']").toArray();
            for (a = 0; a < divDiff.length; ++a) {
                setDiffvalue(divDiff[a]);

            }

        });

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
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

        function CloseCurrent() {

            window.close();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Label ID="lblHeader" runat="server"></asp:Label>
                        </h3>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-11">
                                        <asp:GridView ID="gvComments" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField Visible="false" HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CommentsID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comments" ItemStyle-Width="60%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_User" Width="100%" ToolTip='<%# Bind("Comments") %>' CssClass="label"
                                                            runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User" ItemStyle-Width="20%" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_User" Width="100%" ToolTip='<%# Bind("User_Name") %>' CssClass="label"
                                                            runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date-Time" ItemStyle-Width="20%" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DateTime" Width="100%" ToolTip='<%# Bind("DateTime") %>' CssClass="label"
                                                            runat="server" Text='<%# Bind("DateTime") %>'></asp:Label>
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
                                        <label>
                                            Add Comment:</label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtComments" Width="100%" Height="70px" TextMode="MultiLine"
                                            CssClass="form-control required"> 
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row txt_center">
                                <div class="col-md-12">
                                    <div class="col-md-5">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" Style="margin-left: 10px" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="margin-left: 10px"
                                            CssClass="btn btn-primary btn-sm" OnClientClick="CloseCurrent()" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
