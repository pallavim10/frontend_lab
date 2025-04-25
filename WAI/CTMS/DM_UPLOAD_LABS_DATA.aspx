<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_UPLOAD_LABS_DATA.aspx.cs" Inherits="CTMS.DM_UPLOAD_LABS_DATA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script language="javascript" type="text/javascript">

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == "--Select--" || value == null) {
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


        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_filLabData') {
                    document.getElementById("<%=btnLabData.ClientID %>").click();
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <h4 class="box-title" id="lblHeader" runat="server">Upload Lab Reference Range</h4>
            <div class="pull-right" style="margin-right: 10px;">
                <asp:LinkButton ID="lbtnExportSampleFile" runat="server" Text="Export Sample File" CssClass="btn btn-info btn-sm" ForeColor="White" Style="margin-top: 3px" OnClick="lbtnExportSampleFile_Click"> Export Sample File&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <br />
        <div class="box-body">
            <div align="left" style="margin-left: 5px">
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select File :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:FileUpload runat="server" ID="filLabData" />
                            <asp:Label ID="lblfilLabData" runat="server" />
                                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="filLabData"
                                ErrorMessage="Only Excel files are allowed"
                                ValidationExpression="^.*\.xls[xm]?$">
                            </asp:RegularExpressionValidator>
                            <br />
                            <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnLabData" runat="server" CssClass="disp-none" OnClick="btnLabData_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Site Id :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpSiteId" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Lab Id :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpLabId" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Lab Name :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpLabName" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Test Name :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpTestName" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Gender :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpGender" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Units :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpUnit" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Age Lower Limit :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpAgeLowerLimit" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Age Upper Limit :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpAgeUpperLimit" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Reference Ranges Lower Limit :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpRefLowerLimit" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Reference Ranges Upper Limit :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpRefUpperLimit" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Effective From Date :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpFromDate" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Effective To Date :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpEndDate" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-5">
                            &nbsp;
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnUpload" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpload_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
