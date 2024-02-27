using Cars.Controllers;
using Cars.Database;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cars.Tests
{
    public class CarControllerTests
    {
        private readonly CarController _carController;
        private readonly Mock<ILogger<CarController>> _loggerMock = new Mock<ILogger<CarController>>();
        private readonly Mock<DbConnection> _dbConnectionMock = new Mock<DbConnection>();

        public CarControllerTests()
        {
            _carController = new CarController(_loggerMock.Object, _dbConnectionMock.Object);
        }

        [Fact]
        public void CreateCar_ValidCar_ReturnsOk()
        {
            // Arrange
            var newCar = new Car 
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Price = 50000,
                Year = new DateTime(2022,1 ,1),
                Image = "car_image_url",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _dbConnectionMock.Setup(db => db.Car.Add(newCar)).Verifiable();
            _dbConnectionMock.Setup(db => db.SaveChanges()).Verifiable();

            // Act
            var result = _carController.CreateCar(newCar) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(newCar, result.Value);
            _dbConnectionMock.Verify(db => db.Car.Add(newCar), Times.Once());
            _dbConnectionMock.Verify(db => db.SaveChanges(), Times.Once);
        }
    }
}
