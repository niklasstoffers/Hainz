using Autofac;
using Hainz.InterfaceCommands;
using Hainz.InterfaceCommands.CommandModules;
using Hainz.IO.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hainz.Modules
{
    public class InterfaceCommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>().AsSelf();
            builder.RegisterType<CommandHandlerLocator>().AsSelf();
            builder.RegisterType<CommandParser>().AsSelf();
            builder.RegisterType<CommandBuilder>().AsSelf();

            builder.Register<CommandManager>(ctx => 
            {
                return new CommandManager(ctx.Resolve<CommandDispatcher>(),
                                                 ctx.Resolve<CommandHandlerLocator>(),
                                                 ctx.Resolve<CommandParser>(),
                                                 ctx.Resolve<ConsoleInput>());
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ConfigCommandModule>().AsSelf();
        }
    }
}
