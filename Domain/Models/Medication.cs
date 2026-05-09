namespace Domain.Models;

public class Medication
{
    public int MedicationId { get; set; }
    public string Name { get; set; } = null!;
    public string DosageForm { get; set; } = null!; // orally, topical, intravenously
    public string Form { get; set; } = null!; // "tablet", "capsule", "syrup"
    public decimal StrengthValue { get; set; } // 300
    public string StrengthUnit { get; set; } = null!;  // "mg"
    public string Manufacturer { get; set; } = null!; // "Pfizer"
    public string Packaging { get; set; } = null!; // "box of 20 tablets"
    public string ActiveIngredient { get; set; } = null!; // "Paracetamol"
    public List<PrescriptionItem>? PrescriptionItems { get; set; }
}
