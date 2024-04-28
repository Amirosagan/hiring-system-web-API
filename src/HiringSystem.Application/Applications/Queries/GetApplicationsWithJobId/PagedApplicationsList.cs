using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HiringSystem.Application.Applications.Queries.GetApplicationsWithJobId;

public class PagedApplicationsList
{
    public PagedApplicationsList(List<ApplicationInList> items, int page, int pageSize, int total)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        Total = total;
        HasNext = page < (int)Math.Ceiling(total / (double)pageSize);
        HasPrevious = page > 1;
    }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public List<ApplicationInList> Items { get; set; }
    
    
    public static async Task<PagedApplicationsList> CreateAsync(IQueryable<Domain.Application.Application> source, int page, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new ApplicationInList(
                x.Id.ToString(),
                x.Resume,
                x.Supportive,
                x.JobId.ToString(),
                x.CreatedAt
            )
        ).ToListAsync();
        
        return new PagedApplicationsList(items, page, pageSize, count);
    }
   
    
}