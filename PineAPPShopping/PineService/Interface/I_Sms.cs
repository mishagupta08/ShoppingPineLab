using PineService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineService.Interface
{
    interface I_Sms
    {
        DataSet GetSMSQue();
        int SaveSMS(string regXML);
        DataSet GetEmailQue();
        int SaveEMAIL(string regXML);
        DataSet GetOrderStatus();
        string SaveAPIRequest(ApiPinCoderesponse code);
        DataSet CancelOrderStatus(string orderRefNo, string Status);
        DataSet SaveCrdDetail(order_card crd, string userid, string Status);
        DataSet getBillingAddress(string userid, string OrderRefNo);
        int SaveJobSMSQue(int formNo, string smsBody, string mobileNo, string smsType, string companyId);
        DataSet getuserDetail(string userid);
        int SaveJobEmailQue(int formNo, string emailBody, string emailId, string emailType, int companyId, string subject, string fromEmailID);

    }
}
