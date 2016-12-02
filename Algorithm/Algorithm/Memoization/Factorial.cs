using System;
using System.Diagnostics;
using System.Collections.Generic;


public class Script
{
    private Dictionary<int, int> Memo = new Dictionary<int, int>();
    public int Factorial(int n)
    {
        if (n == 0)
        {
            return 1;
        }
        else if (Memo.ContainsKey(n))
        {
            return Memo[n];
        }

        var x = Factorial(n - 1)*n;
        Memo[n] = x;

        return x;
    }



}