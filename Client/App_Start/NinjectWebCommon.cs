[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Client.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Client.App_Start.NinjectWebCommon), "Stop")]

namespace Client.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MusicRepository;
    using MusicRepository.Interfaces;
    using MusicRepository.Repositories;
    using MusicServices.APITools.Interfaces;
    using MusicServices.APITools.Services;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
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
                GlobalConfiguration.Configuration.DependencyResolver = kernel.Get<IDependencyResolver>();
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
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ITrackService>().To<TrackService>();
            kernel.Bind<IService<DomainModel.Track>>().To<TrackService>();
            kernel.Bind<IService<DomainModel.User>>().To<UserService>();
            kernel.Bind<IService<DomainModel.Tag>>().To<TagService>();
            kernel.Bind<IService<DomainModel.Genre>>().To<GenreService>();
            kernel.Bind<IService<DomainModel.Comment>>().To<CommentService>();
            kernel.Bind<ITrackRepository>().To<TrackRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IGenreRepository>().To<GenreRepository>();
            kernel.Bind<ITagRepository>().To<TagRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
