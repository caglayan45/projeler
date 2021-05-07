package MultiThreading;
public class test {
    public static void main(String[] args){
        int graph[][] =  {{0,1,1,1,0,0,0,0,0,0},    //  İş parçacığı 0
                          {0,0,0,0,1,1,0,0,0,0},    //  İş parçacığı 1
                          {0,0,0,0,0,0,0,0,0,0},    //  İş parçacığı 2
                          {0,0,0,0,0,0,1,0,0,0},    //  İş parçacığı 3
                          {0,0,0,0,0,0,0,0,0,0},    //  İş parçacığı 4
                          {0,0,0,0,0,0,0,1,0,0},    //  İş parçacığı 5
                          {0,0,0,0,0,0,0,0,1,0},    //  İş parçacığı 6
                          {0,0,0,0,0,0,0,0,0,0},    //  İş parçacığı 7
                          {0,0,0,0,0,0,0,0,0,0}};   //  İş parçacığı 8
        
        MultiThreadedApp mta = new MultiThreadedApp(graph);
        System.out.println("Max Height: " + mta.getMaxHeight());
        System.out.println("\n///////////////////////////////////////////////\n");
        int n[] = {1, 5, 9, 10, 20, 25, 90, 10, 100};
        ClientServerApp csa = new ClientServerApp(n);
        System.out.println("Client receives the sum: " + csa.getResult());
    }
}
