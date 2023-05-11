using Autofac;
using $ext_safeprojectname$.Core.Interfaces;
using $ext_safeprojectname$.Core.Services;

namespace $ext_safeprojectname$.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<UsuarioService>()
                .As<IUsuarioService>()
                .InstancePerLifetimeScope();
        }
    }
}
