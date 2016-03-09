using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventWaitHandleDemo
{
	class Program
	{
		static EventWaitHandle eHandle;

		static void UnblockDemo()
		{
			Console.WriteLine("测试EventWaitHandle的初始终止状态");
			eHandle = new EventWaitHandle(true, EventResetMode.AutoReset);//eHandle初始为终止状态，模式为AutoReset
			eHandle.WaitOne();//由于EventWaitHandle对象eHandle初始状态为终止状态，所以这里第一次调用WaitOne时阻塞被立即释放，又由于eHandle为AutoReset模式，所以之后eHandle会被置为非终止状态
			Console.WriteLine("线程未被阻塞");
			eHandle.WaitOne();//由于此时eHandle已经为非终止状态，所以此时调用WaitOne线程会被阻塞
			Console.WriteLine("线程被阻塞");
		}

		static void BlockDemo()
		{
			Console.WriteLine("测试EventWaitHandle的初始非终止状态");
			eHandle = new EventWaitHandle(false, EventResetMode.AutoReset);//eHandle初始为非终止状态，模式为AutoReset
			eHandle.WaitOne();//由于EventWaitHandle对象eHandle初始状态为非终止状态，所以这里第一次调用WaitOne时，线程就被组塞了
			Console.WriteLine("线程被阻塞");
		}

		static void AutoResetDemo()
		{
			Console.WriteLine("测试EventWaitHandle的AutoReset模式");
			eHandle = new EventWaitHandle(false, EventResetMode.AutoReset);//eHandle初始为非终止状态，模式为AutoReset
			ThreadPool.QueueUserWorkItem(new WaitCallback((object o) =>
			{
				//启动另一个线程，每隔3秒钟调用一次eHandle.Set方法，为主线程释放一次阻塞，一共释放3次
				for (int i = 0; i < 3; i++)
				{
					Thread.Sleep(3000);
					eHandle.Set();//由于eHandle处于AutoReset模式，所以每次使用Set将eHandle置为终止状态后，待被WaitOne阻塞的线程被释放后，eHandle又会被自动置回非终止状态
				}
			}), null);

			eHandle.WaitOne();//线程第一次被WaitOne阻塞
			Console.WriteLine("第一次WaitOne调用阻塞已被释放，3秒后第二次WaitOne调用的阻塞会被释放");
			eHandle.WaitOne();//线程第二次被WaitOne阻塞
			Console.WriteLine("第二次WaitOne调用阻塞已被释放，3秒后第三次WaitOne调用的阻塞会被释放");
			eHandle.WaitOne();//线程第三次被WaitOne阻塞
			Console.WriteLine("第三次WaitOne调用阻塞已被释放，所有WaitOne调用的阻塞都已被释放");
		}

		static void ManualResetDemo()
		{
			Console.WriteLine("测试EventWaitHandle的ManualReset模式");
			eHandle = new EventWaitHandle(false, EventResetMode.ManualReset);//eHandle初始为非终止状态，模式为ManualReset
			ThreadPool.QueueUserWorkItem(new WaitCallback((object o) =>
			{
				//启动另一线程，3秒后调用一次eHandle.Set方法，为主线程释放WaitOne阻塞
				Thread.Sleep(3000);
				eHandle.Set();//由于eHandle处于ManualReset模式，所以一旦使用Set将eHandle置为终止状态后，在eHandle的Reset被调用前eHandle会一直处于终止状态，在eHandle调用Reset前，所有被WaitOne阻塞的线程会立即得到释放
			}), null);

			eHandle.WaitOne();//线程第一次被WaitOne阻塞
			Console.WriteLine("第一次WaitOne调用阻塞已被释放，第二次WaitOne调用的阻塞会被立即释放");
			eHandle.WaitOne();//线程第二次被WaitOne阻塞
			Console.WriteLine("第二次WaitOne调用阻塞已被释放，第三次WaitOne调用的阻塞会被立即释放");
			eHandle.WaitOne();//线程第三次被WaitOne阻塞
			Console.WriteLine("第三次WaitOne调用阻塞已被释放，所有WaitOne调用的阻塞都已被释放");

			eHandle.Reset();//调用eHandle的Reset方法，将eHandle手动置回非终止状态，之后再调用WaitOne方法就会被阻塞了
			eHandle.WaitOne();//线程第四次被WaitOne阻塞
			Console.WriteLine("第四次WaitOne调用阻塞已被释放");
		}

		static void Main(string[] args)
		{
			Console.Write("你想测试哪一个方法1=UnblockDemo,2=BlockDemo,3=AutoResetDemo,4=ManualResetDemo:");
			switch (Console.ReadLine())
			{
				case "1":
					UnblockDemo();
					break;
				case "2":
					BlockDemo();
					break;
				case "3":
					AutoResetDemo();
					break;
				case "4":
					ManualResetDemo();
					break;
				default:
					break;
			}
		}
	}
}