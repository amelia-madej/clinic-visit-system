================================================================================
CLINIC VISIT SYSTEM - DOKUMENTACJA DLA PACJENTÓW
BlazorClient Application
================================================================================

SPIS TREŚCI:
1. Wstęp
2. Logowanie do systemu
3. Nawigacja i układ interfejsu
4. Moje wizyty
5. Historia medyczna
6. Moje recepty
7. Moje zaświadczenia
8. Profil i ustawienia
9. Pytania i odpowiedzi
10. Wskazówki dla pacjenta

================================================================================
1. WSTĘP
================================================================================

BlazorClient to interfejs przeznaczony dla pacjentów przychodni. Umożliwia:
- Przeglądanie harmonogramu swoich wizyt
- Dostęp do historii medycznej
- Pobieranie recept
- Pobieranie zaświadczeń lekarskich
- Zarządzanie swoim profilem

================================================================================
2. LOGOWANIE DO SYSTEMU
================================================================================

Dostęp: https://localhost:7002

DANE PACJENTÓW (TESTOWE):

Pacjent 1:
Email:    jane.smith@example.com
Hasło:    password123

Pacjent 2:
Email:    anna.kowalska@example.com
Hasło:    password123

Pacjent 3:
Email:    marek.nowak@example.com
Hasło:    password123

KROKI LOGOWANIA:
1. Otwórz aplikację pod adresem https://localhost:7002
2. Na ekranie logowania wpisz swój email
3. Wpisz hasło (domyślnie: password123)
4. Kliknij przycisk "Zaloguj się" lub naciśnij Enter
5. System sprawdzi dane i zaloguje Cię
6. Po zalogowaniu będziesz na stronie głównej (Home)

[ZRZUT EKRANU: Ekran logowania - prosty formularz ze polami Email i Hasło]

================================================================================
3. NAWIGACJA I UKŁAD INTERFEJSU
================================================================================

MENU GŁÓWNE (Lewa strona):
Po zalogowaniu widać menu z następującymi opcjami:

├─ Home (Strona główna)
│  └ Pulpit z podsumowaniem
├─ Visits (Moje wizyty)
│  └ Lista Twoich zaplanowanych i przeszłych wizyt
├─ Profile (Mój profil)
│  └ Twoje dane osobowe i ustawienia

PASEK GÓRNY (Górny fragment ekranu):
- Nazwa aplikacji / logo (lewa strona)
- Przycisk menu (hamburger ☰) - mobilne urządzenia
- Twoje imię i nazwisko (prawa strona)
- Przycisk wylogowania (prawa strona)
- Godzina i data

STRUKTURA STRONY:
- Nagłówek z Twoim imieniem
- Opis bieżącej strony
- Zawartość (formularz, tabela, szczegóły)
- Stopka z informacjami

[ZRZUT EKRANU: Główny układ aplikacji - menu boczne i zawartość]

================================================================================
4. MOJE WIZYTY
================================================================================

DOSTĘP: Menu → Visits

CO TUTAJ ZOBACZYSZ:

Tabelę z Twoimi wizytami zawierającą:
- Data i godzina wizyty
- Imię lekarza
- Typ wizyty:
  * In-Person (osobiście w klinice)
  * Telemedicine (wideokonferencja/rozmowa)
  * HomeVisit (wizyta u Ciebie w domu)
- Status wizyty:
  * Scheduled (zaplanowana)
  * Completed (już się odbyła)
  * Cancelled (anulowana)

KOLORY STATUSÓW:
- Zaplanowana: NIEBIESKI chip
- Zakończona: ZIELONY chip
- Anulowana: CZERWONY chip

FUNKCJE:

1. WYSZUKIWANIE:
   - Wpisz datę lub imię lekarza w pole wyszukiwania
   - System automatycznie filtruje listę

2. SORTOWANIE:
   - Kliknij na nagłówek kolumny, aby posortować
   - Kolejne kliknięcie zmienia kierunek sortowania

3. SZCZEGÓŁY WIZYTY:
   - Kliknij na wiersz wizyty
   - Widoczna będzie:
     * Pełna data i godzina
     * Imię i specjalizacja lekarza
     * Historia medyczna z tej wizyty (jeśli dostępna)
     * Wystawione recepty (jeśli dostępne)
     * Zaświadczenia lekarskie (jeśli dostępne)

4. POBIERANIE DOKUMENTÓW:
   - Po kliknięciu na wizytę, możesz zobaczyć powiązane dokumenty
   - Kliknij ikonę pobierania (↓) obok dokumentu
   - Plik zostanie pobrany na Twój komputer

[ZRZUT EKRANU: Lista moich wizyt - tabela z wielokolorowymi statusami]
[ZRZUT EKRANU: Szczegóły wizyty - pełne informacje z histoią medyczną]

================================================================================
5. HISTORIA MEDYCZNA
================================================================================

DOSTĘP: Menu → Visits → Kliknij na wizytę → Zakładka "Medical History"

CO TO JEST:

Historia medyczna to dokumentacja z Twojej wizyty zawierająca:
- Wywiad lekarski (co powiedziałeś/powiedziałaś lekarzowi)
- Diagnoza (zdiagnozowana choroba lub stan)
- Zalecenia (co Tobie zalecił lekarz)

STRUKTURA DOKUMENTU:

1. DATA WIZYTY:
   - Dokładna data i godzina wizyty

2. IMIĘ LEKARZA:
   - Lekarz, który Cię badał

3. WYWIAD (Interview):
   - Opis rozmowy z lekarzem
   - Twoje objawy
   - Odpowiedzi na pytania lekarza
   - Przykład: "Patient reports recurring chest tightness on exertion,
     lasting a few minutes, relieved by rest."

4. DIAGNOZA (Diagnosis):
   - Zdiagnozowana choroba lub stan zdrowia
   - Może zawierać kod ICD-10
   - Przykład: "Stable angina pectoris"

5. ZALECENIA (Recommendations):
   - Co powinieneś robić, aby czuć się lepiej
   - Jakie leki przyjmować
   - Kiedy wrócić do lekarza
   - Przykład: "Continue current beta-blocker therapy. Avoid strenuous exercise."

POBIERANIE DOKUMENTU:
- Pod histoią medyczną jest przycisk "Download as PDF"
- Kliknij, aby pobrać dokument
- Możesz go wydrukować lub przechowywać elektronicznie

WAŻNE:
- Historia medyczna jest poufna i dostępna tylko dla Ciebie i Twojego lekarza
- Historia medyczna jest tworzona po wizycie
- Jeśli brakuje Ci informacji, skontaktuj się z kliniką

[ZRZUT EKRANU: Historia medyczna - dokument z trzema sekcjami]
[ZRZUT EKRANU: Pobieranie PDF - przycisk do pobrania dokumentu]

================================================================================
6. MOJE RECEPTY
================================================================================

DOSTĘP: Menu → Visits → Kliknij na wizytę → Zakładka "Prescriptions"

CO TO JEST:

Recepta to dokument wystawiony przez lekarza zawierający:
- Nazwy leków, które powinieneś/powinna przyjmować
- Dawki (ilość leku)
- Instrukcje (ile razy dziennie i jak długo)

STRUKTURA RECEPTY:

1. NUMER RECEPTY:
   - Unikatowy numer (np. RX-20260523-001)

2. DATA WYSTAWIENIA:
   - Data wystawienia recepty przez lekarza

3. PACJENT:
   - Twoje imię i nazwisko
   - PESEL (dla identyfikacji)

4. LEKARZ:
   - Imię i specjalizacja lekarza

5. POZYCJE RECEPTY - LEKI:
   Dla każdego leku:
   - Nazwa leku (np. "Aspirin")
   - Dawka (np. "500 mg")
   - Ilość opakowań (np. "30 tabletek")
   - Instrukcje (np. "1-2 tabletki 3x dziennie po jedzeniu")

6. INSTRUKCJE OGÓLNE:
   - Dodatkowe informacje (opcjonalnie)

7. PODPIS LEKARZA:
   - Elektroniczny podpis lekarza
   - Pieczęć przychodni (w wersji papierowej)

JAK UŻYWAĆ RECEPTĘ:

1. Pobierz receptę (przycisk "Download" lub "Print")
2. Wydrukuj lub pokaż na urządzeniu w aptece
3. Aptekarzu: podaj receptę lub numer recepty
4. Odbierz leki zgodnie z recepty

WAŻNE INFORMACJE:

- Receptę można wydrukować lub pokazać w wersji elektronicznej
- Receptę można pobrać wielokrotnie
- Jeśli masz pytania o lek, skonsultuj się z farmaceută lub lekarzem
- Pamiętaj o dacie ważności recepty (zwykle 30 dni)

[ZRZUT EKRANU: Receptę - dokument z listą leków i instrukcjami]
[ZRZUT EKRANU: Pobieranie recepty - przycisk Print/Download]

================================================================================
7. MOJE ZAŚWIADCZENIA LEKARSKIE
================================================================================

DOSTĘP: Menu → Visits → Kliknij na wizytę → Zakładka "Sick Leave"

CO TO JEST:

Zaświadczenie lekarskie (zwolnienie) to dokument potwierdzający, że byłeś
niezdolny do pracy z powodu choroby lub urazu.

KIEDY PACJENT OTRZYMUJE ZAŚWIADCZENIE:
- Pacjent jest zbyt chory, aby pracować
- Pacjent wymaga opieki nad dzieckiem (rodzic w domu)
- Pacjent wymaga odpoczynku i nie może pracować
- Pacjent wymaga zabiegu chirurgicznego i rekonwalescencji

STRUKTURA ZAŚWIADCZENIA:

1. NUMER ZAŚWIADCZENIA:
   - Unikatowy numer (np. SL-20260523-001)

2. DATY ZWOLNIENIA:
   - Data początkowa (od kiedy zwolniony)
   - Data końcowa (do kiedy zwolniony)
   - Liczba dni zwolnienia

3. PACJENT:
   - Twoje imię i nazwisko
   - PESEL

4. LEKARZ:
   - Imię i specjalizacja lekarza
   - Numer licencji

5. PRZYCZYNA ZWOLNIENIA:
   - Choroba lub przyczyna
   - Przykład: "Acute lumbar strain — unable to perform physical work"

6. OGRANICZENIA:
   - Co Ci jest zabronione (np. "brak pracy fizycznej")
   - Co możesz robić (opcjonalnie)

7. PODPIS LEKARZA:
   - Elektroniczny podpis lekarza
   - Pieczęć przychodni

JAK UŻYWAĆ ZAŚWIADCZENIE:

1. Pobierz zaświadczenie (przycisk "Download" lub "Print")
2. Wydrukuj zaświadczenie
3. Podaj zaświadczenie swojemu pracodawcy
4. Zachowaj kopię dla siebie

INFORMACJE WAŻNE:

- Zaświadczenie jest ważne od daty początkowej do daty końcowej
- Pracodawca musi zaakceptować zaświadczenie lekarskie
- W razie pytań, skontaktuj się z działem HR lub kliniką
- Zaświadczenie dotyczy CAŁEJ nieobecności (nie możesz pracować)

[ZRZUT EKRANU: Zaświadczenie lekarskie - dokument z datami i przyczyn]
[ZRZUT EKRANU: Wydruk zaświadczenia - wersja do papierowej kopii]

================================================================================
8. PROFIL I USTAWIENIA
================================================================================

DOSTĘP: Menu → Profile

DANE PROFILOWE:

Widok zawiera Twoje dane osobowe:
- Imię
- Nazwisko
- Email (adres e-mail)
- Numer telefonu
- PESEL
- Data urodzenia
- Płeć
- Adres zamieszkania
- Zdjęcie profilowe (opcjonalnie)

EDYCJA PROFILU:

Aby zmienić swoje dane:
1. Kliknij przycisk "Edit Profile"
2. Zmień dane, które chcesz aktualizować
3. Kliknij "Save Changes"
4. System potwierdzi zmianę

ZMIANA HASŁA:

Aby zmienić hasło:
1. Kliknij "Change Password"
2. Wpisz bieżące hasło (potwierdzenie)
3. Wpisz nowe hasło
4. Potwierdź nowe hasło (wpisz jeszcze raz)
5. Kliknij "Update Password"
6. System potwierdzi zmianę

ZDJĘCIE PROFILOWE:

Aby dodać lub zmienić zdjęcie:
1. Kliknij ikonę zdjęcia lub przycisk "Upload Photo"
2. Wybierz plik zdjęcia ze swojego komputera
3. System załaduje zdjęcie
4. Kliknij "Save"

WYLOGOWANIE:

Aby wylogować się z systemu:
1. Kliknij przycisk "Logout" w górnym rogu
2. System wyloguje Cię
3. Będziesz przekierowany na ekran logowania

WAŻNE INFORMACJE:

- Dane są przechowywane bezpiecznie
- Możesz zmienić swoje dane w dowolnym momencie
- Hasło powinno być mocne (kombinacja liter, cyfr i znaków)
- Nigdy nie udostępniaj hasła innym osobom

[ZRZUT EKRANU: Profil użytkownika - moje dane personalne]
[ZRZUT EKRANU: Edycja profilu - formularz do zmian]
[ZRZUT EKRANU: Zmiana hasła - bezpieczna zmiana hasła]

================================================================================
9. PYTANIA I ODPOWIEDZI (FAQ)
================================================================================

P: Czy mogę zmieniać zaplanowane wizyty?
O: Zmiany wizyt mogą być dokonane przez klinikę. Skontaktuj się z kliniką
   telefonicznie lub mailowo, aby zmienić datę wizyty.

P: Jak długo receptę mogę przechowywać?
O: Recepta jest ważna zwykle 30 dni od daty wystawienia. Patrz datę
   na recepcie.

P: Czy historia medyczna jest poufna?
O: Tak! Historia medyczna jest poufna i chroniona przez prawo. Dostęp mają
   tylko Ty, Twój lekarz i pracownicy przychodni.

P: Co zrobić, jeśli zapomniałem hasła?
O: Skontaktuj się z administratorem przychodni. Pomoże Ci w zresetowaniu
   hasła.

P: Czy mogę pobrać wszystkie moje dokumenty na raz?
O: Nie, ale możesz pobrać każdy dokument indywidualnie. Pobierz wizyty
   pojedynczo.

P: Jak długo dane są przechowywane?
O: Dane medyczne są przechowywane zgodnie z polskim prawem (zwykle 10 lat).
   Skontaktuj się z kliniką w przypadku pytań.

P: Czy mogę wstawić receptę za pomocą aplikacji?
O: Nie. Recepty muszą być wstawiane przez lekarza podczas wizyty lub
   teleporad.

P: Co zrobić, jeśli dostanę błąd "Connection failed"?
O: Sprawdź połączenie internetowe. Jeśli problem trwa, spróbuj odświeżyć
   stronę (F5) lub skontaktuj się z supportem.

P: Czy aplikacja jest bezpieczna?
O: Tak. Aplikacja używa szyfrowania HTTPS i hasło jest chronione.

P: Czy mogę uzyskać dostęp do aplikacji z telefonu?
O: Tak. Aplikacja jest responsywna i działa na telefonach i tabletach.

================================================================================
10. WSKAZÓWKI DLA PACJENTA
================================================================================

1. REGULARNE LOGOWANIE:
   - Loguj się regularnie, aby sprawdzać nowe wizyty
   - W kalendarzu będą widoczne przedstojące wizyty

2. POBIERANIE DOKUMENTÓW:
   - Zawsze pobieraj i archiwizuj swoje dokumenty
   - W razie utraty konta będziesz miał kopię

3. BEZPIECZEŃSTWO HASŁA:
   - Używaj mocnego hasła (mix liter, cyfr, znaków)
   - Nie udostępniaj hasła innym
   - Wyloguj się po zakończeniu pracy

4. HISTORIA MEDYCZNA:
   - Czytaj uważnie zalecenia lekarza
   - Bierz leki zgodnie z instrukcjami
   - Zgłoś wszelkie efekty uboczne lekarzowi

5. WIZYTY:
   - Przychodzić na czas na wizytę
   - Przynieść dokumenty medyczne, jeśli masz
   - Notuj swoje pytania przed wizytą

6. RECEPTY:
   - Zawsze zabierz receptę do apteki
   - Sprawdź, czy wszystkie leki się zgadzają
   - Pytaj aptekarza o instrukcje

7. ZAŚWIADCZENIA:
   - Podaj zaświadczenie szybko pracodawcy
   - Zachowaj kopię dla siebie
   - W razie pytań, skontaktuj się z kliniką

8. OBSŁUGA BŁĘDÓW:
   - Jeśli widać "Loading...", odczekaj kilka sekund
   - Jeśli błąd trwa, odśwież stronę (F5 lub Ctrl+F5)
   - Jeśli to nie pomaga, skontaktuj się z supportem

9. PRYWATNOŚĆ:
   - Nigdy nie wychodzisz bez wylogowania
   - Nie otwieraj aplikacji na komputerach publicznych
   - Zachowaj bezpieczeństwo swoich danych

10. KONTAKT Z KLINIKĄ:
    - Jeśli masz pytania, zadzwoń do przychodni
    - Nie ufaj radiom czy internetowym poradom
    - Zaufaj swoim lekarzom

================================================================================
PODSUMOWANIE - SZYBKI START
================================================================================

1. Zaloguj się na swoje konto
   Email: jane.smith@example.com (lub Twój email)
   Hasło: password123 (zmień go po pierwszym logowaniu!)

2. Przejrzyj swoje wizyty
   Menu → Visits

3. Sprawdzaj historię medyczną
   Visits → Kliknij wizytę → Medical History

4. Pobierz recepty i zaświadczenia
   Visits → Kliknij wizytę → Prescriptions lub Sick Leave

5. Zarządzaj swoim profilem
   Menu → Profile

6. Wyloguj się po skończeniu
   Kliknij "Logout" w górnym rogu

================================================================================
Ostatnia aktualizacja: 2026-05-23
Wersja: 1.0
Kontakt: support@klinika.pl | Tel: +48-XX-XXX-XX-XX
================================================================================
