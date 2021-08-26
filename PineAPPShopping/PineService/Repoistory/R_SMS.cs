using PineService.Interface;
using PineService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineService.Repoistory
{
    class R_SMS:I_Sms
    {
        string LiveCONNECTION_STRING = ConfigurationManager.ConnectionStrings["liveconstr"].ConnectionString;
        string CONNECTION_STRING = "";
        public DataSet GetSMSQue()
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            //Lets get the list of all employees in a datataable
         
                CONNECTION_STRING = LiveCONNECTION_STRING;
           
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "GetSMSQue"))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;
        }

        public int SaveSMS(string regXML)
        {
            int smsStatus = 0;
            SqlParameter[] parameters = new SqlParameter[]
          {
                new SqlParameter("@regXML", regXML)
          };
            //Lets get the list of all employees in a datataable
            using (DataSet ds = SqlHelper.ExecuteDataset(LiveCONNECTION_STRING, "sp_BulkSMSDataInsert", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    smsStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["SMSStatus"]);
                }
            }
            return smsStatus;
        }

        public DataSet GetEmailQue()
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();


            //Lets get the list of all employees in a datataable
            
                CONNECTION_STRING = LiveCONNECTION_STRING;
            
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "GetEmailQue"))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;
        }


        public int SaveEMAIL(string regXML)
        {
            int EMAILStatus = 0;
            SqlParameter[] parameters = new SqlParameter[]
          {
                new SqlParameter("@regXML", regXML)
          };
            //Lets get the list of all employees in a datataable
            
                CONNECTION_STRING = LiveCONNECTION_STRING;
            
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "sp_BulkEMAILDataInsert", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    EMAILStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["EMAILStatus"]);
                }
            }
            return EMAILStatus;
        }

        public DataSet GetOrderStatus()
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            CONNECTION_STRING = LiveCONNECTION_STRING;
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "sp_getOrderStatus"))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;


        }

        public string SaveAPIRequest(ApiPinCoderesponse code)
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            CONNECTION_STRING = LiveCONNECTION_STRING;
            SqlParameter[] parameters = new SqlParameter[]
        {
                new SqlParameter("@request", code.request),
                  new SqlParameter("@response", code.response),
                     new SqlParameter("@url", code.url)

        };
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "sp_SaveAPIRequest", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn.Tables[0].Rows[0][0].ToString();
        }

        public DataSet CancelOrderStatus(string orderRefNo, string Status)
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            CONNECTION_STRING = LiveCONNECTION_STRING;
            SqlParameter[] parameters = new SqlParameter[]
        {
                new SqlParameter("@Action", "CancelOrderStatus"),
                  new SqlParameter("@Refno", orderRefNo),
                  new SqlParameter("@activationCode", ""),
                  new SqlParameter("@ActivationUrl", ""),
                  new SqlParameter("@Amount", ""),
                  new SqlParameter("@barcode", ""),
                  new SqlParameter("@cardid", ""),
                  new SqlParameter("@CardNumber", ""),
                  new SqlParameter("@EnrptCardNo", ""),
                  new SqlParameter("@PinNo", ""),
                   new SqlParameter("@EnrptCardPin", ""),
                   new SqlParameter("@LabelProductName", ""),
                 new SqlParameter("@LableCardNumber", ""),
                   new SqlParameter("@LableCardPin", ""),
                     new SqlParameter("@lableValidity", ""),
                       new SqlParameter("@ProductSku", ""),
                         new SqlParameter("@RecepientEmail", ""),
                           new SqlParameter("@ProductTheme", ""),
                           new SqlParameter("@Validity", ""),
                             new SqlParameter("@Status", Status),
               new SqlParameter("@userId", ""),
        };
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "Sp_UpdateOrderStatus", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;

        }

        public DataSet SaveCrdDetail(order_card crd, string userid, string Status)
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            CONNECTION_STRING = LiveCONNECTION_STRING;
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "CompleteOrderStatus"),
             new SqlParameter("@Refno", crd.OrderRefNo),
             new SqlParameter("@activationCode", crd.ActivationCode),
             new SqlParameter("@ActivationUrl", crd.ActivationUrl),
             new SqlParameter("@Amount", crd.Amount),
             new SqlParameter("@barcode", crd.barcode),
             new SqlParameter("@cardid", crd.CardId),
             new SqlParameter("@CardNumber",crd.CardNumber),
             new SqlParameter("@EnrptCardNo", crd.EnrptCardNo),
             new SqlParameter("@PinNo", crd.CardPin),
             new SqlParameter("@EnrptCardPin", crd.EnrptCardNo),
             new SqlParameter("@LabelProductName", crd.LabelProductName),
             new SqlParameter("@LableCardNumber", crd.LableCardNumber),
             new SqlParameter("@LableCardPin", crd.LableCardPin),
             new SqlParameter("@lableValidity", crd.lableValidity),
             new SqlParameter("@ProductSku", crd.ProductSku),
             new SqlParameter("@RecepientEmail", crd.RecepientEmail),
             new SqlParameter("@ProductTheme", crd.ProductTheme),
             new SqlParameter("@Validity", crd.Validity),
               new SqlParameter("@Status", Status),
               new SqlParameter("@userId", userid),
            };
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "Sp_UpdateOrderStatus", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;
        }

        public DataSet getBillingAddress(string userid, string OrderRefNo)
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            CONNECTION_STRING = LiveCONNECTION_STRING;
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@Userid", userid),
                  new SqlParameter("@refno", OrderRefNo),
            };

            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "getBillAddress", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;
        }

        public int SaveJobSMSQue(int formNo, string smsBody, string mobileNo, string smsType, string companyId)
        {
            int SMSSTATUS = 0;
            SqlParameter[] parameters = new SqlParameter[]
          {
                new SqlParameter("@formNo", formNo),
                new SqlParameter("@smsBody", smsBody),
                new SqlParameter("@mobileNo", mobileNo),
                new SqlParameter("@smsType", smsType),
                new SqlParameter("@companyId", companyId)
          };
            //Lets get the list of all employees in a datataable

            string CONNECTION_STRING = LiveCONNECTION_STRING;

            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "sp_JobSMSQue", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SMSSTATUS = Convert.ToInt32(ds.Tables[0].Rows[0]["SMSSTATUS"]);
                }
            }
            return SMSSTATUS;
        }

        public DataSet getuserDetail(string userid)
        {
            DataSet dsReturn = null;
            dsReturn = new DataSet();
            CONNECTION_STRING = LiveCONNECTION_STRING;
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@Userid", userid),
                  
            };
            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "getUserDetail", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dsReturn = ds;
                }
            }
            return dsReturn;
        }

        public int SaveJobEmailQue(int formNo, string emailBody, string emailId, string emailType, int companyId, string subject, string fromEmailID)
        {
            int SMSSTATUS = 0;
            SqlParameter[] parameters = new SqlParameter[]
          {
                new SqlParameter("@formNo", formNo),
                new SqlParameter("@EMailBody", emailBody),
                new SqlParameter("@EmailId", emailId),
                new SqlParameter("@EmailType", emailType),
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@Subject", subject),
                new SqlParameter("@FromEmailID", fromEmailID)
          };
            //Lets get the list of all employees in a datataable

            string CONNECTION_STRING = LiveCONNECTION_STRING;

            using (DataSet ds = SqlHelper.ExecuteDataset(CONNECTION_STRING, "sp_JobEmailQue", parameters))
            {
                //check if any record exist or not
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SMSSTATUS = Convert.ToInt32(ds.Tables[0].Rows[0]["EMAILSTATUS"]);
                }
            }
            return SMSSTATUS;
        }

    }
}
