/* 
 * Moosend API
 *
 * TODO: Add a description
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Moosend.Wrappers.CSharpWrapper.Client.SwaggerDateConverter;

namespace Moosend.Wrappers.CSharpWrapper.Model
{
    /// <summary>
    /// CreatingANewSegmentRequest
    /// </summary>
    [DataContract]
    public partial class CreatingANewSegmentRequest :  IEquatable<CreatingANewSegmentRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatingANewSegmentRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreatingANewSegmentRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatingANewSegmentRequest" /> class.
        /// </summary>
        /// <param name="Name">The name of the segment. (required).</param>
        /// <param name="MatchType">Specifies how the segment&#39;s criteria will match together. This must be one of the following values. If not specified, &#x60;All&#x60; will be assumed..</param>
        public CreatingANewSegmentRequest(string Name = default(string), string MatchType = default(string))
        {
            // to ensure "Name" is required (not null)
            if (Name == null)
            {
                throw new InvalidDataException("Name is a required property for CreatingANewSegmentRequest and cannot be null");
            }
            else
            {
                this.Name = Name;
            }
            this.MatchType = MatchType;
        }
        
        /// <summary>
        /// The name of the segment.
        /// </summary>
        /// <value>The name of the segment.</value>
        [DataMember(Name="Name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Specifies how the segment&#39;s criteria will match together. This must be one of the following values. If not specified, &#x60;All&#x60; will be assumed.
        /// </summary>
        /// <value>Specifies how the segment&#39;s criteria will match together. This must be one of the following values. If not specified, &#x60;All&#x60; will be assumed.</value>
        [DataMember(Name="MatchType", EmitDefaultValue=false)]
        public string MatchType { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreatingANewSegmentRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  MatchType: ").Append(MatchType).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as CreatingANewSegmentRequest);
        }

        /// <summary>
        /// Returns true if CreatingANewSegmentRequest instances are equal
        /// </summary>
        /// <param name="other">Instance of CreatingANewSegmentRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreatingANewSegmentRequest other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) && 
                (
                    this.MatchType == other.MatchType ||
                    this.MatchType != null &&
                    this.MatchType.Equals(other.MatchType)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
                if (this.MatchType != null)
                    hash = hash * 59 + this.MatchType.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
