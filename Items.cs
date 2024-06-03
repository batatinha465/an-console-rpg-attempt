namespace TentativaDeRPG;

class Items
{
    // lists
    List<string> inventory = new List<string>(new string[3]);
    List<int> inventoryID = new List<int>(new int[3]);

    // constructor
    public Items()
    {
        inventory = ["Poção de cura simples", string.Empty, string.Empty];
        inventoryID = [1, 0, 0];
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
            if (inventory[i] == string.Empty)
            {
                Console.Write($"{i + 1} - Vazio  ");
            }
            else
            {
                Console.Write($"{i + 1} - {inventory[i]}  ");
            }
        }
    }
}
