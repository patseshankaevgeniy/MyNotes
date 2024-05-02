//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using System.Threading.Tasks;

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"
#pragma warning disable 8625 // Disable "CS8625 Cannot convert null literal to non-nullable reference type"
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

namespace WebApi.IntegrationTests.Infrastructure.Client
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface IMynotesClient
    {

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<LogInResultDto> LogInAsync(LogInDto body = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<LogInResultDto> SignUpAsync(SignUpDto body = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<MemberDto>> GetMembersAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>No Content</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task CreateMemberAsync(CreateMemberDto body = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>No Content</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task AcceptInvitationAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>No Content</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task DeleteMemberAsync(System.Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<UserNoteDto>> GetUserNotesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<UserNoteDto> CreateUserNoteAsync(UserNoteDto body = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>No Content</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task DeleteUserNoteAsync(System.Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<UserDto> GetCurrentUserAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.Generic.ICollection<UserDto>> GetUsersAsync(bool? hasMembers = null, bool? excludeCurrent = null, string searchPattern = null, bool? onlyCurrentUserMembers = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>No Content</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task UpdateUserAsync(UserDto body = null, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CreateMemberDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("userId")]
        public System.Guid? UserId { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static CreateMemberDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<CreateMemberDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ErrorDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string Message { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("details")]
        public string Details { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static ErrorDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<ErrorDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class LogInDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("password")]
        public string Password { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static LogInDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<LogInDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class LogInResultDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("failureReason")]
        public int? FailureReason { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static LogInResultDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<LogInResultDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class MemberDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public System.Guid Id { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("userId")]
        public System.Guid UserId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("status")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public MemberStatus Status { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("secondName")]
        public string SecondName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static MemberDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<MemberDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum MemberStatus
    {

        [System.Runtime.Serialization.EnumMember(Value = @"None")]
        None = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"Approved")]
        Approved = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"WaitForApproval")]
        WaitForApproval = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"RequiredApproval")]
        RequiredApproval = 3,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum NotePriority
    {

        [System.Runtime.Serialization.EnumMember(Value = @"Low")]
        Low = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"Medium")]
        Medium = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"High")]
        High = 2,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class SignUpDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("secondName")]
        public string SecondName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("password")]
        public string Password { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static SignUpDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<SignUpDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class UserDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("userId")]
        public System.Guid UserId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("hasMembers")]
        public bool HasMembers { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("secondName")]
        public string SecondName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("languageId")]
        public System.Guid LanguageId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("email")]
        public string Email { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static UserDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<UserDto>(data, options);

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class UserNoteDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public System.Guid? Id { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("text")]
        public string Text { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("created")]
        public System.DateTime? Created { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("сompletion")]
        public System.DateTime? Сompletion { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("userId")]
        public System.Guid? UserId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("priority")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public NotePriority Priority { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("isActual")]
        public bool? IsActual { get; set; }

        public string ToJson()
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Serialize(this, options);

        }
        public static UserNoteDto FromJson(string data)
        {

            var options = new System.Text.Json.JsonSerializerOptions();

            return System.Text.Json.JsonSerializer.Deserialize<UserNoteDto>(data, options);

        }

    }



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.0.7.0 (NJsonSchema v11.0.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604
#pragma warning restore 8625