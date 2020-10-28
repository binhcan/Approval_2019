using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class Censorship_TEV : System.Web.UI.Page
    {
        string pat;
        string per;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["per"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            //cb = Session["Quyen"].ToString();
            per = Session["per"].ToString();
            pat = Session["pat"].ToString();
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            //string sql = "select id,jobname,model,reson,part,note_date,note_no,create_by,confirm_by, checked from it_note where status=1 and ((part='" + pat + "' and checked =0)  or (semi=0 and checked =0 and topart='" + pat + "')) order by note_date desc";
            string sql = "select a.id,a.jobname,a.model,a.reson,a.part,a.note_date,a.note_no,b1.FULLNAME as CREATE_BY,b2.FULLNAME as CONFIRM_BY, checked "
                        + "from it_note a "
                        + " left join it_note_user b1 on b1.ID = a.CREATE_BY "
                        + " left join it_note_user b2 on b2.ID = a.CONFIRM_BY "
                        + " where status=1 and (part='" + pat + "' and checked =0) order by note_date desc";
            DataTable dt = data.GetDataTable(sql);
            grvNote.DataSource = dt;
            grvNote.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnView.Visible = true;

            }
            else
            {
                btnView.Visible = false;
            }
        }
        protected void grvNote_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this,event)");
                //e.Row.Attributes.Add("onmouseover", "MouseEvents(this,event)");
                if (e.Row.Cells[11].Text.Contains("2"))
                {
                    e.Row.Cells[11].Text = "Not Approved";
                }
                else e.Row.Cells[11].Text = "";
            }
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //Find the checkbox control in header and add an attribute
            //    ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
            //            ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            //} 

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
        protected void btnView_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvNote.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvNote.DataKeys[row.RowIndex].Value.ToString());
                    if (per.Contains("1"))
                    {
                        Response.Redirect("View_TEV.aspx?id=" + id);
                    }
                    if (per == "4")
                    {
                        Response.Redirect("View_TEV_C.aspx?id=" + id);
                    }
                }
            }
        }
    }
}