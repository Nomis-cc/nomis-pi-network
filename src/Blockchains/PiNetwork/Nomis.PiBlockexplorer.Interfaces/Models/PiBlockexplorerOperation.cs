// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerOperation.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer operation data.
    /// </summary>
    public class PiBlockexplorerOperation
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
        /// Transaction successful.
        /// </summary>
        [JsonPropertyName("transaction_successful")]
        public bool TransactionSuccessful { get; set; }

        /// <summary>
        /// Source account.
        /// </summary>
        [JsonPropertyName("source_account")]
        public string? SourceAccount { get; set; }

        /// <summary>
        /// Type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Created at.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonPropertyName("transaction_hash")]
        public string? TransactionHash { get; set; }

        /// <summary>
        /// Asset type.
        /// </summary>
        [JsonPropertyName("asset_type")]
        public string? AssetType { get; set; }

        /// <summary>
        /// From.
        /// </summary>
        [JsonPropertyName("from")]
        public string? From { get; set; }

        /// <summary>
        /// To.
        /// </summary>
        [JsonPropertyName("to")]
        public string? To { get; set; }

        /// <summary>
        /// Amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public string? Amount { get; set; }
    }
}