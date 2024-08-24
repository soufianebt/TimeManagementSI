﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Calendar.Service.Facade.services;

namespace Calendar.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICalendarService _calendarService;

        public OauthController(IHttpClientFactory httpClientFactory, ICalendarService calendarService)
        {
            _httpClientFactory = httpClientFactory;
            _calendarService = calendarService;
        }

        [HttpGet("getRedirectionUrl")]
        public async Task<IActionResult> GetRedirectionUrl()
        {
            var redirectUrl = await _calendarService.GetRedirectionUrl();
            return Ok(redirectUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> HandleOAuthCallback([FromQuery] string code, [FromQuery] string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code is missing");
            }

            var tokenResponse = await _calendarService.ExchangeCodeForAccessTokenAsync(code);
            if (tokenResponse == null)
            {
                return StatusCode(500, "Failed to exchange code for token");
            }
            HttpContext.Session.SetString("OAuthAccessToken", tokenResponse.AccessToken ?? string.Empty);
            HttpContext.Session.SetString("OAuthRefreshToken", tokenResponse.RefreshToken ?? string.Empty);
            return Ok(tokenResponse);
        }

        
    }

    
}
