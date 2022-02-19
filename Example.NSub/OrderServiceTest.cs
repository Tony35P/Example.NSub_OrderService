using MyCart.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.NSub
{
    [TestFixture]
    public class OrderServiceTest
    {
        [Test]
        public void CheckOut_金額過低_Return異常()
        {
            var orderContext = new OrderEntity { OrderTotal = 99 };
            IShippingCostProvider provider = NSubstitute.Substitute.For<IShippingCostProvider>(); //藉由隔離框架產生計算運費的物件，這個物件其實根本不存在

            var service = new OrderService(provider);

            var ex = Assert.Throws<Exception>(() => service.CheckOut(orderContext));

            Assert.That(() => ex.Message.Contains("無法出貨"));
        }

        [Test]
        public void CheckOut_金額過高_不允許線上購物()
        {
            var orderContext = new OrderEntity { OrderTotal = 290000 };
            IShippingCostProvider provider = NSubstitute.Substitute.For<IShippingCostProvider>(); //藉由隔離框架產生計算運費的物件，這個物件其實根本不存在

            var service = new OrderService(provider);

            var ex = Assert.Throws<Exception>(() => service.CheckOut(orderContext));

            Assert.That(() => ex.Message.Contains("無法線上購物"));
        }

        [Test]
        public void CheckOut_可正常出貨()
        {
            var orderContext = new OrderEntity { OrderTotal = 1000, ShippingMethod = enumShippingMethod.PostOffice };

            IShippingCostProvider provider = NSubstitute.Substitute.For<IShippingCostProvider>();
            provider.CalcShippingCost(Arg.Any<OrderEntity>()).Returns(80);

            var service = new OrderService(provider);

            service.CheckOut(orderContext);

            int expected = 1080;
            int actual = orderContext.TotalWithShippingCost;

            Assert.AreEqual(expected, actual);
        }
    }
}
