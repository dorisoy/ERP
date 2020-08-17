using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaiXie.Utils
{
	public class GetConfig
    {
        #region 构造函数

		private static GetConfig _instance;
		public static GetConfig GetInstance()
        {
            if (_instance == null)
            {
				_instance = new GetConfig();
            }
            return _instance;
        }

        #endregion
     
        #region  PageSize 分页数

       // public  const int PageSize = 20;
        private readonly int pagesize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pagesize"].ToString());

        /// <summary>
        /// The the pagesize 
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pagesize;
            }
        }
        #endregion
    }
}
