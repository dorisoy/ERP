using SecretHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CreateSysConfig {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void btnSelectFile_Click(object sender, EventArgs e) {
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "|*.config";
			dlg.Multiselect = false;
			dlg.InitialDirectory = @"E:\erpnet\src\PaiXie\PaiXie.Erp\Config\Custom";
			if (dlg.ShowDialog() == DialogResult.OK) {
				txtSrc.Text = dlg.FileName;
				string xmlStr = File.ReadAllText(dlg.FileName, Encoding.Default);
				try {
					XDocument.Parse(xmlStr);
				}
				catch {
					try {
						//兼容已加密文件不绑定mac
						xmlStr = SecretAuth.DeAuth(xmlStr, true);
					}
					catch {
						//兼容已加密文件绑定mac
						xmlStr = SecretAuth.DeAuth(xmlStr);
					}
				}
				//解析配置信息
				if (xmlStr.Length > 10) {
					XDocument data = XDocument.Parse(xmlStr);
					XElement xe = data.Root.Element("setting");

					for (var i = 0; i < xe.Elements("item").Count(); i++) {
						var xeItem = xe.Elements("item").ToArray()[i];

						string val = xeItem.Attribute("value").Value;
						string key = xeItem.Attribute("key").Value.Trim();
						switch (key) {
							case "SystemTitle":
								txtSystemTitle.Text = val;
								break;
							case "SystemVersion":
								txtSystemVersion.Text = val;
								break;
							case "InstallTime":
								txtInstallTime.Text = "" + val;
								break;
							case "LastModifyTime":
								txtLastModifyTime.Text = "" + val;
								break;
							case "IsSingleWarehouse":
								if (val.ToLower() == "true") {
									rbIsSingleWarehouse1.Checked = true;
									rbIsSingleWarehouse2.Checked = false;
								}
								else {
									rbIsSingleWarehouse2.Checked = true;
									rbIsSingleWarehouse1.Checked = false;
								}
								break;
							default:
								break;
						}
					}
				}
			}
		}

		private void btnCreate_Click(object sender, EventArgs e) {
			string _filePath = txtSrc.Text.Trim();
			string systemTitle = txtSystemTitle.Text.Trim();
			string systemVersion = txtSystemVersion.Text.Trim();
			string installTime = txtInstallTime.Text;
			string lastModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string isSingleWarehouse = "true";
			if (rbIsSingleWarehouse2.Checked) {
				isSingleWarehouse = "false";
			}
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement root = xmlDoc.CreateElement("root");
			xmlDoc.AppendChild(root);
			XmlElement setting = xmlDoc.CreateElement("setting");
			root.AppendChild(setting);
			XmlElement item1 = xmlDoc.CreateElement("item");
			item1.SetAttribute("key", "SystemTitle");
			item1.SetAttribute("value", systemTitle);
			setting.AppendChild(item1);

			XmlElement item2 = xmlDoc.CreateElement("item");
			item2.SetAttribute("key", "IsSingleWarehouse");
			item2.SetAttribute("value", isSingleWarehouse);
			setting.AppendChild(item2);

			XmlElement item3 = xmlDoc.CreateElement("item");
			item3.SetAttribute("key", "SystemVersion");
			item3.SetAttribute("value", systemVersion);
			setting.AppendChild(item3);

			XmlElement item4 = xmlDoc.CreateElement("item");
			item4.SetAttribute("key", "InstallTime");
			item4.SetAttribute("value", installTime);
			setting.AppendChild(item4);

			XmlElement item5 = xmlDoc.CreateElement("item");
			item5.SetAttribute("key", "LastModifyTime");
			item5.SetAttribute("value", lastModifyTime);
			setting.AppendChild(item5);

			string xmlStr = xmlDoc.InnerXml;
			string xmlEn = string.Empty;
			if (rbIsTest2.Checked) {
				//正式环境，带mac加密
				xmlEn = SecretAuth.EnAuth(xmlStr);
				File.WriteAllText(_filePath, xmlEn);
				MessageBox.Show("生成成功");
			}
			else if (rbIsTest1.Checked) {
				//测试环境，不带mac加密
				xmlEn = SecretAuth.EnAuth(xmlStr, true);
				File.WriteAllText(_filePath, xmlEn);
				MessageBox.Show("生成成功");
			}
			else {
				MessageBox.Show("请选择是否测试环境");
			}
		}
	}
}
