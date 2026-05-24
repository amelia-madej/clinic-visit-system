================================================================================
WSKAZANIA DO ZRZUTÓW EKRANU - CLINIC VISIT SYSTEM
Screenshots Placement Guide
================================================================================

Ten plik zawiera informacje o tym, gdzie powinny być umieszczone zrzuty
ekranu (screenshoty) dla każdej dokumentacji.

================================================================================
I. DOKUMENTACJA DLA LEKARZY (BlazorServer)
DOCUMENTATION_USER_BLAZOR_SERVER.md
================================================================================

SEKCJA 2: LOGOWANIE DO SYSTEMU
───────────────────────────────

[ZRZUT EKRANU 1] - Ekran logowania
Gdzie: Część górna strony https://localhost:7001
Co powinno być widać:
- Formularz logowania z polami:
  * Email (tekst)
  * Hasło (pola do wpisania)
- Przycisk "Zaloguj się"
- Logo aplikacji / nazwa "Clinic Visit System"
- Opcjonalnie: link "Zapomnisz hasła?"
Tekst: "Ekran logowania - formularz z polami Email i Hasło"

───────────────────────────────
SEKCJA 3: NAWIGACJA I UKŁAD INTERFEJSU
───────────────────────────────

[ZRZUT EKRANU 2] - Główny layout aplikacji
Gdzie: Po zalogowaniu na stronie głównej (Home)
Co powinno być widać:
- Menu boczne (sidebar) po lewej stronie z opcjami:
  * Home
  * Patients
  * Visits
  * Medications
  * Profile
- Zawartość główna (content area) po prawej stronie
- Pasek górny z imieniem użytkownika i przyciskiem wylogowania
Tekst: "Główny layout aplikacji z menu bocznym"

───────────────────────────────
SEKCJA 4: ZARZĄDZANIE PACJENTAMI
───────────────────────────────

[ZRZUT EKRANU 3] - Lista pacjentów
Gdzie: Menu → Patients
Co powinno być widać:
- Nagłówek "Patients"
- Przycisk "+ New Patient" w górnym rogu
- Tabela z pacjentami zawierająca kolumny:
  * Last Name (Nazwisko)
  * First Name (Imię)
  * Age (Wiek)
  * Last Visit (Data ostatniej wizyty)
  * Ikona oka (view details)
- Co najmniej 3 pacjentów (Jane Smith, Anna Kowalska, Marek Nowak)
- Pola wyszukiwania/filtrowania
Tekst: "Lista pacjentów - tabela z ikonami akcji"

[ZRZUT EKRANU 4] - Szczegóły pacjenta
Gdzie: Menu → Patients → kliknij ikonę oka obok pacjenta
Co powinno być widać:
- Dane pacjenta:
  * Imię i nazwisko
  * Email
  * Telefon
  * PESEL
  * Data urodzenia
  * Adres
- Historia wizyt (tabela)
- Ostatnie recepty
- Przycisk edycji
Tekst: "Szczegóły pacjenta - formularz z danymi"

[ZRZUT EKRANU 5] - Dialog nowy pacjent
Gdzie: Menu → Patients → "+ New Patient"
Co powinno być widać:
- Modal/dialog box z tytułem "New Patient"
- Formularz z polami:
  * First Name
  * Last Name
  * Email
  * Phone Number
  * PESEL
  * Date of Birth
  * Gender (dropdown)
  * Address
- Przyciski "Cancel" i "Save"
Tekst: "Nowy pacjent - dialog do dodawania pacjenta"

───────────────────────────────
SEKCJA 5: HARMONOGRAM WIZYT
───────────────────────────────

[ZRZUT EKRANU 6] - Lista wizyt
Gdzie: Menu → Visits
Co powinno być widać:
- Nagłówek "Visits"
- Przycisk "+ New Visit" w górnym rogu
- Tabela z wizytami zawierająca kolumny:
  * Date & Time (kolorowy tekst daty)
  * Patient (Imię i nazwisko pacjenta)
  * Doctor (Imię i nazwisko lekarza)
  * Type (typ wizyty)
  * Status (kolorowy chip: niebieski/zielony/czerwony)
  * Ikony akcji (edycja, usunięcie)
- Co najmniej 3-4 wizyty z różnymi statusami
Tekst: "Lista wizyt - tabela z kolorowymi statusami"

[ZRZUT EKRANU 7] - Szczegóły wizyty
Gdzie: Menu → Visits → kliknij na wiersz wizyty
Co powinno być widać:
- Dane wizyty:
  * Data i godzina
  * Pacjent
  * Lekarz
  * Typ wizyty
  * Status
- Zakładki: Medical Record, Prescriptions, Sick Leave
- Historia medyczna (jeśli jest)
Tekst: "Szczegóły wizyty - pełne dane wizyty z histoią medyczną"

[ZRZUT EKRANU 8] - Dialog nowa wizyta
Gdzie: Menu → Visits → "+ New Visit"
Co powinno być widać:
- Modal/dialog box z tytułem "New Visit"
- Formularz z polami:
  * Patient (dropdown)
  * Doctor (dropdown)
  * Date & Time (date picker)
  * Visit Type (radio/dropdown)
- Przyciski "Cancel" i "Save"
Tekst: "Nowa wizyta - dialog do rezerwacji wizyty"

[ZRZUT EKRANU 9] - Pulpit (Home)
Gdzie: Menu → Home
Co powinno być widać:
- Nagłówek "Home"
- Karty (cards) ze statystykami:
  * All visits (liczba)
  * Today (liczba wizyt dzisiaj)
  * Scheduled (liczba zaplanowanych)
  * Completed (liczba zakończonych)
- Przycisk "Manage visits"
Tekst: "Pulpit (Home) - karty ze statystykami wizyt"

───────────────────────────────
SEKCJA 6: HISTORIA MEDYCZNA
───────────────────────────────

[ZRZUT EKRANU 10] - Formularz historii medycznej
Gdzie: Menu → Visits → kliknij wizytę → "Add Medical Record"
Co powinno być widać:
- Formularz z trzema głównymi polami tekstowymi:
  1. Interview (duże pole tekstowe)
  2. Diagnosis (średnie pole tekstowe)
  3. Recommendations (duże pole tekstowe)
- Pole informacji o wizycie
- Przyciski "Cancel" i "Save"
Tekst: "Formularz historii medycznej - pola Interview, Diagnosis, Recommendations"

[ZRZUT EKRANU 11] - Historia medyczna (widok)
Gdzie: Menu → Visits → kliknij wizytę → Medical History (po zapisaniu)
Co powinno być widać:
- Sekcje:
  * Interview (tekst)
  * Diagnosis (tekst)
  * Recommendations (tekst)
- Informacje o dacie utworzenia
- Przycisk edycji
- Przycisk "Add Prescription" lub "Add Sick Leave"
Tekst: "Historia medyczna - widok zapisanego rekordu"

───────────────────────────────
SEKCJA 7: RECEPTY I LEKI
───────────────────────────────

[ZRZUT EKRANU 12] - Katalog leków
Gdzie: Menu → Medications
Co powinno być widać:
- Nagłówek "Medications"
- Tabela leków z kolumnami:
  * Name (nazwa leku)
  * Dose (dawka)
  * Packaging (opakowanie)
  * Available (dostępność)
- Pole wyszukiwania
Tekst: "Katalog leków - tabela z lekami"

[ZRZUT EKRANU 13] - Nowa recepta
Gdzie: Menu → Visits → szczegóły wizyty → "Add Prescription"
Co powinno być widać:
- Nagłówek "New Prescription"
- Sekcje:
  * Patient (auto-filled)
  * Prescription Date (auto-filled)
  * Prescription Items (tabela z +Add Item button)
  * General Instructions (textarea)
- Dla każdej pozycji: Medication, Dose, Quantity, Instructions
- Przyciski "Cancel" i "Save"
Tekst: "Nowa recepta - formularz z pozycjami"

[ZRZUT EKRANU 14] - Recepta (gotowy dokument)
Gdzie: Po zapisaniu recepty → przycisk "View/Download PDF"
Co powinno być widać:
- Dokument recepty:
  * Nagłówek "PRESCRIPTION"
  * Data
  * Dane pacjenta
  * Dane lekarza
  * Tabela leków z dawkami i instrukcjami
  * Podpis lekarza (elektroniczny)
  * Numer recepty
Tekst: "Recepta - gotowy dokument do wydruku"

───────────────────────────────
SEKCJA 8: ZAŚWIADCZENIA LEKARSKIE
───────────────────────────────

[ZRZUT EKRANU 15] - Nowe zaświadczenie
Gdzie: Menu → Visits → szczegóły wizyty → "Add Sick Leave"
Co powinno być widać:
- Modal/formularz z tytułem "New Sick Leave"
- Pola:
  * Patient (auto-filled)
  * Medical Record (powiązana historia)
  * Start Date (date picker)
  * End Date (date picker)
  * Reason (textarea)
- Przyciski "Cancel" i "Save"
Tekst: "Nowe zaświadczenie - formularz z datami i przyczyn"

[ZRZUT EKRANU 16] - Zaświadczenie (gotowy dokument)
Gdzie: Po zapisaniu zaświadczenia → przycisk "View/Download PDF"
Co powinno być widać:
- Dokument zaświadczenia:
  * Nagłówek "SICK LEAVE CERTIFICATE"
  * Data rozpoczęcia i zakończenia
  * Dane pacjenta
  * Dane lekarza
  * Przyczyna zwolnienia
  * Liczba dni zwolnienia
  * Pieczęć przychodni (elektroniczna)
  * Podpis lekarza
Tekst: "Zaświadczenie - dokument do wydruku z pieczęcią lekarza"

───────────────────────────────
SEKCJA 9: PROFIL UŻYTKOWNIKA
───────────────────────────────

[ZRZUT EKRANU 17] - Profil użytkownika
Gdzie: Menu → Profile
Co powinno być widać:
- Avatar/zdjęcie profilowe
- Dane lekarza:
  * Imię i nazwisko
  * Email
  * Telefon
  * Specjalizacja
  * Numer licencji
  * Płeć
- Przycisk "Edit Profile"
- Przycisk "Change Password"
- Przycisk "Logout"
Tekst: "Profil użytkownika - dane lekarza"

[ZRZUT EKRANU 18] - Edycja profilu
Gdzie: Menu → Profile → "Edit Profile"
Co powinno być widać:
- Formularz do edycji danych
- Pola: First Name, Last Name, Email, Phone, Specialization, License Number
- Przycisk wyboru zdjęcia (Upload Photo)
- Przyciski "Cancel" i "Save Changes"
Tekst: "Edycja profilu - formularz do zmian"

================================================================================
II. DOKUMENTACJA DLA PACJENTÓW (BlazorClient)
DOCUMENTATION_USER_BLAZOR_CLIENT.md
================================================================================

SEKCJA 2: LOGOWANIE DO SYSTEMU
───────────────────────────────

[ZRZUT EKRANU 1] - Ekran logowania (pacjent)
Gdzie: https://localhost:7002
Co powinno być widać:
- Prosty formularz logowania
- Pola: Email, Password
- Przycisk "Zaloguj się"
- Logo aplikacji
Tekst: "Ekran logowania - prosty formularz ze polami Email i Hasło"

───────────────────────────────
SEKCJA 3: NAWIGACJA I UKŁAD INTERFEJSU
───────────────────────────────

[ZRZUT EKRANU 2] - Główny layout (pacjent)
Gdzie: Po zalogowaniu, strona główna
Co powinno być widać:
- Menu boczne z opcjami:
  * Home
  * Visits
  * Profile
- Główna zawartość
- Pasek górny z imieniem i przyciskiem wylogowania
Tekst: "Główny układ aplikacji - menu boczne i zawartość"

───────────────────────────────
SEKCJA 4: MOJE WIZYTY
───────────────────────────────

[ZRZUT EKRANU 3] - Lista moich wizyt
Gdzie: Menu → Visits
Co powinno być widać:
- Tabela wizyt z kolumnami:
  * Date & Time
  * Doctor
  * Type (kolorowe ikony)
  * Status (kolorowe chips - blue/green/red)
- Pola wyszukiwania/filtrowania
- Co najmniej 3-4 wizyty z różnymi statusami
Tekst: "Lista moich wizyt - tabela z wielokolorowymi statusami"

[ZRZUT EKRANU 4] - Szczegóły wizyty (pacjent)
Gdzie: Menu → Visits → kliknij na wizytę
Co powinno być widać:
- Zakładki: Overview, Medical History, Prescriptions, Sick Leave
- Dane wizyty:
  * Data i godzina
  * Lekarz
  * Typ wizyty
  * Status
- Historia medyczna (jeśli dostępna)
Tekst: "Szczegóły wizyty - pełne informacje z histoią medyczną"

───────────────────────────────
SEKCJA 5: HISTORIA MEDYCZNA
───────────────────────────────

[ZRZUT EKRANU 5] - Historia medyczna
Gdzie: Menu → Visits → kliknij wizytę → Medical History
Co powinno być widać:
- Sekcja "Medical History"
- Trzy główne części:
  * Interview (tekst)
  * Diagnosis (tekst)
  * Recommendations (tekst)
- Data wizyty
- Imię lekarza
- Przycisk "Download as PDF"
Tekst: "Historia medyczna - dokument z trzema sekcjami"

[ZRZUT EKRANU 6] - Pobieranie PDF
Gdzie: Historia medyczna → "Download as PDF"
Co powinno być widać:
- Przycisk Download
- Lub podgląd PDF dokumentu
Tekst: "Pobieranie PDF - przycisk do pobrania dokumentu"

───────────────────────────────
SEKCJA 6: MOJE RECEPTY
───────────────────────────────

[ZRZUT EKRANU 7] - Receptę
Gdzie: Menu → Visits → szczegóły wizyty → Prescriptions
Co powinno być widać:
- Dokument recepty zawierający:
  * Numer recepty
  * Data wystawienia
  * Dane pacjenta
  * Dane lekarza
  * Tabela leków:
    - Nazwa leku
    - Dawka
    - Ilość
    - Instrukcje
  * Podpis lekarza
Tekst: "Receptę - dokument z listą leków i instrukcjami"

[ZRZUT EKRANU 8] - Pobieranie recepty
Gdzie: Prescriptions → Print/Download
Co powinno być widać:
- Przycisk Print lub Download PDF
- Lub podgląd dokumentu
Tekst: "Pobieranie recepty - przycisk Print/Download"

───────────────────────────────
SEKCJA 7: MOJE ZAŚWIADCZENIA LEKARSKIE
───────────────────────────────

[ZRZUT EKRANU 9] - Zaświadczenie lekarskie
Gdzie: Menu → Visits → szczegóły wizyty → Sick Leave
Co powinno być widać:
- Dokument zaświadczenia zawierający:
  * Numer zaświadczenia
  * Daty zwolnienia (od-do)
  * Liczba dni
  * Dane pacjenta (PESEL)
  * Dane lekarza
  * Przyczyna zwolnienia
  * Pieczęć
  * Podpis lekarza
Tekst: "Zaświadczenie lekarskie - dokument z datami i przyczyn"

[ZRZUT EKRANU 10] - Wydruk zaświadczenia
Gdzie: Sick Leave → Print
Co powinno być widać:
- Wersja do wydruku na papierze
- Wszystkie istotne dane widoczne
Tekst: "Wydruk zaświadczenia - wersja do papierowej kopii"

───────────────────────────────
SEKCJA 8: PROFIL I USTAWIENIA
───────────────────────────────

[ZRZUT EKRANU 11] - Profil pacjenta
Gdzie: Menu → Profile
Co powinno być widać:
- Avatar/zdjęcie
- Dane personalne:
  * Imię i nazwisko
  * Email
  * Telefon
  * PESEL
  * Data urodzenia
  * Płeć
  * Adres
- Przyciski: Edit Profile, Change Password, Logout
Tekst: "Profil użytkownika - moje dane personalne"

[ZRZUT EKRANU 12] - Edycja profilu (pacjent)
Gdzie: Menu → Profile → "Edit Profile"
Co powinno być widać:
- Formularz do edycji
- Pola: First Name, Last Name, Email, Phone, Address, Date of Birth, Gender
- Przycisk wyboru zdjęcia
- Przyciski: Cancel, Save Changes
Tekst: "Edycja profilu - formularz do zmian"

[ZRZUT EKRANU 13] - Zmiana hasła (pacjent)
Gdzie: Menu → Profile → "Change Password"
Co powinno być widać:
- Formularz:
  * Current Password
  * New Password
  * Confirm New Password
- Wymagania dla hasła
- Przyciski: Cancel, Update Password
Tekst: "Zmiana hasła - bezpieczna zmiana hasła"

================================================================================
III. DOKUMENTACJA DLA DEWELOPERÓW (WebAPI)
DOCUMENTATION_WEBAPI.md
================================================================================

W dokumentacji API rekomenduje się dodać screenshot'y następujących widoków:

[ZRZUT EKRANU 1] - Swagger UI
Gdzie: https://localhost:7186/swagger
Co powinno być widać:
- Strona Swagger z listą endpoints
- Endpoints pogrupowane po kategoriach (Auth, Users, Doctors, etc.)
- Expandable sekcje z metodami (GET, POST, PUT, DELETE)
- Przycisk "Try it out"
Tekst: "Swagger UI - dokumentacja API z interaktywnym testem"

[ZRZUT EKRANU 2] - Logowanie w Swagger
Gdzie: https://localhost:7186/swagger → Auth → POST /api/auth/login
Co powinno być widać:
- Formularz POST żądania
- Input fields: email, password
- Przycisk "Try it out"
- Przykładowa odpowiedź z tokenem
Tekst: "Logowanie w Swagger - pola do wpisania danych"

[ZRZUT EKRANU 3] - Autentykacja tokenu
Gdzie: https://localhost:7186/swagger → Authorize
Co powinno być widać:
- Dialog "Available authorizations"
- Pole do wpisania tokenu
- Przycisk "Authorize"
Tekst: "Autentykacja tokenu - wpisanie JWT tokenu"

[ZRZUT EKRANU 4] - Endpoint Patients - GET
Gdzie: https://localhost:7186/swagger → Patients → GET /api/patients
Co powinno być widać:
- Opis endpointu
- Request/Response model
- Przycisk "Try it out"
- Przykładowa odpowiedź
Tekst: "Endpoint Patients - przykładowa lista pacjentów"

[ZRZUT EKRANU 5] - Błąd walidacji
Gdzie: https://localhost:7186/swagger → Visits → POST /api/visits (z błędem)
Co powinno być widać:
- Żądanie POST z błędnymi danymi
- Odpowiedź 400 Bad Request
- Szczegóły błędu walidacji
Tekst: "Błąd walidacji - odpowiedź 400 z szczegółami"

[ZRZUT EKRANU 6] - Błąd autoryzacji
Gdzie: https://localhost:7186/swagger → Patients → GET /api/patients (bez tokenu)
Co powinno być widać:
- Żądanie bez Authorization nagłówka
- Odpowiedź 401 Unauthorized
Tekst: "Błąd autoryzacji - odpowiedź 401 bez tokenu"

================================================================================
IV. UWAGI DOTYCZĄCE TWORZENIA SCREENSHOT'ÓW
================================================================================

1. ROZDZIELCZOŚĆ:
   - Zalecana szerokość: 1280px lub 1920px
   - Format: PNG lub JPG (PNG lepszy dla tekstu)
   - DPI: 72-96 DPI

2. ZAWARTOŚĆ:
   - Ukryj dane wrażliwe (hasła, pełne numery)
   - Pokaż realistyczne dane testowe
   - Zaznacz ważne elementy (strzałkami, kółkami)
   - Dodaj tekstowe objaśnienia

3. ORGANIZACJA:
   - Umieść screenshoty w folderze: docs/screenshots/
   - Organizuj po sekcjach: screenshots/blazor-server/, screenshots/blazor-client/, screenshots/webapi/
   - Nazwij pliki: 1_login.png, 2_dashboard.png, itd.

4. ROZMIAR PLIKÓW:
   - Ooptymalizuj obrazy (max 500KB na plik)
   - Używaj PNG z kompresją
   - WebP dla wsparcia nowoczesnych przeglądarek

5. DOSTĘPNOŚĆ:
   - Dodaj alt text dla każdego screenshot'u
   - Opisz co jest widać
   - Użyj kontrastu 4.5:1 dla tekstu

================================================================================
V. STRUKTURA KATALOGÓW - DOCELOWA
================================================================================

clinic-visit-system/
├── README.txt                                    (ogólne info systemowe)
├── DOCUMENTATION_USER_BLAZOR_SERVER.md          (dla lekarzy)
├── DOCUMENTATION_USER_BLAZOR_CLIENT.md          (dla pacjentów)
├── DOCUMENTATION_WEBAPI.md                      (dla deweloperów)
├── SCREENSHOTS_GUIDE.md                         (ten plik)
├── docs/
│   ├── screenshots/
│   │   ├── blazor-server/
│   │   │   ├── 1_login.png
│   │   │   ├── 2_main_layout.png
│   │   │   ├── 3_patients_list.png
│   │   │   ├── 4_patient_details.png
│   │   │   ├── 5_new_patient_dialog.png
│   │   │   ├── 6_visits_list.png
│   │   │   ├── 7_visit_details.png
│   │   │   ├── 8_new_visit_dialog.png
│   │   │   ├── 9_home_dashboard.png
│   │   │   ├── 10_medical_record_form.png
│   │   │   ├── 11_medical_record_view.png
│   │   │   ├── 12_medications_list.png
│   │   │   ├── 13_prescription_form.png
│   │   │   ├── 14_prescription_document.png
│   │   │   ├── 15_sick_leave_form.png
│   │   │   ├── 16_sick_leave_document.png
│   │   │   ├── 17_profile.png
│   │   │   └── 18_edit_profile.png
│   │   ├── blazor-client/
│   │   │   ├── 1_login.png
│   │   │   ├── 2_main_layout.png
│   │   │   ├── 3_visits_list.png
│   │   │   ├── 4_visit_details.png
│   │   │   ├── 5_medical_history.png
│   │   │   ├── 6_download_pdf.png
│   │   │   ├── 7_prescription.png
│   │   │   ├── 8_print_prescription.png
│   │   │   ├── 9_sick_leave.png
│   │   │   ├── 10_print_sick_leave.png
│   │   │   ├── 11_profile.png
│   │   │   ├── 12_edit_profile.png
│   │   │   └── 13_change_password.png
│   │   └── webapi/
│   │       ├── 1_swagger_ui.png
│   │       ├── 2_swagger_login.png
│   │       ├── 3_swagger_auth.png
│   │       ├── 4_swagger_patients_get.png
│   │       ├── 5_validation_error.png
│   │       └── 6_authorization_error.png
│   └── examples/
│       ├── curl_commands.sh
│       ├── postman_collection.json
│       └── test_scenarios.md

================================================================================
Ostatnia aktualizacja: 2026-05-23
Wersja: 1.0
================================================================================
