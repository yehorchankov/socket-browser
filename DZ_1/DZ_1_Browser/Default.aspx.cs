using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZ_1;
using DZ_1_Browser.Models;

namespace DZ_1_Browser
{
    public partial class Default : System.Web.UI.Page
    {
        public string Title { get; set; }
        IRequestBuilder requestBuilder;
        WebListener listener;
        HttpParser parser;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                requestBuilder = new RequestBuilder();
                listener = new WebListener(requestBuilder);
                parser = new HttpParser();
            }
            catch (Exception exc)
            {
                Response.Redirect(string.Format("ErrorPage.html?code={0}&msg={1}", exc.HResult, exc.Message));
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                listener.InstantiateRequest(InputUri.Text); //Creating GET request for adjusted URI
                DZ_1.Request request = requestBuilder.GetResult(); //Getting ready result
                string response = listener.GetResponse(request); //Getting response on the request
                parser.ParseHttpResponse(response); //Parsing the response
            }
            catch (ArgumentNullException exc)
            {
                Output.Text = exc.Message;
                throw new Exception(exc.Message);
            }
            catch (Exception exc)
            {
                Response.Redirect(string.Format("ErrorPage.html?code={0}&msg={1}", exc.HResult, exc.Message));
                return;
            }
            if ((int) parser.ResponseCode >= 400)   //Redirect to ErrorPage if client or server errors occured
            {
                Response.Redirect(string.Format("ErrorPage.html?code={0}&msg={1}", (int)parser.ResponseCode, parser.ResponseCode));
                return;
            }
            
            Title = parser.Title;
            OutputStatus.Text = string.Format(
                "{1} | {0}", parser.ResponseCode, (int)parser.ResponseCode);
            OutputLength.Text = (parser.ContentLength / 1024) + "kb";
            if (ShowBody.Checked)
                Output.Text = parser.Body;
        }
    }
}