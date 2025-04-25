<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eTMF_Archival.aspx.cs" Inherits="CTMS.eTMF_Archival" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: true
            });


            $(document).on("click", ".cls-btnSave", function () {
                var test = "0";

                $('.required').each(function (index, element) {
                    var value = $(this).val();
                    var ctrl = $(this).prop('type');

                    if (ctrl == "select-one") {
                        if (value == "0" || value == "--Select--" || value == null) {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                    }
                    else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
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
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Archival
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
        <div class="box-body">
            <div align="left" style="margin-left: 5px">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-10">
                            <h4>
                                <label>
                                    Note : Please select below Unblinding type, then click on Export button.
                                </label>
                            </h4>
                        </div>
                    </div>
                    <div class="col-md-6">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>
                                Select Unblinding Type :
                            </label>
                        </div>
                        <div class="col-md-1" runat="server" id="divBlinded" style="width:110px;">
                                <asp:CheckBox runat="server" ID="chkBlinded"/>&nbsp;&nbsp;
                                    <label>Blinded</label>
                         </div> 
                         <div class="col-md-1" runat="server" id="divUnblinded"  style="width:110px;">
                                <asp:CheckBox runat="server" ID="chkUnblinded"/>&nbsp;&nbsp;
                                <label>Unblinded</label>
                          </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label>
                                Set Password :
                            </label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtSetPassword" TextMode="Password" Width="191px" runat="server" CssClass="form-control required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                         <div class="col-md-2" style="width:235px;">
                             &nbsp;
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="BtnExpArchival" runat="server" ForeColor="White" CssClass="btn btn-primary cls-btnSave" OnClick="BtnExpArchival_Click"><i class="fa fa-download"></i> &nbsp; Export &nbsp;</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
