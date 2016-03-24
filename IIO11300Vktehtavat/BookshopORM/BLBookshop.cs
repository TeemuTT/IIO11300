using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopORM
{
    class Book
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
        }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }

        public Book(int id)
        {
            this.id = id;
        }

        public Book(int id, string name, string author, string country, int year)
        {
            this.id = id;
            this.Name = name;
            this.Author = author;
            this.Country = country;
            this.Year = year;
        }

        public override string ToString()
        {
            return Name + " written by " + Author;
        }
    }

    class BLBookshop
    {
        private static string cs = BookshopORM.Properties.Settings.Default.Kirjakauppa;
        public static List<Book> GetTestBooks()
        {
            List<Book> temp = new List<Book>()
            {
                new Book(1, "Sota ja rauha", "Leo Tolstoi", "Venäjä", 1867),
                new Book(2, "Anna Karenina", "Leo Tolstoi", "Venäjä", 1867)
            };
            return temp;
        }

        public static List<Book> GetBooks(bool useDB)
        {
            DataTable dt;
            List<Book> books = new List<Book>();
            Book book;

            if (useDB)
            {
                dt = DBBookshop.GetBooks(cs);
            } else
            {
                dt = DBBookshop.GetTestData();
            }

            foreach (DataRow row in dt.Rows)
            {
                book = new Book((int)row[0]);
                book.Name = row["name"].ToString();
                book.Author = row["author"].ToString();
                book.Country = row["country"].ToString();
                book.Year = (int)row["year"];
                books.Add(book);
            }
            return books;
        }

        public static int UpdateBook(Book book)
        {
            try
            {
                int rows = DBBookshop.UpdateBook(cs, book.ID, book.Name, book.Author, book.Country, book.Year);
                return rows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool InsertBook(Book book)
        {
            try
            {
                int rows = DBBookshop.InsertBook(cs, book.Name, book.Author, book.Country, book.Year);
                if (rows > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool DeleteBook(Book book)
        {
            try
            {
                int rows = DBBookshop.DeleteBook(cs, book.ID);
                if (rows > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
