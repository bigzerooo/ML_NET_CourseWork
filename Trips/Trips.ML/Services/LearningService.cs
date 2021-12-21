using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Trips.Data.Context;
using Trips.Data.Services;
using Trips.ML.API.Extensions;
using Trips.ML.API.Models;
using Trips.ML.Interfaces;

namespace Trips.ML.Services
{
    public class LearningService : BaseService, ILearningService
    {
        private static readonly string ModelPath = "model.zip".AbsolutePath();

        public LearningService(TripDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void Learn()
        {
            var mlContext = new MLContext();

            var trainingDataView = LoadData(mlContext);

            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

            SaveModel(mlContext, trainingDataView.Schema, model);
        }

        private List<TripRating> GetAllRating()
        {
            var rating = _dbContext.Rating.ToList();

            var preparedRating = _mapper.Map<List<TripRating>>(rating);

            return preparedRating;
        }

        private IDataView LoadData(MLContext mlContext)
        {
            var rating = GetAllRating();

            var trainingDataView = mlContext.Data.LoadFromEnumerable(rating);

            return trainingDataView;
        }

        private static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
        {
            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "TripIdEncoded", inputColumnName: "TripId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "UserIdEncoded",
                MatrixRowIndexColumnName = "TripIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 1000,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }

        private static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            mlContext.Model.Save(model, trainingDataViewSchema, ModelPath);
        }
    }
}
