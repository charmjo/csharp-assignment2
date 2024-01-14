class Reservation : Library {
    protected List <Borrower> reservationList {get;set;}

    public Reservation () : base () {
        reservationList = new List<Borrower> ();
    }

    public void getMemberTierList () {
        base.displayMemberTierList();
    }

    public void makeReservation () {
        string borrowerId;
        string borrowerName;
        string borrowerEmail;
        string borrowerType;
        List <ReadingMaterial> tempMaterialList = new List<ReadingMaterial> ();

        borrowerId = getMemberId();
        borrowerName = getMemberName();
        borrowerEmail = getMemberEmail();
        borrowerType = getMemberType();
        tempMaterialList = getMaterialsBorrowed(borrowerType);

        insertReservation(new Borrower {
            id = borrowerId,
            name = borrowerName,
            email = borrowerEmail,
            type = borrowerType,
            materialsBorrowed = tempMaterialList
        });
    }

    public void insertReservation (Borrower reservation) {
        reservationList.Add(reservation);
    }

    public void displayReservation () {
        int index = 0;
        string reservationDisplay = "";

        reservationDisplay = "\n====== Reservation List ===== \n";
        foreach (var reservation in reservationList)
        {
            index ++;
            reservationDisplay += $"({index}) \n";
            reservationDisplay += $"Member Id : {formatMemberId(reservation.id)} \n";
            reservationDisplay += $"Member Name : {reservation.name} \n";     
            reservationDisplay += $"Member Email : {reservation.email} \n";
            reservationDisplay += $"Member Type : {reservation.type} \n"; 
            reservationDisplay += $"Books Borrowed : \n";
            reservationDisplay += displayMaterialsBorrowed(reservation.materialsBorrowed); 
            reservationDisplay += "\n";
        }
        Console.WriteLine(reservationDisplay);
    }

    public void cancelReservation () {
        int index;
        displayReservation ();
        Console.WriteLine("Please enter the number of the reservation you would like to delete:");
        
        if (int.TryParse(Console.ReadLine(), out index)){
            Console.WriteLine($"Cancelling {reservationList[index-1].name}'s reservation.");
            reservationList.RemoveAt(index-1);
        }    
    }

    protected string displayMaterialsBorrowed (List <ReadingMaterial> materialsBorrowed) {
        string bookDisplay = "";
        foreach (var material in materialsBorrowed)
        {
            bookDisplay += $"\tType : {material.type} ";
            bookDisplay += $",\t Quantity : {material.qty} \n";     
        }
        return bookDisplay;
    }

    //replace 1234567890 to XXX4567890
    public string formatMemberId (string id) {
        string formattedId;
        formattedId = id.Remove(0,3).Insert(0,"XXX");
        return formattedId;
    }


    
    public string getMemberName () {
        string? name;
        Console.WriteLine("Please enter member name:");
        name = Console.ReadLine()!;

        return name;
    }

    public string getMemberId () {
        string? id;
        Console.WriteLine("Please enter 10-digit member id:");
        id = Console.ReadLine()!;

        if (id.Length != 10) {
            throw (new FormatException("member id format invalid.")); 
        }

        return id;
    }

    public string getMemberEmail () {
        string? email;
        Console.WriteLine("Please enter member email:");
        email = Console.ReadLine()!;
        return email;
    }

    public string getMemberType () {
        int option;
        string type;
        displayMemberTierList ();

        Console.WriteLine("Please enter member type number:");
        option = int.Parse(Console.ReadLine()!);

        type = base.memberTierList[option-1];
        return type;
    }

    // note: people can add more than 1 type per reservation. 
    private List <ReadingMaterial> getMaterialsBorrowed (string memberType) {
        bool run = true;
        string materialType;
        int materialQty;
        List <ReadingMaterial> tempMaterialList = new List<ReadingMaterial> ();

        while (run) {
            materialType = getMaterialName(memberType);
            materialQty = getMaterialQty(materialType);

            if(checkMaterialQty(tempMaterialList,materialQty)){
                tempMaterialList.Add(new ReadingMaterial {type = materialType, qty = materialQty});
                Console.WriteLine("Material successfully added! \n");
            } else {
                Console.WriteLine("Your material wasn't added. You can only borrow 4 items at a time. We apologize. \n");
            }

            // borrower exit option.
            // People can add more materials after they added their first one if they type (Y/y) when the system asks them two 
            //note: if people press other keys besides those I've mentioned on my previous comment above, they will be prompted to add a new material.
            if(tempMaterialList.Count > 0) {
                Console.WriteLine("Do you want to stop adding reading materials? [Y/anyKey]");
                string choice = Console.ReadLine()!;
                if ((choice == "Y") || (choice =="y")){
                    run = false;
                }
            }         
        }
        
        return tempMaterialList;
    }

    public string getMaterialName (string type) {
        int option;
        string materialType;

        base.displayMaterialList(type);

        Console.WriteLine("Please enter list number of material type:");
        option = int.Parse(Console.ReadLine()!);

        materialType = base.materialList[option-1];

        return materialType;
    }

    public int getMaterialQty (string materialType) {
        int materialQty;

        Console.WriteLine($"Please enter quantity you want to borrow for {materialType}:");
        materialQty = int.Parse(Console.ReadLine()!);

        return materialQty;
    }

/*
    My experience with borrowing books:
    1. When I borrow books from the library, I usually borrow more than 1 of the same type. First example, I reserved three 3 Fiction Books.
    2. Second, I borrowed 3 books of different types. 1-fiction, 2 - non-fiction.
    3. I also remembered back when I was still studying that our library also imposed an item limit and it was the number of materials that was counted.

    To conclude, I interpreted that the 4-item limit meant qty, not the type. So the 4-item limit is based on qty. 
*/

    protected bool checkMaterialQty (List <ReadingMaterial> materials, int newMaterialQty) {
        // nee to add qty to be added.
        int materialQty = 0;
        if (materials.Count > 0){
            foreach (var material in materials) {
                materialQty += material.qty;
            }
        } 
        
        //will check if the sum of the total list qty and new qty does not exceed more than 4 items.
        materialQty += newMaterialQty;
        if(materialQty > 4) {
            return false;
        } 

        return true;

    }

}