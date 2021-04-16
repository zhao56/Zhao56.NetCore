using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhao56.Core.Paging;

namespace Zhao56.Core.BaseModel.Paging
{
    public static class QueryableExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IOrderedQueryable<T> dataSource, int pageIndex, int pageSize) where T : BaseEntity
        {
            // todo 是否已经排序
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var pagedList = new PagedList<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = dataSource.Count(),
                Entities = dataSource.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
            };
            return pagedList;
        }


        public static IPagedList<T> ToPagedList<T>(this IOrderedQueryable<T> dataSource, Pager pager) where T : BaseEntity
        {
            pager = pager ?? new Pager();
            return dataSource.ToPagedList(pager.PageIndex, pager.PageSize);
        }
    }
}
