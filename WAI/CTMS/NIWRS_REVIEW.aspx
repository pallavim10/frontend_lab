<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_REVIEW.aspx.cs" Inherits="CTMS.NIWRS_REVIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>

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

        function DownloadDoc(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('input').val();

            var test = "CTMS_DownloadDoc.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
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

        function checkReviewAll(element) {
            $('input[type=checkbox][id*=CheckReView]').each(function () {
                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Review/Freeze 
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="box box-primary">
        <div class="box-header">
            <div class="col-md-12">
                <div class="col-md-12">
                    <h5>
                        <label>Important Points to Note :</label>
                        <ul>
                            <li>
                                <label>Please click on&nbsp;<i class="fa fa-toggle-off" style="color: #333;"></i> &nbsp;to Review and Freeze the configuration.</label>
                            </li>
                            <li>
                                <label>Post review, configuration will not available for editing.</label>
                            </li>
                            <li>
                                <label>Please click on&nbsp;<i class="fa fa-toggle-on" style="color: #333;"></i>&nbsp;to open the configuration for editing.</label>
                            </li>
                    </h5>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-md-12">
                <div class="col-md-5">
                    <label>Please click on this to Review/Freeze configuration OR open for editing.</label>
                </div>
                <div class="col-md-7">
                    <asp:LinkButton runat="server" ID="lbtnReview" ToolTip="Mark as Review/Freeze" OnClick="lbtnReview_Click">
                            <i class="fa fa-toggle-off" style="color: #333; font-size:25px" aria-hidden="true"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lbtnUnReview" ToolTip="Open for editing" OnClick="lbtnUnReview_Click">
                            <i class="fa fa-toggle-on" style="color: #333; font-size:25px" aria-hidden="true"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
