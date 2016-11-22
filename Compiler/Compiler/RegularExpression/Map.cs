using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace RegularExpression
{
  /// <summary>
  /// this class Map many Value object to one Key object  (one-to-many).
  /// Example:
  /// private Map map = new Map();
  /// map.Add(".txt", "notepad.exe");
  /// map.Add(".txt", "wordpad.exe");
  /// map.Add(".jpg", "ie.exe");
  /// map.Add(".jpg", "paint.exe");
  /// 
  /// NFA.Set setProgram = map[".txt"];   // get value is always a NFA.Set
  /// 
  /// </summary>
  public class Map : Hashtable
  {
    public override void Add(object key, object mapTo)
    {
      Set set = null;
      if (base.Contains(key) == true)
      {
        set = (Set)base[key];
      }
      else
      {
        set = new Set();
      }
      set.AddElement(mapTo);
      base[key] = set;      
    }

    public override object this[object key]
    {
      get
      {
        return base[key];
      }
      set
      {
        //base[key] = value;
        Add(key, value);
      }
    }

    

  }
}
