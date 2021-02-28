﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace ToB.PriorityToDo.RBT
{
    public sealed partial class RedBlackTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            return ((IBinarySearchTree<T>)this).InOrderIterator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IBinarySearchTree<T>)this).InOrderIterator.GetEnumerator();
        }
    }
}
