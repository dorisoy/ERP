using System;
using System.ServiceProcess;
using System.Threading;
using PaiXie.Utils;

namespace PaiXie.WinService
{
    public partial class TimingService : ServiceBase
    {
		//自动下载订单
		private Thread _AutoDownOrderThread;
		//自动生成订单
		private Thread _AutogenerationOrderThread;
		//自动删除库位商品
		private Thread _AutoDeleteLocationProductsThread;
       
		public TimingService()
        {
            InitializeComponent();
        }

        #region 进程的主入口点

        // 进程的主入口点
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
            // 同一进程中可以运行多个用户服务。若要将
            // 另一个服务添加到此进程，请更改下行
            // 以创建另一个服务对象。例如，
            //
            //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
            //
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new TimingService() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        #endregion

        #region 服务启动

        protected override void OnStart(string[] args)
        {
			string errmsg = "启动定时服务,";
            try
            {
				_AutoDownOrderThread = new Thread(new ThreadStart(new TimingService().AutoDownOrder));
				_AutoDownOrderThread.Start();
				errmsg += "自动下载订单线程启动,";

				_AutogenerationOrderThread = new Thread(new ThreadStart(new TimingService().AutogenerationOrder));
				_AutogenerationOrderThread.Start();
				errmsg += "自动生成订单线程启动,";

				_AutoDeleteLocationProductsThread = new Thread(new ThreadStart(new TimingService().AutoDeleteLocationProducts));
				_AutoDeleteLocationProductsThread.Start();
				errmsg += "自动删除库位商品线程启动,";

				common.WriteLog(errmsg, LogType.General.ToString());
            }
            catch (Exception ex)
            {
				errmsg += "启动定时服务错误:" + ex.ToString();
				common.WriteLog(errmsg, LogType.General.ToString());

            }
        }

        #endregion

        #region 服务停止

        protected override void OnStop()
        {
			string errmsg = "停止定时服务,";
           
			if (_AutoDownOrderThread != null) {
				_AutoDownOrderThread.Abort();
			}
			errmsg += "自动下载订单线程停止,";

			if (_AutogenerationOrderThread != null) {
				_AutogenerationOrderThread.Abort();
			}
			errmsg += "自动生成订单线程停止,";

			if (_AutoDeleteLocationProductsThread != null) {
				_AutoDeleteLocationProductsThread.Abort();
			}
			errmsg += "自动删除库位商品线程停止,";

			common.WriteLog(errmsg, LogType.General.ToString());
        }

        #endregion

		#region 自动下载订单

		/// <summary>
		/// 自动下载订单
		/// </summary>
		public void AutoDownOrder() {
			try {
				SysOrder objSysPush = new SysOrder();
				objSysPush.AutoDownOrder();
			}
			catch (Exception ex) {
				common.WriteLog(ex.ToString(), LogType.General.ToString());
			}
		}

		#endregion

		#region 自动生成订单

		/// <summary>
		/// 自动生成订单
		/// </summary>
		public void AutogenerationOrder() {
			try {
				SysOrder objSysPush = new SysOrder();
				objSysPush.AutogenerationOrder();
			}
			catch (Exception ex) {
				common.WriteLog(ex.ToString(), LogType.General.ToString());
			}
		}

		#endregion

		#region 自动删除库位商品

		/// <summary>
		/// 自动删除库位商品
		/// </summary>
		public void AutoDeleteLocationProducts() {
			try {
				SysLocationProducts objSysDelete = new SysLocationProducts();
				objSysDelete.AutoDeleteLocationProducts();
			}
			catch (Exception ex) {
				common.WriteLog(ex.ToString(), LogType.General.ToString());
			}
		}

		#endregion
    }
}
