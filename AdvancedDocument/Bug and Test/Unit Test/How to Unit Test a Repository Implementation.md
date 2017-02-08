[How to Unit Test a Repository Implementation](http://www.bradoncode.com/blog/2012/12/how-to-unit-test-repository.html)



I like to unit test every layer within my applications. Unit testing data access code can be a little tricky, firstly because they aren't really unit tests.

Unit tests are defined as:

>   A unit of code tested in isolation from the remainder of the modules of an application.

If write unit tests against a database, we're not really testing anything in isolation since the database is made of multiple modules. Another rule of unit testing is:

Don't write unit tests for code that you don't own.

In my case I didn't write SQL Server so therefore I cannot write unit tests against it. But, I still want to write some form of test that runs against a database to assert the quality and correctness of my code.

Instead of Unit Tests, I can use Integration Tests. Integration testing is:

 >   The phase in software testing in which individual software modules are combined and tested as a group.

It's similar to unit testing, however it's important to know the differences of the two and to understand the terminology.

Below is an example of the type of integration tests I will write for CRUD (create, retrieve, update, delete) operations for a repository. Before we look at the code, note the following design features of this integration test:

    No data dependencies. This test can be run against a blank database and will pass. It constructs all the data is needs for assertions.
    Tests the main CRUD operations in sequence.
    Although designed to run in a sequence, each test can be run standalone when debugging if required.
    Can be run against any implementation of the ICompetitionRepository interface without having to change the test. E.g. If the concrete implementation is rewritten no code changes need to be made to the test.

NB The Competition class in this example is a simple POCO (think Northwind Product class) taken from the blog series ASP.Net 10 Years On: 10 Years of .Net Compressed into Weeks.

Here is the code:


```cs

[TestClass]
public class CompetitionTests
{
    ICompetitionRepository _repository;

    private DateTime CLOSING_DATE;
    private string COMPETITION_KEY;
    private Guid CREATED_BY_ID;
    private DateTime CREATED_DATE;
    private string QUESTION;
    private CompetitionStatus OPEN_STATE;
    private CompetitionStatus CLOSED_STATE;

    /// <summary>
    /// Sets up.
    /// </summary>
    [TestInitialize]
    public void SetUp()
    {
        _repository = (ICompetitionRepository)RepositoryFactory.Instance("CompetitionRepository");
        InitialiseParameters();
    }

    /// <summary>
    /// Competitions the crud.
    /// </summary>
    [TestMethod]
    public void CompetitionCrud()
    {
        Guid newID = Create();
        GetByID(newID);
        GetAll();
        Update(newID);
        Delete(newID);
    }

    private void InitialiseParameters()
    {
        CLOSING_DATE = new DateTime(2013, 1, 1);
        COMPETITION_KEY = "SW";
        CREATED_BY_ID = new Guid("05D5FD46-263E-E211-BFBA-1040F3A7A3B1");
        CREATED_DATE = DateTime.Now;
        QUESTION = "Who is Luke Skywalkers father?";
        OPEN_STATE = CompetitionStatus.Open;
        CLOSED_STATE = CompetitionStatus.Closed;
    }

    /// <summary>
    /// Creates this instance.
    /// </summary>
    /// <returns>The id of the new record.</returns>
    private Guid Create()
    {
        // Arrange
        Competition competition = new Competition();
        competition.ClosingDate = CLOSING_DATE;
        competition.CompetitionKey = COMPETITION_KEY;
        competition.CreatedBy = new User() { ID = CREATED_BY_ID };
        competition.CreatedDate = CREATED_DATE;
        competition.Question = QUESTION;

        // Act
        _repository.Add(competition);

        // Assert
        Assert.AreNotEqual(Guid.Empty, competition.ID, "Creating new record does not return id");

        return competition.ID;
    }

    /// <summary>
    /// Updates the specified id.
    /// </summary>
    /// <param name="id">The id.</param>
    private void Update(Guid id)
    {
        // Arrange
        Competition competition = _repository.FindByID(id);
        competition.CompetitionKey = "SWQ";
        competition.SetCompetitionState(new ClosedState());
        competition.CreatedBy = new User() { ID = CREATED_BY_ID };

        // Act
        _repository.Update(competition);

        Competition updatedCompetition = _repository.FindByID(id);

        // Assert
        Assert.AreEqual("SWQ", updatedCompetition.CompetitionKey, "Record is not updated.");
        Assert.AreEqual(CLOSED_STATE, updatedCompetition.State.Status, "Competition status is not updated.");
    }

    /// <summary>
    /// Gets all.
    /// </summary>
    private void GetAll()
    {
        // Act
        IEnumerable<Competition> items = _repository.FindAll();

        // Assert
        Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");
        Assert.AreEqual(4, items.First().PossibleAnswers.Answers.Count());
    }

    /// <summary>
    /// Gets the by ID.
    /// </summary>
    /// <param name="id">The id of the competition.</param>
    private void GetByID(Guid id)
    {
        // Act
        Competition competition = _repository.FindByID(id);

        // Assert
        Assert.IsNotNull(competition, "GetByID returned null.");
        Assert.AreEqual(id, competition.ID);
        Assert.AreEqual(CLOSING_DATE.Date, competition.ClosingDate.Date);
        Assert.AreEqual(COMPETITION_KEY, competition.CompetitionKey);
        Assert.AreEqual(CREATED_BY_ID, competition.CreatedBy.ID);
        Assert.AreEqual(CREATED_DATE.Date, competition.CreatedDate.Date);
        Assert.AreEqual(QUESTION, competition.Question);
        Assert.AreEqual(OPEN_STATE, competition.State.Status);
    }

    /// <summary>
    /// Deletes the specified id.
    /// </summary>
    /// <param name="id">The id.</param>
    private void Delete(Guid id)
    {
        // Arrange
        Competition competition = _repository.FindByID(id);

        // Act
        _repository.Remove(competition);
        competition = _repository.FindByID(id);

        // Assert
        Assert.IsNull(competition, "Record is not deleted.");
    }
}
```

Note how there is one method with the TestMethod attribute and each C, R, U and D method is a private method. These are set out so they read as individual tests but they need to be run in sequence since they must run on any database and should not be reliant on any pre-determined values.

Unit Test sticklers may have noticed that I have used more than one assertion for some of the tests (private methods). I can live with this. Since the assertions are only checking property values and make a sensible logical grouping. Creating a separate test for each property in my opinion would be overkill and would also make my tests slow as it would require more round trips to the database!

Note this important feature of the test:

_repository = (ICompetitionRepository)RepositoryFactory.Instance("CompetitionRepository");

Here is the code for RepositoryFactory:

```cs
/// <summary>
/// Repository factory for integration tests.
/// </summary>
internal class RepositoryFactory
{
    /// <summary>
    /// Gets an instance of the repository.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    /// An instance of the repository under test.
    /// </returns>
    internal static object Instance(string instance)
    {
        string targetAssembly = ConfigurationManager.AppSettings["targetAssembly"];
        return Activator.CreateInstance(targetAssembly, targetAssembly + "." + instance).Unwrap();
    }
}
```

The reason for introducing RepositoryFactory, in that in the event I want to write a new repository implementation I can re-use my tests without having to change them!
SHARE