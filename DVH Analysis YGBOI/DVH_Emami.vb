'   This class is called from the class GetPatientInfo and it uses the organ names passed from the function
'   findOrgans in the FindOrgans class to get the doses from the Emami database which is stored in class DataEmami.
'   Afer finding the doses for all organs present in the patient file, a DataGridView is then populated and 
'   shown on the form called DVHAnalysis

Public Class DVH_Emami
    Public Sub DVHEmamiSub(fileThatWasRead)
        Dim lengthTest(20, 9) As Boolean
        Dim meanDose As Integer
        Dim irow, icolumn, i, rowTest(12) As Integer
        Dim organsFound(100, 2), organNames(100), organ, name As String
        Dim rowDVH(13) As String
        Dim emamiData As New DataEmami
        Dim patientOrganList As New FindOrgans
        Dim wholeOrganDose(100), onethirdOrganDose(100), twothirdOrganDose(100), partialOrganDose(100) As String
        MessageForm.Show()
        DataGridView1.ColumnCount = 13
        DataGridView1.Columns(0).Width = 165
        DataGridView1.Columns(1).Width = 71
        DataGridView1.Columns(2).Width = 71
        DataGridView1.Columns(3).Width = 71
        DataGridView1.Columns(4).Width = 71
        DataGridView1.Columns(5).Width = 71
        DataGridView1.Columns(6).Width = 71
        DataGridView1.Columns(7).Width = 71
        DataGridView1.Columns(8).Width = 71
        DataGridView1.Columns(9).Width = 71
        DataGridView1.Columns(10).Width = 71
        DataGridView1.Columns(11).Width = 71
        DataGridView1.Columns(12).Width = 71

        DataGridView1.Columns(0).Name = "   Organ"
        DataGridView1.Columns(1).Name = "   Whole"
        DataGridView1.Columns(2).Name = "   2/3"
        DataGridView1.Columns(3).Name = "   1/3"
        DataGridView1.Columns(4).Name = "   Whole"
        DataGridView1.Columns(5).Name = "   2/3"
        DataGridView1.Columns(6).Name = "   1/3"
        DataGridView1.Columns(7).Name = "   Whole"
        DataGridView1.Columns(8).Name = "   2/3"
        DataGridView1.Columns(9).Name = "   1/3"
        DataGridView1.Columns(10).Name = "   Whole"
        DataGridView1.Columns(11).Name = "   2/3"
        DataGridView1.Columns(12).Name = "   1/3"

        rowDVH = New String() {"", "", "", "", "", "", "", "", "", "", "", "", ""}

        meanDose = MsgBox("Calcuate Mean Doses?", 3, )

        If meanDose = 6 Then
            organsFound = patientOrganList.findOrgans(fileThatWasRead)
            wholeOrganDose = patientOrganList.partialOrganDoseMean(1.0)
            onethirdOrganDose = patientOrganList.partialOrganDoseMean(0.333333)
            twothirdOrganDose = patientOrganList.partialOrganDoseMean(0.666667)

        Else
            organsFound = patientOrganList.findOrgans(fileThatWasRead)
            wholeOrganDose = patientOrganList.linePartialOrganDVH(1.0)
            onethirdOrganDose = patientOrganList.linePartialOrganDVH(0.333333)
            twothirdOrganDose = patientOrganList.linePartialOrganDVH(0.666667)

        End If

        TextBox1.Text = FindOrgans.patientName
        TextBox2.Text = FindOrgans.patientMR
        TextBox3.Text = FindOrgans.planName

        i = 1
        organ = organsFound(i, 1)
        name = organsFound(i, 2)
        Do Until organ Is Nothing
            If organ = "Ear" Then                                               ' Ear has two constraints. The first part of the IF allows both to be shown.
                rowDVH(0) = name & " (acute)"
                rowDVH(1) = wholeOrganDose(i)
                rowDVH(2) = twothirdOrganDose(i)
                rowDVH(3) = onethirdOrganDose(i)
                rowDVH(4) = emamiData.whole55("Ear (acute)")
                rowDVH(5) = emamiData.twothird55("Ear (acute)")
                rowDVH(6) = emamiData.onethird55("Ear (acute)")
                rowDVH(7) = wholeOrganDose(i)
                rowDVH(8) = twothirdOrganDose(i)
                rowDVH(9) = onethirdOrganDose(i)
                rowDVH(10) = emamiData.whole505("Ear (acute)")
                rowDVH(11) = emamiData.twothird505("Ear (acute)")
                rowDVH(12) = emamiData.onethird505("Ear (acute)")
                If name <> Nothing Then DataGridView1.Rows.Add(rowDVH)

                rowDVH(0) = name & " (chronic)"
                rowDVH(1) = wholeOrganDose(i)
                rowDVH(2) = twothirdOrganDose(i)
                rowDVH(3) = onethirdOrganDose(i)
                rowDVH(4) = emamiData.whole55("Ear (chronic)")
                rowDVH(5) = emamiData.twothird55("Ear (chronic)")
                rowDVH(6) = emamiData.onethird55("Ear (chronic)")
                rowDVH(7) = wholeOrganDose(i)
                rowDVH(8) = twothirdOrganDose(i)
                rowDVH(9) = onethirdOrganDose(i)
                rowDVH(10) = emamiData.whole505("Ear (chronic)")
                rowDVH(11) = emamiData.twothird505("Ear (chronic)")
                rowDVH(12) = emamiData.onethird505("Ear (chronic)")
                If name <> Nothing Then DataGridView1.Rows.Add(rowDVH)

            Else
                rowDVH(0) = name
                rowDVH(1) = wholeOrganDose(i)
                rowDVH(2) = twothirdOrganDose(i)
                rowDVH(3) = onethirdOrganDose(i)
                rowDVH(4) = emamiData.whole55(organ)
                rowDVH(5) = emamiData.twothird55(organ)
                rowDVH(6) = emamiData.onethird55(organ)
                rowDVH(7) = wholeOrganDose(i)
                rowDVH(8) = twothirdOrganDose(i)
                rowDVH(9) = onethirdOrganDose(i)
                rowDVH(10) = emamiData.whole505(organ)
                rowDVH(11) = emamiData.twothird505(organ)
                rowDVH(12) = emamiData.onethird505(organ)
                If name <> Nothing Then DataGridView1.Rows.Add(rowDVH)
            End If
            i = i + 1
            organ = organsFound(i, 1)
            name = organsFound(i, 2)
        Loop

        ' what follows colours the cells according to pass/fail/no ref values/special data

        Me.Width = 1028                                 ' setting the width of the display window
        Me.Height = 150 + 22 * (i - 2)                  ' setting the height of the display window depending on number of rows
        For irow = 0 To DataGridView1.Rows.Count - 1

            'this colours the row with "cm" in BlanchedAlmond and the cell "Spinal Cord"
            If DataGridView1.Rows(irow).Cells(1).Value.contains("cm") Then
                DataGridView1.Rows(irow + 1).Cells(0).Style.BackColor = Color.BlanchedAlmond
                For icolumn = 0 To 12
                    DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.BlanchedAlmond
                    DataGridView1.Rows(irow).Cells(icolumn).Style.Font = New Font("Arial", 10, FontStyle.Bold)
                Next

                'this determines if the calculated cord length is less than the reference value
                If DataGridView1.Rows(irow).Cells(1).Value <> "20 cm" Then lengthTest(irow + 1, 1) = True
                If DataGridView1.Rows(irow).Cells(2).Value <> "10 cm" Then lengthTest(irow + 1, 2) = True
                If DataGridView1.Rows(irow).Cells(3).Value <> "5 cm" Then lengthTest(irow + 1, 3) = True

                If DataGridView1.Rows(irow).Cells(7).Value <> "20 cm" Then lengthTest(irow + 1, 7) = True
                If DataGridView1.Rows(irow).Cells(8).Value <> "10 cm" Then lengthTest(irow + 1, 8) = True
                If DataGridView1.Rows(irow).Cells(9).Value <> "5 cm" Then lengthTest(irow + 1, 9) = True

            End If

            For icolumn = 1 To 3
                Try
                    If DataGridView1.Rows(irow).Cells(1).Value.contains("cm") = False Then
                        If Convert.ToSingle(DataGridView1.Rows(irow).Cells(icolumn).Value) < Convert.ToSingle(DataGridView1.Rows(irow).Cells(icolumn + 3).Value) Then
                            DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightCyan
                            If lengthTest(irow, icolumn) = True Then DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightSlateGray
                        Else
                            DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.Red
                        End If
                    End If
                Catch
                    DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightSlateGray
                End Try
            Next

            For icolumn = 7 To 9
                Try
                    If DataGridView1.Rows(irow).Cells(1).Value.contains("cm") = False Then
                        If Convert.ToSingle(DataGridView1.Rows(irow).Cells(icolumn).Value) < Convert.ToSingle(DataGridView1.Rows(irow).Cells(icolumn + 3).Value) Then
                            DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightCyan
                            If lengthTest(irow, icolumn) = True Then DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightSlateGray
                        Else
                            DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.Red
                        End If
                    End If
                Catch
                    DataGridView1.Rows(irow).Cells(icolumn).Style.BackColor = Color.LightSlateGray
                End Try
            Next
        Next
        MessageForm.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim res As DialogResult = Me.EmamiPrintDialog.ShowDialog

        If res = Windows.Forms.DialogResult.OK Then
            Try
                ' Copy the settings from the dialog
                Me.EmamiPrintForm.PrinterSettings = Me.EmamiPrintDialog.PrinterSettings
                If Me.EmamiPrintForm.PrinterSettings.PrintToFile = True Then
                    Me.EmamiPrintForm.PrinterSettings.DefaultPageSettings.Margins.Left = 0.5
                    Me.EmamiPrintForm.PrinterSettings.DefaultPageSettings.Margins.Right = 0.5
                    Me.EmamiPrintForm.PrinterSettings.DefaultPageSettings.Margins.Top = 0.75
                    Me.EmamiPrintForm.PrintAction = Drawing.Printing.PrintAction.PrintToFile
                    Me.EmamiPrintForm.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
                Else
                    Me.EmamiPrintForm.PrinterSettings.DefaultPageSettings.Margins.Left = 0.5
                    Me.EmamiPrintForm.PrinterSettings.DefaultPageSettings.Margins.Right = 0.5
                    Me.EmamiPrintForm.PrinterSettings.DefaultPageSettings.Margins.Top = 0.75
                    Me.EmamiPrintForm.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter
                    Me.EmamiPrintForm.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly)
                End If
            Catch ex As Exception
            Finally
            End Try
        End If
    End Sub
End Class