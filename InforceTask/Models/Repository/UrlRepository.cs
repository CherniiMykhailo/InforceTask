
using Microsoft.EntityFrameworkCore;

namespace InforceTask.Models.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlDbContex contex;

        public UrlRepository(UrlDbContex ctx)
        {
            contex = ctx;
        }

        public DbContext GetDbContext()
        {
            return contex;
        }

        public UrlDbContex GetallDbContext()
        {
            return contex;
        }

        public IQueryable<Url> Urls => this.contex.Urls;

        public void CreateUrl(Url url)
        {
            contex.Add(url);
            contex.SaveChanges();
        }

        public void DeleteUrl(Url url)
        {
            contex.Remove(url);
            contex.SaveChanges();
        }

        public void SaveUrl(Url url)
        {
            if (url.Id == 0)
            {
                contex.Urls.Add(url);
            }
            else
            {
                Url? dbEntry = contex.Urls?.FirstOrDefault(p => p.Id == url.Id);

                if (dbEntry != null)
                {
                    dbEntry.OriginalUrl = url.OriginalUrl;
                    dbEntry.ShortUrl = url.ShortUrl;
                    dbEntry.CreatedBy = url.CreatedBy;
                    dbEntry.CreatedDate = url.CreatedDate;
                }
            }

            contex.SaveChanges();
        }
    }
}
