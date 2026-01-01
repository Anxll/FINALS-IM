Imports System
Imports MySqlConnector

Public Class AddEmployee

    ' ComboBoxes for ENUM-like fields
    Private cmbGender As New ComboBox()
    Private cmbMaritalStatus As New ComboBox()
    Private cmbEmploymentStatus As New ComboBox()
    Private cmbEmploymentType As New ComboBox()
    Private cmbWorkShift As New ComboBox()
    Private cmbPosition As New ComboBox()

    Private Sub AddEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup ComboBoxes
        SetupComboBox(cmbGender, Gender, New String() {"Male", "Female", "Other"})
        SetupComboBox(cmbMaritalStatus, MaritalStatus, New String() {"Single", "Married", "Separated", "Divorced", "Widowed"})
        SetupComboBox(cmbEmploymentStatus, EmploymentStatus, New String() {"Active", "On Leave", "Resigned"})
        SetupComboBox(cmbEmploymentType, EmploymentType, New String() {"Full-time", "Part-time", "Contract"})
        SetupComboBox(cmbWorkShift, WorkShift, New String() {"Morning", "Evening", "Split"})
        SetupComboBox(cmbPosition, Position, New String() {"Staff"})

        ' EmployeeID placeholder
        EmployeeID.Text = "(Auto-Generated)"
    End Sub

    ' Helper to configure ComboBoxes
    Private Sub SetupComboBox(cmb As ComboBox, txt As Control, items As String())
        cmb.Parent = txt.Parent
        cmb.Location = txt.Location
        cmb.Size = txt.Size
        cmb.Font = txt.Font
        cmb.BackColor = txt.BackColor
        cmb.DropDownStyle = ComboBoxStyle.DropDownList
        cmb.FlatStyle = FlatStyle.Flat
        cmb.Items.AddRange(items)

        If cmb.Items.Count > 0 Then cmb.SelectedIndex = 0

        txt.Visible = False
        cmb.BringToFront()
    End Sub

    ' Save button
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Open connection
            modDB.openConn()

            Dim query As String = "
                INSERT INTO employee 
                (FirstName, LastName, Gender, DateOfBirth, ContactNumber, Email, Address, HireDate,
                 Position, MaritalStatus, EmploymentStatus, EmploymentType, WorkShift, Salary, EmergencyContact)
                VALUES 
                (@fn, @ln, @gender, @dob, @contact, @mail, @addr, @hire,
                 @pos, @marital, @empStatus, @empType, @shift, @salary, @emergency)
            "

            Using cmd As New MySqlCommand(query, modDB.conn)
                ' Add parameters safely for nullable fields
                cmd.Parameters.AddWithValue("@fn", FirstName.Text)
                cmd.Parameters.AddWithValue("@ln", LastName.Text)
                cmd.Parameters.AddWithValue("@gender", If(String.IsNullOrWhiteSpace(cmbGender.Text), DBNull.Value, cmbGender.Text))
                cmd.Parameters.AddWithValue("@dob", DateOfBirth.Value)
                cmd.Parameters.AddWithValue("@contact", If(String.IsNullOrWhiteSpace(ContactNumber.Text), DBNull.Value, ContactNumber.Text))
                cmd.Parameters.AddWithValue("@mail", If(String.IsNullOrWhiteSpace(Email.Text), DBNull.Value, Email.Text))
                cmd.Parameters.AddWithValue("@addr", If(String.IsNullOrWhiteSpace(Address.Text), DBNull.Value, Address.Text))
                cmd.Parameters.AddWithValue("@hire", HireDate.Value)
                cmd.Parameters.AddWithValue("@pos", If(String.IsNullOrWhiteSpace(cmbPosition.Text), DBNull.Value, cmbPosition.Text))
                cmd.Parameters.AddWithValue("@marital", If(String.IsNullOrWhiteSpace(cmbMaritalStatus.Text), "Single", cmbMaritalStatus.Text))
                cmd.Parameters.AddWithValue("@empStatus", If(String.IsNullOrWhiteSpace(cmbEmploymentStatus.Text), "Active", cmbEmploymentStatus.Text))
                cmd.Parameters.AddWithValue("@empType", If(String.IsNullOrWhiteSpace(cmbEmploymentType.Text), "Full-time", cmbEmploymentType.Text))
                cmd.Parameters.AddWithValue("@shift", If(String.IsNullOrWhiteSpace(cmbWorkShift.Text), DBNull.Value, cmbWorkShift.Text))
                cmd.Parameters.AddWithValue("@salary", If(String.IsNullOrWhiteSpace(Salary.Text), 0D, Convert.ToDecimal(Salary.Text)))
                cmd.Parameters.AddWithValue("@emergency", If(String.IsNullOrWhiteSpace(EmergencyContact.Text), DBNull.Value, EmergencyContact.Text))

                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Employee Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Log Activity
            ActivityLogger.LogUserActivity(
                action:="Add",
                actionCategory:="User Management",
                description:=$"Added New Employee: {FirstName.Text} {LastName.Text}",
                sourceSystem:="Admin Panel",
                referenceTable:="employee",
                newValue:=$"Name: {FirstName.Text} {LastName.Text}, Position: {cmbPosition.Text}"
            )

            ' Refresh employee list if open
            If Application.OpenForms().OfType(Of Employee).Any() Then
                Dim empForm As Employee = Application.OpenForms().OfType(Of Employee).First()
                empForm.LoadEmployees()
                empForm.BringToFront()
            End If

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error adding employee: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            modDB.closeConn()
        End Try
    End Sub

    ' Cancel button
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class