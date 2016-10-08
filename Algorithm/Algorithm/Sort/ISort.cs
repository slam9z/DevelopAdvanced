using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
	public interface ISort
	{
		IList<T> Sort<T>(IList<T> source) where T : IComparable;

		IList<T> Sort<T>(IList<T> source, Func<T, T, bool> larger);

	}
}
