using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace MyBookstore
{
    class Book
    {
        public string Name { get; private set; }
        public int Serial { get; private set; }
        public bool Status { get; private set; } = false; //false means available

        public Book(string name, int serial) //constructor pass parameters to attributes
        {
            Name = name;
            Serial = serial;
        }
        public bool Available() //checks if the book is available
        {
            //to do
            //should return true if a book is available
            if (!Status)
            { 
                return true; 
            }
            else 
            {
                return false;
            }
            

        }
        public void Rent() //rent a book by changing its status
        {

            if (Status == false)
            {
                //A book can be rented if it's rental status is false
                Status = true;

            }

            else
            {
                //otherwise, the book is not available.
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("The book is already rented.");
                return;

            }

        }

        public void Return() //rent a book by changing its status
        {

            if (Status == true)
            {
                //A book can be returned only if, it was rented before!
                Status = false;

            }

            else
            {
                // rent status false means, it is available in the store.
                // Therefore, you should generate error message if some users tries to return this book.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You cannot return a book which is already in the store");
       
            }

        }
        public void BookInfo() //displays info about the book and its status
        {
            //Show name of the book, it's serial and rental status.
            var availability = (Available()) ? "Available" : "Rented";
            Console.WriteLine("Book name: {0}, Serial: {1}, Status: {2}", Name, Serial, availability);

        }
    }//end of class Book

    class Reader
    {
        public string Name { get; private set; }
        public List<Book> books;
        public Reader(string name)
        {
            //to do
            //initialize a reader object
            Name = name;
            books = new List<Book>();
        }
        public void RentABook(Book book)
        {
            //to do
            //user is allowed to rent maximum two books at a time.
            //issue error message, if users want to rent more than two books.
            book.Rent(); 
            books.Add(book); 

        }
        public void ReturnABook(Book book)  
        {
            //to do
            //return a book, means change book status and remove the book for the readers list.
         
            book.Return();        
            books.Remove(book); 

        }
        public void ReaderInfo()
        {
            //to do
            //show reader's name and the list of books rented by the reader.
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Reader {0} rented following books: ", Name);
            Console.ResetColor();

            foreach(Book unit in books) //loop accesses all the books rented by the reader
            {

                unit.BookInfo(); //displays info about the book

            }

        }

    }//end of class Reader 
    class BookStore
    {
        public List<Book> books;
        public List<Reader> readers;
        public BookStore()
        {
            //to do
            //initialize book store
            books = new List<Book>();
            readers = new List<Reader>();
        }
        public void AddAReader(string name)
        {
            //add a new reader to the bookstore's reader list.
            readers.Add(new Reader(name));

        }
        public void RemoveAReader(string name)
        {
            //to do
            //remove a reader, therefore, first return all books(if any) rented by the reader then remove the reader.
            foreach(Reader person in readers.ToList())
            {

                if(person.Name == name) //checks if the reader is in the list
                {

                    foreach(Book unit in person.books.ToList()) //once in the list, runs the return to get back all the books
                    {
                        ReturnABook(person.Name, unit.Name, unit.Serial);                 
                    }

                    readers.Remove(person); //after returning books, the reader is removed from the bookstore's list

                }

            }

        }

        public void AddABook(string name, int serial)
        {
            //to do
            // add a book object to the bookstore's book list.
            books.Add(new Book(name, serial));

        }
        public void RemoveABook(string name, int serial)
        {
            //to do
            //remove a book from book store. Only allowed if bookstore already have the book 'available'!
            //Otherwise, issue an error message because the book is already issued by some reader!
            foreach(Book unit in books)
            {

                if(unit.Name == name && unit.Serial == serial && unit.Available()) //book matches criteria and availability
                {

                    books.Remove(unit);
                    return;

                }

                else if (unit.Name == name && unit.Serial == serial && !unit.Available()) //if books doesn't match availability
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry! '{0}' is already rented. System cannot remove a rented book!", unit.Name);
                    Console.ResetColor();

                }

            }

        }
        public void RentABook(string name, string book)
        {
            //to do
            // A book can be rented, if it is available to the store and not already rented to somone else!
            foreach(Book unit in books)
            {
                if(unit.Name == book && unit.Available())
                {
                    foreach(Reader person in readers)
                    {
                        if (person.Name == name && person.books.Count < 2) //store checks if reader has reached the limit to rent books
                        {
                            person.RentABook(unit);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Book: '{0}' successfully rented.", unit.Name); 
                            return; //return avoids the loop to continue to rent books more than once to same person
                        }
                        else if (person.Name == name && person.books.Count == 2) //if the reader already have 2 books rented, issues a message
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Sorry! {0}, You cannot rent more than two books!", person.Name);
                            return;
                        }

                    }

                }

            }
           
        }

        public void ReturnABook(string name, string book, int serial)
        {
           //A book can be returned by a reader, if he/she actually rented the book.
           foreach(Reader person in readers)
            {

                if(person.Name == name)
                {
                    
                    foreach (Book unit in person.books)
                    {

                        if(unit.Name == book && unit.Serial == serial && !unit.Available()) //checks if book matches criteria to be returned
                        {

                            person.ReturnABook(unit);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Book: '{0}' successfully returned.", unit.Name);
                            return;

                        }

                    }

                }

            }

        }

        public void ShowBookstoreInformation()
        {
            //to do
            //show bookstore information
            //first show all books that are already rented to some readers.
            //then show all books thar are available to the store.
            
            foreach (Reader person in readers)
            {

                if (person.books.Count > 0) //condition to check if someone from the list rented any book. If not, bookstore will not display names
                {

                    person.ReaderInfo();

                }

            }     

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The bookstore has the following books available:");
            Console.ResetColor();

            foreach (Book unit in books) 
            {

                if (unit.Available()) //checks all available books in the store and display their information
                {

                    var availability = (unit.Available()) ? "Available" : "Rented";
                    Console.WriteLine("Book Name: {0}, Serial: {1}, Status: {2}", unit.Name, unit.Serial, availability);

                }

            }

        }
    }//end of class Bookstore




    class Program
    {
        static void Main(string[] args)
        {
            BookStore bs = new BookStore();
            bs.AddAReader("Emmanuel");
            bs.AddAReader("Vinci");
            bs.AddAReader("Supreet");
            bs.AddABook("Object Oriented Programming", 1);
            bs.AddABook("Object Oriented Programming", 2);
            bs.AddABook("Object Oriented Programming", 3);
            bs.AddABook("Programming Fundamentals", 1);
            bs.AddABook("Programming Fundamentals", 2);
            bs.AddABook("Let us C#", 1);
            bs.AddABook("Programming is Fun", 1);
            bs.AddABook("Life is Beautiful", 1);
            bs.AddABook("Let's Talk About the Logic", 1);
            bs.AddABook("How to ace a job interview", 1);
            bs.ShowBookstoreInformation();
            
            Console.WriteLine();
            bs.RentABook("Emmanuel", "Object Oriented Programming");
            bs.RentABook("Emmanuel", "How to ace a job interview");
            bs.RentABook("Emmanuel", "Life is Beautiful");
            Console.WriteLine();

           
            bs.RentABook("Vinci", "Object Oriented Programming");
            bs.RentABook("Vinci", "Programming Fundamentals");
            Console.WriteLine();

            bs.RentABook("Supreet", "Let's Talk About the Logic");
            Console.WriteLine();
            bs.ShowBookstoreInformation();
            Console.WriteLine();



            bs.ReturnABook("Emmanuel", "Object Oriented Programming", 1);
            bs.RentABook("Emmanuel", "Life is Beautiful");
            Console.WriteLine();

            
            bs.RemoveABook("Let us C#", 1);
            bs.RemoveABook("Let's Talk About the Logic", 1);
            Console.WriteLine();
            
            bs.RemoveAReader("Emmanuel");
            Console.WriteLine();
            bs.ShowBookstoreInformation();
            
        }
    }
}



/*
 Once Executed, Your program will have the following output:

The bookstore have following books available:
Book Name: Object Oriented Programming, Serial: 1, Status: Available
Book Name: Object Oriented Programming, Serial: 2, Status: Available
Book Name: Object Oriented Programming, Serial: 3, Status: Available
Book Name: Programming Fundamntals, Serial: 1, Status: Available
Book Name: Programming Fundamntals, Serial: 2, Status: Available
Book Name: Let us C#, Serial: 1, Status: Available
Book Name: Programming is Fun, Serial: 1, Status: Available
Book Name: Life is Beautiful, Serial: 1, Status: Available
Book Name: Let's Talk About the Logic, Serial: 1, Status: Available
Book Name: How to ace a job interview, Serial: 1, Status: Available

Book: 'Object Oriented Programming' successfully rented.
Book: 'How to ace a job interview' successfully rented.
Sorry! Emmanuel, You cannot rent more than two books!

Book: 'Object Oriented Programming' successfully rented.
Book: 'Programming Fundamntals' successfully rented.

Book: 'Let's Talk About the Logic' successfully rented.

Reader Emmanuel rented following books:
Book Name: Object Oriented Programming, Serial: 1, Status: Rented
Book Name: How to ace a job interview, Serial: 1, Status: Rented
Reader Vinci rented following books:
Book Name: Object Oriented Programming, Serial: 2, Status: Rented
Book Name: Programming Fundamntals, Serial: 1, Status: Rented
Reader Supreet rented following books:
Book Name: Let's Talk About the Logic, Serial: 1, Status: Rented
The bookstore have following books available:
Book Name: Object Oriented Programming, Serial: 3, Status: Available
Book Name: Programming Fundamntals, Serial: 2, Status: Available
Book Name: Let us C#, Serial: 1, Status: Available
Book Name: Programming is Fun, Serial: 1, Status: Available
Book Name: Life is Beautiful, Serial: 1, Status: Available

Book: Object Oriented Programming successfully returned.
Book: 'Life is Beautiful' successfully rented.

Sorry! 'Let's Talk About the Logic' is already rented. Syatem cannot remove a rented book!

Book: How to ace a job interview successfully returned.
Book: Life is Beautiful successfully returned.

Reader Vinci rented following books:
Book Name: Object Oriented Programming, Serial: 2, Status: Rented
Book Name: Programming Fundamntals, Serial: 1, Status: Rented
Reader Supreet rented following books:
Book Name: Let's Talk About the Logic, Serial: 1, Status: Rented
The bookstore have following books available:
Book Name: Object Oriented Programming, Serial: 1, Status: Available
Book Name: Object Oriented Programming, Serial: 3, Status: Available
Book Name: Programming Fundamntals, Serial: 2, Status: Available
Book Name: Programming is Fun, Serial: 1, Status: Available
Book Name: Life is Beautiful, Serial: 1, Status: Available
Book Name: How to ace a job interview, Serial: 1, Status: Available

C:\Program Files\dotnet\dotnet.exe (process 9596) exited with code 0.
Press any key to close this window . . .
 */
