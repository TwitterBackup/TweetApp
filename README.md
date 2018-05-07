TWITTER BACKUP
---------------------------------------------------------------------------------------
![](https://raw.githubusercontent.com/TwitterBackup/TweetApp/dev/Documentation/TwitterBackup_for_ReadMe.png)

**+++  Backup your favorite tweets and their tweeters in one click  +++**

![](https://raw.githubusercontent.com/TwitterBackup/TweetApp/dev/Documentation/TwitterBackup_AdminNavBar.png)
---------------------------------------------------------------------------------------
This is a web application based on the following technologies, frameworks and development techniques:

1.   **ASP.NET Core MVC** **2.0** with the following architecture structure:
![](https://raw.githubusercontent.com/TwitterBackup/TweetApp/dev/Documentation/TwitterBackup_Application_Architecture.png)
2.  **Razor** template engine for generating the UI
3.  **MSSQL Server** as database back-end
![](https://raw.githubusercontent.com/TwitterBackup/TweetApp/dev/Documentation/MSSQL_Diagram.png)
4.  **Entity  Framework  Core** with a  Code-First approach to access the database
5.  **Design patterns used**
	Repository pattern and Unit of Work (Generic Entity Framework implementation)
	Service layer pattern (and DTO)
	Inversion of Control (DI) principle with the ASP.NET Core 2.0 build-in container
6.  Separate Administrative "Area" (accessible for users with admin role) as well as Private and Public part
7.  **TWITTER REST API** with POST and GET Requests  
7.  Responsive (Mobile/ Desktop) design and mainly **Bootstrap** controls
8.  Customized  **ASP.NET Identity System** so that user can login with UserName or Email
9.  Multiple **AJAX forms** to preserve customer browsing experience
10.  **InMemory caching** of admin statistics data
11.  **Error handling** with modal popus 
12. **Data validation** with unobtrusive validation on both client-side and server-side
13.  **Security** holes (XSS, XSRF, Parameter Tampering, etc.) has been lead to minimum
14.  Handle correctly the **special HTML characters** and tags like <script>, <br />, etc.
15.  **GitHub** repository was used with feature-based **branches** 
16.  **Unit tests** with MSTest and MOQ  cover the business functionality

---------------------------------------------------------------------------------------
**In Addition this Application also comes equiped with:** 

A) **Documentation** of the project and project architecture (as .md file, including creenshots) under Documentation folder 

B) Setup   **CI/CD** with **Jenkins** on Kestrel behind a local IIS instance 

![](https://raw.githubusercontent.com/TwitterBackup/TweetApp/dev/Documentation/CI_workflow.png)

C) Functionality that provides **Login and register with a Twitter   Account /Social Login/ ** 

D) Deployment in the Cloud (Azure)


OUR FULLY DEDICATED **TEAM**
---------------------------------------------------------------------------------------

 - Angel - github: **aniget** 
 
 - Alex - github: **AlxxlA**

Thank you!
