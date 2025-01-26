namespace InclusiveCity.Domain.Dto.Base
{
    public class GoogleMapsBaseResponseDto
    {
        public GoogleMapsErrorResponse? Error { get; set; }
    }

    public class GoogleMapsErrorResponse
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public string? Status { get; set; }
    }
}
