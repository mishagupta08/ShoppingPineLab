using PineLabsShoppingPortal.Models;
using PineLabsShoppingPortal.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using PineLabsShoppingPortal;

namespace PineLabsShoppingPortal.Controllers
{
    public class HomeController : Controller
    {
        private const int RECORDCOUNT = 12;
        HomeViewModel homeModel;
        Repository repository;
        // GET: Home
        public async Task<ActionResult> Index()
        {
            this.homeModel = new HomeViewModel();
            this.homeModel.CategoryList = await GetMenuList();
            this.homeModel.ProductList = await GetLatestProductList();

            return View(this.homeModel);
        }

        public async Task<ActionResult> GetProductDetailPage(string Sku)
        {
            this.homeModel = new HomeViewModel();
            this.homeModel.cartDeatil = new CartDetail();
            this.homeModel.ProductDetail = await GetProductDetail(Sku);
            if (this.homeModel.ProductDetail != null)
            {
                if (this.homeModel.ProductDetail.ProductDetail != null && this.homeModel.ProductDetail.ProductDetail.PriceType == Resources.PriceTypeRange)
                {
                    this.homeModel.cartDeatil.ProdPrice = this.homeModel.ProductDetail.ProductDetail.Min ?? 0;
                }
            }

            this.homeModel.cartDeatil.Quantity = 1;

            return View("_ProductDetailPage", this.homeModel);
        }

        public async Task<ActionResult> LoadMenus()
        {
            this.homeModel = new HomeViewModel();
            this.homeModel.CategoryList = await GetMenuList();

            return PartialView("~/Views/Shared/_menuContainer.cshtml", this.homeModel);
        }

        public async Task<ActionResult> GetCartCount()
        {
            var res = new ResponceDetail();
            if (CheckLoginUserStatus())
            {
                this.repository = new Repository();
                var cartDeatil = new CartDetail();
                cartDeatil.UserId = (Session["UserDetail"] as User).Id;
                res = await this.repository.ManageCart(cartDeatil, "CartCount");
            }

            return Json(res.CartProductCount, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddToCart(HomeViewModel cartModel)
        {
            var res = new ResponceDetail();
            var messge = string.Empty;
            if (CheckLoginUserStatus())
            {
                this.repository = new Repository();
                cartModel.cartDeatil.UserId = (Session["UserDetail"] as User).Id;
                cartModel.cartDeatil.ProductSku = cartModel.ProductDetail.ProductDetail.Sku;
                cartModel.cartDeatil.PriceType = cartModel.ProductDetail.ProductDetail.PriceType;

                //if (cartModel.ProductDetail.ProductDetail.PriceType == Resources.PriceTypeSlab)
                //{
                //    var cart = new CartDetail();
                //    var selectedDenom = cartModel.ProductDetail.ProductDenominationList.Where(p => p.IsSelected == true);
                //    if (cartModel.ProductDetail.ProductDenominationList.Count > 0)
                //    {
                //        foreach (var denom in selectedDenom)
                //        {
                //            cart = cartModel.cartDeatil;
                //            cart.Quantity = cartModel.cartDeatil.Quantity;
                //            cart.ProdPrice = Convert.ToDecimal(denom.Denomination);
                //            res = await this.repository.ManageCart(cart, "AddToCart");
                //            if (res.Status == false)
                //            {
                //                Console.WriteLine(res.Message);
                //            }
                //        }
                //    }
                //}
                //else
                //{
                res = await this.repository.ManageCart(cartModel.cartDeatil, "AddToCart");

                //}
                //}
            }

            return Json(res);

        }


        public async Task<ActionResult> DeleteCart(decimal cartId)
        {
            var res = new ResponceDetail();
            if (CheckLoginUserStatus())
            {
                this.repository = new Repository();
                var cartModel = new CartDetail { Id = cartId };

                res = await this.repository.ManageCart(cartModel, "DeleteCart");
                if (res == null)
                {
                    return Json("Something went wrong. Please try again later.");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return Json(res.Message);
        }

        public async Task<ActionResult> SaveBillingAddressDetail(HomeViewModel model)
        {
            var res = new ResponceDetail();

            if (CheckLoginUserStatus())
            {
                this.repository = new Repository();
                var odrcontainer = new OrderContainer();
                odrcontainer.billingAddress = model.billingDetail;
                odrcontainer.UserId = (Session["UserDetail"] as User).Id;
                res = await this.repository.ManageOrder(odrcontainer, "SaveBillingAddress");
            }
            else
            {
                res.Message = "Login";
            }

            return Json(res);
        }
        

        public async Task<ActionResult> CheckwalletLogin(HomeViewModel model)
        {
            var res = new ResponceDetail();

            if (CheckLoginUserStatus())
            {
                this.repository = new Repository();
                var odrcontainer = new OrderContainer();
                odrcontainer.billingAddress = model.billingDetail;
                odrcontainer.UserId = (Session["UserDetail"] as User).Id;
                res = await this.repository.ManageOrder(odrcontainer, "SaveBillingAddress");
            }
            else
            {
                res.Message = "Login";
            }

            return Json(res);
        }

        public async Task<ActionResult> GetCartRelatedListView(string pageName,string orderId)
        {
            if (CheckLoginUserStatus())
            {
                await GetCartProductList();

                if (pageName == Resources.ShoppingCartDetailPage)
                {
                    if (this.homeModel != null)
                    {
                        this.homeModel.AssignQty();
                    }

                    return View("ShoppingCartDetailPage", this.homeModel);
                }
                else if (pageName == Resources.CartDetailWithPayment)
                {
                    this.homeModel.OrderId = orderId;
                    return View("cartDetailWithPayment", this.homeModel);
                }
                else if (pageName == Resources.CheckoutPage)
                {

                    return View("CheckoutPage", this.homeModel);
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return null;
        }

        public async Task<ActionResult> GetHeaderCartList(string pageName)
        {

            await GetCartProductList();
            return PartialView("~/Views/Shared/cartMobile.cshtml", this.homeModel);

        }

        public async Task<ActionResult> CreateOrder(string pageName,string orderId)
        {
            try
            {

                if (CheckLoginUserStatus())
                {
                    var ord = new OrderContainer();
                    ord.UserId = (Session["UserDetail"] as User).Id;
                    ord.orderId = orderId;
                    this.repository = new Repository();
                    var res = await this.repository.ManageOrder(ord, "CreateOrder");

                    return View("thankYouPage", res);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                //clsgen.ErrorLog(Server.MapPath("Logs/ErrorLog"), ("FlightPayment.aspx.cs/Page_Load" + ex.Message));
            }

            return null;
        }

        private async Task GetCartProductList()
        {
            if (CheckLoginUserStatus())
            {
                this.repository = new Repository();
                var cart = new CartDetail();
                cart.UserId = (Session["UserDetail"] as User).Id;
                var res = await this.repository.ManageCart(cart, "ListCartByUserId");
                if (res != null)
                {
                    this.homeModel = new HomeViewModel();
                    this.homeModel.CartContainer = new CartDetailcontainer();
                    this.homeModel.CartContainer.cartProductList = res.CartList;
                    if (res.CartList != null && res.CartList.Count > 0)
                    {
                        this.homeModel.CartContainer.TotalAmountToPay = res.CartList.Sum(p => p.TotalPrice) ?? 0;
                    }
                }
            }
        }

        private async Task<List<Category>> GetMenuList()
        {
            var catList = new List<Category>();
            this.repository = new Repository();
            var filter = new Filters();
            if (Session["MenuList"] == null)
            {
                try
                {
                    filter.Operation = "AllCategory";
                    var res = await this.repository.ManageCategory(filter);
                    if (res != null && res.Status && res.CatList != null && res.CatList.Count > 0)
                    {
                        catList = getNestedChildren(res.CatList.Where(r => r.ParentId == r.ApiCatId).ToList(), res.CatList);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (this.homeModel != null && this.homeModel.CategoryList != null && this.homeModel.CategoryList.Count > 0)
                {
                    Session["MenuList"] = catList;
                }
            }
            else
            {
                catList = Session["MenuList"] as List<Category>;
            }

            return catList;
        }

        public async Task<ActionResult> GetProductListView(int page, int catId)

        {
            var prodList = new List<Product>();
            this.repository = new Repository();
            this.homeModel = new HomeViewModel();
            var filter = new Filters();

            try
            {
                filter.Operation = Resources.ProductListByFilter;
                if (page == 0)
                {
                    filter.pageIndex = 1;
                }

                filter.RecordCount = RECORDCOUNT;
                filter.Id = catId;
                filter.Action = Resources.categoryId;
                var res = await this.repository.ManageProducts(filter);
                this.homeModel.PageProductList = new PagewiseProducts();
                if (res != null && res.Status && res.ResultProdList != null && res.ResultProdList.Count > 0)
                {
                    this.homeModel.PageProductList.ProductList = res.ResultProdList.ToList();
                    this.homeModel.TotalRecordCount = res.TotalRecordCount;

                    var list = new List<int>();
                    for (var i = 1; i <= this.homeModel.TotalRecordCount; i++)
                    {
                        list.Add(i);
                    }

                    this.homeModel.PageProductList.pagerCount = list.ToPagedList(Convert.ToInt32(page), 10);
                    ViewBag.Page = page;
                    ViewBag.CatId = catId;
                }

                this.homeModel.CategoryList = await GetMenuList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View("productListView", this.homeModel);
        }

        private async Task<List<Product>> GetLatestProductList()
        {
            var prodList = new List<Product>();
            this.repository = new Repository();
            var filter = new Filters();

            try
            {
                filter.Operation = Resources.ProductList;
                filter.pageIndex = 1;
                filter.RecordCount = 12;
                var res = await this.repository.ManageProducts(filter);
                if (res != null && res.Status && res.ResultProdList != null && res.ResultProdList.Count > 0)
                {
                    prodList = res.ResultProdList.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return prodList;
        }

        private async Task<ProductDetailContainer> GetProductDetail(string Sku)
        {
            this.repository = new Repository();
            var filter = new Filters();
            var prodContainer = new ProductDetailContainer();

            try
            {
                filter.Sku = Sku;
                filter.Operation = Resources.ProductDetailBySku;
                var res = await this.repository.ManageProducts(filter);
                if (res != null && res.Status && res.ProdDetailContainer != null)
                {
                    prodContainer = res.ProdDetailContainer;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return prodContainer;
        }

        public List<Category> getNestedChildren(List<Category> ParentList, List<Category> MenuList)
        {
            var orderedList = new List<Category>();
            if (ParentList.Count > 0)
            {
                foreach (var parent in ParentList)
                {
                    if (parent.SubcategoriesCount > 0)
                    {
                        var subMenuList = MenuList.Where(p => p.ParentId != p.ApiCatId && p.ParentId == parent.ApiCatId).ToList();
                        if (subMenuList != null && subMenuList.Count > 0)
                        {
                            parent.Children = (getNestedChildren(subMenuList, MenuList));
                        }
                    }
                    orderedList.Add(parent);
                }

            }
            return orderedList;
        }

        private bool CheckLoginUserStatus()
        {
            if (Session["UserDetail"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
         

        public  async Task<ActionResult> MyOredrsReport()
        {
            this.repository = new Repository();
            // var userid = Convert.ToString((Session["UserDetail"] as User).Id);
            var user = Session["UserDetail"] as User;
            var res = new ResponceDetail();
            List<MyOrderDetail> ORDERDETAIL = new List<MyOrderDetail>();
            res = await this.repository.MyOrderReport(user);
            List<MyOrderReport> obj = new List<MyOrderReport>();
            HomeViewModel home = new HomeViewModel();
            home.OrderReport = res.OrderReport;
            home.OrderDetail = ORDERDETAIL;
            return View(home);
        }

        public async Task<ActionResult> SrchOrderDetail(string OrderRefNo)
        {
            this.repository = new Repository();
            var user = Session["UserDetail"] as User;
            var res = new ResponceDetail();
            res = await this.repository.MyOrderDetailReport(user, OrderRefNo);
            List<MyOrderDetail> obj = new List<MyOrderDetail>();
            HomeViewModel home = new HomeViewModel();
            home.OrderDetail = res.OrderDetail;
            //return PartialView("_PartialGetOrderNoDetail", home);
            return Json(home, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> OrderPartial(string OrderRefNo)
        {
            this.repository = new Repository();
            var user = Session["UserDetail"] as User;
            var res = new ResponceDetail();
            res = await this.repository.MyOrderDetailReport(user, OrderRefNo);
            List<MyOrderDetail> model = new List<MyOrderDetail>();
            model = res.OrderDetail;
            return PartialView("_OrderDetail", model);
        }
        public async Task<ActionResult> ValidateTransaction(User objUser)
        {
            this.repository = new Repository();
            var user = Session["UserDetail"] as User;
            var res = new ResponceDetail();
            res = await this.repository.GetWallet(objUser);
            WalletResponse obj = new WalletResponse();            
            var WalletDetail = res.WalletResponse;            
            return Json(WalletDetail, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetWallet(User objUser)
        {
            this.repository = new Repository();
            //var user = Session["UserDetail"] as User;
            var res = new ResponceDetail();
            res = await this.repository.GetWallet(objUser);
            WalletResponse obj = new WalletResponse();
            HomeViewModel home = new HomeViewModel();
            home.OrderDetail = res.OrderDetail;
            //return PartialView("_PartialGetOrderNoDetail", home);
            return Json(home, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GenerateOtpDetail()
        {
           
            var result = new ResponceDetail();
            var message = string.Empty;
            this.repository = new Repository();
            try
            {
                if (CheckLoginUserStatus())
                {
                    var user = Session["UserDetail"] as User;
                    result = await this.repository.MangeOtpFunctions(user, "GenerateOtp");
                    if (result == null)
                    {
                        message = "Something went wrong.Please try again later.";
                    }
                    else
                    {
                        message = "Otp Generated Successfully";
                    }
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(message);
        }


        public async Task<ActionResult> ValidateOtp(User detailModel)
        {
            
            var objRepository = new Repository();
            var result = new ResponceDetail ();

            detailModel.Username = "GW223344";
            detailModel.Password = "123456";
            detailModel.OTPCode = detailModel.OTPCode;
            result = await objRepository.MangeOtpFunctions(detailModel, "ValidateOtp");
            if (result == null)
            {
                return Json("Failed");
            }
            else if (result.Status == false)
            {
                return Json(result.Message);
            }
            else
            {
                detailModel.TxnData = "123;10;Test";
                result = await objRepository.DeductWallet(detailModel);
            }
            return Json("Success");
            
        }
    }
}