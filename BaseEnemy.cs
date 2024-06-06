namespace TentativaDeRPG;

class BaseEnemy
{
    // stats
    protected string name = "";
    protected int health;
    protected int maxHealth;
    protected int strength;
    protected int defense;
    protected int expDrop;

    // properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = Math.Max(0, value); }
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

    public int ExpDrop
    {
        get { return expDrop; }
        set { expDrop = value; }
    }

    // functions
    public virtual void ShowEnemyStats()
    {
        Console.WriteLine("Inimigo\n");

        Console.WriteLine($"Nome..: {name}\n" +
                          $"Vida..: {health}/{maxHealth}\n" +
                          $"Força.: {strength}\n" +
                          $"Defesa: {defense}");
    }
}
