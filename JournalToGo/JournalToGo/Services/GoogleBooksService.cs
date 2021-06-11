using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using JournalToGo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JournalToGo.Services
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
            Console.WriteLine(Environment.CurrentDirectory);
            using (var reader = new StreamReader(Environment.CurrentDirectory + @"../appsettings.json"))
            {
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<string>(json);
            }
        }
    }
}
