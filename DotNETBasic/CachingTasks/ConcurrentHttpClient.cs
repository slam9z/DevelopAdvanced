using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingTasks
{
	//https://msdn.microsoft.com/en-us/magazine/hh456402.aspx  (修改)
	public class ConcurrentHttpClient
	{
		private static ConcurrentDictionary<string, Task<string>> s_urlToContents = new ConcurrentDictionary<string, Task<string>>();
		//private static Dictionary<string, Task<string>> s_urlToContents = new Dictionary<string, Task<string>>();
		//下载失败会比较麻烦。
		public static Task<string> GetContentsAsync(string url)
		{
			Task<string> contents;
			if (!s_urlToContents.TryGetValue(url, out contents))
			{
				contents = GetContentsInternalAsync(url);
				s_urlToContents.TryAdd(url, contents);


				//s_urlToContents.Add(url, contents);
				//contents.ContinueWith(delegate
				//{
				//	s_urlToContents.TryAdd(url, contents);
				//}, CancellationToken.None,
				//  TaskContinuationOptions.OnlyOnRanToCompletion |
				//   TaskContinuationOptions.ExecuteSynchronously,
				//  TaskScheduler.Default);

				contents.ContinueWith(delegate
				{
					//s_urlToContents.TryRemove(url, out contents);

					//s_urlToContents.Remove(url);
				}, CancellationToken.None,
				   //TaskContinuationOptions.OnlyOnRanToCompletion |
				   TaskContinuationOptions.ExecuteSynchronously,
				  TaskScheduler.Default);
			}
			return contents;
		}


		private static int InvokeCount;
		private static async Task<string> GetContentsInternalAsync(string url)
		{
			var response = await new HttpClient().GetAsync(url);
			Interlocked.Increment(ref InvokeCount);
			Console.WriteLine("GetContentsInternalAsync: {0}, Count:{1} ThreadId: {2}", url, InvokeCount, Thread.CurrentThread.ManagedThreadId);

			return await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
		}

	}
}
