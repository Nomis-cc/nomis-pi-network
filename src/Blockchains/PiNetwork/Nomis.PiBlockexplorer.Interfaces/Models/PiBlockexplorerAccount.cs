// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerAccount.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer account data.
    /// </summary>
    public class PiBlockexplorerAccount
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Account id.
        /// </summary>
        [JsonPropertyName("account_id")]
        public string? AccountId { get; set; }

        /// <summary>
        /// Last modified time.
        /// </summary>
        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }

        /// <summary>
        /// Balances.
        /// </summary>
        [JsonPropertyName("balances")]
        public IList<PiBlockexplorerAccountBalance> Balances { get; set; } = new List<PiBlockexplorerAccountBalance>();
    }
}