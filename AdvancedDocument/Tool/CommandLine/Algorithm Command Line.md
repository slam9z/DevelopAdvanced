## Algorithm Command Line


###Test compile and  run test

csc /target:library Algorithm/Struct/Graph/*.cs  Algorithm/sort/*.cs Algorithm/struct/*.cs  Algorithm/struct/BPlusTree/*.cs Algorithm/struct/BPlusTree/Storage/*.cs  /out:Algorithm/bin/debug/Algorithm.dll

mstest /testcontainer:AlgorithmTests\bin\debug\AlgorithmTests.dll   

mstest /testcontainer:AlgorithmTests\bin\debug\AlgorithmTests.dll     /test:DepthFirstSearchTest   /detail:errormessage  /detail:stdout  /detail:errorstacktrace

mstest /testcontainer:AlgorithmTests\bin\debug\AlgorithmTests.dll     /test:TopologicalSortTest /detail:errormessage  /detail:stdout  /detail:errorstacktrace

csc /target:library Algorithm/Struct/Graph/*.cs  Algorithm/sort/*.cs Algorithm/struct/*.cs  Algorithm/struct/BPlusTree/*.cs Algorithm/struct/BPlusTree/Storage/*.cs  /out:AlgorithmTests/bin/debug/Algorithm.dll

csc /target:library AlgorithmTests/Struct/Graph/*.cs AlgorithmTests/*.cs AlgorithmTests/Struct/BPlusTree/*.cs AlgorithmTests/Sort/*.cs AlgorithmTests/Struct/*.cs  /out:AlgorithmTests/bin/debug/AlgorithmTests.dll /r:AlgorithmTests/bin/debug/Algorithm.dll  /r:AlgorithmTests/bin/debug/Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll

mstest /testcontainer:AlgorithmTests\bin\debug\AlgorithmTests.dll    /test:BreadthFirstSearchTest /test:DepthFirstSearchTest /test:PrintPathTest   /detail:errormessage  /detail:stdout  /detail:errorstacktrace

