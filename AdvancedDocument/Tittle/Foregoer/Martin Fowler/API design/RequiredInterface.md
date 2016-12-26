[RequiredInterface](http://martinfowler.com/bliki/RequiredInterface.html)

A required interface is an interface that is defined by the client of an interaction that specifies what a supplier component needs to do so that it can be used in that interaction.

A good example of required interface is an interface commonly referred to as “comparable”. Such an interface is usually required by a sort function. Imagine I have a set of albums, and I want to sort them by title, but ignoring articles such as "The", "A", and "An". I can arrange them to be sorted in this way by implementing the required interface for any sort functions.

In Java it would look something like this.

class Album...

```java
  public class Album implements Comparable<Album> {
    private String title;
  
    public Album(String title) {
      this.title = title;
    }
    public String getTitle() {
      return title;
    }
  
    @Override
    public int compareTo(Album o) {
      return this.sortKey().compareTo(o.sortKey());
    }
    private String sortKey() {
      return ignoreSortPrefixes(title).toLowerCase();
    }
    private static String ignoreSortPrefixes(String arg) {
      final String[] prefixes = {"an", "a", "the"};
      return Arrays.stream(prefixes)
              .map(s -> s + " ")
              .filter(s -> arg.toLowerCase().startsWith(s))
              .findFirst()
              .map(s -> arg.substring(s.length(), arg.length()))
              .orElse(arg)
              ;
    }
```

In this case Comparable is the required interface of the various Java sort functions. More complicated examples can have a richer interface with several methods defined on it.

Often people think about interfaces as a decision by the supplier about what to expose to clients. But required interfaces are specified (and often defined) by the client. You often get more useful interfaces by thinking about what clients require - leading towards thinking about RoleInterfaces.
Using an Adapter

A common problem comes up if I want to plug together two modules that have been defined independently. Here we can run into difficulties even if we get names that match.

Consider a task list with a required interface of tasks.

```java
class TaskList...

  private List<Task> tasks;
  private LocalDate deadline;
  public LocalDate latestStart() {
    return deadline.minusDays(tasks.stream().mapToInt(t -> t.shortestLength()).sum());
  }
}
```

interface Task…

```java
  int shortestLength();
```

Let's imagine I want to integrate it with an Activity class I got from a different supplier.

class Activity…

```java
  public int shortestLength() {
    …
```

Even though the activity has a method whose signature happens to match the required interface's, I (rightly) can't create a task list of activities because the type definitions don't match. If I can't modify the activity class I need to use an adapter.

```java
public class ActivityAdapter implements Task {
  private Activity activity;

  public ActivityAdapter(Activity activity) {
    this.activity = activity;
  }
  @Override
  public int shortestLength() {
    return activity.shortestLength();
  }
}
```

In the software world we use the term adapter pretty freely, but here I'm using strictly in the sense of the Gang of Four book. In this usage an adapter is an object that maps one object to the required interface of another.

In this case, I don't need an adapter if I'm using a dynamic language, but I do if the activity class used a method with a different signature.
Acknowledgements
Alexander Zagniotov and Bruno Trecenti commented on drafts of this post. 