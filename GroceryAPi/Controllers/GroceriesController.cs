using GroceryAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GroceryAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceriesController : ControllerBase
    {
        string folder = @"C:\Users\ata.ozdemir\source\repos\GroceryAPi\DataBase";

        [HttpGet]
        public ActionResult<ProductsModel> Get()
        {
            Response response = new Response();
            response.IsCompleted = true;
            response.Message = "Completed!";

            string[] files = Directory.GetFiles(folder, "*.json");
            if (files.Length == 0)
            {
                response.IsCompleted = false;
                response.Message = "Failed!";
                return NotFound(response);
            }
            string latestFile = files.OrderByDescending(files => System.IO.File.GetCreationTimeUtc(files)).First();
            var jsonData = System.IO.File.ReadAllText(latestFile);
            var Data = JsonConvert.DeserializeObject<List<ProductsModel>>(jsonData);
            var latestData = Data.OrderByDescending(d => d.Id).FirstOrDefault();

            if (latestData==null)
            {
                response.IsCompleted = false;
                response.Message = "Failed!";
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult>Insert(ProductsModel model)
        {
            Response response = new Response();
            response.IsCompleted = true;
            response.Message = "Completed!";
            List<ProductsModel> Products;
            string filePath = Path.Combine(folder, $"{DateTime.Now:yyyyMMddHHmmss}.json");
            if(!System.IO.File.Exists(folder)) 
            {
                Products = new List<ProductsModel>();
            }
            else 
            {
                var jsonData = System.IO.File.ReadAllText(folder);
                Products = JsonConvert.DeserializeObject<List<ProductsModel>>(jsonData);
            }
            Products.Add(model);
            var newDataJson = JsonConvert.SerializeObject(Products,Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(filePath, newDataJson);
            return Ok(response);

        }
       
    }
}
