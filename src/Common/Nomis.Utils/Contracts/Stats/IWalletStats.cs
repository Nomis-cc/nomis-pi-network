// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Utils.Contracts.Stats
{
    /// <summary>
    /// Wallet stats.
    /// </summary>
    public interface IWalletStats
    {
        /// <summary>
        /// Calculate wallet default stats score.
        /// </summary>
        /// <returns>Returns wallet default stats score.</returns>
        public double CalculateScore()
        {
            return 0;
        }

        /// <summary>
        /// Calculate wallet default adjusting score multiplier.
        /// </summary>
        /// <returns>Returns wallet default adjusting score multiplier.</returns>
        public double CalculateAdjustingScoreMultiplier()
        {
            return 1;
        }
    }
}