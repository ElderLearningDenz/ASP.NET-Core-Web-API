using System;
using BookApi.Models;

namespace BookApi
{
    public class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Author = "Stuart Turton",
                Title = "The Seven Deaths of Evelyn Hardcastle"
            },

            new Book()
            {
                Author = "Hannah Kent",
                Title = "Burial Rites"
            },

            new Book()
            {
                Author = "William Gibson",
                Title = "Neuromancer"
            }
        };
    }
}

