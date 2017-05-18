using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Prototype.Models
{
    public class Grouping<TK, T> : ObservableCollection<T>
    {
        public TK Key { get; private set; }

        public Grouping(TK key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
