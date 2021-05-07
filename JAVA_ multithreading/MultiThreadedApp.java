package MultiThreading;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class MultiThreadedApp {
    Node root;                                                                  // Başlangıç düğümü
    int maxHeight;                                                              // Kullanıcıya dönecek maksimum yükseklik değeri 
    public MultiThreadedApp(int graph[][]) {                                    // Yapıcı fonksiyon
        root = new Node(0);                                                     // Başlangıç düğümü oluşturur ve idsini 0 yapar
        maxHeight = 0;                                                          // Maksimum yükseklik değerinin başlangıç değeri
        placeChilds(root, graph,0);                                             // Kullanıcıden gelen graf matrisine göre düğümleri oluşturur ve bağları oluşturur
        myThread t1 = new myThread(root);                                       // root düğümü için thread yaratır.
        t1.start();                                                             // root düğümünün threadini başlatır
        try{
            t1.join();                                                          // root düğümünün threadinin işi bitirene kadar bekler.
        }
        catch(InterruptedException e){
            
        }
    }
    public int getMaxHeight(){
        return maxHeight;
    }
    private void placeChilds(Node node,int graph[][],int id){                   // Kullanıcıdan gelen graf matrisine göre düğümleri oluşturup bağlarını yapan rekürsif fonksiyon
        int countOfChlids = 0;                                                  // Düğümün çocuk sayısını tutan değişken
        System.out.println("-----------------------------");
        System.out.println("Current node id: " + id);                            // Şuanki düğümü ekrana yazdır
        for(int i = 0; i < graph.length; i++){
            if(graph[id][i] == 1)                                               // Eğer graf matrisinde şuanki düğümün çocuğu varsa 1 arttır
                countOfChlids++;
        }
        System.out.println("Total child count: " + countOfChlids + " this node's."); // Düğümün toplam çocuk sayısını ekrana yazdır
        if(countOfChlids == 0)                                                  // Eğer  çocuğu yok ise fonksiyondan çık (REKÜRSİF FONKSİYON ÇIKIŞ ŞARTI)
            return;
        else                                                                    // Eğer çocukları varsa
        {
            for(int j = 0; j < graph.length; j++){                              // Matris uzunluğu kadar döngü
                if(graph[id][j] == 1){                                          // Graf matrisinde hangi düğüm (j) hangi düğümün(id) çocuğu ise 
                    node.childs.add(new Node(j));                               // O düğümün çocukları listesine yeni bir düğüm ekle ve yeni oluşan düğümün idsini j yap
                    System.out.println("Current node id: " + id + ", node id: " + j + " add as chlid of this node.");  // Eklemeyi ekrana yazdır
                    placeChilds(node.childs.get(node.childs.size()-1), graph, j);   // Yeni oluşan düğümün çocuklarını yerleştirmek için Rekürsif fonksiyonu tekrar çağır
                }
            }
            System.out.println("-----------------------------");
        }
    }
    
    class Node{                                                                 // Düğüm classı
        int threadId,height;                                                    // Düğüm idsi ve yüksekliği
        List<Node> childs;                                                      // Çocukların listesi
        Node(int id){                                                           // Düğüm yapıcı fonksiyon
            this.threadId = id;                                                 // Düğüm id sini atama
            childs = new ArrayList<>();                                         // Liste oluşturma
            height = 1;                                                         // Yükseklik başlangıç değeri
        }
        private void height_ack(int h){                                         // height_ack fonksiyonu
            if(this.height < h+1){                                                // Eğer düğümün yüksekliği gelen mesajdan küçükse
                this.height = h+1;                                                // Düğümün yüksekliğini gelen mesaj yap
                if(this.height > maxHeight)                                     // Eğer düğümün yüksekliği toplam yükseklikten büyük ise
                    maxHeight = this.height;                                    // toplam yüksekliği düğümün yüksekliği yap
            }
                
        }
    } 
    private class myThread extends Thread{                                      // Thread classı
        Node node;                                                              // Threadin içine aldığı düğüm
        public myThread(Node node){                                             // Thread yapıcı fonksiyon
            this.node = node;
            
        }
        @Override
        public void run(){                                                                      // Thread run fonksiyonu
            System.out.println("Thread id: " + node.threadId + " running...");        // Çalışan threadi ekrana yazdır
            if(node.childs.size() > 0){                                                         // Eğer düğümün çocukları varsa
                myThread threads[] = new myThread[node.childs.size()];                          // Düğümün çocukları kadar yeni Thread dizisi oluştur
                for(int i = 0; i < node.childs.size(); i++){                                    // Düğümün çocukları kadar döngü
                    threads[i] = new myThread(node.childs.get(i));                              // Düğümün her çocuğu için yeni Thread oluştur ve Thread içine Düğümün çocuğunu gönder
                    System.out.println("Thread id: " + node.threadId + " send to this message: Height(" + node.height +") to  Thread id:" + node.childs.get(i).threadId);                  // Mesajı gönderdiğini ekrana yazdır
                    node.childs.get(i).height_ack(node.height);                               // Düğümün döngü içindeki çocuğuna height_ack mesajını gönder
                    threads[i].start();                                                         // Düğümün döngü içindeki çocuğunun Threadini çalıştır
                    while(true){                                                                // Düğümün tüm çocuklarının threadleri bitene kadar dönen sonsuz döngü
                        if(threads[i].isAlive())                                                
                            continue;
                        break;
                    }
                }
            }
            else
            {
                System.out.println("Answer from  Thread id " + node.threadId +" : " + node.height);         // Düğümden gelen cevabı ekrana yazdır
            }
        }
    }
}
