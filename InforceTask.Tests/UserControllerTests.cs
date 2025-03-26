using Microsoft.AspNetCore.Mvc;
using Moq;
using InforceTask.Controllers;
using InforceTask.Models.Repository;
using InforceTask.Models;

namespace InforceTask.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUrlRepository> _repositoryMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _repositoryMock = new Mock<IUrlRepository>();
            _controller = new UserController(_repositoryMock.Object);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var result = _controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_WithEmptyUrl_ReturnsViewWithError()
        {
            var originalUrl = string.Empty;

            var result = await _controller.Create(originalUrl);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Contains(_controller.ModelState.Keys, key => key == "originalUrl");
        }

        [Fact]
        public void Details_ValidId_ReturnsViewResult()
        {
            var url = new Url { Id = 1, OriginalUrl = "https://example.com", ShortUrl = "shortUrl" };
            _repositoryMock.Setup(r => r.Urls).Returns(new[] { url }.AsQueryable());
            var result = _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(url, viewResult.Model);
        }

        [Fact]
        public void Details_InvalidId_ReturnsNotFoundResult()
        {
            _repositoryMock.Setup(r => r.Urls).Returns(Enumerable.Empty<Url>().AsQueryable());

            var result = _controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

     
    }
}
