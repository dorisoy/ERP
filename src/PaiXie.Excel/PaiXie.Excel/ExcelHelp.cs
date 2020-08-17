using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using PaiXie.Excel.Shared;
namespace PaiXie.Excel {
	public class ExcelHelp : IDisposable {
		private static IExportMin _exportMin;
		private static IImportMin _importMin;
		public static IExportMin exportMin {
			get {
				if (ExcelHelp._exportMin == null) {
					ExcelHelp._exportMin = Microsoft.Practices.Unity.UnityContainerExtensions.Resolve<IExportMin>(ExcelHelp.InitContainer(), new ResolverOverride[0]);
				}
				return ExcelHelp._exportMin;
			}
		}
		public static IImportMin importMin {
			get {
				if (ExcelHelp._importMin == null) {
					ExcelHelp._importMin = Microsoft.Practices.Unity.UnityContainerExtensions.Resolve<IImportMin>(ExcelHelp.InitContainer(), new ResolverOverride[0]);
				}
				return ExcelHelp._importMin;
			}
		}
		private static IUnityContainer InitContainer() {
			IUnityContainer unityContainer = new UnityContainer();
			UnityConfigurationSection unityConfigurationSection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
			Microsoft.Practices.Unity.Configuration.UnityContainerExtensions.LoadConfiguration(unityContainer, unityConfigurationSection, "ExcelContainer");
			return unityContainer;
		}
		public void Dispose() {
			ExcelHelp._exportMin = null;
		}
	}
}
