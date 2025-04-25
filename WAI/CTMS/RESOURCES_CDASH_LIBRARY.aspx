<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RESOURCES_CDASH_LIBRARY.aspx.cs" Inherits="CTMS.RESOURCES_CDASH_LIBRARY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        CDASH LIBRARY
                    </h3>
                    <div class="pull-right" style="margin-right: 20px;">
                        <asp:Label ID="lblpagination" runat="server" Font-Bold="true" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Module :
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlModule" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                        OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Field Name :
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtFieldName" CssClass="form-control  width250px"
                                        AutoPostBack="true" OnTextChanged="txtFieldName_TextChanged"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Variable Name :
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtVariablename" CssClass="form-control  width250px"
                                        AutoPostBack="true" OnTextChanged="txtVariablename_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Observation Class :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblOBSERVATION" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Domain :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblDOMAIN" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Data Collection Scenario :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblDATASCENARIO" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Implementation Options :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblIMP_OPTION" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    CDASHIG Variable :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblVARIABLE" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    CDASHIG Variable Label :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblVARLVL" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Control Type :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblControltype" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Field Length :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lbllenght" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Standard Module Y/N :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblModuleYN" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Data Type :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblDATATYPE" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    DRAFT CDASHIG Definition :
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblDEFINATION" Width="720px" Style="height: auto" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Question Text :
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblQUERYTEXT" Width="720px" Style="height: auto" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Prompt :
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblPROMPT" Width="720px" Style="height: auto" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    CDASHIG Core :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblCORE" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    SDTMIG Target :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblTARGET" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Case Report Form Completion Instructions :
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblCOMP_INST" Width="720px" Style="height: auto" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Mapping Instructions :
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblMAP_INST" Style="height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Controlled Terminology Codelist Name :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblCONTROLLED" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Subset Controlled Terminology :
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblSUBSETCONTROLLED" CssClass="form-control  width250px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Implementation Notes :
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblIMPLEMENTATION" Width="720px" TextMode="MultiLine"
                                        Style="height: auto" CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="pull-left">
                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-primary btn-sm"
                                    Style="margin-left: 20px;" OnClick="btnPrevious_Click" />
                            </div>
                            <div class="pull-right">
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary btn-sm"
                                    Style="margin-right: 30px;" OnClick="btnNext_Click" />
                            </div>
                        </div>
                        <br />
                        <asp:HiddenField ID="hdnCurrentID" runat="server" />
                        <asp:HiddenField ID="hdnNEXTID" runat="server" />
                        <asp:HiddenField ID="hdnPREVID" runat="server" />
                        <asp:HiddenField ID="hdnPAGINATION" runat="server" />
                        <asp:HiddenField ID="hdnTotal" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
