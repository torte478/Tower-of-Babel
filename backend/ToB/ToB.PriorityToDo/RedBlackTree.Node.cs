namespace ToB.PriorityToDo
{
    public sealed partial class RedBlackTree<T>
    {
        public sealed class Node
        {
            private bool isBlack;
            
            public T Value { get; internal set; }
            public Node Parent { get; internal set; }
            public Node Left { get; internal set; }
            public Node Right { get; internal set; }

            public bool Black
            {
                get => isBlack;
                internal set => isBlack = this == Nil || value;
            }

            public bool Red
            {
                get => !isBlack;
                internal set => isBlack = this == Nil || !value;
            }

            public Node Grandparent => Parent.Parent;

            public Node Uncle =>
                Parent.IsLeftChild
                    ? Grandparent.Right
                    : Grandparent.Left;

            public Node Sibling => IsLeftChild ? Parent.Right : Parent.Left;

            public bool IsLeftChild => Parent != Nil && Parent.Left == this;
            public bool IsRightChild => Parent != Nil && Parent.Right == this;

            internal Node(bool black)
            {
                Black = black;
            }

            internal Node(T value, Node parent, bool black = false) : this(black)
            {
                Value = value;
                Parent = parent;
                Left = Nil;
                Right = Nil;
            }

            public override string ToString()
            {
                return $"{Left.ToInnerString()} <- {ToInnerString()} -> {Right.ToInnerString()}";
            }

            private string ToInnerString()
            {
                var value = this == Nil ? "NIL" : Value.ToString();
                var color = Black ? "Black" : "Red";
                return $"({value}, {color})";
            }
        }
    }
}