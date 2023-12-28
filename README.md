# Game Store RESTful API

- API created with the aim of improving my skills in the C# language

###
## Documentation

### User:
**Returns all users**
- **Route:** `GET /api​/user`
- **Permission:** admin

**Returns a user by id**
- **Route:** `GET /api​/user​/{id}`
- **Permission:** user, admin

**Register a user**
- **Route:** `POST ​/api​/user`
- **Request body:** JSON -> role: (user or admin)
  ```json
  {
    "name": "string",
    "age": 0,
    "email": "user@example.com",
    "password": "string",
    "role": "user"
  }
  ```
  
**Update a user by id**
- **Route:** `PUT /api​/user​/{id}`
- **Permission:** admin
- **Request body:** JSON
  ```json
  {
    "name": "string",
    "age": 0,
    "email": "user@example.com",
    "password": "string",
    "role": "admin"
  }
  ```

**Delete a user by id**
- **Route:** `DELETE /api​/user​/{id}`
- **Permission:** admin

##
### Login:
**User logs in**
- **Route:** `POST ​/api/login`
- **Request body:** JSON
  ```json
  {
    "email": "user@example.com",
    "password": "string"
  }
  ```

##
### Game:
**Returns all games**
- **Route:** `GET /api​/game`
- **Permission:** admin

**Returns a game by id**
- **Route:** `GET /api​/game​/{id}`
- **Permission:** user, admin

**Register a game**
- **Route:** `POST ​/api​/game`
- **Permission:** admin
- **Request body:** JSON
  ```json
  {
    "name": "string",
    "genre": "string",
    "platform": "string",
    "description": "multiline string",
    "over18": true,
    "price": 0.00
  }
  ```
  
**Update a game by id**
- **Route:** `PUT /api​/game​/{id}`
- **Permission:** admin
- **Request body:** JSON
  ```json
  {
    "name": "string",
    "genre": "string",
    "platform": "string",
    "description": "multiline string",
    "over18": true,
    "price": 0.00
  }
  ```

**Delete a game by id**
- **Route:** `DELETE /api​/game​/{id}`
- **Permission:** admin

##
### Add Balance:
**Add balance to the customer's wallet using their id**
- **Route:** `POST /api​/addbalance​/user​/{id}​/value​/{double}`
- **Permission:** user
- **Request body:** nothing

##
### Buy Game:
**Buy a game with the user id and the game id**
- **Route:** `POST ​/api​/buygame​/user​/{id}​/game​/{id}`
- **Permission:** user
- **Request body:** nothing

##
### Available Games:
**Returns the games available to the user using their id**
- **Route:** `GET /api​/availablegames​/user​/{id}`
- **Permission:** user, admin

##
### Purchased Games:
**Returns the user's purchased games using their id**
- **Route:** `GET /api​/purchasedgames​/user​/{id}`
- **Permission:** user, admin

##
Obs: All routes -except the login and user registration routes- require authentication with the token returned at login.

# Technologies Used
<div style="display: inline_block">
  <img align="center" alt="C#" height="32" width="32" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg">
  <img align="center" alt="MySQL" height="48" width="46" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/mysql/mysql-original-wordmark.svg">
</div>
