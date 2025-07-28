using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLog.Slack.Tests
{
    [TestClass]
    public class SlackTargetTests
    {
        [TestMethod]
        public void DefaultSettings_ShouldBeCorrect()
        {
            var slackTarget = new TestableSlackTarget();

            Assert.IsFalse(slackTarget.Compact);
            Assert.IsNull(slackTarget.WebHookUrl);
        }

        [TestMethod]
        public void CustomSettings_ShouldBeCorrect()
        {
            const bool compact = true;
            const string webHookUrl = "http://slack.is.awesome.com";

            var slackTarget = new TestableSlackTarget
                {
                    Compact = compact,
                    WebHookUrl = webHookUrl
                };

            var logEvent = new LogEventInfo { Level = LogLevel.Info, Message = "This is a ${level} message" };

            Assert.AreEqual(slackTarget.Compact, compact);
            Assert.AreEqual(slackTarget.WebHookUrl, webHookUrl);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_EmptyWebHookUrl_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget();

            slackTarget.Initialize();
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_IncorrectWebHookUrl_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget
                {
                    WebHookUrl = "IM A BIG FAT PHONY"
                };

            slackTarget.Initialize();
        }
    }
}