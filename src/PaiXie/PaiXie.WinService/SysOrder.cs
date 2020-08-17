using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using PaiXie.Api.Bll;
namespace PaiXie.WinService
{
    public class SysOrder
    {
        //间隔5秒
		public int autoDownOrderIntervalSecond = 1000 * ZConvert.StrToInt(ConfigurationManager.AppSettings["AutoDownOrderIntervalSecond"]);

		#region  自动下载订单

		public void AutoDownOrder() {
			while (true) {
				AutoDownOrderManager.AutoDownOrderTask();
				Thread.Sleep(autoDownOrderIntervalSecond);
			}
		}

		#endregion

		#region  自动生成订单

		public void AutogenerationOrder() {
			while (true) {
				AutogenerationOrderManager.AutogenerationTask();
				Thread.Sleep(autoDownOrderIntervalSecond);
			}
		}

		#endregion
    }
}