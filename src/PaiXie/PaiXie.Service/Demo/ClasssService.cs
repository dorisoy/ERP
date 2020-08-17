using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class classsService {

		public static classs GetQuerySingle(string sqlStr) {
			return classsRepository.GetInstance().GetQuerySingle(sqlStr);
		}

		public static List<classs>  GetQueryMany(string sqlStr) {
			return classsRepository.GetInstance().GetQueryMany(sqlStr);
		}

		public static DataTable GetDataTable(string sqlStr) {
			return studentRepository.GetInstance().GetDataTable(sqlStr);
		}

		public static int GetCount(string sqlStr) {
			return classsRepository.GetInstance().GetCount(sqlStr);
		}

		public static int Update(string sqlStr) {
			return classsRepository.GetInstance().Update(sqlStr);
		}
		public static int Update(classs entity) {
			return classsRepository.GetInstance().Update(entity);
		}

		public static int Del(string sqlStr) {
			return classsRepository.GetInstance().Del(sqlStr);
		}

		public static int Add(classs entity) {
			return classsRepository.GetInstance().Add(entity);
		}

		public static List<classs> GetQueryManyForPage(SelectBuilder data, out int count) {
			return classsRepository.GetInstance().GetQueryManyForPage( data,out  count);
		}
	
	}
}





