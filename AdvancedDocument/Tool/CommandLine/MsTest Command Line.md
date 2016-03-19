##MSTest

打开Developer Command Prompt就可以使用了

##如何使用Command Line进行编译

###/testcontainer:[file name] 

```
mstest  /testcontainer:AlgorithmTests.dll
DevelopAdvanced\Algorithm\

//不要进入测试目录,方便编译。

mstest /testcontainer:AlgorithmTests\bin\debug\AlgorithmTests.dll

mstest /testcontainer:AlgorithmTests\bin\debug\AlgorithmTests.dll    /test:BreadthFirstSearchTest   /detail:errormessage  /detail:stdout  /detail:errorstacktrace

```  

就会自动测试，这个还比较简单。一般我也只需要这个命令。

这个会一次测试所有的方法。


###/test:[test name]  

指定testcontainer后指定测试方法可以写多次指定多个方法




##help

Usage:                      MSTest.exe [options].

Description:                        Run tests in test files.

Options:

  /help                             Display this usage message.
                                    (Short form: /? or /h)

  /nologo                           Do not display the startup banner and
                                    copyright message.

  /testcontainer:[file name]        Load a file that contains tests. You can
                                    Specify this option more than once to
                                    load multiple test files.
                                    Examples:
                                      /testcontainer:mytestproject.dll
                                      /testcontainer:loadtest1.loadtest

  /maxpriority:[priority]           Only tests whose priority is less than
                                    or equal to this value will be executed.
                                    Example:
                                    /minpriority:0 /maxpriority:2

  /category:[filter]                Use the specified filter to select tests
                                    to run based on the test category of each
                                    test.
                                    You can use the logical operators &
                                    and ! to construct your filter, or you
                                    can use logical operators | and ! to
                                    filter the tests.
                                    Examples:
                                    /category:Priority1
                                       (any tests with category: Priority1)
                                    /category:"Priority1&MyTests"
                                       (tests that must have multiple
                                        categories: Priority1 and MyTests)
                                    /category:"Priority1|MyTests"
                                       (all tests from multiple
                                        categories: Priority 1 or MyTests)
                                    /category:"Priority1&!MyTests"
                                       (filter out tests: Priority1 tests
                                         that do not have a test category
                                         of MyTests)

  /minpriority:[priority]           Only tests whose priority is greater than
                                    or equal to this value will be executed.
                                    Example:
                                    /minpriority:0 /maxpriority:2

  /testmetadata:[file name]         Load a metadata file.
                                    Example:
                                      /testmetadata:testproject1.vsmdi

  /testsettings:[file name]         Use the specified test settings file.
                                    Example:
                                      /testsettings:mysettings.testsettings

  /resultsfile:[file name]          Save the test run results to the specified
                                    file.
                                    Example:
                                      /resultsfile:c:\temp\myresults.trx

  /testlist:[test list path]        The test list, as specified in the metadata
                                    file, to be run. You can specify this
                                    option multiple times to run more than
                                    one test list.
                                    Example:
                                      /testlist:checkintests/clientteam

  /test:[test name]                 The name of a test to be run. You can
                                    specify this option multiple times to run
                                    more than one test.

  /unique                           Run a test only if one unique match is
                                    found for any given /test.

  /usestderr                        Uses standard error to output error
                                    information.  Without this option all
                                    output is sent to standard output.

  /noisolation                      Run tests within the MSTest.exe process.
                                    This choice improves test run speed but
                                    increases risk to the MSTest.exe process.

  /noresults                        Do not save the test results in a TRX file.
                                    This choice improves test run speed but
                                    does not save the test run results.

  /detail:[property id]             The name of a property that you want to
                                    show values for, in addition to the test
                                    outcome. Please examine a test results
                                    file to see the available properties.
                                    Example: /detail:errormessage