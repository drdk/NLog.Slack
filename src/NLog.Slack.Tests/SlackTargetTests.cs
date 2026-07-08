namespace NLog.Slack.Tests;

[TestFixture]
public class SlackTargetTests
{
    [Test]
    public void DefaultSettings_ShouldBeCorrect()
    {
        var slackTarget = new TestableSlackTarget();

        Assert.That(slackTarget.Compact, Is.False);
        Assert.That(slackTarget.WebHookUrl, Is.Null);
    }

    [Test]
    public void CustomSettings_ShouldBeCorrect()
    {
        const bool compact = true;
        const string webHookUrl = "http://slack.is.awesome.com";

        var slackTarget = new TestableSlackTarget
        {
            Compact = compact,
            WebHookUrl = webHookUrl
        };

        Assert.That(slackTarget.Compact, Is.EqualTo(compact));
        Assert.That(slackTarget.WebHookUrl.ToString(), Is.EqualTo(webHookUrl));
    }

    [Test]
    public void InitializeTarget_EmptyWebHookUrl_ShouldThrowException()
    {
        var slackTarget = new TestableSlackTarget();

        Assert.Throws<ArgumentOutOfRangeException>(slackTarget.Initialize);
    }

    [Test]
    public void InitializeTarget_IncorrectWebHookUrl_ShouldThrowException()
    {
        var slackTarget = new TestableSlackTarget
        {
            WebHookUrl = "I'M NOT AN URL"
        };

        Assert.Throws<ArgumentOutOfRangeException>(slackTarget.Initialize);
    }
}