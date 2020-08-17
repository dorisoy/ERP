using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenVAS.Core;
using JdSoft.Apple.Apns.Notifications;
using OpenVAS.Logging;
using OpenVAS.Core.DataCollection.Enumerations;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace NC.PushService
{
    public class UserPush
    {
        public System.Collections.Hashtable VWUserTokenPushList = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());

        // 每条消息发送间隔5秒
        public int sleepBetweenNotifications = 1000 * ConfigurationManager.AppSettings["ApnsPushMsgedInterval"].ConvertInt();

        // 当前配置洗脑洗
        public UserDeviceTokenConfig PubTokenConfig;

        // 推送服务
        public NotificationService PubService;

        public void ApnPush(object AppType )
        {
            try
            {
         

                // APN推送服务初始化
              ////  InitService();

              ////  // 发送信息
              ////ApnPushData(AppType);

                PubTokenConfig = (UserDeviceTokenConfig)AppType;

                // APN推送服务初始化
                InitService();

                // 发送信息
                ApnPushData(PubTokenConfig);
            }
            catch (Exception ex)
            {


                string exstr = ex.ToString();
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\err" + "\\" + System.Guid.NewGuid().ToString() + ".txt", false);
                sw.WriteLine(exstr);
                sw.Close();//写入

            }
        }

        #region APN推送服务初始化
        private void InitService()
        {
            try
            {
                PubService.Close();
                PubService.Dispose();
            }
            catch { };

            #region APN推送服务初始化
            try
            {
                bool sandbox = PubTokenConfig.ApnsPushSandbox; // 是否测试地址
                string p12File = PubTokenConfig.ApnsP12FileName;
                string p12FilePassword = PubTokenConfig.ApnsP12FilePassword;
                string p12Filename = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, p12File);
                PubService = new NotificationService(sandbox, p12Filename, p12FilePassword, PubTokenConfig.ApnsConnections);
                PubService.SendRetries = 5; //5 retries before generating notificationfailed event
                PubService.ReconnectDelay = PubTokenConfig.ApnsPushMsgedInterval; //5 seconds // 每条消息发送间隔5秒
                PubService.Error += new NotificationService.OnError(service_Error);
                PubService.NotificationTooLong += new NotificationService.OnNotificationTooLong(service_NotificationTooLong);
                PubService.BadDeviceToken += new NotificationService.OnBadDeviceToken(service_BadDeviceToken);
                PubService.NotificationFailed += new NotificationService.OnNotificationFailed(service_NotificationFailed);
                PubService.NotificationSuccess += new NotificationService.OnNotificationSuccess(service_NotificationSuccess);
                PubService.Connecting += new NotificationService.OnConnecting(service_Connecting);
                PubService.Connected += new NotificationService.OnConnected(service_Connected);
                PubService.Disconnected += new NotificationService.OnDisconnected(service_Disconnected);
                PubService.DistributionType = NotificationServiceDistributionType.Random;
            }
            catch (Exception exp)
            {
                Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "APN推送服务初始化：InitService", exp);
            }
            #endregion
        }
        #endregion

        #region 信息推送方法
     ////   public void   ApnPushData()
        private void ApnPushData(UserDeviceTokenConfig objTokenConfigs)
        {

          
            while(true)
            { 
              
                    DataTable dt2 = db.GetInstance().ExecuteSql4Ds(" select    *  from NotificationPush  where iosStatus=0  order by id   asc   update  NotificationPush set  iosStatus=1    where IOSStatus=0  ").Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {

                           
                                NotificationPush o = new NotificationPush();
                                o.csz = (UserDeviceTokenConfig)objTokenConfigs;
                                o.exp = CryptographyHelper.DecryptDESString(dt2.Rows[i]["iosexpexstr"].ToString());
                                o.activeurl = dt2.Rows[i]["activeurl"].ToString();
                                o.contents = dt2.Rows[i]["contents"].ToString();
                                o.title = dt2.Rows[i]["title"].ToString();
                                o.rid = Convert.ToInt32(dt2.Rows[i]["id"].ToString());

                                ////PubTokenConfig = (UserDeviceTokenConfig)item2;
                                ////InitService();





                                NotificationPush objTokenConfiga = new NotificationPush();
                                objTokenConfiga = (NotificationPush)o;
                                UserDeviceTokenConfig objTokenConfig = new UserDeviceTokenConfig();
                                objTokenConfig = objTokenConfiga.csz;
                                string exps = objTokenConfiga.exp;
                                string title = objTokenConfiga.title;
                                string contents = objTokenConfiga.contents;
                                string activeurl = objTokenConfiga.activeurl;
                                int rid = objTokenConfiga.rid;




                                DateTime dtStart = DateTime.Now;
                                try
                                {
                                    #region 推送信息
                                    // 读取推送信息
                                  //  NCBaseRule objNCBaseRule = new NCBaseRule();

                                    // Sql超时时间设置
                                    //objNCBaseRule.CurrentEntities.CommandTimeout = 300;
                                    //List<VWUserTokenPush> objUserTokenList = objNCBaseRule.CurrentEntities.UserTokenPush(objTokenConfig.AppType).ToList();

                                    SqlHelper.SqlCommandTimeout = 300;

                                    string sqlstr = string.Format("    SELECT  row_number() over(order by A.uid desc) as [ID]  ,A.UID AS [ToUID] "
                                            + "  ,0 AS [UID] "
                                              + "   ,0 as [MaxMsgID] "
                                             + "    ,0 AS [TotalNum] "
                                              + "   ,B.[DeviceToken] "
                                             + "    , '{0}' as [Msg] "
                                             + "    ,'' AS [FriendNickname] "
                                             + "    ,16  as [MsgType] "
                                             + "    ,B.[AppType] "
                                             + "    ,'' AS [SendGUID] "
                                             + "    ,0 AS [IsSend] "
                                             + "    ,getdate() as [CreateDate] "
                                             + "    ,'{1}'  as  OpenUrl "


                                       + "    FROM (select  uid   from userinfo where  uid in ( {2})    ) A "
                                       + "    INNER JOIN UserDeviceToken B ON B.[UID]=A.[UID] AND B.AppType={3} "

                                        + "   ORDER BY A.[UID] ", contents, activeurl, exps, objTokenConfig.AppType);








                                    DataSet objUserTokenList = db.GetInstance().ExecuteSql4Ds(sqlstr);    // SqlHelper.ExecuteDataset(PlatformConfig.DBConnectionString, CommandType.Text, "EXEC UserTokenPush " + objTokenConfig.AppType);


                                    if (objUserTokenList != null && objUserTokenList.Tables.Count > 0 && objUserTokenList.Tables[0].Rows.Count > 0)
                                    {
                                        #region 数据初始化到集合
                                        //List<MsgInfo> MsgInfoList = new List<MsgInfo>();
                                        //foreach (DataRow item in objUserTokenList.Tables[0].Rows)
                                        //{
                                        //    MsgInfoList.Add(new MsgInfo
                                        //    {
                                        //        ID = item["ID"].ConvertInt(),
                                        //        MsgType = item["MsgType"].ConvertInt(),
                                        //        ToUID = item["ToUID"].ConvertInt(),
                                        //        DeviceToken = item["DeviceToken"].ConvertString(),
                                        //        Msg = item["Msg"].ConvertString(),
                                        //        TotalNum = item["TotalNum"].ConvertInt(),
                                        //        MaxMsgID = item["MaxMsgID"].ConvertInt()
                                        //    });
                                        //}
                                        #endregion

                                        #region 多核并行
                                        //ParallelOptions opt = new ParallelOptions();
                                        //opt.MaxDegreeOfParallelism = 20;
                                        //Parallel.ForEach(MsgInfoList, opt, (item, loopState) =>
                                        foreach (DataRow itemRow in objUserTokenList.Tables[0].Rows)
                                        {

                                            //日志
                                            db.GetInstance().ExecuteSql(string.Format("INSERT INTO [dbo].[NotificationPushLog]([ERRMSG] ,[NotificationPushID] ,[PUSHType],uid)  VALUES  ('{0}' ,{1} ,'IOS',{2})", "", rid, itemRow["ToUID"].ConvertInt()));


                                            MsgInfo item = new MsgInfo
                                            {
                                                ID = itemRow["ID"].ConvertInt(),
                                                MsgType = itemRow["MsgType"].ConvertInt(),
                                                ToUID = itemRow["ToUID"].ConvertInt(),
                                                DeviceToken = itemRow["DeviceToken"].ConvertString(),
                                                Msg = itemRow["Msg"].ConvertString(),
                                                TotalNum = itemRow["TotalNum"].ConvertInt(),
                                                MaxMsgID = itemRow["MaxMsgID"].ConvertInt(),
                                                OpenUrl = itemRow["OpenUrl"].ConvertString()
                                            };
                                            try
                                            {
                                                #region 组织推送数据并发送
                                                // 组织推送数据并发送 
                                                Notification alertNotification = new Notification(item.DeviceToken);
                                                // 消息
                                                #region 验证用户勿扰时段
                                                NCBaseRule objNCBaseRule_UserNofaze = new NCBaseRule();
                                                UserNofaze userNofaze = objNCBaseRule_UserNofaze.CurrentEntities.UserNofaze.Where(q => q.UId == item.ToUID).FirstOrDefault();
                                                if (userNofaze != null && userNofaze.Status == "Y")
                                                {
                                                    DateTime dt = DateTime.Now;
                                                    if (!(dt.Hour > userNofaze.StartTime && dt.Hour < (userNofaze.EndTime + 24)))
                                                    {
                                                        CreateMsg(item, alertNotification);
                                                    }
                                                }
                                                else
                                                {
                                                    CreateMsg(item, alertNotification);
                                                }
                                                switch (item.MsgType)
                                                {
                                                    case 12: //订单消息
                                                        alertNotification.Payload.Alert.Body = string.Format("您有一条最新订单通知，马上去处理。");//string.Format(msg);
                                                        alertNotification.Payload.Sound = "order.m4a";
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                #endregion

                                                alertNotification.Payload.Badge = item.TotalNum;
                                                // "MsgType":0,		//0表示系统消息，1表示好友消息，2表示好友确认消息
                                                //alertNotification.Payload.CustomItems.Add("MsgType", new object[] { item.MsgType }); // 注释目的是：客户端说点击消息记录才会到默认模块功能
                                                alertNotification.Payload.CustomItems.Add("ToUID", new object[] { item.ToUID });
                                                alertNotification.Payload.CustomItems.Add("MaxMsgID", new object[] { item.MaxMsgID });
                                                alertNotification.Payload.CustomItems.Add("ID", new object[] { item.ID });
                                                // 发送到苹果服务器
                                                PubService.QueueNotification(alertNotification);
                                                #endregion
                                            }
                                            catch (Exception exp)
                                            {
                                                Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：ApnPushData--Parallel.ForEach--处理错误", exp);
                                            }
                                        }
                                        //});
                                        #endregion
                                    }
                                    #endregion
                                }
                                catch (Exception exp)
                                {
                                    DateTime dtEnd = DateTime.Now;
                                    TimeSpan TS = dtEnd - dtStart;
                                    Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：ApnPushData--处理错误 执行时间：" + TS.TotalMilliseconds, exp);
                                }

                                }

                     
                }

                //  每轮疲劳发送间隔
                Thread.Sleep(sleepBetweenNotifications);



            }
        }

        private static void CreateMsg(MsgInfo item, Notification alertNotification)
        {
            switch (item.MsgType)
            {
                case 1: // 文本消息
                case 6: // 图片消息
                case 7: // 语音消息
                case 8: // 视频消息
                case 9: // 欢迎词
                case 10: // 地理位置消息
                case 13: // 二维码内容消息
                case 14: // 分享内容消息
                    alertNotification.Payload.Alert.Body = string.Format("您有一条新消息");//string.Format(msg);
                    alertNotification.Payload.Sound = "default";
                    break;
                case 4: //礼物消息
                    alertNotification.Payload.Alert.Body = string.Format(item.Msg);//string.Format(msg);
                    alertNotification.Payload.Sound = "default";
                    break;
                case 16: // 圈主给圈成员广播消息
                    alertNotification.Payload.Alert.Body = string.Format(item.Msg);//string.Format(msg);
                    alertNotification.Payload.Sound = "default";
                    alertNotification.Payload.CustomItems.Add("URL", new object[] { item.OpenUrl });
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 推送对应的相关事件
        void service_BadDeviceToken(object sender, BadDeviceTokenException exp)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);
            string errmsg = string.Format("Bad Device Token: {0}", exp.Message + exp.Source + exp.StackTrace) + ConnInfo;
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_BadDeviceToken", exp);
        }

        void service_Disconnected(object sender)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_Disconnected", ConnInfo);
        }

        void service_Connected(object sender)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_Connected", ConnInfo);
        }

        void service_Connecting(object sender)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_Connecting", ConnInfo);
        }

        void service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);
            string errmsg = string.Format("Notification Too Long: {0}", ex.Notification.ToString()) + ConnInfo;
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_NotificationTooLong", errmsg);
        }

        void service_NotificationSuccess(object sender, Notification notification)
        {
            try
            {
                object[] objPushID = notification.Payload.CustomItems["ID"];
                if (objPushID.Length > 0)
                {
                    string ID = objPushID[0].ConvertString();

                    // 记录推送日志，用来统计推送失败率
                   // SqlHelper.ExecuteNonQuery(PlatformConfig.DBConnectionString, CommandType.Text, "EXEC UserTokenPush_Rec " + ID);

                    Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_NotificationSuccess--记录推送日志，用来统计推送失败率", ID);
         
                }

            }
            catch (Exception exp)
            {
                Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_NotificationSuccess--处理错误", exp);
            }

            string ConnInfo = ReadNotificationConnectionInfo(sender);
            string errmsg = string.Format("Notification Success: {0}", notification.ToString()) + ConnInfo;
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Info, "事件：service_NotificationSuccess", errmsg);
        }

        void service_NotificationFailed(object sender, Notification notification)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);
            string errmsg = string.Format("Notification Failed: {0}", notification.ToString()) + ConnInfo;
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_NotificationSuccess", errmsg);
        }

        void service_Error(object sender, Exception exp)
        {
            string ConnInfo = ReadNotificationConnectionInfo(sender);

            //InitService();
            Logger.Write(LogTypeEnum.CommonLog.ToString(), LogCategoryType.Error, "事件：service_Error", exp.Message + exp.Source + exp.StackTrace + ConnInfo);
        }

        string ReadNotificationConnectionInfo(object sender)
        {
            string Msg = "";
            try
            {
                NotificationConnection objConn = (NotificationConnection)sender;
                Msg = "  ；p12File：" + objConn.Pub_p12File + "；  p12FilePassword：" + objConn.Pub_p12FilePassword;
            }
            catch (Exception exp)
            {
                Msg = "  ；" + exp.Message + exp.Source + exp.StackTrace;
            }
            return Msg;
        }
        #endregion
    }

    public class MsgInfo
    {
        public int ID { get; set; }

        public int MsgType { get; set; }

        public int ToUID { get; set; }

        public string DeviceToken { get; set; }

        public string Msg { get; set; }

        public int TotalNum { get; set; }

        public int MaxMsgID { get; set; }

        public string OpenUrl { get; set; }
    }


    public  class  NotificationPush
    {
        public UserDeviceTokenConfig csz { get; set; }

        public string  exp { get; set; }

            public string  title { get; set; }
            public string  contents  { get; set; }
            public string  activeurl { get; set; }
            public int  rid { get; set; }

        
	


    }
}
