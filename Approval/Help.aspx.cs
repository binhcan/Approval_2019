using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace Approval
{
    public partial class Hepl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                LoadPage();
            }
        }
        private void LoadPage()
        {
            string FilePath = Server.MapPath("~/File/Help.pdf");

            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(FilePath);

            if (FileBuffer != null)

            {
                //Response.Write("<script>window.open('" + FilePath + "','_blank');</script>");

                Response.ContentType = "application/pdf";

                Response.AddHeader("content-length", FileBuffer.Length.ToString());

                Response.BinaryWrite(FileBuffer);

            }
        }
    }
    
}