using System;
using System.Linq;
using System.Threading.Tasks;
using InforceTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InforceTask.Services
{

    public class UrlShortenerService
    {
        private readonly DbContext _dbContext;
        private readonly Random _random = new Random();
        private const int NumberOfCharsInShortLink = 8;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public UrlShortenerService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateUniqueCode()
        {
            char[] codeChars = new char[NumberOfCharsInShortLink];

            while (true)
            {
                for (int i = 0; i < NumberOfCharsInShortLink; i++)
                {
                    int randomIndex = _random.Next(Alphabet.Length);
                    codeChars[i] = Alphabet[randomIndex];
                }

                string code = new string(codeChars);

                if (!await _dbContext.Set<Url>().AnyAsync(x => x.ShortUrl == code))
                {
                    return code;
                }
            }
        }
    }
}
