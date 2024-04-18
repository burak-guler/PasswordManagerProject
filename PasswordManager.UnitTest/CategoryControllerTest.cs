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
    public class CategoryControllerTest
    {
        private readonly CategoryController _controller;
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<ILog> _mockLogger;

        public CategoryControllerTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockLogger = new Mock<ILog>();

            _controller = new CategoryController(
                _mockCategoryService.Object,
                _mockHttpContextAccessor.Object,
                _mockLogger.Object,
                _mockMemoryCache.Object);
        }

        [Fact]
        public async Task GetAllCategory_ReturnsNonEmptyCategoryList()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Category 1" },
                new Category { CategoryID = 2, CategoryName = "Category 2" }
            };

            _mockCategoryService.Setup(x => x.GetAll()).ReturnsAsync(categories);

            // Act
            var actionResult = await _controller.GetAllCategory();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Category>>(okObjectResult.Value);

            // Assert that the returned list is not null and contains items
            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task GetAllByCompanyIDCategory_WithValidID_ReturnsNonEmptyCategoryList()
        {
            // Arrange
            var companyId = 1;
            var categories = new List<Category>
            {
                new Category { CategoryID = 1, CategoryName = "Category 1", CompanyID = companyId },
                new Category { CategoryID = 2, CategoryName = "Category 2", CompanyID = companyId }
            };

            _mockCategoryService.Setup(x => x.GetAllByCompanyId(companyId)).ReturnsAsync(categories);

            // Act
            var actionResult = await _controller.GetAllBYCompanyIDCategory(companyId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Category>>(okObjectResult.Value);

            
            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task GetCategory_WithValidID_ReturnsCategory()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { CategoryID = categoryId, CategoryName = "Category 1" };

            _mockCategoryService.Setup(x => x.GetById(categoryId)).ReturnsAsync(category);

            // Act
            var actionResult = await _controller.GetCategory(categoryId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsType<Category>(okObjectResult.Value);

            
            Assert.NotNull(model);
        }

        [Fact]
        public async Task AddCategory_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var newCategory = new Category { CategoryName = "New Category" };

            // Act
            var actionResult = await _controller.AddCategory(newCategory);

            // Assert
            var okResult = Assert.IsType<OkResult>(actionResult);
            //_mockCategoryService.Verify(x => x.Add(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCategory_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var categoryToUpdate = new Category { CategoryID = 1, CategoryName = "Updated Category" };

            // Act
            var actionResult = await _controller.UpdateCategory(categoryToUpdate);

            // Assert
            var okResult = Assert.IsType<OkResult>(actionResult);
            _mockCategoryService.Verify(x => x.Update(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public async Task RemoveCategory_WithValidID_ReturnsOkResult()
        {
            // Arrange
            var categoryId = 1;

            // Act
            var actionResult = await _controller.RemoveCategory(categoryId);

            // Assert
            var okResult = Assert.IsType<OkResult>(actionResult);
            _mockCategoryService.Verify(x => x.Remove(categoryId), Times.Once);
        }
    


    }
}
