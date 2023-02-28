// ------------------------------------------------------------------------------------------------------
// <copyright file="IPiBlockexplorerClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.PiBlockexplorer.Interfaces.Models;

namespace Nomis.PiBlockexplorer.Interfaces
{
    /// <summary>
    /// Pi Blockexplorer client.
    /// </summary>
    public interface IPiBlockexplorerClient
    {
        /// <summary>
        /// Get the account data by address.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns <see cref="PiBlockexplorerAccount"/>.</returns>
        Task<PiBlockexplorerAccount> AccountDataAsync(string address);

        /// <summary>
        /// Get list of transactions of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of <see cref="PiBlockexplorerTransaction"/>.</returns>
        Task<IEnumerable<PiBlockexplorerTransaction>> TransactionsAsync(string address);

        /// <summary>
        /// Get list of operations of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of <see cref="PiBlockexplorerOperation"/>.</returns>
        Task<IEnumerable<PiBlockexplorerOperation>> OperationsAsync(string address);
    }
}