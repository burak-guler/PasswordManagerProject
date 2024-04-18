using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using WebApi.Controllers;

namespace PasswordManager.UnitTest
{
    public class LanguageControllerTest
    {
        private readonly LanguageController _controller;
        private readonly Mock<ILanguageService> _mockLanguageService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<ILog> _mockLogger;

        public LanguageControllerTest()
        {
            _mockLanguageService = new Mock<ILanguageService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockLogger = new Mock<ILog>();

            _controller = new LanguageController(
                _mockHttpContextAccessor.Object,
                _mockMemoryCache.Object,
                _mockLanguageService.Object            
               );
        }


        [Fact]
        public async Task GetAllCategory_ReturnsNonEmptyCategoryList()
        {
            // Arrange
            var languagies = new List<Language>
            {
                new Language { LangName="Türkçe", Lang_ISO= "tr-TR", LangID=1 },
                new Language { LangName="Türkçe", Lang_ISO= "tr-TR", LangID=1 }
            };

            _mockLanguageService.Setup(x => x.GetAll()).ReturnsAsync(languagies);

            // Act
            var actionResult = await _controller.GetAllLanguages();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Language>>(okObjectResult.Value);

            // Assert that the returned list is not null and contains items
            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

    }
}
