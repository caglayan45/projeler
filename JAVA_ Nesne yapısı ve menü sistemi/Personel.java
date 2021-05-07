package personel;

public class Personel {

    static int sayac = 1;
    private int depNo,perYas,perID;
    private String perAd,perSoyad;

    Personel(){
        this.perID = sayac++;
    }

    public int GetDepNo() {
        return depNo;
    }

    public void SetDepNo(int depNo) {
        this.depNo = depNo;
    }

    public int GetPerID() {
        return perID;
    }

    public void SetPerID(int perID) {
        this.perID = perID;
    }

    public String GetPerAd() {
        return perAd;
    }

    public void SetPerAd(String perAd) {
        this.perAd = perAd;
    }

    public int GetPerYas() {
        return perYas;
    }

    public void SetPerYas(int perYas) {
        this.perYas = perYas;
    }

    public String GetPerSoyad() {
        return perSoyad;
    }

    public void SetPerSoyad(String perSoyad) {
        this.perSoyad = perSoyad;
    }
}
