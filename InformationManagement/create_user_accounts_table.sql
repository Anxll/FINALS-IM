    -- Create user_accounts table (matches the VB app schema)
    -- Run this SQL script in your MySQL database

    -- Drop existing table if needed
    DROP TABLE IF EXISTS user_accounts;

    -- Create user_accounts table (legacy columns used by the app)
    CREATE TABLE user_accounts (
        id INT PRIMARY KEY AUTO_INCREMENT,
        employee_id INT NULL,
        name VARCHAR(100) NOT NULL,
        username VARCHAR(50) UNIQUE NOT NULL,
        password VARCHAR(255) NOT NULL,
        type INT NOT NULL DEFAULT 1,
        position VARCHAR(100) NULL,
        created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
        INDEX idx_username (username),
        INDEX idx_employee_id (employee_id)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

    -- Insert a sample admin user for testing (password must be AES-encrypted by the app's Encrypt() function)
    INSERT INTO user_accounts (employee_id, name, username, password, type, position)
    VALUES (NULL, 'Administrator', 'admin', 'encrypted_password_here', 1, 'Admin');

    -- Verify the table structure
    DESCRIBE user_accounts;

    -- View data
    SELECT * FROM user_accounts;
