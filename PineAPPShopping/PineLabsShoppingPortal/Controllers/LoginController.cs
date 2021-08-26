using Newtonsoft.Json;
using PineLabsShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PineLabsShoppingPortal.Controllers
{
    public class LoginController : Controller
    {
        int CompanyId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyId"]);
        int RoleId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RoleId"]);

        Repository repository;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ValidateLogin(User userDetail)
        {
            try
            {
                userDetail.CompanyId = CompanyId;
                userDetail.RoleId = RoleId;
                this.repository = new Repository();
                var uList = new List<User>();
                uList.Add(userDetail);

                var result = await this.repository.ManageUser(uList, "Login");
                Session["UserDetail"] = null;
                if (result != null)
                {
                    if (result.Status == true)
                    {
                        User user = result.UserDetail;
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        Session["UserDetail"] = user;
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(result.Message, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Login Failed", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SaveDetail(User userModel)
        {
            if (userModel == null)
            {
                return Json("Please Fill complete detail.");
            }

            this.repository = new Repository();
            userModel.CompanyId = CompanyId;
            userModel.RoleId = RoleId;

            var uList = new List<User>();
            uList.Add(userModel);

            var res = await this.repository.ManageUser(uList, "Add");

            return Json(res.Message);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserDetail"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}