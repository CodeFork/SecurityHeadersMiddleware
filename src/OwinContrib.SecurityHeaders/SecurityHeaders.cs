﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecurityHeadersMiddleware.Infrastructure;

namespace SecurityHeadersMiddleware {
    /// <summary>
    /// OWIN extension methods.
    /// </summary>
    public static class SecurityHeaders {
        #region AntiClickjacking
        /// <summary>
        /// Adds the "X-Frame-Options" header with value DENY to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> AntiClickjackingHeader(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder) {
            return AntiClickjackingHeader(builder, XFrameOption.Deny);
        }
        /// <summary>
        /// Adds the "X-Frame-Options" header with the given option to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <param name="option">The X-Frame option.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> AntiClickjackingHeader(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder, XFrameOption option) {
            builder(_ => AntiClickjackingMiddleware.AntiClickjackingHeader(option));
            return builder;
        }
        /// <summary>
        /// Adds the "X-Frame-Options" with DENY when the request uri is not provided to the response. Otherwise the request uri with ALLOW-FROM &lt;request uri&gt;.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <param name="origins">The allowed uris.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> AntiClickjackingHeader(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder, params string[] origins) {
            origins.MustNotNull("origins");
            origins.MustHaveAtLeastOneValue("origins");
            builder(_ => AntiClickjackingMiddleware.AntiClickjackingHeader(origins));
            return builder;
        }
        /// <summary>
        /// Adds the "X-Frame-Options" with DENY when the request uri is not provided to the response. Otherwise the request uri with ALLOW-FROM &lt;request uri&gt;.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <param name="origins">The allowed uirs.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> AntiClickjackingHeader(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder, params Uri[] origins) {
            origins.MustNotNull("origins");
            origins.MustHaveAtLeastOneValue("origins");
            builder(_ => AntiClickjackingMiddleware.AntiClickjackingHeader(origins));
            return builder;
        }
        #endregion

        #region XssProtection
        /// <summary>
        /// Adds the "X-Xss-Protection" header with value "1; mode=block" to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> XssProtectionHeader(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder) {
            return XssProtectionHeader(builder, false);
        }
        /// <summary>
        /// Adds the "X-Xss-Protection" header depending on <paramref name="disabled"/> to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <param name="disabled">true to set the heade value to "0". false to set the header value to"1; mode=block".</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> XssProtectionHeader(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder, bool disabled) {
            builder(_ => XssProtectionHeaderMiddleware.XssProtectionHeader(disabled));
            return builder;
        }
        #endregion

        #region Strict Transport Security
        /// <summary>
        /// Adds the "Strict-Transport-Security" (STS) header to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> StrictTransportSecurity(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder) {
            return StrictTransportSecurity(builder, new StrictTransportSecurityOptions());
        }
        /// <summary>
        /// Adds the "Strict-Transport-Security" (STS) header with the given option to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <param name="options">The Strict-Transport-Security options.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> StrictTransportSecurity(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder, StrictTransportSecurityOptions options) {
            builder(_ => StrictTransportSecurityHeaderMiddleware.StrictTransportSecurityHeader(options));
            return builder;
        }
        #endregion

        #region Content Type Options
        /// <summary>
        /// Adds the "X-Content-Type-Options" header with value "nosniff" to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> ContentTypeOptions(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder) {
            builder(_ => ContenTypeOptionsHeaderMiddleware.ContentTypeOptionsHeader());
            return builder;
        }
        #endregion

        #region Content Security Policy
        /// <summary>
        /// Adds the "Content-Security-Policy" (CSP) header with the given configuration to the response.
        /// </summary>
        /// <param name="builder">The OWIN builder instance.</param>
        /// <param name="configuration">The Content-Security-Policy configuration.</param>
        /// <returns>The OWIN builder instance.</returns>
        public static Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> ContentSecurityPolicy(this Action<Func<IDictionary<string, object>, Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>> builder, ContentSecurityPolicyConfiguration configuration) {
            configuration.MustNotNull("configuration");
            builder(_ => ContentSecurityPolicyMiddleware.ContentSecurityPolicyHeader(configuration));
            return builder;
        }
        #endregion
    }
}