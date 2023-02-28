// ------------------------------------------------------------------------------------------------------
// <copyright file="PiNetworkExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.PiNetwork.Settings;
using Nomis.PiBlockexplorer.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.PiNetwork.Extensions
{
    /// <summary>
    /// Pi Network extension methods.
    /// </summary>
    public static class PiNetworkExtensions
    {
        /// <summary>
        /// Add Pi Network blockchain.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithPiNetworkBlockchain<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IPiNetworkServiceRegistrar, new()
        {
            return optionsBuilder
                .With<PiNetworkAPISettings, TServiceRegistrar>();
        }
    }
}