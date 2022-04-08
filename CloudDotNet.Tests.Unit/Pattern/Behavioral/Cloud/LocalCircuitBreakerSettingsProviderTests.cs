using CloudDotNet.Pattern.Behavioral.Cloud;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CloudDotNet.Tests.Unit.Pattern.Behavioral.Cloud;

public class LocalCircuitBreakerSettingsProviderTests
{
    [Fact]
    public async Task Set_GetAsync()
    {
        var provider = new LocalCircuitBreakerSettingsProvider();
        var setting = new CircuitBreakerSetting("test", 1, TimeSpan.FromMilliseconds(100), 1, 1);
        await provider.SaveAsync(setting).ConfigureAwait(false);
        var setting2 = await provider.GetAsync("test").ConfigureAwait(false);
        setting2.RetrySuccessThreshold.Should().Be(setting.RetrySuccessThreshold);
        setting2.Key.Should().Be(setting.Key);
        setting2.FailureThreshold.Should().Be(setting.FailureThreshold);
        setting2.RetryTimeout.Should().Be(setting.RetryTimeout);
    }

    [Fact]
    public async void Get_NotExisting()
    {
        Func<Task<CircuitBreakerSetting>> func = async () => await new LocalCircuitBreakerSettingsProvider().GetAsync("test").ConfigureAwait(false);
        await func.Should().ThrowAsync<KeyNotFoundException>();
    }
}