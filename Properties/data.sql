-- Active: 1695612038229@@127.0.0.1@1433@master
CREATE DATABASE webnangcao;
USE [webnangcao];
SELECT * FROM sys.databases WHERE name = 'webnangcao';
CREATE LOGIN [webnangcao] WITH PASSWORD = '12345678', DEFAULT_DATABASE = [webnangcao], CHECK_POLICY = OFF;
CREATE USER [webnangcao] FOR LOGIN [webnangcao];
EXEC sp_addrolemember 'db_owner', 'webnangcao';
GRANT CREATE TABLE, CREATE PROCEDURE, CREATE FUNCTION TO [webnangcao];
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO [webnangcao];

-- INSERT INTO () VALUES()