﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletTokensBalancesRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Blockchain.Abstractions.Contracts
{
    /// <summary>
    /// Wallet tokens balances request.
    /// </summary>
    public interface IWalletTokensBalancesRequest
    {
        /// <summary>
        /// Get wallet hold tokens balances.
        /// </summary>
        public bool GetHoldTokensBalances { get; set; }

        /// <summary>
        /// Time range in hours on either side to find price data for token balances.
        /// </summary>
        public int SearchWidthInHours { get; set; }
    }
}