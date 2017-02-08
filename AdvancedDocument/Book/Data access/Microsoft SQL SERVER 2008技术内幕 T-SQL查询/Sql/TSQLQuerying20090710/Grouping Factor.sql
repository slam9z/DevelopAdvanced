---------------------------------------------------------------------
-- Grouping Factor
---------------------------------------------------------------------

-- Creating and Populating the Stocks Table
USE tempdb;

IF OBJECT_ID('Stocks') IS NOT NULL DROP TABLE Stocks;

CREATE TABLE dbo.Stocks
(
  dt    DATE NOT NULL PRIMARY KEY,
  price INT  NOT NULL
);
GO

INSERT INTO dbo.Stocks(dt, price) VALUES
  ('20090801', 13),
  ('20090802', 14),
  ('20090803', 17),
  ('20090804', 40),
  ('20090805', 40),
  ('20090806', 52),
  ('20090807', 56),
  ('20090808', 60),
  ('20090809', 70),
  ('20090810', 30),
  ('20090811', 29),
  ('20090812', 29),
  ('20090813', 40),
  ('20090814', 45),
  ('20090815', 60),
  ('20090816', 60),
  ('20090817', 55),
  ('20090818', 60),
  ('20090819', 60),
  ('20090820', 15),
  ('20090821', 20),
  ('20090822', 30),
  ('20090823', 40),
  ('20090824', 20),
  ('20090825', 60),
  ('20090826', 60),
  ('20090827', 70),
  ('20090828', 70),
  ('20090829', 40),
  ('20090830', 30),
  ('20090831', 10);

CREATE UNIQUE INDEX idx_price_dt ON Stocks(price, dt);
GO

-- Ranges where Stock Price was >= 50
SELECT MIN(dt) AS startrange, MAX(dt) AS endrange,
  DATEDIFF(day, MIN(dt), MAX(dt)) + 1 AS numdays,
  MAX(price) AS maxprice
FROM (SELECT dt, price,
        (SELECT MIN(dt)
         FROM dbo.Stocks AS S2
         WHERE S2.dt > S1.dt
          AND price < 50) AS grp
      FROM dbo.Stocks AS S1
      WHERE price >= 50) AS D
GROUP BY grp;

-- Solution using ROW_NUMBER
SELECT MIN(dt) AS startrange, MAX(dt) AS endrange,
  DATEDIFF(day, MIN(dt), MAX(dt)) + 1 AS numdays,
  MAX(price) AS maxprice
FROM (SELECT dt, price,
        DATEADD(day, -1 * ROW_NUMBER() OVER(ORDER BY dt), dt) AS grp
      FROM dbo.Stocks AS S1
      WHERE price >= 50) AS D
GROUP BY grp;
GO