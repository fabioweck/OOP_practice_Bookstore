# OOP_practice_Bookstore

Overview

Write a program that manages a simple Book Store by utilizing the Object Oriented Programming Architecture.
Directions
A book store wants to develop a system to add/remove readers, add/remove books, track books rented and returned from registered readers and show book store information. 
In order to endure books rented and returned, your program will need to track books and readers. 
A reader can rent maximum 2 (two) books and he/she can rent a book if it is available to the store and not rented by other readers. 
The store can remove any book if the book is not already rented to someone. 
The store can also remove a reader, however, to remove a reader, all books rented by him/her must be returned before otherwise, 
the system will generate an error message.

Your program must track each books and every readers. Implement your book store program can be done by the following steps:
1. Create a Book class.
a. Add fields for the book’s name, serial and status. (Assume, book names can be same, however will always differ by the serial number. i.e. together book name and serial will be unique! )
b. Add a constructor that initializes every book’s fields with given values.
c. Add a function to check the availability of a book.
d. Add a function to rent a book.
e. Add a function to return a book.
f. Finally, add a function to show book information.
2. Create a Reader class.
a. Add fields for the reader’s name, and a list of books. (Assume each reader name is unique. i.e. there are no two readers in the book store with the same name.)
b. Add a constructor that initializes reader’s name and to create an empty list of books.
c. Add a function to rent a book and add the book to the reader’s list.
d. Add a function to return a book and remove the book from the list.
e. Finally, add a function to show reader information.
3. Create a BookStore class.
a. Add fields in order to store the list of books and the list of readers for the book store.
b. Add a constructor to initializes all lists to empty list.
c. Add a method for adding a reader to the store.
d. Add a method for removing a reader from the store. (to remove a reader, all books rented by him/her must be returned first!)
e. Add a method for adding a book to the store.
f. Add a method to remove a book from the store. (to remove a book, it should be available in the store, otherwise, program should issue an error message!)
g. Add a method to rent a book for a reader.
h. Add a method to return a book from a reader.
i. Finally, add a method to show bookstore information.
