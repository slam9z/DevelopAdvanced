---------------------------------------------------------------------
-- Histograms
---------------------------------------------------------------------
USE InsideTSQL2008;

-- Code Returning Histogram Steps Table
DECLARE @numsteps AS INT;
SET @numsteps = 3;

SELECT n AS step,
  mn + (n - 1) * stepsize AS lb,
  mn + n * stepsize AS hb
FROM dbo.Nums
  CROSS JOIN 
    (SELECT MIN(val) AS mn,
       ((1E0*MAX(val) + 0.0000000001) - MIN(val))
       / @numsteps AS stepsize
     FROM Sales.OrderValues) AS D
WHERE n < = @numsteps;
GO

-- Creation Script for HistSteps Function
IF OBJECT_ID('dbo.HistSteps') IS NOT NULL
  DROP FUNCTION dbo.HistSteps;
GO
CREATE FUNCTION dbo.HistSteps(@numsteps AS INT) RETURNS TABLE
AS
RETURN
  SELECT n AS step,
    mn + (n - 1) * stepsize AS lb,
    mn + n * stepsize AS hb
  FROM dbo.Nums
    CROSS JOIN
      (SELECT MIN(val) AS mn,
         ((1E0*MAX(val) + 0.0000000001) - MIN(val))
         / @numsteps AS stepsize
       FROM Sales.OrderValues) AS D
  WHERE n < = @numsteps;
GO

-- Test Function
SELECT * FROM dbo.HistSteps(3) AS S;
GO

-- Returning Histogram with 3 Steps
SELECT step, COUNT(*) AS numorders
FROM dbo.HistSteps(3) AS S
  JOIN Sales.OrderValues AS O
    ON val >= lb AND val < hb
GROUP BY step;