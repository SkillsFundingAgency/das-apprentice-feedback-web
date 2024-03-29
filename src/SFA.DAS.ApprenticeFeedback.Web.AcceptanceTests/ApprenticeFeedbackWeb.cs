﻿using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests.Hooks;
using SFA.DAS.ApprenticePortal.Authentication.TestHelpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeFeedback.Web.AcceptanceTests
{
    public class ApprenticeFeedbackWeb : IDisposable
    {
        public HttpClient Client { get; private set; }
        public HttpResponseMessage Response { get; set; }
        public Uri BaseAddress { get; private set; }
        public IHook<IActionResult> ActionResultHook { get; set; }
        public Dictionary<string, string> Config { get; }

        private bool isDisposed;

        public ApprenticeFeedbackWeb(HttpClient client, IHook<IActionResult> actionResultHook, Dictionary<string, string> config)
        {
            Client = client;
            BaseAddress = client.BaseAddress;
            ActionResultHook = actionResultHook;
            Config = config;
        }

        public void AuthoriseApprentice(Guid apprenticeId)
        {
            AuthenticationHandlerForTesting.AddUserWithFullAccount(apprenticeId);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apprenticeId.ToString());
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            Response?.Dispose();

            return Response = await Client.GetAsync(url);
        }

        internal async Task<HttpResponseMessage> Post(string url, HttpContent content)
        {
            Response?.Dispose();

            return Response = await Client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> FollowLocalRedirects()
        {
            while (
                (int)Response.StatusCode >= 300 &&
                (int)Response.StatusCode <= 400)
            {
                if (!Response.Headers.Location.ToString().StartsWith('/') && !Response.Headers.Location.ToString().ToLower().StartsWith("http://localhost"))
                    break;

                if (Response.StatusCode == HttpStatusCode.RedirectKeepVerb)
                {
                    return Response = await Client.SendAsync(
                        new HttpRequestMessage(
                            Response.RequestMessage.Method,
                            Response.Headers.Location)
                        {
                            Content = Response.Content
                        });
                }
                else if (
                    Response.StatusCode >= HttpStatusCode.Moved &&
                    Response.StatusCode <= HttpStatusCode.PermanentRedirect)
                {
                    await Get(Response.Headers.Location.ToString());
                }
                else
                {
                    break;
                }
            }

            return Response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                Response?.Dispose();
            }

            isDisposed = true;
        }
    }
}
