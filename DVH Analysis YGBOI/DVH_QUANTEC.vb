Public Class DVH_QUANTEC

    Public Sub DVHQuantecSub(ByVal fileThatWasRead)
        Dim lengthTest(20, 9) As Boolean
        Dim kidneynum As Integer = 0
        Dim foundcenter As Boolean = False
        Dim bilatkidneys As Boolean = False
        Dim lungtotal As Boolean = False
        Dim kidney1 As Boolean = False
        Dim kidney2 As Boolean = False
        Dim kidney2meandose, kidney1meandose As Single
        Dim iVolume, partialOrganVol, planvalue, refvalue, lungNameTest As String
        Dim irow, icolumn, i, j, iArray, iArrayLastMean, iArrayLastMax, rowTest(12), iHeight As Integer
        Dim organsFound(100, 2), organNames(100), organ, name As String
        Dim volQuantecResult(20), meanQuantecResult(20), maxQuantecResult(20), rowDGV(11) As String
        Dim dataQuantec As New DataQuantec
        Dim patientOrganList As New FindOrgans
        Dim partialOrganVolDose(100), partialOrganVolume(100), partialOrganMeanDose(100, 20), partialOrganMaxDose(100, 20) As String
        Dim cellchar() As Char

        MessageForm.Show()

        organsFound = patientOrganList.findOrgans(fileThatWasRead)

        '  find out if a bilateral kidney was created and if either kidney > 18 Gy
        i = 1
        Do Until organsFound(i, 1) Is Nothing

            If organsFound(i, 1) = "Kidneys - Bilateral" Then bilatkidneys = True
            If organsFound(i, 1) = "Kidney" Then
                If kidneynum = 1 Then
                    kidney2meandose = patientOrganList.meanOrganDose(1, i)
                    If kidney2meandose > 18 Then kidney2 = True
                    '     MsgBox("kidney2: " & kidney2)
                Else
                    kidney1meandose = patientOrganList.meanOrganDose(1, i)
                    If kidney1meandose > 18 Then kidney1 = True
                    '     MsgBox("kidney1: " & kidney1)
                    kidneynum = kidneynum + 1
                End If
            End If

            If organsFound(i, 1) = "Lung - Total" Then lungtotal = True ' used in order to not show the results for rt/lt lungs

            i = i + 1
        Loop
        kidneynum = 0

        '  find out if either kidney is > 18 Gy

        If meanQuantecResult.Length <> 11 And iArray + 1 < meanQuantecResult.Length Then                                          ' fraction size of reference volume
            rowDGV(5) = patientOrganList.meanOrganDose(1, i) & " Gy"                                                            ' the mean dose from the plan
        End If

        TextBox1.Text = FindOrgans.patientName
        TextBox2.Text = FindOrgans.patientMR
        TextBox3.Text = FindOrgans.planName

        i = 1
        j = 1
        iHeight = 0
        iArray = 1                                                                                      ' starts at 1 since first entry 0 is name of organ
        iArrayLastMean = 1
        iArrayLastMax = 1
        organ = organsFound(i, 1)
        ' MsgBox(organ)
        name = organsFound(i, 2)
        lungNameTest = LCase(name)
        Do Until organ Is Nothing
            volQuantecResult = dataQuantec.volumeQuantecData(organ)                                     ' for each organ, find volume constraints if any
            meanQuantecResult = dataQuantec.meanQuantecData(organ)                                      ' for each organ, find mean constraints if any
            '  MsgBox(organ)
            'If organ = "Kidney" Then MsgBox("meanQuantecResult.Length: " & volQuantecResult.Length)
            maxQuantecResult = dataQuantec.maxQuantecData(organ)                                        ' for each organ, find max constraints if any

            ' The section below searches for data. The If statement starts the search using the While LoopVolume to look for
            ' volume data for the current organ. For all the volume entries available and creates a row for each. At the 
            ' same time it looks to see if there are mean and/or max entries also and puts those in each row created, just 
            ' repeating the data per row. If there are no Volume data for the organ then the first Else statement starts 
            ' a new If statement starts a search for Mean data adding in as many rows as needed and putting in any max 
            ' data found. The Else statement for the second IF does a final search for Max data. The expressions of the form
            ' volQuantecResult.Length <> 21(11) are needed since if there is no Organ for the query, the array length will
            ' have it's default array size which is 21 for volume and 11 for mean and max (more constraint data for vol than mean or max).

            If organ = "Kidney" And Not kidney1 And Not kidney2 And bilatkidneys And kidneynum = 0 Then GoTo 100 ' this will skip all the single kidney entries if mean dose to either kidney < 18 Gy
            If lungtotal And lungNameTest.Contains("lung") And (Not lungNameTest.Contains("total") Or
                Not lungNameTest.Contains("tot")) Then GoTo 100

            'If (volQuantecResult.Length <> 21 And iArray + 1 < volQuantecResult.Length) Or
            '    (meanQuantecResult.Length <> 11 And iArray + 1 < meanQuantecResult.Length) Or
            '    (maxQuantecResult.Length <> 11 And iArray + 1 < maxQuantecResult.Length) Then              ' if array is empty, it will have length 21. Array+1 since array maximum is always one less than array length

            While (volQuantecResult.Length <> 21 And iArray + 1 < volQuantecResult.Length) Or
            (meanQuantecResult.Length <> 11 And iArray + 1 < meanQuantecResult.Length) Or
            (maxQuantecResult.Length <> 11 And iArray + 1 < maxQuantecResult.Length)            ' This looks for all the volume entries that may be present for an organ
                rowDGV = New String() {"", "", "", "", "", "", "", "", "", "", ""}
                If volQuantecResult(0) <> "" Then
                    rowDGV(0) = name & " (" & volQuantecResult(0) & ")"
                Else
                    rowDGV(0) = name
                End If                                                                              ' first enrty in row is the organ name

                If (volQuantecResult.Length <> 21 And iArray + 1 < volQuantecResult.Length) Then
                    rowDGV(1) = "V" & volQuantecResult(iArray)                                          ' second entry in row is the volume constraint in the form "V50" etc
                    If volQuantecResult(iArray + 1).Contains("%") Then                                  ' determine if the constraint is given in %
                        iVolume = volQuantecResult(iArray) & "%"                                        ' the % is added to tell the sub volumeCoveredbyDoseLine(iVolume, i) in FindOrgans that the constraint is % and not cc
                        partialOrganVol = patientOrganList.volumeCoveredbyDoseLine(iVolume, i)          ' calls the "volumeCoveredbyDoseLine" subroutine in FindOrgans for current organ
                        rowDGV(2) = FormatNumber(Convert.ToSingle(partialOrganVol) * 100, 1) & "%"      ' third entry in row is the plan constraint value convert to % and format to one decimal place
                        rowDGV(3) = "< " & volQuantecResult(iArray + 1)                                 ' fourth entry in row is the  reference constraint value
                    Else
                        iVolume = volQuantecResult(iArray)                                              ' same data except given in cc's
                        partialOrganVol = patientOrganList.volumeCoveredbyDoseLine(iVolume, i)
                        rowDGV(2) = FormatNumber(Convert.ToSingle(partialOrganVol), 1) & " cc"
                        rowDGV(3) = "< " & volQuantecResult(iArray + 1)
                    End If
                    rowDGV(4) = volQuantecResult(iArray + 2)                                            ' fifth entry in row is the reference
                End If
                ' this will evaluate the mean if it is a simultaneous constraint

                If meanQuantecResult.Length <> 11 And iArray + 1 < meanQuantecResult.Length Then
                    iVolume = meanQuantecResult(iArray)                                             ' fraction size of reference volume
                    rowDGV(6) = meanQuantecResult(iArray + 1) & " Gy"                               ' the constraint
                    rowDGV(7) = meanQuantecResult(iArray + 2)                                       ' the reference
                    If kidney1 And organ = "Kidney" Then                                            ' shows the mean dose if 18 Gy limit exceeded
                        rowDGV(5) = FormatNumber(kidney1meandose, 2) & " Gy"
                        kidney1 = False
                        kidneynum = 1
                    Else
                        If kidney2 And organ = "Kidney" Then                                        ' shows the mean dose if 18 Gy limit exceeded
                            rowDGV(5) = FormatNumber(kidney2meandose, 2) & " Gy"
                            kidney2 = False
                            kidneynum = 1
                        Else
                            rowDGV(5) = FormatNumber(patientOrganList.meanOrganDose(iVolume, i), 2) & " Gy" ' the mean dose from the plan
                        End If
                    End If
                End If
                ' this will evaluate the max if it is a simultaneous constraint

                If maxQuantecResult.Length <> 11 And iArray + 1 < maxQuantecResult.Length Then
                    ' MsgBox("maxQuantecResult1")
                    iVolume = maxQuantecResult(iArray)                                              ' fraction size of reference volume
                    rowDGV(9) = maxQuantecResult(iArray + 1) & " Gy"                                ' the constraint
                    rowDGV(10) = maxQuantecResult(iArray + 2)                                       ' the reference 
                    rowDGV(8) = FormatNumber(patientOrganList.maxOrganDose(iVolume, i), 2) & " Gy"  ' the max dose from the plan
                End If
                DGV.Rows.Add(rowDGV)
                iArray = iArray + 3
                iHeight = iHeight + 1
            End While
            'Else

            '' mean data

            'If meanQuantecResult.Length <> 11 And iArray + 1 < meanQuantecResult.Length Then
            '    'MsgBox("meanQuantecResult2")
            '    While meanQuantecResult.Length <> 11 And iArray + 1 < meanQuantecResult.Length      ' This looks for all the mean entries that may be present for an organ
            '        rowDGV = New String() {"", "", "", "", "", "", "", "", "", "", ""}
            '        iVolume = meanQuantecResult(iArray)   ' 
            '        If meanQuantecResult(0) <> "" Then
            '            rowDGV(0) = name & " (" & meanQuantecResult(0) & ")"
            '        Else
            '            rowDGV(0) = name
            '        End If
            '        rowDGV(6) = meanQuantecResult(iArray + 1) & " Gy"                               ' the constraint
            '        rowDGV(7) = meanQuantecResult(iArray + 2)                                       ' the reference
            '        rowDGV(5) = FormatNumber(patientOrganList.meanOrganDose(iVolume, i), 1) & " Gy" ' the mean dose from the plan
            '        iArray = iArray + 3
            '        DGV.Rows.Add(rowDGV)
            '        iHeight = iHeight + 1

            '    End While
            'Else

            '    '   max data

            '    If maxQuantecResult.Length <> 11 And iArray + 1 < maxQuantecResult.Length Then
            '        'MsgBox("maxQuantecResult2")
            '        While maxQuantecResult.Length <> 11 And iArray + 1 < maxQuantecResult.Length
            '            rowDGV = New String() {"", "", "", "", "", "", "", "", "", "", ""}
            '            iVolume = maxQuantecResult(iArray)                                          ' fraction size of reference volume
            '            If meanQuantecResult(0) <> "" Then
            '                rowDGV(0) = name & " (" & maxQuantecResult(0) & ")"
            '            Else
            '                rowDGV(0) = name
            '            End If
            '            rowDGV(9) = maxQuantecResult(iArray + 1) & " Gy"                            ' the constraint
            '            rowDGV(10) = maxQuantecResult(iArray + 2)                                   ' the reference 
            '            rowDGV(8) = FormatNumber(patientOrganList.maxOrganDose(iVolume, i), 1) & " Gy"  '  the max dose from the plan
            '            iArray = iArray + 3
            '            DGV.Rows.Add(rowDGV)
            '            iHeight = iHeight + 1
            '        End While
            '    End If
            'End If
            'End If
            ' If organsFound(i, 1) <> "Length of Cord" Then DGV.Rows.Add(rowDGV)
100:        iArray = 1
            i = i + 1
            organ = organsFound(i, 1)
            name = organsFound(i, 2)
            lungNameTest = LCase(name)
        Loop

        ' what follows colours the cells according to pass/fail/no ref values/special data

        Me.Width = 5000                                                                            ' setting the width of the display window
        Me.Height = 172 + 22 * (iHeight - 1)                                                        ' setting the height based on number of rows
        MessageForm.Close()

        'this colours the row with "cm" in BlanchedAlmond and the cell "Spinal Cord"
        'If DGV.Rows(irow).Cells(1).Value.contains("cm") Then
        '    DGV.Rows(irow + 1).Cells(0).Style.BackColor = Color.BlanchedAlmond
        '    For icolumn = 0 To 12
        '        DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.BlanchedAlmond
        '        DGV.Rows(irow).Cells(icolumn).Style.Font = New Font("Arial", 10, FontStyle.Bold)
        '    Next

        '    'this determines if the calculated cord length is less than the reference value
        '    If DGV.Rows(irow).Cells(1).Value <> "20 cm" Then lengthTest(irow + 1, 1) = True
        '    If DGV.Rows(irow).Cells(2).Value <> "10 cm" Then lengthTest(irow + 1, 2) = True
        '    If DGV.Rows(irow).Cells(3).Value <> "5 cm" Then lengthTest(irow + 1, 3) = True

        '    If DGV.Rows(irow).Cells(7).Value <> "20 cm" Then lengthTest(irow + 1, 7) = True
        '    If DGV.Rows(irow).Cells(8).Value <> "10 cm" Then lengthTest(irow + 1, 8) = True
        '    If DGV.Rows(irow).Cells(9).Value <> "5 cm" Then lengthTest(irow + 1, 9) = True

        'End If
        For irow = 0 To iHeight - 1
            For icolumn = 2 To 8 Step 3
                refvalue = Nothing
                planvalue = DGV.Rows(irow).Cells(icolumn).Value
                If planvalue <> Nothing Then
                    planvalue = planvalue.Replace("Gy", "")
                    planvalue = planvalue.Replace("%", "")
                    planvalue = Trim(planvalue.Replace("cc", ""))
                End If
                refvalue = DGV.Rows(irow).Cells(icolumn + 1).Value
                If refvalue <> Nothing Then
                    refvalue = refvalue.Replace("Gy", "")
                    refvalue = refvalue.Replace("%", "")
                    refvalue = refvalue.Replace("cc", "")
                    refvalue = refvalue.Replace("=", "")
                    refvalue = refvalue.Replace("<", "")
                    If refvalue.Contains("-") Then
                        cellchar = refvalue.ToCharArray()
                        refvalue = Nothing
                        foundcenter = False
                        For Each ch As Char In cellchar                                         ' for constraints such as 10-15%. Finds the higher constraint.
                            If Char.IsNumber(ch) And foundcenter Then refvalue = refvalue & ch
                            If ch = "-" Then foundcenter = True
                        Next
                    End If
                End If
                If planvalue <> Nothing Then
                    If Convert.ToSingle(planvalue) < Convert.ToSingle(refvalue) Then
                        DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightCyan
                    Else
                        DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.Red
                    End If
                Else
                    DGV.Rows(irow).Cells(icolumn).Value = "--"
                End If
            Next
        Next
        'For icolumn = 7 To 9
        '    Try
        '        If DGV.Rows(irow).Cells(1).Value.contains("cm") = False Then
        '            If Convert.ToSingle(DGV.Rows(irow).Cells(icolumn).Value) < Convert.ToSingle(DGV.Rows(irow).Cells(icolumn + 3).Value) Then
        '                DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightCyan
        '                If lengthTest(irow, icolumn) = True Then DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightSlateGray
        '            Else
        '                DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.Red
        '            End If
        '        End If
        '    Catch
        '        DGV.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightSlateGray
        '    End Try
        'Next

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tempColor1 As New Color
        Dim tempColor2 As New Color
        tempColor1 = DGV.BackgroundColor
        tempColor2 = Panel1.BackColor

        DGV.BackgroundColor = Color.White
        Panel1.BackColor = Color.White

        Dim res As DialogResult = Me.VBPBNPrintDialog.ShowDialog

        If res = Windows.Forms.DialogResult.OK Then
            Try
                ' Copy the settings from the dialog
                Me.VBPBNPrintForm.PrinterSettings = Me.VBPBNPrintDialog.PrinterSettings
                If Me.VBPBNPrintForm.PrinterSettings.PrintToFile = True Then
                    Me.VBPBNPrintForm.PrinterSettings.DefaultPageSettings.Margins.Left = 0.5
                    Me.VBPBNPrintForm.PrinterSettings.DefaultPageSettings.Margins.Right = 0.5
                    Me.VBPBNPrintForm.PrinterSettings.DefaultPageSettings.Margins.Top = 0.75
                    Me.VBPBNPrintForm.PrintAction = Drawing.Printing.PrintAction.PrintToFile
                    Me.VBPBNPrintForm.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
                Else
                    Me.VBPBNPrintForm.PrinterSettings.DefaultPageSettings.Margins.Left = 0.5
                    Me.VBPBNPrintForm.PrinterSettings.DefaultPageSettings.Margins.Right = 0.5
                    Me.VBPBNPrintForm.PrinterSettings.DefaultPageSettings.Margins.Top = 0.75
                    Me.VBPBNPrintForm.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter
                    Me.VBPBNPrintForm.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
                End If
            Catch ex As Exception
            Finally
            End Try
        End If

        DGV.BackgroundColor = tempColor1
        Panel1.BackColor = tempColor2
    End Sub

    Private Sub DGV_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles DGV.Paint


        Dim xPos As Integer
        Dim position As New System.Drawing.Point()
        position.Y = 49

        xPos = ((Me.DGV.GetCellDisplayRectangle(1, -1, True).Left + Me.DGV.GetCellDisplayRectangle(4, -1, True).Right) / 2) - (Me.Label3.Width / 2)
        position.X = xPos
        Me.Label3.Location = (position)

        xPos = ((Me.DGV.GetCellDisplayRectangle(5, -1, True).Left + Me.DGV.GetCellDisplayRectangle(7, -1, True).Right) / 2) - (Me.Label4.Width / 2)
        position.X = xPos
        Me.Label4.Location = (position)

        xPos = ((Me.DGV.GetCellDisplayRectangle(8, -1, True).Left + Me.DGV.GetCellDisplayRectangle(10, -1, True).Right) / 2) - (Me.Label5.Width / 2)
        position.X = xPos
        Me.Label5.Location = (position)
    End Sub

    Private Sub Form_Load() Handles Me.Load
        ExtensionMethods.DoubleBuffer(DGV, True)
    End Sub

End Class

Public Module ExtensionMethods
    Public Sub DoubleBuffer(ByVal dgv As DataGridView, ByVal setting As Boolean)
        Dim dgvType As Type = dgv.[GetType]()
        Dim pi As System.Reflection.PropertyInfo = dgvType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic)
        'pi.SetValue(dgv, setting, Nothing)
    End Sub
End Module