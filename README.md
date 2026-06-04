# CLINIC VISIT SYSTEM - INFORMACJE DO URUCHOMIENIA

## Spis treści

1. [Skład grupy projektowej](#1-skład-grupy-projektowej)
2. [Opis projektu](#2-opis-projektu)
3. [Wymagania techniczne](#3-wymagania-techniczne)
4. [Uruchomienie projektu](#4-uruchomienie-projektu)
5. [Konta do logowania](#5-konta-do-logowania)
6. [Najważniejsze funkcjonalności](#6-najważniejsze-funkcjonalności)
7. [Architektura rozwiązania](#7-architektura-rozwiązania)
8. [Struktura bazy danych](#8-struktura-bazy-danych)
9. [Endpointy WebAPI](#9-endpointy-webapi)
10. [Logi systemowe](#10-logi-systemowe)
11. [Rozwiązywanie problemów](#11-rozwiązywanie-problemów)
12. [Dokumentacja użytkownika](#12-dokumentacja-użytkownika)

## 1. Skład grupy projektowej

- Justyna Sarkowicz
- Amelia Madej
- Weronika Duda

## 2. Opis projektu

Clinic Visit System to aplikacja biznesowa do obsługi przychodni medycznej. System obsługuje lekarzy, pacjentów, wizyty, dokumentację medyczną, recepty, leki, zwolnienia lekarskie oraz panel administratora z wykrywaniem anomalii.

Projekt wykorzystuje bazę SQLite:
- `ClinicVisitSystem.db`

Hasła użytkowników w bazie są przechowywane jako hashe PBKDF2, a logowanie w WebAPI i Blazor WebAssembly korzysta z tokenów JWT.

## 3. Wymagania techniczne

- .NET 8.0 SDK lub nowszy
- Visual Studio 2022 albo Visual Studio Code
- SQLite nie wymaga osobnej instalacji, ponieważ baza jest plikiem `.db`

Przed uruchomieniem profili HTTPS warto wykonać:

```bash
dotnet dev-certs https --trust
```

Bez certyfikatu developerskiego aplikacje mogą uruchamiać się po HTTP, ale profile HTTPS mogą zwracać błąd: `"Unable to configure HTTPS endpoint"`.

## 4. Uruchomienie projektu

Zalecana kolejność uruchamiania:

### 1. WebAPI

```bash
dotnet run --project WebAPI/WebAPI.csproj --launch-profile https
```

**Adresy:**
- https://localhost:7013
- http://localhost:5196
- Swagger: https://localhost:7013/swagger

### 2. BlazorServer - panel lekarza i administratora

```bash
dotnet run --project BlazorServer/BlazorServer.csproj --launch-profile BlazorServer
```

**Adresy:**
- https://localhost:7148
- http://localhost:5251

### 3. BlazorClient - Blazor WebAssembly, panel pacjenta

```bash
dotnet run --project BlazorClient/BlazorClient.csproj --launch-profile https
```

**Adresy:**
- https://localhost:7003
- http://localhost:5299

### Konfiguracja klienta WASM

- `BlazorClient/wwwroot/appsettings.json`
- `ClinicVisitAPIUrl = https://localhost:7013`
- `ClinicVisitServerUrl = https://localhost:7148`

## 5. Konta do logowania

### Role w bazie

- 0 = Administrator
- 1 = Doctor
- 2 = Patient

Hasła są zapisane w bazie jako PBKDF2, ale poniższe hasła jawne są poprawnymi danymi logowania do kont seedowanych.

### Administratorzy

1. **admin@example.com**
   - Hasło: `admin123`
   - Rola: Administrator

2. **admin@clinic.local**
   - Hasło: `admin123`
   - Rola: Administrator

### Lekarze

1. **john.doe@example.com**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: John Doe

2. **doctor1@clinic.local**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: Adam Nowak

3. **doctor2@clinic.local**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: Ewa Kowalski

4. **doctor3@clinic.local**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: Piotr Wiśniewski

5. **doctor4@clinic.local**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: Maria Zieliński

6. **doctor5@clinic.local**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: Tomasz Wójcik

7. **doctor6@clinic.local**
   - Hasło: `password123`
   - Rola: Doctor
   - Imię i nazwisko: Katarzyna Kamińska

### Pacjenci

1. **jane.smith@example.com**
   - Hasło: `password123`
   - Rola: Patient

2. **anna.kowalska@example.com**
   - Hasło: `password123`
   - Rola: Patient

3. **marek.nowak@example.com**
   - Hasło: `password123`
   - Rola: Patient

4. **patient1@clinic.local do patient40@clinic.local**
   - Hasło: `password123`
   - Rola: Patient

5. **michalik@mail.com**
   - Rola: Patient
   - Uwaga: konto istnieje w bazie jako dodatkowy pacjent. Jeżeli hasło było zmieniane ręcznie w aplikacji, należy użyć aktualnego hasła ustawionego przez użytkownika.

## 6. Najważniejsze funkcjonalności

### WebAPI

- logowanie użytkowników
- generowanie i walidacja tokenów JWT
- CRUD dla użytkowników, lekarzy, pacjentów, wizyt, dokumentacji medycznej, recept, leków i zwolnień lekarskich
- zmiana danych profilu
- dodawanie i usuwanie zdjęcia profilowego
- zmiana hasła
- wykrywanie anomalii medycznych

### BlazorServer

- logowanie lekarza i administratora
- panel Home ze statystykami lekarza
- lista i szczegóły pacjentów
- lista i szczegóły wizyt
- tworzenie wizyt dla zalogowanego lekarza
- edycja terminu wizyty
- anulowanie wizyty, także po terminie jako informacja, że pacjent nie przyszedł
- zarządzanie profilem lekarza
- dodawanie i usuwanie zdjęcia profilowego
- zmiana hasła
- panel administratora z wykresami i wykrywaniem anomalii

### Blazor WebAssembly

- logowanie pacjenta
- rejestracja konta pacjenta
- panel pacjenta
- lista i szczegóły wizyt pacjenta
- zmiana terminu wizyty
- anulowanie wizyty
- lista lekarzy oraz szczegóły lekarza
- recepty pacjenta
- edycja profilu pacjenta
- dodawanie i usuwanie zdjęcia profilowego
- zmiana hasła

## 7. Architektura rozwiązania

Projekt jest podzielony zgodnie z założeniami czystej architektury:

- **Domain** - Modele domenowe i interfejsy kontraktów.
- **Application** - Usługi aplikacyjne, walidatory, mapowania i logika biznesowa.
- **Infrastructure** - Dostęp do danych, SQLite, Entity Framework Core, repozytoria, Unit of Work oraz DataSeeder.
- **WebAPI** - Kontrolery REST API, autoryzacja JWT, konfiguracja logowania.
- **BlazorServer** - Interfejs lekarza i administratora.
- **BlazorClient** - Interfejs pacjenta w Blazor WebAssembly.
- **SharedKernel** - DTO, enumy i elementy wspólne.

## 8. Struktura bazy danych

### Główne tabele

- Users
- Doctors
- Patients
- Visits
- MedicalRecords
- Medications
- Prescriptions
- PrescriptionItems
- SickLeaves

### Tabela Users

Zawiera m.in.: Email, Password (hash PBKDF2), Role, PhotoDataUrl (zdjęcie profilowe zapisane jako data URL)

Aktualna baza ma 53 konta użytkowników.

## 9. Endpointy WebAPI

Ponieważ kontrolery używają trasy `[Route("api/[controller]")]`, adresy są tworzone od nazw kontrolerów w liczbie pojedynczej.

### AUTH

- `POST /api/Auth/login`

### USER

- `GET /api/User`
- `GET /api/User/{id}`
- `GET /api/User/email/{email}`
- `GET /api/User/phone/{phoneNumber}`
- `GET /api/User/role/{role}`
- `POST /api/User`
- `PUT /api/User/{id}`
- `PUT /api/User/{id}/profile`
- `PUT /api/User/{id}/photo`
- `PUT /api/User/{id}/password`
- `DELETE /api/User/{id}/photo`
- `DELETE /api/User/{id}`

### DOCTOR

- `GET /api/Doctor`
- `GET /api/Doctor/{id}`
- `GET /api/Doctor/lastname/{lastName}`
- `GET /api/Doctor/specialization/{specialization}`
- `POST /api/Doctor`
- `PUT /api/Doctor/{id}`
- `DELETE /api/Doctor/{id}`

### PATIENT

- `GET /api/Patient`
- `GET /api/Patient/{id}`
- `GET /api/Patient/email/{email}`
- `GET /api/Patient/pesel/{pesel}`
- `GET /api/Patient/phone/{phoneNumber}`
- `POST /api/Patient`
- `PUT /api/Patient/{id}`
- `DELETE /api/Patient/{id}`

### VISIT

- `GET /api/Visit`
- `GET /api/Visit/{id}`
- `GET /api/Visit/patient/{patientId}`
- `GET /api/Visit/doctor/{doctorId}`
- `GET /api/Visit/daterange`
- `POST /api/Visit`
- `PUT /api/Visit`
- `POST /api/Visit/{id}/complete`
- `DELETE /api/Visit/{id}`

### MEDICAL RECORD

- `GET /api/MedicalRecord`
- `GET /api/MedicalRecord/{id}`
- `GET /api/MedicalRecord/visit/{visitId}`
- `PUT /api/MedicalRecord`

### PRESCRIPTION

- `GET /api/Prescription`
- `GET /api/Prescription/{id}`
- `GET /api/Prescription/medicalrecord/{medicalRecordId}`
- `GET /api/Prescription/expired`
- `GET /api/Prescription/expiring-soon`
- `POST /api/Prescription`
- `PUT /api/Prescription/{id}`
- `DELETE /api/Prescription/{id}`
- `POST /api/Prescription/{prescriptionId}/items`
- `GET /api/Prescription/{prescriptionId}/items`
- `GET /api/Prescription/items/{itemId}`
- `PUT /api/Prescription/items/{itemId}`
- `DELETE /api/Prescription/items/{itemId}`

### SICK LEAVE

- `GET /api/SickLeave`
- `GET /api/SickLeave/{id}`
- `GET /api/SickLeave/medicalrecord/{medicalRecordId}`
- `GET /api/SickLeave/daterange`
- `POST /api/SickLeave`
- `PUT /api/SickLeave`
- `DELETE /api/SickLeave/{id}`

### MEDICATION

- `GET /api/Medication`
- `GET /api/Medication/{id}`
- `GET /api/Medication/name/{name}`
- `GET /api/Medication/form/{form}`
- `GET /api/Medication/strength/{strengthValue}`
- `POST /api/Medication/active-ingredients`
- `GET /api/Medication/doctor/{doctorId}`
- `GET /api/Medication/patient/{patientId}`
- `GET /api/Medication/prescription/{prescriptionId}`
- `POST /api/Medication/import`
- `GET /api/Medication/visit/{visitId}`

### ANOMALY

- `GET /api/Anomaly`

### Swagger

- https://localhost:7013/swagger

## 10. Logi systemowe

Logi są skonfigurowane w projektach serwerowych:

- `WebAPI/nlog.config`
- `BlazorServer/nlog.config`

Logger tworzy osobne pliki logów dla kolejnych dni oraz zapisuje błędy systemu w osobnych plikach niż zwykłe informacje.

### Typowe lokalizacje logów

- `WebAPI/logs`
- `BlazorServer/logs`

## 11. Rozwiązywanie problemów

### Problem: SQLite Error 11: database disk image is malformed

**Rozwiązanie:**
- plik bazy SQLite jest uszkodzony
- należy przywrócić poprawną wersję `ClinicVisitSystem.db` z repozytorium
- przed podmianą warto zrobić kopię uszkodzonego pliku

### Problem: Unable to configure HTTPS endpoint

**Rozwiązanie:**
- uruchomić: `dotnet dev-certs https --trust`
- następnie ponownie uruchomić Visual Studio lub terminal

### Problem: Exceeded retry count / file is locked by BlazorServer or WebAPI

**Rozwiązanie:**
- zamknąć uruchomione procesy WebAPI, BlazorServer, BlazorClient albo dotnet
- ponownie wykonać build

### Problem: Failed to fetch w Blazor WebAssembly

**Rozwiązanie:**
- najpierw uruchomić WebAPI
- sprawdzić, czy WebAPI działa pod adresem https://localhost:7013
- sprawdzić certyfikat HTTPS
- sprawdzić `BlazorClient/wwwroot/appsettings.json`

### Problem: invalid email or password

**Rozwiązanie:**
- BlazorServer przyjmuje logowanie lekarza lub administratora
- Blazor WebAssembly jest przeznaczony dla pacjenta
- sprawdzić, czy używane konto ma odpowiednią rolę

## 12. Dokumentacja użytkownika

Dokumentacja użytkownika jest przygotowana w osobnych plikach oraz zawiera opis funkcjonalności i zrzuty ekranu.
