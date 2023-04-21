using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Model;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebApplication1.Pages.Student
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;
        private readonly IConfiguration _configuration;
        public CreateModel(WebApplication1.Data.WebApplication1Context context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WebApplication1.Model.Student Student { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string uniqueFileName = UploadedFile(Student);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Student.ProfileImageName = uniqueFileName;
            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private  string  UploadedFile(WebApplication1.Model.Student model)
        {
            if (model.ProfileImage != null)
            {
                /*string uploadsFolder = Path.Combine(Environment.CurrentDirectory, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }*/

                //string connectionString = "DefaultEndpointsProtocol=https;AccountName=newschoolsajustorage;AccountKey=CEg0E3NryeO/EGba/6sncz3J7yEo76qCZU9XaHOcRZmVeKQJQI96n4c5+vL5yvWOyDMxZXfiRi1p+AStci0tSg==;EndpointSuffix=core.windows.net";
                string connectionString = _configuration.GetConnectionString("BlobStorageAccount");
                string containerName = "school-image";
                string blobName = model.ProfileImage.FileName;
               
                BlobContainerClient blobServiceClient = new BlobContainerClient(connectionString, containerName);

                BlobClient blobClient = blobServiceClient.GetBlobClient(blobName);
                blobClient.UploadAsync(model.ProfileImage.OpenReadStream(), true);
            }         
            return model.ProfileImage.FileName;
        }
    }
}
