using HiringSystem.Application.Jobs.Queries.GetJobs;
using HiringSystem.Domain.Job;

using Microsoft.EntityFrameworkCore;

namespace HiringSystem.Application.Jobs.Common;

public class PagedJobList
{
    public PagedJobList(List<JobInList> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public List<JobInList> Items { get; set; }
    
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    public int TotalCount { get; set; }
    
    public bool HasNextPage => Page * PageSize < TotalCount;
    
    public bool HasPreviousPage => Page > 1;
    
    public static async Task<PagedJobList> CreateAsync(IQueryable<Job> source, int page, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new JobInList(
                x.Id.ToString(),
                x.Title,
                x.WorkPlace.ToString(),
                x.JobType.ToString(),
                x.Talent.ProfilePicture,
                x.Talent.Name,
                x.CreatedAt
            )
        ).ToListAsync();
        
        return new PagedJobList(items, page, pageSize, count);
    }
    
    
    
    
    
    
}