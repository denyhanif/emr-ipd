using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Login
/// </summary>
public class Login
{
    public Int64 organization_id { get; set; }
    public string organization_name { get; set; }
    public Int64? hope_organization_id { get; set; }
    public Guid? mobile_organization_id { get; set; }
    public string ax_organization_id { get; set; }
    public Guid role_id { get; set; }
    public string role_name { get; set; }
    public Guid user_id { get; set; }
    public string user_name { get; set; }
    public string full_name { get; set; }
    public Int64? hope_user_id { get; set; }
    public string email { get; set; }
    public Nullable<DateTime> birthday { get; set; }
    public string handphone { get; set; }
    public Int64 user_role_id { get; set; }
}

public class ResultLogin
{
    private List<Login> lists = new List<Login>();
    [JsonProperty("data")]
    public List<Login> list { get { return lists; } }
}

public class ViewOrganizationSetting
{
    public string setting_name { get; set; }
    public string setting_value { get; set; }
}

public class ResultViewOrganizationSetting
{
    private List<ViewOrganizationSetting> lists = new List<ViewOrganizationSetting>();
    [JsonProperty("data")]
    public List<ViewOrganizationSetting> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class MarkerConfig
{
    public string key { get; set; }
    public string value { get; set; }
}
