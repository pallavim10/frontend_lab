<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload_Libraries.aspx.cs" Inherits="CTMS.Upload_Libraries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
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

                if ($(fileUpload).attr('id') == 'MainContent_filMeddra') {
                    document.getElementById("<%=btnMeddraCol.ClientID %>").click();
                }
                else if ($(fileUpload).attr('id') == 'MainContent_filWHODData') {
                    document.getElementById("<%=btnWHODDataCol.ClientID %>").click();
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
            <h3 class="box-title">Upload Libraries </h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary" style="min-height: 300px;">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left" id="lblMedDRAHeader" runat="server">Upload MedDRA</h4>
                </div>
                <br />
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
                                    <asp:FileUpload runat="server" ID="filMeddra" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="filMeddra"
                                        ErrorMessage="Only Excel files are allowed"
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                    </asp:RegularExpressionValidator>
                                    <br />
                                    <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnMeddraCol" runat="server" CssClass="disp-none" OnClick="btnMeddraCol_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        System Organ Class Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpSystemOrganClass" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        System Organ Class Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpSystemOrganClassCode" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        High Level Group Term Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpHighLevelGrpTerm" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        High Level Group Term Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpHighLevelGrpCode" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        High Level Term Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpHighlevelterm" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        High Level Term Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpHighleveltermCode" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Preferred Term Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpPererredTerm" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Preferred Term Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpPererredTermCode" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Lowest Level Term Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpLowestLevelTerm" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Lowest Level Term Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpLowestLevelTermCode" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Primary SOC Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpPrimary" CssClass="form-control width200px required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Version Number :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtMedraVersionNum" runat="server" CssClass="form-control width200px required"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnMeddra" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnMeddra_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnMeddraCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnMeddraCancel_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary" style="min-height: 300px;">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left" id="lblWHODDHeader" runat="server">Upload WHODD</h4>
                </div>
                <br />
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
                                    <asp:FileUpload runat="server" ID="filWHODData" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="filWHODData"
                                        ErrorMessage="Only Excel files are allowed"
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                    </asp:RegularExpressionValidator>
                                    <br />
                                    <asp:Label ID="Label2" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnWHODDataCol" runat="server" CssClass="disp-none" OnClick="btnWHODDataCol_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 1 Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL1" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 1 Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL1Code" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 2 Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL2" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 2 Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL2Code" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 3 Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL3" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 3 Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL3Code" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 4 Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL4" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        ATC Level 4 Code Column:</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL4Code" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Drug Name Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL5" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Drug Code Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpATCLEVEL5Code" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Generic Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpModifyTerm" CssClass="form-control width200px required1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Version Number :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtWHODVersionNo" runat="server" CssClass="form-control width200px required1"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnWHODData" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnWHODData_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnWHODDataCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnWHODDataCancel_Click" />
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
