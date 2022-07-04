using App.Core.Domain;
using App.Core.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Helpers
{
    public static class PagedListExtension
    {
        /// <summary>
        /// Pagination for IQueryable 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="souuce"></param>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> souuce, PagingInfo pagingInfo)
        {
            pagingInfo.TotalCount = await souuce.CountAsync();
            var pagedList = new PagedList<T>(await souuce.Skip(pagingInfo.CurrentPage * pagingInfo.PageSize)
                .Take(pagingInfo.PageSize).ToListAsync());
            return pagedList;    

        }
    }
}
