using Moq;
using TestingLib.Shop;

namespace UnitTesting
{
    public class ShopTesting
    {
        private readonly Mock<INotificationService> mockNotificationService;
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<IOrderRepository> mockOrderRepository;

        public ShopTesting()
        {
            mockNotificationService = new Mock<INotificationService>();
            mockCustomerRepository = new Mock<ICustomerRepository>();
            mockOrderRepository = new Mock<IOrderRepository>();
        }

        [Fact]
        public void CreateOrder_ShouldThrowExceptionIfOrderExist()
        {
            Customer customer = new Customer { Id = 1, Name = "Роман", Email = "rv.sadovsky@mail.ru" };
            Order order = new Order { Id = 1, Date = DateTime.Now, Customer = customer, Amount = 99.9M };

            mockOrderRepository.Setup(repo => repo.GetOrderById(order.Id)).Returns(order);
            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);

            Assert.Throws<ArgumentException>(() => service.CreateOrder(order));
        }

        [Fact]
        public void CreateOrder_SendNotification()
        {
            Customer customer = new Customer { Id = 1, Name = "Роман", Email = "rv.sadovsky@mail.ru" };
            Order order = new Order { Id = 1, Date = DateTime.Now, Customer = customer, Amount = 99.9M };

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);
            service.CreateOrder(order);

            mockNotificationService.Verify(repo => repo.SendNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
        /// <summary>
        /// ////
        /// </summary>
        [Fact]
        public void GetCustomerInfo_ReturnsNumberOfOrders()
        {
            Customer customer = new Customer { Id = 1, Name = "Роман", Email = "rv.sadovsky@mail.ru" };
            Order order = new Order { Id = 1, Date = DateTime.Now, Customer = customer, Amount = 99.9M };

            List<Order> orders = new List<Order> { order };
            string expectedResult = "Customer " + customer.Name + " has " + orders.Count + " orders";

            mockCustomerRepository.Setup(repo => repo.GetCustomerById(customer.Id)).Returns(customer);
            mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(orders);
            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);
            var result = service.GetCustomerInfo(customer.Id);

            Assert.Equal(expectedResult, result);
        }
    }
}
