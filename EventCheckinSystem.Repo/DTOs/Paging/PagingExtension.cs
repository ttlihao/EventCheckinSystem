using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.Paging
{
    public static class PagingExtensions
    {
        public static async Task<PagedResult<T>> CreatePagingAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await source.CountAsync();

            var items = await source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageIndex
            };
        }
    }
}
