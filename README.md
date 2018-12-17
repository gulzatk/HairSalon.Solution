# Hair Salon Webpage

#### C# Independent Project for Epicodus, 12/14/2018

### **By Gulzat Karimova**

## Description
A website that uses a database to show the user all about the stylists, specialties and stylist clients. The use will be add and remove specialties, stylists and clients.

## Specifications

1. The user is able to see all stylists who works in the salon.

2. The user is able to see all specialties in the salon.

3. The user can select one stylist and see all information, stylist specialty and list of clients belong to this stylist.

4. The user can add new stylists

5. The user can add a specialty to the stylist.

6. The use can add new clients for specific stylist.

7. As an employee I can delete all or single stylist.

8. As an employee I can delete all or single specialty.

9. As an employee I can delete all or single client.

7. As an employee I can edit client, stylist and specialty information.

## Setup/Installation Requirements

1. Clone this repository: https://github.com/gulzatk/HairSalon.Solution.git

2. Create a database needed for this application.
  * CREATE DATABASE gulzat_karimova;
  * USE gulzat_karimova;
  * CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), description VARCHAR(255));
  * CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylistId INT);
  * CREATE TABLE specialties (id serial PRIMARY KEY, name VARCHAR(255))
  * CREATE TABLE stylist-specialty (stylist_id serial PRIMARY KEY, specialty_id serial PRIMARY KEY)

3. Start your local server using MAMP.
4. Navigate to $ cd HairSalon.Solution/HairSalon folder in your terminal.
5. Type 'dotnet run' to run a localhost. Navigate to 'http://localhost:5000/' to interact with the website.

## Support and contact details

If you have any questions or suggestions please feel free to email me: gulzat.karimova@gmail.com

## Technologies Used
* C#
* .NET Core
* Atom
* MySql
* MAMP
* GitHub
* HTML
* CSS


## License
This software is licensed under the MIT license
Copyright (c) 2018 (Gulzat Karimova)
