<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DATA_ENTRY.aspx.cs" Inherits="SpecimenTracking.DATA_ENTRY" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="Scripts/btnSave_Required.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
        }


        function showAuditTrail(element) {
            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = '';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/DataEntry_showAuditTrail",
                data: JSON.stringify({ TABLENAME: TABLENAME, ID: ID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    } else {
                        $('#DivAuditTrail').html(response.d);
                        $('#modal-lg').modal('show'); // Show the modal after populating it
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching audit trail:', status, error);
                    alert("An error occurred. Please contact the administrator.");
                }
            });

            return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Data Entry</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Data Entry</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="card card-info">
                    <div class="card-header">
                        <h3 class="card-title">Data Entry</h3>
                        <div class="pull-right">
                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <div class="col-md-12 d-inline-flex">
                                <div class="col-md-3">
                                    <label>Enter Site ID:</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="drpsite" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="drpsite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 d-inline-flex">
                                <div class="col-md-3">
                                    <label>Select Specimen Type:</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="drpSpecimenType" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 d-inline-flex">
                                <div class="col-md-3">
                                    <label>Enter Specimen ID:</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtSpecimen" runat="server" CssClass="form-control required" AutoPostBack="true" OnTextChanged="txtSpecimen_TextChanged">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 d-inline-flex">
                                <div class="col-md-3">
                                    <label>Select Subject ID:</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="drpSubject" runat="server" CssClass="form-control required select" AutoPostBack="true" OnSelectedIndexChanged="drpSubject_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12 d-inline-flex">
                                <div class="col-md-3">
                                    <label>Select Visit:</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Repeater runat="server" ID="repeatFields" OnItemDataBound="repeatFields_ItemDataBound">
                                <ItemTemplate>
                                    <br />
                                    <br />
                                    <div class="col-md-12 d-inline-flex">
                                        <div class="col-md-3">
                                            <label><%# Eval("FIELDNAME") %> :</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtDATA" runat="server" CssClass="form-control required" Visible="false">
                                            </asp:TextBox>
                                            <asp:DropDownList ID="drpDATA" runat="server" CssClass="form-control required" Visible="false">
                                            </asp:DropDownList>
                                            <div class="col-md-12">
                                                <asp:Repeater ID="repeatRadio" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-md-2">
                                                            <asp:RadioButton runat="server" ID="radioDATA" Text='<%# Eval("OPTION_VALUE") %>' onclick="RadioCheck(this);" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:Repeater ID="repeatChk" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox runat="server" ID="chkDATA" Text='<%# Eval("OPTION_VALUE") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                    <asp:HiddenField runat="server" ID="hdnVARIABLENAME" Value='<%# Eval("VARIABLENAME") %>' />
                                    <asp:HiddenField runat="server" ID="hdnCONTROLTYPE" Value='<%# Eval("CONTROLTYPE") %>' />
                                    </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div runat="server" id="divAliquotPrep" visible="false">
                                <br />
                                <hr />
                                <h3>Aliquot Preparation</h3>
                                <br />
                                <asp:GridView runat="server" ID="gridAliquots" AutoGenerateColumns="false" CssClass="table table-bordered table-striped txt_center" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Aliquot ID" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblALIQUOTID" Text='<%# Eval("ALIQUOTID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aliquot Type" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblALIQUOTTYPE" Text='<%# Eval("ALIQUOTTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Box Number" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtBOXNO" CssClass="form-control txt_center"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Slot Number" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSLOTNO" CssClass="form-control txt_center"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
