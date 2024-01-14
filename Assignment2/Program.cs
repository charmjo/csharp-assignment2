namespace Assignment2;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Initialize variables
            string option; 
            var menuList = new List <string> ()
                {   
                    "Make a Reservation",
                    "List All Reservations", 
                    "Cancel a Reservation",
                    "Exit the Program",                    
                };

            Borrower member1 = new Borrower {
                id = "1234567890", name = "Charm Relator", email = "crelator@test.com", type = "elite",
                materialsBorrowed =  new List <ReadingMaterial> {new ReadingMaterial {type = "Fiction Book", qty = 1},
                                                  new ReadingMaterial {type = "Non-Fiction Book", qty = 1},
                                                  new ReadingMaterial {type = "Magazine", qty = 1},
                                                  new ReadingMaterial {type = "Newspaper", qty = 1},
                }
            };

            Borrower member2 = new Borrower {
                id = "0123456789", name = "Y'shtola Rhul", email = "yrhul@sharlayan.edu.sh", type = "scholar",
                materialsBorrowed =  new List <ReadingMaterial> {new ReadingMaterial {type = "Newspaper", qty = 1},
                                                  new ReadingMaterial {type = "Manuscript", qty = 2},
                }
            };

            Borrower member3 = new Borrower {
                id = "1114567890", name = "Aymeric de Borel", email = "adeborel@ishgard.gov.is", type = "basic",
                materialsBorrowed =  new List <ReadingMaterial> {new ReadingMaterial {type = "Fiction Book", qty = 1},
                                                  new ReadingMaterial {type = "Non-Fiction Book", qty = 1},
                }
            };

            Reservation libraryReservation = new Reservation ();
            libraryReservation.insertReservation(member1);
            libraryReservation.insertReservation(member2);   
            libraryReservation.insertReservation(member3);               

            while(true) {
                option = displayMenu(menuList);
                if (option == "1") { // make a reservation
                    libraryReservation.makeReservation();
                    Console.WriteLine("Reservation successfully added.\n");
                } else if (option == "2") { // list all reservations
                    libraryReservation.displayReservation();
                } else if (option == "3") { //cancel a reservation
                    libraryReservation.cancelReservation();
                    Console.WriteLine("Reservation cancelled.\n");
                } else if (option == "4") { // exit the program
                    Console.WriteLine("Program Closing. Thank you for using our program.");
                    break;
                } else {
                    Console.WriteLine("Option not found. Please select from the choices above.\n");
                }
            }            
        }
        catch (Exception e) {
            Console.WriteLine($"Error : \n{e}");
        }
    }

    static string displayMenu (List <string> menu) {
        int index = 0;
        string? option;
        string menuDisplay = "=========== Welcome to the Library of Alexandria ===========\n";
        
        // iterating through the menu list
        foreach (var item in menu) {
            index ++;
            menuDisplay += $"[{index}] {item} \n";
        }

        menuDisplay += "=====================================\n";
        menuDisplay += "Please enter the number of your choice";
        Console.WriteLine(menuDisplay);

        option = Console.ReadLine();
        return option;
    }
}
