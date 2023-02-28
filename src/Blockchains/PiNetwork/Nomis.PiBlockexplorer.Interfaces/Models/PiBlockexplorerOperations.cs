// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerOperations.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer operations data.
    /// </summary>
    public class PiBlockexplorerOperations
    {
        /// <summary>
        /// Links.
        /// </summary>
        [JsonPropertyName("_links")]
        public PiBlockexplorerLinks? Links { get; set; }

        /// <summary>
        /// Embedded.
        /// </summary>
        [JsonPropertyName("_embedded")]
        public PiBlockexplorerOperationsEmbedded? Embedded { get; set; }
    }
}