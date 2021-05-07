package AVL_BST_CSV;

public class BST {

    class Node {

        String data;
        Node left, right;
        int depth;

        public Node(String data){
            this.data = data;
            this.depth = 0;
            this.left = this.right = null;
        }
    }

    Node root,maxNode,minNode;
    int depth = 0, sayac = 0;

    BST()
    {
        root = maxNode = minNode = null;
    }

    Node GetMax(){
        GetMax1(root);
        return maxNode;
    }

    private void GetMax1(Node node){
        while (node.right != null)
            node = node.right;
        if(node != null)
            maxNode = node;
    }

    Node GetMin(){
        GetMin1(root);
        return minNode;
    }

    private void GetMin1(Node node){
        while (node.left != null)
            node = node.left;
        if(node != null)
            minNode = node;
    }

    void Insert(String data)
    {
        this.depth = 0;
        root = InsertRec(root, data);
    }

    private Node InsertRec(Node node, String data)
    {
        if (node == null)
        {
            node = new Node(data);
            node.depth = this.depth;
            return node;
        }
        this.depth++;

        if (data.compareTo(node.data) < 1)
            node.left = InsertRec(node.left, data);
        else if (data.compareTo(node.data) > 0)
            node.right = InsertRec(node.right, data);

        return node;
    }

    void Search(String data)
    {
        long startTime = System.currentTimeMillis();
        Node node = SearchRec(root, data);
        long endTime = System.currentTimeMillis();
        long elapsedTime = endTime - startTime;
        if(node != null){
            System.out.println("---------------\nNode : " + node.data);
            System.out.println("Depth : " + node.depth);
            if(node.left == null && node.right == null)
                System.out.println("Leaf node");
            else
                System.out.println("Not Leaf node");
            sayac = 0;
            System.out.println("SubTree Size : " + FindSubTreeSize(node));
            System.out.println("Search time(ms) : " + elapsedTime + "\n---------------");
        }
        else
            System.out.println("Node could not find.");
    }

    int FindSubTreeSize(Node node){
        if (node == null)
            return sayac;
        sayac++;
        FindSubTreeSize(node.left);
        FindSubTreeSize(node.right);
        return sayac;
    }

    private Node SearchRec(Node node, String data)
    {
        if (node == null)
            return node;

        if (data.compareTo(node.data) < 0)
            return SearchRec(node.left, data);
        else if (data.compareTo(node.data) > 0)
            return  SearchRec(node.right, data);
        else
            return node;
    }

    void InOrder()
    {
        InOrderRec(root);
    }

    private void InOrderRec(Node node)
    {
        if(node == null)
            return;
        InOrderRec(node.left);
        System.out.println(node.data);
        InOrderRec(node.right);
    }

}
