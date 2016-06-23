using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// required for Identity and OWIN Security
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Comp2007_s2016_MIDTERM_200265054
{
    public partial class Logout : System.Web.UI.Page
    {

        /**
        * <summary>
        * This method delete all sessions and log out the user
        * </summary>
        * 
        * @method Page_Load
        * @param {object} sender
        * @param {EventArgs} e
        * @returns {void}         
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            // store session info and authentication methods in the authenticationManager object
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            // perform sign out
            authenticationManager.SignOut();

            // Redirect to the Default page
            Response.Redirect("~/Login.aspx");
        }
    }
}