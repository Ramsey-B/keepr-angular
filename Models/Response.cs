namespace keepr_angular.Models
{
  public class Response
  {
    public string Message { get; set; }

    public Response(string message)
    {
        this.Message = message;
    }
  }
}