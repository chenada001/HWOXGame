using System;

namespace HWOXGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OXGameEngine game = new OXGameEngine();
            game.PlayOXGame();
        }
        // 定義遊戲引擎類別
        public class OXGameEngine
        {
            private char[,] gameMarkers;
            public OXGameEngine()
            {
                gameMarkers = new char[3, 3]; // 遊戲棋盤
                ResetGame(); // 初始化遊戲棋盤並重置遊戲
            }

            public void SetMarker(int x, int y, char player)
            {
                if (IsValidMove(x, y))
                {
                    gameMarkers[x, y] = player;
                }
                else
                {
                    throw new ArgumentException("Invalid move!");
                }
            }

            public void ResetGame() // 重置遊戲
            {
                gameMarkers = new char[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        gameMarkers[i, j] = ' ';
                    }
                }
            }

            public char IsWinner()
            {
                // 檢查橫向
                for (int i = 0; i < 3; i++)
                {
                    if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                    {
                        return gameMarkers[i, 0];
                    }
                }

                // 檢查縱向
                for (int j = 0; j < 3; j++)
                {
                    if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                    {
                        return gameMarkers[0, j];
                    }
                }

                // 檢查對角線
                if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
                {
                    return gameMarkers[0, 0];
                }

                if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
                {
                    return gameMarkers[0, 2];
                }

                return ' '; // 沒有贏家出現
            }

            public bool IsValidMove(int x, int y)
            {
                if (x < 0 || x >= 3 || y < 0 || y >= 3)
                {
                    Console.WriteLine("輸入錯誤，請重新輸入。"); // 當遊戲格式輸入錯誤，要求重新輸入
                    return false;
                }

                if (gameMarkers[x, y] != ' ')
                {
                    Console.WriteLine("已經有人下過了，請重新選位置!"); // 輸入如果重複，要求重新輸入
                    return false;
                }

                return true;
            }

            public char GetMarker(int x, int y)
            {
                return gameMarkers[x, y];
            }

            public void PrintBoard() // 棋盤格式
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(gameMarkers[i, j]);
                        if (j < 2)
                        {
                            Console.Write(" | ");
                        }
                    }
                    Console.WriteLine();
                    if (i < 2)
                    {
                        Console.WriteLine("---------");
                    }
                }
            }
            // 開始遊戲
            public void PlayOXGame()
            {
                Console.WriteLine("Welcome to OX Game!");

                ResetGame();

                char currentPlayer = 'x'; // 預設玩家為 'x'
                char winner = ' ';
                int moves = 0;

                while (winner == ' ' && moves < 9)  // 當有一方贏了或所有格子都填滿了，遊戲結束
                {
                    Console.WriteLine($"玩家 {currentPlayer}, 下位置");
                    PrintBoard();

                    int x, y;
                    do
                    {
                        Console.Write("請輸入行橫向與縱向座標: "); // 輸入座標
                        string[] input = Console.ReadLine().Split(' ');
                        x = int.Parse(input[0]);
                        y = int.Parse(input[1]);
                    } while (!IsValidMove(x, y));

                    SetMarker(x, y, currentPlayer);

                    winner = IsWinner();

                    // 切換玩家
                    currentPlayer = (currentPlayer == 'x') ? 'o' : 'x';
                    moves++;
                }

                PrintBoard(); 

                if(winner == ' ')
                {
                    Console.WriteLine("遊戲結束"); 
                }
                if (winner == ' ')
                {
                    Console.WriteLine("平手!");
                }
                else
                {
                    Console.WriteLine($"玩家 {winner} 贏了!");
                }
            }           
        }
    }
}
