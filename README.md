# HotelApp
Hotel Application provides system to manage your hotel.

### Functionalities:
- Manage hotel's rooms
- Check in a guest		
- Check out a guest


### Features in progress:
- Admin can manage users (their role, access)
- Guest is able to book a room
<br /><br/>

## Login Page
If user tries to access protected page, a redirection to Login Page on OpenIddictServer will occur. It is possible to log in using either google or locally created account. After successful login user is redirected back to the requested page.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147706246-ce55f1fc-9f5a-4007-b7e7-5962ee1a9f33.png)
<br /><br/>

## Home Page (better front-end in development)
There are 4 options (functionalities): 
- Privacy: Used for development purpose (testing API)
- Rooms: Rooms Management Page
- Check In: Form to check in a guest
- Check Out Form to check out a guest
- Sign out: User is logged out
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147707073-170fff1f-4ac7-4c3a-9413-13f19e6beb7c.png)
<br /><br/>

## Rooms Page
Displays all rooms and information about each one, such as: Id, Name, NormalPrice, StudentPrice, Capacity, Status.
If user is not an admin, options: Edit, Details (not available yet), Delete, Create New will not be displayed.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147707152-100bbe0d-5711-4268-ad8f-0b19fdba08ed.png)
<br /><br/>

## Create New Room Page
Admin can create a new room through filling a form.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147707551-d3059270-a470-4f21-ba2e-b82cb6e6df53.png)
<br /><br/>

## Edit Room Page
Admin can edit selected room's properties.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147737385-8fe2910a-b52d-4a06-afe3-7038c1a278a2.png)
<br /><br/>

## Delete Room Page
Admin can delete selected room.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147737463-cfabb8f0-adfe-4335-beee-730bc23d5b15.png)
<br /><br/>

## Check In Page
User with role Cashier or Admin can check in a guest. Available room has to be selected. After clicking Rooms button under RoomId input box, Room Page will be opened in a new tab.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147737655-5a2b5fa1-5dbc-4b4a-a709-31db6db900dd.png)
<br /><br/>

## Check Out Page
User with role Cashier or Admin can check out a guest.
<br /><br/>
![image](https://user-images.githubusercontent.com/33805319/147738165-1c9b8063-50ce-4ed3-a3d7-9d665fb2006d.png)
<br /><br/>

## Used Technologies
- ASP .NET Core 5.0
- Web API
- ASP .NET CORE MVC
- OpenIddict (OpenID Connect server)
- Google authentication
- Authorization Code Flow
- Entity Framework 
- xUnit, Swagger
- Github
