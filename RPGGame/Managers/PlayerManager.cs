using RPGGame.Contracts.Managers;
using RPGGame.Infrastructure.Exceptions;

namespace RPGGame.Managers
{
    public class PlayerManager
    {
        private readonly IScreenManager _screenManager;
        public PlayerManager(IScreenManager screenManager)
        {
            this._screenManager = screenManager;
        }

        public void MovePlayer(char moveDirection, char[,]? gamingBoard, ref int playerPositionRow, ref int playerPositionCol)
        {
            if (gamingBoard != null && Program.player != null)
            {
                switch (moveDirection)
                {
                    case 'W':

                        if (playerPositionRow - Program.player.Range < 0)
                        {
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol] = Program.player!.Symbol;

                        playerPositionRow -= Program.player.Range;

                        break;
                    case 'S':
                        if (playerPositionRow + Program.player.Range > gamingBoard.GetLength(0) - 1)
                        {
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol] = Program.player!.Symbol;

                        playerPositionRow += Program.player.Range;

                        break;
                    case 'D':
                        if (playerPositionCol + Program.player.Range > gamingBoard.GetLength(0) - 1)
                        {
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow, playerPositionCol + Program.player.Range] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow, playerPositionCol + Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol += Program.player.Range;

                        break;
                    case 'A':
                        if (playerPositionCol - Program.player.Range < 0)
                        {
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow, playerPositionCol - Program.player.Range] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow, playerPositionCol - Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol -= Program.player.Range;

                        break;
                    case 'E':
                        if (playerPositionCol + Program.player.Range > gamingBoard.GetLength(0) - 1
                            || playerPositionRow - Program.player.Range < 0)
                        {
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol + Program.player.Range] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
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
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol + Program.player.Range] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
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
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow - Program.player.Range, playerPositionCol - Program.player.Range] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
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
                            _screenManager.OutsidePlayingBoardError();
                        }

                        if (gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol - Program.player.Range] == '■')
                        {
                            _screenManager.MonsterOnDesiredSpotError();
                        }

                        gamingBoard[playerPositionRow, playerPositionCol] = '▒';
                        gamingBoard[playerPositionRow + Program.player.Range, playerPositionCol - Program.player.Range] = Program.player!.Symbol;

                        playerPositionCol -= Program.player.Range;
                        playerPositionRow += Program.player.Range;

                        break;
                    default:
                        _screenManager.ResetPlayerPosition();

                        _screenManager.ResetMonsters();

                        Console.WriteLine();

                        throw new InvalidInputException("Error: You can only choose between W, S, D, A, E, X, Q, or Z!");
                }
            }

        }
    }
}
