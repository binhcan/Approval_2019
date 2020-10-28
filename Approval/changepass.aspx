<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="changepass.aspx.cs" Inherits="Approval.changepass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Change password
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="col-md-4">
								<div class="form-group">

                                    <label>User name</label>
                                    <%--<input class="form-control" placeholder="Placeholder" runat="server" ID="txthoten1">--%>
                                    <asp:TextBox ID="txtusename" class="form-control" runat="server"></asp:TextBox>
								</div>
                                <div class="form-group">
									<label>Current password</label>								                                
								    <asp:TextBox ID="txttpassold" class="form-control" type="password" runat="server"></asp:TextBox>
                                    <label>New password</label>								                                
								    <asp:TextBox ID="txttpassnew" class="form-control" type="password" runat="server"></asp:TextBox>
                                    <label>Confirm new password</label>								                                
								    <asp:TextBox ID="txttpassnew2" class="form-control" type="password" runat="server"></asp:TextBox>
                                    <br />
									<asp:Button ID="Changepassword" class="btn btn-primary" runat="server" Text="Change password" OnClick="Changepassword_Click" />
                                    <asp:Button ID="Exit" class="btn btn-success btn-md" runat="server" Text="Cancel" OnClick="Exit_Click"  />
								</div>
						</div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
