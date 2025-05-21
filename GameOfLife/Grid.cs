
public class Grid
{
    public int Size { get; }
    public OrganismType[,] Cells { get; }

    public Grid(int size = 10)
    {
        Size = size;
        Cells = new OrganismType[size, size];

        var organismProbability = new Dictionary<OrganismType, int>
        {
            { OrganismType.ZOMBIE, 50 },
            { OrganismType.HUMAN, 40 },
            { OrganismType.SUPER_HUMAN, 10 }
        };

        var organismLimits = CreateOrganismProbabilityLimits(organismProbability);
        var random = new Random();

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                int number = (int)Math.Round(random.NextDouble() * 100);
                var organism = organismLimits.FirstOrDefault(limit => number >= limit.Min && number <= limit.Max);
                Cells[row, col] = organism != null ? organism.Type : OrganismType.ZOMBIE;
            }
        }
    }

    public void SetCell(int row, int col, OrganismType type)
    {
        Cells[row, col] = type;
    }

    public OrganismType GetCell(int row, int col)
    {
        return Cells[row, col];
    }

    public static List<OrganismProbabilityLimit> CreateOrganismProbabilityLimits(
        Dictionary<OrganismType, int> propabilityMap
    )
    {
        var sorted = propabilityMap.OrderByDescending(x => x.Value).ToList();

        return sorted.Select((item, i) =>
        {
            if (i == 0)
            {
                return new OrganismProbabilityLimit
                {
                    Min = 0,
                    Max = item.Value,
                    Type = item.Key
                };
            }

            int sumOfPrevProbabilities = sorted.Take(i).Sum(item => item.Value);

            return new OrganismProbabilityLimit
            {
                Min = sumOfPrevProbabilities + 1,
                Max = sumOfPrevProbabilities + item.Value,
                Type = item.Key
            };
        }).ToList();
    }

    public Dictionary<OrganismType, int> CalculateCellNeighbours(int cellRow, int cellCol)
    {
        var neighbours = new Dictionary<OrganismType, int>
        {
            { OrganismType.ZOMBIE, 0 },
            { OrganismType.HUMAN, 0 },
            { OrganismType.SUPER_HUMAN, 0 },
        };

        for (int row = cellRow - 1; row <= cellRow + 1; row++)
        {
            for (int col = cellCol - 1; col <= cellCol + 1; col++)
            {

                // Is same cell
                if (row == cellRow && col == cellCol)
                {
                    continue;
                }

                // Is outside grid
                if (row < 0 || col < 0 || row >= Size || col >= Size)
                {
                    continue;
                }

                var type = Cells[row, col];
                neighbours[type]++;
            }
        }

        return neighbours;
    }
}

public class OrganismProbabilityLimit
{
    public OrganismType Type { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
}