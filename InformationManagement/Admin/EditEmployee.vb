Imports MySqlConnector

Public Class EditEmployee

    Public Property EmployeeIDValue As Integer

    Private Sub EditEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployeeData()
    End Sub

    '=====================================
    ' LOAD EMPLOYEE DETAILS
    '=====================================
    Private Sub LoadEmployeeData()
        Try
            openConn()

            Dim query As String = "SELECT * FROM Employee WHERE EmployeeID=@id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", EmployeeIDValue)

            Dim rdr As MySqlDataReader = cmd.ExecuteReader()

            If rdr.Read() Then
                EmployeeID.Text = rdr("EmployeeID").ToString()
                FirstName.Text = rdr("FirstName").ToString()
                LastName.Text = rdr("LastName").ToString()
                Gender.Text = rdr("Gender").ToString()
                DateOfBirth.Value = rdr("DateOfBirth")
                ContactNumber.Text = rdr("ContactNumber").ToString()
                Email.Text = rdr("Email").ToString()
                Address.Text = rdr("Address").ToString()
                HireDate.Value = rdr("HireDate")
                Position.Text = rdr("Position").ToString()
                MaritalStatus.Text = rdr("MaritalStatus").ToString()
                EmploymentStatus.Text = rdr("EmploymentStatus").ToString()
                EmploymentType.Text = rdr("EmploymentType").ToString()
                WorkShift.Text = rdr("WorkShift").ToString()
                Salary.Text = rdr("Salary").ToString()
                EmergencyContact.Text = rdr("EmergencyContact").ToString()
            End If

            rdr.Close()
            closeConn()

        Catch ex As Exception
            MessageBox.Show("Error loading employee data: " & ex.Message)
        End Try
    End Sub

    '=====================================
    ' UPDATE EMPLOYEE
    '=====================================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            openConn()

            Dim query As String =
            "UPDATE Employee SET 
                FirstName=@fn, 
                LastName=@ln,
                Gender=@gender,
                DateOfBirth=@dob,
                ContactNumber=@contact,
                Email=@mail,
                Address=@addr,
                HireDate=@hire,
                Position=@pos,
                MaritalStatus=@marital,
                EmploymentStatus=@empStatus,
                EmploymentType=@empType,
                WorkShift=@shift,
                Salary=@salary,
                EmergencyContact=@emergency
             WHERE EmployeeID=@id"

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
            cmd.Parameters.AddWithValue("@id", EmployeeIDValue)

            cmd.ExecuteNonQuery()
            closeConn()

            MessageBox.Show("Employee updated successfully!", "Success")

            If Application.OpenForms().OfType(Of Employee).Any Then
                Dim empForm As Employee = Application.OpenForms().OfType(Of Employee).First()
                empForm.LoadEmployees()
                empForm.BringToFront()
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error updating employee: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class