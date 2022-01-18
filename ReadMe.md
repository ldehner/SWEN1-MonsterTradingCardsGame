Linus Dehner

# Monster Trading Cards Game

## Routes

All routes are private except `register` and `login`. The private routes need authentification in form of a token with prefix "Basic", which is returned by a successful `login`.

```bash
--header "Authorization: Basic yourToken"
```



### User

#### Register `POST`

```bash
/users
```

Body:

```json
{
    "Username": "...",
    "Password": "..."
}
```



#### Login `POST`

```
/sessions
```

Body:

```json
{
    "Username": "...",
    "Password": "..."
}
```



#### Logout `POST`

```
/logout
```



#### Get User Data `GET`

```bash
/users/{user}
```



#### Update User Data `PUT`

```bash
/users/{user}
```

Body:

```json
{
    "Name": "...",
    "Bio": "...",
    "Image": "..."
}
```



### Packages

#### Add Package (Admin) `POST`

```bash
/packages
```

Body:

```json
[
    {"Id":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "Name":"...", "Damage": 10.0}, 
    {"Id":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "Name":"...", "Damage": 10.0}, 
  	{"Id":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "Name":"...", "Damage": 10.0}, 
  	{"Id":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "Name":"...", "Damage": 10.0},
  	{"Id":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "Name":"...", "Damage": 10.0}
]
```



#### Aquire Package `POST`

```bash
/transactions/packages
```



### Cards

#### Get Cards `GET`

```bash
/cards
```



#### Get Deck `GET`

```bash
/deck
```



#### Get Deck Plain Text `GET`

```bash
/deck?format=plain
```



#### Set Deck `PUT`

```bash
/deck
```

Body:

```json
[
	"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
	"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
	"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
	"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
]
```

*Id's of the four card's user wants in his deck.

### Trade

#### List Trades `GET`

```bash
/tradings
```



#### Create Trade `POST`

```bash
/tradings
```

Body:

```json
{
    "Id":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "CardToTrade": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "Type": "Desired Type -> Monster/Spell",
    "MinimumDamage": 10.0
}
```



#### Accept Trade `POST`

```bash
/tradings/{tradeId}
```

Body:

```json
"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
```

*Id of the card user wants to trade



#### Delete Trade `DELETE`

```bash
/tradings/{tradeId}
```



### Battle

#### Start Battle `POST`

```bash
/battles
```



#### List Battles `GET`

```bash
/battles
```



#### Get Battle `GET`

```bash
/battles/{battleId}
```

