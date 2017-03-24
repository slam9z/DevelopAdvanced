[不能将显式值插入时间戳列。请对列列表使用 INSERT 来排除时间戳列，或将 DEFAULT 插入时间戳列](http://blog.sina.com.cn/s/blog_7109e92a0100tf7k.html)


ql   server中的TimeStamp是一种二进制的数据，不是时间格式
timestamp   这种数据类型表现自动生成的二进制数，确保这些数在数据库中是唯一的。timestamp   一般用作给表行加版本戳的机制。存储大小为   8   字节。

Transact-SQL   timestamp   数据类型与在   SQL-92   标准中定义的   timestamp   数据类型不同。SQL-92   timestamp   数据类型等价于   Transact-SQL   datetime   数据类型。  

Microsoft&reg;   SQL   Server&#8482;   将来的版本可能会修改   Transact-SQL   timestamp   数据类型的行为，使它与在标准中定义的行为一致。到那时，当前的   timestamp   数据类型将用   rowversion   数据类型替换。

timestamp timestamp 这种数据类型表现自动生成的二进制数，确保这些数在数据库中是唯一的。timestamp 一般用作给表行加版本戳的机制。存储大小为 8 字节。 注释 Transact-SQL timestamp 数据类型与在 SQL-92 标准中定义的 timestamp 数据类型不同。SQL-92 timestamp 数据类型等价于 Transact-SQL datetime 数据类型。 Microsoft® SQL Server™ 将来的版本可能会修改 Transact-SQL timestamp 数据类型的行为，使它与在标准中定义的行为一致。到那时，当前的 timestamp 数据类型将用 rowversion 数据类型替换。 Microsoft® SQL Server™ 2000 引入了 timestamp 数据类型的 rowversion 同义词。在 DDL 语句中尽可能使用 rowversion 而不使用 timestamp。rowversion 受数据类型同义词行为的制约。有关更多信息，请参见数据类型同义词。 在 CREATE TABLE 或 ALTER TABLE 语句中，不必为 timestamp 数据类型提供列名： CREATE TABLE ExampleTable (PriKey int PRIMARY KEY, timestamp) 如果没有提供列名，SQL Server 将生成 timestamp 的列名。rowversion 数据类型同义词不具有这样的行为。指定 rowversion 时必须提供列名。 一个表只能有一个 timestamp 列。每次插入或更新包含 timestamp 列的行时，timestamp 列中的值均会更新。这一属性使 timestamp 列不适合作为键使用，尤其是不能作为主键使用。对行的任何更新都会更改 timestamp 值，从而更改键值。如果该列属于主键，那么旧的键值将无效，进而引用该旧值的外键也将不再有效。如果该表在动态游标中引用，则所有更新均会更改游标中行的位置。如果该列属于索引键，则对数据行的所有更新还将导致索引更新。 不可为空的 timestamp 列在语义上等价于 binary(8) 列。可为空的 timestamp 列在语义上等价于 varbinary(8) 列。 