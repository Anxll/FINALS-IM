Imports MySqlConnector

Public Class AddEmployee

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            openConn()

            Dim query As String =
            "INSERT INTO Employee 
            (FirstName, LastName, Gender, DateOfBirth, ContactNumber, Email, Address, HireDate,
             Position, MaritalStatus, EmploymentStatus, EmploymentType, WorkShift, Salary, EmergencyContact)
             VALUES
            (@fn, @ln, @gender, @dob, @contact, @mail, @addr, @hire,
             @pos, @marital, @empStatus, @empType, @shift, @salary, @emergency)"

            Dim cmd As New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@fn", FirstName.Text)
            cmd.Parameters.AddWithValue("@ln", LastName.Text)
            cmd.Parameters.AddWithValue("@gender", Gender.Text)
            cmd.Parameters.AddWithValue("@dob", DateOfBirth.Value)
            cmd.Parameters.AddWithValue("@contact", ContactNumber.Text)
            cmd.Parameters.AddWithValue("@mail", Email.Text)
            cmd.Parameters.AddWithValue("@addr", Address.Text)
            cmd.Parameters.AddWithValue("@hire", HireDate.Value)
            cmd.Parameters.AddWithValue("@pos", Position.Text)
            cmd.Parameters.AddWithValue("@marital", MaritalStatus.Text)
            cmd.Parameters.AddWithValue("@empStatus", EmploymentStatus.Text)
            cmd.Parameters.AddWithValue("@empType", EmploymentType.Text)
            cmd.Parameters.AddWithValue("@shift", WorkShift.Text)
            cmd.Parameters.AddWithValue("@salary", Salary.Text)
            cmd.Parameters.AddWithValue("@emergency", EmergencyContact.Text)

            cmd.ExecuteNonQuery()
            closeConn()

            MessageBox.Show("Employee Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If Application.OpenForms().OfType(Of Employee).Any Then
                Dim empForm As Employee = Application.OpenForms().OfType(Of Employee).First()
                empForm.LoadEmployees()
                empForm.BringToFront()
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error adding employee: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class