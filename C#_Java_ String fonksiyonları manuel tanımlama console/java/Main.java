package StringManuelFonksiyon;

public class Main {

    public static void main(String[] args) {
        YAZI metin = new YAZI("Merhaba galatasaray fenerbahçe.");
        System.out.println(metin.ParcaAl(8,19));
        System.out.println(metin.IndexBul("gal"));
        System.out.println(metin.Degistir("fenerbahçe","beşiktaş"));
        String[] dizi = {"A","B","C","D"};
        System.out.println(YAZI.Birlestir(' ',dizi));
    }

}
