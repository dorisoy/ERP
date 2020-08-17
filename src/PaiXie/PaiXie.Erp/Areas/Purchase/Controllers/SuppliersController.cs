using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaiXie.Erp.Areas.Purchase
{
	public class SuppliersController : BaseController
    {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 供应商列表

		public ActionResult Search() {
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			string whereSql = GetWhereSql();
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "supp.ID DESC";
			data.From = @"suppliers supp";
			data.Select = @" supp.ID,supp.Name,supp.AliasName,supp.ContactPerson,supp.Tel,supp.Fax,supp.Email,
(SELECT COUNT(DISTINCT ProductsID) FROM suppliersItem WHERE suppliersID=supp.ID) AS ProductsCount";
			data.WhereSql = whereSql;
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<SuppliersList> list = BaseService<SuppliersList>.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);

		}

		private string GetWhereSql() {
			string keyWordType = ZConvert.ToString(Request["keyWordType"]);
			string keyWord = ZConvert.ToString(Request["keyWord"]);
			string whereSql = string.Format("supp.IsEnable={0}", (int)IsEnable.是);

			if (keyWord != "") {
				switch (keyWordType) {
					case "供应商名称":
						whereSql += string.Format(" and supp.Name like '%{0}%'", keyWord);
						break;
					case "供应商简称":
						whereSql += string.Format(" and supp.AliasName like '%{0}%'", keyWord);
						break;
					case "供应商电话":
						whereSql += string.Format(" and supp.Tel like '%{0}%'", keyWord);
						break;
					case "联系人":
						whereSql += string.Format(" and supp.ContactPerson like '%{0}%'", keyWord);
						break;
					case "商品编码":
						whereSql += string.Format(" and supp.ID IN (SELECT SuppliersID FROM suppliersItem Where ProductsCode like '%{0}%')", keyWord);
						break;
				}
			}
			return whereSql;
		}

		#endregion

		#region 供应商编辑

		public ActionResult Edit(int id) {
			Suppliers objSuppliers = new Suppliers();
			if (id > 0) objSuppliers = SuppliersService.GetQuerySingleByID(id);
			ViewBag.Suppliers = objSuppliers;
			return View();
		}

		#endregion

		#region 保存

		[HttpPost]
		public ActionResult Save(Suppliers obj) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = SuppliersManager.AddSuppliers(userCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = SuppliersManager.DeleteSuppliers(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 检查供应商名称唯一性

		/// <summary>
		/// 检查供应商名称唯一性
		/// </summary>
		/// <param name="name">供应商名称</param>
		/// <param name="id">供应商ID</param>
		/// <returns></returns>
		public ActionResult CheckName(string name, int id) {
			BaseResult BaseResult = new BaseResult();
			if (id > 0) {
				if (SuppliersService.GetIDByName(name, id) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (SuppliersService.GetIDByName(name) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}

		#endregion

		#region 检查供应商简称唯一性

		/// <summary>
		/// 检查供应商简称唯一性
		/// </summary>
		/// <param name="name">供应商简称</param>
		/// <param name="id">供应商ID</param>
		/// <returns></returns>
		public ActionResult CheckAliasName(string aliasName, int id) {
			BaseResult BaseResult = new BaseResult();
			if (id > 0) {
				if (SuppliersService.GetIDByAliasName(aliasName, id) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (SuppliersService.GetIDByAliasName(aliasName) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}

		#endregion
	}
}
