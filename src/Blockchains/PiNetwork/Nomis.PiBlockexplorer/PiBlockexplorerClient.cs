// ------------------------------------------------------------------------------------------------------
// <copyright file="PiBlockexplorerClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Nomis.Blockchain.Abstractions.Converters;
using Nomis.PiBlockexplorer.Interfaces;
using Nomis.PiBlockexplorer.Interfaces.Models;
using Nomis.PiBlockexplorer.Settings;
using Nomis.Utils.Exceptions;

namespace Nomis.PiBlockexplorer
{
    /// <inheritdoc cref="IPiBlockexplorerClient"/>
    internal sealed class PiBlockexplorerClient :
        IPiBlockexplorerClient
    {
        private const int ItemsFetchLimit = 100;

        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="PiBlockexplorerClient"/>.
        /// </summary>
        /// <param name="piBlockExplorerSettings"><see cref="PiBlockexplorerSettings"/>.</param>
        public PiBlockexplorerClient(
            IOptions<PiBlockexplorerSettings> piBlockExplorerSettings)
        {
            _client = new()
            {
                BaseAddress = new(piBlockExplorerSettings.Value.ApiBaseUrl ??
                                  throw new ArgumentNullException(nameof(piBlockExplorerSettings.Value.ApiBaseUrl)))
            };
        }

        /// <inheritdoc/>
        public async Task<PiBlockexplorerAccount> AccountDataAsync(string address)
        {
            string request =
                $"/accounts/{address}";

            var response = await _client.GetAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PiBlockexplorerAccount>(new JsonSerializerOptions
                   {
                       Converters = { new BigIntegerConverter() }
                   }).ConfigureAwait(false) ?? throw new CustomException("Can't get account balance.");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PiBlockexplorerTransaction>> TransactionsAsync(string address)
        {
            var result = new List<PiBlockexplorerTransaction>();
            var transactionsData = await TransactionList(address).ConfigureAwait(false);
            result.AddRange(transactionsData.Embedded?.Transactions ?? new List<PiBlockexplorerTransaction>());
            while (!string.IsNullOrWhiteSpace(transactionsData.Links?.Next?.Href) && transactionsData.Embedded?.Transactions?.Any() == true)
            {
                transactionsData = await TransactionList(address, transactionsData.Links?.Next?.Href).ConfigureAwait(false);
                result.AddRange(transactionsData.Embedded?.Transactions ?? new List<PiBlockexplorerTransaction>());
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PiBlockexplorerOperation>> OperationsAsync(string address)
        {
            var result = new List<PiBlockexplorerOperation>();
            var operationsData = await OperationList(address).ConfigureAwait(false);
            result.AddRange(operationsData.Embedded?.Operations ?? new List<PiBlockexplorerOperation>());
            while (!string.IsNullOrWhiteSpace(operationsData.Links?.Next?.Href) && operationsData.Embedded?.Operations?.Any() == true)
            {
                operationsData = await OperationList(address, operationsData.Links?.Next?.Href).ConfigureAwait(false);
                result.AddRange(operationsData.Embedded?.Operations ?? new List<PiBlockexplorerOperation>());
            }

            return result;
        }

        private async Task<PiBlockexplorerTransactions> TransactionList(
            string address,
            string? next = null)
        {
            string request = string.IsNullOrWhiteSpace(next)
                ? $"/accounts/{address}/transactions?limit={ItemsFetchLimit}&order=desc"
                : next;

            var response = await _client.GetAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PiBlockexplorerTransactions>(new JsonSerializerOptions
                   {
                       Converters = { new BigIntegerConverter() }
                   }).ConfigureAwait(false) ?? throw new CustomException("Can't get account transactions.");
        }

        private async Task<PiBlockexplorerOperations> OperationList(
            string address,
            string? next = null)
        {
            string request = string.IsNullOrWhiteSpace(next)
                ? $"/accounts/{address}/operations?limit={ItemsFetchLimit}&order=desc"
                : next;

            var response = await _client.GetAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PiBlockexplorerOperations>(new JsonSerializerOptions
            {
                Converters = { new BigIntegerConverter() }
            }).ConfigureAwait(false) ?? throw new CustomException("Can't get account operations.");
        }
    }
}