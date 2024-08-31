console.log("Hello from script.js");
    function checkrecords(btnexport,grdview) {
    var exportButton = document.getElementById(btnexport);
    var gridview = document.getElementById(grdview);

    //var exportButton = document.getElementById('<%= lbnActualProgramExport.ClientID %>');
    //var gridView = document.getElementById('<%= GRV_Audit_Program.ClientID %>');

    if (gridview.rows.length <= 1) { // Check if only header row exists //gridView.rows.length <= 1
        exportButton.style.display = 'none';
    }

    //function checkRecords(btnexport, grdview) {
    //    var exportButton = document.getElementById(btnexport);
    //    var gridView = document.getElementById(grdview);

    //    if (gridView.rows.length <= 1) { // Check if only header row exists
    //        exportButton.style.display = 'none';
    //    }
    //}

};
