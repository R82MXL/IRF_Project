IRF Project R82MXL

Megvalósítandó tételek:
Adatok importálása
	A) Olvasás CSV fájlból
Adatfeldolgozás
	A) LINQ lekérdezés használata legalább egy WHERE feltétellel
Adatok exportálása / megjelenítése
	B) Formázott Excel létrehozása a rendelkezésre álló adatokból
Általános elemek
	D) Timer használata

A feladatban egy egyszerű népesség kezelő kerül megvalósításra a fent megadott tételek figyelembevételével.

Egy .csv file kerül importálásra, ami kétféleképpen történhet meg.
A file-ban emberek adatai szerepelnek az alábbiaknak megfelelően:
	Keresztnév, Vezetéknév, Kor, Nem, Megye, Város

Alapértelmezetten egy timer segítségével, amely egy kódba égetett útvonalról olvassa be az adatok.
Ez 5 másodpercre van időzítve, de figyelembe veszi azt, hogy a datagridview üres-e.
Amennyiben nem üres, akkor nem fut le (a felhasználó dolgozik az adatokkal).

A másik lehetőség a "Read" gomb segítségével történhet, amellyen más útvonalról is lehet tallózni a .csv file-t.
Itt egy esetleges fejlesztési lehetőség hogy az időzített beolvasásnál a felhasználó által meg lehessen változtatni az útvonalat és esetleg a beolvasás frekvenciáját.

A beolvasott adatok a már fentebb említett datagridview-ben kerülnek megjelenítésre, ahol egy egyszerű LINQ lekérdezéssel lehet az adatokat szűrni.
Jelenleg csak nemre (gender) lehet szűrni. Ez egy további fejlesztési lehetőség, hogy más adatokra is lehessen szűkíteni az adatokat.

Exportálásra kétféle lehetőség van.
Excel és CSV fájlba is lehet kiírni a szűrés után megmaradt sorokat.