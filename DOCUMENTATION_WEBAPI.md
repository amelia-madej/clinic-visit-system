================================================================================
CLINIC VISIT SYSTEM - DOKUMENTACJA API
WebAPI Endpoints Reference
================================================================================

SPIS TREŚCI:
1. Wprowadzenie
2. Autentykacja
3. Dokumentacja Endpoints - Struktura
4. Endpoints - Szczegóły
5. DTOs (Data Transfer Objects)
6. Kody błędów
7. Walidacja
8. Przykłady żądań
9. Instrukcje testowania
10. Best Practices

================================================================================
1. WPROWADZENIE
================================================================================

WebAPI to backend REST API napisany w ASP.NET Core. Udostępnia wszystkie
operacje na danych dla aplikacji BlazorServer i BlazorClient.

DOSTĘP DO API:
- URL: https://localhost:7186
- Swagger UI: https://localhost:7186/swagger
- ReDoc: https://localhost:7186/redoc

PROTOKÓŁ: REST (HTTP)
FORMAT DANYCH: JSON
AUTENTYKACJA: JWT (JSON Web Token)

STRUKTURA URL:
https://localhost:7186/api/{resource}/{action}

Przykład:
GET https://localhost:7186/api/patients
GET https://localhost:7186/api/patients/1
POST https://localhost:7186/api/patients

================================================================================
2. AUTENTYKACJA
================================================================================

LOGOWANIE:

Endpoint: POST /api/auth/login

Żądanie:
```json
{
  "email": "john.doe@example.com",
  "password": "password123"
}
```

Odpowiedź (200 OK):
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "userId": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "role": "Doctor"
  }
}
```

UŻYCIE TOKENU:

Dla każdego autoryzowanego żądania, dodaj nagłówek:
```
Authorization: Bearer {token}
```

Przykład:
```
GET /api/patients
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

WYLOGOWANIE:

Endpoint: POST /api/auth/logout

Żądanie:
```
Authorization: Bearer {token}
```

Odpowiedź (200 OK):
```json
{
  "message": "Logged out successfully"
}
```

WAŻNE:
- Token wygasa po określonym czasie (zwykle 24h)
- Zarejestruj nowy token, jeśli wygaśnie
- Nigdy nie wysyłaj tokenu w URL
- Zawsze używaj HTTPS (nie HTTP)

================================================================================
3. DOKUMENTACJA ENDPOINTS - STRUKTURA
================================================================================

Każdy endpoint jest udokumentowany w następującym formacie:

METHOD /api/{resource}
Opis: ...
Autentykacja: Required/Optional/None
Role: Admin/Doctor/Patient
Status codes: 200/201/400/401/403/404/500

Parametry (Query/Path/Body):
- Parametr: Typ | Wymagane/Opcjonalne | Opis

Odpowiedź (200):
```json
{ ... }
```

Błędy:
- 400: Walidacja
- 401: Brak autentykacji
- 403: Brak uprawnień
- 404: Nie znaleziono

Przykład żądania:
```
GET /api/patients
Authorization: Bearer {token}
```

Przykład odpowiedzi:
```json
[ ... ]
```

================================================================================
4. ENDPOINTS - SZCZEGÓŁY
================================================================================

==================
AUTHENTICATION (Auth)
==================

POST /api/auth/login
Opis: Logowanie użytkownika i otrzymanie JWT tokenu
Autentykacja: None
Status codes: 200/400/401

Body:
```json
{
  "email": "string",
  "password": "string"
}
```

Response 200:
```json
{
  "token": "string",
  "user": {
    "userId": "integer",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "role": "Admin|Doctor|Patient"
  }
}
```

---

POST /api/auth/logout
Opis: Wylogowanie użytkownika
Autentykacja: Required
Status codes: 200/401

Response 200:
```json
{
  "message": "Logged out successfully"
}
```

==================
USERS (Użytkownicy)
==================

GET /api/users
Opis: Pobierz listę wszystkich użytkowników
Autentykacja: Required
Role: Admin, Doctor
Status codes: 200/401/403

Response 200:
```json
[
  {
    "userId": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "phoneNumber": "123456789",
    "role": "Doctor",
    "photoDataUrl": "data:image/png;base64,..."
  }
]
```

---

GET /api/users/{id}
Opis: Pobierz szczegóły konkretnego użytkownika
Autentykacja: Required
Status codes: 200/404/401

Path parameters:
- id: integer (required) - ID użytkownika

Response 200:
```json
{
  "userId": 1,
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "123456789",
  "role": "Doctor",
  "photoDataUrl": "data:image/png;base64,..."
}
```

---

POST /api/users
Opis: Utwórz nowego użytkownika
Autentykacja: Required
Role: Admin
Status codes: 201/400/401/403

Body:
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phoneNumber": "string",
  "password": "string",
  "role": "Admin|Doctor|Patient"
}
```

Response 201:
```json
{
  "userId": 1,
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phoneNumber": "string",
  "role": "Doctor"
}
```

---

PUT /api/users/{id}
Opis: Aktualizuj użytkownika
Autentykacja: Required
Status codes: 200/400/404/401

Path parameters:
- id: integer (required) - ID użytkownika

Body:
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phoneNumber": "string",
  "role": "Admin|Doctor|Patient"
}
```

Response 200:
```json
{
  "userId": 1,
  "firstName": "string",
  ...
}
```

---

DELETE /api/users/{id}
Opis: Usuń użytkownika
Autentykacja: Required
Role: Admin
Status codes: 204/404/401/403

Path parameters:
- id: integer (required) - ID użytkownika

Response 204: (brak treści)

==================
DOCTORS (Lekarze)
==================

GET /api/doctors
Opis: Pobierz listę lekarzy
Autentykacja: Optional
Status codes: 200

Response 200:
```json
[
  {
    "doctorId": 1,
    "firstName": "John",
    "lastName": "Doe",
    "specialization": "Cardiology",
    "licenseNumber": "LIC123",
    "gender": "Male"
  }
]
```

---

GET /api/doctors/{id}
Opis: Pobierz szczegóły lekarza
Autentykacja: Optional
Status codes: 200/404

Path parameters:
- id: integer (required) - ID lekarza

Response 200:
```json
{
  "doctorId": 1,
  "firstName": "John",
  "lastName": "Doe",
  "specialization": "Cardiology",
  "licenseNumber": "LIC123",
  "gender": "Male"
}
```

---

POST /api/doctors
Opis: Utwórz nowego lekarza
Autentykacja: Required
Role: Admin
Status codes: 201/400/401/403

Body:
```json
{
  "userId": 1,
  "specialization": "string",
  "licenseNumber": "string",
  "gender": "Male|Female|Other"
}
```

Response 201:
```json
{
  "doctorId": 1,
  "firstName": "John",
  "lastName": "Doe",
  "specialization": "Cardiology"
}
```

---

PUT /api/doctors/{id}
Opis: Aktualizuj lekarza
Autentykacja: Required
Role: Admin
Status codes: 200/400/404/401/403

Path parameters:
- id: integer (required) - ID lekarza

Body:
```json
{
  "specialization": "string",
  "licenseNumber": "string",
  "gender": "Male|Female|Other"
}
```

Response 200:
```json
{
  "doctorId": 1,
  ...
}
```

==================
PATIENTS (Pacjenci)
==================

GET /api/patients
Opis: Pobierz listę pacjentów
Autentykacja: Required
Role: Doctor, Admin
Status codes: 200/401/403

Response 200:
```json
[
  {
    "patientId": 1,
    "firstName": "Jane",
    "lastName": "Smith",
    "email": "jane.smith@example.com",
    "age": 34,
    "pesel": "12345678901",
    "dateOfBirth": "1990-01-01",
    "gender": "Female",
    "address": "123 Main St"
  }
]
```

---

GET /api/patients/{id}
Opis: Pobierz szczegóły pacjenta
Autentykacja: Required
Role: Doctor, Patient, Admin
Status codes: 200/404/401/403

Path parameters:
- id: integer (required) - ID pacjenta

Response 200:
```json
{
  "patientId": 1,
  "firstName": "Jane",
  "lastName": "Smith",
  "email": "jane.smith@example.com",
  "age": 34,
  "pesel": "12345678901",
  "dateOfBirth": "1990-01-01",
  "gender": "Female",
  "address": "123 Main St"
}
```

---

POST /api/patients
Opis: Utwórz nowego pacjenta
Autentykacja: Required
Role: Admin, Doctor
Status codes: 201/400/401/403

Body:
```json
{
  "userId": 1,
  "pesel": "string",
  "dateOfBirth": "2000-01-01",
  "gender": "Male|Female|Other",
  "address": "string"
}
```

Response 201:
```json
{
  "patientId": 1,
  "firstName": "Jane",
  "lastName": "Smith",
  ...
}
```

---

PUT /api/patients/{id}
Opis: Aktualizuj pacjenta
Autentykacja: Required
Role: Doctor, Patient, Admin
Status codes: 200/400/404/401/403

Path parameters:
- id: integer (required) - ID pacjenta

Body:
```json
{
  "pesel": "string",
  "dateOfBirth": "2000-01-01",
  "gender": "Male|Female|Other",
  "address": "string"
}
```

Response 200:
```json
{
  "patientId": 1,
  ...
}
```

==================
VISITS (Wizyty)
==================

GET /api/visits
Opis: Pobierz listę wizyt
Autentykacja: Required
Status codes: 200/401

Query parameters (opcjonalne):
- status: Scheduled|Completed|Cancelled
- patientId: integer
- doctorId: integer
- fromDate: datetime
- toDate: datetime

Response 200:
```json
[
  {
    "visitId": 1,
    "visitDateTime": "2026-05-26T09:00:00",
    "patient": {
      "patientId": 1,
      "firstName": "Jane",
      "lastName": "Smith"
    },
    "doctor": {
      "doctorId": 1,
      "firstName": "John",
      "lastName": "Doe"
    },
    "visitType": "InPerson",
    "status": "Scheduled"
  }
]
```

---

GET /api/visits/{id}
Opis: Pobierz szczegóły wizyty
Autentykacja: Required
Status codes: 200/404/401

Path parameters:
- id: integer (required) - ID wizyty

Response 200:
```json
{
  "visitId": 1,
  "visitDateTime": "2026-05-26T09:00:00",
  "patient": {...},
  "doctor": {...},
  "visitType": "InPerson",
  "status": "Scheduled",
  "medicalRecord": {...},
  "prescriptions": [...]
}
```

---

POST /api/visits
Opis: Utwórz nową wizytę
Autentykacja: Required
Role: Doctor, Admin
Status codes: 201/400/401/403

Body:
```json
{
  "patientId": 1,
  "doctorId": 1,
  "visitDateTime": "2026-05-26T09:00:00",
  "visitType": "InPerson|Telemedicine|HomeVisit"
}
```

Response 201:
```json
{
  "visitId": 1,
  "visitDateTime": "2026-05-26T09:00:00",
  ...
}
```

---

PUT /api/visits/{id}
Opis: Aktualizuj wizytę
Autentykacja: Required
Role: Doctor, Admin
Status codes: 200/400/404/401/403

Path parameters:
- id: integer (required) - ID wizyty

Body:
```json
{
  "visitDateTime": "2026-05-26T09:00:00",
  "visitType": "InPerson|Telemedicine|HomeVisit",
  "status": "Scheduled|Completed|Cancelled"
}
```

Response 200:
```json
{
  "visitId": 1,
  ...
}
```

---

DELETE /api/visits/{id}
Opis: Usuń/anuluj wizytę
Autentykacja: Required
Role: Doctor, Admin
Status codes: 204/404/401/403

Path parameters:
- id: integer (required) - ID wizyty

Response 204: (brak treści)

==================
MEDICAL RECORDS (Historia medyczna)
==================

GET /api/medicalrecords
Opis: Pobierz listę historii
Autentykacja: Required
Status codes: 200/401

Query parameters (opcjonalne):
- visitId: integer
- patientId: integer

Response 200:
```json
[
  {
    "medicalRecordId": 1,
    "visitId": 1,
    "interview": "Patient reports...",
    "diagnosis": "Stable angina pectoris",
    "recommendations": "Continue current beta-blocker therapy...",
    "createdAt": "2026-05-26T09:00:00"
  }
]
```

---

POST /api/medicalrecords
Opis: Utwórz nową historię medyczną
Autentykacja: Required
Role: Doctor, Admin
Status codes: 201/400/401/403

Body:
```json
{
  "visitId": 1,
  "interview": "string",
  "diagnosis": "string",
  "recommendations": "string"
}
```

Response 201:
```json
{
  "medicalRecordId": 1,
  "visitId": 1,
  ...
}
```

---

PUT /api/medicalrecords/{id}
Opis: Aktualizuj historię
Autentykacja: Required
Role: Doctor, Admin
Status codes: 200/400/404/401/403

Path parameters:
- id: integer (required) - ID historii

Body:
```json
{
  "interview": "string",
  "diagnosis": "string",
  "recommendations": "string"
}
```

Response 200:
```json
{
  "medicalRecordId": 1,
  ...
}
```

==================
PRESCRIPTIONS (Recepty)
==================

GET /api/prescriptions
Opis: Pobierz listę recept
Autentykacja: Required
Status codes: 200/401

Response 200:
```json
[
  {
    "prescriptionId": 1,
    "patientId": 1,
    "prescriptionDate": "2026-05-26",
    "items": [
      {
        "prescriptionItemId": 1,
        "medicationId": 1,
        "medicationName": "Aspirin",
        "dose": "500mg",
        "quantity": 30,
        "instructions": "1-2 tablets 3x daily"
      }
    ]
  }
]
```

---

POST /api/prescriptions
Opis: Wystawie nową receptę
Autentykacja: Required
Role: Doctor, Admin
Status codes: 201/400/401/403

Body:
```json
{
  "patientId": 1,
  "items": [
    {
      "medicationId": 1,
      "dose": "500mg",
      "quantity": 30,
      "instructions": "string"
    }
  ]
}
```

Response 201:
```json
{
  "prescriptionId": 1,
  "patientId": 1,
  ...
}
```

==================
MEDICATIONS (Leki)
==================

GET /api/medications
Opis: Pobierz katalog leków
Autentykacja: Optional
Status codes: 200

Query parameters (opcjonalne):
- search: string

Response 200:
```json
[
  {
    "medicationId": 1,
    "name": "Aspirin",
    "dose": "500mg",
    "packaging": "30 tablets",
    "available": true
  }
]
```

==================
SICK LEAVES (Zaświadczenia)
==================

GET /api/sickleaves
Opis: Pobierz listę zaświadczeń
Autentykacja: Required
Status codes: 200/401

Response 200:
```json
[
  {
    "sickLeaveId": 1,
    "medicalRecordId": 1,
    "startDate": "2026-05-26",
    "endDate": "2026-05-31",
    "reason": "Acute lumbar strain",
    "createdAt": "2026-05-26T09:00:00"
  }
]
```

---

POST /api/sickleaves
Opis: Utwórz nowe zaświadczenie
Autentykacja: Required
Role: Doctor, Admin
Status codes: 201/400/401/403

Body:
```json
{
  "medicalRecordId": 1,
  "startDate": "2026-05-26",
  "endDate": "2026-05-31",
  "reason": "string"
}
```

Response 201:
```json
{
  "sickLeaveId": 1,
  "medicalRecordId": 1,
  ...
}
```

================================================================================
5. DTOs (Data Transfer Objects)
================================================================================

DTO to obiekty przekazywane między API i klientem. Wszystkie DTOs znajdują
się w projekcie SharedKernel/DTOs/

GŁÓWNE DTOs:

UserDto:
- userId: int
- firstName: string
- lastName: string
- email: string
- phoneNumber: string
- role: UserRole (enum)
- photoDataUrl: string (optional)

PatientDto:
- patientId: int
- firstName: string
- lastName: string
- email: string
- phoneNumber: string
- pesel: string
- age: int
- dateOfBirth: DateTime
- gender: Gender (enum)
- address: string

DoctorDto:
- doctorId: int
- firstName: string
- lastName: string
- specialization: string
- licenseNumber: string
- gender: Gender (enum)

VisitDto:
- visitId: int
- visitDateTime: DateTime
- patientId: int
- patient: PatientListItemDto
- doctorId: int
- doctor: DoctorListItemDto
- visitType: VisitType (enum)
- status: VisitStatus (enum)

MedicalRecordDto:
- medicalRecordId: int
- visitId: int
- interview: string
- diagnosis: string
- recommendations: string
- createdAt: DateTime

PrescriptionDto:
- prescriptionId: int
- patientId: int
- prescriptionDate: DateTime
- items: PrescriptionItemDto[]

SickLeaveDto:
- sickLeaveId: int
- medicalRecordId: int
- startDate: DateTime
- endDate: DateTime
- reason: string
- createdAt: DateTime

================================================================================
6. KODY BŁĘDÓW
================================================================================

200 OK
- Żądanie wykonane pomyślnie
- Odpowiedź zawiera dane

201 Created
- Zasób został utworzony
- Odpowiedź zawiera nowy zasób

204 No Content
- Żądanie wykonane pomyślnie
- Brak zawartości (np. DELETE)

400 Bad Request
- Błąd walidacji
- Brakujące lub nieprawidłowe pola
- Odpowiedź zawiera szczegóły błędu

401 Unauthorized
- Brak autentykacji
- Token wygaśnął lub jest nieprawidłowy
- Dodaj nagłówek Authorization

403 Forbidden
- Brak uprawnień
- Użytkownik nie ma roli do wykonania tej akcji
- Skontaktuj się z administratorem

404 Not Found
- Zasób nie znaleziony
- Sprawdź ID zasobu

500 Internal Server Error
- Błąd serwera
- Skontaktuj się z administratorem
- Sprawdź logi (WebAPI/logs/)

FORMAT ODPOWIEDZI BŁĘDU:

```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Validation failed",
    "details": [
      {
        "field": "email",
        "message": "Email is invalid"
      }
    ]
  }
}
```

================================================================================
7. WALIDACJA
================================================================================

Wszystkie dane są walidowane po stronie serwera (API).

REGUŁY WALIDACJI:

Użytkownik (User):
- FirstName:Required, max 50 znaków
- LastName: Required, max 50 znaków
- Email: Required, valid email format, unique
- Password: Required, min 8 znaków
- PhoneNumber: Optional, valid phone format

Pacjent (Patient):
- PESEL: Required, 11 cyfr
- DateOfBirth: Required, past date
- Gender: Required
- Address: Required, max 200 znaków

Wizyta (Visit):
- PatientId: Required
- DoctorId: Required
- VisitDateTime: Required, future date
- VisitType: Required

Historia medyczna (MedicalRecord):
- VisitId: Required, visit must exist
- Interview: Required, max 2000 znaków
- Diagnosis: Required, max 500 znaków
- Recommendations: Required, max 2000 znaków

Recepta (Prescription):
- PatientId: Required
- Items: Required, min 1 item
- Dla każdego leku:
  * MedicationId: Required
  * Dose: Required
  * Quantity: Required, min 1

================================================================================
8. PRZYKŁADY ŻĄDAŃ
================================================================================

LOGOWANIE:

```bash
curl -X POST https://localhost:7186/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "john.doe@example.com",
    "password": "password123"
  }'
```

POBIERANIE LISTY PACJENTÓW:

```bash
curl -X GET https://localhost:7186/api/patients \
  -H "Authorization: Bearer YOUR_TOKEN"
```

TWORZENIE NOWEJ WIZYTY:

```bash
curl -X POST https://localhost:7186/api/visits \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "patientId": 1,
    "doctorId": 1,
    "visitDateTime": "2026-05-26T09:00:00",
    "visitType": "InPerson"
  }'
```

WYSTAWIANIE RECEPTY:

```bash
curl -X POST https://localhost:7186/api/prescriptions \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "patientId": 1,
    "items": [
      {
        "medicationId": 1,
        "dose": "500mg",
        "quantity": 30,
        "instructions": "1-2 tablets 3x daily"
      }
    ]
  }'
```

================================================================================
9. INSTRUKCJE TESTOWANIA
================================================================================

TESTOWANIE ZA POMOCĄ SWAGGER UI:

1. Otwórz https://localhost:7186/swagger
2. Zaloguj się:
   - Kliknij "Authorize"
   - Wpisz token z logowania
   - Kliknij "Authorize"
3. Przetestuj endpoint:
   - Kliknij endpoint
   - Wpisz parametry
   - Kliknij "Try it out"
   - Sprawdź odpowiedź

TESTOWANIE ZA POMOCĄ POSTMAN:

1. Pobierz i otwórz Postman
2. Stwórz nowe żądanie
3. Zaloguj się (POST /api/auth/login)
4. Skopiuj token z odpowiedzi
5. Dodaj nagłówek: Authorization: Bearer {token}
6. Testuj pozostałe endpoints

TESTOWANIE ZA POMOCĄ cURL:

```bash
# Logowanie
TOKEN=$(curl -X POST https://localhost:7186/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"john.doe@example.com","password":"password123"}' \
  | jq '.token')

# Użyj tokenu
curl -X GET https://localhost:7186/api/patients \
  -H "Authorization: Bearer $TOKEN"
```

================================================================================
10. BEST PRACTICES
================================================================================

1. AUTENTYKACJA:
   - Zawsze loguj się pierwszym żądaniem
   - Przechowuj token bezpiecznie
   - Odśwież token przed wygaśnięciem
   - Wyloguj się po zakończeniu

2. BEZPIECZEŃSTWO:
   - Zawsze używaj HTTPS (nigdy HTTP)
   - Nie wysyłaj hasła w parametrach URL
   - Nie loguj wrażliwych danych
   - Weryfikuj certyfikat SSL

3. WYDAJNOŚĆ:
   - Filtruj dane po stronie serwera (query parameters)
   - Stawiaj limity na liczbę wyników
   - Cachuj odpowiedzi tam, gdzie to możliwe
   - Używaj pagination dla dużych zbiorów danych

4. OBSŁUGA BŁĘDÓW:
   - Sprawdzaj kod odpowiedzi HTTP
   - Loguj błędy ze szczegółami
   - Rób retry dla błędów 5xx (z backoff)
   - Nie rób retry dla błędów 4xx (przyczyna w żądaniu)

5. TESTOWANIE:
   - Testuj wszystkie scenariusze (happy path + error cases)
   - Testuj walidację
   - Testuj autoryzację (role-based)
   - Testuj performance pod obciążeniem

6. DOKUMENTACJA:
   - Dokumentuj custom fields
   - Udostępniaj Swagger UI wszystkim
   - Utrzymuj dokumentację aktualną
   - Podawaj przykłady żądań i odpowiedzi

================================================================================
Ostatnia aktualizacja: 2026-05-23
Wersja API: 1.0
================================================================================
