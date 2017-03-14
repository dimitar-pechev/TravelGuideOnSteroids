using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Web.Common;
using TravelGuide.Data;
using TravelGuide.Data.Contracts;
using TravelGuide.Services;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Factories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TravelGuide.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TravelGuide.App_Start.NinjectConfig), "Stop")]

namespace TravelGuide.App_Start
{
    public static class NinjectConfig
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITravelGuideContext>().To<TravelGuideContext>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IArticleService>().To<ArticleService>();

            kernel.Bind<IArticleFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IArticleCommentFactory>().ToFactory().InSingletonScope();
        }
    }
}
