---------------------------------------------------------------------
-- Paging
---------------------------------------------------------------------

---------------------------------------------------------------------
-- Ad-hoc
---------------------------------------------------------------------

-- Second Page of Sales based on qty, empid Order
-- with a page size of 5 rows
DECLARE @pagesize AS INT, @pagenum AS INT;
SET @pagesize = 5;
SET @pagenum = 2;

WITH SalesRN AS
(
  SELECT ROW_NUMBER() OVER(ORDER BY qty, empid) AS rownum,
    empid, mgrid, qty
  FROM dbo.Sales
)
SELECT rownum, empid, mgrid, qty
FROM SalesRN
WHERE rownum > @pagesize * (@pagenum-1)
  AND rownum <= @pagesize * @pagenum
ORDER BY rownum;
GO