##  Run the Application

### Run the Server
Open the terminal/command prompt from the root directory of the project and run:

```
cd server
cd StoreAPI
dotnet restore
dotnet ef database update 
dotnet run
```
 
Open the swagger at http://localhost:5242/swagger/index.html

Login with:

```
{
	"username": "user@example.com",
	"password": "User@123"
}
```

### Run the Client
Open the terminal/command prompt from the root directory of the project and run:
```console
cd client
npm install
ng serve
```
Open the browser at http://localhost:4200/

###  Global Exception Handling
I am using a global filter to handle exceptions (instead of scattering try-catch blocks). This filter intercepts exceptions and converts them to appropriate HTTP responses.

```csharp
public class GlobalExceptionFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		var statusCode = context.Exception switch
		{
			NotFoundException => StatusCodes.Status404NotFound,
			ValidationException => StatusCodes.Status400BadRequest,
			UserAlreadyExistsException => StatusCodes.Status409Conflict,
			Application.Exceptions.UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
			_ => StatusCodes.Status500InternalServerError
		};
		
		context.Result = new ObjectResult(new { error = context.Exception.Message })
		{
			StatusCode = statusCode
		};
	
		context.ExceptionHandled = true;
	}
}
```
