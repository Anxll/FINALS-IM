Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports MySqlConnector

Module modDB

    Public conn As New MySqlConnection()
    Public cmd As MySqlCommand
    Public cmdRead As MySqlDataReader

    Public db_server As String = "localhost"
    Public db_uid As String = "root"
    Public db_pwd As String = ""
    Public db_name As String = "tabeya_system"

    Public strConnection As String =
        $"Server={db_server};Port=3306;Database={db_name};Uid={db_uid};Pwd={db_pwd};SslMode=None;AllowUserVariables=True;"

    Public Structure LoggedUser
        Dim id As Integer
        Dim name As String
        Dim position As String
        Dim username As String
        Dim password As String
        Dim type As Integer
    End Structure

    Public CurrentLoggedUser As LoggedUser

    ' ✔ Open connection
    Public Sub openConn()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.ConnectionString = strConnection
            conn.Open()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ✔ Close connection (ADDED)
    Public Sub closeConn()
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ✔ Read Query
    Public Sub readQuery(ByVal sql As String)
        Try
            openConn()
            cmd = New MySqlCommand(sql, conn)
            cmdRead = cmd.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ✔ Load to DGV
    Function LoadToDGV(query As String, dgv As DataGridView, filter As String) As Integer
        Try
            readQuery(query)
            Dim dt As New DataTable
            dt.Load(cmdRead)
            dgv.DataSource = dt
            dgv.Refresh()
            closeConn() ' ← Added here for cleanup
            Return dgv.Rows.Count
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Return 0
    End Function

    ' ✔ Encryption
    Public Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey,
                New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function

    ' ✔ Decrypt
    Public Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey,
                New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

    ' ✔ Log event
    Sub Logs(transaction As String, Optional events As String = "*_Click")
        Try
            readQuery($"INSERT INTO logs(dt, user_accounts_id, event, transactions)
                       VALUES (NOW(), {CurrentLoggedUser.id}, '{events}', '{transaction}')")
            closeConn() ' ← added to prevent open connection lock
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' ✔ Check and Create Tables
    Public Sub CheckAndCreateTables()
        Try
            openConn()

            ' 1. Create user_accounts table
            Dim sqlUser As String = "
                CREATE TABLE IF NOT EXISTS user_accounts (
                    UserID INT PRIMARY KEY AUTO_INCREMENT,
                    FullName VARCHAR(100) NOT NULL,
                    Username VARCHAR(50) UNIQUE NOT NULL,
                    PasswordHash VARCHAR(255) NOT NULL,
                    Role ENUM('Admin', 'Manager', 'Staff', 'Employee', 'Customer') NOT NULL DEFAULT 'Staff',
                    Status ENUM('Active', 'Inactive', 'Suspended') DEFAULT 'Active',
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                    LastLoginDate DATETIME NULL
                )"
            Dim cmdUser As New MySqlCommand(sqlUser, conn)
            cmdUser.ExecuteNonQuery()

            ' 2. Create payroll table
            Dim sqlPayroll As String = "
                CREATE TABLE IF NOT EXISTS payroll (
                    PayrollID INT PRIMARY KEY AUTO_INCREMENT,
                    EmployeeID INT NOT NULL,
                    PayPeriodStart DATE NOT NULL,
                    PayPeriodEnd DATE NOT NULL,
                    BasicSalary DECIMAL(10,2) NOT NULL,
                    Overtime DECIMAL(10,2) DEFAULT 0,
                    Deductions DECIMAL(10,2) DEFAULT 0,
                    Bonuses DECIMAL(10,2) DEFAULT 0,
                    NetPay DECIMAL(10,2) GENERATED ALWAYS AS (BasicSalary + Overtime + Bonuses - Deductions) STORED,
                    Status ENUM('Pending', 'Approved', 'Paid') DEFAULT 'Pending',
                    ProcessedBy INT NULL,
                    ProcessedDate DATETIME NULL,
                    Notes TEXT NULL,
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (EmployeeID) REFERENCES employee(EmployeeID) ON DELETE CASCADE
                )"
            Dim cmdPayroll As New MySqlCommand(sqlPayroll, conn)
            cmdPayroll.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("Error initializing database tables: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            closeConn()
        End Try
    End Sub

End Module