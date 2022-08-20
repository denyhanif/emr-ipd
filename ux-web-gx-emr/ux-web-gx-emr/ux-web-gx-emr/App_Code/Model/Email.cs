using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    public string email_type_name { get; set; }
    public string email_sender { get; set; }
    public string display_name { get; set; }
    public string email_to { get; set; }
    public string email_cc { get; set; }
    public string email_bcc { get; set; }
    public string subject { get; set; }
    public string body { get; set; }
    public int total_attachment { get; set; }
    public string attachment_name { get; set; }
    public string attachment_file { get; set; }
    public DateTime schedule_date { get; set; }
    public string created_by { get; set; }
}

public class ParamChangePass
{
    public string user_name { get; set; }
    public string old_pass { get; set; }
    public string new_pass { get; set; }
    public string modified_by { get; set; }
}