
Public Class Form1
    Private Const UsernameUser1 As String = "user1"
    Private Const PasswordUser1 As String = "password1"
    Private Const UsernameUser2 As String = "user2"
    Private Const PasswordUser2 As String = "password2"
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim inputUsername As String = TextBox1.Text
        Dim inputPassword As String = TextBox2.Text
        If inputUsername = UsernameUser1 AndAlso inputPassword = PasswordUser1 Then
                MessageBox.Show("Login berhasil sebagai User 1!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Hide()
                Dim form2 As New Form2()
                form2.ShowDialog()
                Me.Close()
            ElseIf inputUsername = UsernameUser2 AndAlso inputPassword = PasswordUser2 Then
                MessageBox.Show("Login berhasil sebagai User 2!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Hide()
                Dim form3 As New Form3()
                form3.ShowDialog()
                Me.Close()
            Else
                MessageBox.Show("Username atau password salah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
