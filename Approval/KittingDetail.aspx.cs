using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Approval
{
    public partial class KittingDetail : System.Web.UI.Page
    {
        int id, reasoncode, wh, stt; string use, pat, location;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            //try
            //{
                use = Session["id"].ToString();
                pat = Session["pat"].ToString();
                id = int.Parse(Request.QueryString["id"].ToString());
                stt = 1;
            //tmpid = "";
            //get location and reasoncode
            //string sql = "select locationtmp,reasoncode,warehouse from IT_NOTE where id = '" + id + "' ";
            //DataTable tbl = data.GetDataTable(sql);
            //reasoncode = Convert.ToInt32(tbl.Rows[0]["reasoncode"].ToString());
            //location = tbl.Rows[0]["locationtmp"].ToString();
            //wh = Convert.ToInt32(tbl.Rows[0]["warehouse"].ToString());
            if (!IsPostBack)
                {
                    LoadPage();
                    LoadData();
                    LoadGridView();
                }
            //}
            //catch
            //{
            //    Response.Redirect("KittingList.aspx");
            //}
        }
        private void LoadPage()
        {
            txtItem.Text = "";
            txtqty.Text = "";
            txtItem.Focus();
            //btautoQty.Visible = false;
        }
        private void LoadData()
        {
            string sql = "select a.note_no, b.description,a.reasoncode from IT_NOTE a left join IT_Reson_approva b on a.reasoncode = b.id where a.id = '" + id + "' ";
            DataTable tbl = data.GetDataTable(sql);
            lblvoteno.Text = tbl.Rows[0]["note_no"].ToString() + "/" + tbl.Rows[0]["description"].ToString();
            if (Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 1 || Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 4 || Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 8)
            {
                btautoQty.Visible = true;
            }
            else
            {
                btautoQty.Visible = false;
            }
        }
        public void LoadGridView()
        {
            string sql2 = "select *, [quantity] - [quantity_act] as remain from it_note_detail where displaydetail = 1 and note_id=" + id;
            DataTable note_detail = data.GetDataTable(sql2);
            if (note_detail.Rows.Count > 0)
            {
                grvDetail.DataSource = note_detail;
                grvDetail.DataBind();
            }
        }

        protected void btupdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HiddenField2.Value) && !string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                string sql1 = "select Item,quantity,quantity_act,quantity_daily from IT_NOTE_DETAIL where item = '" + txtItem.Text.Trim() + "' and id = '" + HiddenField2.Value + "' ";
                DataTable tbl = data.GetDataTable(sql1);
                double tmp1 = Convert.ToDouble(tbl.Rows[0]["quantity"].ToString());
                double tmp2 = Convert.ToDouble(txtqty.Text.Trim()) + Convert.ToDouble(tbl.Rows[0]["quantity_act"].ToString());
                double tmp3 = Convert.ToDouble(txtqty.Text.Trim()) + Convert.ToDouble(tbl.Rows[0]["quantity_daily"].ToString());

                if (tbl.Rows.Count == 1 && tmp2 >= 0 && tmp2 <= tmp1)
                {
                    string sql2 = "update IT_NOTE_DETAIL set quantity_act = '" + tmp2 + "',quantity_daily = '" + tmp3 + "',kitting_date = getDate(), kitting_by = '" + use + "' " +
                              " where item = '" + txtItem.Text.Trim() + "' and id = '" + HiddenField2.Value + "' ";
                    data.ExcuteQuery(sql2);
                    HiddenField2.Value = "";
                    LoadGridView();
                    LoadPage();
                }
                else
                {
                    HiddenField2.Value = "";
                    Response.Write("<script language='javascript'> alert('Check lại Item và số lượng') </script>");
                }
            }          
                
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("KittingList.aspx");
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            string[] substrings = txtItem.Text.Trim().Split('|');
            string tmpItem = "";
            if (chSL.Checked)
            {
                 tmpItem = substrings[0];
            }
           else
            {
                 tmpItem = substrings[1];
            }

            string sql = "select id,Item,quantity,quantity_act from IT_NOTE_DETAIL where item = '" + tmpItem + "' and note_id = '" + id +"' order by quantity desc ";
            DataTable tbl = data.GetDataTable(sql);
            if (tbl != null)
            {
                if (tbl.Rows.Count ==1)
                {
                    txtqty.Focus();
                    txtItem.Text = tmpItem;
                    txtqty.Text = (Convert.ToDouble(tbl.Rows[0]["quantity"].ToString()) - Convert.ToDouble(tbl.Rows[0]["quantity_act"].ToString())).ToString();
                    HiddenField2.Value = tbl.Rows[0]["id"].ToString();
                }
                else if (tbl.Rows.Count > 1 && string.IsNullOrEmpty(tbl.Rows[0]["semilot"].ToString()))
                {
                    string sql2 = "select Item,quantity,quantity_act from IT_NOTE_DETAIL where item = '" + tmpItem + "' and quantity <> quantity_act and note_id = '" + id + "' order by quantity desc ";
                    DataTable tbl2 = data.GetDataTable(sql2);
                    txtqty.Focus();
                    txtItem.Text = tmpItem;
                    txtqty.Text = (Convert.ToDouble(tbl2.Rows[0]["quantity"].ToString()) - Convert.ToDouble(tbl2.Rows[0]["quantity_act"].ToString())).ToString();
                    HiddenField2.Value = tbl2.Rows[0]["id"].ToString();
                }
            }
            else
            {
                Response.Write("<script language='javascript'> alert('Item không có trong list!') </script>");
                LoadPage();
            }
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            int tmp = 0;
            string sqlfinish = "select * from IT_NOTE_DETAIL where note_id ='" + id + "' ";
            DataTable tblfinish = data.GetDataTable(sqlfinish);
            string sqlupdate = "";
            string sqlupdate2 = "";
            for (int i = 0;i < tblfinish.Rows.Count;i++)
            {
                double tmp1 =  Convert.ToDouble(tblfinish.Rows[i]["quantity"].ToString());
                double tmp2 = Convert.ToDouble(tblfinish.Rows[i]["quantity_act"].ToString());
                if(tmp1 != tmp2)
                {
                    sqlupdate2 = "update IT_NOTE_DETAIL set NoOfKitting ='" + Convert.ToDouble(tblfinish.Rows[i]["NoOfKitting"].ToString()) + "' + 1,statuskitting = '0' where id ='" + Convert.ToDouble(tblfinish.Rows[i]["id"].ToString()) + "'";
                    data.ExcuteQuery(sqlupdate2);
                    tmp = 1;
                }
                else
                {
                    sqlupdate2 = "update IT_NOTE_DETAIL set NoOfKitting ='" + Convert.ToDouble(tblfinish.Rows[i]["NoOfKitting"].ToString()) + "' + 1,statuskitting = '1' where id ='" + Convert.ToDouble(tblfinish.Rows[i]["id"].ToString()) + "'";
                    data.ExcuteQuery(sqlupdate2);
                }
            }

            if (tmp == 0)
            {
                sqlupdate = "update IT_NOTE set KittingStatus ='1', user_kitting = 0 where id ='" + id + "'";
                data.ExcuteQuery(sqlupdate);
            }
            else
            {
                sqlupdate = "update IT_NOTE set KittingStatus ='2' where id ='" + id + "'";
                data.ExcuteQuery(sqlupdate);
            }          
            
            Response.Redirect("KittingList.aspx");
        }

        protected void btautoQty_Click(object sender, EventArgs e)
        {
            string check = "select reasoncode from  IT_NOTE where id ='" + id + "' ";
            DataTable tbl = data.GetDataTable(check);
            if (Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 1 || Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 4 || Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 8)
            {
                string sqlupdate = "";
                foreach (GridViewRow row in grvDetail.Rows)
                {
                    CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                    if (chk.Checked)
                    {
                        int id = int.Parse(grvDetail.DataKeys[row.RowIndex].Value.ToString());
                        DataTable tam = data.GetDataTable("select quantity from it_note_detail where id = " + id);
                        double tmp1 = Convert.ToInt64(tam.Rows[0]["quantity"].ToString());
                        if (tam.Rows.Count > 0)
                        {
                            sqlupdate = "update IT_NOTE_DETAIL set quantity_act = '" + tmp1 + "',quantity_daily = '" + tmp1 + "',kitting_date = getDate(), kitting_by = '" + use + "' " +
                              " where id = '" + id + "' and quantity <> quantity_act ";
                            data.ExcuteQuery(sqlupdate);
                            LoadGridView();
                        }
                    }

                }
            }
                

            //string sqlautoqty = "select a.*, b.reasoncode from IT_NOTE_DETAIL a left join IT_NOTE b on a.note_id = b.id where note_id ='" + id + "' ";
            //DataTable tbl = data.GetDataTable(sqlautoqty);
            //string sqlupdate = "";
            //for (int i = 0; i < tbl.Rows.Count; i++)
            //{
            //    double tmp1 = Convert.ToInt64(tbl.Rows[i]["quantity"].ToString());

            //    if (Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString()) == 1 || Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString())== 4 || Convert.ToInt64(tbl.Rows[0]["reasoncode"].ToString())==8)
            //    {
            //        sqlupdate = "update IT_NOTE_DETAIL set quantity_act = '" + tmp1 + "',quantity_daily = '" + tmp1 + "',kitting_date = getDate(), kitting_by = '" + use + "' " +
            //                  " where id = '" + Convert.ToInt64(tbl.Rows[i]["id"].ToString()) + "' ";
            //        data.ExcuteQuery(sqlupdate);
            //        LoadGridView();
            //        LoadPage();
            //    }
            //}
        }

        
        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
        protected void grvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDetail.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvDetail.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadGridView();
        }
        protected void grvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Find the checkbox control in header and add an attribute
                ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                        ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToDecimal(e.Row.Cells[7].Text) == Convert.ToDecimal(e.Row.Cells[9].Text))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    //e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }
                else if (Convert.ToDecimal(e.Row.Cells[7].Text) - Convert.ToDecimal(e.Row.Cells[9].Text) > 0 && Convert.ToDecimal(e.Row.Cells[9].Text) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                    //e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
    }
}