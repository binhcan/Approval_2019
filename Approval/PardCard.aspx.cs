using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class PardCard : System.Web.UI.Page
    {
        string per, pat;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["per"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            per = Session["per"].ToString();
            if (!IsPostBack)
            {
                Load_warehouse();
                Load_drDept();
                LoadData();
            }
        }
        private void Load_drDept()
        {
            string sql = "select * from Dept";
            DataTable tbl = data.GetDataTable(sql);
            drdept.DataSource = tbl;
            drdept.DataTextField = "Dept";
            drdept.DataValueField = "Dept";
            drdept.DataBind();

            drdept.Items.Insert(0, "--Select Dept--");

        }
        private void Load_warehouse()
        {
            string sql = "select *  from Warehouse ";
            DataTable tbl = data.GetDataTable(sql);
            drwarehouse.DataSource = tbl;
            drwarehouse.DataTextField = "warehouse";
            drwarehouse.DataValueField = "ID_WH";
            drwarehouse.DataBind();

            drwarehouse.Items.Insert(0, "--Warehouse--");

        }
        private void LoadData()
        {
            string sql = "";

            sql = "select a.*, b.fullname from it_note a left join IT_NOTE_USER b on a.user_kitting = b.ID " +
                " where a.partcardstatus = 1 and a.status = 1 and a.checked=1 and a.kittingstatus= 0 and a.warehouse = 5 and (a.reasoncode = 2 or a.reasoncode = 3 or a.reasoncode = 6) order by note_date desc";

            //if (drdept.SelectedIndex == 0 && drwarehouse.SelectedIndex == 0)
            //{
            //    sql = "select a.*, b.fullname from it_note a left join IT_NOTE_USER b on a.user_kitting = b.ID " +
            //        " where (a.reasoncode = 2 or reasoncode = 3 or reasoncode = 6) and a.status = 1 and a.checked=1 and a.kittingstatus = 0 order by note_date desc";

            //}
            //else if (drdept.SelectedIndex != 0 && drwarehouse.SelectedIndex != 0)
            //{
            //    sql = "select a.*, b.fullname from it_note a left join IT_NOTE_USER b on a.user_kitting = b.ID " +
            //        " where (a.reasoncode = 2 or reasoncode = 3 or reasoncode = 6) and a.status = 1 and a.checked=1 and a.part='" + drdept.Text + "' and a.warehouse='" + drwarehouse.SelectedValue + "' order by note_date desc";
            //}
            //else
            //{
            //    if (drdept.SelectedIndex != 0)
            //    {
            //        sql = "select a.*, b.fullname from it_note a left join IT_NOTE_USER b on a.user_kitting = b.ID " +
            //            " (a.reasoncode = 2 or reasoncode = 3 or reasoncode = 6) and where a.status = 1 and a.checked=1  and a.part='" + drdept.Text + "' order by note_date desc";
            //    }
            //    if (drwarehouse.SelectedIndex != 0)
            //    {
            //        sql = "select a.*, b.fullname from it_note a left join IT_NOTE_USER b on a.user_kitting = b.ID " +
            //            " (a.reasoncode = 2 or reasoncode = 3 or reasoncode = 6) and where a.status = 1 and a.checked=1  and a.warehouse='" + drwarehouse.SelectedValue + "' order by note_date desc";
            //    }
            //}
            DataTable dt = data.GetDataTable(sql);
            grvNote.DataSource = dt;
            grvNote.DataBind();
        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
        protected void grvNote_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvNote.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvNote.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }
        protected void grvNote_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this,event)");
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this,event)");
                if (e.Row.Cells[10].Text.Contains("2"))
                {
                    e.Row.Cells[10].Text = "Qty not OK";
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }
                else if (e.Row.Cells[10].Text.Contains("1"))
                {
                    e.Row.Cells[10].Text = "Qty OK";
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }

                else if (!string.IsNullOrEmpty(e.Row.Cells[9].Text) && e.Row.Cells[9].Text != "&nbsp;")
                {
                    e.Row.Cells[10].Text = "Kitting";
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                }
                else
                {
                    e.Row.Cells[10].Text = "";
                }

            }
        }

        protected void btnrefresh_Click(object sender, EventArgs e)
        {
            drdept.SelectedIndex = 0;
            drwarehouse.SelectedIndex = 0;
            LoadData();
        }
        protected void btcreate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvNote.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvNote.DataKeys[row.RowIndex].Value.ToString());
                    Response.Redirect("CreatePardCard.aspx?id=" + id);
                    break;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("warehouse.aspx");
        }
    }
}