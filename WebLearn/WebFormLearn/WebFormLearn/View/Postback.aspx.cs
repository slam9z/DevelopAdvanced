using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebFormLearn.View
{
    public partial class Postback : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            string message = string.Format("The {0} event of {1} is fired", "Click", "Button1");
            LabelMessage.Text = message;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string message = string.Format("The {0} event of {1} is fired", "Click", "Button2");
            LabelMessage.Text = message;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string message = string.Format("The {0} event of {1} is fired", "Click", "Button3");
            LabelMessage.Text = message;
        }

        protected void Button_Command(object sender, CommandEventArgs e)
        {
            string message = string.Format("The {0} event of {1} is fired", "Command", e.CommandArgument);
            LabelMessage.Text = message;
        }

   
    }
}