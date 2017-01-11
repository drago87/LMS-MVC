using System;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.AspNet.Identity;
using Unity.WebApi;
using Queries.Core;
using Queries.Persistence;
using Queries.Persistence.Repositories;
using Queries.Core.Domain;
using Queries.Core.Repositories;
using Queries.Core.Models;
using bat_mvc.Controllers;
using bat_mvc.Controllers.api;

namespace bat_mvc.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // container.LoadConfiguration(); // To load from web.config. add using Microsoft.Practices.Unity.Configuration

            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());

            //container.RegisterType<IUserStore<ApplicationUser>, ApplicationUserManager>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Subject>,   Repository<Subject>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Lesson>,    Repository<Lesson>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<ClassUnit>, Repository<ClassUnit>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Folder>,    Repository<Folder>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Dossier>,   Repository<Dossier>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<ApplicationUser>,   Repository<ApplicationUser>>(new PerRequestLifetimeManager());

            container.RegisterType<IFolderRepository, FolderRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new PerRequestLifetimeManager());
        }

        // for WEBAPI
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Subject>,   Repository<Subject>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Lesson>,    Repository<Lesson>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<ClassUnit>, Repository<ClassUnit>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Folder>,    Repository<Folder>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<Dossier>,   Repository<Dossier>>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<ApplicationUser>,   Repository<ApplicationUser>>(new PerRequestLifetimeManager());

            //container.RegisterType<IFolderRepository, FolderRepository>(new PerRequestLifetimeManager());
            //container.RegisterType<IUserRepository, UserRepository>(new PerRequestLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
