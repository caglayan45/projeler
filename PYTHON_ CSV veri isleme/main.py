#Çağlayan SANCAKTAR
import pandas as pan, matplotlib.pyplot as plt

veri = pan.read_csv("top50.csv")

gruplanmis_veri = []
grup_isimleri = []
danceability = []
sarki_IDler = []
enerjiler = []
enerjiOrtalamalari = []
grup_numaralari = []

for item in veri.values:
    sarki_IDler.append(item[0])
    danceability.append(item[6])
    if item[3] in grup_isimleri:
        index = grup_isimleri.index(item[3])
        gruplanmis_veri[index].append(item)
        enerjiler[index].append(item[5])
        pass
    else:
        gruplanmis_veri.append([item[3], item])
        grup_isimleri.append(item[3])
        enerjiler.append([item[5]])
        pass

for index, item in enumerate(gruplanmis_veri):
    baslik = item[0]
    print("##", index+1, "-", baslik, " : ")
    for i in item:
        if str(i) != baslik:
            tempVeri = ""
            for j in i:
                if j != i[len(i)-1]:
                    tempVeri += str(j) + ", "
                    pass
                else:
                    tempVeri += str(j)
                    pass
                pass
            print("\t", tempVeri)

            pass
        pass
    print("")
    pass

for i, v in enumerate(enerjiler):
    toplam = 0
    for j in v:
        toplam += j
        pass
    enerjiOrtalamalari.append(toplam/len(v))
    grup_numaralari.append((i+1))
    pass

plt.plot(grup_numaralari, enerjiOrtalamalari)
plt.xlabel("Grup Numarası")
plt.ylabel("Enerji Ortalaması")
plt.title("Enerji Grafiği")
plt.show()

plt.plot(sarki_IDler, danceability)
plt.xlabel("Şarkı ID")
plt.ylabel("Danceability")
plt.title("Danceability Grafiği")
plt.show()