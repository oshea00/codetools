﻿using System;
using System.Collections.Generic;

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    Func<TValue> _init;
    public DefaultDictionary(Func<TValue> init)
    {
        _init = init;
    }
    public new TValue this[TKey k]
    {
        get
        {
            if (!ContainsKey(k))
                Add(k, _init());
            return base[k];
        }
        set => base[k] = value;
    }
}