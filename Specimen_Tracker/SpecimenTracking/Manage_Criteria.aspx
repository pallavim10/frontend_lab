<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Criteria.aspx.cs" Inherits="SpecimenTracking.Manage_Criteria" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                                            <label>Eneter Discription : &nbsp;</label>
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
                                                <div class="col-md-10">
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
                                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
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
                                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
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
                                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
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
                                                                <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                                <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                                <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
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
    </div>
</asp:Content>
