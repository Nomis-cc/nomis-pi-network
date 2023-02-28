// ------------------------------------------------------------------------------------------------------
// <copyright file="PiNetworkStatCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nomis.Blockchain.Abstractions.Calculators;
using Nomis.Blockchain.Abstractions.Models;
using Nomis.PiBlockexplorer.Interfaces.Extensions;
using Nomis.PiBlockexplorer.Interfaces.Models;

namespace Nomis.PiBlockexplorer.Calculators
{
    /// <summary>
    /// Pi Network wallet stats calculator.
    /// </summary>
    internal sealed class PiNetworkStatCalculator :
        IStatsCalculator<PiNetworkWalletStats, PiNetworkTransactionIntervalData>
    {
        private readonly string _address;
        private readonly decimal _balance;
        private readonly decimal _usdBalance;
        private readonly PiBlockexplorerAccount _account;
        private readonly IEnumerable<PiBlockexplorerTransaction> _transactions;
        private readonly IEnumerable<PiBlockexplorerOperation> _operations;

        public PiNetworkStatCalculator(
            string address,
            decimal balance,
            decimal usdBalance,
            PiBlockexplorerAccount account,
            IEnumerable<PiBlockexplorerTransaction> transactions,
            IEnumerable<PiBlockexplorerOperation> operations)
        {
            _address = address;
            _balance = balance;
            _usdBalance = usdBalance;
            _account = account;
            _transactions = transactions;
            _operations = operations;
        }

        public PiNetworkWalletStats GetStats()
        {
            if (!_transactions.Any() || !_operations.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var intervals = IStatsCalculator
                .GetTransactionsIntervals(_transactions.Select(x => x.CreatedAt)).ToList();
            if (intervals.Count == 0)
            {
                return new()
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);
            var yearAgo = DateTime.Now.AddYears(-1);

            var turnoverIntervalsDataList =
                _operations.Select(x => new TurnoverIntervalsData(
                    x.CreatedAt,
                    new BigInteger(x.Amount?.ToPiMultiplied() ?? 0),
                    x.From?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true));
            var turnoverIntervals = IStatsCalculator<PiNetworkTransactionIntervalData>
                .GetTurnoverIntervals(turnoverIntervalsDataList, _operations.Min(x => x.CreatedAt)).ToList();

            return new()
            {
                NativeBalance = _balance.ToPi(),
                NativeBalanceUSD = _usdBalance,
                WalletAge = IStatsCalculator
                    .GetWalletAge(_transactions.Select(x => x.CreatedAt)),
                TotalTransactions = _transactions.Count(),
                TotalRejectedTransactions = 0,
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                WalletTurnover = _operations.Sum(x => x.Amount?.ToPiMultiplied() ?? 0).ToPi(),
                TurnoverIntervals = turnoverIntervals,
                BalanceChangeInLastMonth = IStatsCalculator<PiNetworkTransactionIntervalData>.GetBalanceChangeInLastMonth(turnoverIntervals),
                BalanceChangeInLastYear = IStatsCalculator<PiNetworkTransactionIntervalData>.GetBalanceChangeInLastYear(turnoverIntervals),
                LastMonthTransactions = _transactions.Count(x => x.CreatedAt > monthAgo),
                LastYearTransactions = _transactions.Count(x => x.CreatedAt > yearAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.OrderBy(x => x.CreatedAt).Last().CreatedAt).TotalDays / 30)
            };
        }
    }
}