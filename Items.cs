namespace TentativaDeRPG;

class Items
{
    // lists
    List<string> inventory = [];
    List<int> inventoryID = [];

    // constructor
    public Items()
    {
        inventory = ["Poção de cura simples"];
        inventoryID = [1];
    }

    // properties
    public List<string> Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public List<int> InventoryID
    {
        get { return inventoryID; }
        set { inventoryID = value; }
    }

    // functions
    public void ShowInventory()
    {
        Console.WriteLine();

        for (int i = 0; i < inventory.Count; i++)
        {
            Console.Write($"{i + 1} - {inventory[i]}  ");
        }

        Console.Write("0 - Voltar");
    }
}