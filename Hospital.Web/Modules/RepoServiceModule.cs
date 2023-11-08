using Autofac;
using Hospital.Core.Repositories;
using Hospital.Core.Services;
using Hospital.Core.UnitOfWorks;
using Hospital.Repo;
using Hospital.Repo.Repositories;
using Hospital.Repo.UnitOfWorks;
using Hospital.Service.Mapper;
using Hospital.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace Hospital.Web.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<,>)).As(typeof(IService<,>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            var mvcAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(mvcAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(mvcAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
