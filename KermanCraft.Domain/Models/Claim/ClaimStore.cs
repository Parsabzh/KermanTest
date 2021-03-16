using System.Collections.Generic;

namespace KermanCraft.Domain.Models.Claim
{
    public static class ClaimStore
    {
        public static List<System.Security.Claims.Claim> AllClaims=new List<System.Security.Claims.Claim>()
        {
            new System.Security.Claims.Claim("Add Account","ایجاد کاربر"),
            new System.Security.Claims.Claim("Edit Account","ویرایش کاربر"),
            new System.Security.Claims.Claim("Delete Account","حذف کاربر"),
            new System.Security.Claims.Claim("Accounts List","لیست کاربر"),
            new System.Security.Claims.Claim("Claims List","لیست دسترسی ها"),
            new System.Security.Claims.Claim("Get Claim","دسترسی"),
            new System.Security.Claims.Claim("Update Claim","ویرایش دسترسی"),
            new System.Security.Claims.Claim("Create Customer","ایجاد مشتری"),
            new System.Security.Claims.Claim("Customers List","ویرایش مشتری"),
            new System.Security.Claims.Claim("Delete Customer","حذف مشتری"),
            new System.Security.Claims.Claim("List Customer","لیست مشتری"),
            new System.Security.Claims.Claim("Create CafeUser","ایجاد کاربر کافی شاپ"),
            new System.Security.Claims.Claim("Edit CafeUser","ویرایش کاربر کافی شاپ "),
            new System.Security.Claims.Claim("Delete CafeUser","حذف کاربر کافی شاپ"),
            new System.Security.Claims.Claim("CafeUsers List","لیست کاربر کافی شاپ"),
            new System.Security.Claims.Claim("Create Cafe","ایجاد کافی شاپ"),
            new System.Security.Claims.Claim("Edit Cafe","ویرایش کافی شاپ"),
            new System.Security.Claims.Claim("Delete Cafe","حذف کافی شاپ"),
            new System.Security.Claims.Claim("Cafes List","لیست کافی شاپ"),
            new System.Security.Claims.Claim("Create Menu","ایجاد منو"),
            new System.Security.Claims.Claim("Edit Menu","ویرایش منو"),
            new System.Security.Claims.Claim("Delete Menu","حذف منو"),
            new System.Security.Claims.Claim("Cafes Menu","لیست منو"),
            new System.Security.Claims.Claim("Order","دسترسی به سفارشات"),
           // new System.Security.Claims.Claim("OrderDetail","دسترسی به جزییات سفارشات"),
        };

    }
}
