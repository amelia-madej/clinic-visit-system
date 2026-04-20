namespace Domain.Models;

public class Medication
{
    public int MedicationId { get; set; }
    public string Name { get; set; }
    public string DosageForm { get; set; } // orally, topical, intravenously
    public string Form { get; set; } // "tablet", "capsule", "syrup"
    public decimal StrengthValue { get; set; } // 300
    public string StrengthUnit { get; set; }   // "mg"
    public string Manufacturer { get; set; } // "Pfizer"
    public string Packaging { get; set; } // "box of 20 tablets"
    public string ActiveIngredient { get; set; } // "Paracetamol"
    public List<PrescriptionItem>? PrescriptionItems { get; set; }
}
