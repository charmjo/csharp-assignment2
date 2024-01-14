class Library: ILibraryTemplate {
    public List <string> memberTierList {get;set;}
    public List <string> materialList {get;set;}

    public Library () {
        memberTierList = new List<string> () {
            "basic",
            "elite",
            "scholar"
        };

        materialList = new List <string> () {
            "Fiction Book",
            "Non-Fiction Book",
            "Magazine"
        };
    }

    public void displayMemberTierList () {
        int index = 0;
        string menuDisplay = "\n== Membership Tier List ==\n";
        
        // iterating through the menu list
        foreach (var item in memberTierList) {
            index ++;
            menuDisplay += $"[{index}] {item} \n";
        }

        menuDisplay += "== End of Member Tier List ==\n";
        Console.WriteLine(menuDisplay);
    }

    protected void setMaterialList (string tier) {
        // I opt to restrict the material types the borrower can view.
        // it would not make any sense if both basic and elite would have access to newspapers so I restricted the access of "basic" to what all users can have access to. Only elite above has access to newspapers.
        if (tier == "basic"){
            if (materialList.Count > 3) {
                materialList.RemoveRange(3, materialList.Count - 3); 
            }
        } else if(tier == "elite") { 
            if (materialList.Count > 3) {
                materialList.RemoveRange(3, materialList.Count - 3);
            }
            materialList.Add("Newspaper");
        } else if(tier == "scholar") {
            if (materialList.Count > 3) {
                materialList.RemoveRange(3, materialList.Count - 3);
            }
            materialList.Add("Newspaper");
            materialList.Add("Manuscript");
        } 
    }

    public void displayMaterialList (string tier) {
        int index = 0;
        setMaterialList(tier);

        string menuDisplay = "\n== Reading Material List ==\n";
        
        // iterating through the menu list
        foreach (var material in materialList) {
            index ++;
            menuDisplay += $"[{index}] {material} \n";
        }

        menuDisplay += "== Reading Material List ==\n";
        Console.WriteLine(menuDisplay);
    }
    

}