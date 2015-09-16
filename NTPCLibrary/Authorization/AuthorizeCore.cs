using System.Linq;
using System;
using System.Text.RegularExpressions;

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
        ///  OpenID擴充授權判斷：OpenID登入後，可額外擴充OpenID所沒有的角色。擴充角色是從資料庫RoleUser資料表讀取
        /// <para></para>
        /// <para>優點：可擴充OpenID沒有的Role角色權限。(額外擴充的Role角色要從COOKIE讀取)</para>
        /// <para>缺點：需耦合資料庫、Util.cs、LoginMultiViewExtension.aspx</para>
        /// </summary>
        /// <param name="page">目前的Page，加入this即可</param>
        public static bool IsExtensionAuthorized(object page)
        {
            OpenID openId = new OpenID();

            bool isAuth = true;
            string Roles = string.Empty;

            //取得Roles
            AuthorizeExtensionAttribute auth = null;
            var attrs = page.GetType().GetCustomAttributes(typeof(AuthorizeExtensionAttribute), true);

            foreach (var attribute in attrs)
            {
                if (attribute is AuthorizeExtensionAttribute)
                {
                    auth = attribute as AuthorizeExtensionAttribute;

                    Roles = TrimAll(auth.Roles);
                }
            }

            //Roles驗證：傳[AuthorizeExtension(Roles:"角色名稱")]
            if (Roles != string.Empty)
            {
                if (openId.IsAuthenticated)
                {
                    string[] rolesArray = Roles.Split(',');

                    //1)自訂擴充角色(資料庫RoleUser)
                    int role_id = Util.GetCookie(Util.OPENID_ROLE_COOKIE) != string.Empty ? Convert.ToInt16(Util.GetCookie(Util.OPENID_ROLE_COOKIE)) : 0;

                    if (role_id != 0)
                    {
                        if (rolesArray.Contains(Util.角色名稱((Util.角色權限)role_id)))
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
                else
                {
                    isAuth = false;
                }
            }
            else
            {
                return true;
            }

            //若以上沒有任何一個權限被滿足，就是無訪問權限
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
        /// OpenID基本授權 + OpenID擴充授權
        /// </summary>
        /// <param name="page">目前的Page，加入this即可</param>
        /// <returns></returns>
        public static bool IsMultiExtensionAuthorized(object page)
        {
            bool isAuth = true;

            //1)OpenID基本多學校職稱[選取]授權判斷
            bool IsOpenidSelectUser = Util.GetCookie<User>(Util.OPENID_SELECT_USER_COOKIE) != default(User);
            if (IsOpenidSelectUser)
            {
                bool IsMulti = IsMultiAuthorized(page);
                if (IsMulti)
                {
                    return true;
                }
                else
                {
                    isAuth = false;
                }
            }

            //2)擴充權限
            bool IsOpenIdRole = !((string.IsNullOrEmpty(Util.GetCookie(Util.OPENID_ROLE_COOKIE))) || (Util.GetCookie(Util.OPENID_ROLE_COOKIE) == "0"));
            if (IsOpenIdRole)
            {
                bool IsExtension = IsExtensionAuthorized(page);
                if (IsExtension)
                {
                    return true;
                }
                else
                {
                    isAuth = false;
                }
            }

            //若以上沒有任何一個權限被滿足，就是無訪問權限
            if (!isAuth)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
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
