using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaiXie.Data;
using System.Data;
namespace  PaiXie.Service 
{
	public class TreeService : BaseService<Tree> {
    
		public static int Update(Tree entity) {
			return TreeRepository.GetInstance().Update(entity);
		}
		public static int Add(Tree entity) {
			return TreeRepository.GetInstance().Add(entity);
		}

	
	
	}
}





