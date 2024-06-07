namespace TentativaDeRPG;

class Game
{
    // object instantiation
    Player player;
    BaseEnemy enemy;
    Items items;

    // variables
    Random random;
    bool gameEnded;
    bool battleEnded;
    int playerOriginalDefense;
    int killCount;

    // constructor
    public Game()
    {
        player = new Player();
        enemy = new BaseEnemy();
        items = new Items();

        random = new Random();
        gameEnded = false;
        playerOriginalDefense = player.Defense;
    }

    // options enum
    public enum Moves
    {
        Attack = 1,
        Defend = 2,
        Item = 3
    }

    // items enum
    public enum ItemsID
    {
        SimpleLifePotion = 1
    }

    // game functions
    public void Run()
    {
        while (gameEnded == false)
        {
            StartBattle();

            while (battleEnded == false)
            {
                ShowStats();

                battleEnded = CheckBattleState();
                if (battleEnded == true) continue;

                PlayerTurn();

                battleEnded = CheckBattleState();
                if (battleEnded == true) continue;

                EnemyTurn();
                ResetStats();
            }

            gameEnded = CheckGameState();
        }
    }

    public void StartBattle()
    {
        int spawnChance = random.Next(1, 101);

        if (killCount >= 5 && spawnChance < 40)
        {
            enemy = new Gordalho();
        }
        else
        {
            Console.WriteLine(spawnChance);
            Console.ReadKey();
            enemy = new Lixoso();
        }

        battleEnded = false;
    }

    public void ShowStats()
    {
        player.ShowPlayerStats();
        Console.WriteLine();
        enemy.ShowEnemyStats();
    }

    // player functions
    public void PlayerTurn()
    {
        bool validMove = false;

        while (validMove == false)
        {
            Console.WriteLine("\n1 - Atacar");
            Console.WriteLine("2 - Defender");
            Console.WriteLine("3 - Item");
            Console.Write("\nO que deseja fazer: ");

            if (int.TryParse(Console.ReadLine(), out int move) && Enum.IsDefined(typeof(Moves), move))
            {
                ExecutePlayerMove((Moves)move);
                validMove = true;
            }
            else
            {
                Console.WriteLine("\nOpção inválida, tente novamente.");
                Console.ReadKey();
                ShowStats();
            }
        }
    }

    public void ExecutePlayerMove(Moves move)
    {
        int damage;

        switch (move)
        {
            case Moves.Attack:
                damage = Math.Max(0, player.Strength - enemy.Defense);
                Console.WriteLine($"\nVocê causou {damage} de dano a {enemy.Name}.");
                Console.ReadKey();
                enemy.Health -= damage;
                break;
            case Moves.Defend:
                Console.WriteLine("\nVocê aumenta sua defesa em 1 nessa rodada.");
                Console.ReadKey();
                player.Defense++;
                break;
            case Moves.Item:
                ChooseItem();
                break;
        }
    }

    public void ChooseItem()
    {
        bool validMove = false;

        while (validMove == false)
        {
            items.ShowInventory();

            Console.Write("\n\nEscolha um item: ");

            if (int.TryParse(Console.ReadLine(), out int itemChosen) && itemChosen > 0 && itemChosen < 4)
            {
                CheckItem(itemChosen - 1);
                validMove = true;
            }
            else
            {
                Console.WriteLine("\nOpção inválida, tente novamente.");
                Console.ReadKey();
                ShowStats();
            }
        }
    }

    public void CheckItem(int index)
    {
        switch ((ItemsID)items.InventoryID[index])
        {
            case ItemsID.SimpleLifePotion:
                player.UseSimpleLifePotion();
                break;
            case 0:
                Console.WriteLine("\nOpção inválida, tente novamente.");
                Console.ReadKey();
                ShowStats();
                ChooseItem();
                break;
        }

        items.Inventory[index] = "Vazio";
        items.InventoryID[index] = 0;
    }

    // enemy functions
    public void EnemyTurn()
    {
        int damage = Math.Max(0, enemy.Strength - player.Defense);

        Console.WriteLine($"{enemy.Name} causou {damage} de dano a você.");
        Console.ReadKey();

        player.Health -= damage;
    }

    public void CheckDrops()
    {
        player.Exp += enemy.ExpDrop;

        if (player.Exp >= player.MaxExp)
        {
            int extraExp = player.Exp - player.MaxExp;

            player.Exp = extraExp;

            Console.WriteLine("\nVocê upou de level!");
            Console.ReadKey();

            player.Lvl++;
            player.MaxExp = Convert.ToInt32(player.MaxExp * 1.1);
            player.IncreaseStats();
        }
    }

    // checking functions
    public bool CheckBattleState()
    {
        if (player.Health == 0)
        {
            ShowStats();
            Console.WriteLine("\nVocê morreu. Jogo acabou.");
            Console.ReadKey();
            return true;
        }
        else if (enemy.Health == 0)
        {
            killCount++;
            ShowStats();
            Console.WriteLine("\nInimigo morreu. Você venceu!");
            Console.ReadKey();

            Console.WriteLine($"\nVocê ganhou {enemy.ExpDrop} de xp.");
            Console.ReadKey();
            CheckDrops();
            return true;
        }

        return false;
    }

    public bool CheckGameState()
    {
        if (player.Health == 0)
        {
            return true;
        }
        else if (killCount == 10)
        {
            Console.WriteLine($"\nVocê matou {killCount} inimigos. Fim de jogo, por enquanto.");
            return true;
        }

        return false;
    }

    // misc functions
    public void ResetStats()
    {
        player.Defense = playerOriginalDefense;
    }
}
