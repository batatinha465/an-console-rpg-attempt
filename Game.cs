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
        BaseEnemy[] enemies = [new Lixoso(), new Gordalho(), new Xexelento()];
        
        int spawnChance = random.Next(1, 101);

        if (killCount >= 5 && spawnChance < 40)
        {
            int index = random.Next(1, enemies.Length);
            
            enemy = enemies[index];
        }
        else
        {
            enemy = enemies[0];
        }

        battleEnded = false;
    }

    public void ShowStats()
    {
        Console.Clear();
        Console.WriteLine($"Batalha {killCount + 1}\n");
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

            if (int.TryParse(Console.ReadLine(), out int itemChosen) && itemChosen > 0 && itemChosen <= items.Inventory.Count)
            {
                CheckItem(itemChosen - 1);
                validMove = true;
            }
            else if (itemChosen == 0)
            {
                ShowStats();
                PlayerTurn();
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

        items.Inventory.RemoveAt(index);
        items.InventoryID.RemoveAt(index);
    }

    // enemy functions
    public void EnemyTurn()
    {
        int damage = Math.Max(0, enemy.Strength - player.Defense);

        Console.WriteLine($"{enemy.Name} causou {damage} de dano a você.");
        Console.ReadKey();

        player.Health -= damage;
    }

    // checking functions
    public void CheckExp()
    {
        player.Exp += enemy.ExpDrop;

        if (player.Exp >= player.MaxExp)
        {
            int extraExp = player.Exp - player.MaxExp;

            player.Exp = extraExp;

            Console.WriteLine("\nVocê upou de level!");
            Console.ReadKey();

            player.Lvl++;
            player.IncreaseStats();
        }
    }

    public void CheckDrops()
    {
        int dropChance = random.Next(1, 101); 
        
        if (dropChance < enemy.DropChance)
        {
            int index = random.Next(0, enemy.DroppableItems.Length);

            Console.WriteLine($"\nVocê ganhou {enemy.DroppableItems[index]}!");
            Console.ReadKey();

            items.Inventory.Add(enemy.DroppableItems[index]);
            items.InventoryID.Add(enemy.ItemsID[index]);
        }
    }

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
            ShowStats();
            Console.WriteLine("\nInimigo morreu. Você venceu!");
            Console.ReadKey();

            Console.WriteLine($"\nVocê ganhou {enemy.ExpDrop} de xp.");
            Console.ReadKey();
            killCount++;
            CheckExp();
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
