# 💶 Expense Tracker
A web application to track personal expenses and incomes for better insight. Expenses can be categorized into user-created cateogories, and also tags can be created to mark expenses. In addition to manual expense tracking, where an expense can be created by filling out a form, it is also possible to use camera to load purchase receipt. The user loads the QR code of the receipt via the camera and the form for the expense is automatically pre-filled. Templates can be created for companies that issue receipts where the category and tags of the expense can be predefined. The user can also keep track of their receipts in the application and can view the reports in the form of charts.

The application was developed as a semester work on the Advanced Object Technologies course at the Faculty of Management Science and Informatics of the University of Žilina.

## 📊 Application functions
- 📈 Overview of expenses and incomes in the form of charts
- 💶 Recording of user's expenses
- 🛍️ Categorization of expenses
- 🏷️ Tags for marking expenses
- 🧾 Loading a receipt for automatic pre-filling of the expense form
- 📑 Manage templates for expenses
- 💰 Recording of user's incomes
- 💸 Categorization of incomes

## 🖥️ Technical details
The application is developed in ASP.NET and Blazor technologies. WebAPI is built for the front-end, which is implemented using Blazor. We communicate with the database through EF Core. The business logic of the application is modeled through use-cases by the MediatR library. Additionally, we use the ReactorBlazorQRCodeScanner and Blazor.Bootstrap libraries.

## ⚙️ Starting the application
Before running the application, you need to set up the database connection in the configuration files, and also set the URL and the specific endpoint of the external API to get the receipt information.

<br>
<br>



# 💶 Expense Tracker
Webová aplikácia na sledovanie osobných výdavkov a príjmov za účelom lepšieho prehľadu. Výdavky je možné kategorizovať do uživateľom vytvorených kateógrií a navyše je možné vytvoriť tagy pre označenie výdavkov. Okrem manuálnej evidencie výdavkov, kedy je možné výdavok vytvoriť vyplnení formuláru, je tiež možné využiť funkciu načítania Bločku. Používateľ cez kameru načíta QR kód bločku a formulár pre výdavok sa automaticky predvyplní. Pre spoločnosti, ktoré vydávajú bločky je možné vytvoriť šablóny, kde je možné preddefinovať kategóriu a tagy výdavku. V aplikácií si používateľ môže evidovať aj svoje príjmy a prehľady si vie zobraziť v podobe grafov.

Aplikácia vznikla v rámci predmetu Pokročilé objektové technológie ako semestrálna práca na Fakulte riadenia a informatiky Žilinskej univerzity v Žiline.

## 📊 Funkcie aplikácie
- 📈 Prehľad výdavkov a príjmov v podobe grafov
- 💶 Evidencia používateľových výdavkov
- 🛍 Kategorizácia výdavkov
- 🏷 Tagy pre označenie výdavkov
- 🧾 Načítanie bločku pre automatické predvyplnenie formulára výdavku
- 📑 Správa šablón pre výdavky
- 💰 Evidencia používateľových príjmov
- 💸 Kategorizácia príjmov

## 🖥 Technické detaily
Aplikácia je vyvíjaná v technológiach ASP.NET a Blazor. Pre front-end, ktorý je realizovaný cez Blazor je vybudované WebAPI. S databázou komunikujeme cez EF Core. Biznis logiku aplikácie modelujeme cez prípady použitia (use-cases) knižnicou MediatR. Navyše používame knižnice ReactorBlazorQRCodeScanner a Blazor.Bootstrap.

## ⚙️ Spustenie aplikácie
Pred spustením aplikácie je potrebné v konfiguračných súboroch nastaviť pripojenie na databázu, a tiež nastaviť URL a konkrétny endpoint externého API pre získanie informácií o bločku.

