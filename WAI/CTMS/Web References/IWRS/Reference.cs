﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace CTMS.IWRS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WAI_IWRSSoap", Namespace="http://tempuri.org/")]
    public partial class WAI_IWRS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback IWRS_INSERT_DATA_SPOperationCompleted;
        
        private System.Threading.SendOrPostCallback IWRS_ACTIONS_SPOperationCompleted;
        
        private System.Threading.SendOrPostCallback IWRS_VISIT_SPOperationCompleted;
        
        private System.Threading.SendOrPostCallback IWRS_KITS_SPOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WAI_IWRS() {
            this.Url = global::CTMS.Properties.Settings.Default.CTMS_IWRS1_WAI_IWRS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event IWRS_INSERT_DATA_SPCompletedEventHandler IWRS_INSERT_DATA_SPCompleted;
        
        /// <remarks/>
        public event IWRS_ACTIONS_SPCompletedEventHandler IWRS_ACTIONS_SPCompleted;
        
        /// <remarks/>
        public event IWRS_VISIT_SPCompletedEventHandler IWRS_VISIT_SPCompleted;
        
        /// <remarks/>
        public event IWRS_KITS_SPCompletedEventHandler IWRS_KITS_SPCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IWRS_INSERT_DATA_SP", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet IWRS_INSERT_DATA_SP(string ACTION, string TABLENAME, string SUBJID, string INSERTQUERY, string UPDATEQUERY) {
            object[] results = this.Invoke("IWRS_INSERT_DATA_SP", new object[] {
                        ACTION,
                        TABLENAME,
                        SUBJID,
                        INSERTQUERY,
                        UPDATEQUERY});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void IWRS_INSERT_DATA_SPAsync(string ACTION, string TABLENAME, string SUBJID, string INSERTQUERY, string UPDATEQUERY) {
            this.IWRS_INSERT_DATA_SPAsync(ACTION, TABLENAME, SUBJID, INSERTQUERY, UPDATEQUERY, null);
        }
        
        /// <remarks/>
        public void IWRS_INSERT_DATA_SPAsync(string ACTION, string TABLENAME, string SUBJID, string INSERTQUERY, string UPDATEQUERY, object userState) {
            if ((this.IWRS_INSERT_DATA_SPOperationCompleted == null)) {
                this.IWRS_INSERT_DATA_SPOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIWRS_INSERT_DATA_SPOperationCompleted);
            }
            this.InvokeAsync("IWRS_INSERT_DATA_SP", new object[] {
                        ACTION,
                        TABLENAME,
                        SUBJID,
                        INSERTQUERY,
                        UPDATEQUERY}, this.IWRS_INSERT_DATA_SPOperationCompleted, userState);
        }
        
        private void OnIWRS_INSERT_DATA_SPOperationCompleted(object arg) {
            if ((this.IWRS_INSERT_DATA_SPCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IWRS_INSERT_DATA_SPCompleted(this, new IWRS_INSERT_DATA_SPCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IWRS_ACTIONS_SP", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet IWRS_ACTIONS_SP(
                    string ID, 
                    string NAV_TYPE, 
                    string ACTION, 
                    string INSERTQUERY, 
                    string NAV_ID, 
                    string SUBJID, 
                    string LISTID, 
                    string SITEID, 
                    string STEPID, 
                    string WHERE, 
                    string FIELDID, 
                    string SUBSITEID, 
                    string RANDNO, 
                    string STRATA, 
                    string VISITNUM, 
                    string ENTERED_DT, 
                    string NEXT_VISIT, 
                    string WINDOW, 
                    string EARLY, 
                    string LATE, 
                    string MODULEID, 
                    string VARIABLENAME, 
                    string TABLENAME, 
                    string DATA, 
                    string NEXT_VISIT_DATE, 
                    string EARLY_DATE, 
                    string LATE_DATE, 
                    string CurrentDate, 
                    string INDICATION, 
                    string INDICATION_ID) {
            object[] results = this.Invoke("IWRS_ACTIONS_SP", new object[] {
                        ID,
                        NAV_TYPE,
                        ACTION,
                        INSERTQUERY,
                        NAV_ID,
                        SUBJID,
                        LISTID,
                        SITEID,
                        STEPID,
                        WHERE,
                        FIELDID,
                        SUBSITEID,
                        RANDNO,
                        STRATA,
                        VISITNUM,
                        ENTERED_DT,
                        NEXT_VISIT,
                        WINDOW,
                        EARLY,
                        LATE,
                        MODULEID,
                        VARIABLENAME,
                        TABLENAME,
                        DATA,
                        NEXT_VISIT_DATE,
                        EARLY_DATE,
                        LATE_DATE,
                        CurrentDate,
                        INDICATION,
                        INDICATION_ID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void IWRS_ACTIONS_SPAsync(
                    string ID, 
                    string NAV_TYPE, 
                    string ACTION, 
                    string INSERTQUERY, 
                    string NAV_ID, 
                    string SUBJID, 
                    string LISTID, 
                    string SITEID, 
                    string STEPID, 
                    string WHERE, 
                    string FIELDID, 
                    string SUBSITEID, 
                    string RANDNO, 
                    string STRATA, 
                    string VISITNUM, 
                    string ENTERED_DT, 
                    string NEXT_VISIT, 
                    string WINDOW, 
                    string EARLY, 
                    string LATE, 
                    string MODULEID, 
                    string VARIABLENAME, 
                    string TABLENAME, 
                    string DATA, 
                    string NEXT_VISIT_DATE, 
                    string EARLY_DATE, 
                    string LATE_DATE, 
                    string CurrentDate, 
                    string INDICATION, 
                    string INDICATION_ID) {
            this.IWRS_ACTIONS_SPAsync(ID, NAV_TYPE, ACTION, INSERTQUERY, NAV_ID, SUBJID, LISTID, SITEID, STEPID, WHERE, FIELDID, SUBSITEID, RANDNO, STRATA, VISITNUM, ENTERED_DT, NEXT_VISIT, WINDOW, EARLY, LATE, MODULEID, VARIABLENAME, TABLENAME, DATA, NEXT_VISIT_DATE, EARLY_DATE, LATE_DATE, CurrentDate, INDICATION, INDICATION_ID, null);
        }
        
        /// <remarks/>
        public void IWRS_ACTIONS_SPAsync(
                    string ID, 
                    string NAV_TYPE, 
                    string ACTION, 
                    string INSERTQUERY, 
                    string NAV_ID, 
                    string SUBJID, 
                    string LISTID, 
                    string SITEID, 
                    string STEPID, 
                    string WHERE, 
                    string FIELDID, 
                    string SUBSITEID, 
                    string RANDNO, 
                    string STRATA, 
                    string VISITNUM, 
                    string ENTERED_DT, 
                    string NEXT_VISIT, 
                    string WINDOW, 
                    string EARLY, 
                    string LATE, 
                    string MODULEID, 
                    string VARIABLENAME, 
                    string TABLENAME, 
                    string DATA, 
                    string NEXT_VISIT_DATE, 
                    string EARLY_DATE, 
                    string LATE_DATE, 
                    string CurrentDate, 
                    string INDICATION, 
                    string INDICATION_ID, 
                    object userState) {
            if ((this.IWRS_ACTIONS_SPOperationCompleted == null)) {
                this.IWRS_ACTIONS_SPOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIWRS_ACTIONS_SPOperationCompleted);
            }
            this.InvokeAsync("IWRS_ACTIONS_SP", new object[] {
                        ID,
                        NAV_TYPE,
                        ACTION,
                        INSERTQUERY,
                        NAV_ID,
                        SUBJID,
                        LISTID,
                        SITEID,
                        STEPID,
                        WHERE,
                        FIELDID,
                        SUBSITEID,
                        RANDNO,
                        STRATA,
                        VISITNUM,
                        ENTERED_DT,
                        NEXT_VISIT,
                        WINDOW,
                        EARLY,
                        LATE,
                        MODULEID,
                        VARIABLENAME,
                        TABLENAME,
                        DATA,
                        NEXT_VISIT_DATE,
                        EARLY_DATE,
                        LATE_DATE,
                        CurrentDate,
                        INDICATION,
                        INDICATION_ID}, this.IWRS_ACTIONS_SPOperationCompleted, userState);
        }
        
        private void OnIWRS_ACTIONS_SPOperationCompleted(object arg) {
            if ((this.IWRS_ACTIONS_SPCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IWRS_ACTIONS_SPCompleted(this, new IWRS_ACTIONS_SPCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IWRS_VISIT_SP", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet IWRS_VISIT_SP(
                    string SUBJID, 
                    string VISIT, 
                    string CritCode, 
                    string Criteria, 
                    string Field1, 
                    string Field2, 
                    string Field3, 
                    string Field4, 
                    string Field5, 
                    string Field6, 
                    string CONDITION1, 
                    string Condition2, 
                    string Condition3, 
                    string Condition4, 
                    string Condition5, 
                    string Condition6, 
                    string Value1, 
                    string Value2, 
                    string Value3, 
                    string Value4, 
                    string Value5, 
                    string Value6, 
                    string AndOr1, 
                    string AndOr2, 
                    string AndOr3, 
                    string AndOr4, 
                    string AndOr5, 
                    string DEPEND, 
                    string EARLY_DATE, 
                    string LATE_DATE, 
                    string LAST_VISIT_DATE, 
                    string NEXT_VISIT_DATE, 
                    string CurrentDate, 
                    string LAST_VISIT, 
                    string COL_NAME, 
                    string VISITNUM, 
                    string INDICATION_ID, 
                    string NEXT_VISIT, 
                    string NEXT_VISITNUM, 
                    string WINDOW, 
                    string EARLY, 
                    string LATE, 
                    string MODULEID, 
                    string VARIABLENAME, 
                    string FIELDID, 
                    string SEQNO, 
                    string TABLENAME, 
                    string DATA, 
                    string ACTION, 
                    string STEPID, 
                    string ENTEREDBY, 
                    string ENTEREDBYNAME, 
                    string ENTERED_TZVAL) {
            object[] results = this.Invoke("IWRS_VISIT_SP", new object[] {
                        SUBJID,
                        VISIT,
                        CritCode,
                        Criteria,
                        Field1,
                        Field2,
                        Field3,
                        Field4,
                        Field5,
                        Field6,
                        CONDITION1,
                        Condition2,
                        Condition3,
                        Condition4,
                        Condition5,
                        Condition6,
                        Value1,
                        Value2,
                        Value3,
                        Value4,
                        Value5,
                        Value6,
                        AndOr1,
                        AndOr2,
                        AndOr3,
                        AndOr4,
                        AndOr5,
                        DEPEND,
                        EARLY_DATE,
                        LATE_DATE,
                        LAST_VISIT_DATE,
                        NEXT_VISIT_DATE,
                        CurrentDate,
                        LAST_VISIT,
                        COL_NAME,
                        VISITNUM,
                        INDICATION_ID,
                        NEXT_VISIT,
                        NEXT_VISITNUM,
                        WINDOW,
                        EARLY,
                        LATE,
                        MODULEID,
                        VARIABLENAME,
                        FIELDID,
                        SEQNO,
                        TABLENAME,
                        DATA,
                        ACTION,
                        STEPID,
                        ENTEREDBY,
                        ENTEREDBYNAME,
                        ENTERED_TZVAL});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void IWRS_VISIT_SPAsync(
                    string SUBJID, 
                    string VISIT, 
                    string CritCode, 
                    string Criteria, 
                    string Field1, 
                    string Field2, 
                    string Field3, 
                    string Field4, 
                    string Field5, 
                    string Field6, 
                    string CONDITION1, 
                    string Condition2, 
                    string Condition3, 
                    string Condition4, 
                    string Condition5, 
                    string Condition6, 
                    string Value1, 
                    string Value2, 
                    string Value3, 
                    string Value4, 
                    string Value5, 
                    string Value6, 
                    string AndOr1, 
                    string AndOr2, 
                    string AndOr3, 
                    string AndOr4, 
                    string AndOr5, 
                    string DEPEND, 
                    string EARLY_DATE, 
                    string LATE_DATE, 
                    string LAST_VISIT_DATE, 
                    string NEXT_VISIT_DATE, 
                    string CurrentDate, 
                    string LAST_VISIT, 
                    string COL_NAME, 
                    string VISITNUM, 
                    string INDICATION_ID, 
                    string NEXT_VISIT, 
                    string NEXT_VISITNUM, 
                    string WINDOW, 
                    string EARLY, 
                    string LATE, 
                    string MODULEID, 
                    string VARIABLENAME, 
                    string FIELDID, 
                    string SEQNO, 
                    string TABLENAME, 
                    string DATA, 
                    string ACTION, 
                    string STEPID, 
                    string ENTEREDBY, 
                    string ENTEREDBYNAME, 
                    string ENTERED_TZVAL) {
            this.IWRS_VISIT_SPAsync(SUBJID, VISIT, CritCode, Criteria, Field1, Field2, Field3, Field4, Field5, Field6, CONDITION1, Condition2, Condition3, Condition4, Condition5, Condition6, Value1, Value2, Value3, Value4, Value5, Value6, AndOr1, AndOr2, AndOr3, AndOr4, AndOr5, DEPEND, EARLY_DATE, LATE_DATE, LAST_VISIT_DATE, NEXT_VISIT_DATE, CurrentDate, LAST_VISIT, COL_NAME, VISITNUM, INDICATION_ID, NEXT_VISIT, NEXT_VISITNUM, WINDOW, EARLY, LATE, MODULEID, VARIABLENAME, FIELDID, SEQNO, TABLENAME, DATA, ACTION, STEPID, ENTEREDBY, ENTEREDBYNAME, ENTERED_TZVAL, null);
        }
        
        /// <remarks/>
        public void IWRS_VISIT_SPAsync(
                    string SUBJID, 
                    string VISIT, 
                    string CritCode, 
                    string Criteria, 
                    string Field1, 
                    string Field2, 
                    string Field3, 
                    string Field4, 
                    string Field5, 
                    string Field6, 
                    string CONDITION1, 
                    string Condition2, 
                    string Condition3, 
                    string Condition4, 
                    string Condition5, 
                    string Condition6, 
                    string Value1, 
                    string Value2, 
                    string Value3, 
                    string Value4, 
                    string Value5, 
                    string Value6, 
                    string AndOr1, 
                    string AndOr2, 
                    string AndOr3, 
                    string AndOr4, 
                    string AndOr5, 
                    string DEPEND, 
                    string EARLY_DATE, 
                    string LATE_DATE, 
                    string LAST_VISIT_DATE, 
                    string NEXT_VISIT_DATE, 
                    string CurrentDate, 
                    string LAST_VISIT, 
                    string COL_NAME, 
                    string VISITNUM, 
                    string INDICATION_ID, 
                    string NEXT_VISIT, 
                    string NEXT_VISITNUM, 
                    string WINDOW, 
                    string EARLY, 
                    string LATE, 
                    string MODULEID, 
                    string VARIABLENAME, 
                    string FIELDID, 
                    string SEQNO, 
                    string TABLENAME, 
                    string DATA, 
                    string ACTION, 
                    string STEPID, 
                    string ENTEREDBY, 
                    string ENTEREDBYNAME, 
                    string ENTERED_TZVAL, 
                    object userState) {
            if ((this.IWRS_VISIT_SPOperationCompleted == null)) {
                this.IWRS_VISIT_SPOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIWRS_VISIT_SPOperationCompleted);
            }
            this.InvokeAsync("IWRS_VISIT_SP", new object[] {
                        SUBJID,
                        VISIT,
                        CritCode,
                        Criteria,
                        Field1,
                        Field2,
                        Field3,
                        Field4,
                        Field5,
                        Field6,
                        CONDITION1,
                        Condition2,
                        Condition3,
                        Condition4,
                        Condition5,
                        Condition6,
                        Value1,
                        Value2,
                        Value3,
                        Value4,
                        Value5,
                        Value6,
                        AndOr1,
                        AndOr2,
                        AndOr3,
                        AndOr4,
                        AndOr5,
                        DEPEND,
                        EARLY_DATE,
                        LATE_DATE,
                        LAST_VISIT_DATE,
                        NEXT_VISIT_DATE,
                        CurrentDate,
                        LAST_VISIT,
                        COL_NAME,
                        VISITNUM,
                        INDICATION_ID,
                        NEXT_VISIT,
                        NEXT_VISITNUM,
                        WINDOW,
                        EARLY,
                        LATE,
                        MODULEID,
                        VARIABLENAME,
                        FIELDID,
                        SEQNO,
                        TABLENAME,
                        DATA,
                        ACTION,
                        STEPID,
                        ENTEREDBY,
                        ENTEREDBYNAME,
                        ENTERED_TZVAL}, this.IWRS_VISIT_SPOperationCompleted, userState);
        }
        
        private void OnIWRS_VISIT_SPOperationCompleted(object arg) {
            if ((this.IWRS_VISIT_SPCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IWRS_VISIT_SPCompleted(this, new IWRS_VISIT_SPCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IWRS_KITS_SP", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet IWRS_KITS_SP(
                    string SITEID, 
                    string SUBSITEID, 
                    string RANDNO, 
                    string SUBJID, 
                    string VISITNUM, 
                    string ENTEREDBY, 
                    string VISIT, 
                    string OLD_KITNO, 
                    string NEW_KITNO, 
                    string REASON, 
                    string STEPID, 
                    string CurrentDate, 
                    string WHERE, 
                    string TREAT_STRENGTH, 
                    string STATUSNAME, 
                    string TREAT_GRP, 
                    string LAST_VISIT, 
                    string LAST_VISIT_DATE, 
                    string QUANTITY, 
                    string ACTION, 
                    string INDICATION_ID, 
                    string LOTNO, 
                    string KITNO, 
                    string EXPIRYDAT, 
                    string EXPIRY_COMMENT, 
                    string USERID, 
                    string User_Name, 
                    string TZ_VAL, 
                    string DISPENSE_IDX) {
            object[] results = this.Invoke("IWRS_KITS_SP", new object[] {
                        SITEID,
                        SUBSITEID,
                        RANDNO,
                        SUBJID,
                        VISITNUM,
                        ENTEREDBY,
                        VISIT,
                        OLD_KITNO,
                        NEW_KITNO,
                        REASON,
                        STEPID,
                        CurrentDate,
                        WHERE,
                        TREAT_STRENGTH,
                        STATUSNAME,
                        TREAT_GRP,
                        LAST_VISIT,
                        LAST_VISIT_DATE,
                        QUANTITY,
                        ACTION,
                        INDICATION_ID,
                        LOTNO,
                        KITNO,
                        EXPIRYDAT,
                        EXPIRY_COMMENT,
                        USERID,
                        User_Name,
                        TZ_VAL,
                        DISPENSE_IDX});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void IWRS_KITS_SPAsync(
                    string SITEID, 
                    string SUBSITEID, 
                    string RANDNO, 
                    string SUBJID, 
                    string VISITNUM, 
                    string ENTEREDBY, 
                    string VISIT, 
                    string OLD_KITNO, 
                    string NEW_KITNO, 
                    string REASON, 
                    string STEPID, 
                    string CurrentDate, 
                    string WHERE, 
                    string TREAT_STRENGTH, 
                    string STATUSNAME, 
                    string TREAT_GRP, 
                    string LAST_VISIT, 
                    string LAST_VISIT_DATE, 
                    string QUANTITY, 
                    string ACTION, 
                    string INDICATION_ID, 
                    string LOTNO, 
                    string KITNO, 
                    string EXPIRYDAT, 
                    string EXPIRY_COMMENT, 
                    string USERID, 
                    string User_Name, 
                    string TZ_VAL, 
                    string DISPENSE_IDX) {
            this.IWRS_KITS_SPAsync(SITEID, SUBSITEID, RANDNO, SUBJID, VISITNUM, ENTEREDBY, VISIT, OLD_KITNO, NEW_KITNO, REASON, STEPID, CurrentDate, WHERE, TREAT_STRENGTH, STATUSNAME, TREAT_GRP, LAST_VISIT, LAST_VISIT_DATE, QUANTITY, ACTION, INDICATION_ID, LOTNO, KITNO, EXPIRYDAT, EXPIRY_COMMENT, USERID, User_Name, TZ_VAL, DISPENSE_IDX, null);
        }
        
        /// <remarks/>
        public void IWRS_KITS_SPAsync(
                    string SITEID, 
                    string SUBSITEID, 
                    string RANDNO, 
                    string SUBJID, 
                    string VISITNUM, 
                    string ENTEREDBY, 
                    string VISIT, 
                    string OLD_KITNO, 
                    string NEW_KITNO, 
                    string REASON, 
                    string STEPID, 
                    string CurrentDate, 
                    string WHERE, 
                    string TREAT_STRENGTH, 
                    string STATUSNAME, 
                    string TREAT_GRP, 
                    string LAST_VISIT, 
                    string LAST_VISIT_DATE, 
                    string QUANTITY, 
                    string ACTION, 
                    string INDICATION_ID, 
                    string LOTNO, 
                    string KITNO, 
                    string EXPIRYDAT, 
                    string EXPIRY_COMMENT, 
                    string USERID, 
                    string User_Name, 
                    string TZ_VAL, 
                    string DISPENSE_IDX, 
                    object userState) {
            if ((this.IWRS_KITS_SPOperationCompleted == null)) {
                this.IWRS_KITS_SPOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIWRS_KITS_SPOperationCompleted);
            }
            this.InvokeAsync("IWRS_KITS_SP", new object[] {
                        SITEID,
                        SUBSITEID,
                        RANDNO,
                        SUBJID,
                        VISITNUM,
                        ENTEREDBY,
                        VISIT,
                        OLD_KITNO,
                        NEW_KITNO,
                        REASON,
                        STEPID,
                        CurrentDate,
                        WHERE,
                        TREAT_STRENGTH,
                        STATUSNAME,
                        TREAT_GRP,
                        LAST_VISIT,
                        LAST_VISIT_DATE,
                        QUANTITY,
                        ACTION,
                        INDICATION_ID,
                        LOTNO,
                        KITNO,
                        EXPIRYDAT,
                        EXPIRY_COMMENT,
                        USERID,
                        User_Name,
                        TZ_VAL,
                        DISPENSE_IDX}, this.IWRS_KITS_SPOperationCompleted, userState);
        }
        
        private void OnIWRS_KITS_SPOperationCompleted(object arg) {
            if ((this.IWRS_KITS_SPCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IWRS_KITS_SPCompleted(this, new IWRS_KITS_SPCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void IWRS_INSERT_DATA_SPCompletedEventHandler(object sender, IWRS_INSERT_DATA_SPCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IWRS_INSERT_DATA_SPCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IWRS_INSERT_DATA_SPCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void IWRS_ACTIONS_SPCompletedEventHandler(object sender, IWRS_ACTIONS_SPCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IWRS_ACTIONS_SPCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IWRS_ACTIONS_SPCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void IWRS_VISIT_SPCompletedEventHandler(object sender, IWRS_VISIT_SPCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IWRS_VISIT_SPCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IWRS_VISIT_SPCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    public delegate void IWRS_KITS_SPCompletedEventHandler(object sender, IWRS_KITS_SPCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9037.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IWRS_KITS_SPCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IWRS_KITS_SPCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591