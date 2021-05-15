
dosya = open("metin.txt", "r")

veri = dosya.read()
temiz_veri = ""

for i in range(len(veri)):
    if veri[i].isalpha() or veri[i] == '\n':
        temiz_veri += veri[i]

dosya.close()

print(temiz_veri)

dosya = open("temiz_veri.txt", "w")
dosya.write(temiz_veri)
dosya.close()

