
using RPGGame.Contracts.Managers;
using RPGGame.Contracts.Services;
using RPGGame.Infrastructure.Enums;
using RPGGame.Infrastructure.Exceptions;

namespace RPGGame
{
    public class ConsoleScreenManager : IScreenManager
    {
        private readonly IPlayerService _playerService;
        private char[,]? gamingBoard;
        private int playerPositionRow = 0;
        private int playerPositionCol = 0;
        public ConsoleScreenManager(IPlayerService playerService)
        {
            this._playerService = playerService;
        }
        public async void ShowCharacterSelect()
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

                await _playerService.SavePlayerToDatabaseAsync();

                Program.currentScreen = Screen.InGame;
            }
            else if (response == 'N')
            {
                Program.player?.Setup();

                await _playerService.SavePlayerToDatabaseAsync();

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
                    if (actionNumber == 1)
                    {

                    }
                    else if (actionNumber == 2)
                    {
                        Console.WriteLine("Direction to move: ");

                        var moveDirection = char.ToUpper(Console.ReadKey().KeyChar);

                        MovePlayer(moveDirection);
                    }

                    Console.Clear();
                }
                else
                {
                    Error();
                }
            }

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
            if (gamingBoard != null)
            {
                switch (moveDirection)
                {
                    case 'W':

                        if (playerPositionRow - 1 < 0)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - 1, playerPositionCol] = Program.player!.Symbol;

                        playerPositionRow -= 1;

                        break;
                    case 'S':
                        if (playerPositionRow + 1 > gamingBoard.GetLength(0) - 1)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + 1, playerPositionCol] = Program.player!.Symbol;

                        playerPositionRow += 1;

                        break;
                    case 'D':
                        if (playerPositionCol + 1 > gamingBoard.GetLength(0) - 1)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow, playerPositionCol + 1] = Program.player!.Symbol;

                        playerPositionCol += 1;

                        break;
                    case 'A':
                        if (playerPositionCol - 1 < 0)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow, playerPositionCol - 1] = Program.player!.Symbol;

                        playerPositionCol -= 1;

                        break;
                    case 'E':
                        if (playerPositionCol + 1 > gamingBoard.GetLength(0) - 1
                            || playerPositionRow - 1 < 0)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - 1, playerPositionCol + 1] = Program.player!.Symbol;

                        playerPositionCol += 1;
                        playerPositionRow -= 1;

                        break;
                    case 'X':
                        if (playerPositionCol + 1 > gamingBoard.GetLength(0) - 1
                            || playerPositionRow + 1 > gamingBoard.GetLength(0) - 1)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + 1, playerPositionCol + 1] = Program.player!.Symbol;

                        playerPositionCol += 1;
                        playerPositionRow += 1;

                        break;
                    case 'Q':
                        if (playerPositionCol - 1 < 0
                            || playerPositionRow - 1 < 0)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - 1, playerPositionCol - 1] = Program.player!.Symbol;

                        playerPositionCol -= 1;
                        playerPositionRow -= 1;

                        break;
                    case 'Z':
                        if (playerPositionCol - 1 < 0
                            || playerPositionRow + 1 > gamingBoard.GetLength(0) - 1)
                        {
                            Error();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + 1, playerPositionCol - 1] = Program.player!.Symbol;

                        playerPositionCol -= 1;
                        playerPositionRow += 1;

                        break;
                    default:
                        throw new InvalidInputException("Error: You can only choose between W, S, D, A, E, X, Q, or Z!");
                }
            }

        }

        private void OutsidePlayingBoardError()
        {
            Console.WriteLine();

            throw new InvalidInputException("Error: You cannot go outside the playing board!");
        }

        private void MonsterOnDesiredSpotError()
        {
            Console.WriteLine();

            throw new InvalidInputException("Error: You cannot land on a spot where there is a monster already there!");
        }
    }
}
