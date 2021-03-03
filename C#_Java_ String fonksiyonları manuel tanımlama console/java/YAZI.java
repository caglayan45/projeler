package StringManuelFonksiyon;

public class YAZI {

    private String veri;

    YAZI(String veri){
        this.veri = veri;
    }

    String ParcaAl(int a, int b){
        if(b < a){
            System.out.println("2. parametre 1.den büyük olmalı");
            return "";
        }
        if(b > veri.length()){
            System.out.println("Geçerli parametreler yolladığınızdan emin olun.");
            return "";
        }
        String parca = "";
        for (int i = a; i < b; i++) 
            parca += veri.toCharArray()[i];
        return parca;
    }

    int IndexBul(String a){
        for (int i = 0; i < veri.length(); i++) {
            if(i+a.length() <= veri.length()){
                if(a.equals(ParcaAl(i,i+a.length())))
                    return i;
            }
        }
        return -1;
    }

    String Degistir(String a, String b){
        String yeniVeri = "";
        int index = IndexBul(a),j = 0;
        if(index == -1) {
            System.out.println("Birinci parametre bulunamadı.");
            return "";
        }
        String bas = ParcaAl(0,index), son = ParcaAl(index+a.length(),veri.length());
        yeniVeri = bas + b + son;
        return yeniVeri;
    }

    static String Birlestir(char a, String[] b){
        String birlesmisVeri = "";
        for (int i = 0; i < b.length; i++) {
            if(i == b.length-1){
                birlesmisVeri += b[i];
                break;
            }
            birlesmisVeri += (b[i] + a);
        }
        return birlesmisVeri;
    }

}
