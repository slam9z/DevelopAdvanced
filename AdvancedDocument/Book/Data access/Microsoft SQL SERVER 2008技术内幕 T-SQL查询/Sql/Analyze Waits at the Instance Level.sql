SELECT
  wait_type,
  waiting_tasks_count,
  wait_time_ms,
  max_wait_time_ms,
  signal_wait_time_ms
FROM sys.dm_os_wait_stats
ORDER BY wait_type;

-- Isolate top waits
WITH Waits AS
(
  SELECT
    wait_type,
    wait_time_ms / 1000. AS wait_time_s,
    100. * wait_time_ms / SUM(wait_time_ms) OVER() AS pct,
    ROW_NUMBER() OVER(ORDER BY wait_time_ms DESC) AS rn,
    100. * signal_wait_time_ms / wait_time_ms as signal_pct
  FROM sys.dm_os_wait_stats
  WHERE wait_time_ms > 0
    AND wait_type NOT LIKE N'%SLEEP%'
    AND wait_type NOT LIKE N'%IDLE%'
    AND wait_type NOT LIKE N'%QUEUE%'    
    AND wait_type NOT IN(  N'CLR_AUTO_EVENT'
                         , N'REQUEST_FOR_DEADLOCK_SEARCH'
                         , N'SQLTRACE_BUFFER_FLUSH'
                         /* filter out additional irrelevant waits */ )
)