﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhyNotEarth.Meredith.App.Auth;
using WhyNotEarth.Meredith.App.Models.Api.v0.Volkswagen;
using WhyNotEarth.Meredith.Data.Entity.Models.Modules.Volkswagen;
using WhyNotEarth.Meredith.Volkswagen;

namespace WhyNotEarth.Meredith.App.Controllers.Api.v0.Volkswagen
{
    [Returns401]
    [Returns403]
    [ApiVersion("0")]
    [Route("api/v0/volkswagen/memos")]
    [ProducesErrorResponseType(typeof(void))]
    [Authorize(Policy = Policies.ManageVolkswagen)]
    public class MemoController : ControllerBase
    {
        private readonly MemoService _memoService;

        public MemoController(MemoService memoService)
        {
            _memoService = memoService;
        }

        [Returns201]
        [HttpPost("")]
        public async Task<StatusCodeResult> Create(MemoModel model)
        {
            await _memoService.CreateAsync(model.DistributionGroup, model.Subject, model.Date, model.To,
                model.Description);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPost("stats")]
        public async Task<ActionResult<List<MemoRecipient>>> Stats()
        {
            var stats = await _memoService.GetStatsAsync();

            return Ok(stats);
        }
    }
}