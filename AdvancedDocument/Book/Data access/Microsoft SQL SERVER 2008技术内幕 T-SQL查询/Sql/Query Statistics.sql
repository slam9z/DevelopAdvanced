-- Query Statistics
SELECT TOP (5)
  MAX(query) AS sample_query,
  SUM(execution_count) AS cnt,
  SUM(total_worker_time) AS cpu,
  SUM(total_physical_reads) AS reads,
  SUM(total_logical_reads) AS logical_reads,
  SUM(total_elapsed_time) AS duration
FROM (SELECT 
        QS.*,
        SUBSTRING(ST.text, (QS.statement_start_offset/2) + 1,
           ((CASE statement_end_offset 
              WHEN -1 THEN DATALENGTH(ST.text)
              ELSE QS.statement_end_offset END 
                  - QS.statement_start_offset)/2) + 1
        ) AS query
      FROM sys.dm_exec_query_stats AS QS
        CROSS APPLY sys.dm_exec_sql_text(QS.sql_handle) AS ST
        CROSS APPLY sys.dm_exec_plan_attributes(QS.plan_handle) AS PA
      WHERE PA.attribute = 'dbid'
        AND PA.value = DB_ID('Performance')) AS D
GROUP BY query_hash
ORDER BY duration DESC;
