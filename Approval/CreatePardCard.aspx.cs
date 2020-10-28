using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZXing;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Image = System.Web.UI.WebControls.Image;

namespace Approval
{
    public partial class CreatePardCard : System.Web.UI.Page
    {
        int id, reasoncode, wh, No; string use, pat, location;       

        DataProfile data = new DataProfile();       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null) Response.Redirect("Default.aspx");
            
            use = Session["id"].ToString();
            pat = Session["pat"].ToString();
            id = int.Parse(Request.QueryString["id"].ToString());
            if (!IsPostBack)
            {

                LoadData();
                CreateQRcode();
            }
        }
        public void LoadData()
        {
            string sql = "select a.*, b.Note_No from it_note_detail a left join IT_NOTE b on a.Note_id = b.id where displaydetail = 1 and note_id= " + id;
            DataTable tbl = data.GetDataTable(sql);
            No = tbl.Rows.Count;
            DataList1.DataSource = tbl;
            DataList1.DataBind();
        }
        private void CreateQRcode()
        {
            int i = 1;
            foreach (DataListItem e in DataList1.Items)
            {
               if (i<=No)
               {
                    Label lbllo = (Label)e.FindControl("lbllocation");
                    Label lblItem = (Label)e.FindControl("lblItem");
                    string creatcode = lbllo.Text + "|" + lblItem.Text;

                    //Zxing Generate 
                    var writer = new BarcodeWriter();
                    writer.Format = BarcodeFormat.QR_CODE;
                    var result = writer.Write(creatcode);

                    String path = Server.MapPath("~/Img/QRCode/QRcode"+i+".jpg");
                    var BarcodeBitmap = new Bitmap(result);

                    using (MemoryStream memory = new MemoryStream())
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                        {
                            BarcodeBitmap.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                        }
                    }
                    Image imgQrcode = (System.Web.UI.WebControls.Image)e.FindControl("imgQrcode");
                    imgQrcode.Visible = true;
                    imgQrcode.ImageUrl = "~/Img/QRCode/QRcode" + i + ".jpg";
                    i++;
                }
            }

        }
        protected void btnDone_Click(object sender, EventArgs e)
        {
            Response.Redirect("PardCard.aspx");
        }
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //Label lbllo = (Label)e.Item.FindControl("lbllocation");
            //Label lblItem = (Label)e.Item.FindControl("lblItem");
            //string creatcode = lblItem.Text + "|" + lbllo.Text;

            ////Zxing Generate 
            //var writer = new BarcodeWriter();
            //writer.Format = BarcodeFormat.QR_CODE;
            //var result = writer.Write(creatcode);

            //String path = Server.MapPath("~/Img/QRcode.jpg");
            //var BarcodeBitmap = new Bitmap(result);

            //using (MemoryStream memory = new MemoryStream())
            //{
            //    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            //    {
            //        BarcodeBitmap.Save(memory, ImageFormat.Jpeg);
            //        byte[] bytes = memory.ToArray();
            //        fs.Write(bytes, 0, bytes.Length);
            //    }
            //}
            //Image imgQrcode = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgQrcode");
            //imgQrcode.Visible = true;
            //imgQrcode.ImageUrl = "~/Img/QRcode.jpg";
        }
        protected void btnfinish_Click(object sender, EventArgs e)
        {
            string sql = "update IT_NOTE set partcardstatus = 0 where id =" + id;
            data.ExcuteQuery(sql);
            Response.Redirect("PardCard.aspx");
        }
    }
}