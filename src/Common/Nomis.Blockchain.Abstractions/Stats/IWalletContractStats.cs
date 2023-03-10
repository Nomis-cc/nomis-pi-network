// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletContractStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Stats;

namespace Nomis.Blockchain.Abstractions.Stats
{
    /// <summary>
    /// Wallet contract stats.
    /// </summary>
    public interface IWalletContractStats :
        IWalletStats
    {
        /// <summary>
        /// Amount of deployed smart-contracts.
        /// </summary>
        public int DeployedContracts { get; init; }

        /// <summary>
        /// Calculate wallet contract stats score.
        /// </summary>
        /// <returns>Returns wallet contract stats score.</returns>
        public new double CalculateScore()
        {
            // TODO - add calculation
            return 0;
        }
    }
}