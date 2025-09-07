using Dota2WinPredictor.Data;
using Dota2WinPredictor.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dota2WinPredictor.Repositories
{
    public interface IDataRepository
    {
        Task<List<HeroDTO>> FetchExternalHeroDataAsync();
        Task<List<HeroDTO>> GetData(PredictRequest request);
        Task SaveHeroesToDbAsync(List<HeroDTO> heroes);

    }

    public class DataRepository : IDataRepository
    {
        private readonly HttpClient _httpClient;
        private readonly MyAppContext _context;


        public DataRepository(HttpClient httpClient, MyAppContext context)
        {
            _httpClient = httpClient;
            _context = context;

        }

        public async Task<List<HeroDTO>> GetData(PredictRequest request)
        {
            var allHeroes = new HashSet<string>(request.namesA.Concat(request.namesB));

            var heroes = new List<HeroDTO>();

            foreach (var element in allHeroes)
            {
                var existingHero = await _context.HeroDTOs.FirstOrDefaultAsync(h => h.Name == element);
                if (existingHero != null)
                {
                    heroes.Add(existingHero);
                }

            }

            return heroes;

        }
        public async Task<List<HeroDTO>> FetchExternalHeroDataAsync()
        {
            
            var response = await _httpClient.GetAsync("https://valorant-api.com/v1/agents");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement.GetProperty("data");

            var heroes = new List<HeroDTO>();

            foreach (var element in root.EnumerateArray())
            {
                Random random = new Random();
                int pc = random.Next(1001, 10000);
                double val = random.NextDouble();
                double scaled = 0.28 + (val * (0.72 - 0.28));
                int wc = (int)(scaled * pc);

                var hero = new HeroDTO
                {
                    Id = element.GetProperty("uuid").GetString(),
                    Name = element.GetProperty("displayName").GetString(),
                    Icon = element.GetProperty("displayIconSmall").GetString(),
                    Potrait = element.GetProperty("fullPortrait").GetString(),
                    PickCount = pc,
                    WinCount = wc,
                    WinRate = scaled
                };

                heroes.Add(hero);
            }

            return heroes;
        }

        public async Task SaveHeroesToDbAsync(List<HeroDTO> heroDtos)
        {
            foreach (var dto in heroDtos)
            {
                
                var existingHero = await _context.HeroDTOs.FirstOrDefaultAsync(h => h.Id == dto.Id);

                if (existingHero == null)
                {
                    _context.HeroDTOs.Add(dto);
                }
                else
                {
                    // Update existing hero
                    existingHero.Name = dto.Name ?? string.Empty;
                    existingHero.Icon = dto.Icon;
                    existingHero.Potrait = dto.Potrait;
                    existingHero.PickCount = dto.PickCount;
                    existingHero.WinCount = dto.WinCount;
                    existingHero.WinRate = dto.WinRate;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
