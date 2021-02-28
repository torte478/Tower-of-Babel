namespace ToB.PriorityToDo
{
    public sealed partial class RedBlackTree<T>
    {
        private sealed class Node
        {
            public T Value { get; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            
            public bool Black { get; set; }

            public bool Red
            {
                get => !Black;
                set => Black = !value;
            }

            public Node(bool black)
            {
                Black = black;
            }

            public Node(T value, Node parent, bool black = false) : this(black)
            {
                Value = value;
                Parent = parent;
                Red = true;
                Left = Nil;
                Right = Nil;
            }
        }
    }
}