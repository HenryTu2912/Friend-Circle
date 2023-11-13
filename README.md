# ASPNET-Core
1. Check the dotnet information: dotnet --info
2. Show dotnet help menu: dotnet -h
3. Show the list of template in dotnet: dotnet new list
4. Create the solution file: dotnet new sln
5. Create a dotnet API with the name API: dotnet new webapi -n API
6. Add one or more projects to a solution file: dotnet sln add API/
7. List the project in a solution: dotnet sln list
8. Run the project: In terminal, goto API folder -> Run: dotnet run
9. Configure the dotnet run execution: Goto Properties folder -> launchSettings.json -> profiles -> http:
    -> applicationUrl: change to "http://localhost:5000; https://localhost:5001"
    -> launchBrouser: Should turn to false
    -> launchUrl: can run http://localhost:5202/swagger/index.html like Postman
10. If you cannot run https, use the command: dotnet dev-certs https --trust
11. There are 2 file: appsettings.json and appsettings.Development.json to configure the application at runtime
    - In appsettings.Development.json: change Microsoft.AspNetCore to "Information" to get more information int he terminal about what's happening with our application as it is running
12. In API.csproj:
    - TargetFramework: the .Net project
    - Nullable: Responsible for string behavior, we can disable of enable it.
    - ImplicitUsings: Remove some of the boilerplate inside our code. Should be enable.
13. Entity Framework:
    - What is it?
        + An Object Relational Mapper (ORM)
        + Translate our code into SQL commands that update our tables in the database
        + DbContext is a class acting as a bridge between our domain or entity classes and the database
    - Features:
        + Querying
        + Change Tracking
        + Saving
        + Concurrency
        + Transactions
        + Caching
        + Built-in conventions
        + Configurations
        + Migrations
14. Add Entity Framework:
    - In VS Code, add extension NuGet Gallery:
        + Press Ctrl + P, type >NuGet: Open NuGet Gallery
        + Install Microsoft.EntityFrameworkCore.Sqlite with the version fix the .Net version and
        + Install Microsoft.EntityFrameworkCore.Design to use the code first approach to creating our database
    - Recheck in API.csproj, there is a new ItemGroup displaying EntityFrameworkCore.Design and Sqlite
15. Next step is creating DbContext.
16. Primary key of an entity
    - If we have a property name Id inside an entity classes, then entity framework will automatically use that as the primary key of our database
    - If we don't, then we have to add [Key] before the property
17. Creating the connection string
    - Appsetting.json is always read
    - Appsettings.Development.json is read in development environment -> We should change this file
    - Add those line below with datingapp.db is the name of the database file:
        {
            "Logging": {
                ...
                }
            },
            "ConnectionStrings": {
                "DefaultConnection": "Data source=datingapp.db"
            }
        }
    - Search for dotnet-ef in the browser or go to the link: https://www.nuget.org/packages/dotnet-ef/#readme-body-tab and run the command line code to install the tool.
    - Run the command line: dotnet ef migrations add InitialCreate -o Data/Migrations
        + This will add a new migrations name InitialCreate and output/stored in Data/Migrations.
        + Run: dotnet build
            $ Check if there is any error.
18. Create the database using Entity Framework Code first migrations.
    - To create the database, run the command: dotnet ef database update.
    - Open a SQLite database:
        + Install extension to read SQLite database.
        + Use Shift+Ctrl+P -> choose Open SQLite database -> Choose the database file in the project.
        + Under SQLite Explorer, we can examine the database.
    - Insert some data to the table:
        + Right click on the table, choose New Query [Insert].
19. Add a new API Controller:
    - When adding [Route("api/[controller]")], if the name of Controller is User Controller, the path should be api/user (omit the word "controller").
    - In Postman, turn of the SSL certificate in Settings if it does not allow you to use https.
20. Make our code Asynchronous
    - Add "async" word to the method
    - Add Task<...> to the return type of method
    - Add "await" to the inside method
    - Change the inside method asSync
21. Source control
    - Create a gitignore file: dotnet new gitignore, add appsettings.json to the file
    - Create globaljson file: dotnet new globaljson
        + This file tells us which version of the SDK we're going to use for this particular project
22. Create the Angular app
    - Install Angular CLI: https://angular.io/guide/setup-local
    - Check Angular version: ng version
        + If the system error appears, run set-ExecutionPolicy RemoteSigned -Scope CurrentUser
    - Create the client side: ng new client
        + Angular routing: Yes
        + Stylesheet format: CSS
23. Create HTTP requests in Angular
    - In app.module.ts: 
        + Import Http module: import { HttpClientModule } from '@angular/common/http';
        + Add HttpClientModule to @NgModule->Imports
    - Implement OnInit interface in app.component.ts
    - When running our webpage, there is an error in the Inspection by CORS policy.
24. Adding CORS support in the API
    - The error above because the browser security feature prevents our browser fro downloading nasty data from a server that is different origin.
    - Our API server is responsible for sending out CORS headers when a client web applicaiton wants to make an API request.
    - In Program.cs, add the following line of code: 
        builder.Services.AddCors();
        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
25. Display the fetched users in the browser
    - To create a loop and get users
        + Use *ngFor="let user of users" in the ul
            $ Ex: <li *ngFor="let user of users">
                    {{ user.id }} - {{ user.userName}}
                  </li>
26. Adding bootstrap and font-awesome:
    - Go to the site og ngx-bootstrap to get the information: https://valor-software.com/ngx-bootstrap/#/documentation
    - Installation:
        + Go to client folder.
        + Run the command: ng add ngx-bootstrap
    - Install the font:
        + Run: npm install font-awesome
        + Go to angular.json and add to "styles":
            $ "./node_modules/font-awesome/css/font-awesome.min.css",
27. Adding HTTPS to Angular using mkcert
    - Install Chocolatey by cmd: https://docs.chocolatey.org/en-us/choco/setup#install-with-cmd.exe
    - Run the command in our project: choco install mkcert
    - In PowerShell with admin right:
        + Go to our project "client", create new folder ssl
        + In folder ssl, create new local CA: mkcert -install
        + Create new certificate with the command: mkcert localhost
            $ New certificate will be created tiwh the path, name, and expired date
    - Go to angular.json, we have to configure ssl in serve when running by adding new options to serve section:
        "serve": {
          "options": {
            "ssl": true,
            "sslCert": "./ssl/localhost.pem",
            "sslKey": "./ssl/localhost-key.pem"
          },
    - When running the client side, an error will appear indicate CORS issue. To fix the problem:
        - In Program.cs, change the http to https in the following line of code:
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
28. Using HTTPS in angular:
    - OS X
        1. Double click on the certificate (server.crt)
        2. Select your desired keychain (login should suffice)
        3. Add the certificate
        4. Open Keychain Access if it isn’t already open
        5. Select the keychain you chose earlier
        6. You should see the certificate localhost
        7. Double click on the certificate
        8. Expand Trust
        9. Select the option Always Trust in When using this certificate
        10. Close the certificate window
        The certificate is now installed.

    - Windows 10
        1. Double click on the certificate (server.crt)
        2. Click on the button “Install Certificate …”
        3. Select whether you want to store it on user level or on machine level
        4. Click “Next”
        5. Select “Place all certificates in the following store”
        6. Click “Browse”
        7. Select “Trusted Root Certification Authorities”
        8. Click “Ok”
        9. Click “Next”
        10. Click “Finish”
        If you get a prompt, click “Yes”
        + Copy server certificate and server.key to ssl folder.
        + We can change the following line in angular.json
            "serve": {
                "options": {
                    "ssl": true,
                    "sslCert": "./ssl/server.crt",
                    "sslKey": "./ssl/server.key"
                },...}
33. Introduction to Authentication basics
    - Where to start a project? - Requirements
        + Users should be able to log in
        + Users should be able to register
        + Users should be able to view other users
        + Users should be able to privately message other users
34. Safe storage of passwords
    - Hashing the password which is one way only
        + Drawbacks:
            $ Same passwords applied the same algorithm will get the same result
            $ If the database gets compromised, the attacker will know 2 people have the same password.
    - Hashing and salting the password
        + A salt applies to a hashing algorithm will scramble the hash
        + The password salt is another randomized string that we pass into the computed hash that then scambles.
35. Updating the user entity
    - Disable the Hot Reload:
        + Adding: "hotReloadEnabled": false to lauchSetting.json cannot make the Hot Reload disable.
        + The solution is use: dotnet watch --no-hot-reload
    - Any time we add new properties to an entity, we need to tell our database that it needs to create a couple of additional columns to accommodate these properties => We have to create a new migration
        + Run the command: dotnet ef migrations add UserPasswordAdded
        + We don't need to specify the output directory if we already have a previous migration created in the place that we want it.
        + Update the database: dotnet ef database update
36. Creating a base API controller
    - Create a BaseApiController with the following code:
        [ApiController]
        [Route("api/[controller]")]
        public class BaseApiController: ControllerBase
        {
            
        }
    - Now we can omit those line in other controller and replace ControllerBase with BaseApiController
        [ApiController]
        [Route("api/[controller]")]
37. Creating an Account Controller with a register endpoint
    - To Hash the password, we use hmac: using var hmac = new HMACSHA512();
    - Create a new user with parameter in a method:
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            //Initialize a new instance of HMACSHA512 class with a randomly generated key used as password salt
            //By using "using" keyword, when we finish with this class, a dispose method will be called and the class will be removed from memory
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = username,
                //Hash the password
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
38. Using the debugger
    - If your explorer does not have .vscode and their file, Go and look for >.NET:Generate Assets for Build and Debug
    - Go to launch.json
    - In configurations, there is how to lanch the debugger
        + One is called "lanch" with the name of ".NET Core Launch (web)"
        + Another is called "attach" which will allow the debugger to an already running process
    - Run the debugger as usual.
39. Using DTOs (Data transfer objects)
    - is an object that use to encapsulate some data and send it from one subsystem of an application to another.
    - Previous problem:
        + [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
          public async Task<ActionResult<AppUser>> Register(string username, string password)
        + When running on Postman, the app cannot translate json object from Postman to usernam and password of this function.
    - Solution:
        + Create DTOs folder in API, and RegisterDto.cs object with usernam and password properties inside.
        + Change the method like below:
            [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
            public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
40. Adding validation
    - At first, turn off Nullable flag in API.csproj => <Nullable>disable</Nullable>
        + If this is enabled, this automatically makes string properties required => That will feed into our database migration as well
    - At database level, in Entity classes in username property, we will add: [Required]
        + Update the database schema: dotnet ef migrations add UsernameRequired
        + If we want to remove the migration: dotnet ef migrations remove //To remove the most recent migration
    - At DTO level, in RegisterDto.cs, add [Required] to the fields you want non-nullable
    - Addition: 
        + If deleting [ApiController] in BaseApiController.cs, to run our application, ưe have to add [FromBody] to the code in AccountController.cs like 
            public async Task<ActionResult<AppUser>> Register([FromBody]RegisterDto registerDto)
            $ The ApiController will no longer take care of the validation in DTO level, we have to do it ourselves
41. Adding a login endpoint
    - Create a login function
    - Get the user from the database:
        + Use Find if there is a primary key when you use to look for
        + Use FirstOrDefaultAsync => return the 1st element of a sequence or defaul value (Null) if we cannot find the user
        + Use SingleOrDefault => return only element of a sequence or a default value if the sequence is empty. If more than 1 user have the same name, it will throw an exception.
    - To compare the password hash, we have to get the hmac with the password Salt from the user in database
        + using var hmac = new HMACSHA512(user.PasswordSalt);
    - Hash the login user's password with the hmac
    - Compare each word in the password.
        + We can use for loop to commpare the array or
        + We can use this function 
            var check = computedHash.SequenceEqual(user.PasswordHash); 
            if(!check) return Unauthorized("Invalid Password");
42. JSON web tokens
    - Self-contained and can contain:
        + Credentials
        + Claims
        + Other information
    - It has 3 parts, each part is separated by a period.
        + The header of the token
            $ Contains the algorithms which was used to encrypt the signature in the third part of the token
            $ Contains the type of token
        + The payload
            $ Contains the information about our claims and our credentials
            $ Contains 3 time stamps:
                _ nbf: The token cannot be used before a certain date and time
                _ exp: The expiry date in time for the token
                _ iat: when the token was issued at 
        + The verify signature - Where the signature is contained and this signature is encrypted
            $ All the token signature is encrypted by the server itself using a secure key that never ever leaves the server
            $ The only part that is encrypted
    - Benefits
        + No session to manage - JWTs are self containted tokens
        + Portable - A single token can be used with multiple backends
        + No cookies required - mobile friendly
        + Performance - Once a token is issued, there is no need to make a database request to verify a users authentication
43. Adding a token service
    - We will create our own services and add it to our services contrianer so that we can inject our own service into the controller or account controller so that when a user login, we can issue them with a token that we get from this service that we're about to create
    - When creating a service, we will
        + Create an interface which tells the implementation of the service what methods it supports.
    - Steps:
        + Create an interface with an method to CreateToken with a user object
        + Create a class TokenService to implement the interface
        + Add service in the Programm.cs with 3 options:
            $ AddTransient: very short lived service => The token service will be creted and disposed of within the request as soon as it's used and finished
            $ AddScoped: After a request hit the endpoint in a controller, the framework instantiates a new instance of that controller. The controller looks at its dependencies or the framework does to create new instances of these services when the controllers created. When the controllers disposed of at the end of the HTTP request, then any dependent services are also disposed.
            $ AddSingleton: The application first starts and is never disposed until the application has closed down. The service will hange around in memory. The case to use this one is if you are using a caching service and you want every single request that comes into your API, you want to save the respoinse form that request int oyour cache so the next request from somebody else could go and take a look in that chache and retrieve the cached data => improving the performance of the application.
            => We mostly use AddScoped
        + Use: builder.Services.AddScoped<ITokenService, TokenService>();
44. Adding the create token logic
    - Open up the NuGet package manager or NuGet Gallery
    - Search for and install System.IdentityModel.Tokens.Jwt
    - Steps:
        + Add a SymetricSecurityKey variable: private readonly SymmetricSecurityKey _key;
            $ A symmetric key: The same key is used to encrypt the data and decrypt the data. Our system, the server will encrypt and decrypt the token
            $ A asymmetric: when the server needs to encrypt something and the client needs to decrypt something => we have a public and a private key. That is how it works with HTTPS and SSL.
        + Create constructor for the TokenServices class with IConfiguration parameter
            $ The config would be a json type with pairs of key and value.
            public TokenService(IConfiguration config)
            {
                _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            }
        + In the CreateToken method:
            $ Create a list of Claim:
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
                };
            $ Create signing credential with the key and SecurityAlgorithms
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            $ Create Token Descriptor to describe what our token is going to include
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = creds
                };
            $ Create a token handler:
                var tokenHandler = new JwtSecurityTokenHandler();
            $ Create the token:
                var token = tokenHandler.CreateToken(tokenDescriptor);
45. Creating a User DTO and returning the token
    - We will manage to return the token when the user login or register
    - Currently we return the AppUser which contains PasswordHash
    - So we will create a UserDto to use:
    - Steps:
        + Create UserDto.cs, contain Username and Token as the properties
        + In AccountController.cs constructor, we need to specify the ITokenService interface to use to create token logic.
        + In Register and Login function, we will replace AppUser to UserToken
        + In return of each function, change to the following:
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        + In appsettings.Development.json, add the field TokenKey to fit with the field in TokenService.cs
            "TokenKey": "super secret ungusesable keywhat the hell super secret ungusesable keywhat the hell"
        Note: This token must be more than 512 bytes or you will have to deal with error when create the token.
        + To examine the token return in Postman
            $ Go to: jwt.ms and past the token to see the information inside it.
46. Adding the authentication middleware
    - Ensure that only authorized users can access a specific endpoint, we can specify that we want to authourized to it
        + We should add the attribute to HTTP Get in UserController.cs
            [Authorize]
        + In higher level, we can add the attribute to the begining of the class in UserController.cs so that all of the endpoints inside this controller, we want to authorize every request
            $ If you decide to allow GetUser to be used by anonymous user, we can add attribute to this HTTP method [AllowAnonymous]
        + We cannot use [AllowAnonymous] in the highest level and override it with [Authorize] in the sub request because it will be ignored.
    - We need to tell our server how to authenticate which will be set up in Program.cs
        + Go to NuGet Gallery, search for "Microsoft.AspNetCore.Authentication.JwtBearer" and install it
        + Add the line: builder.Services.AddAuthentication([This is the type of authentication scheme that we're going to be using inside this parameter])
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        + Then we add JWT Bearer and add some option for this like below:
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option => {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        //We will want to validate the Issuer Signing Key
                        //Our servier will check the token signing key and make sure it is valid
                        ValidateIssuerSigningKey = true,
                        //Specify what our issuer signing key is
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                        .UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        //We did not pass the Validate Issuer and Audience so we set them false
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        + We also need to add the authentication middleware below the Cors and above the MapControllers
            //Add authentication middleware before the map controllers
            app.UseAuthentication(); //Ask if you have a valid token
            app.UseAuthorization(); //Ok you have the token, so what are you allowed to do?
    - In Postman, we have to add the header:
        + Key: Authorization
        + Value: Bearer {Paste the token you receive when get the user}
    - What if you want to trick the browser. 
        + Go to jwt.io, paste the token
        + Try to change the nameid field
        + Notice that the signature part also change
        => So that you cannot get the user login any more.
47. Adding extension methods
    - Extension methods allow us to extend the class in some way and that class can be one that we write or it can be the one that's provided by the framework
    - Create a new file ApplicationServiceExtensions.cs in folder Extensions
    - To create an extensions method, we need to make this class static => we can use the methods inside it without instantiating a new instance of this class
    - Create ApplicationServiceExtension.cs:
        + Make the class and the method static
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){}
        + Copy all the services from Program.cs (AddDbContext. AddCors, AddScoped)
        + Change the methods to make them work
        + return the services.
        + In Programm.cs, add the following line: 
        builder.Services.AddApplicationServices(builder.Configuration);
    - Create IdentityServiceExtensions
        + Make the class and the method static
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config){}
        + Copy the services from Program.cs (AddAuthentication)
        + Change the methods to make them work
        + return the services.
        + In Programm.cs, add the following line: 
        builder.Services.AddIdentityServices(builder.Configuration);
49. Client Login and Register:
    - Creating components using the Angular CLI
    - Using Angular Template forms
    - Using Angular services
    - Understanding Observables
    - Using Angular structural directives to conditionally display elements on a page
    - Component communication fromparent to child
    - Component coommunication from child to parent
50. Creating a nav bar
    - On client side: 
        + ng g --help (Look at generate method help document)
        + ng g c nav --dry-run (Create nav component in dry run options which means no changes were made)
            $ Create nav.component.css
            $ Create nav.component.html
            $ Create nav.component.spec.ts - use for testing
            $ Create nav.component.ts
            $ Update app.module.ts
        + ng g c nav --skip-tests (Create nav component in skip tests option which means no changes were made and skip the test file)
    - After running the cmd, in app.module.ts, NavComponent is added
    - Copy nav bar from Carousel template in https://getbootstrap.com/docs/5.3/examples/carousel/# and paste to nav.component.html
    - Explore the nav bar:
        + Explain <div class="container-fluid">: Links and search bar are stretched to the edge of the display
        + Explain class="navbar-toggler": Collapse our nav bar to toggler
51. Introduction to Angular template forms
    - Should read: https://www.geeksforgeeks.org/whats-the-difference-between-an-angular-component-and-module/
    - Turn Submit form into an angular form
        + Give it the template reference variable: #loginForm="ngForm" - loginForm is provided by NgForm
        + The ng-submit directive specifies a function to run when the form is submitted.
        + Set autoComplete off
        <form #loginForm="ngForm" class="d-flex" (ngSubmit)="login()" autocomplete="off">
        + For input, we give the name: name="username"
        + Two way binding to the component and angular form, we need to use square brackets followed by parentheses and then specify the object name with its property
        <input name="username" [(ngModel)]="model.username" class="form-control me-2" type="text" placeholder="Username">
    - At the end of this step, we can take the information from the form.
52. Introduction to Angular services
    - We will make this request to send this data up to our API so we can actually log in.
    - Create new folder inside src/app/_services
    - Create a service: ng g s folder/account --skip-tests
        + It will create the file call account.service.ts
        + We have a decorator called Injectable and Angular services can be injected into our components or into other services
            $ Metadata called providedIn:'root': will automatically add this new service to the providers array in app.module.ts
    - When we create a component, each time that component is destroyed, anything stored inside will be also destroyed, while the services will live for the liftime of our application
    - The file account.service.ts:
        + Make the HTTP request from out client to our server.
        + We use the service to centralize our HTTP requests
        + In Angular, when the service is singleton which is provided in the root module (in app module), our app and the services will be initialized at the same time. The service will not be destroyed until our application is finished or the user is finished with our application.
        + In constructor, we have to inject HTTPClient as a parameter
        + Create a function login and return a post http to send the data from the form to the server. 
            $ The HTTP POST will return an Observable of the response, with the response body as an object parsed from JSON
    - The nav.component.ts:
        + Inject AccountService to the constructor
        + In login function, we will use the login function injected from the accountService.
        + Because we return an observable from the service, we need to subscribe to this to get back the observed object
            $ next: what do we want to do next with this observable
            $ error: Take the error we get back from the server
54. Using conditionals to show and remove the content
    - File nav.components.ts: add the function loggout to set the loggedIn to false
    - We will hide the context in nav bar. Actually, we will remove the content from the DOM, the domain object model => Go to nav.component.html
        + We will use Angular structural directive.
        + A structural directive always begins with an asterix => *ngIf
        + Then we can use ngIf to decide whether or not to display this content, inside the double quote, we use the name of boolean variable in nav.component.ts
        + If we don't want to remove the content from the DOM, we can use [hidden]
55. Using Angular bootstrap components - Dropdown
    - Explore the Dropdown bootstrap at: https://valor-software.com/ngx-bootstrap/#/components/dropdowns?tab=api
    - Install the API: ng add ngx-bootstrap --component dropdowns
        + UPDATE package.json (1125 bytes)
        + UPDATE angular.json (3068 bytes)
        + UPDATE src/app/app.module.ts (805 bytes) => Add BsDropdownModule.forRoot()
    - In Bootstrap Overview site -> Notify and go back to nav.component.html.
    - In nav.component.html:
        + In class "dropdown", add the word "dropdown"
        + In class "dropdown-toggle", add the word "dropdownToggle"
        + In the class "dropdown-menu", add the words "*dropdownMenu"
        + Delete the Logout button on the nav bar.
    - In nav.component.css: Change the mount pointer to pointer stype
        + Add the following code: 
            .dropdown-toggle, .dropdown-item{
                cursor: pointer;
            }
56. Introduction to observables
    - What are Observables?
        + New standard for managing async data included included in ES7 (ES2016)
        + Introduced in Angular v2
        + Observables are lazy collections of multiple values over time.
        + Like a newsletter
            $ Only subscribers of the newsletter receive the newsletter
            $ If no-one subscribes to the newsletter it probably will not be printed
    - Promises vs Observables
        + Promises:  
            $ Provide a single value in the future
            $ Not lazy
            $ Cannot cancel
        + Observable:
            $ Emits multiple values over time
            $ Lazy
            $ Able to cancel
            $ Can use with map, filter, reduce and other operators
    - Observable and angular:
        + An async method will return an observable
            $ Example: getMembers(){ return this.http.get('api/users') }
        + We need to subscribe to the service.
    - Observables and RxJS (Reactive extension for javascript):
        + We can transform data before we pass it to the subscriber, that is when RxJS comes in.
        + We will add a pipe method to the request/observable
            $ Example: getMembers(){ return this.http.get('api/users').pipe( map (members => { Console.log(members.id); return members.id}))}
    - Getting data from observables
        + Subscribe
        + After we subscribe, we have to add what we want to do next - next function; what if there is an error - error function; What to do when complete - complete function.
        + Not automatically unsubscribes the service.
            $ Example: getMembers(){ this.service.getMembers().subscribe(members => {this.members = members}, error => {console.log(error)}, () => {console.log('completed')}) }
        + ToPromise()
        + Another way to get the data from observables without subscribing to them is we can send them to a normal JavaScript promise
            $ Example: getMembers(){ return this.http.get('api/users').toPromise()}
    - Async pipe
        <li *ngFor='let member of service.getMembers() | async'>{{member.username}}</>
        + Explaination: We got an ngFor and memebr of service, then we pipe this into the async pipe. Then we've got access to the properties inside member using the async pipe
        + This will automatically subscribes and unsubscribes from the Observables.
57. Persisting the login
    - When using components, they dont remember things for very long. So we will use service to persist out login until the user close the browser.
    - In account.service.ts, login function:
        + Modify the response from HTTP POST, map response as any, if user exists, store user as item in localStorage so that we can access it anywhere from our application
    - Inside app folder, create _models/user.ts:
        + Export interface called User with username as string and token as string.
    - Back to account.service.ts, login function:
        + Set response to User type => There will be a complaint displayed
        + We have to specify the type that HTTP POST return which is User DTO => ...post<User>(...)
    - Currently, only component know if we logged in is the NAV component. But we need that information elsewhere inother components as well => We will create an observable inside our account service so that the other components can use the account service to ascertain whether or not a user is logged in.
    - We will use a special kind of observable called a behavior subject which allow us to give an observable an initial value that we can then use elsewhere in our app.
        + Create a private filed type BehaviorSubject
            $ private \fieldName\ = new BehaviorSubject<User | null>(null)
        + Create an observable object:
            $ current$ = this.\fieldName\.asObservable()
            $ The '$' sign presents that this is an observable
    - If we log in successfully, we have to update the current user source/behavior subject by adding the user to the next of BehaviorSubject
        + Example: this.currentUserSource.next(user)
    - In account.service.ts, logout function, we also update the currentUserSouce to null
    - In account.service.ts, we also create a convenient function setCurrentUser with parameter is user and set the curentUserSource to this user.
        + setCurrentUser(user: User){this.currentUserSource.next(user);}
    - In app.component.ts, 
        + In constructor, add AccountService as a parameter.
        + Add a method to set the current user
            $ We can use JSON.parse(localStorage.getItem('user')!)
            $ Or we can check if there is any userString we can get from localStorage.
            $ Then set the current User to this user
        + In ngOnInit function, add setCurrentUser function to this method.
    - In nav.component.ts:
        + In logout function, use accountService logout function.
        + Add getCurrentUser function, we use accountService to get currentUser$ - observable, then we need to subscribe.
        + In subscribe, we will set the next function with the loggedIn to !!user - !! will turn user to a boolean, if user exists, it true, else false. And we set the error function for subscribe.
        + In ngOnInit, add getCurrentUser to it.
    => So in every component, we have to create getCurrentUser in ngOnInit.
58. Using the async pipe
    - In HTTP request, it's not necessary to unsubscribe because it will automatically do that.
    - In component, it is good to unsubscribe the service
    - But we have a better way that is use the async pipe in our template. It will automatically subscribe and unsubscribe for us.
    - In nav.component.ts:
        + Delete loggedIn property.
        + Add currentUser$ as observable User
            $ When we have Angular in strict mode, we have to initialize class properties, if not we will get a warning
            $ In observable type, we cannot just say it null. We need to use an operator which in this case, we use an Observable Of something - of(null)
        + In ngOnInit(), replace this.getCurrentUser() by this.currentUser$ and get user directly from accountService.
        + In getCurrentUser(): We dont need this method anymore.
    - In nav.component.html:
        + Class "navbar-nav me-auto mb-2 mb-md-0", replace loggedIn by currentUser$ | async
            $ It means: if this has something that's not null, then we know we are logged in and we'll do the same for the class "dropdown" and "d=flex"
    - ANOTHER WAY TO SIMPLIFY:
        + We can remove the current user observable in nav.component.ts (the property currentUser$ and the line inside ngOnInit method)
        + In nav.component.ts: in constructor, change "private accoutnService" to "public accountService". Because when we use private modifier inside our component, that means we can only use the account service inside the component itself, not inside a template
        + In nav.component.html: change currentUser$ to accountService.currentUser$
59. Adding a home page
    - Run the command: ng g c home --skip-tests
    - In home.component.html, add html code
    - In home.component.ts, add registerMode property and registerToggle method.
    - In app.component.html, replace all previous code with <app-home></app-home>
60. Adding a register form
    - Run the command: ng g c register --skip-tests
    - We will use a template form initially for the register component called model
    - In register.component.ts:
        $ Add register method and cancel method
    - In register.component.html:
        $ Add form.
61. Parent to child communication
    - At home component, we can see register component is a child of home component or home component is parent of regisster component.
    - Now we will pass information from home component - the parent - to the registered component - the child.
        + In app.component.ts: cut the get users method and paste it into our home.component.ts
        + In home.component.ts: 
            $ Paste the getUsers method from app.component.ts.
            $ In contructor, add parameter http from HttpClient
            $ We need a property to store users to get from the API in any type.
            $ In ngOnInit, add this.getUsers method.
    - We need to add something to our register component to get some information from a parent component => We need to use an input decorator which we get from angular core.
    - In register.component.ts:
        + Add @Input() #name: any;
    - In home.component.html, app-register:
        + Add [#name]="users"
        + Now we're going to have access to this inside the register component as well
    - In register.component.html:
        + Add a div below Username
62. Child to parent communication
    - When we click on cancel button, we fire off an event that goes up to the home component. Which then causes the register mode flag inside the home component to change to false, which will then effectively close our register form.
    - In register.component.ts:
        + Add @Output() #name: new EventEmitter();
        + Change cancel() method: this.cencelRegister.emit("we can add anything")
        => In this case, we add false because we want to turn off the register mode in the home components.
        + Go to parent component
    - In home.component.ts
        + Add a new method called cancelRegisterMode()
63. Hooking up the register method to the service
    - We'll look at actually registering our users to our API server.
    - In account.service.ts: Add register(model: any) method.
    - We don't need to pass our users down from the home component. So we will clean the @Input we did in section 61 in register.component.ts and register.component.html and home.component.html ([usersFromHomeComponent]="users").
    - In RegisterComponent of register.component.ts:
        + Modify constructor: Add parameter to constructor as acountService
        + Modify register method: Add code to call register method of accountService
    - Try to test the register in the client side
        + In the network tab when inspection, there are 2 register requests.
        + When we make an HTTP request to an API server, there's two requests really once a pre-flight request to see what headers it supports and where the cause is enabled.
        + In console tab: There is an undefined object in line 21 of register.component.ts.
            $ If we go back to our account service, we can see in our register method we're using the return keyword, which means we're returning from this register method. But we didn't get the response. 
            $ We do have to return from this because our component needs to know if the request has been completed.
            $ But when we used MAP and we project as we are doing in this case, then if we want to return the response.
            $ To get the retun, in account.service.ts, register method, add "return user".
