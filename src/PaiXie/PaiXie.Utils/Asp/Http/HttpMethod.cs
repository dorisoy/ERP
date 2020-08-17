using System;
using System.Web;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Linq;
namespace PaiXie.Utils
{
    /// <summary>
    /// WEB请求上下文信息工具类
    /// </summary>
    public partial class ZHttp
    {
        #region 判断当前页面是否接收到了Post请求
        /// <summary>
        /// 判断当前页面是否接收到了Post请求,如果当前请求不存在,返回false
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod.Equals("POST");
            }
        }
        #endregion

        #region 判断当前页面是否接收到了Get请求
        /// <summary>
        /// 判断当前页面是否接收到了Get请求,如果当前请求不存在,返回 false
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod.Equals("GET");
            }
        }
        #endregion

        #region 获取客户端使用的HTTP数据传输方法
        /// <summary>
        /// 获取客户端使用的HTTP数据传输方法 
        /// </summary>
        public static string Method
        {
            get
            {
                return HttpContext.Current.Request.HttpMethod;
            }
        }
        #endregion

		#region Post And Get
		public static string WebRequestGet(string url) {
			string rs = "";
			try {
				string xmlContent = "";
				HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
				HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
				StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
				xmlContent = sr.ReadToEnd().Trim();
				sr.Close();
				HttpWResp.Close();
				HttpWReq.Abort();
				rs = xmlContent;

			}
			catch (Exception ex) {
				rs = "";
				return ex.Message;
			}

			return rs;
		}

		/// <summary>
		/// 提交Get请求并获取输出内容
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters">提交参数</param>
		/// <returns></returns>
		public static string WebRequestGet(string url, IDictionary<string, string> parameters) {
			if ((parameters != null) && (parameters.Count > 0)) {
				if (url.Contains("?")) {
					url = url + "&" + BuildPostData(parameters);
				}
				else {
					url = url + "?" + BuildPostData(parameters);
				}
			}
			HttpWebRequest request;
			//如果是发送HTTPS请求   
			if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase)) {
				ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
				request = WebRequest.Create(url) as HttpWebRequest;
				request.ProtocolVersion = HttpVersion.Version10;
			}
			else {
				request = (HttpWebRequest)WebRequest.Create(url);
			}
			request.Method = "GET";
			request.KeepAlive = false;
			request.UserAgent = "U1city";
			request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
			HttpWebResponse rsp = (HttpWebResponse)request.GetResponse();
			Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
			return GetResponseString(rsp, encoding);
		}

		/// <summary>
		/// 提交Post请求并获取输出内容
		/// </summary>
		/// <param name="url">URL地址</param>
		/// <param name="formDataDict">提交参数</param>
		/// <returns></returns>
		public static string WebRequestPost(string url, IDictionary<string, string> formDataDict) {
			string strRequestData = BuildPostData(formDataDict);
			return WebRequestPost(url, strRequestData);
		}

		/// <summary>
		/// 提交Post请求并获取输出内容
		/// </summary>
		/// <param name="url">URL地址</param>
		/// <param name="param">提交参数。示例 key1=value1&key2=value2</param>
		/// <returns></returns>
		public static string WebRequestPost(string url, string param) {
			if (url == "") {
				return "url未配置";
			}

			System.Text.Encoding encode = System.Text.Encoding.UTF8;
			string strRequestData = param;

			//把数组转换成流中所需字节数组类型
			byte[] bytesRequestData = encode.GetBytes(strRequestData);


			//请求远程HTTP
			string strResult = "";
			try {
				//设置HttpWebRequest基本信息
				HttpWebRequest request;
				//如果是发送HTTPS请求   
				if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase)) {
					ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
					request = WebRequest.Create(url) as HttpWebRequest;
					request.ProtocolVersion = HttpVersion.Version10;
				}
				else {
					request = (HttpWebRequest)WebRequest.Create(url);
				}
				request.Method = "post";
				request.KeepAlive = false;
				request.UserAgent = "U1city";
				request.ContentType = "application/x-www-form-urlencoded";

				//填充POST数据
				request.ContentLength = bytesRequestData.Length;
				Stream requestStream = request.GetRequestStream();
				requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
				requestStream.Close();

				//发送POST数据请求服务器
				HttpWebResponse HttpWResp = (HttpWebResponse)request.GetResponse();
				Stream myStream = HttpWResp.GetResponseStream();

				//获取服务器返回信息
				StreamReader reader = new StreamReader(myStream, encode);

				strResult = reader.ReadToEnd();
				reader.Close();

				//释放
				myStream.Close();

			}
			catch (Exception exp) {
				strResult = "报错：" + exp.Message;
			}

			return strResult;
		}


		/// <summary>
		/// 获取输出字符串内容
		/// </summary>
		/// <param name="rsp"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		private static string GetResponseString(HttpWebResponse rsp, Encoding encoding) {
			string strResult = string.Empty;
			Stream responseStream = null;
			StreamReader reader = null;
			try {
				responseStream = rsp.GetResponseStream();
				reader = new StreamReader(responseStream, encoding);
				strResult = reader.ReadToEnd();
				reader.Close();
			}
			finally {
				if (reader != null) {
					reader.Close();
				}
				if (responseStream != null) {
					responseStream.Close();
				}
				if (rsp != null) {
					rsp.Close();
				}
			}
			return strResult;
		}
		/// <summary>
		///  把参数添加到post请求流里面
		/// </summary>
		/// <param name="memStream">MemoryStream</param>
		/// <param name="formDataDict"></param>
		/// <param name="boundary"></param>
		private static void AddFormData(MemoryStream memStream, IDictionary<string, string> formDataDict, string boundary) {
			//没有form参数的情况下，直接返回
			if (formDataDict == null || formDataDict.Keys.Count == 0) {
				return;
			}

			// 写入字符串的Key  
			var stringKeyHeader = "\r\n--" + boundary +
								   "\r\nContent-Disposition: form-data; name=\"{0}\"" +
								   "\r\n\r\n{1}";

			//将需要提交的form的参数信息，设置到post请求流里面
			foreach (byte[] formitembytes in from string key in formDataDict.Keys
											 select string.Format(stringKeyHeader, key, formDataDict[key])
												 into formitem
												 select Encoding.UTF8.GetBytes(formitem)) {
				memStream.Write(formitembytes, 0, formitembytes.Length);
			}

		}

		private static bool CheckValidationResult(object sender,
			System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors) {
			return true;// Always accept
		}

		/// <summary>
		/// 生成Post请求字符串
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private static string BuildPostData(IDictionary<string, string> parameters) {
			StringBuilder builder = new StringBuilder();
			bool flag = false;
			IEnumerator<KeyValuePair<string, string>> enumerator = parameters.GetEnumerator();
			while (enumerator.MoveNext()) {
				KeyValuePair<string, string> current = enumerator.Current;
				string key = current.Key;
				string str2 = current.Value;
				if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(str2)) {
					if (flag) {
						builder.Append("&");
					}
					builder.Append(key);
					builder.Append("=");
					builder.Append(Uri.EscapeDataString(str2));
					flag = true;
				}
			}
			return builder.ToString();
		}
		#endregion
    }
}

