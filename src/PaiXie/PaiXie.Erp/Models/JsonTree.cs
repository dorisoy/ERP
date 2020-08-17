using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
	
namespace PaiXie.Erp {
	public class JsonTree {
		private string _id;
		private string _text;
		private string _state = "open";
		private Dictionary<string, string> _attributes = new Dictionary<string, string>();
		private object _children;

		public string id {
			get { return _id; }
			set { _id = value; }
		}
		public string text {
			get { return _text; }
			set { _text = value; }
		}
		public string state {
			get { return _state; }
			set { _state = value; }
		}
		public Dictionary<string, string> attributes {
			get { return _attributes; }
			set { _attributes = value; }
		}
		public object children {
			get { return _children; }
			set { _children = value; }
		}
	}

	public class EasyUITree {
		public List<JsonTree> initTree(DataTable dt, string wherestr = "parentid=0", int order=0,int  iscodetype=0) {
			DataRow[] drList = order == 0 ? dt.Select(wherestr, "Id Asc") : dt.Select(wherestr); 
			List<JsonTree> rootNode = new List<JsonTree>();
			foreach (DataRow dr in drList) {
				JsonTree jt = new JsonTree();
				jt.id = dr["id"].ToString();
				jt.text = dr["TEXT"].ToString();
				jt.state = dr["state"].ToString();
				jt.attributes = CreateUrl(dt, jt, iscodetype);
				jt.children = CreateChildTree(dt, jt, iscodetype);
				rootNode.Add(jt);
			}
			return rootNode;
		}

		private List<JsonTree> CreateChildTree(DataTable dt, JsonTree jt, int iscodetype) {
			string  keyid = jt.id;                                        //根节点ID
			List<JsonTree> nodeList = new List<JsonTree>();
			DataRow[] children = dt.Select("Parentid='" + keyid + "'");
			foreach (DataRow dr in children) {
				JsonTree node = new JsonTree();
				node.id = dr["id"].ToString();
				node.text = dr["TEXT"].ToString();
				node.state = dr["state"].ToString();
				node.attributes = CreateUrl(dt, node, iscodetype);
				node.children = CreateChildTree(dt, node, iscodetype);
				nodeList.Add(node);
			}
			return nodeList;
		}


		private Dictionary<string, string> CreateUrl(DataTable dt, JsonTree jt, int iscodetype)    //把Url属性添加到attribute中，如果需要别的属性，也可以在这里添加
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			string keyid = jt.id;
			DataRow[] urlList = dt.Select("id='" + keyid + "'");

			string url = urlList[0]["attr"].ToString();
			dic.Add("attr", url);



			if(iscodetype==1)
			{	string IsEnable = urlList[0]["IsEnable"].ToString();
				dic.Add("IsEnable", IsEnable);

				string Description = urlList[0]["Description"].ToString();
				dic.Add("Description", Description);

			}
			
		
			
			return dic;
		}
	}
}