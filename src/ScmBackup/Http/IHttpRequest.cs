﻿using System.Net.Http;
using System.Threading.Tasks;

namespace ScmBackup.Http
{
    /// <summary>
    /// Wrapper for HttpClient
    /// </summary>
    internal interface IHttpRequest
    {
        HttpClient HttpClient { get; set; }

        void SetBaseUrl(string url);

        void AddHeader(string name, string value);

        void AddBasicAuthHeader(string username, string password);

        Task<HttpResult> Execute(string url);
    }
}