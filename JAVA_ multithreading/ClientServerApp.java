package MultiThreading;

public class ClientServerApp{
    private myThread myT1,myT2,myT3;                                    // Threadler
    private int numbers[];                                              // Gelen sayı dizisi
    private int totalResult;                                            // Döndürülecek sonuç
    public ClientServerApp(int numbers[]) {                             // class contructor
        this.numbers = numbers;                                         // Gelen sayı dizisini class değişkenine alma
        this.totalResult = 0;                                           // Dödürülecek sonucun başlangıç değeri
        myT1 = new myThread(numbers[0], numbers[1], numbers[2]);        // Thread 1' oluşturma ve sayıları kendi thread classımıza gönderme
        myT2 = new myThread(numbers[3], numbers[4], numbers[5]);        //              //
        myT3 = new myThread(numbers[6], numbers[7], numbers[8]);        //              //
        myT3.start();                                                   // Thread 1 başlat
        myT2.start();                                                   //         //
        myT1.start();                                                   //         //
        while(true){                                                    // Tüm Threadler bitene kadar bekle
            if(myT3.isAlive() || myT2.isAlive() || myT1.isAlive())                                                
                continue;
            break;
        }
    }
    private void setResult(int sum){
        totalResult += sum;
    }
    public int getResult(){                                             // Sonucu kullanıcıya döndüren fonksiyon
        return totalResult;
    }
    
    private class myThread extends Thread{                              // Thread classı ile extend edilmiş kendi myThread classımız
        int x, y, z;                                                    // class değişkenleri
        public myThread(int x, int y, int z) {                          // class yapıcı fonksiyonu
            this.x = x;
            this.y = y;
            this.z = z;
        }
        @Override
        public void run(){                                                                      // Override edilmiş Thread run fonksiyonu
            //totalResult += x + y + z;                                                         
            System.out.println("Sends " + x + "," + y + "," + z + " to " + this.getName());
            int sum = x + y + z;
            System.out.println(this.getName() + " performs its task and writes the sum " + sum);
            setResult(sum);
        }
    }
}
