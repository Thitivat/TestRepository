﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.33440.
// 


/// <remarks/>
// CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="IReversalService_Endpoint", Namespace="http://service.matrix.financials.fivedegrees.nl/1.0")]
public partial class ReversalServiceBSL : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback ReversibleOperationCompleted;
    
    private System.Threading.SendOrPostCallback RevertOperationCompleted;
    
    /// <remarks/>
    public ReversalServiceBSL() {
        this.Url = "https://localhost:44310/Service.svc";
    }
    
    /// <remarks/>
    public event ReversibleCompletedEventHandler ReversibleCompleted;
    
    /// <remarks/>
    public event RevertCompletedEventHandler RevertCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.matrix.financials.fivedegrees.nl/1.0/IReversalService/Reversible", RequestNamespace="http://service.matrix.financials.fivedegrees.nl/1.0", ResponseNamespace="http://service.matrix.financials.fivedegrees.nl/1.0", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public OperationResult_ReversibleItem Reversible(string correlationId, int centerId, [System.Xml.Serialization.XmlIgnoreAttribute()] bool centerIdSpecified, long operationId, [System.Xml.Serialization.XmlIgnoreAttribute()] bool operationIdSpecified) {
        object[] results = this.Invoke("Reversible", new object[] {
                    correlationId,
                    centerId,
                    centerIdSpecified,
                    operationId,
                    operationIdSpecified});
        return ((OperationResult_ReversibleItem)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginReversible(string correlationId, int centerId, bool centerIdSpecified, long operationId, bool operationIdSpecified, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("Reversible", new object[] {
                    correlationId,
                    centerId,
                    centerIdSpecified,
                    operationId,
                    operationIdSpecified}, callback, asyncState);
    }
    
    /// <remarks/>
    public OperationResult_ReversibleItem EndReversible(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((OperationResult_ReversibleItem)(results[0]));
    }
    
    /// <remarks/>
    public void ReversibleAsync(string correlationId, int centerId, bool centerIdSpecified, long operationId, bool operationIdSpecified) {
        this.ReversibleAsync(correlationId, centerId, centerIdSpecified, operationId, operationIdSpecified, null);
    }
    
    /// <remarks/>
    public void ReversibleAsync(string correlationId, int centerId, bool centerIdSpecified, long operationId, bool operationIdSpecified, object userState) {
        if ((this.ReversibleOperationCompleted == null)) {
            this.ReversibleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReversibleOperationCompleted);
        }
        this.InvokeAsync("Reversible", new object[] {
                    correlationId,
                    centerId,
                    centerIdSpecified,
                    operationId,
                    operationIdSpecified}, this.ReversibleOperationCompleted, userState);
    }
    
    private void OnReversibleOperationCompleted(object arg) {
        if ((this.ReversibleCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.ReversibleCompleted(this, new ReversibleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.matrix.financials.fivedegrees.nl/1.0/IReversalService/Revert", RequestNamespace="http://service.matrix.financials.fivedegrees.nl/1.0", ResponseNamespace="http://service.matrix.financials.fivedegrees.nl/1.0", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public OperationResult_ReversalResultItem Revert(string correlationId, int centerId, [System.Xml.Serialization.XmlIgnoreAttribute()] bool centerIdSpecified, long operationId, [System.Xml.Serialization.XmlIgnoreAttribute()] bool operationIdSpecified) {
        object[] results = this.Invoke("Revert", new object[] {
                    correlationId,
                    centerId,
                    centerIdSpecified,
                    operationId,
                    operationIdSpecified});
        return ((OperationResult_ReversalResultItem)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginRevert(string correlationId, int centerId, bool centerIdSpecified, long operationId, bool operationIdSpecified, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("Revert", new object[] {
                    correlationId,
                    centerId,
                    centerIdSpecified,
                    operationId,
                    operationIdSpecified}, callback, asyncState);
    }
    
    /// <remarks/>
    public OperationResult_ReversalResultItem EndRevert(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((OperationResult_ReversalResultItem)(results[0]));
    }
    
    /// <remarks/>
    public void RevertAsync(string correlationId, int centerId, bool centerIdSpecified, long operationId, bool operationIdSpecified) {
        this.RevertAsync(correlationId, centerId, centerIdSpecified, operationId, operationIdSpecified, null);
    }
    
    /// <remarks/>
    public void RevertAsync(string correlationId, int centerId, bool centerIdSpecified, long operationId, bool operationIdSpecified, object userState) {
        if ((this.RevertOperationCompleted == null)) {
            this.RevertOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRevertOperationCompleted);
        }
        this.InvokeAsync("Revert", new object[] {
                    correlationId,
                    centerId,
                    centerIdSpecified,
                    operationId,
                    operationIdSpecified}, this.RevertOperationCompleted, userState);
    }
    
    private void OnRevertOperationCompleted(object arg) {
        if ((this.RevertCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.RevertCompleted(this, new RevertCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:OperationResult")]
public partial class OperationResult_ReversibleItem {
    
    private int resultCodeField;
    
    private long operationIdField;
    
    private string errorMessageField;
    
    private ReversibleItem dataField;
    
    private int centerIdField;
    
    private OperationStatuses statusField;
    
    private bool statusFieldSpecified;
    
    private bool waitingApprovalField;
    
    /// <remarks/>
    public int ResultCode {
        get {
            return this.resultCodeField;
        }
        set {
            this.resultCodeField = value;
        }
    }
    
    /// <remarks/>
    public long OperationId {
        get {
            return this.operationIdField;
        }
        set {
            this.operationIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string ErrorMessage {
        get {
            return this.errorMessageField;
        }
        set {
            this.errorMessageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public ReversibleItem Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
    
    /// <remarks/>
    public int CenterId {
        get {
            return this.centerIdField;
        }
        set {
            this.centerIdField = value;
        }
    }
    
    /// <remarks/>
    public OperationStatuses Status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool StatusSpecified {
        get {
            return this.statusFieldSpecified;
        }
        set {
            this.statusFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public bool WaitingApproval {
        get {
            return this.waitingApprovalField;
        }
        set {
            this.waitingApprovalField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public partial class ReversibleItem {
    
    private bool canBeReversedField;
    
    private long operationIdField;
    
    private MovementItem[] candidatesField;
    
    private string reasonField;
    
    /// <remarks/>
    public bool CanBeReversed {
        get {
            return this.canBeReversedField;
        }
        set {
            this.canBeReversedField = value;
        }
    }
    
    /// <remarks/>
    public long OperationId {
        get {
            return this.operationIdField;
        }
        set {
            this.operationIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
    public MovementItem[] Candidates {
        get {
            return this.candidatesField;
        }
        set {
            this.candidatesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string Reason {
        get {
            return this.reasonField;
        }
        set {
            this.reasonField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public partial class MovementItem {
    
    private int paymentIdField;
    
    private System.DateTime valueDateField;
    
    private decimal amountField;
    
    private string referenceField;
    
    private string operationNameField;
    
    /// <remarks/>
    public int PaymentId {
        get {
            return this.paymentIdField;
        }
        set {
            this.paymentIdField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime ValueDate {
        get {
            return this.valueDateField;
        }
        set {
            this.valueDateField = value;
        }
    }
    
    /// <remarks/>
    public decimal Amount {
        get {
            return this.amountField;
        }
        set {
            this.amountField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string Reference {
        get {
            return this.referenceField;
        }
        set {
            this.referenceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string OperationName {
        get {
            return this.operationNameField;
        }
        set {
            this.operationNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public partial class EventDateItem {
    
    private System.Nullable<int> userIdField;
    
    private bool userIdFieldSpecified;
    
    private string userNameField;
    
    private System.Nullable<System.DateTime> eventDateField;
    
    private bool eventDateFieldSpecified;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public System.Nullable<int> UserId {
        get {
            return this.userIdField;
        }
        set {
            this.userIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool UserIdSpecified {
        get {
            return this.userIdFieldSpecified;
        }
        set {
            this.userIdFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string UserName {
        get {
            return this.userNameField;
        }
        set {
            this.userNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public System.Nullable<System.DateTime> EventDate {
        get {
            return this.eventDateField;
        }
        set {
            this.eventDateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool EventDateSpecified {
        get {
            return this.eventDateFieldSpecified;
        }
        set {
            this.eventDateFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public partial class EntityInfoItem {
    
    private EventDateItem createdField;
    
    private EventDateItem changedField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public EventDateItem Created {
        get {
            return this.createdField;
        }
        set {
            this.createdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public EventDateItem Changed {
        get {
            return this.changedField;
        }
        set {
            this.changedField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public partial class ReversalAffectedPaymentItem {
    
    private int paymentIdField;
    
    private bool paymentIdFieldSpecified;
    
    private string serviceField;
    
    private System.DateTime valueDateField;
    
    private bool valueDateFieldSpecified;
    
    private decimal amountField;
    
    private bool amountFieldSpecified;
    
    private string paymentTypeField;
    
    private string currencyField;
    
    private string referenceField;
    
    private string clarificationField;
    
    private string paymentSourceField;
    
    private decimal balanceAfterField;
    
    private bool balanceAfterFieldSpecified;
    
    private bool isReversedField;
    
    private bool isReversedFieldSpecified;
    
    private string operationTypeField;
    
    private EntityInfoItem entityInfoField;
    
    /// <remarks/>
    public int PaymentId {
        get {
            return this.paymentIdField;
        }
        set {
            this.paymentIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PaymentIdSpecified {
        get {
            return this.paymentIdFieldSpecified;
        }
        set {
            this.paymentIdFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string Service {
        get {
            return this.serviceField;
        }
        set {
            this.serviceField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime ValueDate {
        get {
            return this.valueDateField;
        }
        set {
            this.valueDateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ValueDateSpecified {
        get {
            return this.valueDateFieldSpecified;
        }
        set {
            this.valueDateFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public decimal Amount {
        get {
            return this.amountField;
        }
        set {
            this.amountField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool AmountSpecified {
        get {
            return this.amountFieldSpecified;
        }
        set {
            this.amountFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string PaymentType {
        get {
            return this.paymentTypeField;
        }
        set {
            this.paymentTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string Currency {
        get {
            return this.currencyField;
        }
        set {
            this.currencyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string Reference {
        get {
            return this.referenceField;
        }
        set {
            this.referenceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string Clarification {
        get {
            return this.clarificationField;
        }
        set {
            this.clarificationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string PaymentSource {
        get {
            return this.paymentSourceField;
        }
        set {
            this.paymentSourceField = value;
        }
    }
    
    /// <remarks/>
    public decimal BalanceAfter {
        get {
            return this.balanceAfterField;
        }
        set {
            this.balanceAfterField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool BalanceAfterSpecified {
        get {
            return this.balanceAfterFieldSpecified;
        }
        set {
            this.balanceAfterFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public bool IsReversed {
        get {
            return this.isReversedField;
        }
        set {
            this.isReversedField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsReversedSpecified {
        get {
            return this.isReversedFieldSpecified;
        }
        set {
            this.isReversedFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string OperationType {
        get {
            return this.operationTypeField;
        }
        set {
            this.operationTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public EntityInfoItem EntityInfo {
        get {
            return this.entityInfoField;
        }
        set {
            this.entityInfoField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public partial class ReversalResultItem {
    
    private decimal balanceAfterField;
    
    private bool balanceAfterFieldSpecified;
    
    private ReversalAffectedPaymentItem[] affectedPaymentsField;
    
    /// <remarks/>
    public decimal BalanceAfter {
        get {
            return this.balanceAfterField;
        }
        set {
            this.balanceAfterField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool BalanceAfterSpecified {
        get {
            return this.balanceAfterFieldSpecified;
        }
        set {
            this.balanceAfterFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
    public ReversalAffectedPaymentItem[] AffectedPayments {
        get {
            return this.affectedPaymentsField;
        }
        set {
            this.affectedPaymentsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:OperationResult")]
public partial class OperationResult_ReversalResultItem {
    
    private int resultCodeField;
    
    private long operationIdField;
    
    private string errorMessageField;
    
    private ReversalResultItem dataField;
    
    private int centerIdField;
    
    private OperationStatuses statusField;
    
    private bool statusFieldSpecified;
    
    private bool waitingApprovalField;
    
    /// <remarks/>
    public int ResultCode {
        get {
            return this.resultCodeField;
        }
        set {
            this.resultCodeField = value;
        }
    }
    
    /// <remarks/>
    public long OperationId {
        get {
            return this.operationIdField;
        }
        set {
            this.operationIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public string ErrorMessage {
        get {
            return this.errorMessageField;
        }
        set {
            this.errorMessageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
    public ReversalResultItem Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
    
    /// <remarks/>
    public int CenterId {
        get {
            return this.centerIdField;
        }
        set {
            this.centerIdField = value;
        }
    }
    
    /// <remarks/>
    public OperationStatuses Status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool StatusSpecified {
        get {
            return this.statusFieldSpecified;
        }
        set {
            this.statusFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public bool WaitingApproval {
        get {
            return this.waitingApprovalField;
        }
        set {
            this.waitingApprovalField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://data.matrix.financials.fivedegrees.nl/1.0")]
public enum OperationStatuses {
    
    /// <remarks/>
    Started,
    
    /// <remarks/>
    Completed,
    
    /// <remarks/>
    FunctionalError,
    
    /// <remarks/>
    OperationalError,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void ReversibleCompletedEventHandler(object sender, ReversibleCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ReversibleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal ReversibleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public OperationResult_ReversibleItem Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((OperationResult_ReversibleItem)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void RevertCompletedEventHandler(object sender, RevertCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class RevertCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal RevertCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public OperationResult_ReversalResultItem Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((OperationResult_ReversalResultItem)(this.results[0]));
        }
    }
}
