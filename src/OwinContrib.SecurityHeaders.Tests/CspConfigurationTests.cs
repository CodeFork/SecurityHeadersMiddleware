﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SecurityHeadersMiddleware.Tests {
    public class CspConfigurationTests {
        [Fact]
        public void When_generating_header_value_and_no_configurations_are_set_return_empty_string() {
            var config = new ContentSecurityPolicyConfiguration();

            config.ToHeaderValue().Should().BeEmpty();
        }

        [Fact]
        public void When_set_scriptSrc_to_none_the_header_value_should_contain_the_directive_with_none() {
            var config = new ContentSecurityPolicyConfiguration();
            config.ScriptSrc.SetToNone();

            config.ToHeaderValue().Should().Be("script-src 'none'");
        }

        [Fact]
        public void When_set_two_sources_they_should_be_separated_by_a_semicolon() {
            var config = new ContentSecurityPolicyConfiguration();
            config.ScriptSrc.AddScheme("https");
            config.ImgSrc.AddKeyword(CspKeyword.Self);

            var value = config.ToHeaderValue();
            var split = value.Split(new[] { ";" }, StringSplitOptions.None);

            split.Length.Should().Be(2);
            split.Should().Contain(item => item.Trim().Equals("script-src https:"));
            split.Should().Contain(item => item.Trim().Equals("img-src 'self'"));
        }

        [Fact]
        public void Source_types_should_be_separated_by_a_semicolon() {
            var config = new ContentSecurityPolicyConfiguration();
            config.StyleSrc.AddKeyword(CspKeyword.Self);
            config.ImgSrc.AddScheme("https");
            config.MediaSrc.AddKeyword(CspKeyword.UnsafeInline);
            config.BaseUri.AddScheme("https");

            var split = config.ToHeaderValue().Split(new[] { ";" }, StringSplitOptions.None);

            split.Length.Should().Be(4);
        }


        [Fact]
        public void All_source_types_should_be_in_the_header_value() {
            var config = new ContentSecurityPolicyConfiguration();
            config.BaseUri.AddKeyword(CspKeyword.Self);
            config.ChildSrc.AddKeyword(CspKeyword.Self);
            config.ConnectSrc.AddKeyword(CspKeyword.Self);
            config.DefaultSrc.AddKeyword(CspKeyword.Self);
            config.FontSrc.AddKeyword(CspKeyword.Self);
            config.FormAction.AddKeyword(CspKeyword.Self);
            config.FrameAncestors.AddKeyword(CspKeyword.Self);
            config.FrameSrc.AddKeyword(CspKeyword.Self);
            config.ImgSrc.AddKeyword(CspKeyword.Self);
            config.MediaSrc.AddKeyword(CspKeyword.Self);
            config.ObjectSrc.AddKeyword(CspKeyword.Self);
            config.ScriptSrc.AddKeyword(CspKeyword.Self);
            config.StyleSrc.AddKeyword(CspKeyword.Self);

            var expected = new List<string> {
                "base-uri", "child-src", "connect-src", "default-src","font-src", "form-action", "frame-ancestors", "frame-src",
                "img-src", "media-src", "object-src", "plugin-types", "referrer", "reflected-xss", "report-uri", "sandbox", 
                "script-src", "style-src"
            };

            var values = config.ToHeaderValue().Split(new[] { ";" }, StringSplitOptions.None).SelectMany(i => i.Split(new[] { " " }, StringSplitOptions.None)).ToList();
            values.Should().Contain(expected);
        }
    }
}