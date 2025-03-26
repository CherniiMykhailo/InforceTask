using InforceTask.Services;
using InforceTask.Models.Repository;
using Microsoft.EntityFrameworkCore;
using InforceTask.Models;

namespace InforceTask.Tests
{
    public class UrlShortenerTests
    {
        private readonly UrlShortenerService _urlShortenerService;
        private readonly DbContextOptions<UrlDbContex> _options;

        public UrlShortenerTests()
        {
            _options = new DbContextOptionsBuilder<UrlDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new UrlDbContex(_options);
            var repository = new UrlRepository(context);
            _urlShortenerService = new UrlShortenerService(repository);
        }

        [Fact]
        public async Task GenerateUniqueCode_ShouldGenerateCodeWithEightCharacters()
        {
            var generatedCode = await _urlShortenerService.GenerateUniqueCode();

            Assert.NotNull(generatedCode);
            Assert.Equal(8, generatedCode.Length);
        }
    }
}
