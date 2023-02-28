// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerLinks.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer links.
    /// </summary>
    public class PiBlockexplorerLinks
    {
        /// <summary>
        /// Self.
        /// </summary>
        [JsonPropertyName("self")]
        public PiBlockexplorerLink? Self { get; set; }

        /// <summary>
        /// Next.
        /// </summary>
        [JsonPropertyName("next")]
        public PiBlockexplorerLink? Next { get; set; }

        /// <summary>
        /// Prev.
        /// </summary>
        [JsonPropertyName("prev")]
        public PiBlockexplorerLink? Prev { get; set; }
    }
}