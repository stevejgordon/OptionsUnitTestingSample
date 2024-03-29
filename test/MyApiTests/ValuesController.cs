using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using MyApi.Config;
using MyApi.Controllers;
using Xunit;

namespace MyApiTests
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Get_ReturnsNotFoundResult_WhenConfigIsDisabled()
        {
            var options = Options.Create(new MyConfig {IsEnabled = false});

            var sut = new ValuesController(options);

            var result = sut.Get();

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Get_ReturnsOkResult_WhenConfigIsEnabled()
        {
            var options = Options.Create(new MyConfig { IsEnabled = true, Text = "Hello" });

            var sut = new ValuesController(options);

            var result = sut.Get();

            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be("Hello");
        }

        [Fact]
        public void Get_ReturnsNotFoundResult_WhenConfigIsDisabled_UsingMock()
        {
            var options = new Mock<IOptions<MyConfig>>();
            options.Setup(x => x.Value).Returns(new MyConfig {IsEnabled = false});

            var sut = new ValuesController(options.Object);

            var result = sut.Get();

            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
