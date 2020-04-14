using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
		public Random rand = new Random();
		public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
		int score, headX, headY, fruitX, fruitY, nTail;
		int[] TailX = new int[100];
		int[] TailY = new int[100];
		
		int speed = 0;

		const int height = 25;
		const int width = 75;

		bool gameOver, reset, isprinted, horizontal, vertical, level;
		string dir, predir;
		
		void ShowBanner()
		{
			Console.SetWindowSize(width, height + 6);
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.CursorVisible = false;

			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("                          WELCOME TO GAME SNAKE");
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("            Controller: Use Arrow buttons to move the shake");
			Console.WriteLine("                        -Press S to pause");
			Console.WriteLine("                        -Press R to reset game");
			Console.WriteLine("                        -Press ESC to quit game ");
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("                        Select the difficulty level");
			Console.WriteLine("                                1 = easy");
			Console.WriteLine("                                2 = Average");
			Console.WriteLine("                                3 = hard");
			Console.WriteLine("                        Press to key for select level");
			Console.WriteLine();
			Console.WriteLine();
			


			level = true;
			keypress = Console.ReadKey(true);
			while (level == true)
			{
				if (keypress.Key == ConsoleKey.D1)
				{
					speed = 1;
					Console.WriteLine("                            SELECTED EASY LEVEL");
					level = false;
				}
				else if (keypress.Key == ConsoleKey.D2)
				{
					speed = 2;
					Console.WriteLine("                            SELECTED AVERAGE LEVEL");
					level = false;
				}
				else if (keypress.Key == ConsoleKey.D3)
				{
					speed = 3;
					Console.WriteLine("                            SELECTED HARD LEVEL");
					level = false;
				}
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("                            Press Any Key to Play");
			keypress = Console.ReadKey(true);
			if (keypress.Key == ConsoleKey.Escape)
				Environment.Exit(0);

		}


		void Setup()
		{
			dir = "RIGHT";
			predir = "";
			score = 0;
			nTail = 0;



			gameOver = false;
			reset = false;
			isprinted = false;

			headX = width / 2;
			headY = height / 2;

		    fruitX = rand.Next(1, width - 1);
		    fruitY = rand.Next(1, height - 1);

		}
	



		

		void CheckInput()
		{
			while (Console.KeyAvailable)
			{
				keypress = Console.ReadKey(true);
				if (keypress.Key == ConsoleKey.Escape)
				{
					Environment.Exit(0);
				}
				if (keypress.Key == ConsoleKey.S)
				{
					predir = dir;
					dir = "STOP";
				}
				else if (keypress.Key == ConsoleKey.LeftArrow)
				{
					predir = dir;
					dir = "LEFT";
				}
				else if (keypress.Key == ConsoleKey.RightArrow)
				{
					predir = dir;
					dir = "RIGHT";
				}
				else if (keypress.Key == ConsoleKey.LeftArrow)
				{
					predir = dir;
					dir = "LEFT";
				}
				else if (keypress.Key == ConsoleKey.UpArrow)
				{
					predir = dir;

					dir = "UP";
				}
				else if (keypress.Key == ConsoleKey.DownArrow)
				{
					predir = dir;
					dir = "DOWN";
				}

			}
		}

		void Logic()
		{
			int PreX = TailX[0];
			int PreY = TailY[0];
			int tempX, tempY;

			if (dir != "STOP")
			{
				TailX[0] = headX;
				TailY[0] = headY;

				for (int i = 1; i < nTail; ++i)
				{
					tempX = TailX[i];
					tempY = TailY[i];
					TailX[i] = PreX;
					TailY[i] = PreY;
					PreX = tempX;
					PreY = tempY;
				}
			}

			switch (dir)
			{
				case "RIGHT":
					headX+=speed;
					break;

				case "LEFT":
					headX-=speed;
					break;

				case "UP":
					headY-=speed;
					break;

				case "DOWN":
					headY+=speed;
					break;
				case "STOP":
					while (true)
					{
						Console.Clear();
						Console.CursorLeft = width / 2 - 6;
						Console.WriteLine("Game paused");
						Console.WriteLine();
						Console.WriteLine();
						Console.WriteLine("    -Press S to resume game");
						Console.WriteLine("    -Press R to reset game");
						Console.WriteLine("    -Press ESC to quit game");
						keypress = Console.ReadKey(true);
						if (keypress.Key == ConsoleKey.Escape)
						{
							Environment.Exit(0);
						}
						if (keypress.Key == ConsoleKey.R)
						{
							reset = true;
							break;
						}
						if (keypress.Key == ConsoleKey.S)
						{
							break;
						}
					}
						dir = predir;
						break;
			}
			if (headX <= 0 || headX >= width - 1 || headY <= 0 || headY >= height - 1)
			{
				gameOver = true;
			}
			else
			{
				gameOver = false;
			}

			if (headX == fruitX && headY == fruitY)
			{
				score += 10;
				nTail++;
				fruitX = rand.Next(1, width - 1);
				fruitY = rand.Next(1, height - 1);
			}

			if (((dir == "LEFT" && predir != "UP") && (dir == "LEFT" && predir != "DOWN")) || ((dir == "RIGHT" && predir != "UP") && (dir == "RIGHT" && predir != "DOWN")))
			{
				horizontal = true;
			}
			else
			{
				horizontal = false;
			}

			if (((dir == "UP" && predir != "LEFT") && (dir == "UP" && predir != "RIGHT")) || ((dir == "DOWN" && predir != "LEFT") && (dir == "DOWN" && predir != "RIGHT")))
			{
				vertical = true;
			}
			else
			{
				vertical = false;
			}

			for (int i = 1; i < nTail; ++i)
			{
				if (TailX[i] == headX && TailY[i] == headY)
				{
					if (horizontal || vertical)
					{
						gameOver = false;
					}
					else
					{
						gameOver = true;
					}
				}
				if (TailX[i] == fruitX && TailY[i] == fruitY)
				{
					fruitX = rand.Next(1, width - 1);
					fruitY = rand.Next(1, height - 1);
				}
				
				
				
			}
		}

		void Render()
		{
			Console.SetCursorPosition(0, 0);
			for (int i = 0; i < height; ++i)
			{
				for (int j = 0; j < width; ++j)
				{
					if (i == 0 || i == height - 1)
					{
						Console.Write("-");
					}
					else if (j == 0 || j == width - 1)
					{
						Console.Write("|");
					}
					else if (j == fruitX && i == fruitY)
					{
						Console.Write("F");
					}
					else if (j == headX && i == headY)
					{
						Console.Write("0");
					}
					else
					{
						isprinted = false;
						for (int k = 0; k < nTail; ++k)
						{
							if (TailX[k] == j && TailY[k] == i)
							{
								Console.Write("o");
								isprinted = true;
							}
						}
						if (!isprinted)
						{
							Console.Write(" ");
						}
					}



				}
				Console.WriteLine();

			}
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("        Shake Game              ");
			Console.WriteLine();
			Console.WriteLine("Your score:" + score);


		}

		void Lose()
		{
			Console.CursorTop = height + 3;
			Console.CursorLeft = width / 2 - 4;
			Console.WriteLine("YOU DIED");
			Console.WriteLine("Press R to reset game");
			Console.WriteLine("Press ESC to quit game");
			while (true)
			{
				keypress = Console.ReadKey(true);
				if (keypress.Key == ConsoleKey.Escape)
				{
					Environment.Exit(0);
				}
				if (keypress.Key == ConsoleKey.R)
				{
					reset = true;
					break;
				}
			}
		}

		void Update()
		{
			while (!gameOver)
			{
				CheckInput();
				Logic();
				Render();
				if (reset)
					break;
			}
			if (gameOver)
				Lose();


		}

		static void Main(string[] args)
		{
			Program Snake = new Program();
			Snake.ShowBanner();
			while (true)
			{
				Snake.Setup();
				Snake.Update();
				Console.Clear();
			}



		}
	}
}
