using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.OleDb;

namespace Approval
{
    public partial class Create_detail : System.Web.UI.Page
    {
        DataProfile data = new DataProfile();
        string pat, use, location, Note_no;
        int id, reasoncode,wh;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            use = Session["id"].ToString();
            pat = Session["pat"].ToString();
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
            //get location and reasoncode
            string sql = "select locationtmp,reasoncode,warehouse,Note_No  from IT_NOTE where id = '" + id + "' ";
            DataTable tbl = data.GetDataTable(sql);
            reasoncode = Convert.ToInt32(tbl.Rows[0]["reasoncode"].ToString());
            location = tbl.Rows[0]["locationtmp"].ToString();
            wh = Convert.ToInt32(tbl.Rows[0]["warehouse"].ToString());
            Note_no = tbl.Rows[0]["Note_No"].ToString();
            if (!IsPostBack)
            {
                load_trang();
            }
        }
        public void load_trang()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtQuan.Text = "";
            txtlocation.Text = "";
            txtSemilot.Text = "";
            
            if (reasoncode != 4 || reasoncode !=7)
            {
                txtlocation.Enabled = false;
                txtlocation.Text = location;
            }

            LoadData();
        }
        public void LoadData()
        {
                DataTable dt = data.GetDataTable("select *, [quantity] - [quantity_act] as remain from it_note_detail where note_id=" + id);
                grvDetail.DataSource = dt;
                grvDetail.DataBind();
        }
        protected void grvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDetail.PageIndex = e.NewPageIndex;
            int trang_thu = e.NewPageIndex;
            int so_dong = grvDetail.PageSize;
            stt = trang_thu * so_dong + 1;
            LoadData();
        }

        int stt = 1;

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
                        //20170823 binhnt comment
                        //txtCode.Text = tam.Rows[0]["code"].ToString();
                        //txtName.Text = tam.Rows[0]["name"].ToString();
                        //txtQuan.Text = tam.Rows[0]["quantity"].ToString();
                        //txtItemID.Text = tam.Rows[0]["inventory"].ToString();

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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvDetail.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("cbSelectAll");
                if (chk.Checked)
                {
                    int id = int.Parse(grvDetail.DataKeys[row.RowIndex].Value.ToString());
                    data.ExcuteQuery("Delete it_note_detail where id = " + id);
                }
            }
            LoadData();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtQuan.Text = "";
            txtSemilot.Text = "";
            txtlocation.Text = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (HiddenField1.Value == "")
                {
                    string sql = "insert into it_note_detail(item,description,quantity,note_id,location,semilot,NoOfKitting, statuskitting, displaydetail,quantity_act,quantity_daily)"
                                 + "values('" + txtCode.Text + "','" + txtName.Text + "'," + float.Parse(txtQuan.Text) + "," + id + ",'" + txtlocation.Text + "','" + txtSemilot.Text + "',0,0,1,0,0)";
                    data.ExcuteQuery(sql);
                    LoadData();
                }
                else
                {
                    int id_detail = int.Parse(HiddenField1.Value.ToString());
                    string sql = "update it_note_detail set item='" + txtCode.Text + "',description='" + txtName.Text + "',quantity='" + float.Parse(txtQuan.Text.Trim()) + "',location='" + txtlocation.Text + "',semilot='" + txtSemilot.Text + "' where id=" + id_detail;
                    data.ExcuteQuery(sql);
                    load_trang();
                }
            }
                
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if ((reasoncode != 4 || reasoncode != 7) && (location== "Location-RM" || location == "Location-Inspection" ) && txtCode.Text != "")
            {
                string sql = "select location,Description from locationdetail where item = '" + txtCode.Text.Trim() + "' and type =  '" + location + "' and warehouse = '"+ wh +"' ";
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
                if (tbl2.Rows.Count == 0 && tbl3.Rows.Count ==0)
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

        protected void btnImport_Click(object sender, EventArgs e)
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
                                    if ((reasoncode != 4 || reasoncode != 7) && (location == "Location-RM" || location == "Location-Inspection") )
                                    {
                                        string sqllo = "select location from locationdetail where item = '" + _code + "' and type =  '" + location + "' and warehouse = '" + wh + "' ";
                                        DataTable tbl = data.GetDataTable(sqllo);
                                        if (tbl.Rows.Count < 0)
                                        {
                                            Response.Write("<script language='javascript'> alert('Item' + '"+ _code + "' + 'không có location! liên hệ với LOG để kiểm tra dữ liệu') </script>");
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


                                    string sql = "insert into it_note_detail(item, description, quantity, note_id, semilot, location,NoOfKitting, statuskitting, displaydetail,quantity_act,quantity_daily) "
                                                    //20170824 binhnt backup
                                                    //+"values('" + dt.Rows[i][1].ToString() + "','" + dt1.Rows[0]["description"].ToString() + "'," + float.Parse(dt.Rows[i][2].ToString()) + ",'" + dt1.Rows[0]["item_id"] + "'," + int.Parse(HiddenField1.Value.ToString()) + ",'" + dt.Rows[i][3] + "')";
                                                    + " values('" + _code + "','" + _name + "'," + _quantity + "," + id + ",'" + _semilot + "'" + ",'" + _location + "',0,0,1,0,0)";
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            //Ket thuc qua trinh tao, gui mail toi nguoi dc quyen xac nhan
            string sql = "Select * from it_note_user where parts='" + pat + "' and permission = '4'";
            //string sqluse = "select * from it_note_user where id=" + use;
            //DataTable mailuse = data.GetDataTable(sqluse);
            DataTable mail = data.GetDataTable(sql);
            for (int i = 0; i < mail.Rows.Count; i++)
            {
                SendMail _send = new SendMail("gr.webmaster@meiko-t.com.vn", "Gds@12345", mail.Rows[i]["mail"].ToString(), "Vote " + Note_no + " is complete, Please confirm!", "Dear Sir, \nVote " + Note_no + " is complete, Please confirm!");
                _send.SendM();

            }
            Response.Redirect("Manage_Detail.aspx");
        }

        public string get_stt()
        {
            return Convert.ToString(stt++);
        }
    }
}