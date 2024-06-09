namespace TentativaDeRPG;

class Xexelento : BaseEnemy
{
    // constructor
    public Xexelento()
    {
        name = "Xexelento";
        health = 3;
        maxHealth = 3;
        strength = 2;
        defense = 0;
        expDrop = 30;

        droppableItems = ["Poção de cura simples"];
        itemsID = [1];
        dropChance = 27;
    }
}
