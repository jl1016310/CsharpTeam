using System;
using System.Collections.Generic;

namespace LibraryApp
{
    public class Member
    {
        // Private variables
        private string _name = "";
        private int _memberId;
        private List<Book> _checkedOutBooks = new();

        // Public variables
        public string Name { get { return _name; } set { _name = value; } }
        public int MemberId { get { return _memberId; } set { _memberId = value; } }
        public List<Book> CheckedOutBooks { get { return _checkedOutBooks; } set { _checkedOutBooks = value; } }

        public decimal OutstandingFine { get; set; }

        // Constructor
        public Member(string name, int memberId)
        {
            // Invalid input handling
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (memberId <= 0)
                throw new ArgumentException("MemberId must be greater than zero.", nameof(memberId));

            // Assigning constructor values
            Name = name;
            MemberId = memberId;
        }

        // Methods
        public void CheckOut(Book book)
        {
            // Invalid checkout handling
            if (!book.IsAvailable)
                throw new InvalidOperationException($"No available copies of '{book.Title}' to check out.");

            if (CheckedOutBooks.Count >= 5)
                throw new InvalidOperationException($"Member '{Name}' has checked out the max amount of books");

            // Add book to List
            CheckedOutBooks.Add(book);

        }
        // Return
        public void Return(Book book)
        {
            // Invalid return handling
            if (!CheckedOutBooks.Contains(book))
                throw new InvalidOperationException($"Member '{Name}' has not checked out the book '{book.Title}'.");
            // Remove book from List
            CheckedOutBooks.Remove(book);
        }
        public void AddFine(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Fine amount cannot be negative.");

            OutstandingFine += amount;
        }
        public void PayFine(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount cannot be negative.");
            if (amount > OutstandingFine)
                throw new InvalidOperationException($"Payment amount ({amount}) exceeds the outstanding fine balance ({OutstandingFine}).");
            OutstandingFine -= amount;
        }
    }//end of Member
}//end of namespace