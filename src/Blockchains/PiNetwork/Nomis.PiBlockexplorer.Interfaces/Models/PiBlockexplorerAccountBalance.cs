// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerAccountBalance.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer account balance.
    /// </summary>
    public class PiBlockexplorerAccountBalance
    {
        /// <summary>
        /// Balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public string? Balance { get; set; }

        /// <summary>
        /// Asset type.
        /// </summary>
        [JsonPropertyName("asset_type")]
        public string? AssetType { get; set; }
    }
}