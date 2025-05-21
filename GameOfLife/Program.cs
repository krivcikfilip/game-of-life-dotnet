class Program
{
    static void Main()
    {
        Simulation simulation = new Simulation(10);

        while (true)
        {
            Console.Clear();
            DrawGrid(simulation.CurrentGrid);
            Thread.Sleep(5000);
            simulation.Run();
        }
    }

    static void DrawGrid(Grid grid)
    {
        for (int row = 0; row < grid.Size; row++)
        {
            for (int col = 0; col < grid.Size; col++)
            {
                OrganismType cell = grid.GetCell(row, col);


                switch (cell)
                {
                    case OrganismType.ZOMBIE:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }

                    case OrganismType.HUMAN:
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        }
                    case OrganismType.SUPER_HUMAN:
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        }
                }

                Console.Write($"{(int)cell} ");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}