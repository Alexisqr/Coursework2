using Microsoft.ML.Data;

namespace WebApplication1.Model
{
    public class ModelOutput
    {
        [VectorType(1, 120)]
        public float[] dense_4;
    }
}
