namespace TentativaDeRpg;

internal class Game
{
    // object instantiation
    Player player;
    BaseEnemy enemy;

    // variables
    bool gameEnded;
    bool battleEnded;
    int playerOriginalDefense;
    int killCount;

    // constructor
    public Game()
    {
        player = new Player();
        enemy = new BaseEnemy();

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

    // functions
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
        enemy = new Lixoso();
        battleEnded = false;
    }

    public void ShowStats()
    {
        player.ShowPlayerStats();
        Console.WriteLine();
        enemy.ShowEnemyStats();
    }

    public void PlayerTurn()
    {
        Console.WriteLine("\n1 - Atacar");
        Console.WriteLine("2 - Defender");
        Console.WriteLine("3 - Item");
        Console.Write("\nO que deseja fazer: ");

        if (int.TryParse(Console.ReadLine(), out int move) && Enum.IsDefined(typeof(Moves), move))
        {
            ExecutePlayerMove((Moves)move);
        }
        else
        {
            Console.WriteLine("\nOpção inválida, tente novamente.");
            Console.ReadKey();
            ShowStats();
            PlayerTurn();
            return;
        }
    }

    public void ExecutePlayerMove(Moves move)
    {
        int damage;

        switch (move)
        {
            case Moves.Attack:
                damage = Math.Max(0, player.Strength - enemy.Defense);
                Console.WriteLine($"\nVocê causou {damage} de dano a {enemy.Name}");
                enemy.Health -= damage;
                break;
            case Moves.Defend:
                Console.WriteLine("\nVocê aumenta sua defesa em 1 nessa rodada.");
                player.Defense++;
                break;
            case Moves.Item:
                Console.WriteLine("\nVocê não possui nenhum item.");
                Console.ReadKey();
                ShowStats();
                PlayerTurn();
                break;
        }

        Console.ReadKey();
    }

    public void EnemyTurn()
    {
        int damage = Math.Max(0, enemy.Strength - player.Defense);

        Console.WriteLine($"{enemy.Name} causou {damage} de dano a você.");
        Console.ReadLine();

        player.Health -= damage;
    }

    public void ResetStats()
    {
        player.Defense = playerOriginalDefense;
    }

    public bool CheckBattleState()
    {
        if (player.Health == 0)
        {
            ShowStats();
            Console.WriteLine("Você morreu. Jogo acabou.");
            Console.ReadKey();
            return true;
        }
        else if (enemy.Health == 0)
        {
            ShowStats();
            Console.WriteLine("\nInimigo morreu. Você venceu!");
            Console.ReadKey();
            killCount++;
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
        else if (killCount == 3)
        {
            Console.WriteLine($"\nVocê matou {killCount} inimigos. Fim de jogo, por enquanto.");
            return true;
        }

        return false;
    }
}

