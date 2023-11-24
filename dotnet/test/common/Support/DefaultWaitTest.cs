using System;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace OpenQA.Selenium.Support
{
    public class DefaultWaitTest
    {
        [Test]
        public async Task ReturnWhenTrue()
        {
            var sut = new DefaultWait<string>("SomeString");
            await sut.UntilAsync(_ => true);
        }

        [Test]
        public void ThrowWhenFalseAndTimeout()
        {
            var sut = new DefaultWait<string>("SomeString")
            {
                Timeout = TimeSpan.FromSeconds(2)
            };
            Assert.ThrowsAsync<WebDriverTimeoutException>(async () => await sut.UntilAsync(_ => false));
        }

        [Test]
        public async Task ReturnWhenNotNull()
        {
            var sut = new DefaultWait<string>("SomeString");
            await sut.UntilAsync(_ => new object());
        }

        [Test]
        public void ThrowWhenNullAndTimeout()
        {
            var sut = new DefaultWait<string>("SomeString")
            {
                Timeout = TimeSpan.FromSeconds(2)
            };
            Assert.ThrowsAsync<WebDriverTimeoutException>(async () => await sut.UntilAsync<object>(_ => null));
        }

        [Test]
        public void RethrowUnknownExceptionForBoolean()
        {
            var sut = new DefaultWait<string>("SomeString");
            Assert.ThrowsAsync<ArgumentException>(async () => await sut.UntilAsync(_ => throw new ArgumentException("Dummy exception")));
        }

        [Test]
        public void IgnoreKnownExceptionForBoolean()
        {
            var sut = new DefaultWait<string>("SomeString")
            {
                Timeout = TimeSpan.FromSeconds(2)
            };
            sut.IgnoreExceptionTypes(typeof(ArgumentException));
            Assert.ThrowsAsync<WebDriverTimeoutException>(async () => await sut.UntilAsync(_ => throw new ArgumentException("Dummy exception")));
        }

        [Test]
        public void RethrowUnknownExceptionForClass()
        {
            var sut = new DefaultWait<string>("SomeString");
            Assert.ThrowsAsync<ArgumentException>(async () => await sut.UntilAsync<object>(_ => throw new ArgumentException("Dummy exception")));
        }

        [Test]
        public void IgnoreKnownExceptionForClass()
        {
            var sut = new DefaultWait<string>("SomeString")
            {
                Timeout = TimeSpan.FromSeconds(2)
            };
            sut.IgnoreExceptionTypes(typeof(ArgumentException));
            Assert.ThrowsAsync<WebDriverTimeoutException>(async () => await sut.UntilAsync<object>(_ => throw new ArgumentException("Dummy exception")));
        }
    }
}
