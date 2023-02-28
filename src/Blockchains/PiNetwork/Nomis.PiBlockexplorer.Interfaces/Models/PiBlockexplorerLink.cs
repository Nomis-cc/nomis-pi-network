// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerLink.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer link.
    /// </summary>
    public class PiBlockexplorerLink
    {
        /// <summary>
        /// Href.
        /// </summary>
        [JsonPropertyName("href")]
        public string? Href { get; set; }
    }
}