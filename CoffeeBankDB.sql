CREATE DATABASE CoffeBank_DB
GO
USE CoffeBank_DB
Go
CREATE TABLE Musteriler
(
	ID int IDENTITY(1,1),
	Isim nvarchar(50),
	Soyisim nvarchar(50),
	DogumTarihi date,
	IsActive bit,
	CONSTRAINT pk_Musteri PRIMARY KEY(ID)
)
GO
CREATE TABLE Hesaplar
(
	ID int IDENTITY(1,1),
	Musteri_ID int,
	IBAN nvarchar(50),
	Bakiye decimal(18,2),
	IsActive bit,
	CONSTRAINT pk_hesap PRIMARY KEY(ID),
	CONSTRAINT fk_hesapMusteri FOREIGN KEY(Musteri_ID)
	REFERENCES Musteriler(ID)
)
GO
CREATE TABLE Kartlar
(
	ID int IDENTITY(1,1),
	Hesap_ID int,
	KartNo nvarchar(16),
	SonKullanmaAy nvarchar(2),
	SonKullanmaYil nvarchar(2),
	CVCKodu nvarchar(4),
	CONSTRAINT pk_kart PRIMARY KEY(ID),
	CONSTRAINT fk_kartHesap FOREIGN KEY(Hesap_ID)
	REFERENCES Hesaplar(ID)
)
GO
CREATE TABLE SanalPosMusteriler
(
	ID int IDENTITY(1,1),
	FirmaAdi nvarchar(500),
	SaticiKodu nvarchar(12),
	Sifre nvarchar(10),
	IsActive bit,
	CONSTRAINT pk_SanalPosMusteri PRIMARY KEY(ID)
)
