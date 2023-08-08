﻿
using BookDemo.Models;

namespace BookDemo.Data
{
    public static class AplicationContext
    {
        public static List<Book> Books { get; set; }
        static AplicationContext()
        {
            Books = new List<Book>()
            {
                    new Book() {Id = 1, Title = "Karagoz Ve Hacivat", Price = 75 },
                    new Book() {Id = 2, Title = "Mesnevi", Price = 150 },
                    new Book() {Id = 3, Title = "Dede Korkut", Price = 75}
            };
        }
    }
}