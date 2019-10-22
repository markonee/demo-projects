
SELECT * FROM zaposleni;
SELECT * FROM poslovnice;
SELECT * FROM lokacije;
SELECT * FROM zemlje;
SELECT * FROM kontinenti;
SELECT * FROM ponuda;
SELECT * FROM poslovi;
SELECT * FROM klijenti;
SELECT * FROM opklade;
SELECT * FROM statistika;
SELECT * FROM stavke;

/* Kreiranje tabela i ogranicenja primarnog kljuca */

CREATE TABLE zaposleni (id_zaposlenog NUMBER NOT NULL,
                        ime VARCHAR(20) NOT NULL,
                        prezime VARCHAR(20) NOT NULL,
                        datum_rodjenja DATE NOT NULL,
                        email VARCHAR(40),
                        telefon VARCHAR(15),
                        datum_zaposlenja DATE,
                        id_posla NUMBER NOT NULL,
                        plata NUMBER,
                        id_sefa NUMBER,
                        id_poslovnice NUMBER NOT NULL,
                        CONSTRAINT zaposleni_pk PRIMARY KEY (id_zaposlenog)
                        );

CREATE TABLE poslovi (id_posla NUMBER NOT NULL,
                      naziv_posla VARCHAR(30) NOT NULL,
                      CONSTRAINT poslovi_pk PRIMARY KEY (id_posla));

CREATE TABLE lokacije (id_lokacije NUMBER NOT NULL,
                       adresa_ulice VARCHAR(50),
                       grad VARCHAR(30) NOT NULL,
                       id_zemlje NUMBER NOT NULL,
                       CONSTRAINT lokacije_pk PRIMARY KEY (id_lokacije));

CREATE TABLE zemlje (id_zemlje NUMBER NOT NULL,
                     ime_zemlje VARCHAR(40) NOT NULL,
                     id_kontinenta NUMBER NOT NULL,
                     porez NUMBER NOT NULL,
                     CONSTRAINT zemlje_pk PRIMARY KEY (id_zemlje));

CREATE TABLE kontinenti (id_kontinenta NUMBER NOT NULL,
                         ime_kontinenta VARCHAR(25) NOT NULL,
                         CONSTRAINT kontinenti_pk PRIMARY KEY (id_kontinenta));

CREATE TABLE poslovnice (id_poslovnice NUMBER NOT NULL,
                         naziv_poslovnice VARCHAR(35),
                         id_lokacije NUMBER NOT NULL,
                         id_nadredjene_poslovnice NUMBER,
                         stanje_racuna NUMBER NOT NULL,
                         CONSTRAINT poslovnice_pk PRIMARY KEY (id_poslovnice));


CREATE TABLE klijenti (id_klijenta NUMBER NOT NULL,
                       ime_klijenta VARCHAR(25) NOT NULL,
                       prezime_klijenti VARCHAR(25) NOT NULL,
                       id_poslovnice NUMBER NOT NULL,
                       stanje_racuna NUMBER NOT NULL,
                       CONSTRAINT klijenti_pk PRIMARY KEY (id_klijenta));

CREATE TABLE opklade (id_opklade NUMBER NOT NULL,
                      id_klijenta NUMBER NOT NULL,
                      uplata NUMBER NOT NULL,
                      vrijeme_opklade DATE NOT NULL,
                      CONSTRAINT opklade_pk PRIMARY KEY (id_opklade));

CREATE TABLE ponuda (id_meca NUMBER NOT NULL,
                      naziv_sporta VARCHAR(30),
                      dogadjaj VARCHAR(90),
                      vrijeme_meca DATE NOT NULL,
                      domacin VARCHAR(30),
                      gost VARCHAR(30),
                      pojedini_igrac VARCHAR(30),
                      koeficijent_1 DECIMAL,
                      koeficijent_x DECIMAL,
                      koeficijent_2 DECIMAL,
                      koeficijent_domacin_daje_gol DECIMAL,
                      koeficijent_gost_daje_gol DECIMAL,
                      koeficijent_vise_od_tri_gola DECIMAL,
                      koeficijent_preko_200_koseva DECIMAL,
                      koeficijent_mec_u_tri_seta DECIMAL,
                      koeficijent_mec_u_pet_setova DECIMAL,
                      CONSTRAINT ponuda_pk PRIMARY KEY (id_meca));

CREATE TABLE statistika (id_meca NUMBER NOT NULL,
                         broj_poena_1 NUMBER,
                         broj_poena_2 NUMBER,
                         broj_setova NUMBER,
                         broj_gemova NUMBER,
                         broj_kartona_1 NUMBER,
                         broj_kartona_2 NUMBER,
                         vrijeme_rezultata REAL);

CREATE TABLE stavke (id_stavke NUMBER NOT NULL,
                     id_opklade NUMBER NOT NULL,
                     id_meca NUMBER NOT NULL,
                     ishod VARCHAR(45),
                     CONSTRAINT stavke_pk PRIMARY KEY (id_stavke));

CREATE TABLE arhiva_zaposleni (id_obrisanog NUMBER NOT NULL,
                               ime VARCHAR(50),
                               prezime VARCHAR(50),
                               korisnik_izmijenio VARCHAR(50),
                               datum_izmjene DATE,
                               CONSTRAINT arhiva_zap_pk PRIMARY KEY (id_obrisanog));

CREATE TABLE arhiva_poslovnice (id_obrisane_poslovnice NUMBER NOT NULL,
                                naziv_poslovnice VARCHAR(50),
                                korisnik_izmijenio VARCHAR(50),
                                datum_izmjene DATE,
                                CONSTRAINT arhiva_pos_pk PRIMARY KEY (id_obrisane_poslovnice));

CREATE TABLE arhiva_opklade (id_opklade NUMBER NOT NULL,
                             id_klijenta NUMBER NOT NULL,
                             korisnik_izmijenio VARCHAR(50),
                             datum_izmjene DATE,
                             CONSTRAINT arhiva_op_pk PRIMARY KEY (id_opklade));

/* Dodavanje stranih kljuceva u tabele */

ALTER TABLE zaposleni
ADD CONSTRAINT id_posla_fk
FOREIGN KEY (id_posla) REFERENCES poslovi(id_posla);

ALTER TABLE zaposleni
ADD CONSTRAINT id_sefa_fk
FOREIGN KEY (id_sefa) REFERENCES zaposleni(id_zaposlenog);

ALTER TABLE zaposleni
ADD CONSTRAINT id_poslovnice_fk
FOREIGN KEY (id_poslovnice) REFERENCES poslovnice(id_poslovnice);

ALTER TABLE lokacije
ADD CONSTRAINT id_zemlje_fk
FOREIGN KEY (id_zemlje) REFERENCES zemlje(id_zemlje);

ALTER TABLE zemlje
ADD CONSTRAINT id_kontinenta_fk
FOREIGN KEY (id_kontinenta) REFERENCES kontinenti(id_kontinenta);

ALTER TABLE poslovnice
ADD CONSTRAINT id_lokacije_fk
FOREIGN KEY (id_lokacije) REFERENCES lokacije(id_lokacije);

ALTER TABLE poslovnice
ADD CONSTRAINT id_nadredjene_fk
FOREIGN KEY (id_nadredjene_poslovnice) REFERENCES poslovnice(id_poslovnice);

ALTER TABLE klijenti
ADD CONSTRAINT klijenti_id_poslovnice_fk
FOREIGN KEY (id_poslovnice) REFERENCES poslovnice(id_poslovnice);

ALTER TABLE opklade
ADD CONSTRAINT opklade_id_klijenta_fk
FOREIGN KEY (id_klijenta) REFERENCES klijenti(id_klijenta);

ALTER TABLE stavke
ADD CONSTRAINT stavke_id_meca_fk
FOREIGN KEY (id_meca) REFERENCES ponuda(id_meca);

ALTER TABLE stavke
ADD CONSTRAINT stavke_id_opklade_fk
FOREIGN KEY (id_opklade) REFERENCES opklade(id_opklade);

ALTER TABLE statistika
ADD CONSTRAINT id_meca_fk
FOREIGN KEY (id_meca) REFERENCES ponuda(id_meca);

/* Sekvence */

CREATE SEQUENCE zaposleni_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE poslovi_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE lokacije_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE zemlje_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE kontinenti_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE poslovnice_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE klijenti_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE opklade_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE ponuda_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

CREATE SEQUENCE stavke_sek
START WITH 1
INCREMENT BY 1
NOMINVALUE
NOMAXVALUE
NOCACHE
NOCYCLE;

/* Popunjavanje tabela */

--- Tabela zaposleni

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Marko', 'Nedic', To_Date('03.07.1997', 'dd.mm.yyyy'), 'mnedic1@etf.unsa.ba', '062/537-946', To_Date('01.01.2018', 'dd.mm.yyyy'), 1, 12000, NULL, 1);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Stefan', 'Nedic', To_Date('08.11.2000', 'dd.mm.yyyy'), 'snedic1@etf.unsa.ba', '033/840-662', To_Date('31.12.2017', 'dd.mm.yyyy'), 2, 8000, 1, 1);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Stephen', 'King', To_Date('24.01.1968', 'dd.mm.yyyy'), 'sking1@etf.unsa.ba', '062/331-935', To_Date('12.01.2017', 'dd.mm.yyyy'), 3, 9000, 1, 1);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'LeBron', 'James', To_Date('05.05.1984', 'dd.mm.yyyy'), 'ljames1@etf.unsa.ba', '061/170-878', To_Date('01.01.2018', 'dd.mm.yyyy'), 1, 4000, 1, 2);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Huse', 'Fatkic', To_Date('01.01.1948', 'dd.mm.yyyy'), 'hfatkic1@etf.unsa.ba', '061/131-187', To_Date('05.01.1970', 'dd.mm.yyyy'), 2, 350, 1, 2);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Izet', 'Fazlinovic', To_Date('01.05.1937', 'dd.mm.yyyy'), 'ifazlinovic1@etf.unsa.ba', '063/333-946', To_Date('03.05.2016', 'dd.mm.yyyy'), 3, 8000, 5, 2);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Razija', 'Mujanovic', To_Date('01.09.1968', 'dd.mm.yyyy'), 'raza@etf.unsa.ba', '065/555-555', To_Date('04.04.2010', 'dd.mm.yyyy'), 1, 12000, 3, 3);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Emira', 'Omeragic', To_Date('01.02.1960', 'dd.mm.yyyy'), 'emira@etf.unsa.ba', '062/512-962', To_Date('01.01.2018', 'dd.mm.yyyy'), 2, 10000, 1, 2);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Ana', 'Marija', To_Date('06.06.1996', 'dd.mm.yyyy'), 'anamarija1@etf.unsa.ba', '061/531-936', To_Date('05.05.2014', 'dd.mm.yyyy'), 3, 900, 3, 2);

INSERT INTO zaposleni
VALUES (zaposleni_sek.NEXTVAL, 'Samira', 'Panjeta', To_Date('03.07.1980', 'dd.mm.yyyy'), 'spanjeta1@etf.unsa.ba', '060/530-940', To_Date('02.07.2015', 'dd.mm.yyyy'), 1, 1000, 1, 3);

--- Tabela poslovi
INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Direktor');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Sef proizvodnje');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'IT strucnjak');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Sef skladista');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Cistac');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Racunovodja');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Salterusa');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Sekretarica');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Doktor za prelaze');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Administrator');

INSERT INTO poslovi
VALUES(poslovi_sek.NEXTVAL, 'Moderator');

-- Tabela lokacije
INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Mustafe Behmena 24', 'Sarajevo', 1);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Aleja Lipa', 'Mexico City', 2);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Strosmajerova', 'Madrid', 3);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Ferhadija', 'Brasilia', 4);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Muhameda ef. Pandže', 'Pariz', 5);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Skladi 61', 'Rijad', 6);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Cekalusa 13', 'Vankuver', 7);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Trg djece Dobrinje', 'Moskva', 8);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Hamdije Cemerlica', 'Los Angeles', 9);

INSERT INTO lokacije
VALUES(lokacije_sek.NEXTVAL, 'Trg heroja', 'Herceg Novi', 10);

--- Tabela zemlje

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Bosna i Hercegovina', 1, 2);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Mexico', 2, 10);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Spanija', 1, 3.11);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Brazil', 3, 5.12);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Francuska', 1, 1.1);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Saudijska Arabija', 4, 7.5);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Kanada', 2, 1.13);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Rusija', 1, 20.20);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'USA', 2, 5.15);

INSERT INTO zemlje
VALUES(zemlje_sek.NEXTVAL, 'Crna Gora', 1, 17.00);

-- Tabela kontinenti

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Evropa');

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Sjeverna Amerika');

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Juzna Amerika');

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Azija');

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Afrika');

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Australija');

INSERT INTO kontinenti
VALUES(kontinenti_sek.NEXTVAL, 'Antartik');

-- Tabela poslovnice

INSERT INTO poslovnice
VALUES(poslovnice_sek.NEXTVAL, 'Mexico City - glavni ured', 2, 250000, NULL);

INSERT INTO poslovnice
VALUES(poslovnice_sek.NEXTVAL, 'Sarajevo - glavni ured Evropa', 1, 150000, 1);

INSERT INTO poslovnice
VALUES(poslovnice_sek.NEXTVAL, 'Brasilia - glavni ured J.Amerika', 4, 100000, 1);

-- Tabela klijenti

SELECT *
FROM klijenti;

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Muhamed', 'Medic', 1, 5000);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Muamer', 'Crnovrsanin', 1, 6000);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Midhat', 'Hodo', 3, 7000);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Dejan', 'Krmpotic', 2, 3000);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Mehmed', 'Arslanagic', 2, 2000);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Roberto', 'Carlos', 1, 1000);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Carlos', 'Tevez', 2, 500);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Pablo', 'Escobar', 2, 800);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Radamel', 'Falcao', 3, 950);

INSERT INTO klijenti
VALUES(klijenti_sek.NEXTVAL, 'Paolo', 'Guerrero', 2, 1110);

-- Tabela opklade
INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 3, 10, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 3, 20, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 3, 150, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 3, 38, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 3, 63, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 4, 300, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 5, 60, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 7, 55, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 3, 70, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 7, 50, SYSDATE);

INSERT INTO opklade
VALUES (opklade_sek.NEXTVAL, 8, 200, SYSDATE);

-- Tabela ponuda

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Atletika', 'Svjetsko prvenstvo 2018', To_Date('22.01.2018 8:45:00', 'dd.mm.yyyy hh:mi:ss'), NULL, NULL, 'Usain Bolt', 1.55, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Fudbal', 'Spanska Primera Liga', To_Date('22.01.2018 8:45:00', 'dd.mm.yyyy hh:mi:ss'), 'Barcelona', 'Real', NULL, 1.85, 2.35, 1.85, 1.18, 1.20, 1.45, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Fudbal', 'Premier Liga BiH', To_Date('22.01.2018 4:00:00', 'dd.mm.yyyy hh:mi:ss'), 'FK Zeljeznicar', 'NK Zrinjski', NULL, 1.65, 2.55, 2.10, 1.30, 1.55, 2.00, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Fudbal', 'Premier Liga BiH', To_Date('22.01.2018 3:00:00', 'dd.mm.yyyy hh:mi:ss'), 'GOSK', 'TOSK', NULL, 1.70, 2.35, 4.10, 1.40, 1.35, 2.00, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Fudbal', 'Engleska Premier Liga', To_Date('22.01.2018 8:30:00', 'dd.mm.yyyy hh:mi:ss'), 'Manchester City', 'Manchester United', NULL, 1.40, 3.50, 7.60, 1.08, 1.50, 1.90, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Trke pasa', 'Velesici championship',  To_Date('05.01.2018 3:00:00', 'dd.mm.yyyy hh:mi:ss'), NULL, NULL, 'Hektor', 8.00, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Trke pasa', 'Velesici championship',  To_Date('05.01.2018 3:00:00', 'dd.mm.yyyy hh:mi:ss'), NULL, NULL, 'Viktor', 12.00, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Borba pijetlova', 'Velesici championship',  To_Date('22.01.2018 3:00:00', 'dd.mm.yyyy hh:mi:ss'), NULL, NULL, 'Pujdo', 16.00, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Plivanje', 'Svjetsko prvenstvo Kotor 2018', To_Date('22.01.2018 7:45:00', 'dd.mm.yyyy hh:mi:ss'), NULL, NULL, 'Michael Phelps', 1.55, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Kosarka', 'Premier Liga BiH', To_Date('22.01.2018 7:30:00', 'dd.mm.yyyy hh:mi:ss'), 'KK Bosna', 'KK Igokea', NULL, 1.55, 14.00, 3,30, NULL, NULL, NULL, 3.70, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Kosarka', 'NBA Liga', To_Date('22.01.2018 3:30:00', 'dd.mm.yyyy hh:mi:ss'), 'Philadelphia 76ers', 'LA Lakers', NULL, 1.80, 12.00, 1.90, NULL, NULL, NULL, 1.65, NULL, NULL);

INSERT INTO ponuda
VALUES (ponuda_sek.NEXTVAL, 'Tenis', 'Roland Garros 2018',To_Date('22.01.2018 8:30:00', 'dd.mm.yyyy hh:mi:ss'), 'Dzumhur', 'Basic', NULL, 1.40, NULL, 3.10, NULL, NULL, NULL, NULL, 1.85, 1.85);

-- Tabela statistika (Zavrseni mecevi)
INSERT INTO statistika
VALUES (11, NULL, NULL, NULL, NULL, NULL,NULL, 37.38);

INSERT INTO statistika
VALUES (12, NULL, NULL, NULL, NULL, NULL, NULL, 38.38);

-- Tabela stavke
INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 5, 4, 'X');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 6, 5, '1');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 7, 6, '2');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 8, 8, '1');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 5, 12, '1');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 6, 10, 'preko_200_koseva');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 7, 10, 'X');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 8, 9, '1');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 9, 5, 'domacin_daje_gol');

INSERT INTO stavke
VALUES(stavke_sek.NEXTVAL, 10, 4, 'gost_daje_gol');

SELECT * FROM stavke;


-- Upiti

  -- 10 upita po slobodnom izboru

    -- Prikazi ponudu
       SELECT *
       FROM ponuda;

    -- Prikazi sve odigrane meceve
       SELECT id_meca, domacin, gost
       FROM ponuda
       WHERE vrijeme_meca < SYSDATE;

    -- Ispisi sve zaposlene iz Afrike
      SELECT z.ime || ' ' || z.prezime
      FROM zaposleni z, poslovnice p, lokacije l, zemlje ze, kontinenti ki
      WHERE z.id_poslovnice = p.id_poslovnice AND p.id_lokacije = l.id_lokacije AND l.id_zemlje = ze.id_zemlje AND ze.id_kontinenta = ki.id_kontinenta
      AND ki.naziv_kontinenta LIKE ('Afrika');

    -- Ispisi podatke o svim mecevima na koje se neko opkladio (od 8.1.2018.)
      SELECT p.id_meca, p.naziv_sporta, p.domacin, p.gost, p.pojedini_igrac
      FROM ponuda p, opklade o, stavke s
      WHERE o.id_opklade = s.id_opklade AND s.id_meca = p.id_meca
      AND o.vrijeme_opklade > To_Date('08.01.2018', 'dd.mm.yyyy');

    -- Ispisi sve fudbalske utakmice
      SELECT id_meca, dogadjaj, vrijeme_meca, domacin, gost
      FROM ponuda
      WHERE naziv_sporta LIKE ('Fudbal');

    -- Ispisi statistiku o svim zavrsenim mecevima
      SELECT s.broj_poena_1, s.broj_poena_2, s.broj_setova, s.broj_gemova, s.broj_kartona_1, s.broj_kartona_2, s.vrijeme_rezultata
      FROM statistika s, ponuda p
      WHERE s.id_meca = p.id_meca
      AND p.vrijeme_meca < SYSDATE;

    -- Prikazi lokacije svih poslovnica u formatu Kontinent - Zemlja - Grad - Adresa sortirano po imenu kontinenta
      SELECT k.naziv_kontinenta, z.naziv_zemlje, l.grad, l.adresa_ulice
      FROM poslovnice p, lokacije l, zemlje z, kontinenti k
      WHERE p.id_lokacije = l.id_lokacije AND l.id_zemlje = z.id_zemlje AND z.id_kontinenta = k.id_kontinenta
      ORDER BY k.naziv_kontinenta;

    -- Za svaku poslovnicu prikazi stanje novca, lokaciju i lokaciju njene nadredjene poslovnice
      SELECT p1.stanje_racuna, l1.grad, l2.grad
      FROM poslovnice p1, poslovnice p2, lokacije l1, lokacije l2
      WHERE p1.id_lokacije = l1.id_lokacije AND p1.id_nadredjene_poslovnice = p2.id_poslovnice AND p2.id_lokacije = l2.id_lokacije;

    -- Prikazi podatke o uposlenim i stepen posla A - Direktor, B - Sef proizvodnje, C - IT strucnjak, D - ostali
      SELECT z.ime || ' ' || z.prezime, z.datum_rodjenja, p.naziv_posla, Decode (p.naziv posla,
                                                                                 'Direktor', 'A',
                                                                                 'Sef proizvodnje', 'B',
                                                                                 'IT strucnjak', 'C',
                                                                                 'D')
      FROM zaposleni z, poslovi p
      WHERE z.id_posla = p.id_posla;


    -- Prikazi sve uposlene i njihove sefove za sve poslovnice iz Evrope
      SELECT z.ime || ' ' || z.prezime, s.ime || ' ' || s.prezime
      FROM zaposleni z, zaposleni s, poslovnice p, lokacije l, zemlje z, kontinenti k
      WHERE s.id_zaposlenog = z.id_sefa AND z.id_poslovnice = p.id_poslovnice AND p.id_lokacije = l.id_lokacije AND l.id_zemlje = z.id_zemlje AND z.id_kontinenta = k.id_kontinenta
      AND k.naziv_kontinenta LIKE ('Evropa');



  -- 5 upita sa grupnim funkcijama

    -- Prikazi sumu svih raspolozivih stanja u poslovnicama
       SELECT Sum(stanje_racuna)
       FROM poslovnice;

    -- Prikazi prosjecnu uplatu (svih uplata iznad 1000 pezosa) iz opklada izvrsenih u poslovnici 2
       SELECT p.naziv_poslovnice, Avg(o.uplata)
       FROM opklade o, klijenti k, poslovnice p
       WHERE o.id_klijenta = k.id_klijenta AND k.id_poslovnice = p.id_poslovnice AND p.id_poslovnice = 2
       GROUP BY p.naziv_poslovnice
       HAVING Avg(o.uplata) > 1000;

    -- Prikazi maksimalnu uplatu i podatke o klijentu koji je uplatio
       SELECT k.ime_klijenta || ' ' || k.prezime_klijenti,  Max(o.uplata)
       FROM klijenti k, opklade o
       WHERE o.id_klijenta = k.id_klijenta
       GROUP BY k.ime_klijenta, k.prezime_klijenti;

    -- Prikazi podatke u uposlenom sa maksimalnom platom (kontinent Afrika)
       SELECT z.ime || ' ' || z.prezime, Max(k.plata)
       FROM zaposleni z, poslovnice p, lokacije l, zemlje ze, kontinenti ki
       WHERE z.id_poslovnice = p.id_poslovnice AND p.id_lokacije = l.id_lokacije AND l.id_zemlje = ze.id_zemlje AND ze.id_kontinenta = ki.id_kontinenta
       AND ki.naziv_kontinenta LIKE ('Afrika')
       GROUP BY z.ime, z.prezime;


    -- Prikazi broj prijavljenih klijenta po poslovnicama - sa svim onima koji imaju manje od 5 prijavljenih.
       SELECT p.id_poslovnice, Count(k.id_klijenta)
       FROM poslovnice p, klijenti k
       WHERE k.id_poslovnice = p.id_poslovnice
       GROUP BY p.id_poslovnice
       HAVING Count(k.id_klijenta) < 5;

  -- 5 upita sa koristenjem podupita

    -- Prikazi podatke o zaposlenim koji ima vecu platu od najvece plate iz poslovnice 4
       SELECT z.ime || ' ' || z.prezime, z.plata
       FROM zaposleni z
       WHERE z.plata > (SELECT Max(z1.plata)
                        FROM zaposleni z1
                        WHERE z1.id_poslovnice = 4)
       AND z.id_poslovnice != 4;

    -- Prikazi podatke o zaposlenima koji su zaposleni prije svojih sefova
       SELECT  z.ime || ' ' || z.prezime, z.datum_zaposlenja
       FROM zaposleni z
       WHERE z.datum_zaposlenja > (SELECT z1.datum_zaposlenja
                                   FROM zaposleni z1
                                   WHERE z1.id_zaposlenog = z.id_sefa);

    -- Prikazi sve poslovnice koji imaju vece stanje racuna od glavne poslovnice u Mexicu
       SELECT p.id_poslovnice, p.naziv_poslovnice
       FROM poslovnice p
       WHERE p.stanje_racuna > (SELECT p1.stanje_racuna
                                FROM poslovnice p1, lokacije l, zemlje z
                                WHERE p1.id_lokacije = l.id_lokacije AND l.id_zemlje = z.id_zemlje
                                AND z.naziv_zemlje LIKE ('Mexico'));

    -- Prikazi podatke o klijentu koji ima najvece stanje racuna
       SELECT k.ime_klijenta || ' ' || k.prezime_klijenti
       FROM klijenti k
       WHERE k.stanje_racuna = (SELECT Max(k1.stanje_racuna)
                                FROM klijenti k1);

    -- Prikazi zaposlene koji nemaju sefa
       SELECT z.ime || ' ' || z.prezime
       FROM zaposleni z
       WHERE z.id_zaposlenog != ALL(SELECT z1.id_zaposlenog
                                    WHERE z.id_sefa = z1.id_zaposlenog);

  -- 5 upita koristenjem vise od jednog nivoa podupita

    -- Azuriraj platu zaposlenog sa ID #3 na maksimalnu platu svih zaposlenika koji su se zaposlili nakon Kinga
       UPDATE zaposleni z
       SET z.plata = (SELECT Max(z1.plata)
                      FROM zaposleni z1
                      WHERE z1.datum_zaposlenja > (SELECT z2.datum_zaposlenja
                                                FROM zaposleni z2
                                                WHERE z2.prezime LIKE ('King'))
                  )
       WHERE z.id_zaposlenog = 3;

    -- Azuriraj stanje racuna u glavnoj kladionici kao sumu svih stanja racuna u ostalim kladionicama sa drugih kontinenata (osim Sjeverne Amerike) koji pocinju na A
       UPDATE poslovnice
       SET stanje_racuna = Nvl((SELECT Sum(p.stanje_racuna)
                            FROM poslovnice p, lokacije l, zemlje z, kontinenti k
                            WHERE p.id_lokacije = l.id_lokacije AND l.id_zemlje = z.id_zemlje AND z.id_kontinenta = k.id_kontinenta
                            AND k.ime_kontinenta IN  (SELECT kon.ime_kontinenta
                                                      FROM kontinenti kon
                                                      WHERE kon.ime_kontinenta LIKE('A%'))
                            ),0)
       WHERE id_poslovnice = 1;

    -- Azuriraj posljednju uplata klijenta sa ID #3 na 38000
       UPDATE opklade
       SET uplata = 38000
       WHERE id_opklade = (SELECT id_opklade
                           FROM opklade o
                           WHERE o.id_klijenta = 3
                           AND o.vrijeme_opklade = (SELECT Max(o1.vrijeme_opklade)
                                                    FROM opklade o1
                                                    WHERE o1.id_klijenta = 3)
                          );

   --  Azurira poslovnicu klijenta sa ID #3 - nova poslovnica postaje ona kojoj je nadreðena poslovnica poslovnica iz Sarajeva
       UPDATE klijenti
       SET id_poslovnice = ANY(SELECT p.id_poslovnice
                            FROM poslovnice p
                            WHERE p.id_nadredjene_poslovnice IN (SELECT p1.id_poslovnice
                                                                 FROM poslovnice p1, lokacije l1
                                                                 WHERE p1.id_lokacije = l1.id_lokacije
                                                                 AND l1.grad LIKE ('Sarajevo')
                                                                 )
                            )
      WHERE id_klijenta = 3;

   -- Postavlja porez Crne Gore na porez BiH
      UPDATE zemlje z
      SET z.porez = (SELECT z2.porez
                     FROM zemlje z2
                     WHERE z2.id_zemlje = (SELECT z3.id_zemlje
                                           FROM zemlje z3
                                           WHERE z3.naziv_zemlje LIKE 'Bosna i Hercegovina')
                     )
      WHERE z.naziv_zemlje LIKE ('Crna Gora');

  -- 2 upita sa Top N analizom

    -- Izlistaj prve 3 poslovnice po stanju novca
    SELECT ROWNUM Rang, naziv, stanje
    FROM (SELECT naziv_poslovnice AS naziv, stanje_racuna AS stanje
          FROM poslovnice
          ORDER BY stanje_racuna DESC)
    WHERE ROWNUM <= 3;

    -- Prikazi 3 zaposlenika koji su se najkasnije zaposlili
    SELECT ROWNUM Rang, datum_zaposlenja
    FROM (SELECT datum_zaposlenja
          FROM zaposleni
          ORDER BY datum_zaposlenja ASC)
    WHERE ROWNUM <= 3;

  -- 2 upita sa subtotalom

    --  Suma stanja racuna - po lokacijama i poslovnicama sa ROLLUP
        SELECT l.id_lokacije, p.id_poslovnice, Sum(k.stanje_racuna)
        FROM lokacije l, poslovnice p, klijenti k
        WHERE k.id_poslovnice = p.id_poslovnice AND p.id_lokacije = l.id_lokacije
        GROUP BY ROLLUP(l.id_lokacije, p.id_poslovnice);

    --  Suma stanja racuna - po lokacijama i poslovnicama sa CUBE
        SELECT l.id_lokacije, p.id_poslovnice, Sum(k.stanje_racuna)
        FROM lokacije l, poslovnice p, klijenti k
        WHERE k.id_poslovnice = p.id_poslovnice AND p.id_lokacije = l.id_lokacije
        GROUP BY CUBE(l.id_lokacije, p.id_poslovnice);

  -- 1 upit sa SET operatorom
    -- Prikazi ID svih klijenata koji se nisu opkladili
        SELECT id_klijenta
        FROM klijenti
          MINUS
        SELECT id_klijenta
        FROM opklade;

-- Kreiranje indeksa

-- OBJASNJENJE: primarni kljucevi se najvise koriste od svih atributa u svim tabelama
-- stoga je najlogicnije na njih postaviti indekse

    CREATE INDEX zaposleni_idx
    ON zaposleni(id_zaposlenog);

    CREATE INDEX opklade_idx
    ON opklade(id_opklade);

    CREATE INDEX poslovnice_idx
    ON poslovnice(id_poslovnice);

    CREATE INDEX klijenti_idx
    ON klijenti(id_klijenta);

    CREATE INDEX zemlje_idx
    ON zemlje(id_zemlje);

-- Kreiranje triggera
  CREATE OR REPLACE TRIGGER zaposleni_osiguraj
    BEFORE INSERT ON zaposleni
    BEGIN
      IF(To_Char(SYSDATE, 'DY') IN ('SAT', 'SUN')) OR (To_Char(SYSDATE, 'HH24') NOT BETWEEN '08' AND '18')
      THEN Raise_Application_Error (-20500, 'Mozete insertovati novi slog u tabeli tokom radnih dana ili neradnih sati!');
      END IF;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER osiguraj_update_stavke
    BEFORE UPDATE ON stavke
    BEGIN
      IF(To_Char(SYSDATE, 'DY') IN ('SAT', 'SUN')) OR (To_Char(SYSDATE, 'HH24') NOT BETWEEN '08' AND '18')
      THEN Raise_Application_Error (-20503, 'Mozete azurirati novi slog u tabeli tokom radnih dana ili neradnih sati!');
      END IF;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER osiguraj_delete_opklade
    BEFORE DELETE ON opklade
    BEGIN
      IF(To_Char(SYSDATE, 'DY') IN ('SAT', 'SUN')) OR (To_Char(SYSDATE, 'HH24') NOT BETWEEN '08' AND '18')
      THEN Raise_Application_Error (-20502, 'Mozete brisati slog u tabeli tokom radnih dana ili neradnih sati!');
      END IF;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER prije_ubacivanja_zaposlenog
    BEFORE INSERT ON zaposleni
    FOR EACH ROW
    BEGIN
      :new.id_zaposlenog := zaposleni_sek.NEXTVAL;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER prije_ubacivanja_opklade
    BEFORE INSERT ON opklade
    FOR EACH ROW
    BEGIN
      :new.id_opklade := opklade_sek.NEXTVAL;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER prije_ubacivanja_klijenta
    BEFORE INSERT ON klijenti
    FOR EACH ROW
    BEGIN
      :new.id_klijenta := klijenti_sek.NEXTVAL;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER prije_ubacivanja_mecevi
    BEFORE INSERT ON mecevi
    FOR EACH ROW
    BEGIN
      :new.id_meca := mecevi_sek.NEXTVAL;
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER izmjena_zaposleni       -- upisuje id, ime i prezime osobe koja je izmijenjena kao i korisnika koji je izmijenio (kao i datum izmjene)
    AFTER UPDATE OR DELETE ON zaposleni
    REFERENCING OLD AS OLD NEW AS NEW
    FOR EACH ROW
    BEGIN
      INSERT INTO arhiva_zaposleni VALUES (:old.id_zaposlenog, :old.ime, :old.prezime, USER, SYSDATE)
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER izmjena_poslovnice       -- upisuje id i naziv poslovnice koja je izmijenjena kao i korisnika koji je izmijenio (kao i datum izmjene)
    AFTER UPDATE OR DELETE ON poslovnice
    REFERENCING OLD AS OLD NEW AS NEW
    FOR EACH ROW
    BEGIN
      INSERT INTO arhiva_poslovnice VALUES (:old.id_poslovnice, :old.naziv_poslovnice, USER, SYSDATE)
    COMMIT;
    END;
  /

  CREATE OR REPLACE TRIGGER izmjena_opklade       -- upisuje id opklade i klijenta za opkladu koja je izmijenjena kao i korisnika koji je izmijenio (kao i datum izmjene)
    AFTER UPDATE OR DELETE ON opklade
    REFERENCING OLD AS OLD NEW AS NEW
    FOR EACH ROW
    BEGIN
      INSERT INTO arhiva_opklade VALUES (:old.id_opklade, :old.id_klijenta, USER, SYSDATE)
    COMMIT;
    END;
  /

-- 10 funkcija

  -- Funkcija koja vraca uplatu za poslani ID klijenta

  CREATE OR REPLACE FUNCTION daj_Uplatu(id_klijenta IN klijenti.id_klijenta%TYPE)
    RETURN NUMBER
  IS
    uplata_tmp  NUMBER :=0;
  BEGIN
    SELECT o.uplata
    INTO uplata_tmp
    FROM opklade o
    WHERE o.id_klijenta = id_klijenta;
    RETURN (uplata_tmp);
  END daj_Uplatu;
  /

  -- Funckija koja vraca stanje racuna za poslani ID klijenta

  CREATE OR REPLACE FUNCTION daj_Stanje_Racuna(id_klijenta IN klijenti.id_klijenta%TYPE)
    RETURN NUMBER
  IS
    stanje_tmp  NUMBER :=0;
  BEGIN
    SELECT k.stanje_racuna
    INTO stanje_tmp
    FROM klijenti k
    WHERE k.id_klijenta = id_klijenta;
    RETURN (stanje_tmp);
  END daj_Stanje_Racuna;
  /

  -- Funkcija koja vraca stanje racuna poslovnice - koristit ce se za navedenu stavku
  -- u postavci teksta projekta gdje se od poslovnica trazi da podnesu izvjestaj
  -- njima nadredjenim poslovnicama.

  CREATE OR REPLACE FUNCTION obracunaj_Stanje(id_poslovnice IN poslovnice.id_poslovnice%TYPE)
    RETURN NUMBER
  IS
    stanje_tmp NUMBER := 0;
  BEGIN
    SELECT p.stanje_racuna
    INTO stanje_tmp
    FROM poslovnice p
    WHERE p.id_poslovnice = id_poslovnice;
    RETURN (stanje_tmp);
  END obracunaj_Stanje;
  /

  --  Funkcija koja vraca platu zaposlenog sa ID koji je primljen kao parametar

  CREATE OR REPLACE FUNCTION daj_Platu(id_zaposlenog IN zaposleni.id_zaposlenog%TYPE)
    RETURN NUMBER
  IS
    plata_tmp := 0;
  BEGIN
    SELECT z.plata
    INTO plata_tmp
    FROM zaposleni z
    WHERE z.id_zaposlenog = id_zaposlenog;
    RETURN (plata_tmp);
  END daj_Platu;
  /

  -- Funkcija koja vraca porez zemlje sa navedenim ID

  CREATE OR REPLACE FUNCTION daj_Porez(id_zemlje IN zemlje.id_zemlje%TYPE)
    RETURN NUMBER
  IS
    porez_tmp NUMBER :=0;
  BEGIN
    SELECT z.porez
    INTO porez_tmp
    FROM zemlje
    WHERE z.id_zemlje = id_zemlje;
    RETURN (porez_tmp);
  END daj_Porez;
  /

  -- Funkcija koja vraca poruku koja vraca da li ima neko manju platu od 3800
  CREATE OR REPLACE FUNCTION provjeri_Plate(id_poslovnice IN poslovnice.id_poslovnice%TYPE)
    RETURN VARCHAR
    IS
      poruka_tmp VARCHAR(100) := '';
      broj_ljudi NUMBER := 0;

      CURSOR cur IS
        SELECT Count(*)
        FROM poslovnice p, zaposleni z
        WHERE p.id_poslovnice = id_poslovnice AND z.id_poslovnice = p.id_poslovnice
        AND z.plata > 3800;

    BEGIN
      OPEN cur;
      FETCH cur INTO broj_ljudi;
      CLOSE cur;
      IF broj_ljudi = 0 THEN
        poruka_tmp := 'Ne postoji nijedan zaposleni sa platom < 3800';
      ELSIF
        poruka_tmp := Concat('Postoji ', Concat(broj_ljudi, ' ljudi sa platom < 3800'));
      END IF;
      RETURN (poruka_tmp);
  END provjeri_Plate;
  /

-- Funkcija koja vraca broj zaposlenih koji rade u poslovnici sa ID koji je primljen
-- kao parametar, a da su ti zaposleni poceli raditi prije svojih sefova

  CREATE OR REPLACE FUNCTION provjeri_Datume(id_poslovnice IN poslovnice.id_poslovnice%TYPE)
    RETURN VARCHAR
    IS
      poruka_tmp VARCHAR(100) := '';
      broj_ljudi NUMBER := 0;

      CURSOR cur IS
        SELECT Count(*)
        FROM poslovnice p, zaposleni z, zaposleni z2
        WHERE p.id_poslovnice = id_poslovnice AND z.id_poslovnice = p.id_poslovnice AND z2.id_zaposlenog = z.id_sefa
        AND z.datum_zaposlenja < z2.datum_zaposlenja;

    BEGIN
      OPEN cur;
      FETCH cur INTO broj_ljudi;
      CLOSE cur;
      IF broj_ljudi = 0 THEN
        poruka_tmp := 'Ne postoji nijedan zaposleni da je poceo raditi prije sefa u ovom odjelu.';
      ELSIF
        poruka_tmp := Concat('Postoji ', Concat(broj_ljudi, ' ljudi koji su poceli raditi prije svog sefa u ovom odjelu.'));
      END IF;
      RETURN (poruka_tmp);
  END provjeri_Datume;
  /

  -- Broj ljudi sto se klade u poslovnici sa ID koji je primljen kao parametar
  -- a da imaju opkladu vecu od 100 pezosa

  CREATE OR REPLACE FUNCTION provjeri_Uplate(id_poslovnice IN poslovnice.id_poslovnice%TYPE)
    RETURN VARCHAR
    IS
      poruka_tmp VARCHAR(100) := ' ';
      broj_ljudi NUMBER := 0;

      CURSOR cur IS
        SELECT Count(*)
        FROM poslovnice p, klijenti k, opklade o
        WHERE id_poslovnice = p.id_poslovnice AND p.id_poslovnice = k.id_poslovnice AND k.id_klijenta = o.id_klijenta
        AND o.uplata > 100;

      BEGIN
        OPEN cur;
        FETCH cur INTO broj_ljudi;
        CLOSE cur;
        IF broj_ljudi = 0 THEN
          poruka_tmp := Concat('Ne postoji nijedan klijent u poslovnici sa ID ', Concat(id_poslovnice, ' sa uplatom vecom od 100 pezosa'));
        ELSIF
          poruka_tmp := Concat('Postoji ', Concat(broj_ljudi, Concat(' klijenata u poslovnici sa ID ', Concat(id_poslovnice, ' poslovnice sa uplatom vecom od 100 pezosa'))));
        END IF;
        RETURN (poruka_tmp);
   END provjeri_Uplate;
   /

	-- Funkcija koja vraca poruku da li u unesenoj poslovnici ima neko da nema sefa

  CREATE OR REPLACE FUNCTION provjeri_Sefove(id_poslovnice in poslovnice.id_poslovnice%TYPE)
    RETURN VARCHAR
    IS
	    poruka_tmp VARCHAR(100) :='';
	    broj_ljudi NUMBER :=0;

    CURSOR cur IS
	    SELECT Count(*)
	    FROM zaposleni z, zaposleni s, poslovnice p
	    WHERE z.id_poslovnice = p.id_poslovnice
		  AND z.id_sefa = s.id_zaposlenog
		  AND p.id_poslovnice = id_poslovnice;

    BEGIN
	    OPEN cur;
	    FETCH cur INTO broj_ljudi;
	    CLOSE cur;

	    poruka_tmp := CONCAT(CONCAT(CONCAT(CONCAT('U poslovnici koja ima id = ', id_poslovnice) ' ima ' ), broj_zaposlenih_bez_sefa), ' zaposlenih koji nemaju sefova.' );

	  RETURN (poruka_tmp);
  END provjeri_Sefove;

	-- Funckija koja vraca ime i prezime zaposlenog (unos id_zaposlenog) --

  CREATE OR REPLACE FUNCTION daj_Ime_Zaposlenog (id_zaposlenog IN zaposleni.id_zaposlenog%TYPE)
  RETURN varchar IS
	zap.naziv varchar(100) :='' ;

  BEGIN
	  SELECT z.ime || ' ' || z.prezime
	  INTO zap.naziv
	  FROM zaposleni z
	  WHERE z.id_zaposlenog = id_zaposlenog;

	  RETURN (zap.naziv);
  END daj_Ime_Zaposlenog;

-- 10 procedura

    -- Uvecaj platu svim zaposlenim iz poslovnice koja je primljena ko parametar
    -- za postotak koji je primljen ko parametar

    CREATE OR REPLACE PROCEDURE uvecaj_Plate(id_poslovnice IN poslovnice.id_poslovnice%TYPE, postotak IN NUMBER) IS

    BEGIN
      UPDATE zaposleni z
      SET z.plata = z.plata + plata*postotak
      WHERE z.id_zaposlenog = (SELECT z1.id_zaposlenog
                               FROM zaposleni z1, poslovnice p
                               WHERE z1.id_poslovnice = p.id_poslovnice
                               AND p.id_poslovnice = id.poslovnice);
    END uvecaj_Plate;


    -- Nagradi klijenta sa ID koji je primljen kao parametar procedure
    -- za 100 pezosa na racunu

    CREATE OR REPLACE PROCEDURE nagradi_Klijenta(id_klijenta IN klijenti.id_klijenta%TYPE) IS

      BEGIN
        UPDATE klijenti k
        SET k.stanje_racuna = k.stanje_racuna + 100
        WHERE k.id_klijenta = id_klijenta;

    END nagradi_Klijenta;

    -- Dodaj na racune poslovnice iznos koji je primljen kao parametar

    CREATE OR REPLACE PROCEDURE dodaj_Poslovnici(id_poslovnice IN poslovnice.id_poslovnice%TYPE, iznos IN NUMBER) IS

      BEGIN
        UPDATE poslovnice p
        SET p.stanje_racuna = p.stanje_racuna + iznos
        WHERE p.id_poslovnice = id.poslovnice;

    END dodaj_Poslovnici;

    -- Skini sa racuna poslovnice iznos koji je primljen kao parametar

    CREATE OR REPLACE PROCEDURE oduzmi_Poslovnici(id_poslovnice IN poslovnice.id_poslovnice%TYPE, iznos IN NUMBER) IS

      trenutno_stanje NUMBER :=0;

      BEGIN
        trenutno_stanje = (SELECT p.stanje_racuna
                           FROM poslovnice p
                           WHERE p.id_poslovnice = id_poslovnice);

        IF trenutno_stanje < iznos THEN
          Raise_Application_Error(-20803, 'Data poslovnica nema dovoljno sredstava za isplatu!');
        ELSIF

          UPDATE poslovnice p
          SET p.stanje_racuna = p.stanje_racuna + iznos
          WHERE p.id_poslovnice = id.poslovnice;

        END IF;

    END oduzmi_Poslovnici;


    -- Dodaj novi red u tabeli opklade sa primljenim podacima

    CREATE OR REPLACE PROCEDURE insert_Opklade(uplata IN opklade.uplata%TYPE, vrijeme_uplate IN opklade.vrijeme_opklade%TYPE, id_klijenta IN opklade.id_klijenta%TYPE) IS

      ima_ID NUMBER := 0;

      BEGIN
        ima_ID = SELECT(Count(*)
                        FROM klijenti k
                        WHERE k.id_klijenta = id_klijenta);

        IF ima_ID = 0 THEN
          Raise_Application_Error(-20505, 'Ne postoji nijedan klijent sa navedenim ID parametrom!');
        ELSIF
          IF uplata < 0 THEN
            Raise_Application_Error(-20508, 'Uplata ne smije biti negativna!');
            ELSIF
              INSERT INTO opklade
              VALUES(opklade_sek.NEXTVAL, uplata, vrijeme_uplate, id_klijenta);
            END IF;
        END IF;

    END insert_Opklade;

    --  Dodaj novi red u tabelu lokacije sa primljenim podacima

    CREATE OR REPLACE PROCEDURE insert_Lokacije(adresa_ulice IN lokacije.adresa_ulice%TYPE, grad IN lokacije.grad%TYPE, id_zemlje IN lokacije.id_zemlje%TYPE) IS

      ima_ID NUMBER := 0;

      BEGIN
        ima_ID = (SELECT Count(*)
          FROM zemlje z
          WHERE z.id_zemlje = id_zemlje);

        IF ima_ID = 0 THEN
          Raise_Application_Error(-20521, 'Ne postoji nijedna zemlja sa ID primljenim kao parametrom!');
          ELSIF
            INSERT INTO lokacije
            VALUES(lokacije_sek.NEXTVAL, adresa_ulice, grad, id_zemlje);
        ENDIF;

    END insert_Lokacije;

    -- Dodaj novi red u tabelu zemlje sa primljenim podacima

    CREATE OR REPLACE PROCEDURE insert_Zemlje(naziv_zemlje IN zemlje.naziv_zemlje%TYPE, porez IN zemlje.porez%TYPE, id_kontinenta IN zemlje.id_kontinenta%TYPE) IS

      ima_ID NUMBER := 0;

      BEGIN
        ima_ID = (SELECT Count(*)
          FROM kontinenti k
          WHERE k.id_kontinenta = id_kontinenta);

        IF ima_ID = 0 THEN
          Raise_Application_Error(-20521, 'Ne postoji nijedan kontinent sa ID primljenim kao parametrom!');
          ELSIF
            INSERT INTO zemlje
            VALUES(zemlje_sek.NEXTVAL, naziv_zemlje, porez, id_kontinenta);
        ENDIF;

    END insert_Zemlje;

    -- Dodaj novi red u tabelu poslovi sa primljenim podacima

    CREATE OR REPLACE PROCEDURE insert_Poslovi(naziv_posla IN poslovi.naziv_posla%TYPE) IS

      BEGIN
        INSERT INTO poslovi VALUES(poslovi_sek.NEXTVAL, naziv_posla);

    END insert_Poslovi;


    -- Brise poslovnicu kojoj je stanje racuna manje od
    -- praga koji je primljen kao parametar

    CREATE OR REPLACE PROCEDURE ugasi_Poslovnicu(prag IN NUMBER) IS

      BEGIN
        DELETE poslovnice
        WHERE stanje_racuna < prag;

    END ugasi_Poslovnicu;

    -- Brise uposlenika sa ID primljenim kao parametar

      CREATE OR REPLACE PROCEDURE daj_Otkaz(id_zaposlenog IN NUMBER) IS

        BEGIN
          DELETE zaposleni z
          WHERE z.id_zaposlenog = id_zaposlenog;

      END daj_Otkaz;

   -- Omogucava klijentu da promijeni poslovnicu u kojoj vrsi uplate
   -- id nove poslovnice se prima kao parametar procedure

   CREATE OR REPLACE PROCEDURE promijeni_Poslovnicu(id_klijenta IN NUMBER, id_nove IN NUMBER) IS

    postoji_ID NUMBER := 0;

    BEGIN
      postoji_ID = (SELECT Count(*)
                    FROM poslovnice p
                    WHERE p.id_poslovnice = id_nove);

     IF postoji_ID = 0 THEN
      Raise_Application_Error(-20505, 'Ne postoji nijedna poslovnica sa zadatim ID!');

      ELSIF
        UPDATE klijenti k
        SET k.id_poslovnice = id_nove
        WHERE k.id_klijenta = id_klijenta;
     END IF;

  END promijeni_Poslovnicu;


-- PL/SQL skripta
-- obavlja podnosenje izvjestaja o radu svih poslovnica
-- na stanje glavne poslovnice ce se dodavati stanje trenutne
-- stanje trenutne ce biti 0.

-- Slicno ovom primjeru, moglo bi se napraviti i obratno
-- tj. da glavna poslovnica rasporedjuje svoja sredstva na njoj
-- podredjene poslovnice.

DECLARE
  broj_poslovnica NUMBER := 0;
  brojac NUMBER := 1;          -- necemo traziti izvjestaj glavne poslovnice
  trenutno_stanje NUMBER := 0;

  CURSOR cur IS
    SELECT Count(*)
    FROM poslovnice
    WHERE id_nadredjene_poslovnice IS NOT NULL;
  -- kursor ce dobavljati podatke u broj_poslovnica
  -- broji sve poslovnice osim glavne

BEGIN
  OPEN cur;
  FETCH cur INTO broj_poslovnica;
  CLOSE cur;

  WHILE brojac <= broj_poslovnica LOOP
    trenutno_stanje = obracunaj_Stanje(brojac);
    -- u varijabli trenutno_stanje se nalazi vraceno
    -- stanje novca u poslovnici sa ID brojem jednakom brojacu

    oduzmi_Poslovnici(brojac, trenutno_stanje);      -- stanje trenutne poslovnice
                                                     -- se umanjuje za iznos trenutno_stanje

    dodaj_Poslovnici(1, trenutno_stanje);            -- stanje glavne poslovnice
                                                     -- se uvecava za iznos trenutno_stanje

    brojac := brojac + 1;
  END LOOP;
  COMMIT;
END;





