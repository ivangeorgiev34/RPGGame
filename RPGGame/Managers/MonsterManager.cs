using RPGGame.Infrastructure.Models;
using RPGGame.Infrastructure.Structs;

namespace RPGGame.Managers
{
    public class MonsterManager
    {
        private readonly ConsoleScreenManager _screenManager;

        public MonsterManager(ConsoleScreenManager screenManager)
        {
            _screenManager = screenManager;
        }

        public void MoveMonsters(IList<MonsterCoordinates> monstersDamagedPlayerCoordinates, Dictionary<MonsterCoordinates, Monster> monsterCoordinates, ref int playerPositionRow, ref int playerPositionCol, char[,]? gamingBoard)
        {
            var movingMonsters = monsterCoordinates.Keys
                .Where(m => monstersDamagedPlayerCoordinates.Any(mc => mc.Equals(m)) == false)
                .ToList();

            MonsterCoordinates currentMonsterCoordinates;

            for (int i = 0; i < movingMonsters.Count; i++)
            {
                currentMonsterCoordinates = movingMonsters[i];

                if (currentMonsterCoordinates.Row != playerPositionRow)
                {
                    if (currentMonsterCoordinates.Row < playerPositionRow)
                    {
                        MoveMonster(currentMonsterCoordinates, "Row", '+', gamingBoard, monsterCoordinates);

                    }
                    else if (currentMonsterCoordinates.Row > playerPositionRow)
                    {
                        MoveMonster(currentMonsterCoordinates, "Row", '-', gamingBoard, monsterCoordinates);
                    }
                }
                else if (currentMonsterCoordinates.Column != playerPositionCol)
                {
                    if (currentMonsterCoordinates.Column < playerPositionCol)
                    {
                        MoveMonster(currentMonsterCoordinates, "Column", '+', gamingBoard, monsterCoordinates);
                    }
                    else if (currentMonsterCoordinates.Column > playerPositionCol)
                    {
                        MoveMonster(currentMonsterCoordinates, "Column", '-', gamingBoard, monsterCoordinates);
                    }
                }
            }
        }

        public void MoveMonster(MonsterCoordinates currentMonsterCoordinates, string dimension, char operation, char[,]? gamingBoard, Dictionary<MonsterCoordinates, Monster> monsterCoordinates)
        {
            monsterCoordinates.TryGetValue(currentMonsterCoordinates, out Monster monster);

            monsterCoordinates.Remove(currentMonsterCoordinates);

            if (dimension == "Row")
            {
                if (operation == '+')
                {
                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '▒';

                    currentMonsterCoordinates.Row += 1;

                    monsterCoordinates.TryAdd(currentMonsterCoordinates, monster!);

                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '■';

                }
                else if (operation == '-')
                {
                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '▒';

                    currentMonsterCoordinates.Row -= 1;

                    monsterCoordinates.TryAdd(currentMonsterCoordinates, monster!);

                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '■';
                }
            }
            else if (dimension == "Column")
            {
                if (operation == '+')
                {
                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '▒';

                    currentMonsterCoordinates.Column += 1;

                    monsterCoordinates.TryAdd(currentMonsterCoordinates, monster!);

                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '■';

                }
                else if (operation == '-')
                {
                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '▒';

                    currentMonsterCoordinates.Column -= 1;

                    monsterCoordinates.TryAdd(currentMonsterCoordinates, monster!);

                    gamingBoard![currentMonsterCoordinates.Row, currentMonsterCoordinates.Column] = '■';
                }
            }

        }
    }
}
