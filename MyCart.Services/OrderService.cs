using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCart.Services
{
    public class OrderService
    {
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
