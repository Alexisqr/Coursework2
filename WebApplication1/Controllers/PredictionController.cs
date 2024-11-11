using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using WebApplication1.ONNX;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using WebApplication1.ONNX;
using Newtonsoft.Json;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly PredictionService _predictionService;

        public PredictionController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

       
        [HttpPost("predict")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> Predict( IFormFile image) 
        {
            if (image == null || image.Length == 0)
                return BadRequest("Завантажте зображення.");

            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);
            var imageData = PrepareImage(stream);

            var prediction = _predictionService.Predict(imageData);
            // Отримання індексу найбільш ймовірного класу
            var predictedClassIndex = prediction.dense_4.ToList().IndexOf(prediction.dense_4.Max());
            var breeds = JsonConvert.DeserializeObject<List<string>>(System.IO.File.ReadAllText("C:\\Users\\user\\source\\repos\\WebApplication1\\WebApplication1\\Model\\breeds.json"));

            // Отримуємо породу за індексом
            var predictedBreed = breeds[predictedClassIndex];
            Console.WriteLine("All confidence scores:");
            for (int i = 0; i < prediction.dense_4.Length; i++)
            {
                Console.WriteLine($"Class {i}: {prediction.dense_4[i]}");
            }
            return Ok(new
            {
                PredictedBreed = predictedBreed,
  
            });

        }

        private float[] PrepareImage(Stream imageStream)
        {
            using var bitmap = new Bitmap(imageStream);
            var resized = new Bitmap(bitmap, new Size(224, 224));
            var floatArray = new float[224 * 224 * 3];

            for (int y = 0; y < resized.Height; y++)
            {
                for (int x = 0; x < resized.Width; x++)
                {
                    var color = resized.GetPixel(x, y);
                    int index = (y * 224 + x) * 3;
                    floatArray[index] = color.R / 255f;
                    floatArray[index + 1] = color.G / 255f;
                    floatArray[index + 2] = color.B / 255f;
                }
            }
            Console.WriteLine("First 10 values of the image array:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Pixel {i}: {floatArray[i]}");
            }
            resized.Save("resized_image.jpg");
            Console.WriteLine($"Prepared image with {floatArray.Length} elements.");
            return floatArray;
           
        }
      
    }
}