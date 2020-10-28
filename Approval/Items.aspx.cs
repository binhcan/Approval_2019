using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;

namespace Approval
{
    public partial class Items : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        int use;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            use = Convert.ToInt32(Session["id"].ToString());
            if (!IsPostBack)
            {
                load_trang();
                LoadData();
            }
        }
        public void load_trang()
        {
            txtCode.Text = "";
            txtName.Text = "";
            //btnEdit.Visible = false;           
        }
        public void refres()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtsearch.Text = "";
            HiddenField1.Value = "";
        }
        public void LoadData()
        {
            string sql = "";
            if (String.IsNullOrEmpty(txtsearch.Text))
            {
                sql = "select * from Item ";
            }
            else
            {
                sql = "select * from Item where item = '" + txtsearch.Text.Trim() + "' ";
            }

            DataTable tbl = data.GetDataTable(sql);
            grvItem.DataSource = tbl;
            grvItem.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtName.Text))
            {
                Response.Write("<script language='javascript'> alert('Bạn phải nhập đủ dữ liệu!!!') </script>");
            }
            else
            {
                if (HiddenField1.Value != "")
                {
                    int idlo = int.Parse(HiddenField1.Value.ToString());
                    string sql = "update item set item='" + txtCode.Text.Trim() + "',description='" + txtName.Text.Trim() + "',update_date = getdate(), update_by = '" + use + "' where id=" + idlo;
                    data.ExcuteQuery(sql);
                    refres();
                    LoadData();
                }
                else
                {
                    string sql2 = "insert into Item(item,description,Create_by,Create_date) "
                    + " values('" + txtCode.Text + "','" + txtName.Text + "','" + use + "',getdate())";
                    data.ExcuteQuery(sql2);
                    refres();
                    LoadData();
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvItem.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvItem.DataKeys[row.RowIndex].Value.ToString());
                    HiddenField1.Value = id.ToString();
                    DataTable tam = data.GetDataTable("select * from locationdetail where id = " + id);
                    if (tam.Rows.Count > 0)
                    {
                        txtCode.Text = tam.Rows[0]["item"].ToString();
                        txtName.Text = tam.Rows[0]["description"].ToString();
                        txtsearch.Text = "";
                    }
                    break;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvItem.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvItem.DataKeys[row.RowIndex].Value.ToString());
                    data.ExcuteQuery("delete from Item where id=" + id);
                    break;
                }

            }
            refres();
            LoadData();
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("warehouse.aspx");
        }
        protected void grvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvItem.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvItem.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }
        protected void grvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }
        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btrefresh_Click(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            LoadData();
            refres();
        }

        protected void btexport_Click(object sender, EventArgs e)
        {
            string sql1 = "select * from Item";        

                DataTable Hoso = data.GetDataTable(sql1);
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = Hoso;
                GridView1.DataBind();
                //GridDecorator.MergeRows(GridView1);
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename= Item_List.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    // GridView_ListDefault.AllowPaging = false;
                    //LoadData();

                    GridView1.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
          
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}