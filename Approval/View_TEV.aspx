<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="View_TEV.aspx.cs" Inherits="Approval.View_TEV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
         function apply_confim() {
             var result = confirm("Are you sure, you want approval ?");
             if (result) {                  
                 return result
             }
             else {
                 window.location.reload();
                 return false;
             }
         }
    </script>
     <script type="text/javascript">
         function apply_confim1() {
             var result = confirm("Are you sure, you want confirm ?");
             if (result) {
                 return result
             }
             else {
                 window.location.reload();
                 return false;
             }
         }
    </script>
    <script type="text/javascript">
        function notapply_confim() {
            var result = confirm("Are you sure, not approval this vote ?");
            if (result) {
                return result
            }
            else {
                window.location.reload();
                return false;
            }
        }
    </script>
     <section class="content-header">
        <h1>Confirm Vote
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Information:</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Date:</label>
                            <asp:TextBox ID="txtDate" runat="server" class="form-control pull-right" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Reason Code:</label>
                            <asp:TextBox ID="txtReson_no" class="form-control pull-right" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblReson" runat="server" ForeColor="#FF3300"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Reason vote:   </label>
                            <br />
                            <asp:Label ID="lblresonapproval" runat="server" ForeColor="#FF3300"></asp:Label>
                        </div>
                        <%--<div class="form-group">
                            <label>From Depart:</label>
                            <asp:TextBox ID="txtPart" runat="server" ReadOnly="True" class="form-control pull-right"
                                TabIndex="1"></asp:TextBox>
                        </div>--%>
                        <div class="form-group">
                            <label>Create By:   </label>
                            <asp:Label ID="lbCreate" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Confirm By:   </label>
                            <asp:Label ID="lbConfirm" runat="server"></asp:Label>
                            <asp:Image ID="imgconfirm" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Img/Confirm.jpg" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Warehouse: </label>
                            <asp:TextBox ID="txtwarehouse" runat="server" CssClass="form-control pull-right" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Jobname: </label>
                            <asp:TextBox ID="txtJobname" class="form-control pull-right" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Model: </label>
                            <asp:TextBox ID="txtModel" runat="server" ReadOnly="True" class="form-control pull-right"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>No: </label>
                            <asp:TextBox ID="txtNo" runat="server" ReadOnly="True" class="form-control pull-right"></asp:TextBox>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <asp:GridView ID="grvDetail" runat="server" Width="90%"
                AutoGenerateColumns="False"
                DataKeyNames="id"
                OnPageIndexChanging="grvDetail_PageIndexChanging"
                OnRowDataBound="grvDetail_RowDataBound" BackColor="White"
                BorderStyle="Solid">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#get_stt() %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
                    <asp:BoundField DataField="item" HeaderText="Item" />
                    <asp:BoundField DataField="description" HeaderText="Description">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="note_id" HeaderText="note_id" Visible="False" />
                    <%--<asp:BoundField DataField="Amount" HeaderText="Amount" />--%>
                </Columns>
                <FooterStyle BackColor="#336699" Font-Bold="true" />
                <HeaderStyle BackColor="#99CCFF" />
            </asp:GridView>
        </div>
        <div class="text-center">
            <br /> >
            <div class="form-group">
                <asp:Button ID="btnApp" class="btn btn-primary btn-md" runat="server" Text="Approval" onclick="btnApp_Click" onclientclick="javascript:return apply_confim()" />
                <asp:Button ID="btnNotApp" class="btn btn-danger btn-md" runat="server" Text="Not Approval" OnClick="btnNotApp_Click" />
                <asp:Button ID="btnOK" class="btn btn-info" runat="server" Text="Done" OnClick="btnOK_Click" />
            </div>
        </div>
    </section>
    <section class="content container-fluid">
        
    </section>
        


  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="modal" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-full" role="document">

            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h3 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h3>
                        </div>
                        <div class="modal-body">
                            <%--<asp:Literal ID="videopath" runat="server" Text=""></asp:Literal>--%>
                            <div class="text-center">
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control pull-right"  TextMode="MultiLine" 
                                    Width="98%" Height="85%"></asp:TextBox>
                            </div>

                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" class="btn btn-primary btn-md" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
                            <asp:Button class="btn btn-info" ID="closemodal" runat="server" Text="Cancel" OnClick="closemodal_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
