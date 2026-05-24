================================================================================
CLINIC VISIT SYSTEM - DOKUMENTACJA DLA LEKARZY
BlazorServer Application
================================================================================

SPIS TREŚCI:
1. Wstęp
2. Logowanie do systemu
3. Nawigacja i układ interfejsu
4. Zarządzanie pacjentami
5. Harmonogram wizyt
6. Historia medyczna
7. Recepty i leki
8. Zaświadczenia lekarskie
9. Profil użytkownika
10. Wskazówki i porady

================================================================================
1. WSTĘP
================================================================================

BlazorServer to interfejs przeznaczony dla lekarzy i pracowników kliniki.
Umożliwia:
- Przeglądanie listy pacjentów
- Zarządzanie harmonogramem wizyt
- Dokumentowanie historii medycznej
- Wystawianie recept
- Generowanie zaświadczeń lekarskich

================================================================================
2. LOGOWANIE DO SYSTEMU
================================================================================

Dostęp: https://localhost:7001

DANE LEKARZA (TESTOWE):
Email:    john.doe@example.com
Hasło:    password123
Rola:     Lekarz (Cardiology - Kardiologia)

KROKI LOGOWANIA:
1. Otwórz aplikację pod adresem https://localhost:7001
2. Wpisz email: john.doe@example.com
3. Wpisz hasło: password123
4. Kliknij "Zaloguj się"
5. Po zalogowaniu będziesz przekierowany na stronę główną

UWAGA: Jeśli zapomnisz hasła, skontaktuj się z administratorem systemu.

[ZRZUT EKRANU: Ekran logowania - formularz z polami Email i Hasło]

================================================================================
3. NAWIGACJA I UKŁAD INTERFEJSU
================================================================================

MENU GŁÓWNE:
Po lewej stronie widać pasek boczny (navbar) z następujących opcji:

├─ Home
│  └ Pulpit główny (Dashboard) z przeglądem wizyt
├─ Patients
│  └ Lista pacjentów w clinice
├─ Visits
│  └ Harmonogram wszystkich wizyt
├─ Medications
│  └ Katalog dostępnych leków
├─ Profile
│  └ Twój profil i ustawienia

PASEK GÓRNY:
- Logo/nazwa aplikacji (lewy górny róg)
- Przycisk wylogowania (prawy górny róg)
- Godzina i data aktualnego dnia

[ZRZUT EKRANU: Główny layout aplikacji z menu bocznym]

================================================================================
4. ZARZĄDZANIE PACJENTAMI
================================================================================

DOSTĘP: Menu → Patients

LISTA PACJENTÓW:

Widok tabelaryczny z kolumnami:
- Last Name (Nazwisko)
- First Name (Imię)
- Age (Wiek)
- Last Visit (Data ostatniej wizyty)
- Przycisk (oczy) - szczegóły pacjenta

FUNKCJE:

1. PRZESZUKIWANIE:
   - Wpisz imię lub nazwisko w pole wyszukiwania
   - System automatycznie filtruje listę

2. SORTOWANIE:
   - Kliknij na nagłówek kolumny, aby sortować
   - Zaznacz/Odznacz kolumny do wyświetlania

3. NOWY PACJENT:
   - Kliknij przycisk "+ New Patient"
   - Wpisz dane pacjenta:
     * Imię (First Name)
     * Nazwisko (Last Name)
     * Email
     * Numer telefonu
     * PESEL
     * Data urodzenia
     * Płeć (Male/Female/Other)
     * Adres
   - Kliknij "Save"

4. SZCZEGÓŁY PACJENTA:
   - Kliknij ikonę oka obok pacjenta
   - Widok zawiera:
     * Dane personalne
     * Historię medyczną (ostatnie wizyty)
     * Ostatnie recepty
     * Dane kontaktowe

[ZRZUT EKRANU: Lista pacjentów - tabela z ikonami akcji]
[ZRZUT EKRANU: Szczegóły pacjenta - formularz z danymi]
[ZRZUT EKRANU: Nowy pacjent - dialog do dodawania pacjenta]

================================================================================
5. HARMONOGRAM WIZYT
================================================================================

DOSTĘP: Menu → Visits lub przycisk "Manage visits" na pulpicie

LISTA WIZYT:

Widok tabelaryczny z kolumnami:
- Date & Time (Data i godzina)
- Patient (Imię i nazwisko pacjenta)
- Doctor (Lekarz prowadzący)
- Type (Typ wizyty: In-Person, Telemedicine, HomeVisit)
- Status (Staatus: Scheduled, Completed, Cancelled)
- Akcje (szczegóły, edycja, usunięcie)

STATUS WIZYT:
- Scheduled (Zaplanowana) - niebieski chip
- Completed (Zakończona) - zielony chip
- Cancelled (Anulowana) - czerwony chip

TYPY WIZYT:
- In-Person (osobiście w klinice)
- Telemedicine (konsultacja online)
- HomeVisit (wizyta domowa)

FUNKCJE:

1. NOWA WIZYTA:
   - Kliknij "+ New Visit"
   - Formularz:
     * Pacjent (dropdown)
     * Lekarz (będzie Twoje imię, jeśli jesteś lekarzem)
     * Data i godzina
     * Typ wizyty
   - Kliknij "Save"

2. EDYCJA WIZYTY:
   - Kliknij ikonę edycji (ołówek)
   - Zmień dane
   - Kliknij "Save"

3. ANULOWANIE WIZYTY:
   - Kliknij ikonę kosza (delete)
   - Potwierdź anulowanie

4. SZCZEGÓŁY WIZYTY:
   - Kliknij na wiersz wizyty
   - Widok szczegółów zawiera:
     * Dane pacjenta
     * Historia medyczna z tej wizyty
     * Recepty wystawione
     * Zaświadczenia

5. FILTROWANIE I SORTOWANIE:
   - Wpisz w pole wyszukiwania
   - Kliknij na nagłówki kolumn do sortowania

[ZRZUT EKRANU: Lista wizyt - tabela z kolorowymi statusami]
[ZRZUT EKRANU: Szczegóły wizyty - pełne dane wizyty z histoią medyczną]
[ZRZUT EKRANU: Nowa wizyta - dialog do rezerwacji wizyty]
[ZRZUT EKRANU: Pulpit (Home) - karty ze statystykami wizyt]

================================================================================
6. HISTORIA MEDYCZNA
================================================================================

DOSTĘP: Menu → Visits → kliknij wizytę → zakładka "Medical Record"

DOKUMENTOWANIE WIZYTY:

Po zakończeniu wizyty możesz dodać historię medyczną:

1. OTWARCIE FORMULARZA:
   - Przejdź do szczegółów wizyty
   - Kliknij "Add Medical Record" lub "Edit Medical Record"

2. POLA FORMULARZA:
   a) Interview (Wywiad):
      - Opis rozmowy z pacjentem
      - Objawy, przyczyna wizyty
      - Pytania zadane pacjentowi
      - Format wolny (pole tekstowe)
   
   b) Diagnosis (Diagnoza):
      - Zdiagnozowana choroba/stan pacjenta
      - ICD-10 lub inna klasyfikacja (opcjonalnie)
      - Przykład: "Stable angina pectoris"
   
   c) Recommendations (Zalecenia):
      - Zalecane leczenie
      - Zmiany w trybie życia
      - Kolejne badania
      - Specjaliści do skonsultowania
      - Przykład: "Continue current beta-blocker therapy. Avoid strenuous exercise."

3. ZAPISANIE:
   - Kliknij "Save"
   - Historia medyczna będzie dostępna dla pacjenta i innych lekarzy

POWIĄZANE FUNKCJE:
- Recepty: Możesz wystawić receptę z tej samej wizyty
- Zaświadczenia: Możesz wygenerować zwolnienie lekarskie

[ZRZUT EKRANU: Formularz historii medycznej - pola Interview, Diagnosis, Recommendations]
[ZRZUT EKRANU: Historia medyczna - widok zapisanego rekordu]

================================================================================
7. RECEPTY I LEKI
================================================================================

DOSTĘP: Menu → Medications (katalog leków)
WYSTAWIANIE: Menu → Visits → szczegóły wizyty → "Add Prescription"

KATALOG LEKÓW:

Menu "Medications" wyświetla listę dostępnych leków w systemie:
- Nazwa leku
- Dawka
- Opakowanie
- Dostępność

Możesz przeszukiwać leki po nazwie.

WYSTAWIANIE RECEPTY:

1. DOSTĘP:
   - Przejdź do szczegółów wizyty
   - Kliknij "Add Prescription" lub "New Prescription"

2. FORMULARZ RECEPTY:
   a) Pacjent: (auto-wypełniony z wizyt)
   
   b) Pozycje recepty (Prescription Items):
      - Kliknij "+ Add Item"
      - Dla każdej pozycji:
        * Lek (dropdown - wybierz z listy)
        * Dawka (np. 500mg)
        * Ilość (liczba opakowań)
        * Instrukcje (np. "1-2 tabletki 3x dziennie")
      
      - Możesz dodać wiele leków w jednej recepcie
   
   c) Data recepty: (auto-wypełniona)
   
   d) Instrukcje ogólne: (opcjonalnie)

3. ZAPISANIE I WYDRUK:
   - Kliknij "Save"
   - System wygeneruje numer recepty
   - Receptę można pobrać jako PDF i wydrukować

4. EDYCJA:
   - Recepty można edytować przed wydaniem pacjentowi
   - Po wydaniu recepta jest zablokowana

[ZRZUT EKRANU: Katalog leków - tabela z lekami]
[ZRZUT EKRANU: Nowa recepta - formularz z pozycjami]
[ZRZUT EKRANU: Recepta - gotowy dokument do wydruku]

================================================================================
8. ZAŚWIADCZENIA LEKARSKIE
================================================================================

DOSTĘP: Menu → Visits → szczegóły wizyty → "Add Sick Leave"

GENEROWANIE ZAŚWIADCZENIA:

1. KIEDY WYSTAWIAĆ:
   - Pacjent jest niezdolny do pracy
   - Wymaga przerwy w pracy
   - Wymaga opieki nad dzieckiem lub inną osobą zależną

2. FORMULARZ:
   a) Pacjent: (auto-wypełniony)
   
   b) Start Date (Data początkowa):
      - Data od kiedy pacjent jest zwolniony
      - Format: DD.MM.YYYY
   
   c) End Date (Data końcowa):
      - Data ostatniego dnia zwolnienia
      - Format: DD.MM.YYYY
   
   d) Reason (Przyczyna):
      - Nazwa schorzenia/przyczyna
      - Przykład: "Acute lumbar strain — unable to perform physical work"
      - Może być szczegółowy opis ograniczeń

   e) Medical Record (Historia medyczna):
      - Auto-Link do wizyty
      - Zawiera diagnozę i zalecenia

3. ZAPISANIE:
   - Kliknij "Save"
   - Zaświadczenie jest generowane
   - Pacjent może pobrać i wydrukować

4. EDYCJA:
   - Zaświadczenia można edytować przed wydaniem
   - Po wydaniu zaświadczenie jest zablokowane

WAŻNE:
- Zaświadczenie musi być powiązane z wizytą
- Data początkowa zwolnienia nie może być przed datą wizyty
- Data końcowa musi być po dacie początkowej

[ZRZUT EKRANU: Nowe zaświadczenie - formularz z datami i przyczyn]
[ZRZUT EKRANU: Zaświadczenie - dokument do wydruku z pieczęcią lekarza]

================================================================================
9. PROFIL UŻYTKOWNIKA
================================================================================

DOSTĘP: Menu → Profile lub ikonka profilu w górnym rogu

DANE PROFILU:
- Imię i nazwisko
- Email
- Numer telefonu
- Specjalizacja (dla lekarzy)
- Numer licencji (dla lekarzy)
- Zdjęcie profilowe (opcjonalnie)

EDYCJA PROFILU:
1. Kliknij "Edit Profile"
2. Zmień dane
3. Kliknij "Save Changes"

ZMIANA HASŁA:
1. Kliknij "Change Password"
2. Wpisz stare hasło
3. Wpisz nowe hasło
4. Potwierdź nowe hasło
5. Kliknij "Update Password"

WYLOGOWANIE:
- Kliknij przycisk "Logout" w górnym rogu
- System wyloguje Cię i przeniesie na stronę logowania

[ZRZUT EKRANU: Profil użytkownika - dane lekarza]
[ZRZUT EKRANU: Edycja profilu - formularz do zmian]

================================================================================
10. WSKAZÓWKI I PORADY
================================================================================

1. EFEKTYWNE SZUKANIE:
   - Używaj paska wyszukiwania do szybkiego znalezienia pacjenta
   - Sortuj po ostatniej wizycie, aby znaleźć pacjentów wymagających śledzenia

2. ZARZĄDZANIE CZASEM:
   - Harmonogram wizyt ułatwia planowanie dnia
   - Widok "Home" pokazuje wizyt na dziś

3. DOKUMENTACJA:
   - Dokładnie dokumentuj każdą wizytę
   - Zalecenia pomagają pacjentom w przestrzeganiu zaleceń
   - Historia medyczna jest dostępna dla innych lekarzy

4. RECEPTY:
   - Najczęściej używane leki znajdują się na górze listy
   - Zawsze sprawdź dawkę i interakcje leków

5. BEZPIECZEŃSTWO:
   - Wyloguj się po zakończeniu pracy
   - Nie udostępniaj hasła innym osobom
   - Zgłos wszelkie podejrzane działania administratorowi

6. OBSŁUGA BŁĘDÓW:
   - Jeśli widzisz błąd "Loading...", odczekaj kilka sekund
   - Jeśli problem się powtarza, odśwież stronę (F5)
   - Jeśli błąd trwa, skontaktuj się z supportem

[ZRZUT EKRANU: Komunikat błędu - przykład obsługi błędów]
[ZRZUT EKRANU: Powiadomienie - potwierdzenie zapisania danych]

================================================================================
PODSUMOWANIE - NAJCZĘSTSZE ZADANIA
================================================================================

ZADANIE: Dodaj nowego pacjenta
KROKI: Menu → Patients → "+ New Patient" → Wpisz dane → Save

ZADANIE: Zaplanuj nową wizytę
KROKI: Menu → Visits → "+ New Visit" → Wybierz pacjenta i godzinę → Save

ZADANIE: Dokumentuj historię medyczną po wizycie
KROKI: Menu → Visits → Kliknij wizytę → "Add Medical Record" → Opisz → Save

ZADANIE: Wystawie receptę
KROKI: Menu → Visits → Kliknij wizytę → "Add Prescription" → Dodaj leki → Save

ZADANIE: Generuj zaświadczenie lekarskie
KROKI: Menu → Visits → Kliknij wizytę → "Add Sick Leave" → Wpisz daty → Save

================================================================================
Ostatnia aktualizacja: 2026-05-23
Wersja: 1.0
================================================================================
