using Microsoft.ML.Data;
namespace WebApplication1.Model
{

    internal class ModelInput
    {
        [VectorType(1, 224, 224, 3)]
        public float[] input;
    }
}
