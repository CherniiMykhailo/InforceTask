using System;
using System.Linq;
using System.Threading.Tasks;
using InforceTask.Models;
using InforceTask.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace InforceTask.Services
{
    public class UrlShortenerService
    {
        private readonly IUrlRepository _repository;
        private readonly Random _random = new Random();
        private const int NumberOfCharsInShortLink = 8;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public UrlShortenerService(IUrlRepository repository)
        {
            _repository = repository;
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

                if (!await _repository.GetallDbContext().Set<Url>().AnyAsync(x => x.ShortUrl == code))
                {
                    return code;
                }
            }
        }
    }
}
