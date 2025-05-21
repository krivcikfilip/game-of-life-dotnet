[TestClass]
public sealed class GridTest
{
    [TestMethod]
    public void Constructor_Initializes_CorrectSize()
    {
        int expectedSize = 15;
        var grid = new Grid(expectedSize);

        Assert.AreEqual(expectedSize, grid.Size);
        Assert.AreEqual(expectedSize, grid.Cells.GetLength(0));
        Assert.AreEqual(expectedSize, grid.Cells.GetLength(1));
    }

    [TestMethod]
    public void CreateOrganismProbabilityLimits_ReturnsCorrectLimits()
    {
        var organismProbability = new Dictionary<OrganismType, int>
    {
        { OrganismType.ZOMBIE, 50 },
        { OrganismType.HUMAN, 40 },
        { OrganismType.SUPER_HUMAN, 10 }
    };

        var probabilityLimits = Grid.CreateOrganismProbabilityLimits(organismProbability);

        Assert.AreEqual(OrganismType.ZOMBIE, probabilityLimits[0].Type);
        Assert.AreEqual(0, probabilityLimits[0].Min);
        Assert.AreEqual(50, probabilityLimits[0].Max);

        Assert.AreEqual(OrganismType.HUMAN, probabilityLimits[1].Type);
        Assert.AreEqual(51, probabilityLimits[1].Min);
        Assert.AreEqual(90, probabilityLimits[1].Max);

        Assert.AreEqual(OrganismType.SUPER_HUMAN, probabilityLimits[2].Type);
        Assert.AreEqual(91, probabilityLimits[2].Min);
        Assert.AreEqual(100, probabilityLimits[2].Max);
    }


    [TestMethod]
    public void SetCell_And_GetCell_WorkCorrectly()
    {
        var grid = new Grid(10);
        int row = 0;
        int col = 0;

        OrganismType expectedType = OrganismType.SUPER_HUMAN;

        grid.SetCell(row, col, expectedType);
        OrganismType actualType = grid.GetCell(row, col);

        Assert.AreEqual(expectedType, actualType);
    }

    [TestMethod]
    public void CalculateCellNeighbours_ReturnsCorrectCounts()
    {
        var grid = new Grid(10);

        grid.SetCell(0, 0, OrganismType.ZOMBIE);
        grid.SetCell(0, 1, OrganismType.ZOMBIE);
        grid.SetCell(0, 2, OrganismType.ZOMBIE);
        grid.SetCell(1, 0, OrganismType.ZOMBIE);
        grid.SetCell(1, 1, OrganismType.HUMAN);
        grid.SetCell(1, 2, OrganismType.ZOMBIE);
        grid.SetCell(2, 0, OrganismType.ZOMBIE);
        grid.SetCell(2, 1, OrganismType.SUPER_HUMAN);
        grid.SetCell(2, 2, OrganismType.ZOMBIE);

        var neighbors = grid.CalculateCellNeighbours(1, 1);

        Assert.AreEqual(0, neighbors[OrganismType.HUMAN]);
        Assert.AreEqual(1, neighbors[OrganismType.SUPER_HUMAN]);
        Assert.AreEqual(7, neighbors[OrganismType.ZOMBIE]);
    }

}
