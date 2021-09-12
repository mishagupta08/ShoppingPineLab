using Newtonsoft.Json;
using PineLabsShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PineLabsShoppingPortal
{
    public class Repository
    {
        //private string ApiUrl = "http://pineapp.bisplindia.in/api/Home/";
        //private string ApiUrl = "http://papi.bisplindia.in/api/Home/";
        //private string ApiUrl = "http://localhost:55641/api/Home/";
        private string ApiUrl = "http://localhost:55708/api/Home/";

        private string CategoryAction = "ManageCategory";

        private string ProductAction = "ManageProducts";

        private string ManageCartAction = "ManageCart/";

        private string ManageOrderAction = "ManageOrder/";
        private string OrderReportDetail = "OrderReportDetail/";

        private string ManageUserAction = "ManageUser/";
        private string OrderReport = "OrderReport";
        private string FundRequestAction = "FundRequest";


        private string GetProductListByCategoryIdAction = "GetProductListByCategoryId";

        public async Task<ResponceDetail> ManageCategory(Filters filterdetail)
        {
            var detail = JsonConvert.SerializeObject(filterdetail);

            var result = await CallPostFunction(detail, CategoryAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> ManageCart(CartDetail cDetail, string operation)
        {
            var detail = JsonConvert.SerializeObject(cDetail);

            var result = await CallPostFunction(detail, ManageCartAction + operation);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
         public async Task<ResponceDetail>MyOrderReport(User user )
        {
            var detail = JsonConvert.SerializeObject(user);
            var result = await CallPostFunction(detail, "OrderReport");
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> MyOrderDetailReport(User user,string refno)
        {
            var detail = JsonConvert.SerializeObject(user);
            var result = await CallPostFunction(detail, OrderReportDetail + refno);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> ManageOrder(OrderContainer cDetail, string operation)
        {
            var detail = JsonConvert.SerializeObject(cDetail);

            var result = await CallPostFunction(detail, ManageOrderAction + operation);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> ManageUser(List<User> uDetail, string operation)
        {
            var detail = JsonConvert.SerializeObject(uDetail);

            var result = await CallPostFunction(detail, ManageUserAction + operation);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> ManageProducts(Filters filterdetail)
        {
            var detail = JsonConvert.SerializeObject(filterdetail);

            var result = await CallPostFunction(detail, ProductAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> ValidateTransaction(User objUser)
        {
            var detail = JsonConvert.SerializeObject(objUser);

            var result = await CallPostFunction(detail, "ValidateTransactionLogin");
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> DeductWallet(User ObjUser)
        {
            var detail = JsonConvert.SerializeObject(ObjUser);

            var result = await CallPostFunction(detail, "DeductWallet");
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> MangeOtpFunctions(User user, string operation)
        {
            var data = JsonConvert.SerializeObject(user);
            var result = await CallPostFunction(data, "MangeOtpFunctions/" + operation);
            return result;
        }



        public async Task<ResponceDetail> GetWallet(User ObjUser)
        {
            var detail = JsonConvert.SerializeObject(ObjUser);

            var result = await CallPostFunction(detail, "GetWalletDetails");
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<ResponceDetail> ConfirmTrasaction(User ObjUser)
        {
            var detail = JsonConvert.SerializeObject(ObjUser);

            var result = await CallPostFunction(detail, "VerifyTrasaction");
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public async Task<ResponceDetail> FundRequest(FundRequest objFundRequest)
        {
            var detail = JsonConvert.SerializeObject(objFundRequest);
            var result = await CallPostFunction(detail, FundRequestAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        #region commonfunction

        private async Task<ResponceDetail> CallPostFunction(string detail, string action)
        {
            using (var httpClient = new HttpClient())
            {
                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(detail, Encoding.UTF8, "application/json");

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(ApiUrl + action, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();


                    var result = JsonConvert.DeserializeObject<ResponceDetail>(responseContent);

                    return result;
                }
            }

            return null;
        }

        private async Task<ResponceDetail> CallGetFunction(string action)
        {
            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse = await httpClient.GetAsync(ApiUrl + action);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<ResponceDetail>(responseContent);

                    return result;
                }
            }

            return null;
        }

        #endregion commonfunction
    }
}