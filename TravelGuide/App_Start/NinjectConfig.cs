using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Web.Common;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Data;
using TravelGuide.Data.Contracts;
using TravelGuide.Services;
using TravelGuide.Services.Account.Contracts;
using TravelGuide.Services.Articles;
using TravelGuide.Services.Articles.Contracts;
using TravelGuide.Services.Factories;
using TravelGuide.Services.Gallery;
using TravelGuide.Services.Gallery.Contacts;
using TravelGuide.Services.Requests;
using TravelGuide.Services.Requests.Contracts;
using TravelGuide.Services.Store;
using TravelGuide.Services.Store.Contracts;

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

            kernel.Bind<IMappingService>().To<MappingService>();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<IStoreService>().To<StoreService>();
            kernel.Bind<ICartService>().To<CartService>();
            kernel.Bind<IRequestService>().To<RequestService>();
            kernel.Bind<IGalleryImageService>().To<GalleryImageService>();

            kernel.Bind<IArticleFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IArticleCommentFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IStoreItemFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IRequestFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IGalleryImageFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IGalleryLikeFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IGalleryCommentFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IStoryFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IStoryCommentFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IStoryLikeFactory>().ToFactory().InSingletonScope();
        }
    }
}
