<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PDList_New.aspx.cs" Inherits="CTMS.PDList_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });



        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        function pageLoad() {
            $(".Datatable").DataTable();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
        }

        
    </script>
    <style>
        .txt_center
        {
            text-align: center;
        }
        .style1
        {
        }
        .style2
        {
            width: 377px;
        }
        
        .label
        {
            max-width: 100%;
            font-weight: bold;
            font-size: 11px;
            margin-left: 9px;
            width: 91px;
        }
        .form-control
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Masters
                <asp:DropDownList ID="Drp_Project" runat="server" ForeColor="Blue" Visible="false"
                    class="disp-none form-control drpControl required" AutoPostBack="True">
                </asp:DropDownList>
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Site ID :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_InvID" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                        OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Subject ID :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_SUBJID" runat="server" AutoPostBack="true" class="form-control drpControl required ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Visit No. :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtVISITNUM" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Department :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_DEPT" runat="server" AutoPostBack="true" class="form-control drpControl required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Source :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtSource" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Reference :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtReference" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Date of Identification :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDateIdentified" runat="server" class="form-control txtDate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Date of Ocuurence :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtOCDate" runat="server" class="form-control txtDate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Date of Report :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCloseDate" runat="server" class="form-control txtDate"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Status :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_Status" runat="server" AutoPostBack="true" class="form-control drpControl required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Duplicacy :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="ddlDuplicacy" ForeColor="Maroon" CssClass="form-control">
                                        <asp:ListItem Selected="True" Text="New" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Possibly Duplicate" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Duplicate" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Count :
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtPdmasterID" runat="server" Visible="false" class="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtCount" runat="server" Style="color: Red" class="form-control txt_center"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Category :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="drp_Nature" ForeColor="Maroon" CssClass="form-control required"
                                        AutoPostBack="true" OnSelectedIndexChanged="drp_Nature_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Sub-Category :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="drp_PDCode1" ForeColor="Maroon" CssClass="form-control required"
                                        AutoPostBack="True" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="label col-md-4">
                                    Factor :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="drp_PDCode2" ForeColor="Maroon" CssClass="form-control required"
                                        OnSelectedIndexChanged="drp_PDCode2_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="label col-md-4">
                                    Classification :
                                </div>
                                <div class="col-md-8">
                                    <asp:Label runat="server" ID="lblClassficationDefault" ForeColor="Maroon" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="label col-md-4">
                                    Classification by Clinical Operations :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_Priority_Ops" runat="server" ForeColor="Maroon" class="form-control drpControl required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="label col-md-4">
                                    Classification by Medical :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_Priority_Med" runat="server" ForeColor="Maroon" class="form-control drpControl">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="label col-md-4">
                                    Final Classification :
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="drp_Priority_Final" runat="server" ForeColor="Maroon" class="form-control drpControl ">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-4">
                                Summary :
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtSummary" runat="server" CssClass=" form-control" TextMode="MultiLine"
                                    Height="50px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-4">
                                Description :
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtDescription" runat="server" class="form-control required" TextMode="MultiLine"
                                    Height="50px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-4">
                                Rationale :
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtRationalise" runat="server" class="form-control" TextMode="MultiLine"
                                    Height="50px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
