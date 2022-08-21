using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public const string SessionOrganization = "EMR_organizationId";
    public const string SessionLanguage = "EMR_language";
    public const string SessionListOrganization = "EMR_ListOrganization";
    public const string SessionUserFullName = "EMR_UserFullName";
    public const string SessionUserID = "EMR_UserId";
    public const string SessionDoctor = "EMR_doctorid";
    public const string SessionLabPathologyChecked = "EMR_ListLabPathologyChecked";
    public const string SessionDrug = "EMR_MasterDrug";
    public const string SessionItemDrugPres = "EMR_ItemDrugPres";
    public const string SessionItemDiagProc = "EMR_ItemDiagProc";
    public const string SessionDataselectedDiagProc = "EMR_DataselectedDiagProc";
    public const string SessionFrequency = "EMR_FrequencyList";
    public const string SessionRoute = "EMR_RouteList";
    public const string SessionDrugPres = "EMR_DrugPres";
    public const string SessionCompPres = "EMR_CompPres";
    public const string SessionCompDetailPres = "EMR_CompDetailPres";
    public const string SessionCompPresHdn = "EMR_CompPresHdn";
    public const string SessionCompHeaderHdn = "EMR_CompPresHeaderHdn";
    public const string SessionRacikanHeader = "EMR_RacikanHeader";
    public const string SessionRacikanDetail = "EMR_RacikanDetail";
    public const string SessionTempInputRacikanHeader = "EMR_TempInputRacikanHeader";
    public const string SessionTempInputRacikanDetail = "EMR_TempInputRacikanDetail";
    public const string SessionRacikanHeaderAdd = "EMR_RacikanHeader_Additional";
    public const string SessionRacikanDetailAdd = "EMR_RacikanDetail_Additional";
    public const string SessionAllergySubjective = "EMR_AllergySubjective";
    public const string SessionSurgerySubjective = "EMR_SurgerySubjective";
    public const string SessionOrganizationSetting = "EMR_SessionOrganizationSetting";
    public const string SessionDrugsConsumables = "EMR_SessionDrugsConsumables";
    public const string Sessionroutinemedication = "EMR_Sessionroutinemedication";
    public const string Sessionradcheck = "EMR_Sessionradcheck";
    public const string Sessionpolidata = "EMR_Sessionpolidata";
    public const string Sessionsoapmodel = "EMR_Sessionsoapmodel";
    public const string Sessiondosage = "EMR_Sessiondosage";
    public const string Sessionuom = "EMR_Sessionuom";
    public const string Sessionmaplab = "EMR_Sessionmaplab";
    public const string SessionConsumablesList = "EMR_SessionConsumablesList";
    public const string Sessionadditionalpres = "EMR_Sessionadditionalpres";
    public const string SessionConsumablesListAdd = "EMR_SessionConsumablesListAdd";
    public const string SessionMedicalHistory = "EMR_medicalHistory";
    public const string SessionMedicalHistoryFiltered = "EMR_medicalHistoryFiltered";
    public const string SessionDoctorIdLogin = "EMR_DoctorIdLogin";
    public const string SessionPreviousRowIndex = "EMR_PreviousRowIndex";//PreviousRowIndex
    public const string SessionItemAllCPOE = "EMR_ItemAllCPOE";
    public const string SessionUserSetting = "EMR_UserSetting";
    public const string SessionTemplateSOAP = "EMR_TemplateSOAP";
    public const string SessionDataDetailVaksinasi = "EMR_DataDetailVaksinasi";
    public const string SessionDataDetailVaksinasi_Save = "EMR_DataDetailVaksinasi_Save";
    public const string SessionVaccineDWS = "EMR_VaccineDewasa";
    public const string SessionVaccineANK = "EMR_VaccineAnak";
    public const string Sessionsoapbackup = "EMR_Sessionsoapbackup";
    public const string SessionJsonsoapbackup = "EMR_SessionJsonsoapbackup";
    public const string SessionDataDetailChart = "EMR_DataDetailChart";
    public const string SessionDataDetailChart_Save = "EMR_DataDetailChart_Save";
    public const string SessionAppointmentID = "EMR_AppointmentID";

    public const string SessionSchedule = "EMR_SessionSchedule";
    public const string SessionRevHeader = "EMR_RevisionHeader";
    public const string SessionRevSoap = "EMR_RevisionSoap";
    public const string SessionRevCpoe = "EMR_RevisionCpoe";
    public const string SessionRevPres = "EMR_RevisionPrescription";


    //labrad 
    public const string SessionRadiologyCheck = "EMR_SessionRadiology";
    public const string SessionLaboratoryCheck = "EMR_SessionLaboratory";

    // Referral (rujukan & rawat inap)
    public const string SessionProcedureInpatientData = "EMR_SessionProcedureData";
    public const string SessionAnestheticInpatientData = "EMR_SessionAnestheticData";
    public const string SessionWardInpatientData = "EMR_SessionWardData";
    public const string SessionRecoveryRoomInpatientData = "EMR_SessionRecoveryRoomData";
    public const string SessionProcedureInpatientChecked = "EMR_ListProcedureInpatientChecked";


    //public const string ViewStateOrderSetDrugs = "EMR_OrderSetDrugs";
    //public const string ViewStateOrderSetCompound = "EMR_OrderSetCompound";
    //public const string ViewStateOrderSetLaboratory = "EMR_OrderSetLaboratory";
    public const string ViewStateOrderSetType = "EMR_OrderSetType";
    public const string ViewStateWorklistNonAppoitnmentData = "EMR_WorklistNonAppoitnmentData";
    public const string ViewStateWorklistAppoitnmentData = "EMR_WorklistAppoitnmentData";

    public const string ViewStateDrug = "EMR_DrugList";
    public const string ViewStateCompound = "EMR_CompoundList";
    public const string ViewStateOnoPaging = "EMR_PagingOno";
    public const string ViewStateDataItem = "EMR_DataItem";
    public const string ViewStateItemList = "EMR_ItemList";
    public const string ViewStateHeaderChecklist = "EMR_HeaderChecklist";
    public const string ViewStateAllHeaderLaboratory = "EMR_AllHeaderLaboratory";

    public const string ViewStateOtherUnitMR = "EMR_OtherUnitMR";
    public const string ViewStateScannedData = "EMR_ScannedData";
    public const string ViewStateEncounterData = "EMR_EncounterData";
    public const string ViewStatePageData = "EMR_PageData";
    public const string ViewStatePatientHistoryInner = "EMR_PatientHistoryInner";
    public const string ViewStatePatientHistoryCompound = "EMR_PatientHistoryCompound";
    public const string ViewStatePatientHistoryHOPEemr = "EMR_PatientHistoryHOPEemr";
    public const string ViewStatePatientHistoryEye = "EMR_PatientHistoryEye";
    public const string ViewStatePatientHistoryMove = "EMR_PatientHistoryMove";
    public const string ViewStatePatientHistoryVerbal = "EMR_PatientHistoryVerbal";
    public const string ViewStatePatientHistoryDoseUOM = "EMR_PatientHistoryDoseUOM";
    public const string ViewStatePatientHistoryPatientType = "EMR_PatientHistoryPatientType";

    public const string ViewStateRadiologyResult = "EMR_RadiologyResult";
    public const string ViewStateListLaboratory = "EMR_ListLaboratory"; 
    public const string ViewStateListData = "EMR_ListData";

    public const string SessionMimsResultData = "EMR_MimsResultData";

    //specialty session
    public const string SessionObgynKontrasepsi = "EMR_SessionObgynKontrasepsi"; 
    public const string SessionObgynPregnantHistory = "EMR_SessionObgynPregnantHistory";

    public const string SessionDocTypeSOAP = "Nurse_DocTypeSOAP";
    public const string SessionDocument = "SOAPDocument";

    //referral Session
    public const string SessionSOAPReferral = "EMR_SOAPReferral";
    public const string SessionSpeciality = "EMR_MasterSpeciality";
    public const string SessionDoctorByOrg = "EMR_MasterDoctorByOrg";

    //rawat inap Session
    public const string SessionSOAPRawatInap = "EMR_SOAPRawatinap";

    public static List<MarkerConfig> setInitializeMarker()
    {
        List<MarkerConfig> MARKERR = new List<MarkerConfig>();

        MARKERR.Add(new MarkerConfig { key = "DOBmarker", value = "unmarked" });
        MARKERR.Add(new MarkerConfig { key = "SAVESOAPmarker", value = "unmarked" });
        MARKERR.Add(new MarkerConfig { key = "SUBMITSOAPmarker", value = "unmarked" });
        MARKERR.Add(new MarkerConfig { key = "TAKENmarker", value = "unmarked" });
        MARKERR.Add(new MarkerConfig { key = "SAVEORDERmarker", value = "unmarked" });

        MARKERR.Add(new MarkerConfig { key = "HFflagsoapisdisable", value = "0" });
        MARKERR.Add(new MarkerConfig { key = "HFflagdrugisdisable", value = "0" });
        MARKERR.Add(new MarkerConfig { key = "HFflagadddrugisdisable", value = "0" });

        MARKERR.Add(new MarkerConfig { key = "BACKUPSOAPmarker", value = "false" });
        MARKERR.Add(new MarkerConfig { key = "IsChooseTemplate", value = "false" });

        return MARKERR;
    }

    public const string SESSIONmarker = "Marker";

    //public static string DOBmarker = "unmarked";
    //public static string SAVESOAPmarker = "unmarked";
    //public static string SUBMITSOAPmarker = "unmarked";
    //public static string TAKENmarker = "unmarked";
    //public static string SAVEORDERmarker = "unmarked";

    //public static string HFflagsoapisdisable = "0";
    //public static string HFflagdrugisdisable = "0";
    //public static string HFflagadddrugisdisable = "0";

    public const string SessionQSPatient = "EMR_querystringpatient";
    public const string SessionLastKeyword = "EMR_lastkeyword";
    public const string SessionLastKeywordAppointment = "EMR_lastkeyword_appointment";

    public static long organizationId
    {
        get
        {
            if (HttpContext.Current.Session[SessionOrganization] != null)
            {
                return long.Parse(HttpContext.Current.Session[SessionOrganization].ToString());
            }
            else
                return 0;
        }
    }

    public static long doctorid
    {
        get
        {
            if (HttpContext.Current.Session[SessionDoctorIdLogin] != null)
            {
                return long.Parse(HttpContext.Current.Session[SessionDoctorIdLogin].ToString());
            }
            else
                return 0;
        }
    }

    public static int language
    {
        get
        {
            return int.Parse(HttpContext.Current.Session[SessionLanguage].ToString());
        }
    }

    public static string ListLabPathologyChecked
    {
        get
        {
            return HttpContext.Current.Session[SessionLabPathologyChecked].ToString();
        }
    }

    public static string GetLoginUser(Page p)
    {
        Label UserName = (Label)p.Master.FindControl("lblUsername");
        return UserName.Text.ToString();
    }

    public static string GetUserID(Page p)
    {
        HiddenField hfUserID = (HiddenField)p.Master.FindControl("hfUserID");
        return hfUserID.Value.ToString();
    }

    public static string GetUserFullname(Page p)
    {
        HiddenField hfUserID = (HiddenField)p.Master.FindControl("hfUserFullName");
        return hfUserID.Value.ToString();
    }

    public static string GetFlagCompound(Page p)
    {
        HiddenField hfflagcompound = (HiddenField)p.Master.FindControl("hfCompoundflag");
        return hfflagcompound.Value.ToString();
    }

    public static string GetDoctorID(Page p)
    {
        HiddenField hfUserID = (HiddenField)p.Master.FindControl("hfDoctorID");
        return hfUserID.Value.ToString();
    }

    public static string GetHospitalID(Page p)
    {
        HiddenField hfHospitalID = (HiddenField)p.Master.FindControl("hfHospitalID");
        return hfHospitalID.Value.ToString();
    }

    public static string GetOrgName(Page p)
    {
        HiddenField hfOrgName = (HiddenField)p.Master.FindControl("hfOrgName");
        return hfOrgName.Value.ToString();
    }


    public static string GetRoleID(Page p)
    {
        HiddenField hfRoleID = (HiddenField)p.Master.FindControl("hfRoleID");
        return hfRoleID.Value.ToString();
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string ipaddress = "";
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }

        return ipaddress;
    }

    //convert List to DataTable
    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table 
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name, type);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

    //convert DataTable to List
    public static List<T> ToDataList<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }

    public static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }

    

    public static string GetTempPatient(Page p)
    {
        HiddenField hfTempPatient = (HiddenField)p.Master.FindControl("hfTempPatient");
        return hfTempPatient.Value.ToString();
    }

    public static string GetTempAdmission(Page p)
    {
        HiddenField hfTempAdmission = (HiddenField)p.Master.FindControl("hfTempAdmission");
        return hfTempAdmission.Value.ToString();
    }

    public static string GetTempEncounter(Page p)
    {
        HiddenField hfTempEncounter = (HiddenField)p.Master.FindControl("hfTempEncounter");
        return hfTempEncounter.Value.ToString();
    }

    public static List<templateMappingID> getMappingTemplateSOAP(Page p)
    {
        List<templateMappingID> templateMappingData = new List<templateMappingID>();
        templateMappingData.Add(new templateMappingID { mappingID = "E851F782-8210-49EB-A074-F26C104F5DDF", mappingName = "S - Chief Complaint" });
        templateMappingData.Add(new templateMappingID { mappingID = "2874A832-8503-4CAD-B5DD-535775E94AC0", mappingName = "S - Anamnesis" });
        templateMappingData.Add(new templateMappingID { mappingID = "7218971C-E89F-4172-AE3C-B7FB855C1D6D", mappingName = "Objective" });
        templateMappingData.Add(new templateMappingID { mappingID = "D24D0881-7C06-4563-BF75-3A20B843DC47", mappingName = "Assessment" });
        templateMappingData.Add(new templateMappingID { mappingID = "337A371F-BAF5-424A-BDC5-C320C277CAC6", mappingName = "Planning" });
        return templateMappingData;
    }

    public static void LinkBinder(Page p, string PatientId, string AdmissionId, string EncounterId, string PagefaId, string PageSoapId, string AppointmentId, string IsTele)
    {
        LinkBinder(p, PatientId, AdmissionId, EncounterId, PagefaId, PageSoapId);

        if (PageSoapId.ToUpper() == "00000000-0000-0000-0000-000000000000" || PageSoapId.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            if (IsTele == "False")
            {
                HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
                SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/stdsoapPagelite.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
                    , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);
            }
            else if (IsTele == "True")
            {
                List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)(HttpContext.Current.Session[SessionOrganizationSetting]);
                if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_TELECONSUL_SOAP".ToUpper()).setting_value == "TRUE")
                {
                    HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
                    SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/stdsoapteleconsultation.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
                        , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);
                }
                else
                {
                    HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
                    SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/stdsoapPagelite.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
                        , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);
                }


            }
        }
        else if (PageSoapId.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A" || PageSoapId.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002" || PageSoapId.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF")
        {
            HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
            SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/StdSoapPageSpecialty.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
                , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);
        }
        else
        {
            HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
            SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/stdsoaptemplate.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
                , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

            //HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
            //SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/StdSoapPageSpecialty.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            //    , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);
        }

        HyperLink PatientDashboard = (HyperLink)p.Master.FindControl("PatientDashboardLink");
        PatientDashboard.NavigateUrl =
            string.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

        HyperLink PatientHistory = (HyperLink)p.Master.FindControl("PatientHistoryLink");
        PatientHistory.NavigateUrl = string.Format("~/Form/General/PatientHistory.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

        HyperLink MedicationHistory = (HyperLink)p.Master.FindControl("MedicationHistoryLink");
        MedicationHistory.NavigateUrl = string.Format("~/Form/General/MedicationHistory.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

        HyperLink Orderset = (HyperLink)p.Master.FindControl("OrderSetLink");
        Orderset.NavigateUrl = string.Format("~/Form/General/OrderSet/ManageOrderSet.aspx",
            PatientId, AdmissionId, EncounterId);

        HyperLink Result = (HyperLink)p.Master.FindControl("ResultLink");
        Result.NavigateUrl = string.Format("~/Form/General/Result/Result.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

        HyperLink ResultLabRad = (HyperLink)p.Master.FindControl("ResultLinkLabRad");
        ResultLabRad.NavigateUrl = string.Format("~/Form/General/Result/ResultLABRAD.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

        HyperLink LinkVaccineHistory = (HyperLink)p.Master.FindControl("CovidFormLink");
        LinkVaccineHistory.NavigateUrl = string.Format("~/Form/General/View/ViewCovidVaccination.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);

        HyperLink LinkDiabisa = (HyperLink)p.Master.FindControl("DiabisaLink");
        LinkDiabisa.NavigateUrl = string.Format("~/Form/General/View/ViewDiabisa.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}&AppointmentId={4}&IsTele={5}"
            , PatientId, AdmissionId, EncounterId, PageSoapId, AppointmentId, IsTele);
    }

    public static void LinkBinder(Page p, string PatientId, string AdmissionId, string EncounterId,string PagefaId,string PageSoapId)
    {
        //string encvalue = Encrypt("?idPatient=" + PatientId + "&AdmissionId="+ AdmissionId + "&EncounterId="+ EncounterId);
        List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)(HttpContext.Current.Session[SessionOrganizationSetting]);

        //List<string> sessionpasien = new List<string>();
        //sessionpasien.Add(PatientId);
        //sessionpasien.Add(AdmissionId);
        //sessionpasien.Add(EncounterId);
        //sessionpasien.Add(PageSoapId);
        //HttpContext.Current.Session[SessionQSPatient] = sessionpasien;

        HyperLink worklistlink = (HyperLink)p.Master.FindControl("linkGoSomewhere");
        if (orgsetting.Find(y => y.setting_name.ToUpper() == "WORKLIST_TYPE".ToUpper()).setting_value == "APPOINTMENT")
        {
            worklistlink.NavigateUrl =
            string.Format("~/Form/General/WorklistAppointment.aspx");
        }
        else if (orgsetting.Find(y => y.setting_name.ToUpper() == "WORKLIST_TYPE".ToUpper()).setting_value == "NON_APPOINTMENT")
        {
            worklistlink.NavigateUrl =
            string.Format("~/Form/General/WorklistNonAppointment.aspx");            
        }
        HyperLink PatientDashboard = (HyperLink)p.Master.FindControl("PatientDashboardLink");
        PatientDashboard.NavigateUrl =
            string.Format("~/Form/General/PatientDashboard.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

        //string.Format("~/Form/General/PatientDetail.aspx"+ encvalue);

        //HyperLink FirstAnalysis = (HyperLink)p.Master.FindControl("FirstAnalysisLink");
        //if (PagefaId == "00000000-0000-0000-0000-000000000000" || PagefaId == "136219c4-7dff-4490-97f2-62f6667c2346")
        //{
        //    FirstAnalysis.NavigateUrl = string.Format("~/Form/General/FirstAssesment/Template/StdFirstAssesment.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}"
        //    , PatientId, AdmissionId, EncounterId, PagefaId, PageSoapId);
        //}
        //else if (PagefaId == "fcda0e46-16fd-40bc-b3a4-cc71fe372ef7")
        //{
        //    FirstAnalysis.NavigateUrl = string.Format("~/Form/General/FirstAssesment/Template/StdPediatric.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PagefaId={3}&PageSoapId={4}"
        //    , PatientId, AdmissionId, EncounterId, PagefaId, PageSoapId);
        //}

        if (PageSoapId.ToUpper() == "00000000-0000-0000-0000-000000000000" || PageSoapId.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
            SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/stdsoapPagelite.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
                , PatientId, AdmissionId, EncounterId, PageSoapId);
        }
        else //if (PageSoapId == "212cbbfa-3e70-4a4e-9e82-d687757a9a08")
        {
            HyperLink SOAP = (HyperLink)p.Master.FindControl("SOAPLink");
            SOAP.NavigateUrl = string.Format("~/Form/SOAP/Template/StdSoapPageSpecialty.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
                , PatientId, AdmissionId, EncounterId, PageSoapId);
        }
        HyperLink PatientHistory = (HyperLink)p.Master.FindControl("PatientHistoryLink");
        PatientHistory.NavigateUrl = string.Format("~/Form/General/PatientHistory.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

        HyperLink MedicationHistory = (HyperLink)p.Master.FindControl("MedicationHistoryLink");
        MedicationHistory.NavigateUrl = string.Format("~/Form/General/MedicationHistory.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

        HyperLink Orderset = (HyperLink)p.Master.FindControl("OrderSetLink");
        Orderset.NavigateUrl = string.Format("~/Form/General/OrderSet/ManageOrderSet.aspx",
            PatientId, AdmissionId, EncounterId);

        HyperLink Result = (HyperLink)p.Master.FindControl("ResultLink");
        Result.NavigateUrl = string.Format("~/Form/General/Result/Result.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

        HyperLink ResultLabRad = (HyperLink)p.Master.FindControl("ResultLinkLabRad");
        ResultLabRad.NavigateUrl = string.Format("~/Form/General/Result/ResultLABRAD.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

        HyperLink LinkVaccineHistory = (HyperLink)p.Master.FindControl("CovidFormLink");
        LinkVaccineHistory.NavigateUrl = string.Format("~/Form/General/View/ViewCovidVaccination.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

        HyperLink LinkDiabisa = (HyperLink)p.Master.FindControl("DiabisaLink");
        LinkDiabisa.NavigateUrl = string.Format("~/Form/General/View/ViewDiabisa.aspx?idPatient={0}&AdmissionId={1}&EncounterId={2}&PageSoapId={3}"
            , PatientId, AdmissionId, EncounterId, PageSoapId);

    }

    public static void ResponseRedirectFA(string PatientId, string AdmissionId, string EncounterId, string PagefaId, string PageSoapId)
    {
        if (PagefaId.ToUpper() == "00000000-0000-0000-0000-000000000000" || PagefaId.ToUpper() == "136219C4-7DFF-4490-97F2-62F6667C2346")
        {
            HttpContext.Current.Response.Redirect("~/Form/General/FirstAssesment/Template/StdFirstAssesment.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
            + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId, false);
        }
        else if (PagefaId.ToUpper() == "FCDA0E46-16FD-40BC-B3A4-CC71FE372EF7")
        {
            HttpContext.Current.Response.Redirect("~/Form/General/FirstAssesment/Template/StdPediatric.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
            + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId, false);
        }
    }

    public static void ResponseRedirectSOAP(string PatientId, string AdmissionId, string EncounterId, string PagefaId, string PageSoapId, string AppointmentId, string IsTele)
    {
        if (PageSoapId.ToUpper() == "00000000-0000-0000-0000-000000000000" || PageSoapId.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            if (IsTele == "False")
            {
                HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdsoapPagelite.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
                + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId + "&AppointmentId=" + AppointmentId + "&IsTele=" + IsTele, false);
            }
            else if (IsTele == "True")
            {
                List<ViewOrganizationSetting> orgsetting = (List<ViewOrganizationSetting>)(HttpContext.Current.Session[SessionOrganizationSetting]);
                if (orgsetting.Find(y => y.setting_name.ToUpper() == "USE_TELECONSUL_SOAP".ToUpper()).setting_value == "TRUE")
                {
                    HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdsoapTeleconsultation.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
                + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId + "&AppointmentId=" + AppointmentId + "&IsTele=" + IsTele, false);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdsoapPagelite.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
                + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId + "&AppointmentId=" + AppointmentId + "&IsTele=" + IsTele, false);
                }
            }
        }
        else if (PageSoapId.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A" || PageSoapId.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002" || PageSoapId.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF")
        {
            HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdSoapPageSpecialty.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
            + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId + "&AppointmentId=" + AppointmentId + "&IsTele=" + IsTele, false);
        }
        else
        {
            HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdsoapTemplate.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
                + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId + "&AppointmentId=" + AppointmentId + "&IsTele=" + IsTele, false);

            //HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdSoapPageSpecialty.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
            //    + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId + "&AppointmentId=" + AppointmentId + "&IsTele=" + IsTele, false);
        }
    }

    public static void ResponseRedirectSOAP(string PatientId, string AdmissionId, string EncounterId, string PagefaId, string PageSoapId)
    {
        if (PageSoapId.ToUpper() == "00000000-0000-0000-0000-000000000000" || PageSoapId.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
        {
            HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdsoapPagelite.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
            + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId, false);
        }
        else //if (PageSoapId == "212cbbfa-3e70-4a4e-9e82-d687757a9a08")
        {
            HttpContext.Current.Response.Redirect("~/Form/SOAP/Template/StdSoapPageSpecialty.aspx?idPatient=" + PatientId + "&AdmissionId=" + AdmissionId
            + "&EncounterId=" + EncounterId + "&PagefaId=" + PagefaId + "&PageSoapId=" + PageSoapId, false);
        }
    }

    public static string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string formatDecimal(string ColumnOfTable)
    {
        string[] temp = ColumnOfTable.ToString().Split('.');

        if (temp.Count() > 1)
        {
            if (temp[1].Length == 3)
            {
                if (temp[1] == "000")
                {
                    ColumnOfTable = decimal.Parse(temp[0]).ToString();
                }
                else if (temp[1].Substring(temp[1].Length - 2) == "00")
                {
                    ColumnOfTable = temp[0] + "." + temp[1].Substring(0, 1);
                }
                else if (temp[1].Substring(temp[1].Length - 1) == "0")
                {
                    ColumnOfTable = temp[0] + "." + temp[1].Substring(0, 2);
                }
            }
        }

        return ColumnOfTable;
    }

    public static string GenerateURLPrint(string printtype, string org_id, string adm_id, string enc_id, string ptn_id, string printby, string doktername, string pagesoapid)
    {
        string IP = GetLocalIPAddress();
        string URL = "";

        if (printtype == "SHORTSOAP")
        {
            URL = "http://" + IP + "/printingemr?printtype=MedicalResumeLite&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&PrintBy=" + printby + "&DoctorBy=" + doktername;
        }
        else if (printtype == "LONGSOAP")
        {
            URL = "http://" + IP + "/printingemr?printtype=MedicalResume&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&PageSOAP=" + pagesoapid + "&PrintBy=" + printby;
        }
        else if (printtype == "LAB")
        {
            URL = "http://" + IP + "/printingemr?printtype=OrderLab&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&IsLabRad=True" + "&PrintBy=" + printby;
        }
        else if (printtype == "RAD")
        {
            URL = "http://" + IP + "/printingemr?printtype=OrderRad&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&IsLabRad=True" + "&PrintBy=" + printby;
        }
        // procedure and diagnostic
        else if (printtype == "ProcedureDiagnostic")
        {
            URL = "http://" + IP + "/printingemr?printtype=ProcedureDiagnostic&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&PageSOAP=" + pagesoapid + "&PrintBy=" + printby;
        }

        return URL;
    }

}