namespace ToB.PriorityToDo
{
    public sealed partial class RedBlackTree<T>
    {
        private sealed class Node
        {
            private bool isBlack;
            
            public T Value { get; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public bool Black
            {
                get => isBlack;
                set => isBlack = this == Nil || value;
            }

            public bool Red
            {
                get => !isBlack;
                set => isBlack = this == Nil || value;
            }

            public Node Grandparent => Parent.Parent;

            public Node Uncle =>
                Parent.IsLeftChild
                    ? Grandparent.Right
                    : Grandparent.Left;

            public Node Sibling => IsLeftChild ? Parent.Right : Parent.Left;

            public bool IsLeftChild => Parent != Nil && Parent.Left == this;
            public bool IsRightChild => Parent != Nil && Parent.Right == this;

            public Node(bool black)
            {
                Black = black;
            }

            public Node(T value, Node parent, bool black = false) : this(black)
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