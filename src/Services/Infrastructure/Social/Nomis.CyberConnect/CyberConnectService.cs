// ------------------------------------------------------------------------------------------------------
// <copyright file="CyberConnectService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using Nomis.CyberConnect.Interfaces;
using Nomis.CyberConnect.Interfaces.Models;
using Nomis.CyberConnect.Interfaces.Requests;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.CyberConnect
{
    /// <inheritdoc cref="ICyberConnectService"/>
    internal sealed class CyberConnectService :
        ICyberConnectService,
        ISingletonService
    {
        private readonly ICyberConnectGraphQLClient _client;

        /// <summary>
        /// Initialize <see cref="CyberConnectService"/>.
        /// </summary>
        /// <param name="client"><see cref="ICyberConnectGraphQLClient"/>.</param>
        public CyberConnectService(
            ICyberConnectGraphQLClient client)
        {
            _client = client;
        }

        /// <inheritdoc />
        public async Task<Result<string?>> HandleAsync(CyberConnectHandleRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = """
                query getHandleByAddress($address: AddressEVM!){
                    address(address: $address) {
                        wallet{
                            primaryProfile{
                                handle
                            }
                        }
                    }
                }
                """,
                Variables = request
            };

            string? data = await GetDataAsync<string>(query, "address", "wallet", "primaryProfile", "handle").ConfigureAwait(false);

            return await Result<string?>.SuccessAsync(data, "Handle by address received.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<CyberConnectProfileData>> ProfileDataAsync(CyberConnectProfileRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = """
                query getProfileByHandle($handle: String!, $chainId: ChainID!){
                    profileByHandle(handle: $handle) {
                        followerCount
                        subscriberCount
                        metadataInfo {
                            avatar
                            bio
                            displayName
                            handle
                        }
                        owner {
                            address
                            metadata(chainID: $chainId) {
                                labels
                                projectInteractionStats{
                                    firstInteraction
                                    lastInteraction
                                    numReceived
                                    numSent
                                    project
                                    txCount
                                }
                            }
                        }
                        isPrimary
                        externalMetadataInfo {
                            type
                            verifiedTwitterID
                            organization {
                                cmcTokenId
                                sector
                                networks
                            }
                            personal {
                                verifiedDiscordID
                                title
                                organization {
                                    id
                                    handle
                                    name
                                    avatar
                                }
                            }
                        }
                    }
                }
                """,
                Variables = request
            };

            var data = await GetDataAsync<CyberConnectProfileData>(query, "profileByHandle").ConfigureAwait(false);

            return await Result<CyberConnectProfileData>.SuccessAsync(data!, "Profile data received.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<Result<IEnumerable<CyberConnectSubscribingProfileData>?>> SubscribingsAsync(CyberConnectSubscribingsRequest request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<Result<IEnumerable<CyberConnectLikeData>?>> LikesAsync(CyberConnectLikesRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = """
                query PrimaryProfileEssences($address: AddressEVM!, $first: Int, $after: Cursor) {
                    address(address: $address) {
                        likes(first: $first, after: $after) {
                            totalCount
                            pageInfo{
                                hasNextPage
                                endCursor
                            }
                            edges {
                                node {
                                    id
                                    author
                                    handle
                                    title
                                    body
                                    digest
                                    arweaveTxHash
                                    createdAt
                                    updatedAt
                                    likeCount
                                    dislikeCount
                                }
                            }
                        }
                    }
                }
                """,
                Variables = request
            };

            // TODO - add pageInfo
            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var result = response.Data["address"]?["likes"]?["edges"]?.AsArray().Select(x => x!["node"]);
            var data = result?
                .Select(x => x?.ToJsonString())
                .Select(x => x == null ? null : JsonSerializer.Deserialize<CyberConnectLikeData>(x))
                .Where(x => x != null)
                .Cast<CyberConnectLikeData>()
                .ToList();

            return await Result<IEnumerable<CyberConnectLikeData>?>.SuccessAsync(data, "Likes received.").ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<IEnumerable<CyberConnectEssenceData>?>> EssencesAsync(CyberConnectEssencesRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = """
                query getProfileByHandle($handle: String!, $first: Int, $after: Cursor){
                    profileByHandle(handle: $handle) {
                        owner {
                            address
                            chainID
                            collectedEssences(first: $first, after: $after){
                                totalCount
                                pageInfo{
                                    endCursor
                                    hasNextPage
                                }
                                edges{
                                    node{
                                        essence{
                                            essenceID
                                            id
                                            metadata{
                                                name
                                                animation_url
                                                app_id
                                                content
                                                description
                                                external_url
                                                issue_date
                                                lang
                                                metadata_id
                                            }
                                            name
                                            symbol
                                            tokenURI
                                            createdBy {
                                                profileID
                                                handle
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                """,
                Variables = request
            };

            // TODO - add pageInfo
            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var result = response.Data["profileByHandle"]?["owner"]?["collectedEssences"]?["edges"]?.AsArray().Select(x => x?["node"]?["essence"]);
            var data = result?
                .Select(x => x?.ToJsonString())
                .Select(x => x == null ? null : JsonSerializer.Deserialize<CyberConnectEssenceData>(x))
                .Where(x => x != null)
                .Cast<CyberConnectEssenceData>()
                .ToList();

            return await Result<IEnumerable<CyberConnectEssenceData>?>.SuccessAsync(data, "Essences received.").ConfigureAwait(false);
        }

        private async Task<TResult?> GetDataAsync<TResult>(GraphQLRequest query, params string[] responseAliases)
        {
            var responseAliasList = responseAliases.ToList();
            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var result = response.Data[responseAliasList.First()];
            responseAliasList.RemoveAt(0);
            foreach (string responseAlias in responseAliasList)
            {
                if (result == null)
                {
                    return default;
                }

                result = result![responseAlias];
            }

            var data = JsonSerializer.Deserialize<TResult?>(result!.ToJsonString()) !;

            return data;
        }
    }
}