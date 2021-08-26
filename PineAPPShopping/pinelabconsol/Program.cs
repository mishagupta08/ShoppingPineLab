using Newtonsoft.Json;
using pinelabconsol.Interface;
using pinelabconsol.Models;
using pinelabconsol.Repoistory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pinelabconsol
{
    class Program
    {
        
        #region variables

        private static int BUNCH_SIZE = Convert.ToInt32(ConfigurationSettings.AppSettings["RecordBunchSize"].ToString());
        public static DataTable dtJobSMSQue = new DataTable();
        public static DataTable dtJobEmailQue = new DataTable();
        public static DataTable dtgetOrderStatus = new DataTable();
        private static byte[] KeyByte = Encoding.ASCII.GetBytes("6b04d38748f94490a636cf1be3d82841");
        private static byte[] IVByte = Encoding.ASCII.GetBytes("f8adbf3c94b7463d");

        #endregion
        string LiveCONNECTION_STRING = ConfigurationManager.ConnectionStrings["liveconstr"].ConnectionString;
        I_Sms isms;


         public Program()
        {
            this.isms = new R_SMS();
            
        }
        public static void Main(string[] args)
        {
            Program p = new Program();
           // p.ProccessSmsQueue();
           // p.SendEmailQue();
            p.GetOrderStatusQue();
        }
        public void ProccessSmsQueue()
        {
            try
            {
                SendSmsQue();
                //SendEmailQue();
            }
            catch (Exception ex)
            {
                
            }
        }


        //public string SendEmailQue()
        //{
        //    int cnt;
        //    try
        //    {
                

        //        cnt = 0;
        //        string email = "";
        //        string Sql = "";
        //        DataSet ds = isms.GetEmailQue();
        //        if ((ds.Tables[0].Rows.Count > 0))
        //        {
        //            email = Convert.ToString(ds.Tables[0].Rows[0]["Body"]);
        //            StringBuilder sb = new StringBuilder();
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
                        
        //                foreach (DataRow row in ds.Tables[0].Rows)
        //                {
        //                    //LogService("SUCCESS-2");
        //                    if (Convert.ToString(row["ToEmail"]).Length >= 0)
        //                    {
        //                        if ((sendEmail(Convert.ToString(row["FromEmail"]), Convert.ToString(row["ToEmail"]), Convert.ToString(row["Body"]), Convert.ToString(row["Subject"]), Convert.ToString(row["SMSUserName"]), Convert.ToString(row["SMSPassword"]), Convert.ToString(row["SMTPServer"])) == true))
        //                        {
        //                            //LogService("SUCCESS-3");
        //                            sb.AppendLine("<EMAIL>");
        //                            sb.AppendLine("<EMAILData>");
        //                            sb.AppendLine(("<JOBEMAILQueID>"
        //                                            + (Convert.ToString(row["ID"]).Trim() + "</JOBEMAILQueID>")));
        //                            //sb.AppendLine(("<Body> "
        //                            //                + (Convert.ToString(row["Body"]).Trim() + "</Body>")));
        //                            sb.AppendLine(("<IsEmailSent>"
        //                                            + (Convert.ToString(row["IsMailSent"]).Trim() + "</IsEmailSent>")));
        //                            sb.AppendLine(("<FromEmailID>"
        //                                            + (Convert.ToString(row["FromEmail"]).Trim() + "</FromEmailID>")));
        //                            sb.AppendLine(("<ToEmailID>"
        //                                            + (Convert.ToString(row["ToEmail"]).Trim() + "</ToEmailID>")));
        //                            sb.AppendLine(("<EMAILType>"
        //                                           + (Convert.ToString(row["EMAILType"]).Trim() + "</EMAILType>")));
        //                            sb.AppendLine(("<IsError>"
        //                                          + (Convert.ToString("0") + "</IsError>")));
        //                            sb.AppendLine(("<CompanyID>"
        //                                        + (Convert.ToString(row["CompanyID"]) + "</CompanyID>")));
        //                            sb.AppendLine("</EMAILData>");
        //                            sb.AppendLine("</EMAIL>");
        //                        }
        //                    }
        //                }
        //                //LogService("XML" + sb.ToString());

        //                int smsStatus = isms.SaveEMAIL(sb.ToString());
        //                //LogService("SUCCESS-4");
        //                //LogService("" + smsStatus);
        //            }
        //            return ("Message Count=" + (cnt + ""));
        //        }

        //        return ("Message Count=" + (cnt + ""));
        //    }
        //    catch (Exception ex)
        //    {
                
        //        return "";
        //        throw ex;
        //    }
        //}

         public void SendEmailQue()
        {
            try
            {

                Task[] tasksArray = new Task[1];
                tasksArray[0] = Task.Factory.StartNew(() =>
                GetJOBEmailQue()
                );
                Task.WaitAll(tasksArray);
            }
             catch( Exception ex )
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        private bool sendEmail(string fromEmail, string toEmail, string body, string subject, string userName, string password, string smp) /*string athPath*/
        {
            try
            {
                
                string StrMsg = "";
                //System.Net.Mail.MailAddress SendFrom = new System.Net.Mail.MailAddress(fromEmail);
                //System.Net.Mail.MailAddress SendTo = new System.Net.Mail.MailAddress(toEmail);
                System.Net.Mail.MailMessage MyMessage = new System.Net.Mail.MailMessage();
                StrMsg = body;
                string[] multi = toEmail.Split(',');
                foreach (string mul in multi)
                {
                    MyMessage.To.Add(new System.Net.Mail.MailAddress(mul));
                }
                if (string.IsNullOrEmpty(fromEmail))
                {
                    MyMessage.From = new System.Net.Mail.MailAddress("ccareutility@gmail.com");
                    userName = "ccareutility@gmail.com";
                    password = "CCare12345";
                }
                else
                {
                    MyMessage.From = new System.Net.Mail.MailAddress(fromEmail);
                }

                MyMessage.Bcc.Add(new System.Net.Mail.MailAddress("amit.bispl@gmail.com"));
                MyMessage.Bcc.Add(new System.Net.Mail.MailAddress("mukesh.innovative@gmail.com"));
                MyMessage.Subject = subject;
                MyMessage.Body = StrMsg;
                //if (!string.IsNullOrEmpty(athPath))
                //{
                //    System.Net.Mail.Attachment attachment;
                //    attachment = new System.Net.Mail.Attachment(athPath);
                //    MyMessage.Attachments.Add(attachment);
                //}
                MyMessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smp);
                //smtp.Port = 995;         
                smtp.Port = 587;
                smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(userName, password);
                smtp.Send(MyMessage);
            }
            catch (Exception ex)
            {
               
                return false;
            }
            return true;
        }



        public void SendSmsQue()
        {
            int cnt;
            // LogService("SUCCESS-1");

            try
            {
                Task[] tasksArray = new Task[1];
                tasksArray[0] = Task.Factory.StartNew(() =>
                GetJOBSMSQue()
                );
                Task.WaitAll(tasksArray);
            }
            catch (Exception ex)
            {

            }
            //try
            //{

            //    cnt = 0;
            //    string email = "";
            //    string Sql = "";
            //    DataSet ds = isms.GetSMSQue();
            //    cnt = 0;
            //    string sms = "";
            //    if ((ds.Tables[0].Rows.Count > 0))
            //    {
            //        sms = Convert.ToString(ds.Tables[0].Rows[0]["SMSBody"]);
            //        StringBuilder sb = new StringBuilder();
            //        List<M_Sms> message = new List<M_Sms>();
            //        //var parallelOptions = new ParallelOptions();
            //        //var maxProc = System.Environment.ProcessorCount;
            //        //parallelOptions.MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling(maxProc * 0.75));
            //        //try
            //        //{

            //        //}
            //        // catch( Exception ex)
            //        //{

            //        //}
            //        // this option use around 75 % core capacity
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow row in ds.Tables[0].Rows)
            //            {
            //                if (Convert.ToString(row["MobileNo"]).Length >= 10)
            //                {
            //                    M_Sms msg = new M_Sms()
            //                    {
            //                        JOBSMSQueID= Convert.ToString(row["ID"]),
            //                        SMSBody= Convert.ToString(row["SMSBody"]),
            //                        IsSMSSent= Convert.ToString(row["IsSMSSent"]),
            //                        mobileno= Convert.ToString(row["mobileno"]),
            //                        SMSType= Convert.ToString(row["SMSType"]),
            //                        IsError= Convert.ToString("0"),
            //                        CompanyID= Convert.ToString(row["CompanyID"]),
            //                        SMSAPI= Convert.ToString(row["SMSAPI"]),
            //                        SMSUSerNAme= Convert.ToString(row["SMSUSerNAme"]),
            //                        SMSPassword=Convert.ToString(row["SMSPassword"]),
            //                        SMSSenderID=Convert.ToString(row["SMSSenderID"])
            //                    };
            //                    message.Add(msg);
            //                    messageSenger(message);
            //                    //if ((SendSMS(Convert.ToString(row["MobileNo"]), Convert.ToString(row["SMSBody"]), Convert.ToString(row["SMSUSerNAme"]), Convert.ToString(row["SMSPassword"]), Convert.ToString(row["SMSSenderID"]), Convert.ToString(row["SMSAPI"])) == true))
            //                    //{
            //                    //    LogService("SUCCESS-0");
            //                    //    sb.AppendLine("<SMS>");
            //                    //    sb.AppendLine("<SMSData>");
            //                    //    sb.AppendLine(("<JOBSMSQueID>"
            //                    //                    + (Convert.ToString(row["ID"]).Trim() + "</JOBSMSQueID>")));
            //                    //    sb.AppendLine(("<SMSBody> "
            //                    //                    + (Convert.ToString(row["SMSBody"]).Trim() + "</SMSBody>")));
            //                    //    sb.AppendLine(("<IsSMSSent>"
            //                    //                    + (Convert.ToString(row["IsSMSSent"]).Trim() + "</IsSMSSent>")));
            //                    //    sb.AppendLine(("<mobileno>"
            //                    //                    + (Convert.ToString(row["mobileno"]).Trim() + "</mobileno>")));
            //                    //    sb.AppendLine(("<SMSType>"
            //                    //                    + (Convert.ToString(row["SMSType"]).Trim() + "</SMSType>")));
            //                    //    sb.AppendLine(("<IsError>"
            //                    //                  + (Convert.ToString("0") + "</IsError>")));
            //                    //    sb.AppendLine(("<CompanyID>"
            //                    //                + (Convert.ToString(row["CompanyID"]) + "</CompanyID>")));
            //                    //    sb.AppendLine("</SMSData>");
            //                    //    sb.AppendLine("</SMS>");
            //                    //}
            //                }
            //            }
            //            //LogService("XML" + sb.ToString());

            //            //int smsStatus = isms.SaveSMS(sb.ToString());

            //        }
            //        return ("Message Count=" + (cnt + ""));
            //    }

            //    return ("Message Count=" + (cnt + ""));
            //}
            //catch (Exception ex)
            //{
            //    LogService(ex.Message);
            //    return "";
            //}
        }

        public void messageSenger(List<M_Sms> msg)
        {
            var parallelOptions = new ParallelOptions();
            var maxProc = System.Environment.ProcessorCount;
            // this option use around 75% core capacity
            parallelOptions.MaxDegreeOfParallelism = Convert.ToInt32(Math.Ceiling(maxProc * 0.75));
            try
            {



                //Parallel.ForEach(msg, parallelOptions, message =>
                //{
                //    try
                //    {
                //        SendSMS(message.mobileno, message.SMSBody, message.SMSUSerNAme, message.SMSPassword, message.SMSSenderID, message.SMSAPI);
                //       
                //_logger.Info($"Successfully sent {text.Title}.");
                //    }
                //    catch (Exception ex)
                //    {
                //        //_logger.Error($"Something went wrong {ex}.");
                //    }
                //});
            }
            catch (Exception ex)
            {

            }
        }

         private void GetJOBEmailQue()
        {
            int cnt;
            try
            {
                cnt = 0;
                string email = "";
                string Sql = "";
                DataSet ds = isms.GetEmailQue();
                if (ds != null)
                {
                    dtJobEmailQue = ds.Tables[0];
                    int n = 0; int rcount = dtJobEmailQue.Rows.Count;
                    while (n <= (int)(rcount / BUNCH_SIZE))
                    {
                        Task[] taskArrayJOBEmailQue = new Task[n == (int)(rcount / BUNCH_SIZE) ? (rcount % BUNCH_SIZE) : BUNCH_SIZE];
                        int taskArrayCounter = 0;
                        for (int i = n * BUNCH_SIZE; i < rcount && i < (n + 1) * BUNCH_SIZE; i++)
                        {
                            taskArrayJOBEmailQue[taskArrayCounter++] = Task.Factory.StartNew((Object obj) =>
                            {
                                try
                                {
                                    int counter = (int)obj;
                                    string JOBEMAILQueID = Convert.ToString(ds.Tables[0].Rows[counter]["ID"]);
                                    string IsEmailSent = Convert.ToString(ds.Tables[0].Rows[counter]["IsMailSent"]);
                                    string FromEmailID = Convert.ToString(ds.Tables[0].Rows[counter]["FromEmail"]);
                                    string ToEmailID = Convert.ToString(ds.Tables[0].Rows[counter]["ToEmail"]);
                                    string EMAILType = Convert.ToString(ds.Tables[0].Rows[counter]["EMAILType"]);
                                    //string IsError = Convert.ToString(ds.Tables[0].Rows[counter]["IsError"]);
                                    string CompanyID = Convert.ToString(ds.Tables[0].Rows[counter]["CompanyID"]);
                                   // string Password = Convert.ToString(ds.Tables[0].Rows[counter]["SMSPassword"]);
                                   // string SenderId = Convert.ToString(ds.Tables[0].Rows[counter]["SMSSenderID"]);
                                    // var tplResult = sendEmail(Convert.ToString(ds.Tables[0].Rows[counter]["FromEmail"]), Convert.ToString(ds.Tables[0].Rows[counter]["ToEmail"]), Convert.ToString(ds.Tables[0].Rows[counter]["Body"]), Convert.ToString(ds.Tables[0].Rows[counter]["Subject"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSUserName"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSPassword"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMTPServer"]));
                                    StringBuilder sb = new StringBuilder();
                                    if (Convert.ToString(ds.Tables[0].Rows[counter]["ToEmail"]).Length >= 0)
                                    {
                                        if ((sendEmail(Convert.ToString(ds.Tables[0].Rows[counter]["FromEmail"]), Convert.ToString(ds.Tables[0].Rows[counter]["ToEmail"]), Convert.ToString(ds.Tables[0].Rows[counter]["Body"]), Convert.ToString(ds.Tables[0].Rows[counter]["Subject"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSUserName"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSPassword"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMTPServer"])) == true))
                                        {
                                            //LogService("SUCCESS-3");
                                            sb.AppendLine("<EMAIL>");
                                            sb.AppendLine("<EMAILData>");
                                            sb.AppendLine(("<JOBEMAILQueID>"
                                                            + (Convert.ToString(ds.Tables[0].Rows[counter]["ID"]).Trim() + "</JOBEMAILQueID>")));
                                            sb.AppendLine(("<Body> "
                                                            + (Convert.ToString(ds.Tables[0].Rows[counter]["Body"]).Trim() + "</Body>")));
                                            sb.AppendLine(("<IsEmailSent>"
                                                            + (Convert.ToString(ds.Tables[0].Rows[counter]["IsMailSent"]).Trim() + "</IsEmailSent>")));
                                            sb.AppendLine(("<FromEmailID>"
                                                            + (Convert.ToString(ds.Tables[0].Rows[counter]["FromEmail"]).Trim() + "</FromEmailID>")));
                                            sb.AppendLine(("<ToEmailID>"
                                                            + (Convert.ToString(ds.Tables[0].Rows[counter]["ToEmail"]).Trim() + "</ToEmailID>")));
                                            sb.AppendLine(("<EMAILType>"
                                                           + (Convert.ToString(ds.Tables[0].Rows[counter]["EMAILType"]).Trim() + "</EMAILType>")));
                                            //sb.AppendLine(("<IsError>"
                                            //              + (Convert.ToString("0") + "</IsError>")));
                                            sb.AppendLine(("<CompanyID>"
                                                        + (Convert.ToString(ds.Tables[0].Rows[counter]["CompanyID"]) + "</CompanyID>")));
                                            sb.AppendLine("</EMAILData>");
                                            sb.AppendLine("</EMAIL>");
                                             int EmailStatus = isms.SaveEMAIL(sb.ToString());
                                        }
                                    }
                                
                                }
                                catch (Exception ex )
                                {
                                    int counter = (int)obj;
                                   

                                }

                            }, i);
                        }
                        Task.WaitAll(taskArrayJOBEmailQue);
                        n++;
                    }
                }
            }
            catch (Exception ex )
            {

            }
        }

        private  void GetJOBSMSQue()
        {
            try
            {
                //CommonFunction.WriteLog("SUCCESS:2", CommonFunction.EnumAppKeys.LogFilePath.ToString());

                int IsSmsSend = 1;
                  DataSet ds= GetSMSQue();
                //DataSet ds1 = isms.GetSMSQue();
                if (ds != null)
                {
                    dtJobSMSQue = ds.Tables[0];
                    int n = 0; int rcount = dtJobSMSQue.Rows.Count;
                    while (n <= (int)(rcount / BUNCH_SIZE))
                    {
                        //Task[] taskArrayUpdateMobile = new Task[DSStudentUpdateMobileNo.Rows.Count];
                        //for (int i = 0; i < DSStudentUpdateMobileNo.Rows.Count; i++)
                        Task[] taskArrayJOBSMSQue = new Task[n == (int)(rcount / BUNCH_SIZE) ? (rcount % BUNCH_SIZE) : BUNCH_SIZE];
                        int taskArrayCounter = 0;
                        for (int i = n * BUNCH_SIZE; i < rcount && i < (n + 1) * BUNCH_SIZE; i++)
                        {
                            taskArrayJOBSMSQue[taskArrayCounter++] = Task.Factory.StartNew((Object obj) =>
                            {
                                try
                                {
                                    int counter = (int)obj;
                                    string JobSMSQueID = Convert.ToString(ds.Tables[0].Rows[counter]["ID"]);
                                    string SMSBody = Convert.ToString(ds.Tables[0].Rows[counter]["SMSBody"]);
                                    string IsSMSSent = Convert.ToString(ds.Tables[0].Rows[counter]["IsSMSSent"]);
                                    string SMSType = Convert.ToString(ds.Tables[0].Rows[counter]["SMSType"]);
                                    string CompanyID = Convert.ToString(ds.Tables[0].Rows[counter]["CompanyID"]);
//string IsError = Convert.ToString(ds.Tables[0].Rows[counter]["IsError"]);
                                    string UserName = Convert.ToString(ds.Tables[0].Rows[counter]["SMSUSerNAme"]);
                                    string Password = Convert.ToString(ds.Tables[0].Rows[counter]["SMSPassword"]);
                                    string SenderId = Convert.ToString(ds.Tables[0].Rows[counter]["SMSSenderID"]);
                                    // Tuple<int, string, string> tplResult = new Tuple<int, string, string>(-1, "", "");
                                    var tplResult = SendSMS(Convert.ToString(ds.Tables[0].Rows[counter]["mobileno"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSBody"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSUSerNAme"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSPassword"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSSenderID"]), Convert.ToString(ds.Tables[0].Rows[counter]["SMSAPI"]));
                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine("<SMS>");
                                    sb.AppendLine("<SMSData>");
                                    sb.AppendLine(("<JOBSMSQueID>"
                                                    + (Convert.ToString(ds.Tables[0].Rows[counter]["ID"]).Trim() + "</JOBSMSQueID>")));
                                    sb.AppendLine(("<SMSBody> "
                                                    + (Convert.ToString(ds.Tables[0].Rows[counter]["SMSBody"]).Trim() + "</SMSBody>")));
                                    sb.AppendLine(("<IsSMSSent>"
                                                    + (Convert.ToString(ds.Tables[0].Rows[counter]["IsSMSSent"]).Trim() + "</IsSMSSent>")));
                                    sb.AppendLine(("<mobileno>"
                                                    + (Convert.ToString(ds.Tables[0].Rows[counter]["mobileno"]).Trim() + "</mobileno>")));
                                    sb.AppendLine(("<SMSType>"
                                                    + (Convert.ToString(ds.Tables[0].Rows[counter]["SMSType"]).Trim() + "</SMSType>")));
                                    //sb.AppendLine(("<IsError>"
                                    //              + (Convert.ToString("0") + "</IsError>")));
                                    sb.AppendLine(("<CompanyID>"
                                                + (Convert.ToString(ds.Tables[0].Rows[counter]["CompanyID"]) + "</CompanyID>")));
                                    sb.AppendLine("</SMSData>");
                                    sb.AppendLine("</SMS>");


                                    //IsSmsSend = tplResult.Item1;
                                    //sms = SmsMessage;
                                    //if (IsSmsSend == 0)
                                    //{
                                    int sms = isms.SaveSMS(sb.ToString());
                                    //}
                                }
                                catch (Exception ex)
                                {

                                }
                            }, i);
                        }
                        Task.WaitAll(taskArrayJOBSMSQue);
                        n++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

         public  DataSet  GetSMSQue()
        {
            DataSet ds = isms.GetSMSQue();
            return ds;
        }

        private bool SendSMS(string Mobl, string sms, string smsUserName, string smsPassword, string smsSenderId, string smsAPI)
        {
            WebClient client = new WebClient();
            Stream data;
            
            try
            {
                
                string baseurl = smsAPI;
                baseurl = baseurl.Replace("#username#", smsUserName);
                baseurl = baseurl.Replace("#password#", smsPassword);
                baseurl = baseurl.Replace("#mobileno#", Mobl);
                baseurl = baseurl.Replace("#message#", sms);
                baseurl = baseurl.Replace("#Sendid#", smsSenderId);
                data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s;
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                
                // LogService("BaseURL" + baseurl);
                //LogService("Response" + s);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

         public void GetOrderStatus()
        {
            try
            {

                Task[] tasksArray = new Task[1];
                tasksArray[0] = Task.Factory.StartNew(() =>
                GetOrderStatusQue()
                );
                Task.WaitAll(tasksArray);
            }
             catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

         public void GetOrderStatusQue ()
        {
            int cnt;
            General clsgen = new General();
            var responceObject = new ResponceDetail();
            try
            {
               //string path = System.Web.HttpContext.Current.Server.MapPath("~/Logs/ErrorLog");
               // if (!Directory.Exists(path))
               // {
               //     Directory.CreateDirectory(path);
               // }
                cnt = 0;
                DataSet ds = isms.GetOrderStatus();
               // clsgen.ErrorLog(path, ("SUCCESS-0:" + ds));
                M_reqOrderStatus orderstus = new M_reqOrderStatus();
                HttpWebRequest reponse;
              
                string output2 = string.Empty;
                string jsonResponse = string.Empty;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        orderstus.CompanyId = "1";
                        var userid = Convert.ToString(row["userid"]);
                        orderstus.OrderRefNo = Convert.ToString(row["refno"]);
                        output2 = JsonConvert.SerializeObject(orderstus);
                        //clsgen.ErrorLog(path, ("SUCCESS-1:" + output2));
                        
                       //http://pineapp.bisplindia.in
                        reponse = clsgen.PostJSON(output2, "http://localhost:55641/api/Home/GetOrderStatus");
                        jsonResponse = clsgen.GetResponse(reponse);
                        
                        //ApiPinCoderesponse Codeorder = new ApiPinCoderesponse();
                        //{
                        //    Codeorder.request = output2;
                        //    Codeorder.response = jsonResponse;
                        //    Codeorder.url = "http://localhost:55641/api/Home/GetOrderStatus";
                        //}
                        //var statusID = isms.SaveAPIRequest(Codeorder);

                        //  responceObject.CreateOrderResponceDetail = JsonConvert.DeserializeObject<OrderRoot>(jsonResponse);
                        responceObject = JsonConvert.DeserializeObject<ResponceDetail>(jsonResponse);
                        if(responceObject.CreateOrderResponceDetail.status=="COMPLETE")
                        {
                            var Ststus = responceObject.CreateOrderResponceDetail.status;
                            M_reqCardActivate card = new M_reqCardActivate();
                            card.OrderID = responceObject.CreateOrderResponceDetail.orderId;
                            card.offset = 0;
                            card.limit = 0;
                            output2 = JsonConvert.SerializeObject(card);
                            reponse = clsgen.PostJSON(output2, "http://localhost:55641/api/Home/ActivetedCardApi");
                            jsonResponse = clsgen.GetResponse(reponse);
                            //ApiPinCoderesponse Code = new ApiPinCoderesponse();
                            //{
                            //    Code.request = output2;
                            //    Code.response = jsonResponse;
                            //    Code.url = "http://localhost:55641/api/Home/ActivetedCardApi";
                            //}
                           // var statusID1 = isms.SaveAPIRequest(Code);
                             responceObject = JsonConvert.DeserializeObject<ResponceDetail>(jsonResponse);
                           // responceObject.CardActivated = JsonConvert.DeserializeObject<M_ResCardActivated>(jsonResponse);
                            List<order_card> cd = new List<order_card>();
                            order_card crd = new order_card();
                            if (responceObject.CardActivated != null)
                            {

                                 foreach (var data in responceObject.CardActivated.cards)
                                {
                                        crd.ActivationCode = Convert.ToString(data.activationCode);
                                        crd.ActivationUrl = Convert.ToString(data.activationUrl);
                                        crd.Amount = data.amount;
                                        crd.barcode = Convert.ToString(data.barcode);
                                        crd.CardId = data.cardId;
                                        crd.CardNumber = data.cardNumber;
                                        crd.EnrptCardNo = Encrypt(data.cardNumber, KeyByte, IVByte);
                                        crd.CardPin = data.cardPin;
                                        crd.EnrptCardPin = Encrypt(data.cardPin, KeyByte, IVByte);
                                    if (data.validity == null)
                                    {
                                        crd.Validity = "";
                                    }
                                    else
                                    {
                                        crd.Validity = data.validity.ToString();
                                    }
                                    crd.ProductSku = data.sku;
                                        crd.LabelProductName = data.productName; 
                                    if(data.labels != null)
                                    {
                                        crd.LableCardNumber = data.cardNumber;
                                        crd.LableCardPin = data.cardPin;
                                        crd.lableValidity = data.validity;
                                    }
                                    if(data.recipientDetails!=null)
                                    {
                                        crd.RecepientEmail = data.recipientDetails.email;
                                    }
                                    crd.Created = System.DateTime.Now;
                                    crd.OrderRefNo = orderstus.OrderRefNo;
                                    cd.Add(crd);

                                    DataSet ds1 = isms.SaveCrdDetail(crd, userid, Ststus);
                                  

                                }

                                string sms = string.Empty;
                                string productsku = string.Empty;
                                string cardNo = string.Empty;
                                string pinNo = string.Empty;
                                string Amount = string.Empty;
                                string validity = string.Empty;

                                DataSet Address = isms.getBillingAddress(userid, orderstus.OrderRefNo);
                                var mobile = Address.Tables[0].Rows[0]["Mobile"];
                                sms = " you have  just received your gift voucher.your code:";
                                foreach (var item in responceObject.CardActivated.cards)
                                {
                                    productsku = item.sku;
                                    cardNo = item.cardNumber;
                                    pinNo = item.cardPin; ;
                                    Amount = item.amount;
                                    validity = Convert.ToDateTime(item.validity).ToString("dd/MM/yyyy");
                                    sms += "voucher code" + cardNo + "Activition Pin: voucher pin" + cardNo + " of Rs." + Amount + "will Expire on " + validity + "Use the given detail for redeeming at BIgbazar uBRe";
                                }
                                string sms1 = sms;

                                var statusId = isms.SaveJobSMSQue(Convert.ToInt32(Address.Tables[0].Rows[0]["UserId"]), sms1, Convert.ToString(mobile), "giftVoucher", "1");

                                string msgbody = string.Empty;
                                DataSet user = isms.getuserDetail(userid);
                                string URL = System.Web.HttpContext.Current.Request.Url.Host.ToUpper().Replace("HTTP://", "").Replace("HTTPS://", "").Replace("WWW.", "").Replace("/", "").Replace("Utility.", "").Replace("Care.", "");// System.Web.HttpContext.Current.Request.UserHostName;
                                msgbody = "<div class='container'><h3> Hello " + user.Tables[0].Rows[0]["Firstname"] + "" + user.Tables[0].Rows[0]["Lastname"] + "(" + user.Tables[0].Rows[0]["Username"] + ")!!</h3>";
                                msgbody += "<p>You have just received your gift voucher worth Rs." + responceObject.CardActivated.cards.Sum(s => Convert.ToDecimal(s.amount)) + ".Please find the details of E-gift Voucher and use the given details for redemption at respective shop.</p></div>";
                                msgbody += "<div class='col-md-12'> <table cellpadding='5' cellspacing='1' border='2' style='font-family:Arial'><thead style='background-color:Aqua;text-transform:uppercase;font-weight:bold;font-size:13px'><tr align='center'><th>Amount</th><th>ForUse</th><th>VoucherCode</th><th>Pin</th><th>Expiry</th></tr></thead>";
                                msgbody += "<tbody style='font-size:12px'>";
                                foreach (var data in responceObject.CardActivated.cards)
                                {
                                    msgbody += "<tr align='center'><td>" + Convert.ToString(data.amount) + "</td>";
                                    msgbody += "<td>" + data.sku + "</td>";
                                    msgbody += "<td>" + data.cardNumber + "</td>";
                                    msgbody += "<td>" + data.cardPin + "</td>";
                                    msgbody += "<td>" + Convert.ToDateTime(data.validity).ToString("dd/MM/yyyy") + "</td>";
                                    msgbody += "</tr>";
                                }
                                msgbody += "</tbody></table></div></div>";

                                var status = isms.SaveJobEmailQue(Convert.ToInt32(Address.Tables[0].Rows[0]["UserId"]), msgbody, Convert.ToString(Address.Tables[0].Rows[0]["Email"]), "Gift Voucher", Convert.ToInt32(1), "PineShop more E-Gift Voucher Order no" + orderstus.OrderRefNo, "");


                            }

                        }
                         else if(responceObject.CreateOrderResponceDetail.status == "CANCELED")
                        {
                            var Ststus = responceObject.CreateOrderResponceDetail.status;
                            var OrderRefNo = responceObject.CreateOrderResponceDetail.refno;
                            DataSet dcancel = isms.CancelOrderStatus(OrderRefNo, Ststus);
                            reponse = clsgen.PostJSON(output2, "http://localhost:55641/api/Home/GetOrderStatus");
                            jsonResponse = clsgen.GetResponse(reponse);

                            ApiPinCoderesponse order = new ApiPinCoderesponse();
                            {
                                order.request = output2;
                                order.response = jsonResponse;
                                order.url = "http://localhost:55641/api/Home/GetOrderStatus";
                            }
                            var status = isms.SaveAPIRequest(order);
                        }


                        responceObject.Status = true;

                        
                        //clsgen.ErrorLog(path, ("SUCCESS-3:" + jsonResponse));
                      
                       
                    }
                }
                   
                //if (ds != null)
                //{
                //    dtgetOrderStatus= ds.Tables[0];
                //    int n = 0; int rcount = dtJobEmailQue.Rows.Count;
                //    while (n <= (int)(rcount / BUNCH_SIZE))
                //    {
                //        Task[] taskArrayJOBOrderStatusQue = new Task[n == (int)(rcount / BUNCH_SIZE) ? (rcount % BUNCH_SIZE) : BUNCH_SIZE];
                //        int taskArrayCounter = 0;
                //        for (int i = n * BUNCH_SIZE; i < rcount && i < (n + 1) * BUNCH_SIZE; i++)
                //        {
                //            taskArrayJOBOrderStatusQue[taskArrayCounter++] = Task.Factory.StartNew((Object obj) =>
                //            {
                //                int counter = (int)obj;


                //            }, i);
                //        }
                //        Task.WaitAll(taskArrayJOBOrderStatusQue);
                //        n++;
                //    }
                //}

            }
             catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
        }

        static string Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return Convert.ToBase64String(encrypted);
        }

        static string Decrypt(string data, byte[] Key, byte[] IV)
        {
            byte[] cipherText = Convert.FromBase64String(data);
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }


    }
}
