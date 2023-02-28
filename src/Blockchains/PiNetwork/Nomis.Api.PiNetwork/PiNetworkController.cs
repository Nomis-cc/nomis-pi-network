// ------------------------------------------------------------------------------------------------------
// <copyright file="PiNetworkController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.PiBlockexplorer.Interfaces;
using Nomis.PiBlockexplorer.Interfaces.Models;
using Nomis.PiBlockexplorer.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.PiNetwork
{
    /// <summary>
    /// A controller to aggregate all Pi Network-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Pi Network blockchain.")]
    public sealed class PiNetworkController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/pi-network";

        /// <summary>
        /// Common tag for PiNetwork actions.
        /// </summary>
        internal const string PiNetworkTag = "PiNetwork";

        private readonly ILogger<PiNetworkController> _logger;
        private readonly IPiNetworkScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="PiNetworkController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IPiNetworkScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public PiNetworkController(
            IPiNetworkScoringService scoringService,
            ILogger<PiNetworkController> logger)
        {
            _scoringService = scoringService ?? throw new ArgumentNullException(nameof(scoringService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Nomis Score for given wallet address.
        /// </summary>
        /// <param name="request">Request for getting the wallet stats.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>An Nomis Score value and corresponding statistical data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/pi-network/wallet/GBNHILSFQUJZUQPLVWU76VE3EX6EP4KCB4BZBLKNXIWDMBCMJ2GJ7OBL/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetPiNetworkWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetPiNetworkWalletScore",
            Tags = new[] { PiNetworkTag })]
        [ProducesResponseType(typeof(Result<PiNetworkWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetPiNetworkWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] PiNetworkWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<PiNetworkWalletStatsRequest, PiNetworkWalletScore, PiNetworkWalletStats, PiNetworkTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}