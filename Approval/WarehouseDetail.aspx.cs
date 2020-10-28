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
    public partial class WarehouseDetail : System.Web.UI.Page
    {
        int id, reasoncode, wh; string use, pat, location;
        DataProfile data = new DataProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            //try
            //{
                use = Session["id"].ToString();
                pat = Session["pat"].ToString();
                id = int.Parse(Request.QueryString["id"].ToString());
                //get location and reasoncode
                //string sql = "select locationtmp,reasoncode,warehouse from IT_NOTE where id = '" + id + "' ";
                //DataTable tbl = data.GetDataTable(sql);
                //reasoncode = Convert.ToInt32(tbl.Rows[0]["reasoncode"].ToString());
                //location = tbl.Rows[0]["locationtmp"].ToString();
                //wh = Convert.ToInt32(tbl.Rows[0]["warehouse"].ToString());
                if (!IsPostBack)
                {

                    LoadData();
                    //LoadDropkitting();
                }
            //}
            //catch
            //{
            //    Response.Redirect("warehouse.aspx");
            //}
        }
        //private void LoadDropkitting()
        //{
        //    DataTable tbl = data.GetDataTable("select id,fullname from it_note_user where permission = 5 ");
        //    Dropkitting.DataSource = tbl;
        //    Dropkitting.DataTextField = "fullname";
        //    Dropkitting.DataValueField = "ID";
        //    Dropkitting.DataBind();

        //    Dropkitting.Items.Insert(0, "--select PIC--");
        //}
        public void LoadData()
        {
            string sql = "Select *, b.description,c.warehouse, d.fullname from it_note a " +
                " left join IT_RESON_APPROVA b on a.reasoncode = b.id" +
                " left join warehouse c on a.warehouse = c.ID_WH" +
                " left join IT_NOTE_USER d on d.id = a.User_kitting " +
                " where a.id=" + id;
            DataTable note = data.GetDataTable(sql);
            if (note.Rows.Count > 0)
            {
                txtDate.Text = DateTime.Parse(note.Rows[0]["note_date"].ToString()).ToString("dd/MM/yyyy hh:mm");

                txtreasonvote.Text = note.Rows[0]["description"].ToString();
                txtwarehouse.Text = note.Rows[0]["warehouse"].ToString();
                txtplanner.Text = note.Rows[0]["planner"].ToString();
                txtReson_no.Text = note.Rows[0]["reson_no"].ToString();
                txtPart.Text = note.Rows[0]["part"].ToString();
                txtJobname.Text = note.Rows[0]["jobname"].ToString();
                txtModel.Text = note.Rows[0]["model"].ToString();
                lblvoteno.Text = note.Rows[0]["note_no"].ToString();
                txtkitting.Text = note.Rows[0]["fullname"].ToString();

                //lay ten reson
                string sql1 = "Select * from it_reson_creat where code='" + txtReson_no.Text.Trim() + "'";
                DataTable note1 = data.GetDataTable(sql1);
                if (note1.Rows.Count > 0)
                {
                    lblReson.Text = note1.Rows[0]["Description"].ToString();
                }
                else
                {
                    lblReson.Text = "Sai lý do";
                }

                //drToPart.Text = note.Rows[0]["topart"].ToString();            
                //if (note.Rows[0]["semi"].ToString().Contains('1')) drToPart.Text = note.Rows[0]["topart"].ToString();
                //else { drToPart.Visible = false; lbPart.Visible = false; }
                //if (note.Rows[0]["semi"].ToString().Contains('1'))
                //{
                //    drToPart.Text = note.Rows[0]["topart"].ToString();
                //    if (note.Rows[0]["NgayNhan"].ToString() != "")
                //        txtNgayNhan.Text = DateTime.Parse(note.Rows[0]["NgayNhan"].ToString()).ToString("dd/MM/yyyy");
                //}
                //else { lbPart.Visible = false; lbNgayNhan.Visible = false; txtNgayNhan.Visible = false; IbtnSave0.Visible = false; }

                LoadGridView();
            }

        }
        public void LoadGridView()
        {
            string sql2 = "select *, [quantity] - [quantity_act] as remain from it_note_detail where displaydetail = 1 and note_id=" + id ;
            DataTable note_detail = data.GetDataTable(sql2);
            if (note_detail.Rows.Count >= 0)
            {
                grvDetail.DataSource = note_detail;
                grvDetail.DataBind();
            }
        }


        int stt = 1;
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
                if (Convert.ToDouble(e.Row.Cells[7].Text.Trim()) == Convert.ToDouble(e.Row.Cells[9].Text.Trim()))
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    //e.Row.BackColor = System.Drawing.Color.OrangeRed;
                }
                else if (Convert.ToDouble(e.Row.Cells[7].Text) - Convert.ToDouble(e.Row.Cells[9].Text) > 0 && Convert.ToDouble(e.Row.Cells[9].Text) > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.OrangeRed;
                    //e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql2 = "select * from it_note where id=" + id;
            DataTable dttmp = data.GetDataTable(sql2);
            string sqlvote = "update it_note set checked=0,status=3, message = N'" + txtReason.Text + "' where id=" + id;
            data.ExcuteQuery(sqlvote);

            string sqldailyqty = "update it_note_detail set quantity_daily = 0 where Note_id=" + id;
            data.ExcuteQuery(sqldailyqty);

            string vote_no = dttmp.Rows[0]["Note_no"].ToString();
            string info = vote_no + "-" + use + "- return vote -";
            GhiTextFile(info + DateTime.Now.ToString("ddMMyy"));

            string sql1 = "Select * from it_note_user where id in (select create_by from it_note where id=" + id + " )";
            DataTable mail = data.GetDataTable(sql1);
            //string sqlsend = "select * from it_note_user where id=" + use_id;
            //DataTable mailsend = data.GetDataTable(sqlsend);
            if (mail.Rows[0]["mail"].ToString() != "")
            {
                SendMail _send = new SendMail("webmaster@towada.com.vn", "Yar7u82&", mail.Rows[0]["mail"].ToString(), "Recheck vote " + dttmp.Rows[0]["note_no"].ToString(), "Recheck vote " + dttmp.Rows[0]["note_no"].ToString());
                _send.SendM();
            }
            Response.Redirect("Warehouse.aspx");
        }

        protected void closemodal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#myModal", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
            upModal.Update();
        }

        //protected void btsetkitting_Click(object sender, EventArgs e)
        //{
        //    if (Dropkitting.SelectedIndex ==0)
        //    {
        //        Response.Write("<script language='javascript'> alert('Bạn phải chọn người kitting') </script>");
        //    }
        //    else
        //    {
        //        string sql = "update IT_NOTE set user_kitting = '" + Dropkitting.SelectedValue + "', kittingstatus = '0' where id =" + id;
        //        data.ExcuteQuery(sql);
        //        Response.Redirect("Warehouse.aspx");
        //    }

        //}

        protected void btexport_Click(object sender, EventArgs e)
        {
            string sqlcheck = "select kittingstatus,reasoncode from IT_NOTE where id =" + id;
            DataTable tblcheck = data.GetDataTable(sqlcheck);
            if (Convert.ToUInt32(tblcheck.Rows[0]["kittingstatus"].ToString()) == 0 && Convert.ToUInt32(tblcheck.Rows[0]["reasoncode"].ToString()) != 5)
            {
                Response.Write("<script language='javascript'> alert('Vote đang Kitting bạn không thể xuất dữ liệu') </script>");
            }
            else
            {
                string sql1 = "";
                if (Convert.ToUInt32(tblcheck.Rows[0]["reasoncode"].ToString()) == 5)
                {
                    sql1 = "select '' as No, a.Item, a.description as Item_Description, a.quantity as Quantity, b.Reson, '' as Unit1, '' as Unit2, '' as Unit3, '' as Unit4, " +
                          " a.location, a.semilot as Lot,getdate() as TransactionDate, b.Note_no as Document_Number, c.description,d.warehouse, b.Jobname from IT_NOTE b  " +
                          " left join it_note_detail a on a.Note_id = b.id " +
                          " left join IT_RESON_APPROVA c on b.reasoncode = c.id " +
                          " left join warehouse d on b.warehouse = d.id_WH " +
                          " where a.note_id=" + id;
                }
                else
                {
                    sql1 = "select '' as No, a.Item, a.description as Item_Description, a.quantity_daily as Quantity, b.Reson, '' as Unit1, '' as Unit2, '' as Unit3, '' as Unit4, " +
                          " a.location, a.semilot as Lot,getdate() as TransactionDate, b.Note_no as Document_Number, c.description,d.warehouse, b.Jobname from IT_NOTE b " +
                          " left join it_note_detail a on a.Note_id = b.id " +
                          " left join IT_RESON_APPROVA c on b.reasoncode = c.id " +
                          " left join warehouse d on b.warehouse = d.id_WH " +
                          " where a.displaydetail = 1 and a.quantity_daily >0 and a.note_id=" + id;
                }
                    

                DataTable Hoso = data.GetDataTable(sql1);
                GridView GridView1 = new GridView();
                GridView1.AllowPaging = false;
                GridView1.DataSource = Hoso;
                GridView1.DataBind();
                //GridDecorator.MergeRows(GridView1);
                string filename = Hoso.Rows[0]["Jobname"].ToString() + "_" + Hoso.Rows[0]["warehouse"].ToString() + ".xls";
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename="+ filename);
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

            
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public void GhiTextFile(string tmp)
        {
            string path = Server.MapPath("~/Log/tmp_"+use+".txt");
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
            }
            using (StreamWriter sw1 = new StreamWriter(path, true))
            {
                sw1.WriteLine(tmp);
                sw1.Close();
            }

            //List<string> text = File.ReadAllLines(path).ToList();
            //text.Add(tmp);
            //File.WriteAllLines(path, text.ToArray());
        }

        protected void btcompleted_Click(object sender, EventArgs e)
        {           

            string sql2 = "select * from it_note where id=" + id;
            DataTable dttmp = data.GetDataTable(sql2);
            int kittingstatus = Convert.ToInt32(dttmp.Rows[0]["Kittingstatus"].ToString());
            int reasoncode = Convert.ToInt32(dttmp.Rows[0]["Reasoncode"].ToString());
            if (kittingstatus == 0 && (reasoncode==2|| reasoncode ==3|| reasoncode == 6))
            {
                Response.Write("<script language='javascript'> alert('Vote chưa kitting bạn không thể đóng') </script>");
            }
            else
            {
                string vote_no = dttmp.Rows[0]["Note_no"].ToString();
                string info = vote_no + "-" + use + "- complte vote -";
                GhiTextFile(info + DateTime.Now.ToString("ddMMyy"));

                //update data
                string sql = "update it_note set status=0,process_by=" + use + " where id=" + id;
                data.ExcuteQuery(sql);
                string sqlvotedetail = "update it_note_detail set displaydetail=0 where quantity = quantity_act and Note_id=" + id;
                data.ExcuteQuery(sqlvotedetail);

                string sqldailyqty = "update it_note_detail set quantity_daily = 0 where Note_id=" + id;
                data.ExcuteQuery(sqldailyqty);

                string sql1 = "Select * from it_note_user where id in (select create_by from it_note where id=" + id + " )";
                DataTable mail = data.GetDataTable(sql1);
                //string sqlsend = "select * from it_note_user where id=" + use_id;
                //DataTable mailsend = data.GetDataTable(sqlsend);
                if (mail.Rows[0]["mail"].ToString() != "")
                {
                    SendMail _send = new SendMail("gr.webmaster@meiko-t.com.vn", "Gds@12345", mail.Rows[0]["mail"].ToString(), "Finish vote " + dttmp.Rows[0]["note_no"].ToString(), "Finish vote " + dttmp.Rows[0]["note_no"].ToString());
                    _send.SendM();
                }
                Response.Redirect("Warehouse.aspx");
            }
            
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
            upModal.Update();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("warehouse.aspx");
        }

        
    }
}