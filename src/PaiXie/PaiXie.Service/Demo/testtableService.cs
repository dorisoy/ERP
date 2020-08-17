using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;

namespace PaiXie.Service {
	public class testtableService {

		public static testtable GetQuerySingle(string sqlStr) {
			return testtableRepository.GetInstance().GetQuerySingle(sqlStr);
		}

		public static List<testtable>  GetQueryMany(string sqlStr) {
			return testtableRepository.GetInstance().GetQueryMany(sqlStr);
		}

		public static int GetCount(string sqlStr) {
			return testtableRepository.GetInstance().GetCount(sqlStr);
		}

		public static int Update(string sqlStr) {
			return testtableRepository.GetInstance().Update(sqlStr);
		}
		public static int Update(testtable entity) {
			return testtableRepository.GetInstance().Update(entity);
		}

		public static int Del(string sqlStr) {
			return testtableRepository.GetInstance().Del(sqlStr);
		}

		public static int Add(testtable entity) {
			return testtableRepository.GetInstance().Add(entity);
		}

		public static List<testtable> GetQueryManyForPage(SelectBuilder data, out int count) {
			return testtableRepository.GetInstance().GetQueryManyForPage( data,out  count);
		}
	
	}
}
