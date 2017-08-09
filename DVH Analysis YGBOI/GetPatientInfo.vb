'   This class contains three subroutines. The three subroutines all respond to button clicks
'   on the main page of the program. browse_click lets the user browse to the file containing the patient
'   of interest and open. patientName_Click allows the user to enter the patient name directly. DVHAnalysis_Click
'   alternately shows or closes the form DVHAnalysis on which all results will appear.

Imports System.IO
Public Class GetPatientInfo
    Public Shared locationOfPatient As String
    Public Shared patientFileData As System.IO.StreamReader
    Dim testFileReader As System.IO.StreamReader
    Dim fileThatWasRead As String
    Dim nameOfPatient As String
    Dim patientFile, test As String
    Dim checkPatientSelected As Boolean = False
    Dim patientFilesLocation As String = "U:\Trilogy 4829\DVH Exports\"

    'Private Sub Form_Start() Handles Me.Load
    'patientFilesLocation = InputBox("Location: ", "Enter the Location of the Patient Files", patientFilesLocation)
    'If Not patientFilesLocation.EndsWith("\") Then
    'patientFilesLocation = patientFilesLocation & "\"
    ' End If
    'End Sub

    Public Sub browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles browse.Click

        OpenPatientDialog.Filter = "All Files|*.*|Text Files|*.txt"
        OpenPatientDialog.Title = "Select Patient File to Open"
        OpenPatientDialog.InitialDirectory = patientFilesLocation
        If OpenPatientDialog.ShowDialog() = DialogResult.OK Then
            locationOfPatient = OpenPatientDialog.FileName

            'section below checks that DVH data is in Absolute Dose and Absolute Volume
            testFileReader = New StreamReader(locationOfPatient) 'System.IO.File.OpenText(GetPatientInfo.locationOfPatient)
            test = testFileReader.ReadLine()
            Do Until test.Contains("Min Dose") = True
                test = testFileReader.ReadLine()
            Loop
            If test.Contains("Min Dose [%]") = True Then MsgBox("DVH must be in Absolute Dose and Absolute Volume")
            If test.Contains("Min Dose [%]") = True Then GoTo 100

            Do Until test.Contains("Structure Volume") = True
                test = testFileReader.ReadLine()
            Loop
            If test.Contains("Structure Volume [%]") = True Then MsgBox("DVH must be in Absolute Dose and Absolute Volume")
            If test.Contains("Structure Volume [%]") = True Then GoTo 100

            fileThatWasRead = Nothing                                                   ' just making sure string is clean again

            ' if DVH data format is correct, read in entire file
            MessageForm.Show()
            Cursor.Current = Cursors.WaitCursor
            patientFileData = New StreamReader(locationOfPatient)
            fileThatWasRead = patientFileData.ReadToEnd()
            PatientDataTextBox.Text = fileThatWasRead
            patientFileData.Close()
            checkPatientSelected = True
            MessageForm.Close()
        End If

        DVH_Emami.Close()
        DVH_QUANTEC.Close()
100:
    End Sub

    Public Sub patientName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles patientName.Click
        Dim fileInfo As System.IO.FileInfo
        Dim errorText As String = ""

        'Loops until the user enters a patient name that is found in the directory contained in patientFilesLocation (which is set at the start of the program)
        Do
            nameOfPatient = InputBox(errorText, "Enter Patient Name", "")
            fileInfo = New System.IO.FileInfo(patientFilesLocation & nameOfPatient)

            If Not fileInfo.Exists And Not nameOfPatient.Equals("") Then
                errorText = "That patient could not be found. A misspelling may have been made."
            End If
        Loop While Not fileInfo.Exists

        locationOfPatient = patientFilesLocation & nameOfPatient
        'C:\Users\Eric\Desktop\Patient DVH's

        'section below checks that DVH data is in Absolute Dose and Absolute Volume
        testFileReader = New StreamReader(locationOfPatient) 'System.IO.File.OpenText(GetPatientInfo.locationOfPatient)
        test = testFileReader.ReadLine()
        Do Until test.Contains("Min Dose") = True
            test = testFileReader.ReadLine()
        Loop
        If test.Contains("Min Dose [%]") = True Then MsgBox("DVH must be in Absolute Dose and Absolute Volume")
        If test.Contains("Min Dose [%]") = True Then Return

        Do Until test.Contains("Structure Volume") = True
            test = testFileReader.ReadLine()
        Loop
        If test.Contains("Structure Volume [%]") = True Then MsgBox("DVH must be in Absolute Dose and Absolute Volume")
        If test.Contains("Structure Volume [%]") = True Then Return

        fileThatWasRead = Nothing      ' making sure string is clean

        MessageForm.ShowDialog()
        Cursor.Current = Cursors.WaitCursor
        patientFileData = New StreamReader(locationOfPatient)
        fileThatWasRead = patientFileData.ReadToEnd()
        PatientDataTextBox.Text = fileThatWasRead
        patientFileData.Close()
        checkPatientSelected = True
        MessageForm.Close()

        DVH_Emami.Close()
        DVH_QUANTEC.Close()
    End Sub

    Private Sub DVHAnalysisEmami_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DVH_Analysis_Emami.Click
        Cursor.Current = Cursors.WaitCursor
        DVH_Emami.Close() 'closed in case user wants to open another patient
        If checkPatientSelected = True Then
            DVH_Emami.DVHEmamiSub(fileThatWasRead)
            DVH_Emami.Show()
        Else
            If MsgBox("Select a patient", 5, "Patient Selection") = MsgBoxResult.Cancel Then Me.Close()
        End If
    End Sub

    Private Sub DVHAnalysisQUANTEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        DVH_QUANTEC.Close() 'closed in case user wants to open another patient
        If checkPatientSelected = True Then
            DVH_QUANTEC.DVHQuantecSub(fileThatWasRead)
            DVH_QUANTEC.TopLevel = False
            Me.SplitContainer1.Panel2.Controls().Add(DVH_QUANTEC)
            DVH_QUANTEC.Show()

        Else
            If MsgBox("Select a patient", 5, "Patient Selection") = MsgBoxResult.Cancel Then Me.Close()
        End If
    End Sub
    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint
        Dim minSize As New System.Drawing.Point
        minSize.X = 400
        minSize.Y = 560
        SplitContainer1.Panel1.MinimumSize = minSize
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SelectTx.Close()
        SelectTx.Visible = True
    End Sub

End Class