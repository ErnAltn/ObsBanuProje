using Autofac;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Fakes.DArch;
using Business.Services.Authentication;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ClaimsPrincipal GetPrincipal(IServiceProvider sp) =>
                sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(GetPrincipal);
            services.AddMemoryCache();

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });

            services.AddTransient<IAuthenticationCoordinator, AuthenticationCoordinator>();

            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();

            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<DisplayAttribute>()
                    ?.GetName();
            };
        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IST_SinavTuruRepository, ST_SinavTuruRepository>();
            services.AddTransient<IDegerlendirmeRepository, DegerlendirmeRepository>();
            services.AddTransient<IDersAcmaRepository, DersAcmaRepository>();
            services.AddTransient<IDersAlmaRepository, DersAlmaRepository>();
            services.AddTransient<IDersHavuzuRepository, DersHavuzuRepository>();
            services.AddTransient<IDerslikRepository, DerslikRepository>();
            services.AddTransient<IDersProgramiRepository, DersProgramiRepository>();
            services.AddTransient<IMufredatRepository, MufredatRepository>();
            services.AddTransient<IOgretimElemaniRepository, OgretimElemaniRepository>();
            services.AddTransient<ISinavRepository, SinavRepository>();
            services.AddTransient<IST_AkademikDonemRepository, ST_AkademikDonemRepository>();
            services.AddTransient<IST_AkademikYilRepository, ST_AkademikYilRepository>();
            services.AddTransient<IST_DersAlmaDurumuRepository, ST_DersAlmaDurumuRepository>();
            services.AddTransient<IST_DersDiliRepository, ST_DersDiliRepository>();
            services.AddTransient<IST_DersGunuRepository, ST_DersGunuRepository>();
            services.AddTransient<IST_DerslikTuruRepository, ST_DerslikTuruRepository>();
            services.AddTransient<IST_DersSeviyesiRepository, ST_DersSeviyesiRepository>();
            services.AddTransient<IST_DersTuruRepository, ST_DersTuruRepository>();
            services.AddTransient<IST_OgrenciDurumRepository, ST_OgrenciDurumRepository>();
            services.AddTransient<IST_OgretimDiliRepository, ST_OgretimDiliRepository>();
            services.AddTransient<IST_OgretimTuruRepository, ST_OgretimTuruRepository>();
            
            services.AddTransient<IST_ProgramTuruRepository, ST_ProgramTuruRepository>();
            services.AddTransient<IST_ProgramTuruRepository, ST_ProgramTuruRepository>();
            services.AddTransient<IDanismanlikRepository, DanismanlikRepository>();
            services.AddTransient<IDanismanlikRepository, DanismanlikRepository>();
            services.AddTransient<IOgrenciRepository, OgrenciRepository>();
            services.AddTransient<IBolumRepository, BolumRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<ProjectDbContext, DArchInMemory>(ServiceLifetime.Transient);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IST_SinavTuruRepository, ST_SinavTuruRepository>();
            services.AddTransient<IDegerlendirmeRepository, DegerlendirmeRepository>();
            services.AddTransient<IDersAcmaRepository, DersAcmaRepository>();
            services.AddTransient<IDersAlmaRepository, DersAlmaRepository>();
            services.AddTransient<IDersHavuzuRepository, DersHavuzuRepository>();
            services.AddTransient<IDerslikRepository, DerslikRepository>();
            services.AddTransient<IDersProgramiRepository, DersProgramiRepository>();
            services.AddTransient<IMufredatRepository, MufredatRepository>();
            services.AddTransient<IOgretimElemaniRepository, OgretimElemaniRepository>();
            services.AddTransient<ISinavRepository, SinavRepository>();
            services.AddTransient<IST_AkademikDonemRepository, ST_AkademikDonemRepository>();
            services.AddTransient<IST_AkademikYilRepository, ST_AkademikYilRepository>();
            services.AddTransient<IST_DersAlmaDurumuRepository, ST_DersAlmaDurumuRepository>();
            services.AddTransient<IST_DersDiliRepository, ST_DersDiliRepository>();
            services.AddTransient<IST_DersGunuRepository, ST_DersGunuRepository>();
            services.AddTransient<IST_DerslikTuruRepository, ST_DerslikTuruRepository>();
            services.AddTransient<IST_DersSeviyesiRepository, ST_DersSeviyesiRepository>();
            services.AddTransient<IST_DersTuruRepository, ST_DersTuruRepository>();
            services.AddTransient<IST_OgrenciDurumRepository, ST_OgrenciDurumRepository>();
            services.AddTransient<IST_OgretimDiliRepository, ST_OgretimDiliRepository>();
            services.AddTransient<IST_OgretimTuruRepository, ST_OgretimTuruRepository>();
           
            services.AddTransient<IST_ProgramTuruRepository, ST_ProgramTuruRepository>();
            services.AddTransient<IST_ProgramTuruRepository, ST_ProgramTuruRepository>();
            services.AddTransient<IDanismanlikRepository, DanismanlikRepository>();
            services.AddTransient<IDanismanlikRepository, DanismanlikRepository>();
            services.AddTransient<IOgrenciRepository, OgrenciRepository>();
            services.AddTransient<IBolumRepository, BolumRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext,MsDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IST_SinavTuruRepository, ST_SinavTuruRepository>();
            services.AddTransient<IDegerlendirmeRepository, DegerlendirmeRepository>();
            services.AddTransient<IDersAcmaRepository, DersAcmaRepository>();
            services.AddTransient<IDersAlmaRepository, DersAlmaRepository>();
            services.AddTransient<IDersHavuzuRepository, DersHavuzuRepository>();
            services.AddTransient<IDerslikRepository, DerslikRepository>();
            services.AddTransient<IDersProgramiRepository, DersProgramiRepository>();
            services.AddTransient<IMufredatRepository, MufredatRepository>();
            services.AddTransient<IOgretimElemaniRepository, OgretimElemaniRepository>();
            services.AddTransient<ISinavRepository, SinavRepository>();
            services.AddTransient<IST_AkademikDonemRepository, ST_AkademikDonemRepository>();
            services.AddTransient<IST_AkademikYilRepository, ST_AkademikYilRepository>();
            services.AddTransient<IST_DersAlmaDurumuRepository, ST_DersAlmaDurumuRepository>();
            services.AddTransient<IST_DersDiliRepository, ST_DersDiliRepository>();
            services.AddTransient<IST_DersGunuRepository, ST_DersGunuRepository>();
            services.AddTransient<IST_DerslikTuruRepository, ST_DerslikTuruRepository>();
            services.AddTransient<IST_DersSeviyesiRepository, ST_DersSeviyesiRepository>();
            services.AddTransient<IST_DersTuruRepository, ST_DersTuruRepository>();
            services.AddTransient<IST_OgrenciDurumRepository, ST_OgrenciDurumRepository>();
            services.AddTransient<IST_OgretimDiliRepository, ST_OgretimDiliRepository>();
            services.AddTransient<IST_OgretimTuruRepository, ST_OgretimTuruRepository>();
            services.AddTransient<IST_ProgramTuruRepository, ST_ProgramTuruRepository>();
            services.AddTransient<IST_ProgramTuruRepository, ST_ProgramTuruRepository>();
            services.AddTransient<IDanismanlikRepository, DanismanlikRepository>();
            services.AddTransient<IDanismanlikRepository, DanismanlikRepository>();
            services.AddTransient<IOgrenciRepository, OgrenciRepository>();
            services.AddTransient<IBolumRepository, BolumRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();


            services.AddDbContext<ProjectDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}
