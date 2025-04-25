$(function callChange() {

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
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }


    DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild2').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild2').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild2').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild2').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild2').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild2').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild2').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild2').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild3').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild3').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild3').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild3').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild3').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild3').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild3').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild4').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild4').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild4').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild4').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild4').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild4').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild4').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }

});

function callChange() {

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
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }


    DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild2').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild2').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild2').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild2').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild2').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild2').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild2').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild2').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild3').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild3').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild3').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild3').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild3').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild3').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild3').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }



    DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
    for (a = 0; a < DRP_FIELD.length; ++a) {
        showChild(DRP_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild4').find("input[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    TXT_FIELD = $('.ShowChild4').find("textarea[id*='TXT_FIELD']").toArray();
    for (a = 0; a < TXT_FIELD.length; ++a) {
        showChild(TXT_FIELD[a]);
    }

    CHK_FIELD1 = $('.ShowChild4').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
    for (c = 0; c < CHK_FIELD1.length; ++c) {
        showChild(CHK_FIELD1[c]);
    }

    CHK_FIELD = $('.ShowChild4').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
    for (c = 0; c < CHK_FIELD.length; ++c) {
        showChild(CHK_FIELD[c]);
    }

    RAD_FIELD1 = $('.ShowChild4').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
    for (d = 0; d < RAD_FIELD1.length; ++d) {
        showChild(RAD_FIELD1[d]);
    }

    RAD_FIELD = $('.ShowChild4').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
    for (d = 0; d < RAD_FIELD.length; ++d) {
        showChild(RAD_FIELD[d]);
    }

    CONTROLTYPE = $('.ShowChild4').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
    for (e = 0; e < CONTROLTYPE.length; ++e) {
        SetHeader(CONTROLTYPE[e]);
    }

}

function SetHeader(element) {

    if ($(element).text() == 'HEADER') {
        $(element).closest('td').next('td').attr('colspan', '2');
    }

}