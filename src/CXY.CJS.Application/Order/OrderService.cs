using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 订单服务
    /// </summary>
    [Authorize]
    public class OrderService : CJSAppServiceBase, IOrderService
    {
    }
}
