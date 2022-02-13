namespace Powerplant.Core.Domain.Model.System
{
    /// <summary>
    /// Detail of Error
    /// </summary>
    public sealed class ErrorDetail
    {
        /// <summary>
        /// Code
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Field
        /// </summary>
        public string? Field { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }

        public ErrorDetail()
        { }

        public ErrorDetail(string Error)
        {
            this.Error = Error;
        }

        public ErrorDetail(string Code, string Error)
        {
            this.Code = Code;
            this.Error = Error;
        }

        public ErrorDetail(string Code, string Field, string Error)
        {
            this.Code = Code;
            this.Field = Field;
            this.Error = Error;
        }
    }

}
