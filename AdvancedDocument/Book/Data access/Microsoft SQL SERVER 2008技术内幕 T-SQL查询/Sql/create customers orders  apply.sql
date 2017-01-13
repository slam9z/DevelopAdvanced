USE WebShop

--APPLY

SELECT  C.customerid,C.city ,A.orderid
FROM dbo.Customers AS C
CROSS APPLY
(SELECT TOP (2) O.customerid,O.orderid
FROM dbo.Orders AS O
WHERE o.customerid=c.customerid
ORDER BY orderid DESC
) AS A;


SELECT  C.customerid,C.city ,A.orderid
FROM dbo.Customers AS C
OUTER  APPLY
(SELECT TOP (2) O.customerid,O.orderid
FROM dbo.Orders AS O
WHERE o.customerid=c.customerid
ORDER BY orderid DESC
) AS A;

