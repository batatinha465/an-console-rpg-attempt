namespace TentativaDeRPG;

class Player
{
    // leveling system
    int lvl = 1;
    int exp = 0;
    int maxExp = 100;

    // stats
    string name = "";
    int health = 0;
    int maxHealth = 10;
    int strength = 1;
    int defense = 0;

    // constructors
    public Player()
    {
        Console.Write("Digite o nome do player: ");
        name = Console.ReadLine()!;

        health = maxHealth;
    }

    // properties
    public int Health
    {
        get { return health; }
        set { health = Math.Max(0, value); }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    // functions
    public void ShowPlayerStats()
    {
        Console.Clear();

        Console.WriteLine("Player\n");

        Console.WriteLine($"Nome..: {name}\n" +
                          $"Vida..: {health}/{maxHealth}\n" +
                          $"Força.: {strength}\n" +
                          $"Defesa: {defense}");
    }

    public void UseSimpleLifePotion()
    {
        Health += 5;

        Console.WriteLine($"\nPoção curou 5 de vida.");
        Console.ReadKey();

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}