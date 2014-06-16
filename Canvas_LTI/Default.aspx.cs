using LtiLibrary.OAuth;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Canvas_LTI
{
    partial class Default : System.Web.UI.Page
    {
        protected Literal auth;

        protected void Page_Load(object sender, EventArgs e)
        {
            Uri url = HttpContext.Current.Request.Url;
            string app = Request.Form["custom_app"];
            string requestType = HttpContext.Current.Request.RequestType;
            string consumerSecret = "4010df2a-5322-4968-8b7d-92185fea2513";
            
            string signature = OAuthUtility.GenerateSignature(requestType, url, new NameValueCollection() { HttpContext.Current.Request.Form, HttpContext.Current.Request.QueryString }, consumerSecret);

            if (!(signature == this.Request.Form["oauth_signature"]))
                return;

            switch (app)
            {
                case "mymds":
                    this.Response.Redirect("http://mymds-dev.bham.ac.uk/portals/intrameddevtest/test/Course/#/Home");
                    break;
                
                case "saver":
                    HttpCookie myCookie = new HttpCookie("CanvasLTIAuthCookie");
                    myCookie.Values.Add("username", this.Request.Form["custom_canvas_user_login_id"]);
                    myCookie.Values.Add("role", this.Request.Form["roles"]);
                    Response.Cookies.Add(myCookie);
                    
                    this.Response.Redirect("https://mymds.bham.ac.uk/canvasSecure/southwood_mediaSearch/index.asp");
                    break;
                
                default:
                    break;
            }

        }
    }
}


