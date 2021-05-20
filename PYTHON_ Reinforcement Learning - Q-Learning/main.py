import pygame, random, matplotlib, pylab, time
matplotlib.use("Agg")
import matplotlib.backends.backend_agg as agg

dosya = None
baslangicKonum = None
bitisKonum = None

WIN_WIDTH = 1400
WIN_HEIGHT = 900

SQUARE_COUNT = 50
SQUARE_SIZE = 18

WHITE = (255, 255, 255)
RED = (255, 0, 0)
GREEN = (0, 255, 0)
BLUE = (0, 0, 255)
BLACK = (0, 0, 0)

ogrenmeKatsayisi = 0.05
indirimKatsayisi = 0.9
hamleSayisi = 0

suankiSkor = 0.0
oncekiSkor = 0.0
ortalamaSkorlar = [0]

EVE = pygame.image.load("EVE.png")
EVE_ID = -1

Engeller = []

WIN = pygame.display.set_mode((WIN_WIDTH, WIN_HEIGHT))
WIN.fill(WHITE)
pygame.display.set_caption("Oyun Ekranı")

FPS = 60

fig = pylab.figure(figsize=[5, 5], dpi=100,)


QTable = [[0]*8 for n in range(SQUARE_COUNT*SQUARE_COUNT)]

for i in range(0, SQUARE_COUNT*(SQUARE_COUNT-1)+1, SQUARE_COUNT):
    QTable[i][7] = -50
    QTable[i][0] = -50
    QTable[i][1] = -50

for i in range(SQUARE_COUNT*(SQUARE_COUNT-1), SQUARE_COUNT*(SQUARE_COUNT)):
    QTable[i][1] = -50
    QTable[i][2] = -50
    QTable[i][3] = -50

for i in range(SQUARE_COUNT-1, SQUARE_COUNT*(SQUARE_COUNT), SQUARE_COUNT):
    QTable[i][3] = -50
    QTable[i][4] = -50
    QTable[i][5] = -50

for i in range(0, SQUARE_COUNT):
    QTable[i][5] = -50
    QTable[i][6] = -50
    QTable[i][7] = -50

def main():
    #deneme()
    global baslangicKonum, bitisKonum, Engeller, hamleSayisi
    run = True
    clock = pygame.time.Clock()

    HaritaCiz()
    dragging = False
    mouse_type = 0
    baslangicKonum = -1
    bitisKonum = -1

    RandomEngelOlustur()

    txtKaydetBTN = pygame.image.load("kaydetButon.png")
    baslatBTN = pygame.image.load("baslatButon.png")

    WIN.blit(txtKaydetBTN, (970, 830))
    WIN.blit(baslatBTN, (1170, 830))
    pygame.display.update()

    while run:
        clock.tick(FPS)

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                run = False
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_s:
                    OgrenmeyeBasla()
            if event.type == pygame.MOUSEBUTTONDOWN:
                dragging = True
                mouse_type = event.button

                if mouse_type == 2:
                    if KonumToIndex(pygame.mouse.get_pos()) == baslangicKonum:
                        KareCiz(baslangicKonum, WHITE)
                        baslangicKonum = -1
                    elif KonumToIndex(pygame.mouse.get_pos()) == bitisKonum:
                        KareCiz(bitisKonum, WHITE)
                        bitisKonum = -1
                    elif baslangicKonum == -1:
                        baslangicKonum = KonumToIndex(pygame.mouse.get_pos())
                        KareCiz(baslangicKonum, BLUE)
                    elif bitisKonum == -1:
                        bitisKonum = KonumToIndex(pygame.mouse.get_pos())
                        KareCiz(bitisKonum, GREEN)

                elif mouse_type == 1:
                    if (pygame.mouse.get_pos()[0] >= 970 and pygame.mouse.get_pos()[0] <= (970+150)) and (pygame.mouse.get_pos()[1] >= 830 and pygame.mouse.get_pos()[1] <= (830+50)):
                        TxtKaydet()
                        EVE_Konumlandir(baslangicKonum)
                    elif (pygame.mouse.get_pos()[0] >= 1170 and pygame.mouse.get_pos()[0] <= (1170+150)) and (pygame.mouse.get_pos()[1] >= 830 and pygame.mouse.get_pos()[1] <= (830+50)):
                        OgrenmeyeBasla()

                if event.type == pygame.MOUSEBUTTONUP:
                    dragging = False

        while dragging:

            for event in pygame.event.get():

                if event.type == pygame.MOUSEBUTTONUP:
                    dragging = False

                if pygame.mouse.get_pos()[0] < SQUARE_SIZE*SQUARE_COUNT and pygame.mouse.get_pos()[1] < SQUARE_SIZE*SQUARE_COUNT:
                    id = KonumToIndex(pygame.mouse.get_pos())

                    if id != baslangicKonum and id != bitisKonum:
                        if mouse_type == 1:
                            if not id in Engeller:
                                Engeller.append(id)
                                KareCiz(id, RED)
                        elif mouse_type == 3:
                            if id in Engeller:
                                Engeller.remove(id)
                                KareCiz(id, WHITE)

                    pygame.display.update()
    pygame.quit()
    quit()

def QV(fromID, toID, cost, secilenYon):
    QTable[fromID][secilenYon] += ogrenmeKatsayisi * (cost + indirimKatsayisi * max(QTable[toID]) - QTable[fromID][secilenYon])

def EVE_Konumlandir(id):
    global EVE_ID, baslangicKonum

    if EVE_ID != -1:
        KareCiz(EVE_ID, WHITE)

    if EVE_ID == baslangicKonum:
        KareCiz(baslangicKonum, BLUE)

    EVE_ID = id
    WIN.blit(EVE, KareCiz(id, RED, 1))
    pygame.display.flip()
    pygame.display.update()

def YolCizdir():
    global baslangicKonum, bitisKonum
    suankiKonum = CevreKontrolu(baslangicKonum, QTable[baslangicKonum].index(max(QTable[baslangicKonum])))
    boncuk = pygame.image.load("hedefYolCizdirme.png")
    EVE_Konumlandir(baslangicKonum)

    while suankiKonum != bitisKonum:
        konum = KareCiz(suankiKonum, RED, 1)
        WIN.blit(boncuk, (konum[0]+1, konum[1]+1))
        #print("Boncuk : ", konum)
        pygame.display.update()
        suankiKonum = CevreKontrolu(suankiKonum, QTable[suankiKonum].index(max(QTable[suankiKonum])))
    konum = KareCiz(suankiKonum, RED, 1)
    WIN.blit(boncuk, (konum[0] + 1, konum[1] + 1))
    pygame.display.update()

def HareketSec(id):
    maxDeger = max(QTable[id])
    maxDegerDizisi = []
    for i in range(8):
        if maxDeger == QTable[id][i]:
            maxDegerDizisi.append(i)
    return maxDegerDizisi[random.randint(0, len(maxDegerDizisi)-1)]

def CevreKontrolu(id, yon):
    if yon == 0:
        return id-1
    elif yon == 1:
        return id + SQUARE_COUNT - 1
    elif yon == 2:
        return id + SQUARE_COUNT
    elif yon == 3:
        return id + SQUARE_COUNT + 1
    elif yon == 4:
        return id + 1
    elif yon == 5:
        return id - (SQUARE_COUNT - 1)
    elif yon == 6:
        return id - SQUARE_COUNT
    elif yon == 7:
        return id - (SQUARE_COUNT + 1)

def GidilebilirMi(id, yon):
    global Engeller

    if id == 0:
        if yon == 7 or yon == 6 or yon == 0 or yon == 1 or yon == 5:
            return False
    elif id == SQUARE_COUNT - 1:
        if yon == 3 or yon == 4 or yon == 5 or yon == 6 or yon == 7:
            return False
    elif id == SQUARE_COUNT*(SQUARE_COUNT - 1):
        if yon == 3 or yon == 0 or yon == 1 or yon == 2 or yon == 7:
            return False
    elif id == SQUARE_COUNT*SQUARE_COUNT - 1:
        if yon == 3 or yon == 4 or yon == 1 or yon == 2 or yon == 5:
            return False

    if id < SQUARE_COUNT:
        # en soldasın
        if yon == 7 or yon == 6 or yon == 5:
            return False
        elif CevreKontrolu(id, yon) in Engeller:
            return False
    elif id % SQUARE_COUNT == 0:
        #en üsttesin
        if yon == 7 or yon == 0 or yon == 1:
            return False
        elif CevreKontrolu(id, yon) in Engeller:
            return False
    elif (id+1) % SQUARE_COUNT == 0:
        #en alttasın
        if yon == 5 or yon == 4 or yon == 3:
            return False
        elif CevreKontrolu(id, yon) in Engeller:
            return False
    elif id >= SQUARE_COUNT*(SQUARE_COUNT-1):
        #en sağdasın
        if yon == 1 or yon == 2 or yon == 3:
            return False
        elif CevreKontrolu(id, yon) in Engeller:
            return False
    else:
        #orta alan
        if CevreKontrolu(id, yon) in Engeller:
            return False

    return True


def OgrenmeyeBasla():
    global suankiSkor, oncekiSkor, baslangicKonum, hamleSayisi
    print("Öğrenmeye başlandı.")

    while True:
        secilenYon = HareketSec(EVE_ID)
        #time.sleep(0.05)
        if GidilebilirMi(EVE_ID, secilenYon):
            if CevreKontrolu(EVE_ID, secilenYon) == bitisKonum:
                QV(EVE_ID, CevreKontrolu(EVE_ID, secilenYon), +5, secilenYon)
                print("Hedef bulundu.")
                suankiSkor += QTable[EVE_ID][secilenYon]
                ortalamaSkorlar.append(suankiSkor)
                #GrafigeYansit()

                if oncekiSkor == suankiSkor:
                    YolCizdir()
                    GrafigeYansit()
                    break
                else:
                    oncekiSkor = suankiSkor
                suankiSkor = 0
                hamleSayisi = 0
                EVE_Konumlandir(baslangicKonum)
            else:
                hamleSayisi += 1
                QV(EVE_ID, CevreKontrolu(EVE_ID, secilenYon), -0.1, secilenYon)
                suankiSkor += QTable[EVE_ID][secilenYon]
                EVE_Konumlandir(CevreKontrolu(EVE_ID, secilenYon))
        else:
            hamleSayisi = 0
            QV(EVE_ID, CevreKontrolu(EVE_ID, secilenYon), -5, secilenYon)
            suankiSkor += QTable[EVE_ID][secilenYon]
        
            ortalamaSkorlar.append(suankiSkor)
            #GrafigeYansit()

            oncekiSkor = suankiSkor
            suankiSkor = 0
            EVE_Konumlandir(baslangicKonum)

def TxtKaydet():
    global Engeller
    dosya = open("engel.txt", "w")

    for id in range(SQUARE_COUNT*SQUARE_COUNT):
        matrisKonumu = KonumToIndex(KareCiz(id, RED, 1), 1)
        if id == baslangicKonum:
            yazi = "(" + str(matrisKonumu[0]) + "," + str(matrisKonumu[1]) + ",M)\n"
        elif id == bitisKonum:
            yazi = "(" + str(matrisKonumu[0]) + "," + str(matrisKonumu[1]) + ",Y)\n"
        elif id in Engeller:
            yazi = "(" + str(matrisKonumu[0]) + "," + str(matrisKonumu[1]) + ",K)\n"
        else:
            yazi = "(" + str(matrisKonumu[0]) + "," + str(matrisKonumu[1]) + ",B)\n"
        dosya.write(yazi)

    dosya.close()

def HaritaCiz():
    for i in range(SQUARE_COUNT):
        for j in range(SQUARE_COUNT):
            pygame.draw.rect(WIN, BLACK, (i * SQUARE_SIZE, j*SQUARE_SIZE, SQUARE_SIZE, SQUARE_SIZE), 1)
    pygame.display.update()

def KonumToIndex(Konum, kontrol = 0):
    xPos = int(Konum[0] / SQUARE_SIZE)#yatay
    yPos = int(Konum[1] / SQUARE_SIZE)#dikey
    if kontrol == 1:
        return (yPos, xPos)
    return IDBelirle(yPos, xPos)

def IDBelirle(xPos,yPos):
    id = xPos + yPos * SQUARE_COUNT
    return id

def KareCiz(id, renk = RED, kontrol = 0):
    xPos = int(id % SQUARE_COUNT)
    yPos = int(id / SQUARE_COUNT)

    xPos = xPos * SQUARE_SIZE
    yPos = yPos * SQUARE_SIZE

    if kontrol == 1:
        return (yPos, xPos)

    pygame.draw.rect(WIN, renk, (yPos, xPos, SQUARE_SIZE, SQUARE_SIZE))
    pygame.draw.rect(WIN, BLACK, (yPos, xPos, SQUARE_SIZE, SQUARE_SIZE), 1)

def RandomEngelOlustur():
    for i in range(0, int((SQUARE_COUNT*SQUARE_COUNT)*0.3)):
        sayi = random.randint(0, SQUARE_COUNT*SQUARE_COUNT-1)
        if not sayi in Engeller:
            Engeller.append(sayi)
        else:
            i -= 1
    EngelleriCiz(Engeller)
    #print(Engeller)

def EngelleriCiz(engelDizisi):
    for i in range(0, len(engelDizisi)):
        KareCiz(engelDizisi[i], RED)

def GrafigeYansit():
    ax = fig.gca()
    ax.plot(ortalamaSkorlar, 'b-')
    ax.set_ylabel('Aldığı Skor', color='blue')
    ax.set_xlabel('Tekrar Sayısı', color='red')
    canvas = agg.FigureCanvasAgg(fig)
    canvas.draw()
    renderer = canvas.get_renderer()
    raw_data = renderer.tostring_rgb()
    size = canvas.get_width_height()
    surf = pygame.image.fromstring(raw_data, size, "RGB")
    WIN.blit(surf, (900, 0))
    pygame.display.flip()
    pass

pygame.display.update()
main()
