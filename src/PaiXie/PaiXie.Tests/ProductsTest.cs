using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaiXie.Service;
using PaiXie.Data;
using PaiXie.Core;
using System.Collections.Generic;
using PaiXie.Api.Bll;

namespace PaiXie.Tests {
	[TestClass]
	public class ProductsTest {
		[TestMethod]
		public void TestSave() {
			ProductsInfo productsInfo = new ProductsInfo();
			Products products = ProductsService.GetQuerySingle("Select * from products where ID=1");
			products.Code = "test02";
			products.No = "test02";
			products.Name = "test02";
			products.UpdateDate = DateTime.Now;
			products.UpdatePerson = "sheng.hao";
			productsInfo.Products = products;
			ProductsSku productSku = ProductsSkuService.GetQuerySingle("Select * from productsSku where Id=1");
			productSku.ProductsCode = "test02";
			productSku.UpdateDate = DateTime.Now;
			productSku.UpdatePerson = "sheng.hao";
			productsInfo.ProductsSkuList.Add(productSku);
			//ProductsSku productsSku2 = new ProductsSku();
			//productsSku2.ProductsCode = "test01";
			//productsSku2.Code = "test0102";
			//productsSku2.CreateDate = DateTime.Now;
			//productsSku2.CreatePerson = "sheng.hao";
			//productsSku2.Saleprop = "颜色：红色,尺码：41";
			//productsInfo.ProductsSkuList.Add(productsSku2);

			//ProductsSku productsSku3 = new ProductsSku();
			//productsSku3.ProductsCode = "test01";
			//productsSku3.Code = "test0103";
			//productsSku3.CreateDate = DateTime.Now;
			//productsSku3.CreatePerson = "sheng.hao";
			//productsSku3.Saleprop = "颜色：红色,尺码：42";
			//productsInfo.ProductsSkuList.Add(productsSku3);
			string userCode = FormsAuth.GetUserCode();
			string position = "ProductsTest/TestSave";
			string buttonName = "保存商品";
			string target = "单元测试";
			bool isUpdate = false;
			BaseResult resultInfo = ProductsManager.Save(userCode, position, target, buttonName, productsInfo, isUpdate);
			Assert.AreEqual(1, resultInfo.result, resultInfo.message);
		}
	}
}

