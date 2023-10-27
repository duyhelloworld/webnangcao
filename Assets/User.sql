CREATE DATABASE webnangcao;
USE [webnangcao];
CREATE LOGIN [webnangcao] WITH PASSWORD = '12345678', DEFAULT_DATABASE = [webnangcao], CHECK_POLICY = OFF;
CREATE USER [webnangcao] FOR LOGIN [webnangcao];
EXEC sp_addrolemember 'db_owner', 'webnangcao';
GRANT CREATE, SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO [webnangcao];