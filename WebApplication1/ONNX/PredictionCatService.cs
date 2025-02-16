using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML;
using WebApplication1.Model;
using Microsoft.ML.OnnxRuntime;

namespace WebApplication1.ONNX
{
    public class PredictionCatService  : IPredictionCatService
    {
        private readonly PredictionEngine<ModelInput, ModelOutputCat> _predictionEngine;

        public PredictionCatService()
        {
            var mlContext = new MLContext();
            var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Model", "best_model_cat.onnx");
            using (var session = new InferenceSession(modelPath))
            {
                Console.WriteLine("Input Nodes:");
                foreach (var input in session.InputMetadata)
                {
                    Console.WriteLine($" - {input.Key}: {input.Value.Dimensions}");
                }

                Console.WriteLine("Output Nodes:");
                foreach (var output in session.OutputMetadata)
                {
                    Console.WriteLine($" - {output.Key}: {output.Value.Dimensions}");
                }
            }
            var pipeline = mlContext.Transforms.ApplyOnnxModel(
                modelFile: modelPath,
                outputColumnNames: new[] { "dense_1" },
                inputColumnNames: new[] { "input" }  // Використовуємо правильне ім'я "input"
            );

            var emptyData = mlContext.Data.LoadFromEnumerable(new List<ModelInput>());
            var model = pipeline.Fit(emptyData);

            _predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutputCat>(model);
        }

        public ModelOutputCat Predict(float[] imageData)
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
