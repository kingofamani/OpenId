using System;
using System.Collections.Generic;
using System.Linq;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Web;
using Newtonsoft.Json;

namespace NTPCLibrary
{
    public class OpenID
    {
        public const string OPENID_COOKIE = "openid";

        private IAuthenticationResponse response;
        /// <summary>
        /// 預設連接新北市OpenId Url
        /// <para>用法：NTPCLibrary.OpenID openID = new NTPCLibrary.OpenID();</para>
        /// </summary>
        public OpenID()
        {
            Url = "http://openid.ntpc.edu.tw";
            
            //IsAuthenticated = GetCookie(OPENID_COOKIE) != string.Empty;

            if (GetCookie(OPENID_COOKIE) != string.Empty)
            {
                User = JsonConvert.DeserializeObject<User>(GetCookie(OPENID_COOKIE));
            }            
        }
        /// <summary>
        /// 自訂其他縣市OpenId Url
        /// <para>用法：</para>
        /// <para>string url = System.Configuration.ConfigurationManager.AppSettings["openIdUrl"];</para>
        /// <para>NTPCLibrary.OpenID openID = new NTPCLibrary.OpenID(url);</para>
        /// </summary>
        /// <param name="url">指定縣市Open ID Url</param>
        public OpenID(string url)
        {
            Url = url;

            //IsAuthenticated = GetCookie(OPENID_COOKIE) != string.Empty;

            if (GetCookie(OPENID_COOKIE) != string.Empty)
            {
                User = JsonConvert.DeserializeObject<User>(GetCookie(OPENID_COOKIE));
            }  
        }

        public string Url { get; set; }
        public string CookieDomain
        {
            get { throw new NotImplementedException(); }
        }

        public string CookieName
        {
            get { throw new NotImplementedException(); }
        }

        public string CookiePath
        {
            get { throw new NotImplementedException(); }
        }

        public string CookieValue
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { return GetCookie(OPENID_COOKIE) != string.Empty; }
        }

        public string LoginUrl
        {
            get { throw new NotImplementedException(); }
        }

        public string LogoutUrl
        {
            get { throw new NotImplementedException(); }
        }

        public string ReturnUrl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime TimeStamp
        {
            get { throw new NotImplementedException(); }
        }

        public User User
        { get; set; }

        public void Login()
        {
            response = new OpenIdRelyingParty().GetResponse();

            if (response == null)
            {
                var request = new OpenIdRelyingParty().CreateRequest(Url);

                request.AddExtension
                (
                    new ClaimsRequest()
                    {
                        Email = DemandLevel.Require,
                        Language = DemandLevel.Require,
                        TimeZone = DemandLevel.Require,
                        Country = DemandLevel.Require,
                        BirthDate = DemandLevel.Require,
                        PostalCode = DemandLevel.Require,
                        Gender = DemandLevel.Require,
                        Nickname = DemandLevel.Require,
                        FullName = DemandLevel.Require
                    }
                );
                request.RedirectToProvider();

            } else if (response != null && response.Status == AuthenticationStatus.Authenticated)
            {                
                //基本
                var claimResponse = response.GetExtension<ClaimsResponse>();

                User = new NTPCLibrary.User();
                User.Identity = response.ClaimedIdentifier.ToString().Split('/').Last();
                User.ID = claimResponse.PostalCode;
                User.FullName = claimResponse.FullName;
                User.NickName = claimResponse.Nickname;
                User.Email = claimResponse.Email;
                User.Gender = claimResponse!=null?(claimResponse.Gender.Equals(Gender.Male) ? "男" : "女"):string.Empty;
                User.BirthDate = claimResponse.BirthDate;
                User.SchoolName = claimResponse.Country;
                User.ClassRoom = claimResponse.Language;
                User.Departments = JsonConvert.DeserializeObject<IEnumerable<Department>>(claimResponse.TimeZone);

                //延伸
                FetchResponse fetchResponse = response.GetExtension<FetchResponse>();

                User.AXExtension = fetchResponse != null ? GetAx(fetchResponse) : "無";

                //寫入Cookie
                SetCookie(OPENID_COOKIE, JsonConvert.SerializeObject(User));

            }

        }

        public void Logout()
        {
            CleanCookie(OPENID_COOKIE);
        }

        private string GetAx(DotNetOpenAuth.OpenId.Extensions.AttributeExchange.FetchResponse fetchrespone)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Dictionary<string, string> userdata = new Dictionary<string, string>();
            foreach (var attribs in fetchrespone.Attributes)
            {
                sb.Append(attribs.TypeUri.Split('/').ToList().Last() + "..." + attribs.Values[0].ToString() + "...");
            }
            return sb.ToString();
        }

        private void CleanCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private void SetCookie(string cookieName, string value)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                return HttpUtility.UrlDecode(cookie.Value);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
