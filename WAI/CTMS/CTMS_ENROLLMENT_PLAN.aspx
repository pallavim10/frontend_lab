<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_ENROLLMENT_PLAN.aspx.cs" Inherits="CTMS.CTMS_ENROLLMENT_PLAN" %>

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
                        Enrollment Plan
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                        </div>
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control "
                                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Enrollment Start Month :
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control ">
                                <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Enrollment Start Year :
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control ">
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                                <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                                <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Enrollment Months :
                            </label>
                            <div class="Control">
                                <asp:TextBox ID="txtEnrollSteps" AutoPostBack="true" runat="server" CssClass="form-control"
                                    OnTextChanged="txtEnrollSteps_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="box">
                            <div style="width: 100%; height: 300px; overflow: auto;">
                                <asp:GridView ID="grdEnrollmentStep" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Months" ItemStyle-CssClass="txt_center"
                                            HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonth" runat="server" Text='<%# Bind("MONTH") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Months" ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="txt_center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonths" runat="server" Text='<%# Bind("MONTHS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Subjects" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblsubjects" runat="server" Text='<%# Bind("SUBJECTS") %>' CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="txt_center">
                            <asp:Button ID="btnEnrollSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnEnrollSubmit_Click" />
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Randomization Plan
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="Label1" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                        </div>
                        <div runat="server" id="Div1" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpINVID1" runat="server" AutoPostBack="True" CssClass="form-control "
                                        OnSelectedIndexChanged="drpINVID1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                                                <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Enrollment Start Month :
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpMonth1" runat="server" CssClass="form-control ">
                                <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Enrollment Start Year :
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpYear1" runat="server" CssClass="form-control ">
                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                                <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                                <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Randomization Months :
                            </label>
                            <div class="Control">
                                <asp:TextBox ID="txtRand" AutoPostBack="true" runat="server" CssClass="form-control"
                                    OnTextChanged="txtRand_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="box">
                            <div style="width: 100%; height: 300px; overflow: auto;">
                                <asp:GridView ID="grdRad" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Months" ItemStyle-CssClass="txt_center"
                                            HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonth" runat="server" Text='<%# Bind("MONTH") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Months" ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="txt_center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonths" runat="server" Text='<%# Bind("MONTHS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Subjects" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblsubjects" runat="server" Text='<%# Bind("SUBJECTS") %>' CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="txt_center">
                            <asp:Button ID="btnRand" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnRand_Click" />
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
