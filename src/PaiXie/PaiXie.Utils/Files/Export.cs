using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Utils {
	/// <summary>
	/// 导出报表
	/// </summary>
	public class Export {
		/// <summary>
		/// 定义一个静态的哈希表，存储下载的进度
		/// </summary>
		public static Hashtable ht_Down_Task_Progress = new Hashtable();
		
		/// <summary>
		/// 添加进度，每生成一条数据，进度+1
		/// </summary>
		/// <param name="TaskId">任务ID</param>
		public static void add_Down_Task_Progress(string TaskId) {
			ht_Down_Task_Progress[TaskId] = ZConvert.StrToInt(ht_Down_Task_Progress[TaskId], 0) + 1;
		}
		
		/// <summary>
		/// 读取任务进度
		/// </summary>
		/// <param name="TaskId">任务ID</param>
		/// <returns>返回进度</returns>
		public static int get_Down_Task_Progress(string TaskId) {
			return ZConvert.StrToInt(ht_Down_Task_Progress[TaskId], 0);
		}
		
		/// <summary>
		/// 读取该任务总的记录数
		/// </summary>
		/// <param name="TaskId"></param>
		public static int get_Down_Task_Total(string TaskId) {
			return ZConvert.StrToInt(ht_Down_Task_Progress[TaskId + "_Total"], 0);
		}
		
		/// <summary>
		/// 释放内存
		/// </summary>
		/// <param name="TaskId">任务ID</param>
		public static void clear_Down_Task_Progress(string TaskId) {
			ht_Down_Task_Progress.Remove(TaskId);
		}
		
		/// <summary>
		/// 释放内存
		/// </summary>
		/// <param name="TaskId">任务ID</param>
		public static void clear_Down_Task_Total(string TaskId) {
			ht_Down_Task_Progress.Remove(TaskId + "_Total");
		}
	}
}
