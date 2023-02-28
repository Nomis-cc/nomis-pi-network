// ------------------------------------------------------------------------------------------------------
// <copyright file="PiNetworkAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.PiNetwork.Settings
{
    /// <summary>
    /// Pi Network API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class PiNetworkAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; set; }

        /// <inheritdoc/>
        public string APIName => PiNetworkController.PiNetworkTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(PiNetworkController);
    }
}