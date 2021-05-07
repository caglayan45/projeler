package personel;

public class Servis {

    static int sayac = 1;
    private int depNo,servisID;
    private String servisGuzergah;

    Servis(){
        this.servisID = sayac++;
    }

    public int GetDepNo() {
        return depNo;
    }

    public void SetDepNo(int depNo) {
        this.depNo = depNo;
    }

    public int GetServisID() {
        return servisID;
    }

    public void SetServisID(int servisID) {
        this.servisID = servisID;
    }

    public String GetServisGuzergah() {
        return servisGuzergah;
    }

    public void SetServisGuzergah(String servisGuzergah) {
        this.servisGuzergah = servisGuzergah;
    }
}
