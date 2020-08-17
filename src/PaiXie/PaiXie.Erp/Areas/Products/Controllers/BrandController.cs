using PaiXie.Api.Bll;
using PaiXie.Core;
using PaiXie.Data;
using PaiXie.Service;
using PaiXie.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace PaiXie.Erp.Areas.Products
{
    public class BrandController : BaseController
    {
		#region Index

		public ActionResult Index() {
			return View();
		}

		#endregion

		#region 品牌列表

		/// <summary>
		/// 品牌列表
		/// </summary>
		/// <returns></returns>
		public ActionResult Search() {
			//   Json格式的要求{total:22,rows:{}}
			int pageIndex = ZConvert.StrToInt(Request["page"], 1);
			int pageSize = ZConvert.StrToInt(Request["rows"], ZConfig.GetConfigInt("pagesize"));
			SelectBuilder data = new SelectBuilder();
			data.Having = "";
			data.GroupBy = "";
			data.OrderBy = "Seq ASC,ID DESC";
			data.From = @"brand";
			data.Select = "ID, Code, Name, Remark";
			data.WhereSql = "";
			data.PagingCurrentPage = pageIndex;
			data.PagingItemsPerPage = pageSize;
			int total = 0;
			List<Brand> list = BrandService.GetQueryManyForPage(data, out total);
			//   构造成Json的格式传递
			var result = new { total = total, rows = list };
			return JsonDate(result);
		}

		#endregion

		#region 品牌编辑

		public ActionResult Edit(int id) {
			Brand objBrand = new Brand();
			if (id > 0) objBrand = BrandService.GetSingleBrand(id);
			ViewBag.Brand = objBrand;
			return View();
		}

		#endregion

		#region 保存

		[HttpPost]
		public ActionResult Save(Brand obj) {
			string userCode = FormsAuth.GetUserCode();
			BaseResult resultInfo = BrandManager.Save(userCode, obj);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 删除

		public ActionResult Delete(string ids) {
			string userCode = FormsAuth.GetUserCode();
			List<int> idList = new List<int>();
			idList.AddRange(ids.Split(',').Select(id => ZConvert.StrToInt(id)).Where(id => id > 0));
			BaseResult resultInfo = BrandManager.Del(userCode, idList);
			return JsonDate(resultInfo);
		}

		#endregion

		#region 检查品牌名称唯一性

		public ActionResult CheckName(string name, int id) {
			BaseResult BaseResult = new BaseResult();
			if (id > 0) {
				if (BrandService.GetBrandID(name, id) > 0) {
					BaseResult.result = -1;
				}

			}
			else {
				if (BrandService.GetBrandID(name) > 0) {
					BaseResult.result = -1;
				}
			}
			return JsonDate(BaseResult);
		}
		#endregion
    }
}
