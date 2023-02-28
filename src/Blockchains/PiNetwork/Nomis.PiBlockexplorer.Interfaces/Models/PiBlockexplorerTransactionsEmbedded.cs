// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerTransactionsEmbedded.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer transactions embedded data.
    /// </summary>
    public class PiBlockexplorerTransactionsEmbedded
    {
        /// <summary>
        /// Transactions.
        /// </summary>
        [JsonPropertyName("records")]
        public IList<PiBlockexplorerTransaction> Transactions { get; set; } = new List<PiBlockexplorerTransaction>();
    }
}