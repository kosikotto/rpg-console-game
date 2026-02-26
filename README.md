⚔️ Console RPG – Shakes & Fidget Style

Ez a projekt egy konzol alapú szerepjáték, amely a népszerű Shakes & Fidget világából merít inspirációt. Bár a felület minimalista, a háttérben egy komplex, vállalati környezetben is megálló szoftverarchitektúra biztosítja a működést.

🏗️ Architekturális megoldások

A fejlesztés fő fókusza nem csupán a játékmeneten, hanem a tiszta kód (Clean Code) és a szétválasztott felelősségi körök alkalmazásán volt:

1. Rétegezett architektúra (Layered Architecture):
2. Domain: Az üzleti logika és a játék entitásai.
3. Application: A játékmenet folyamatai és szolgáltatásai.
4. Infrastructure: Adatelérés és külső kapcsolatok kezelése.
5. UI (Presentation): A konzolos megjelenítésért felelős réteg.
6. Dependency Injection (DI): A komponensek közötti laza csatolás biztosítása érdekében.
7. Adatkezelés: Entity Framework használata DB-First megközelítéssel, MSSQL adatbázis-háttérrel.

🎮 Főbb jellemzők

1. Moduláris felépítés: A logika, az adatelérés és a megjelenítés élesen elkülönül, így bármelyik réteg cserélhető a többi módosítása nélkül.
2. Verziózott adatbázis: A játék állapota és a karakteradatok MSSQL adatbázisban tárolódnak.
3. Inspirált játékmenet: Karakterfejlődés, küldetések és statisztikák a klasszikus RPG mechanikák alapján.

⚠️ Ismert hibák és folyamatban lévő fejlesztések

A projekt jelenleg aktív fejlesztés alatt áll. Jelenleg a mentés és betöltés funkció korlátozottan működik:

1. Hiba oka: A fájlrendszer-ellenőrzési logika (mappa és fájl létezésének validálása) hiánya miatt a program bizonyos környezetekben nem találja az írási útvonalat.
2. Státusz: A hiba javítása és az I/O kezelés finomhangolása a következő fejlesztési ciklus része.

🛠️ Technológiák

1. Nyelv: C# / .NET
2. Adatbázis: MSSQL
3. ORM: Entity Framework
4. Minták: Repository pattern, Dependency Injection, Layered Architecture

💻 Futtatás

A projekt futtatása rendkívül egyszerű, nem igényel bonyolult konfigurációt:
1. Klónozd a repository-t a gépedre.
2. Nyisd meg a solution fájlt (pl. Visual Studio-ban).
3. Nyomj a "Play" (Futtatás) gombra, és a konzol azonnal elindul.
