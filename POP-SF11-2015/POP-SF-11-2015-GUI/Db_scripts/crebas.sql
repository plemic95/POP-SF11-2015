--tabele moraju bas ovim redom jer ne mozes npr napraviti
--namestaj koji referencira na tip i akciju ako ih prethodno
--nisi napravio
 
 

CREATE TABLE TipNamestaja (
    ID INT PRIMARY KEY IDENTITY(1, 1),
    Naziv VARCHAR(80),
    Obrisan BIT    
    );
 
CREATE TABLE Akcije(
    ID INT PRIMARY KEY IDENTITY(1, 1),
    Obrisan BIT,
    ImeAkcije VARCHAR(100),
    --moglo bi ovako pa odatle da se preracuna procenat
    Popust tinyint,
    DatumPocetka DateTime,
    DatumZavrsetka DateTime 
)

 
 
CREATE TABLE Namestaj(
    --primarni kljuc i krece od 1
    ID INT PRIMARY KEY IDENTITY(1, 1),
	Naziv VARCHAR(100),
	Sifra VARCHAR(100),
    TipNamestajaID INT,
	Cena DECIMAL(9, 2),
    --AkcijaID INT DEFAULT 0,
   -- Cena NUMERIC(9, 2),
    KolicinaUMagacinu INT,
	--AkcijskaCena INT,
	--AkcijskaCena NUMERIC(9, 2),
    Obrisan BIT,
    --tipNamestajaID je id iz tabele TipNamestaja
    --Akcija ID je id iz tabele Akcije
    --FOREIGN KEY (AkcijaID) REFERENCES Akcije(ID),
    FOREIGN KEY(tipNamestajaId) REFERENCES TipNamestaja(ID)
    ); 
 
 
--gledaj datum between
 
 
CREATE TABLE DodatnaUsluga(
    ID INT PRIMARY KEY IDENTITY(1, 1),
    Naziv VARCHAR(100),
	Cena DECIMAL(9, 2),
    --Cena NUMERIC(9, 2),
	Obrisan BIT,
)
 

CREATE TABLE Korisnik(
    ID INT PRIMARY KEY IDENTITY(1, 1),
    Ime VARCHAR(50),
    Prezime VARCHAR(50),
    KorisnickoIme VARCHAR(20),
    Lozinka VARCHAR(20),
    --namapirao u enumeraciji direktno, admin = 1, prodavac = 2
    TipKorisnika smallint,
	Obrisan BIT,
)

CREATE TABLE Salon(
    Naziv VARCHAR(50),
    Adresa VARCHAR(50),
    Telefon VARCHAR(50),
    Email VARCHAR(50),
	WebSajt VARCHAR(50),
    PIB INT,
    Maticnibroj INT,
    BrojZiroRacuna VARCHAR(50),
)
   
CREATE TABLE Prodaja(
    ID INT PRIMARY KEY IDENTITY(1, 1),
    DatumProdaje DateTime,
    BrojRacuna VARCHAR(20),
	--NamestajZaProdaju smallint,
	--ProdataUsluga smallint,
    Kupac VARCHAR(20),
    Cena DECIMAL(9, 2)
 
)

 
--za sada ovako, posle vidi ako pravi svinjac
--dodati ogranicenja kojekakva verovatno

CREATE TABLE ProdatiNamestaj(
    IDProdaje INT,
    IDNamestaja INT
    FOREIGN KEY (IDProdaje) REFERENCES Prodaja(ID),
    FOREIGN KEY(IDNamestaja) REFERENCES Namestaj(ID)
)
 
CREATE TABLE ProdateUsluge(
    IDProdaje INT,
    IDDodatneUsluge INT
    FOREIGN KEY (IDProdaje) REFERENCES Prodaja(ID),
    FOREIGN KEY(IDDodatneUsluge) REFERENCES DodatnaUsluga(ID)
)
 
 
 
 
 
 
 
 
 
 
 
 
     
 
 
 
 
/* 
    VRACA CONNECTION STRING
    select
    'data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN'
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name()
    end
from sys.server_principals
where name = suser_name()
*/
 
 
 
 
 
/*
One-to-one: Use a foreign key to the referenced table:
 
student: student_id, first_name, last_name, address_id
address: address_id, address, city, zipcode, student_id # you can have a
                                                        # "link back" if you need
One-to-many: Use a foreign key on the many side of the relationship linking back to the "one" side:
 
teachers: teacher_id, first_name, last_name # the "one" side
classes:  class_id, class_name, teacher_id  # the "many" side
 
Many-to-many: Use a junction table (example):
 
student: student_id, first_name, last_name
classes: class_id, name, teacher_id
student_classes: class_id, student_id     # the junction table
*/
--CREATE TABLE TipNamestaja (
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	Naziv VARCHAR(80),
--	Obrisan BIT
--	)

--GO
--CREATE TABLE Namestaj (
--	Id INT PRIMARY KEY IDENTITY(1,1),
--	TipNamestajaId INT,
--	Naziv VARCHAR(100),
--	Cena NUMERIC(9,2),
--	Kolicina INT,
--	Obrisan BIT,
--	FOREIGN KEY (TipNamestajaId) REFERENCES TipNamestaja(Id)
--	)