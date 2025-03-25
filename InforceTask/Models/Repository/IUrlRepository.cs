using Microsoft.EntityFrameworkCore;

namespace InforceTask.Models.Repository
{
    public interface IUrlRepository
    {
        IQueryable<Url> Urls { get; }
        void SaveUrl(Url url);
        void CreateUrl(Url url);
        void DeleteUrl(Url url);
        DbContext GetDbContext();
    }
}
