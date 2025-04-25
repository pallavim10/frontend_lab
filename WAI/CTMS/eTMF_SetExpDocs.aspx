<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_SetExpDocs.aspx.cs" Inherits="CTMS.eTMF_SetExpDocs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/eTMF/eTMF_ConfirmMsg.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ADD_NEW_ACTIVITY(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var ArtifactsID = $(element).closest('tr').find('td:eq(2)').find('span').text();
            var DOCTYPEID = $(element).closest('tr').find('td:eq(3)').find('span').text();

            var test = "eTMF_ADD_NEW_ACTIVITY.aspx?ArtifactsID=" + ArtifactsID + "&DOCTYPEID=" + DOCTYPEID;
            window.location.href = test;
            return false;
        }



        function SetVerDateYes(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetVerDateYes",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnVerDateYES']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnVerDateNO']").attr('id')).removeClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function SetVerDateNo(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetVerDateNo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnVerDateYES']").attr('id')).removeClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(7)').find("a[id*='lbtnVerDateNO']").attr('id')).addClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function SetVerType(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            var VerType = $(element).val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetVerType",
                data: '{ID: "' + ID + '", VerType: "' + VerType + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }

        function SetAutoReplace(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            var AutoReplace = $(element).val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetAutoReplace",
                data: '{ID: "' + ID + '", AutoReplace: "' + AutoReplace + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        }

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

        function UpdateComments(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var EXPDOC = $(element).closest('tr').find('td:eq(4)').find('input').val();

            var Comments = "";
            $('#MainContent_txtID').val(ID);
            $('#MainContent_txtExpDoc').val(EXPDOC);

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/GetDocComment",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#MainContent_txteTMFComments').val(response.d);

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            $("#popup_ExpectedDoc_Comment").dialog({
                title: "Expected Documents Comment",
                width: 500,
                height: "auto",
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });
            return false;
        }

        function SetDocOwner(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            var UserName = $(element).val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetDocOwner",
                data: '{ID: "' + ID + '", UserName: "' + UserName + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }


        function SetUnblinding(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            var UnblindingType = $(element).val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetUnblinding",
                data: '{ID: "' + ID + '", UnblindingType: "' + UnblindingType + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }

        function SetEmailYes(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetEmailYes",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(14)').find("a[id*='lbtnSendEmailYes']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(14)').find("a[id*='lbtnSendEmailNO']").attr('id')).removeClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function SetEmailNo(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetEmailNo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(14)').find("a[id*='lbtnSendEmailYes']").attr('id')).removeClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(14)').find("a[id*='lbtnSendEmailNO']").attr('id')).addClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function SetVerSPECYes(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetVerSPECYes",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(8)').find("a[id*='lbtnVerSPECYES']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(8)').find("a[id*='lbtnVerSPECNO']").attr('id')).removeClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function SetVerSPECNo(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetVerSPECNo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(8)').find("a[id*='lbtnVerSPECYES']").attr('id')).removeClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(8)').find("a[id*='lbtnVerSPECNO']").attr('id')).addClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }


        function UpdateDocInstruction() {

            var ID = $("#MainContent_lblDocId").val();
            var Comments = $("#MainContent_txtInstruction").val().trim().replace(/"/g, '');

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SetDocInstruction",
                data: '{ID: "' + ID + '", Comments:"' + Comments + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#MainContent_lblDocId').val("");
                    $("#popup_Instrunction").dialog("close");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }

        function SetOptions(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var DOCNAME = $(element).closest('tr').find('td:eq(3)').find('input').val().trim();

            var test = "eTMF_Set_Expected_Settings.aspx?DOCID=" + ID + "&DOCNAME=" + DOCNAME;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function UPDATE_DOC_NAME(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var DOCNAME = $(element).closest('tr').find('td:eq(3)').find('input').val().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/UPDATE_DOC_NAME",
                data: '{ID: "' + ID + '", DOCNAME:"' + DOCNAME + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }

        function Delete_Doc(element) {

            var newLine = "\r\n"

            var error_msg = "Are you sure you want to Delete this Document?";

            error_msg += newLine;
            error_msg += newLine;

            error_msg += "Note : Press OK to Proceed.";

            if (confirm(error_msg)) {

                var ID = $(element).closest('tr').find('td:eq(0)').text().trim();
                var DOCNAME = $(element).closest('tr').find('td:eq(3)').find('input').val().trim();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/Delete_Doc",
                    data: '{ID: "' + ID + '",DOCNAME:"' + DOCNAME + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        $(element).closest('tr').addClass("disp-none");

                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert(response.d);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }
                    }
                });

            }

            return false;
        }

        function Enable_Doc_No(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/Enable_Doc_No",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#' + $(element).closest('tr').find('td:eq(16)').find("a[id*='lbtnEnableDocYes']").attr('id')).removeClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(16)').find("a[id*='lbtnEnableDocNo']").attr('id')).addClass("disp-none");

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            return false;
        }

        function UPDATE_UniqueRefNo(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var UniqueRefNo = $(element).closest('tr').find('td:eq(2)').find('input').val().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/UPDATE_UniqueRefNo",
                data: '{ID: "' + ID + '", UniqueRefNo:"' + UniqueRefNo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }

        function UPDATE_RefNo(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var RefNo = $(element).closest('tr').find('td:eq(1)').find('input').val().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/UPDATE_RefNo",
                data: '{ID: "' + ID + '", RefNo:"' + RefNo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
            return false;
        }
    </script>

    <style>
        .layer1 {
            color: #800000;
        }

        .layer2 {
            color: #800000;
        }

        .layer3 {
            color: #008000;
        }

        .layer4 {
            color: Black;
        }

        .layerFiles {
            color: #800000;
            font-style: italic;
        }

        .strikeThrough {
            text-decoration: line-through;
        }
    </style>
    <style>
        .label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="popup_ExpectedDoc_Comment" title="Expected Documents Comment" class="disp-none">
        <div class="formControl disp-none">
            <asp:Label ID="Label31" runat="server" CssClass="wrapperLable" Text="id"></asp:Label>
            <asp:TextBox ID="txtID" CssClass="width245px" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtExpDoc" CssClass="width245px" runat="server"></asp:TextBox>
        </div>
        <div id="Div5" class="formControl">
            <asp:Label ID="Label32" runat="server" CssClass="wrapperLable" Text="Comments"></asp:Label>
            <asp:TextBox ID="txteTMFComments" Width="400" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
        <div style="margin-top: 10px">
            <asp:Button ID="btneTMFUpdate" runat="server" Style="margin-left: 107px;" Text="Submit"
                OnClientClick="UpdateeTMFexpComment();" />
        </div>
    </div>
    <div id="popup_Instrunction" title="Documents Instrunction" class="disp-none">
        <div class="formControl disp-none">
            <asp:Label ID="lblDocId" runat="server" CssClass="wrapperLable" Text=""></asp:Label>
        </div>
        <div id="Div2" class="formControl">
            <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="Instruction"></asp:Label>
            <asp:TextBox ID="txtInstruction" Width="400" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
        <div style="margin-top: 10px">
            <asp:Button ID="btnsubmit" runat="server" Style="margin-left: 107px;" Text="Submit"
                OnClientClick="UpdateDocInstruction();" />
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%">
                <asp:Label runat="server" ID="lblHeader" Text="Manage Expected Documents" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <%--<asp:LinkButton ID="lbtnExportDocs" runat="server" Text="Export Docs" OnClick="lbtnExportDocs_Click"> Export Docs&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>--%>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div id="divCountry" runat="server">
                    <div class="label col-md-1 width100px">
                        Zones :&nbsp;
                        <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlZone" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="divINVID" runat="server">
                    <div class="label col-md-1 width100px">
                        Sections : &nbsp;
                        <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlSections" Width="250px" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSections_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <asp:GridView ID="gvArtifact" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped layer1" OnRowDataBound="gvArtifact_RowDataBound">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                HeaderStyle-CssClass="txt_center" ItemStyle-Width="5%">
                <HeaderTemplate>
                    <a href="JavaScript:ManipulateAll('_Docs');" id="_Folder" style="color: #333333"><i
                        id="img_Docs" class="icon-plus-sign-alt"></i></a>
                </HeaderTemplate>
                <ItemTemplate>
                    <div runat="server" id="anchor">
                        <a href="JavaScript:divexpandcollapse('_Docs<%# Eval("ID") %>');" style="color: #333333">
                            <i id="img_Docs<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="MainRefNo">
                <ItemTemplate>
                    <asp:Label ID="MainRefNo" runat="server" Text='<%# Bind("MainRefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                ItemStyle-CssClass="disp-none" HeaderText="DocTypeId">
                <ItemTemplate>
                    <asp:Label ID="DocTypeId" runat="server" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ref." ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                <ItemTemplate>
                    <asp:Label ID="lbl_RefNo" Width="100%" onclick="ShowDocs(this);" ToolTip='<%# Bind("RefNo") %>'
                        CssClass="label" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="20%">
                <ItemTemplate>
                    <asp:Label ID="lbl_Artifact" Width="100%" ToolTip='<%# Bind("Artifact_Name") %>'
                        CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Definition">
                <ItemTemplate>
                    <asp:Label ID="lbl_Definition" Width="100%" ToolTip='<%# Bind("DEFINITION") %>'
                        CssClass="label" runat="server" Text='<%# Bind("DEFINITION") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-CssClass="txt_center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnAddNewActivity" runat="server" Text="Add New Activity" OnClientClick="return ADD_NEW_ACTIVITY(this);"
                        ToolTip="Add New Sub-Artifact"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <tr>
                        <td colspan="100%" style="padding: 2px;">
                            <div style="float: right; font-size: 13px;">
                            </div>
                            <div>
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div id="_Docs<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                            <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                OnRowDataBound="gvDocs_RowDataBound" CssClass="table table-bordered table-striped layer2">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref." ItemStyle-Width="5%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedRefNo" Width="100%" Font-Bold="true" ForeColor="Blue" ToolTip='<%# Bind("RefNo") %>'
                                                                CssClass="txt_center" runat="server" Text='<%# Bind("RefNo") %>' Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtRefNo" Width="100%" Font-Bold="true" ForeColor="Blue" ToolTip='<%# Bind("RefNo") %>'
                                                                onchange="return UPDATE_RefNo(this);" CssClass="form-control txt_center" runat="server"
                                                                Text='<%# Bind("RefNo") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unique Ref." ItemStyle-Width="5%" ItemStyle-CssClass="disp-none"
                                                        HeaderStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_UniqueRefNo" Width="100%" Font-Bold="true" ForeColor="Blue"
                                                                onchange="return UPDATE_UniqueRefNo(this);" ToolTip='<%# Bind("UniqueRefNo") %>'
                                                                CssClass="form-control txt_center" runat="server" Text='<%# Bind("UniqueRefNo") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document" ItemStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedDocName" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("DocName") %>'
                                                                runat="server" Text='<%# Bind("DocName") %>' Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtDocName" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("DocName") %>'
                                                                onchange="return UPDATE_DOC_NAME(this);" CssClass="form-control" runat="server"
                                                                Text='<%# Bind("DocName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Replace Superseded Version?" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedAutoReplace" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("AutoReplace") %>'
                                                                runat="server" Text='<%# Bind("AutoReplace") %>' Visible="false"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlAutoReplace" Width="100%" CssClass="form-control txt_center"
                                                                onchange="return SetAutoReplace(this);">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comment" ItemStyle-Width="5%" ItemStyle-CssClass="txt_center disp-none"
                                                        HeaderStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lbtnUpdateComment" OnClientClick="return UpdateComments(this);"
                                                                CommandArgument='<%# Bind("ID") %>'><i id="" class="fa fa-plus"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document Level" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedVerType" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("VerType") %>'
                                                                runat="server" Text='<%# Bind("VerType") %>' Visible="false"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlVerType" Width="100%" CssClass="form-control txt_center"
                                                                onchange="return SetVerType(this);">
                                                                <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                                                <asp:ListItem Value="Study" Text="Study"></asp:ListItem>
                                                                <asp:ListItem Value="Country" Text="Country"></asp:ListItem>
                                                                <asp:ListItem Value="Site" Text="Site"></asp:ListItem>
                                                                <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
                                                                <asp:ListItem Value="Subject" Text="Subject"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ver. Date" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedVerDate" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("VerDate") %>'
                                                                runat="server" Text='<%# (Boolean.Parse(Eval("VerDate").ToString())) ? "Yes" : "No" %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbtnVerDateYES" OnClientClick="return SetVerDateYes(this);" runat="server">
                                                            <i title="Mark as Yes" class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnVerDateNO" OnClientClick="return SetVerDateNo(this);" runat="server">
                                                            <i title="Mark as No" class="icon-check" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ver. Spec." ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedVerSPEC" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("VerSPEC") %>'
                                                                runat="server" Text='<%# (Boolean.Parse(Eval("VerSPEC").ToString())) ? "Yes" : "No" %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbtnVerSPECYES" OnClientClick="return SetVerSPECYes(this);" runat="server">
                                                            <i title="Mark as Yes" class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnVerSPECNO" OnClientClick="return SetVerSPECNo(this);" runat="server">
                                                            <i title="Mark as No" class="icon-check" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Access Control" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="blockedUnblind" ForeColor="Blue" Font-Bold="true" Width="100%" ToolTip='<%# Bind("Unblind") %>'
                                                                runat="server" Text='<%# Bind("Unblind") %>' Visible="false"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlUnblind" Width="100%" CssClass="form-control txt_center"
                                                                onchange="return SetUnblinding(this);">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="Blinded" Text="Blinded"></asp:ListItem>
                                                                <asp:ListItem Value="Unblinded" Text="Unblinded"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Additional settings" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnOptions" OnClientClick="return SetOptions(this);" runat="server">
                                                            <i title="Additional settings" class="icon-cog" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDeleteDoc" ToolTip="Delete Document" OnClientClick="return Delete_Doc(this);"
                                                                runat="server">
                                                            <i title="Delete" class="icon-trash" style="color: #333333;" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
