using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using ML_API_V1.DataModels;
using System;

namespace ML_API_V1.Controllers
{
    [Route("api/v1/predictions")]
    [ApiController]
    public class IrisController : ControllerBase
    {
        private readonly PredictionEnginePool<IrisData, IrisPrediction> _predictionEnginePool;

        public IrisController(PredictionEnginePool<IrisData, IrisPrediction> predictionEnginePool)
        {
            this._predictionEnginePool = predictionEnginePool;
        }

        private string GetTypeFromCluster(uint clusterId)
        {
            return clusterId switch
            {
                1 => "Iris Setosa",
                2 => "Iris Versicolor",
                3 => "Iris Virginica",
                _ => null,
            };
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] IrisData data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IrisPrediction predictedValue = _predictionEnginePool.Predict(modelName: "IrisModel", example: data);
            return Ok(GetTypeFromCluster(predictedValue.ClusterPrediction));
        }
    }
}
