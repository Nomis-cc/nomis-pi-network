// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerOperationsEmbedded.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.PiBlockexplorer.Interfaces.Models
{
    /// <summary>
    /// Pi Blockexplorer operations embedded data.
    /// </summary>
    public class PiBlockexplorerOperationsEmbedded
    {
        /// <summary>
        /// Operations.
        /// </summary>
        [JsonPropertyName("records")]
        public IList<PiBlockexplorerOperation> Operations { get; set; } = new List<PiBlockexplorerOperation>();
    }
}