namespace TentativaDeRPG;

class Player
{
    // leveling system
    int lvl;
    int exp;
    int maxExp;

    // stats
    readonly string name = "";
    int health;
    int maxHealth;
    int strength;
    int defense;

    // constructors
    public Player()
    {
        Console.Write("Digite o nome do player: ");
        name = Console.ReadLine()!;

        lvl = 1;
        exp = 0;
        maxExp = 100;
    
        health = 10;
        maxHealth = 10;
        strength = 1;
        defense = 0;
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

    public int Lvl
    {
        get { return lvl; }
        set { lvl = value; }
    }

    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }

    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }

    // functions
    public void ShowPlayerStats()
    {
        Console.WriteLine($"Player  Level: {lvl}  {exp}/{maxExp}\n");

        Console.WriteLine($"Nome..: {name}\n" +
                          $"Vida..: {health}/{maxHealth}\n" +
                          $"Força.: {strength}\n" +
                          $"Defesa: {defense}");
    }

    public void IncreaseStats()
    {
        MaxExp = Convert.ToInt32(MaxExp * 1.1);
        MaxHealth = Convert.ToInt32(MaxHealth * 1.2);
        Health = MaxHealth;

        if (Lvl % 3 == 0)
        {
            Strength++;
        }

        if (Lvl % 5 == 0)
        {
            Defense++;
        }
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