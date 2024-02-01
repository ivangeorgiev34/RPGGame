using RPGGame.Contracts.Managers;
using RPGGame.Contracts.Services;
using RPGGame.Infrastructure.Enums;
using RPGGame.Infrastructure.Exceptions;
using RPGGame.Infrastructure.Models;
using RPGGame.Infrastructure.Structs;
using RPGGame.Managers;

namespace RPGGame
{
    public class ConsoleScreenManager : IScreenManager
    {
        private readonly IPlayerService _playerService;
        private char[,]? gamingBoard;
        private int playerPositionRow = 0;
        private int playerPositionCol = 0;
        private Dictionary<MonsterCoordinates, Monster> monsterCoordinates = new Dictionary<MonsterCoordinates, Monster>();
        private Random random = new Random();

        public ConsoleScreenManager(IPlayerService playerService)
        {
            this._playerService = playerService;
        }
        public void ShowCharacterSelect()
        {
            Console.Clear();

            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.WriteLine("Your pick: ");

            var characterNumber = Console.ReadKey(true).KeyChar;

            _playerService.AssignHeroToPlayer(characterNumber);

            Console.WriteLine("Would you like to buff up your stats before starting?                    (Limit: 3 points total)");
            Console.WriteLine("Response (Y\\N): ");

            var response = char.ToUpper(Console.ReadKey(true).KeyChar);

            if (response == 'Y')
            {
                int remainingPoints = 3;

                var remainingPointsText = $"Remaining Points: {remainingPoints}";

                Console.WriteLine(remainingPointsText);

                //gets the coordinates of the remaining points number, that is displayed in the console
                var pointsConsoleRowPosition = Console.CursorTop - 1;
                var pointsConsoleColPosition = Console.CursorLeft + remainingPointsText.Length - 1;

                _playerService.AddPointsToPlayer(remainingPoints, pointsConsoleRowPosition, pointsConsoleColPosition);

                Program.player?.Setup();

                _playerService.SavePlayerToDatabase();

                Program.currentScreen = Screen.InGame;
            }
            else if (response == 'N')
            {
                Program.player?.Setup();

                _playerService.SavePlayerToDatabase();

                Program.currentScreen = Screen.InGame;

            }
            else if (response != 'N')
            {
                throw new InvalidInputException("Error: You cannot type symbols that are different than Y or N!");
            }
        }

        public void ShowInGame()
        {
            Console.Clear();

            if (Program.player == null)
            {
                Console.WriteLine();

                ResetPlayerPosition();

                ResetMonsters();

                throw new ArgumentNullException("Error: Player cannot be null");
            }

            gamingBoard = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        gamingBoard[i, j] = Program.player.Symbol;

                        continue;
                    }

                    gamingBoard[i, j] = '▒';
                }
            }

            string statsText;
            int healthConsoleRowPosition;
            int manaConsoleRowPosition;

            while (Program.player.Health > 0)
            {
                statsText = $"Health: {Program.player.Health} Mana: {Program.player.Mana}";

                Console.WriteLine(statsText);

                //getting the location of the health and mana numbers on the console
                healthConsoleRowPosition = statsText.IndexOf(Program.player.Health.ToString()) + 1;

                manaConsoleRowPosition = statsText.IndexOf(Program.player.Mana.ToString()) + 1;

                Console.WriteLine();

                PrintGamingBoard(gamingBoard);

                Console.WriteLine("Choose action");
                Console.WriteLine("1) Attack");
                Console.WriteLine("2) Move");

                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int actionNumber) == true
                    && (actionNumber == 1 || actionNumber == 2))
                {
                    Console.WriteLine();

                    if (actionNumber == 1)
                    {
                        PrintMonstersInRange();

                        SpawnMonster();

                        CheckIfPlayerNeedsToBeDamaged();
                    }
                    else if (actionNumber == 2)
                    {
                        Console.WriteLine("Direction to move: ");

                        var moveDirection = char.ToUpper(Console.ReadKey().KeyChar);

                        MovePlayer(moveDirection);

                        SpawnMonster();

                        CheckIfPlayerNeedsToBeDamaged();
                    }

                    Console.Clear();
                }
                else
                {
                    ResetPlayerPosition();

                    ResetMonsters();

                    Console.WriteLine();

                    throw new InvalidInputException("Error: You can choose only between the numbers 1 and 2!");
                }
            }

            ResetPlayerPosition();

            ResetMonsters();

            Program.currentScreen = Screen.Exit;
        }

        public void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play!");

            var key = Console.ReadKey(true);

            if (key.Key.ToString() != "")
            {
                Program.currentScreen = Screen.CharacterSelect;
            }
        }

        private void PrintGamingBoard(char[,] gamingBoard)
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(gamingBoard[row, col]);
                }
                Console.WriteLine();
            }
        }

        private void MovePlayer(char moveDirection)
        {
            if (gamingBoard != null && Program.player != null)
            {
                switch (moveDirection)
                {
                    case 'W':

                        if (playerPositionRow - Program.player.Range < 0)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol] = Program.player!.Symbol;

                        playerPositionRow -= Program.player.Range;

                        break;
                    case 'S':
                        if (playerPositionRow + Program.player.Range > gamingBoard.GetLength(0) - 1)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol] = Program.player!.Symbol;

                        playerPositionRow += Program.player.Range;

                        break;
                    case 'D':
                        if (playerPositionCol + Program.player.Range > gamingBoard.GetLength(0) - 1)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow, playerPositionCol + Program.player.Range] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow, playerPositionCol + Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol += Program.player.Range;

                        break;
                    case 'A':
                        if (playerPositionCol - Program.player.Range < 0)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow, playerPositionCol - Program.player.Range] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow, playerPositionCol - Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol -= Program.player.Range;

                        break;
                    case 'E':
                        if (playerPositionCol + Program.player.Range > gamingBoard.GetLength(0) - 1
                            || playerPositionRow - Program.player.Range < 0)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol + Program.player.Range] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol + Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol += Program.player.Range;
                        playerPositionRow -= Program.player.Range;

                        break;
                    case 'X':
                        if (playerPositionCol + Program.player.Range > gamingBoard.GetLength(0) - 1
                            || playerPositionRow + Program.player.Range > gamingBoard.GetLength(0) - 1)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol + Program.player.Range] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol + Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol += Program.player.Range;
                        playerPositionRow += Program.player.Range;

                        break;
                    case 'Q':
                        if (playerPositionCol - Program.player.Range < 0
                            || playerPositionRow - Program.player.Range < 0)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol - Program.player.Range] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol - Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol -= Program.player.Range;
                        playerPositionRow -= Program.player.Range;

                        break;
                    case 'Z':
                        if (playerPositionCol - Program.player.Range < 0
                            || playerPositionRow + Program.player.Range > gamingBoard.GetLength(0) - 1)
                        {
                            OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol - Program.player.Range] == '■')
                        {
                            MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol - Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol -= Program.player.Range;
                        playerPositionRow += Program.player.Range;

                        break;
                    default:
                        ResetPlayerPosition();

                        ResetMonsters();

                        Console.WriteLine();

                        throw new InvalidInputException("Error: You can only choose between W, S, D, A, E, X, Q, or Z!");
                }
            }

        }

        private void CheckIfPlayerNeedsToBeDamaged()
        {

            IList<MonsterCoordinates> monstersDamagedPlayerCoordinates = new List<MonsterCoordinates>();

            //checking if there are any monsters around players that can attack the player
            for (int i = playerPositionRow - 1; i < playerPositionRow + 2; i++)
            {
                for (int j = playerPositionCol - 1; j < playerPositionCol + 2; j++)
                {
                    if (i < 0
                    || j < 0
                    || i > gamingBoard!.GetLength(0) - 1
                    || j > gamingBoard!.GetLength(0) - 1
                    || (playerPositionRow == i && playerPositionCol == j))
                    {
                        continue;
                    }

                    if (gamingBoard[i, j] == '■')
                    {
                        var monsterThatShouldDamageCoordinates = new MonsterCoordinates(i, j);

                        monsterCoordinates.TryGetValue(monsterThatShouldDamageCoordinates, out Monster monster);

                        Program.player.Health -= monster!.Damage;

                        monstersDamagedPlayerCoordinates.Add(monsterThatShouldDamageCoordinates);
                    }
                }
            }

            var monsterManager = new MonsterManager(this);

            monsterManager.MoveMonsters(monstersDamagedPlayerCoordinates, monsterCoordinates, ref playerPositionRow, ref playerPositionCol, gamingBoard);

            //MoveMonsters(monstersDamagedPlayerCoordinates, monsterCoordinates, playerPositionRow, playerPositionCol);
        }

        private void OutsidePlayingBoardError()
        {
            ResetPlayerPosition();

            ResetMonsters();

            Console.WriteLine();

            throw new InvalidInputException("Error: You cannot go outside the playing board!");
        }

        private void MonsterOnDesiredSpotError()
        {
            ResetPlayerPosition();

            ResetMonsters();

            Console.WriteLine();

            throw new InvalidInputException("Error: You cannot land on a spot where there is a monster already there!");
        }

        private void ResetPlayerPosition()
        {
            playerPositionRow = 0;
            playerPositionCol = 0;
        }

        private void ResetMonsters()
        {
            monsterCoordinates.Clear();
        }

        private void SpawnMonster()
        {
            var strenght = random.Next(1, 4);
            var agility = random.Next(1, 4);
            var intelligence = random.Next(1, 4);

            var monster = new Monster(strenght, agility, intelligence);

            monster.Setup();

            int randomRow;
            int randomCol;

            while (true)
            {
                randomRow = random.Next(0, 10);
                randomCol = random.Next(0, 10);

                if (gamingBoard![randomRow, randomCol] == '▒')
                {
                    gamingBoard[randomRow, randomCol] = monster.Symbol;

                    var newMonsterCoordinates = new MonsterCoordinates(randomRow, randomCol);

                    monsterCoordinates.Add(newMonsterCoordinates, monster);

                    if (randomRow + 1 == playerPositionRow
                        || randomRow - 1 == playerPositionRow
                        || randomCol + 1 == playerPositionCol
                        || randomRow - 1 == playerPositionCol
                        || (randomRow + 1 == playerPositionRow && randomCol - 1 == randomCol)
                        || (randomRow + 1 == playerPositionRow && randomCol + 1 == randomCol)
                        || (randomRow - 1 == playerPositionRow && randomCol - 1 == randomCol)
                        || (randomRow - 1 == playerPositionRow && randomCol + 1 == randomCol))
                    {
                        Program.player!.Health -= monster.Damage;
                    }

                    break;
                }
            }
        }

        private void PrintMonstersInRange()
        {
            var monsterChoiceDictionary = new Dictionary<int, MonsterCoordinates>();

            var monsterChoicesCounter = 0;


            //checking for monster in our range

            for (int i = playerPositionRow - Program.player!.Range; i <= Program.player!.Range * 2 + 2; i++)
            {
                for (int j = playerPositionCol - Program.player!.Range; j <= Program.player!.Range * 2 + 2; j++)
                {
                    //checking if we are on our player position or outside the array
                    if (i < 0
                        || j < 0
                        || i > gamingBoard!.GetLength(0) - 1
                        || j > gamingBoard!.GetLength(0) - 1
                        || (playerPositionRow == i && playerPositionCol == j))
                    {
                        continue;
                    }

                    if (gamingBoard![i, j] == '■')
                    {
                        monsterChoiceDictionary.Add(monsterChoicesCounter, new MonsterCoordinates(i, j));

                        monsterChoicesCounter += 1;
                    }
                }
            }

            if (monsterChoicesCounter == 0)
            {
                Console.WriteLine("No available targets in your range");
                Console.WriteLine("Press any key to continue with your next move!");

                Console.ReadKey(true);

                return;
            }

            foreach (var key in monsterChoiceDictionary.Keys)
            {
                if (monsterChoiceDictionary.TryGetValue(key, out MonsterCoordinates coordinates) == true && monsterCoordinates.TryGetValue(coordinates, out Monster monster) == true)
                {
                    Console.WriteLine($"{key}) target with remaining blood {monster.Health}");
                }
                else
                {
                    ResetPlayerPosition();

                    ResetMonsters();

                    throw new InvalidOperationException("Error: Monster not found on the map!");
                }
            }

            Console.Write("Which one to attack: ");

            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out int indexOfMonsterToAttack) == true)
            {

                if (monsterChoiceDictionary.TryGetValue(indexOfMonsterToAttack, out MonsterCoordinates coordinates) == true && monsterCoordinates.TryGetValue(coordinates, out Monster monster) == true)
                {
                    monster.Health -= Program.player!.Damage;

                    if (monster.Health <= 0)
                    {
                        gamingBoard![coordinates.Row, coordinates.Column] = '▒';

                        monsterCoordinates.Remove(coordinates);
                    }
                }
                else
                {
                    ResetPlayerPosition();

                    ResetMonsters();

                    Console.WriteLine();

                    throw new InvalidOperationException("Error: Monster not found on the map!");
                }
            }
            else
            {
                ResetPlayerPosition();

                ResetMonsters();

                Console.WriteLine();

                throw new InvalidInputException($"You can only choose between ${string.Join(", ", monsterChoiceDictionary.Keys)}");
            }


        }

    }
}
