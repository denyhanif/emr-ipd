using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_Control_TempCard : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        log4net.ThreadContext.Properties["Organization"] = MyUser.GetHopeOrgIDLog();
    }

    [WebMethod]
    public static string LoadStickerPatient(string PatientId, string AdmissiontId, string EncounterId)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var _UserID = HttpContext.Current.Session[Helper.SessionUserID].ToString();

        try
        {
            var DataSticker1 = clsHealthInfo.GetStickerHealthInfo1(long.Parse(PatientId));
            var StickerJson1 = JObject.Parse(DataSticker1.Result);
            var DataSticker2 = clsHealthInfo.GetStickerHealthInfo2(long.Parse(MyUser.GetHopeOrgID()), long.Parse(PatientId), long.Parse(AdmissiontId), EncounterId);
            var StickerJson2 = JObject.Parse(DataSticker2.Result);

            var Status = StickerJson1["status"].ToString();
            var Message = StickerJson1["message"].ToString();

            Log.Debug(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", PatientId, "LoadStickerPatient", StartTime, "OK", _UserID, "", "", "Status : " + Status + "Message : " + Message));

            if (Status == "Fail")
            {
                return Status + ", " + Message;
            }
            else
            {
                ((JObject)StickerJson1["data"]).Add(new JProperty("is_covidvac", StickerJson2["data"]["is_covidvac"].ToString()));
                return StickerJson1["data"].ToString();
            }

        }
        catch (Exception ex)
        {
            Log.Error(LogLibrary.SaveLogNew(Helper.organizationId.ToString(), "PatientId", PatientId, "LoadStickerPatient", StartTime, "ERROR", _UserID, "", "", ex.Message));
            return "ERROR" + ", " + ex.ToString();
        }
    }
}