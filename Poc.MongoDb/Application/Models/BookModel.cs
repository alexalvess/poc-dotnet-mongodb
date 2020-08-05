using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookModel
    {
        public BookModel(string name, int year) =>
            (Name, Year) = (name, year);

        public string Name { get; set; }

        public int Year { get; set; }
    }
}
