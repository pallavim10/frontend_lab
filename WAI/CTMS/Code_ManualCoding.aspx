<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Code_ManualCoding.aspx.cs" Inherits="CTMS.Code_ManualCoding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        select {
            width: 100%
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="content">
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Medical Coding
                </h3>
            </div>
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
        </div>
        <div runat="server" id="divRecode" class="box box-info">
            <div class="box-header">
                <h3 class="box-title">Current codes
                </h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <asp:ListView ID="lstCodes" runat="server" AutoGenerateColumns="false">
                        <GroupTemplate>
                            <div class="col-md-12">
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <div class="col-md-12 label" style="display: inline-flex;">
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblName" Text='<%# Eval("NAME") +" :"%>' Width="90%"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label runat="server" ID="lblVal" Text='<%# Bind("VAL") %>' CssClass="form-control" Width="90%" Height="100%" TextMode="MultiLine"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <br />
            </div>
        </div>
        <div class="box box-primary">
            <br />
            <div class="box-body">
                <div runat="server" id="GetData" visible="false">
                    <div class="row col-md-12">
                        <div class="col-md-3">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label" style="color: blue;">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:Label ID="lblSiteID" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group" style="display: inline-flex" id="subjectid">
                                <label class="label" style="color: blue;">
                                    Subject Id:
                                </label>
                                <div class="Control">
                                    <asp:Label ID="lblSubjectID" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label" style="color: blue;">
                                    <asp:Label ID="lblFIELDNAME" runat="server"></asp:Label>:
                                </label>
                                <div class="Control">
                                    <asp:Label ID="lblTerm" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:ListView ID="lstInfo" runat="server" AutoGenerateColumns="false">
                            <GroupTemplate>
                                <div class="col-md-12">
                                    <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="col-md-12 label" style="display: inline-flex;">
                                    <div class="col-md-5">
                                        <asp:Label runat="server" ID="lblName" Text='<%# Eval("NAME") +" :"%>' Width="90%"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="lblVal" Text='<%# Bind("VAL") %>' CssClass="form-control" Width="90%" Height="100%" TextMode="MultiLine"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <br />
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Search
                </h3>
                <div class="pull-right">
                    <asp:LinkButton ID="lbtnNotApplicable" runat="server" OnClick="lbtnNotApplicable_Click" Style="margin-top: 3px;" CssClass="btn btn-warning" ForeColor="White">&nbsp;&nbsp;<span class="glyphicon glyphicon-ban-circle 2x"></span>&nbsp;&nbsp;Not Applicable&nbsp;&nbsp;</asp:LinkButton>
                </div>
            </div>
            <br />
            <div class="box-body">
                <div runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Enter Criteria :</label>
                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <asp:HiddenField runat="server" ID="AUTOCODELEB" />
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpField" runat="server" CssClass="form-control required" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required"
                                            Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpField2" runat="server" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue2"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpField3" runat="server" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue3"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpField4" runat="server" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue4"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                            <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpField5" runat="server" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                            <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                            <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                            <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                            <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue5"
                                            Width="100%"> </asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        &nbsp;
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnSearch" Text="Search" runat="server" Width="13%" CssClass="btn btn-DarkGreen btn-sm cls-btnSave" OnClick="btnSearch_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnClear" Text="Clear" Width="13%" runat="server" CssClass="btn  btn-danger btn-sm" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </center>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body">
                    <div class="rows">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="GridView1_PreRender" CssClass="table table-bordered Datatable table-striped" OnRowCommand="gridData_RowCommand" OnRowDataBound="gridData_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="CHEKCRIT"
                                                    runat="server" ToolTip="Select"><i class="fa fa-check"></i></asp:LinkButton>
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
</asp:Content>
