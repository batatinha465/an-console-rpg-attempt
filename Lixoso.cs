namespace TentativaDeRPG;

class Lixoso : BaseEnemy
{
    // constructor
    public Lixoso()
    {
        name = "Lixoso";
        health = 2;
        maxHealth = 2;
        strength = 1;
        defense = 0;
        expDrop = 20;

        droppableItems = ["Poção de cura simples"];
        itemsID = [1];
        dropChance = 20;
    }
}
