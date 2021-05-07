package AVL_BST_CSV;

public class AVL<T extends Comparable<T>> {

    class Node<T extends Comparable<T>> {
        T data;
        Node<T> left, right;
        int height;

        public Node(T data){
            this.data = data;
            this.height = 1;
            this.left = null;
            this.right = null;
        }
    }

    private Node<T> root;

    AVL(){
        this.root = null;
    }

    private int Height(Node<T> node){
        return (node == null)? 0 : node.height;
    }

    private int BalanceFactor(Node<T> node){
        return (node == null)? 0 : ((Height(node.left)) - (Height(node.right)));
    }

    private int FindMax(int a, int b){
        return (a > b)? a : b;
    }

    void BFO(){
        System.out.print("Breadth First Order : \n\t");
        for (int i = 1; i <= root.height; i++)
            BFORec(root,i);
        System.out.println("\n");
    }

    private void BFORec(Node<T> node,int height){
        if(node == null)
            return;
        if(height == 1)
            System.out.print(node.data + " : " + node.height + "\t");
        else if(height > 1){
            BFORec(node.left,height-1);
            BFORec(node.right,height-1);
        }
    }

    void DFO(){
        System.out.print("Depth First Order : \n\t");
        System.out.print("InOrder : ");
        InOrder(root);
        System.out.print("\n\tPreOrder : ");
        PreOrder(root);
        System.out.print("\n\tPostOrder : ");
        PostOrder(root);
        System.out.println("\n");

    }

    private void InOrder(Node<T> node)
    {
        if(node == null)
            return;
        InOrder(node.left);
        System.out.print(node.data + " : " + node.height + "\t");
        InOrder(node.right);
    }
    private void PreOrder(Node<T> node)
    {
        if(node == null)
            return;
        System.out.print(node.data + " : " + node.height + "\t");
        PreOrder(node.left);
        PreOrder(node.right);
    }
    private void PostOrder(Node<T> node)
    {
        if(node == null)
            return;
        PostOrder(node.left);
        PostOrder(node.right);
        System.out.print(node.data + " : " + node.height + "\t");
    }

    private Node<T> RotateLeft(Node<T> node){
        Node<T> nodeRight = node.right;
        Node<T> nodeRightLeft = nodeRight.left;

        nodeRight.left = node;
        node.right = nodeRightLeft;

        node.height = FindMax(Height(node.left),Height(node.right)) + 1;
        nodeRight.height = FindMax(Height(nodeRight.left),Height(nodeRight.right)) + 1;
        return nodeRight;
    }

    private Node<T> RotateRight(Node<T> node){
        Node<T> nodeLeft = node.left;
        Node<T> nodeLeftRight = nodeLeft.right;

        nodeLeft.right = node;
        node.left = nodeLeftRight;

        node.height = FindMax(Height(node.left),Height(node.right)) + 1;
        nodeLeft.height = FindMax(Height(nodeLeft.left),Height(nodeLeft.right)) + 1;

        return nodeLeft;
    }

    void Insert(T data){
        root = InsertRec(root,data);
    }

    private Node<T> InsertRec(Node<T> node,T data){
        if(node == null)
            return new Node<T>(data);
        else{
            int comparison = data.compareTo(node.data);
            if(comparison < 1)
                node.left = InsertRec(node.left,data);
            else if (comparison > 0)
                node.right = InsertRec(node.right, data);

            node.height = FindMax(Height(node.left),Height(node.right)) + 1;
            int balanceFactor = BalanceFactor(node);

            if(balanceFactor > 1 && data.compareTo(node.left.data) < 1)
                return RotateRight(node);
            if(balanceFactor < -1 && data.compareTo(node.right.data) > 0)
                return RotateLeft(node);
            if(balanceFactor > 1 && data.compareTo(node.left.data) > 0){
                node.left = RotateLeft(node.left);
                return RotateRight(node);
            }
            if(balanceFactor < -1 && data.compareTo(node.right.data) < 1){
                node.right = RotateRight(node.right);
                return RotateLeft(node);
            }

            return node;
        }
    }

    void Search(T data){
        Node<T> node = root;

        while (node != null){
            int comparison = data.compareTo(node.data);
            if (comparison == 0) {
                System.out.println("Found! : " + node.data + " - " + node.height);
                return;
            } else if (comparison < 0) {
                node = node.left;
            } else {
                node = node.right;
            }
        }
        System.out.println(data + " Could not found!");
    }

    private boolean Find(T data){
        Node<T> node = root;

        while (node != null){
            int comparison = data.compareTo(node.data);
            if (comparison == 0) {
                return true;
            } else if (comparison < 0) {
                node = node.left;
            } else {
                node = node.right;
            }
        }
        return false;
    }

    void Delete(T data){
        if(Find(data))
            System.out.println("Delete Successfully");
        else{
            System.out.println("Could not found node");
            return;
        }

        root = DeleteRec(root,data);
    }

    private Node<T> MinValueNode(Node<T> node)
    {
        Node<T> current = node;
        while (current.left != null)
            current = current.left;
        return current;
    }

    private Node<T> DeleteRec(Node<T> node, T data)
    {
        if (node == null)
            return node;

        int comparison = data.compareTo(node.data);
        if (comparison < 0)
            node.left = DeleteRec(node.left, data);
        else if (comparison > 0)
            node.right = DeleteRec(node.right, data);
        else
        {
            if ((node.left == null) || (node.right == null))
            {
                Node<T> temp = null;
                if (temp == node.left)
                    temp = node.right;
                else
                    temp = node.left;

                if (temp == null)
                {
                    temp = node;
                    node = null;
                }
                else
                    node = temp;
            }
            else
            {
                Node<T> temp = MinValueNode(node.right);
                node.data = temp.data;
                node.right = DeleteRec(node.right, temp.data);
            }
        }

        if (node == null)
            return node;

        node.height = FindMax(Height(node.left), Height(node.right)) + 1;

        int balance = BalanceFactor(node);
        if (balance > 1 && BalanceFactor(node.left) >= 0)
            return RotateRight(node);

        if (balance > 1 && BalanceFactor(node.left) < 0)
        {
            node.left = RotateLeft(node.left);
            return RotateRight(node);
        }

        if (balance < -1 && BalanceFactor(node.right) <= 0)
            return RotateLeft(node);

        if (balance < -1 && BalanceFactor(node.right) > 0)
        {
            node.right = RotateRight(node.right);
            return RotateLeft(node);
        }

        return node;
    }

}
