using System;
using System.Collections.Generic;
using System.Text;

namespace JournalToGo.Models
{
    public class Book : IBook
    {
        public string Id { get; set; }
        public string Link { get; set; }
    }
}
