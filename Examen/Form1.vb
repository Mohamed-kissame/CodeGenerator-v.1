Public Class Form1


    Private float


    Private Sub btnMoyenne_Click(sender As Object, e As EventArgs) Handles btnMoyenne.Click


        txtMoyenne.Text = ((Convert.ToSingle(txtFrontEndNote.Text) + Convert.ToSingle(txtNote_SGbd.Text) + Convert.ToSingle(txtNoteProgrammation.Text)) / 3)




    End Sub

    Private Sub btnDecison_Click(sender As Object, e As EventArgs) Handles btnDecison.Click

        If (txtMoyenne.Text >= 10) Then

            txtDecison.Text = "Admis"

        Else

            txtDecison.Text = "No Admis"



        End If



    End Sub
End Class
