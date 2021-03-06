- BidOneTest.Mvc (.NET 6.0)
	- Simple Asp.Net Core Web App (Model-View-Controller) project with a Model (ViewModel), Controller (TestController) and View (Test\Index.cshtml)
		- ViewModel
			- Easier to handle validations using annotations in a small project. For a large number of view models, validation is easier to handle using Fluent Validation.
		- TestController
			- Constructor
				- Injecting AppSettings (ApiRootUrl from appsettings.json) from Program.cs
			- HttpPost Index method uses HttpClient to post object as Json to WebApi
				- Validation on AntiForgery Token
		- Program.cs
			- Configure AppSettings from appsettings.json -> AppSettings section
- BidOneTest.Api (.NET 6.0)
	- Simple Asp.Net Core Web Api project with a Model (Model), Controller (DefaultController), Interface (IFileHandler) and Implementation (FileHandler)
		- Model
			- Easier to handle validations using annotations in a small project. Validations can be handled easily with Fluent Validation and Action Filter Attribute on large number of models.
		- DefaultController
			- Constructor
				- Injecting IFileHandler interface
			- HttpPost Index method accepting object from Body
				- returns BadRequest if model validation failed
				- return Ok on success
				- returns Problem with exception message if exception occurs
		- IFileHandler, FileHandler
			- Constructor
				- Injecting DirectoryPath
				- Creating directory
			- Write to file
				- Write all filecontent to file
		- Program.cs
			- Register IFileHandler interface and resolving it with FileHandler implementation class, with DirectoryPath constructor parameter
- BidOneTest.Tests (Microsoft.UnitTesting, MOQ)
	- DefaultControllerTests
		-	Unit test controller asserting status BadRequestObjectResult and validation error messages for first name, last name, and both
		-	Unit test controller asserting status code Ok (success)
	- FileHandlerTests
		-	Unit test interface/implementation verifying that the method is called once
		-	Unit test interface/implementation asserting file content after file writing
