﻿using System;
using System.Threading.Tasks;
using Auth0.AuthenticationApi.Client.Models;
using Auth0.ManagementApi.Client;
using FluentAssertions;
using NUnit.Framework;

namespace Auth0.AuthenticationApi.Client.IntegrationTests
{
    [TestFixture]
    public class UriBuildersTests : TestBase
    {
        [Test]
        public async Task Can_build_authorization_uri()
        {
            var authenticationApiClient = new AuthenticationApiClient(new Uri(GetVariable("AUTH0_AUTHENTICATION_API_URL")));

            var authorizationUrl = authenticationApiClient.BuildAuthorizationUrl()
                .WithResponseType(AuthorizationResponseType.Code)
                .WithClient("rLNKKMORlaDzrMTqGtSL9ZSXiBBksCQW")
                .WithConnection("google-oauth2")
                .WithRedirectUrl("http://www.jerriepelser.com/test")
                .WithScope("openid offline_access")
                .Build();

            authorizationUrl.Should()
                .Be(
                    @"https://auth0-dotnet-integration-tests.auth0.com/authorize?response_type=code&client_id=rLNKKMORlaDzrMTqGtSL9ZSXiBBksCQW&connection=google-oauth2&redirect_uri=http%3A%2F%2Fwww.jerriepelser.com%2Ftest&scope=openid%20offline_access");
        }

        [Test]
        public void Can_build_logout_url()
        {
            var authenticationApiClient = new AuthenticationApiClient(new Uri(GetVariable("AUTH0_AUTHENTICATION_API_URL")));

            var logoutUrl = authenticationApiClient.BuildLogoutUrl()
                .Build();

            logoutUrl.Should()
                .Be(
                    @"https://auth0-dotnet-integration-tests.auth0.com/logout");
        }

        [Test]
        public void Can_build_logout_url_with_return_url()
        {
            var authenticationApiClient = new AuthenticationApiClient(new Uri(GetVariable("AUTH0_AUTHENTICATION_API_URL")));

            var logoutUrl = authenticationApiClient.BuildLogoutUrl()
                .WithReturnUrl("http://www.jerriepelser.com/test")
                .Build();

            logoutUrl.Should()
                .Be(
                    @"https://auth0-dotnet-integration-tests.auth0.com/logout?returnTo=http:%2F%2Fwww.jerriepelser.com%2Ftest");
        }

        [Test]
        public void Can_build_saml_url()
        {
            var authenticationApiClient = new AuthenticationApiClient(new Uri(GetVariable("AUTH0_AUTHENTICATION_API_URL")));

            var samlUrl = authenticationApiClient.BuildSamlUrl("myclientid")
                .WithConnection("my-connection-name")
                .Build();

            samlUrl.Should().Be(@"https://auth0-dotnet-integration-tests.auth0.com/samlp/myclientid?connection=my-connection-name");
        }
    }
}