// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer transaction data.
    /// </summary>
    public class PiBlockexplorerTransaction
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Paging token.
        /// </summary>
        [JsonPropertyName("paging_token")]
        public string? PagingToken { get; set; }

        /// <summary>
        /// Successful.
        /// </summary>
        [JsonPropertyName("successful")]
        public bool Successful { get; set; }

        /// <summary>
        /// Hash.
        /// </summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }

        /// <summary>
        /// Created at.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Source account.
        /// </summary>
        [JsonPropertyName("source_account")]
        public string? SourceAccount { get; set; }

        /// <summary>
        /// Fee account.
        /// </summary>
        [JsonPropertyName("fee_account")]
        public string? FeeAccount { get; set; }

        /// <summary>
        /// Operation count.
        /// </summary>
        [JsonPropertyName("operation_count")]
        public int? OperationCount { get; set; }
    }
}