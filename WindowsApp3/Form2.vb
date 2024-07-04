Imports MySql.Data.MySqlClient

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()

        ' Muat data ke DataGridView saat form dimuat
        Try
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_karyawan", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_karyawan")
            DataGridView1.DataSource = myDataSet.Tables("tb_karyawan")
        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat memuat data: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Periksa apakah baris valid diklik
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Isi form input dengan data dari baris yang diklik
            TextBox1.Text = selectedRow.Cells("username").Value.ToString()
            TextBox2.Text = selectedRow.Cells("password").Value.ToString()
            TextBox3.Text = selectedRow.Cells("nama_karyawan").Value.ToString()
            ComboBox1.SelectedItem = selectedRow.Cells("jenis_kelamin").Value.ToString()
            TextBox4.Text = selectedRow.Cells("email").Value.ToString()

            ' Penanganan konversi tanggal
            Dim tglLahirString As String = selectedRow.Cells("tgl_lahir").Value.ToString()
            Dim tglLahir As DateTime
            If DateTime.TryParse(tglLahirString, tglLahir) Then
                DateTimePicker1.Value = tglLahir
            Else
                ' Atur tanggal default jika konversi gagal
                DateTimePicker1.Value = DateTime.Now
                MsgBox("Format tanggal tidak valid. Menggunakan tanggal saat ini sebagai default.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Fungsi ini dapat diimplementasikan jika diperlukan
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        ' Fungsi ini dapat diimplementasikan jika diperlukan
    End Sub

    Private Sub sv_Click(sender As Object, e As EventArgs) Handles sv.Click
        Call koneksi() ' Pastikan koneksi dibuka saat tombol klik

        Try
            ' Mengambil nilai dari form
            Dim username As String = TextBox1.Text
            Dim password As String = TextBox2.Text
            Dim nama_karyawan As String = TextBox3.Text
            Dim jenis_kelamin As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), String.Empty)
            Dim email As String = TextBox4.Text
            Dim tgl_lahir As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")

            ' Validasi input
            If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) OrElse String.IsNullOrEmpty(nama_karyawan) OrElse String.IsNullOrEmpty(jenis_kelamin) OrElse String.IsNullOrEmpty(email) Then
                MsgBox("Semua kolom harus diisi", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim insertCmd As New MySqlCommand("INSERT INTO tb_karyawan (username, password, nama_karyawan, jenis_kelamin, email, tgl_lahir) VALUES (@username, @password, @nama_karyawan, @jenis_kelamin, @email, @tgl_lahir)", conn)
            insertCmd.Parameters.AddWithValue("@username", username)
            insertCmd.Parameters.AddWithValue("@password", password)
            insertCmd.Parameters.AddWithValue("@nama_karyawan", nama_karyawan)
            insertCmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin)
            insertCmd.Parameters.AddWithValue("@email", email)
            insertCmd.Parameters.AddWithValue("@tgl_lahir", tgl_lahir)

            ' Eksekusi perintah SQL
            insertCmd.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan", MsgBoxStyle.Information)

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_karyawan", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_karyawan")
            DataGridView1.DataSource = myDataSet.Tables("tb_karyawan")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub edit_Click(sender As Object, e As EventArgs) Handles edit.Click
        Call koneksi() ' Pastikan koneksi dibuka

        Try
            ' Mengambil nilai dari form
            Dim username As String = TextBox1.Text
            Dim password As String = TextBox2.Text
            Dim nama_karyawan As String = TextBox3.Text
            Dim jenis_kelamin As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), String.Empty)
            Dim email As String = TextBox4.Text
            Dim tgl_lahir As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")

            ' Validasi input
            If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) OrElse String.IsNullOrEmpty(nama_karyawan) OrElse String.IsNullOrEmpty(jenis_kelamin) OrElse String.IsNullOrEmpty(email) Then
                MsgBox("Semua kolom harus diisi", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim updateCmd As New MySqlCommand("UPDATE tb_karyawan SET password = @password, 
                                          nama_karyawan = @nama_karyawan, 
                                          jenis_kelamin = @jenis_kelamin, 
                                          email = @email, 
                                          tgl_lahir = @tgl_lahir 
                                          WHERE username = @username", conn)
            updateCmd.Parameters.AddWithValue("@username", username)
            updateCmd.Parameters.AddWithValue("@password", password)
            updateCmd.Parameters.AddWithValue("@nama_karyawan", nama_karyawan)
            updateCmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin)
            updateCmd.Parameters.AddWithValue("@email", email)
            updateCmd.Parameters.AddWithValue("@tgl_lahir", tgl_lahir)

            ' Eksekusi perintah SQL
            updateCmd.ExecuteNonQuery()
            MsgBox("Data berhasil diperbarui", MsgBoxStyle.Information)

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_karyawan", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_karyawan")
            DataGridView1.DataSource = myDataSet.Tables("tb_karyawan")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs) Handles delete.Click
        Call koneksi() ' Pastikan koneksi dibuka

        Try
            ' Mengambil nilai username yang dipilih
            Dim username As String = TextBox1.Text

            ' Validasi input
            If String.IsNullOrEmpty(username) Then
                MsgBox("Pilih data yang akan dihapus terlebih dahulu", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim deleteCmd As New MySqlCommand("DELETE FROM tb_karyawan WHERE username = @username", conn)
            deleteCmd.Parameters.AddWithValue("@username", username)

            ' Eksekusi perintah SQL
            deleteCmd.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus", MsgBoxStyle.Information)

            ' Clear form input setelah hapus
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.SelectedIndex = -1
            TextBox4.Text = ""
            DateTimePicker1.Value = DateTime.Now

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_karyawan", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_karyawan")
            DataGridView1.DataSource = myDataSet.Tables("tb_karyawan")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim form1 As New Form1()
        form1.ShowDialog()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call koneksi()

        Try
            ' Mengambil nilai username yang dipilih
            Dim username As String = TextBox1.Text

            ' Validasi input
            If String.IsNullOrEmpty(username) Then
                MsgBox("Pilih data yang akan dihapus terlebih dahulu", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim deleteCmd As New MySqlCommand("DELETE FROM tb_karyawan WHERE username = @username", conn)
            deleteCmd.Parameters.AddWithValue("@username", username)

            ' Eksekusi perintah SQL
            deleteCmd.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus", MsgBoxStyle.Information)

            ' Clear form input setelah hapus
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.SelectedIndex = -1
            TextBox4.Text = ""
            DateTimePicker1.Value = DateTime.Now

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_karyawan", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_karyawan")
            DataGridView1.DataSource = myDataSet.Tables("tb_karyawan")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
