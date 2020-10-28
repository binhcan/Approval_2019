<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Edit_Detail.aspx.cs" Inherits="Approval.Edit_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function delete_confim() {
            var result = confirm("Are you sure you want delete ?");
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
            padding-right: 10px;
        }
    </style>
    <section class="content-header">
        <h1>EDIT VOTE
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <asp:Button ID="btnDone" class="btn btn-danger btn-md" runat="server" Text="Back" OnClick="btnDone_Click" />
        </ol>
    </section>

    <section class="content container-fluid">
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Information:</h3>

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
                                <asp:TextBox ID="txtPart" runat="server" ReadOnly="True" class="form-control pull-right"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Approval reason</label>
                                <asp:DropDownList ID="drreason" runat="server" class="form-control pull-right" AutoPostBack="true" OnSelectedIndexChanged="drreason_SelectedIndexChanged">
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
                        <div class="form-group">
                            <label>------  </label>
                            <br />
                            <asp:Button ID="btnOK" class="btn btn-primary btn-md" runat="server" Text="Update" OnClick="btnOK_Click" />
                        </div>
                    </div>

                </div>
                <%--                <div class="box-footer">
                    
                </div>--%>
            </div>
        </div>

        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header">
                    <%--<h3 class="box-title">Information:</h3>--%>
                </div>
                <div class="box-body">
                    <div class="col-md-6" id="boxbody1" runat="server">
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
                    <div class="col-md-6" id="boxbody2" runat="server">
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
                        <%--</div>--%>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:Button ID="btnAdd" class="btn btn-primary btn-md" runat="server" Text="Change" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" class="btn btn-success btn-md" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" class="btn btn-danger btn-md" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="javascript:return delete_confim()" />
                    <asp:Button ID="btresent" class="btn btn-success btn-md" runat="server" Text="Resend Vote" OnClick="btresent_Click" />
                    <asp:Button ID="btclosevote" class="btn btn-danger btn-md" runat="server" Text="Close Vote" OnClick="btclosevote_Click" OnClientClick="return confirm('Bạn có chắc muốn kết thúc kitting cho vote này?');" />

                </div>
            </div>
        </div>

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
                    <asp:BoundField DataField="quantity" HeaderText="Quantity">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="note_id" HeaderText="note_id" Visible="False" />
                    <asp:BoundField HeaderText="Semi Lot" DataField="semilot" />
                    <asp:BoundField HeaderText="Qty Act" DataField="quantity_act" />
                    <asp:BoundField HeaderText="Qty remain" DataField="remain" />
                </Columns>
                <PagerStyle BackColor="#99CCFF" HorizontalAlign="Center" CssClass="cssPager" />

            </asp:GridView>
        </div>
    </section>
    <section class="content container-fluid">
        <div class="col-md-12">
        </div>
    </section>
</asp:Content>
