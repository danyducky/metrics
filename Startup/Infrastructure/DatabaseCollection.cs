using System;
using System.Collections;
using System.Collections.Generic;

namespace Startup.Infrastructure
{
    public class DatabaseCollection : IEnumerable
    {
        private readonly IEnumerable<(Type context, Type implementation)> _items;

        public DatabaseCollection(IEnumerable<(Type context, Type implementation)> items)
        {
            _items = items;
        }
        
        public IEnumerator GetEnumerator() => _items.GetEnumerator();
    }
}