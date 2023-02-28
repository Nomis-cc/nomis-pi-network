﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletHapiStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.Utils.Contracts.Stats;

namespace Nomis.HapiExplorer.Interfaces.Stats
{
    /// <summary>
    /// Wallet HAPI protocol stats.
    /// </summary>
    public interface IWalletHapiStats :
        IWalletStats
    {
        /// <summary>
        /// The HAPI protocol risk score.
        /// </summary>
        public HapiProxyRiskScoreResponse? HapiRiskScore { get; init; }

        /// <summary>
        /// Calculate wallet HAPI protocol stats score.
        /// </summary>
        /// <returns>Returns wallet HAPI protocol stats score.</returns>
        public new double CalculateScore()
        {
            return 0;
        }

        /// <summary>
        /// Calculate wallet HAPI protocol adjusting score multiplier.
        /// </summary>
        /// <remarks>
        /// <see href="https://hapi-one.gitbook.io/hapi-protocol/hapi-core-of-decentralized-cybersecurity/risk-assessment"/>
        /// </remarks>
        /// <returns>Returns wallet HAPI protocol adjusting score multiplier.</returns>
        public new double CalculateAdjustingScoreMultiplier()
        {
            return HapiRiskScore?.Address?.Risk switch
            {
                >= 10 => 0,
                >= 5 => 0.6,
                >= 1 => 0.8,
                _ => 1
            };
        }
    }
}