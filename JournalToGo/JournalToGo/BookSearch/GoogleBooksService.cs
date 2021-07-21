using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace JournalToGo.BookSearch
{
    public class GoogleBooksService
    {
        public IBook GetFirstBook(string searchtext)
        {
            var service = CreateService();
            var listRequest = service.Volumes.List(searchtext);
            listRequest.MaxResults = 1;
            var volumes = listRequest.Execute();
            return FoundBook(volumes);
        }

        private BooksService CreateService()
        { 
            return new BooksService(new BaseClientService.Initializer
            {
                ApplicationName = "Journal to go",
                ApiKey = LoadKeyFromJson(),
            });
        }

        private IBook FoundBook(Volumes vol)
        {
            var book = new Book();
            foreach (var r in vol.Items)
            {
                book.Id = r.Id;
                book.Link = r.SelfLink;
            }
            return book;
        }

        private string LoadKeyFromJson()
        {
            string apiKey = null;

            try
            {
                using (var reader = new StreamReader(GetStreamOfResource("appsettings.json")))
                {
                    var json = reader.ReadToEnd();
                    apiKey = JsonSerializer.Deserialize<string>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return apiKey;
        }

        private Stream GetStreamOfResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var appSettings = assembly.GetManifestResourceNames().ToList();
            var resource = appSettings.FirstOrDefault(x => x.Contains(name));
            return assembly.GetManifestResourceStream(resource);
        }
    }
}
