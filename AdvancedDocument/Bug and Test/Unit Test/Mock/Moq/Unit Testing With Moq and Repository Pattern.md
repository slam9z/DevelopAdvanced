[Unit Testing With Moq and Repository Pattern](http://stackoverflow.com/questions/19766885/unit-testing-with-moq-and-repository-pattern)

	

Mocking is generally used when you need to supply a fake dependency and in this case you appear to be trying to Mock the System Under Test (SUT) which doesn't really make sense - there's literally no point because your test is not actually telling you anything about the behaviour of UserRepository; all you are doing is testing if you setup your Mock correctly which isn't very useful!

The test code you have given seems to indicate that you want to test UserRepository.HolidayEntitlement.

I would be much more inclined to move functions like that out of your Repository class and into a separate business-logic type class. This way you can test the logic of calculating a user's holiday entitlement in total isolation which is a major principle of unit testing.

In order to test that this function does what it's supposed to do (i.e perform a calculation based on properties of a User) you need to ensure that whatever User instance is being operated on within that function is 100% isolated and under your control - either with a Mock or Fake (Stub) instance of User, in this case Mocks are an excellent choice because you only need to implement the parts of the dependency that your SUT is going to need.

So, what you could do is this:

Define an interface for User

```cs
public interface IUser
{
  int BaseHolidayEntitlement{get;set;}
  DateTime EmploymentStartDate {get;set;}
  //other properties for a User here
}
```

Implement this on your User class

```cs
public class User:IUser
{
   //implemement your properties here
   public int BaseHolidayEntitlement{get;set;}
   public DateTime EmploymentStartDate {get;set;}
   //and so on
}
```

Create a class for User logic

```cs
public class UserRules
{
  public int GetHolidayEntitlement(IUser user,DateTime dateTime)
  {
     //perform your logic here and return the result
  }
}
```

Now your test becomes much simpler and doesn't even need the repository

```cs
[TestMethod]
public void GetHolidayEntitlement_WithBase25_Returns25()
{
   //Arrange
   var user = new Mock<IUser>();
   //setup known, controlled property values on the mock:
   user.SetupGet(u=>u.BaseHolidayEntitlement).Returns(25);
   user.SetupGet(u=>u.EmploymentStartDate).Returns(new DateTime(2013,1,1));
   var sut = new UserRules();
   int expected = 25;
   //Act
   int actual = sut.GetHolidayEntitlement(user.Object,DateTime.UtcNow);
   //Assert
   Assert.AreEqual(expected,actual,"GetHolidayEntitlement isn't working right...");
}
```
