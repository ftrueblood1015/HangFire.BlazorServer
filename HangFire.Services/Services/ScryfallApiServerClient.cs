﻿namespace HangFire.Services.Services
{
    public class ScryfallApiServerClient
    {
        private readonly HttpClient _httpClient;

        public ScryfallApiServerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient Client => _httpClient;
    }
}
