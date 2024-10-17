using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TestingLib.Shop;
using TestingLib.Weather;

namespace UnitTesting
{
    public class WeatherTests
    {
        private readonly Mock<IWeatherForecastSource> mockWeatherForecastSource;

        public WeatherTests()
        {
            mockWeatherForecastSource = new Mock<IWeatherForecastSource>();
        }

        [Fact]
        public void GetWeatherForecast_ShouldReturnCorrectValue()
        {
            WeatherForecast weather = new WeatherForecast { Summary = "Облачно", TemperatureC = -2 };
            DateTime dateTime = DateTime.Now;

            mockWeatherForecastSource.Setup(repo => repo.GetForecast(dateTime)).Returns(weather);
            var service = new WeatherForecastService(mockWeatherForecastSource.Object);
            var result = service.GetWeatherForecast(dateTime);

            Assert.NotNull(result);
            mockWeatherForecastSource.Verify(repo => repo.GetForecast(It.IsAny<DateTime>()), Times.Once());
        }
    }
}
