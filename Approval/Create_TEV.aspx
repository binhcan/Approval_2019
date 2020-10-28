<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create_TEV.aspx.cs" Inherits="Approval.Create_TEV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>CREATE VOTE
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>

    <section class="content container-fluid">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Information:</h3>
                </div>
                <div class="box-body">
                    <div class="col-xs-12">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Department</label>
                                <asp:TextBox ID="txtPart" runat="server" ReadOnly="True" class="form-control pull-right"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Approval reason</label>
                                <asp:DropDownList ID="drreason" runat="server" class="form-control pull-right" AutoPostBack="true" OnSelectedIndexChanged="drreason_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </div>
                        </div>                        
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Warehouse</label>
                                <asp:DropDownList ID="drwarehouse" runat="server" class="form-control pull-right" AutoPostBack="true" OnSelectedIndexChanged="drwarehouse_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Planner</label>
                                <asp:DropDownList ID="drplaner" runat="server" class="form-control pull-right" AutoPostBack="true" OnSelectedIndexChanged="drplaner_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Date:</label>
                            <div class="input-group date" id="datepicker">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox ID="txtDate" runat="server" class="form-control pull-right"></asp:TextBox>
                                <%--<input type="text"  runat="server" class="form-control pull-right" >--%>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <label>Reason Code:</label>
                            <asp:TextBox ID="txtReson_no" class="form-control pull-right" runat="server" AutoPostBack="True" OnTextChanged="txtReson_no_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblReson" runat="server" ForeColor="#FF3300"></asp:Label>
                        </div>
                        <%--<div class="form-group">
                            <label>From Depart:</label>
                            <asp:TextBox ID="txtPart" runat="server" ReadOnly="True" class="form-control pull-right"
                                TabIndex="1"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Issue to:</label>
                            <asp:DropDownList ID="drToPart" runat="server" class="form-control pull-right">
                                <asp:ListItem>AI</asp:ListItem>
                                <asp:ListItem>MF</asp:ListItem>
                                <asp:ListItem>LOG</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                    </div>
                    <div class="col-md-6">
                        <%--<div class="form-group">
                            <label>Org:</label>
                            <asp:DropDownList ID="drOrg" runat="server" class="form-control pull-right">
                            </asp:DropDownList>
                        </div>--%>

                        <div class="form-group">
                            <label>Jobname:</label>
                            <asp:TextBox ID="txtJobname" runat="server" class="form-control pull-right"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            
                            <label>Model:</label>
                            <br />
                            <asp:TextBox ID="txtModel" runat="server" class="form-control pull-right"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="box-footer">
                    <div class="col-md-6">
                        <asp:Button ID="btnContinue" class="btn btn-primary btn-md" runat="server" Text="Continue" OnClick="btnContinue_Click" />
                        <asp:Button ID="btnEnd" class="btn btn-danger btn-md" runat="server" Text="Cancel" OnClick="btnEnd_Click" />
                    </div>
                </div>
            </div>
        </div>       

    </section>
</asp:Content>
