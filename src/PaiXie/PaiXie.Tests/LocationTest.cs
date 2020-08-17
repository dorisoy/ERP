using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaiXie.Service;
using PaiXie.Data;
using PaiXie.Api.Bll;
using PaiXie.Core;

namespace PaiXie.Tests {
	[TestClass]
	public class LocationTest {
		[TestMethod]
		public void TestSave() {
			WarehouseLocationInfo obj = new WarehouseLocationInfo();
			obj.Name = "DDD区";
			obj.Code = "DDDq";
			obj.TypeID = (int)LocationType.发货区;
			obj.StructCount = new int[] { 1, 1, 10, 10, 10 };
			obj.StructName = new string[] { "行", "排", "组", "层", "位" };
			obj.StructCode = new string[] { "H", "P", "Z", "C", "W" };
			string userCode = "admin";
			string warehouseCode = "001";
			string position = "LocationTest/TestSave";
			string buttonName = "保存库区测试";
			string target = "保存库区测试";
			BaseResult resultInfo = LocationManager.Save(userCode, warehouseCode, position, target, buttonName, obj);
			Assert.AreEqual(1, resultInfo.result, resultInfo.message);
		}
	}
}
