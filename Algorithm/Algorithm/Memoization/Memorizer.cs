using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Memoization
{
    public class Memorizer<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _mem;
        private Func<TKey, TValue> _function;

        public Memorizer(Func<TKey, TValue> function)
        {
            _function = function;
            _mem = new Dictionary<TKey, TValue>();
        }

        public TValue Invoke(TKey arg)
        {
            if (_mem.ContainsKey(arg))
            {
                return _mem[arg];
            }
            else
            {
                TValue ret = _function(arg);
                _mem[arg] = ret;
                return ret;
            }
        }
    }

    public class FactorialCalculator
    {
        private Memorizer<int, long> _memorizedFactorial;
        public FactorialCalculator()
        {
            _memorizedFactorial = new Memorizer<int, long>(innerFactorial);
        }

        private long innerFactorial(int x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }

        public long Factorial(int x)
        {
            return _memorizedFactorial.Invoke(x);
        }

    }
}
