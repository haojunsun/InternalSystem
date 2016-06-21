using System.IO;
using System.Net;
using System.Text;
using InternalSystem.Core.Models;
using InternalSystem.Infrastructure.Services;
using System.Web;
using InternalSystem.Core.Services;

namespace InternalSystem.Core.Basis
{
    public class UserLogin
    {
        private static readonly IHelperServices Help = new HelperServices();

        /// <summary>
        /// 判断是否已经登录(解决Session超时问题)
        /// </summary>
        private static bool IsUserLogin()
        {
            //如果Session为Null
            return Help.GetSessionObject("SESSION_USER_INFO") != null;
        }

        /// <summary>
        /// 取得会员信息
        /// </summary>
        public static Manager GetUserInfo()
        {
            if (!IsUserLogin())
            {
                return null;
            }
            var model = Help.GetSessionObject("SESSION_USER_INFO") as Manager;
            return model;
        }

        /// <summary>
        /// 清除记录信息
        /// </summary>
        public void ExitSign()
        {
            HttpContext.Current.Session.Clear(); //清掉session
        }

        #region 请求Url，不发送数据

        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public string RequestUrl(string url)
        {
            return RequestUrl(url, "POST");
        }

        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public string RequestUrl(string url, string method)
        {
            // 设置参数
            var request = WebRequest.Create(url) as HttpWebRequest;
            var cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            var response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            var responseStream = response.GetResponseStream();
            var sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            var content = sr.ReadToEnd();
            return content;
        }

        #endregion

        /// <summary>
        /// 获取Json字符串某节点的值
        /// </summary>
        public static string GetJsonValue(string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }

                    result = jsonStr.Substring(index, end - index);
                    result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                }
            }
            return result;
        }


    }


}
