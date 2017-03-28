using BND.Services.Payments.eMandates.Business.Implementations;
using BND.Services.Payments.eMandates.Business.Interfaces;
using BND.Services.Payments.eMandates.Data.Context;
using BND.Services.Payments.eMandates.Data.Repositories;
using BND.Services.Payments.eMandates.Domain.Interfaces;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using eMandates.Merchant.Library;
using System;
using System.Configuration;
using System.Web.Http;
using config = eMandates.Merchant.Library.Configuration;

namespace BND.Services.Payments.eMandates.IoC
{
    /// <summary>
    /// Class ComponentInstaller.
    /// </summary>
    public class ComponentInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();

            config.Configuration eMandatesConfiguration = new config.Configuration(
                ConfigurationManager.AppSettings["eMandateContractId"],
                ConfigurationManager.AppSettings["merchantReturnUrl"],
                ConfigurationManager.AppSettings["signingCertificateFingerprint"],
                ConfigurationManager.AppSettings["acquirerCertificateFingerprint"],
                ConfigurationManager.AppSettings["acquirerUrlDirectoryReq"],
                ConfigurationManager.AppSettings["acquirerUrlTransactionReq"],
                ConfigurationManager.AppSettings["acquirerUrlStatusReq"],
                Convert.ToBoolean(ConfigurationManager.AppSettings["serviceLogsEnabled"]),
                ConfigurationManager.AppSettings["serviceLogsLocation"]
                );

            container.Register(

                // Registers all api controllers.
                // REMARK: This is done this way because we cannot add a reference to WebService assembly in this project (circular dependency)
                // Maybe we can use another way here?
                Classes.FromAssemblyNamed("BND.Services.Payments.eMandates.WebService").BasedOn<ApiController>().LifestylePerWebRequest(),

                Component.For<config.IConfiguration>()
                         .ImplementedBy<config.Configuration>()
                         .DependsOn(Dependency.OnValue("eMandateContractId", ConfigurationManager.AppSettings["eMandateContractId"]))
                         .DependsOn(Dependency.OnValue("merchantReturnUrl", ConfigurationManager.AppSettings["merchantReturnUrl"]))
                         .DependsOn(Dependency.OnValue("signingCertificateFingerprint", ConfigurationManager.AppSettings["signingCertificateFingerprint"]))
                         .DependsOn(Dependency.OnValue("acquirerCertificateFingerprint", ConfigurationManager.AppSettings["acquirerCertificateFingerprint"]))
                         .DependsOn(Dependency.OnValue("acquirerUrlDirectoryReq", ConfigurationManager.AppSettings["acquirerUrlDirectoryReq"]))
                         .DependsOn(Dependency.OnValue("acquirerUrlTransactionReq", ConfigurationManager.AppSettings["acquirerUrlTransactionReq"]))
                         .DependsOn(Dependency.OnValue("acquirerUrlStatusReq", ConfigurationManager.AppSettings["acquirerUrlStatusReq"]))
                         .DependsOn(Dependency.OnValue("serviceLogsEnabled", Convert.ToBoolean(ConfigurationManager.AppSettings["serviceLogsEnabled"])))
                         .DependsOn(Dependency.OnValue("serviceLogsLocation", ConfigurationManager.AppSettings["serviceLogsLocation"]))
                         .LifestylePerWebRequest(),
                Component.For<CoreCommunicator>()
                         .DependsOn(Dependency.OnValue("configuration", eMandatesConfiguration))
                         .LifestylePerWebRequest(),
                Component.For<IEMandatesClient>()
                         .ImplementedBy<EMandatesClient>()
                         .LifestylePerWebRequest(),
                Component.For<IUnitOfWork>()
                         .ImplementedBy<EMandateUnitOfWork>()
                         .DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["eMandates"].ToString()))
                         .LifestylePerWebRequest(),
                Component.For<IDirectoryRepository>()
                         .ImplementedBy<DirectoryRepository>()
                         .LifestylePerWebRequest(),
                Component.For<ITransactionRepository>()
                         .ImplementedBy<TransactionRepository>()
                         .LifestylePerWebRequest(),
                Component.For<ISettingRepository>()
                         .ImplementedBy<SettingRepository>()
                         .LifestylePerWebRequest(),
                Component.For<ILogRepository>()
                         .ImplementedBy<LogRepository>()
                         .LifestylePerWebRequest(),
                Component.For<ILogger>()
                         .ImplementedBy<Logger>()
                         .DependsOn(Dependency.OnValue("appName", ConfigurationManager.AppSettings["AppName"]))
                         .LifestylePerWebRequest(),
                Component.For<IEMandatesManager>()
                         .ImplementedBy<EMandatesManager>()
                         .LifestylePerWebRequest()

            );
        }
    }
}