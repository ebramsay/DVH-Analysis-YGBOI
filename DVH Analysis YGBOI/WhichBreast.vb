Public Class WhichBreast
    Dim choice As String
    Dim buttonpressed As Boolean = False
    Function choicertlt()
        Me.Show()
        ' Test comment
        Do Until buttonpressed
            If RadioButton1.Checked = True Then choice = "right"
            If RadioButton2.Checked = True Then choice = "left"
            Application.DoEvents()
        Loop
        Return choice
    End Function
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        buttonpressed = True
        Me.Close()
    End Sub
End Class