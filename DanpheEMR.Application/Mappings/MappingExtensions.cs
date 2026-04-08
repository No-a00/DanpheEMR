using AutoMapper;
using AutoMapper.QueryableExtensions;
using DanpheEMR.Application.Common.Models; 
using Microsoft.EntityFrameworkCore;


namespace DanpheEMR.Application.Mappings
{
    public static class MappingExtensions
    {
        public static async Task<PagedResult<TDestination>> ProjectToPagedResultAsync<TDestination>(
            this IQueryable queryable,
            IConfigurationProvider configuration,
            int pageNumber,
            int pageSize)
        {
           
            var projectedQuery = queryable.ProjectTo<TDestination>(configuration);
            var count = await projectedQuery.CountAsync();
            var items = await projectedQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TDestination>(items, count, pageNumber, pageSize);
        }
    }
}