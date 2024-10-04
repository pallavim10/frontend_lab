<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Criteria.aspx.cs" Inherits="SpecimenTracking.Manage_Criteria" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        $(".numeric").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

        //only for numeric value
        $('.numeric').keypress(function (event) {

            if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                // let it happen, don't do anything
                return true;
            }
            else {
                event.preventDefault();
            }
        });
        
        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'SETUP_MANAGE_CRIT';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
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


       function confirm(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to delete this Record?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                if (isConfirm) {
                    var linkButton = event.target;
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }
                    linkButton.onclick = null;
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });
            return false;
        } 
    
    </script>

    <script type="text/javascript">
        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        if ($(this).hasClass("select") == true) {
                            $(this).next("span").addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
                    if (value == "") {
                        if ($(this).hasClass("ckeditor")) {
                            $(this).next('div').addClass("brd-1px-redimp");
                        }
                        else {
                            $(this).addClass("brd-1px-redimp");
                        }
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        function bindOptionValues() {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue1') {
                    avaTag = $('#ContentPlaceHolder1_hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue2') {
                    avaTag = $('#ContentPlaceHolder1_hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue3') {
                    avaTag = $('#ContentPlaceHolder1_hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue4') {
                    avaTag = $('#ContentPlaceHolder1_hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue5') {
                    avaTag = $('#ContentPlaceHolder1_hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        }

        $(function () {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue1') {
                    avaTag = $('#ContentPlaceHolder1_hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue2') {
                    avaTag = $('#ContentPlaceHolder1_hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue3') {
                    avaTag = $('#ContentPlaceHolder1_hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue4') {
                    avaTag = $('#ContentPlaceHolder1_hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'ContentPlaceHolder1_txtLISTValue5') {
                    avaTag = $('#ContentPlaceHolder1_hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });
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
                        <h1 class="m-0 text-dark">Manage Criteria</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Manage Criteria</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Define Criteria</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Criteria &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Sequence Number : &nbsp;</label>
                                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtSeqtNo" runat="server" CssClass="form-control required numeric w-25"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Description : &nbsp;</label>
                                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox runat="server" ID="txtNotes" CssClass="form-control required w-75" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label id="lblsetcriteria" runat="server">
                                                        Set Criteria :</label>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required" Width="100%">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="hfValue1" />
                                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1" Width="100%"> </asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="hfValue2" />
                                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue2"
                                                                Width="100%"> </asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="hfValue3" />
                                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue3"
                                                                Width="100%"> </asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField4_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="hfValue4" />
                                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue4"
                                                                Width="100%"> </asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField5_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                                <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                                <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                                <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                                <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                                <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                                <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                                <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="hfValue5" />
                                                            <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue5"
                                                                Width="100%"> </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <center>
                                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="lbnUpdate_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Records</h3>
                                        <div class="pull-right">
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div style="width: 100%; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="GrdCRIT" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable" OnRowCommand="GrdCRIT_RowCommand" OnPreRender="GrdCRIT_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtEdit" runat="server" class="btn-info btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                            CommandName="EDITED" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Seq No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSquenceNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Criteria">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCriteria" runat="server" Text='<%# Bind("Criteria") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Criteria Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCritCode" runat="server" Text='<%# Bind("CritCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtdelete" runat="server" class="btn-danger btn-sm" CommandArgument='<%# Bind("ID") %>'
                                                                            CommandName="DELETED" OnClientClick="return confirm(event);" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
