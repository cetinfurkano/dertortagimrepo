using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DertOrtagim.Business.Abstract;
using DertOrtagim.Business.Managers;
using DertOrtagim.DataAccess.Abstract;
using DertOrtagim.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ManagerLoads(builder);
            EntityFrameworkLoads(builder);
            UtilitiesLoads(builder);
            ExternelResourcesLoad(builder);

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }

        private void ExternelResourcesLoad(ContainerBuilder builder)
        {

        }

        private void UtilitiesLoads(ContainerBuilder builder)
        {
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

        }

        private void EntityFrameworkLoads(ContainerBuilder builder)
        {
            builder.RegisterType<EfCommentDal>().As<ICommentDal>().SingleInstance();
            builder.RegisterType<EfLikeDal>().As<ILikeDal>().SingleInstance();
            builder.RegisterType<EfMessageDal>().As<IMessageDal>().SingleInstance();
            builder.RegisterType<EfPostDal>().As<IPostDal>().SingleInstance();
            builder.RegisterType<EfProfilePictureDal>().As<IProfilePictureDal>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

        }

        private void ManagerLoads(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();
            builder.RegisterType<LikeManager>().As<ILikeService>().SingleInstance();
            builder.RegisterType<MessageManager>().As<IMessageService>().SingleInstance();
            builder.RegisterType<PostManager>().As<IPostService>().SingleInstance();
            builder.RegisterType<ProfilePictureManager>().As<IProfilePictureService>().SingleInstance();
            builder.RegisterType<UserRateManager>().As<IUserRateService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        }
    }
}
