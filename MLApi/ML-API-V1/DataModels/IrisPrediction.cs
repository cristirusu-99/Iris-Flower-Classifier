using Microsoft.ML.Data;

namespace ML_API_V1.DataModels
{
    public class IrisPrediction : IrisData
    {
        [ColumnName("PredictedLabel")]
        public uint ClusterPrediction { get; set; }

        [ColumnName("Score")]
        public float[] Distances;
    }
}
