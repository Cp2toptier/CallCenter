using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Callcenter
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipUser userX = Membership.GetUser();
            Guid userIdX = userX == null ? Guid.Empty : (Guid)userX.ProviderUserKey;
            Label1.Text = userIdX.ToString(); 
        }
    }
}
