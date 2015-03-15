use projektBD;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Administrator_U�ytkownik') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Administrator DROP CONSTRAINT FK_Administrator_U�ytkownik
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Zak�ad') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE nale�y_do DROP CONSTRAINT FK_Zak�ad
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Prowadz�cy') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE nale�y_do DROP CONSTRAINT FK_Prowadz�cy
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('ocenia') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Ocena DROP CONSTRAINT ocenia
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('jest_wystawiana') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Ocena DROP CONSTRAINT jest_wystawiana
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('jest_realizowany_w') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Projekt DROP CONSTRAINT jest_realizowany_w
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Prowadz�cy_U�ytkownik') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Prowadz�cy DROP CONSTRAINT FK_Prowadz�cy_U�ytkownik
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Rozmowa') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE prowadzi DROP CONSTRAINT FK_Rozmowa
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_U�ytkownik') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE prowadzi DROP CONSTRAINT FK_U�ytkownik
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('kierownik') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Przedmiot DROP CONSTRAINT kierownik
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_PrzedmiotObieralny_Przedmiot') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE PrzedmiotObieralny DROP CONSTRAINT FK_PrzedmiotObieralny_Przedmiot
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('pisze') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Raport DROP CONSTRAINT pisze
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('daje_informacje') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Raport DROP CONSTRAINT daje_informacje
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Student_U�ytkownik') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Student DROP CONSTRAINT FK_Student_U�ytkownik
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('jest_cz�ci�') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Wiadomo�� DROP CONSTRAINT jest_cz�ci�
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Przedmiot') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE zapisuje_si�_na DROP CONSTRAINT FK_Przedmiot
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Student') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE zapisuje_si�_na DROP CONSTRAINT FK_Student
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('potwierdza') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Zg�oszenie DROP CONSTRAINT potwierdza
;



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Administrator') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Administrator
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('nale�y_do') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE nale�y_do
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Ocena') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Ocena
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Projekt') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Projekt
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Prowadz�cy') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Prowadz�cy
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('prowadzi') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE prowadzi
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Przedmiot') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Przedmiot
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('PrzedmiotObieralny') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE PrzedmiotObieralny
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Raport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Raport
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Rozmowa') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Rozmowa
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Student') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Student
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('U�ytkownik') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE U�ytkownik
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Wiadomo��') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Wiadomo��
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Zak�ad') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Zak�ad
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('zapisuje_si�_na') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE zapisuje_si�_na
;

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Zg�oszenie') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Zg�oszenie
;


CREATE TABLE Administrator ( 
	administratorID Integer NOT NULL
)
;

CREATE TABLE nale�y_do ( 
	zak�adID Integer,
	prowadz�cyID Integer
)
;

CREATE TABLE Ocena ( 
	dataWpisania Date,
	idOceny bigint,
	komentarz nvarchar(500),
	warto�� float,
	ocenaID Integer NOT NULL,
	Cel Integer,
	Podstawa Integer
)
;

CREATE TABLE Projekt ( 
	idProjektu int,
	maxLiczbaStudent�w int,
	nazwa nvarchar(50) NOT NULL,
	opis nvarchar(1000) NOT NULL,
	projektID Integer NOT NULL,
	Podstawa Integer
)
;

CREATE TABLE Prowadz�cy ( 
	zak�ad nvarchar(50) NOT NULL,
	prowadz�cyID Integer NOT NULL
)
;

CREATE TABLE prowadzi ( 
	rozmowaID Integer,
	u�ytkownikID Integer
)
;

CREATE TABLE Przedmiot ( 
	idPrzedmiotu int,
	liczbaStudent�w int,
	nazwa nvarchar(50) NOT NULL,
	opis nvarchar(1000) NOT NULL,
	przedmiotID Integer NOT NULL,
	Kierownik Integer
)
;

CREATE TABLE PrzedmiotObieralny ( 
	maxLiczbaStudent�w int,
	przedmiotObieralnyID Integer NOT NULL
)
;

CREATE TABLE Raport ( 
	idRaportu smallint,
	tresc nvarchar(2000) NOT NULL,
	raportID Integer NOT NULL,
	Autor Integer,
	Punkt_docelowy Integer
)
;

CREATE TABLE Rozmowa ( 
	dataRozpoczecia Date,
	idRozmowy int,
	rozmowaID Integer NOT NULL
)
;

CREATE TABLE Student ( 
	nrIndeksu int,
	studentID Integer NOT NULL
)
;

CREATE TABLE U�ytkownik ( 
	dataUrodzenia Date,
	email nvarchar(50),
	haslo nvarchar(50) NOT NULL,
	idUzytkownika int,
	login nvarchar(50) NOT NULL,
	miejsceZamieszkania nvarchar(100) NOT NULL,
	u�ytkownikID Integer NOT NULL
)
;

CREATE TABLE Wiadomo�� ( 
	dataWys�ania Date,
	nadawca nvarchar(50) NOT NULL,
	tresc nvarchar(2000) NOT NULL,
	wiadomo��ID Integer NOT NULL,
	Zbi�r Integer NOT NULL
)
;

CREATE TABLE Zak�ad ( 
	idZak�adu smallint,
	nazwa nvarchar(50) NOT NULL,
	opis nvarchar(1000) NOT NULL,
	zak�adID Integer NOT NULL
)
;

CREATE TABLE zapisuje_si�_na ( 
	przedmiotID Integer,
	studentID Integer
)
;

CREATE TABLE Zg�oszenie ( 
	idZgloszenia bigint,
	jestZaakceptowane bit DEFAULT 0,
	zg�oszenieID Integer NOT NULL,
	Kierownik Integer
)
;


ALTER TABLE Administrator ADD CONSTRAINT PK_Administrator 
	PRIMARY KEY CLUSTERED (administratorID)
;

ALTER TABLE Ocena ADD CONSTRAINT PK_Ocena 
	PRIMARY KEY CLUSTERED (ocenaID)
;

ALTER TABLE Projekt ADD CONSTRAINT PK_Projekt 
	PRIMARY KEY CLUSTERED (projektID)
;

ALTER TABLE Prowadz�cy ADD CONSTRAINT PK_Prowadz�cy 
	PRIMARY KEY CLUSTERED (prowadz�cyID)
;

ALTER TABLE Przedmiot ADD CONSTRAINT PK_Przedmiot 
	PRIMARY KEY CLUSTERED (przedmiotID)
;

ALTER TABLE PrzedmiotObieralny ADD CONSTRAINT PK_PrzedmiotObieralny 
	PRIMARY KEY CLUSTERED (przedmiotObieralnyID)
;

ALTER TABLE Raport ADD CONSTRAINT PK_Raport 
	PRIMARY KEY CLUSTERED (raportID)
;

ALTER TABLE Rozmowa ADD CONSTRAINT PK_Rozmowa 
	PRIMARY KEY CLUSTERED (rozmowaID)
;

ALTER TABLE Student ADD CONSTRAINT PK_Student 
	PRIMARY KEY CLUSTERED (studentID)
;

ALTER TABLE U�ytkownik ADD CONSTRAINT PK_U�ytkownik 
	PRIMARY KEY CLUSTERED (u�ytkownikID)
;

ALTER TABLE Wiadomo�� ADD CONSTRAINT PK_Wiadomo�� 
	PRIMARY KEY CLUSTERED (wiadomo��ID)
;

ALTER TABLE Zak�ad ADD CONSTRAINT PK_Zak�ad 
	PRIMARY KEY CLUSTERED (zak�adID)
;

ALTER TABLE Zg�oszenie ADD CONSTRAINT PK_Zg�oszenie 
	PRIMARY KEY CLUSTERED (zg�oszenieID)
;



ALTER TABLE Administrator ADD CONSTRAINT FK_Administrator_U�ytkownik 
	FOREIGN KEY (administratorID) REFERENCES U�ytkownik (u�ytkownikID)
;

ALTER TABLE nale�y_do ADD CONSTRAINT FK_Zak�ad 
	FOREIGN KEY (zak�adID) REFERENCES Zak�ad (zak�adID)
;

ALTER TABLE nale�y_do ADD CONSTRAINT FK_Prowadz�cy 
	FOREIGN KEY (prowadz�cyID) REFERENCES Prowadz�cy (prowadz�cyID)
;

ALTER TABLE Ocena ADD CONSTRAINT ocenia 
	FOREIGN KEY (Cel) REFERENCES Student (studentID)
;

ALTER TABLE Ocena ADD CONSTRAINT jest_wystawiana 
	FOREIGN KEY (Podstawa) REFERENCES Przedmiot (przedmiotID)
;

ALTER TABLE Projekt ADD CONSTRAINT jest_realizowany_w 
	FOREIGN KEY (Podstawa) REFERENCES Przedmiot (przedmiotID)
;

ALTER TABLE Prowadz�cy ADD CONSTRAINT FK_Prowadz�cy_U�ytkownik 
	FOREIGN KEY (prowadz�cyID) REFERENCES U�ytkownik (u�ytkownikID)
;

ALTER TABLE prowadzi ADD CONSTRAINT FK_Rozmowa 
	FOREIGN KEY (rozmowaID) REFERENCES Rozmowa (rozmowaID)
;

ALTER TABLE prowadzi ADD CONSTRAINT FK_U�ytkownik 
	FOREIGN KEY (u�ytkownikID) REFERENCES U�ytkownik (u�ytkownikID)
;

ALTER TABLE Przedmiot ADD CONSTRAINT kierownik 
	FOREIGN KEY (Kierownik) REFERENCES Prowadz�cy (prowadz�cyID)
;

ALTER TABLE PrzedmiotObieralny ADD CONSTRAINT FK_PrzedmiotObieralny_Przedmiot 
	FOREIGN KEY (przedmiotObieralnyID) REFERENCES Przedmiot (przedmiotID)
;

ALTER TABLE Raport ADD CONSTRAINT pisze 
	FOREIGN KEY (Autor) REFERENCES Prowadz�cy (prowadz�cyID)
;

ALTER TABLE Raport ADD CONSTRAINT daje_informacje 
	FOREIGN KEY (Punkt_docelowy) REFERENCES Przedmiot (przedmiotID)
;

ALTER TABLE Student ADD CONSTRAINT FK_Student_U�ytkownik 
	FOREIGN KEY (studentID) REFERENCES U�ytkownik (u�ytkownikID)
;

ALTER TABLE Wiadomo�� ADD CONSTRAINT jest_cz�ci� 
	FOREIGN KEY (Zbi�r) REFERENCES Rozmowa (rozmowaID)
;

ALTER TABLE zapisuje_si�_na ADD CONSTRAINT FK_Przedmiot 
	FOREIGN KEY (przedmiotID) REFERENCES Przedmiot (przedmiotID)
;

ALTER TABLE zapisuje_si�_na ADD CONSTRAINT FK_Student 
	FOREIGN KEY (studentID) REFERENCES Student (studentID)
;

ALTER TABLE Zg�oszenie ADD CONSTRAINT potwierdza 
	FOREIGN KEY (Kierownik) REFERENCES Prowadz�cy (prowadz�cyID)
;
