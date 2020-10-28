<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WarehouseDetail.aspx.cs" Inherits="Approval.WarehouseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= grvDetail.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
    <style>
        .cssPager td {
            padding-left: 10px;
            padding-right:  10px;
        }
    </style>
    <section class="content-header">
        <h1>VOTE DETAIL
            <small></small>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Vote No:
                        <asp:Label ID="lblvoteno" runat="server" ForeColor="#FF3300"></asp:Label></h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-minus"></i>
                        </button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-xs-12">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Department</label>
                                <asp:TextBox ID="txtPart" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Approval reason</label>
                                <asp:TextBox ID="txtreasonvote" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Warehouse</label>
                                <asp:TextBox ID="txtwarehouse" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Planner</label>
                                <asp:TextBox ID="txtplanner" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
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
                                <asp:TextBox ID="txtDate" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
                                <%--<input type="text"  runat="server" class="form-control pull-right" >--%>
                            </div>
                            <!-- /.input group -->
                        </div>
                        <div class="form-group">
                            <label>Reason Code:</label>
                            <asp:TextBox ID="txtReson_no" ReadOnly="True" CssClass="form-control pull-right" runat="server"></asp:TextBox>
                            <asp:Label ID="lblReson" runat="server" ForeColor="#FF3300"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Kitting by  </label>
                            <br />
                            <asp:TextBox ID="txtkitting" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
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
                            <asp:TextBox ID="txtJobname" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
                        </div>

                        <div class="form-group">

                            <label>Model:</label>
                            <br />
                            <asp:TextBox ID="txtModel" runat="server" ReadOnly="True" CssClass="form-control pull-right"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>----------------</label>
                            <br />
                            <asp:Button ID="btcompleted" class="btn btn-primary btn-md" runat="server" Text="Complete vote" OnClick="btcompleted_Click" OnClientClick="return confirm('Bạn có chắc muốn đóng vote này?');" />
                            <asp:Button ID="btexport" class="btn btn-success btn-md" runat="server" Text="Export Vote" OnClick="btexport_Click" />
                            <asp:Button ID="btnreturn" class="btn btn-danger btn-md" runat="server" Text="Return vote" OnClick="btnreturn_Click" />
                            <asp:Button ID="btncancel" class="btn btn-success btn-md" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                        </div>

                    </div>

                </div>
                <%--<div class="box-footer">
                    <div class="col-md-4">
                    <asp:DropDownList ID="Dropkitting" runat="server" CssClass="form-control pull-right"></asp:DropDownList>                    
                    </div>
                    <asp:Button ID="btsetkitting" class="btn btn-primary btn-md" runat="server" Text="Set Kitting" OnClick="btsetkitting_Click" />
                </div>--%>
            </div>
        </div>

        <%--<div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Information:</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Location:</label>
                            <asp:TextBox ID="txtlocation" CssClass="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Item:</label>
                            <asp:TextBox ID="txtCode" class="form-control pull-right" runat="server" AutoPostBack="true" OnTextChanged="txtCode_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtName" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <br />
                            <asp:Button ID="Button1" class="btn btn-primary btn-md" runat="server" Text="Add" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Quantity:</label>
                            <asp:TextBox ID="txtQuan" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Semi Lot:</label>
                            <asp:TextBox ID="txtSemilot" class="form-control pull-right" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Import by excel file</label>
                            <div class="col-md-9">
                                <asp:FileUpload ID="fileupload" runat="server" class="btn btn-default btn-md" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnImport" class="btn btn-primary btn-md" runat="server" Text="Import" OnClick="btnImport_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnAdd" class="btn btn-primary btn-md" runat="server" Text="Change" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" class="btn btn-success btn-md" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" class="btn btn-danger btn-md" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="javascript:return delete_confim()" />

                </div>
            </div>
        </div>--%>

        <div class="col-md-12">
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="grvDetail" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvDetail_RowDataBound"
                DataKeyNames="id" AllowPaging="True" OnPageIndexChanging="grvDetail_PageIndexChanging" Width="90%" BackColor="White"
                CssClass="Grid"
                AlternatingRowStyle-CssClass="alt"
                PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField>
                        <AlternatingItemTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" />
                        </AlternatingItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelectAll" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <%#get_stt() %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
                    <asp:BoundField DataField="location" HeaderText="Location" />
                    <asp:BoundField DataField="Item" HeaderText="Item" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="semilot" HeaderText="Semi Lot" />
                    <asp:BoundField DataField="quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="note_id" HeaderText="note_id" Visible="False" />                    
                    <asp:BoundField DataField="quantity_act" HeaderText="Qty Act" />
                    <asp:BoundField DataField="remain" HeaderText="Qty remain" />
                    <asp:BoundField DataField="quantity_daily" HeaderText="Qty Daily" />

                </Columns>
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />

            </asp:GridView>
        </div>
    </section>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="modal" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-full" role="document">

            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <label>Reason return vote</label>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h3 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h3>
                        </div>
                        <div class="modal-body">
                            <%--<asp:Literal ID="videopath" runat="server" Text=""></asp:Literal>--%>
                            <div class="text-center">
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control pull-right" TextMode="MultiLine"
                                    Width="98%" Height="85%"></asp:TextBox>
                            </div>

                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" class="btn btn-primary btn-md" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button class="btn btn-info" ID="closemodal" runat="server" Text="Cancel" OnClick="closemodal_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
