using Microsoft.ML.Data;

namespace Trips.ML.API.Interfaces.Services
{
    public interface ILearningService
    {
        void PrepareData();
        RegressionMetrics Learn();
        

    }
}
