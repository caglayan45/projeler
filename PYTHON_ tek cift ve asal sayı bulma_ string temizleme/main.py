#Çağlayan SANCAKTAR

#1. soru
print("Sayı giriniz : ")
sayi = int(input())
if sayi % 2 == 0:
    print("Sayı çift")
else:
    print("Sayı tek")
#1. soru

#2. soru
print("\nArd arda 5 adet sayı girilecek;")
sayilar = []
for i in range(5):
    print(i+1,". sayıyı giriniz : ")
    sayi = int(input())
    sayilar.append(sayi)

kontrol = 0
for i in range(5):
    if sayilar[i] < 2:
        print(i + 1, ". sayı(", sayilar[i], ") asal değildir.")
        continue

    for j in range(2, sayilar[i]):
        if sayilar[i] % j == 0:
            print(i+1, ". sayı(", sayilar[i], ") asal değildir.")
            kontrol = 1
            break

    if kontrol == 0:
        print(i + 1, ". sayı(", sayilar[i], ") asaldır.")
    kontrol = 0
#2. soru


#3. soru
def TemizVeri(s1, s2, s3):
    temizVeri = ""
    for i in range(len(s1)):
        if not s1[i].isdigit():
            temizVeri += s1[i]
    temizVeri += "-"

    for i in range(len(s2)):
        if not s2[i].isdigit():
            temizVeri += s2[i]
    temizVeri += "-"

    for i in range(len(s3)):
        if not s3[i].isdigit():
            temizVeri += s3[i]

    return temizVeri

print("\n", TemizVeri("Ah5me4t", "M9eHm4eT", "Ha3K5a1n"))
#3. soru