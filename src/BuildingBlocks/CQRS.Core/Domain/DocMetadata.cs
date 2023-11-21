using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CQRS.Core.Domain
{
    /*
    == Overview ==
        The DocMetadata class serves as an abstract base class providing a common structure for metadata
        associated with documents or data entities. It includes various metadata fields such as authorship,
        machine learning generation details, approval status, and data quality indicators.

    == Design Considerations ==
        1. The class is designed as an abstract base class, meant to be inherited by other classes that require metadata.
        2. It uses nullable properties to allow for optional metadata fields.
        3. The JsonIgnore attribute is used to prevent serialization of default values, reducing JSON size.
        4. The copy constructor facilitates the creation of deep copies, which is useful in scenarios where
        an existing metadata object needs to be replicated with the same values.

    == Usage ==
        This class is not intended to be instantiated directly but to be inherited by other classes that require metadata.
        Example:
        public class Document : DocMetadata
        {
            public string Content { get; set; }
            <Additional properties and methods specific to Document>
        }
    */

    public abstract class DocMetadata
    {

        /* Version Meta Data */
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Author { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? Date { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Comment { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Provenance { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Source { get; set; }



        /* ML Generated Properties */
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? IsMLGenerated { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? MLGeneratedBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime MLGeneratedDate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? MLGeneratedComment { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public float? MLGeneratedConfidence { get; set; }


        /* Approval Properties */
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ApprovalStatus { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? IsVerified { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? VerifiedBy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? VerifiedComment { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? VerifiedDate { get; set; }


        /* Data Quality Properties */
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? DataQualityIndicator { get; set; }


        /* Default Constructor */
        public DocMetadata()
        {
            Author = default!;
            Date = default!;
            Comment = default!;
            Provenance = default!;
            Source = default!;
            IsMLGenerated = default!;
            MLGeneratedBy = default!;
            MLGeneratedDate = default!;
            MLGeneratedComment = default!;
            MLGeneratedConfidence = default!;
            ApprovalStatus = default!;
            IsVerified = default!;
            VerifiedBy = default!;
            VerifiedComment = default!;
            VerifiedDate = default!;
            DataQualityIndicator = default!;
        }
        /* Copy Constructor */
        public DocMetadata(DocMetadata docMetadata)
        {
            Author = docMetadata.Author;
            Date = docMetadata.Date;
            Comment = docMetadata.Comment;
            Provenance = docMetadata.Provenance;
            Source = docMetadata.Source;
            IsMLGenerated = docMetadata.IsMLGenerated;
            MLGeneratedBy = docMetadata.MLGeneratedBy;
            MLGeneratedDate = docMetadata.MLGeneratedDate;
            MLGeneratedComment = docMetadata.MLGeneratedComment;
            MLGeneratedConfidence = docMetadata.MLGeneratedConfidence;
            ApprovalStatus = docMetadata.ApprovalStatus;
            IsVerified = docMetadata.IsVerified;
            VerifiedBy = docMetadata.VerifiedBy;
            VerifiedComment = docMetadata.VerifiedComment;
            VerifiedDate = docMetadata.VerifiedDate;
            DataQualityIndicator = docMetadata.DataQualityIndicator;
        }

    }
}