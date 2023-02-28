// ------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nomis.Coingecko.Interfaces;
using Nomis.DefiLlama.Interfaces;
using Nomis.PiBlockexplorer.Interfaces;
using Nomis.PiBlockexplorer.Settings;
using Nomis.ScoringService.Interfaces;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.Utils.Extensions;

namespace Nomis.PiBlockexplorer.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Pi BlockExplorer service.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        internal static IServiceCollection AddPiBlockexplorerService(
            this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            serviceProvider.GetRequiredService<ICoingeckoService>();
            serviceProvider.GetRequiredService<IScoringService>();
            serviceProvider.GetRequiredService<INonEvmSoulboundTokenService>();
            serviceProvider.GetRequiredService<IDefiLlamaService>();
            services.AddSettings<PiBlockexplorerSettings>(configuration);
            return services
                .AddTransient<IPiBlockexplorerClient, PiBlockexplorerClient>()
                .AddTransientInfrastructureService<IPiNetworkScoringService, PiBlockexplorerService>();
        }
    }
}