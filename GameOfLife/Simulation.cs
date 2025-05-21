public class Simulation
{

    public Grid CurrentGrid { get; set; }

    public Simulation(int gridSize = 10)
    {
        CurrentGrid = new Grid(gridSize);
    }

    public void Run()
    {
        Grid newGrid = new Grid(CurrentGrid.Size);

        for (int row = 0; row < CurrentGrid.Size; row++)
        {
            for (int col = 0; col < CurrentGrid.Size; col++)
            {
                var neighbours = CurrentGrid.CalculateCellNeighbours(row, col);
                var currentType = CurrentGrid.Cells[row, col];

                // If there are two or three organisms of the same type living in the elements surrounding an organism of the same, type then it may survive.
                if (neighbours[currentType] == 2 || neighbours[currentType] == 3)
                {
                    newGrid.SetCell(row, col, currentType);
                    continue;
                }

                // If there are less than two organisms of one type surrounding one of the same type then it will die due to isolation.
                if (neighbours[currentType] < 2)
                {
                    newGrid.SetCell(row, col, OrganismType.ZOMBIE);
                    continue;
                }

                // If there are four or more organisms of one type surrounding one of the same type then it will die due to overcrowding.
                if (neighbours[currentType] >= 4)
                {
                    newGrid.SetCell(row, col, OrganismType.ZOMBIE);
                    continue;
                }

                List<OrganismType> birthCandidates = [];

                foreach (var neighbour in neighbours)
                {
                    if (neighbour.Value == 3)
                    {
                        birthCandidates.Add(neighbour.Key);
                    }
                }

                // If there are exactly three organisms of one type surrounding one element, they may give birth into that cell. The new organism is the same type as its parents.
                if (birthCandidates.Count > 0)
                {
                    var random = new Random();
                    int index = random.Next(birthCandidates.Count - 1);

                    newGrid.SetCell(row, col, birthCandidates[index]);
                }
                else
                {
                    newGrid.SetCell(row, col, currentType);
                }

            }
        }

        CurrentGrid = newGrid;
    }
}