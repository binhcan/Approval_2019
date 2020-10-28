using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace Approval
{
    public partial class Manage_Detail : System.Web.UI.Page
    {
        string per, pat, use;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            use = Session["id"].ToString();
            per = Session["per"].ToString();
            pat = Session["pat"].ToString();
            if (!IsPostBack)
            {

                LoadData();
            }
        }
        public void LoadData()
        {
            string sql = "select a.id,a.reson,a.part,a.jobname,a.model,a.note_date,a.note_no,a.checked,a.status,a.kittingstatus,b.fullname as confirm_by,a.message "
                        + " from it_note a "
                        + " left join it_note_user b on b.ID = a.confirm_by "
                        + " where (a.kittingstatus = 2 and a.status = 0 and a.create_by='" + int.Parse(use.Trim()) + "') or ( a.status in (1,3) and a.checked !=1 and a.part='" + pat + "' and a.create_by='" + int.Parse(use.Trim()) + "') order by a.note_date desc";
            DataTable dt = data.GetDataTable(sql);
            grvNote_detail.DataSource = dt;
            grvNote_detail.DataBind();
        }
        int stt = 1;
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
        protected void grvNote_detail_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this,event)");
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this,event)");
                if (e.Row.Cells[10].Text.Contains("2"))
                {
                    e.Row.Cells[10].Text = "Not Approved";
                }
                else if (e.Row.Cells[10].Text.Contains("3"))
                    e.Row.Cells[10].Text = "Vote NG";
                else if (e.Row.Cells[10].Text.Contains("0"))
                    e.Row.Cells[10].Text = "Wait Approve";
                else if (e.Row.Cells[10].Text.Contains("1"))
                {
                    e.Row.Cells[10].Text = "Approved";
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }

                //vote return 
                //if (e.Row.Cells[11].Text.Contains("2"))
                //{
                //    e.Row.Cells[10].Text = "Vote return";
                //    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                //}
            }
        }
        // protected void grvNote_RowCreated(object sender, GridViewRowEventArgs e)
        // {
        //     if (e.Row.RowType == DataControlRowType.DataRow)
        //     {
        //         PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

        //         string behaviorID = "pce_" + e.Row.RowIndex;
        //         pce.BehaviorID = behaviorID;

        //         Image img = (Image)e.Row.FindControl("Image1");

        //         string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
        //         string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

        //         img.Attributes.Add("onmouseover", OnMouseOverScript);
        //         img.Attributes.Add("onmouseout", OnMouseOutScript);
        //     }
        // }
        // [System.Web.Services.WebMethodAttribute(),
        //System.Web.Script.Services.ScriptMethodAttribute()]
        // public static string GetDynamicContent(string contextKey)
        // {
        //     string sql = "Select * from it_note where id=" + contextKey;
        //     DataProfile data = new DataProfile();
        //     DataTable table = data.GetDataTable(sql);
        //     if (table.Rows[0]["message"].ToString() != "")
        //     {

        //         StringBuilder b = new StringBuilder();
        //         b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
        //         b.Append("width:200px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

        //         b.Append("<tr><td colspan='2' style='background-color:#336699; color:white;'>");
        //         b.Append("<center><b>Message</b></center>"); b.Append("</td></tr>");


        //         b.Append("<tr>");
        //         b.Append("<td colspan='2'>" + table.Rows[0]["message"].ToString() + "</td>");

        //         b.Append("</tr>");

        //         b.Append("</table>");

        //         return b.ToString();
        //     }
        //     else return "";
        // }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvNote_detail.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvNote_detail.DataKeys[row.RowIndex].Value.ToString());
                    DataTable tam = data.GetDataTable("select * from it_note where id = " + id);
                    if (tam.Rows.Count > 0)
                    {
                        Response.Redirect("Edit_Detail.aspx?id=" + id);
                    }
                }

            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Create_TEV.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvNote_detail.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("cbSelectAll");
                if (chk.Checked)
                {
                    int id = int.Parse(grvNote_detail.DataKeys[row.RowIndex].Value.ToString());
                    //data.ExcuteQuery("Delete it_note_detail where note_id = " + id);
                    data.ExcuteQuery("Update it_note set status=2 where (checked = 0  or checked = 2) and id = " + id);
                }
            }
            LoadData();
        }

        protected void grvNote_detail_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grvNote_detail.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvNote_detail.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }

        protected void Downloadreport_Click(object sender, EventArgs e)
        {
            Response.Redirect("DownloadNote.aspx");
        }
        [System.Web.Services.WebMethodAttribute(),
 System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetDynamicContent(string contextKey)
        {
            string sql = "Select * from it_note where id=" + contextKey;
            DataProfile data = new DataProfile();
            DataTable table = data.GetDataTable(sql);
            if (table.Rows[0]["message"].ToString() != "")
            {

                StringBuilder b = new StringBuilder();
                b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
                b.Append("width:200px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

                b.Append("<tr><td colspan='2' style='background-color:#336699; color:white;'>");
                b.Append("<center><b>Message</b></center>"); b.Append("</td></tr>");


                b.Append("<tr>");
                b.Append("<td colspan='2'>" + table.Rows[0]["message"].ToString() + "</td>");

                b.Append("</tr>");

                b.Append("</table>");

                return b.ToString();
            }
            else return "";
        }
    }

}