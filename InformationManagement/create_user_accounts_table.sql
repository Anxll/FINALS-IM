    -- Create user_accounts table
    -- Run this SQL script in your MySQL database

    -- Drop existing table if needed
    DROP TABLE IF EXISTS user_accounts;

    -- Create user_accounts table
    CREATE TABLE user_accounts (
        UserID INT PRIMARY KEY AUTO_INCREMENT,
        FullName VARCHAR(100) NOT NULL,
        Username VARCHAR(50) UNIQUE NOT NULL,
        PasswordHash VARCHAR(255) NOT NULL,
        Role ENUM('Admin', 'Manager', 'Staff', 'Employee', 'Customer') NOT NULL DEFAULT 'Staff',
        Status ENUM('Active', 'Inactive', 'Suspended') DEFAULT 'Active',
        CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
        UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        LastLoginDate DATETIME NULL,
        INDEX idx_username (Username),
        INDEX idx_status (Status)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

    -- Insert a sample admin user for testing (password is encrypted "admin")
    -- You should change this password after first login
    INSERT INTO user_accounts (FullName, Username, PasswordHash, Role, Status)
    VALUES ('Administrator', 'admin', 'encrypted_password_here', 'Admin', 'Active');

    -- Verify the table structure
    DESCRIBE user_accounts;

    -- View data
    SELECT * FROM user_accounts;
