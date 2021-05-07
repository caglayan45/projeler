package personel;

public class Departman {

    private int depNo;
    private String depAd;
    public Personel[] personeller;
    public Servis[] servisler;

    Departman(){

    }

    public int GetDepNo() {
        return depNo;
    }

    public void SetDepNo(int depNo) {
        this.depNo = depNo;
    }

    public String GetDepAd() {
        return depAd;
    }

    public void SetDepAd(String depAd) {
        this.depAd = depAd;
    }
}
