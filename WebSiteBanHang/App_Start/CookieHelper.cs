using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.App_Start
{
    public static class CookieHelper
    {
        public static void Create(string name, string value, DateTime expire)
        {
            // Create a cookie object, khai báo 1 cookie
            HttpCookie cookie = new HttpCookie(name);
            // Set the cookie's value, set giá trị cho cookie
            cookie.Value = value;
            // Set the cookie's expiration date, set ngày hết hạn cookie
            cookie.Expires  = expire;
            // Add the cookie to the cookie collection
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string Get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if(cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                return "";
            }
        }

        public static void Remove(string name)
        {
            //create a cookie object
            HttpCookie cookie = new HttpCookie(name);
            //set the cookie's value
            cookie.Value = "";
            //add the cookie to the cookie collection
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}