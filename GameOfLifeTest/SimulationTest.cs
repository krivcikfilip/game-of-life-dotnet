[TestClass]
public sealed class SimulationTest
{
    [TestMethod]
    public void Constructor_Initializes_CorrectSize()
    {
        int expectedSize = 15;
        var simulation = new Simulation(expectedSize);

        Assert.AreEqual(expectedSize, simulation.CurrentGrid.Size);
        Assert.AreEqual(expectedSize, simulation.CurrentGrid.Cells.GetLength(0));
        Assert.AreEqual(expectedSize, simulation.CurrentGrid.Cells.GetLength(1));
    }

    [TestMethod]
    public void Run_AppliesCorrectRules_ForSingleGeneration()
    {
        var simulation = new Simulation(10);

        simulation.CurrentGrid.SetCell(0, 0, OrganismType.HUMAN);
        simulation.CurrentGrid.SetCell(0, 1, OrganismType.ZOMBIE);
        simulation.CurrentGrid.SetCell(0, 2, OrganismType.ZOMBIE);
        simulation.CurrentGrid.SetCell(1, 0, OrganismType.ZOMBIE);
        simulation.CurrentGrid.SetCell(1, 1, OrganismType.ZOMBIE);
        simulation.CurrentGrid.SetCell(1, 2, OrganismType.ZOMBIE);

        simulation.Run();

        Assert.AreEqual(OrganismType.ZOMBIE, simulation.CurrentGrid.Cells[0, 0]);

        simulation.CurrentGrid.SetCell(0, 0, OrganismType.HUMAN);
        simulation.CurrentGrid.SetCell(0, 1, OrganismType.HUMAN);
        simulation.CurrentGrid.SetCell(0, 2, OrganismType.ZOMBIE);
        simulation.CurrentGrid.SetCell(1, 0, OrganismType.HUMAN);
        simulation.CurrentGrid.SetCell(1, 1, OrganismType.ZOMBIE);
        simulation.CurrentGrid.SetCell(1, 2, OrganismType.ZOMBIE);

        simulation.Run();

        Console.WriteLine(simulation.CurrentGrid.GetCell(0, 0));
        Console.WriteLine(simulation.CurrentGrid.GetCell(0, 1));
        Console.WriteLine(simulation.CurrentGrid.GetCell(0, 2));

        Assert.AreEqual(OrganismType.HUMAN, simulation.CurrentGrid.Cells[0, 0]);
    }

}
