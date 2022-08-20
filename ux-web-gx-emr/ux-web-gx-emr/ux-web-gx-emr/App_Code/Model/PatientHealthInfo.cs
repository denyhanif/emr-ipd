using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHealthInfo
/// </summary>
public class PatientHealthInfo
{
    public long organization_id { get; set; }
    public long patient_id { get; set; }
    public int status_id { get; set; }
    public Guid encounter_id { get; set; }
    public List<InfoHealth> list_info { get; set; }
}

public class InfoHealth
{
    public Guid health_info_id { get; set; }
    public int health_info_type_id { get; set; }
    public long external_master_data_id { get; set; }
    public string health_info_value { get; set; }
    public string health_info_remarks { get; set; }
    public string health_info_status { get; set; }
    public bool is_waiting_delete { get; set; }
    public string delete_requester { get; set; }
    public Nullable<DateTime> delete_request_date { get; set; }
    public string delete_reason { get; set; }
    public bool is_active { get; set; }
    public DateTime created_date { get; set; }
    public string created_by { get; set; }
    public DateTime modified_date { get; set; }
    public string modified_by { get; set; }
    public Guid master_id { get; set; }
    public bool is_routine { get; set; }
    public Nullable<DateTime> last_modified_allergy { get; set; }
    public Nullable<DateTime> last_modified_surgery { get; set; }
    public Nullable<DateTime> last_modified_disease { get; set; }
    public Nullable<DateTime> last_modified_medication { get; set; }
    public Nullable<DateTime> last_modified_procedure { get; set; }

    public bool _is_newdata { get; set; }
}

public class ResponsePatientHealthInfo
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public PatientHealthInfo Data { set; get; }
}