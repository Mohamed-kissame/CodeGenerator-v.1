<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        btnMoyenne = New Button()
        btnDecison = New Button()
        txtFrontEndNote = New TextBox()
        txtNote_SGbd = New TextBox()
        txtNoteProgrammation = New TextBox()
        txtMoyenne = New TextBox()
        txtDecison = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' btnMoyenne
        ' 
        btnMoyenne.Location = New Point(70, 283)
        btnMoyenne.Name = "btnMoyenne"
        btnMoyenne.Size = New Size(135, 38)
        btnMoyenne.TabIndex = 0
        btnMoyenne.Text = "Afficher Le Moyenne "
        btnMoyenne.UseVisualStyleBackColor = True
        ' 
        ' btnDecison
        ' 
        btnDecison.Location = New Point(70, 357)
        btnDecison.Name = "btnDecison"
        btnDecison.Size = New Size(135, 38)
        btnDecison.TabIndex = 1
        btnDecison.Text = "Afficher la Decison"
        btnDecison.UseVisualStyleBackColor = True
        ' 
        ' txtFrontEndNote
        ' 
        txtFrontEndNote.Location = New Point(333, 88)
        txtFrontEndNote.Name = "txtFrontEndNote"
        txtFrontEndNote.Size = New Size(152, 23)
        txtFrontEndNote.TabIndex = 2
        ' 
        ' txtNote_SGbd
        ' 
        txtNote_SGbd.Location = New Point(333, 153)
        txtNote_SGbd.Name = "txtNote_SGbd"
        txtNote_SGbd.Size = New Size(152, 23)
        txtNote_SGbd.TabIndex = 3
        ' 
        ' txtNoteProgrammation
        ' 
        txtNoteProgrammation.Location = New Point(333, 217)
        txtNoteProgrammation.Name = "txtNoteProgrammation"
        txtNoteProgrammation.Size = New Size(152, 23)
        txtNoteProgrammation.TabIndex = 4
        ' 
        ' txtMoyenne
        ' 
        txtMoyenne.Location = New Point(333, 283)
        txtMoyenne.Name = "txtMoyenne"
        txtMoyenne.Size = New Size(152, 23)
        txtMoyenne.TabIndex = 5
        ' 
        ' txtDecison
        ' 
        txtDecison.Location = New Point(333, 357)
        txtDecison.Name = "txtDecison"
        txtDecison.Size = New Size(152, 23)
        txtDecison.TabIndex = 6
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(70, 91)
        Label1.Name = "Label1"
        Label1.Size = New Size(58, 15)
        Label1.TabIndex = 7
        Label1.Text = "Front End"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(70, 156)
        Label2.Name = "Label2"
        Label2.Size = New Size(36, 15)
        Label2.TabIndex = 8
        Label2.Text = "SGBD"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(70, 217)
        Label3.Name = "Label3"
        Label3.Size = New Size(91, 15)
        Label3.TabIndex = 9
        Label3.Text = "Progrramatioon"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtDecison)
        Controls.Add(txtMoyenne)
        Controls.Add(txtNoteProgrammation)
        Controls.Add(txtNote_SGbd)
        Controls.Add(txtFrontEndNote)
        Controls.Add(btnDecison)
        Controls.Add(btnMoyenne)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnMoyenne As Button
    Friend WithEvents btnDecison As Button
    Friend WithEvents txtFrontEndNote As TextBox
    Friend WithEvents txtNote_SGbd As TextBox
    Friend WithEvents txtNoteProgrammation As TextBox
    Friend WithEvents txtMoyenne As TextBox
    Friend WithEvents txtDecison As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label

End Class
