using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BATCH336B.ViewModel
{
    public class VMMMedicalItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        
        public long? MedicalItemCategoryId { get; set; }
        public string? MedicalItemCategoryName { get; set; }

        public string? Composition { get; set; }

        public long? MedicalItemSegmentationId { get; set; }
        public string? MedicalItemSegmentationName { get; set; }

        public string? Manufacturer { get; set; }
        public string? Indication { get; set; }
        public string? Dosage { get; set; }
        public string? Directions { get; set; }
        public string? Contraindication { get; set; }
        public string? Caution { get; set; }
        public string? Packaging { get; set; }
        public long? PriceMin { get; set; }
        public long? PriceMax { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
