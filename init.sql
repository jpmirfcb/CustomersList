CREATE DATABASE IF NOT EXISTS customersList;
USE customersList;

CREATE TABLE IF NOT EXISTS Users (
    Id CHAR(36) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Active BOOLEAN NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    DeactivatedAt DATETIME NULL,
    INDEX IX_Users_Email (Email)
);

CREATE TABLE IF NOT EXISTS Customers (
    Id CHAR(36) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Phone VARCHAR(50) NOT NULL,
	INDEX IX_Customers_Email (Email)
);

-- Grant privileges to 
GRANT ALL PRIVILEGES ON customersList.* TO 'customersUser'@'%';
FLUSH PRIVILEGES;