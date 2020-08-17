using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
 	public class studentService {

		public static student GetQuerySingle(string sqlStr) {
			return studentRepository.GetInstance().GetQuerySingle(sqlStr);
		}

		public static List<student>  GetQueryMany(string sqlStr) {
			return studentRepository.GetInstance().GetQueryMany(sqlStr);
		}

		public static List<studentList> GetQueryManyList(string sqlStr) {
			return studentRepository.GetInstance().GetQueryManyList(sqlStr);
		}





		public static DataTable GetDataTable(string sqlStr) {
			return studentRepository.GetInstance().GetDataTable(sqlStr);
		}

		public static int GetCount(string sqlStr) {
			return studentRepository.GetInstance().GetCount(sqlStr);
		}

		public static int Update(string sqlStr) {
			return studentRepository.GetInstance().Update(sqlStr);
		}
		public static int Update(student entity) {
			return studentRepository.GetInstance().Update(entity);
		}

		public static int Del(string sqlStr) {
			return studentRepository.GetInstance().Del(sqlStr);
		}

		public static int Add(student entity) {
			return studentRepository.GetInstance().Add(entity);
		}

		public static List<student> GetQueryManyForPage(SelectBuilder data, out int count) {
			return studentRepository.GetInstance().GetQueryManyForPage( data,out  count);
		}

		public static List<studentList> GetQueryManyForPageList(SelectBuilder data, out int count) {
			BaseRepository<studentList> obj = new BaseRepository<studentList>();
			return obj.GetQueryManyForPage(data, out  count);
		}
	
	}
}





