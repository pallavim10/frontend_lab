<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_All_Inbox.aspx.cs" Inherits="CTMS.Comm_All_Inbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function ChangeDivQuery() {
            if ($('#MainContent_chkMM').prop('checked') == true) {
                $('#divQueryText').removeClass('disp-none');
                $('#divParent').removeClass('disp-none');
                $('#divAddFuncs').removeClass('disp-none');
            }
            else {

                if (!$('#divQueryText').hasClass('disp-none')) {
                    $('#divQueryText').addClass('disp-none');
                }

                if (!$('#divParent').hasClass('disp-none')) {
                    $('#divParent').addClass('disp-none');
                }

                if (!$('#divAddFuncs').hasClass('disp-none')) {
                    $('#divAddFuncs').addClass('disp-none');
                }
            }
        }

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "-1" || value == null || value == "-Select-" || value == "--Select--") {
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager11" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Inbox Folders And Rules
            </h3>
        </div>
        <div class="box-body">
            <div class="row">
             <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hfValues" />
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">
                                Add Inbox Folder
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Parent Folder Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 150px;" ID="txtParent" Text="Inbox" Enabled="false" runat="server"
                                                    CssClass="form-control required"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    SEQNO :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 150px;" ID="txtLISTSEQNO" runat="server" CssClass="form-control required"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Folder Name :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 150px;" ID="txtListing" runat="server" CssClass="form-control required"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnAddListing" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnAddListing_Click" />
                                                <asp:Button ID="btnUpdateListing" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                    OnClick="btnUpdateListing_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Lists Records
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdListing" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                    Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdListing_RowCommand"
                                                    OnRowDataBound="grdListing_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SEQNO.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SEQNO" runat="server" Text='<%# Eval("SEQNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("LISTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Parent Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParentNAME" runat="server" Text='<%# Eval("PARENTLIST_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%"
                                                            ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="EditList" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="DeleteList" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
        <div class="box-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">
                                Add Rules
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Sequence No.:</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtSEQNO" runat="server" Width="10%" CssClass="form-control numeric required1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Description:</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtCritName" runat="server" TextMode="MultiLine" Height="70px" Width="48%"
                                                    CssClass="form-control required1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Set Criteria :</label>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required1 width200px">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="From Email Id" Value="FROMID"></asp:ListItem>
                                                            <asp:ListItem Text="To Email Id" Value="TOID"></asp:ListItem>
                                                            <asp:ListItem Text="CC Email Id" Value="CCID"></asp:ListItem>
                                                            <asp:ListItem Text="BCC Email Id" Value="BCCID"></asp:ListItem>
                                                            <asp:ListItem Text="Subject Contains" Value="SUBJECT"></asp:ListItem>
                                                            <asp:ListItem Text="BODY Contains" Value="BODY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required1"
                                                            Width="100%">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:HiddenField runat="server" ID="hfValue1" />
                                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1"
                                                            Width="100%"> </asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control width200px">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="From Email Id" Value="FROMID"></asp:ListItem>
                                                            <asp:ListItem Text="To Email Id" Value="TOID"></asp:ListItem>
                                                            <asp:ListItem Text="CC Email Id" Value="CCID"></asp:ListItem>
                                                            <asp:ListItem Text="BCC Email Id" Value="BCCID"></asp:ListItem>
                                                            <asp:ListItem Text="Subject Contains" Value="SUBJECT"></asp:ListItem>
                                                            <asp:ListItem Text="BODY Contains" Value="BODY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
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
                                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control width200px">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="From Email Id" Value="FROMID"></asp:ListItem>
                                                            <asp:ListItem Text="To Email Id" Value="TOID"></asp:ListItem>
                                                            <asp:ListItem Text="CC Email Id" Value="CCID"></asp:ListItem>
                                                            <asp:ListItem Text="BCC Email Id" Value="BCCID"></asp:ListItem>
                                                            <asp:ListItem Text="Subject Contains" Value="SUBJECT"></asp:ListItem>
                                                            <asp:ListItem Text="BODY Contains" Value="BODY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
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
                                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control width200px">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="From Email Id" Value="FROMID"></asp:ListItem>
                                                            <asp:ListItem Text="To Email Id" Value="TOID"></asp:ListItem>
                                                            <asp:ListItem Text="CC Email Id" Value="CCID"></asp:ListItem>
                                                            <asp:ListItem Text="BCC Email Id" Value="BCCID"></asp:ListItem>
                                                            <asp:ListItem Text="Subject Contains" Value="SUBJECT"></asp:ListItem>
                                                            <asp:ListItem Text="BODY Contains" Value="BODY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
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
                                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control width200px">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="From Email Id" Value="FROMID"></asp:ListItem>
                                                            <asp:ListItem Text="To Email Id" Value="TOID"></asp:ListItem>
                                                            <asp:ListItem Text="CC Email Id" Value="CCID"></asp:ListItem>
                                                            <asp:ListItem Text="BCC Email Id" Value="BCCID"></asp:ListItem>
                                                            <asp:ListItem Text="Subject Contains" Value="SUBJECT"></asp:ListItem>
                                                            <asp:ListItem Text="BODY Contains" Value="BODY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
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
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTAndOr5" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTField6" runat="server" CssClass="form-control width200px">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="From Email Id" Value="FROMID"></asp:ListItem>
                                                            <asp:ListItem Text="To Email Id" Value="TOID"></asp:ListItem>
                                                            <asp:ListItem Text="CC Email Id" Value="CCID"></asp:ListItem>
                                                            <asp:ListItem Text="BCC Email Id" Value="BCCID"></asp:ListItem>
                                                            <asp:ListItem Text="Subject Contains" Value="SUBJECT"></asp:ListItem>
                                                            <asp:ListItem Text="BODY Contains" Value="BODY"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="drpLISTCondition6" runat="server" CssClass="form-control" Width="100%">
                                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:HiddenField runat="server" ID="hfValue6" />
                                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue6"
                                                            Width="100%"> </asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Move To Folder :</label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddlFoldername" class="form-control" runat="server" Style="width: 400px;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Flag :</label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="ddlFlag" class="form-control" runat="server" Style="width: 400px;">
                                                    <asp:ListItem Selected="True" Text="None" Value="None"></asp:ListItem>
                                                    <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                                    <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                                    <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnRuleSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                    OnClick="btnRuleSubmit_Click" />
                                                <asp:Button ID="btnRuleUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                    OnClick="btnRuleUpdate_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnRuleCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btnRuleCancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Rule Records
                            </h4>
                        </div>
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; height: 264px; overflow: auto;">
                                    <div class="rows">
                                        <asp:GridView ID="grdStepCrits" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            OnRowCommand="grdStepCrits_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No." ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Criteria">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Options" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                                            runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit"
                                                            runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div>
                                        <%--<asp:GridView ID="grdRules" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                            Style="width: 96%; border-collapse: collapse; margin-left: 10px;" OnRowCommand="grdRules_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FROMID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FROMID" runat="server" Text='<%# Eval("FROMID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COND1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="COND1" runat="server" Text='<%# Eval("COND1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TOID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TOID" runat="server" Text='<%# Eval("TOID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COND2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="COND2" runat="server" Text='<%# Eval("COND2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CCID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CCID" runat="server" Text='<%# Eval("CCID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COND3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="COND3" runat="server" Text='<%# Eval("COND3") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SUBJECT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SUBJECT" runat="server" Text='<%# Eval("SUBJECT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COND4">
                                                    <ItemTemplate>
                                                        <asp:Label ID="COND4" runat="server" Text='<%# Eval("COND4") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FOLDERN NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FOLDERNNAME" runat="server" Text='<%# Eval("FOLDERNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"
                                                    ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                            CommandName="EditList" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                            CommandName="DeleteList" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
