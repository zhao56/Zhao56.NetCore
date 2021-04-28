using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.BaseModel;
using Zhao56.Core.BaseModel.Orders;
using Zhao56.Core.BaseModel.Paging;
using Zhao56.Core.Enums;
using Zhao56.Core.Extensions;
using Zhao56.Core.Paging;
using Zhao56.Core.TransResponse;

namespace Zhao56.Core.BaseProvider
{
    public abstract class BaseService<T, TRepository> where T : BaseEntity where TRepository : IRepository<T>
    {
        protected IRepository<T> repository;
        protected ResponseBase Response;
        public BaseService() { }
        public BaseService(TRepository repository)
        {
            Response = new ResponseBase();
            this.repository = repository;
        }
        protected ResponseBase OK()
        {
            Response.IsSuccess = true;
            Response.Result = null;
            return Response;
        }
        protected ResponseBase OK<TResult>(TResult value)where TResult:class,new()
        {
            Response.IsSuccess = true;
            Response.Result = value;
            return Response;
        }
        protected ResponseBase Err(string msg)
        {
            Response.IsSuccess = false;
            Response.Message = msg;
            return Response;
        }
        protected PageResponse<T> PageResult(IPagedList<T> value)
        {
            PageResponse<T> pageResponse = new PageResponse<T>();
            pageResponse.IsSuccess = true;
            pageResponse.Result = value;
            return pageResponse;
        }


        public virtual PageResponse<T> GetPageData(PageDataOptions request)
        {
            var props = typeof(T).GetProperties();
            //过滤掉参数名不正确的
            var conditions = request.Wheres.Where(w => props.Select(t => t.Name.ToUpper()).Contains(w.Name.ToUpper()));
            var orderConditions = request.Orders.Where(w => props.Select(t => t.Name.ToUpper()).Contains(w.Name.ToUpper()));
            //传入条件转换为表达式目录树
            Expression<Func<T, bool>> query = PressCondition(conditions);
            IOrder<T> order = PressOrders(orderConditions);
            var pageData = repository.GetPageData(query, order, request.Pager);
            return PageResult(pageData);
        }
        /// <summary>
        /// 多条件排序
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        private IOrder<T> PressOrders(IEnumerable<OrderParameters> orders)
        {
            IOrder<T> iOrder = new Order<T>();
            var parameter = Expression.Parameter(typeof(T), "p");
            foreach (var order in orders)
            {
                var temp = Expression.Lambda<Func<T, Object>>(ParseOrderExpressionBody(order, parameter), parameter);
                iOrder.ApplyOrderBy<T>(temp, order.IsAsc ? QueryOrderBy.Asc : QueryOrderBy.Desc);
            }
            
            return iOrder;
        }



        /// <summary>
        /// 排序转换为表达式
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private Expression<Func<T, bool>> PressCondition(IEnumerable<SearchParameters> conditions)
        {
            //将条件转化成表达是的Body
            var parameter = Expression.Parameter(typeof(T), "p");
            var query = ParseExpressionBody(conditions, parameter);
            return Expression.Lambda<Func<T, bool>>(query, parameter);
        }
        /// <summary>
        /// 条件转换为表达式
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private Expression ParseExpressionBody(IEnumerable<SearchParameters> conditions, ParameterExpression parameter)
        {
            if (conditions == null || conditions.Count() == 0)
            {
                return Expression.Constant(true, typeof(bool));
            }
            else if (conditions.Count() == 1)
            {
                var first = conditions.First();
                Expression first1 = first.Name.CreateConditionExpression(first.Value, parameter, first.OpType.StringToEfOpType());
                return first1;
            }
            else
            {
                var first = conditions.First();
                Expression queryExpression = first.Name.CreateConditionExpression(first.Value, parameter, first.OpType.StringToEfOpType());
                int i = 0;
                foreach (var item in conditions)
                {
                    if (i++ == 1)//刨除第一个元素
                    {
                        continue;
                    }
                    queryExpression = Expression.AndAlso(queryExpression, item.Name.CreateConditionExpression(item.Value, parameter, item.OpType.StringToEfOpType()));
                }
                return queryExpression;
            }
        }

        private Expression ParseOrderExpressionBody(OrderParameters condition, ParameterExpression parameter)
        {
            if (condition == null)
            {
                return Expression.Constant(true, typeof(bool));
            }
            else
            {
                Expression queryExpression = condition.Name.CreateOrdersExpression(parameter);
                return queryExpression;
            }
        }
    }
}
