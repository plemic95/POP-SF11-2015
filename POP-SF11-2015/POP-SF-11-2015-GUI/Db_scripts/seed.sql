INSERT INTO TipNamestaja  VALUES ('Krevet', 0);
INSERT INTO TipNamestaja  VALUES ('Lampa', 0);
INSERT INTO TipNamestaja  VALUES ('Sto', 0);
INSERT INTO TipNamestaja  VALUES ('Podna Garnitura', 0);
 
INSERT INTO Akcije
    VALUES( 0, 'Novogodisnja', 10, '01-12-2017', '01-20-2018');
INSERT INTO Akcije
    VALUES( 0, 'Neka', 25, '04-19-2017', '05-11-2018');
--ovde cemo jos videti da li ne treba ID
 
INSERT INTO Namestaj (Naziv, Sifra, TipNamestajaID, Cena, KolicinaUMagacinu, Obrisan)
    VALUES ('Francuski lezaj', 'FL145', 1, 12000, 22, 0);
INSERT INTO Namestaj (Naziv, Sifra, TipNamestajaID, Cena, KolicinaUMagacinu, Obrisan)
    VALUES ('Stona lampa', 'SL584', 2,  1700, 12, 0);
INSERT INTO Namestaj (Naziv, Sifra, TipNamestajaID, Cena, KolicinaUMagacinu, Obrisan)
    VALUES ('Zidna lampa', 'ZL22', 2,  2900, 24, 0);
INSERT INTO Namestaj (Naziv, Sifra, TipNamestajaID, Cena, KolicinaUMagacinu, Obrisan)
    VALUES ('Radni sto', 'RS89', 3,  4500, 10, 0);
 

 
INSERT INTO DodatnaUsluga
    VALUES('Dostava', 500, 0);
INSERT INTO DodatnaUsluga
    VALUES('Montaza', 750, 0);

 
INSERT INTO Korisnik
    VALUES('Milan', 'Plemic', 'plemic95', '12345', 1, 0);
INSERT INTO Korisnik
    VALUES('Marko', 'Markovic', 'markok', '555', 2, 0);
INSERT INTO Korisnik
    VALUES('Deni', 'Tomasovic', 'devito95', '333', 2, 1);
INSERT INTO Korisnik
    VALUES('Goran', 'Nikolic', 'goran1', 'gg99', 2, 0);

 
INSERT INTO Salon
    VALUES('PLEMKEA', 'Antona Cehova 28', '061/2737551', 'milanplemic@yahoo.com', 'www.plemkea.com', 810855, 6783, '190-234568200-200');
 
      
INSERT INTO Prodaja
    VALUES('02-12-2017', '101', 'Mile', 17000 );
INSERT INTO Prodaja
    VALUES('06-19-2017', '105', 'Bili', 12000 );
--INSERT INTO Prodaja
    --VALUES('11-23-2017', '109', 'Cane', 10000 );
 

INSERT INTO ProdatiNamestaj
    VALUES(1, 1)
INSERT INTO ProdatiNamestaj
    VALUES(2, 2)
INSERT INTO ProdatiNamestaj
    VALUES(2, 3)
 
 
INSERT INTO ProdateUsluge
    VALUES(1, 1);
 
 
 
 
 
 
 
 
 
 
 
 
     
 
 
 
 
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