================================================================================
CLINIC VISIT SYSTEM - INFORMACJE SYSTEMOWE
================================================================================

OPIS PROJEKTU:
System zarządzania wizytami lekarskimi i dokumentacją medyczną. Umożliwia
zarządzanie pacjentami, harmonogramem wizyt, historią medyczną, receptami
i zaświadczeniami lekarskimi.

================================================================================
1. INSTALACJA I URUCHOMIENIE
================================================================================

WYMAGANIA:
- .NET 8.0 lub nowsza
- SQLite (baza danych - nie wymaga instalacji)
- Visual Studio 2022 / VS Code

BUDOWANIE PROJEKTU:
1. Otwórz terminal w głównym katalogu projektu
2. Uruchom: dotnet build
3. Lub uruchom zadanie w VS Code: Tasks > Run Task > build: all

URUCHOMIENIE:
1. WebAPI (Backend):
   dotnet run --project WebAPI/WebAPI.csproj
   Dostęp: https://localhost:7186 (Swagger: https://localhost:7186/swagger)

2. BlazorServer (Interfejs dla lekarzy):
   dotnet run --project BlazorServer/BlazorServer.csproj
   Dostęp: https://localhost:7001

3. BlazorClient (Alternatywny interfejs):
   dotnet run --project BlazorClient/BlazorClient.csproj
   Dostęp: https://localhost:7002

================================================================================
2. DANE TESTOWE (SEEDING)
================================================================================

Baza danych jest automatycznie inicjalizowana z danymi testowymi przy
pierwszym uruchomieniu. Dane są seeded z pliku: Infrastructure/DataSeeder.cs

KONTA DO LOGOWANIA:

┌─────────────────────────────────────────────────────────────────────────────┐
│ LEKARZE                                                                     │
├─────────────────────────────────────────────────────────────────────────────┤
│ Email:      john.doe@example.com                                            │
│ Hasło:      password123                                                     │
│ Rola:       Doctor (Lekarz)                                                │
│ Nazwa:      John Doe                                                       │
│ Specjalizacja: Cardiology (Kardiologia)                                    │
│ Uprawnienia:                                                               │
│   - Przeglądanie pacjentów                                                 │
│   - Zarządzanie wizytami                                                   │
│   - Dodawanie historii medycznej                                           │
│   - Wystawianie recept                                                     │
│   - Wystawianie zaświadczeń lekarskich                                     │
└────────────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────────────┐
│ PACJENCI                                                                    │
├─────────────────────────────────────────────────────────────────────────────┤
│ Email:      jane.smith@example.com          Hasło: password123             │
│ Email:      anna.kowalska@example.com       Hasło: password123             │
│ Email:      marek.nowak@example.com         Hasło: password123             │
│ Rola:       Patient (Pacjent)                                              │
│ Uprawnienia:                                                                │
│   - Przeglądanie swoich wizyt                                              │
│   - Przeglądanie historii medycznej                                        │
│   - Pobieranie recept i zaświadczeń                                        │
└─────────────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────────────┐
│ ADMINISTRATOR                                                               │
├─────────────────────────────────────────────────────────────────────────────┤
│ Email:      admin@example.com                                               │
│ Hasło:      admin123                                                        │
│ Rola:       Admin                                                           │
│ Uprawnienia:                                                                │
│   - Zarządzanie wszystkimi użytkownikami                                    │
│   - Pełny dostęp do systemu                                                 │
└─────────────────────────────────────────────────────────────────────────────┘

DANE PACJENTÓW:

1. Jane Smith
   PESEL:        12345678901
   Data urodzenia: 01.01.1990
   Płeć:         Kobieta
   Adres:        123 Main St

2. Anna Kowalska
   PESEL:        98765432100
   Data urodzenia: 15.06.1985
   Płeć:         Kobieta
   Adres:        ul. Kwiatowa 5, Warszawa

3. Marek Nowak
   PESEL:        55031208193
   Data urodzenia: 12.03.1955
   Płeć:         Mężczyzna
   Adres:        ul. Lipowa 12, Kraków

PRZYKŁADOWE WIZYTY:
- Zaplanowane wizyty (Scheduled)
- Zakończone wizyty (Completed)
- Anulowane wizyty (Cancelled)

Typy wizyt: In-Person (osobiście), Telemedicine (tele-wizyta), HomeVisit (domowa)

================================================================================
3. STRUKTURA BAZY DANYCH
================================================================================

MAIN TABLES:
- Users (Użytkownicy)
- Doctors (Lekarze)
- Patients (Pacjenci)
- Visits (Wizyty)
- MedicalRecords (Historia medyczna)
- Medications (Leki)
- Prescriptions (Recepty)
- PrescriptionItems (Pozycje recept)
- SickLeaves (Zaświadczenia lekarskie)

Baza danych: ClinicVisitSystem.db (SQLite)

================================================================================
4. ARCHITEKTURA PROJEKTU
================================================================================

Domain/          - Modele domeny i interfejsy (core biznesowy)
Infrastructure/  - Implementacja bazy danych, seeding, repozytoria
Application/     - Usługi biznesowe, walidatory, mapowania
WebAPI/          - REST API (ASP.NET Core Controllers)
BlazorServer/    - Interfejs dla lekarzy (Razor Components)
BlazorClient/    - Alternatywny interfejs (Blazor WebAssembly)
SharedKernel/    - Wspólne enumeracje i DTOs

================================================================================
5. ENDPOINTY API
================================================================================

AUTHENTICATION:
POST   /api/auth/login              - Logowanie
POST   /api/auth/logout             - Wylogowanie

USERS:
GET    /api/users                   - Lista użytkowników
GET    /api/users/{id}              - Szczegóły użytkownika
POST   /api/users                   - Nowy użytkownik
PUT    /api/users/{id}              - Aktualizacja użytkownika
DELETE /api/users/{id}              - Usunięcie użytkownika

DOCTORS:
GET    /api/doctors                 - Lista lekarzy
GET    /api/doctors/{id}            - Szczegóły lekarza
POST   /api/doctors                 - Nowy lekarz
PUT    /api/doctors/{id}            - Aktualizacja lekarza

PATIENTS:
GET    /api/patients                - Lista pacjentów
GET    /api/patients/{id}           - Szczegóły pacjenta
POST   /api/patients                - Nowy pacjent
PUT    /api/patients/{id}           - Aktualizacja pacjenta

VISITS:
GET    /api/visits                  - Lista wizyt
GET    /api/visits/{id}             - Szczegóły wizyty
POST   /api/visits                  - Nowa wizyta
PUT    /api/visits/{id}             - Aktualizacja wizyty
DELETE /api/visits/{id}             - Anulowanie wizyty

MEDICAL RECORDS:
GET    /api/medicalrecords          - Lista historii
POST   /api/medicalrecords          - Nowa historia
PUT    /api/medicalrecords/{id}     - Aktualizacja historii

PRESCRIPTIONS:
GET    /api/prescriptions           - Lista recept
POST   /api/prescriptions           - Nowa recepta
PUT    /api/prescriptions/{id}      - Aktualizacja recepty

SICK LEAVES:
GET    /api/sickleaves              - Lista zaświadczeń
POST   /api/sickleaves              - Nowe zaświadczenie

MEDICATIONS:
GET    /api/medications             - Lista leków

Dokumentacja API: https://localhost:7186/swagger (Swagger UI)

================================================================================
6. LOGI SYSTEMOWE
================================================================================

Logi są zapisywane w folderze: WebAPI/logs/
Konfiguracja: nlog.config

================================================================================
7. ROZWIĄZYWANIE PROBLEMÓW
================================================================================

Problem: "Cannot connect to database"
Rozwiązanie:
- Sprawdź, czy baza danych nie jest otwarta w innej aplikacji
- Usuń folder bin/ i obj/ oraz odbuduj projekt
- Upewnij się, że baza danych ma uprawnienia zapisu

Problem: Port już w użyciu
Rozwiązanie:
- Zmień port w launchSettings.json
- Lub zamknij aplikację używającą portu

Problem: Błędy walidacji
Rozwiązanie:
- Sprawdź logi systemowe w WebAPI/logs/

================================================================================
8. FLAGI FUNKCJI I KONFIGURACJA
================================================================================

Plik konfiguracji: appsettings.json (lub appsettings.Development.json)

Opcje:
- DatabasePath: Ścieżka do bazy danych
- LogLevel: Poziom logowania (Debug, Info, Warning, Error)
- CORS: Konfiguracja Cross-Origin requests

================================================================================
9. KONTAKT I WSPARCIE
================================================================================

W razie pytań lub problemów zapoznaj się z dokumentacją użytkownika
dołączoną do projektu:
- DOCUMENTATION_USER_BLAZOR_SERVER.md  (dla lekarzy)
- DOCUMENTATION_USER_BLAZOR_CLIENT.md  (dla pacjentów)
- DOCUMENTATION_WEBAPI.md              (dla deweloperów)