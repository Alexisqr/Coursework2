using Microsoft.ML.Data;

namespace WebApplication1.Model
{
    public class ModelOutputCat
    {
        [VectorType(1, 20)]
        public float[] dense_1;
    }
}
