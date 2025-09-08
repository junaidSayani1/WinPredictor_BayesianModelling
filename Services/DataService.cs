using Dota2WinPredictor.Models;
using Dota2WinPredictor.Repositories;

namespace Dota2WinPredictor.Services
{
    public interface IDataService
    {
        Task<List<HeroDTO>> GetDataAsync();
        Task<PredictResponse> PredictBayes(PredictRequest request);
    }
    public class DataService:  IDataService
    {
        private readonly IDataRepository _dataRepository;
        private double priorMean;
        private double priorStrength;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            priorMean = 0.5;
            priorStrength = 100.0;
        }
        public async Task<List<HeroDTO>> GetDataAsync()
        {

            var result = await _dataRepository.FetchExternalHeroDataAsync();
            await _dataRepository.SaveHeroesToDbAsync(result);
            return result;
        }

        public async Task<PredictResponse> PredictBayes(PredictRequest request)
        {
            var data = await _dataRepository.GetData(request);

            var teamA = data
                .Where(h => request.namesA.Contains(h.Name))
                .ToList();

            var teamB = data
                .Where(h => request.namesB.Contains(h.Name))
                .ToList();

            double N = priorStrength;

            double teamAScore = 0.0;
            double teamBScore = 0.0;

            foreach (var hero in teamA)
            {
                int games = (int)hero.PickCount;
                double heroWinRate = (double)hero.WinRate;

                double adjustedBayesianShrinkedWinRate = (games * heroWinRate + N * priorMean) / (games + N);
                double strengthScore = Math.Log(adjustedBayesianShrinkedWinRate / (1 - adjustedBayesianShrinkedWinRate));
                hero.AdjustedWinRate = adjustedBayesianShrinkedWinRate;
                hero.LogOdds = strengthScore;

                teamAScore = teamAScore + strengthScore;

            }

            foreach (var hero in teamB)
            {
                int games = (int)hero.PickCount;
                double heroWinRate = (double)hero.WinRate;

                double adjustedBayesianShrinkedWinRate = (games * heroWinRate + N * priorMean) / (games + N);
                double strengthScore = Math.Log(adjustedBayesianShrinkedWinRate / (1 - adjustedBayesianShrinkedWinRate));
                hero.AdjustedWinRate = adjustedBayesianShrinkedWinRate;
                hero.LogOdds = strengthScore;

                teamBScore = teamBScore + strengthScore;

            }

            double teamAdvantage = teamAScore - teamBScore;
            double teamAWinChance = 1.0 / (1.0 + Math.Exp(-teamAdvantage));
            double teamBWinChance = 1.0 - teamAWinChance;

            var response = new PredictResponse { 

                    TeamA = teamA,
                    TeamB = teamB,
                    TeamAAdvantage = teamAdvantage,
                    TeamAWinChance = teamAWinChance,
                    TeamBWinChance = teamBWinChance
                };
            
            return response;
        }

    }
}
