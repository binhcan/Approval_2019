using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Approval
{
    public partial class Edit_Detail : System.Web.UI.Page
    {
        int id, reasoncode, wh; string use, pat, location, Note_no;
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
            string sql = "select locationtmp,reasoncode,warehouse,Note_No from IT_NOTE where id = '" + id + "' ";
            DataTable tbl = data.GetDataTable(sql);
            reasoncode = Convert.ToInt32(tbl.Rows[0]["reasoncode"].ToString());
            location = tbl.Rows[0]["locationtmp"].ToString();
            wh = Convert.ToInt32(tbl.Rows[0]["warehouse"].ToString());
            Note_no = tbl.Rows[0]["Note_No"].ToString();
            if (!IsPostBack)
            {

                load_trang();
                Load_drseason();
                Load_warehouse();
                Load_Planner_default();
                LoadData();
            }
            //}
            //catch
            //{
            //    Response.Redirect("Manage_Detail.aspx");
            //}
        }
        private void Load_drseason()
        {
            string sql = "select * from IT_RESON_APPROVA";
            DataTable tbl = data.GetDataTable(sql);
            drreason.DataSource = tbl;
            drreason.DataTextField = "description";
            drreason.DataValueField = "ID";
            drreason.DataBind();

            drreason.Items.Insert(0, "--Reason vote--");

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
        private void Load_Planner()
        {
            string sql1 = "select PlannerNo from IT_RESON_APPROVA where id = '" + drreason.SelectedValue + "' ";
            DataTable tbl1 = data.GetDataTable(sql1);
            if (tbl1.Rows.Count > 0)
            {
                string plannerNo = tbl1.Rows[0]["PlannerNo"].ToString();
                string sql = "select DISTINCT Planner from LocationMaster where PlannerNo = '" + plannerNo + "' and warehouse = '" + drwarehouse.SelectedValue + "' and (Dept = '" + pat + "' or Dept = 'ALL') and reasoncode ='" + drreason.SelectedValue + "' ";
                DataTable tbl = data.GetDataTable(sql);
                drplaner.DataSource = tbl;
                drplaner.DataTextField = "Planner";
                drplaner.DataValueField = "Planner";
                drplaner.DataBind();

                drplaner.Items.Insert(0, "--Planner--");
            }


        }
        private void Load_Planner_default()
        {
            
                string sql = "select DISTINCT Planner from LocationMaster where (Dept = '" + pat + "' or Dept = 'ALL') ";
                DataTable tbl = data.GetDataTable(sql);
                drplaner.DataSource = tbl;
                drplaner.DataTextField = "Planner";
                drplaner.DataValueField = "Planner";
                drplaner.DataBind();

                drplaner.Items.Insert(0, "--Planner--");
            


        }
        public void load_trang()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtQuan.Text = "";
            txtlocation.Text = "";
            txtSemilot.Text = "";

            if (reasoncode != 4 || reasoncode != 7)
            {
                txtlocation.Enabled = false;
                txtlocation.Text = location;
            }
            //view return or not return
            string sql = "select kittingstatus from IT_NOTE where id=" + id;
            DataTable tbl = data.GetDataTable(sql);
            if (Convert.ToInt32(tbl.Rows[0]["kittingstatus"].ToString()) == 2)
            {
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                btnOK.Visible = false;
                boxbody1.Visible = false;
                boxbody2.Visible = false;
            }
            else
            {
                btresent.Visible = false;
                btclosevote.Visible = false;
            }

            //LoadData();
        }
        public void refres()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtQuan.Text = "";
            txtlocation.Text = "";
            txtSemilot.Text = "";
            HiddenField1.Value = "";
        }
        public void LoadData()
        {
            string sql = "Select * from it_note where id=" + id;
            DataTable note = data.GetDataTable(sql);
            if (note.Rows.Count > 0)
            {
                txtDate.Text = DateTime.Parse(note.Rows[0]["note_date"].ToString()).ToString("dd/MM/yyyy hh:mm");

                drreason.SelectedValue = note.Rows[0]["reasoncode"].ToString();
                drwarehouse.SelectedValue = note.Rows[0]["warehouse"].ToString();
                drplaner.SelectedValue = note.Rows[0]["planner"].ToString().Trim();
                txtReson_no.Text = note.Rows[0]["reson_no"].ToString();
                txtPart.Text = note.Rows[0]["part"].ToString();
                txtJobname.Text = note.Rows[0]["jobname"].ToString();
                txtModel.Text = note.Rows[0]["model"].ToString();
                //txtDesciption.Text = note.Rows[0]["model"].ToString();


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
        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if ((reasoncode != 4 || reasoncode != 7) && (location == "Location-RM" || location == "Location-Inspection") && txtCode.Text != "")
            {
                string sql = "select location,Description from locationdetail where item = '" + txtCode.Text.Trim() + "' and type =  '" + location + "' and warehouse = '" + wh + "' ";
                DataTable tbl = data.GetDataTable(sql);
                if (tbl.Rows.Count < 0)
                {
                    Response.Write("<script language='javascript'> alert('Item không có location! liên hệ với LOG để kiểm tra dữ liệu') </script>");
                }
                else
                {
                    if (tbl.Rows.Count == 1)
                    {
                        txtlocation.Text = tbl.Rows[0]["location"].ToString();
                        txtName.Text = tbl.Rows[0]["Description"].ToString();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'> alert('Item có nhiều location! liên hệ với LOG để kiểm tra dữ liệu') </script>");
                    }
                }
            }
            else
            {
                string sql2 = "select * from Item where item = '" + txtCode.Text.Trim() + "' ";
                DataTable tbl2 = data.GetDataTable(sql2);
                string sql3 = "select * from locationdetail where item = '" + txtCode.Text.Trim() + "' ";
                DataTable tbl3 = data.GetDataTable(sql3);
                if (tbl2.Rows.Count == 0 && tbl3.Rows.Count == 0)
                {
                    Response.Write("<script language='javascript'> alert('Item không có location! liên hệ với LOG để kiểm tra dữ liệu') </script>");
                }
                else
                {
                    if (tbl2.Rows.Count == 1)
                    {
                        txtName.Text = tbl2.Rows[0]["Description"].ToString();
                    }
                    else if (tbl3.Rows.Count == 1)
                    {
                        txtName.Text = tbl3.Rows[0]["Description"].ToString();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'> alert('Có nhiều Item trùng tên, liên hệ với LOG để kiểm tra dữ liệu') </script>");
                    }
                }
            }

        }
        protected void drreason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select PlannerNo from IT_RESON_APPROVA where id = '" + drreason.SelectedValue + "' ";
            DataTable tbl = data.GetDataTable(sql);
            if (tbl.Rows.Count > 0)
            {
                HiddenField2.Value = tbl.Rows[0]["PlannerNo"].ToString();
            }

            Load_warehouse();
            Load_Planner();
        }

        protected void drwarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Planner();
        }
        protected void drplaner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void LoadGridView()
        {
            string sql2 = "select *, [quantity] - [quantity_act] as remain from it_note_detail where statuskitting = 0 and note_id=" + id;
            DataTable note_detail = data.GetDataTable(sql2);
            if (note_detail.Rows.Count >= 0)
            {
                grvDetail.DataSource = note_detail;
                grvDetail.DataBind();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string sql;
            string loc = "";
            int partcardstatus = 0;
            //lay location cho vote
            if (Convert.ToInt32(drreason.SelectedValue) == 5 || Convert.ToInt32(drreason.SelectedValue) == 6 || Convert.ToInt32(drreason.SelectedValue) == 8)
            {
                string sql1 = "select location from locationMaster where Dept = '" + txtPart.Text.Trim() + "' and warehouse = '" + drwarehouse.SelectedValue + "' and planner = '" + drplaner.SelectedValue + "' and ReasonCode = '" + drreason.SelectedValue + "' ";
                DataTable tbl1 = data.GetDataTable(sql1);
                loc = tbl1.Rows[0]["Location"].ToString();
            }
            if (Convert.ToInt32(drreason.SelectedValue) == 1 || Convert.ToInt32(drreason.SelectedValue) == 2 || Convert.ToInt32(drreason.SelectedValue) == 3)
            {
                loc = "Location-RM";
            }
            if (Convert.ToInt32(drreason.SelectedValue) == 4)
            {
                loc = "No_location";
            }
            if (Convert.ToInt32(drreason.SelectedValue) == 7)
            {
                loc = "location_by_user";
            }
            //check partcardstatus
            if ((Convert.ToInt32(drreason.SelectedValue) == 2 || Convert.ToInt32(drreason.SelectedValue) == 3 || Convert.ToInt32(drreason.SelectedValue) == 6) && drwarehouse.SelectedValue == "5")
            {
                partcardstatus = 1;
            }
            else partcardstatus = 0;

            sql = "update it_note  set confirm_by='',message='',checked=0,status = 1, reson_no=N'" + txtReson_no.Text + "', reson='" + txtReson_no.Text + "'," +
                    " part='" + txtPart.Text + "',jobname='" + txtJobname.Text.Trim() + "', model='" + txtModel.Text.Trim() + "' ,update_date=CONVERT(varchar, GETDATE(), 101)," +
                    " warehouse='" + drwarehouse.SelectedValue + "', planner='" + drplaner.Text + "',reasoncode = '" + drreason.SelectedValue + "',locationtmp = '" + loc + "', partcardstatus = '"+ partcardstatus + "' where checked = 0 and id=" + id;
            data.ExcuteQuery(sql);



            //string sql1 = "Select * from it_note_user where parts='" + pat + "' and permission=4";
            ////string sqluse = "select * from it_note_user where id=" + use;
            ////DataTable mailuse = data.GetDataTable(sqluse);
            //DataTable mail = data.GetDataTable(sql1);
            //for (int i = 0; i < mail.Rows.Count; i++)
            //{
            //    SendMail _send = new SendMail("webmaster@towada.com.vn", "Yar7u82&", mail.Rows[i]["mail"].ToString(), "Vote " + txtNo.Text + " is complete, Please confirm!", "Dear Sir, \nVote " + txtNo.Text + " is complete, Please confirm!");
            //    _send.SendM();
            //}

            Response.Redirect("Manage_Detail.aspx");
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
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvDetail.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvDetail.DataKeys[row.RowIndex].Value.ToString());
                    HiddenField1.Value = id.ToString();
                    DataTable tam = data.GetDataTable("select * from it_note_detail where id = " + id);
                    if (tam.Rows.Count > 0)
                    {
                        txtCode.Text = tam.Rows[0]["item"].ToString();
                        txtName.Text = tam.Rows[0]["description"].ToString();
                        txtQuan.Text = tam.Rows[0]["quantity"].ToString();
                        txtlocation.Text = tam.Rows[0]["location"].ToString();
                        txtSemilot.Text = tam.Rows[0]["semilot"].ToString();
                    }
                    break;
                }


            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (HiddenField1.Value != "")
            {
                int id_detail = int.Parse(HiddenField1.Value.ToString());
                string sql = "update it_note_detail set item='" + txtCode.Text + "',description='" + txtName.Text + "',quantity='" + float.Parse(txtQuan.Text.Trim()) + "',location='" + txtlocation.Text + "', semilot='" + txtSemilot.Text + "' where id=" + id_detail;
                data.ExcuteQuery(sql);
                refres();
                LoadGridView();
            }
            else
            {
                string sql2 = "insert into it_note_detail(item,description,quantity,note_id,location,semilot,statusKitting,NoOfKitting,displaydetail,quantity_act,quantity_daily)"
                + "values('" + txtCode.Text + "','" + txtName.Text + "'," + float.Parse(txtQuan.Text) + "," + id + ",'" + txtlocation.Text + "','" + txtSemilot.Text + "',0,0,1,0,0)";
                data.ExcuteQuery(sql2);
                LoadData();
            }
            //cap nhat lai trang thai vote ve chua duyet
            string sqlupdate = "update IT_NOTE set status = 1, CHECKED = 0 where id =" + id;
            data.ExcuteQuery(sqlupdate);
        }

        protected void btresent_Click(object sender, EventArgs e)
        {
            string sql = "";
            string check = "select reasoncode, warehouse from IT_NOTE where id =" + id;
            DataTable tblcheck = data.GetDataTable(check);
            if (Convert.ToInt32(tblcheck.Rows[0]["warehouse"].ToString()) == 5 && (Convert.ToInt32(tblcheck.Rows[0]["reasoncode"].ToString()) == 2 || Convert.ToInt32(tblcheck.Rows[0]["reasoncode"].ToString()) == 3 || Convert.ToInt32(tblcheck.Rows[0]["reasoncode"].ToString()) == 6))
            {
                sql = "update IT_Note set partcardstatus = '1',kittingstatus = '0', status = 1,user_kitting = 0 where id =" + id;
            }
            else
            {
                sql = "update IT_Note set kittingstatus = '0', status = 1,user_kitting = 0 where id =" + id;
            }
            
            data.ExcuteQuery(sql);
            Response.Redirect("Manage_Detail.aspx");
        }

        protected void btclosevote_Click(object sender, EventArgs e)
        {
            string sql = "update IT_Note set kittingstatus = '3' where id =" + id;
            data.ExcuteQuery(sql);
            Response.Redirect("Manage_Detail.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvDetail.Rows)
            {
                CheckBox chk = (row.FindControl("cbSelectAll") as CheckBox);
                if (chk.Checked)
                {
                    int id = int.Parse(grvDetail.DataKeys[row.RowIndex].Value.ToString());
                    data.ExcuteQuery("delete from it_note_detail where id=" + id);
                    break;
                }

            }
            LoadGridView();
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Detail.aspx");
        }
        protected void txtReson_no_TextChanged(object sender, EventArgs e)
        {
            DataTable model = data.GetDataTable("select * from Approval.dbo.it_reson_creat where code='" + txtReson_no.Text.Trim() + "'");
            //lblReson.Text = "";
            //DataTable model = data.GetDataTable("select * from mtl_system_items a where a.org_id!=8");
            if (model.Rows.Count > 0)
            {
                lblReson.Text = model.Rows[0]["Description"].ToString();
            }
            else
            {
                txtReson_no.Text = "";
                lblReson.Text = "Nhập sai Reson Code";
            }
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            DataTable tblcheck = data.GetDataTable("select checked from IT_NOTE where id = '" + id +"' ");
            if (Convert.ToInt32(tblcheck.Rows[0]["checked"].ToString()) == 1)
                return;
            else
            {
                //string FilePath = ConfigurationManager.AppSettings["FolderPath"].ToString();
                string filename = string.Empty;
                if (fileupload.HasFile)
                {
                    try
                    {
                        string[] allowdFile = { ".xls", ".xlsx" };
                        //Here we are allowing only excel file so verifying selected file pdf or not
                        string FileExt = System.IO.Path.GetExtension(fileupload.PostedFile.FileName);
                        //Check whether selected file is valid extension or not
                        bool isValidFile = allowdFile.Contains(FileExt);
                        if (!isValidFile)
                        {
                            Response.Write("<script language='javascript'> alert('Please upload only Excel') </script>");
                        }
                        else
                        {
                            // Get size of uploaded file, here restricting size of file
                            int FileSize = fileupload.PostedFile.ContentLength;
                            if (FileSize <= 1048576)//1048576 byte = 1MB
                            {
                                //Get file name of selected file
                                filename = Path.GetFileName(Server.MapPath(fileupload.FileName));

                                //Save selected file into server location
                                fileupload.SaveAs(Server.MapPath("~/File/") + filename);
                                //Get file path
                                string filePath = Server.MapPath("~/File/") + filename;
                                //Open the connection with excel file based on excel version
                                OleDbConnection con = null;
                                if (FileExt == ".xls")
                                {
                                    con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;");

                                }
                                else if (FileExt == ".xlsx")
                                {
                                    con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                                }
                                con.Open();
                                //Get the list of sheet available in excel sheet
                                DataTable dt9 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                //Get first sheet name
                                string getExcelSheetName = dt9.Rows[0]["Table_Name"].ToString();
                                //Select rows from first sheet in excel sheet and fill into dataset
                                OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [" + getExcelSheetName + @"]", con);
                                OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                                DataTable dt = new DataTable();
                                ExcelAdapter.Fill(dt);
                                con.Close();

                                if (dt.Rows.Count != 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        //string _location = dt.Rows[i][0].ToString();
                                        string _code = dt.Rows[i][1].ToString().Trim();

                                        string _name = dt.Rows[i][2].ToString();
                                        float _quantity = float.Parse(dt.Rows[i][3].ToString());
                                        string _semilot = dt.Rows[i][4].ToString();

                                        //get location
                                        string _location = "";
                                        if ((reasoncode != 4 || reasoncode != 7) && (location == "Location-RM" || location == "Location-Inspection"))
                                        {
                                            string sqllo = "select location from locationdetail where item = '" + _code + "' and type =  '" + location + "' and warehouse = '" + wh + "' ";
                                            DataTable tbl = data.GetDataTable(sqllo);
                                            if (tbl.Rows.Count < 0)
                                            {
                                                Response.Write("<script language='javascript'> alert('Item' + '" + _code + "' + 'không có location! liên hệ với LOG để kiểm tra dữ liệu') </script>");
                                                break;
                                            }
                                            else
                                            {
                                                if (tbl.Rows.Count == 1)
                                                {
                                                    _location = tbl.Rows[0]["location"].ToString();
                                                }
                                                else
                                                {
                                                    Response.Write("<script language='javascript'> alert('Item có ' + '" + tbl.Rows.Count + "' + ' location! liên hệ với LOG để kiểm tra dữ liệu') </script>");
                                                    break;
                                                }
                                            }
                                        }
                                        else if (location == "location_by_user" || location == "ItemStockroomLocation")
                                        {
                                            _location = dt.Rows[i][0].ToString().Trim();
                                        }
                                        else
                                        {
                                            _location = location;
                                        }


                                        string sql = "insert into it_note_detail(item, description, quantity, note_id, semilot, location,statusKitting,NoOfKitting,displaydetail,quantity_act) "
                                                        //20170824 binhnt backup
                                                        //+"values('" + dt.Rows[i][1].ToString() + "','" + dt1.Rows[0]["description"].ToString() + "'," + float.Parse(dt.Rows[i][2].ToString()) + ",'" + dt1.Rows[0]["item_id"] + "'," + int.Parse(HiddenField1.Value.ToString()) + ",'" + dt.Rows[i][3] + "')";
                                                        + " values('" + _code + "','" + _name + "'," + _quantity + "," + id + ",'" + _semilot + "'" + ",'" + _location + "',0,0,1,0)";
                                        data.ExcuteQuery(sql);
                                        // }
                                    }
                                    LoadData();
                                }

                            }
                            else
                            {
                                Response.Write("<script language='javascript'> alert('Attachment file size should not be greater then 1 MB!') </script>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script language='javascript'> alert('Error occurred while uploading a file: " + ex.Message + "') </script>");
                    }
                }
            }
           
        }
    }
}