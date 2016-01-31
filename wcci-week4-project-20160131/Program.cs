using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcci_week4_project_20160131
{
    class Program
    {
        static string[] availableResources = new[] { "HARRY POTTER", "MOBY DICK", "GREEN EGGS", "MOUNTAIN TO THE MAN", "ALL THINGS SMALL", "BIG TROUBLE", "WHY THINGS SUCK", "CARS", "BATS", "YOU" };
        static string[] allResources = new[] { "HARRY POTTER", "MOBY DICK", "GREEN EGGS", "MOUNTAIN TO THE MAN", "ALL THINGS SMALL", "BIG TROUBLE", "WHY THINGS SUCK", "CARS", "BATS", "YOU" };
        static string[] students = new[] { "JOE.JAMES", "0", " ", " ", " ", " ", " ", "MARY.JORDAN", "7", " ", " ", " ", " ", " ", "KIM.VARGAS", "14", " ", " ", " ", " ", " ", "RICHARD.SHURE", "21", " ", " ", " ", " ", " ", "BARRY.SHINE", "28", " ", " ", " ", " ", " ", };
        static int studentIdEntered = 0;
        static string currentStudentName = null;
        static int booksAllowed = 0;
        static string checkedOut = "checkedOut";
        static string titleQuery = null;
        static int resourcePointer = 0;
        static int menuChoice = 0;
        static List<string> studentsAsList = new List<string>();
        static List<string> studentBooksCheckedOutList = new List<string>();
        static List<string> availableResourcesAsList = new List<string>();



        public static void BookReturnStep1()
        {
            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

            Console.Write("Title of book to return: ");
            string input = Console.ReadLine().ToUpper();
            input = input.Trim();
            titleQuery = input;

            int j = studentIdEntered + 3;
            for (int i = studentIdEntered + 3; i < j + 3; i++) //search student account to see if book was checked out
            {
                if (students[i] == titleQuery)
                {
                    students[i] = " "; //clears book from students account
                    BookReturnStep2();
                }
            }
            Console.WriteLine("Error: Request Unavailable.\n"); //if no match is found in student's account
            MenuDisplay();

        }
        public static void BookReturnStep2()
        {

            for (int i = 0; i < allResources.Length; i++) //find if book is in catalog
            {
                if (allResources[i] == titleQuery && availableResources[i] == checkedOut) //if book is in library's catalog AND has been recorded as checked out in available resources array, do this:
                {
                    availableResources[i] = titleQuery;// returns book title to available pool                   
                                                       //resourcePointer = i; //this will be used in return step 2 - remove book from students's account?? maybe not needed
                                                       //HERE GO TO MENU
                    Console.WriteLine($"{currentStudentName} returned {titleQuery}.");
                    System.Threading.Thread.Sleep(4000); //check it out! tyring to do 4 second pause before moving on! seems to work fine
                    Console.Clear();
                    Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                    MenuDisplay();
                    //Console.WriteLine("success"); //DEBUG

                }
            }

            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

            Console.WriteLine("Error: Request Unavailable"); //this comes up if book is not owned by the library
            MenuDisplay();
        }

        public static void BooksAllowedCheck()
        {
            int j = studentIdEntered + 3;

            for (int i = studentIdEntered + 3; i < j + 3; i++)
            {
                if (string.IsNullOrWhiteSpace(students[i]) == true) //if a whitespace is found (no book), number of books allowed to check out increases by 1

                {
                    booksAllowed++; //increases number of books student can check out by 1 eaach iteration (if " " is found)
                                    //students[i] = "open";  // was bad idea
                }
                else if (string.IsNullOrWhiteSpace(students[i]) == false)
                {
                    continue;
                }
            }
            students[studentIdEntered + 2] = booksAllowed.ToString(); //assign a value to the "books allowed to check out" index..for later use maybe
            //Console.WriteLine("Books allowed: " + students[studentIdEntered + 2]); //DEBUG
            //Console.WriteLine("Slot 1 " + students[studentIdEntered + 3]); //DEBUG
            //Console.WriteLine("Slot 2 " + students[studentIdEntered + 4]); //DEBUG
            //Console.WriteLine("Slot 3 " + students[studentIdEntered + 5]); //DEBUG
            //Console.ReadKey(); //DEBUG

            if (booksAllowed < 1)
            {
                Console.Clear();
                Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                Console.WriteLine(currentStudentName + " has checked out the max number of resources.");
                MenuDisplay();
            }

            else
            {
                //go to checkout method step 1
                CheckOutStep1();
            }

            //EnterStudentIDFirstTime(); //instead should go to method that asks for name of resource //NOT SURE THIS IS NECCESSARY
        }
        public static void CheckOutStep1()
        {
            //Catalog Check
            //Search for book and assign to bookCheckout variable

            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

            Console.Write("Title of book to check out: ");
            string input = Console.ReadLine().ToUpper();
            input = input.Trim();
            titleQuery = input;


            for (int i = 0; i < allResources.Length; i++) //find if book is in catalog
            {

                if (allResources[i] == titleQuery) //if book is in library's catalog, do this:
                {
                    if (availableResources[i] == checkedOut) //checking if book is checked out or not
                    {
                        Console.Clear();
                        Console.WriteLine($"{titleQuery} is checked out."); //if it is, leave message then go to menu ...leave this method
                        MenuDisplay();
                    }
                    else //if book is NOT checked out, assign the allResources index to a temp static variable(resource pointer) proceed to checkout step 2
                    {
                        resourcePointer = i; //this will be used in step to mark book as checked out
                        CheckOutStep2();
                    }
                }
            }

            Console.WriteLine("Error: Request Unavailable"); //this comes up if book is not owned by the library
            MenuDisplay();

        }
        public static void CheckOutStep2()
        {
            int j = studentIdEntered + 3;
            for (int i = studentIdEntered + 3; i < j + 3; i++)
            {
                if (string.IsNullOrWhiteSpace(students[i]) == true)
                {
                    students[i] = titleQuery; //assign resource/book to students first open reserve slot


                    availableResources[resourcePointer] = checkedOut; //will assign the string checkedOut to correct index (identified in checkout step1)
                    Console.WriteLine($"{currentStudentName} checked out {titleQuery}.");
                    System.Threading.Thread.Sleep(4000); //check it out! tyring to do 4 second pause before moving on! seems to work fine
                    Console.Clear();
                    Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                    MenuDisplay();
                }
            }

        }

        public static void ListAllStudents()
        {
            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

            studentsAsList.Clear(); //clear list of students (just in case!)

            for (int i = 0; i < students.Length; i += 7) //cycle through students array, only acting on 1, 7, 14... +6.

            {
                int j = 0;
                j += 7;
                studentsAsList.Add(students[i] + "  ID# - " + students[i + 1].ToString()); //insert array student into list
            }
            studentsAsList.Sort();
            foreach (string student in studentsAsList)
            {
                Console.WriteLine(student);
            }

            MenuDisplay();
        }
        public static void ViewAvailableResources()
        {
            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

            int counter = 0;  //counts number available books
            availableResourcesAsList.Clear(); //clear list (just in case!)

            for (int i = 0; i < availableResources.Length; i++)
            {
                if (!(availableResources[i] == checkedOut)) //if it has any value besides "checkedOut"...will never be empty or null
                {
                    availableResourcesAsList.Add(availableResources[i]); //add checked out books to a new list of checked out books
                    //Console.WriteLine(bookStatus[i]);

                    counter++; //keep track of available books in counter variable
                }

                else
                {
                    continue;
                }
            }
            if (counter <= 0)
            {
                Console.WriteLine("All resources are checked out");
                MenuDisplay();
            }
            availableResourcesAsList.Sort();

            foreach (string book in availableResourcesAsList) //prints list of available books
            {
                Console.WriteLine(book);
            }
            MenuDisplay();
        }
        public static void ViewStudent()
        {
            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

            int j = studentIdEntered + 3;
            studentBooksCheckedOutList.Clear(); //removes all contents of list (just in case!)

            for (int i = studentIdEntered + 3; i < j + 3; i++)
            {
                if (string.IsNullOrWhiteSpace(students[i]) == true) //if a whitespace is found (no book), number of books allowed to check out increases by 1

                {
                    booksAllowed++; //increases number of books student can check out by 1 eaach iteration (if " " is found)
                    continue;
                }
                else if (string.IsNullOrWhiteSpace(students[i]) == false)
                {
                    studentBooksCheckedOutList.Add(students[i]); //add checked out books to a new list of checked out books
                    //list will be sorted and printed after loop runs
                    //after added to the list, loop shoud continue to run its course (continue)

                    continue; //continue loop checking for null or strings
                }
            }
            if (booksAllowed == 3)
            {
                Console.Clear();
                Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                Console.WriteLine("Account: " + currentStudentName + "\n");
                Console.WriteLine("Nothing is checked out...\n");
                MenuDisplay();
            }
            else
            {
                //here will print books checked out alphabetically
                Console.WriteLine("Account: " + currentStudentName + "\n");
                studentBooksCheckedOutList.Sort();
                foreach (string book in studentBooksCheckedOutList)
                {
                    Console.WriteLine(book);
                }

                MenuDisplay();

            }

        }

        public static void EnterStudentIDFirstTime()
        {
            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");
            Console.Write("\nEnter student's ID #: ");
            string input = Console.ReadLine();
            input = input.Trim();

            if (int.TryParse(input, out studentIdEntered))
            {
                if (studentIdEntered % 7 != 0 && studentIdEntered != 0 || studentIdEntered < 0 || studentIdEntered > 28)
                {
                    Console.Clear();
                    Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");
                    Console.WriteLine("\nError: Request unavailable");
                    MenuDisplay();
                }
                else
                {
                    currentStudentName = students[studentIdEntered];
                    if (menuChoice == 4)
                    {
                        BooksAllowedCheck(); //here, go bookAllowedCheck()..if allowed to check out resources, proceeds to book checkout step 1
                    }
                    else if (menuChoice == 3)
                    {
                        ViewStudent();
                    }
                    else if (menuChoice == 5)
                    {
                        BookReturnStep1();
                    }

                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                Console.WriteLine("Please enter a number");
                EnterStudentIDFirstTime();
            }

        }
        public static void MenuDisplay()
        {
            //PRINT MENU TO SCREEN


            string[] optionsMenu = new string[] { "1 - View Students", "2 - View Available Resources", "3 - View Student Account", "4 - Check Out Item", "5 - Return Item", "6 - Exit" };

            Console.WriteLine();
            for (int i = 0; i < optionsMenu.Length; i++)
            {

                Console.WriteLine(optionsMenu[i]);
            }
            Console.Write("\nChoose a menu item: ");
            string input = Console.ReadLine();
            input = input.Trim();

            //int menuChoice;
            if (int.TryParse(input, out menuChoice))
            {
                if (menuChoice < 1 || menuChoice > 6)
                {
                    Console.Clear();
                    Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                    Console.WriteLine("Enter a number from 1 to 6 to make a selection");
                    MenuDisplay();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");

                Console.WriteLine("Enter a number from 1 to 6 to make a selection");
                MenuDisplay();
            }

            if (menuChoice == 1)
            {
                ListAllStudents();
            }
            else if (menuChoice == 2)
            {
                ViewAvailableResources();
            }
            else if (menuChoice == 3)
            {

                EnterStudentIDFirstTime();
            }
            else if (menuChoice == 4)
            {
                EnterStudentIDFirstTime();
            }
            else if (menuChoice == 5)
            {
                EnterStudentIDFirstTime();
            }
            else if (menuChoice == 6)
            {
                Exit();
            }

        }

        public static void Exit()
        {
            Console.Clear();
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************\n\n");
            Console.Write("GOOD BYE");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public static void Welcome()
        {
            Console.WriteLine("*************** Bootcamp Resources Checkout System ****************");
            System.Threading.Thread.Sleep(1000);

        }



        static void Main(string[] args)
        {

            Welcome();
            MenuDisplay();
            Console.WriteLine("Press Any Key To Quit");
            Console.ReadKey();

        }
    }
}

