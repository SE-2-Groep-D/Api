namespace Api.CustomMiddleware; 
public class AuthorizationHeaderMiddleware {

  private readonly RequestDelegate _next;

  public AuthorizationHeaderMiddleware(RequestDelegate next) {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context) {

    var token = context.Request.Cookies["access_token"];
    Console.WriteLine(token != null ? token : "er is geen token");
    if (token != null) {
      context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }

    await _next(context);
  }

}
