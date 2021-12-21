using Microsoft.ML;
using Trips.ML.API.Interfaces.Services;
using Trips.ML.API.Extensions;
using Trips.ML.API.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Data;

namespace Trips.ML.API.Services
{
    public class LearningService : ILearningService
    {
        private static readonly string BaseDataSetRelativepath = @"../../../Data";

        private static readonly string DataLocation = $"{BaseDataSetRelativepath}/trips.csv".AbsolutePath();

        private static readonly string TrainingDataLocation = $"{BaseDataSetRelativepath}/trips_train.csv".AbsolutePath();
        private static readonly string TestDataLocation = $"{BaseDataSetRelativepath}/trips_test.csv".AbsolutePath();

        private static readonly string ModelPath = @"../../../Model/model.zip".AbsolutePath();

        public RegressionMetrics Learn()
        {
            var mlContext = new MLContext();

            (IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);

            ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

            //var metrics = EvaluateModel(mlContext, testDataView, model);

            SaveModel(mlContext, trainingDataView.Schema, model);

            return null;
        }

        public void PrepareData()
        {
            string[] dataset = File.ReadAllLines(DataLocation);

            string[] new_dataset = new string[dataset.Length];
            new_dataset[0] = dataset[0];
            for (int i = 1; i < dataset.Length; i++)
            {
                string line = dataset[i];
                string[] lineSplit = line.Split(',');
                int rating = int.Parse(lineSplit[2]);
                rating = rating > 3 ? 1 : 0;
                lineSplit[2] = rating.ToString();
                string new_line = string.Join(',', lineSplit);
                new_dataset[i] = new_line;
            }
            dataset = new_dataset;
            int numLines = dataset.Length;
            var body = dataset.Skip(1).ToArray();
            body.Shuffle();
            
            File.WriteAllLines(TrainingDataLocation, dataset.Take(1).Concat(body.Take((int)(numLines * 0.9))));
            File.WriteAllLines(TestDataLocation, dataset.Take(1).Concat(body.TakeLast((int)(numLines * 0.1))));
        }

        private static (IDataView trainingData, IDataView testData) LoadData(MLContext mlContext)
        {
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<TripRating>(TrainingDataLocation, hasHeader: true, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<TripRating>(TestDataLocation, hasHeader: true, separatorChar: ',');

            return (trainingDataView, testDataView);
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
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            ITransformer model = trainerEstimator.Fit(trainingDataView);

            return model;
        }

        private static RegressionMetrics EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
        {
            var prediction = model.Transform(testDataView);

            RegressionMetrics metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

            return metrics;
        }

        private static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            mlContext.Model.Save(model, trainingDataViewSchema, ModelPath);
        }
    }
}
