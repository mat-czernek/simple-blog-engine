# Simple blog engine

Blog created in ASP.Net Core. Based on Entity and Identity frameworks. SQLite as a database provider.

###### Setting up project

Install all required dependencies by running this command:

```
dontnet restore
```

###### Setup defaults

Open customSettings.json file and configure default user data to seed database
(You will be asked to change default password after the initial login)

```
{
  "DefaultUserData": {
    "Firstname": "John",
    "Lastname": "Doe",
    "Email": "john.doe@mail.com",
    "Password": "P@ssword12345",
    "AboutAuthor": "Few words about the blog author",
    "GithubProfile": "https://github.com/",
    "LinkedinProfile": "https://linkedin.com/"
  },
  "DatabaseOptions": {
    "ConnectionString": "BlogDatabase.sdb"
  }
}
```


Run following command to setup the EF migrations and create database:

```
dotnet ef migrations add Initial
```
