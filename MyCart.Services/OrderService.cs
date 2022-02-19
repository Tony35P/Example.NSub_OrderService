using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCart.Services
{
    public class OrderService
    {
        IShippingCostProvider _ShippingCostProvider;
        public OrderService(IShippingCostProvider shippingCostProvider)
        {
            _ShippingCostProvider = shippingCostProvider;
        }

        /// <summary>
        /// 結帳100元以下和20萬以上，不允許結帳
        /// </summary>
        /// <param name="orderContext">訂單基本資料</param>
        public void CheckOut(OrderEntity orderContext)
        {
            if (orderContext.OrderTotal <= 100)
                throw new Exception("訂單金額過低，無法出貨");
            if (orderContext.OrderTotal >= 200000)
                throw new Exception("訂單金額過高，無法線上購物，將有專人為您服務");

            int shippingCost = _ShippingCostProvider.CalcShippingCost(orderContext);

            orderContext.ShippingCost = shippingCost;

            // todo 將訂單存放到資料庫
        }
    }

    public class OrderEntity
    {
        public int MemberId { get; set; }
        public int OrderTotal { get; set; }
        public enumShippingMethod ShippingMethod { get; set; }
        public int ShippingCost { get; set; }
        public int TotalWithShippingCost 
        { 
            get
            {
                return this.OrderTotal + ShippingCost;
            }
        }
    }

    public enum enumShippingMethod
    {
        TCat, // 黑貓
        PostOffice // 郵寄
    }

    public interface IShippingCostProvider
    {
        int CalcShippingCost(OrderEntity order);
    }

    ///// <summary>
    ///// 提供購物車相關服務
    ///// </summary>
    //public class ShippingCart : IShippingCostProvider
    //{
    //    /// <summary>
    //    /// 計算運費, 若總金額2000以上免運費, 2000以下 黑貓150 郵寄80
    //    /// </summary>
    //    /// <param name="order"></param>
    //    /// <returns></returns>
    //    /// <exception cref="NotImplementedException"></exception>
    //    public int CalcShippingCost(OrderEntity order)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
