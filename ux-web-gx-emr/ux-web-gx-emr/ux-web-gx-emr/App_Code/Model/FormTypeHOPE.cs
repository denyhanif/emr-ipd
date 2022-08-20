using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DocTypeHOPE
/// </summary>

public class FormTypeHOPE
{
    public int formTypeId { get; set; }
    public string formTypeName { get; set; }
    public bool isActive { get; set; }
}

public class ResultFormTypeHOPE
{
    [JsonProperty("data")]
    public List<FormTypeHOPE> list
    {
        get; set;
    }
}