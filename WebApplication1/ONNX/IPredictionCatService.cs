using WebApplication1.Model;

namespace WebApplication1.ONNX
{
        public interface IPredictionCatService
        {
            ModelOutputCat Predict(float[] imageData);
        }
    
}
