using ClosedXML.Excel;
using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MedicationImportService : IMedicationImportService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly HttpClient _httpClient;

        private const string DownloadUrl =
            "https://api.dane.gov.pl/resources/65521,wykaz-produktow-leczniczych-plik-w-formacie-xlsx/file";

        // Column header names
        private const string ColName             = "Nazwa produktu leczniczego";
        private const string ColForm             = "Postać farmaceutyczna";
        private const string ColStrength         = "Moc";
        private const string ColManufacturer     = "Podmiot odpowiedzialny";
        private const string ColActiveIngredient = "Substancja czynna";
        private const string ColDosageForm       = "Droga podania - Gatunek - Tkanka - Okres karencji";
        private const string ColPackaging        = "Opakowanie";

        public MedicationImportService(IClinicUnitOfWork uow, IHttpClientFactory httpClientFactory)
        {
            _uow = uow;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<int> ImportAsync()
        {
            var bytes = await _httpClient.GetByteArrayAsync(DownloadUrl);

            using var stream = new System.IO.MemoryStream(bytes);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            // Map header names to column numbers
            var headers = worksheet.Row(1).CellsUsed()
                .ToDictionary(c => c.Value.ToString(), c => c.Address.ColumnNumber, StringComparer.OrdinalIgnoreCase);

            ValidateHeaders(headers);

            var existing = _uow.MedicationRepository.GetAll()
                .Select(m => m.Name)
                .ToHashSet();

            int imported = 0;

            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                var name = GetCell(row, headers, ColName);
                if (string.IsNullOrWhiteSpace(name) || existing.Contains(name))
                    continue;

                var (strengthValue, strengthUnit) = ParseStrength(GetCell(row, headers, ColStrength));

                var medication = new Medication
                {
                    Name             = name,
                    Form             = GetCell(row, headers, ColForm),
                    DosageForm       = GetCell(row, headers, ColDosageForm),
                    StrengthValue    = strengthValue,
                    StrengthUnit     = strengthUnit,
                    Manufacturer     = GetCell(row, headers, ColManufacturer),
                    Packaging        = GetCell(row, headers, ColPackaging),
                    ActiveIngredient = GetCell(row, headers, ColActiveIngredient)
                };

                _uow.MedicationRepository.Insert(medication);
                existing.Add(name);
                imported++;
            }

            if (imported > 0)
                _uow.Commit();

            return imported;
        }

        private static void ValidateHeaders(Dictionary<string, int> headers)
        {
            var required = new[] { ColName, ColForm, ColStrength, ColManufacturer, ColActiveIngredient, ColDosageForm, ColPackaging };
            var missing = required.Where(h => !headers.ContainsKey(h)).ToList();
            if (missing.Any())
                throw new Exception($"XLSX is missing expected columns: {string.Join(", ", missing)}. Actual columns found: {string.Join(", ", headers.Keys)}");
        }

        private static string GetCell(IXLRow row, Dictionary<string, int> headers, string columnName)
        {
            if (!headers.TryGetValue(columnName, out var colIndex))
                return string.Empty;
            return row.Cell(colIndex).Value.ToString() ?? string.Empty;
        }

        private static (decimal value, string unit) ParseStrength(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return (0, string.Empty);

            var parts = raw.Trim().Split(' ', 2);
            if (parts.Length == 2 && decimal.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
                return (val, parts[1]);

            return (0, raw);
        }
    }
}
