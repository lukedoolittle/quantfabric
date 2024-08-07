﻿using Material.Framework.Metadata;

namespace Material.Framework.Enums
{
    public enum OAuth2Parameter
    {
        [Description("oauth_token")]
        OAuthToken,
        [Description("redirect_uri")]
        RedirectUri,
        [Description("client_id")]
        ClientId,
        [Description("client_secret")]
        ClientSecret,
        [Description("scope")]
        Scope,
        [Description("grant_type")]
        GrantType,
        [Description("response_type")]
        ResponseType,
        [Description("refresh_token")]
        RefreshToken,
        [Description("state")]
        State,
        [Description("nonce")]
        Nonce,
        [Description("assertion")]
        Assertion,
        [Description("Bearer")]
        BearerHeader,
        [Description("Basic")]
        BasicHeader,
        [Description("error")]
        Error,
        [Description("code_challenge")]
        Challenge,
        [Description("code_challenge_method")]
        ChallengeMethod,
        [Description("code_verifier")]
        Verifier
    }
}
