using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Data {
	public class SysroleInfo : Sysrole {
		//创建人
		public string cname { set; get; }
		//修改人
		public string uname { set; get; }
		//扩展字段1
		public string part1 { set; get; }
	}
}
