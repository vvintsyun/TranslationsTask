This project was generated with Angular CLI version 12.0.2.

To setup environment:
1) Install SqlLocalDB (https://go.microsoft.com/fwlink/?LinkID=853017) and create localdb instance if you don't have any (use command: sqllocaldb create MSSQLLocalDB)
2) Create database Translations and move its files to <project folder>/Data (detach and attach required)
3) Apply the database migrations (dotnet ef database update)
4) Manually add Translator entities

 
To run the project open it with IDE and run TranslationTask project. Front-end and back-end sides will run automatically.
