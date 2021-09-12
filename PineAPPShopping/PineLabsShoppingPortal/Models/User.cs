﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PineLabsShoppingPortal.Models
{
    public partial class User
    {
        public decimal Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string WalletType { get; set; }
        public string TxnData { get; set; }
        public string OTPCode { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> ParentId { get; set; }
    }

    public partial class Distributor
    {
        public string formno { get; set; }
        public string loginid { get; set; }
        public string name { get; set; }
        public string doj { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string city { get; set; }
        public string isactive { get; set; }
        public int kitid { get; set; }
        public string kitname { get; set; }
        public string kitstatus { get; set; }
        public string status { get; set; }
        public double rwallet { get; set; }
        public double ewallet { get; set; }
        public string ismovie { get; set; }
        public string TxnData { get; set; }
        public DateTime activedate { get; set; }
        public double kitamount { get; set; }
        public string isholiday { get; set; }
        public int shoppoint { get; set; }
        public int promoid { get; set; }
        public int coupon { get; set; }
        public int promovalue { get; set; }
    }

    public partial class WalletResponse
    {
        public string loginid { get; set; }
        public string response { get; set; }
        public double deductamount { get; set; }
        public int voucherno { get; set; }
        public double walletBalance { get; set; }
        public string msg { get; set; }
        public string wallettype { get; set; }
    }
}