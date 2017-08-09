'   This class consists of one function, findOrgans. The function findOrgans reads in the patient DVH text  
'   file one line at a time and then looks to see if the current line contains the name of any of the 
'   pre-defined organs. As it finds a match, it continuously populates the array organList. For those organs 
'   that may have more than one occurrence (eg right lung, left lung total lung), each of those options is 
'   an element of the arrayList. Once the entire patient file is read, arrayList is returned to the function 
'   findOrgans. The class DVHAnalysis is then called and from there, the organs stored in findOrgans() are 
'   checked against the Emami list (in Class DataEmami which is called from DVHAnalysis) and the doses for 
'   each organ are then used to populate a DataGrid which is drawn on the form DVHAnalysis.

Imports System.IO
Public Class FindOrgans
    Public Shared patientName, patientMR, planName As String
    Public Shared cordLength(3) As Single
    Public Shared organVol(100, 8000), relDose(100, 8000) As Single
    Public Shared absDose(100, 8000) As Integer
    Public Shared doseunit As Boolean = False
    Public Shared breastplan As Boolean = False
    Dim breastrt As Boolean = False
    Dim breastlt As Boolean = False
    Dim breastfinished As Boolean = False
    Dim answer As Boolean = False
    Dim organList(100, 2) As String
    Dim sInputLine, nameLine, checkLine, primary, second, third, fourth, fifth As String
    Dim entered As Boolean = False
    Dim alreadyasked As Boolean = False
    Dim i, j, jTotal(100), ii, numberofkidneys As Integer
    Dim partialOrganDoseRel(100), partialOrganVolume As Single
    Dim partialOrganDoseAbs(100) As Single
    Dim partialOrganVol(100) As Single
    Dim tempString, splitIntoLines() As String
    Dim organ, choice As String
    Dim organDVHData(100, 3) As Integer
    Dim charDVH(), test() As Char

    Public Function findOrgans(fileThatWasRead)
        Dim j, ii, linenumber As Integer
        Dim organIndex(100) As Boolean
        numberofkidneys = 0
        linenumber = 0
        sInputLine = ""
        splitIntoLines = fileThatWasRead.Split(vbCr)
        sInputLine = splitIntoLines(linenumber).Replace(vbLf, "")
        linenumber = linenumber + 1

        breastplan = False                                                          ' resets this variable since a previously chosen patient my have set it to true

        ' get patient name, MR# and plan name
        patientName = ""
        patientMR = ""
        planName = ""
        For j = 1 To Len(sInputLine)
            If Not IsNumeric(Mid(sInputLine, j, 1)) Then
                patientName = patientName & Mid(sInputLine, j, 1)
            End If
        Next j
        patientName = patientName.Replace("Patient Name         : ", "")            ' patient name from first line of file
        patientName = patientName.Replace("()", "")

        sInputLine = splitIntoLines(linenumber).Replace(vbLf, "")                   ' patient MR# from second line of file
        patientMR = sInputLine.Replace("Patient ID           : ", "")

        Do Until sInputLine.Contains("Plan:") Or sInputLine.Contains("Plan sum:") = True                                ' determine type of plan (needed for choosing options on some constraints)
            linenumber = linenumber + 1
            sInputLine = splitIntoLines(linenumber).Replace(vbLf, "")
        Loop
        planName = sInputLine.Replace("Plan:", "")                                ' Reads plan name here
        planName = sInputLine.Replace("Plan sum:", "")                              ' Reads plan sum name here
        sInputLine = LCase(sInputLine)
        If sInputLine.Contains("breast") Then
            breastplan = True
            If sInputLine.Contains("rt") Then breastrt = True
            If sInputLine.Contains("right") Then breastrt = True
            If sInputLine.Contains("lt") Then breastlt = True
            If sInputLine.Contains("left") Then breastlt = True
            If breastrt = False And breastlt = False Then                           ' if program cannot identify left or right breast then asks user
                MessageForm.Close()
                choice = WhichBreast.choicertlt
                MessageForm.Show()
            End If
            If choice = "right" Then breastrt = True
            If choice = "left" Then breastlt = True
        End If

        linenumber = linenumber + 1
        sInputLine = splitIntoLines(linenumber).Replace(vbLf, "")

        ' START SEARCH

        Erase organList                                                             ' Start clean
        ReDim organList(100, 2)
        j = 1
        ii = 1
        Do Until sInputLine Is Nothing
            nameLine = sInputLine.Replace("Structure: ", "")
            checkLine = LCase(((sInputLine.Replace(" ", "")).Replace(".", "")).Replace("_", "")).Replace("-", "").Replace("#", "")

            'look for Primary Rx ID
            If SelectTx.TextBox_RxID1.Text <> "" Then
                primary = LCase(((SelectTx.TextBox_RxID1.Text.Replace(" ", "")).Replace(".", "")).Replace("_", "")).Replace("-", "")
                If checkLine.Equals("structure:" & primary) Then
                    organList(ii, 1) = SelectTx.TextBox_RxID1.Text
                    organList(ii, 2) = nameLine
                    ii = ii + 1
                End If
            End If

            'look for Second Rx ID
            If SelectTx.TextBox_RxID2.Text <> "" Then
                second = LCase(((SelectTx.TextBox_RxID2.Text.Replace(" ", "")).Replace(".", "")).Replace("_", "")).Replace("-", "")
                If checkLine.Equals("structure:" & second) Then
                    organList(ii, 1) = SelectTx.TextBox_RxID2.Text
                    organList(ii, 2) = nameLine
                    ii = ii + 1
                End If
            End If

            'look for Third Rx ID
            If SelectTx.TextBox_RxID3.Text <> "" Then
                third = LCase(((SelectTx.TextBox_RxID3.Text.Replace(" ", "")).Replace(".", "")).Replace("_", "")).Replace("-", "")
                If checkLine.Equals("structure:" & third) Then
                    organList(ii, 1) = SelectTx.TextBox_RxID3.Text
                    organList(ii, 2) = nameLine
                    ii = ii + 1
                End If
            End If

            'look for Fourth Rx ID
            If SelectTx.TextBox_RxID4.Text <> "" Then
                fourth = LCase(((SelectTx.TextBox_RxID4.Text.Replace(" ", "")).Replace(".", "")).Replace("_", "")).Replace("-", "")
                If checkLine.Equals("structure:" & fourth) Then
                    organList(ii, 1) = SelectTx.TextBox_RxID4.Text
                    organList(ii, 2) = nameLine
                    ii = ii + 1
                End If
            End If

            'look for Fifth Rx ID
            If SelectTx.TextBox_RxID5.Text <> "" Then
                fifth = LCase(((SelectTx.TextBox_RxID5.Text.Replace(" ", "")).Replace(".", "")).Replace("_", "")).Replace("-", "")
                If checkLine.Equals("structure:" & fifth) Then
                    organList(ii, 1) = SelectTx.TextBox_RxID5.Text
                    organList(ii, 2) = nameLine
                    ii = ii + 1
                End If
            End If

            'look for PTVn_AXILLA
            'If checkLine.Equals("structure:ptvnaxilla") Then
            '    organList(ii, 1) = "PTVn_AXILLA"
            '    organList(ii, 2) = nameLine
            '    ii = ii + 1
            'End If

            'look for Bladder
            If checkLine.Equals("structure:bladder") Or checkLine.Equals("structure:blad") Then
                organList(ii, 1) = "Bladder"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Brachial Plexus
            If checkLine.Equals("structure:brachialplexus") Or checkLine.Equals("structure:brachplexus") Then
                organList(ii, 1) = "Brachial Plexus"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Brain
            If checkLine.Equals("structure:brain") Then
                organList(ii, 1) = "Brain"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Brainstem
            If checkLine.Equals("structure:brainstem") Or checkLine.Equals("structure:stem") Then
                organList(ii, 1) = "Brainstem"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for CTV_Breast
            'If checkLine.Equals("structure:ctvbreast") Then
            '    organList(ii, 1) = "CTV_Breast"
            '    organList(ii, 2) = nameLine
            '    ii = ii + 1
            'End If

            'look for PTV_Breast_Eval
            'If checkLine.Equals("structure:ptvbreasteval") Then
            '    organList(ii, 1) = "PTV_Breast_Eval"
            '    organList(ii, 2) = nameLine
            '    ii = ii + 1
            'End If

            'look for Bronchial Tree
            If checkLine.Equals("structure:bronchialtree") Then
                organList(ii, 1) = "Bronchial Tree"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Cauda Equina
            If checkLine.Equals("structure:caudaequina") Then
                organList(ii, 1) = "Cauda Equina"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Colon
            If checkLine.Equals("structure:colon") Or checkLine.Equals("structure:largeintestine") Or checkLine.Equals("structure:lrgintestine") Then
                organList(ii, 1) = "Colon"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lt Inner Ear
            If checkLine.Equals("structure:ltear") Or checkLine.Equals("structure:innerearlt") Or checkLine.Equals("structure:cochlealt") Or checkLine.Equals("structure:ltcochlea") Or checkLine.Equals("structure:earlt") Then
                organList(ii, 1) = "Lt Inner Ear"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Rt Inner Ear
            If checkLine.Equals("structure:rtear") Or checkLine.Equals("structure:innerearrt") Or checkLine.Equals("structure:cochleart") Or checkLine.Equals("structure:rtcochlea") Or checkLine.Equals("structure:earrt") Then
                organList(ii, 1) = "Rt Inner Ear"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Esophagus
            If checkLine.Equals("structure:esophagus") Then
                organList(ii, 1) = "Esophagus"
                'If MsgBox("Is this a H&N Plan?", 36) = 6 Then
                'organList(ii, 1) = "Esophagus - H&N Plan"
                'Else
                organList(ii, 1) = "Esophagus"
                'End If
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lt Eye
            If checkLine.Equals("structure:eyelt") Or checkLine.Equals("structure:lefteye") Or checkLine.Equals("structure:eyeleft") Or checkLine.Equals("structure:lteye") Then
                organList(ii, 1) = "LT Eye"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Rt Eye
            If checkLine.Equals("structure:eyert") Or checkLine.Equals("structure:righteye") Or checkLine.Equals("structure:eyeright") Or checkLine.Equals("structure:rteye") Then
                organList(ii, 1) = "RT Eye"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Femoral Head
            If checkLine.Equals("structure:femoralhead") Or checkLine.Equals("structure:femur") Or checkLine.Equals("structure:ltfemur") Or checkLine.Equals("structure:rtfemur") Or
               checkLine.Equals("structure:leftfemur") Or checkLine.Equals("structure:rightfemur") Or checkLine.Equals("structure:ltfemoralhead") Or
               checkLine.Equals("structure:rtfemoralhead") Or checkLine.Equals("structure:leftfemoralhead") Or checkLine.Equals("structure:rightfemoralhead") Or
               checkLine.Equals("structure:rightfemoralhead") Then
                organList(ii, 1) = "Femoral Head"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Heart
            If checkLine.Equals("structure:heart") Then
                If breastplan Then
                    organList(ii, 1) = "Heart - Breast Plan"
                Else
                    organList(ii, 1) = "Heart"
                End If
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for PTVn_IMN
            If checkLine.Equals("structure:ptvnimn") Then
                organList(ii, 1) = "PTVn_IMN"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Kidney
            If checkLine.Equals("structure:kidney") Or checkLine.Equals("structure:ltkidney") Or checkLine.Equals("structure:rtkidney") Or
                checkLine.Equals("structure:leftkidney") Or checkLine.Equals("structure:rightkidney") Then
                organList(ii, 1) = "Kidney"
                organList(ii, 2) = nameLine
                ii = ii + 1
                numberofkidneys = numberofkidneys + 1
                If numberofkidneys = 2 Then                                 ' This creates a new organ which is the combined kidneys if both are present since the plan may or may not already have a bilateral kidney predefined
                    organList(ii, 1) = "Kidneys - Bilateral"                ' Since the program will create the bilateral structure, it will not look to see if theere is already one in the data
                    organList(ii, 2) = "Kidneys"                            ' Kidneys to differentiate from Kidney
                    ii = ii + 1
                End If
            End If

            'look for Lacrimal Gland
            If checkLine.Equals("structure:lacrimalgland") Then
                organList(ii, 1) = "Lacrimal Gland"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Larynx
            If checkLine.Equals("structure:larynx") Then
                organList(ii, 1) = "Glottic Larynx"
                organList(ii, 2) = nameLine
                ii = ii + 1
                '   organList(ii, 1) = "Larynx (Edema)"
                '  organList(ii, 2) = nameLine
                '  ii = ii + 1
            End If

            'look for Lens
            If checkLine.Equals("structure:lens") Or checkLine.Equals("structure:ltlens") Or checkLine.Equals("structure:rtlens") Or checkLine.Equals("structure:leftlens") Or
                checkLine.Equals("structure:rightlens") Then
                organList(ii, 1) = "Lens"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lips
            If checkLine.Equals("structure:lips") Then
                organList(ii, 1) = "Lips"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lips for Oral Cancers
            'If checkLine.Equals("structure:lips") Then
            '    organList(ii, 1) = "Lips for Oral Cancers"
            '    organList(ii, 2) = nameLine
            '    ii = ii + 1
            'End If

            'look for Liver
            If checkLine.Equals("structure:liver") Then
                If MsgBox("Does the patient have liver cancer?", 36) = 6 Then
                    If MsgBox("Is this Primary liver cancer?", 36) = 6 Then
                        organList(ii, 1) = "Liver - Primary"
                    Else
                        organList(ii, 1) = "Liver - Metastatic"
                    End If
                Else
                    organList(ii, 1) = "Liver"
                End If
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lung and also check if breast plan. If breast plan, only use ipsilateral lung
            'If checkLine.Equals("structure:rightlung") Or checkLine.Equals("structure:rtlung") Or checkLine.Equals("structure:lungright") Or
            '    checkLine.Equals("structure:lungrt") Or checkLine.Equals("structure:leftlung") Or checkLine.Equals("structure:ltlung") Or
            '    checkLine.Equals("structure:lungleft") Or checkLine.Equals("structure:lunglt") Or checkLine.Equals("structure:lung") Or
            '    checkLine.Equals("structure:totallung") Or checkLine.Equals("structure:totlung") Or checkLine.Equals("structure:lungtotal") Or
            '    checkLine.Equals("structure:lungtot") Then
            '    If breastplan And Not breastfinished Then
            '        organList(ii, 1) = "Lung - Breast Plan"                             ' name that will look up correct constraint 

            '        If breastrt And (checkLine.Equals("structure:rightlung") Or checkLine.Equals("structure:rtlung") Or
            '                         checkLine.Equals("structure:lungright") Or checkLine.Equals("structure:lungrt")) Then                                           ' checks if right or left breast, this will cause total lung to be ignored and only enter the ipsilateral lung into organList
            '            organList(ii, 2) = nameLine
            '            ii = ii + 1
            '            breastrt = False                                        ' makes false so doesn't double count
            '            breastfinished = True
            '        End If
            '        If breastlt And (checkLine.Equals("structure:leftlung") Or checkLine.Equals("structure:ltlung") Or
            '                         checkLine.Equals("structure:lungleft") Or checkLine.Equals("structure:lunglt")) Then
            '            organList(ii, 2) = nameLine
            '            ii = ii + 1
            '            breastlt = False                                        ' makes false so doesn't double count
            '            breastfinished = True
            '        End If
            '    End If

            '    If Not breastplan Then
            '        If Not alreadyasked Then
            '            If MsgBox("Is this a patient with a pneumonectomy?", 36) = 6 Then
            '                alreadyasked = True
            '                answer = True
            '            Else
            '                alreadyasked = True
            '                answer = False
            '            End If
            '        End If
            '        If answer Then organList(ii, 1) = "Lung - Single"
            '        If Not answer Then organList(ii, 1) = "Lung - Total"
            '        organList(ii, 2) = nameLine
            '        ii = ii + 1
            '    End If
            'End If

            'look for Bilateral Lung
            If checkLine.Contains("bilateral") And checkLine.Contains("lung") Then
                organList(ii, 1) = "Bilateral Lung"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lung RT
            If checkLine.Contains("lung") And checkLine.Contains("rt") Then
                organList(ii, 1) = "Lung RT"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Lung LT
            If checkLine.Contains("lung") And checkLine.Contains("lt") Then
                organList(ii, 1) = "Lung LT"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If


            'look for Optic Chiasm
            If checkLine.Equals("structure:opticchiasm") Or checkLine.Equals("structure:optchiasm") Or checkLine.Equals("structure:chiasm") Then
                organList(ii, 1) = "Chiasm"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Rt Optic Nerve
            If checkLine.Equals("structure:rightopticnerve") Or checkLine.Equals("structure:opnervert") Or
                checkLine.Equals("structure:rightoptnerve") Or checkLine.Equals("structure:rtopticnerve") Or checkLine.Equals("structure:rtoptnerve") Or
                checkLine.Equals("structure:rtopticn") Then
                organList(ii, 1) = "RT Optic Nerve"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If


            'look for Lt Optic Nerve
            If checkLine.Equals("structure:ltoptnerve") Or checkLine.Equals("structure:lefttopticnerve") Or checkLine.Equals("structure:leftoptnerve") Or
                checkLine.Equals("structure:ltopticnerve") Or checkLine.Equals("structure:opnervelt") Or checkLine.Equals("structure:ltopticn") Then
                organList(ii, 1) = "LT Optic Nerve"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If



            'look for Oral Cavity
            If checkLine.Equals("structure:oralcavity") Then
                If MsgBox("Is this a patient with oral cavity cancer?", 36) = 6 Then
                    organList(ii, 1) = "Oral Cavity CA"
                Else
                    organList(ii, 1) = "Oral Cavity"
                End If
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for RT Parotid Gland
            If checkLine.Equals("structure:rightparotidgland") Or checkLine.Equals("structure:rtparotidgland") Or checkLine.Equals("structure:rightprtdgland") Or
                checkLine.Equals("structure:rtprtdgland") Or checkLine.Equals("structure:rightptdgland") Or checkLine.Equals("structure:rtptdgland") Or
                checkLine.Equals("structure:rightptgland") Or checkLine.Equals("structure:rtptgland") Or checkLine.Equals("structure:rightparotid") Or
                checkLine.Equals("structure:rtparotid") Or checkLine.Equals("structure:parotidrt") Then
                organList(ii, 1) = "RT Parotid"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for LT Parotid Gland
            If checkLine.Equals("structure:leftparotidgland") Or checkLine.Equals("structure:ltparotidgland") Or checkLine.Equals("structure:lefttprtdgland") Or
                checkLine.Equals("structure:ltprtdgland") Or checkLine.Equals("structure:lefttptdgland") Or checkLine.Equals("structure:ltptdgland") Or
                checkLine.Equals("structure:lefttptgland") Or checkLine.Equals("structure:ltptgland") Or checkLine.Equals("structure:leftparotid") Or
                checkLine.Equals("structure:ltparotid") Or checkLine.Equals("structure:parotidlt") Then
                organList(ii, 1) = "LT Parotid"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Penile Bulb
            If checkLine.Equals("structure:penilebulb") Then
                organList(ii, 1) = "Penile Bulb"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Pituitary Gland
            If checkLine.Equals("structure:pituitarygland") Then
                organList(ii, 1) = "Pituitary Gland"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Rectum
            If checkLine.Equals("structure:rectum") Or checkLine.Equals("structure:rect") Then
                organList(ii, 1) = "Rectum"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Retina
            If checkLine.Equals("structure:retina") Or checkLine.Equals("structure:rightretina") Or checkLine.Equals("structure:rtretina") Or
                checkLine.Equals("structure:lefttretina") Or checkLine.Equals("structure:ltretina") Then
                organList(ii, 1) = "Retina"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Rib Cage
            If checkLine.Equals("structure:ribcage") Or checkLine.Equals("structure:ribs") Or checkLine.Equals("structure:rightribcage") Or
                checkLine.Equals("structure:rtribcage") Or checkLine.Equals("structure:rightribs") Or checkLine.Equals("structure:rtribs") Or
                checkLine.Equals("structure:leftribcage") Or checkLine.Equals("structure:ltribcage") Or checkLine.Equals("structure:leftribs") Or
                checkLine.Equals("structure:ltribs") Then
                organList(ii, 1) = "Rib Cage"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for PTVn_SCL
            'If checkLine.Equals("structure:ptvnscl") Then
            '    organList(ii, 1) = "PTVn_SCL"
            '    organList(ii, 2) = nameLine
            '    ii = ii + 1
            'End If

            'look for Skin
            If checkLine.Equals("structure:skin") Then
                organList(ii, 1) = "Skin"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Spinal Cord
            If checkLine.Equals("structure:spinalcord") Or checkLine.Equals("structure:cord") Then
                organList(ii, 1) = "Length of Cord"
                organList(ii, 2) = "Length of Cord"
                ii = ii + 1
                organList(ii, 1) = "Spinal Cord"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Small Intestine
            If checkLine.Equals("structure:smallintestine") Or checkLine.Equals("structure:smintestine") Or checkLine.Equals("structure:smallbowel") Or
                checkLine.Equals("structure:smbowel") Then
                organList(ii, 1) = "Small Bowel"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Stomach
            If checkLine.Equals("structure:stomach") Then
                organList(ii, 1) = "Stomach"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Submandibular Gland
            If checkLine.Equals("structure:submandibulargland") Then
                organList(ii, 1) = "Submandibular Gland"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for CTV_TB
            If checkLine.Equals("structure:ctvtb") Or checkLine.Equals("structure:ctvtumorbed") Then
                organList(ii, 1) = "CTV_TB"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If


            'look for PTV_TB_Eval
            'If checkLine.Equals("structure:ptvtbeval") Or checkLine.Equals("structure:ptvtumorbedeval") Then
            '    organList(ii, 1) = "PTV_TB_Eval"
            '    organList(ii, 2) = nameLine
            '    ii = ii + 1
            'End If


            'look for Testis
            If checkLine.Equals("structure:testis") Then
                organList(ii, 1) = "Testis"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Mandible
            If checkLine.Equals("structure:mandible") Or checkLine.Equals("structure:rightmandible") Or checkLine.Equals("structure:rtmandible") Or
                checkLine.Equals("structure:leftmandible") Or checkLine.Equals("structure:ltmandible") Then
                organList(ii, 1) = "Mandible"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If

            'look for Thyroid
            If checkLine.Equals("structure:thyroid") Then
                organList(ii, 1) = "Thyroid"
                organList(ii, 2) = nameLine
                ii = ii + 1
            End If
            linenumber = linenumber + 1
            Try
                sInputLine = splitIntoLines(linenumber).Replace(vbLf, "")
            Catch
                sInputLine = Nothing
            End Try
            j = j + 1
        Loop
        Return organList
    End Function


    Public Function findDoseandVol() As Integer()
        Dim kidney1j, kidney2j, ikidney As Integer
        Dim linenumber As Integer = 0
        Dim kidneynumber As Integer = 0
        Dim bilatkidneyRelDose1(8000), bilatkidneyOrganVol1(8000), bilatkidneyRelDose2(8000), bilatkidneyOrganVol2(8000) As Single
        Dim bilatkidneyAbsDose1(8000), bilatkidneyAbsDose2(8000) As Integer

        sInputLine = ""                                                ' clean everything
        Erase absDose
        Erase relDose
        Erase organVol
        Erase jTotal
        Erase bilatkidneyAbsDose1
        Erase bilatkidneyRelDose1
        Erase bilatkidneyOrganVol1
        Erase bilatkidneyAbsDose2
        Erase bilatkidneyRelDose2
        Erase bilatkidneyOrganVol2
        ReDim absDose(1000, 8000)
        ReDim relDose(1000, 8000)
        ReDim organVol(1000, 8000)
        ReDim jTotal(1000)
        ReDim bilatkidneyAbsDose1(8000)
        ReDim bilatkidneyRelDose1(8000)
        ReDim bilatkidneyOrganVol1(8000)
        ReDim bilatkidneyAbsDose2(8000)
        ReDim bilatkidneyRelDose2(8000)
        ReDim bilatkidneyOrganVol2(8000)

        sInputLine = LCase(splitIntoLines(linenumber).Replace(vbLf, ""))
        linenumber = linenumber + 1
        ii = 1
        j = 1
        sInputLine = sInputLine.Replace("structure: ", "")
        organ = organList(ii, 2)
        kidneynumber = 0
        Do Until organ Is Nothing

            If organ = "Kidneys" Then                                                   ' this is the case when bilateral kidneys are calculated
                If kidney1j = kidney2j Then
                    j = kidney1j + 1                                                       ' need to let the program know how many lines in this organ's entry
                    For ikidney = 1 To kidney1j
                        absDose(ii, ikidney) = bilatkidneyAbsDose1(ikidney)
                        relDose(ii, ikidney) = bilatkidneyRelDose1(ikidney)
                        organVol(ii, ikidney) = bilatkidneyOrganVol1(ikidney) + bilatkidneyOrganVol2(ikidney)
                    Next
                Else
                    If kidney1j > kidney2j Then
                        j = kidney1j + 1
                        For ikidney = 1 To kidney2j
                            absDose(ii, ikidney) = bilatkidneyAbsDose2(ikidney)
                            relDose(ii, ikidney) = bilatkidneyRelDose2(ikidney)
                            organVol(ii, ikidney) = bilatkidneyOrganVol2(ikidney) + bilatkidneyOrganVol1(ikidney)
                        Next
                        For ikidney = kidney2j + 1 To kidney1j
                            absDose(ii, ikidney) = bilatkidneyAbsDose1(ikidney)
                            relDose(ii, ikidney) = bilatkidneyRelDose1(ikidney)
                            organVol(ii, ikidney) = bilatkidneyOrganVol1(ikidney)
                        Next
                    End If
                    If kidney2j > kidney1j Then
                        j = kidney2j + 1
                        For ikidney = 1 To kidney1j
                            absDose(ii, ikidney) = bilatkidneyAbsDose1(ikidney)
                            relDose(ii, ikidney) = bilatkidneyRelDose1(ikidney)
                            organVol(ii, ikidney) = bilatkidneyOrganVol1(ikidney) + bilatkidneyOrganVol2(ikidney)
                        Next
                        For ikidney = kidney1j + 1 To kidney2j
                            absDose(ii, ikidney) = bilatkidneyAbsDose2(ikidney)
                            relDose(ii, ikidney) = bilatkidneyRelDose2(ikidney)
                            organVol(ii, ikidney) = bilatkidneyOrganVol2(ikidney)
                        Next
                    End If
                    kidneynumber = 0
                    GoTo 20
                End If
            End If

            If organList(ii, 1) = "Kidney" Then kidneynumber = kidneynumber + 1 ' sets kidneynumber to 1 when second kidney, used below in section that creates bilateral kidney data 

            Do Until sInputLine.Equals(LCase(organ)) = True Or organ = ("Length of Cord")       'finds section of file containing organ of interest
                sInputLine = LCase(splitIntoLines(linenumber).Replace(vbLf, ""))
                sInputLine = sInputLine.Replace("structure: ", "")                              ' organList(ii,2) contains only the original organ name so remove word "structure:"
                linenumber = linenumber + 1
            Loop
            If organ = ("Length of Cord") Then organVol(ii, 1) = 1.0 ' this value is assigned to prevent a divide by zero exception in the three functions below
            If organ = ("Length of Cord") Then GoTo 20

            Do Until sInputLine.Contains("structure volume") = True                             'scrolls through file for organ of interest until reaches first line of data
                sInputLine = LCase(splitIntoLines(linenumber).Replace(vbLf, "")).Replace(vbLf, "")
                linenumber = linenumber + 1
            Loop

            If sInputLine.Contains("[cgy]") Then doseunit = True ' checks to if the units are in cGy(True) or Gy(False)
            sInputLine = LCase(splitIntoLines(linenumber).Replace(vbLf, ""))
            linenumber = linenumber + 1
            While IsNumeric((sInputLine.Replace(" ", "")).Replace(".", ""))
                charDVH = RTrim(sInputLine.ToCharArray())                                       ' convert string to characters for digit extraction
                entered = False
                tempString = ""
                i = 1
 
                For Each ch As Char In charDVH                                              ' go through string until each set of numbers is found
                    If Char.IsNumber(ch) Or Char.IsPunctuation(ch) Or ch = "e" Then
                        tempString = tempString + ch
                        entered = True
                        ' MessageBox.Show(i & "      " & tempString)
                    End If
                    If (entered = True) And (Char.IsNumber(ch) <> True) And (Char.IsPunctuation(ch) <> True And ch <> "e") Then
                        If i = 1 Then absDose(ii, j) = tempString
                        If i = 2 Then relDose(ii, j) = tempString
                        i = i + 1
                        ' MessageBox.Show("2" & "      " & i)
                        entered = False
                        tempString = ""
                    End If
                Next
                organVol(ii, j) = tempString
                'If sInputLine.Contains("e") = True Then
                'End If
                If i = 2 Then
                    relDose(ii, j) = (absDose(ii, j) / Convert.ToSingle(SelectTx.TextBox_RxDose1.Text))                                        ' this takes care of a DVH from a combined plan which does not have a rel dose column
                    'MessageBox.Show("absdose" & "      " & absDose(ii, j))
                    'MessageBox.Show("reldose" & "      " & relDose(ii, j))
                End If

                'If sInputLine.Contains("e") = False Then                                        ' to exclude lines with very small entries needing to be expressed as xxxE-xx
                '    For Each ch As Char In charDVH                                              ' go through string until each set of numbers is found
                '        If Char.IsNumber(ch) Or Char.IsPunctuation(ch) Then
                '            tempString = tempString + ch
                '            If i = 1 Then absDose(ii, j) = Convert.ToInt32(tempString)
                '            If i = 2 Then relDose(ii, j) = Convert.ToSingle(tempString)
                '            If i = 3 Then organVol(ii, j) = Convert.ToSingle(tempString)
                '            entered = True
                '        End If
                '        If (entered = True) And (Char.IsNumber(ch) <> True) And (Char.IsPunctuation(ch) <> True) Then
                '            i = i + 1
                '            entered = False
                '            tempString = ""
                '        End If
                '    Next

                '    If organList(ii, 1) = "Kidney" Then                                         ' this section stores the data for each kidney which is then combined in the section above to give the data for the bilateral kidneys
                '        If kidneynumber = 2 Then
                '            bilatkidneyAbsDose2(j) = absDose(ii, j)                             ' this is the data for the second kidney
                '            bilatkidneyRelDose2(j) = relDose(ii, j)
                '            bilatkidneyOrganVol2(j) = organVol(ii, j)
                '            kidney2j = j
                '        Else
                '            bilatkidneyAbsDose1(j) = absDose(ii, j)                             ' this is the data for the first kidney
                '            bilatkidneyRelDose1(j) = relDose(ii, j)
                '            bilatkidneyOrganVol1(j) = organVol(ii, j)
                '            kidney1j = j
                '        End If
                '    End If

                'End If
                'If i = 2 Then
                '    organVol(ii, j) = relDose(ii, j)                                            ' this takes care of a DVH from a combined plan which does not have a rel dose column
                '    bilatkidneyOrganVol2(j) = bilatkidneyRelDose2(j)
                '    bilatkidneyOrganVol1(j) = bilatkidneyRelDose1(j)
                'End If


                Try
                    sInputLine = splitIntoLines(linenumber).Replace(vbLf, "")
                Catch
                    sInputLine = Nothing
                End Try

                linenumber = linenumber + 1
                If sInputLine = "                               " Or organVol(ii, j) = 0 Then GoTo 20
                j = j + 1
            End While

20:         jTotal(ii) = j - 1  'for each organ, jTotal keeps track of exactly how many lines contain data
            ii = ii + 1
            j = 1
            organ = organList(ii, 2)
        Loop
        Return jTotal

    End Function


    ' Determine isodose line covering partial organ for Emami

    Public Function linePartialOrganDVH(fraction)
        Dim i, j As Integer
        Dim partialOrganDoseAbsString(100) As String

        Erase partialOrganDoseAbsString                                             ' clean everything
        Erase partialOrganDoseAbs
        Erase partialOrganDoseRel
        ReDim partialOrganDoseAbsString(100)
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)

        findDoseandVol()
        For i = 1 To (ii - 1)
            partialOrganVol(i) = organVol(i, 1) * Convert.ToSingle(fraction)
            partialOrganDoseRel(i) = 0
            partialOrganDoseAbs(i) = 0

            ' Determine isodose line covering one third organ
            If fraction = "0.333333" Then
                j = 1
                Do Until organVol(i, j) <= organVol(i, 1) * Convert.ToSingle(fraction) ' will not proceed if organ = "length of cord" since jTotal = 0
                    partialOrganDoseAbs(i) = j
                    j = j + 1
                Loop

                If organList(i, 2) = "Length of Cord" Then
                    partialOrganDoseAbsString(i) = "5 cm"
                Else
                    partialOrganDoseAbsString(i) = Convert.ToString(partialOrganDoseAbs(i))
                End If
                '            If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 5cm length of cord
                'jLength = 1
                'cordLength(2) = 0
                'onethirdOrganDoseAbs(i) = 0
                'Do Until cordLength(2) > 5 Or jLength > (jTotal(i) - 1)
                'cordLength(2) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
                'onethirdOrganDoseAbs(i) = onethirdOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
                'jLength = jLength + 1
                '   If jLength > (jTotal(i) - 1) Then onethirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(2).ToString("0.0"))
                'Loop
                'End If
            End If

            ' Determine isodose line covering two third organ
            If fraction = "0.666667" Then
                j = 1
                Do Until organVol(i, j) <= organVol(i, 1) * Convert.ToSingle(fraction) ' will not proceed if organ = "length of cord" since jTotal = 0
                    partialOrganDoseAbs(i) = j
                    j = j + 1
                Loop

                If organList(i, 2) = "Length of Cord" Then
                    partialOrganDoseAbsString(i) = "10 cm"
                Else
                    partialOrganDoseAbsString(i) = Convert.ToString(partialOrganDoseAbs(i))
                End If
                '           If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 10cm length of cord
                'jLength = 1
                'cordLength(3) = 0
                'twothirdOrganDoseAbs(i) = 0
                'Do Until cordLength(3) > 10 Or jLength > (jTotal(i) - 1)
                ' cordLength(3) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
                ' twothirdOrganDoseAbs(i) = twothirdOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
                ' jLength = jLength + 1
                '   If jLength > (jTotal(i) - 1) Then twothirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(3).ToString("0.0"))
                'Loop
                'End If
            End If

            ' Determine isodose line covering whole organ
            If fraction = "1.0" Then
                j = 1
                Do Until (organVol(i, j) < organVol(i, 1)) ' will not proceed if organ = "length of cord" since jTotal = 0
                    partialOrganDoseAbs(i) = j
                    j = j + 1
                Loop
                If organList(i, 2) = "Length of Cord" Then
                    partialOrganDoseAbsString(i) = "20 cm"
                Else
                    partialOrganDoseAbsString(i) = Convert.ToString(partialOrganDoseAbs(i))
                End If
            End If
            '
            '           If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 20cm length of cord
            ' jLength = 1
            ' cordLength(1) = 0
            ' wholeOrganDoseAbs(i) = 0
            ' Do Until cordLength(1) > 20 Or jLength > (jTotal(i) - 1)
            ' cordLength(1) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
            ' wholeOrganDoseAbs(i) = wholeOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
            ' jLength = jLength + 1
            '     If jLength > (jTotal(i) - 1) Then wholeOrganDoseAbsString(i - 1) = MsgBox(cordLength(1).ToString("0.0"))
            ' Loop
            'End If

        Next

        Return partialOrganDoseAbsString
    End Function

    ' Determine partial organ volume and isodose line covering that partial volume for Emami

    Public Function partialOrganDoseVol(fraction)
        Dim i, j, jstart, jLength As Integer
        Dim partialOrganDoseAbsString(100) As String

        Erase partialOrganDoseAbsString                                             ' clean everything
        Erase partialOrganDoseAbs
        Erase partialOrganDoseRel
        ReDim partialOrganDoseAbsString(100)
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)

        findDoseandVol()
        For i = 1 To (ii - 1)
            ' If 
            partialOrganVol(i) = organVol(i, 1) * Convert.ToSingle(fraction)
            partialOrganDoseRel(i) = 0
            partialOrganDoseAbs(i) = 0
            j = 1
            jstart = 1
            While organVol(i, jstart) >= partialOrganVol(i)
                jstart = jstart + 1
            End While
            For j = jstart To (jTotal(i) - 1)
                partialOrganDoseRel(i) = partialOrganDoseRel(i) + relDose(i, j)
                partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, j) - organVol(i, j + 1)) * absDose(i, j + 1)
            Next
            If organList(i, 2) = "Length of Cord" Then
                If fraction = "1.0" Then partialOrganDoseAbsString(i) = "20 cm"
                If fraction = "0.666667" Then partialOrganDoseAbsString(i) = "10 cm"
                If fraction = "0.333333" Then partialOrganDoseAbsString(i) = "5 cm"
            Else
                partialOrganDoseAbs(i) = Convert.ToInt32(partialOrganDoseAbs(i) / partialOrganVol(i))
                partialOrganDoseAbsString(i) = Convert.ToString(partialOrganDoseAbs(i))
            End If

            If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 5cm length of cord
                jLength = 1
                cordLength(2) = 0
                partialOrganDoseAbs(i) = 0
                Do Until cordLength(2) > 5 Or jLength > (jTotal(i) - 1)
                    cordLength(2) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
                    partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
                    jLength = jLength + 1
                    '   If jLength > (jTotal(i) - 1) Then onethirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(2).ToString("0.0"))
                Loop
            End If

        Next
        Return partialOrganDoseAbsString
    End Function

    ' Determine partial organ volume and the mean dose for that partial organ for Emami

    Public Function partialOrganDoseMean(fraction)
        Dim i, j, jstart, jLength, jEnd(100) As Integer
        Dim partialOrganDoseAbsString(100) As String

        Erase partialOrganDoseAbsString                                             ' clean everything
        Erase partialOrganDoseAbs
        Erase partialOrganDoseRel
        Erase partialOrganVol
        ReDim partialOrganDoseAbsString(100)
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)
        ReDim partialOrganVol(100)

        jEnd = findDoseandVol()
        For i = 1 To (ii - 1)
            ' If 
            partialOrganVol(i) = organVol(i, 1) * Convert.ToSingle(fraction)    ' size of partial organ being considered
            partialOrganDoseRel(i) = 0
            partialOrganDoseAbs(i) = 0
            '    j = 1
            jstart = 1
            While organVol(i, jstart) > partialOrganVol(i)    ' find the point in the array where the partial organ volume begins 
                jstart = jstart + 1
            End While
            For j = jstart To (jEnd(i) - 1)     ' start evaluating doses only for the partial organ. jEnd, from function findDoseandVol(), tells where data ends.
                partialOrganDoseRel(i) = partialOrganDoseRel(i) + relDose(i, j)
                partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + ((organVol(i, j) - organVol(i, j + 1)) * absDose(i, j + 1))
            Next
            If organList(i, 2) = "Length of Cord" Then
                If fraction = "1.0" Then partialOrganDoseAbsString(i) = "20 cm"
                If fraction = "0.666667" Then partialOrganDoseAbsString(i) = "10 cm"
                If fraction = "0.333333" Then partialOrganDoseAbsString(i) = "5 cm"
            Else
                partialOrganDoseAbs(i) = Convert.ToInt32(partialOrganDoseAbs(i) / partialOrganVol(i))  ' only want to see the result to the nearest integer
                partialOrganDoseAbsString(i) = Convert.ToString(partialOrganDoseAbs(i))
            End If

            If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 5cm length of cord
                jLength = 1
                cordLength(2) = 0
                partialOrganDoseAbs(i) = 0
                Do Until cordLength(2) > 5 Or jLength > (jEnd(i) - 1)
                    cordLength(2) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
                    partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
                    jLength = jLength + 1
                    '   If jLength > (jTotal(i) - 1) Then onethirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(2).ToString("0.0"))
                Loop
            End If

        Next
        Return partialOrganDoseAbsString
    End Function

    ' Determine partial organ volume and max dose for QUANTEC

    Public Function partialOrganDoseMax(fraction)
        Dim i, j, jstart, jEnd(100) As Integer
        Dim partialOrganDoseAbsString(100) As String

        Erase partialOrganDoseAbsString                                             ' clean everything
        Erase partialOrganDoseAbs
        Erase partialOrganDoseRel
        Erase partialOrganVol
        ReDim partialOrganDoseAbsString(100)
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)
        ReDim partialOrganVol(100)

        jEnd = findDoseandVol()     ' findDoseandVol returns the value of jTotal which is the number of lines found before the volume goes to zero
        For i = 1 To (ii - 1)
            ' If 
            partialOrganVol(i) = organVol(i, 1) * Convert.ToSingle(fraction)
            partialOrganDoseRel(i) = 0
            partialOrganDoseAbs(i) = 0
            j = 1
            jstart = 1
            While organVol(i, jstart) >= partialOrganVol(i)
                jstart = jstart + 1
            End While
            For j = jstart To (jEnd(i) - 1)
                partialOrganDoseRel(i) = partialOrganDoseRel(i) + relDose(i, j)
                partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, j) - organVol(i, j + 1)) * absDose(i, j + 1)
            Next
            'If organList(i, 2) = "Length of Cord" Then
            'If fraction = "1.0" Then partialOrganDoseAbsString(i) = "20 cm"
            ' If fraction = "0.666667" Then partialOrganDoseAbsString(i) = "10 cm"
            ' If fraction = "0.333333" Then partialOrganDoseAbsString(i) = "5 cm"
            '  Else
            partialOrganDoseAbs(i) = Convert.ToInt32(partialOrganDoseAbs(i) / partialOrganVol(i))
            partialOrganDoseAbsString(i) = Convert.ToString(partialOrganDoseAbs(i))
            'End If

            'If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 5cm length of cord
            'jLength = 1
            '  cordLength(2) = 0
            '  partialOrganDoseAbs(i) = 0
            ' Do Until cordLength(2) > 5 Or jLength > (jEnd(i) - 1)
            '  cordLength(2) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
            '   partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
            '   jLength = jLength + 1
            '   If jLength > (jTotal(i) - 1) Then onethirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(2).ToString("0.0"))
            '    Loop
            '    End If

        Next
        Return partialOrganDoseAbsString
    End Function

    ' Volume covered by a dose line (e.g. V50) for a specific organ for QUANTEC
    Public Function volumeCoveredbyDoseLine(fraction, iorgan)
        'Dim j, iFraction As Integer
        Dim j As Integer
        Dim iFraction As Single
        Dim volCoveredbyDoseLine As String

        volCoveredbyDoseLine = Nothing                                             ' clean everything
        Erase partialOrganDoseAbs
        Erase partialOrganDoseRel
        Erase partialOrganVol
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)
        ReDim partialOrganVol(100)
        '   MessageBox.Show(fraction & "     " & organ)
        findDoseandVol()
        'iFraction = Convert.Toint(fraction.Replace("%", ""))      ' remove the % symbol added in DVH_QUANTEC
        iFraction = Convert.ToSingle(fraction.Replace("%", ""))      ' remove the % symbol added in DVH_QUANTEC
        If doseunit Then iFraction = iFraction * 100 ' to convert to cGy if that is the planning system default

        ' Determine volume covered by given isodose line
        j = 1
        Do Until j > iFraction
            If fraction.contains("%") Then
                partialOrganVolume = organVol(iorgan, j) / organVol(iorgan, 1)
            Else
                partialOrganVolume = organVol(iorgan, j)
            End If
            j = j + 1
        Loop

        volCoveredbyDoseLine = Convert.ToString(partialOrganVolume)

        Return volCoveredbyDoseLine
    End Function

    ' Mean dose of either the whole or a part of a specific organ for QUANTEC

    Public Function meanOrganDose(fraction, iorgan)                                                 'fraction gives fraction of organ for which the mean dose is to be calculated
        Dim j, jstart, jEnd(100) As Integer                                                         'iorgan gives the number of the array element for the organ of interest
        Dim partialOrganDoseAbsString As String

        partialOrganDoseAbsString = Nothing                                            ' clean everything
        Erase partialOrganDoseAbs
        Erase partialOrganDoseRel
        Erase partialOrganVol
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)
        ReDim partialOrganVol(100)

        jEnd = findDoseandVol()                                                                     ' findDoseandVol returns number of lines in file while volume <> 0
        partialOrganVol(iorgan) = organVol(iorgan, 1) * Convert.ToSingle(fraction)                  ' size of partial organ being considered
        partialOrganDoseRel(iorgan) = 0
        partialOrganDoseAbs(iorgan) = 0
        jstart = 1
        While organVol(iorgan, jstart) > partialOrganVol(iorgan)                                    ' find the point in the array where the partial organ volume begins 
            jstart = jstart + 1
        End While
        For j = jstart To (jEnd(iorgan) - 1)                                                   ' start evaluating doses only for the partial organ. jEnd, from function findDoseandVol(), tells where data ends.
            '   If organList(iorgan, 2) = "Kidneys" Then MsgBox("data: " & absDose(iorgan, j) & "     " & relDose(iorgan, j) & "     " & (organVol(iorgan, j)))
            partialOrganDoseRel(iorgan) = partialOrganDoseRel(iorgan) + relDose(iorgan, j)
            partialOrganDoseAbs(iorgan) = partialOrganDoseAbs(iorgan) + ((organVol(iorgan, j) - organVol(iorgan, j + 1)) * absDose(iorgan, j + 1))
            ' If organList(iorgan, 2) = "Kidneys" Then MsgBox("partialOrganDoseAbs(iorgan): " & partialOrganDoseAbs(iorgan))
        Next

        '  If organList(i, 2) = "Length of Cord" Then
        'If fraction = "1.0" Then partialOrganDoseAbsString(i) = "20 cm"
        '  If fraction = "0.666667" Then partialOrganDoseAbsString(i) = "10 cm"
        '  If fraction = "0.333333" Then partialOrganDoseAbsString(i) = "5 cm"
        '   Else
        If doseunit = True Then                                                                            ' converts dose to Gy if planning system units is cGy
            partialOrganDoseAbs(iorgan) = Convert.ToSingle(partialOrganDoseAbs(iorgan) / (100 * partialOrganVol(iorgan)))
        Else
            partialOrganDoseAbs(iorgan) = Convert.ToSingle(partialOrganDoseAbs(iorgan)) '/ (partialOrganVol(iorgan)))

        End If

        partialOrganDoseAbsString = Convert.ToString(partialOrganDoseAbs(iorgan))

        '   End If

        '   If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 5cm length of cord
        'jLength = 1
        '  cordLength(2) = 0
        ' partialOrganDoseAbs(i) = 0
        ' Do Until cordLength(2) > 5 Or jLength > (jEnd(i) - 1)
        'cordLength(2) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
        '  partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
        '  jLength = jLength + 1
        '   If jLength > (jTotal(i) - 1) Then onethirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(2).ToString("0.0"))
        '  Loop
        'End If

        '  Next

        Return partialOrganDoseAbsString
    End Function

    ' Max dose of either the whole or a part of a specific organ for QUANTEC

    Public Function maxOrganDose(fraction, iorgan)
        Dim jEnd(100) As Integer
        Dim partialOrganDoseAbsString As String

        partialOrganDoseAbsString = Nothing
        Erase partialOrganDoseAbs                                                   ' clean everything
        Erase partialOrganDoseRel
        Erase partialOrganVol
        ReDim partialOrganDoseAbs(100)
        ReDim partialOrganDoseRel(100)
        ReDim partialOrganVol(100)

        jEnd = findDoseandVol()        ' findDoseandVol returns the value of jTotal which is the number of lines found before the volume goes to zero
        partialOrganVol(iorgan) = organVol(iorgan, 1) * Convert.ToSingle(fraction)
        partialOrganDoseRel(iorgan) = 0
        partialOrganDoseAbs(iorgan) = 0
        ' j = 1
        ' jstart = 1

        ' While organVol(iorgan, jstart) >= partialOrganVol(iorgan)
        'jstart = jstart + 1
        'End While
        partialOrganDoseAbs(iorgan) = absDose(iorgan, jEnd(iorgan) - 1)

        '  For j = jstart To (jEnd(i) - 1)
        'partialOrganDoseRel(i) = partialOrganDoseRel(i) + relDose(i, j)
        ' partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, j) - organVol(i, j + 1)) * absDose(i, j + 1)
        ' Next
        'If organList(i, 2) = "Length of Cord" Then
        'If fraction = "1.0" Then partialOrganDoseAbsString(i) = "20 cm"
        ' If fraction = "0.666667" Then partialOrganDoseAbsString(i) = "10 cm"
        ' If fraction = "0.333333" Then partialOrganDoseAbsString(i) = "5 cm"
        '  Else

        If doseunit Then                                                                        ' converts dose to Gy if planning system units is cGy
            partialOrganDoseAbs(iorgan) = Convert.ToSingle(partialOrganDoseAbs(iorgan) / 100)
        Else
            partialOrganDoseAbs(iorgan) = Convert.ToSingle(partialOrganDoseAbs(iorgan))
        End If
        partialOrganDoseAbsString = Convert.ToString(partialOrganDoseAbs(iorgan))

        'End If

        'If organList(i, 1) = "Spinal Cord" Then ' this calculates dose for a 5cm length of cord
        'jLength = 1
        '  cordLength(2) = 0
        '  partialOrganDoseAbs(i) = 0
        ' Do Until cordLength(2) > 5 Or jLength > (jEnd(i) - 1)
        '  cordLength(2) = (organVol(i, 1) - organVol(i, jLength + 1)) / 1.2
        '   partialOrganDoseAbs(i) = partialOrganDoseAbs(i) + (organVol(i, jLength) - organVol(i, jLength + 1)) * absDose(i, jLength + 1) 'this replaces the previous calculation results
        '   jLength = jLength + 1
        '   If jLength > (jTotal(i) - 1) Then onethirdOrganDoseAbsString(i - 1) = MsgBox(cordLength(2).ToString("0.0"))
        '    Loop
        '    End If

        'Next


        Return partialOrganDoseAbsString
    End Function
End Class
