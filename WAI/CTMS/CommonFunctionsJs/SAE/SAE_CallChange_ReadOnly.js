$(function callChange_ReadOnly() {

    var a;
    var b;
    var c;
    var d;
    var e = 0;

    var DRP_FIELD = '';
    var TXT_FIELD = '';
    var CHK_FIELD1 = '';
    var CHK_FIELD = '';
    var RAD_FIELD1 = '';
    var RAD_FIELD = '';
    var CONTROLTYPE = '';


    DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }


    DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild2').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild2').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild2').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild2').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild2').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild2').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild2').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild3').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild3').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild3').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild3').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild3').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild3').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild4').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild4').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild4').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild4').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild4').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild4').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }

    var f = 0;
    var HEADER = $("span:Contains(HEADER)").toArray();
    for (f = 0; f < HEADER.length; ++f) {
        HideHeaderIcons(HEADER[f]);
    }

    var LBTN_FIELD = $('span:contains(Click here to fill the Form opened in another tab.)').toArray();
    for (a = 0; a < LBTN_FIELD.length; ++a) {
        HideLinkIcons(LBTN_FIELD[a]);
    }
});

function callChange_ReadOnly() {

    var a;
    var b;
    var c;
    var d;
    var e = 0;

    var DRP_FIELD = '';
    var TXT_FIELD = '';
    var CHK_FIELD1 = '';
    var CHK_FIELD = '';
    var RAD_FIELD1 = '';
    var RAD_FIELD = '';
    var CONTROLTYPE = '';


    DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }


    DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild2').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild2').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild2').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild2').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild2').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild2').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild2').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild3').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild3').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild3').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild3').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild3').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild3').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild_ReadOnly(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild4').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild_ReadOnly(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild4').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild_ReadOnly(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild4').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild_ReadOnly(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild4').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild_ReadOnly(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild4').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild_ReadOnly(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild4').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }
}

function HideHeaderIcons(element) {

    $(element).closest('tr').find('td').eq(5).addClass('disp-none');
    $(element).closest('tr').find('td').eq(6).addClass('disp-none');
    $(element).closest('tr').find('td').eq(7).addClass('disp-none');
    $(element).closest('tr').find('td').eq(8).addClass('disp-none');
    $(element).closest('tr').find('td').eq(9).addClass('disp-none');
    $(element).closest('tr').find('td').eq(10).addClass('disp-none');
    $(element).closest('tr').find('td').eq(11).addClass('disp-none');

}

function HideLinkIcons(element) {

    $(element).closest('tr').find('td').eq(5).addClass('disp-none');
    $(element).closest('tr').find('td').eq(6).addClass('disp-none');
    $(element).closest('tr').find('td').eq(7).addClass('disp-none');
    $(element).closest('tr').find('td').eq(8).addClass('disp-none');
    $(element).closest('tr').find('td').eq(9).addClass('disp-none');
    $(element).closest('tr').find('td').eq(10).addClass('disp-none');
    $(element).closest('tr').find('td').eq(11).addClass('disp-none');

}

function SetHeader(element) {

    if ($(element).text() == 'HEADER') {
        $(element).closest('td').next('td').attr('colspan', '2');
    }

}