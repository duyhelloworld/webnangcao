-- Active: 1702345088705@@127.0.0.1@1433@webnangcao
CREATE DATABASE webnangcao;
USE [webnangcao];
CREATE LOGIN [webnangcao] WITH PASSWORD = '12345678', DEFAULT_DATABASE = [webnangcao], CHECK_POLICY = OFF;
CREATE USER [webnangcao] FOR LOGIN [webnangcao];
EXEC sp_addrolemember 'db_owner', 'webnangcao';
-- Add quyền db_creator để tạo database
EXEC sp_addsrvrolemember 'webnangcao', 'dbcreator';
GRANT CREATE TABLE, CREATE PROCEDURE, CREATE FUNCTION TO [webnangcao];

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO [webnangcao];