// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.PiBlockexplorer.Extensions;
using Nomis.PiBlockexplorer.Interfaces;

namespace Nomis.PiBlockexplorer
{
    /// <summary>
    /// Pi Blockexplorer service registrar.
    /// </summary>
    public sealed class PiBlockexplorer :
        IPiNetworkServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddPiBlockexplorerService();
        }
    }
}