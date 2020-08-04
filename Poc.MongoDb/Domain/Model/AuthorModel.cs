using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class AuthorModel
    {
        public string Name { get; set; }

        public IEnumerable<BookModel> Books { get; set; }
    }
}
