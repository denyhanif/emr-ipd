using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for QRCodeLogin
/// </summary>
public class QRCodeLogin
{
	public QRCodeLogin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region QR Code

    public class Result_check_login_qr
    {
        public string company { get; set; }
        public string status { get; set; }
        public Int16 code { get; set; }
        public string message { get; set; }
        private List<Login> lists = new List<Login>();
        [JsonProperty("data")]
        public List<Login> list { get { return lists; } }
        public Int16 total { get; set; }
    }
    // model for generate QR Code
    public class QRCodeGenerate
    {
        public string base64StringQR { get; set; }
        public Guid qr_guid { get; set; }
        public string computer_name { get; set; }
        public List<QRCodeResponse> QRCodeResponse { get; set; }
    }
    #endregion

    #region model helper
    public class QRCodeResponse
    {
        public Int16 message_code { get; set; }
        public string message_text { get; set; }
    }
    #endregion
}