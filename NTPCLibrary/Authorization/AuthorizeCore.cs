using System.Linq;
using System.Web;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using NTPCLibrary;

namespace NTPCLibrary
{
    /// <summary>
    /// 執行[Authorize]或[NtpcAuthorizeExtension]授權判斷
    /// <para>基本授權判斷：AuthorizeCore.IsAuthorized(this)</para>
    /// <para>延伸授權判斷：AuthorizeCore.IsExtensionAuthorized(this)</para>
    /// </summary>
    public class AuthorizeCore
    {
        public AuthorizeCore()
        {
        }
        /// <summary>
        /// OpenID基本授權判斷：OpenID登入者是否符合[Authorize]指定Users、Roles或Schools授權
        /// <para>優點：無耦合，限定的Users、Roles、Schools，只要[任一]符合即可登入</para>
        /// <para>缺點：要判斷使用者用[選取]哪一個角色登入，則無法判斷</para>
        /// </summary>
        /// <param name="page">目前的Page，加入this即可</param>
        public static bool IsAuthorized(object page)
        {
            OpenID openId = new OpenID();

            bool isAuth = true;
            string Users = string.Empty;
            string Roles = string.Empty;
            string Schools = string.Empty;

            //取得Users、Roles、Schools
            AuthorizeAttribute auth = null;
            var attrs = page.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true);

            foreach (var attribute in attrs)
            {
                if (attribute is AuthorizeAttribute)
                {
                    auth = attribute as AuthorizeAttribute;

                    Users = TrimAll(auth.Users);

                    Roles = TrimAll(auth.Roles);

                    Schools = TrimAll(auth.Schools);
                }
            }
                        
            //●1.基本OpenID驗證：只傳[Authorize]
            if (auth != null && Users == string.Empty && Roles == string.Empty)
            {
                if (!openId.IsAuthenticated)
                {
                    return false;
                }
            }

            //●2.Users驗證：傳[Authorize(Users:"使用者帳號")]
            if (Users != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] usersArray = Users.Split(',');

                    if (usersArray.Contains(openId.User.Identity))
                    {
                        return true;
                    }
                    else
                    {
                        isAuth = false;
                    }
                }
                else
                {
                    isAuth = false;
                }
            }

            //●3.Roles驗證：傳[Authorize(Roles:"角色名稱")]
            if (Roles != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] rolesArray = Roles.Split(',');

                    if (openId.User.Departments.Where(s => rolesArray.Intersect(s.Groups).Count() > 0).Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        isAuth = false;
                    }
                }
                else
                {
                    isAuth = false;
                }
            }

            //●4.Schools驗證：傳[Authorize(Schools:"學校代碼")]
            if (Schools != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] schoolsArray = Schools.Split(',');

                    if (openId.User.Departments.Where(s => schoolsArray.Contains(s.ID)).Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        isAuth = false;
                    }
                }
                else
                {
                    isAuth = false;
                }
            }

            //●5.若以上沒有任何一個權限被滿足，就是無訪問權限
            if (!isAuth)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// OpenID基本多學校職稱[選取]授權判斷：OpenID登入後，可選擇[其一]學校職稱，並只針對所選取得學校職稱做授權判斷
        /// 
        /// <para>優點：可依使用者登入時選取的單一角色群組來判斷</para>
        /// <para>缺點：從COOKIE來判斷[與LoginMultiView耦合]</para>
        /// </summary>
        /// <param name="page">目前的Page，加入this即可</param>
        public static bool IsMultiAuthorized(object page)
        {
            User SelectUser = Util.GetCookie<User>(Util.OPENID_SELECT_USER_COOKIE);
            OpenID openId = new OpenID();

            bool isAuth = true;
            string Users = string.Empty;
            string Roles = string.Empty;
            string Schools = string.Empty;

            //取得Users、Roles、Schools
            AuthorizeAttribute auth = null;
            var attrs = page.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true);

            foreach (var attribute in attrs)
            {
                if (attribute is AuthorizeAttribute)
                {
                    auth = attribute as AuthorizeAttribute;

                    Users = TrimAll(auth.Users);

                    Roles = TrimAll(auth.Roles);

                    Schools = TrimAll(auth.Schools);
                }
            }

            //●1.基本OpenID驗證：只傳[Authorize]
            if (auth != null && Users == string.Empty && Roles == string.Empty)
            {
                if (!openId.IsAuthenticated)
                {
                    return false;
                }
            }

            //●2.Users驗證：傳[Authorize(Users:"使用者帳號")]
            if (Users != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] usersArray = Users.Split(',');

                    if (usersArray.Contains(SelectUser.Identity))
                    {
                        return true;
                    }
                    else
                    {
                        isAuth = false;
                    }
                }
                else
                {
                    isAuth = false;
                }
            }

            //●3.Roles驗證：傳[Authorize(Roles:"角色名稱")]
            if (Roles != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] rolesArray = Roles.Split(',');

                    if (SelectUser.Departments.Where(s => rolesArray.Intersect(s.Groups).Count() > 0).Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        isAuth = false;
                    }
                }
                else
                {
                    isAuth = false;
                }
            }

            //●4.Schools驗證：傳[Authorize(Schools:"學校代碼")]
            if (Schools != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] schoolsArray = Schools.Split(',');

                    if (SelectUser.Departments.Where(s => schoolsArray.Contains(s.ID)).Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        isAuth = false;
                    }
                }
                else
                {
                    isAuth = false;
                }
            }

            //●5.若以上沒有任何一個權限被滿足，就是無訪問權限
            if (!isAuth)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 從COOKIE來判斷[與LoginUtil、LoginMultiView耦合]
        /// <para>額外擴充Roles，從COOKIE讀取</para>
        /// <para>優點：可依使用者登入時選取的單一角色群組來判斷</para>
        /// <para>缺點：需耦合資料庫</para>
        /// <para>備註：由於廠商非SSO登入，因此不從資料庫中的RoleUser讀取</para>
        /// </summary>
        /// <param name="page">目前的Page，加入this即可</param>
        //public static void IsExtensionAuthorized(object page)
        //{
        //    bool isAuth = true;
        //    string Roles = string.Empty;

        //    //取得Roles
        //    NtpcAuthorizeExtensionAttribute auth = null;
        //    var attrs = page.GetType().GetCustomAttributes(typeof(NtpcAuthorizeExtensionAttribute), true);

        //    foreach (var attribute in attrs)
        //    {
        //        if (attribute is NtpcAuthorizeExtensionAttribute)
        //        {
        //            auth = attribute as NtpcAuthorizeExtensionAttribute;

        //            Roles = TrimAll(auth.Roles);
        //        }
        //    }

        //    //Roles驗證：傳[NtpcAuthorizeExtension(Roles:"角色名稱")]
        //    if (Roles != string.Empty)
        //    {
        //        string[] rolesArray = Roles.Split(',');

        //        //1)自訂擴充角色(資料庫RoleUser)
        //        int role_id = LoginUtil.GetCookie(LoginUtil.NTPC_ROLE_COOKIE) != string.Empty ? Convert.ToInt16(LoginUtil.GetCookie(LoginUtil.NTPC_ROLE_COOKIE)) : 0;

        //        if (role_id != 0)
        //        {
        //            if (rolesArray.Contains(Global.角色名稱((Global.角色權限)role_id)))
        //            {
        //                return;// true;
        //            }
        //            else
        //            {
        //                isAuth = false;
        //            }
        //        }
        //        else
        //        {
        //            isAuth = false;
        //        }

        //        //2)SSO基本角色
        //        string group = LoginUtil.GetNtpcCookie(LoginUtil.NTPC_SCHOOL_GROUP_COOKIE);

        //        if (group != string.Empty)
        //        {
        //            if (rolesArray.Contains(group))
        //            {
        //                return;// true;
        //            }
        //            else
        //            {
        //                isAuth = false;
        //            }
        //        }
        //        else
        //        {
        //            isAuth = false;
        //        }

        //    }
        //    else
        //    {
        //        return;//true;
        //    }

        //    //若以上沒有任何一個權限被滿足，就是無訪問權限
        //    if (!isAuth)
        //    {
        //        //return false;
        //        HttpContext.Current.Response.Redirect("Error.aspx?msg=沒有擴充(Extension)角色訪問權限&rd=Default.aspx");
        //    }

        //}

        private static string TrimAll(string str)
        {
            if (str != null)
            {
                return Regex.Replace(str, @"\s", "");
            }
            else
            {
                return string.Empty;
            }
        }

        

    }
}
