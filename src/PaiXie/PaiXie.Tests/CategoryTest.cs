using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaiXie.Service;
using PaiXie.Data;
using PaiXie.Core;
using System.Collections.Generic;
using PaiXie.Api.Bll;
using System.Linq;

namespace PaiXie.Tests {
	[TestClass]
	public class CategoryTest {
		[TestMethod]
		public void TestDel() {
			List<int> idList = new List<int>();
			idList.Add(1);
			BaseResult resultInfo = CategoryManager.Del("admin", idList);
			Assert.AreEqual(1, resultInfo.result, resultInfo.message);
		}

		public class PickItem {
			public string BillNo { get; set; }
			public string ProductsCode { get; set; }
			public int OrderItemID { get; set; }
			public int Num { get; set; }
		}
	}
}

