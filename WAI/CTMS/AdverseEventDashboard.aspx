<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdverseEventDashboard.aspx.cs" Inherits="CTMS.AdverseEventDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/amcharts.js"></script>
    <script src="Scripts/serial.js"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css"
        type="text/css" media="all" />
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
    <script type="text/javascript">
        function changeIcon(element) {
            if ($(element).hasClass('fa fa-plus-square')) {
                $(element).removeClass();
                $(element).addClass('fa fa-minus-square');
            }
            else {
                $(element).removeClass();
                $(element).addClass('fa fa-plus-square');
            }
        }
    </script>
    <asp:ScriptManager ID="scriptmanager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        <div class="col-lg-3 col-xs-6" style="width: 20%; margin-top:20px;">
            <div class="small-box bg-red">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lbltotalevent" runat="server" Font-Bold="true" Font-Size="XX-Large">
                        </asp:Label><br />
                        <asp:Label runat="server" Text="Total no. of Event" Font-Size="Small">    
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
        <div class="col-lg-3 col-xs-6" style="width: 20%; margin-top:20px;">
            <div class="small-box bg-green">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lbltotalse" runat="server" Font-Bold="true" Font-Size="XX-Large">
                        </asp:Label><br />
                        <asp:Label runat="server" Text="No. of Serious Event" Font-Size="Small">   
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
        <div class="col-lg-3 col-xs-6" style="width: 20%; margin-top:20px;">
            <div class="small-box bg-yellow">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lblrelatedaes" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label><br />
                        <asp:Label runat="server" Text="No .of Related AES" Font-Size="Small">   
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
        <div class="col-lg-3 col-xs-6" style="width: 20%;">
            <div class="small-box bg-aqua">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lblsplint" runat="server" Font-Bold="true" Font-Size="XX-Large">
                        </asp:Label><br />
                        <asp:Label runat="server" Text="Event with special interest" Font-Size="Small">    
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-xs-6" style="width: 20%;">
            <div class="small-box bg-aqua">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lbldeath" runat="server" Font-Bold="true" Font-Size="XX-Large">
                        </asp:Label><br />
                        <asp:Label runat="server" Text="No .of Deaths" Font-Size="Small">    
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
        <div class="col-lg-3 col-xs-6" style="width: 20%;">
            <div class="small-box bg-yellow">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lblinfusion" runat="server" Font-Bold="true" Font-Size="XX-Large">
                        </asp:Label><br />
                        <asp:Label runat="server" Text="No. of Infusion Reaction" Font-Size="Small">   
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
        <div class="col-lg-3 col-xs-6" style="width: 20%;">
            <div class="small-box bg-red">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lblongoingevents" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label><br />
                        <asp:Label runat="server" Text="Total No of Ongoing Event" Font-Size="Small">   
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
        <div class="col-lg-3 col-xs-6" style="width: 20%;">
            <div class="small-box bg-green">
                <div class="inner">
                    <div style="text-align: center;">
                        <asp:Label ID="lblseverity3n4" runat="server" Font-Bold="true" Font-Size="XX-Large">
                        </asp:Label><br />
                        <asp:Label runat="server" Text="Total severity 3,4" Font-Size="Small">    
                        </asp:Label>
                    </div>
                </div>
                <div class="icon">
                    <i class="ion ion-stats-bars"></i>
                </div>
                <a href="#" class="small-box-footer"></a>
            </div>
        </div>
    </div>
    <div class="row">
        <section class="col-lg-12 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Most frequent AEs (Percentage of Subjects) - mCRC </h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#frequentmCRCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="row" style="margin-left:10px;">
                                        <asp:HiddenField ID="hffrequentmCRC" runat="server" />
                                        <asp:DropDownList ID="ddlfrequentmcrc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfrequentmcrc_SelectedIndexChanged">
                                           
                                            <asp:ListItem Value="1">10</asp:ListItem>
                                            <asp:ListItem Value="2">20</asp:ListItem>
                                            <asp:ListItem Value="3">30</asp:ListItem>
                                            <asp:ListItem Value="4">40</asp:ListItem>
                                        </asp:DropDownList>
                                        <div id="frequentmCRCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-12 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Most frequent AEs (Percentage of Subjects) - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#frequentNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <asp:UpdatePanel ID="updatepanel2" runat="server">
                                        <ContentTemplate>
                                    <div class="row" style="margin-left:10px;">
                                        <asp:DropDownList ID="ddlfrequentnsclc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfrequentnsclc_SelectedIndexChanged">
                                         
                                            <asp:ListItem Value="1">10</asp:ListItem>
                                            <asp:ListItem Value="2">20</asp:ListItem>
                                            <asp:ListItem Value="3">30</asp:ListItem>
                                            <asp:ListItem Value="4">40</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hffrequentNSCLC" runat="server" />
                                        <div id="frequentNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
    </div>
    <div class="row">
        <section class="col-lg-12 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Events - Grade III, Severe- mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#AdverseSevereBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfAdverseSevere" runat="server" />	
                                     <div id="AdverseSevereBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Events - Grade III, Severe - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#AdverseSevereNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfAdverseSevereNSCLC" runat="server" />	
                                     <div id="AdverseSevereNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Events - Grade IV, Life Threatening- mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#LifeThreateningBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfLifeThreatening" runat="server" />	
                                     <div id="LifeThreateningBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Events - Grade IV, Life Threatening- NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#AdverseThreateningNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfAdverseThreateningNSCLC" runat="server" />	
                                     <div id="AdverseThreateningNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Event - Related, Grade III: Severe - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                       <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#RelatedSevereBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfRelatedSevere" runat="server" />
                                        <div id="RelatedSevereBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Event - Related, Grade III: Severe - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#AdvRelatedSevereNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfAdvRelatedSevereNSCLC" runat="server" />
                                        <div id="AdvRelatedSevereNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Event - Related, Grade IV: Life Threatening - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#RelatedThreateningBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfRelatedThreatening" runat="server" />	
                                     <div id="RelatedThreateningBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Event - Related, Grade IV: Life Threatening - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                       <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#InfusionMildBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfInfusionMild" runat="server" />	
                                     <div id="InfusionMildBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Adverse Event - Related, Grade IV: Life Threatening - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#InfusionMildBar1">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfInfusionMild1" runat="server" />	
                                     <div id="InfusionMildBar1" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - IMP/RMP; Grade III Severe - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#InfusionSevereBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfInfusionSevere" runat="server" />	
                                     <div id="InfusionSevereBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - IMP/RMP; Grade III Severe - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#InfusionSevereNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfInfusionSevereNSCLC" runat="server" />	
                                     <div id="InfusionSevereNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - IMP/RMP; Grade IV Life Threatening - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#InfusionThreateningBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfInfusionThreatening" runat="server" />	
                                     <div id="InfusionThreateningBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - IMP/RMP; Grade IV Life Threatening - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#InfusionThreateningNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfInfusionThreateningNSCLC" runat="server" />	
                                     <div id="InfusionThreateningNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - Chemotherapy, Grade III Severe - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#ChemotherapySevereBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfChemotherapySevere" runat="server" />	
                                     <div id="ChemotherapySevereBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - Chemotherapy, Grade III Severe - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#ChemotherapySevereNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfChemotherapySevereNSCLC" runat="server" />	
                                     <div id="ChemotherapySevereNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - Chemotherapy, Grade IV Life Threatening - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#ChemotherapyThreateningBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfChemotherapyThreatening" runat="server" />	
                                     <div id="ChemotherapyThreateningBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Infusion Reactions - Chemotherapy, Grade IV Life Threatening - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                       <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#ChemoThreateningNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfChemoThreateningNSCLC" runat="server" />	
                                     <div id="ChemoThreateningNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Grade III Severe - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousSevereBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousSevere" runat="server" />	
                                     <div id="SeriousSevereBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Grade III Severe - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousSevereNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousSevereNSCLC" runat="server" />	
                                     <div id="SeriousSevereNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Grade IV Life Threatening - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousThreateningBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousThreatening" runat="server" />	
                                     <div id="SeriousThreateningBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  
                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Grade IV Life Threatening - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SerThreateningNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSerThreateningNSCLC" runat="server" />	
                                     <div id="SerThreateningNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Grade V Death- mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousDeathBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousDeath" runat="server" />	
                                     <div id="SeriousDeathBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Grade V Death - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousDeathNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousDeathNSCLC" runat="server" />	
                                     <div id="SeriousDeathNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Related, Grade III Severe - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousRelatedSevereBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousRelatedSevere" runat="server" />	
                                     <div id="SeriousRelatedSevereBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Related, Grade III Severe - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SerRelSevereNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSerRelSevereNSCLC" runat="server" />	
                                     <div id="SerRelSevereNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Related, Grade IV Life Threatening - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SeriousRelatedThreateningBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSeriousRelatedThreatening" runat="server" />	
                                     <div id="SeriousRelatedThreateningBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Related, Grade IV Life Threatening - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SerRelThreateningNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSerRelThreateningNSCLC" runat="server" />	
                                     <div id="SerRelThreateningNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Related, Grade V Death - mCRC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SerRelatedDeathBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSerRelatedDeath" runat="server" />	
                                     <div id="SerRelatedDeathBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
        <section class="col-lg-6 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Serious Adverse Events, Related, Grade V Death - NSCLC</h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SerRelDeathNSCLCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSerRelDeathNSCLC" runat="server" />	
                                     <div id="SerRelDeathNSCLCBar" class="chartdiv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <div class="row">
        <section class="col-lg-12 connectedSortable"> 
                            <!-- Box (with bar chart) -->
                            <div class="box box-danger" >
                                <div class="box-header">
                                   <h4 class="box-title">Study Status- Site-wise Distribution </h4>
                                    <!-- tools box -->
                                    <div class="pull-right box-tools">
                                        <button type="button" id="expandCollapse4" data-toggle="collapse" style="height: 19px;padding: 0px;width: 20px;margin: 4px 0px;" onclick="changeIcon(this)" class="fa fa-plus-square"
                            data-target="#SiteWisemCRCBar">
                                    </div><!-- /. tools -->
                                </div><!-- /.box-header -->
                                <div class="box-body no-padding">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSiteWisemCRC" runat="server" />
                                        <div id="SiteWisemCRCBar" class="piediv"></div>
                                    </div><!-- /.row - inside box -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->  

                        </section>
        <!-- /.Left col -->
    </div>
    <script type="text/javascript">

        var SerRelDeathNSCLCdata = eval('(' + $("#<%=hfSerRelDeathNSCLC.ClientID%>").val() + ')');
        var SerRelDeathNSCLCchart = AmCharts.makeChart("SerRelDeathNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SerRelDeathNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SerRelatedDeathdata = eval('(' + $("#<%=hfSerRelatedDeath.ClientID%>").val() + ')');
        var SerRelatedDeathchart = AmCharts.makeChart("SerRelatedDeathBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SerRelatedDeathdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SerRelThreatNSCLCdata = eval('(' + $("#<%=hfSerRelThreateningNSCLC.ClientID%>").val() + ')');
        var SerRelThreatNSCLCchart = AmCharts.makeChart("SerRelThreateningNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SerRelThreatNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SeriousRelatedThreateningdata = eval('(' + $("#<%=hfSeriousRelatedThreatening.ClientID%>").val() + ')');
        var SeriousRelatedThreateningchart = AmCharts.makeChart("SeriousRelatedThreateningBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SeriousRelatedThreateningdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SerRelSevrNSCLCdata = eval('(' + $("#<%=hfSerRelSevereNSCLC.ClientID%>").val() + ')');
        var SerRelSevrNSCLCchart = AmCharts.makeChart("SerRelSevereNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SerRelSevrNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SeriousRelatedSeveredata = eval('(' + $("#<%=hfSeriousRelatedSevere.ClientID%>").val() + ')');
        var SeriousRelatedSeverechart = AmCharts.makeChart("SeriousRelatedSevereBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SeriousRelatedSeveredata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SerDeathNSCLCdata = eval('(' + $("#<%=hfSeriousDeathNSCLC.ClientID%>").val() + ')');
        var SerDeathNSCLCchart = AmCharts.makeChart("SeriousDeathNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SerDeathNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SeriousDeathdata = eval('(' + $("#<%=hfSeriousDeath.ClientID%>").val() + ')');
        var SeriousDeathchart = AmCharts.makeChart("SeriousDeathBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SeriousDeathdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SerThreatNSCLCdata = eval('(' + $("#<%=hfSerThreateningNSCLC.ClientID%>").val() + ')');
        var SerThreatNSCLCchart = AmCharts.makeChart("SerThreateningNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SerThreatNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SeriousThreateningdata = eval('(' + $("#<%=hfSeriousThreatening.ClientID%>").val() + ')');
        var SeriousThreateningchart = AmCharts.makeChart("SeriousThreateningBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SeriousThreateningdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SeriousSevereNSCLCdata = eval('(' + $("#<%=hfSeriousSevereNSCLC.ClientID%>").val() + ')');
        var SeriousSevereNSCLCchart = AmCharts.makeChart("SeriousSevereNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SeriousSevereNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var SeriousSeveredata = eval('(' + $("#<%=hfSeriousSevere.ClientID%>").val() + ')');
        var SeriousSeverechart = AmCharts.makeChart("SeriousSevereBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": SeriousSeveredata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var ChemoThreatNSCLCdata = eval('(' + $("#<%=hfChemoThreateningNSCLC.ClientID%>").val() + ')');
        var ChemoThreatNSCLCchart = AmCharts.makeChart("ChemoThreateningNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": ChemoThreatNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var ChemoThreatdata = eval('(' + $("#<%=hfChemotherapyThreatening.ClientID%>").val() + ')');
        var ChemoThreatchart = AmCharts.makeChart("ChemotherapyThreateningBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": ChemoThreatdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var ChemoSevereNSCLCbar_data = eval('(' + $("#<%=hfChemotherapySevereNSCLC.ClientID%>").val() + ')');
        var ChemoSevereNSCLCchart = AmCharts.makeChart("ChemotherapySevereNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": ChemoSevereNSCLCbar_data,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var ChemoSeveredata = eval('(' + $("#<%=hfChemotherapySevere.ClientID%>").val() + ')');
        var ChemoSeverechart = AmCharts.makeChart("ChemotherapySevereBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": ChemoSeveredata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var InfusThreatNSCLCdata = eval('(' + $("#<%=hfInfusionThreateningNSCLC.ClientID%>").val() + ')');
        var InfusThreatNSCLCchart = AmCharts.makeChart("InfusionThreateningNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": InfusThreatNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var InfusThreatdata = eval('(' + $("#<%=hfInfusionThreatening.ClientID%>").val() + ')');
        var InfusThreatchart = AmCharts.makeChart("InfusionThreateningBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": InfusThreatdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var InfusSevereNSCLCdata = eval('(' + $("#<%=hfInfusionSevereNSCLC.ClientID%>").val() + ')');
        var InfusSevereNSCLCchart = AmCharts.makeChart("InfusionSevereNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": InfusSevereNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var InfusionSeveredata = eval('(' + $("#<%=hfInfusionSevere.ClientID%>").val() + ')');
        var InfusionSeverechart = AmCharts.makeChart("InfusionSevereBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": InfusionSeveredata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var InfusionMilddata = eval('(' + $("#<%=hfInfusionMild.ClientID%>").val() + ')');
        var InfusionMildchart = AmCharts.makeChart("InfusionMildBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": InfusionMilddata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var AdvRelSevNSCLCdata = eval('(' + $("#<%=hfAdvRelatedSevereNSCLC.ClientID%>").val() + ')');
        var AdvRelSevNSCLCchart = AmCharts.makeChart("AdvRelatedSevereNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": AdvRelSevNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var AdvThreateningNSCLCdata = eval('(' + $("#<%=hfAdverseThreateningNSCLC.ClientID%>").val() + ')');
        var AdvThreateningNSCLCchart = AmCharts.makeChart("AdverseThreateningNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": AdvThreateningNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var AdverseSevereNSCLCdata = eval('(' + $("#<%=hfAdverseSevereNSCLC.ClientID%>").val() + ')');
        var AdverseSevereNSCLCchart = AmCharts.makeChart("AdverseSevereNSCLCBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": AdverseSevereNSCLCdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">
        var multi_data = eval('(' + $("#<%=hfSiteWisemCRC.ClientID%>").val() + ')');
        var chart = AmCharts.makeChart("SiteWisemCRCBar", {
            "type": "serial",
            "theme": "light",
            "legend": {
                "horizontalGap": 10,
                "maxColumns": 1,
                "position": "right",
                "useGraphSettings": true,
                "markerSize": 10
            },
            "dataProvider": multi_data,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "Registered: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "title": "Registered",
                "color": "#000000",
                "valueField": "Registered"
            },
                {
                    "balloonText": "Screened: <b>[[value]]</b>",
                    "fillAlphas": 0.8,
                    "lineAlpha": 0.2,
                    "type": "column",
                    "title": "Screened",
                    "color": "#000000",
                    "valueField": "Screened"
                },
                {
                    "balloonText": "ScreenFailure: <b>[[value]]</b>",
                    "fillAlphas": 0.8,
                    "lineAlpha": 0.2,
                    "type": "column",
                    "title": "Screen Failure",
                    "color": "#000000",
                    "valueField": "Screen Failure"
                },
                {
                    "balloonText": "Randomised: <b>[[value]]</b>",
                    "fillAlphas": 0.8,
                    "lineAlpha": 0.2,
                    "type": "column",
                    "title": "Randomised",
                    "color": "#000000",
                    "valueField": "Randomized"
                },
                {
                    "balloonText": "Termminated: <b>[[value]]</b>",
                    "fillAlphas": 0.8,
                    "lineAlpha": 0.2,
                    "type": "column",
                    "title": "Terminated",
                    "color": "#000000",
                    "valueField": "Termminated"
                }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "Site",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var AdverseSeveredata = eval('(' + $("#<%=hfAdverseSevere.ClientID%>").val() + ')');
        var AdverseSeverechart = AmCharts.makeChart("AdverseSevereBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": AdverseSeveredata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "valueField": "value",
                "labelText": "[[value]]"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var LifeThreateningdata = eval('(' + $("#<%=hfLifeThreatening.ClientID%>").val() + ')');
        var LifeThreateningchart = AmCharts.makeChart("LifeThreateningBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": LifeThreateningdata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "labelText": "[[value]]",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var RelatedThreateningdata1 = eval('(' + $("#<%=hfRelatedThreatening.ClientID%>").val() + ')');
        var RelatedThreateningchart1 = AmCharts.makeChart("RelatedThreateningBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": RelatedThreateningdata1,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "labelText": "[[value]]",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">

        var RelatedSeveredata = eval('(' + $("#<%=hfRelatedSevere.ClientID%>").val() + ')');
        var RelatedSeverechart = AmCharts.makeChart("RelatedSevereBar", {
            "type": "serial",
            "theme": "light",
            "dataProvider": RelatedSeveredata,
            "valueAxes": [{
                "gridColor": "#FFFFFF",
                "gridAlpha": 0.2,
                "dashLength": 0,
                "title": "Percentage of subjects"
            }],
            "gridAboveGraphs": true,
            "startDuration": 1,
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillAlphas": 0.8,
                "lineAlpha": 0.2,
                "type": "column",
                "labelText": "[[value]]",
                "valueField": "value"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "category",
            "categoryAxis": {
                "gridPosition": "start",
                "gridAlpha": 0,
                "tickPosition": "start",
                "tickLength": 20,
                "labelRotation": 90
            },
            "export": {
                "enabled": true
            }

        });
    </script>
    <script type="text/javascript">
        function pageLoad() {
            var frequentNSCLCdata = eval('(' + $("#<%=hffrequentNSCLC.ClientID%>").val() + ')');
            var frequentNSCLCchart = AmCharts.makeChart("frequentNSCLCBar", {
                "type": "serial",
                "addClassNames": true,
                "theme": "light",
                "autoMargins": false,
                "marginLeft": 30,
                "marginRight": 8,
                "marginTop": 10,
                "marginBottom": 26,
                "balloon": {
                    "adjustBorderColor": false,
                    "horizontalPadding": 10,
                    "verticalPadding": 8,
                    "color": "#ffffff"
                },

                "dataProvider": frequentNSCLCdata,
                "valueAxes": [{
                    "axisAlpha": 0,
                    "position": "left"
                }],
                "startDuration": 1,
                "graphs": [{
                    "id": "graph2",
                    "balloonText": "<span style='font-size:12px;'>[[category]]:<br><span style='font-size:20px;'>[[value]]%</span></span>",
                    "lineThickness": 0,
                    "bulletSize": 14,
                    "customBullet": "https://www.amcharts.com/lib/3/images/star.png?x",
                    "customBulletField": "customBullet",
                    "bulletBorderAlpha": 1,
                    "title": "Percentage",
                    "valueField": "PERCENT",
                    "dashLengthField": "dashLengthLine"
                }],
                "categoryField": "AEPT",
                "categoryAxis": {
                    "gridPosition": "start",
                    "axisAlpha": 0,
                    "tickLength": 0,
                    "labelRotation": 20
                },
                "export": {
                    "enabled": true
                }
            });

            var frequentmCRCdata = eval('(' + $("#<%=hffrequentmCRC.ClientID%>").val() + ')');
            var frequentmCRCchart = AmCharts.makeChart("frequentmCRCBar", {
                "type": "serial",
                "addClassNames": true,
                "theme": "light",
                "autoMargins": false,
                "marginLeft": 30,
                "marginRight": 8,
                "marginTop": 10,
                "marginBottom": 26,
                "balloon": {
                    "adjustBorderColor": false,
                    "horizontalPadding": 10,
                    "verticalPadding": 8,
                    "color": "#ffffff"
                },

                "dataProvider": frequentmCRCdata,
                "valueAxes": [{
                    "axisAlpha": 0,
                    "position": "left"
                }],
                "startDuration": 1,
                "graphs": [{
                    "id": "graph2",
                    "balloonText": "<span style='font-size:12px;'>[[category]]:<br><span style='font-size:20px;'>[[value]]%</span></span>",
                    "lineThickness": 0,
                    "bulletSize": 14,
                    "customBullet": "https://www.amcharts.com/lib/3/images/star.png?x",
                    "customBulletField": "customBullet",
                    "bulletBorderAlpha": 1,
                    "title": "Percentage",
                    "valueField": "PERCENT",
                    "dashLengthField": "dashLengthLine"
                }],
                "categoryField": "AEPT",
                "categoryAxis": {
                    "gridPosition": "start",
                    "axisAlpha": 0,
                    "tickLength": 0,
                    "labelRotation": 20
                },
                "export": {
                    "enabled": true
                }
            });
        }
    </script>
</asp:Content>
