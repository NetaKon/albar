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
