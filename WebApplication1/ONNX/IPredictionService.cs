using WebApplication1.Model;

namespace WebApplication1.ONNX
{
    public interface IPredictionService
    {
        ModelOutput Predict(float[] imageData);
    }
}
