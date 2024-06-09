namespace TentativaDeRPG;

class Gordalho : BaseEnemy
{
    // constructor
    public Gordalho()
    {
        name = "Gordalho";
        health = 5;
        maxHealth = 5;
        strength = 1;
        defense = 0;
        expDrop = 35;

        droppableItems = ["Poção de cura simples"];
        itemsID = [1];
        dropChance = 35;
    }
}
