using System;
using System.Collections.Generic;
using System.Linq;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Web;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
            CookieDomain = UrlToDomain(Url);            

            IsAuthenticated = Util.GetCookie(OPENID_COOKIE) != string.Empty;

            if (response != null && response.Status == AuthenticationStatus.Authenticated)
            {
                IsAuthenticated = true;
            }

            if (Util.GetCookie(OPENID_COOKIE) != string.Empty)
            {
                User = Util.GetCookie<User>(OPENID_COOKIE);
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
            CookieDomain = UrlToDomain(Url); 

            IsAuthenticated = Util.GetCookie(OPENID_COOKIE) != string.Empty;

            if (Util.GetCookie(OPENID_COOKIE) != string.Empty)
            {
                User = Util.GetCookie<User>(OPENID_COOKIE);
            }  
        }

        public string Url { get; set; }
        public string CookieDomain{ get; set; }

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
            get;
            set;
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
                IsAuthenticated = true;
                //基本
                var claimResponse = response.GetExtension<ClaimsResponse>();

                User = new NTPCLibrary.User();
                User.Identity = response.ClaimedIdentifier.ToString().Split('/').Last();

                User.Departments = new List<Department>();
                if (claimResponse != null)
                {
                    User.ID = claimResponse.PostalCode;
                    User.FullName = claimResponse.FullName;
                    User.NickName = claimResponse.Nickname;
                    User.Email = claimResponse.Email;
                    User.Gender = claimResponse != null ? (claimResponse.Gender.Equals(Gender.Male) ? "男" : "女") : string.Empty;
                    User.BirthDate = claimResponse.BirthDate;
                    User.SchoolName = claimResponse.Country;
                    User.ClassRoom = claimResponse.Language;
                    User.Departments = JsonConvert.DeserializeObject<List<Department>>(claimResponse.TimeZone);
                }

                //延伸
                FetchResponse fetchResponse = response.GetExtension<FetchResponse>();

                User.AXExtension = fetchResponse != null ? GetAx(fetchResponse) : "無";

                //寫入Cookie                
                Util.SetCookie<User>(OPENID_COOKIE, User, CookieDomain, true);
            }

        }

        public void Logout()
        {
            //OPENID_COOKIE的Domain是.ntpc.edu.tw，所以要刪2次
            HttpCookie cookie = new HttpCookie(OPENID_COOKIE, "");
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.AddYears(-1); 

            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["domain"]))
            {
                string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));//找Logout.aspx

                cookie.Domain = HttpContext.Current.Request.Url.Host;
                HttpContext.Current.Response.SetCookie(cookie);
                HttpContext.Current.Response.Redirect(rdpath+"?domain=1", true);
            }
            else
            {
                cookie.Domain = CookieDomain;
                HttpContext.Current.Response.SetCookie(cookie);
            }
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

        private string UrlToDomain(string Url)
        {
            string domain = string.Empty;
            string[] urls = Url.TrimEnd('/').Split('.');//{http://openid,ntpc,edu,tw}
            string[] domainUrls = urls.Where(u => u != urls[0]).ToArray();//{ntpc,edu,tw}
            domain = string.Join(".", domainUrls);//ntpc.edu.tw
            return domain;
        }
                
    }
}
