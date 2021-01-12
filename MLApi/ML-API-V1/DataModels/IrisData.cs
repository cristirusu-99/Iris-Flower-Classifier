using Microsoft.ML.Data;

namespace ML_API_V1.DataModels
{
    public class IrisData
    {
        [LoadColumn(0)]
        public float SepalLength { get; set; }

        [LoadColumn(1)]
        public float SepalWidth { get; set; }

        [LoadColumn(2)]
        public float PetalLength { get; set; }

        [LoadColumn(3)]
        public float PetalWidth { get; set; }

        [LoadColumn(4)]
        [LoadColumnName("Label")]
        public string IrisType { get; set; }
    }
}
