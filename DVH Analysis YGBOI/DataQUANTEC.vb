Public Class DataQuantec
    Dim rowVolume(20) As String
    Dim rowMean(10), temp(10) As String
    Dim rowMax(10) As String
    Dim PrimaryRxID, SecondRxID, ThirdRxID, FourthRxID, FifthRxID As String
    Dim RxDose1, RxDose2, RxDose3, RxDose4, RxDose5 As Single
    Dim RxDose1_107, RxDose2_107, RxDose3_107, RxDose4_107, RxDose5_107 As Single
    Dim RxDose1_105, RxDose2_105, RxDose3_105, RxDose4_105, RxDose5_105 As Single
    Dim RxDose1_110, RxDose2_110, RxDose3_110, RxDose4_110, RxDose5_110 As Single
    Dim RxDose1_90, RxDose2_90, RxDose3_90, RxDose4_90, RxDose5_90 As Single
    Dim RxDose1_95, RxDose2_95, RxDose3_95, RxDose4_95, RxDose5_95 As Single
    Dim RxDose1_102, RxDose2_102, RxDose3_102, RxDose4_102, RxDose5_102 As Single

    

    Function volumeQuantecData(ByVal organ As String) As String()
        Dim volData() As String
        volData = volumeData(organ)
        Return volData
    End Function

    Function volumeData(ByVal organ As String)
        
        ' Format for data is {Condition for constraint if any, Dose for Volume eg 60 means V60, volume in cc's (> 1) or % expressed as a fraction (< 1), reference}
        If SelectTx.TextBox_RxDose1.Text <> "" Then RxDose1 = Convert.ToSingle(SelectTx.TextBox_RxDose1.Text)
        If SelectTx.TextBox_RxDose2.Text <> "" Then RxDose2 = Convert.ToSingle(SelectTx.TextBox_RxDose2.Text)
        If SelectTx.TextBox_RxDose3.Text <> "" Then RxDose3 = Convert.ToSingle(SelectTx.TextBox_RxDose3.Text)
        If SelectTx.TextBox_RxDose4.Text <> "" Then RxDose4 = Convert.ToSingle(SelectTx.TextBox_RxDose4.Text)
        If SelectTx.TextBox_RxDose5.Text <> "" Then RxDose5 = Convert.ToSingle(SelectTx.TextBox_RxDose5.Text)
        RxDose1_90 = RxDose1 * 0.9
        RxDose2_90 = RxDose2 * 0.9
        RxDose3_90 = RxDose3 * 0.9
        RxDose4_90 = RxDose4 * 0.9
        RxDose5_90 = RxDose5 * 0.9
        RxDose1_95 = RxDose1 * 0.95
        RxDose2_95 = RxDose2 * 0.95
        RxDose3_95 = RxDose3 * 0.95
        RxDose4_95 = RxDose4 * 0.95
        RxDose5_95 = RxDose5 * 0.95
        RxDose1_102 = RxDose1 * 1.02
        RxDose2_102 = RxDose2 * 1.02
        RxDose3_102 = RxDose3 * 1.02
        RxDose4_102 = RxDose4 * 1.02
        RxDose5_102 = RxDose5 * 1.02
        RxDose1_105 = RxDose1 * 1.05
        RxDose2_105 = RxDose2 * 1.05
        RxDose3_105 = RxDose3 * 1.05
        RxDose4_105 = RxDose4 * 1.05
        RxDose5_105 = RxDose5 * 1.05
        RxDose1_107 = RxDose1 * 1.07
        RxDose2_107 = RxDose2 * 1.07
        RxDose3_107 = RxDose3 * 1.07
        RxDose4_107 = RxDose4 * 1.07
        RxDose5_107 = RxDose5 * 1.07
        RxDose1_110 = RxDose1 * 1.1
        RxDose2_110 = RxDose2 * 1.1
        RxDose3_110 = RxDose3 * 1.1
        RxDose4_110 = RxDose4 * 1.1
        RxDose5_110 = RxDose5 * 1.1
        

        Erase rowVolume         ' Cleaning up array prior to each use
        ReDim rowVolume(20)
        PrimaryRxID = SelectTx.TextBox_RxID1.Text
        SecondRxID = SelectTx.TextBox_RxID2.Text
        ThirdRxID = SelectTx.TextBox_RxID3.Text
        FourthRxID = SelectTx.TextBox_RxID4.Text
        FifthRxID = SelectTx.TextBox_RxID5.Text
        If SelectTx.RadioButton2.Checked = True Then
            If organ = PrimaryRxID Then rowVolume = {"", RxDose1, "100.1%", "YGBOI", RxDose1_107, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = SecondRxID Then rowVolume = {"", RxDose2, "100.1%", "YGBOI", RxDose2_107, "100.1%", "YGBOI", RxDose2_95, "100.1%", "YGBOI"}
            If organ = ThirdRxID Then rowVolume = {"", RxDose3, "100.1%", "YGBOI", RxDose3_107, "100.1%", "YGBOI", RxDose3_95, "100.1%", "YGBOI"}
            If organ = FourthRxID Then rowVolume = {"", RxDose4, "100.1%", "YGBOI", RxDose4_107, "100.1%", "YGBOI", RxDose4_95, "100.1%", "YGBOI"}
            If organ = FifthRxID Then rowVolume = {"", RxDose5, "100.1%", "YGBOI", RxDose5_107, "100.1%", "YGBOI", RxDose5_95, "100.1%", "YGBOI"}
            If organ = "RT Parotid" Then rowVolume = {"", "30", "50%", "YGBOI"}
            If organ = "LT Parotid" Then rowVolume = {"", "30", "50%", "YGBOI"}
            If organ = "Lt Inner Ear" Then rowVolume = {"", "55", "5%", "YGBOI"}
            If organ = "Rt Inner Ear" Then rowVolume = {"", "55", "5%", "YGBOI"}
            'If organ = "Glottic Larynx" Then rowVolume = {"", "35", "50%", "YGBOI"}   want 2/3 below 50

        End If

        If SelectTx.RadioButton3.Checked = True Then
            If organ = PrimaryRxID Then rowVolume = {"", RxDose1, "100.1%", "YGBOI", RxDose1_107, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = SecondRxID Then rowVolume = {"", RxDose2, "100.1%", "YGBOI", RxDose2_107, "100.1%", "YGBOI", RxDose2_95, "100.1%", "YGBOI"}
            If organ = ThirdRxID Then rowVolume = {"", RxDose3, "100.1%", "YGBOI", RxDose3_107, "100.1%", "YGBOI", RxDose3_95, "100.1%", "YGBOI"}
            If organ = FourthRxID Then rowVolume = {"", RxDose4, "100.1%", "YGBOI", RxDose4_107, "100.1%", "YGBOI", RxDose4_95, "100.1%", "YGBOI"}
            If organ = FifthRxID Then rowVolume = {"", RxDose5, "100.1%", "YGBOI", RxDose5_107, "100.1%", "YGBOI", RxDose5_95, "100.1%", "YGBOI"}
            If organ = "Bilateral Lung" Then rowVolume = {"", "5", "65%", "YGBOI", "20", "35%", "YGBOI"}
            If organ = "Heart" Then rowVolume = {"", "30", "50%", "YGBOI", "45", "35%", "YGBOI", "70", "0.03 cc", "YGBOI"}
            If organ = "Esophagus" Then rowVolume = {"", "35", "50%", "YGBOI", "60", "33%", "YGBOI", "70", "20%", "YGBOI"}
            If organ = "CTVn" Then rowVolume = {"", "60", "100%", "YGBOI", "64.2", "100%", "YGBOI", "57", "100.1%", "YGBOI"}
            If organ = "PTVn" Then rowVolume = {"", "60", "100%", "YGBOI", "64.2", "100%", "YGBOI", "57", "100.1%", "YGBOI"}
        End If

        If SelectTx.RadioButton4.Checked = True Then
            rowVolume = {"", "48", "0.03 cc", "YGBOI", "70", "1 cc", "YGBOI", "55", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton5.Checked = True Then
            rowVolume = {"", "40", "101%", "YGBOI", "45", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton6.Checked = True Then
            rowVolume = {"", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "15", "101%", "YGBOI", "20", "101%", "YGBOI", "25", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton7.Checked = True Then
            rowVolume = {"", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "20", "101%", "YGBOI", "30", "101%", "YGBOI", "40", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton8.Checked = True Then
            rowVolume = {"", "18", "101%", "YGBOI", "20", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "54", "101%", "YGBOI", "60", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton9.Checked = True Then
            rowVolume = {"", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI", "80", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton10.Checked = True Then
            rowVolume = {"", "40", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton11.Checked = True Then
            rowVolume = {"", "40", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton12.Checked = True Then
            rowVolume = {"", "18", "101%", "YGBOI", "20", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "54", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton13.Checked = True Then
            rowVolume = {"", "20", "101%", "YGBOI", "30", "101%", "YGBOI", "35", "101%", "YGBOI", "40", "101%", "YGBOI", "44", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton14.Checked = True Then
            rowVolume = {"", "1.86", "101%", "YGBOI", "3.1", "101%", "YGBOI", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "20", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton15.Checked = True Then
            rowVolume = {"", "1.86", "101%", "YGBOI", "3.1", "101%", "YGBOI", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "15", "101%", "YGBOI", "20", "101%", "YGBOI", "25", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton16.Checked = True Then
            If organ = PrimaryRxID Then rowVolume = {"", RxDose1, "100.1%", "YGBOI", RxDose1_107, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = SecondRxID Then rowVolume = {"", RxDose2, "100.1%", "YGBOI", RxDose2_107, "100.1%", "YGBOI", RxDose2_95, "100.1%", "YGBOI"}
            If organ = ThirdRxID Then rowVolume = {"", RxDose3, "100.1%", "YGBOI", RxDose3_107, "100.1%", "YGBOI", RxDose3_95, "100.1%", "YGBOI"}
            If organ = FourthRxID Then rowVolume = {"", RxDose4, "100.1%", "YGBOI", RxDose4_107, "100.1%", "YGBOI", RxDose4_95, "100.1%", "YGBOI"}
            If organ = FifthRxID Then rowVolume = {"", RxDose5, "100.1%", "YGBOI", RxDose5_107, "100.1%", "YGBOI", RxDose5_95, "100.1%", "YGBOI"}
            If organ = "CTV_BREAST" Then rowVolume = {"", RxDose1, "100.1%", "YGBOI", RxDose1_105, "100.1%", "YGBOI", RxDose1_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "PTV_BREAST_Eval" Then rowVolume = {"", RxDose1, "100.1%", "YGBOI", RxDose1_105, "100.1%", "YGBOI", RxDose1_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "PTVn_SCL" Then rowVolume = {"", RxDose2, "100.1%", "YGBOI", RxDose1_105, "100.1%", "YGBOI", RxDose1_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "PTVn_Axilla" Then rowVolume = {"", RxDose2, "100.1%", "YGBOI", RxDose1_105, "100.1%", "YGBOI", RxDose1_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "PTVn_IMN" Then rowVolume = {"", RxDose1, "100.1%", "YGBOI", RxDose1_105, "100.1%", "YGBOI", RxDose1_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "CTV_TB" Then rowVolume = {"", RxDose3, "100.1%", "YGBOI", RxDose3_105, "100.1%", "YGBOI", RxDose3_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "CTV_TB_Eval" Then rowVolume = {"", RxDose3, "100.1%", "YGBOI", RxDose3_105, "100.1%", "YGBOI", RxDose3_110, "100.1%", "YGBOI", RxDose1_90, "100.1%", "YGBOI", RxDose1_95, "100.1%", "YGBOI"}
            If organ = "Heart" Then rowVolume = {"", "15", "30%", "YGBOI", "25", "5%", "YGBOI", "25", "0.03 cc", "YGBOI"}
            If organ = "Lung LT" Then rowVolume = {"", "5", "65%", "YGBOI", "10", "50%", "YGBOI", "20", "30%", "YGBOI"}
            If organ = "Lung RT" Then rowVolume = {"", "5", "65%", "YGBOI", "10", "50%", "YGBOI", "20", "30%", "YGBOI"}
            If organ = "Thyroid" Then rowVolume = {"", "30", "30%", "YGBOI"}
        End If

        Return rowVolume
    End Function
    Function meanQuantecData(ByVal organ As String) As String()
        Dim meanQData() As String
        meanQData = meanData(organ)
        Return meanQData
    End Function

    Function meanData(ByVal organ As String)

        ' Format for data is {Organ Name with Conditions, Volume as a fraction eg whole =1 half = 0.5, Mean Dose in Gy, reference}

        Erase rowMean           ' Cleaning up array prior to each use
        ReDim rowMean(10)

        If SelectTx.RadioButton2.Checked = True Then
            If organ = PrimaryRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = SecondRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = ThirdRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = FourthRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = FifthRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = "Lips" Then rowMean = {"", "1", "20", "YGBOI"}
            If organ = "RT Parotid" Then rowMean = {"", "1", "26", "YGBOI"}
            If organ = "LT Parotid" Then rowMean = {"", "1", "26", "YGBOI"}
            If organ = "Oral Cavity" Then rowMean = {"", "1", "30", "YGBOI"}
            If organ = "Lt Inner Ear" Then rowMean = {"", "1", "45", "YGBOI"}
            If organ = "Rt Inner Ear" Then rowMean = {"", "1", "45", "YGBOI"}
            If organ = "Glottic Larynx" Then rowMean = {"", "1", "35", "YGBOI", "1", "45", "YGBOI"}
            If organ = "Posterior Pharynx" Then rowMean = {"", "1", "54", "YGBOI"}
            If organ = "Esophagus" Then rowMean = {"", "1", "45", "YGBOI"}
        End If

        If SelectTx.RadioButton3.Checked = True Then
            If organ = PrimaryRxID Then rowMean = {"", "1", "70", "YGBOI"}
            If organ = SecondRxID Then rowMean = {"", "1", "70", "YGBOI"}
            If organ = "Bilateral Lung" Then rowMean = {"", "1", "20", "YGBOI"}
            If organ = "Esophagus" Then rowMean = {"", "1", "34", "YGBOI"}
        End If

        If SelectTx.RadioButton4.Checked = True Then
            rowMean = {"", "48", "0.03 cc", "70", "1 cc", "YGBOI", "YGBOI", "55", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton5.Checked = True Then
            rowMean = {"", "40", "101%", "YGBOI", "45", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton6.Checked = True Then
            rowMean = {"", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "15", "101%", "YGBOI", "20", "101%", "YGBOI", "25", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton7.Checked = True Then
            rowMean = {"", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "20", "101%", "YGBOI", "30", "101%", "YGBOI", "40", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton8.Checked = True Then
            rowMean = {"", "18", "101%", "YGBOI", "20", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "54", "101%", "YGBOI", "60", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton9.Checked = True Then
            rowMean = {"", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI", "80", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton10.Checked = True Then
            rowMean = {"", "40", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton11.Checked = True Then
            rowMean = {"", "40", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton12.Checked = True Then
            rowMean = {"", "18", "101%", "YGBOI", "20", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "54", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton13.Checked = True Then
            rowMean = {"", "20", "101%", "YGBOI", "30", "101%", "YGBOI", "35", "101%", "YGBOI", "40", "101%", "YGBOI", "44", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton14.Checked = True Then
            rowMean = {"", "1.86", "101%", "YGBOI", "3.1", "101%", "YGBOI", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "20", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton15.Checked = True Then
            rowMean = {"", "1.86", "101%", "YGBOI", "3.1", "101%", "YGBOI", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "15", "101%", "YGBOI", "20", "101%", "YGBOI", "25", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton16.Checked = True Then
            If organ = PrimaryRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = SecondRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = ThirdRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = FourthRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = FifthRxID Then rowMean = {"", "1", "100", "YGBOI"}
            If organ = "Heart" Then rowMean = {"", "1", "4", "YGBOI"}
        End If

        Return rowMean
    End Function
    Function maxQuantecData(ByVal organ As String) As String()
        Dim maxQData() As String
        maxQData = maxData(organ)
        Return maxQData
    End Function


    Function maxData(ByVal organ As String)

        ' Format for data is {Organ Name with Conditions, Volume as a fraction eg whole =1 half = 0.5, Max Dose in Gy, reference}

        Erase rowMax            ' Cleaning up array prior to each use
        ReDim rowMax(10)
        If SelectTx.RadioButton2.Checked = True Then
            If organ = "Brainstem" Then rowMax = {"", "1", "54", "YGBOI"}
            If organ = "Spinal Cord" Then rowMax = {"", "1", "45", "YGBOI"}
            If organ = "Optic Nerves" Then rowMax = {"", "1", "50", "YGBOI"}
            If organ = "RT Optic Nerve" Then rowMax = {"", "1", "50", "YGBOI"}
            If organ = "LT Optic Nerve" Then rowMax = {"", "1", "50", "YGBOI"}
            If organ = "Chiasm" Then rowMax = {"", "1", "50", "YGBOI"}
            If organ = "LT Eye" Then rowMax = {"", "1", "45", "YGBOI"}
            If organ = "RT Eye" Then rowMax = {"", "1", "45", "YGBOI"}
            If organ = "Mandible" Then rowMax = {"", "1", "70", "YGBOI"}
            If organ = "Lips" Then rowMax = {"", "1", "30", "YGBOI", "1", "50", "YGBOI"}
        End If

        If SelectTx.RadioButton3.Checked = True Then
            If organ = "Spinal Cord" Then rowMax = {"", "1", "50", "YGBOI"}
            If organ = "Esophagus" Then rowMax = {"", "1", "63", "YGBOI"}
        End If

        If SelectTx.RadioButton4.Checked = True Then
            rowMax = {"", "48", "0.03 cc", "70", "1 cc", "YGBOI", "YGBOI", "55", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton5.Checked = True Then
            rowMax = {"", "40", "101%", "YGBOI", "45", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton6.Checked = True Then
            rowMax = {"", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "15", "101%", "YGBOI", "20", "101%", "YGBOI", "25", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton7.Checked = True Then
            rowMax = {"", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "20", "101%", "YGBOI", "30", "101%", "YGBOI", "40", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton8.Checked = True Then
            rowMax = {"", "18", "101%", "YGBOI", "20", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "54", "101%", "YGBOI", "60", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton9.Checked = True Then
            rowMax = {"", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI", "80", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton10.Checked = True Then
            rowMax = {"", "40", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton11.Checked = True Then
            rowMax = {"", "40", "101%", "YGBOI", "50", "101%", "YGBOI", "60", "101%", "YGBOI", "65", "101%", "YGBOI", "70", "101%", "YGBOI", "75", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton12.Checked = True Then
            rowMax = {"", "18", "101%", "YGBOI", "20", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI", "54", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton13.Checked = True Then
            rowMax = {"", "20", "101%", "YGBOI", "30", "101%", "YGBOI", "35", "101%", "YGBOI", "40", "101%", "YGBOI", "44", "101%", "YGBOI", "45", "101%", "YGBOI", "50", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton14.Checked = True Then
            rowMax = {"", "1.86", "101%", "YGBOI", "3.1", "101%", "YGBOI", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "20", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton15.Checked = True Then
            rowMax = {"", "1.86", "101%", "YGBOI", "3.1", "101%", "YGBOI", "5", "101%", "YGBOI", "10", "101%", "YGBOI", "15", "101%", "YGBOI", "20", "101%", "YGBOI", "25", "101%", "YGBOI"}
        End If

        If SelectTx.RadioButton16.Checked = True Then
            If organ = "Thyroid" Then rowMax = {"", "1", RxDose1_102, "YGBOI"}
        End If

        Return rowMax
    End Function
End Class
