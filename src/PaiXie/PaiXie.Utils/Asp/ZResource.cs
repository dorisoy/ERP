using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
//using System.Drawing;
using System.Resources;


namespace PaiXie.Utils
{

    public static class ZResource
    {

        private static ResourceManager m_resourceManager;
    
        static ZResource()
		{
            m_resourceManager = new ResourceManager("ResourceNamespace.myResources", typeof(ZResource).Assembly);

            //myString = myManager.GetString("StringResource");
            //myImage = (System.Drawing.Image)myManager.GetObject("ImageResource");
		}

        public static string GetMessage(string MessageCode)
		{
            string FieldName="";
            if (MessageCode == null || MessageCode.Trim().Equals("")){return "";}
            try{FieldName = HttpContext.GetGlobalResourceObject("Message", MessageCode).ToString();}
            catch{FieldName = MessageCode;}
            return FieldName;
		}

        public static string GetMessage(string MessageCode,params object[] Parms)
        {
            string msg = GetMessage(MessageCode);
            if (Parms != null) msg = String.Format(msg, Parms);
            return msg;
        }

        public static string GetResource(string Resource,string Field)
        {
            //return m_resourceManager.GetString(name);
            //return HttpContext.GetGlobalResourceObject("ResMessage", FieldCode).ToString();
            string FieldName = "";
            try { FieldName = HttpContext.GetGlobalResourceObject(Resource, Field).ToString();}
            catch {FieldName=Field; }
            return FieldName;
        }

    }
}
