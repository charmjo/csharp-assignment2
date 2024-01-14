interface ILibraryTemplate {
    List <string> memberTierList {get;set;}
    List <string> materialList {get;set;}



    void displayMemberTierList ();
    void displayMaterialList (string tier);
}