using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_SOAP_Control_Template_StdAssessment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initializevalue(List<Assessment> listassessment)
    {
        if (listassessment.Count > 0)
        {
            foreach (Assessment x in listassessment)
            {
                if (x.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
                {//"Primary"
                    txtPrimary.Text = x.remarks;
                }
            }
        }
    }

    public SOAP GetAssessmentValues(SOAP SOA)
    {
        foreach (var assessment in SOA.assessment)
        {
            if (assessment.soap_mapping_id == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47"))
            {//"Primary"
                assessment.remarks = txtPrimary.Text;
            }
        }
        return SOA;
    }
}