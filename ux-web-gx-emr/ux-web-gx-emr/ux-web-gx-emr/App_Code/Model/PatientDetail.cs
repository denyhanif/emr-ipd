using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientDetail
/// </summary>
public class PatientDetail
{
    public string mr_no { get; set; }

    public string admission_no { get; set; }

    public string admission_name { get; set; }

    public int Age { get; set; }

    public int gender { get; set; }

    public string birth_date { get; set; }

    public int patientTypeId { get; set; }

    public string patientTypeName { get; set; }

    public int payerID { get; set; }

    public string payerName { get; set; }



    public PatientDetail()
    {

    }
}