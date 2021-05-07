Public Class Form1
    Dim ogretmenler(), dersler(), haftalikProgram(), personeller(), veri As String
    Dim personelMaaslari() As Double
    Dim sonDuzenlenen As Integer = 0
    Dim apartman As New Dictionary(Of Integer, String)
    Private Sub btnOgrtDers_Click(sender As Object, e As EventArgs) Handles btnOgrtDers.Click
        Dim ogrtSayisi, dersSayisi As Integer
        ogrtSayisi = 0
        dersSayisi = 0
yukari1:
        Do
            veri = InputBox("Öğretmen sayısını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
        Loop Until (Integer.TryParse(veri, ogrtSayisi))
        Select Case ogrtSayisi
            Case Integer.MinValue To 0
                GoTo yukari1
        End Select
yukari2:
        Do
            veri = InputBox("Ders sayısını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
        Loop Until (Integer.TryParse(veri, dersSayisi))
        Select Case dersSayisi
            Case Integer.MinValue To 0
                GoTo yukari2
        End Select
        ReDim ogretmenler(ogrtSayisi - 1)
        ReDim dersler(dersSayisi - 1)
        Dim sayac As Integer
        Dim k As Integer
        sayac = 0

        Do
            Do
                k = 0
                veri = InputBox((sayac + 1).ToString() + ". Öğretmen adını giriniz", "Veri Girişi", "Örn : Mustafa Sandal", 300, 300)
                Dim i As Integer = 1
                While (i <= veri.Length)
                    Dim chr As Char = GetChar(veri, i)
                    Select Case chr
                        Case "A" To "Z"
                            k = k + 1
                        Case "a" To "z"
                            k = k + 1
                        Case " "
                            k = k + 1
                        Case Else
                            Dim str As String = "üÜğĞİıçÇöÖş"
                            Dim kontrol As Integer
                            kontrol = str.IndexOf(chr)
                            Select Case kontrol
                                Case -1
                                    k = k
                                Case Else
                                    k = k + 1
                            End Select
                    End Select
                    i = i + 1
                End While
            Loop Until (k = veri.Length)
            ogretmenler(sayac) = veri
            sayac = sayac + 1
        Loop Until (sayac = ogrtSayisi)

        sayac = 0
        Do
            Do
                k = 0
                veri = InputBox((sayac + 1).ToString() + ". Ders adını giriniz", "Veri Girişi", "Örn : Veritabanı", 300, 300)
                Dim i As Integer = 1
                While (i <= veri.Length)
                    Dim chr As Char = GetChar(veri, i)
                    Select Case chr
                        Case "A" To "Z"
                            k = k + 1
                        Case "a" To "z"
                            k = k + 1
                        Case " "
                            k = k + 1
                        Case Else
                            Dim str As String = "üÜğĞİıçÇöÖş"
                            Dim kontrol As Integer
                            kontrol = str.IndexOf(chr)
                            Select Case kontrol
                                Case -1
                                    k = k
                                Case Else
                                    k = k + 1
                            End Select
                    End Select
                    i = i + 1
                End While
            Loop Until (k = veri.Length)
            dersler(sayac) = veri
            sayac = sayac + 1
        Loop Until (sayac = dersSayisi)
        sayac = 0
        Dim rand, rand1 As New Random
        ReDim haftalikProgram(ogrtSayisi - 1)

        While (sayac < ogrtSayisi)
            Dim gun As String = ""
            Select Case rand1.Next(5) + 1
                Case 1
                    gun = "Pazartesi"
                Case 2
                    gun = "Salı"
                Case 3
                    gun = "Çarşamba"
                Case 4
                    gun = "Perşembe"
                Case 5
                    gun = "Cuma"
            End Select
            haftalikProgram(sayac) = (sayac + 1).ToString() + " - " + ogretmenler(sayac) + " " + dersler(rand.Next(dersSayisi - 1)) + " " + gun
            sayac += 1
        End While

        btnOgretmenleriGetir.Enabled = True
        btnOgrtDers.Enabled = False
    End Sub

    Private Sub btnOgretmenleriGetir_Click(sender As Object, e As EventArgs) Handles btnOgretmenleriGetir.Click
        Select Case RadioButtonTumBilgiler.Checked
            Case True
                ListBox1.Items.Clear()
                Dim i As Integer = 0
                While (i < haftalikProgram.Count)
                    ListBox1.Items.Add(haftalikProgram(i))
                    i += 1
                End While
            Case False
                Dim sayi As Integer
                Do
                    veri = InputBox("Listelemek istediğiniz öğretmenin numarasını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
                Loop Until (Integer.TryParse(veri, sayi))
                ListBox1.Items.Clear()
                Select Case sayi
                    Case 1 To ogretmenler.Count
                        ListBox1.Items.Add(haftalikProgram(sayi - 1))
                    Case Else
                        MsgBox("Bu numarada bir öğretmen yok.")
                End Select
        End Select
    End Sub

    Private Sub btnApartman_Click(sender As Object, e As EventArgs) Handles btnApartman.Click
        Dim apartmanSakiniSayisi As Integer
        apartmanSakiniSayisi = 0
yukari7:
        Do
            veri = InputBox("Apartman Sakini sayısını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
        Loop Until (Integer.TryParse(veri, apartmanSakiniSayisi))
        Select Case apartmanSakiniSayisi
            Case Integer.MinValue To 0
                GoTo yukari7
        End Select

        Dim sayac, k, daireNo As Integer
        sayac = 0
        Dim eklenecekVeri As String

        While (sayac < apartmanSakiniSayisi)
            Do
                k = 0
                veri = InputBox((sayac + 1).ToString() + ". Apartman Sakini adını ve soyadını giriniz", "Veri Girişi", "Örn : Mustafa Sandal", 300, 300)
                Dim i As Integer = 1
                While (i <= veri.Length)
                    Dim chr As Char = GetChar(veri, i)
                    Select Case chr
                        Case "A" To "Z"
                            k = k + 1
                        Case "a" To "z"
                            k = k + 1
                        Case " "
                            k = k + 1
                        Case Else
                            Dim str As String = "üÜğĞİıçÇöÖş"
                            Dim kontrol As Integer
                            kontrol = str.IndexOf(chr)
                            Select Case kontrol
                                Case -1
                                    k = k
                                Case Else
                                    k = k + 1
                            End Select
                    End Select
                    i = i + 1
                End While
            Loop Until (k = veri.Length)
            eklenecekVeri = veri

            Do
                k = 0
                veri = InputBox((sayac + 1).ToString() + ". Apartman Sakininin ev sahibi/kiracı bilgisini giriniz", "Veri Girişi", "Örn : ev sahibi", 300, 300)
                Dim i As Integer = 1
                While (i <= veri.Length)
                    Dim chr As Char = GetChar(veri, i)
                    Select Case chr
                        Case "A" To "Z"
                            k = k + 1
                        Case "a" To "z"
                            k = k + 1
                        Case " "
                            k = k + 1
                        Case Else
                            Dim str As String = "üÜğĞİıçÇöÖş"
                            Dim kontrol As Integer
                            kontrol = str.IndexOf(chr)
                            Select Case kontrol
                                Case -1
                                    k = k
                                Case Else
                                    k = k + 1
                            End Select
                    End Select
                    i = i + 1
                End While
            Loop Until (k = veri.Length)
            eklenecekVeri += " " + veri

yukari8:
            Do
                veri = InputBox((sayac + 1).ToString() + ". Apartman Sakininin daire numarasını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
            Loop Until (Integer.TryParse(veri, daireNo))
            Select Case daireNo
                Case Integer.MinValue To 0
                    GoTo yukari8
            End Select
            Select Case apartman.ContainsKey(daireNo)
                Case True
                    GoTo yukari8
            End Select
            apartman.Add(daireNo, eklenecekVeri)
            sayac += 1
        End While
        btnApartmanSakiniBul.Enabled = True
    End Sub

    Private Sub btnApartmanSakiniBul_Click(sender As Object, e As EventArgs) Handles btnApartmanSakiniBul.Click
        Dim daireNo As Integer = 0
        Do
            veri = InputBox("Bulmak istediğiniz apartman sakininin daire numarasını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
        Loop Until (Integer.TryParse(veri, daireNo))

        Select Case apartman.ContainsKey(daireNo)
            Case True
                Dim i As Integer = 0
                While (i < apartman.Count)
                    Select Case apartman.Keys(i)
                        Case daireNo
                            MsgBox(apartman.Keys(i).ToString() + " - " + apartman.Values(i))
                    End Select
                    i += 1
                End While
            Case False
                MsgBox("Böyle bir apartman sakini yok")
        End Select
    End Sub

    Private Sub btnPersonel_Click(sender As Object, e As EventArgs) Handles btnPersonel.Click
        Dim personelSayisi As Integer
        personelSayisi = 0
yukari3:
        Do
            veri = InputBox("Personel sayısını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
        Loop Until (Integer.TryParse(veri, personelSayisi))
        Select Case personelSayisi
            Case Integer.MinValue To 0
                GoTo yukari3
        End Select

        ReDim personeller(personelSayisi - 1)
        ReDim personelMaaslari(personelSayisi - 1)
        Dim sayac As Integer
        Dim k As Integer
        sayac = 0

        Do
            Do
                k = 0
                veri = InputBox((sayac + 1).ToString() + ". Personel adını ve soyadını giriniz", "Veri Girişi", "Örn : Mustafa Sandal", 300, 300)
                Dim i As Integer = 1
                While (i <= veri.Length)
                    Dim chr As Char = GetChar(veri, i)
                    Select Case chr
                        Case "A" To "Z"
                            k = k + 1
                        Case "a" To "z"
                            k = k + 1
                        Case " "
                            k = k + 1
                        Case Else
                            Dim str As String = "üÜğĞİıçÇöÖş"
                            Dim kontrol As Integer
                            kontrol = str.IndexOf(chr)
                            Select Case kontrol
                                Case -1
                                    k = k
                                Case Else
                                    k = k + 1
                            End Select
                    End Select
                    i = i + 1
                End While
            Loop Until (k = veri.Length)
            personeller(sayac) = veri
            Dim maas As Integer = 0
yukari4:
            Do
                veri = InputBox((sayac + 1).ToString() + ". Personel'in Brüt maaşını giriniz(min 2324)", "Veri Girişi", "Örn : 3", 300, 300)
            Loop Until (Integer.TryParse(veri, maas))
            Select Case maas
                Case Integer.MinValue To 2323
                    GoTo yukari4
            End Select
            personelMaaslari(sayac) = maas
            sayac = sayac + 1
        Loop Until (sayac = personelSayisi)
        btnPersonelMaasHesap.Enabled = True
        btnPersonel.Enabled = False
    End Sub

    Private Sub btnPersonelMaasHesap_Click(sender As Object, e As EventArgs) Handles btnPersonelMaasHesap.Click
        While (sonDuzenlenen < personelMaaslari.Count)
            personelMaaslari(sonDuzenlenen) *= 0.6
            sonDuzenlenen += 1
        End While
        btnPersonelGoruntule.Enabled = True
        btnPersonelEkle.Enabled = True
    End Sub

    Private Sub btnPersonelGoruntule_Click(sender As Object, e As EventArgs) Handles btnPersonelGoruntule.Click
        ListBox1.Items.Clear()
        Dim i As Integer = 0
        While (i < personeller.Count)
            ListBox1.Items.Add((i + 1).ToString() + " - " + personeller(i) + " " + personelMaaslari(i).ToString())
            i += 1
        End While
    End Sub

    Private Sub btnPersonelEkle_Click(sender As Object, e As EventArgs) Handles btnPersonelEkle.Click
        Dim eklenecekPersonelSayisi As Integer = 0
yukari5:
        Do
            veri = InputBox("Eklenecek personel sayısını giriniz", "Veri Girişi", "Örn : 3", 300, 300)
        Loop Until (Integer.TryParse(veri, eklenecekPersonelSayisi))
        Select Case eklenecekPersonelSayisi
            Case Integer.MinValue To -1
                GoTo yukari5
            Case 0
                Return
        End Select

        Dim sayac As Integer
        Dim k As Integer
        sayac = personeller.Count

        ReDim Preserve personeller(personeller.Count + eklenecekPersonelSayisi - 1)
        ReDim Preserve personelMaaslari(personelMaaslari.Count + eklenecekPersonelSayisi - 1)

        Do
            Do
                k = 0
                veri = InputBox((sayac + 1).ToString() + ". Personel adını ve soyadını giriniz", "Veri Girişi", "Örn : Mustafa Sandal", 300, 300)
                Dim i As Integer = 1
                While (i <= veri.Length)
                    Dim chr As Char = GetChar(veri, i)
                    Select Case chr
                        Case "A" To "Z"
                            k = k + 1
                        Case "a" To "z"
                            k = k + 1
                        Case " "
                            k = k + 1
                        Case Else
                            Dim str As String = "üÜğĞİıçÇöÖş"
                            Dim kontrol As Integer
                            kontrol = str.IndexOf(chr)
                            Select Case kontrol
                                Case -1
                                    k = k
                                Case Else
                                    k = k + 1
                            End Select
                    End Select
                    i = i + 1
                End While
            Loop Until (k = veri.Length)
            personeller(sayac) = veri
            Dim maas As Integer = 0
yukari6:
            Do
                veri = InputBox((sayac + 1).ToString() + ". Personel'in Brüt maaşını giriniz(min 2324)", "Veri Girişi", "Örn : 3", 300, 300)
            Loop Until (Integer.TryParse(veri, maas))
            Select Case maas
                Case Integer.MinValue To 2323
                    GoTo yukari6
            End Select
            personelMaaslari(sayac) = maas
            sayac = sayac + 1
        Loop Until (sayac = personeller.Count)
        btnPersonelGoruntule.Enabled = False
    End Sub

End Class
