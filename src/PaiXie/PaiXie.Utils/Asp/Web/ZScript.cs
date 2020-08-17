using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;

namespace PaiXie.Utils
{
    public class ZScript
    {
        #region  Alert()
        /**/
        /// <summary>
        /// 简单弹出对话框功能
        /// 代码调用:
        /// UIHelper.Alert(this.Page,"OKOK");
        /// 
        /// 
        /// </summary>
        /// <param name="pageCurrent">
        /// 当前的页面
        /// </param>
        /// <param name="strMsg">
        /// 弹出信息的内容
        /// </param>
        public static void Alert(System.Web.UI.Page pageCurrent, string strMsg)
        {
            //Replace \n
            strMsg = strMsg.Replace("\n", "file://n/");
            //Replace \r
            strMsg = strMsg.Replace("\r", "file://r/");
            //Replace "
            strMsg = strMsg.Replace("\"", "\\\"");
            //Replace '
            strMsg = strMsg.Replace("\'", "\\\'");

            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                System.Guid.NewGuid().ToString(),
                "<script>window.alert('" + strMsg + "')</script>"
                );

            //以下代码是兼容.net1.1版本的，但到了2.0时代此API就过时了
            //pageCurrent.RegisterStartupScript(
            //    System.Guid.NewGuid().ToString(),
            //    "<script>window.alert('" + strMsg + "')</script>"
            //    );
        }
        public static void Alert(System.Web.UI.Page pageCurrent, string strMsg, string GoBackUrl)
        {
            //Replace \n
            strMsg = strMsg.Replace("\n", "file://n/");
            //Replace \r
            strMsg = strMsg.Replace("\r", "file://r/");
            //Replace "
            strMsg = strMsg.Replace("\"", "\\\"");
            //Replace '
            strMsg = strMsg.Replace("\'", "\\\'");

            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                System.Guid.NewGuid().ToString(),
                "<script>window.alert('" + strMsg + "');location='" + GoBackUrl + "'</script>"
                );

            //以下代码是兼容.net1.1版本的，但到了2.0时代此API就过时了
            //pageCurrent.RegisterStartupScript(
            //    System.Guid.NewGuid().ToString(),
            //    "<script>window.alert('" + strMsg + "')</script>"
            //    );
        }


        #endregion

        #region ScrollMessage
        /**/
        /// <summary>
        /// 简单的滚动信息栏
        /// 代码调用
        ///         UIHelper.ScrollMessage(this.Page, "滚动的内容");
        /// </summary>
        /// <param name="pageCurrent">
        /// 当前页面
        /// </param>
        /// <param name="strMsg">
        /// 要滚动的信息
        /// </param>
        public static string ScrollMessage(string strMsg)
        {
            //Replace \n
            strMsg = strMsg.Replace("\n", "file://n/");
            //Replace \r
            strMsg = strMsg.Replace("\r", "file://r/");
            //Replace "
            strMsg = strMsg.Replace("\"", "\\\"");
            //Replace '
            strMsg = strMsg.Replace("\'", "\\\'");


            StringBuilder sb = new StringBuilder();
            sb.Append("<MARQUEE>");
            sb.Append(strMsg);
            sb.Append("</MARQUEE>");
            return sb.ToString();
            //pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
            //        System.Guid.NewGuid().ToString(),sb.ToString());
        }


        /**/
        /// <summary>
        /// 指定滚动文字的详细方法
        /// 
        /// </summary>
        /// <param name="pageCurrent">
        /// 当前的页面
        /// </param>
        /// <param name="strMsg">
        /// 要滚动的文字
        /// </param>
        /// <param name="aligh">
        /// align：是设定活动字幕的位置，居左、居中、居右、靠上和靠下三种位置
        /// left center  right top bottom 
        /// </param>
        /// <param name="bgcolor">
        /// 用于设定活动字幕的背景颜色，一般是十六进制数。如#CCCCCC 
        /// </param>
        /// <param name="direction">
        /// 用于设定活动字幕的滚动方向是向左、向右、向上、向下
        /// left|right|up|down 
        /// </param>
        /// <param name="behavior">
        /// 用于设定滚动的方式，主要由三种方式：scroll slide和alternate
        /// 
        /// </param>
        /// <param name="height">
        /// 用于设定滚动字幕的高度
        /// 
        /// </param>
        /// <param name="hspace">
        /// 则设定滚动字幕的宽度
        /// </param>
        /// <param name="scrollamount">
        /// 用于设定活动字幕的滚动距离
        /// </param>
        /// <param name="scrolldelay">
        /// 用于设定滚动两次之间的延迟时间
        /// </param>
        /// <param name="width"></param>
        /// <param name="vspace">
        /// 分别用于设定滚动字幕的左右边框和上下边框的宽度
        /// 
        /// </param>
        /// <param name="loop">
        /// 用于设定滚动的次数，当loop=-1表示一直滚动下去，直到页面更新
        /// </param>
        /// <param name="MarqueejavascriptPath">
        /// 脚本的存放位置
        /// </param>
        /// <returns></returns>
        public static string ScrollMessage(System.Web.UI.Page pageCurrent, string strMsg, string aligh, string bgcolor,
                                    string direction, string behavior, string height, string hspace,
                                    string scrollamount, string scrolldelay, string width, string vspace, string loop,
                                    string MarqueejavascriptPath)
        {
            StreamReader sr = new StreamReader(pageCurrent.MapPath(MarqueejavascriptPath));
            StringBuilder sb = new StringBuilder();
            string line;
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);

                }
                sr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            sb.Replace("$strMessage", strMsg);
            sb.Replace("$aligh", aligh);
            sb.Replace("$bgcolor", bgcolor);
            sb.Replace("$direction", direction);
            sb.Replace("$behavior", behavior);
            sb.Replace("$height", height);
            sb.Replace("$hspace", hspace);
            sb.Replace("$scrollamount", scrollamount);
            sb.Replace("$scrolldelay", scrolldelay);
            sb.Replace("$width", width);
            sb.Replace("$vspace", vspace);
            sb.Replace("$loop", loop);
            //pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
            //            System.Guid.NewGuid().ToString(), sb.ToString());
            return sb.ToString();
        }

        #endregion

        #region  Redirect()
        /**/
        /// <summary>
        /// Add the javascript method to redirect page on client
        /// Created : Wang Hui, May 18,2006
        /// Modified: 
        /// </summary>
        /// <param name="pageCurrent"></param>
        /// <param name="strPage"></param>
        public static void Redirect(System.Web.UI.Page pageCurrent, string strPage)
        {
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                    System.Guid.NewGuid().ToString(),
                    "<script>window.location.href='" + strPage + "'</script>"
                    );

            //以下方法是兼容1.1的,2.0过时
            //pageCurrent.RegisterStartupScript(
            //    System.Guid.NewGuid().ToString(),
            //    "<script>window.location.href='" + strPage + "'</script>"
            //    );
        }
        /**/
        /// <summary>
        /// 主要用于跳出带有框架的页面
        /// </summary>
        /// <param name="pageCurrent">当前页面如this.page</param>
        /// <param name="strPage">要跳出的页面</param>
        public static void RedirectFrame(System.Web.UI.Page pageCurrent, string strPage)
        {
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                    System.Guid.NewGuid().ToString(),
                    "<script>window.top.location.href='" + strPage + "'</script>"
                    );

            //以下方法是兼容1.1的,2.0过时
            //pageCurrent.RegisterStartupScript(
            //    System.Guid.NewGuid().ToString(),
            //    "<script>window.location.href='" + strPage + "'</script>"
            //    );
        }

        #endregion

        #region AddConfirm
        /**/
        /// <summary>
        /// Add the confirm message to button
        /// Created : GuangMing Chu 1,1,2007
        /// Modified: GuangMing Chu 1,1,2007
        /// Modified: GuangMing Chu 1,1,2007
        /// 代码调用：
        ///    UIHelper.AddConfirm(this.Button1, "真的要删了？？");
        /// 点确定按钮就会执行事件中的代码，点取消不会
        /// </summary>
        /// <param name="button">The control, must be a button</param>
        /// <param name="strMsg">The popup message</param>
        public static void AddConfirm(System.Web.UI.WebControls.Button button, string strMsg)
        {
            strMsg = strMsg.Replace("\n", "file://n/");
            strMsg = strMsg.Replace("\r", "file://r/");
            strMsg = strMsg.Replace("\"", "\\\"");
            strMsg = strMsg.Replace("\'", "\\\'");
            button.Attributes.Add("onClick", "return confirm('" + strMsg + "')");
        }

        /**/
        /// <summary>
        /// Add the confirm message to button
        /// Created : GuangMing Chu, 1 1,2007
        /// Modified: GuangMing Chu, 1 1,2007
        /// Modified:
        /// 代码调用:
        ///       UIHelper.AddConfirm(this.Button1, "真的要删了？？");
        /// 点确定按钮就会执行事件中的代码，点取消不会
        ///      
        /// </summary>
        /// <param name="button">The control, must be a button</param>
        /// <param name="strMsg">The popup message</param>
        public static void AddConfirm(System.Web.UI.WebControls.ImageButton button, string strMsg)
        {
            strMsg = strMsg.Replace("\n", "file://n/");
            strMsg = strMsg.Replace("\r", "file://r/");
            strMsg = strMsg.Replace("\"", "\\\"");
            strMsg = strMsg.Replace("\'", "\\\'");
            button.Attributes.Add("onClick", "return confirm('" + strMsg + "')");
        }

        /**/
        /// <summary>
        /// Add the confirm message to one column of gridview
        /// 代码调用：
        ///         UIHelper myHelp = new UIHelper();
        ///         myHelp.AddConfirm(this.GridView1,1, "ok");
        /// 请使用时注意，此方法的调用必须实例化，调用
        /// </summary>
        /// <param name="grid">The control, must be a GridView</param>
        /// <param name="intColIndex">The column index. It's usually the column which has the "delete" button.</param>
        /// <param name="strMsg">The popup message</param>
        public static void AddConfirm(System.Web.UI.WebControls.GridView grid, int intColIndex, string strMsg)
        {
            strMsg = strMsg.Replace("\n", "file://n/");
            strMsg = strMsg.Replace("\r", "file://r/");
            strMsg = strMsg.Replace("\"", "\\\"");
            strMsg = strMsg.Replace("\'", "\\\'");
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                grid.Rows[i].Cells[intColIndex].Attributes.Add("onclick", "return confirm('" + strMsg + "')");
            }
        }
        #endregion

        #region AddShowDialog
        /**/
        /// <summary>
        /// Add the javascript method showModalDialog to button
        /// 为Button按钮加入一个弹出窗体对话框
        /// 代码调用
        ///         UIHelper.AddShowDialog(this.Button1, "www.sina.com.cn", 300, 300);
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="button">The control, must be a button</param>
        /// <param name="strUrl">The page url, including query string</param>
        /// <param name="intWidth">Width of window</param>
        /// <param name="intHeight">Height of window</param>
        public static void AddShowDialog(System.Web.UI.WebControls.Button button, string strUrl, int intWidth, int intHeight)
        {
            string strScript = "";
            strScript += "var strFeatures = 'dialogWidth=" + intWidth.ToString() + "px;dialogHeight=" + intHeight.ToString() + "px;center=yes;help=no;status=no';";
            strScript += "var strName ='';";

            if (strUrl.Substring(0, 1) == "/")
            {
                strUrl = strUrl.Substring(1, strUrl.Length - 1);
            }

            strUrl = BaseUrl + "DialogFrame.aspx?URL=" + strUrl;

            strScript += "window.showModalDialog(\'" + strUrl + "\',window,strFeatures);return false;";

            button.Attributes.Add("onClick", strScript);
        }
        #endregion

        #region  AddShowDialog
        /**/
        /// <summary>
        /// Add the javascript method showModalDialog to button
        /// </summary>
        /// <param name="button">The control, must be a link button</param>
        /// <param name="strUrl">The page url, including query string</param>
        /// <param name="intWidth">Width of window</param>
        /// <param name="intHeight">Height of window</param>
        public static void AddShowDialog(System.Web.UI.WebControls.LinkButton button, string strUrl, int intWidth, int intHeight)
        {
            string strScript = "";
            strScript += "var strFeatures = 'dialogWidth=" + intWidth.ToString() + "px;dialogHeight=" + intHeight.ToString() + "px;center=yes;help=no;status=no';";
            strScript += "var strName ='';";

            if (strUrl.Substring(0, 1) == "/")
            {
                strUrl = strUrl.Substring(1, strUrl.Length - 1);
            }

            strUrl = BaseUrl + "DialogFrame.aspx?URL=" + strUrl;

            strScript += "window.showModalDialog(\'" + strUrl + "\',strName,strFeatures);return false;";

            button.Attributes.Add("onClick", strScript);
        }
        #endregion

        #region  AddShowDialog
        /**/
        /// <summary>
        /// Add the javascript method showModalDialog to button
        /// </summary>
        /// <param name="button">The control, must be a button</param>
        /// <param name="strUrl">The page url, including query string</param>
        /// <param name="intWidth">Width of window</param>
        /// <param name="intHeight">Height of window</param>
        public static void AddShowDialog(System.Web.UI.WebControls.ImageButton button, string strUrl, int intWidth, int intHeight)
        {
            string strScript = "";
            strScript += "var strFeatures = 'dialogWidth=" + intWidth.ToString() + "px;dialogHeight=" + intHeight.ToString() + "px;center=yes;help=no;status=no';";
            strScript += "var strName ='';";

            if (strUrl.Substring(0, 1) == "/")
            {
                strUrl = strUrl.Substring(1, strUrl.Length - 1);
            }

            strUrl = BaseUrl + "DialogFrame.aspx?URL=" + strUrl;

            strScript += "window.showModalDialog(\'" + strUrl + "\',window,strFeatures);return false;";

            button.Attributes.Add("onClick", strScript);
        }
        #endregion

        #region ClearTextBox
        /**/
        /// <summary>
        /// 将选定的TextBox值清空
        /// </summary>
        /// <param name="myTextBox"></param>
        public static void ClearTextBox(System.Web.UI.WebControls.TextBox myTextBox)
        {
            myTextBox.Attributes.Add("onclick", "this.value=''");
        }
        #endregion

        #region OpenWindow
        /**/
        /// <summary>
        /// Use "window.open" to popup the window
        /// Created : Wang Hui, Feb 24,2006
        /// Modified: Wang Hui, Feb 24,2006
        /// 打开窗体的对话框功能
        /// 代码调用：
        /// 
        /// 
        ///         UIHelper.OpenWindow(this.Page, "www.sina.com.cn", 400, 300);
        ///         UIHelper.ShowDialog(this.Page, "lsdjf.com", 300, 200);
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="strUrl">The url of window, start with "/", not "http://"</param>
        /// <param name="intWidth">Width of popup window</param>
        /// <param name="intHeight">Height of popup window</param>
        public static void OpenWindow(System.Web.UI.Page pageCurrent, string strUrl, int intWidth, int intHeight, int intLeft, int intTop, string WinName)
        {
            #region 老版本
            //string strScript = "";
            //strScript += "var strFeatures = 'width:" + intWidth.ToString() + "px;height:" + intHeight.ToString() + "px';";
            //strScript += "var strName ='__WIN';";
            /**/
            ////strScript += "alert(strFeatures);";

            ////--- Add by Wang Hui on Feb 27
            //if (strUrl.Substring(0, 1) == "/")
            //{
            //    strUrl = strUrl.Substring(1, strUrl.Length - 1);
            //}
            /**/
            ////--- End Add by Wang Hui on Feb 27

            //strUrl = BaseUrl + strUrl;

            //strScript += "window.open(\"" + strUrl + "\",strName,strFeatures);";

            //pageCurrent.RegisterStartupScript(
            //    System.Guid.NewGuid().ToString(),
            //    "<script language='javascript'>" + strScript + "</script>"
            //    );


            //pageCurrent.RegisterStartupScript(
            //    System.Guid.NewGuid().ToString(),
            //    "<script language='javascript'>" + strScript + "</script>"
            //    );
            #endregion

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"myleft={0};mytop={1};", intLeft.ToString(), intTop.ToString());
            sb.AppendLine();
            sb.AppendFormat(@"settings='top=' + mytop + ',left=' + myleft + ',width={0},height={1},location=no,directories=no,menubar=no,toolbar=no,status=no,scrollbars=no,resizable=no,fullscreen=no';",
                             intWidth.ToString(), intHeight.ToString());
            sb.AppendLine();
            sb.AppendFormat(@"window.open('{0}','{1}', settings);", strUrl, WinName);

            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                    System.Guid.NewGuid().ToString(),
                    "<script language='javascript'>" + sb.ToString() + "</script>"
                    );

        }
        #endregion

        #region ShowDialog
        /**/
        /// <summary>
        /// Use "window.showModalDialog" to show the dialog
        /// Created : Wang Hui, Feb 24,2006
        /// Modified: Wang Hui, Feb 27,2006
        /// 此窗体是模式窗体,和C/S结构中的模式窗体是一致的
        /// </summary>
        /// <param name="strUrl">The url of dialog, start with "/", not "http://"</param>
        /// <param name="intWidth">Width of dialog</param>
        /// <param name="intHeight">Height of dialog</param>
        public static void ShowDialog(System.Web.UI.Page pageCurrent, string strUrl, int intWidth, int intHeight)
        {
            string strScript = "";
            strScript += "var strFeatures = 'dialogWidth=" + intWidth.ToString() + "px;dialogHeight=" + intHeight.ToString() + "px;center=yes;help=no;status=no';";
            strScript += "var strName ='';";

            //--- Add by Wang Hui on Feb 27 
            if (strUrl.Substring(0, 1) == "/")
            {
                strUrl = strUrl.Substring(1, strUrl.Length - 1);
            }
            //--- End Add by Wang Hui on Feb 27

            strUrl = BaseUrl + "DialogFrame.aspx?URL=" + strUrl;

            //strScript += "window.showModalDialog(\"" + strUrl + "\",strName,strFeatures);";
            strScript += "window.showModalDialog(\"" + strUrl + "\",window,strFeatures); ";

            pageCurrent.ClientScript.RegisterStartupScript(
                pageCurrent.GetType(), System.Guid.NewGuid().ToString(),
                "<script language='javascript'>" + strScript + "</script>"
                );
        }
        #endregion

        #region CloseWindows
        /**/
        /// <summary>
        /// 关闭窗体,没有任何提示的关闭窗体
        /// </summary>
        /// <param name="pageCurrent"></param>
        public static void CloseWindows(System.Web.UI.Page pageCurrent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>window.opener=null;window.close();</script>");
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                System.Guid.NewGuid().ToString(), sb.ToString());
        }



        /**/
        /// <summary>
        /// 有提示信息的关闭窗体
        /// </summary>
        /// <param name="pageCurrent"></param>
        /// <returns></returns>
        public static void CloseWindows(System.Web.UI.Page pageCurrent, string strMessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>if(confirm(\"" + strMessage + "\")==true){window.close();}</script>");
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                                System.Guid.NewGuid().ToString(), sb.ToString());
        }
        /**/
        /// <summary>
        /// 有等待时间的关闭窗体
        /// </summary>
        /// <param name="pageCurrent"></param>
        /// <param name="WaitTime">等待时间，以毫秒为记量单位</param>
        public static void CloseWindows(System.Web.UI.Page pageCurrent, int WaitTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\">");
            //加入此行功能后没有提提示功能
            sb.Append("window.opener=null;");
            sb.Append("setTimeout");
            sb.Append("(");
            sb.Append("'");
            sb.Append("window.close()");
            sb.Append("'");

            sb.Append(",");
            sb.Append(WaitTime.ToString());
            sb.Append(")");
            sb.Append("</script>");
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                        System.Guid.NewGuid().ToString(), sb.ToString());

        }
        #endregion

        #region  ShowStatusBar
        public static void ShowStatus(System.Web.UI.Page pageCurrent, string StatusString)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\">");
            sb.Append("window.status=");
            sb.Append("\"");
            sb.Append(StatusString);
            sb.Append("\"");
            sb.Append("</script>");
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                System.Guid.NewGuid().ToString(), sb.ToString());
        }
        #endregion

        #region PlayMediaFile
        /**/
        /// <summary>
        /// 调用Media播放mp3或电影文件
        /// </summary>
        /// <param name="pageCurrent">
        /// 当前的页面对象
        /// </param>
        /// <param name="PlayFilePath">
        /// 播放文件的位置
        /// </param>
        /// <param name="MediajavascriptPath">
        /// Mediajavascript的脚本位置
        /// </param>
        /// <param name="enableContextMenu">
        /// 是否可以使用右键
        /// 指定是否使右键菜单有效
        /// 指定右键是否好用,默认为0不好用
        /// 指定为1时就是好用
        /// </param>
        /// <param name="uiMode">
        /// 播放器的大小显示
        /// None，mini，或full，指定Windows媒体播放器控制如何显示
        /// </param>
        public static string PlayMediaFile(System.Web.UI.Page pageCurrent,
                        string PlayFilePath, string MediajavascriptPath,
                        string enableContextMenu, string uiMode)
        {
            StreamReader sr = new StreamReader(pageCurrent.MapPath(MediajavascriptPath));
            StringBuilder sb = new StringBuilder();
            string line;
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);

                }
                sr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            sb.Replace("$URL", pageCurrent.MapPath(PlayFilePath));
            sb.Replace("$enableContextMenu", enableContextMenu);
            sb.Replace("$uiMode", uiMode);
            //pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
            //            System.Guid.NewGuid().ToString(), sb.ToString());
            return sb.ToString();
        }
        #endregion

        #region ShowProgBar
        /**/
        /// <summary>
        /// 主要实现进度条的功能，这段代码的调用就要实现进度的调度
        /// 实现主要过程
        /// default.aspx.cs是调用页面
        /// 放入page_load事件中
        ///            UIHelper myUI = new UIHelper();
        ///            Response.Write(myUI.ShowProgBar(this.Page,"../JS/progressbar.htm"));
        ///            Thread thread = new Thread(new ThreadStart(ThreadProc));
        ///            thread.Start();
        ///            LoadData();//load数据 
        ///            thread.Join();
        ///            Response.Write("OK");
        /// 
        /// 其中ThreadProc方法为
        ///     public void ThreadProc()
        ///    {
        ///    string strScript = "<script>setPgb('pgbMain','{0}');</script>";
        ///    for (int i = 0; i <= 100; i++)
        ///     {
        ///        System.Threading.Thread.Sleep(10);
        ///        Response.Write(string.Format(strScript, i));
        ///        Response.Flush();
        ///     }
        ///    }
        /// 其中LoadData()
        ///     public void LoadData()
        ///        {
        ///            for (int m = 0; m < 900; m++)
        ///            {
        ///                for (int i = 0; i < 900; i++)
        ///                {
        ///
        ///                }
        ///            }
        ///        }
        /// 
        /// </summary>
        /// <param name="pageCurrent"></param>
        /// <param name="ShowProgbarScript"></param>
        /// <returns></returns>
        public static string ShowProgBar(System.Web.UI.Page pageCurrent, string ShowProgbarScript)
        {
            StreamReader sr = new StreamReader(pageCurrent.MapPath(ShowProgbarScript), System.Text.Encoding.Default);
            StringBuilder sb = new StringBuilder();
            string line;
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);

                }
                sr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
            //            System.Guid.NewGuid().ToString(), sb.ToString());
            return sb.ToString();
        }
        #endregion

        #region fixedHeader
        public static string fixedHeader()
        {
            StringBuilder s = new StringBuilder();
            s.Append(@"<table width='100%' border='1' cellspacing='0' style='MARGIN-TOP:-2px'>");
            s.Append(@"<TR class='fixedHeaderTr' style='BACKGROUND:navy;COLOR:white'>");
            s.Append(@"<TD nowrap>Header A</TD>");
            s.Append(@"<TD nowrap>Header B</TD>");
            s.Append(@"<TD nowrap>Header C</TD>");
            s.Append(@"</TR>");
            for (int m = 0; m < 100; m++)
            {
                s.Append(@"<TR>");
                s.Append(@"<TD>A").Append(m).Append("</TD>");
                s.Append(@"<TD>B").Append(m).Append("</TD>");
                s.Append(@"<TD>C").Append(m).Append("</TD>");
                s.Append(@"</TR>");
            }
            s.Append(@"</table>");
            return s.ToString();
        }
        #endregion

        #region refreshPage
        public static void refreshPage(System.Web.UI.Page pageCurrent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\">");
            sb.Append("window.location.reload(true);");
            sb.Append("</script>");
            pageCurrent.ClientScript.RegisterStartupScript(pageCurrent.GetType(),
                        System.Guid.NewGuid().ToString(), sb.ToString());

        }
        #endregion

        #region Page_revealTrans
        //进入页面<meta http-equiv="Page-Enter" content="revealTrans(duration=x, transition=y)">
        //推出页面<meta http-equiv="Page-Exit" content="revealTrans(duration=x, transition=y)"> 
        //这个是页面被载入和调出时的一些特效。duration表示特效的持续时间，以秒为单位。transition表示使用哪种特效，取值为1-23:
        //  0 矩形缩小 
        //  1 矩形扩大 
        //  2 圆形缩小
        //  3 圆形扩大 
        //  4 下到上刷新 
        //  5 上到下刷新
        //  6 左到右刷新 
        //  7 右到左刷新 
        //  8 竖百叶窗
        //  9 横百叶窗 
        //  10 错位横百叶窗 
        //  11 错位竖百叶窗
        //  12 点扩散 
        //  13 左右到中间刷新 
        //  14 中间到左右刷新
        //  15 中间到上下
        //  16 上下到中间 
        //  17 右下到左上
        //  18 右上到左下 
        //  19 左上到右下 
        //  20 左下到右上
        //  21 横条 
        //  22 竖条 
        //  23 以上22种随机选择一种

        public static string Page_revealTrans(System.Web.UI.Page currentPage, string duration,
                                       string transition)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"Page-Enter\"");
            sb.Append("content=\"");
            sb.Append("revealTrans(duration=" + duration);
            sb.Append(",transition=" + transition);
            sb.Append(")\">");
            //currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(),
            //        System.Guid.NewGuid().ToString(), sb.ToString());
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 显示一段自定义的输出代码
        /// </summary>
        /// <param name="page">页面指针,一般为This</param>
        public static void RegisterStartupScript(System.Web.UI.Page page, string script)
        {
            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\"> ");
            sb.Append(script.Trim());
            sb.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), page.GetType().Name, sb.ToString());
        }

        public static void RegisterClientScriptBlock(System.Web.UI.Page page, string script)
        {
            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\"> ");
            sb.Append(script.Trim());
            sb.Append("</script>");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), page.GetType().Name, sb.ToString());
        }
        

        /**/
        /// <summary>
        /// 调用客户端JavaScript函数
        /// </summary>
        /// <param name="page">页面指针,一般为This</param>
        /// <param name="scriptName">函数名,带参数,如:FunA(1);</param>
        public static void CallClientScript(System.Web.UI.Page page, string scriptName)
        {
            String csname = "PopupScript";
            Type cstype = page.GetType();
            System.Web.UI.ClientScriptManager cs = page.ClientScript;
            if (!cs.IsStartupScriptRegistered(cstype, csname))
            {
                String cstext = scriptName;
                cs.RegisterStartupScript(cstype, csname, cstext, true);
            }
        }

        /**/
        /// <summary>
        /// 弹出对话框(弹出对话框后css会失效)
        /// </summary>
        /// <param name="message">提示信息</param>
        public static void ShowMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + message.Trim() + "\"); \n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /**/
        /// <summary>
        /// 弹出对话框(不影响css样式)
        /// </summary>
        /// <param name="page">页面指针,一般为this</param>
        /// <param name="scriptKey">脚本键,唯一</param>
        /// <param name="message">提示信息</param>
        public static void ShowMessage(System.Web.UI.Page page, string scriptKey, string message)
        {
            System.Web.UI.ClientScriptManager csm = page.ClientScript;
            if (!csm.IsClientScriptBlockRegistered(scriptKey))
            {
                string strScript = "alert('" + message + "');";
                csm.RegisterClientScriptBlock(page.GetType(), scriptKey, strScript, true);
            }
        }

        /**/
        /// <summary>
        /// 为控件添加确认提示对话框
        /// </summary>
        /// <param name="Control">需要添加提示的对话框</param>
        /// <param name="message">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string message)
        {
            Control.Attributes.Add("onclick", "return confirm('" + message + "');");
        }

        /**/
        /**/
        /**/
        /// <summary>
        /// 显示一个弹出窗口，并转向目标页(导航)
        /// </summary>
        public static void ShowAndRedirect(string message, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + message.Trim() + "\"); \n");
            sb.Append("window.location.href=\"" + url.Trim().Replace("'", "") + "\";\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /**/
        /// <summary>
        /// 显示一个弹出窗口，重新加载当前页
        /// </summary>
        public static void ShowAndReLoad(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + message.Trim() + "\"); \n");
            sb.Append("window.location.href=window.location.href;\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /**/
        /// <summary>
        /// 显示一个弹出窗口,刷新当前页(危险的,有可能陷入死循环)
        /// </summary>
        public static void ShowAndRefresh(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + message.Trim() + "\"); \n");
            sb.Append("document.execCommand('Refresh')");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /**/
        /// <summary>
        /// 显示一个弹出窗口,并关闭当前页
        /// </summary>
        /// <param name="message"></param>
        public static void ShowAndClose(string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=\"javascript\">\n");
            sb.Append("alert(\"" + message.Trim() + "\"); \n");
            sb.Append("window.close();\n");
            sb.Append("</script>\n");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /**/
        /// <summary>
        /// 显示一个弹出窗口,并转向上一页
        /// </summary>
        /// <param name="message"></param>
        public static void ShowPre(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("alert(\"" + message.Trim() + "\"); \n");
            sb.Append("var p=document.referrer; \n");
            sb.Append("window.location.href=p;\n");
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        /**/
        /// <summary>
        /// 页面重载
        /// </summary>
        public static void ReLoad()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append("window.location.href=window.location.href;");
            sb.Append("</script>");
            System.Web.HttpContext.Current.Response.Write(sb.ToString());

        }

        /**/
        /// <summary>
        /// 重定向
        /// </summary>
        public static void Redirect(string url)
        {
            //string path = "http://" + System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + url;
            string path = "http://" + System.Web.HttpContext.Current.Request.Url.Host + url;
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\"> \n");
            sb.Append(string.Format("window.location.href='{0}';", @path.Replace("'", "")));
            sb.Append("</script>");

            System.Web.HttpContext.Current.Response.Write(sb.ToString());
        }

        #region  Static Property Get BaseUrl(静态属性获取URL地址)
        /**/
        /// <summary>
        /// 这个静态属性的调用必须用以下代码方法调用
        /// 代码调用:
        /// Response.Write(UIHelper.BaseUrl);
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                //strBaseUrl用于存储URL地址
                string strBaseUrl = "";
                //获取当前HttpContext下的地址
                strBaseUrl += "http://" + HttpContext.Current.Request.Url.Host;
                //如果端口不是80的话，那么加入特殊端口
                if (HttpContext.Current.Request.Url.Port.ToString() != "80")
                {
                    strBaseUrl += ":" + HttpContext.Current.Request.Url.Port;
                }
                strBaseUrl += HttpContext.Current.Request.ApplicationPath;

                return strBaseUrl + "/";
            }
        }
        #endregion
    }
}