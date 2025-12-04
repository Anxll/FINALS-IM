Imports MySqlConnector

Public Class AddEmployee
    ' Dynamic ComboBoxes for ENUM fields
    Private cmbGender As New ComboBox()
    Private cmbMaritalStatus As New ComboBox()
    Private cmbEmploymentStatus As New ComboBox()
    Private cmbEmploymentType As New ComboBox()
    Private cmbWorkShift As New ComboBox()
    Private cmbPosition As New ComboBox()

    Private Sub AddEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup Gender ComboBox
        SetupComboBox(cmbGender, Gender, New String() {"Male", "Female", "Other"})
        
        ' Setup MaritalStatus ComboBox
        SetupComboBox(cmbMaritalStatus, MaritalStatus, New String() {"Single", "Married", "Separated", "Divorced", "Widowed"})
        
        ' Setup EmploymentStatus ComboBox
        SetupComboBox(cmbEmploymentStatus, EmploymentStatus, New String() {"Active", "On Leave", "Resigned"})
        
        ' Setup EmploymentType ComboBox
        SetupComboBox(cmbEmploymentType, EmploymentType, New String() {"Full-time", "Part-time", "Contract"})
        
        ' Setup WorkShift ComboBox
        SetupComboBox(cmbWorkShift, WorkShift, New String() {"Morning", "Evening", "Split"})
        
        ' Setup Position ComboBox
        SetupComboBox(cmbPosition, Position, New String() {"Staff"})
        
        ' Set EmployeeID placeholder
        EmployeeID.Text = "(Auto-Generated)"
    End Sub

    Private Sub SetupComboBox(cmb As ComboBox, textBox As TextBox, items As String())
        ' Configure ComboBox
        cmb.Parent = textBox.Parent
        cmb.Location = textBox.Location
        cmb.Size = textBox.Size
        cmb.Font = textBox.Font
        cmb.BackColor = textBox.BackColor
        cmb.DropDownStyle = ComboBoxStyle.DropDownList
        cmb.FlatStyle = FlatStyle.Flat
        
        ' Add items
        cmb.Items.AddRange(items)
        cmb.SelectedIndex = 0 ' Select first item by default
        
        ' Hide original textbox and bring ComboBox to front
        textBox.Visible = False
        cmb.BringToFront()
    End Sub

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
            cmd.Parameters.AddWithValue("@gender", cmbGender.Text)
            cmd.Parameters.AddWithValue("@dob", DateOfBirth.Value)
            cmd.Parameters.AddWithValue("@contact", ContactNumber.Text)
            cmd.Parameters.AddWithValue("@mail", Email.Text)
            cmd.Parameters.AddWithValue("@addr", Address.Text)
            cmd.Parameters.AddWithValue("@hire", HireDate.Value)
            cmd.Parameters.AddWithValue("@pos", cmbPosition.Text)
            cmd.Parameters.AddWithValue("@marital", cmbMaritalStatus.Text)
            cmd.Parameters.AddWithValue("@empStatus", cmbEmploymentStatus.Text)
            cmd.Parameters.AddWithValue("@empType", cmbEmploymentType.Text)
            cmd.Parameters.AddWithValue("@shift", cmbWorkShift.Text)
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