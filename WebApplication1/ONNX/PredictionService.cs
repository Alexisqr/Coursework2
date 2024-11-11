
using Microsoft.ML;
using System.IO;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using WebApplication1.Model;
using Microsoft.ML.OnnxRuntime.Tensors;
namespace WebApplication1.ONNX
{
    public class PredictionService : IPredictionService
    {
        private readonly PredictionEngine<ModelInput, ModelOutput> _predictionEngine;

        public PredictionService()
        {
            var mlContext = new MLContext();
            var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Model", "best_model_first.onnx");
           
            var pipeline = mlContext.Transforms.ApplyOnnxModel(
                modelFile: modelPath,
                outputColumnNames: new[] { "dense_4" },
                inputColumnNames: new[] { "input" }  // Використовуємо правильне ім'я "input"
            );

            var emptyData = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());
            var model = pipeline.Fit(emptyData);

            _predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        }

        public ModelOutput Predict(float[] imageData)
        {
            var inputTensor = PrepareImageTensor(imageData); // Створення тензора
            var input = new ModelInput { input = imageData };
            return _predictionEngine.Predict(input);
        }

        // Підготовка зображення як тензора
        private Tensor<float> PrepareImageTensor(float[] imageData)
        {
            var tensor = new DenseTensor<float>(new[] { 1, 224, 224, 3 });

            // Заповнення тензора значеннями з масиву
            for (int i = 0; i < imageData.Length; i++)
            {
                tensor[0, i / (224 * 3), (i % (224 * 3)) / 3, i % 3] = imageData[i];
            }
            return tensor;
        }
    }
}