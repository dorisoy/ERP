using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
using FluentData;
namespace  PaiXie.Service 
{
	public class SysareaService : BaseService<Sysarea> {

		public static int Update(Sysarea entity) {
			return SysareaRepository.GetInstance().Update(entity);
		}



		public static int Add(Sysarea entity) {
			return SysareaRepository.GetInstance().Add(entity);
		}
		/// <summary>
		/// 获取区域
		/// </summary>
		/// <returns></returns>
		public static DataTable GetDataTable() {
			return SysareaRepository.GetInstance().GetDataTable();
		}
		/// <summary>
		/// 区域管理列表
		/// </summary>
		/// <returns></returns>
		public static DataTable GetareaDataTable() {
			return SysareaRepository.GetInstance().GetareaDataTable();
		}
		/// <summary>
		/// 删除区域
		/// </summary>
		/// <param name="id">主键id</param>
		/// <param name="level">等级  0  省级  1  市级  2  区级</param>
		/// <returns></returns>
		public static int DelArea(int id, int level) {
			return SysareaRepository.GetInstance().DelArea(id, level);
		}
		/// <summary>
		/// 获取区域
		/// </summary>
		/// <param name="id">主键id</param>
		/// <returns></returns>
		public static Sysarea GetArea(int id) {
			return SysareaRepository.GetInstance().GetArea(id);
		}

		/// <summary>
		/// 恢复初始化区域设置
		/// </summary>
		/// <returns></returns>
		public static int initArea() {
			return SysareaRepository.GetInstance().initArea();
		}

		/// <summary>
		/// 编辑区域
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="id">主键id</param>
		/// <returns></returns>
		public static int EditArea(string name, int id) {
			return SysareaRepository.GetInstance().EditArea(name, id);
		}

		#region 获取大区列表

		/// <summary>
		/// 获取大区列表
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Sysarea> GetLargeAreaList(IDbContext context = null) {
			return SysareaRepository.GetInstance().GetLargeAreaList(context);
		}

		#endregion

		#region 根据大区名称获取省份列表

		/// <summary>
		/// 根据大区名称获取省份列表
		/// </summary>
		/// <param name="largeAreaName">大区名称</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Sysarea> GetProvinceList(string largeAreaName, IDbContext context = null) {
			return SysareaRepository.GetInstance().GetProvinceList(largeAreaName, context);
		}

		#endregion

		#region 根据省份ID获取城市列表

		/// <summary>
		/// 根据省份ID获取城市列表
		/// </summary>
		/// <param name="provinceID">省份ID</param>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static List<Sysarea> GetCityList(int provinceID, IDbContext context = null) {
			return SysareaRepository.GetInstance().GetCityList(provinceID, context);
		}

		#endregion

		#region 获取所有省市区街道

		/// <summary>
		/// 获取所有省市区街道
		/// </summary>
		/// <param name="context">数据库连接对象</param>
		/// <returns></returns>
		public static DataTable GetManySysarea(IDbContext context = null) {
			return SysareaRepository.GetInstance().GetManySysarea(context);
		}

		#endregion
	}
}