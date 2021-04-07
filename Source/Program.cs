/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.Configuration;
using System.Reflection;

namespace RaaLabs.Edge.Connectors.NMEA
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            var application = new ApplicationBuilder()
                .WithModule<EventHandling>()
                .WithModule<EdgeHub>()
                .WithModule<Configuration>()
                .WithTask<Connector>()
                .WithHandler<NMEALineHandler>()
                .WithHandler<StateHandler>()
                .WithAllImplementationsOf<ISentenceFormat>()
                .Build();

            await application.Run();
        }
    }

}
